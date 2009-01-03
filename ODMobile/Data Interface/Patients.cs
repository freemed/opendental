using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Xml;

namespace OpenDentMobile {
	public class Patients {
		///<summary>Writes one patient to the database.  Or deletes one patient from database.</summary>
		public static void Sync(XmlNode objectNode){
			Patient pat=new Patient();
			XmlNodeList childNodes=objectNode.ChildNodes;
			XmlNode node;
			for(int i=0;i<childNodes.Count;i++){
				node=childNodes[i];
				switch(node.Name){
					case "PatNum":
						pat.PatNum=PIn.PInt(node.InnerText);
						break;
					case "LName":
						pat.LName=node.InnerText;
						break;
					case "FName":
						pat.FName=node.InnerText;
						break;
					case "Preferred":
						pat.Preferred=node.InnerText;
						break;
					case "PatStatus":
						pat.PatStatus=(PatientStatus)PIn.PInt(node.InnerText);
						break;
					case "Gender":
						pat.Gender=(PatientGender)PIn.PInt(node.InnerText);
						break;
					case "Position":
						pat.Position=(PatientPosition)PIn.PInt(node.InnerText);
						break;
					case "Birthdate":
						pat.Birthdate=PIn.PDate(node.InnerText);
						break;
					case "Address":
						pat.Address=node.InnerText;
						break;
					case "Address2":
						pat.Address2=node.InnerText;
						break;
					case "City":
						pat.City=node.InnerText;
						break;
					case "State":
						pat.State=node.InnerText;
						break;
					case "HmPhone":
						pat.HmPhone=node.InnerText;
						break;
					case "WkPhone":
						pat.WkPhone=node.InnerText;
						break;
					case "WirelessPhone":
						pat.WirelessPhone=node.InnerText;
						break;
					case "Guarantor":
						pat.Guarantor=PIn.PInt(node.InnerText);
						break;
					case "CreditType":
						pat.CreditType=node.InnerText;
						break;
					case "FamFinUrgNote":
						pat.FamFinUrgNote=node.InnerText;
						break;
					case "MedUrgNote":
						pat.MedUrgNote=node.InnerText;
						break;
					case "PrimaryInsurance":
						pat.PrimaryInsurance=node.InnerText;
						break;
				}
			}
			if(objectNode.Attributes.GetNamedItem("action").InnerText=="delete"){
				Delete(pat.PatNum);
			}
			else if(objectNode.Attributes.GetNamedItem("action").InnerText=="write"){
				string command="SELECT COUNT(*) FROM patient WHERE PatNum="+POut.PInt(pat.PatNum);
				DataTable table=General.GetTable(command);
				if(table.Rows[0][0].ToString()=="0"){
					Insert(pat);
				}
				else{
					Update(pat);
				}
			}
		}

		///<summary>This is a way to get a single patient from the database if you don't already have a family object to use.</summary>
		public static Patient GetPat(int patNum){
			if(patNum==0) {
				return null;
			}
			string command="SELECT * FROM patient WHERE PatNum="+POut.PInt(patNum);
			List<Patient> pats=SubmitAndFill(command);
			return pats[0];
		}

		private static List<Patient> SubmitAndFill(string command){
			DataTable table=General.GetTable(command);
			List<Patient> list=new List<Patient>();
			Patient pat;
			for(int i=0;i<table.Rows.Count;i++){
				pat=new Patient();
				pat.PatNum          =PIn.PInt   (table.Rows[i]["PatNum"].ToString());
				pat.LName           =PIn.PString(table.Rows[i]["LName"].ToString());
				pat.FName           =PIn.PString(table.Rows[i]["FName"].ToString());
				pat.Preferred       =PIn.PString(table.Rows[i]["Preferred"].ToString());
				pat.PatStatus       =(PatientStatus)PIn.PInt   (table.Rows[i]["PatStatus"].ToString());
				pat.Gender          =(PatientGender)PIn.PInt   (table.Rows[i]["Gender"].ToString());
				pat.Position        =(PatientPosition)PIn.PInt   (table.Rows[i]["Position"].ToString());
				pat.Birthdate       =PIn.PDate  (table.Rows[i]["Birthdate"].ToString());
				pat.Address         =PIn.PString(table.Rows[i]["Address"].ToString());
				pat.Address2        =PIn.PString(table.Rows[i]["Address2"].ToString());
				pat.City            =PIn.PString(table.Rows[i]["City"].ToString());
				pat.State           =PIn.PString(table.Rows[i]["State"].ToString());
				pat.HmPhone         =PIn.PString(table.Rows[i]["HmPhone"].ToString());
				pat.WkPhone         =PIn.PString(table.Rows[i]["WkPhone"].ToString());
				pat.WirelessPhone   =PIn.PString(table.Rows[i]["WirelessPhone"].ToString());
				pat.Guarantor       =PIn.PInt   (table.Rows[i]["Guarantor"].ToString());
				pat.CreditType      =PIn.PString(table.Rows[i]["CreditType"].ToString());
				pat.FamFinUrgNote   =PIn.PString(table.Rows[i]["FamFinUrgNote"].ToString());
				pat.MedUrgNote      =PIn.PString(table.Rows[i]["MedUrgNote"].ToString());
				pat.PrimaryInsurance=PIn.PString(table.Rows[i]["PrimaryInsurance"].ToString());
				list.Add(pat);
			}
			return list;
		}

