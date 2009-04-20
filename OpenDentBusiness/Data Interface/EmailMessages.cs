using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text.RegularExpressions;

namespace OpenDentBusiness{
	///<summary>An email message is always attached to a patient.</summary>
	public class EmailMessages{
		///<summary>Gets one email message from the database.</summary>
		public static EmailMessage GetOne(int msgNum){
			string command="SELECT * FROM emailmessage WHERE EmailMessageNum = "+POut.PInt(msgNum);
			DataTable table=Db.GetTable(command);
			EmailMessage Cur=new EmailMessage();
			if(table.Rows.Count==0)
				return null;
			//for(int i=0;i<table.Rows.Count;i++){
			Cur.EmailMessageNum=PIn.PInt   (table.Rows[0][0].ToString());
			Cur.PatNum         =PIn.PInt   (table.Rows[0][1].ToString());
			Cur.ToAddress      =PIn.PString(table.Rows[0][2].ToString());
			Cur.FromAddress    =PIn.PString(table.Rows[0][3].ToString());
			Cur.Subject        =PIn.PString(table.Rows[0][4].ToString());
			Cur.BodyText       =PIn.PString(table.Rows[0][5].ToString());
			Cur.MsgDateTime    =PIn.PDateT (table.Rows[0][6].ToString());
			Cur.SentOrReceived =(CommSentOrReceived)PIn.PInt(table.Rows[0][7].ToString());
			command="SELECT * FROM emailattach WHERE EmailMessageNum = "+POut.PInt(msgNum);
			table=Db.GetTable(command);
			Cur.Attachments=new List<EmailAttach>();
			EmailAttach attach;
			for(int i=0;i<table.Rows.Count;i++){
				attach=new EmailAttach();
				attach.EmailAttachNum   =PIn.PInt   (table.Rows[i][0].ToString());
				attach.EmailMessageNum  =PIn.PInt   (table.Rows[i][1].ToString());
				attach.DisplayedFileName=PIn.PString(table.Rows[i][2].ToString());
				attach.ActualFileName   =PIn.PString(table.Rows[i][3].ToString());
				Cur.Attachments.Add(attach);
			}
			return Cur;
		}

		///<summary></summary>
		public static void Update(EmailMessage message){
			string command="UPDATE emailmessage SET "
				+ "PatNum = '"      +POut.PInt(message.PatNum)+"' "
				+ ",ToAddress = '"  +POut.PString(message.ToAddress)+"' "
				+ ",FromAddress = '"+POut.PString(message.FromAddress)+"' "
				+ ",Subject = '"    +POut.PString(message.Subject)+"' "
				+ ",BodyText = '"   +POut.PString(message.BodyText)+"' "
				+ ",MsgDateTime = "+POut.PDateT(message.MsgDateTime)+" "
				+ ",SentOrReceived = '"+POut.PInt((int)message.SentOrReceived)+"' "
				+"WHERE EmailMessageNum = "+POut.PInt(message.EmailMessageNum);
			Db.NonQ(command);
			//now, delete all attachments and recreate.
			command="DELETE FROM emailattach WHERE EmailMessageNum="+POut.PInt(message.EmailMessageNum);
			Db.NonQ(command);
			for(int i=0;i<message.Attachments.Count;i++) {
				message.Attachments[i].EmailMessageNum=message.EmailMessageNum;
				EmailAttaches.Insert(message.Attachments[i]);
			}
		}

		///<summary></summary>
		public static void Insert(EmailMessage message){
			if(PrefC.RandomKeys) {
				message.EmailMessageNum=MiscData.GetKey("emailmessage","EmailMessageNum");
			}
			string command="INSERT INTO emailmessage (";
			if(PrefC.RandomKeys) {
				command+="EmailMessageNum,";
			}
			command+="PatNum,ToAddress,FromAddress,Subject,BodyText,"
				+"MsgDateTime,SentOrReceived) VALUES(";
			if(PrefC.RandomKeys) {
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
		}

		///<summary></summary>
		public static void Delete(EmailMessage message){
			if(message.EmailMessageNum==0){
				return;//this prevents deletion of all commlog entries of something goes wrong.
			}
			string command="DELETE FROM emailmessage WHERE EmailMessageNum="+POut.PInt(message.EmailMessageNum);
			Db.NonQ(command);
		}

		
	}

	
	

}













