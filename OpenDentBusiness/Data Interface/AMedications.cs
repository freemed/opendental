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
  public class AMedications
	 
    {
		///<summary>This data adapter is used for all queries to the database.</summary>
		static MySqlConnection con;
		public MySqlCommand cmd;
		
	  
      public static DataTable GetAMDataTable() 
      {
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
      public static DataTable GetAMInventory()
      {
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
	public static int InsertAnesth_Data(int patID, string anestheticOpen, string anestheticClose, string surgOpen, string surgClose, string anesthetist, string surgeon, string asst, string circulator, string ASA, string ASA_EModifier, int inhO2, int inhN20, int O2LMin, int N2OLMin, int RteNasCan, int RteNasHood, int RteETT, int MedRouteIVCath, int MedRouteIVButtFly, int MedRouteIM,int MedRouteNasal, int MedRoutePO, int MedRouteRectal, int IVGauge, string IVSite, int IVSideR, int IVSideL, int IVAtt, string IVF, int IVFVol, int MonBP, int MonEKG, int MonEtCO2, int MonPrecordial, int MonSpO2, int MonTemp, int PatWgt, int WgtUnitsLbs, int WgtUnitsKgs, int PatHgt, int HgtUnitsIn, int HgtUnitsCm, string NPOTime, string EscortName, string EscortCellNum, string EscortRel, string Notes, string VSMName, string VSMSerNum)
      {
          int recordnum = AnestheticRecords.GetRecordNum(patID);
          string command = "INSERT INTO anestheticdata(AnestheticRecordNum,AnesthOpen,AnesthClose,SurgOpen,SurgClose,Anesthetist,Surgeon,Asst,Circulator,ASA,ASA_EModifier,inhO2,inhN2O,o2LMin,n2OLMin,RteNasCan,RteNasHood,RteETT,MedRouteIVCath,MedRouteIVButtFly,MedRouteIM, MedRouteNasal,MedRoutePO, MedRouteRectal, IVGauge,IVSite,IVSideR,IVSideL,IVAtt,IVF,IVFVol,MonBP,MonEKG,MonEtCO2,MonPrecordial,MonSpO2,MonTemp,PatWgt,WgtUnitsLbs,WgtUnitsKgs,PatHgt,wgtUnitsIn,WgtUnitsCm,NPOTime,EscortName,EscortRel,Notes,VSMName,VSMSerNum)" +
			  "values(" + recordnum + ",'" + anestheticOpen + "','" + anestheticClose + "','" + surgOpen + "','" + surgClose + "','" + anesthetist + "','" + surgeon + "','" + asst + "','" + circulator + "','" + ASA + "', '" + ASA_EModifier + "'," + inhO2 + ", " + inhN20 + ", " + O2LMin + ", " + N2OLMin + ", " + RteNasCan + ", " + RteNasHood + ", " + RteETT + ", " + MedRouteIVCath + ", " + MedRouteIVButtFly + ", " + MedRouteIM + ", " + MedRouteNasal + ", " + MedRoutePO + "," + MedRouteRectal + ", " + IVGauge + ", '" + IVSite + "', " + IVSideR + ", " + IVSideL + ", " + IVAtt + ", '" + IVF + "', " + IVFVol + ", " + MonBP + "," + MonEKG + "," + MonEtCO2 + "," + MonPrecordial + ", " + MonSpO2 + "," + MonTemp + "," + PatWgt + ", " + WgtUnitsLbs + ", " + WgtUnitsKgs + ", " + PatHgt + ", " + HgtUnitsIn + "," + HgtUnitsCm + ",'" + NPOTime + "', '" + EscortName + "', '" + EscortCellNum + "', '" + EscortRel + "', '" + Notes +  "', '" + VSMName + "','" + VSMSerNum + "')";
          int val =  General.NonQ(command);
          return val;
      }

      /// <summary>Inserts the data from anesthetic intake form into the anesthmedsintake table in the database</summary>
      public static void InsertMed_Intake(string AMedName,int qty,string supplier,string invoice)
      {
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
		public static void Insertanesth_howsupplied(string anesth_Medname, string howSupplied)   
		{
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
		public static void InsertAMedDose(string anesth_Medname, decimal dose, int patnum)
		{
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
		public static DataTable GetdataForGrid() 
		{
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
		public static void UpdateAMedInvAdj(string anesthMedName, double qty, double qtyOnHand)
		{
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
		public static void UpdateMedNum(string notes, string adjPos, string aMed, string howSupplied, int oldqty, int rownumber)
		{
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
			string command = "update anesthmedsinventoryadj set anestheticMedNum=" + rownumber + " where notes='" + notes2 + "' and adjpos='" + adjPos2 + "' and anestheticMedNum=" + rownumber + "";
			General.NonQ(command);
		}

	       /// <summary>Updates the table anesthmedsinventory</summary>
    
	  public static void UpdateAMed_Adj_Qty(string aMed, string howsupplied, int qtyOnHand,int newQTY)
      {
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
		public static void DeleteRow(string anesthMedName, decimal  QtyGiven, string TimeStamp)
		{
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
		public static int GetMedNum2(string aMed, double qtyOnHand)
		{
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


		public static double GetQtyOnHand(string aMed)
		{
			MySqlCommand cmd = new MySqlCommand();
			con = new MySqlConnection(DataSettings.ConnectionString);
			cmd.Connection = con;
			if (con.State == ConnectionState.Open)
				con.Close();
			con.Open();

			//if (con.State == ConnectionState.Open) MessageBox.Show("Connection to MySQL opened through OLE DB Provider"); 
			cmd.CommandText = "SELECT QtyOnHand FROM anesthmedsinventory WHERE AnesthMedName='" + aMed + "'";
			string QtyOnHand = Convert.ToString(cmd.ExecuteScalar());
			int qtyOnHand = Convert.ToInt32(QtyOnHand);
			con.Close();
			return qtyOnHand;

		}

	}
	
}
