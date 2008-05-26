using System;
using System.Collections;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{

	///<summary>Refreshed with local data.</summary>
	public class ElectIDs{
		///<summary>This is the list of all electronic IDs.</summary>
		public static ElectID[] List;

		///<summary>Since users not allowed to edit, this only gets run on startup.</summary>
		public static void Refresh(){
			string command="SELECT * from electid "
				+"ORDER BY CarrierName";
 			DataTable table=General.GetTable(command);
			List=new ElectID[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++){
				List[i]=new ElectID();
				List[i].ElectIDNum   = PIn.PInt   (table.Rows[i][0].ToString());
				List[i].PayorID      = PIn.PString(table.Rows[i][1].ToString());
				List[i].CarrierName  = PIn.PString(table.Rows[i][2].ToString());
				List[i].IsMedicaid   = PIn.PBool  (table.Rows[i][3].ToString());
				List[i].ProviderTypes= PIn.PString(table.Rows[i][4].ToString());
				List[i].Comments     = PIn.PString(table.Rows[i][5].ToString());
			}
		}

		///<summary></summary>
		public static ProviderSupplementalID[] GetRequiredIdents(string payorID){
			ElectID electID=GetID(payorID);
			if(electID==null){
				return new ProviderSupplementalID[0];
			}
			if(electID.ProviderTypes==""){
				return new ProviderSupplementalID[0];
			}
			string[] provTypes=electID.ProviderTypes.Split(',');
			if(provTypes.Length==0){
				return new ProviderSupplementalID[0];
			}
			ProviderSupplementalID[] retVal=new ProviderSupplementalID[provTypes.Length];
			for(int i=0;i<provTypes.Length;i++){
				retVal[i]=(ProviderSupplementalID)(Convert.ToInt32(provTypes[i]));
			}
			/*
			if(electID=="SB601"){//BCBS of GA
				retVal=new ProviderSupplementalID[2];
				retVal[0]=ProviderSupplementalID.BlueShield;
				retVal[1]=ProviderSupplementalID.SiteNumber;
			}*/
			return retVal;
		}

		///<summary>Gets ONE ElectID that uses the supplied payorID. Even if there are multiple payors using that ID.  So use this carefully.</summary>
		public static ElectID GetID(string payorID){
			ArrayList electIDs=GetIDs(payorID);
			if(electIDs.Count==0){
				return null;
			}
			return (ElectID)electIDs[0];//simply return the first one we encounter
		}

		///<summary>Gets an arrayList of ElectID objects based on a supplied payorID. If no matches found, then returns array of 0 length. Used to display payors in FormInsPlan and also to get required idents.  This means that all payors with the same ID should have the same required idents and notes.</summary>
		public static ArrayList GetIDs(string payorID){
			ArrayList retVal=new ArrayList();
			for(int i=0;i<List.Length;i++){
				if(List[i].PayorID==payorID){
					retVal.Add(List[i]);
				}
			}
			return retVal;
		}

		///<summary>Gets the names of the payors to display based on the payorID.  Since carriers sometimes share payorIDs, there will often be multiple payor names returned.</summary>
		public static string[] GetDescripts(string payorID){
			if(payorID==""){
				return new string[]{};
			}
			ArrayList electIDs=GetIDs(payorID);
			string[] retVal=new string[electIDs.Count];
			for(int i=0;i<retVal.Length;i++){
				retVal[i]=((ElectID)electIDs[i]).CarrierName;
			}
			return retVal;
		}

	



	
	}
	
	

}










