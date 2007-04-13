using System;
using System.Collections;
using System.Data;
using System.Windows.Forms;
using System.Xml.Serialization;
using OpenDentBusiness;

namespace OpenDental{
	///<summary></summary>
	public class ClaimFormItems {
		///<summary></summary>
		private static ClaimFormItem[] List;

		///<summary>Gets all claimformitems for all claimforms.  Items for individual claimforms can later be extracted as needed.</summary>
		public static void Refresh() {
			string command=
				"SELECT * FROM claimformitem ORDER BY imagefilename desc";
			DataTable table=General.GetTable(command);
			List=new ClaimFormItem[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++) {
				List[i]=new ClaimFormItem();
				List[i].ClaimFormItemNum= PIn.PInt(table.Rows[i][0].ToString());
				List[i].ClaimFormNum    = PIn.PInt(table.Rows[i][1].ToString());
				List[i].ImageFileName   = PIn.PString(table.Rows[i][2].ToString());
				List[i].FieldName       = PIn.PString(table.Rows[i][3].ToString());
				List[i].FormatString    = PIn.PString(table.Rows[i][4].ToString());
				List[i].XPos            = PIn.PFloat(table.Rows[i][5].ToString());
				List[i].YPos            = PIn.PFloat(table.Rows[i][6].ToString());
				List[i].Width           = PIn.PFloat(table.Rows[i][7].ToString());
				List[i].Height          = PIn.PFloat(table.Rows[i][8].ToString());
			}
		}

		///<summary></summary>
		public static void Insert(ClaimFormItem item){
			string command="INSERT INTO claimformitem (claimformnum,imagefilename,fieldname,formatstring"
				+",xpos,ypos,width,height) VALUES("
				+"'"+POut.PInt   (item.ClaimFormNum)+"', "
				+"'"+POut.PString(item.ImageFileName)+"', "
				+"'"+POut.PString(item.FieldName)+"', "
				+"'"+POut.PString(item.FormatString)+"', "
				+"'"+POut.PFloat (item.XPos)+"', "
				+"'"+POut.PFloat (item.YPos)+"', "
				+"'"+POut.PFloat (item.Width)+"', "
				+"'"+POut.PFloat (item.Height)+"')";
			//MessageBox.Show(string command);
 			item.ClaimFormItemNum=General.NonQ(command,true);
		}

		///<summary></summary>
		public static void Update(ClaimFormItem item){
			string command= "UPDATE claimformitem SET "
				+"claimformnum = '" +POut.PInt   (item.ClaimFormNum)+"' "
				+",imagefilename = '"+POut.PString(item.ImageFileName)+"' "
				+",fieldname = '"    +POut.PString(item.FieldName)+"' "
				+",formatstring = '" +POut.PString(item.FormatString)+"' "
				+",xpos = '"         +POut.PFloat (item.XPos)+"' "
				+",ypos = '"         +POut.PFloat (item.YPos)+"' "
				+",width = '"        +POut.PFloat (item.Width)+"' "
				+",height = '"       +POut.PFloat (item.Height)+"' "
				+"WHERE ClaimFormItemNum = '"+POut.PInt   (item.ClaimFormItemNum)+"'";
 			General.NonQ(command);
		}

		///<summary></summary>
		public static void Delete(ClaimFormItem item){
			string command = "DELETE FROM claimformitem "
				+"WHERE ClaimFormItemNum = '"+POut.PInt(item.ClaimFormItemNum)+"'";
 			General.NonQ(command);
		}


		///<summary>Gets all claimformitems for the specified claimform from the preloaded List.</summary>
		public static ClaimFormItem[] GetListForForm(int claimFormNum){
			ArrayList tempAL=new ArrayList();
			for(int i=0;i<List.Length;i++){
				if(List[i].ClaimFormNum==claimFormNum){
					tempAL.Add(List[i]);
				}
			}
			ClaimFormItem[] ListForForm=new ClaimFormItem[tempAL.Count];
			tempAL.CopyTo(ListForForm);
			return ListForForm;
		}


	}

	

	

}









