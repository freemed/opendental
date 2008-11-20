using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace OpenDentBusiness
{
    ///<summary></summary>
    public class AnesthMedsInventoryAdjs
    {
        ///<summary>Gets data from anesthmedsinventory</summary>
        public static DataTable RefreshCache()
        {
            string command = "SELECT * FROM anesthmedsinventory";
            DataTable table = General.GetTable(command);
            table.TableName = "anesthmedsinventory";
            FillCache(table);
            return table;
        }
        public static void FillCache(DataTable table)
        {
            ArrayList AL = new ArrayList();
            AnesthMedsInventoryC.ListLong = new AnesthMedsInventory[table.Rows.Count];
            List<AnesthMedsInventory> provList = TableToList(table);
            for (int i = 0; i < provList.Count; i++)
            {
                AnesthMedsInventoryC.ListLong[i] = provList[i];
                AL.Add(AnesthMedsInventoryC.ListLong[i]);
            }
            AnesthMedsInventoryC.List = new AnesthMedsInventory[AL.Count];
            AL.CopyTo(AnesthMedsInventoryC.List);
            
        }

        private static List<AnesthMedsInventory> TableToList(DataTable table)
        {
            List<AnesthMedsInventory> retVal = new List<AnesthMedsInventory>();
            AnesthMedsInventory prov;
            for (int i = 0; i < table.Rows.Count; i++)
            {
                prov = new AnesthMedsInventory();
                prov.AnestheticMedNum = PIn.PInt(table.Rows[i][0].ToString());
                prov.AnesthMedName = PIn.PString(table.Rows[i][1].ToString());
                prov.AnesthHowSupplied = PIn.PString(table.Rows[i][2].ToString());
                prov.QtyOnHand = PIn.PString(table.Rows[i][3].ToString());
                retVal.Add(prov);
            }
            return retVal;
        }

        ///<summary></summary>
        public static string GetAbbr(int userNum)
        {
            if (AnesthMedsInventoryC.ListLong == null)
            {
                RefreshCache();
            }
            for (int i = 0; i < AnesthMedsInventoryC.ListLong.Length; i++)
            {
                if (AnesthMedsInventoryC.ListLong[i].AnestheticMedNum == userNum)
                {
                    return AnesthMedsInventoryC.ListLong[i].AnesthMedName;
                }
            }
            return "";
        }
        /*public static string GetLongDesc(int userNum)
        {
            for (int i = 0; i < AnesthMedsInventoryC.ListLong.Length; i++)
            {
                if (AnesthMedsInventoryC.ListLong[i].AnestheticMedNum == userNum)
                {
                    return AnesthMedsInventoryC.ListLong[i].GetLongDesc();
                }
            }
            return "";
        }*/
    }
}
