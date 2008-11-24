using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using OpenDental.DataAccess;
using OpenDental;
using MySql.Data.MySqlClient;

namespace OpenDentBusiness
{
	
		/// <summary>Handles database commands for the anesthmedsgiven table in the database</summary>
		public class AMedications{

		///<summary>This data adapter is used for all queries to the database.</summary>
		static MySqlConnection con;
		public MySqlCommand cmd;
		

		public static DataTable GetAMDataTable() {

			string command = "SELECT AnesthMedName as 'Anesthetic Medication',AnesthDose as 'Dose',DoseTimeStamp as 'Time Stamp' FROM anestheticdata";
			DataTable table = General.GetTable(command);
			DataTable AMDataTable = table.Clone();//does not copy any data
			AMDataTable.TableName = "anesthmedsgiven";
			for (int i = 0; i < AMDataTable.Columns.Count; i++)
			{
				AMDataTable.Columns[i].DataType = typeof(string);
			}
			DataRow r;
			DateTime date;
			for (int i = 0; i < table.Rows.Count; i++)
			{
				r = AMDataTable.NewRow();
				r["Anesthetic Medication"] = table.Rows[i]["Anesthetic Medication"].ToString();
				r["Dose"] = table.Rows[i]["Dose"].ToString();
				r["Time Stamp"] = table.Rows[i]["Time Stamp"].ToString();
				AMDataTable.Rows.Add(r);
			}
			return AMDataTable;
			}
			/// <summary>Gets the data from anesthmedsinventory table</summary>

