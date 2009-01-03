using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Xml;

namespace OpenDentMobile {
	public class Appointments {
		///<summary>Writes one appointment to the database.  Or deletes one appointment from database.</summary>
		public static void Sync(XmlNode objectNode){
			Appointment apt=new Appointment();
			XmlNodeList childNodes=objectNode.ChildNodes;
			XmlNode node;
			for(int i=0;i<childNodes.Count;i++){
				node=childNodes[i];
				switch(node.Name){
					case "AptNum":
						apt.AptNum=PIn.PInt(node.InnerText);
						break;
					case "PatNum":
						apt.PatNum=PIn.PInt(node.InnerText);
						break;
					case "AptStatus":
						apt.AptStatus=(ApptStatus)PIn.PInt(node.InnerText);
						break;
					case "Pattern":
						apt.Pattern=node.InnerText;
						break;
					case "Confirmed":
						apt.Confirmed=PIn.PInt(node.InnerText);
						break;
					case "Op":
						apt.Op=PIn.PInt(node.InnerText);
						break;
					case "Note":
						apt.Note=node.InnerText;
						break;
					case "ProvNum":
						apt.ProvNum=PIn.PInt(node.InnerText);
						break;
					case "ProvHyg":
						apt.ProvHyg=PIn.PInt(node.InnerText);
						break;
					case "AptDateTime":
						apt.AptDateTime=PIn.PDateT(node.InnerText);
						break;
					case "ProcDescript":
						apt.ProcDescript=node.InnerText;
						break;
					case "IsHygiene":
						apt.IsHygiene=PIn.PBool(node.InnerText);
						break;
				}
			}
			if(objectNode.Attributes.GetNamedItem("action").InnerText=="delete"){
				Delete(apt.AptNum);
			}
			else if(objectNode.Attributes.GetNamedItem("action").InnerText=="write"){
				string command="SELECT COUNT(*) FROM appointment WHERE AptNum="+POut.PInt(apt.AptNum);
				DataTable table=General.GetTable(command);
				if(table.Rows[0][0].ToString()=="0"){
					Insert(apt);
				}
				else{
					Update(apt);
				}
			}
		}

		/*
		///<summary>Get one appt from db.</summary>
		public static appointment GetPat(int patNum){
			if(patNum==0) {
				return null;
			}
			string command="SELECT * FROM appointment WHERE PatNum="+POut.PInt(patNum);
			List<appointment> pats=SubmitAndFill(command);
			return pats[0];
		}

		private static List<appointment> SubmitAndFill(string command){
			DataTable table=General.GetTable(command);
			List<appointment> list=new List<appointment>();
			appointment pat;
			for(int i=0;i<table.Rows.Count;i++){
				pat=new appointment();
				pat.PatNum          =PIn.PInt   (table.Rows[i]["PatNum"].ToString());
				pat.LName           =PIn.PString(table.Rows[i]["LName"].ToString());
				pat.FName           =PIn.PString(table.Rows[i]["FName"].ToString());
				pat.FamFinUrgNote   =PIn.PString(table.Rows[i]["FamFinUrgNote"].ToString());
				pat.MedUrgNote      =PIn.PString(table.Rows[i]["MedUrgNote"].ToString());
				pat.PrimaryInsurance=PIn.PString(table.Rows[i]["PrimaryInsurance"].ToString());
				list.Add(pat);
			}
			return list;
		}*/

		///<summary></summary>
		private static void Insert(Appointment apt){
			string command= "INSERT INTO appointment (AptNum,PatNum,AptStatus,Pattern,Confirmed,Op,Note,ProvNum,"
				+"ProvHyg,AptDateTime,ProcDescript,IsHygiene) "
				+"VALUES ("
				+"'"+POut.PInt   (apt.AptNum)+"', "
				+"'"+POut.PInt   (apt.PatNum)+"', "
				+"'"+POut.PInt   ((int)apt.AptStatus)+"', "
				+"'"+POut.PString(apt.Pattern)+"', "
				+"'"+POut.PInt   (apt.Confirmed)+"', "
				+"'"+POut.PInt   (apt.Op)+"', "
				+"'"+POut.PString(apt.Note)+"', "
				+"'"+POut.PInt   (apt.ProvNum)+"', "
				+"'"+POut.PInt   (apt.ProvHyg)+"', "
				+"'"+POut.PDateT (apt.AptDateTime)+"', "
				+"'"+POut.PString(apt.ProcDescript)+"', "
				+"'"+POut.PBool  (apt.IsHygiene)+"')";
			General.NonQ(command);
		}

