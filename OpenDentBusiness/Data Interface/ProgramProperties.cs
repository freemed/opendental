using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace OpenDentBusiness {

	///<summary></summary>
	public class ProgramProperties{
		///<summary></summary>
		public static DataTable RefreshCache() {
			//No need to check RemotingRole; Calls GetTableRemotelyIfNeeded().
			string command="SELECT * FROM programproperty";
			DataTable table=Cache.GetTableRemotelyIfNeeded(MethodBase.GetCurrentMethod(),command);
			table.TableName="ProgramProperty";
			FillCache(table);
			return table;
		}
	
		///<summary></summary>
		public static void FillCache(DataTable table) {
			//No need to check RemotingRole; no call to db.
			ProgramPropertyC.Listt=new List<ProgramProperty>();
			ProgramProperty progprop;
			for (int i=0;i<table.Rows.Count;i++){
				progprop=new ProgramProperty();
				progprop.ProgramPropertyNum =PIn.Long(table.Rows[i][0].ToString());
				progprop.ProgramNum         =PIn.Long(table.Rows[i][1].ToString());
				progprop.PropertyDesc       =PIn.String(table.Rows[i][2].ToString());
				progprop.PropertyValue      =PIn.String(table.Rows[i][3].ToString());
				ProgramPropertyC.Listt.Add(progprop);
				//List[i].ValueType          =(FieldValueType)PIn.PInt(table.Rows[i][4].ToString());
			}
			//MessageBox.Show();
		}

		///<summary></summary>
		public static void Update(ProgramProperty Cur){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),Cur);
				return;
			}
			string command= "UPDATE programproperty SET "
				+"ProgramNum = '"     +POut.Long   (Cur.ProgramNum)+"'"
				+",PropertyDesc  = '" +POut.String(Cur.PropertyDesc)+"'"
				+",PropertyValue = '" +POut.String(Cur.PropertyValue)+"'"
				+" WHERE ProgramPropertyNum = '"+POut.Long(Cur.ProgramPropertyNum)+"'";
			Db.NonQ(command);
		}

		///<summary>This can only be called from ClassConversions. Users not allowed to add properties so there is no user interface.</summary>
		public static long Insert(ProgramProperty Cur){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Cur.ProgramPropertyNum=Meth.GetLong(MethodBase.GetCurrentMethod(),Cur);
				return Cur.ProgramPropertyNum;
			}
			if(PrefC.RandomKeys) {
				Cur.ProgramPropertyNum=ReplicationServers.GetKey("programproperty","ProgramPropertyNum");
			}
			string command="INSERT INTO programproperty (";
			if(PrefC.RandomKeys) {
				command+="ProgramPropertyNum,";
			}
			command+="ProgramNum,PropertyDesc,PropertyValue) VALUES(";
			if(PrefC.RandomKeys) {
				command+=POut.Long(Cur.ProgramPropertyNum)+", ";
			}
			command+=
				 "'"+POut.Long   (Cur.ProgramNum)+"', "
				+"'"+POut.String(Cur.PropertyDesc)+"', "
				+"'"+POut.String(Cur.PropertyValue)+"')";
			if(PrefC.RandomKeys) {
				Db.NonQ(command);
			}
			else{
				Cur.ProgramPropertyNum=Db.NonQ(command,true);
			}
			return Cur.ProgramPropertyNum;
		}

		
		///<summary>This can only be called from ClassConversions. Users not allowed to delete properties so there is no user interface.</summary>
		public static void Delete(ProgramProperty Cur){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),Cur);
				return;
			}
			string command= "DELETE from programproperty WHERE programpropertynum = '"+Cur.ProgramPropertyNum.ToString()+"'";
			Db.NonQ(command);
		}

		///<summary>Returns a List of programproperties attached to the specified programNum</summary>
		public static List<ProgramProperty> GetListForProgram(long programNum) {
			//No need to check RemotingRole; no call to db.
			List<ProgramProperty> ForProgram=new List<ProgramProperty>();
			for(int i=0;i<ProgramPropertyC.Listt.Count;i++) {
				if(ProgramPropertyC.Listt[i].ProgramNum==programNum) {
					ForProgram.Add(ProgramPropertyC.Listt[i]);
				}
			}
			return ForProgram;
		}

		///<summary>Returns an ArrayList of programproperties attached to the specified programNum</summary>
		public static ArrayList GetForProgram(long programNum) {
			//No need to check RemotingRole; no call to db.
			ArrayList ForProgram=new ArrayList();
			for(int i=0;i<ProgramPropertyC.Listt.Count;i++) {
				if(ProgramPropertyC.Listt[i].ProgramNum==programNum) {
					ForProgram.Add(ProgramPropertyC.Listt[i]);
				}
			}
			return ForProgram;
		}

		public static void SetProperty(long programNum,string desc,string propval) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),programNum,desc,propval);
				return;
			}
			string command="UPDATE programproperty SET PropertyValue='"+POut.String(propval)+"' "
				+"WHERE ProgramNum="+POut.Long(programNum)+" "
				+"AND PropertyDesc='"+POut.String(desc)+"'";
			Db.NonQ(command);
		}

		///<summary>After GetForProgram has been run, this gets one of those properties.</summary>
		public static ProgramProperty GetCur(ArrayList ForProgram, string desc){
			//No need to check RemotingRole; no call to db.
			for(int i=0;i<ForProgram.Count;i++){
				if(((ProgramProperty)ForProgram[i]).PropertyDesc==desc){
					return (ProgramProperty)ForProgram[i];
				}
			}
			return null;
		}

		public static string GetPropVal(long programNum,string desc) {
			//No need to check RemotingRole; no call to db.
			for(int i=0;i<ProgramPropertyC.Listt.Count;i++) {
				if(ProgramPropertyC.Listt[i].ProgramNum!=programNum) {
					continue;
				}
				if(ProgramPropertyC.Listt[i].PropertyDesc!=desc) {
					continue;
				}
				return ProgramPropertyC.Listt[i].PropertyValue;
			}
			throw new ApplicationException("Property not found: "+desc);
		}

		public static string GetPropVal(string progName,string propertyDesc) {
			//No need to check RemotingRole; no call to db.
			long programNum=Programs.GetProgramNum(progName);
			for(int i=0;i<ProgramPropertyC.Listt.Count;i++) {
				if(ProgramPropertyC.Listt[i].ProgramNum!=programNum) {
					continue;
				}
				if(ProgramPropertyC.Listt[i].PropertyDesc!=propertyDesc) {
					continue;
				}
				return ProgramPropertyC.Listt[i].PropertyValue;
			}
			throw new ApplicationException("Property not found: "+propertyDesc);
		}

		///<summary>Used in FormUAppoint to get frequent and current data.</summary>
		public static string GetValFromDb(long programNum,string desc) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetString(MethodBase.GetCurrentMethod(),programNum,desc);
			}
			string command="SELECT PropertyValue FROM programproperty WHERE ProgramNum="+POut.Long(programNum)
				+" AND PropertyDesc='"+POut.String(desc)+"'";
			DataTable table=Db.GetTable(command);
			if(table.Rows.Count==0){
				return "";
			}
			return table.Rows[0][0].ToString();
		}




		
	}

	

	


}










