using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using OpenDentBusiness.DataAccess;


namespace OpenDentBusiness{
	///<summary>A list of Anesthetic Medication Suppliers (Vendors) </summary>
	public class AnesthMedSuppliers{

        public bool IsNew;

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
			AnesthMedSupplier supplCur;
			for(int i=0;i<table.Rows.Count;i++){
				supplCur=new AnesthMedSupplier();
				supplCur.IsNew = false;
				supplCur.SupplierIDNum  = PIn.PInt(table.Rows[i][0].ToString());
				supplCur.SupplierName   = PIn.PString(table.Rows[i][1].ToString());
				supplCur.Phone          = PIn.PString(table.Rows[i][2].ToString());
                supplCur.PhoneExt       = PIn.PString(table.Rows[i][3].ToString());
				supplCur.Fax            = PIn.PString(table.Rows[i][4].ToString());
				supplCur.Addr1          = PIn.PString(table.Rows[i][5].ToString());
				supplCur.Addr2          = PIn.PString(table.Rows[i][6].ToString());
				supplCur.City           = PIn.PString(table.Rows[i][7].ToString());
				supplCur.State          = PIn.PString(table.Rows[i][8].ToString());
				supplCur.Zip            = PIn.PString(table.Rows[i][9].ToString());
                supplCur.Contact        = PIn.PString(table.Rows[i][10].ToString());
				supplCur.WebSite        = PIn.PString(table.Rows[i][11].ToString());
                supplCur.Notes          = PIn.PString(table.Rows[i][12].ToString());
				AnesthMedSupplierC.Listt.Add(supplCur);
			}
		}

		///<Summary>Gets one Anesthetic Medication Supplier from the database.</Summary>
		public static AnesthMedSupplier CreateObject(int SupplierIDNum){
			return DataObjectFactory<AnesthMedSupplier>.CreateObject(SupplierIDNum);
		}

		public static List<AnesthMedSupplier> GetAnesthMedSuppliers(int[] SupplierIDNums){
			Collection<AnesthMedSupplier> collectState=DataObjectFactory<AnesthMedSupplier>.CreateObjects(SupplierIDNums);
			return new List<AnesthMedSupplier>(collectState);		
		}

		///<summary></summary>
		public static void WriteObject(AnesthMedSupplier AnesthMedSupplier){
			DataObjectFactory<AnesthMedSupplier>.WriteObject(AnesthMedSupplier);
		}

		///<summary></summary>
		public static void DeleteObject(int SupplierIDNum){
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
			DataObjectFactory<AnesthMedSupplier>.DeleteObject(SupplierIDNum);
		}

		//public static void DeleteObject(int PharmacyNum){
		//	DataObjectFactory<Pharmacy>.DeleteObject(PharmacyNum);
		//}

		public static string GetSupplierName(int AnesthMedSupplierNum){
			if(AnesthMedSupplierNum==0){
				return "";
			}
			for(int i=0;i<AnesthMedSupplierC.Listt.Count;i++){
				if(AnesthMedSupplierC.Listt[i].SupplierIDNum==AnesthMedSupplierNum){
					return AnesthMedSupplierC.Listt[i].SupplierName;
				}
			}
			return "";
		}
		public static int GetSupplierIDNum (int AnesthMedSupplierNum){
			int SupplierIDNum = AnesthMedSupplierNum;
			return SupplierIDNum;
		}

		}
}