		///<summary></summary>
		private static void Update(Appointment apt){
			string command = "UPDATE appointment SET "
				+"PatNum ='"       +POut.PInt   (apt.PatNum)+"'"
				+",AptStatus ='"   +POut.PInt   ((int)apt.AptStatus)+"'"
				+",Pattern ='"     +POut.PString(apt.Pattern)+"'"
				+",Confirmed ='"   +POut.PInt   (apt.Confirmed)+"'"
				+",Op ='"          +POut.PInt   (apt.Op)+"'"
				+",Note ='"        +POut.PString(apt.Note)+"'"
				+",ProvNum ='"     +POut.PInt   (apt.ProvNum)+"'"
				+",ProvHyg ='"     +POut.PInt   (apt.ProvHyg)+"'"
				+",AptDateTime ='" +POut.PDateT (apt.AptDateTime)+"'"
				+",ProcDescript ='"+POut.PString(apt.ProcDescript)+"'"
				+",IsHygiene ='"  +POut.PBool  (apt.IsHygiene)+"'"
				+" WHERE AptNum= '"+POut.PInt   (apt.AptNum)+"'";
			General.NonQ(command);
		}

		private static void Delete(int aptNum){
			string command = "DELETE FROM appointment WHERE AptNum="+POut.PInt(aptNum);
			General.NonQ(command);
		}

		public static void DeleteAll(){
			string command = "DELETE FROM appointment";
			General.NonQ(command);
		}

		public static DataTable RefreshPeriod(DateTime date){
			string command="SELECT AptDateTime,AptNum,FName,LName,Note,appointment.PatNum,Pattern,Preferred,ProcDescript "
				+"FROM appointment,patient "
				+"WHERE appointment.PatNum=patient.PatNum "
				+"AND AptDateTime > '"+POut.PDate(date)+"' "
				+"AND AptDateTime < '"+POut.PDate(date.AddDays(1))+"' "
				+"ORDER BY AptDateTime";
			DataTable raw=General.GetTable(command);
			DataTable table=new DataTable();
			table.Columns.Add("AptNum");
			table.Columns.Add("length");
			table.Columns.Add("Note");
			table.Columns.Add("patient");
			table.Columns.Add("PatNum");
			table.Columns.Add("ProcDescript");
			table.Columns.Add("time");
			DataRow row;
			string patName;
			DateTime dateT;
			for(int i=0;i<raw.Rows.Count;i++){
				row=table.NewRow();
				row["AptNum"]=raw.Rows[i]["AptNum"].ToString();
				row["length"]=(raw.Rows[i]["Pattern"].ToString().Length*5).ToString();
				row["Note"]=raw.Rows[i]["Note"].ToString();
				patName=raw.Rows[i]["LName"].ToString()+", ";
				if(raw.Rows[i]["Preferred"].ToString()!=""){
					patName+="'"+raw.Rows[i]["Preferred"].ToString()+"' ";
				}
				patName+=raw.Rows[i]["FName"].ToString();
				row["patient"]=patName;
				row["PatNum"]=raw.Rows[i]["PatNum"].ToString();
				row["ProcDescript"]=raw.Rows[i]["ProcDescript"].ToString();
				dateT=PIn.PDateT(raw.Rows[i]["AptDateTime"].ToString());
				row["time"]=dateT.ToShortTimeString();
				table.Rows.Add(row);
			}
			return table;
		}

	


	}
}