		///<summary></summary>
		private static void Insert(Patient pat){
			string command= "INSERT INTO patient (PatNum,LName,FName,Preferred,PatStatus,Gender,Position,"
				+"Birthdate,Address,Address2,City,State,HmPhone,WkPhone,WirelessPhone,Guarantor,CreditType,"
				+"FamFinUrgNote,MedUrgNote,PrimaryInsurance) "
				+"VALUES ("
				+"'"+POut.PInt   (pat.PatNum)+"', "
				+"'"+POut.PString(pat.LName)+"', "
				+"'"+POut.PString(pat.FName)+"', "
				+"'"+POut.PString(pat.Preferred)+"', "
				+"'"+POut.PInt   ((int)pat.PatStatus)+"', "
				+"'"+POut.PInt   ((int)pat.Gender)+"', "
				+"'"+POut.PInt   ((int)pat.Position)+"', "
				+"'"+POut.PDate  (pat.Birthdate)+"', "
				+"'"+POut.PString(pat.Address)+"', "
				+"'"+POut.PString(pat.Address2)+"', "
				+"'"+POut.PString(pat.City)+"', "
				+"'"+POut.PString(pat.State)+"', "
				+"'"+POut.PString(pat.HmPhone)+"', "
				+"'"+POut.PString(pat.WkPhone)+"', "
				+"'"+POut.PString(pat.WirelessPhone)+"', "
				+"'"+POut.PInt   (pat.Guarantor)+"', "
				+"'"+POut.PString(pat.CreditType)+"', "
				+"'"+POut.PString(pat.FamFinUrgNote)+"', "
				+"'"+POut.PString(pat.MedUrgNote)+"', "
				+"'"+POut.PString(pat.PrimaryInsurance)+"')";
			General.NonQ(command);
		}

		///<summary></summary>
		private static void Update(Patient pat){
			string command = "UPDATE patient SET "
				+"LName ='"           +POut.PString(pat.LName)+"'"
				+",FName ='"          +POut.PString(pat.FName)+"'"
				+",Preferred ='"      +POut.PString(pat.Preferred)+"'"
				+",PatStatus ='"      +POut.PInt   ((int)pat.PatStatus)+"'"
				+",Gender ='"         +POut.PInt   ((int)pat.Gender)+"'"
				+",Position ='"       +POut.PInt   ((int)pat.Position)+"'"
				+",Birthdate ='"      +POut.PDate  (pat.Birthdate)+"'"
				+",Address ='"        +POut.PString(pat.Address)+"'"
				+",Address2 ='"       +POut.PString(pat.Address2)+"'"
				+",City ='"           +POut.PString(pat.City)+"'"
				+",State ='"          +POut.PString(pat.State)+"'"
				+",HmPhone ='"        +POut.PString(pat.HmPhone)+"'"
				+",WkPhone ='"        +POut.PString(pat.WkPhone)+"'"
				+",WirelessPhone ='"  +POut.PString(pat.WirelessPhone)+"'"
				+",Guarantor ='"      +POut.PInt   (pat.Guarantor)+"'"
				+",CreditType ='"     +POut.PString(pat.CreditType)+"'"
				+",FamFinUrgNote ='"  +POut.PString(pat.FamFinUrgNote)+"'"
				+",MedUrgNote ='"     +POut.PString(pat.MedUrgNote)+"'"
				+",PrimaryInsurance='"+POut.PString(pat.PrimaryInsurance)+"'"
				+" WHERE PatNum = '"+POut.PInt(pat.PatNum)+"'";
			General.NonQ(command);
		}

		private static void Delete(int patNum){
			string command = "DELETE FROM patient WHERE PatNum="+POut.PInt(patNum);
			General.NonQ(command);
		}

		public static void DeleteAll(){
			string command = "DELETE FROM patient";
			General.NonQ(command);
		}

		///<summary>Only used for the Select Patient dialog.</summary>
		public static DataTable GetPtDataTable(string lname){
			string command= 
				"SELECT PatNum,LName,FName,HmPhone,WirelessPhone FROM patient "
				+"WHERE LName LIKE '"+POut.PString(lname)+"%' "
				+"ORDER BY LName,FName ";
 			DataTable table=General.GetTable(command);
			return table;
		}

	


	}
}
