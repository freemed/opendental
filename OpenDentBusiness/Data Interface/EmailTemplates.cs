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
			list=Crud.EmailTemplateCrud.TableToList(table).ToArray();
		}

		///<summary></summary>
		public static long Insert(EmailTemplate template) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				template.EmailTemplateNum=Meth.GetLong(MethodBase.GetCurrentMethod(),template);
				return template.EmailTemplateNum;
			}
			return Crud.EmailTemplateCrud.Insert(template);
		}

		///<summary></summary>
		public static void Update(EmailTemplate template){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),template);
				return;
			}
			Crud.EmailTemplateCrud.Update(template);
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













