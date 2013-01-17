package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.opendental.odweb.client.remoting.Serializing;
import com.google.gwt.i18n.client.DateTimeFormat;
import java.util.Date;

//DO NOT MAKE CHANGES TO THIS FILE.  THEY WILL GET OVERWRITTEN BY THE CRUD.
public class SecurityLog {
		/** Primary key. */
		public int SecurityLogNum;
		/** Enum:Permissions */
		public Permissions PermType;
		/** FK to user.UserNum */
		public int UserNum;
		/** The date and time of the entry.  It's value is set when inserting and can never change.  Even if a user changes the date on their ocmputer, this remains accurate because it uses server time. */
		public Date LogDateTime;
		/** The description of exactly what was done. Varies by permission type. */
		public String LogText;
		/** FK to patient.PatNum.  Can be 0 if not applicable. */
		public int PatNum;
		/** . */
		public String CompName;
		/** PatNum-NameLF */
		public String PatientName;
		/** FK to relevant table.  Not implemented yet.  Table will be based on PermType. */
		public int FKey;

		/** Deep copy of object. */
		public SecurityLog deepCopy() {
			SecurityLog securitylog=new SecurityLog();
			securitylog.SecurityLogNum=this.SecurityLogNum;
			securitylog.PermType=this.PermType;
			securitylog.UserNum=this.UserNum;
			securitylog.LogDateTime=this.LogDateTime;
			securitylog.LogText=this.LogText;
			securitylog.PatNum=this.PatNum;
			securitylog.CompName=this.CompName;
			securitylog.PatientName=this.PatientName;
			securitylog.FKey=this.FKey;
			return securitylog;
		}

