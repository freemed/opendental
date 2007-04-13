using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{
	///<summary>An email message is always attached to a patient.</summary>
	public class EmailMessages{
		///<summary>Gets one email message from the database.</summary>
		public static EmailMessage GetOne(int msgNum){
			string commands="SELECT * FROM emailmessage WHERE EmailMessageNum = "+POut.PInt(msgNum)
				+";SELECT * FROM emailattach WHERE EmailMessageNum = "+POut.PInt(msgNum);
			DataSet ds=null;
			try {
				if(RemotingClient.OpenDentBusinessIsLocal) {
					ds=GeneralB.GetDataSet(commands);
				}
				else {
					DtoGeneralGetDataSet dto=new DtoGeneralGetDataSet();
					dto.Commands=commands;
					ds=RemotingClient.ProcessQuery(dto);
				}
			}
			catch(Exception e) {
				MessageBox.Show(e.Message);
			}
			DataTable table=ds.Tables[0];
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
			table=ds.Tables[1];
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
			try {
				if(RemotingClient.OpenDentBusinessIsLocal) {
					EmailMessageB.Update(message);
				}
				else {
					DtoEmailMessageUpdate dto=new DtoEmailMessageUpdate();
					dto.Message=message;
					RemotingClient.ProcessCommand(dto);
				}
			}
			catch(Exception e) {
				MessageBox.Show(e.Message);
			}
		}

		///<summary></summary>
		public static void Insert(EmailMessage message){
			int insertID=0;
			try {
				if(RemotingClient.OpenDentBusinessIsLocal) {
					insertID=EmailMessageB.Insert(message);
				}
				else {
					DtoEmailMessageInsert dto=new DtoEmailMessageInsert();
					dto.Message=message;
					insertID=RemotingClient.ProcessCommand(dto);
				}
			}
			catch(Exception e) {
				MessageBox.Show(e.Message);
			}
			message.EmailMessageNum=insertID;
			/*
			int insertID=0;
			try {
				if(RemotingClient.OpenDentBusinessIsLocal) {
					if(PrefB.RandomKeys) {
						GeneralB.NonQ(command);
					}
					else {
						insertID=GeneralB.NonQ(command,true);
						message.EmailMessageNum=insertID;
					}
				}
				else {
					DtoGeneralNonQ dto=new DtoGeneralNonQ();
					dto.Command=command;
					if(PrefB.RandomKeys) {
						RemotingClient.ProcessCommand(dto);
					}
					else {
						dto.GetInsertID=true;
						insertID=RemotingClient.ProcessCommand(dto);
						message.EmailMessageNum=insertID;
					}
				}
			}
			catch(Exception e) {
				MessageBox.Show(e.Message);
				return;
			}*/
		}

		///<summary></summary>
		public static void Delete(EmailMessage message){
			if(message.EmailMessageNum==0){
				return;//this prevents deletion of all commlog entries of something goes wrong.
			}
			string command="DELETE FROM emailmessage WHERE EmailMessageNum="+POut.PInt(message.EmailMessageNum);//+";"
				//+"DELETE FROM commlog WHERE EmailMessageNum="+POut.PInt(message.EmailMessageNum);
			try {
				if(RemotingClient.OpenDentBusinessIsLocal) {
					GeneralB.NonQ(command);
				}
				else {
					DtoGeneralNonQ dto=new DtoGeneralNonQ();
					dto.Command=command;
					RemotingClient.ProcessCommand(dto);
				}
			}
			catch(Exception e) {
				MessageBox.Show(e.Message);
			}
		}

		
	}

	
	

}