			public static DataTable GetAMInventory(){
			string command = "SELECT distinct AnesthMedName 'Anesthetic Medication', AnesthHowSupplied as 'How Supplied', QtyOnHand as 'Quantity on hand(mL)' FROM anesthmedsinventory order by AnestheticMedNum desc";
			DataTable table = General.GetTable(command);
			DataTable AMDataTable = table.Clone();//does not copy any data
			AMDataTable.TableName = "anesthmedsinventory";
			for (int i = 0; i < AMDataTable.Columns.Count; i++)
			{
				AMDataTable.Columns[i].DataType = typeof(string);
			}
			DataRow r;
			DateTime date;
			for (int i = 0; i < table.Rows.Count; i++)
			{
				r = AMDataTable.NewRow();
				r["Anesthetic Medication"] = table.Rows[i]["Anesthetic Medication"].ToString();
				r["How Supplied"] = table.Rows[i]["How Supplied"].ToString();
				r["Quantity on hand(mL)"] = table.Rows[i]["Quantity on hand(mL)"].ToString();
				AMDataTable.Rows.Add(r);
			}
			return AMDataTable;
      }
      /// <summary>Inserts the selected Anesthetic medication and dose values into the anesthmedsgiven table in the database</summary>
	public static int InsertAnesth_Data(int anestheticRecordNum,string anestheticOpen, string anestheticClose, string surgOpen, string surgClose, string anesthetist, string surgeon, string asst, string circulator, string VSMName, string VSMSerNum, string ASA, string ASA_EModifier, int inhO2, int inhN2O, int O2LMin, int N2OLMin, int RteNasCan, int RteNasHood, int RteETT, int MedRouteIVCath, int MedRouteIVButtFly, int MedRouteIM, int MedRoutePO, int MedRouteNasal, int MedRouteRectal, string IVSite, int IVGauge, int IVSideR, int IVSideL, int IVAtt, string IVF, int IVFVol, int MonBP, int MonSpO2, int MonEtCO2, int MonTemp, int MonPrecordial, int MonEKG, string Notes, int PatWgt, int WgtUnitsLbs, int WgtUnitsKgs, int PatHgt, string EscortName, string EscortCellNum, string EscortRel, string NPOTime, int HgtUnitsIn, int HgtUnitsCm){
			int recordnum = AnestheticRecords.GetRecordNum(anestheticRecordNum);
		
			string command = "INSERT INTO anestheticdata (AnestheticRecordNum,AnesthOpen,AnesthClose,SurgOpen,SurgClose,Anesthetist,Surgeon,Asst,Circulator,VSMName,VSMSerNum,ASA,ASA_EModifier,InhO2,InhN2O,O2LMin,N2OLMin,RteNasCan,RteNasHood,RteETT,MedRouteIVCath,MedRouteIVButtFly,MedRouteIM,MedRoutePO,MedRouteNasal,MedRouteRectal,IVSite,IVGauge,IVSideR,IVSideL,IVAtt,IVF,IVFVol,MonBP,MonSpO2,MonEtCO2,MonTemp,MonPrecordial,MonEKG,Notes,PatWgt,WgtUnitsLbs,WgtUnitsKgs,PatHgt,EscortName,EscortCellNum,EscortRel,NPOTime,HgtUnitsIn,HgtUnitsCm)" +
				"VALUES (" + POut.PInt(recordnum) + ",'" 
				+ POut.PString(anestheticOpen) + "','"
				+ POut.PString(anestheticClose) + "','" 
				+ POut.PString(surgOpen) + "','" 
				+ POut.PString(surgClose) + "','" 
				+ POut.PString(anesthetist) + "','" 
				+ POut.PString(surgeon) + "','" 
				+ POut.PString(asst) + "','" 
				+ POut.PString(circulator) + "','"
				+ POut.PString(VSMName) + "','"
				+ POut.PString(VSMSerNum) + "', '"
				+ POut.PString(ASA) + "', '"
				+ POut.PString(ASA_EModifier) + "',"
				+ POut.PInt(inhO2) + ","
				+ POut.PInt(inhN2O) + ","
				+ POut.PInt(O2LMin) + ","
				+ POut.PInt(N2OLMin) + ","
				+ POut.PInt(RteNasCan) + ","
				+ POut.PInt(RteNasHood) + ","
				+ POut.PInt(RteETT) + ","
				+ POut.PInt(MedRouteIVCath) + ","
				+ POut.PInt(MedRouteIVButtFly) + ","
				+ POut.PInt(MedRouteIM) + ","
				+ POut.PInt(MedRoutePO) + ","
				+ POut.PInt(MedRouteNasal) + ","
				+ POut.PInt(MedRouteRectal) + ",'"
				+ POut.PString(IVSite) + "',"
				+ POut.PInt(IVGauge) + ","
				+ POut.PInt(IVSideR) + ","
				+ POut.PInt(IVSideL) + ","
				+ POut.PInt(IVAtt) + ",'"
				+ POut.PString(IVF) + "',"
				+ POut.PInt(IVFVol) + ","
				+ POut.PInt(MonBP) + ","
				+ POut.PInt(MonSpO2) + ","
				+ POut.PInt(MonEtCO2) + ","
				+ POut.PInt(MonTemp) + ","
				+ POut.PInt(MonPrecordial) + ","
				+ POut.PInt(MonEKG) + ",'"
				+ POut.PString(Notes) + "',"
				+ POut.PInt(PatWgt) + ","
				+ POut.PInt(WgtUnitsLbs) + ","
				+ POut.PInt(WgtUnitsKgs) + ","
				+ POut.PInt(PatHgt) + ",'"
				+ POut.PString(EscortName) + "','"
				+ POut.PString(EscortCellNum) + "','"
				+ POut.PString(EscortRel) + "','" 
				+ POut.PString(NPOTime) + "',"
				+ POut.PInt(HgtUnitsIn) + ","
				+ POut.PInt(HgtUnitsCm) + ")" ;
				int val =  General.NonQ(command);
				return val;

		}

