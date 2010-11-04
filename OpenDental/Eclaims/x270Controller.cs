using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental.Eclaims {
	///<summary>Handles all 270/270 logic.  Contains UI elements.  Passes off the 270 to the correct clearinghouse.</summary>
	public class x270Controller {

		///<summary>The insplan that's passed in need not be properly updated to the database first.</summary>
		public static void RequestBenefits(Clearinghouse clearhouse,InsPlan plan,long patNum,Carrier carrier,List<Benefit> benList,long patPlanNum,InsSub insSub) {
			Patient pat=Patients.GetPat(patNum);
			Patient subsc=Patients.GetPat(insSub.Subscriber);
			Clinic clinic=Clinics.GetClinic(pat.ClinicNum);
			Provider billProv=Providers.GetProv(Providers.GetBillingProvNum(pat.PriProv,pat.ClinicNum));
			//validation.  Throw exception if missing info----------------------------------------
			string validationResult=X270.Validate(clearhouse,carrier,billProv,clinic,plan,subsc,insSub);
			if(validationResult != "") {
				throw new Exception(Lan.g("FormInsPlan","Please fix the following errors first:")+"\r\n"+validationResult);
			}
			//create a 270 message---------------------------------------------------------------
			string x12message=X270.GenerateMessageText(clearhouse,carrier,billProv,clinic,plan,subsc,insSub);
			EtransMessageText etransMessageText=new EtransMessageText();
			etransMessageText.MessageText=x12message;
			EtransMessageTexts.Insert(etransMessageText);
			//attach it to an etrans-------------------------------------------------------------
			Etrans etrans=new Etrans();
			etrans.DateTimeTrans=DateTime.Now;
			etrans.ClearingHouseNum=clearhouse.ClearinghouseNum;
			etrans.Etype=EtransType.BenefitInquiry270;
			etrans.PlanNum=plan.PlanNum;
			etrans.InsSubNum=insSub.InsSubNum;
			etrans.EtransMessageTextNum=etransMessageText.EtransMessageTextNum;
			Etranss.Insert(etrans);
			//send the 270----------------------------------------------------------------------
			string x12response="";
			//a connection error here needs to bubble up
			try {
				if(clearhouse.CommBridge==EclaimsCommBridge.ClaimConnect) {
					x12response=ClaimConnect.Benefits270(clearhouse,x12message);
				}
			}
			catch(Exception ex) {
				EtransMessageTexts.Delete(etrans.EtransMessageTextNum);
				Etranss.Delete(etrans.EtransNum);
				throw new ApplicationException(Lan.g("FormInsPlan","Connection Error:")+"\r\n"+ex.GetType().Name+"\r\n"+ex.Message);
			}
			//start to process the 271----------------------------------------------------------
			X271 x271=null;
			if(X12object.IsX12(x12response)) {
				X12object x12obj=new X12object(x12response);
				if(x12obj.Is271()) {
					x271=new X271(x12response);
				}
			}
			else {//neither a 997 nor a 271
				EtransMessageTexts.Delete(etrans.EtransMessageTextNum);
				Etranss.Delete(etrans.EtransNum);
				throw new ApplicationException(Lan.g("FormInsPlan","Clearinghouse server sent this error:")+"\r\n"+x12response);
			}
			/*
			//In realtime mode, X12 limits the request to one patient.
			//We will always use the subscriber.
			//So all EB segments are for the subscriber.
			List<EB271> listEB=new List<EB271>();
			EB271 eb;
			if(x271 != null) {
				for(int i=0;i<x271.Segments.Count;i++) {
					if(x271.Segments[i].SegmentID != "EB") {
						continue;
					}
					eb=new EB271(x271.Segments[i]);
					listEB.Add(eb);
				}
			}*/
			//create an etrans for the 271------------------------------------------------------
			etransMessageText=new EtransMessageText();
			etransMessageText.MessageText=x12response;
			EtransMessageTexts.Insert(etransMessageText);
			Etrans etrans271=new Etrans();
			etrans271.DateTimeTrans=DateTime.Now;
			etrans271.ClearingHouseNum=clearhouse.ClearinghouseNum;
			etrans271.Etype=EtransType.TextReport;
			if(X12object.IsX12(x12response)) {//this shouldn't need to be tested because it was tested above.
				if(x271==null){
					etrans271.Etype=EtransType.Acknowledge_997;
				}
				else{
					etrans271.Etype=EtransType.BenefitResponse271;
				}
			}
			etrans271.PlanNum=plan.PlanNum;
			etrans271.InsSubNum=insSub.InsSubNum;
			etrans271.EtransMessageTextNum=etransMessageText.EtransMessageTextNum;
			Etranss.Insert(etrans271);
			etrans.AckEtransNum=etrans271.EtransNum;
			if(etrans271.Etype==EtransType.Acknowledge_997) {
				X997 x997=new X997(x12response);
				string error997=x997.GetHumanReadable();
				etrans.Note="Error: "+error997;//"Malformed document sent.  997 error returned.";
				Etranss.Update(etrans);
				MessageBox.Show(etrans.Note);
				//CodeBase.MsgBoxCopyPaste msgbox=new CodeBase.MsgBoxCopyPaste(etrans.Note);
				//msgbox.ShowDialog();
				//don't show the 270 interface.
				return;
			}
			else {
				string processingerror=x271.GetProcessingError();
				if(processingerror != "") {
					etrans.Note=processingerror;
					Etranss.Update(etrans);
					MessageBox.Show(etrans.Note);
					//CodeBase.MsgBoxCopyPaste msgbox=new CodeBase.MsgBoxCopyPaste(etrans.Note);
					//msgbox.ShowDialog();
					//don't show the 270 interface.
					return;
				}
				else {
					etrans.Note="Normal 271 response.";//change this later to be explanatory of content.
				}
			}
			Etranss.Update(etrans);
			//show the user a list of benefits to pick from for import--------------------------
			FormEtrans270Edit formE=new FormEtrans270Edit(patPlanNum,plan.PlanNum,insSub.InsSubNum);
			formE.EtransCur=etrans;
			formE.IsInitialResponse=true;
			formE.benList=benList;
			formE.ShowDialog();
		}

	}
}
