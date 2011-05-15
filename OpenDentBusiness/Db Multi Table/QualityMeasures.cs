using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace OpenDentBusiness {
	///<summary>Used in Ehr quality measures.</summary>
	public class QualityMeasures {
		///<summary>Generates a list of all the quality measures.  Performs all calculations and manipulations.  Returns list for viewing/output.</summary>
		public static List<QualityMeasure> GetAll(DateTime dateStart,DateTime dateEnd) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<QualityMeasure>>(MethodBase.GetCurrentMethod(),dateStart,dateEnd);
			}
			List<QualityMeasure> list=new List<QualityMeasure>();
			//add one of each type
			QualityMeasure measure;
			for(int i=0;i<Enum.GetValues(typeof(QualityType)).Length;i++) {
				measure=new QualityMeasure();
				measure.Type=(QualityType)i;
				measure.Id=GetId(measure.Type);
				measure.Descript=GetDescript(measure.Type);
				DataTable table=GetTable(measure.Type,dateStart,dateEnd);
				if(table!=null) {
					measure.Denominator=table.Rows.Count;
					measure.Numerator=CalcNumerator(table);
					measure.Exclusions=CalcExclusions(table);
					measure.NotMet=measure.Denominator-measure.Exclusions-measure.Numerator;
					measure.ReportingRate=100;
					measure.PerformanceRate=0;
					if(measure.Numerator > 0) {
						measure.PerformanceRate=(int)((float)(measure.Numerator)/(float)(measure.Numerator+measure.NotMet));
					}
				}
			}
			return list;
			//measure.Id="421a";
			//measure.Descript=";
		}

		private static string GetId(QualityType qtype){
			switch(qtype) {
				case QualityType.WeightAdult_a:
					return "421a";
				case QualityType.WeightAdult_b:
					return "421b";
				default:
					throw new ApplicationException("Type not found: "+qtype.ToString());
			}
		}

		private static string GetDescript(QualityType qtype) {
			switch(qtype) {
				case QualityType.WeightAdult_a:
					return "Weight, Adult, 65 or older";
				case QualityType.WeightAdult_b:
					return "Weight, Adult, 18 to 64 years old";
				default:
					throw new ApplicationException("Type not found: "+qtype.ToString());
			}
		}

		public static DataTable GetTable(QualityType qtype,DateTime dateStart,DateTime dateEnd) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetTable(MethodBase.GetCurrentMethod(),qtype,dateStart,dateEnd);
			}
			string command="";
			switch(qtype) {
				case QualityType.WeightAdult_a:
					/*
					command="SELECT PatNum,LName,FName, "
						+"(SELECT COUNT(*) FROM disease WHERE PatNum=patient.PatNum AND DiseaseDefNum="
							+POut.Long(PrefC.GetLong(PrefName.ProblemsIndicateNone))+") AS problemsNone, "
						+"(SELECT COUNT(*) FROM disease WHERE PatNum=patient.PatNum) AS problemsAll "
						+"FROM patient "
						+"WHERE EXISTS(SELECT * FROM procedurelog WHERE patient.PatNum=procedurelog.PatNum "
						+"AND procedurelog.ProcStatus=2 "//complete
						+"AND procedurelog.ProcDate >= "+POut.Date(dateStart)+" "
						+"AND procedurelog.ProcDate <= "+POut.Date(dateEnd)+")";*/
					break;
				case QualityType.WeightAdult_b:
					command="";
					break;
				default:
					throw new ApplicationException("Type not found: "+qtype.ToString());
			}
			DataTable tableRaw=null;
			if(command=="") {
				tableRaw=new DataTable();
			}
			else {
				tableRaw=Db.GetTable(command);
			}
			//PatNum, PatientName, Numerator(X), and Exclusion(X).
			DataTable table=new DataTable("audit");
			DataRow row;
			table.Columns.Add("PatNum");
			table.Columns.Add("patientName");
			table.Columns.Add("numerator");
			table.Columns.Add("exclusion");
			List<DataRow> rows=new List<DataRow>();
			Patient pat;
			string explanation;
			for(int i=0;i<tableRaw.Rows.Count;i++) {
				row=table.NewRow();
				row["PatNum"]=tableRaw.Rows[i]["PatNum"].ToString();
				pat=new Patient();
				pat.LName=tableRaw.Rows[i]["LName"].ToString();
				pat.FName=tableRaw.Rows[i]["FName"].ToString();
				pat.Preferred="";
				row["patientName"]=pat.GetNameLF();
				row["numerator"]="";
				row["exclusion"]="";
				switch(qtype) {
					case QualityType.WeightAdult_a:
						/*
						if(tableRaw.Rows[i]["problemsNone"].ToString()!="0") {
							explanation="Problems indicated 'None'";
							row["met"]="X";
						}
						else if(tableRaw.Rows[i]["problemsAll"].ToString()!="0") {
							explanation="Problems entered: "+tableRaw.Rows[i]["problemsAll"].ToString();
							row["met"]="X";
						}
						else {
							explanation="No Problems entered";
						}*/
						break;
					case QualityType.WeightAdult_b:
						
						break;
					default:
						throw new ApplicationException("Type not found: "+qtype.ToString());
				}
				rows.Add(row);
			}
			for(int i=0;i<rows.Count;i++) {
				table.Rows.Add(rows[i]);
			}
			return table;
		}

		///<summary>Just counts up the number of rows with an X in the numerator column.  Very simple.</summary>
		public static int CalcNumerator(DataTable table) {
			//No need to check RemotingRole; no call to db.
			int retVal=0;
			for(int i=0;i<table.Rows.Count;i++) {
				if(table.Rows[i]["numerator"].ToString()=="X") {
					retVal++;
				}
			}
			return retVal;
		}

		///<summary>Just counts up the number of rows with an X in the exclusion column.  Very simple.</summary>
		public static int CalcExclusions(DataTable table) {
			//No need to check RemotingRole; no call to db.
			int retVal=0;
			for(int i=0;i<table.Rows.Count;i++) {
				if(table.Rows[i]["exclusion"].ToString()=="X") {
					retVal++;
				}
			}
			return retVal;
		}


	}
}
