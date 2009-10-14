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
				+"WHERE PatNum="+POut.PLong(patNum)
				+" ORDER BY DateTP";
			DataTable table=Db.GetTable(command);
			TreatPlan[] List=new TreatPlan[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++) {
				List[i]=new TreatPlan();
				List[i].TreatPlanNum= PIn.PLong   (table.Rows[i][0].ToString());
				List[i].PatNum      = PIn.PLong   (table.Rows[i][1].ToString());
				List[i].DateTP      = PIn.PDate  (table.Rows[i][2].ToString());
				List[i].Heading     = PIn.PString(table.Rows[i][3].ToString());
				List[i].Note        = PIn.PString(table.Rows[i][4].ToString());
				List[i].Signature   = PIn.PString(table.Rows[i][5].ToString());
				List[i].SigIsTopaz  = PIn.PBool  (table.Rows[i][6].ToString());
				List[i].ResponsParty= PIn.PLong   (table.Rows[i][7].ToString());
			}
			return List;
		}

		///<summary></summary>
		public static void Update(TreatPlan tp){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),tp);
				return;
			}
			string command= "UPDATE treatplan SET "
				+"PatNum = '"     +POut.PLong   (tp.PatNum)+"'"
				+",DateTP = "     +POut.PDate  (tp.DateTP)
				+",Heading = '"   +POut.PString(tp.Heading)+"'"
				+",Note = '"      +POut.PString(tp.Note)+"'"
				+",Signature = '" +POut.PString(tp.Signature)+"'"
				+",SigIsTopaz = '"+POut.PBool  (tp.SigIsTopaz)+"'"
				+",ResponsParty='"+POut.PLong   (tp.ResponsParty)+"'"
				+" WHERE TreatPlanNum = '"+POut.PLong(tp.TreatPlanNum)+"'";
 			Db.NonQ(command);
		}

		///<summary></summary>
		public static long Insert(TreatPlan tp) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				tp.TreatPlanNum=Meth.GetLong(MethodBase.GetCurrentMethod(),tp);
				return tp.TreatPlanNum;
			}
			if(PrefC.RandomKeys){
				tp.TreatPlanNum=ReplicationServers.GetKey("treatplan","TreatPlanNum");
			}
			string command= "INSERT INTO treatplan (";
			if(PrefC.RandomKeys){
				command+="TreatPlanNum,";
			}
			command+="PatNum,DateTP,Heading,Note,Signature,SigIsTopaz,ResponsParty) VALUES(";
			if(PrefC.RandomKeys){
				command+="'"+POut.PLong(tp.TreatPlanNum)+"', ";
			}
			command+=
				 "'"+POut.PLong   (tp.PatNum)+"', "
				+POut.PDate  (tp.DateTP)+", "
				+"'"+POut.PString(tp.Heading)+"', "
				+"'"+POut.PString(tp.Note)+"', "
				+"'"+POut.PString(tp.Signature)+"', "
				+"'"+POut.PBool  (tp.SigIsTopaz)+"', "
				+"'"+POut.PLong   (tp.ResponsParty)+"')";
 			if(PrefC.RandomKeys){
				Db.NonQ(command);
			}
			else{
 				tp.TreatPlanNum=Db.NonQ(command,true);
			}
			return tp.TreatPlanNum;
		}

		///<summary>Dependencies checked first and throws an exception if any found. So surround by try catch</summary>
		public static void Delete(TreatPlan tp){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),tp);
				return;
			}
			//check proctp for dependencies
			string command="SELECT * FROM proctp WHERE TreatPlanNum ="+POut.PLong(tp.TreatPlanNum);
			DataTable table=Db.GetTable(command);
			if(table.Rows.Count>0){
				//this should never happen
				throw new InvalidProgramException(Lans.g("TreatPlans","Cannot delete treatment plan because it has ProcTP's attached"));
			}
			command= "DELETE from treatplan WHERE TreatPlanNum = '"+POut.PLong(tp.TreatPlanNum)+"'";
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
			//Array.Copy(filebytes,0,buffer,0,fileLength);
			//Array.Copy(textbytes,0,buffer,fileLength,textbytes.Length);
			HashAlgorithm algorithm = MD5.Create();
			byte[] hash = algorithm.ComputeHash(textbytes);//always results in length of 16.
			return Encoding.ASCII.GetString(hash);
		}


	

	
	}

	

	


}




















