using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text.RegularExpressions;

namespace OpenDentBusiness{
	///<summary>An email message is always attached to a patient.</summary>
	public class EmailMessages{
		///<summary>Gets one email message from the database.</summary>
		public static EmailMessage GetOne(long msgNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<EmailMessage>(MethodBase.GetCurrentMethod(),msgNum);
			}
			return Crud.EmailMessageCrud.SelectOne(msgNum);
		}

		///<summary></summary>
		public static void Update(EmailMessage message){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),message);
				return;
			}
			string command="UPDATE emailmessage SET "
				+ "PatNum = '"      +POut.Long(message.PatNum)+"' "
				+ ",ToAddress = '"  +POut.String(message.ToAddress)+"' "
				+ ",FromAddress = '"+POut.String(message.FromAddress)+"' "
				+ ",Subject = '"    +POut.String(message.Subject)+"' "
				+ ",BodyText = '"   +POut.String(message.BodyText)+"' "
				+ ",MsgDateTime = "+POut.DateT(message.MsgDateTime)+" "
				+ ",SentOrReceived = '"+POut.Long((int)message.SentOrReceived)+"' "
				+"WHERE EmailMessageNum = "+POut.Long(message.EmailMessageNum);
			Db.NonQ(command);
			//now, delete all attachments and recreate.
			command="DELETE FROM emailattach WHERE EmailMessageNum="+POut.Long(message.EmailMessageNum);
			Db.NonQ(command);
			for(int i=0;i<message.Attachments.Count;i++) {
				message.Attachments[i].EmailMessageNum=message.EmailMessageNum;
				EmailAttaches.Insert(message.Attachments[i]);
			}
		}

		///<summary></summary>
		public static long Insert(EmailMessage message) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				message.EmailMessageNum=Meth.GetLong(MethodBase.GetCurrentMethod(),message);
				return message.EmailMessageNum;
			}
			if(PrefC.RandomKeys) {
				message.EmailMessageNum=ReplicationServers.GetKey("emailmessage","EmailMessageNum");
			}
			string command="INSERT INTO emailmessage (";
			if(PrefC.RandomKeys) {
				command+="EmailMessageNum,";
			}
			command+="PatNum,ToAddress,FromAddress,Subject,BodyText,"
				+"MsgDateTime,SentOrReceived) VALUES(";
			if(PrefC.RandomKeys) {
				command+="'"+POut.Long(message.EmailMessageNum)+"', ";
			}
			command+=
				 "'"+POut.Long(message.PatNum)+"', "
				+"'"+POut.String(message.ToAddress)+"', "
				+"'"+POut.String(message.FromAddress)+"', "
				+"'"+POut.String(message.Subject)+"', "
				+"'"+POut.String(message.BodyText)+"', "
				+POut.DateT(message.MsgDateTime)+", "
				+"'"+POut.Long((int)message.SentOrReceived)+"')";
			if(PrefC.RandomKeys) {
				Db.NonQ(command);
			}
			else {
				message.EmailMessageNum=Db.NonQ(command,true);
			}
			//now, insert all the attaches.
			for(int i=0;i<message.Attachments.Count;i++) {
				message.Attachments[i].EmailMessageNum=message.EmailMessageNum;
				EmailAttaches.Insert(message.Attachments[i]);
			}
			return message.EmailMessageNum;
		}

		///<summary></summary>
		public static void Delete(EmailMessage message){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),message);
				return;
			}
			if(message.EmailMessageNum==0){
				return;//this prevents deletion of all commlog entries of something goes wrong.
			}
			string command="DELETE FROM emailmessage WHERE EmailMessageNum="+POut.Long(message.EmailMessageNum);
			Db.NonQ(command);
		}

		
	}

	
	

}













