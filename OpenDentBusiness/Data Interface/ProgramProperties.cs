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
			ProgramPropertyC.Listt=Crud.ProgramPropertyCrud.TableToList(table);
		}

		///<summary></summary>
		public static void Update(ProgramProperty Cur){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),Cur);
				return;
			}
			Crud.ProgramPropertyCrud.Update(Cur);
		}

		///<summary>This can only be called from ClassConversions. Users not allowed to add properties so there is no user interface.</summary>
		public static long Insert(ProgramProperty Cur){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Cur.ProgramPropertyNum=Meth.GetLong(MethodBase.GetCurrentMethod(),Cur);
				return Cur.ProgramPropertyNum;
			}
			return Crud.ProgramPropertyCrud.Insert(Cur);
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

		///<summary>Returns a List of programproperties attached to the specified programNum.  Does not include path overrides.</summary>
		public static List<ProgramProperty> GetListForProgram(long programNum) {
			//No need to check RemotingRole; no call to db.
			List<ProgramProperty> listProgProp=new List<ProgramProperty>();
			for(int i=0;i<ProgramPropertyC.Listt.Count;i++) {
				if(ProgramPropertyC.Listt[i].ProgramNum==programNum && ProgramPropertyC.Listt[i].PropertyDesc!="") {
					listProgProp.Add(ProgramPropertyC.Listt[i]);
				}
			}
			return listProgProp;
		}

		///<summary>Returns an ArrayList of programproperties attached to the specified programNum.  Does not include path overrides.</summary>
		public static ArrayList GetForProgram(long programNum) {
			//No need to check RemotingRole; no call to db.
			ArrayList ForProgram=new ArrayList();
			for(int i=0;i<ProgramPropertyC.Listt.Count;i++) {
				if(ProgramPropertyC.Listt[i].ProgramNum==programNum && ProgramPropertyC.Listt[i].PropertyDesc!="") {
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

		public static string GetPropVal(ProgramName progName,string propertyDesc) {
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

		///<summary>Returns the path override for the current computer and the specified programNum.  Returns empty string if no override found.</summary>
		public static string GetLocalPathOverrideForProgram(long programNum) {
			//No need to check RemotingRole; no call to db.
			for(int i=0;i<ProgramPropertyC.Listt.Count;i++) {
				if(ProgramPropertyC.Listt[i].ProgramNum==programNum
					&& ProgramPropertyC.Listt[i].PropertyDesc==""
					&& ProgramPropertyC.Listt[i].ComputerName.ToUpper()==Environment.MachineName.ToUpper()) 
				{
					return ProgramPropertyC.Listt[i].PropertyValue;
				}
			}
			return "";
		}

		///<summary>This will insert or update a local path override property for the specified programNum.</summary>
		public static void InsertOrUpdateLocalOverridePath(long programNum,string newPath) {
			//No need to check RemotingRole; no call to db.
			for(int i=0;i<ProgramPropertyC.Listt.Count;i++) {
				if(ProgramPropertyC.Listt[i].ProgramNum==programNum
					&& ProgramPropertyC.Listt[i].PropertyDesc==""
					&& ProgramPropertyC.Listt[i].ComputerName.ToUpper()==Environment.MachineName.ToUpper()) 
				{
					ProgramPropertyC.Listt[i].PropertyValue=newPath;
					ProgramProperties.Update(ProgramPropertyC.Listt[i]);
					return;//Will only be one override per computer per program.
				}
			}
			//Path override does not exist for the current computer so create a new one.
			ProgramProperty pp=new ProgramProperty();
			pp.ProgramNum=programNum;
			pp.PropertyValue=newPath;
			pp.ComputerName=Environment.MachineName.ToUpper();
			ProgramProperties.Insert(pp);
		}




		
	}

	

	


}










