using System;
using System.Collections;
using System.Data;
using System.Reflection;

namespace OpenDentBusiness{
	///<summary></summary>
	public class Clearinghouses {
		///<summary>List of all clearinghouses.</summary>
		private static Clearinghouse[] list;
		///<summary>Key=PayorID. Value=ClearinghouseNum.</summary>
		private static Hashtable HList;

		public static Clearinghouse[] List{
			//No need to check RemotingRole; no call to db.
			get{
				if(list==null){
					RefreshCache();
				}
				return list;
			}
			set{
				list=value;
			}
		}

		public static DataTable RefreshCache() {
			//No need to check RemotingRole; Calls GetTableRemotelyIfNeeded().
			string command="SELECT * FROM clearinghouse";
			DataTable table=Cache.GetTableRemotelyIfNeeded(MethodBase.GetCurrentMethod(),command);
			table.TableName="Clearinghouse";
			FillCache(table);
			return table;
		}

		///<summary></summary>
		public static void FillCache(DataTable table) {
			//No need to check RemotingRole; no call to db.
			list=new Clearinghouse[table.Rows.Count];
			HList=new Hashtable();
			string[] payors;
			for(int i=0;i<table.Rows.Count;i++) {
				list[i]=new Clearinghouse();
				list[i].ClearinghouseNum= PIn.Long(table.Rows[i][0].ToString());
				list[i].Description     = PIn.String(table.Rows[i][1].ToString());
				list[i].ExportPath      = PIn.String(table.Rows[i][2].ToString());
				list[i].IsDefault       = PIn.Bool(table.Rows[i][3].ToString());
				list[i].Payors          = PIn.String(table.Rows[i][4].ToString());
				list[i].Eformat         = (ElectronicClaimFormat)PIn.Long(table.Rows[i][5].ToString());
				list[i].ISA05           = PIn.String(table.Rows[i][6].ToString());
				list[i].SenderTIN       = PIn.String(table.Rows[i][7].ToString());
				list[i].ISA07           = PIn.String(table.Rows[i][8].ToString());
				list[i].ISA08           = PIn.String(table.Rows[i][9].ToString());
				list[i].ISA15           = PIn.String(table.Rows[i][10].ToString());
				list[i].Password        = PIn.String(table.Rows[i][11].ToString());
				list[i].ResponsePath    = PIn.String(table.Rows[i][12].ToString());
				list[i].CommBridge      = (EclaimsCommBridge)PIn.Long(table.Rows[i][13].ToString());
				list[i].ClientProgram   = PIn.String(table.Rows[i][14].ToString());
				//15: LastBatchNumber
				list[i].ModemPort       = PIn.Int(table.Rows[i][16].ToString());
				list[i].LoginID         = PIn.String(table.Rows[i][17].ToString());
				list[i].SenderName      = PIn.String(table.Rows[i][18].ToString());
				list[i].SenderTelephone = PIn.String(table.Rows[i][19].ToString());
				list[i].GS03            = PIn.String(table.Rows[i][20].ToString());
				payors=list[i].Payors.Split(',');
				for(int j=0;j<payors.Length;j++) {
					if(!HList.ContainsKey(payors[j])) {
						HList.Add(payors[j],list[i].ClearinghouseNum);
					}
				}
			}
		}

		///<summary>Inserts this clearinghouse into database.</summary>
		public static long Insert(Clearinghouse clearhouse){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				clearhouse.ClearinghouseNum=Meth.GetLong(MethodBase.GetCurrentMethod(),clearhouse);
				return clearhouse.ClearinghouseNum;
			}
			if(PrefC.RandomKeys) {
				clearhouse.ClearinghouseNum=ReplicationServers.GetKey("clearinghouse","ClearinghouseNum");
			}
			string command="INSERT INTO clearinghouse (";
			if(PrefC.RandomKeys) {
				command+="ClearinghouseNum,";
			}
			command+="Description,ExportPath,IsDefault,Payors"
				+",Eformat,ISA05,SenderTIN,ISA07,ISA08,ISA15,Password,ResponsePath,CommBridge,ClientProgram,"
				+"LastBatchNumber,ModemPort,LoginID,SenderName,SenderTelephone,GS03) VALUES(";
			if(PrefC.RandomKeys) {
				command+=POut.Long(clearhouse.ClearinghouseNum)+", ";
			}
			command+=
				 "'"+POut.String(clearhouse.Description)+"', "
				+"'"+POut.String(clearhouse.ExportPath)+"', "
				+"'"+POut.Bool  (clearhouse.IsDefault)+"', "
				+"'"+POut.String(clearhouse.Payors)+"', "
				+"'"+POut.Long   ((int)clearhouse.Eformat)+"', "
				+"'"+POut.String(clearhouse.ISA05)+"', "
				+"'"+POut.String(clearhouse.SenderTIN)+"', "
				+"'"+POut.String(clearhouse.ISA07)+"', "
				+"'"+POut.String(clearhouse.ISA08)+"', "
				+"'"+POut.String(clearhouse.ISA15)+"', "
				+"'"+POut.String(clearhouse.Password)+"', "
				+"'"+POut.String(clearhouse.ResponsePath)+"', "
				+"'"+POut.Long   ((int)clearhouse.CommBridge)+"', "
				+"'"+POut.String(clearhouse.ClientProgram)+"', "
				+"'0', "//LastBatchNumber
				+"'"+POut.Long   (clearhouse.ModemPort)+"', "
				+"'"+POut.String(clearhouse.LoginID)+"', "
				+"'"+POut.String(clearhouse.SenderName)+"', "
				+"'"+POut.String(clearhouse.SenderTelephone)+"', "
				+"'"+POut.String(clearhouse.GS03)+"')";
			if(PrefC.RandomKeys) {
				Db.NonQ(command);
			}
			else {
				clearhouse.ClearinghouseNum=Db.NonQ(command,true);
			}
			return clearhouse.ClearinghouseNum;
		}

