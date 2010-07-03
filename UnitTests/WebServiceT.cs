using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Text;
using OpenDentBusiness;

namespace UnitTests {
	public class WebServiceT {
		/// <summary></summary>
		public static string RunAll() {
			string retVal="";
			//GetString
			string strResult=WebServiceTests.GetString("Input");
			if(strResult!="Input-Processed"){
				throw new Exception("Should be Input-Processed");
			}
			retVal+="GetString: Passed.\r\n";
			strResult=WebServiceTests.GetStringNull("Input");
			if(strResult!=null){
				throw new Exception("Should be null");
			}
			retVal+="GetStringNull: Passed.\r\n";
			//GetInt
			int intResult=WebServiceTests.GetInt(1);
			if(intResult!=2){
				throw new Exception("Should be 2");
			}
			retVal+="GetInt: Passed.\r\n";
			//GetLong
			long longResult=WebServiceTests.GetLong(1);
			if(longResult!=2){
				throw new Exception("Should be 2");
			}
			retVal+="GetLong: Passed.\r\n";
			//GetVoid
			WebServiceTests.GetVoid();
			retVal+="GetVoid: Passed.\r\n";
			//GetBool
			bool boolResult=WebServiceTests.GetBool();
			if(boolResult!=true){
				throw new Exception("Should be true");
			}
			retVal+="GetBool: Passed.\r\n";
			//GetObject
			Patient pat=WebServiceTests.GetObjectPat();
			if(pat.LName!="Smith"){
				throw new Exception("Should be Smith");
			}
			if(pat.FName!=null){
				throw new Exception("Should be null");
			}
			retVal+="GetObjectPat: Passed.\r\n";
			//GetTable
			DataTable table=WebServiceTests.GetTable();
			if(table.Rows[0][0].ToString()!="cell00"){
				throw new Exception("Should be cell00");
			}
			retVal+="GetTable: Passed.\r\n";
			//GetDataSet
			DataSet ds=WebServiceTests.GetDataSet();
			if(ds.Tables[0].TableName!="table0"){
				throw new Exception("Should be table0");
			}
			retVal+="GetDataSet: Passed.\r\n";
			//GetList
			List<int> listInt=WebServiceTests.GetListInt();
			if(listInt[0]!=2){
				throw new Exception("Should be 2");
			}
			retVal+="GetListInt: Passed.\r\n";
			//GetArrayPatient
			Patient[] arrayPat=WebServiceTests.GetArrayPatient();
			if(arrayPat[0].LName!="Jones"){
				throw new Exception("Should be Jones");
			}
			if(arrayPat[1]!=null){
				throw new Exception("Should be null");
			}
			retVal+="GetArrayPatient: Passed.\r\n";
			//SendNullParam
			strResult=WebServiceTests.SendNullParam(null);
			if(strResult!="nullOK"){
				throw new Exception("Should be nullOK");
			}
			retVal+="SendNullParam: Passed.\r\n";
			//GetObjectNull
			Patient pat2=WebServiceTests.GetObjectNull();
			if(pat2!=null){
				throw new Exception("Should be null");
			}
			retVal+="GetObjectNull: Passed.\r\n";
			//SendColorParam
			Color colorResult=WebServiceTests.SendColorParam(Color.Fuchsia);
			if(colorResult.ToArgb()!=Color.Green.ToArgb()) {
				throw new Exception("Should be green.");
			}
			retVal+="SendColorParam: Passed.\r\n";
			//SendProviderColor
			Provider prov=new Provider();
			prov.ProvColor=Color.Fuchsia;
			strResult=WebServiceTests.SendProviderColor(prov);
			if(strResult!="fuchsiaOK") {
				throw new Exception("Should be fuchsiaOK.");
			}
			retVal+="SendProviderColor: Passed.\r\n";
			//SendSheetParameter
			SheetParameter sheetParam=new SheetParameter(false,"ParamNameOK");
			strResult=WebServiceTests.SendSheetParameter(sheetParam);
			if(strResult!="paramNameOK") {
				throw new Exception("Should be paramNameOK.");
			}
			retVal+="SendSheetParameter: Passed.\r\n";
			//SendSheetWithFields
			Sheet sheet=new Sheet();
			sheet.SheetFields=new List<SheetField>();
			sheet.Parameters=new List<SheetParameter>();
			SheetField field=new SheetField();
			field.FieldName="FieldNameGreen";
			sheet.SheetFields.Add(field);
			strResult=WebServiceTests.SendSheetWithFields(sheet);
			if(strResult!="fieldOK") {
				throw new Exception("Should be fieldOK.");
			}
			retVal+="SendSheetWithFields: Passed.\r\n";
			
			

			
			return retVal;
		}
	}
}
