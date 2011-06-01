using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace OpenDentBusiness{
	///<summary></summary>
	public class MedicalOrders{
		///<summary></summary>
		public static DataTable GetOrderTable(long patNum,bool includeDiscontinued){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetTable(MethodBase.GetCurrentMethod(),patNum,includeDiscontinued);
			}
			DataTable table=new DataTable("orders");
			DataRow row;
			table.Columns.Add("date");
			table.Columns.Add("type");
			table.Columns.Add("description");
			table.Columns.Add("status");
			List<DataRow> rows=new List<DataRow>();
			string command="SELECT DateTimeOrder,IsDiscontinued,MedOrderType FROM medicalorder WHERE PatNum = "+POut.Long(patNum);
			if(!includeDiscontinued) {//only include current orders
				command+=" AND IsDiscontinued=0";//false
			}
			DataTable rawOrder=Db.GetTable(command);
			DateTime dateT;
			MedicalOrderType medOrderType;
			bool isDiscontinued;
			for(int i=0;i<rawOrder.Rows.Count;i++) {
				row=table.NewRow();
				dateT=PIn.DateT(rawOrder.Rows[i]["DateTimeOrder"].ToString());
				row["date"]=dateT.ToShortDateString();
				medOrderType=(MedicalOrderType)PIn.Int(rawOrder.Rows[i]["MedOrderType"].ToString());
				row["type"]=medOrderType.ToString();
				row["description"]=PIn.String(rawOrder.Rows[i]["Description"].ToString());
				isDiscontinued=PIn.Bool(rawOrder.Rows[i]["IsDiscontinued"].ToString());
				if(isDiscontinued) {
					row["status"]="Discontinued";
				}
				else {
					row["status"]="Active";
				}
				rows.Add(row);
			}
			//Medications
			command="SELECT DateStart,DateStop,MedName,PatNote "
				+"FROM medicationpat "
				+"LEFT JOIN medication ON medication.MedicationNum=medicationpat.MedicationNum "
				+"WHERE PatNum = "+POut.Long(patNum);
			if(!includeDiscontinued) {//exclude invalid orders
				command+=" AND DateStart > "+POut.Date(new DateTime(1880,1,1))+" AND PatNote !=''";
			}
			DataTable rawMed=Db.GetTable(command);
			DateTime dateStop;
			for(int i=0;i<rawMed.Rows.Count;i++) {
				row=table.NewRow();
				dateT=PIn.DateT(rawMed.Rows[i]["DateStart"].ToString());
				if(dateT.Year<1880) {
					row["date"]="";
				}
				else {
					row["date"]=dateT.ToShortDateString();
				}
				row["type"]="Medication";
				row["description"]=PIn.String(rawMed.Rows[i]["MedName"].ToString())+", "
					+PIn.String(rawMed.Rows[i]["PatNote"].ToString());
				dateStop=PIn.DateT(rawMed.Rows[i]["DateStop"].ToString());
				if(dateStop.Year<1880) {//not stopped
					row["status"]="Active";
				}
				else {
					row["status"]="Discontinued";
				}
				rows.Add(row);
			}
			//Sorting-----------------------------------------------------------------------------------------
			rows.Sort(new MedicalOrderLineComparer());
			for(int i=0;i<rows.Count;i++) {
				table.Rows.Add(rows[i]);
			}
			return table;
		}

		/*
		///<summary></summary>
		public static int GetCountMedical(long patNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				return Meth.GetInt(MethodBase.GetCurrentMethod(),patNum);
			}
			string command="SELECT COUNT(*) FROM medicalorder WHERE MedOrderType="+POut.Int((int)MedicalOrderType.Medication)+" "
				+"AND PatNUm="+POut.Long(patNum);
			return PIn.Int(Db.GetCount(command));
		}*/

		///<summary></summary>
		public static List<MedicalOrder> GetPendingLabs() {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<MedicalOrder>>(MethodBase.GetCurrentMethod());
			}
			string command="SELECT * FROM medicalorder WHERE MedOrderType="+POut.Int((int)MedicalOrderType.Laboratory)+" "
				+"AND IsLabPending = 1";
			//NOT EXISTS(SELECT * FROM labpanel WHERE labpanel.MedicalOrderNum=medicalorder.MedicalOrderNum)";
			return Crud.MedicalOrderCrud.SelectMany(command);
		}

		///<summary>Gets one MedicalOrder from the db.</summary>
		public static MedicalOrder GetOne(long medicalOrderNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				return Meth.GetObject<MedicalOrder>(MethodBase.GetCurrentMethod(),medicalOrderNum);
			}
			return Crud.MedicalOrderCrud.SelectOne(medicalOrderNum);
		}

		///<summary></summary>
		public static long Insert(MedicalOrder medicalOrder){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				medicalOrder.MedicalOrderNum=Meth.GetLong(MethodBase.GetCurrentMethod(),medicalOrder);
				return medicalOrder.MedicalOrderNum;
			}
			return Crud.MedicalOrderCrud.Insert(medicalOrder);
		}

		///<summary></summary>
		public static void Update(MedicalOrder medicalOrder){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				Meth.GetVoid(MethodBase.GetCurrentMethod(),medicalOrder);
				return;
			}
			Crud.MedicalOrderCrud.Update(medicalOrder);
		}

		///<summary></summary>
		public static void Delete(long medicalOrderNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),medicalOrderNum);
				return;
			}
			string command= "DELETE FROM medicalorder WHERE MedicalOrderNum = "+POut.Long(medicalOrderNum);
			Db.NonQ(command);
		}

		



	}

	///<summary>The supplied DataRows must include the following columns: date</summary>
	class MedicalOrderLineComparer:IComparer<DataRow> {
		///<summary></summary>
		public int Compare(DataRow x,DataRow y) {
			return (((DateTime)x["date"]).Date).CompareTo(((DateTime)y["date"]).Date);
		}
	}
}