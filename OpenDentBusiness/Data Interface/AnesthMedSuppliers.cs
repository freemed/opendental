using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using OpenDental.DataAccess;

namespace OpenDentBusiness{
	///<summary></summary>
	public class AnesthMedSuppliers{
		///<summary></summary>
		public static DataTable RefreshCache(){
			string c="SELECT * FROM anesthmedsuppliers ORDER BY SupplierName";
			DataTable table=General.GetTable(c);
			table.TableName="AnesthMedSuppliers";
			FillCache(table);
			return table;
		}

		public static void FillCache(DataTable table){
			AnesthMedSupplierC.Listt=new List<AnesthMedSupplier>();
			AnesthMedSupplier suppl;
			for(int i=0;i<table.Rows.Count;i++){
				suppl=new AnesthMedSupplier();
				suppl.IsNew = false;
				//suppl.AnesthMedSupplierNum = PIn.PInt(table.Rows[i][0].ToString());
				//suppl.AnesthMedSupplierID = PIn.PString(table.Rows[i][1].ToString());
				suppl.SupplierName = PIn.PString(table.Rows[i][0].ToString());
				suppl.Phone = PIn.PString(table.Rows[i][1].ToString());
				suppl.Fax = PIn.PString(table.Rows[i][2].ToString());
				suppl.Addr1 = PIn.PString(table.Rows[i][3].ToString());
				//suppl.Addr2 = PIn.PString(table.Rows[i][6].ToString());
				suppl.City = PIn.PString(table.Rows[i][4].ToString());
				suppl.State = PIn.PString(table.Rows[i][5].ToString());
				suppl.Zip = PIn.PString(table.Rows[i][6].ToString());
				suppl.WebSite = PIn.PString(table.Rows[i][7].ToString());
				AnesthMedSupplierC.Listt.Add(suppl);
			}
		}

		///<Summary>Gets one AnesthMedSupplier from the database.</Summary>
		public static AnesthMedSupplier CreateObject(int AnesthMedSupplierNum){
			return DataObjectFactory<AnesthMedSupplier>.CreateObject(AnesthMedSupplierNum);
		}

		public static List<AnesthMedSupplier> GetAnesthMedSuppliers(int[] AnesthMedSupplierNums){
			Collection<AnesthMedSupplier> collectState=DataObjectFactory<AnesthMedSupplier>.CreateObjects(AnesthMedSupplierNums);
			return new List<AnesthMedSupplier>(collectState);		
		}

		///<summary></summary>
		public static void WriteObject(AnesthMedSupplier AnesthMedSupplier){
			DataObjectFactory<AnesthMedSupplier>.WriteObject(AnesthMedSupplier);
		}

		///<summary></summary>
		public static void DeleteObject(int AnesthMedSupplierNum){
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
			DataObjectFactory<AnesthMedSupplier>.DeleteObject(AnesthMedSupplierNum);
		}

		//public static void DeleteObject(int PharmacyNum){
		//	DataObjectFactory<Pharmacy>.DeleteObject(PharmacyNum);
		//}

		/*public static string GetDescription(int AnesthMedSupplierNum){
			if(AnesthMedSupplierNum==0){
				return "";
			}
			for(int i=0;i<AnesthMedSupplierC.Listt.Count;i++){
				if(AnesthMedSupplierC.Listt[i].AnesthMedSupplierNum==AnesthMedSupplierNum){
					return AnesthMedSupplierC.Listt[i].SupplierName;
				}
			}
			return "";
		}*/

	}
}