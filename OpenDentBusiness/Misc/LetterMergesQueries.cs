using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace OpenDentBusiness {
	public class LetterMergesQueries {

		public static DataTable GetLetterMergeInfo(Patient PatCur,LetterMerge letter){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetTable(MethodBase.GetCurrentMethod(),PatCur,letter);
			}
			//jsparks- This is messy and prone to bugs.  It needs to be reworked to work just like
			//in SheetFiller.FillFieldsInStaticText.  Just grab a bunch of separate objects
			//instead of one result row.
			string command;
			//We need a very small table that tells us which tp is the most recent.
			//command="DROP TABLE IF EXISTS temptp;";
			//Db.NonQ(command);
			//command=@"CREATE TABLE temptp(
			//	DateTP date NOT NULL default '0001-01-01')";
			//Db.NonQ(command);
			//command+=@"CREATE TABLE temptp
			//	SELECT MAX(treatplan.DateTP) DateTP
			//	FROM treatplan
			//	WHERE PatNum="+POut.PInt(PatCur.PatNum)+";";
			//Db.NonQ(command);
			command="SET @maxTpDate=(SELECT MAX(treatplan.DateTP) FROM treatplan WHERE PatNum="+POut.Long(PatCur.PatNum)+");";
			command+="SELECT ";
			for(int i=0;i<letter.Fields.Count;i++) {
				if(i>0) {
					command+=",";
				}
				if(letter.Fields[i]=="NextAptNum") {
					command+="plannedappt.AptNum NextAptNum";
				}
					//other:
				else if(letter.Fields[i]=="TPResponsPartyNameFL") {
					command+="CONCAT(patResp.FName,' ',patResp.LName) TPResponsPartyNameFL";
				} 
				else if(letter.Fields[i]=="TPResponsPartyAddress") {
					command+="patResp.Address TPResponsPartyAddress";
				} 
				else if(letter.Fields[i]=="TPResponsPartyCityStZip") {
					command+="CONCAT(patResp.City,', ',patResp.State,' ',patResp.Zip) TPResponsPartyCityStZip";
				} 
				else if(letter.Fields[i]=="SiteDescription") {
					command+="site.Description SiteDescription";
				} 
				else if(letter.Fields[i]=="DateOfLastSavedTP") {
					command+="DATE(treatplan.DateTP) DateOfLastSavedTP";
				} 
				else if(letter.Fields[i]=="DateRecallDue") {
					command+="recall.DateDue  DateRecallDue";
				} 
				else if(letter.Fields[i]=="CarrierName") {
					command+="CarrierName";
				} 
				else if(letter.Fields[i]=="CarrierAddress") {
					command+="carrier.Address CarrierAddress";
				} 
				else if(letter.Fields[i]=="CarrierCityStZip") {
					command+="CONCAT(carrier.City,', ',carrier.State,' ',carrier.Zip) CarrierCityStZip";
				} 
				else if(letter.Fields[i]=="SubscriberNameFL") {
					command+="CONCAT(patSubsc.FName,' ',patSubsc.LName) SubscriberNameFL";
				} 
				else if(letter.Fields[i]=="SubscriberID") {
					command+="insplan.SubscriberID";
				} 
				else if(letter.Fields[i]=="NextSchedAppt") {
					command+="MIN(appointment.AptDateTime) NextSchedAppt";
				}
				else if(letter.Fields[i]=="Age") {
					command+="patient.Birthdate BirthdateForAge";
				}
				else if(letter.Fields[i].StartsWith("referral.")) {
					command+="referral."+letter.Fields[i].Substring(9);
				}
				else {
					command+="patient."+letter.Fields[i];
				}
			}
			command+=" FROM patient "
				+"LEFT JOIN refattach ON patient.PatNum=refattach.PatNum AND refattach.IsFrom=1 "
				+"LEFT JOIN referral ON refattach.ReferralNum=referral.ReferralNum "
				+"LEFT JOIN plannedappt ON plannedappt.PatNum=patient.PatNum AND plannedappt.ItemOrder=1 "
				+"LEFT JOIN site ON patient.SiteNum=site.SiteNum "
				+"LEFT JOIN treatplan ON patient.PatNum=treatplan.PatNum AND DateTP=@maxTpDate "
				+"LEFT JOIN patient patResp ON treatplan.ResponsParty=patResp.PatNum "
				+"LEFT JOIN recall ON recall.PatNum=patient.PatNum "
					+"AND (recall.RecallTypeNum="+POut.Long(PrefC.GetLong(PrefName.RecallTypeSpecialProphy))
					+" OR recall.RecallTypeNum="+POut.Long(PrefC.GetLong(PrefName.RecallTypeSpecialPerio))+") "
				+"LEFT JOIN patplan ON patplan.PatNum=patient.PatNum AND Ordinal=1 "
				+"LEFT JOIN insplan ON patplan.PlanNum=insplan.PlanNum "
				+"LEFT JOIN carrier ON carrier.CarrierNum=insplan.CarrierNum "
				+"LEFT JOIN patient patSubsc ON patSubsc.PatNum=insplan.Subscriber "
				+"LEFT JOIN appointment ON appointment.PatNum=patient.PatNum "
					+"AND AptStatus="+POut.Long((int)ApptStatus.Scheduled)+" "
					+"AND AptDateTime > NOW() "
				+"WHERE patient.PatNum="+POut.Long(PatCur.PatNum)
				+" GROUP BY patient.PatNum "
				+"ORDER BY refattach.ItemOrder";
			return Db.GetTable(command);
		}

	}
}
