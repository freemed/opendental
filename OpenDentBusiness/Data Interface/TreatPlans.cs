using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;

namespace OpenDentBusiness{
	///<summary></summary>
	public class TreatPlans {

		///<summary>Gets all TreatPlans for a given Patient, ordered by date.</summary>
		public static TreatPlan[] Refresh(long patNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<TreatPlan[]>(MethodBase.GetCurrentMethod(),patNum);
			}
			string command="SELECT * FROM treatplan "
				+"WHERE PatNum="+POut.Long(patNum)
				+" ORDER BY DateTP";
			return Crud.TreatPlanCrud.SelectMany(command).ToArray();
		}

		///<summary></summary>
		public static void Update(TreatPlan tp){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),tp);
				return;
			}
			Crud.TreatPlanCrud.Update(tp);
		}

		///<summary></summary>
		public static long Insert(TreatPlan tp) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				tp.TreatPlanNum=Meth.GetLong(MethodBase.GetCurrentMethod(),tp);
				return tp.TreatPlanNum;
			}
			return Crud.TreatPlanCrud.Insert(tp);
		}

		///<summary>Dependencies checked first and throws an exception if any found. So surround by try catch</summary>
		public static void Delete(TreatPlan tp){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),tp);
				return;
			}
			//check proctp for dependencies
			string command="SELECT * FROM proctp WHERE TreatPlanNum ="+POut.Long(tp.TreatPlanNum);
			DataTable table=Db.GetTable(command);
			if(table.Rows.Count>0){
				//this should never happen
				throw new InvalidProgramException(Lans.g("TreatPlans","Cannot delete treatment plan because it has ProcTP's attached"));
			}
			command= "DELETE from treatplan WHERE TreatPlanNum = '"+POut.Long(tp.TreatPlanNum)+"'";
 			Db.NonQ(command);
		}

		public static string GetHashString(TreatPlan tp,List<ProcTP> proclist) {
			//No need to check RemotingRole; no call to db.
			//the key data is a concatenation of the following:
			//tp: Note, DateTP
			//each proctp: Descript,PatAmt
			//The procedures MUST be in the correct order, and we'll use ItemOrder to order them.
			StringBuilder strb=new StringBuilder();
			strb.Append(tp.Note);
			strb.Append(tp.DateTP.ToString("yyyyMMdd"));
			for(int i=0;i<proclist.Count;i++){
				strb.Append(proclist[i].Descript);
				strb.Append(proclist[i].PatAmt.ToString("F2"));
			}
			byte[] textbytes=Encoding.UTF8.GetBytes(strb.ToString());
			//byte[] filebytes = GetBytes(doc);
			//int fileLength = filebytes.Length;
			//byte[] buffer = new byte[textbytes.Length + filebytes.Length];
			//Array.Clone(filebytes,0,buffer,0,fileLength);
			//Array.Clone(textbytes,0,buffer,fileLength,textbytes.Length);
			HashAlgorithm algorithm = MD5.Create();
			byte[] hash = algorithm.ComputeHash(textbytes);//always results in length of 16.
			return Encoding.ASCII.GetString(hash);
		}


	

	
	}

	

	


}




