	/// <summary>Updates changes to the selected Anesthetic Record's data in the database</summary>
	public static int UpdateAnesth_Data(int anestheticRecordNum, string anestheticOpen, string anestheticClose, string surgOpen, string surgClose, string anesthetist, string surgeon, string asst, string circulator, string VSMName, string VSMSerNum, string ASA, string ASA_EModifier, int inhO2, int inhN2O, int O2LMin, int N2OLMin, int RteNasCan, int RteNasHood, int RteETT, int MedRouteIVCath, int MedRouteIVButtFly, int MedRouteIM, int MedRoutePO, int MedRouteNasal, int MedRouteRectal, string IVSite, int IVGauge, int IVSideR, int IVSideL, int IVAtt, string IVF, int IVFVol, int MonBP, int MonSpO2, int MonEtCO2, int MonTemp, int MonPrecordial, int MonEKG, string Notes, int PatWgt, int WgtUnitsLbs, int WgtUnitsKgs, int PatHgt, string EscortName, string EscortCellNum, string EscortRel, string NPOTime, int HgtUnitsIn, int HgtUnitsCm)
	{
		int recordnum = AnestheticRecords.GetRecordNum(anestheticRecordNum);
		string command = "UPDATE anestheticdata SET "
				+	" AnesthOpen		='"	+ POut.PString(anestheticOpen) + "' "
				+	",AnesthClose		='"	+ POut.PString(anestheticClose) + "' "
				+	",SurgOpen			='"	+ POut.PString(surgOpen) + "' "
				+	",SurgClose			='"	+ POut.PString(surgClose) + "'"
				+	",Anesthetist		='"	+ POut.PString(anesthetist) + "' "
				+	",Surgeon			='"	+ POut.PString(surgeon) + "' "
				+	",Asst				='"	+ POut.PString(asst) + "' "
				+	",Circulator		='"	+ POut.PString(circulator) + "' "
				+	",VSMName			='"	+ POut.PString(VSMName) + "' "
				+	",VSMSerNum			='"	+ POut.PString(VSMSerNum) + "' "
				+	",ASA				='"	+ POut.PString(ASA) + "' "
				+	",ASA_EModifier		='"	+ POut.PString(ASA_EModifier) + "' "
				+	",InhO2				="	+ POut.PInt(inhO2) + " "
				+	",InhN2O			="	+ POut.PInt(inhN2O) + " "
				+	",O2LMin			="	+ POut.PInt(O2LMin) + " "
				+	",N2OLMin			="	+ POut.PInt(N2OLMin) + " "
				+	",RteNasCan			="	+ POut.PInt(RteNasCan) + " "
				+	",RteNasHood		="	+ POut.PInt(RteNasHood) + " "
				+	",RteETT			="	+ POut.PInt(RteETT) + " "
				+	",MedRouteIVCath	="	+ POut.PInt(MedRouteIVCath) + " "
				+	",MedRouteIVButtFly	="	+ POut.PInt(MedRouteIVButtFly) + " "
				+	",MedRouteIM		="	+ POut.PInt(MedRouteIM) + " "
				+	",MedRoutePO		="	+ POut.PInt(MedRoutePO) + " "
				+	",MedRouteNasal		="	+ POut.PInt(MedRouteNasal) + " "
				+	",MedRouteRectal	='"	+ POut.PInt(MedRouteRectal) + " '"
				+	",IVSite			='"	+ POut.PString(IVSite) + "' "
				+	",IVGauge			="	+ POut.PInt(IVGauge) + " "
				+	",IVSideR			="	+ POut.PInt(IVSideR) + " "
				+	",IVSideL			="	+ POut.PInt(IVSideL) + " "
				+	",IVAtt				="	+ POut.PInt(IVAtt) + " "
				+	",IVF				='"	+ POut.PString(IVF) + "' "
				+	",IVFVol			="	+ POut.PInt(IVFVol) + " "
				+	",MonBP				="	+ POut.PInt(MonBP) + " "
				+	",MonSpO2			="	+ POut.PInt(MonSpO2) + " "
				+	",MonEtCO2			="	+ POut.PInt(MonEtCO2) + " "
				+	",MonTemp			="	+ POut.PInt(MonTemp) + " "
				+	",MonPrecordial		="	+ POut.PInt(MonPrecordial) + " "
				+	",MonEKG			="	+ POut.PInt(MonEKG) + " "
				+	",Notes				='"	+ POut.PString(Notes) + "' "
				+	",PatWgt			="	+ POut.PInt(PatWgt) + " "
				+	",WgtUnitsLbs		="	+ POut.PInt(WgtUnitsLbs) + " "
				+	",WgtUnitsKgs		="	+ POut.PInt(WgtUnitsKgs) + " "
				+	",PatHgt			="	+ POut.PInt(PatHgt) + " "
				+	",EscortName		='"	+ POut.PString(EscortName) + "' "
				+	",EscortCellNum		='"	+ POut.PString(EscortCellNum) + "' "
				+	",EscortRel			='"	+ POut.PString(EscortRel) + "' "
				+	",NPOTime			='"	+ POut.PString(NPOTime) + "' "
				+	",HgtUnitsIn		="	+ POut.PInt(HgtUnitsIn) + " "
				+	",HgtUnitsCm		="	+ POut.PInt(HgtUnitsCm)+ " "
				+	"WHERE AnestheticRecordNum =" + anestheticRecordNum + "";	
		int val = General.NonQ(command);
		return val;
	}
		/// <summary>Inserts the data from anesthetic intake form into the anesthmedsintake table in the database</summary>
		public static void InsertMed_Intake(string AMedName,int qty,string supplier,string invoice){

			string AMname = AMedName, Inum = invoice;
			if (AMedName.Contains("'"))
				{
					AMname = AMedName.Replace("'", "''");
				}
			if (invoice.Contains("'"))
				{
					Inum = invoice.Replace("'", "''");
				}
				
			if (qty == 0)
				{
					qty = 0;
				}
		 
			string command = "INSERT INTO anesthmedsintake(IntakeDate,AnesthMedName,Qty,SupplierIDNum,InvoiceNum)values('" + MiscData.GetNowDateTime().ToString("yyyy-MM-dd hh:mm:ss") + "','" + AMname + "'," + qty + ",'" + supplier + "','" + Inum + "')";
			General.NonQ(command);
			string command1 = "UPDATE anesthmedsinventory SET QtyOnHand = '" + qty + "' WHERE AnesthMedName = '" + AMname + "'";
			General.NonQ(command1);
		}
		/// <summary>Inserts the newly added anesthetic medication and How supplied into the anesthmedsgiven table in the database</summary>
		public static void Insertanesth_howsupplied(string anesth_Medname, string howSupplied){
			string AMedname = anesth_Medname, HSupplied = howSupplied;
			if (anesth_Medname.Contains("'"))
				{
					AMedname = anesth_Medname.Replace("'", "''");
				}
			if (howSupplied.Contains("'"))
				{
					HSupplied = howSupplied.Replace("'", "''");
				}
			string command = "INSERT INTO anesthmedsinventory(AnesthMedName,AnesthHowSupplied) VALUE('" + AMedname + "','" + HSupplied + "')";
			General.NonQ(command);
		}
		/// <summary>Inserts the newly added anesthetic medication and How supplied into the anesthmedsgiven table in the database</summary>
		public static void InsertAMedDose(string anesth_Medname, decimal dose, int patnum){
			int anesthrecnum = AnestheticRecords.GetRecordNum(patnum);
			string AMName = anesth_Medname;
			int amtwasted = 0;
			if (anesth_Medname.Contains("'"))
				{
					AMName = anesth_Medname.Replace("'", "''");
				}
			string command = "INSERT INTO anesthmedsgiven(AnestheticRecordNum,AnesthMedName,QtyGiven,QtyWasted,DoseTimeStamp) VALUE('" + anesthrecnum + "','" + AMName + "','" + dose + "', '" + amtwasted + "', '" + MiscData.GetNowDateTime().ToString("hh:mm:ss tt") + "')";
			General.NonQ(command);
		}
		/// <summary>Gets the data from anesthmedsgiven table</summary>
		public static DataTable GetdataForGrid() {
			string command = "SELECT AnesthMedName as 'Anesthetic Medication', QtyGiven as 'Dose', QtyWasted as 'Dose Wasted',DoseTimeStamp as 'Time Stamp' FROM anesthmedsgiven order by AnestheticMedNum  desc";
			DataTable table = General.GetTable(command);
			DataTable AMDataTable = table.Clone();//does not copy any data
			AMDataTable.TableName = "anesthmedsgiven";
			for (int i = 0; i < AMDataTable.Columns.Count; i++)
				{
					AMDataTable.Columns[i].DataType = typeof(string);
				}
			DataRow r;
			DateTime date;
			for (int i = 0; i < table.Rows.Count; i++)
				{
					r = AMDataTable.NewRow();
					r["Anesthetic medication"] = table.Rows[i]["Anesthetic medication"].ToString();
					r["Dose"] = table.Rows[i]["Dose"].ToString();
					r["Dose Wasted"] = table.Rows[i]["Dose Wasted"].ToString();
					r["Time Stamp"] = table.Rows[i]["Time Stamp"].ToString();
					AMDataTable.Rows.Add(r);
				}
			return AMDataTable;
		}
		/// <summary>Updates the table anesthmedsinventory with the new quantity adjustment</summary>
		public static void UpdateAMedInvAdj(string anesthMedName, double qty, double qtyOnHand){
			double adjQty = 0.0;
			adjQty = Convert.ToDouble(qty) + Convert.ToDouble(qtyOnHand);

			string AMedname = anesthMedName;
			if (anesthMedName.Contains("'"))
				{
					AMedname = anesthMedName.Replace("'", "''");
				}
			string command = "UPDATE anesthmedsinventory SET QtyOnHand = '" + Convert.ToString(adjQty) + "' WHERE AnesthMedName = '" + anesthMedName + "'";
			General.NonQ(command);

		}

