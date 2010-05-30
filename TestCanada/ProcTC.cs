using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenDentBusiness;

namespace TestCanada {
	public class ProcTC {
		///<summary>The tooth number passed in should be in international format.</summary>
		public static void SetExtracted(string toothNumInternat, DateTime procDate,long patNum) {
			Procedure proc=new Procedure();
			proc.CodeNum=ProcedureCodes.GetCodeNum("71101");
			proc.PatNum=patNum;
			proc.ProcDate=procDate;
			proc.ToothNum=Tooth.FromInternat(toothNumInternat);
			proc.ProcStatus=ProcStat.EO;
			Procedures.Insert(proc);
			ToothInitialTC.SetMissing(toothNumInternat,patNum);
		}

	}
}
