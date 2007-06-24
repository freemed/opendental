using System;
using System.Collections;
using System.Data;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{
	///<summary></summary>
	public class JournalEntries {

		///<summary>Used when displaying the splits for a transaction.</summary>
		public static ArrayList GetForTrans(int transactionNum) {
			string command=
				"SELECT * FROM journalentry "
				+"WHERE TransactionNum="+POut.PInt(transactionNum);
			JournalEntry[] List=RefreshAndFill(command);
			ArrayList retVal=new ArrayList();
			for(int i=0;i<List.Length;i++) {
				retVal.Add(List[i]);
			}
			return retVal;
		}

		///<summary>Used to display a list of entries for one account.</summary>
		public static JournalEntry[] GetForAccount(int accountNum) {
			string command=
				"SELECT * FROM journalentry "
				+"WHERE AccountNum="+POut.PInt(accountNum)
				+" ORDER BY DateDisplayed";
			return RefreshAndFill(command);
		}

		///<summary>Used in reconcile window.</summary>
		public static JournalEntry[] GetForReconcile(int accountNum,bool includeUncleared,int reconcileNum) {
			string command=
				"SELECT * FROM journalentry "
				+"WHERE AccountNum="+POut.PInt(accountNum)
				+" AND (ReconcileNum="+POut.PInt(reconcileNum);
			if(includeUncleared) {
				command+=" OR ReconcileNum=0)";
			}
			else {
				command+=")";
			}
			command+=" ORDER BY DateDisplayed";
			return RefreshAndFill(command);
		}

		private static JournalEntry[] RefreshAndFill(string command) {
			DataTable table=General.GetTable(command);
			JournalEntry[] List=new JournalEntry[table.Rows.Count];
			for(int i=0;i<List.Length;i++) {
				List[i]=new JournalEntry();
				List[i].JournalEntryNum= PIn.PInt(table.Rows[i][0].ToString());
				List[i].TransactionNum = PIn.PInt(table.Rows[i][1].ToString());
				List[i].AccountNum     = PIn.PInt(table.Rows[i][2].ToString());
				List[i].DateDisplayed  = PIn.PDate(table.Rows[i][3].ToString());
				List[i].DebitAmt       = PIn.PDouble(table.Rows[i][4].ToString());
				List[i].CreditAmt      = PIn.PDouble(table.Rows[i][5].ToString());
				List[i].Memo           = PIn.PString(table.Rows[i][6].ToString());
				List[i].Splits         = PIn.PString(table.Rows[i][7].ToString());
				List[i].CheckNumber    = PIn.PString(table.Rows[i][8].ToString());
				List[i].ReconcileNum   = PIn.PInt(table.Rows[i][9].ToString());
			}
			return List;
		}

		///<summary></summary>
		public static void Insert(JournalEntry je) {
			if(je.DebitAmt<0 || je.CreditAmt<0){
				throw new ApplicationException(Lan.g("JournalEntries","Error. Credit and debit must both be positive."));
			}
			if(PrefB.RandomKeys) {
				je.JournalEntryNum=MiscData.GetKey("journalentry","JournalEntryNum");
			}
			string command="INSERT INTO journalentry (";
			if(PrefB.RandomKeys) {
				command+="JournalEntryNum,";
			}
			command+="TransactionNum,AccountNum,DateDisplayed,DebitAmt,CreditAmt,Memo,Splits,CheckNumber,"
				+"ReconcileNum) VALUES(";
			if(PrefB.RandomKeys) {
				command+="'"+POut.PInt(je.JournalEntryNum)+"', ";
			}
			command+=
				 "'"+POut.PInt   (je.TransactionNum)+"', "
				+"'"+POut.PInt   (je.AccountNum)+"', "
				    +POut.PDate  (je.DateDisplayed)+", "
				+"'"+POut.PDouble(je.DebitAmt)+"', "
				+"'"+POut.PDouble(je.CreditAmt)+"', "
				+"'"+POut.PString(je.Memo)+"', "
				+"'"+POut.PString(je.Splits)+"', "
				+"'"+POut.PString(je.CheckNumber)+"', "
				+"'"+POut.PInt   (je.ReconcileNum)+"')";
			if(PrefB.RandomKeys) {
				General.NonQ(command);
			}
			else {
				je.JournalEntryNum=General.NonQ(command,true);
			}
		}

		///<summary></summary>
		public static void Update(JournalEntry je) {
			if(je.DebitAmt<0 || je.CreditAmt<0) {
				throw new ApplicationException(Lan.g("JournalEntries","Error. Credit and debit must both be positive."));
			}
			string command= "UPDATE journalentry SET "
				+"TransactionNum = '"+POut.PInt   (je.TransactionNum)+"' "
				+",AccountNum = '"   +POut.PInt   (je.AccountNum)+"' "
				+",DateDisplayed = "+POut.PDate  (je.DateDisplayed)+" "
				+",DebitAmt = '"     +POut.PDouble(je.DebitAmt)+"' "
				+",CreditAmt = '"    +POut.PDouble(je.CreditAmt)+"' "
				+",Memo = '"         +POut.PString(je.Memo)+"' "
				+",Splits = '"       +POut.PString(je.Splits)+"' "
				+",CheckNumber = '"  +POut.PString(je.CheckNumber)+"' "
				+",ReconcileNum = '" +POut.PInt   (je.ReconcileNum)+"' "
				+"WHERE JournalEntryNum = '"+POut.PInt(je.JournalEntryNum)+"'";
			General.NonQ(command);
		}

		///<summary></summary>
		public static void Delete(JournalEntry je) {
			string command= "DELETE FROM journalentry WHERE JournalEntryNum = "+POut.PInt(je.JournalEntryNum);
			General.NonQ(command);
		}

		///<summary>Used in FormTransactionEdit to synch database with changes user made to the journalEntry list for a transaction.  Must supply an old list for comparison.  Only the differences are saved.  Surround with try/catch, because it will thrown an exception if any entries are negative.</summary>
		public static void UpdateList(ArrayList oldJournalList,ArrayList newJournalList) {
			for(int i=0;i<newJournalList.Count;i++){
				if(((JournalEntry)newJournalList[i]).DebitAmt<0 || ((JournalEntry)newJournalList[i]).CreditAmt<0){
					throw new ApplicationException(Lan.g("JournalEntries","Error. Credit and debit must both be positive."));
				}
			}
			JournalEntry newJournalEntry;
			for(int i=0;i<oldJournalList.Count;i++) {//loop through the old list
				newJournalEntry=null;
				for(int j=0;j<newJournalList.Count;j++) {
					if(newJournalList[j]==null || ((JournalEntry)newJournalList[j]).JournalEntryNum==0) {
						continue;
					}
					if(((JournalEntry)oldJournalList[i]).JournalEntryNum==((JournalEntry)newJournalList[j]).JournalEntryNum) {
						newJournalEntry=(JournalEntry)newJournalList[j];
						break;
					}
				}
				if(newJournalEntry==null) {
					//journalentry with matching journalEntryNum was not found, so it must have been deleted
					Delete((JournalEntry)oldJournalList[i]);
					continue;
				}
				//journalentry was found with matching journalEntryNum, so check for changes
				if(newJournalEntry.AccountNum != ((JournalEntry)oldJournalList[i]).AccountNum
					|| newJournalEntry.DateDisplayed != ((JournalEntry)oldJournalList[i]).DateDisplayed
					|| newJournalEntry.DebitAmt != ((JournalEntry)oldJournalList[i]).DebitAmt
					|| newJournalEntry.CreditAmt != ((JournalEntry)oldJournalList[i]).CreditAmt
					|| newJournalEntry.Memo != ((JournalEntry)oldJournalList[i]).Memo
					|| newJournalEntry.Splits != ((JournalEntry)oldJournalList[i]).Splits
					|| newJournalEntry.CheckNumber!= ((JournalEntry)oldJournalList[i]).CheckNumber) 
				{
					Update(newJournalEntry);
				}
			}
			for(int i=0;i<newJournalList.Count;i++) {//loop through the new list
				if(newJournalList[i]==null) {
					continue;
				}
				if(((JournalEntry)newJournalList[i]).JournalEntryNum!=0) {
					continue;
				}
				//entry with journalEntryNum=0, so it's new
				Insert((JournalEntry)newJournalList[i]);
			}
		}

		///<summary>Called from FormTransactionEdit.</summary>
		public static bool AttachedToReconcile(ArrayList journalList){
			for(int i=0;i<journalList.Count;i++){
				if(((JournalEntry)journalList[i]).ReconcileNum!=0){
					return true;
				}
			}
			return false;
		}

		///<summary>Called from FormTransactionEdit.</summary>
		public static DateTime GetReconcileDate(ArrayList journalList) {
			for(int i=0;i<journalList.Count;i++) {
				if(((JournalEntry)journalList[i]).ReconcileNum!=0) {
					return Reconciles.GetOne(((JournalEntry)journalList[i]).ReconcileNum).DateReconcile;
				}
			}
			return DateTime.MinValue;
		}

		///<summary>Called once from FormReconcileEdit when closing.  Saves the reconcileNum for every item in the list.</summary>
		public static void SaveList(JournalEntry[] journalList,int reconcileNum) {
			string command="UPDATE journalentry SET ReconcileNum=0 WHERE";
			string str="";
			for(int i=0;i<journalList.Length;i++){
				if(journalList[i].ReconcileNum==0){
					if(str!=""){
						str+=" OR";
					}
					str+=" JournalEntryNum="+POut.PInt(journalList[i].JournalEntryNum);
				}
			}
			if(str!=""){
				command+=str;
				General.NonQ(command);
			}
			command="UPDATE journalentry SET ReconcileNum="+POut.PInt(reconcileNum)+" WHERE";
			str="";
			for(int i=0;i<journalList.Length;i++) {
				if(journalList[i].ReconcileNum==reconcileNum) {
					if(str!="") {
						str+=" OR";
					}
					str+=" JournalEntryNum="+POut.PInt(journalList[i].JournalEntryNum);
				}
			}
			if(str!=""){
				command+=str;
				General.NonQ(command);
			}
		}

		/*//<summary>Attempts to delete all journal entries for one transaction.  Will later throw an error if attached to any reconciles.</summary>
		public static void DeleteForTrans(int transactionNum){
			string command="DELETE FROM journalentry WHERE TransactionNum="+POut.PInt(transactionNum);
			DataConnection dcon=new DataConnection();
			General.NonQ(command);
		}*/




	}

	
}