		/// <summary>Updates the table anesthmedsinventoryadj</summary>
		public static void UpdateMedNum(string notes, string adjPos, string aMed, string howSupplied, int oldqty, int rownumber){
			string notes2 = notes, adjPos2 = adjPos, aMed2 = aMed, howSupplied2 = howSupplied;
			if (notes.Contains("'"))
				{
					notes2 = notes.Replace("'", "''");
				}
			if (adjPos.Contains("'"))
				{
					adjPos2 = adjPos.Replace("'", "''");
				}
			if (aMed.Contains("'"))
				{
					aMed2 = aMed.Replace("'", "''");
				}
			if (howSupplied.Contains("'"))
				{
					howSupplied2 = howSupplied.Replace("'", "''");
				}
			string command = "UPDATE anesthmedsinventoryadj SET anestheticMedNum=" + rownumber + " WHERE notes='" + notes2 + "' AND adjpos='" + adjPos2 + "' AND anestheticMedNum=" + rownumber + "";
			General.NonQ(command);
		}

		/// <summary>Updates the table anesthmedsinventory</summary>

		public static void UpdateAMed_Adj_Qty(string aMed, string howsupplied, int qtyOnHand, int newQTY){

			string aMed2 = aMed, howsupplied2 = howsupplied;
			if (aMed.Contains("'"))
			{
				aMed2 = aMed.Replace("'", "''");
			}
			if (howsupplied.Contains("'"))
			{
				howsupplied2 = howsupplied.Replace("'", "''");
			}
			string command = "UPDATE anesthmedsinventory SET QtyOnHand=" + newQTY + " WHERE AnestheticMed= '" + aMed2 + "' and AnesthHowSupplied='" + howsupplied2 + "' and QtyOnHand=" + qtyOnHand;
			General.NonQ(command);
		}
		
