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
			cred.Client="208";//TODO: Figure out what the real value needs to be.
			cred.ServiceID="103";//TODO: Figure out what the real value needs to be.
			cred.version="0310";
			return cred;
		}

		///<summary>Shows a message box on error.</summary>
		public static PayConnectService.Status ProcessCreditCard(long paymentNum,decimal amount,string cardNumber,int expYear,int expMonth,string nameOnCard,string securityCode,string zip){
			Program prog=Programs.GetCur(ProgramName.PayConnect);
			PayConnectService.Credentials cred=GetCredentials(prog);
			PayConnectService.creditCardRequest request=new OpenDental.PayConnectService.creditCardRequest();
			request.Amount=amount;
			request.AmountSpecified=true;
			request.CardNumber=cardNumber;
			request.Expiration=new OpenDental.PayConnectService.expiration();
			request.Expiration.year=expYear;
			request.Expiration.month=expMonth;			
			//request.MagData //Needed if we decide to support card swiping.
			request.NameOnCard=nameOnCard;
			request.RefNumber=paymentNum.ToString();
			request.SecurityCode=securityCode;
			request.TransType=PayConnectService.transType.SALE;
			request.Zip=zip;
			PayConnectService.MerchantService ms=new OpenDental.PayConnectService.MerchantService();
			PayConnectService.transResponse response=ms.processCreditCard(cred,request);
			ms.Dispose();
			if(response.Status.code!=0){//Error
				MessageBox.Show(Lan.g("PayConnect","Payment failed")+". "+response.Status.description);
			}
			return response.Status;
		}

	}
}
