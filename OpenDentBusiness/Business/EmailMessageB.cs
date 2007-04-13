using System;
using System.Data;
using System.Collections.Generic;
using System.Text;

namespace OpenDentBusiness {
	public class EmailMessageB{
		///<summary></summary>
		public static int Insert(EmailMessage message){
			if(PrefB.RandomKeys) {
				message.EmailMessageNum=MiscDataB.GetKey("emailmessage","EmailMessageNum");
			}
			string command="INSERT INTO emailmessage (";
			if(PrefB.RandomKeys) {
				command+="EmailMessageNum,";
			}
			command+="PatNum,ToAddress,FromAddress,Subject,BodyText,"
				+"MsgDateTime,SentOrReceived) VALUES(";
			if(PrefB.RandomKeys) {
				command+="'"+POut.PInt(message.EmailMessageNum)+"', ";
			}
			command+=
				 "'"+POut.PInt(message.PatNum)+"', "
				+"'"+POut.PString(message.ToAddress)+"', "
				+"'"+POut.PString(message.FromAddress)+"', "
				+"'"+POut.PString(message.Subject)+"', "
				+"'"+POut.PString(message.BodyText)+"', "
				+POut.PDateT(message.MsgDateTime)+", "
				+"'"+POut.PInt((int)message.SentOrReceived)+"')";
			DataConnection dcon=new DataConnection();
			if(PrefB.RandomKeys) {
				dcon.NonQ(command);
			}
			else {
				dcon.NonQ(command,true);
				message.EmailMessageNum=dcon.InsertID;
			}
			//now, insert all the attaches.
			for(int i=0;i<message.Attachments.Count;i++) {
				message.Attachments[i].EmailMessageNum=message.EmailMessageNum;
				EmailAttachB.Insert(message.Attachments[i]);
			}
			return message.EmailMessageNum;
		}

		///<summary></summary>
		public static int Update(EmailMessage message){
			string command="UPDATE emailmessage SET "
				+ "PatNum = '"      +POut.PInt(message.PatNum)+"' "
				+ ",ToAddress = '"  +POut.PString(message.ToAddress)+"' "
				+ ",FromAddress = '"+POut.PString(message.FromAddress)+"' "
				+ ",Subject = '"    +POut.PString(message.Subject)+"' "
				+ ",BodyText = '"   +POut.PString(message.BodyText)+"' "
				+ ",MsgDateTime = "+POut.PDateT (message.MsgDateTime)+" "
				+ ",SentOrReceived = '"+POut.PInt((int)message.SentOrReceived)+"' "
				+"WHERE EmailMessageNum = "+POut.PInt(message.EmailMessageNum);
			DataConnection dcon=new DataConnection();
			dcon.NonQ(command);
			//now, delete all attachments and recreate.
			command="DELETE FROM emailattach WHERE EmailMessageNum="+POut.PInt(message.EmailMessageNum);
			dcon.NonQ(command);
			for(int i=0;i<message.Attachments.Count;i++){
				message.Attachments[i].EmailMessageNum=message.EmailMessageNum;
				EmailAttachB.Insert(message.Attachments[i]);
			}
			return 0;
		}
		
		






	}



}