		/// <summary>Delete rows from the table anesthmedsgiven</summary>
		public static void DeleteRow(string anesthMedName, decimal  QtyGiven, string TimeStamp){

			string command = "DELETE FROM anesthmedsgiven WHERE AnesthMedName='" + anesthMedName + "' and QtyGiven=" + QtyGiven + " and DoseTimeStamp='" + TimeStamp.ToString() + "'";
			General.NonQ(command);
		}

		public static void Update(string aMed, string howsupplied, int qtyOnHand, string notes, int oldQty, int medNum){

			string notes2 = notes, aMed2 = aMed, howsupplied2 = howsupplied;
			if (notes.Contains("'"))
				{
					notes2 = notes.Replace("'", "''");
				}
			if (aMed.Contains("'"))
				{
					aMed2 = aMed.Replace("'", "''");
				}
			if (howsupplied.Contains("'"))
				{
					howsupplied2 = howsupplied.Replace("'", "''");
				}
			int mednum = AMedications.GetMedNum(aMed2,howsupplied2, oldQty);
			int medadj = AMedications.GetAdjMedNum(mednum, notes);

			if (mednum == medadj && mednum != 0 && medadj != 0)
				{
					string command1 = "UPDATE anesthmedsinventoryadj SET AnestheticMedNum=" + AMedications.GetMedNum(aMed2, howsupplied2, qtyOnHand) + " WHERE notes='" + notes2 + "'";
					General.NonQ(command1);
				}
			else
				{
					string command1 = "INSERT INTO anesthmedsinventoryadj(AnestheticMedNum) VALUES(" + AMedications.GetMedNum(aMed2, howsupplied2, qtyOnHand) + ")";
					General.NonQ(command1);
				}
			string command = "UPDATE anesthmedsinventoryadj SET Notes='" + notes2 + "' WHERE AnestheticMedNum=" + AMedications.GetMedNum(aMed2, howsupplied2, qtyOnHand);
			General.NonQ(command);
		}
		public static int GetMedNum2(string aMed, double qtyOnHand){

			MySqlCommand command2 = new MySqlCommand();
			if (con.State == ConnectionState.Open)
				con.Close();
			con.Open();
			command2.CommandText = "SELECT AnestheticMedNum FROM anesthmedsinventory WHERE AnesthMedName= '" + (aMed) + "'";
			command2.Connection = con;
			string mednum = Convert.ToString(command2.ExecuteScalar());
			int medid = 0;
			if (mednum != null && mednum != "")
				return medid = Convert.ToInt32(mednum);
			else
				return medid;
		}

