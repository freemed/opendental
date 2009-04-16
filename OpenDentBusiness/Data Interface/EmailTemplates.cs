using System;
using System.Collections;
using System.Data;
using System.Text.RegularExpressions;

namespace OpenDentBusiness{
	///<summary>emailtemplates are refreshed as local data.</summary>
	public class EmailTemplates {
		private static EmailTemplate[] list;

		///<summary>List of all email templates</summary>
		public static EmailTemplate[] List {
			get {
				if(list==null) {
					Refresh();
				}
				return list;
			}
			set {
				list=value;
			}
		}

		///<summary></summary>
		public static void Refresh() {
			string command=
				"SELECT * from emailtemplate ORDER BY Subject";
			DataTable table=General.GetTable(command);
			List=new EmailTemplate[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++) {
				List[i]=new EmailTemplate();
				List[i].EmailTemplateNum=PIn.PInt(table.Rows[i][0].ToString());
				List[i].Subject         =PIn.PString(table.Rows[i][1].ToString());
				List[i].BodyText        =PIn.PString(table.Rows[i][2].ToString());
			}
		}

		///<summary></summary>
		public static void Insert(EmailTemplate template){
			if(PrefC.RandomKeys){
				template.EmailTemplateNum=MiscData.GetKey("emailtemplate","EmailTemplateNum");
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
				General.NonQ(command);
			}
			else{
 				template.EmailTemplateNum=General.NonQ(command,true);
			}
		}

		///<summary></summary>
		public static void Update(EmailTemplate template){
			string command= "UPDATE emailtemplate SET "
				+ "Subject = '"  +POut.PString(template.Subject)+"' "
				+ ",BodyText = '"+POut.PString(template.BodyText)+"' "
				+"WHERE EmailTemplateNum = '"+POut.PInt(template.EmailTemplateNum)+"'";
 			General.NonQ(command);
		}

		///<summary></summary>
		public static void Delete(EmailTemplate template){
			string command= "DELETE from emailtemplate WHERE EmailTemplateNum = '"
				+template.EmailTemplateNum.ToString()+"'";
 			General.NonQ(command);
		}

		
	
	

		

		

		
		
	}

	
	

}













