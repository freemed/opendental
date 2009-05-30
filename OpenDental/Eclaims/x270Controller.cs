using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenDentBusiness;

namespace OpenDental.Eclaims {
	///<summary>Handles all 270/270 logic.  Contains UI elements.  Passes off the 270 to the correct clearinghouse.</summary>
	public class x270Controller {

		public static void RequestBenefits(Clearinghouse clearhouse,int planNum) {
			//create a 270 message---------------------------------------------------------------
			string x12message=X270.GenerateMessageText();
			EtransMessageText etransMessageText=new EtransMessageText();
			etransMessageText.MessageText=x12message;
			EtransMessageTexts.Insert(etransMessageText);
			//attach it to an etrans-------------------------------------------------------------
			Etrans etrans=new Etrans();
			etrans.DateTimeTrans=DateTime.Now;
			etrans.ClearinghouseNum=clearhouse.ClearinghouseNum;
			etrans.Etype=EtransType.BenefitInquiry270;
			etrans.PlanNum=planNum;
			etrans.EtransMessageTextNum=etransMessageText.EtransMessageTextNum;
			Etranss.Insert(etrans);
			//send the 270----------------------------------------------------------------------
			if(clearhouse.CommBridge==EclaimsCommBridge.ClaimConnect) {
				ClaimConnect.Benefits270(clearhouse);
				
			}
			//process the 271-------------------------------------------------------------------

			//create an etrans for the 271------------------------------------------------------

		}

	}
}
