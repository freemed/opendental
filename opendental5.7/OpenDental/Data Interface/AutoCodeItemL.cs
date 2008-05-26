using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using OpenDentBusiness;

namespace OpenDental{
	public class AutoCodeItemL {
		public static void Refresh(){
			DataTable table=Gen.GetTable(MethodNameTable.AutoCodeItem_RefreshCache);
			AutoCodeItems.FillCache(table);//now, we have an arrays on both the client and the server.
		}

		///<summary>Only called from ContrChart.listProcButtons_Click.  Called once for each tooth selected and for each autocode item attached to the button.</summary>
		public static int GetCodeNum(int autoCodeNum,string toothNum,string surf,bool isAdditional,int patNum,int age){
			bool allCondsMet;
			AutoCodeItems.GetListForCode(autoCodeNum);
			if(AutoCodeItems.ListForCode.Length==0){
				return 0;
			}
			bool willBeMissing=Procedures.WillBeMissing(toothNum,patNum);
			for(int i=0;i<AutoCodeItems.ListForCode.Length;i++){
				AutoCodeConds.GetListForItem(AutoCodeItems.ListForCode[i].AutoCodeItemNum);
				allCondsMet=true;
				for(int j=0;j<AutoCodeConds.ListForItem.Length;j++){
					if(!AutoCodeConds.ConditionIsMet
						(AutoCodeConds.ListForItem[j].Cond,toothNum,surf,isAdditional,willBeMissing,age)){
						allCondsMet=false;
					}
				}
				if(allCondsMet){
					return AutoCodeItems.ListForCode[i].CodeNum;
				}
			}
			return AutoCodeItems.ListForCode[0].CodeNum;//if couldn't find a better match
		}

		///<summary>Only called when closing the procedure edit window. Usually returns the supplied CodeNum, unless a better match is found.</summary>
		public static int VerifyCode(int codeNum,string toothNum,string surf,bool isAdditional,int patNum,int age,
			out AutoCode AutoCodeCur)
		{
			bool allCondsMet;
			AutoCodeCur=null;
			if(!AutoCodeItemC.HList.ContainsKey(codeNum)){
				return codeNum;
			}
			if(!AutoCodeC.HList.ContainsKey((int)AutoCodeItemC.HList[codeNum])){
				return codeNum;//just in case.
			}
			AutoCodeCur=(AutoCode)AutoCodeC.HList[(int)AutoCodeItemC.HList[codeNum]];
			if(AutoCodeCur.LessIntrusive){
				return codeNum;
			}
			bool willBeMissing=Procedures.WillBeMissing(toothNum,patNum);
			AutoCodeItems.GetListForCode((int)AutoCodeItemC.HList[codeNum]);
			for(int i=0;i<AutoCodeItems.ListForCode.Length;i++){
				AutoCodeConds.GetListForItem(AutoCodeItems.ListForCode[i].AutoCodeItemNum);
				allCondsMet=true;
				for(int j=0;j<AutoCodeConds.ListForItem.Length;j++){
					if(!AutoCodeConds.ConditionIsMet
						(AutoCodeConds.ListForItem[j].Cond,toothNum,surf,isAdditional,willBeMissing,age)){
						allCondsMet=false;
					}
				}
				if(allCondsMet){
					return AutoCodeItems.ListForCode[i].CodeNum;
				}
			}
			return codeNum;//if couldn't find a better match
		}
	}
}
