using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace OpenDentBusiness {
	public class X12Validate {
		///<summary>StringBuilder does not get altered if no invalid data.</summary>
		public static void ISA(Clearinghouse clearhouse,StringBuilder strb) {
			if(clearhouse.ISA05!="01" && clearhouse.ISA05!="14" && clearhouse.ISA05!="20" && clearhouse.ISA05!="27" 
				&& clearhouse.ISA05!="28"	&& clearhouse.ISA05!="29" && clearhouse.ISA05!="30" && clearhouse.ISA05!="33"
				&& clearhouse.ISA05!="ZZ")
			{
				Comma(strb);
				strb.Append("Clearinghouse ISA05");
			}
			if(clearhouse.SenderTIN!="") {//if it IS blank, then we'll be using OD's info as the sender, so no need to validate the rest
				if(clearhouse.SenderTIN.Length<2) {
					Comma(strb);
					strb.Append("Clearinghouse SenderTIN");
				}
				if(clearhouse.SenderName=="") {//1000A NM103 min length=1
					Comma(strb);
					strb.Append("Clearinghouse Sender Name");
				}
				if(!Regex.IsMatch(clearhouse.SenderTelephone,@"^\d{10}$")) {//1000A PER04 min length=1
					Comma(strb);
					strb.Append("Clearinghouse Sender Phone");
				}
			}
			if(clearhouse.ISA07!="01" && clearhouse.ISA07!="14" && clearhouse.ISA07!="20" && clearhouse.ISA07!="27" 
				&& clearhouse.ISA07!="28"	&& clearhouse.ISA07!="29" && clearhouse.ISA07!="30" && clearhouse.ISA07!="33"
				&& clearhouse.ISA07!="ZZ") 
			{
				Comma(strb);
				strb.Append("Clearinghouse ISA07");
			}
			if(clearhouse.ISA08.Length<2) {
				Comma(strb);
				strb.Append("Clearinghouse ISA08");
			}
			if(clearhouse.ISA15!="T" && clearhouse.ISA15!="P") {
				Comma(strb);
				strb.Append("Clearinghouse ISA15");
			}
		}

		///<summary>StringBuilder does not get altered if no invalid data.</summary>
		public static void Carrier(Carrier carrier,StringBuilder strb) {
			if(carrier.Address=="") {
				Comma(strb);
				strb.Append("Carrier Address");
			}
			if(carrier.City.Length<2) {
				Comma(strb);
				strb.Append("Carrier City");
			}
			if(carrier.State.Length!=2) {
				Comma(strb);
				strb.Append("Carrier State(2 char)");
			}
			if(carrier.Zip.Length<3) {
				Comma(strb);
				strb.Append("Carrier Zip");
			}
		}

		///<summary>StringBuilder does not get altered if no invalid data.</summary>
		public static void BillProv(Provider billProv,StringBuilder strb) {
			if(billProv.LName=="") {
				Comma(strb);
				strb.Append("Billing Prov LName");
			}
			if(!billProv.IsNotPerson && billProv.FName==""){//if is a person, first name cannot be blank.
				Comma(strb);
				strb.Append("Billing Prov FName");
			}
			if(billProv.SSN.Length<2) {
				Comma(strb);
				strb.Append("Billing Prov SSN");
			}
			//if(billProv.NationalProvID.Length<2) {
			//  Comma(strb);
			//  strb.Append("Billing Prov NPI");
			//}
			if(!Regex.IsMatch(billProv.NationalProvID,"^(80840)?[0-9]{10}$")) {
				Comma(strb);
				strb.Append("Billing Prov NPI must be a 10 digit number with an optional prefix of 80840");
			}
			if(billProv.TaxonomyCodeOverride.Length>0 && billProv.TaxonomyCodeOverride.Length!=10) {
				Comma(strb);
				strb.Append("Billing Prov Taxonomy Code must be 10 characters");
			}
			/* This was causing problems when dummy providers were used for office and no license number was applicable.
			 * Delta carriers key off this number and start assigning to wrong provider. Customer: ATD.
			if(billProv.StateLicense=="") {
				if(strb.Length!=0) {
					strb.Append(",");
				}
				strb.Append("Billing Prov Lic #");
			}*/
		}

		public static void PracticeAddress(StringBuilder strb) {
			if(PrefC.GetString(PrefName.PracticePhone).Length!=10) {
				//10 digit phone is required by WebMD and is universally assumed 
				Comma(strb);
				strb.Append("Practice Phone");
			}
			if(PrefC.GetString(PrefName.PracticeAddress)=="") {
				Comma(strb);
				strb.Append("Practice Address");
			}
			if(PrefC.GetString(PrefName.PracticeCity).Length<2) {
				Comma(strb);
				strb.Append("Practice City");
			}
			if(PrefC.GetString(PrefName.PracticeST).Length!=2) {
				Comma(strb);
				strb.Append("Practice State(2 char)");
			}
			if(PrefC.GetString(PrefName.PracticeZip).Length<3) {
				Comma(strb);
				strb.Append("Practice Zip");
			}
		}

		public static void BillingAddress(StringBuilder strb) {
			if(PrefC.GetString(PrefName.PracticePhone).Length!=10) { //There is no billing phone, so the practice phone is sent electronically.
				//10 digit phone is required by WebMD and is universally assumed 
				Comma(strb);
				strb.Append("Practice Phone");
			}
			if(PrefC.GetString(PrefName.PracticeBillingAddress)=="") {
				Comma(strb);
				strb.Append("Billing Address");
			}
			if(PrefC.GetString(PrefName.PracticeBillingCity).Length<2) {
				Comma(strb);
				strb.Append("Billing City");
			}
			if(PrefC.GetString(PrefName.PracticeBillingST).Length!=2) {
				Comma(strb);
				strb.Append("Billing State(2 char)");
			}
			if(PrefC.GetString(PrefName.PracticeBillingZip).Length<3) {
				Comma(strb);
				strb.Append("Billing Zip");
			}
		}

		///<summary>Clinic passed in must not be null.</summary>
		public static void Clinic(Clinic clinic,StringBuilder strb) {
			if(clinic.Phone.Length!=10) {//1000A PER04 min length=1.
				//But 10 digit phone is required in 2010AA and is universally assumed 
				Comma(strb);
				strb.Append("Clinic Phone");
			}
			if(clinic.Address=="") {
				Comma(strb);
				strb.Append("Clinic Address");
			}
			if(clinic.City.Length<2) {
				Comma(strb);
				strb.Append("Clinic City");
			}
			if(clinic.State.Length!=2) {
				Comma(strb);
				strb.Append("Clinic State(2 char)");
			}
			if(clinic.Zip.Length<3) {
				Comma(strb);
				strb.Append("Clinic Zip");
			}
		}

		private static void Comma(StringBuilder strb){
			if(strb.Length!=0) {
				strb.Append(",");
			}
		}




	}
}
