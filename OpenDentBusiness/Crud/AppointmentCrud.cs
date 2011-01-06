//This file is automatically generated.
//Do not attempt to make changes to this file because the changes will be erased and overwritten.
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;

namespace OpenDentBusiness.Crud{
	internal class AppointmentCrud {
		///<summary>Gets one Appointment object from the database using the primary key.  Returns null if not found.</summary>
		internal static Appointment SelectOne(long aptNum){
			string command="SELECT * FROM appointment "
				+"WHERE AptNum = "+POut.Long(aptNum);
			List<Appointment> list=TableToList(Db.GetTable(command));
			if(list.Count==0) {
				return null;
			}
			return list[0];
		}

		///<summary>Gets one Appointment object from the database using a query.</summary>
		internal static Appointment SelectOne(string command){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				throw new ApplicationException("Not allowed to send sql directly.  Rewrite the calling class to not use this query:\r\n"+command);
			}
			List<Appointment> list=TableToList(Db.GetTable(command));
			if(list.Count==0) {
				return null;
			}
			return list[0];
		}

		///<summary>Gets a list of Appointment objects from the database using a query.</summary>
		internal static List<Appointment> SelectMany(string command){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				throw new ApplicationException("Not allowed to send sql directly.  Rewrite the calling class to not use this query:\r\n"+command);
			}
			List<Appointment> list=TableToList(Db.GetTable(command));
			return list;
		}

		///<summary>Converts a DataTable to a list of objects.</summary>
		internal static List<Appointment> TableToList(DataTable table){
			List<Appointment> retVal=new List<Appointment>();
			Appointment appointment;
			for(int i=0;i<table.Rows.Count;i++) {
				appointment=new Appointment();
				appointment.AptNum               = PIn.Long  (table.Rows[i]["AptNum"].ToString());
				appointment.PatNum               = PIn.Long  (table.Rows[i]["PatNum"].ToString());
				appointment.AptStatus            = (ApptStatus)PIn.Int(table.Rows[i]["AptStatus"].ToString());
				appointment.Pattern              = PIn.String(table.Rows[i]["Pattern"].ToString());
				appointment.Confirmed            = PIn.Long  (table.Rows[i]["Confirmed"].ToString());
				appointment.TimeLocked           = PIn.Bool  (table.Rows[i]["TimeLocked"].ToString());
				appointment.Op                   = PIn.Long  (table.Rows[i]["Op"].ToString());
				appointment.Note                 = PIn.String(table.Rows[i]["Note"].ToString());
				appointment.ProvNum              = PIn.Long  (table.Rows[i]["ProvNum"].ToString());
				appointment.ProvHyg              = PIn.Long  (table.Rows[i]["ProvHyg"].ToString());
				appointment.AptDateTime          = PIn.DateT (table.Rows[i]["AptDateTime"].ToString());
				appointment.NextAptNum           = PIn.Long  (table.Rows[i]["NextAptNum"].ToString());
				appointment.UnschedStatus        = PIn.Long  (table.Rows[i]["UnschedStatus"].ToString());
				appointment.IsNewPatient         = PIn.Bool  (table.Rows[i]["IsNewPatient"].ToString());
				appointment.ProcDescript         = PIn.String(table.Rows[i]["ProcDescript"].ToString());
				appointment.Assistant            = PIn.Long  (table.Rows[i]["Assistant"].ToString());
				appointment.ClinicNum            = PIn.Long  (table.Rows[i]["ClinicNum"].ToString());
				appointment.IsHygiene            = PIn.Bool  (table.Rows[i]["IsHygiene"].ToString());
				appointment.DateTStamp           = PIn.DateT (table.Rows[i]["DateTStamp"].ToString());
				appointment.DateTimeArrived      = PIn.DateT (table.Rows[i]["DateTimeArrived"].ToString());
				appointment.DateTimeSeated       = PIn.DateT (table.Rows[i]["DateTimeSeated"].ToString());
				appointment.DateTimeDismissed    = PIn.DateT (table.Rows[i]["DateTimeDismissed"].ToString());
				appointment.InsPlan1             = PIn.Long  (table.Rows[i]["InsPlan1"].ToString());
				appointment.InsPlan2             = PIn.Long  (table.Rows[i]["InsPlan2"].ToString());
				appointment.DateTimeAskedToArrive= PIn.DateT (table.Rows[i]["DateTimeAskedToArrive"].ToString());
				appointment.ProcsColored         = PIn.String(table.Rows[i]["ProcsColored"].ToString());
				retVal.Add(appointment);
			}
			return retVal;
		}

		///<summary>Inserts one Appointment into the database.  Returns the new priKey.</summary>
		internal static long Insert(Appointment appointment){
			return Insert(appointment,false);
		}

		///<summary>Inserts one Appointment into the database.  Provides option to use the existing priKey.</summary>
		internal static long Insert(Appointment appointment,bool useExistingPK){
			if(!useExistingPK && PrefC.RandomKeys) {
				appointment.AptNum=ReplicationServers.GetKey("appointment","AptNum");
			}
			string command="INSERT INTO appointment (";
			if(useExistingPK || PrefC.RandomKeys) {
				command+="AptNum,";
			}
			command+="PatNum,AptStatus,Pattern,Confirmed,TimeLocked,Op,Note,ProvNum,ProvHyg,AptDateTime,NextAptNum,UnschedStatus,IsNewPatient,ProcDescript,Assistant,ClinicNum,IsHygiene,DateTimeArrived,DateTimeSeated,DateTimeDismissed,InsPlan1,InsPlan2,DateTimeAskedToArrive,ProcsColored) VALUES(";
			if(useExistingPK || PrefC.RandomKeys) {
				command+=POut.Long(appointment.AptNum)+",";
			}
			command+=
				     POut.Long  (appointment.PatNum)+","
				+    POut.Int   ((int)appointment.AptStatus)+","
				+"'"+POut.String(appointment.Pattern)+"',"
				+    POut.Long  (appointment.Confirmed)+","
				+    POut.Bool  (appointment.TimeLocked)+","
				+    POut.Long  (appointment.Op)+","
				+"'"+POut.String(appointment.Note)+"',"
				+    POut.Long  (appointment.ProvNum)+","
				+    POut.Long  (appointment.ProvHyg)+","
				+    POut.DateT (appointment.AptDateTime)+","
				+    POut.Long  (appointment.NextAptNum)+","
				+    POut.Long  (appointment.UnschedStatus)+","
				+    POut.Bool  (appointment.IsNewPatient)+","
				+"'"+POut.String(appointment.ProcDescript)+"',"
				+    POut.Long  (appointment.Assistant)+","
				+    POut.Long  (appointment.ClinicNum)+","
				+    POut.Bool  (appointment.IsHygiene)+","
				//DateTStamp can only be set by MySQL
				+    POut.DateT (appointment.DateTimeArrived)+","
				+    POut.DateT (appointment.DateTimeSeated)+","
				+    POut.DateT (appointment.DateTimeDismissed)+","
				+    POut.Long  (appointment.InsPlan1)+","
				+    POut.Long  (appointment.InsPlan2)+","
				+    POut.DateT (appointment.DateTimeAskedToArrive)+","
				+"'"+POut.String(appointment.ProcsColored)+"')";
			if(useExistingPK || PrefC.RandomKeys) {
				Db.NonQ(command);
			}
			else {
				appointment.AptNum=Db.NonQ(command,true);
			}
			return appointment.AptNum;
		}

		///<summary>Updates one Appointment in the database.</summary>
		internal static void Update(Appointment appointment){
			string command="UPDATE appointment SET "
				+"PatNum               =  "+POut.Long  (appointment.PatNum)+", "
				+"AptStatus            =  "+POut.Int   ((int)appointment.AptStatus)+", "
				+"Pattern              = '"+POut.String(appointment.Pattern)+"', "
				+"Confirmed            =  "+POut.Long  (appointment.Confirmed)+", "
				+"TimeLocked           =  "+POut.Bool  (appointment.TimeLocked)+", "
				+"Op                   =  "+POut.Long  (appointment.Op)+", "
				+"Note                 = '"+POut.String(appointment.Note)+"', "
				+"ProvNum              =  "+POut.Long  (appointment.ProvNum)+", "
				+"ProvHyg              =  "+POut.Long  (appointment.ProvHyg)+", "
				+"AptDateTime          =  "+POut.DateT (appointment.AptDateTime)+", "
				+"NextAptNum           =  "+POut.Long  (appointment.NextAptNum)+", "
				+"UnschedStatus        =  "+POut.Long  (appointment.UnschedStatus)+", "
				+"IsNewPatient         =  "+POut.Bool  (appointment.IsNewPatient)+", "
				+"ProcDescript         = '"+POut.String(appointment.ProcDescript)+"', "
				+"Assistant            =  "+POut.Long  (appointment.Assistant)+", "
				+"ClinicNum            =  "+POut.Long  (appointment.ClinicNum)+", "
				+"IsHygiene            =  "+POut.Bool  (appointment.IsHygiene)+", "
				//DateTStamp can only be set by MySQL
				+"DateTimeArrived      =  "+POut.DateT (appointment.DateTimeArrived)+", "
				+"DateTimeSeated       =  "+POut.DateT (appointment.DateTimeSeated)+", "
				+"DateTimeDismissed    =  "+POut.DateT (appointment.DateTimeDismissed)+", "
				+"InsPlan1             =  "+POut.Long  (appointment.InsPlan1)+", "
				+"InsPlan2             =  "+POut.Long  (appointment.InsPlan2)+", "
				+"DateTimeAskedToArrive=  "+POut.DateT (appointment.DateTimeAskedToArrive)+", "
				+"ProcsColored         = '"+POut.String(appointment.ProcsColored)+"' "
				+"WHERE AptNum = "+POut.Long(appointment.AptNum);
			Db.NonQ(command);
		}

		///<summary>Updates one Appointment in the database.  Uses an old object to compare to, and only alters changed fields.  This prevents collisions and concurrency problems in heavily used tables.</summary>
		internal static void Update(Appointment appointment,Appointment oldAppointment){
			string command="";
			if(appointment.PatNum != oldAppointment.PatNum) {
				if(command!=""){ command+=",";}
				command+="PatNum = "+POut.Long(appointment.PatNum)+"";
			}
			if(appointment.AptStatus != oldAppointment.AptStatus) {
				if(command!=""){ command+=",";}
				command+="AptStatus = "+POut.Int   ((int)appointment.AptStatus)+"";
			}
			if(appointment.Pattern != oldAppointment.Pattern) {
				if(command!=""){ command+=",";}
				command+="Pattern = '"+POut.String(appointment.Pattern)+"'";
			}
			if(appointment.Confirmed != oldAppointment.Confirmed) {
				if(command!=""){ command+=",";}
				command+="Confirmed = "+POut.Long(appointment.Confirmed)+"";
			}
			if(appointment.TimeLocked != oldAppointment.TimeLocked) {
				if(command!=""){ command+=",";}
				command+="TimeLocked = "+POut.Bool(appointment.TimeLocked)+"";
			}
			if(appointment.Op != oldAppointment.Op) {
				if(command!=""){ command+=",";}
				command+="Op = "+POut.Long(appointment.Op)+"";
			}
			if(appointment.Note != oldAppointment.Note) {
				if(command!=""){ command+=",";}
				command+="Note = '"+POut.String(appointment.Note)+"'";
			}
			if(appointment.ProvNum != oldAppointment.ProvNum) {
				if(command!=""){ command+=",";}
				command+="ProvNum = "+POut.Long(appointment.ProvNum)+"";
			}
			if(appointment.ProvHyg != oldAppointment.ProvHyg) {
				if(command!=""){ command+=",";}
				command+="ProvHyg = "+POut.Long(appointment.ProvHyg)+"";
			}
			if(appointment.AptDateTime != oldAppointment.AptDateTime) {
				if(command!=""){ command+=",";}
				command+="AptDateTime = "+POut.DateT(appointment.AptDateTime)+"";
			}
			if(appointment.NextAptNum != oldAppointment.NextAptNum) {
				if(command!=""){ command+=",";}
				command+="NextAptNum = "+POut.Long(appointment.NextAptNum)+"";
			}
			if(appointment.UnschedStatus != oldAppointment.UnschedStatus) {
				if(command!=""){ command+=",";}
				command+="UnschedStatus = "+POut.Long(appointment.UnschedStatus)+"";
			}
			if(appointment.IsNewPatient != oldAppointment.IsNewPatient) {
				if(command!=""){ command+=",";}
				command+="IsNewPatient = "+POut.Bool(appointment.IsNewPatient)+"";
			}
			if(appointment.ProcDescript != oldAppointment.ProcDescript) {
				if(command!=""){ command+=",";}
				command+="ProcDescript = '"+POut.String(appointment.ProcDescript)+"'";
			}
			if(appointment.Assistant != oldAppointment.Assistant) {
				if(command!=""){ command+=",";}
				command+="Assistant = "+POut.Long(appointment.Assistant)+"";
			}
			if(appointment.ClinicNum != oldAppointment.ClinicNum) {
				if(command!=""){ command+=",";}
				command+="ClinicNum = "+POut.Long(appointment.ClinicNum)+"";
			}
			if(appointment.IsHygiene != oldAppointment.IsHygiene) {
				if(command!=""){ command+=",";}
				command+="IsHygiene = "+POut.Bool(appointment.IsHygiene)+"";
			}
			//DateTStamp can only be set by MySQL
			if(appointment.DateTimeArrived != oldAppointment.DateTimeArrived) {
				if(command!=""){ command+=",";}
				command+="DateTimeArrived = "+POut.DateT(appointment.DateTimeArrived)+"";
			}
			if(appointment.DateTimeSeated != oldAppointment.DateTimeSeated) {
				if(command!=""){ command+=",";}
				command+="DateTimeSeated = "+POut.DateT(appointment.DateTimeSeated)+"";
			}
			if(appointment.DateTimeDismissed != oldAppointment.DateTimeDismissed) {
				if(command!=""){ command+=",";}
				command+="DateTimeDismissed = "+POut.DateT(appointment.DateTimeDismissed)+"";
			}
			if(appointment.InsPlan1 != oldAppointment.InsPlan1) {
				if(command!=""){ command+=",";}
				command+="InsPlan1 = "+POut.Long(appointment.InsPlan1)+"";
			}
			if(appointment.InsPlan2 != oldAppointment.InsPlan2) {
				if(command!=""){ command+=",";}
				command+="InsPlan2 = "+POut.Long(appointment.InsPlan2)+"";
			}
			if(appointment.DateTimeAskedToArrive != oldAppointment.DateTimeAskedToArrive) {
				if(command!=""){ command+=",";}
				command+="DateTimeAskedToArrive = "+POut.DateT(appointment.DateTimeAskedToArrive)+"";
			}
			if(appointment.ProcsColored != oldAppointment.ProcsColored) {
				if(command!=""){ command+=",";}
				command+="ProcsColored = '"+POut.String(appointment.ProcsColored)+"'";
			}
			if(command==""){
				return;
			}
			command="UPDATE appointment SET "+command
				+" WHERE AptNum = "+POut.Long(appointment.AptNum);
			Db.NonQ(command);
		}

		///<summary>Deletes one Appointment from the database.</summary>
		internal static void Delete(long aptNum){
			string command="DELETE FROM appointment "
				+"WHERE AptNum = "+POut.Long(aptNum);
			Db.NonQ(command);
		}

	}
}