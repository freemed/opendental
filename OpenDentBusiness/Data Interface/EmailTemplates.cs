using System;
using System.Collections;
using System.Data;
using System.Reflection;
using System.Text.RegularExpressions;

namespace OpenDentBusiness{
	///<summary>emailtemplates are refreshed as local data.</summary>
	public class EmailTemplates {
		private static EmailTemplate[] list;

		///<summary>List of all email templates</summary>
		public static EmailTemplate[] List {
			//No need to check RemotingRole; no call to db.
			get {
				if(list==null) {
					RefreshCache();
				}
				return list;
			}
			set {
				list=value;
			}
		}

		public static DataTable RefreshCache() {
			//No need to check RemotingRole; Calls GetTableRemotelyIfNeeded().
			string command=
				"SELECT * from emailtemplate ORDER BY Subject";
			DataTable table=Cache.GetTableRemotelyIfNeeded(MethodBase.GetCurrentMethod(),command);
			table.TableName="EmailTemplate";
			FillCache(table);
			return table;
		}

		///<summary></summary>
		public static void FillCache(DataTable table) {
			//No need to check RemotingRole; no call to db.
			List=new EmailTemplate[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++) {
				List[i]=new EmailTemplate();
				List[i].EmailTemplateNum=PIn.PInt(table.Rows[i][0].ToString());
				List[i].Subject=PIn.PString(table.Rows[i][1].ToString());
				List[i].BodyText=PIn.PString(table.Rows[i][2].ToString());
			}
		}

		///<summary></summary>
		public static long Insert(EmailTemplate template) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				template.EmailTemplateNum=Meth.GetInt(MethodBase.GetCurrentMethod(),template);
				return template.EmailTemplateNum;
			}
			if(PrefC.RandomKeys){
				template.EmailTemplateNum=ReplicationServers.GetKey("emailtemplate","EmailTemplateNum");
			}
			string command= "INSERT INTO emailtemplate (";
			if(PrefC.RandomKeys){
				command+="EmailTemplateNum,";
			}
			command+="Subject,BodyText"
				+") VALUES(";
			if(PrefC.RandomKeys){
				command+="'"+POut.PInt(template.EmailTemplateNum)+"', ";
			}
			command+=
				 "'"+POut.PString(template.Subject)+"', "
				+"'"+POut.PString(template.BodyText)+"')";
			//MessageBox.Show(string command);
			if(PrefC.RandomKeys){
				Db.NonQ(command);
			}
			else{
 				template.EmailTemplateNum=Db.NonQ(command,true);
			}
			return template.EmailTemplateNum;
		}

		///<summary></summary>
		public static void Update(EmailTemplate template){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),template);
				return;
			}
			string command= "UPDATE emailtemplate SET "
				+ "Subject = '"  +POut.PString(template.Subject)+"' "
				+ ",BodyText = '"+POut.PString(template.BodyText)+"' "
				+"WHERE EmailTemplateNum = '"+POut.PInt(template.EmailTemplateNum)+"'";
 			Db.NonQ(command);
		}

		///<summary></summary>
		public static void Delete(EmailTemplate template){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),template);
				return;
			}
			string command= "DELETE from emailtemplate WHERE EmailTemplateNum = '"
				+template.EmailTemplateNum.ToString()+"'";
 			Db.NonQ(command);
		}

		
	
	

		

		

		
		
	}

	
	

}













