using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental.Bridges {
	public class PayConnect {

		private static PayConnectService.Credentials GetCredentials(Program prog){
			PayConnectService.Credentials cred=new OpenDental.PayConnectService.Credentials();
			ArrayList props=ProgramProperties.GetForProgram(prog.ProgramNum);
			cred.Username="";
			cred.Password="";
			for(int i=0;i<props.Count;i++){
				ProgramProperty prop=(ProgramProperty)props[i];
				if(prop.PropertyDesc=="Username"){
					cred.Username=prop.PropertyValue;
				}else if(prop.PropertyDesc=="Password"){
					cred.Password=prop.PropertyValue;
				}
			}
			cred.Client="OpenDental2";
			cred.ServiceID="DCI Web Service ID: 006328";//Production
			//cred.ServiceID="DCI Web Service ID: 002778";//Testing
			cred.version="0310";
			return cred;
		}

		public static PayConnectService.creditCardRequest BuildSaleRequest(decimal amount,string cardNumber,int expYear,int expMonth,string nameOnCard,string securityCode,string zip,string magData,PayConnectService.transType transtype,string refNumber) {
			PayConnectService.creditCardRequest request=new OpenDental.PayConnectService.creditCardRequest();
			request.Amount=amount;
			request.AmountSpecified=true;
			request.CardNumber=cardNumber;
			request.Expiration=new OpenDental.PayConnectService.expiration();
			request.Expiration.year=expYear;
			request.Expiration.month=expMonth;
			if(magData!=null) { //MagData is the data returned from magnetic card readers. Will only be present if a card was swiped.
				request.MagData=magData;
			}
			request.NameOnCard=nameOnCard;
			request.RefNumber=refNumber;
			request.SecurityCode=securityCode;
			request.TransType=transtype;
			request.Zip=zip;
			return request;
		}

		///<summary>Shows a message box on error.</summary>
		public static PayConnectService.transResponse ProcessCreditCard(PayConnectService.creditCardRequest request) {
			try{
				Program prog=Programs.GetCur(ProgramName.PayConnect);
				PayConnectService.Credentials cred=GetCredentials(prog);
				PayConnectService.MerchantService ms=new OpenDental.PayConnectService.MerchantService();
				PayConnectService.transResponse response=ms.processCreditCard(cred,request);
				ms.Dispose();
				if(response.Status.code!=0){//Error
					MessageBox.Show(Lan.g("PayConnect","Payment failed")+". "+response.Status.description);
				}
				return response;
			}catch(Exception ex){
				MessageBox.Show(Lan.g("PayConnect","Payment failed")+". "+ex.Message);
			}
			return null;
		}

	}
}
