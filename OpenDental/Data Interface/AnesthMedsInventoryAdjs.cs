using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Windows.Forms;
using OpenDentBusiness;
using OpenDental;
using OpenDental.DataAccess;

namespace OpenDental
{
    ///<summary></summary>
    public class AnesthMedsInventoryAdjs
    {
        public static AnesthMedsInventoryAdjT[] List;
        ///<summary>Gets all Anesthetic Medications from the database</summary>
        public static List<AnesthMedsInventoryAdjT> CreateObjects()
        {
            string command = "SELECT * FROM anesthmedsinventoryadj ORDER BY AnestheticMedNum";
            return new List<AnesthMedsInventoryAdjT>(DataObjectFactory<AnesthMedsInventoryAdjT>.CreateObjects(command));
        }

        ///<summary></summary>
        public static void WriteObject(AnesthMedsInventoryAdjT med)
        {
            DataObjectFactory<AnesthMedsInventoryAdjT>.WriteObject(med);
        }
        public static void updateMed_adj_qty(string aMed, string howsupplied, int qtyOnHand, int newQTY)
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
           //string command = "update anesthmedsinventory set QtyOnHand=" + newQTY + " where AnesthMedName= '" + aMed2 + "' and AnesthHowSupplied='" + howsupplied2 + "' and QtyOnHand=" + qtyOnHand;
            string command = "UPDATE anesthmedsinventory SET QtyOnHand=" + newQTY + " WHERE AnesthMedName= '" + aMed2 + "' AND AnesthHowSupplied='" + howsupplied2 + "'";
            General.NonQ(command);
        }
        public static AnesthMedsInventoryAdjT GetLim()
        {
            string command =
                "SELECT *"
                + " FROM anesthmedsinventoryadj order by AnestheticMedNum desc";
            DataTable table = General.GetTable(command);
            if (table.Rows.Count == 0)
            {
                return new AnesthMedsInventoryAdjT();
            }

            AnesthMedsInventoryAdjT Lim = new AnesthMedsInventoryAdjT();
            Lim.AnestheticMedNum = PIn.PInt(table.Rows[0][0].ToString());
            //Lim.AdjPos = PIn.PInt(table.Rows[0][1].ToString());
            //Lim.AdjNeg = PIn.PInt(table.Rows[0][2].ToString());
            Lim.Provider = PIn.PString(table.Rows[0][3].ToString());
            Lim.Notes = PIn.PString(table.Rows[0][4].ToString());
            Lim.TimeStamp = PIn.PDateT(table.Rows[0][5].ToString());

            return Lim;
        }

        public static int getmedName(string medName,string howsupplied, int qtyOnHand)
        {
            string medName2 = medName, howsupplied2 = howsupplied;
            if (medName.Contains("'"))
            {
                medName2 = medName.Replace("'", "''");
            }
            if (howsupplied.Contains("'"))
            {
                howsupplied2 = howsupplied.Replace("'", "''");
            }

           //select AnestheticMedNum from anesthmedsinventory where AnestheticMed= '" + aMed + "' and AnesthHowSupplied='" + howsupplied + "' and QtyOnHand=" + qtyOnHand + "";
            string command = "SELECT AnestheticMedNum "
                            + " FROM anesthmedsinventory"
                             + " where AnesthMedName = '" + medName2 + "' and AnesthHowSupplied='" + howsupplied2 + "' and QtyOnHand=" + qtyOnHand + "";

            return General.NonQ(command);
        }

        /////<summary>Surround with try-catch.</summary>
        //public static void DeleteObject(AnesthMedsIntake med)
        //{
        //    //validate that not already in use.
        //    string command = "SELECT COUNT(*) FROM anesthmedsinventory WHERE AnestheticMedNum=" + POut.PInt(med.AnestheticMedNum);
        //    int count = PIn.PInt(General.GetCount(command));
        //    //disabled during development, will probably need to enable for release
        //    /*if(count>0) {
        //        throw new ApplicationException(Lan.g("AnestheticMeds","Anesthetic Medication is already in use. Not allowed to delete."));
        //    }*/
        //    command = "SELECT COUNT(*) FROM anesthmedsinventory WHERE AnestheticMedNum=" + POut.PInt(med.AnestheticMedNum);
        //    count = PIn.PInt(General.GetCount(command));
        //    //disabled for now...
        //    /*if(count>0) {
        //        throw new ApplicationException(Lan.g("AnestheticMeds","Anesthetic Medication is already in use. Not allowed to delete."));
        //    }*/
        //    DataObjectFactory<AnesthMedsInventory>.DeleteObject(med);
        //}

        //public static string GetName(List<AnesthMedsInventoryAdj> listAnesthMedIntake, int anestheticMedNum)
        //{
        //    for (int i = 0; i < listAnesthMedIntake.Count; i++)
        //    {
        //        if (listAnesthMedIntake[i].AnestheticMedNum == anestheticMedNum)
        //        {
        //            return listAnesthMedIntake[i].AnesthMedName;
        //        }
        //    }
        //    return "";
        //}


    }







}









