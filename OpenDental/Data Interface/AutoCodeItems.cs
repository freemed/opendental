using System;
using System.Collections;
using System.Data;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{
	///<summary></summary>
	public class AutoCodeItems{
		//<summary></summary>
		//public static AutoCodeItem Cur;
		///<summary></summary>
		public static AutoCodeItem[] List;//all
		///<summary></summary>
		public static AutoCodeItem[] ListForCode;//all items for a specific AutoCode
		///<summary>key=ADACode,value=AutoCodeNum</summary>
		public static Hashtable HList;

		///<summary></summary>
		public static void Refresh(){
			string command="SELECT * from autocodeitem";
			DataTable table=General.GetTable(command);
			HList=new Hashtable();
			List=new AutoCodeItem[table.Rows.Count];
			for(int i=0;i<List.Length;i++){
				List[i]=new AutoCodeItem();
				List[i].AutoCodeItemNum= PIn.PInt   (table.Rows[i][0].ToString());
				List[i].AutoCodeNum    = PIn.PInt   (table.Rows[i][1].ToString());
				List[i].ADACode        = PIn.PString(table.Rows[i][2].ToString());
				if(!HList.ContainsKey(List[i].ADACode)){
					HList.Add(List[i].ADACode,List[i].AutoCodeNum);
				}
			}
		}

		///<summary></summary>
		public static void Insert(AutoCodeItem Cur){
			string command= "INSERT INTO autocodeitem (autocodenum,adacode) "
				+"VALUES ("
				+"'"+POut.PInt   (Cur.AutoCodeNum)+"', "
				+"'"+POut.PString(Cur.ADACode)+"')";
			//MessageBox.Show(string command);
			Cur.AutoCodeItemNum=General.NonQ(command,true);
		}

		///<summary></summary>
		public static void Update(AutoCodeItem Cur){
			string command= "UPDATE autocodeitem SET "
				+"autocodenum='"+POut.PInt   (Cur.AutoCodeNum)+"'"
				+",adacode ='"  +POut.PString(Cur.ADACode)+"'"
				+" WHERE autocodeitemnum = '"+POut.PInt(Cur.AutoCodeItemNum)+"'";
			General.NonQ(command);
		}

		///<summary></summary>
		public static void Delete(AutoCodeItem Cur){
			string command= "DELETE from autocodeitem WHERE autocodeitemnum = '"
				+POut.PInt(Cur.AutoCodeItemNum)+"'";
			General.NonQ(command);
		}

		///<summary></summary>
		public static void Delete(int autoCodeNum){
			string command= "DELETE from autocodeitem WHERE AutoCodeNum = '"
				+POut.PInt(autoCodeNum)+"'";
			General.NonQ(command);
		}

		///<summary></summary>
		public static void GetListForCode(int autoCodeNum){
			//loop through AutoCodeItems.List to fill ListForCode
			ArrayList ALtemp=new ArrayList();
			for(int i=0;i<List.Length;i++){
				if(List[i].AutoCodeNum==autoCodeNum){
					ALtemp.Add(List[i]);
				} 
			}
			ListForCode=new AutoCodeItem[ALtemp.Count];
			if(ALtemp.Count>0){
				ALtemp.CopyTo(ListForCode);
			}     
		}

		///<summary>Only called from ContrChart.listProcButtons_Click.  Called once for each tooth selected and for each autocode item attached to the button.</summary>
		public static string GetADA(int autoCodeNum,string toothNum,string surf,bool isAdditional,int patNum,int age){
			bool allCondsMet;
			GetListForCode(autoCodeNum);
			if(ListForCode.Length==0){
				return "";
			}
			bool willBeMissing=Procedures.WillBeMissing(toothNum,patNum);
			for(int i=0;i<ListForCode.Length;i++){
				AutoCodeConds.GetListForItem(ListForCode[i].AutoCodeItemNum);
				allCondsMet=true;
				for(int j=0;j<AutoCodeConds.ListForItem.Length;j++){
					if(!AutoCodeConds.ConditionIsMet
						(AutoCodeConds.ListForItem[j].Cond,toothNum,surf,isAdditional,willBeMissing,age)){
						allCondsMet=false;
					}
				}
				if(allCondsMet){
					return ListForCode[i].ADACode;
				}
			}
			return ListForCode[0].ADACode;//if couldn't find a better match
		}

		///<summary>Only called when closing the procedure edit window. Usually returns the supplied adaCode, unless a better match is found.</summary>
		public static string VerifyCode(string ADACode,string toothNum,string surf,bool isAdditional,int patNum,int age,
			out AutoCode AutoCodeCur)
		{
			bool allCondsMet;
			AutoCodeCur=null;
			if(!HList.ContainsKey(ADACode)){
				return ADACode;
			}
			if(!AutoCodes.HList.ContainsKey((int)HList[ADACode])){
				return ADACode;//just in case.
			}
			AutoCodeCur=(AutoCode)AutoCodes.HList[(int)HList[ADACode]];
			if(AutoCodeCur.LessIntrusive){
				return ADACode;
			}
			bool willBeMissing=Procedures.WillBeMissing(toothNum,patNum);
			//AutoCode verAutoCode=(AutoCode)HList[ADACode];
			GetListForCode((int)HList[ADACode]);
			for(int i=0;i<ListForCode.Length;i++){
				AutoCodeConds.GetListForItem(ListForCode[i].AutoCodeItemNum);
				allCondsMet=true;
				for(int j=0;j<AutoCodeConds.ListForItem.Length;j++){
					if(!AutoCodeConds.ConditionIsMet
						(AutoCodeConds.ListForItem[j].Cond,toothNum,surf,isAdditional,willBeMissing,age)){
						allCondsMet=false;
					}
				}
				if(allCondsMet){
					return ListForCode[i].ADACode;
				}
			}
			return ADACode;//if couldn't find a better match
		}

		



	}

	
	


}









