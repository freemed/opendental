using System;
using System.Collections;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{
	///<summary>Refreshed with local data.</summary>
	public class ProviderIdents {
		///<summary>This is the list of all id's for all providers. They are extracted as needed.</summary>
		private static ProviderIdent[] List;

		///<summary></summary>
		public static void Refresh() {
			string command="SELECT * from providerident";
			DataTable table=General.GetTable(command);
			List=new ProviderIdent[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++) {
				List[i]=new ProviderIdent();
				List[i].ProviderIdentNum= PIn.PInt(table.Rows[i][0].ToString());
				List[i].ProvNum         = PIn.PInt(table.Rows[i][1].ToString());
				List[i].PayorID         = PIn.PString(table.Rows[i][2].ToString());
				List[i].SuppIDType      = (ProviderSupplementalID)PIn.PInt(table.Rows[i][3].ToString());
				List[i].IDNumber        = PIn.PString(table.Rows[i][4].ToString());
			}
		}

		///<summary></summary>
		public static void Update(ProviderIdent pi){
			string command= "UPDATE providerident SET "
				+ "ProvNum = '"   +POut.PInt   (pi.ProvNum)+"'"
				+",PayorID = '"   +POut.PString(pi.PayorID)+"'"
				+",SuppIDType = '"+POut.PInt   ((int)pi.SuppIDType)+"'"
				+",IDNumber = '"  +POut.PString(pi.IDNumber)+"'"
				+" WHERE ProviderIdentNum = '"+POut.PInt(pi.ProviderIdentNum)+"'";
 			General.NonQ(command);
		}

		///<summary></summary>
		public static void Insert(ProviderIdent pi){
			string command= "INSERT INTO providerident (ProvNum,PayorID,SuppIDType,IDNumber"
				+") VALUES ("
				+"'"+POut.PInt   (pi.ProvNum)+"', "
				+"'"+POut.PString(pi.PayorID)+"', "
				+"'"+POut.PInt   ((int)pi.SuppIDType)+"', "
				+"'"+POut.PString(pi.IDNumber)+"')";
			//MessageBox.Show(string command);
 			General.NonQ(command);
			//ClaimProcNum=dcon.InsertID;
		}

		///<summary></summary>
		public static void Delete(ProviderIdent pi){
			string command= "DELETE FROM providerident "
				+"WHERE ProviderIdentNum = "+POut.PInt(pi.ProviderIdentNum);
 			General.NonQ(command);
		}


	
	

		///<summary>Gets all supplemental identifiers that have been attached to this provider. Used in the provider edit window.</summary>
		public static ProviderIdent[] GetForProv(int provNum){
			ArrayList arrayL=new ArrayList();
			for(int i=0;i<List.Length;i++){
				if(List[i].ProvNum==provNum){
					arrayL.Add(List[i]);
				}
			}
			ProviderIdent[] ForProv=new ProviderIdent[arrayL.Count];
			for(int i=0;i<arrayL.Count;i++){
				ForProv[i]=(ProviderIdent)arrayL[i];
			}
			return ForProv;
		}

		///<summary>Gets all supplemental identifiers that have been attached to this provider and for this particular payorID.  Called from X12 when creating a claim file.  Also used now on printed claims.</summary>
		public static ProviderIdent[] GetForPayor(int provNum,string payorID){
			ArrayList arrayL=new ArrayList();
			for(int i=0;i<List.Length;i++){
				if(List[i].ProvNum==provNum
					&& List[i].PayorID==payorID)
				{
					arrayL.Add(List[i]);
				}
			}
			ProviderIdent[] ForPayor=new ProviderIdent[arrayL.Count];
			for(int i=0;i<arrayL.Count;i++){
				ForPayor[i]=(ProviderIdent)arrayL[i];
			}
			return ForPayor;
		}

		///<summary>Called from FormProvEdit if cancel on a new provider.</summary>
		public static void DeleteAllForProv(int provNum){
			string command= "DELETE from providerident WHERE provnum = '"+POut.PInt(provNum)+"'";
 			General.NonQ(command);
		}

		/// <summary></summary>
		public static bool IdentExists(ProviderSupplementalID type,int provNum,string payorID){
			for(int i=0;i<List.Length;i++){
				if(List[i].ProvNum==provNum
					&& List[i].SuppIDType==type
					&& List[i].PayorID==payorID)
				{
					return true;
				}
			}
			return false;
		}

	
	}
	
	

}










