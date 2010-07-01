using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental.Eclaims {
	/// <summary> The idea is to make reading a CCD message in each of the different forms a smaller amount of overall typing, to save time and reduce bugs.</summary>
	public class CCDFieldInputter{
		private List<CCDField> fieldList = new List<CCDField>();//List of fields that make up the message
		public bool isVersion2;
		public string messageText="";

		public CCDFieldInputter(){
		}

		public CCDFieldInputter(string message){
			messageText=message;
			string version=message.Substring(18,2);
			string msgType=message.Substring(20,2);
			if(version=="04") {
				switch(msgType) {
					case "11":
						ParseClaimAck_11(message);
						break;
					case "21":
						ParseEOB_21(message);
						break;
					case "12":
						ParseClaimReversalResponse_12(message);
						break;
					case "18":
						ParseResponseToElegibility_18(message);
						break;
					case "24":
						ParseEmailResponse_24(message);
						break;
					case "14":
						ParseOutstandingTransactionsAck_14(message);
						break;
					case "23":
						ParsePredeterminationEOB_23(message);
						break;
					case "13":
						ParsePredeterminationAck_13(message);
						break;
					case "16":
						ParsePaymentReconciliation_16(message);
						break;
					case "15":
						ParseReconciliaiton_15(message);
						break;
					default:
						throw new ApplicationException(this.ToString()+".CCDFieldInputter: Version 4 CCD Message type not recognized: "+msgType);
				}
			}
			else {//version 02
				isVersion2=true;
				switch(msgType) {
					case "10"://eligibility response
						ParseElegibilityResponse_v2_10(message);
						break;
					case "11"://claim response
						ParseClaimResponse_v2_11(message);
						break;
					case "21"://eob
						ParseEOB_v2_21(message);
						break;
					case "12"://reversal response
						ParseClaimReversalResponse_v2_12(message);
						break;
					case "13"://response to predetermination
						ParsePredeterminationAck_v2_13(message);
						break;
					default:
						throw new ApplicationException(this.ToString()+".CCDFieldInputter: Version 2 CCD Message type not recognized: "+msgType);
				}
			}
		}

		public CCDField[] GetLoadedFields(){
			CCDField[] loadedFields=new CCDField[fieldList.Count];
			fieldList.CopyTo(loadedFields);
			return loadedFields;
		}

		/// <summary>Input a single field.<summary>
		public string InputField(string message,string fieldId){
			CCDField field=new CCDField(fieldId,isVersion2);
			int len=field.GetRequiredLength(this);
			if(len<0 || message==null || message.Length<len){
				return null;
			}
			if(len==0){
				return message;
			}
			string substr=message.Substring(0,len);
			if(!field.CheckValue(this,substr)){
				throw new ApplicationException("Invalid value for CCD message field '"+field.fieldName+"'"+((substr==null)?"":(": "+substr)));
			}
			field.valuestr=substr;
			fieldList.Add(field);
			return message.Substring(substr.Length,message.Length-substr.Length);//Skip text that has already been read in.
		}

		///<summary>Inputs fields based on a pseudo-script input string. This is possible, since each field id identifies a unique 
		///input pattern. If a field Id is seen, then that field is inputted, but if the ### sequence is encountered, then a field 
		///list is inputted based on the number in the next specified field, which has the format of the field after that. If the
		///input string leads with a "#" followed by a 2 digit number, then the string is input that many times.</summary>
		public string InputFields(string message,string fieldOrderStr) {
			fieldOrderStr=fieldOrderStr.ToUpper();
			if(fieldOrderStr.Length%3!=0) {
				throw new ApplicationException("Internal error, invalid field order string (not divisible by 3): "+fieldOrderStr);
			}
			if(fieldOrderStr.Length<1){
				return message;
			}
			for(int i=0;i<fieldOrderStr.Length;i+=3) {
				string token=fieldOrderStr.Substring(i,3);
				if(token=="###") {//Input field value by field id, then input the value number of fields with the next template.
					string valueFieldId=fieldOrderStr.Substring(i+3,3);
					if(valueFieldId==null||valueFieldId.Length!=3) {
						throw new ApplicationException("Internal error, invalid value field id in: "+fieldOrderStr);
					}
					CCDField valueField=GetFieldById(valueFieldId);
					if(valueField==null) {
						throw new ApplicationException(this.ToString()+".InputCCDFields: Internal error, could not locate value field '"+valueFieldId+"'");
					}
					if(valueField.format!="N") {
						throw new ApplicationException(this.ToString()+".InputCCDFields: Internal error, value field '"+valueFieldId+"' is not an integer");
					}
					string listFieldId=fieldOrderStr.Substring(i+6,3);
					if(listFieldId==null||listFieldId.Length!=3) {
						throw new ApplicationException("Internal error, invalid field list id in: "+fieldOrderStr);
					}
					i+=6;
					for(int p=0;p<Convert.ToInt32(valueField.valuestr);p++) {
						message=InputField(message,listFieldId);
					}
				}
				else {//Input a single field.
					message=InputField(message,token);
				}
			}
			return message;
		}

		///<summary>Get a list of loaded fields by a common field id.<summary>
		public CCDField[] GetFieldsById(string fieldId){
			//lists are short, so just use a simple list search.
			List<CCDField> fields=new List<CCDField>();
			foreach(CCDField field in fieldList){
				if(field.fieldId==fieldId){
					fields.Add(field);//(new CCDField(field,isVersion2));
				}
			}
			return fields.ToArray();
		}

		///<summary>Same as GetFieldsById, but gets only a single field, or returns field with empty value if there are multiple.</summary>
		public CCDField GetFieldById(string fieldId){
			CCDField[] fields=GetFieldsById(fieldId);
			if(fields==null || fields.Length==0) {
				return null;
			}
			if(fields.Length>1) {
				throw new ApplicationException("Internal error, invalid use of ambiguous CCD field id"+((fieldId==null)?"":(": "+fieldId)));
			}
			return fields[0];
		}

		public string GetValue(string fieldId) {
			CCDField[] fields=GetFieldsById(fieldId);
			if(fields==null || fields.Length==0) {
				return "";//Doesn't exist, return with empty value, so at least some information can be used.
			}
			if(fields.Length>1) {
				throw new ApplicationException("Internal error, invalid use of ambiguous CCD field id"+((fieldId==null)?"":(": "+fieldId)));
			}
			return fields[0].valuestr;
		}

		private void ParseClaimAck_11(string message) {
			message=this.InputFields(message,
																		"A01A02A03A04A05A07A11B01B02G01G05G06G07G04G27G31G39"+
																		"###G31G32"+//Read field G32 the number of times equal to the value in G31.
																		"G42");
			if(message==null) {
				return;//error, but print what we have.
			}
			CCDField fieldG06=this.GetFieldById("G06");
			if(fieldG06==null) {
				return;//error
			}
			if(fieldG06.format!="N") {
				MessageBox.Show(this.ToString()+"PrintClaimAck_11: Internal error, field G06 is not an integer");
				return;//error
			}
			for(int i=0;i<Convert.ToInt32(fieldG06.valuestr);i++) {//Input a list of sub-records.
				message=this.InputFields(message,"F07G08");
				if(message==null) {
					return;//error
				}
			}
			message=this.InputField(message,"G40");
		}

		private void ParseEOB_21(string message) {
			message=this.InputFields(message,"A01A02A03A04A05A07A11B01B02G01G03G04G27F06G10G11G28G29G30F01G33G55G39G42");
			CCDField fieldF06=this.GetFieldById("F06");
			if(fieldF06==null) {
				return;//error
			}
			if(fieldF06.format!="N") {
				MessageBox.Show(this.ToString()+".PrintEOB_21: Internal error, field F06 is not of integer type!");
				return;
			}
			for(int i=0;i<Convert.ToInt32(fieldF06.valuestr);i++) {
				message=this.InputFields(message,"F07G12G13G14G15G43G56G57G58G02G59G60G61G16G17");
			}
			CCDField fieldG10=this.GetFieldById("G10");
			if(fieldG10==null) {
				return;//error	
			}
			if(fieldG10.format!="N") {
				MessageBox.Show(this.ToString()+".PrintEOB_21: Internal error, field G10 is not of integer type!");
				return;//error
			}
			for(int i=0;i<Convert.ToInt32(fieldG10.valuestr);i++) {
				message=this.InputFields(message,"G18G19G20G44G21G22G23G24G25");
			}
			CCDField fieldG11=this.GetFieldById("G11");
			if(fieldG11==null) {
				return;//error
			}
			if(fieldG11.format!="N") {
				MessageBox.Show(this.ToString()+".PrintEOB_21: Internal error, field G11 is not of integer type!");
				return;//error
			}
			for(int i=0;i<Convert.ToInt32(fieldG11.valuestr);i++) {
				message=this.InputFields(message,"G41G45G26");
			}
			message=this.InputFields(message,"G40");
		}

		private void ParseClaimReversalResponse_12(string message) {			
			message=this.InputFields(message,"A01A02A03A04A05A07A11B01B02E19G01G05G06G07G04G31"+
																					"###G31G32"+
																					"###G06G08");
			return;
		}

		private void ParseResponseToElegibility_18(string message) {
			message=this.InputFields(message,"A01A02A03A04A05A07A11B01B02G01G05G06G07G31G42"+
																					"###G06G08"+
																					"###G31G32");
			return;
		}

		private void ParseEmailResponse_24(string message) {			
			message=this.InputFields(message,"A01A02A03A04A07A11G48G54G49G50G51G52"+
																					"###G52G53");
			return;
		}

		private void ParseOutstandingTransactionsAck_14(string message) {			
			message=this.InputFields(message,"A01A02A03A04A05A07A11B01B02B03G05G06G07"+
																					"###G06G08");
			return;
		}

		private void ParsePredeterminationEOB_23(string message) {
			message=this.InputFields(message,"A01A02A03A04A05A07A11B01B02G01G04G27F06G10G11G28G29G30G39G42G46G47");
			CCDField fieldF06=this.GetFieldById("F06");
			if(fieldF06==null) {
				return;//error, but return as much of the form as we were able to understand.
			}
			if(fieldF06.format!="N") {
				MessageBox.Show(this.ToString()+".ParsePredeterminationEOB_23: Internal error, field F06 is not of integer type!");
				return;//error
			}
			for(int i=0;i<Convert.ToInt32(fieldF06.valuestr);i++) {
				message=this.InputFields(message,"F07G12G13G14G15G43G56G57G58G02G59G60G61G16G17");
			}
			CCDField fieldG10=this.GetFieldById("G10");
			if(fieldG10==null) {
				return;//error
			}
			if(fieldG10.format!="N") {
				MessageBox.Show(this.ToString()+".ParsePredeterminationEOB_23: Internal error, field G10 is not of integer type!");
				return;//error
			}
			for(int i=0;i<Convert.ToInt32(fieldG10.valuestr);i++) {
				message=this.InputFields(message,"G18G19G20G44G21G22G23G24G25");
			}
			CCDField fieldG11=this.GetFieldById("G11");
			if(fieldG11==null) {
				return;//error
			}
			if(fieldG11.format!="N") {
				MessageBox.Show(this.ToString()+".ParsePredeterminationEOB_23: Internal error, field G11 is not of integer type!");
				return;//error
			}
			for(int i=0;i<Convert.ToInt32(fieldG11.valuestr);i++) {
				message=this.InputFields(message,"G41G45G26");
			}
			message=this.InputFields(message,"G40");
			return;
		}

		private void ParsePredeterminationAck_13(string message) {			
			message=this.InputFields(message,"A01A02A03A04A05A07A11B01B02G01G05G06G07G04G27G31G39"+
																					"###G31G32"+
																					"G42G46G47");
			CCDField fieldG06=this.GetFieldById("G06");
			if(fieldG06==null) {
				return;//error
			}
			if(fieldG06.format!="N") {
				MessageBox.Show(this.ToString()+".ParsePredeterminationAck_13: Internal error, field G06 is not of integer type!");
				return;//error
			}
			for(int i=0;i<Convert.ToInt32(fieldG06.valuestr);i++) {
				message=this.InputFields(message,"F07G08");
			}
			message=this.InputFields(message,"G40");
			return;
		}

		private void ParsePaymentReconciliation_16(string message) {			
			message=this.InputFields(message,"A01A02A03A04A07A11B04G01G05G06G11G34G35G36G37G33F38G62");
			CCDField fieldG37=this.GetFieldById("G37");
			if(fieldG37==null) {
				return;//error
			}
			if(fieldG37.format!="N") {
				MessageBox.Show(this.ToString()+".ParsePaymentReconciliation_16: Internal error, field G37 is not of integer type!");
				return;//error
			}
			for(int i=0;i<Convert.ToInt32(fieldG37.valuestr);i++) {
				message=this.InputFields(message,"B01B02B03A05A02G01G38");
			}
			CCDField fieldG11=this.GetFieldById("G11");
			if(fieldG11==null) {
				return;//error
			}
			if(fieldG11.format!="N") {
				MessageBox.Show(this.ToString()+".ParsePaymentReconciliation_16: Internal error, field G11 is not of integer type!");
				return;//error
			}
			for(int i=0;i<Convert.ToInt32(fieldG11.valuestr);i++) {
				message=this.InputFields(message,"G41G26");
			}
			message=this.InputFields(message,"###G06G08");
			return;
		}

		private void ParseReconciliaiton_15(string message) {			
			message=this.InputFields(message,"A01A02A03A04A07A11B02G01G05G06G11G34G35G36G37G33");
			CCDField fieldG37=this.GetFieldById("G37");
			if(fieldG37==null) {
				return;//error
			}
			if(fieldG37.format!="N") {
				MessageBox.Show(this.ToString()+".ParseReconciliaiton_15: Internal error, field G37 is not of integer type!");
				return;//error
			}
			for(int i=0;i<Convert.ToInt32(fieldG37.valuestr);i++) {
				message=this.InputFields(message,"B01A05A02G01G38");
			}
			CCDField fieldG11=this.GetFieldById("G11");
			if(fieldG11==null) {
				return;//error
			}
			if(fieldG11.format!="N") {
				MessageBox.Show(this.ToString()+".ParseReconciliaiton_15: Internal error, field G11 is not of integer type!");
				return;//error
			}
			for(int i=0;i<Convert.ToInt32(fieldG11.valuestr);i++) {
				message=this.InputFields(message,"G41G26");
			}
			message=this.InputFields(message,"###G06G08");
			return;
		}

		private void ParseElegibilityResponse_v2_10(string message) {
			message=this.InputFields(message,"A01A02A03A04A05A07B01B02G01G05G06G07G02"+
																				"###G06G08");
			return;
		}

		private void ParseClaimResponse_v2_11(string message){
			message=this.InputFields(message,"A01A02A03A04A05A07B01B02G01G05G06G07G02G04G27");
			CCDField fieldG06=this.GetFieldById("G06");
			if(fieldG06==null) {
				return;//error
			}
			if(fieldG06.format!="N") {
				MessageBox.Show(this.ToString()+".ParseClaimResponse_v2_11: Internal error, field G06 is not of integer type!");
				return;//error
			}
			for(int i=0;i<Convert.ToInt32(fieldG06.valuestr);i++) {
				message=this.InputFields(message,"F07G08");
			}
		}

		private void ParseEOB_v2_21(string message){
			message=this.InputFields(message,"A01A02A03A04A05A07B01B02G01G03G04G27G09F06G10G11G28G29G30");
			CCDField fieldF06=this.GetFieldById("F06");
			if(fieldF06==null) {
				return;//error
			}
			if(fieldF06.format!="N") {
				MessageBox.Show(this.ToString()+".ParseEOB_v2_21: Internal error, field F06 is not of integer type!");
				return;//error
			}
			for(int i=0;i<Convert.ToInt32(fieldF06.valuestr);i++) {
				message=this.InputFields(message,"F07G12G13G14G15G16G17");
			}
			CCDField fieldG10=this.GetFieldById("G10");
			if(fieldG10==null) {
				return;//error
			}
			if(fieldG10.format!="N") {
				MessageBox.Show(this.ToString()+".ParseEOB_v2_21: Internal error, field G10 is not of integer type!");
				return;//error
			}
			for(int i=0;i<Convert.ToInt32(fieldG10.valuestr);i++) {
				message=this.InputFields(message,"G18G19G20G21G22G23G24G25");
			}
			message=this.InputFields(message,"###G11G26");
		}

		private void ParseClaimReversalResponse_v2_12(string message){
			message=this.InputFields(message,"A01A02A03A04A05A07B01B02G01G05G06G07G04###G06G08");
		}

		private void ParsePredeterminationAck_v2_13(string message){
			message=this.InputFields(message,"A01A02A03A04A05A07B01B02G01G05G06G07G02G04");
			CCDField fieldG06=this.GetFieldById("G06");
			if(fieldG06==null) {
				return;//error
			}
			if(fieldG06.format!="N") {
				MessageBox.Show(this.ToString()+".ParsePredeterminationAck_v2_13: Internal error, field G06 is not of integer type!");
				return;//error
			}
			for(int i=0;i<Convert.ToInt32(fieldG06.valuestr);i++) {
				message=this.InputFields(message,"F07G08");
			}
		}

		///<summary>Probably some missing types.  Mostly focussed on response types.</summary>
		public EtransType GetEtransType() {
			string msgType=GetValue("A04");
			if(!isVersion2) {//version 4
				switch(msgType) {
					case "11":
						return EtransType.ClaimAck_CA;
					case "21":
						return EtransType.ClaimEOB_CA;
					case "12":
						return EtransType.ReverseResponse_CA;
					case "18":
						return EtransType.EligResponse_CA;
					case "24":
						return EtransType.EmailResponse_CA;
					case "14":
						return EtransType.OutstandingAck_CA;
					case "23":
						return EtransType.PredetermEOB_CA;
					case "13":
						return EtransType.PredetermAck_CA;
					case "16":
						return EtransType.PaymentResponse_CA;
					case "15":
						return EtransType.SummaryResponse_CA;
					default:
						throw new ApplicationException("Version 4 message type not recognized: "+msgType);
				}
			}
			else {//version 02
				switch(msgType) {
					case "10"://eligibility response
						return EtransType.EligResponse_CA;
					case "11"://claim response
						return EtransType.ClaimAck_CA;
					case "21"://eob
						return EtransType.ClaimEOB_CA;
					case "12"://reversal response
						return EtransType.ReverseResponse_CA;
					case "13"://response to predetermination
						return EtransType.PredetermAck_CA;
					default:
						throw new ApplicationException("Version 2 message type not recognized: "+msgType);
				}
			}
		}

	}
}