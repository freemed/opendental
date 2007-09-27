using System;
using System.Collections;
using System.Data;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{
  ///<summary></summary>
	public class AutoCodeConds{
		///<summary></summary>
		public static AutoCodeCond[] List;
		///<summary></summary>
		public static AutoCodeCond[] ListForItem;
		private static ArrayList ALlist;
		//public static Hashtable HList; 

		///<summary></summary>
		public static void Refresh(){
			string command="SELECT * from autocodecond ORDER BY cond";
			DataTable table=General.GetTable(command);
			//HList=new Hashtable();
			List=new AutoCodeCond[table.Rows.Count];
			for(int i=0;i<List.Length;i++){
				List[i]=new AutoCodeCond();
				List[i].AutoCodeCondNum= PIn.PInt        (table.Rows[i][0].ToString());
				List[i].AutoCodeItemNum= PIn.PInt        (table.Rows[i][1].ToString());
				List[i].Cond=(AutoCondition)PIn.PInt(table.Rows[i][2].ToString());	
				//HList.Add(List[i].AutoCodeItemNum,List[i]);
			}
		}

		///<summary></summary>
		public static void Insert(AutoCodeCond Cur){
			string command= "INSERT INTO autocodecond (AutoCodeItemNum,Cond) "
				+"VALUES ("
				+"'"+POut.PInt(Cur.AutoCodeItemNum)+"', "
				+"'"+POut.PInt((int)Cur.Cond)+"')";
			//MessageBox.Show(string command);
			Cur.AutoCodeCondNum=General.NonQ(command,true);
		}

		///<summary></summary>
		public static void Update(AutoCodeCond Cur){
			string command = "UPDATE autocodecond SET "
				+"autocodeitemnum='"+POut.PInt(Cur.AutoCodeItemNum)+"'"
				+",cond ='"     +POut.PInt((int)Cur.Cond)+"'"
				+" WHERE autocodecondnum = '"+POut.PInt(Cur.AutoCodeCondNum)+"'";
			General.NonQ(command);
		}

		///<summary></summary>
		public static void Delete(AutoCodeCond Cur){
			string command= "DELETE from autocodecond WHERE autocodecondnum = '"
				+POut.PInt(Cur.AutoCodeCondNum)+"'";
			General.NonQ(command);
		}

		///<summary></summary>
		public static void DeleteForItemNum(int itemNum){
			string command= "DELETE from autocodecond WHERE autocodeitemnum = '"
				+POut.PInt(itemNum)+"'";//AutoCodeItems.Cur.AutoCodeItemNum)
			General.NonQ(command);
		}

		///<summary></summary>
		public static void GetListForItem(int autoCodeItemNum){
			ALlist=new ArrayList();
			for(int i=0;i<List.Length;i++){
				if(List[i].AutoCodeItemNum==autoCodeItemNum){
					ALlist.Add(List[i]);
				} 
			}
			ListForItem=new AutoCodeCond[ALlist.Count];
			if(ALlist.Count > 0){			
				ALlist.CopyTo(ListForItem);
			}     
		}

		///<summary></summary>
		public static bool IsSurf(AutoCondition myAutoCondition){
			switch(myAutoCondition){
				case AutoCondition.One_Surf:
				case AutoCondition.Two_Surf:
				case AutoCondition.Three_Surf:
				case AutoCondition.Four_Surf:
				case AutoCondition.Five_Surf:
					return true;
				default:
					return false;
			}
		}

		///<summary></summary>
		public static bool ConditionIsMet(AutoCondition myAutoCondition, string toothNum,string surf,bool isAdditional,bool willBeMissing,int age){
			switch(myAutoCondition){
				case AutoCondition.Anterior:
					return Tooth.IsAnterior(toothNum);
				case AutoCondition.Posterior:
					return Tooth.IsPosterior(toothNum);
				case AutoCondition.Premolar:
					return Tooth.IsPreMolar(toothNum);
				case AutoCondition.Molar:
					return Tooth.IsMolar(toothNum);
				case AutoCondition.One_Surf:
					return surf.Length==1;
				case AutoCondition.Two_Surf:
					return surf.Length==2;
				case AutoCondition.Three_Surf:
					return surf.Length==3;
				case AutoCondition.Four_Surf:
					return surf.Length==4;
				case AutoCondition.Five_Surf:
					return surf.Length==5;
				case AutoCondition.First:
					return !isAdditional;
				case AutoCondition.EachAdditional:
					return isAdditional;
				case AutoCondition.Maxillary:
					return Tooth.IsMaxillary(toothNum);
				case AutoCondition.Mandibular:
					return !Tooth.IsMaxillary(toothNum);
				case AutoCondition.Primary:
					return Tooth.IsPrimary(toothNum);
				case AutoCondition.Permanent:
					return !Tooth.IsPrimary(toothNum);
				case AutoCondition.Pontic:
					return willBeMissing;
				case AutoCondition.Retainer:
					return !willBeMissing;
				case AutoCondition.AgeOver18:
					return age>18;
				default:
					return false;
			}
		}



	}

	

	


}