		public static int GetMedNum(string aMed, string howsupplied, int qtyOnHand){

			string command = "SELECT AnestheticMedNum FROM anesthmedsinventory WHERE AnesthMedName= '" + aMed + "' and AnesthHowSupplied='" + howsupplied + "' and QtyOnHand=" + qtyOnHand + "";
			return General.NonQ(command);
			//int medid = 0;
			//if (mednum != null && mednum != "")
			//    return medid = Convert.ToInt32(mednum);
			//else
			//    return medid;
		}

		/// <summary> Gets the anestheticmednum from the anesthmedsinventoryadj table. /// </summary>

		public static int GetAdjMedNum(int mednum, string notes){
			
			string command = "SELECT AnestheticMedNum FROM anesthmedsinventoryadj WHERE AnestheticMedNum  =" + mednum + "";
			return General.NonQ(command);
			//int medid = 0;
			//if (mednumadj != null && mednumadj != "")
			//    return medid = Convert.ToInt32(mednumadj);
			//else
			//    return medid;
		}
		public static DataTable GetMedNumber(int rownumber){
			
			string command = "SELECT AnestheticMedNum FROM anesthmedsinventoryadj WHERE AnestheticMedNum=" + rownumber;
			DataTable dt = new DataTable();
			dt = General.GetTable(command);
			DataTable dtclone = dt.Clone();

			for (int i = 0; i < dtclone.Columns.Count; i++)
			{
				dtclone.Columns[i].DataType = typeof(string);
			}
			DataRow r;
			for (int i = 0; i < dt.Rows.Count; i++)
				{
					r = dtclone.NewRow();
					r["AnestheticMedNum"] = dt.Rows[i]["AnestheticMedNum"].ToString();
					//r["How Supplied"] = dt.Rows[i]["How Supplied"].ToString();
					//r["Quantity on hand(mLs)"] = dt.Rows[i]["Quantity on hand(mLs)"].ToString();
					//r["Quantity Adjustment(mLs)"] = "";
					//r["Notes"] = dt.Rows[i]["Notes"].ToString();
					dtclone.Rows.Add(r);
			}
				return dtclone;
		}

		/// <summary>Returns QtyOnHand for anesthetic medication inventory adjustment calculations/// </summary>

		public static double GetQtyOnHand(string aMed){

			MySqlCommand cmd = new MySqlCommand();
			con = new MySqlConnection(DataSettings.ConnectionString);
			cmd.Connection = con;
			if (con.State == ConnectionState.Open)
				con.Close();
			con.Open();

			//if (con.State == ConnectionState.Open) MessageBox.Show("Connection to MySQL opened through OLE DB Provider"); //for testing mySQL connection
			cmd.CommandText = "SELECT QtyOnHand FROM anesthmedsinventory WHERE AnesthMedName='" + aMed + "'";
			string QtyOnHand = Convert.ToString(cmd.ExecuteScalar());
			int qtyOnHand = Convert.ToInt32(QtyOnHand);
			con.Close();
			return qtyOnHand;

		}

	}
	
}
