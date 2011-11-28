using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace OpenDentBusiness.Mobile {
	///<summary></summary>
	public class Recallms {
		/*Dennis: This pseudocode will be refactored/revisited*/ //dennis recall
		/*		
		#region Only used for webserver for mobile.
		///<summary>Gets all Recallm for a single patient </summary>
		public static List<Recallm> GetRecallms(long customerNum,long patNum) {
			string command=
					"SELECT * from recallm "
					+"WHERE CustomerNum = "+POut.Long(customerNum)
					+" AND PatNum = "+POut.Long(patNum);
			return Crud.RecallmCrud.SelectMany(command);
		}
	
		#endregion

		#region Used only on OD
		///<summary>The values returned are sent to the webserver.</summary>
		public static List<long> GetChangedSinceRecallNums(DateTime changedSince) {
			return Recalls.GetChangedSinceRecallNums(changedSince);
		}

		///<summary>The values returned are sent to the webserver.</summary>
		public static List<Recallm> GetMultRecallms(List<long> recallNums) {
			List<Recall> recallList=Recalls.GetMultRecalls(recallNums);
			List<Recallm> recallmList=ConvertListToM(recallList);
			return recallmList;
		}

		///<summary>First use GetChangedSince.  Then, use this to convert the list a list of 'm' objects.</summary>
		public static List<Recallm> ConvertListToM(List<Recall> list) {
			List<Recallm> retVal=new List<Recallm>();
			for(int i=0;i<list.Count;i++) {
				retVal.Add(Crud.RecallmCrud.ConvertToM(list[i]));
			}
			return retVal;
		}
		#endregion

		#region Used only on the Mobile webservice server for  synching.
		///<summary>Only run on server for mobile.  Takes the list of changes from the dental office and makes updates to those items in the mobile server db.  Also, make sure to run DeletedObjects.DeleteForMobile().</summary>
		public static void UpdateFromChangeList(List<Recallm> list,long customerNum) {
			for(int i=0;i<list.Count;i++) {
				list[i].CustomerNum=customerNum;
				Recallm recallm=Crud.RecallmCrud.SelectOne(customerNum,list[i].RecallNum);
				if(recallm==null) {//not in db
					Crud.RecallmCrud.Insert(list[i],true);
				}
				else {
					Crud.RecallmCrud.Update(list[i]);
				}
			}
		}

		///<summary>used in tandem with Full synch</summary>
		public static void DeleteAll(long customerNum) {
			string command= "DELETE FROM recallm WHERE CustomerNum = "+POut.Long(customerNum); ;
			Db.NonQ(command);
		}

		///<summary>Delete all recalls of a particular patient</summary>
		public static void Delete(long customerNum,long PatNum) {
			string command= "DELETE FROM recallm WHERE CustomerNum = "+POut.Long(customerNum)+" AND PatNum = "+POut.Long(PatNum);
			Db.NonQ(command);
		}
		#endregion
	*/


		/* move this to Recalls.cs
		 
		 * 		public static List<long> GetChangedSinceRecallNums(DateTime changedSince) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<long>>(MethodBase.GetCurrentMethod(),changedSince);
			}
			string command="SELECT RecallNum FROM recall WHERE DateTStamp > "+POut.DateT(changedSince);
			DataTable dt=Db.GetTable(command);
			List<long> recallnums = new List<long>(dt.Rows.Count);
			for(int i=0;i<dt.Rows.Count;i++) {
				recallnums.Add(PIn.Long(dt.Rows[i]["RecallNum"].ToString()));
			}
			return recallnums;
		}

		///<summary>Used along with GetChangedSinceRecallNums</summary>
		public static List<Recall> GetMultRecalls(List<long> recallNums) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<Recall>>(MethodBase.GetCurrentMethod(),recallNums);
			}
			string strRecallNums="";
			DataTable table;
			if(recallNums.Count>0) {
				for(int i=0;i<recallNums.Count;i++) {
					if(i>0) {
						strRecallNums+="OR ";
					}
					strRecallNums+="RecallNum='"+recallNums[i].ToString()+"' ";
				}
				string command="SELECT * FROM recall WHERE "+strRecallNums;
				table=Db.GetTable(command);
			}
			else {
				table=new DataTable();
			}
			Recall[] multRecalls=Crud.RecallCrud.TableToList(table).ToArray();
			List<Recall> recallList=new List<Recall>(multRecalls);
			return recallList;
		}

		///<summary>Changes the value of the DateTStamp column to the current time stamp for all recalls of a patient</summary>
		public static void ResetTimeStamps(long patNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),patNum);
				return;
			}
			string command="UPDATE recall SET DateTStamp = CURRENT_TIMESTAMP WHERE PatNum ="+POut.Long(patNum);
			Db.NonQ(command);
		}
		 
		 
		 */


	}
}