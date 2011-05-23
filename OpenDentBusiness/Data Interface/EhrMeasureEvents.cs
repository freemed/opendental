using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace OpenDentBusiness{
	///<summary></summary>
	public class EhrMeasureEvents{
		///<summary>Ordered by dateT</summary>
		public static List<EhrMeasureEvent> Refresh(long patNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<EhrMeasureEvent>>(MethodBase.GetCurrentMethod(),patNum);
			}
			string command="SELECT * FROM ehrmeasureevent WHERE PatNum = "+POut.Long(patNum)+" "
				+"ORDER BY DateTEvent";
			return Crud.EhrMeasureEventCrud.SelectMany(command);
		}

		///<summary>Ordered by dateT</summary>
		public static List<EhrMeasureEvent> RefreshByType(EhrMeasureEventType ehrMeasureEventType,long patNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<EhrMeasureEvent>>(MethodBase.GetCurrentMethod(),ehrMeasureEventType,patNum);
			}
			string command="SELECT * FROM ehrmeasureevent WHERE EventType = "+POut.Int((int)ehrMeasureEventType)+" "
				+"AND PatNum = "+POut.Long(patNum)+" "
				+"ORDER BY DateTEvent";
			return Crud.EhrMeasureEventCrud.SelectMany(command);
		}

		///<summary></summary>
		public static long Insert(EhrMeasureEvent ehrMeasureEvent){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				ehrMeasureEvent.EhrMeasureEventNum=Meth.GetLong(MethodBase.GetCurrentMethod(),ehrMeasureEvent);
				return ehrMeasureEvent.EhrMeasureEventNum;
			}
			return Crud.EhrMeasureEventCrud.Insert(ehrMeasureEvent);
		}

		///<summary></summary>
		public static void Delete(long ehrMeasureEventNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),ehrMeasureEventNum);
				return;
			}
			string command= "DELETE FROM ehrmeasureevent WHERE EhrMeasureEventNum = "+POut.Long(ehrMeasureEventNum);
			Db.NonQ(command);
		}

		///<summary></summary>
		public static List<EhrMeasureEvent> GetByType(List<EhrMeasureEvent> listMeasures,EhrMeasureEventType eventType) {
			//No need to check RemotingRole; no call to db.
			List<EhrMeasureEvent> retVal=new List<EhrMeasureEvent>();
			for(int i=0;i<listMeasures.Count;i++) {
				if(listMeasures[i].EventType==eventType) {
					retVal.Add(listMeasures[i]);
				}
			}
			return retVal;
		}

		/*
		Only pull out the methods below as you need them.  Otherwise, leave them commented out

		///<summary>Gets one EhrMeasureEvent from the db.</summary>
		public static EhrMeasureEvent GetOne(long ehrMeasureEventNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				return Meth.GetObject<EhrMeasureEvent>(MethodBase.GetCurrentMethod(),ehrMeasureEventNum);
			}
			return Crud.EhrMeasureEventCrud.SelectOne(ehrMeasureEventNum);
		}

		///<summary></summary>
		public static void Update(EhrMeasureEvent ehrMeasureEvent){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				Meth.GetVoid(MethodBase.GetCurrentMethod(),ehrMeasureEvent);
				return;
			}
			Crud.EhrMeasureEventCrud.Update(ehrMeasureEvent);
		}

		*/



	}
}