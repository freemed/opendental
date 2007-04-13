using System;
using System.Collections;
using System.Data;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{
	///<summary></summary>
	public class Commlogs {

		///<summary>Gets all items for the current patient ordered by date.</summary>
		public static Commlog[] Refresh(int patNum) {
			string command=
				"SELECT * FROM commlog"
				+" WHERE PatNum = '"+patNum+"'"
				+" ORDER BY CommDateTime";
			return RefreshAndFill(command);
		}

		///<summary>Gets one commlog item from database.</summary>
		public static Commlog GetOne(int commlogNum) {
			string command=
				"SELECT * FROM commlog"
				+" WHERE CommlogNum = "+POut.PInt(commlogNum);
			Commlog[] commList=RefreshAndFill(command);
			if(commList.Length==0) {
				return null;
			}
			return commList[0];
		}

		private static Commlog[] RefreshAndFill(string command) {
			DataTable table=General.GetTable(command);
			Commlog[] List=new Commlog[table.Rows.Count];
			for(int i=0;i<List.Length;i++) {
				List[i]=new Commlog();
				List[i].CommlogNum     = PIn.PInt(table.Rows[i][0].ToString());
				List[i].PatNum         = PIn.PInt(table.Rows[i][1].ToString());
				List[i].CommDateTime   = PIn.PDate(table.Rows[i][2].ToString());
				List[i].CommType       = (CommItemType)PIn.PInt(table.Rows[i][3].ToString());
				List[i].Note           = PIn.PString(table.Rows[i][4].ToString());
				List[i].Mode_          = (CommItemMode)PIn.PInt(table.Rows[i][5].ToString());
				List[i].SentOrReceived = (CommSentOrReceived)PIn.PInt(table.Rows[i][6].ToString());
			}
			return List;
		}

		///<summary></summary>
		public static void Insert(Commlog comm){
			if(PrefB.RandomKeys) {
				comm.CommlogNum=MiscData.GetKey("commlog","CommlogNum");
			}
			string command="INSERT INTO commlog (";
			if(PrefB.RandomKeys) {
				command+="CommlogNum,";
			}
			command+="PatNum,CommDateTime,CommType,Note,Mode_,SentOrReceived) VALUES(";
			if(PrefB.RandomKeys) {
				command+="'"+POut.PInt(comm.CommlogNum)+"', ";
			}
			command+=
				 "'"+POut.PInt   (comm.PatNum)+"', "
				+POut.PDateT (comm.CommDateTime)+", "
				+"'"+POut.PInt   ((int)comm.CommType)+"', "
				+"'"+POut.PString(comm.Note)+"', "
				+"'"+POut.PInt   ((int)comm.Mode_)+"', "
				+"'"+POut.PInt   ((int)comm.SentOrReceived)+"')";
			if(PrefB.RandomKeys) {
				General.NonQ(command);
			}
			else {
				comm.CommlogNum=General.NonQ(command,true);
			}
		}

		///<summary></summary>
		public static void Update(Commlog comm) {
			string command= "UPDATE commlog SET "
				+"PatNum = '"         +POut.PInt   (comm.PatNum)+"', "
				+"CommDateTime= "    +POut.PDateT (comm.CommDateTime)+", "
				+"CommType = '"       +POut.PInt   ((int)comm.CommType)+"', "
				+"Note = '"           +POut.PString(comm.Note)+"', "
				+"Mode_ = '"          +POut.PInt   ((int)comm.Mode_)+"', "
				+"SentOrReceived = '" +POut.PInt   ((int)comm.SentOrReceived)+"' "
				+"WHERE commlognum = '"+POut.PInt(comm.CommlogNum)+"'";
			General.NonQ(command);
		}

		///<summary></summary>
		public static void Delete(Commlog comm) {
			string command= "DELETE FROM commlog WHERE CommLogNum = '"+POut.PInt(comm.CommlogNum)+"'";
			General.NonQ(command);
		}
	
		///<summary></summary>
		public static int UndoStatements(DateTime date){
			string command="DELETE FROM commlog WHERE CommDateTime LIKE '"+POut.PDate(date,false)+"%' "
				+"AND CommType=1";
			int rowsAffected=General.NonQ(command);
			return rowsAffected;
		}

		///<summary>Used when printing recall cards to make a commlog entry for everyone at once.</summary>
		public static void InsertForRecallPostcard(int patNum){
			string command="SELECT COUNT(*) FROM  commlog WHERE ";
			if(FormChooseDatabase.DBtype==DatabaseType.Oracle){
				command+="TO_DATE(CommDateTime) = "+POut.PDate(MiscData.GetNowDateTime());
			}else{//Assume MySQL
				command+="DATE(CommDateTime) = CURDATE()";
			}
			command+=" AND PatNum="+POut.PInt(patNum)+" AND CommType=5 AND Mode_=2 AND SentOrReceived=1";
			if(General.GetCount(command)!="0"){
				return;
			}
			Commlog com=new Commlog();
			com.PatNum=patNum;
			com.CommDateTime=DateTime.Now;
			com.CommType=CommItemType.Recall;
			com.Mode_=CommItemMode.Mail;
			com.SentOrReceived=CommSentOrReceived.Sent;
			com.Note=Lan.g("FormRecallList","Sent recall postcard");
			Insert(com);
		}

	}

	




}

















