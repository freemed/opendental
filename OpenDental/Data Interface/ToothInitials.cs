using System;
using System.Collections;
using System.Data;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{
	///<summary></summary>
	public class ToothInitials {

		///<summary>Gets all toothinitial entries for the current patient.</summary>
		public static ToothInitial[] Refresh(int patNum) {
			string command=
				"SELECT * FROM toothinitial"
				+" WHERE PatNum = "+POut.PInt(patNum);
			DataTable table=General.GetTable(command);
			ToothInitial[] List=new ToothInitial[table.Rows.Count];
			for(int i=0;i<List.Length;i++) {
				List[i]=new ToothInitial();
				List[i].ToothInitialNum= PIn.PInt(table.Rows[i][0].ToString());
				List[i].PatNum         = PIn.PInt(table.Rows[i][1].ToString());
				List[i].ToothNum       = PIn.PString(table.Rows[i][2].ToString());
				List[i].InitialType    = (ToothInitialType)PIn.PInt(table.Rows[i][3].ToString());
				List[i].Movement       = PIn.PFloat(table.Rows[i][4].ToString());
			}
			return List;
		}
	

		///<summary></summary>
		public static void Insert(ToothInitial init){
			if(PrefB.RandomKeys) {
				init.ToothInitialNum=MiscData.GetKey("toothinitial","ToothInitialNum");
			}
			string command="INSERT INTO toothinitial (";
			if(PrefB.RandomKeys) {
				command+="ToothInitialNum,";
			}
			command+="PatNum,ToothNum,InitialType,Movement) VALUES(";
			if(PrefB.RandomKeys) {
				command+="'"+POut.PInt(init.ToothInitialNum)+"', ";
			}
			command+=
				 "'"+POut.PInt   (init.PatNum)+"', "
				+"'"+POut.PString(init.ToothNum)+"', "
				+"'"+POut.PInt   ((int)init.InitialType)+"', "
				+"'"+POut.PFloat (init.Movement)+"')";
			if(PrefB.RandomKeys) {
				General.NonQ(command);
			}
			else {
				init.ToothInitialNum=General.NonQ(command,true);
			}
		}

		///<summary></summary>
		public static void Update(ToothInitial init) {
			string command= "UPDATE toothinitial SET "
				+"PatNum = '"     +POut.PInt   (init.PatNum)+"', "
				+"ToothNum= '"    +POut.PString(init.ToothNum)+"', "
				+"InitialType = '"+POut.PInt   ((int)init.InitialType)+"', "
				+"Movement = '"   +POut.PFloat (init.Movement)+"' "
				+"WHERE ToothInitialNum = '"+POut.PInt(init.ToothInitialNum)+"'";
			General.NonQ(command);
		}

		///<summary></summary>
		public static void Delete(ToothInitial init) {
			string command= "DELETE FROM toothinitial WHERE ToothInitialNum = '"+init.ToothInitialNum.ToString()+"'";
			General.NonQ(command);
		}
		
		///<summary>Sets teeth missing, or sets primary, or sets movement values.  It first clears the value from the database, then adds a new row to represent that value.  Movements require an amount.  If movement amt is 0, then no row gets added.</summary>
		public static void SetValue(int patNum,string tooth_id,ToothInitialType initialType) {
			SetValue(patNum,tooth_id,initialType,0);
		}

		///<summary>Sets teeth missing, or sets primary, or sets movement values.  It first clears the value from the database, then adds a new row to represent that value.  Movements require an amount.  If movement amt is 0, then no row gets added.</summary>
		public static void SetValue(int patNum,string tooth_id,ToothInitialType initialType,float moveAmt) {
			ClearValue(patNum,tooth_id,initialType);
			ToothInitial ti=new ToothInitial();
			ti.PatNum=patNum;
			ti.ToothNum=tooth_id;
			ti.InitialType=initialType;
			ti.Movement=moveAmt;
			ToothInitials.Insert(ti);
		}

		///<summary>Same as SetValue, but does not clear any values first.  Only use this if you have first run ClearAllValuesForType.</summary>
		public static void SetValueQuick(int patNum,string tooth_id,ToothInitialType initialType,float moveAmt) {
			ToothInitial ti=new ToothInitial();
			ti.PatNum=patNum;
			ti.ToothNum=tooth_id;
			ti.InitialType=initialType;
			ti.Movement=moveAmt;
			ToothInitials.Insert(ti);
		}

		///<summary>Only used for incremental tooth movements.  Automatically adds a movement to any existing movement.  Supply a list of all toothInitials for the patient.</summary>
		public static void AddMovement(ToothInitial[] initialList,int patNum,string tooth_id,ToothInitialType initialType,float moveAmt) {
			ToothInitial ti=null;
			for(int i=0;i<initialList.Length;i++){
				if(initialList[i].ToothNum==tooth_id
					&& initialList[i].InitialType==initialType)
				{
					ti=initialList[i].Copy();
				}
			}
			if(ti==null){
				ti=new ToothInitial();
				ti.PatNum=patNum;
				ti.ToothNum=tooth_id;
				ti.InitialType=initialType;
				ti.Movement=moveAmt;
				ToothInitials.Insert(ti);
				return;
			}
			ti.Movement+=moveAmt;
			ToothInitials.Update(ti);		
		}

		///<summary>Sets teeth not missing, or sets to perm, or clears movement values.</summary>
		public static void ClearValue(int patNum,string tooth_id,ToothInitialType initialType) {
			string command="DELETE FROM toothinitial WHERE PatNum="+POut.PInt(patNum)
				+" AND ToothNum='"+POut.PString(tooth_id)
				+"' AND InitialType="+POut.PInt((int)initialType);
			General.NonQ(command);
		}

		///<summary>Sets teeth not missing, or sets to perm, or clears movement values.  Clears all the values of one type for all teeth in the mouth.</summary>
		public static void ClearAllValuesForType(int patNum,ToothInitialType initialType) {
			string command="DELETE FROM toothinitial WHERE PatNum="+POut.PInt(patNum)
				+" AND InitialType="+POut.PInt((int)initialType);
			General.NonQ(command);
		}

		///<summary>Gets a list of missing teeth as strings. Includes "1"-"32", and "A"-"Z".</summary>
		public static ArrayList GetMissingOrHiddenTeeth(ToothInitial[] initialList) {
			ArrayList missing=new ArrayList();
			for(int i=0;i<initialList.Length;i++) {
				if((initialList[i].InitialType==ToothInitialType.Missing || initialList[i].InitialType==ToothInitialType.Hidden)
					&& Tooth.IsValidDB(initialList[i].ToothNum)
					&& !Tooth.IsSuperNum(initialList[i].ToothNum))
				{
					missing.Add(initialList[i].ToothNum);
				}
			}
			return missing;
		}

		///<summary>Gets a list of primary teeth as strings. Includes "1"-"32".</summary>
		public static ArrayList GetPriTeeth(ToothInitial[] initialList) {
			ArrayList pri=new ArrayList();
			for(int i=0;i<initialList.Length;i++) {
				if(initialList[i].InitialType==ToothInitialType.Primary
					&& Tooth.IsValidDB(initialList[i].ToothNum)
					&& !Tooth.IsPrimary(initialList[i].ToothNum)
					&& !Tooth.IsSuperNum(initialList[i].ToothNum))
				{
					pri.Add(initialList[i].ToothNum);
				}
			}
			return pri;
		}

		///<summary>Loops through supplied initial list to see if the specified tooth is already marked as missing or hidden.</summary>
		public static bool ToothIsMissingOrHidden(ToothInitial[] initialList,string toothNum){
			for(int i=0;i<initialList.Length;i++){
				if(initialList[i].InitialType!=ToothInitialType.Missing
					&& initialList[i].InitialType!=ToothInitialType.Hidden)
				{
					continue;
				}
				if(initialList[i].ToothNum!=toothNum){
					continue;
				}
				return true;
			}
			return false;
		}

		///<summary>Gets the current movement value for a single tooth by looping through the supplied list.</summary>
		public static float GetMovement(ToothInitial[] initialList,string toothNum,ToothInitialType initialType){
			for(int i=0;i<initialList.Length;i++) {
				if(initialList[i].InitialType==initialType
					&& initialList[i].ToothNum==toothNum)
				{
					return initialList[i].Movement;
				}
			}
			return 0;
		}

		///<summary>Gets a list of the hidden teeth as strings. Includes "1"-"32", and "A"-"Z".</summary>
		public static ArrayList GetHiddenTeeth(ToothInitial[] initialList) {
			ArrayList hidden=new ArrayList();
			for(int i=0;i<initialList.Length;i++) {
				if(initialList[i].InitialType==ToothInitialType.Hidden
					&& Tooth.IsValidDB(initialList[i].ToothNum)
					&& !Tooth.IsSuperNum(initialList[i].ToothNum))
				{
					hidden.Add(initialList[i].ToothNum);
				}
			}
			return hidden;
		}


	}

	




}

















