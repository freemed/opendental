using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace OpenDentBusiness{
	///<summary></summary>
	public class RxPats {
		
		///<summary></summary>
		public static RxPat GetRx(long rxNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<RxPat>(MethodBase.GetCurrentMethod(),rxNum);
			}
			return Crud.RxPatCrud.SelectOne(rxNum);
		}

		///<summary></summary>
		public static void Update(RxPat rx) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),rx);
				return;
			}
			Crud.RxPatCrud.Update(rx);
		}

		///<summary></summary>
		public static long Insert(RxPat rx) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				rx.RxNum=Meth.GetLong(MethodBase.GetCurrentMethod(),rx);
				return rx.RxNum;
			}
			return Crud.RxPatCrud.Insert(rx);
		}

		///<summary></summary>
		public static void Delete(long rxNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),rxNum);
				return;
			}
			string command= "DELETE FROM rxpat WHERE RxNum = '"+POut.Long(rxNum)+"'";
			Db.NonQ(command);
			DeletedObjects.SetDeleted(DeletedObjectType.RxPat,rxNum);
		}

		public static List<long> GetChangedSinceRxNums(DateTime changedSince) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<long>>(MethodBase.GetCurrentMethod(),changedSince);
			}
			string command="SELECT RxNum FROM rxpat WHERE DateTStamp > "+POut.DateT(changedSince);
			DataTable dt=Db.GetTable(command);
			List<long> rxnums = new List<long>(dt.Rows.Count);
			for(int i=0;i<dt.Rows.Count;i++) {
				rxnums.Add(PIn.Long(dt.Rows[i]["RxNum"].ToString()));
			}
			return rxnums;
		}

		///<summary>Used along with GetChangedSinceRxNums</summary>
		public static List<RxPat> GetMultRxPats(List<long> rxNums) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<RxPat>>(MethodBase.GetCurrentMethod(),rxNums);
			}
			string strRxNums="";
			DataTable table;
			if(rxNums.Count>0) {
				for(int i=0;i<rxNums.Count;i++) {
					if(i>0) {
						strRxNums+="OR ";
					}
					strRxNums+="RxNum='"+rxNums[i].ToString()+"' ";
				}
				string command="SELECT * FROM rxpat WHERE "+strRxNums;
				table=Db.GetTable(command);
			}
			else {
				table=new DataTable();
			}
			RxPat[] multRxs=Crud.RxPatCrud.TableToList(table).ToArray();
			List<RxPat> rxList=new List<RxPat>(multRxs);
			return rxList;
		}

		///<summary>Used in FormRxSend to fill electronic queue filtered by pharmacy.</summary>
		public static List<RxPat> GetMultElectQueueRx(long pharmacyNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<RxPat>>(MethodBase.GetCurrentMethod(),pharmacyNum);
			}
			string command="SELECT * FROM rxpat WHERE IsElectQueue=1 AND PharmacyNum="+pharmacyNum;
			return Crud.RxPatCrud.SelectMany(command);
		}

	}

	


}