		/** Serialize the object into XML. */
		public String serialize() {
			StringBuilder sb=new StringBuilder();
			sb.append("<SecurityLog>");
			sb.append("<SecurityLogNum>").append(SecurityLogNum).append("</SecurityLogNum>");
			sb.append("<PermType>").append(PermType.ordinal()).append("</PermType>");
			sb.append("<UserNum>").append(UserNum).append("</UserNum>");
			sb.append("<LogDateTime>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(LogDateTime)).append("</LogDateTime>");
			sb.append("<LogText>").append(Serializing.escapeForXml(LogText)).append("</LogText>");
			sb.append("<PatNum>").append(PatNum).append("</PatNum>");
			sb.append("<CompName>").append(Serializing.escapeForXml(CompName)).append("</CompName>");
			sb.append("<PatientName>").append(Serializing.escapeForXml(PatientName)).append("</PatientName>");
			sb.append("<FKey>").append(FKey).append("</FKey>");
			sb.append("</SecurityLog>");
			return sb.toString();
		}

		/** Sets all the variables on this object based on the values in the XML document.  Variables that are not in the XML document will be null or their default values.
		 * @param doc A parsed XML document.  Must be valid XML.  Does not need to contain a node for every variable on this object.
		 * @throws Exception DeserializeFromXml is entirely encased in a try catch and will throw exceptions if anything goes wrong. */
		public void deserialize(Document doc) throws Exception {
			try {
				if(Serializing.getXmlNodeValue(doc,"SecurityLogNum")!=null) {
					SecurityLogNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"SecurityLogNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"PermType")!=null) {
					PermType=Permissions.values()[Integer.valueOf(Serializing.getXmlNodeValue(doc,"PermType"))];
				}
				if(Serializing.getXmlNodeValue(doc,"UserNum")!=null) {
					UserNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"UserNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"LogDateTime")!=null) {
					LogDateTime=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(Serializing.getXmlNodeValue(doc,"LogDateTime"));
				}
				if(Serializing.getXmlNodeValue(doc,"LogText")!=null) {
					LogText=Serializing.getXmlNodeValue(doc,"LogText");
				}
				if(Serializing.getXmlNodeValue(doc,"PatNum")!=null) {
					PatNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"PatNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"CompName")!=null) {
					CompName=Serializing.getXmlNodeValue(doc,"CompName");
				}
				if(Serializing.getXmlNodeValue(doc,"PatientName")!=null) {
					PatientName=Serializing.getXmlNodeValue(doc,"PatientName");
				}
				if(Serializing.getXmlNodeValue(doc,"FKey")!=null) {
					FKey=Integer.valueOf(Serializing.getXmlNodeValue(doc,"FKey"));
				}
			}
			catch(Exception e) {
				throw new Exception("Error deserializing SecurityLog: "+e.getMessage());
			}
		}

		/** A hard-coded list of permissions which may be granted to usergroups. */
		public enum Permissions {
			/** 0 */
			None,
			/** 1 */
			AppointmentsModule,
			/** 2 */
			FamilyModule,
			/** 3 */
			AccountModule,
			/** 4 */
			TPModule,
			/** 5 */
			ChartModule,
			/** 6 */
			ImagesModule,
			/** 7 */
			ManageModule,
			/** 8. Currently covers a wide variety of setup functions.  */
			Setup,
			/** 9 */
			RxCreate,
			/** 10. Uses date restrictions.  Covers editing AND deleting of completed procs.  Deleting non-completed procs is covered by ProcDelete. */
			ProcComplEdit,
			/** 11 */
			ChooseDatabase,
			/** 12 */
			Schedules,
			/** 13 */
			Blockouts,
			/** 14. Uses date restrictions. */
			ClaimSentEdit,
			/** 15 */
			PaymentCreate,
			/** 16. Uses date restrictions. */
			PaymentEdit,
			/** 17 */
			AdjustmentCreate,
			/** 18. Uses date restrictions. */
			AdjustmentEdit,
			/** 19 */
			UserQuery,
			/** 20.  Not used anymore. */
			StartupSingleUserOld,
			/** 21 Not used anymore. */
			StartupMultiUserOld,
			/** 22 */
			Reports,
			/** 23. Includes setting procedures complete. */
			ProcComplCreate,
			/** 24. At least one user must have this permission. */
			SecurityAdmin,
			/** 25.  */
			AppointmentCreate,
			/** 26 */
			AppointmentMove,
			/** 27 */
			AppointmentEdit,
			/** 28 */
			Backup,
			/** 29 */
			TimecardsEditAll,
			/** 30 */
			DepositSlips,
			/** 31. Uses date restrictions. */
			AccountingEdit,
			/** 32. Uses date restrictions. */
			AccountingCreate,
			/** 33 */
			Accounting,
			/** 34 */
			AnesthesiaIntakeMeds,
			/** 35 */
			AnesthesiaControlMeds,
			/** 36 */
			InsPayCreate,
			/** 37. Uses date restrictions. Also includes completed claimprocs even if unattached to an insurance check.  However, it's not actually enforced when creating a check because it would be very complex. */
			InsPayEdit,
			/** 38. Uses date restrictions. */
			TreatPlanEdit,
			/** 39 */
			ReportProdInc,
			/** 40. Uses date restrictions. */
			TimecardDeleteEntry,
			/** 41. Uses date restrictions. All other equipment functions are covered by .Setup. */
			EquipmentDelete,
			/** 42. Uses date restrictions. Also used in audit trail to log web form importing. */
			SheetEdit,
			/** 43. Uses date restrictions. */
			CommlogEdit,
			/** 44. Uses date restrictions. */
			ImageDelete,
			/** 45. Uses date restrictions. */
			PerioEdit,
			/** 46 */
			ProcEditShowFee,
			/** 47 */
			AdjustmentEditZero,
			/** 48 */
			EhrEmergencyAccess,
			/** 49. Uses date restrictions.  This only applies to non-completed procs.  Deletion of completed procs is covered by ProcComplEdit. */
			ProcDelete,
			/** 50 - Only used at OD HQ.  No user interface. */
			EhrKeyAdd,
			/** 51 */
			Providers,
			/** 52 */
			EcwAppointmentRevise,
			/** 53 */
			ProcedureNote,
			/** 54 */
			ReferralAdd,
			/** 55 */
			InsPlanChangeSubsc,
			/** 56 */
			RefAttachAdd,
			/** 57 */
			RefAttachDelete,
			/** 58 */
			CarrierCreate,
			/** 59 */
			ReportDashboard,
			/** 60 */
			AutoNoteQuickNoteEdit,
			/** 61 */
			EquipmentSetup,
			/** 62 */
			Billing,
			/** 63 */
			ProblemEdit
		}


}
