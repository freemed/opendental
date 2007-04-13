using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{
	///<summary></summary>
	public class DocAttaches{

		///<summary>For one patient. This should be followed by Documents.Refresh</summary>
		public static List<DocAttach> Refresh(int patNum){
			string command="SELECT * FROM docattach WHERE PatNum = "+POut.PInt(patNum);
			DataSet ds=null;
			try {
				if(RemotingClient.OpenDentBusinessIsLocal) {
					ds=GeneralB.GetTable(command);
				}
				else {
					DtoGeneralGetTable dto=new DtoGeneralGetTable();
					dto.Command=command;
					ds=RemotingClient.ProcessQuery(dto);
				}
			}
			catch(Exception e) {
				MessageBox.Show(e.Message);
			}
			DataTable table=ds.Tables[0];
			List<DocAttach> list=new List<DocAttach>();//[table.Rows.Count];
			DocAttach attach;
			for (int i=0;i<table.Rows.Count;i++){
				attach=new DocAttach();
				attach.DocAttachNum =PIn.PInt   (table.Rows[i][0].ToString());
				attach.PatNum       =PIn.PInt   (table.Rows[i][1].ToString());
				attach.DocNum       =PIn.PInt   (table.Rows[i][2].ToString());
				list.Add(attach);
			}
			return list;
		}

		/*
		///<summary></summary>
		public static void Update(){
			string command = "UPDATE docattach SET " 
				+ ",PatNum = '"  +POut.PInt(Cur.PatNum)+"'"
				+ ",DocNum = '"  +POut.PInt(Cur.DocNum)+"'"
				+" WHERE DocAttachNum = '" +POut.PInt(Cur.DocAttachNum)+"'";
			NonQ(false);
		}*/
	
		
				
		/*public static void DeleteDocNum(string myDocNum){
			string command = "DELETE from docattach WHERE docnum = '"+myDocNum+"'";
			NonQ(false);
		}
*/
	}

	

	

}









