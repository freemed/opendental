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
		///<summary>key=CodeNum,value=AutoCodeNum</summary>
		public static Hashtable HList;

		///<summary></summary>
		public static void Refresh(){
			string command="SELECT * FROM autocodeitem";
			DataTable table=General.GetTable(command);
			HList=new Hashtable();
			List=new AutoCodeItem[table.Rows.Count];
			for(int i=0;i<List.Length;i++){
				List[i]=new AutoCodeItem();
				List[i].AutoCodeItemNum= PIn.PInt   (table.Rows[i][0].ToString());
				List[i].AutoCodeNum    = PIn.PInt   (table.Rows[i][1].ToString());
				//List[i].OldCode      = PIn.PString(table.Rows[i][2].ToString());
				List[i].CodeNum        = PIn.PInt   (table.Rows[i][3].ToString());
				if(!HList.ContainsKey(List[i].CodeNum)){
					HList.Add(List[i].CodeNum,List[i].AutoCodeNum);
				}
			}
		}

		///<summary></summary>
		public static void Insert(AutoCodeItem Cur){
			string command= "INSERT INTO autocodeitem (autocodenum,OldCode,CodeNum) "
				+"VALUES ("
				+"'"+POut.PInt   (Cur.AutoCodeNum)+"', "
				+"'"+POut.PString(Cur.OldCode)+"', "
				+"'"+POut.PInt   (Cur.CodeNum)+"')";
			//MessageBox.Show(string command);
			Cur.AutoCodeItemNum=General.NonQ(command,true);
		}

		///<summary></summary>
		public static void Update(AutoCodeItem Cur){
			string command= "UPDATE autocodeitem SET "
				+"AutoCodeNum='"+POut.PInt   (Cur.AutoCodeNum)+"'"
				//+",Oldcode ='"  +POut.PString(Cur.OldCode)+"'"
				+",CodeNum ='"  +POut.PInt   (Cur.CodeNum)+"'"
				+" WHERE AutoCodeItemNum = '"+POut.PInt(Cur.AutoCodeItemNum)+"'";
			General.NonQ(command);
		}

		///<summary></summary>
		public static void Delete(AutoCodeItem Cur){
			string command= "DELETE FROM autocodeitem WHERE AutoCodeItemNum = '"
				+POut.PInt(Cur.AutoCodeItemNum)+"'";
			General.NonQ(command);
		}

		///<summary></summary>
		public static void Delete(int autoCodeNum){
			string command= "DELETE FROM autocodeitem WHERE AutoCodeNum = '"
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
		public static int GetCodeNum(int autoCodeNum,string toothNum,string surf,bool isAdditional,int patNum,int age){
			bool allCondsMet;
			GetListForCode(autoCodeNum);
			if(ListForCode.Length==0){
				return 0;
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
					return ListForCode[i].CodeNum;
				}
			}
			return ListForCode[0].CodeNum;//if couldn't find a better match
		}

		///<summary>Only called when closing the procedure edit window. Usually returns the supplied CodeNum, unless a better match is found.</summary>
		public static int VerifyCode(int codeNum,string toothNum,string surf,bool isAdditional,int patNum,int age,
			out AutoCode AutoCodeCur)
		{
			bool allCondsMet;
			AutoCodeCur=null;
			if(!HList.ContainsKey(codeNum)){
				return codeNum;
			}
			if(!AutoCodes.HList.ContainsKey((int)HList[codeNum])){
				return codeNum;//just in case.
			}
			AutoCodeCur=(AutoCode)AutoCodes.HList[(int)HList[codeNum]];
			if(AutoCodeCur.LessIntrusive){
				return codeNum;
			}
			bool willBeMissing=Procedures.WillBeMissing(toothNum,patNum);
			GetListForCode((int)HList[codeNum]);
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
					return ListForCode[i].CodeNum;
				}
			}
			return codeNum;//if couldn't find a better match
		}

		



	}

	
	


}









