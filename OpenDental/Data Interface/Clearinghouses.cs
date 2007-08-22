using System;
using System.Collections;
using System.Data;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{
	///<summary></summary>
	public class Clearinghouses {
		///<summary>List of all clearinghouses.</summary>
		private static Clearinghouse[] list;
		///<summary>Key=PayorID. Value=ClearinghouseNum.</summary>
		private static Hashtable HList;

		public static Clearinghouse[] List{
			get{
				if(list==null){
					Refresh();
				}
				return list;
			}
			//set{list=List;}
		}

		///<summary></summary>
		public static void Refresh() {
			string command=
				"SELECT * FROM clearinghouse";
			DataTable table=General.GetTable(command);
			list=new Clearinghouse[table.Rows.Count];
			HList=new Hashtable();
			string[] payors;
			for(int i=0;i<table.Rows.Count;i++) {
				list[i]=new Clearinghouse();
				list[i].ClearinghouseNum= PIn.PInt(table.Rows[i][0].ToString());
				list[i].Description     = PIn.PString(table.Rows[i][1].ToString());
				list[i].ExportPath      = PIn.PString(table.Rows[i][2].ToString());
				list[i].IsDefault       = PIn.PBool(table.Rows[i][3].ToString());
				list[i].Payors          = PIn.PString(table.Rows[i][4].ToString());
				list[i].Eformat         = (ElectronicClaimFormat)PIn.PInt(table.Rows[i][5].ToString());
				list[i].ISA05           = PIn.PString(table.Rows[i][6].ToString());
				list[i].SenderTIN       = PIn.PString(table.Rows[i][7].ToString());
				list[i].ISA07           = PIn.PString(table.Rows[i][8].ToString());
				list[i].ISA08           = PIn.PString(table.Rows[i][9].ToString());
				list[i].ISA15           = PIn.PString(table.Rows[i][10].ToString());
				list[i].Password        = PIn.PString(table.Rows[i][11].ToString());
				list[i].ResponsePath    = PIn.PString(table.Rows[i][12].ToString());
				list[i].CommBridge      = (EclaimsCommBridge)PIn.PInt(table.Rows[i][13].ToString());
				list[i].ClientProgram   = PIn.PString(table.Rows[i][14].ToString());
				//15: LastBatchNumber
				list[i].ModemPort       = PIn.PInt(table.Rows[i][16].ToString());
				list[i].LoginID         = PIn.PString(table.Rows[i][17].ToString());
				list[i].SenderName      = PIn.PString(table.Rows[i][18].ToString());
				list[i].SenderTelephone = PIn.PString(table.Rows[i][19].ToString());
				list[i].GS03            = PIn.PString(table.Rows[i][20].ToString());
				payors=list[i].Payors.Split(',');
				for(int j=0;j<payors.Length;j++) {
					if(!HList.ContainsKey(payors[j])) {
						HList.Add(payors[j],list[i].ClearinghouseNum);
					}
				}
			}
		}

		///<summary>Inserts this clearinghouse into database.</summary>
		public static void Insert(Clearinghouse clearhouse){
			string command="INSERT INTO clearinghouse (Description,ExportPath,IsDefault,Payors"
				+",Eformat,ISA05,SenderTIN,ISA07,ISA08,ISA15,Password,ResponsePath,CommBridge,ClientProgram,"
				+"LastBatchNumber,ModemPort,LoginID,SenderName,SenderTelephone,GS03) VALUES("
				+"'"+POut.PString(clearhouse.Description)+"', "
				+"'"+POut.PString(clearhouse.ExportPath)+"', "
				+"'"+POut.PBool  (clearhouse.IsDefault)+"', "
				+"'"+POut.PString(clearhouse.Payors)+"', "
				+"'"+POut.PInt   ((int)clearhouse.Eformat)+"', "
				+"'"+POut.PString(clearhouse.ISA05)+"', "
				+"'"+POut.PString(clearhouse.SenderTIN)+"', "
				+"'"+POut.PString(clearhouse.ISA07)+"', "
				+"'"+POut.PString(clearhouse.ISA08)+"', "
				+"'"+POut.PString(clearhouse.ISA15)+"', "
				+"'"+POut.PString(clearhouse.Password)+"', "
				+"'"+POut.PString(clearhouse.ResponsePath)+"', "
				+"'"+POut.PInt   ((int)clearhouse.CommBridge)+"', "
				+"'"+POut.PString(clearhouse.ClientProgram)+"', "
				+"'0', "//LastBatchNumber
				+"'"+POut.PInt   (clearhouse.ModemPort)+"', "
				+"'"+POut.PString(clearhouse.LoginID)+"', "
				+"'"+POut.PString(clearhouse.SenderName)+"', "
				+"'"+POut.PString(clearhouse.SenderTelephone)+"', "
				+"'"+POut.PString(clearhouse.GS03)+"')";
 			General.NonQ(command);
		}

		///<summary></summary>
		public static void Update(Clearinghouse clearhouse){
			string command="UPDATE clearinghouse SET "
				+"Description = '"  +POut.PString(clearhouse.Description)+"' "
				+",ExportPath = '"  +POut.PString(clearhouse.ExportPath)+"' "
				+",IsDefault = '"   +POut.PBool  (clearhouse.IsDefault)+"' "
				+",Payors = '"      +POut.PString(clearhouse.Payors)+"' "
				+",Eformat = '"     +POut.PInt   ((int)clearhouse.Eformat)+"' "
				+",ISA05 = '"       +POut.PString(clearhouse.ISA05)+"' "
				+",SenderTIN = '"   +POut.PString(clearhouse.SenderTIN)+"' "
				+",ISA07 = '"       +POut.PString(clearhouse.ISA07)+"' "
				+",ISA08 = '"       +POut.PString(clearhouse.ISA08)+"' "
				+",ISA15 = '"       +POut.PString(clearhouse.ISA15)+"' "
				+",Password = '"    +POut.PString(clearhouse.Password)+"' "
				+",ResponsePath = '"+POut.PString(clearhouse.ResponsePath)+"' "
				+",CommBridge = '"  +POut.PInt   ((int)clearhouse.CommBridge)+"' "
				+",ClientProgram ='"+POut.PString(clearhouse.ClientProgram)+"' "
				//LastBatchNumber
				+",ModemPort ='"    +POut.PInt   (clearhouse.ModemPort)+"' "
				+",LoginID ='"      +POut.PString(clearhouse.LoginID)+"' "
				+",SenderName = '"  +POut.PString(clearhouse.SenderName)+"' "
				+",SenderTelephone='"+POut.PString(clearhouse.SenderTelephone)+"' "
				+",GS03 = '"         +POut.PString(clearhouse.GS03)+"' "
				+"WHERE ClearinghouseNum = '"+POut.PInt   (clearhouse.ClearinghouseNum)+"'";
 			General.NonQ(command);
		}

		///<summary></summary>
		public static void Delete(Clearinghouse clearhouse){
			string command="DELETE FROM clearinghouse "
				+"WHERE ClearinghouseNum = '"+POut.PInt(clearhouse.ClearinghouseNum)+"'";
			General.NonQ(command);
		}

		///<summary>Gets the last batch number for this clearinghouse and increments it by one.  Saves the new value, then returns it.  So even if the new value is not used for some reason, it will have already been incremented. Remember that LastBatchNumber is never accurate with local data in memory.</summary>
		public static int GetNextBatchNumber(Clearinghouse clearhouse){
			//get last batch number
			string command="SELECT LastBatchNumber FROM clearinghouse "
				+"WHERE ClearinghouseNum = "+POut.PInt(clearhouse.ClearinghouseNum);
 			DataTable table=General.GetTable(command);
			int batchNum=PIn.PInt(table.Rows[0][0].ToString());
			//and increment it by one
			if(clearhouse.Eformat==ElectronicClaimFormat.Canadian){
				if(batchNum==999999)
					batchNum=1;
				else
					batchNum++;
			}
			else{
				if(batchNum==999)
					batchNum=1;
				else
					batchNum++;
			}
			//save the new batch number. Even if user cancels, it will have incremented.
			command="UPDATE clearinghouse SET LastBatchNumber="+batchNum.ToString()
				+" WHERE ClearinghouseNum = "+POut.PInt(clearhouse.ClearinghouseNum);
			General.NonQ(command);
			return batchNum;
		}

		///<summary>Returns the clearinghouseNum for claims for the supplied payorID.  If the payorID was not entered or if no default was set, then 0 is returned.</summary>
		public static int GetNumForPayor(string payorID){
			//this is not done because Renaissance does not require payorID
			//if(payorID==""){
			//	return ElectronicClaimFormat.None;
			//}
			if(HList==null) {
				Refresh();
			}
			if(payorID!="" && HList.ContainsKey(payorID)){
				return (int)HList[payorID];
			}
			//payorID not found
			Clearinghouse defaultCH=GetDefault();
			if(defaultCH==null){
				return 0;//ElectronicClaimFormat.None;
			}
			return defaultCH.ClearinghouseNum;
		}

		///<summary>Returns the clearinghouse specified by the given num.</summary>
		public static Clearinghouse GetClearinghouse(int clearinghouseNum){
			for(int i=0;i<list.Length;i++){
				if(list[i].ClearinghouseNum==clearinghouseNum){
					return list[i];
				}
			}
			MessageBox.Show("Error. Could not locate Clearinghouse.");
			return null;
		}

		///<summary>Returns the default clearinghouse. If no default present, returns null.</summary>
		public static Clearinghouse GetDefault(){
			for(int i=0;i<List.Length;i++){
				if(List[i].IsDefault){
					return List[i];
				}
			}
			return null;
		}
		
		///<summary></summary>
		public static string GetDescript(int clearinghouseNum){
			if(clearinghouseNum==0){
				return "";
			}
			return GetClearinghouse(clearinghouseNum).Description;
		}

		///<summary>Gets the index of this clearinghouse within List</summary>
		public static int GetIndex(int clearinghouseNum){
			for(int i=0;i<List.Length;i++){
				if(List[i].ClearinghouseNum==clearinghouseNum){
					return i;
				}
			}
			MessageBox.Show("Clearinghouses.GetIndex failed.");
			return -1;
		}

		


	}

	



}









