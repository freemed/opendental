using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace OpenDentBusiness {
	///<summary>Used in Ehr patient lists.</summary>
	public class EhrPatListElements {
		public static DataTable GetListOrderBy(List<EhrPatListElement> elementList,bool isAsc) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetTable(MethodBase.GetCurrentMethod(),elementList,isAsc);
			}
			DataTable table=new DataTable();
			string command="DROP TABLE IF EXISTS tempehrlist";
			Db.NonQ(command);
			command="CREATE TABLE tempehrlist SELECT patient.PatNum,patient.LName,patient.FName";
			for(int i=0;i<elementList.Count;i++) {
				string compStr=elementList[i].CompareString;
				switch(elementList[i].Restriction) {
					case EhrRestrictionType.Birthdate:
						command+=",patient.Birthdate ";
						break;
					case EhrRestrictionType.Problem:
						command+=",(SELECT disease.ICD9Num FROM disease WHERE disease.PatNum=patient.PatNum AND disease.ICD9Num IN (SELECT ICD9Num FROM icd9 WHERE ICD9Code LIKE '"+compStr+"%')) `"+compStr+"` ";
						break;
					case EhrRestrictionType.LabResult:
						command+=",(SELECT IFNULL(MAX(ObsValue),0) FROM labresult,labpanel WHERE labresult.LabPanelNum=labpanel.LabPanelNum AND labpanel.PatNum=patient.PatNum AND labresult.TestName='"+compStr+"') `"+compStr+"` ";
						break;
					case EhrRestrictionType.Medication:
						command+=",(SELECT COUNT(*) FROM medication,medicationpat WHERE medicationpat.PatNum=patient.PatNum AND medication.MedicationNum=medicationpat.MedicationNum AND medication.MedName LIKE '%"+compStr+"%') `"+compStr+"` ";
						break;
					case EhrRestrictionType.Gender:
						command+=",patient.Gender ";
						break;
				}
			}
			command+="FROM patient";
			Db.NonQ(command);
			string order="";
			command="SELECT * FROM tempehrlist ";
			for(int i=0;i<elementList.Count;i++) {
				if(i<1) {
					command+="WHERE "+GetFilteringText(elementList[i]);
				}
				else {
					command+="AND "+GetFilteringText(elementList[i]);
				}
				if(elementList[i].OrderBy) {
					if(elementList[i].Restriction==EhrRestrictionType.Birthdate) {
						order="ORDER BY Birthdate"+GetOrderBy(isAsc);
					}
					else if(elementList[i].Restriction==EhrRestrictionType.Gender) {
						order="ORDER BY Gender"+GetOrderBy(isAsc);
					}
					else {
						order="ORDER BY `"+POut.String(elementList[i].CompareString)+"`"+GetOrderBy(isAsc);
					}
				}
			}
			command+=order;
			table=Db.GetTable(command);
			command="DROP TABLE IF EXISTS tempehrlist";
			Db.NonQ(command);
			return table;
		}

		private static string GetOrderBy(bool isAsc) {
			if(isAsc) {
				return " ASC";
			}
			return " DESC";
		}

		///<summary>Returns lt, gt, or equals</summary>
		private static string GetOperandText(EhrOperand ehrOp){
			string operand="";
			switch(ehrOp) {
				case EhrOperand.Equals:
					operand="=";
					break;
				case EhrOperand.GreaterThan:
					operand=">";
					break;
				case EhrOperand.LessThan:
					operand="<";
					break;
			}
			return operand;
		}

		///<summary>Returns text used in WHERE clause of query for tempehrlist.</summary>
		private static string GetFilteringText(EhrPatListElement element) {
			string filter="";
			string compStr=POut.String(element.CompareString);
			string labStr=POut.String(element.LabValue);
			switch(element.Restriction) {
				case EhrRestrictionType.Birthdate:
					filter="DATE_SUB(CURDATE(),INTERVAL "+compStr+" YEAR)"+GetOperandText(element.Operand)+"Birthdate ";
					break;
				case EhrRestrictionType.Problem:
					filter="`"+compStr+"`"+" IS NOT NULL ";//Has the disease.
					break;
				case EhrRestrictionType.LabResult:
					filter="`"+compStr+"`"+GetOperandText(element.Operand)+labStr+" ";
					break;
				case EhrRestrictionType.Medication:
					filter="`"+compStr+"`"+">0 ";//Count greater than 0 (is taking the med).
					break;
				case EhrRestrictionType.Gender:
					filter="Gender>-1 ";//Just so WHERE clause won't fail.
					break;
			}
			return filter;
		}
	}
}
