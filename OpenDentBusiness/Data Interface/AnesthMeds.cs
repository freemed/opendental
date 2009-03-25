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
		public class AnesthMeds{

		///<summary>This data adapter is used for all queries to the database.</summary>
		static MySqlConnection con;
		public MySqlCommand cmd;
				
		///<summary>Gets all Anesthetic Medications from the database</summary>
		public static List<AnesthMedsInventory> CreateObjects() {
			string command="SELECT * FROM anesthmedsinventory ORDER BY AnesthMedName";
			return new List<AnesthMedsInventory>(DataObjectFactory<AnesthMedsInventory>.CreateObjects(command));
		}

		public static void WriteObject(AnesthMedsInventory med){
			DataObjectFactory<AnesthMedsInventory>.WriteObject(med);
		}

		///<summary>Deletes and Anesthetic Medication from inventory if it has never been given to a patient</summary>
		public static void DeleteObject(AnesthMedsInventory med){
			//validate that anesthetic med is not already in use.
			string command = "SELECT COUNT(*) FROM anesthmedsgiven WHERE AnesthMedNum=" + POut.PInt(med.AnestheticMedNum);
			int count=PIn.PInt(General.GetCount(command));
			if (count > 0)
			{
				MessageBox.Show(Lan.g("AnesthMeds", "Anesthetic Medication is already in use. Not allowed to delete."));
			}

			else
			{
				DataObjectFactory<AnesthMedsInventory>.DeleteObject(med);
			}
		}

		public static string GetName(List<AnesthMedsInventory> listAnesthMedInventory, int anestheticMedNum){
			for (int i = 0; i < listAnesthMedInventory.Count; i++)
			{
				if (listAnesthMedInventory[i].AnestheticMedNum == anestheticMedNum)
				{
					return listAnesthMedInventory[i].AnesthMedName;
				}
			}
			return "";
		}

		public static string GetQtyOnHand(List<AnesthMedsInventory> listAnesthMedInventory,int anestheticMedNum){
			for (int i = 0; i < listAnesthMedInventory.Count; i++){
				if (listAnesthMedInventory[i].AnestheticMedNum == anestheticMedNum){
					return listAnesthMedInventory[i].QtyOnHand;
				}
			}
			return "";
		}

		/// <summary> Gets the Anesthetic Record number from the anestheticrecord table./// </summary>
		public static int getRecordNum(int patnum){
			MySqlCommand command2 = new MySqlCommand();
			con.Open();
			command2.CommandText = "SELECT Max(AnestheticRecordNum)  FROM opendental_test.anestheticrecord a,opendental_test.Patient p where a.Patnum = p.Patnum and p.patnum = " + patnum + "";
			command2.Connection = con;
			int supplierID = Convert.ToInt32(command2.ExecuteScalar());
			con.Close();
			return supplierID;
			
		}

  ///<summary>Inserts the selected Anesthetic medication and dose values into the anesthmedsgiven table in the database</summary>
		public static int InsertAnesth_Data(int anestheticRecordNum,string anestheticOpen, string anestheticClose, string surgOpen, string surgClose, string anesthetist, string surgeon, string asst, string circulator, string VSMName, string VSMSerNum, string ASA, string ASA_EModifier, int O2LMin, int N2OLMin, int RteNasCan, int RteNasHood, int RteETT, int MedRouteIVCath, int MedRouteIVButtFly, int MedRouteIM, int MedRoutePO, int MedRouteNasal, int MedRouteRectal, string IVSite, int IVGauge, int IVSideR, int IVSideL, int IVAtt, string IVF, int IVFVol, int MonBP, int MonSpO2, int MonEtCO2, int MonTemp, int MonPrecordial, int MonEKG, string Notes, int PatWgt, int WgtUnitsLbs, int WgtUnitsKgs, int PatHgt, string EscortName, string EscortCellNum, string EscortRel, string NPOTime, int HgtUnitsIn, int HgtUnitsCm, string Signature, bool SigIsTopaz){
				
			int recordnum = AnestheticRecords.GetRecordNum(anestheticRecordNum);
			string command = "INSERT INTO anestheticdata (AnestheticRecordNum,AnesthOpen,AnesthClose,SurgOpen,SurgClose,Anesthetist,Surgeon,Asst,Circulator,VSMName,VSMSerNum,ASA,ASA_EModifier,O2LMin,N2OLMin,RteNasCan,RteNasHood,RteETT,MedRouteIVCath,MedRouteIVButtFly,MedRouteIM,MedRoutePO,MedRouteNasal,MedRouteRectal,IVSite,IVGauge,IVSideR,IVSideL,IVAtt,IVF,IVFVol,MonBP,MonSpO2,MonEtCO2,MonTemp,MonPrecordial,MonEKG,Notes,PatWgt,WgtUnitsLbs,WgtUnitsKgs,PatHgt,EscortName,EscortCellNum,EscortRel,NPOTime,HgtUnitsIn,HgtUnitsCm,Signature,SigIsTopaz)" +
				"VALUES(" + POut.PInt(recordnum) + ",'" 
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
				+ POut.PInt(HgtUnitsCm) + ",'"
				+ POut.PString(Signature) + "',"
				+ POut.PBool(SigIsTopaz) + ")";
				int val =  General.NonQ(command);
				return val;
		}

		public static void UpdateVSMData(int anestheticRecordNum, string VSMName, string VSMSerNum){
			int recordnum = AnestheticRecords.GetRecordNum(anestheticRecordNum);
			string command = "UPDATE anestheticdata SET "
				+	"VSMName ='"	+ POut.PString(VSMName) + "' "
				+	",VSMSerNum ='"	+ POut.PString(VSMSerNum) + "'"
				+	"WHERE AnestheticRecordNum =" + anestheticRecordNum + "";	
				General.NonQ(command);
		}

		/// <summary>Updates changes to the selected Anesthetic Record's data in the database</summary>
		public static int UpdateAnesth_Data(int anestheticRecordNum, string anestheticOpen, string anestheticClose, string surgOpen, string surgClose, string anesthetist, string surgeon, string asst, string circulator, string VSMName, string VSMSerNum, string ASA, string ASA_EModifier, int O2LMin, int N2OLMin, int RteNasCan, int RteNasHood, int RteETT, int MedRouteIVCath, int MedRouteIVButtFly, int MedRouteIM, int MedRoutePO, int MedRouteNasal, int MedRouteRectal, string IVSite, int IVGauge, int IVSideR, int IVSideL, int IVAtt, string IVF, int IVFVol, int MonBP, int MonSpO2, int MonEtCO2, int MonTemp, int MonPrecordial, int MonEKG, string Notes, int PatWgt, int WgtUnitsLbs, int WgtUnitsKgs, int PatHgt, string EscortName, string EscortCellNum, string EscortRel, string NPOTime, int HgtUnitsIn, int HgtUnitsCm, string Signature, bool SigIsTopaz){

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
				+	",Signature			='"	+ POut.PString(Signature)+ "' "
				+	",SigIsTopaz		="	+ POut.PBool(SigIsTopaz)+ " "
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

		/// <summary>Inserts the newly added anesthetic medication and how supplied into the anesthmedsgiven table in the database</summary>
		public static void InsertAMedDose(string anesth_Medname, double qtyonhandold, double dose, double amtwasted, double qtyonhandnew, int anestheticRecordNum, int anesthmednum){
			string AMName = anesth_Medname;
			if (anesth_Medname.Contains("'"))
				{
					AMName = anesth_Medname.Replace("'", "''");
				}
			string doseTimeStamp = MiscData.GetNowDateTime().ToString("hh:mm:ss tt");
			//update anesthmedsgiven
			string command = "INSERT INTO anesthmedsgiven(AnestheticRecordNum,AnesthMedName,QtyGiven,QtyWasted,DoseTimeStamp,QtyOnHandOld,AnesthMedNum) VALUES('" + anestheticRecordNum + "','" + AMName + "','" + dose + "','" + amtwasted + "','" + doseTimeStamp + "','" + qtyonhandold + "','" + anesthmednum + "'" + ")";
			General.NonQ(command);
			string command2 = "UPDATE anesthmedsgiven SET "
					+ "QtyOnHandOld = " + GetQtyOnHand(AMName) + " "
					+ "WHERE AnesthMedName ='" + Convert.ToString(AMName) + "'" + "AND DoseTimeStamp= '" + Convert.ToString(doseTimeStamp) + "'";
			General.NonQ(command2);
			//update anesthmedsinventory
			double AdjQty = GetQtyOnHand(AMName) - dose;
			string command3 = "UPDATE anesthmedsinventory SET "
					+ " QtyOnHand		=	" + POut.PDouble(AdjQty) + " "
					+ "WHERE AnesthMedName ='" + Convert.ToString(AMName) + "'";
			General.NonQ(command3);
		}

		public static void UpdateAMedDose(string anesthMedName, double dose, double amtwasted, string dosetimestamp, int anestheticMedNum, int anestheticRecordNum){
			string AMName = anesthMedName;
			if (anesthMedName.Contains("'"))
			{
				AMName = anesthMedName.Replace("'", "''");
			}
			//update anesthmedsinventory
			if (Convert.ToDouble(GetQtyGiven(anesthMedName, dosetimestamp, anestheticMedNum)) != dose) //no adjustment made if textQtyGiven textbox is filled with same amt
			{
				//put old qty back in inventory first
					double newQty = GetQtyOnHand(anesthMedName) + GetQtyGiven(anesthMedName, dosetimestamp,anestheticMedNum);
					string command = "UPDATE anesthmedsinventory SET "
							+ " QtyOnHand		=	" + POut.PDouble(newQty) + " "
							+ "WHERE AnesthMedName ='" + Convert.ToString(AMName) + "'";
					General.NonQ(command);

				//the new qty to be deducted from inventory
					double AdjQty = Convert.ToDouble(GetQtyOnHand(anesthMedName)) - (dose) - (amtwasted);
					string command2 = "UPDATE anesthmedsinventory SET "
						+ " QtyOnHand		=	" + POut.PDouble(AdjQty) + " "
						+ "WHERE AnesthMedName ='" + Convert.ToString(AMName) + "'";
					General.NonQ(command2);
			}

			else if (Convert.ToDouble(GetQtyWasted(anesthMedName, dosetimestamp, anestheticMedNum)) != amtwasted) //no adjustment made if textQtyWasted textbox is filled with same amt

			{
				//put old wasted qty back into inventory first
					double newWaste = GetQtyOnHand(anesthMedName) + GetQtyWasted(anesthMedName, dosetimestamp, anestheticMedNum);
					string command = "UPDATE anesthmedsinventory SET "
						+ " QtyOnHand		=	" + POut.PDouble(newWaste) + " "
						+ "WHERE AnesthMedName ='" + Convert.ToString(AMName) + "'";
					General.NonQ(command);

				//now, deduct the new wasted qty
					double AdjQty = Convert.ToDouble(GetQtyOnHand(anesthMedName)) - (amtwasted);
					string command2 = "UPDATE anesthmedsinventory SET "
						+ " QtyOnHand		=	" + POut.PDouble(AdjQty) + " "
						+ "WHERE AnesthMedName ='" + Convert.ToString(AMName) + "'";
					General.NonQ(command2);
			}
		
			//update anesthmedsgiven
			string command3 = "UPDATE anesthmedsgiven SET "
				+ " AnesthMedName		='" + POut.PString(AMName) + "' "
				+ ",QtyGiven			=" + POut.PDouble((dose)) + " "
				+ ",QtyWasted			=" + POut.PDouble((amtwasted)) + " "
				+ ",DoseTimeStamp		='" + POut.PString(Convert.ToString(dosetimestamp)) + "'"
				+ "WHERE AnestheticMedNum = " + anestheticMedNum + " AND AnestheticRecordNum = " + anestheticRecordNum;
			General.NonQ(command3);
		}

		public static double GetQtyGiven(string anesthMedName, string doseTimeStamp, int anestheticMedNum){
			MySqlCommand command = new MySqlCommand();
			con = new MySqlConnection(DataSettings.ConnectionString);
			command.Connection = con;
			if (con.State == ConnectionState.Open)
				con.Close();
			con.Open();
			command.CommandText = "SELECT QtyGiven FROM anesthmedsgiven WHERE AnesthMedName ='" + anesthMedName + "'" + " AND AnestheticMedNum = " + anestheticMedNum ;
			command.Connection = con;
			string qtyGiven = Convert.ToString(command.ExecuteScalar());
			double doseGiven = 0;
			return doseGiven = Convert.ToDouble(qtyGiven);
		}

		public static double GetQtyWasted(string anesthMedName, string doseTimeStamp, int anestheticMedNum){
			MySqlCommand command = new MySqlCommand();
			con = new MySqlConnection(DataSettings.ConnectionString);
			command.Connection = con;
			if (con.State == ConnectionState.Open)
				con.Close();
			con.Open();
			command.CommandText = "SELECT QtyWasted FROM anesthmedsgiven WHERE AnesthMedName ='" + anesthMedName + "'" + " AND AnestheticMedNum = " + anestheticMedNum;
			command.Connection = con;
			string qtyWasted = Convert.ToString(command.ExecuteScalar());
			double doseWasted = 0;
			return doseWasted = Convert.ToDouble(qtyWasted);

		}

		///<summary> Updates the table anesthmedsinventory with the new quantity adjustment</summary>
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
		
		///<summary>Deletes anesthetic meds from the table anesthmedsgiven, and updates inventory accordingly </summary>
		public static void DeleteAMedDose(string anesthMedName, double  QtyGiven, double QtyWasted, string TimeStamp, int anestheticRecordNum){
			//Update anesthmedsinventory
			double AdjQty = Convert.ToDouble(GetQtyOnHand(anesthMedName)) + QtyGiven + QtyWasted;
			string command3 = "UPDATE anesthmedsinventory SET "
					+ " QtyOnHand		=	" + POut.PDouble(AdjQty) + " "
					+ "WHERE AnesthMedName ='" + Convert.ToString(anesthMedName) + "'";
			General.NonQ(command3);
			//Update anesthmedsgiven
			string command = "DELETE FROM anesthmedsgiven WHERE AnesthMedName='" + anesthMedName + "' and QtyGiven=" + QtyGiven + " and DoseTimeStamp='" + TimeStamp.ToString() + "'" + " and AnestheticRecordNum = " + anestheticRecordNum;
			General.NonQ(command);
		}

				///<summary>Deletes anesthetic meds from the table anesthmedsgiven, and updates inventory accordingly </summary>
		public static void DeleteAnesthMedsGiven(string anesthMedName, double  QtyGiven, double QtyWasted, string TimeStamp, int anestheticRecordNum){
			//Update anesthmedsinventory
			double AdjQty = Convert.ToDouble(GetQtyOnHand(anesthMedName)) + QtyGiven + QtyWasted;
			string command3 = "UPDATE anesthmedsinventory SET "
					+ " QtyOnHand		=	" + POut.PDouble(AdjQty) + " "
					+ "WHERE AnesthMedName ='" + Convert.ToString(anesthMedName) + "'";
			General.NonQ(command3);
			//Update anesthmedsgiven
			string command = "DELETE FROM anesthmedsgiven WHERE AnestheticRecordNum = " + anestheticRecordNum;
			General.NonQ(command);
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
			double qtyOnHand = Convert.ToDouble(QtyOnHand);
			con.Close();
			return qtyOnHand;
		}
		/// <summary>Returns AnesthMedNum. Used to check a unique med num in table anesthmedsgiven vs. those in anesthmedsinventory. 
		public static int GetAnesthMedNum(string aMed){
			MySqlCommand cmd = new MySqlCommand();
			con = new MySqlConnection(DataSettings.ConnectionString);
			cmd.Connection = con;
			if (con.State == ConnectionState.Open)
				con.Close();
			con.Open();
			cmd.CommandText = "SELECT AnestheticMedNum FROM anesthmedsinventory WHERE AnesthMedName='" + aMed + "'";
			string anesthmednum = Convert.ToString(cmd.ExecuteScalar());
			int anesthMedNum = Convert.ToInt32(anesthmednum);
			con.Close();
			return anesthMedNum;
		}

	}
	
}
