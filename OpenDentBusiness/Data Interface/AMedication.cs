using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using OpenDental.DataAccess;

namespace OpenDentBusiness
{
    /// <summary>Handles database commands for the anesthmedsgiven table in the database</summary>
  public static class AMedication
    {
      /// <summary>Gets the data from anestheticdata table</summary>
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
      public static int Insertanesth_dose(int patID, string anestheticOpen, string anestheticClose, string surgOpen, string surgClose, string anesthetist, string surgeon, string asst, string circulator, string ASA, int inho2, int inhN20, int o2LMin, int N2oLMin, int RteNasCan, int RteNasHood, int RteETT, int MedRouteIVCath, int MedRouteIVButtFly, int IVGauge, int IVSiteR, int IVSiteL, int IVAtt, string IVF, int IVFVol, int PatWgt, int WgtUnitsLbs, int WgtUnitsKgs, int PatHgt, string NPOTime, string EscortName, string EscortRel)
      {
          int recordnum = DataConnection.getRecordNum(patID);
          string command = "insert into anestheticdata(AnestheticRecordNum,AnesthOpen,AnesthClose,SurgOpen,SurgClose,Anesthetist,Surgeon,Asst,Circulator,ASA,inho2,inhN2o,o2LMin,N2oLMin,RteNasCan,RteNasHood,RteETT,MedRouteIVCath,MedRouteIVButtFly,IVGauge,IVSideR,IVSideL,IVAtt,IVF,IVFVol,PatWgt,WgtUnitsLbs,WgtUnitsKgs,PatHgt,NPOTime,EscortName, EscortRel )" +
                                               "values(" + recordnum + ",'" + anestheticOpen + "','" + anestheticClose + "','" + surgOpen + "','" + surgClose + "','" + anesthetist + "','" + surgeon + "','" + asst + "','" + circulator + "','" + ASA + "', " + inho2 + ", " + inhN20 + ", " + o2LMin + ", " + N2oLMin + ", " + RteNasCan + ", " + RteNasHood + ", " + RteETT + ", " + MedRouteIVCath + ", " + MedRouteIVButtFly + ", " + IVGauge + ", " + IVSiteR + ", " + IVSiteL + ", " + IVAtt + ", '" + IVF + "', " + IVFVol + ", " + PatWgt + ", " + WgtUnitsLbs + ", " + WgtUnitsKgs + ", " + PatHgt + ", '" + NPOTime + "', '" + EscortName + "', '" + EscortRel + "')";
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
          string command = "insert into anesthmedsintake(IntakeDate,AnesthMedName,Qty,SupplierIDNum,InvoiceNum)values('" + MiscData.GetNowDateTime().ToString("yyyy-MM-dd hh:mm:ss") + "','" + AMname + "'," + qty + ",'" + supplier + "','" + Inum + "')";
          General.NonQ(command);
          string command1 = "update anesthmedsinventory set qtyonhand = '" + qty + "' where anesthmedname = '" + AMname + "'";
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
          string command = "insert into anesthmedsinventory(AnesthMedName,AnesthHowSupplied) value('" + AMedname + "','" + HSupplied + "')";
          General.NonQ(command);
      }
      /// <summary>Inserts the newly added anesthetic medication and How supplied into the anesthmedsgiven table in the database</summary>
      public static void InsertanesthMed_dose(string anesth_Medname, decimal dose)
      {
          string AMName = anesth_Medname;
          if (anesth_Medname.Contains("'"))
          {
              AMName = anesth_Medname.Replace("'", "''");
          }
          string command = "insert into anesthmedsgiven(AnesthMedName,QtyGiven,DoseTimeStamp) value('" + AMName + "'," + dose + ",'" + MiscData.GetNowDateTime().ToString("yyyy-MM-dd hh:mm:ss") + "')";
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
      /// <summary>Updates/Inserts the table anesthmedsinventoryadj</summary>
      public static void updateMed_adj(string anesthmedname, string howsupplied, int qtyOnHand, string qtyAdj, string notes,int oldQty)
      {
          string notes2 = notes, aMed2 = anesthmedname, howsupplied2 = howsupplied;
          if (notes.Contains("'"))
          {
              notes2 = notes.Replace("'", "''");
          }
          if (anesthmedname.Contains("'"))
          {
              aMed2 = anesthmedname.Replace("'", "''");
          }
          if (howsupplied.Contains("'"))
          {
              howsupplied2 = howsupplied.Replace("'", "''");
          }
          int mednum = DataConnection.getMedNum(aMed2, howsupplied2, oldQty);
          updateMedNum(notes2, qtyAdj, aMed2, howsupplied2, oldQty);
          int medadj = DataConnection.getadjMedNum(mednum,notes);

          if (mednum == medadj && mednum != 0 && medadj != 0)
          {
              string command = "update anesthmedsinventoryadj set AdjPos= '" + qtyAdj + "' ,Notes='" + notes2 + "' where AnestheticMedNum=" + DataConnection.getMedNum(aMed2,howsupplied2,qtyOnHand);
              General.NonQ(command);
          }
          else 
          {
              string command = "insert into anesthmedsinventoryadj (AnestheticMedNum,AdjPos,Notes)values( " + mednum + ", '" + qtyAdj + "' ,'" + notes2 + "')";
              General.NonQ(command);
          }
      }
      /// <summary>Updates the table anesthmedsinventoryadj</summary>
      public static void updateMedNum(string notes,string adjPos,string aMed,string howSupplied,int oldqty) 
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
          string command="update anesthmedsinventoryadj set anestheticMedNum="+DataConnection.getMedNum(aMed2,howSupplied2,oldqty)+" where notes='"+notes2+"' and adjpos='"+adjPos2+"'";
          General.NonQ(command);
      }
      /// <summary>Delete rows from the table anesthmedsgiven</summary>
      public static void deleteRow(string anesthMedName, decimal  QtyGiven, string TimeStamp)
      {
          string command = "delete from anesthmedsgiven where AnesthMedName='" + anesthMedName + "' and QtyGiven=" + QtyGiven + " and DoseTimeStamp='" + TimeStamp.ToString() + "'";
          General.NonQ(command);
      }
      /// <summary>Updates the table anesthmedsinventory</summary>
      public static void updateMed_adj_qty(string aMed, string howsupplied, int qtyOnHand,int newQTY)
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
          string command = "update anesthmedsinventory set QtyOnHand=" + newQTY + " where AnesthMedName= '" + aMed2 + "' and AnesthHowSupplied='" + howsupplied2 + "' and QtyOnHand=" + qtyOnHand;
          General.NonQ(command);
      }
      /// <summary>Gets the data from anesthmedsinventory table</summary>
      public static DataTable GetdataForGridADJ()
      {
          string command = "select distinct a.AnesthMedName as 'Anesthetic Medication',a.AnesthHowSupplied as 'How Supplied',a.QtyOnHand as 'Quantity on hand(mLs)',b.adjpos as 'Quantity Adjustment(mLs)',b.notes as 'Notes' from anesthmedsinventory a left join anesthmedsinventoryadj b  on a.AnestheticMedNum  =   b.AnestheticMedNum order by a.AnestheticMedNum desc";
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
              r["Anesthetic Medication"] = dt.Rows[i]["Anesthetic Medication"].ToString();
              r["How Supplied"] = dt.Rows[i]["How Supplied"].ToString();
              r["Quantity on hand(mLs)"] = dt.Rows[i]["Quantity on hand(mLs)"].ToString();
              //r["Quantity Adjustment(mLs)"] = dt.Rows[i]["Quantity Adjustment(mLs)"].ToString();
              r["Quantity Adjustment(mLs)"] = "";
              r["Notes"] = dt.Rows[i]["Notes"].ToString();
              dtclone.Rows.Add(r);
          }
          return dtclone;
      }
      public static void update(string aMed, string howsupplied, int qtyOnHand,string notes,int oldQty)
      {
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
          int mednum = DataConnection.getMedNum(aMed2, howsupplied2, oldQty);
          int medadj = DataConnection.getadjMedNum(mednum, notes);

          if (mednum == medadj && mednum != 0 && medadj != 0)
          {
              string command1 = "update anesthmedsinventoryadj set AnestheticMedNum=" + DataConnection.getMedNum(aMed2, howsupplied2, qtyOnHand) + " where notes='" + notes2 + "'";
              General.NonQ(command1);
          }
          else
          {
              string command1 = "insert into anesthmedsinventoryadj(AnestheticMedNum) values(" + DataConnection.getMedNum(aMed2, howsupplied2, qtyOnHand) + ")";
              General.NonQ(command1);
          }
              string command = "update anesthmedsinventoryadj set Notes='" + notes2 + "' where AnestheticMedNum=" + DataConnection.getMedNum(aMed2, howsupplied2, qtyOnHand);
              General.NonQ(command);
      }
    }
}
