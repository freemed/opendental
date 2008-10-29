using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using OpenDental.DataAccess;

namespace OpenDentBusiness{
	///<summary></summary>
	public class AnestheticMeds{
		///<summary></summary>
		public static DataTable RefreshCache(){
			string c="SELECT * FROM anesthmedsinventory ORDER BY AnesthMedName";
			DataTable table=General.GetTable(c);
			table.TableName="AnesthMedsInventory";
			FillCache(table);
			return table;
		}

		public static void FillCache(DataTable table){
			AnesthMedInvC.Listt=new List<AnesthMedsInventory>();
			AnesthMedsInventory med;
			for(int i=0;i<table.Rows.Count;i++){
				med=new AnesthMedsInventory();
				med.IsNew=false;
				med.AnestheticMedNum= PIn.PInt   (table.Rows[i][0].ToString());
				med.AnesthMedName    = PIn.PString(table.Rows[i][1].ToString());
				med.AnesthHowSupplied  = PIn.PString(table.Rows[i][2].ToString());
                med.QtyOnHand = PIn.PString(table.Rows[i][3].ToString());
			}
		}

		///<Summary>Gets one Anesthetic Medication from the database.</Summary>
		public static AnesthMedsInventory CreateObject(int AnestheticMedNum){
			return DataObjectFactory<AnesthMedsInventory>.CreateObject(AnestheticMedNum);
		}

		public static List<AnesthMedsInventory> GetAnestheticMeds(int[] AnestheticMedNums){
			Collection<AnesthMedsInventory> collectState=DataObjectFactory<AnesthMedsInventory>.CreateObjects(AnestheticMedNums);
			return new List<AnesthMedsInventory>(collectState);		
		}

		///<summary></summary>
		public static void WriteObject(AnesthMedsInventory Med){
			DataObjectFactory<AnesthMedsInventory>.WriteObject(Med);
		}

		///<summary></summary>
		public static void DeleteObject(int AnestheticMedNum){
			//validate that not already in use.
			/*string command="SELECT LName,FName FROM patient WHERE PharmacyNum="+POut.PInt(PharmacyNum);
			DataTable table=General.GetTable(command);
			string pats="";
			for(int i=0;i<table.Rows.Count;i++){
				if(i>0){
					pats+=", ";
				}
				pats+=table.Rows[i]["FName"].ToString()+" "+table.Rows[i]["LName"].ToString();
			}
			if(table.Rows.Count>0){
				throw new ApplicationException(Lan.g("Pharmacys","Pharmacy is already in use by patient(s). Not allowed to delete. "+pats));
			}*/
			DataObjectFactory<AnesthMedsInventory>.DeleteObject(AnestheticMedNum);
		}

		//public static void DeleteObject(int PharmacyNum){
		//	DataObjectFactory<Pharmacy>.DeleteObject(PharmacyNum);
		//}

		public static string GetName(int AnestheticMedNum){
			if(AnestheticMedNum==0){
				return "";
			}
			for(int i=0;i<AnesthMedInvC.Listt.Count;i++){
				if(AnesthMedInvC.Listt[i].AnestheticMedNum==AnestheticMedNum){
					return AnesthMedInvC.Listt[i].AnesthMedName;
				}
			}
			return "";
		}

	}
}