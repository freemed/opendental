using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace OpenDentBusiness{
	///<summary></summary>
	public class ScheduleOps {
		///<summary></summary>
		public static void Insert(ScheduleOp op){
			if(PrefC.RandomKeys){
				op.ScheduleOpNum=MiscData.GetKey("scheduleop","ScheduleOpNum");
			}
			string command= "INSERT INTO scheduleop (";
			if(PrefC.RandomKeys){
				command+="ScheduleOpNum,";
			}
			command+="ScheduleNum,OperatoryNum) VALUES(";
			if(PrefC.RandomKeys){
				command+="'"+POut.PInt(op.ScheduleOpNum)+"', ";
			}
			command+=
				 "'"+POut.PInt   (op.ScheduleNum)+"', "
				+"'"+POut.PInt   (op.OperatoryNum)+"')";
			if(PrefC.RandomKeys) {
				Db.NonQ(command);
			}
			else {
				op.ScheduleOpNum=Db.NonQ(command,true);
			}
		}

		
	}

	

	

}













