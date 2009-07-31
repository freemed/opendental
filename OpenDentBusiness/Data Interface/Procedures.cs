using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace OpenDentBusiness {
	public class Procedures {
		///<summary></summary>
		public static int Insert(Procedure proc) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				proc.ProcNum=Meth.GetInt(MethodBase.GetCurrentMethod(),proc);
				return proc.ProcNum;
			}
			if(PrefC.RandomKeys) {
				proc.ProcNum=MiscData.GetKey("procedurelog","ProcNum");
			}
			string command= "INSERT INTO procedurelog (";
			if(PrefC.RandomKeys) {
				command+="ProcNum,";
			}
			command+="PatNum, AptNum, OldCode, ProcDate,ProcFee,Surf,"
				+"ToothNum,ToothRange,Priority,ProcStatus,ProvNum,"
				+"Dx,PlannedAptNum,PlaceService,Prosthesis,DateOriginalProsth,ClaimNote,"
				+"DateEntryC,ClinicNum,MedicalCode,DiagnosticCode,IsPrincDiag,ProcNumLab,"
				+"BillingTypeOne,BillingTypeTwo,CodeNum,CodeMod1,CodeMod2,CodeMod3,CodeMod4,RevCode,UnitCode,"
				+"UnitQty,BaseUnits,StartTime,StopTime,DateTP,SiteNum) VALUES(";
			if(PrefC.RandomKeys) {
				command+="'"+POut.PInt(proc.ProcNum)+"', ";
			}
			command+=
				 "'"+POut.PInt(proc.PatNum)+"', "
				+"'"+POut.PInt(proc.AptNum)+"', "
				+"'"+POut.PString(proc.OldCode)+"', "
				+POut.PDate(proc.ProcDate)+", "
				+"'"+POut.PDouble(proc.ProcFee)+"', "
				+"'"+POut.PString(proc.Surf)+"', "
				+"'"+POut.PString(proc.ToothNum)+"', "
				+"'"+POut.PString(proc.ToothRange)+"', "
				+"'"+POut.PInt(proc.Priority)+"', "
				+"'"+POut.PInt((int)proc.ProcStatus)+"', "
				+"'"+POut.PInt(proc.ProvNum)+"', "
				+"'"+POut.PInt(proc.Dx)+"', "
				+"'"+POut.PInt(proc.PlannedAptNum)+"', "
				+"'"+POut.PInt((int)proc.PlaceService)+"', "
				+"'"+POut.PString(proc.Prosthesis)+"', "
				+POut.PDate(proc.DateOriginalProsth)+", "
				+"'"+POut.PString(proc.ClaimNote)+"', ";
			if(DataConnection.DBtype==DatabaseType.Oracle) {
				command+=POut.PDateT(MiscData.GetNowDateTime());
			}
			else {//Assume MySQL
				command+="NOW()";
			}
			command+=", "//DateEntryC
				+"'"+POut.PInt(proc.ClinicNum)+"', "
				+"'"+POut.PString(proc.MedicalCode)+"', "
				+"'"+POut.PString(proc.DiagnosticCode)+"', "
				+"'"+POut.PBool(proc.IsPrincDiag)+"', "
				+"'"+POut.PInt(proc.ProcNumLab)+"', "
				+"'"+POut.PInt(proc.BillingTypeOne)+"', "
				+"'"+POut.PInt(proc.BillingTypeTwo)+"', "
				+"'"+POut.PInt(proc.CodeNum)+"', "
				+"'"+POut.PString(proc.CodeMod1)+"', "
				+"'"+POut.PString(proc.CodeMod2)+"', "
				+"'"+POut.PString(proc.CodeMod3)+"', "
				+"'"+POut.PString(proc.CodeMod4)+"', "
				+"'"+POut.PString(proc.RevCode)+"', "
				+"'"+POut.PString(proc.UnitCode)+"', "
				+"'"+POut.PInt(proc.UnitQty)+"', "
				+"'"+POut.PInt(proc.BaseUnits)+"', "
				+"'"+POut.PInt(proc.StartTime)+"', "
				+"'"+POut.PInt(proc.StopTime)+"', "
				+POut.PDate(proc.DateTP)+", "
				+"'"+POut.PInt(proc.SiteNum)+"')";
			//MessageBox.Show(cmd.CommandText);
			if(PrefC.RandomKeys) {
				Db.NonQ(command);
			}
			else {
				proc.ProcNum=Db.NonQ(command,true);
			}
			if(proc.Note!="") {
				ProcNote note=new ProcNote();
				note.PatNum=proc.PatNum;
				note.ProcNum=proc.ProcNum;
				note.UserNum=proc.UserNum;
				note.Note=proc.Note;
				ProcNotes.Insert(note);
			}
			return proc.ProcNum;
		}

		///<summary>Updates only the changed columns.</summary>
		public static int Update(Procedure proc,Procedure oldProc) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetInt(MethodBase.GetCurrentMethod(),proc,oldProc);
			}
			bool comma=false;
			string c = "UPDATE procedurelog SET ";
			if(proc.PatNum!=oldProc.PatNum) {
				c+="PatNum = '"     +POut.PInt(proc.PatNum)+"'";
				comma=true;
			}
			if(proc.AptNum!=oldProc.AptNum) {
				if(comma) c+=",";
				c+="AptNum = '"		+POut.PInt(proc.AptNum)+"'";
				comma=true;
			}
			if(proc.OldCode!=oldProc.OldCode) {
				if(comma) c+=",";
				c+="OldCode = '"		+POut.PString(proc.OldCode)+"'";
				comma=true;
			}
			if(proc.ProcDate!=oldProc.ProcDate) {
				if(comma) c+=",";
				c+="ProcDate = "	+POut.PDate(proc.ProcDate);
				comma=true;
			}
			if(proc.ProcFee!=oldProc.ProcFee) {
				if(comma) c+=",";
				c+="ProcFee = '"		+POut.PDouble(proc.ProcFee)+"'";
				comma=true;
			}
			if(proc.Surf!=oldProc.Surf) {
				if(comma) c+=",";
				c+="Surf = '"			+POut.PString(proc.Surf)+"'";
				comma=true;
			}
			if(proc.ToothNum!=oldProc.ToothNum) {
				if(comma) c+=",";
				c+="ToothNum = '"	+POut.PString(proc.ToothNum)+"'";
				comma=true;
			}
			if(proc.ToothRange!=oldProc.ToothRange) {
				if(comma) c+=",";
				c+="ToothRange = '"+POut.PString(proc.ToothRange)+"'";
				comma=true;
			}
			if(proc.Priority!=oldProc.Priority) {
				if(comma) c+=",";
				c+="Priority = '"	+POut.PInt(proc.Priority)+"'";
				comma=true;
			}
			if(proc.ProcStatus!=oldProc.ProcStatus) {
				if(comma) c+=",";
				c+="ProcStatus = '"+POut.PInt((int)proc.ProcStatus)+"'";
				comma=true;
			}
			if(proc.ProvNum!=oldProc.ProvNum) {
				if(comma) c+=",";
				c+="ProvNum = '"		+POut.PInt(proc.ProvNum)+"'";
				comma=true;
			}
			if(proc.Dx!=oldProc.Dx) {
				if(comma) c+=",";
				c+="Dx = '"				+POut.PInt(proc.Dx)+"'";
				comma=true;
			}
			if(proc.PlannedAptNum!=oldProc.PlannedAptNum) {
				if(comma) c+=",";
				c+="PlannedAptNum = '"+POut.PInt(proc.PlannedAptNum)+"'";
				comma=true;
			}
			if(proc.PlaceService!=oldProc.PlaceService) {
				if(comma) c+=",";
				c+="PlaceService = '"	+POut.PInt((int)proc.PlaceService)+"'";
				comma=true;
			}
			if(proc.Prosthesis!=oldProc.Prosthesis) {
				if(comma) c+=",";
				c+="Prosthesis = '"+POut.PString(proc.Prosthesis)+"'";
				comma=true;
			}
			if(proc.DateOriginalProsth!=oldProc.DateOriginalProsth) {
				if(comma) c+=",";
				c+="DateOriginalProsth = "+POut.PDate(proc.DateOriginalProsth);
				comma=true;
			}
			if(proc.ClaimNote!=oldProc.ClaimNote) {
				if(comma) c+=",";
				c+="ClaimNote = '"+POut.PString(proc.ClaimNote)+"'";
				comma=true;
			}
			if(proc.DateEntryC!=oldProc.DateEntryC) {
				if(comma) c+=",";
				c+="DateEntryC = ";
				if(DataConnection.DBtype==DatabaseType.Oracle) {
					c+=POut.PDateT(MiscData.GetNowDateTime());
				}
				else {//Assume MySQL
					c+="NOW()";
				}
				comma=true;
			}
			if(proc.ClinicNum!=oldProc.ClinicNum) {
				if(comma) c+=",";
				c+="ClinicNum = '"+POut.PInt(proc.ClinicNum)+"'";
				comma=true;
			}
			if(proc.MedicalCode!=oldProc.MedicalCode) {
				if(comma) c+=",";
				c+="MedicalCode = '"+POut.PString(proc.MedicalCode)+"'";
				comma=true;
			}
			if(proc.DiagnosticCode!=oldProc.DiagnosticCode) {
				if(comma) c+=",";
				c+="DiagnosticCode = '"+POut.PString(proc.DiagnosticCode)+"'";
				comma=true;
			}
			if(proc.IsPrincDiag!=oldProc.IsPrincDiag) {
				if(comma) c+=",";
				c+="IsPrincDiag = '"+POut.PBool(proc.IsPrincDiag)+"'";
				comma=true;
			}
			if(proc.ProcNumLab!=oldProc.ProcNumLab) {
				if(comma) c+=",";
				c+="ProcNumLab = '"+POut.PInt(proc.ProcNumLab)+"'";
				comma=true;
			}
			if(proc.BillingTypeOne!=oldProc.BillingTypeOne) {
				if(comma)
					c+=",";
				c+="BillingTypeOne = '"+POut.PInt(proc.BillingTypeOne)+"'";
				comma=true;
			}
			if(proc.BillingTypeTwo!=oldProc.BillingTypeTwo) {
				if(comma)
					c+=",";
				c+="BillingTypeTwo = '"+POut.PInt(proc.BillingTypeTwo)+"'";
				comma=true;
			}
			if(proc.CodeNum!=oldProc.CodeNum) {
				if(comma)
					c+=",";
				c+="CodeNum = '"+POut.PInt(proc.CodeNum)+"'";
				comma=true;
			}
			if(proc.CodeMod1!=oldProc.CodeMod1) {
				if(comma) c+= ",";
				c+="CodeMod1 = '"+POut.PString(proc.CodeMod1)+"'";
				comma=true;
			}
			if(proc.CodeMod2!=oldProc.CodeMod2) {
				if(comma) c+= ",";
				c+="CodeMod2 = '"+POut.PString(proc.CodeMod2)+"'";
				comma=true;
			}
			if(proc.CodeMod3!=oldProc.CodeMod3) {
				if(comma) c+= ",";
				c+="CodeMod3 = '"+POut.PString(proc.CodeMod3)+"'";
				comma=true;
			}
			if(proc.CodeMod4!=oldProc.CodeMod4) {
				if(comma) c+= ",";
				c+="CodeMod4 = '"+POut.PString(proc.CodeMod4)+"'";
				comma=true;
			}
			if(proc.RevCode!=oldProc.RevCode) {
				if(comma) c+=",";
				c+="RevCode = '"+POut.PString(proc.RevCode)+"'";
				comma=true;
			}
			if(proc.UnitCode!=oldProc.UnitCode) {
				if(comma) c+=",";
				c+="UnitCode = '"+POut.PString(proc.UnitCode)+"'";
				comma=true;
			}
			if(proc.UnitQty!=oldProc.UnitQty) {
				if(comma) c+=",";
				c+="UnitQty = '"+POut.PInt(proc.UnitQty)+"'";
				comma=true;
			}
			if(proc.BaseUnits!=oldProc.BaseUnits) {
				if(comma) c+=",";
				c+="BaseUnits = '"+POut.PInt(proc.BaseUnits)+"'";
				comma=true;
			}
			if(proc.StartTime != oldProc.StartTime) {
				if(comma) c += ",";
				c += "StartTime = '" + POut.PInt(proc.StartTime) + "'";
				comma = true;
			}
			if(proc.StopTime != oldProc.StopTime) {
				if(comma) c += ",";
				c += "StopTime = '" + POut.PInt(proc.StopTime) + "'";
				comma = true;
			}
			if(proc.DateTP != oldProc.DateTP) {
				if(comma) c += ",";
				c += "DateTP = " + POut.PDate(proc.DateTP);
				comma = true;
			}
			if(proc.SiteNum != oldProc.SiteNum) {
				if(comma) c += ",";
				c += "SiteNum = '" + POut.PInt(proc.SiteNum)+"'";
				comma = true;
			}
			int rowsChanged=0;
			if(comma) {
				c+=" WHERE ProcNum = '"+POut.PInt(proc.ProcNum)+"'";
				//DataConnection dcon=new DataConnection();
				rowsChanged=Db.NonQ(c);
			}
			else {
				//rowsChanged=0;//this means no change is actually required.
			}
			if(proc.Note!=oldProc.Note
				|| proc.UserNum!=oldProc.UserNum
				|| proc.SigIsTopaz!=oldProc.SigIsTopaz
				|| proc.Signature!=oldProc.Signature) {
				ProcNote note=new ProcNote();
				note.PatNum=proc.PatNum;
				note.ProcNum=proc.ProcNum;
				note.UserNum=proc.UserNum;
				note.Note=proc.Note;
				note.SigIsTopaz=proc.SigIsTopaz;
				note.Signature=proc.Signature;
				ProcNotes.Insert(note);
			}
			return rowsChanged;
			/*
			try{
				if(RemotingClient.OpenDentBusinessIsLocal) {
					ProcedureB.Update(proc,procOld);
				}
				else {
					DtoProcedureUpdate dto=new DtoProcedureUpdate();
					dto.Proc=proc;
					dto.OldProc=procOld;
					RemotingClient.ProcessCommand(dto);
				}
			}
			catch(Exception e) {
				MessageBox.Show(e.Message);
			}*/
		}

		///<summary>Also deletes any claimProcs. Must test to make sure claimProcs are not part of a payment first.  This does not actually delete the procedure, but just changes the status to deleted.  If not allowed to delete, then it throws an exception.</summary>
		public static void Delete(int procNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),procNum);
				return;
			}
			//Test to see if any payment at all has been received for this proc
			string command="SELECT COUNT(*) FROM claimproc WHERE ProcNum="+POut.PInt(procNum)
				+" AND InsPayAmt > 0 AND Status != "+POut.PInt((int)ClaimProcStatus.Preauth);
			if(Db.GetCount(command)!="0") {
				throw new Exception(Lans.g("Procedures","Not allowed to delete a procedure that is attached to a payment."));
			}
			//delete adjustments
			command="DELETE FROM adjustment WHERE ProcNum='"+POut.PInt(procNum)+"'";
			Db.NonQ(command);
			//delete claimprocs
			command="DELETE from claimproc WHERE ProcNum = '"+POut.PInt(procNum)+"'";
			Db.NonQ(command);
			//resynch appointment description-------------------------------------------------------------------------------------
			command="SELECT AptNum,PlannedAptNum FROM procedurelog WHERE ProcNum = "+POut.PInt(procNum);
			DataTable table=Db.GetTable(command);
			string aptnum=table.Rows[0][0].ToString();
			string plannedaptnum=table.Rows[0][1].ToString();
			string procdescript;
			if(aptnum!="0") {
				command=@"SELECT AbbrDesc FROM procedurecode,procedurelog
					WHERE procedurecode.CodeNum=procedurelog.CodeNum
					AND ProcNum != "+POut.PInt(procNum)
					+" AND procedurelog.AptNum="+aptnum;
				table=Db.GetTable(command);
				procdescript="";
				for(int i=0;i<table.Rows.Count;i++) {
					if(i>0) procdescript+=", ";
					procdescript+=table.Rows[i]["AbbrDesc"].ToString();
				}
				command="UPDATE appointment SET ProcDescript='"+POut.PString(procdescript)+"' "
					+"WHERE AptNum="+aptnum;
				Db.NonQ(command);
			}
			if(plannedaptnum!="0") {
				command=@"SELECT AbbrDesc FROM procedurecode,procedurelog
					WHERE procedurecode.CodeNum=procedurelog.CodeNum
					AND ProcNum != "+POut.PInt(procNum)
					+" AND procedurelog.PlannedAptNum="+plannedaptnum;
				table=Db.GetTable(command);
				procdescript="";
				for(int i=0;i<table.Rows.Count;i++) {
					if(i>0) procdescript+=", ";
					procdescript+=table.Rows[i]["AbbrDesc"].ToString();
				}
				command="UPDATE appointment SET ProcDescript='"+POut.PString(procdescript)+"' "
					+"WHERE NextAptNum="+plannedaptnum;
				Db.NonQ(command);
			}
			//set the procedure deleted-----------------------------------------------------------------------------------------
			command="UPDATE procedurelog SET ProcStatus = "+POut.PInt((int)ProcStat.D)+", "
				+"AptNum=0, "
				+"PlannedAptNum=0 "
				+"WHERE ProcNum = '"+POut.PInt(procNum)+"'";
			Db.NonQ(command);
		}

		public static void UpdateAptNum(int procNum,int newAptNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),procNum,newAptNum);
				return;
			}
			string command="UPDATE procedurelog SET AptNum = "+POut.PInt(newAptNum)
				+" WHERE ProcNum = "+POut.PInt(procNum);
			Db.NonQ(command);
		}

		public static void UpdatePlannedAptNum(int procNum,int newPlannedAptNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),procNum,newPlannedAptNum);
				return;
			}
			string command="UPDATE procedurelog SET PlannedAptNum = "+POut.PInt(newPlannedAptNum)
				+" WHERE ProcNum = "+POut.PInt(procNum);
			Db.NonQ(command);
		}

		public static void UpdatePriority(int procNum,int newPriority) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),procNum,newPriority);
				return;
			}
			string command="UPDATE procedurelog SET Priority = "+POut.PInt(newPriority)
				+" WHERE ProcNum = "+POut.PInt(procNum);
			Db.NonQ(command);
		}

		public static void UpdateFee(int procNum,double newFee) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),procNum,newFee);
				return;
			}
			string command="UPDATE procedurelog SET ProcFee = "+POut.PDouble(newFee)
				+" WHERE ProcNum = "+POut.PInt(procNum);
			Db.NonQ(command);
		}

		///<summary>Gets all procedures for a single patient, without notes.  Does not include deleted procedures.</summary>
		public static List<Procedure> Refresh(int patNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<Procedure>>(MethodBase.GetCurrentMethod(),patNum);
			}
			string command="SELECT * FROM procedurelog WHERE PatNum="+POut.PInt(patNum)
				+" AND ProcStatus !=6"//don't include deleted
				+" ORDER BY ProcDate";
			DataTable table=Db.GetTable(command);
			List<Procedure> procList=ConvertToList(table);
			return procList;
		}

		///<summary>Gets one procedure directly from the db.  Option to include the note.</summary>
		public static Procedure GetOneProc(int procNum,bool includeNote) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<Procedure>(MethodBase.GetCurrentMethod(),procNum,includeNote);
			}
			string command=
				"SELECT * FROM procedurelog "
				+"WHERE ProcNum="+procNum.ToString();
			DataTable table=Db.GetTable(command);
			List<Procedure> procList=ConvertToList(table);
			if(procList.Count==0) {
				//MessageBox.Show(Lans.g("Procedures","Error. Procedure not found")+": "+procNum.ToString());
				return new Procedure();
			}
			Procedure proc=procList[0];
			if(!includeNote) {
				return proc;
			}
			command="SELECT * FROM procnote WHERE ProcNum="+POut.PInt(procNum)+" ORDER BY EntryDateTime DESC ";
			if(DataConnection.DBtype==DatabaseType.Oracle) {
				command="SELECT * FROM ("+command+") WHERE ROWNUM<=1";
			}
			else {//Assume MySQL
				command+="LIMIT 1";
			}
			table=Db.GetTable(command);
			if(table.Rows.Count==0) {
				return proc;
			}
			proc.UserNum   =PIn.PInt(table.Rows[0]["UserNum"].ToString());
			proc.Note      =PIn.PString(table.Rows[0]["Note"].ToString());
			proc.SigIsTopaz=PIn.PBool(table.Rows[0]["SigIsTopaz"].ToString());
			proc.Signature =PIn.PString(table.Rows[0]["Signature"].ToString());
			return proc;
		}

		/*
		///<summary>Gets many procedure directly from the db all at once.  Never include the notes.</summary>
		public static List<Procedure> GetManyProcs(List<int> procNums){
			string command="SELECT * FROM procedurelog WHERE ";
			for(int i=0;i<procNums.Count;i++){
				if(i>0){
					command+="OR ";
				}
				command+="ProcNum="+POut.PInt(procNums[i])+" ";
			}
			return RefreshAndFill(command);
		}*/

		/*
		///<summary>Gets all procedures from the database for the given plan/patient combo.  Used once when creating claims.</summary>
		public static Procedure[] GetProcsForPlan(int planNum,int patNum){
			string command="SELECT * FROM procedurelog WHERE "
				+"PatNum="+POut.PInt(patNum)+" "
				+"AND PlanNum="+POut.PInt(patNum)+" "
			return RefreshAndFill(command).ToArray();
		}*/

		/*
		private static List<Procedure> RefreshAndFill(string command) {
			DataTable table=Db.GetTable(command);
			return ConvertToList(table);
		}*/

		private static List<Procedure> ConvertToList(DataTable table) {
			//No need to check RemotingRole; no call to db.
			List<Procedure> retVal=new List<Procedure>();
			Procedure proc;
			for(int i=0;i<table.Rows.Count;i++) {
				proc=new Procedure();
				proc.ProcNum				= PIn.PInt(table.Rows[i][0].ToString());
				proc.PatNum					= PIn.PInt(table.Rows[i][1].ToString());
				proc.AptNum					= PIn.PInt(table.Rows[i][2].ToString());
				proc.OldCode				= PIn.PString(table.Rows[i][3].ToString());
				proc.ProcDate				= PIn.PDate(table.Rows[i][4].ToString());
				proc.ProcFee				= PIn.PDouble(table.Rows[i][5].ToString());
				proc.Surf						= PIn.PString(table.Rows[i][6].ToString());
				proc.ToothNum				= PIn.PString(table.Rows[i][7].ToString());
				proc.ToothRange			= PIn.PString(table.Rows[i][8].ToString());
				proc.Priority				= PIn.PInt(table.Rows[i][9].ToString());
				proc.ProcStatus			= (ProcStat)PIn.PInt(table.Rows[i][10].ToString());
				proc.ProvNum				= PIn.PInt(table.Rows[i][11].ToString());
				proc.Dx							= PIn.PInt(table.Rows[i][12].ToString());
				proc.PlannedAptNum	= PIn.PInt(table.Rows[i][13].ToString());
				proc.PlaceService		= (PlaceOfService)PIn.PInt(table.Rows[i][14].ToString());
				proc.Prosthesis		  = PIn.PString(table.Rows[i][15].ToString());
				proc.DateOriginalProsth= PIn.PDate(table.Rows[i][16].ToString());
				proc.ClaimNote		    = PIn.PString(table.Rows[i][17].ToString());
				proc.DateEntryC      = PIn.PDate(table.Rows[i][18].ToString());
				proc.ClinicNum       = PIn.PInt(table.Rows[i][19].ToString());
				proc.MedicalCode     = PIn.PString(table.Rows[i][20].ToString());
				proc.DiagnosticCode  = PIn.PString(table.Rows[i][21].ToString());
				proc.IsPrincDiag     = PIn.PBool(table.Rows[i][22].ToString());
				proc.ProcNumLab      = PIn.PInt(table.Rows[i][23].ToString());
				proc.BillingTypeOne  = PIn.PInt(table.Rows[i][24].ToString());
				proc.BillingTypeTwo  = PIn.PInt(table.Rows[i][25].ToString());
				proc.CodeNum         = PIn.PInt(table.Rows[i][26].ToString());
				proc.CodeMod1        = PIn.PString(table.Rows[i][27].ToString());
				proc.CodeMod2        = PIn.PString(table.Rows[i][28].ToString());
				proc.CodeMod3        = PIn.PString(table.Rows[i][29].ToString());
				proc.CodeMod4        = PIn.PString(table.Rows[i][30].ToString());
				proc.RevCode         = PIn.PString(table.Rows[i][31].ToString());
				proc.UnitCode        = PIn.PString(table.Rows[i][32].ToString());
				proc.UnitQty         = PIn.PInt(table.Rows[i][33].ToString());
				proc.BaseUnits       = PIn.PInt(table.Rows[i][34].ToString());
				proc.StartTime       = PIn.PInt(table.Rows[i][35].ToString());
				proc.StopTime        = PIn.PInt(table.Rows[i][36].ToString());
				proc.DateTP          = PIn.PDate(table.Rows[i][37].ToString());
				proc.SiteNum         = PIn.PInt(table.Rows[i][38].ToString());
				//only used sometimes:
				/*if(table.Columns.Count>24){
					List[i].UserNum       = PIn.PInt   (table.Rows[i][24].ToString());
					List[i].Note          = PIn.PString(table.Rows[i][25].ToString());
					List[i].SigIsTopaz    = PIn.PBool  (table.Rows[i][26].ToString());
					List[i].Signature     = PIn.PString(table.Rows[i][27].ToString());
				}*/
				retVal.Add(proc);
			}
			return retVal;
		}

		///<summary>Gets Procedures for a single appointment directly from the database</summary>
		public static List<Procedure> GetProcsForSingle(int aptNum,bool isPlanned) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<Procedure>>(MethodBase.GetCurrentMethod(),aptNum,isPlanned);
			}
			string command;
			if(isPlanned) {
				command = "SELECT * from procedurelog WHERE PlannedAptNum = '"+POut.PInt(aptNum)+"'";
			}
			else {
				command = "SELECT * from procedurelog WHERE AptNum = '"+POut.PInt(aptNum)+"'";
			}
			DataTable table=Db.GetTable(command);
			return ConvertToList(table);
		}

		///<summary>Gets a list (procsMultApts is a struct of type ProcDesc(aptNum, string[], and production) of all the procedures attached to the specified appointments.  Then, use GetProcsOneApt to pull procedures for one appointment from this list.  This process requires only one call to the database. "myAptNums" is the list of appointments to get procedures for.</summary>
		public static List<Procedure> GetProcsMultApts(List <int> myAptNums) {
			//No need to check RemotingRole; no call to db.
			return GetProcsMultApts(myAptNums,false);
		}

		///<summary>Gets a list (procsMultApts is a struct of type ProcDesc(aptNum, string[], and production) of all the procedures attached to the specified appointments.  Then, use GetProcsOneApt to pull procedures for one appointment from this list or GetProductionOneApt.  This process requires only one call to the database.  "myAptNums" is the list of appointments to get procedures for.  isForNext gets procedures for a list of next appointments rather than regular appointments.</summary>
		public static List<Procedure> GetProcsMultApts(List<int> myAptNums,bool isForPlanned) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<Procedure>>(MethodBase.GetCurrentMethod(),myAptNums,isForPlanned);
			}
			if(myAptNums.Count==0) {
				return new List<Procedure>();
			}
			string strAptNums="";
			for(int i=0;i<myAptNums.Count;i++) {
				if(i>0) {
					strAptNums+=" OR";
				}
				if(isForPlanned) {
					strAptNums+=" PlannedAptNum='"+POut.PInt(myAptNums[i])+"'";
				}
				else {
					strAptNums+=" AptNum='"+POut.PInt(myAptNums[i])+"'";
				}
			}
			string command = "SELECT * FROM procedurelog WHERE"+strAptNums;
			DataTable table=Db.GetTable(command);
			return ConvertToList(table);
		}

		///<summary>Gets procedures for one appointment by looping through the procsMultApts which was filled previously from GetProcsMultApts.</summary>
		public static Procedure[] GetProcsOneApt(int myAptNum,List<Procedure> procsMultApts) {
			//No need to check RemotingRole; no call to db.
			ArrayList AL=new ArrayList();
			for(int i=0;i<procsMultApts.Count;i++) {
				if(procsMultApts[i].AptNum==myAptNum) {
					AL.Add(procsMultApts[i].Copy());
				}
			}
			Procedure[] retVal=new Procedure[AL.Count];
			AL.CopyTo(retVal);
			return retVal;
		}

		///<summary>Gets the production for one appointment by looping through the procsMultApts which was filled previously from GetProcsMultApts.</summary>
		public static double GetProductionOneApt(int myAptNum,Procedure[] procsMultApts,bool isPlanned) {
			//No need to check RemotingRole; no call to db.
			double retVal=0;
			for(int i=0;i<procsMultApts.Length;i++) {
				if(isPlanned && procsMultApts[i].PlannedAptNum==myAptNum) {
					retVal+=procsMultApts[i].ProcFee;
				}
				if(!isPlanned && procsMultApts[i].AptNum==myAptNum) {
					retVal+=procsMultApts[i].ProcFee;
				}
			}
			return retVal;
		}

		///<summary>Used in FormClaimEdit,FormClaimPrint,FormClaimPayTotal,ContrAccount etc to get description of procedure. Procedure list needs to include the procedure we are looking for.</summary>
		public static Procedure GetProcFromList(List<Procedure> list,int procNum) {
			//No need to check RemotingRole; no call to db.
			for(int i=0;i<list.Count;i++) {
				if(procNum==list[i].ProcNum) {
					return list[i];
				}
			}
			//MessageBox.Show("Error. Procedure not found");
			return new Procedure();
		}

		/*
		///<summary>Does not make any calls to db.</summary>
		public static double ComputeBal(Procedure[] List){
			double retVal=0;
			double procFee=0;
			double qty=0;
			for(int i=0;i<List.Length;i++) {
				if(List[i].ProcStatus==ProcStat.C) {//complete
					procFee=List[i].ProcFee;
					qty=List[i].UnitQty+List[i].BaseUnits;
					if(qty > 0) {
						procFee*=qty;
					}
					retVal+=procFee;//List[i].ProcFee;
				}
			}
			return retVal;
		}*/

		///<summary>Sets the patient.DateFirstVisit if necessary. A visitDate is required to be passed in because it may not be today's date. This is triggered by:
		///1. When any procedure is inserted regardless of status. From Chart or appointment. If no C procs and date blank, changes date.
		///2. When updating a procedure to status C. If no C procs, update visit date. Ask user first?
		///  #2 was recently changed to only happen if date is blank or less than 7 days old.
		///3. When an appointment is deleted. If no C procs, clear visit date.
		///  #3 was recently changed to not occur at all unless appt is of type IsNewPatient
		///4. Changing an appt date of type IsNewPatient. If no C procs, change visit date.
		///Old: when setting a procedure complete in the Chart module or the ProcEdit window.  Also when saving an appointment that is marked IsNewPat.</summary>
		public static void SetDateFirstVisit(DateTime visitDate,int situation,Patient pat) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),visitDate,situation,pat);
				return;
			}
			if(situation==1) {
				if(pat.DateFirstVisit.Year>1880) {
					return;//a date has already been set.
				}
			}
			if(situation==2) {
				if(pat.DateFirstVisit.Year>1880 && pat.DateFirstVisit<DateTime.Now.AddDays(-7)) {
					return;//a date has already been set.
				}
			}
			string command="SELECT Count(*) from procedurelog WHERE "
				+"PatNum = '"+POut.PInt(pat.PatNum)+"' "
				+"AND ProcStatus = '2'";
			DataTable table=Db.GetTable(command);
			if(PIn.PInt(table.Rows[0][0].ToString())>0) {
				return;//there are already completed procs (for all situations)
			}
			if(situation==2) {
				//ask user first?
			}
			if(situation==3) {
				command="UPDATE patient SET DateFirstVisit ='0001-01-01'"
					+" WHERE PatNum ='"
					+POut.PInt(pat.PatNum)+"'";
			}
			else {
				command="UPDATE patient SET DateFirstVisit ="
					+POut.PDate(visitDate)+" WHERE PatNum ='"
					+POut.PInt(pat.PatNum)+"'";
			}
			//MessageBox.Show(cmd.CommandText);
			//dcon.NonQ(command);
			Db.NonQ(command);
		}

		///<summary>Called from FormApptsOther when creating a new appointment.  Returns true if there are any procedures marked complete for this patient.  The result is that the NewPt box on the appointment won't be checked.</summary>
		public static bool AreAnyComplete(int patNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetBool(MethodBase.GetCurrentMethod(),patNum);
			}
			string command="SELECT COUNT(*) FROM procedurelog "
				+"WHERE PatNum="+patNum.ToString()
				+" AND ProcStatus=2";
			DataTable table=Db.GetTable(command);
			if(table.Rows[0][0].ToString()=="0") {
				return false;
			}
			else return true;
		}

		///<summary>Called from AutoCodeItems.  Makes a call to the database to determine whether the specified tooth has been extracted or will be extracted. This could then trigger a pontic code.</summary>
		public static bool WillBeMissing(string toothNum,int patNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetBool(MethodBase.GetCurrentMethod(),toothNum,patNum);
			}
			//first, check for missing teeth
			string command="SELECT COUNT(*) FROM toothinitial "
				+"WHERE ToothNum='"+toothNum+"' "
				+"AND PatNum="+POut.PInt(patNum)
				+" AND InitialType=0";//missing
			DataTable table=Db.GetTable(command);
			if(table.Rows[0][0].ToString()!="0") {
				return true;
			}
			//then, check for a planned extraction
			command="SELECT COUNT(*) FROM procedurelog,procedurecode "
				+"WHERE procedurelog.CodeNum=procedurecode.CodeNum "
				+"AND procedurelog.ToothNum='"+toothNum+"' "
				+"AND procedurelog.PatNum="+patNum.ToString()
				+" AND procedurecode.PaintType=1";//extraction
			table=Db.GetTable(command);
			if(table.Rows[0][0].ToString()!="0") {
				return true;
			}
			return false;
		}

		public static void AttachToApt(int procNum,int aptNum,bool isPlanned) {
			//No need to check RemotingRole; no call to db.
			List<int> procNums=new List<int>();
			procNums.Add(procNum);
			AttachToApt(procNums,aptNum,isPlanned);
		}

		public static void AttachToApt(List<int> procNums,int aptNum,bool isPlanned) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),procNums,aptNum,isPlanned);
				return;
			}
			if(procNums.Count==0) {
				return;
			}
			string command="UPDATE procedurelog SET ";
			if(isPlanned) {
				command+="PlannedAptNum";
			}
			else {
				command+="AptNum";
			}
			command+="="+POut.PInt(aptNum)+" WHERE ";
			for(int i=0;i<procNums.Count;i++) {
				if(i>0) {
					command+=" OR ";
				}
				command+="ProcNum="+POut.PInt(procNums[i]);
			}
			Db.NonQ(command);
		}

		public static void DetachFromApt(List<int> procNums,bool isPlanned) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),procNums,isPlanned);
				return;
			}
			if(procNums.Count==0) {
				return;
			}
			string command="UPDATE procedurelog SET ";
			if(isPlanned) {
				command+="PlannedAptNum";
			}
			else {
				command+="AptNum";
			}
			command+="=0 WHERE ";
			for(int i=0;i<procNums.Count;i++) {
				if(i>0) {
					command+=" OR ";
				}
				command+="ProcNum="+POut.PInt(procNums[i]);
			}
			Db.NonQ(command);
		}


		//--------------------Taken from Procedure class--------------------------------------------------


		/*
		///<summary>Gets allowedOverride for this procedure based on supplied claimprocs. Includes all claimproc types.  Only used in main TP module when calculating PPOs. The claimProc array typically includes all claimProcs for the patient, but must at least include all claimprocs for this proc.</summary>
		public static double GetAllowedOverride(Procedure proc,ClaimProc[] claimProcs,int priPlanNum) {
			//double retVal=0;
			for(int i=0;i<claimProcs.Length;i++) {
				if(claimProcs[i].ProcNum==proc.ProcNum && claimProcs[i].PlanNum==priPlanNum) {
					return claimProcs[i].AllowedOverride;
					//retVal+=claimProcs[i].WriteOff;
				}
			}
			return 0;//retVal;
		}*/

		///<summary>Gets total writeoff for this procedure based on supplied claimprocs. Includes all claimproc types.  Only used in main TP module. The claimProc array typically includes all claimProcs for the patient, but must at least include all claimprocs for this proc.</summary>
		public static double GetWriteOff(Procedure proc,List<ClaimProc> claimProcs) {
			//No need to check RemotingRole; no call to db.
			double retVal=0;
			for(int i=0;i<claimProcs.Count;i++) {
				if(claimProcs[i].ProcNum==proc.ProcNum) {
					retVal+=claimProcs[i].WriteOff;
				}
			}
			return retVal;
		}

		///<summary>WriteOff'Complete'. Only used in main Account module. Gets writeoff for this procedure based on supplied claimprocs. Only includes claimprocs with status of CapComplete,CapClaim,NotReceived,Received,or Supplemental. Used to ONLY include Writeoffs not attached to claims, because those would display on the claim line, but now they show on each procedure instead.  /*In practice, this means only writeoffs with CapComplete status get returned because they are to be subtracted from the patient portion on the proc line*/. The claimProc array typically includes all claimProcs for the patient, but must at least include all claimprocs for this proc.</summary>
		public static double GetWriteOffC(Procedure proc,ClaimProc[] claimProcs) {
			//No need to check RemotingRole; no call to db.
			double retVal=0;
			for(int i=0;i<claimProcs.Length;i++) {
				if(claimProcs[i].ProcNum!=proc.ProcNum) {
					continue;
				}
				//if(claimProcs[i].ClaimNum>0) {
				//	continue;
				//}
				if(
					//adj skipped
					claimProcs[i].Status==ClaimProcStatus.CapClaim
					|| claimProcs[i].Status==ClaimProcStatus.CapComplete
					//capEstimate would never happen because procedure is C.
					//estimate means not attached to claim, so don't count
					//|| claimProcs[i].Status==ClaimProcStatus.NotReceived//see below
					//preAuth -no
					|| claimProcs[i].Status==ClaimProcStatus.Received
					|| claimProcs[i].Status==ClaimProcStatus.Supplemental
					) {
					retVal+=claimProcs[i].WriteOff;
				}
				if(!PrefC.GetBool("BalancesDontSubtractIns")//this is the typical situation
					&& claimProcs[i].Status==ClaimProcStatus.NotReceived) {
					//so, if user IS using "balances don't subtract ins", and a proc as been sent but not received,
					//then we do not subtract the writeoff because it's considered part of the estimate.
					retVal+=claimProcs[i].WriteOff;
				}
			}
			return retVal;
		}

		///<summary>Used in deciding how to display procedures in Account. The claimProcList can be all claimProcs for the patient or only those attached to this proc. Will be true if any claimProcs at all are attached to this procedure.</summary>
		public static bool IsCoveredIns(Procedure proc,ClaimProc[] List) {
			//No need to check RemotingRole; no call to db.
			for(int i=0;i<List.Length;i++) {
				if(List[i].ProcNum==proc.ProcNum) {
					return true;
				}
			}
			return false;
		}

		///<summary>Used in deciding how to display procedures in Account. The claimProcList can be all claimProcs for the patient or only those attached to this proc. Will be true if any claimProcs attached to this procedure are set NoBillIns.</summary>
		public static bool NoBillIns(Procedure proc,List<ClaimProc> claimProcList) {
			//No need to check RemotingRole; no call to db.
			for(int i=0;i<claimProcList.Count;i++) {
				if(claimProcList[i].ProcNum==proc.ProcNum
					&& claimProcList[i].NoBillIns) {
					return true;
				}
			}
			return false;
		}

		///<summary>Used in ContrAccount.CreateClaim when validating selected procedures. Returns true if there is any claimproc for this procedure and plan which is marked NoBillIns.  The claimProcList can be all claimProcs for the patient or only those attached to this proc. Will be true if any claimProcs attached to this procedure are set NoBillIns.</summary>
		public static bool NoBillIns(Procedure proc,List<ClaimProc> claimProcList,int planNum) {
			//No need to check RemotingRole; no call to db.
			if(proc==null) {
				return false;
			}
			for(int i=0;i<claimProcList.Count;i++) {
				if(claimProcList[i].ProcNum==proc.ProcNum
					&& claimProcList[i].PlanNum==planNum
					&& claimProcList[i].NoBillIns) {
					return true;
				}
			}
			return false;
		}

		///<summary>Used in deciding how to display procedures in Account. The claimProcList can be all claimProcs for the patient or only those attached to this proc. Will be true if any claimProcs attached to this procedure are status estimate, which means they haven't been attached to a claim because their status would have been changed to NotReceived.  And if the patient doesn't have ins, then the estimates would have been deleted.</summary>
		public static bool IsUnsent(Procedure proc,ClaimProc[] List) {
			//No need to check RemotingRole; no call to db.
			//unsent if no claimprocs with claimNums
			for(int i=0;i<List.Length;i++) {
				if(List[i].ProcNum==proc.ProcNum
					&& List[i].Status==ClaimProcStatus.Estimate
					//&& List[i].ClaimNum>0
					//&& List[i].Status!=ClaimProcStatus.Preauth
					) {
					return true;
				}
			}
			return false;
		}

		///<summary>Only called from FormProcEdit to signal when to disable much of the editing in that form. If the procedure is 'AttachedToClaim' then user should not change it very much.  The claimProcList can be all claimProcs for the patient or only those attached to this proc.</summary>
		public static bool IsAttachedToClaim(Procedure proc,List<ClaimProc> claimProcList) {
			//No need to check RemotingRole; no call to db.
			for(int i=0;i<claimProcList.Count;i++) {
				if(claimProcList[i].ProcNum==proc.ProcNum
					&& claimProcList[i].ClaimNum>0
					&& (claimProcList[i].Status==ClaimProcStatus.CapClaim
					|| claimProcList[i].Status==ClaimProcStatus.NotReceived
					|| claimProcList[i].Status==ClaimProcStatus.Preauth
					|| claimProcList[i].Status==ClaimProcStatus.Received
					|| claimProcList[i].Status==ClaimProcStatus.Supplemental
					)) {
					return true;
				}
			}
			return false;
		}

		///<summary>Only called from FormProcEditAll to signal when to disable much of the editing in that form. If the procedure is 'AttachedToClaim' then user should not change it very much.  The claimProcList can be all claimProcs for the patient or only those attached to this proc.</summary>
		public static bool IsAttachedToClaim(List<Procedure> procList,List<ClaimProc> claimprocList) {
			//No need to check RemotingRole; no call to db.
			for(int j=0;j<procList.Count;j++) {
				if(IsAttachedToClaim(procList[j],claimprocList)) {
					return true;
				}
			}
			return false;
		}

		///<summary>Queries the database to determine if this procedure is attached to a claim already.</summary>
		public static bool IsAttachedToClaim(int procNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetBool(MethodBase.GetCurrentMethod(),procNum);
			}
			string command="SELECT COUNT(*) FROM claimproc "
				+"WHERE ProcNum="+POut.PInt(procNum)+" "
				+"AND ClaimNum>0";
			DataTable table=Db.GetTable(command);
			if(table.Rows[0][0].ToString()=="0") {
				return false;
			}
			return true;
		}

		///<summary>Used in ContrAccount.CreateClaim to validate that procedure is not already attached to a claim for this specific insPlan.  The claimProcList can be all claimProcs for the patient or only those attached to this proc.</summary>
		public static bool IsAlreadyAttachedToClaim(Procedure proc,List<ClaimProc> claimProcList,int planNum) {
			//No need to check RemotingRole; no call to db.
			for(int i=0;i<claimProcList.Count;i++) {
				if(claimProcList[i].ProcNum==proc.ProcNum
					&& claimProcList[i].PlanNum==planNum
					&& claimProcList[i].ClaimNum>0
					&& claimProcList[i].Status!=ClaimProcStatus.Preauth) {
					return true;
				}
			}
			return false;
		}

		///<summary>Only used in ContrAccount.OnInsClick to automate selection of procedures.  Returns true if this procedure should be selected.  This happens if there is at least one claimproc attached for this plan that is an estimate, and it is not set to NoBillIns.  The list can be all ClaimProcs for patient, or just those for this procedure. The plan is the primary plan.</summary>
		public static bool NeedsSent(int procNum,List<ClaimProc> claimProcList,int planNum) {
			//No need to check RemotingRole; no call to db.
			for(int i=0;i<claimProcList.Count;i++) {
				if(claimProcList[i].ProcNum==procNum
					&& !claimProcList[i].NoBillIns
					&& claimProcList[i].PlanNum==planNum
					&& claimProcList[i].Status==ClaimProcStatus.Estimate) 
				{
					return true;
				}
			}
			return false;
		}

		///<summary>Only used in ContrAccount.CreateClaim to decide whether a given procedure has an estimate that can be used to attach to a claim for the specified plan.  Returns a valid claimProc if this procedure has an estimate attached that is not set to NoBillIns.  The list can be all ClaimProcs for patient, or just those for this procedure. Returns null if there are no claimprocs that would work.</summary>
		public static ClaimProc GetClaimProcEstimate(int procNum,List<ClaimProc> claimProcList,InsPlan plan) {
			//No need to check RemotingRole; no call to db.
			//bool matchOfWrongType=false;
			for(int i=0;i<claimProcList.Count;i++) {
				if(claimProcList[i].ProcNum==procNum
					&& !claimProcList[i].NoBillIns
					&& claimProcList[i].PlanNum==plan.PlanNum) {
					if(plan.PlanType=="c") {
						if(claimProcList[i].Status==ClaimProcStatus.CapComplete) {
							return claimProcList[i];
						}
					}
					else {//any type except capitation
						if(claimProcList[i].Status==ClaimProcStatus.Estimate) {
							return claimProcList[i];
						}
					}
				}
			}
			return null;
		}

		/// <summary>Used by GetProcsForSingle and GetProcsMultApts to generate a short string description of a procedure.</summary>
		public static string ConvertProcToString(int codeNum,string surf,string toothNum) {
			//No need to check RemotingRole; no call to db.
			string strLine="";
			ProcedureCode code=ProcedureCodes.GetProcCode(codeNum);
			switch(code.TreatArea) {
				case TreatmentArea.Surf:
					strLine+="#"+Tooth.ToInternat(toothNum)+"-"+surf+"-";//""#12-MOD-"
					break;
				case TreatmentArea.Tooth:
					strLine+="#"+Tooth.ToInternat(toothNum)+"-";//"#12-"
					break;
				default://area 3 or 0 (mouth)
					break;
				case TreatmentArea.Quad:
					strLine+=surf+"-";//"UL-"
					break;
				case TreatmentArea.Sextant:
					strLine+="S"+surf+"-";//"S2-"
					break;
				case TreatmentArea.Arch:
					strLine+=surf+"-";//"U-"
					break;
				case TreatmentArea.ToothRange:
					//strLine+=table.Rows[j][13].ToString()+" ";//don't show range
					break;
			}//end switch
			strLine+=code.AbbrDesc;
			return strLine;
		}

		///<summary>Used do display procedure descriptions on appointments. The returned string also includes surf and toothNum.</summary>
		public static string GetDescription(Procedure proc) {
			//No need to check RemotingRole; no call to db.
			return ConvertProcToString(proc.CodeNum,proc.Surf,proc.ToothNum);
		}

		///<Summary>Supply the list of procedures attached to the appointment.  It will loop through each and assign the correct provider.  Also sets clinic.  Also sets procDate for TP procs.</Summary>
		public static void SetProvidersInAppointment(Appointment apt,List<Procedure> procList) {
			//No need to check RemotingRole; no call to db.
			ProcedureCode procCode;
			Procedure changedProc;
			for(int i=0;i<procList.Count;i++) {
				changedProc=procList[i].Copy();
				if(apt.ProvHyg!=0) {//if the appointment has a hygiene provider
					procCode=ProcedureCodes.GetProcCode(procList[i].CodeNum);
					if(procCode.IsHygiene) {//hygiene proc
						changedProc.ProvNum=apt.ProvHyg;
					} 
					else {//dentist proc
						changedProc.ProvNum=apt.ProvNum;
					}
				} 
				else {//same provider for every procedure
					changedProc.ProvNum=apt.ProvNum;
				}
				changedProc.ClinicNum=apt.ClinicNum;
				if(procList[i].ProcStatus==ProcStat.TP) {
					changedProc.ProcDate=apt.AptDateTime;
				}
				Procedures.Update(changedProc,procList[i]);//won't go to db unless a field has changed.
			}
		}

		///<summary>Gets a list of procedures representing extracted teeth.  Status of C,EC,orEO. Includes procs with toothNum "1"-"32".  Will not include procs with unreasonable dates.  Used for Canadian e-claims instead of the usual ToothInitials.GetMissingOrHiddenTeeth, because Canada requires dates on the extracted teeth.  Supply all procedures for the patient.</summary>
		public static List<Procedure> GetExtractedTeeth(Procedure[] procList) {
			//No need to check RemotingRole; no call to db.
			List<Procedure> extracted=new List<Procedure>();
			ProcedureCode procCode;
			for(int i=0;i<procList.Length;i++) {
				if(procList[i].ProcStatus!=ProcStat.C&&procList[i].ProcStatus!=ProcStat.EC&&procList[i].ProcStatus!=ProcStat.EO) {
					continue;
				}
				if(!Tooth.IsValidDB(procList[i].ToothNum)) {
					continue;
				}
				if(Tooth.IsSuperNum(procList[i].ToothNum)) {
					continue;
				}
				if(Tooth.IsPrimary(procList[i].ToothNum)) {
					continue;
				}
				if(procList[i].ProcDate.Year<1880||procList[i].ProcDate>DateTime.Today) {
					continue;
				}
				procCode=ProcedureCodes.GetProcCode(procList[i].CodeNum);
				if(procCode.TreatArea!=TreatmentArea.Tooth) {
					continue;
				}
				if(procCode.PaintType!=ToothPaintingType.Extraction) {
					continue;
				}
				extracted.Add(procList[i].Copy());
			}
			return extracted;
		}

		/*
		///<summary>InsEstTotal or override is retrieved from supplied claimprocs. Includes annual max and deductible.  The claimProc array typically includes all claimProcs for the patient, but must at least include the claimprocs for this proc that we need.  Will always return a meaningful value rather than -1.</summary>
		public static double GetEst(Procedure proc,List<ClaimProc> claimProcs,int planNum) {
			//No need to check RemotingRole; no call to db.
			for(int i=0;i<claimProcs.Count;i++) {
				//adjustments automatically ignored since no ProcNum
				if(claimProcs[i].Status==ClaimProcStatus.CapClaim
					||claimProcs[i].Status==ClaimProcStatus.Preauth
					||claimProcs[i].Status==ClaimProcStatus.Supplemental) {
					continue;
				}
				if(claimProcs[i].ProcNum!=proc.ProcNum) {
					continue;
				}
				if(claimProcs[i].PlanNum!=planNum) {
					continue;
				}
				if(claimProcs[i].InsEstTotalOverride != -1){
					return claimProcs[i].InsEstTotalOverride;
				}
				return claimProcs[i].InsEstTotal;
			}
			return 0;
		}*/

		///<summary>Only fees, not estimates.  Returns number of fees changed.</summary>
		public static int GlobalUpdateFees() {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetInt(MethodBase.GetCurrentMethod());
			}
			string command=@"SELECT procedurecode.CodeNum,ProcNum,patient.PatNum,procedurelog.PatNum,
				insplan.FeeSched AS PlanFeeSched,patient.FeeSched AS PatFeeSched,patient.PriProv,
				procedurelog.ProcFee,insplan.PlanType
				FROM procedurelog
				LEFT JOIN patient ON patient.PatNum=procedurelog.PatNum
				LEFT JOIN patplan ON patplan.PatNum=procedurelog.PatNum
				AND patplan.Ordinal=1
				LEFT JOIN procedurecode ON procedurecode.CodeNum=procedurelog.CodeNum
				LEFT JOIN insplan ON insplan.PlanNum=patplan.PlanNum
				WHERE procedurelog.ProcStatus=1";
			/*@"SELECT procedurelog.ProcCode,insplan.FeeSched AS PlanFeeSched,patient.FeeSched AS PatFeeSched,
							patient.PriProv,ProcNum
							FROM procedurelog,patient
							LEFT JOIN patplan ON patplan.PatNum=procedurelog.PatNum
							AND patplan.Ordinal=1
							LEFT JOIN insplan ON insplan.PlanNum=patplan.PlanNum
							WHERE procedurelog.ProcStatus=1
							AND patient.PatNum=procedurelog.PatNum
						";*/
			DataTable table=Db.GetTable(command);
			int priPlanFeeSched;
			//int feeSchedNum;
			int patFeeSched;
			int patProv;
			string planType;
			double insfee;
			double standardfee;
			double newFee;
			double oldFee;
			int rowsChanged=0;
			for(int i=0;i<table.Rows.Count;i++) {
				priPlanFeeSched=PIn.PInt(table.Rows[i]["PlanFeeSched"].ToString());
				patFeeSched=PIn.PInt(table.Rows[i]["PatFeeSched"].ToString());
				patProv=PIn.PInt(table.Rows[i]["PriProv"].ToString());
				planType=PIn.PString(table.Rows[i]["PlanType"].ToString());
				insfee=Fees.GetAmount0(PIn.PInt(table.Rows[i]["CodeNum"].ToString()),Fees.GetFeeSched(priPlanFeeSched,patFeeSched,patProv));
				if(planType=="p") {//PPO
					standardfee=Fees.GetAmount0(PIn.PInt(table.Rows[i]["CodeNum"].ToString()),Providers.GetProv(patProv).FeeSched);
					if(standardfee>insfee) {
						newFee=standardfee;
					} else {
						newFee=insfee;
					}
				} else {
					newFee=insfee;
				}
				oldFee=PIn.PDouble(table.Rows[i]["ProcFee"].ToString());
				if(newFee==oldFee) {
					continue;
				}
				command="UPDATE procedurelog SET ProcFee='"+POut.PDouble(newFee)+"' "
					+"WHERE ProcNum="+table.Rows[i]["ProcNum"].ToString();
				rowsChanged+=Db.NonQ(command);
			}
			return rowsChanged;
		}

		///<summary>Used from TP to get a list of all TP procs, ordered by priority, toothnum.</summary>
		public static Procedure[] GetListTP(List<Procedure> procList) {
			//No need to check RemotingRole; no call to db.
			ArrayList AL=new ArrayList();
			for(int i=0;i<procList.Count;i++) {
				if(procList[i].ProcStatus==ProcStat.TP) {
					AL.Add(procList[i]);
				}
			}
			IComparer myComparer=new ProcedureComparer();
			AL.Sort(myComparer);
			Procedure[] retVal=new Procedure[AL.Count];
			AL.CopyTo(retVal);
			return retVal;
		}

		public static void ComputeEstimates(Procedure proc,int patNum,List<ClaimProc> claimProcs,bool isInitialEntry,List<InsPlan> PlanList,List<PatPlan> patPlans,List<Benefit> benefitList,int patientAge) {
			//This is a stub that needs revision.
			ComputeEstimates(proc,patNum,ref claimProcs,isInitialEntry,PlanList,patPlans,benefitList,null,null,true,patientAge);
		}

		///<summary>Used whenever a procedure changes or a plan changes.  All estimates for a given procedure must be updated. This frequently includes adding claimprocs, but can also just edit the appropriate existing claimprocs. Skips status=Adjustment,CapClaim,Preauth,Supplemental.  Also fixes date,status,and provnum if appropriate.  The claimProc list only needs to include claimprocs for this proc, although it can include more.  Only set isInitialEntry true from Chart module; it is for cap procs.  loopList only contains information about procedures that come before this one in a list such as TP or claim.</summary>
		public static void ComputeEstimates(Procedure proc,int patNum,ref List<ClaimProc> claimProcs,bool isInitialEntry,List<InsPlan> PlanList,List<PatPlan> patPlans,List<Benefit> benefitList,List<ClaimProcHist> histList,List<ClaimProcHist> loopList,bool saveToDb,int patientAge) {
			//No need to check RemotingRole; no call to db.
			bool doCreate=true;
			if(proc.ProcDate<DateTime.Today && proc.ProcStatus==ProcStat.C) {
				//don't automatically create an estimate for completed procedures
				//especially if they are older than today
				//Very important after a conversion from another software.
				//This may need to be relaxed a little for offices that enter treatment a few days after it's done.
				doCreate=false;
			}
			//first test to see if each estimate matches an existing patPlan (current coverage),
			//delete any other estimates
			for(int i=0;i<claimProcs.Count;i++) {
				if(claimProcs[i].ProcNum!=proc.ProcNum) {
					continue;
				}
				if(claimProcs[i].PlanNum==0) {
					continue;
				}
				if(claimProcs[i].Status==ClaimProcStatus.CapClaim
					||claimProcs[i].Status==ClaimProcStatus.Preauth
					||claimProcs[i].Status==ClaimProcStatus.Supplemental) {
					continue;
					//ignored: adjustment
					//included: capComplete,CapEstimate,Estimate,NotReceived,Received
				}
				if(claimProcs[i].Status!=ClaimProcStatus.Estimate && claimProcs[i].Status!=ClaimProcStatus.CapEstimate) {
					continue;
				}
				bool planIsCurrent=false;
				for(int p=0;p<patPlans.Count;p++) {
					if(patPlans[p].PlanNum==claimProcs[i].PlanNum) {
						planIsCurrent=true;
						break;
					}
				}
				//If claimProc estimate is for a plan that is not current, delete it
				if(!planIsCurrent) {
					if(saveToDb) {
						ClaimProcs.Delete(claimProcs[i]);
					}
					else {
						claimProcs[i].DoDelete=true;
					}
				}
			}
			InsPlan PlanCur;
			bool estExists;
			bool cpAdded=false;
			//loop through all patPlans (current coverage), and add any missing estimates
			for(int p=0;p<patPlans.Count;p++) {//typically, loop will only have length of 1 or 2
				if(!doCreate) {
					break;
				}
				//test to see if estimate exists
				estExists=false;
				for(int i=0;i<claimProcs.Count;i++) {
					if(claimProcs[i].ProcNum!=proc.ProcNum) {
						continue;
					}
					if(claimProcs[i].PlanNum==0) {
						continue;
					}
					if(claimProcs[i].Status==ClaimProcStatus.CapClaim
						||claimProcs[i].Status==ClaimProcStatus.Preauth
						||claimProcs[i].Status==ClaimProcStatus.Supplemental) {
						continue;
						//ignored: adjustment
						//included: capComplete,CapEstimate,Estimate,NotReceived,Received
					}
					if(patPlans[p].PlanNum!=claimProcs[i].PlanNum) {
						continue;
					}
					estExists=true;
					break;
				}
				if(estExists) {
					continue;
				}
				//estimate is missing, so add it.
				ClaimProc cp=new ClaimProc();
				cp.ProcNum=proc.ProcNum;
				cp.PatNum=patNum;
				cp.ProvNum=proc.ProvNum;
				PlanCur=InsPlans.GetPlan(patPlans[p].PlanNum,PlanList);
				if(PlanCur==null) {
					continue;//??
				}
				if(PlanCur.PlanType=="c") {
					if(proc.ProcStatus==ProcStat.C) {
						cp.Status=ClaimProcStatus.CapComplete;
					}
					else {
						cp.Status=ClaimProcStatus.CapEstimate;//this may be changed below
					}
				}
				else {
					cp.Status=ClaimProcStatus.Estimate;
				}
				cp.PlanNum=PlanCur.PlanNum;
				cp.DateCP=proc.ProcDate;
				cp.AllowedOverride=-1;
				cp.PercentOverride=-1;
				cp.NoBillIns=ProcedureCodes.GetProcCode(proc.CodeNum).NoBillIns;
				cp.PaidOtherIns=-1;
				cp.CopayOverride=-1;
				cp.ProcDate=proc.ProcDate;
				cp.BaseEst=0;
				cp.InsEstTotal=0;
				cp.InsEstTotalOverride=-1;
				cp.DedEst=-1;
				cp.DedEstOverride=-1;
				cp.PaidOtherInsOverride=-1;
				//ComputeBaseEst will fill AllowedOverride,Percentage,CopayAmt,BaseEst
				if(saveToDb) {
					ClaimProcs.Insert(cp);
				}
				else {
					claimProcs.Add(cp);//this newly added cp has not ClaimProcNum and is not yet in the db.
				}
				cpAdded=true;
			}
			//if any were added, refresh the list
			if(cpAdded && saveToDb) {//no need to refresh the list if !saveToDb, because list already made current.
				claimProcs=ClaimProcs.Refresh(patNum);
			}
			//int ordinal;
			//int patPlanNum;
			double priEstTotal=0;
			double secEstTotal=0;
			double tertEstTotal=0;
			double priBaseEst=0;
			double secBaseEst=0;
			double tertBaseEst=0;
			PatPlan patplan;
			for(int i=0;i<claimProcs.Count;i++) {
				if(claimProcs[i].ProcNum!=proc.ProcNum) {
					continue;
				}
				//This was a longstanding bug. I hope there are not other consequences for commenting it out.
				//claimProcs[i].DateCP=proc.ProcDate;
				claimProcs[i].ProcDate=proc.ProcDate;
				//capitation estimates are always forced to follow the status of the procedure
				PlanCur=InsPlans.GetPlan(claimProcs[i].PlanNum,PlanList);
				if(PlanCur!=null
					&&PlanCur.PlanType=="c"
					&&(claimProcs[i].Status==ClaimProcStatus.CapComplete
					||claimProcs[i].Status==ClaimProcStatus.CapEstimate)) {
					if(isInitialEntry) {
						//this will be switched to CapComplete further down if applicable.
						//This makes ComputeBaseEst work properly on new cap procs w status Complete
						claimProcs[i].Status=ClaimProcStatus.CapEstimate;
					}
					else if(proc.ProcStatus==ProcStat.C) {
						claimProcs[i].Status=ClaimProcStatus.CapComplete;
					}
					else {
						claimProcs[i].Status=ClaimProcStatus.CapEstimate;
					}
				}
				//ignored: adjustment
				//ComputeBaseEst automatically skips: capComplete,Preauth,capClaim,Supplemental
				//does recalc est on: CapEstimate,Estimate,NotReceived,Received
				if(claimProcs[i].PlanNum>0) {
					patplan=PatPlans.GetFromList(patPlans,claimProcs[i].PlanNum);
					if(patplan != null) {
						//the cp is altered within ComputeBaseEst, but not saved.
						if(patplan.Ordinal==1) {
							ClaimProcs.ComputeBaseEst(claimProcs[i],proc.ProcFee,proc.ToothNum,proc.CodeNum,PlanCur,patplan.PatPlanNum,
								benefitList,histList,loopList,patPlans,0,0,patientAge);
							priEstTotal+=claimProcs[i].InsEstTotal;
							priBaseEst+=claimProcs[i].BaseEst;
						}
						else if(patplan.Ordinal==2) {
							ClaimProcs.ComputeBaseEst(claimProcs[i],proc.ProcFee,proc.ToothNum,proc.CodeNum,PlanCur,patplan.PatPlanNum,
								benefitList,histList,loopList,patPlans,priEstTotal,priBaseEst,patientAge);
							secEstTotal+=claimProcs[i].InsEstTotal;
							secBaseEst+=claimProcs[i].BaseEst;
						}
						else if(patplan.Ordinal==3) {
							ClaimProcs.ComputeBaseEst(claimProcs[i],proc.ProcFee,proc.ToothNum,proc.CodeNum,PlanCur,patplan.PatPlanNum,
								benefitList,histList,loopList,patPlans,priEstTotal+secEstTotal,priBaseEst+secBaseEst,patientAge);
							tertEstTotal+=claimProcs[i].InsEstTotal;
							tertBaseEst+=claimProcs[i].BaseEst;
						}
						else {//estimate will malfunction if more than 4 insurances.
							ClaimProcs.ComputeBaseEst(claimProcs[i],proc.ProcFee,proc.ToothNum,proc.CodeNum,PlanCur,patplan.PatPlanNum,
								benefitList,histList,loopList,patPlans,priEstTotal+secEstTotal+tertEstTotal,priBaseEst+secBaseEst+tertBaseEst,patientAge);
						}
					}
				}
				if(isInitialEntry
					&&claimProcs[i].Status==ClaimProcStatus.CapEstimate
					&&proc.ProcStatus==ProcStat.C) 
				{
					claimProcs[i].Status=ClaimProcStatus.CapComplete;
				}
				//prov only updated if still an estimate
				if(claimProcs[i].Status==ClaimProcStatus.Estimate
					||claimProcs[i].Status==ClaimProcStatus.CapEstimate) {
					claimProcs[i].ProvNum=proc.ProvNum;
				}
				if(saveToDb) {
					ClaimProcs.Update(claimProcs[i]);
				}
			}
		}

		///<summary>After changing important coverage plan info, this is called to recompute estimates for all procedures for this patient.</summary>
		public static void ComputeEstimatesForAll(int patNum,List<ClaimProc> claimProcs,List<Procedure> procs,List<InsPlan> PlanList,List<PatPlan> patPlans,List<Benefit> benefitList,int patientAge) {
			//No need to check RemotingRole; no call to db.
			for(int i=0;i<procs.Count;i++) {
				ComputeEstimates(procs[i],patNum,claimProcs,false,PlanList,patPlans,benefitList,patientAge);
			}
		}

		///<summary>Loops through each proc. Does not add notes to a procedure that already has notes. Used twice, security checked in both places before calling this.  Also sets provider for each proc.</summary>
		public static void SetCompleteInAppt(Appointment apt,List<InsPlan> PlanList,List<PatPlan> patPlans,int siteNum,int patientAge) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),apt,PlanList,patPlans,siteNum);
				return;
			}
			List<Procedure> ProcList=Procedures.Refresh(apt.PatNum);
			List<ClaimProc> ClaimProcList=ClaimProcs.Refresh(apt.PatNum);
			List<Benefit> benefitList=Benefits.Refresh(patPlans);
			//this query could be improved slightly to only get notes of interest.
			string command="SELECT * FROM procnote WHERE PatNum="+POut.PInt(apt.PatNum)+" ORDER BY EntryDateTime";
			DataTable rawNotes=Db.GetTable(command);
			//CovPats.Refresh(PlanList,patPlans);
			//bool doResetRecallStatus=false;
			ProcedureCode procCode;
			Procedure oldProc;
			//int siteNum=0;
			//if(!PrefC.GetBool("EasyHidePublicHealth")){
			//	siteNum=Patients.GetPat(apt.PatNum).SiteNum;
			//}
			for(int i=0;i<ProcList.Count;i++) {
				if(ProcList[i].AptNum!=apt.AptNum) {
					continue;
				}
				//attach the note, if it exists.
				for(int n=rawNotes.Rows.Count-1;n>=0;n--) {//loop through each note, backwards.
					if(ProcList[i].ProcNum.ToString()!=rawNotes.Rows[n]["ProcNum"].ToString()) {
						continue;
					}
					ProcList[i].UserNum=PIn.PInt(rawNotes.Rows[n]["UserNum"].ToString());
					ProcList[i].Note=PIn.PString(rawNotes.Rows[n]["Note"].ToString());
					ProcList[i].SigIsTopaz=PIn.PBool(rawNotes.Rows[n]["SigIsTopaz"].ToString());
					ProcList[i].Signature=PIn.PString(rawNotes.Rows[n]["Signature"].ToString());
					break;//out of note loop.
				}
				oldProc=ProcList[i].Copy();
				procCode=ProcedureCodes.GetProcCode(ProcList[i].CodeNum);
				if(procCode.PaintType==ToothPaintingType.Extraction) {//if an extraction, then mark previous procs hidden
					//SetHideGraphical(ProcList[i]);//might not matter anymore
					ToothInitials.SetValue(apt.PatNum,ProcList[i].ToothNum,ToothInitialType.Missing);
				}
				ProcList[i].ProcStatus=ProcStat.C;
				ProcList[i].ProcDate=apt.AptDateTime.Date;
				if(oldProc.ProcStatus!=ProcStat.C) {
					ProcList[i].DateEntryC=DateTime.Now;//this triggers it to set to server time NOW().
				}
				ProcList[i].PlaceService=(PlaceOfService)PrefC.GetInt("DefaultProcedurePlaceService");
				ProcList[i].ClinicNum=apt.ClinicNum;
				ProcList[i].SiteNum=siteNum;
				ProcList[i].PlaceService=Clinics.GetPlaceService(apt.ClinicNum);
				if(apt.ProvHyg!=0) {//if the appointment has a hygiene provider
					if(procCode.IsHygiene) {//hyg proc
						ProcList[i].ProvNum=apt.ProvHyg;
					}
					else {//regular proc
						ProcList[i].ProvNum=apt.ProvNum;
					}
				}
				else {//same provider for every procedure
					ProcList[i].ProvNum=apt.ProvNum;
				}
				//if procedure was already complete, then don't add more notes.
				if(oldProc.ProcStatus!=ProcStat.C) {
					ProcList[i].Note+=ProcCodeNotes.GetNote(ProcList[i].ProvNum,ProcList[i].CodeNum);
				}
				Procedures.Update(ProcList[i],oldProc);
				Procedures.ComputeEstimates(ProcList[i],apt.PatNum,ClaimProcList,false,PlanList,patPlans,benefitList,patientAge);
			}
			//if(doResetRecallStatus){
			//	Recalls.Reset(apt.PatNum);//this also synchs recall
			//}
			Recalls.Synch(apt.PatNum);
			//Patient pt=Patients.GetPat(apt.PatNum);
			//jsparks-See notes within this method:
			//Reporting.Allocators.AllocatorCollection.CallAll_Allocators(pt.Guarantor);
		}




	}

	/*================================================================================================================
	=========================================== class ProcedureComparer =============================================*/

	///<summary>This sorts procedures based on priority, then tooth number, then code (but if Canadian lab code, uses proc code here instead of lab code).  Finally, if comparing a proc and its Canadian lab code, it puts the lab code after the proc.  It does not care about dates or status.  Currently used in TP module and Chart module sorting.</summary>
	public class ProcedureComparer:IComparer {
		///<summary>This sorts procedures based on priority, then tooth number.  It does not care about dates or status.  Currently used in TP module and Chart module sorting.</summary>
		int IComparer.Compare(Object objx,Object objy) {
			Procedure x=(Procedure)objx;
			Procedure y=(Procedure)objy;
			//first, by priority
			if(x.Priority!=y.Priority) {//if priorities are different
				if(x.Priority==0) {
					return 1;//x is greater than y. Priorities always come first.
				}
				if(y.Priority==0) {
					return -1;//x is less than y. Priorities always come first.
				}
				return DefC.GetOrder(DefCat.TxPriorities,x.Priority).CompareTo(DefC.GetOrder(DefCat.TxPriorities,y.Priority));
			}
			//priorities are the same, so sort by toothrange
			if(x.ToothRange!=y.ToothRange) {
				//empty toothranges come before filled toothrange values
				return x.ToothRange.CompareTo(y.ToothRange);
			}
			//toothranges are the same (usually empty), so compare toothnumbers
			if(x.ToothNum!=y.ToothNum) {
				//this also puts invalid or empty toothnumbers before the others.
				return Tooth.ToInt(x.ToothNum).CompareTo(Tooth.ToInt(y.ToothNum));
			}
			//priority and toothnums are the same, so sort by code.
			/*string adaX=x.Code;
			if(x.ProcNumLab !=0){//if x is a Canadian lab proc
				//then use the Code of the procedure instead of the lab code
				adaX=Procedures.GetOneProc(
			}
			string adaY=y.Code;*/
			return ProcedureCodes.GetStringProcCode(x.CodeNum).CompareTo(ProcedureCodes.GetStringProcCode(y.CodeNum));
			//return x.Code.CompareTo(y.Code);
			//return 0;//priority, tooth number, and code are all the same
		}
	}

}
