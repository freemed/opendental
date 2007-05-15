/*using System;
using System.Collections;
using System.Data;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{
	///<summary></summary>
	public class SchedDefaults {
		///<summary>All sched defaults</summary>
		public static SchedDefault[] List;

		///<summary>Gets all scheddefaults and stores them in a static array.</summary>
		public static void Refresh() {
			string command=
				"SELECT * from scheddefault "
				+"ORDER BY SchedType,"//this keeps the painting in the correct order
				+"StartTime";//this helps in the monthly display
			DataTable table=General.GetTable(command);
			List=new SchedDefault[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++) {
				List[i]=new SchedDefault();
				List[i].SchedDefaultNum= PIn.PInt(table.Rows[i][0].ToString());
				List[i].DayOfWeek      = PIn.PInt(table.Rows[i][1].ToString());
				List[i].StartTime      = PIn.PDateT(table.Rows[i][2].ToString());
				List[i].StopTime       = PIn.PDateT(table.Rows[i][3].ToString());
				List[i].SchedType      = (ScheduleType)PIn.PInt(table.Rows[i][4].ToString());
				List[i].ProvNum        = PIn.PInt(table.Rows[i][5].ToString());
				List[i].BlockoutType   = PIn.PInt(table.Rows[i][6].ToString());
				List[i].Op             = PIn.PInt(table.Rows[i][7].ToString());
			}
		}

		///<summary></summary>
		private static void Update(SchedDefault sd){
			string command= "UPDATE scheddefault SET " 
				+"DayOfWeek = '"    +POut.PInt   (sd.DayOfWeek)+"'"
				+",StartTime = "   +POut.PDateT (sd.StartTime)
				+",StopTime = "    +POut.PDateT (sd.StopTime)
				+",SchedType = '"   +POut.PInt   ((int)sd.SchedType)+"'"
				+",ProvNum = '"     +POut.PInt   (sd.ProvNum)+"'"
				+",BlockoutType = '"+POut.PInt   (sd.BlockoutType)+"'"
				+",Op = '"          +POut.PInt   (sd.Op)+"'"
				+" WHERE SchedDefaultNum = '" +POut.PInt (sd.SchedDefaultNum)+"'";
 			General.NonQ(command);
		}

		///<summary></summary>
		private static void Insert(SchedDefault sd){
			string command= "INSERT INTO scheddefault (DayOfWeek,StartTime,StopTime,SchedType,"
				+"ProvNum,BlockoutType,Op) VALUES("
				+"'"+POut.PInt   (sd.DayOfWeek)+"', "
				+POut.PDateT (sd.StartTime)+", "
				+POut.PDateT (sd.StopTime)+", "
				+"'"+POut.PInt   ((int)sd.SchedType)+"', "
				+"'"+POut.PInt   (sd.ProvNum)+"', "
				+"'"+POut.PInt   (sd.BlockoutType)+"', "
				+"'"+POut.PInt   (sd.Op)+"')";
 			sd.SchedDefaultNum=General.NonQ(command,true);
		}

		///<summary></summary>
		public static void InsertOrUpdate(SchedDefault sd, bool isNew){
			if(sd.StartTime.TimeOfDay > sd.StopTime.TimeOfDay){
				throw new Exception(Lan.g("SchedDefault","Stop time must be later than start time."));
			}
			if(sd.StopTime.TimeOfDay-sd.StartTime.TimeOfDay < new TimeSpan(0,10,0)){//< 5 min
				throw new Exception(Lan.g("SchedDefault","Block is too short."));
			}
			if(Overlaps(sd)){
				throw new Exception(Lan.g("SchedDefault","Cannot overlap another time block."));
			}
			if(isNew){
				Insert(sd);
			}
			else{
				Update(sd);
			}
		}

		///<summary></summary>
		private static bool Overlaps(SchedDefault sd){
			SchedDefaults.Refresh();
			SchedDefault[] ListForType=SchedDefaults.GetForType(sd.SchedType,sd.ProvNum);
			for(int i=0;i<ListForType.Length;i++){
				//if(SchedDefaults.List[i].SchedType!=SchedType){
				//	continue;//skip if different sched type
				//}
				//if(SchedDefaults.List[i].SchedType==ScheduleType.Provider
				//	&& SchedDefaults.List[i].ProvNum!=ProvNum)
				//{
				//	continue;//skip if provider type and this is different provider
				//}
				if(ListForType[i].SchedType==ScheduleType.Blockout){
					//skip if blockout, and ops don't interfere
					if(sd.Op!=0 && ListForType[i].Op!=0){//neither op can be zero, or they will interfere
						if(sd.Op != ListForType[i].Op){
							continue;
						}
					}
				}
				if(sd.SchedDefaultNum!=ListForType[i].SchedDefaultNum
					&& sd.DayOfWeek==ListForType[i].DayOfWeek
					&& sd.StartTime.TimeOfDay >= ListForType[i].StartTime.TimeOfDay
					&& sd.StartTime.TimeOfDay < ListForType[i].StopTime.TimeOfDay)
				{
					return true;
				}
				if(sd.SchedDefaultNum!=ListForType[i].SchedDefaultNum
					&& sd.DayOfWeek==ListForType[i].DayOfWeek
					&& sd.StopTime.TimeOfDay > ListForType[i].StartTime.TimeOfDay
					&& sd.StopTime.TimeOfDay <= ListForType[i].StopTime.TimeOfDay)
				{
					return true;
				}
				if(sd.SchedDefaultNum!=ListForType[i].SchedDefaultNum
					&& sd.DayOfWeek==ListForType[i].DayOfWeek
					&& sd.StartTime.TimeOfDay <= ListForType[i].StartTime.TimeOfDay
					&& sd.StopTime.TimeOfDay >= ListForType[i].StopTime.TimeOfDay)
				{
					return true;
				}
			}
			return false;
		}

		///<summary></summary>
		public static void Delete(SchedDefault sd){
			string command= "DELETE from scheddefault WHERE scheddefaultnum = '"
				+POut.PInt(sd.SchedDefaultNum)+"'";
 			General.NonQ(command);
		}


	

	

		///<summary>Returns an array of all schedDefaults for a single type (practice, prov, or blockout)</summary>
		public static SchedDefault[] GetForType(ScheduleType schedType,int provNum){
			ArrayList al=new ArrayList();
			for(int i=0;i<List.Length;i++){
				if(List[i].SchedType==schedType
					&& List[i].ProvNum==provNum)
				{
					al.Add(List[i]);
				}
			}
			SchedDefault[] retVal=new SchedDefault[al.Count];
			for(int i=0;i<retVal.Length;i++){
				retVal[i]=(SchedDefault)al[i];
			}
			return retVal;
		}

	
	}

	

	


}




*/








