using System;
using System.Collections;
using System.Data;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;
using OpenDentBusiness;

namespace OpenDental{
	///<summary></summary>
	public class ClaimForms {
		///<summary>List of all claim forms.</summary>
		public static ClaimForm[] ListLong;
		///<summary>List of all claim forms except those marked as hidden.</summary>
		public static ClaimForm[] ListShort;


		///<summary>Fills ListShort and ListLong with all claimforms from the db. Also attaches corresponding claimformitems to each.</summary>
		public static void Refresh() {
			string command=
				"SELECT * FROM claimform";
			DataTable table=General.GetTable(command);
			ListLong=new ClaimForm[table.Rows.Count];
			ArrayList tempAL=new ArrayList();
			for(int i=0;i<table.Rows.Count;i++) {
				ListLong[i]=new ClaimForm();
				ListLong[i].ClaimFormNum= PIn.PInt(table.Rows[i][0].ToString());
				ListLong[i].Description = PIn.PString(table.Rows[i][1].ToString());
				ListLong[i].IsHidden    = PIn.PBool(table.Rows[i][2].ToString());
				ListLong[i].FontName    = PIn.PString(table.Rows[i][3].ToString());
				ListLong[i].FontSize    = PIn.PFloat(table.Rows[i][4].ToString());
				ListLong[i].UniqueID    = PIn.PString(table.Rows[i][5].ToString());
				ListLong[i].PrintImages = PIn.PBool(table.Rows[i][6].ToString());
				ListLong[i].OffsetX     = PIn.PInt(table.Rows[i][7].ToString());
				ListLong[i].OffsetY     = PIn.PInt(table.Rows[i][8].ToString());
				ListLong[i].Items=ClaimFormItems.GetListForForm(ListLong[i].ClaimFormNum);
				if(!ListLong[i].IsHidden)
					tempAL.Add(ListLong[i]);
			}
			ListShort=new ClaimForm[tempAL.Count];
			for(int i=0;i<ListShort.Length;i++) {
				ListShort[i]=(ClaimForm)tempAL[i];
			}
		}

		///<summary>Inserts this claimform into database and retrieves the new primary key.</summary>
		public static void Insert(ClaimForm cf){
			string command="INSERT INTO claimform (Description,IsHidden,FontName,FontSize"
				+",UniqueId,PrintImages,OffsetX,OffsetY) VALUES("
				+"'"+POut.PString(cf.Description)+"', "
				+"'"+POut.PBool  (cf.IsHidden)+"', "
				+"'"+POut.PString(cf.FontName)+"', "
				+"'"+POut.PFloat (cf.FontSize)+"', "
				+"'"+POut.PString(cf.UniqueID)+"', "
				+"'"+POut.PBool  (cf.PrintImages)+"', "
				+"'"+POut.PInt   (cf.OffsetX)+"', "
				+"'"+POut.PInt   (cf.OffsetY)+"')";
			//MessageBox.Show(string command);
 			cf.ClaimFormNum=General.NonQ(command,true);
		}

		///<summary></summary>
		public static void Update(ClaimForm cf){
			string command="UPDATE claimform SET "
				+"Description = '" +POut.PString(cf.Description)+"' "
				+",IsHidden = '"    +POut.PBool  (cf.IsHidden)+"' "
				+",FontName = '"    +POut.PString(cf.FontName)+"' "
				+",FontSize = '"    +POut.PFloat (cf.FontSize)+"' "
				+",UniqueID = '"    +POut.PString(cf.UniqueID)+"' "
				+",PrintImages = '" +POut.PBool  (cf.PrintImages)+"' "
				+",OffsetX = '"     +POut.PInt   (cf.OffsetX)+"' "
				+",OffsetY = '"     +POut.PInt   (cf.OffsetY)+"' "
				+"WHERE ClaimFormNum = '"+POut.PInt   (cf.ClaimFormNum)+"'";
 			General.NonQ(command);
		}

		///<summary> Called when cancelling out of creating a new claimform, and from the claimform window when clicking delete. Returns true if successful or false if dependencies found.</summary>
		public static bool Delete(ClaimForm cf){
			//first, do dependency testing
			string command="SELECT * FROM insplan WHERE claimformnum = '"
				+cf.ClaimFormNum.ToString()+"' ";
			if(FormChooseDatabase.DBtype==DatabaseType.Oracle){
				command+="AND ROWNUM <= 1";
			}else{//Assume MySQL
				command+="LIMIT 1";
			}
 			DataTable table=General.GetTable(command);
			if(table.Rows.Count==1){
				return false;
			}
			//Then, delete the claimform
			command="DELETE FROM claimform "
				+"WHERE ClaimFormNum = '"+POut.PInt(cf.ClaimFormNum)+"'";
			General.NonQ(command);
			command="DELETE FROM claimformitem "
				+"WHERE ClaimFormNum = '"+POut.PInt(cf.ClaimFormNum)+"'";
			General.NonQ(command);
			return true;
		}

		///<summary>Updates all claimforms with this unique id including all attached items.</summary>
		public static void UpdateByUniqueID(ClaimForm cf){
			//first get a list of the ClaimFormNums with this UniqueId
			string command=
				"SELECT ClaimFormNum FROM claimform WHERE UniqueID ='"+cf.UniqueID.ToString()+"'";
 			DataTable table=General.GetTable(command);
			int[] claimFormNums=new int[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++){
				claimFormNums[i]=PIn.PInt   (table.Rows[i][0].ToString());
			}
			//loop through each matching claimform
			for(int i=0;i<claimFormNums.Length;i++){
				cf.ClaimFormNum=claimFormNums[i];
				Update(cf);
				command="DELETE FROM claimformitem "
					+"WHERE ClaimFormNum = '"+POut.PInt(claimFormNums[i])+"'";
				General.NonQ(command);
				for(int j=0;j<cf.Items.Length;j++){
					cf.Items[j].ClaimFormNum=claimFormNums[i];
					ClaimFormItems.Insert(cf.Items[j]);
				}
			}
		}

		///<summary>Returns the claim form specified by the given claimFormNum</summary>
		public static ClaimForm GetClaimForm(int claimFormNum){
			for(int i=0;i<ListLong.Length;i++){
				if(ListLong[i].ClaimFormNum==claimFormNum){
					return ListLong[i];
				}
			}
			MessageBox.Show("Error. Could not locate Claim Form.");
			return null;
		}

		///<summary>Returns number of insplans affected.</summary>
		public static int Reassign(int oldClaimFormNum, int newClaimFormNum){
			string command="UPDATE insplan SET ClaimFormNum="+POut.PInt(newClaimFormNum)
				+" WHERE ClaimFormNum="+POut.PInt(oldClaimFormNum);
			return General.NonQ(command);
		}



	}

	



}









