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
		public static void RequestBenefits(Clearinghouse clearhouse,InsPlan plan,int patNum,Carrier carrier) {
			//throw exception if missing info
			Patient pat=Patients.GetPat(patNum);
			Patient subsc=Patients.GetPat(plan.Subscriber);
			//validation goes here

			//create a 270 message---------------------------------------------------------------
			string x12message=X270.GenerateMessageText();//replace this

			EtransMessageText etransMessageText=new EtransMessageText();
			etransMessageText.MessageText=x12message;
			EtransMessageTexts.Insert(etransMessageText);
			//attach it to an etrans-------------------------------------------------------------
			Etrans etrans=new Etrans();
			etrans.DateTimeTrans=DateTime.Now;
			etrans.ClearinghouseNum=clearhouse.ClearinghouseNum;
			etrans.Etype=EtransType.BenefitInquiry270;
			etrans.PlanNum=plan.PlanNum;
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
				throw ex;
			}
			//start to process the 271----------------------------------------------------------
			X271 x271=null;
			if(X12object.IsX12(x12response)) {
				X12object x12obj=new X12object(x12response);
				if(x12obj.Is271()) {
					x271=new X271(x12response);
				}
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
			etrans271.ClearinghouseNum=clearhouse.ClearinghouseNum;
			etrans271.Etype=EtransType.TextReport;
			if(X12object.IsX12(x12response)) {
				if(x271==null){
					etrans271.Etype=EtransType.Acknowledge_997;
				}
				else{
					etrans271.Etype=EtransType.BenefitResponse271;
				}
			}
			etrans271.PlanNum=plan.PlanNum;
			etrans271.EtransMessageTextNum=etransMessageText.EtransMessageTextNum;
			Etranss.Insert(etrans271);
			etrans.AckEtransNum=etrans271.EtransNum;
			etrans.Note="Normal 271 response.";//change this later to be explanatory of content.
			if(etrans271.Etype==EtransType.Acknowledge_997) {
				etrans.Note="Malformed document sent.  997 error returned.";
			}
			Etranss.Update(etrans);
			//show the user a list of benefits to pick from for import--------------------------
			FormEtrans270Edit formE=new FormEtrans270Edit();
			formE.EtransCur=etrans;
			formE.IsInitialResponse=true;
			formE.ShowDialog();
		}

	}
}