		///<summary></summary>
		public static void Update(Clearinghouse clearhouse){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),clearhouse);
				return;
			}
			string command="UPDATE clearinghouse SET "
				+"Description = '"  +POut.String(clearhouse.Description)+"' "
				+",ExportPath = '"  +POut.String(clearhouse.ExportPath)+"' "
				+",IsDefault = '"   +POut.Bool  (clearhouse.IsDefault)+"' "
				+",Payors = '"      +POut.String(clearhouse.Payors)+"' "
				+",Eformat = '"     +POut.Long   ((int)clearhouse.Eformat)+"' "
				+",ISA05 = '"       +POut.String(clearhouse.ISA05)+"' "
				+",SenderTIN = '"   +POut.String(clearhouse.SenderTIN)+"' "
				+",ISA07 = '"       +POut.String(clearhouse.ISA07)+"' "
				+",ISA08 = '"       +POut.String(clearhouse.ISA08)+"' "
				+",ISA15 = '"       +POut.String(clearhouse.ISA15)+"' "
				+",Password = '"    +POut.String(clearhouse.Password)+"' "
				+",ResponsePath = '"+POut.String(clearhouse.ResponsePath)+"' "
				+",CommBridge = '"  +POut.Long   ((int)clearhouse.CommBridge)+"' "
				+",ClientProgram ='"+POut.String(clearhouse.ClientProgram)+"' "
				//LastBatchNumber
				+",ModemPort ='"    +POut.Long   (clearhouse.ModemPort)+"' "
				+",LoginID ='"      +POut.String(clearhouse.LoginID)+"' "
				+",SenderName = '"  +POut.String(clearhouse.SenderName)+"' "
				+",SenderTelephone='"+POut.String(clearhouse.SenderTelephone)+"' "
				+",GS03 = '"         +POut.String(clearhouse.GS03)+"' "
				+"WHERE ClearinghouseNum = '"+POut.Long   (clearhouse.ClearinghouseNum)+"'";
 			Db.NonQ(command);
		}

		///<summary></summary>
		public static void Delete(Clearinghouse clearhouse){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),clearhouse);
				return;
			}
			string command="DELETE FROM clearinghouse "
				+"WHERE ClearinghouseNum = '"+POut.Long(clearhouse.ClearinghouseNum)+"'";
			Db.NonQ(command);
		}

		///<summary>Gets the last batch number for this clearinghouse and increments it by one.  Saves the new value, then returns it.  So even if the new value is not used for some reason, it will have already been incremented. Remember that LastBatchNumber is never accurate with local data in memory.</summary>
		public static int GetNextBatchNumber(Clearinghouse clearhouse){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetInt(MethodBase.GetCurrentMethod(),clearhouse);
			}
			//get last batch number
			string command="SELECT LastBatchNumber FROM clearinghouse "
				+"WHERE ClearinghouseNum = "+POut.Long(clearhouse.ClearinghouseNum);
 			DataTable table=Db.GetTable(command);
			int batchNum=PIn.Int(table.Rows[0][0].ToString());
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
				+" WHERE ClearinghouseNum = "+POut.Long(clearhouse.ClearinghouseNum);
			Db.NonQ(command);
			return batchNum;
		}

		///<summary>Returns the clearinghouseNum for claims for the supplied payorID.  If the payorID was not entered or if no default was set, then 0 is returned.</summary>
		public static long GetNumForPayor(string payorID){
			//No need to check RemotingRole; no call to db.
			//this is not done because Renaissance does not require payorID
			//if(payorID==""){
			//	return ElectronicClaimFormat.None;
			//}
			if(HList==null) {
				RefreshCache();
			}
			if(payorID!="" && HList.ContainsKey(payorID)){
				return (long)HList[payorID];
			}
			//payorID not found
			Clearinghouse defaultCH=GetDefault();
			if(defaultCH==null){
				return 0;//ElectronicClaimFormat.None;
			}
			return defaultCH.ClearinghouseNum;
		}

		///<summary>Returns the default clearinghouse. If no default present, returns null.</summary>
		public static Clearinghouse GetDefault(){
			//No need to check RemotingRole; no call to db.
			for(int i=0;i<List.Length;i++){
				if(List[i].IsDefault){
					return List[i];
				}
			}
			return null;
		}

	}
}