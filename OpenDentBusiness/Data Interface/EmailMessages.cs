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
			string command="SELECT * FROM emailmessage WHERE EmailMessageNum = "+POut.Long(msgNum);
			EmailMessage message=Crud.EmailMessageCrud.SelectOne(msgNum);
			command="SELECT * FROM emailattach WHERE EmailMessageNum = "+POut.Long(msgNum);
			message.Attachments=Crud.EmailAttachCrud.SelectMany(command);
			return message;
		}

		public static List<EmailMessage> GetReceivedForAddress(string emailAddress) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List <EmailMessage>>(MethodBase.GetCurrentMethod(),emailAddress);
			}
			string command="SELECT * FROM emailmessage "
				+"WHERE SentOrReceived IN ("
					+POut.Int((int)EmailSentOrReceived.Read)+","
					+POut.Int((int)EmailSentOrReceived.Received)+","
					+POut.Int((int)EmailSentOrReceived.ReceivedEncrypted)+","
					+POut.Int((int)EmailSentOrReceived.ReceivedDirect)+","
					+POut.Int((int)EmailSentOrReceived.ReadDirect)+","
					+POut.Int((int)EmailSentOrReceived.WebMailRecdRead)+","
					+POut.Int((int)EmailSentOrReceived.WebMailReceived)
				+") AND ToAddress='"+POut.String(emailAddress)+"' "
				+"ORDER BY MsgDateTime";
			return Crud.EmailMessageCrud.SelectMany(command);
		}

		///<summary></summary>
		public static void Update(EmailMessage message){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),message);
				return;
			}
			Crud.EmailMessageCrud.Update(message);
			//now, delete all attachments and recreate.
			string command="DELETE FROM emailattach WHERE EmailMessageNum="+POut.Long(message.EmailMessageNum);
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
			Crud.EmailMessageCrud.Insert(message);
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













