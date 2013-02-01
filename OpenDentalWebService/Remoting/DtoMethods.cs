using System;
using System.Collections.Generic;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class DtoMethods {
		///<summary>Processes any type of data transfer object by calling the desired method.</summary>
		public static object ProcessDtoObject(DataTransferObject dto) {
			string classAndMethod=dto.MethodName;
			List<object> parameters=new List<object>();
			for(int i=0;i<dto.Params.Count;i++) {
				parameters.Add(dto.Params[i].Obj);
			}
			return CallMethod(classAndMethod,parameters);
		}

		///<summary>Calls the class serializer for any supported object, primitive or not.  objectType must be fully qualified unless it's a primitive, then either way is fine.  Ex: Sytem.In32 or int or OpenDentBusiness.Account or List&lt;OpenDentBusiness.Account&gt;.  Throws exceptions if the object or class is not supported yet.</summary>
		public static string CallClassSerializer(string objectType,Object obj) {
			#region Primitive and General Types
			//To add more primitive/general types go to method xCrudGeneratorWebService.Form1.GetPrimGenSerializerTypes and manually add it there.
			switch(objectType) {
				case "int":
				case "System.Int32":
				case "long":
				case "System.Int64":
				case "bool":
				case "System.Boolean":
				case "string":
				case "System.String":
				case "char":
				case "System.Char":
				case "Single":
				case "System.Single":
				case "byte":
				case "System.Byte":
				case "double":
				case "System.Double":
				case "DataTable":
					return aaGeneralTypes.Serialize(objectType,obj);
			}
			if(objectType.StartsWith("List")) {//Lists.
				return aaGeneralTypes.SerializeList(objectType,obj);
			}
			if(objectType.Contains("[")) {//Arrays.
				return aaGeneralTypes.SerializeArray(objectType,obj);
			}
			#endregion
			#region Open Dental Classes
			if(objectType=="OpenDentBusiness.Account") {
				return Account.Serialize((OpenDentBusiness.Account)obj);
			}
			if(objectType=="OpenDentBusiness.AccountingAutoPay") {
				return AccountingAutoPay.Serialize((OpenDentBusiness.AccountingAutoPay)obj);
			}
			if(objectType=="OpenDentBusiness.Adjustment") {
				return Adjustment.Serialize((OpenDentBusiness.Adjustment)obj);
			}
			if(objectType=="OpenDentBusiness.Allergy") {
				return Allergy.Serialize((OpenDentBusiness.Allergy)obj);
			}
			if(objectType=="OpenDentBusiness.AllergyDef") {
				return AllergyDef.Serialize((OpenDentBusiness.AllergyDef)obj);
			}
			if(objectType=="OpenDentBusiness.Appointment") {
				return Appointment.Serialize((OpenDentBusiness.Appointment)obj);
			}
			if(objectType=="OpenDentBusiness.AppointmentRule") {
				return AppointmentRule.Serialize((OpenDentBusiness.AppointmentRule)obj);
			}
			if(objectType=="OpenDentBusiness.ApptField") {
				return ApptField.Serialize((OpenDentBusiness.ApptField)obj);
			}
			if(objectType=="OpenDentBusiness.ApptFieldDef") {
				return ApptFieldDef.Serialize((OpenDentBusiness.ApptFieldDef)obj);
			}
			if(objectType=="OpenDentBusiness.ApptView") {
				return ApptView.Serialize((OpenDentBusiness.ApptView)obj);
			}
			if(objectType=="OpenDentBusiness.ApptViewItem") {
				return ApptViewItem.Serialize((OpenDentBusiness.ApptViewItem)obj);
			}
			if(objectType=="OpenDentBusiness.AutoCode") {
				return AutoCode.Serialize((OpenDentBusiness.AutoCode)obj);
			}
			if(objectType=="OpenDentBusiness.AutoCodeCond") {
				return AutoCodeCond.Serialize((OpenDentBusiness.AutoCodeCond)obj);
			}
			if(objectType=="OpenDentBusiness.AutoCodeItem") {
				return AutoCodeItem.Serialize((OpenDentBusiness.AutoCodeItem)obj);
			}
			if(objectType=="OpenDentBusiness.Automation") {
				return Automation.Serialize((OpenDentBusiness.Automation)obj);
			}
			if(objectType=="OpenDentBusiness.AutomationCondition") {
				return AutomationCondition.Serialize((OpenDentBusiness.AutomationCondition)obj);
			}
			if(objectType=="OpenDentBusiness.AutoNote") {
				return AutoNote.Serialize((OpenDentBusiness.AutoNote)obj);
			}
			if(objectType=="OpenDentBusiness.AutoNoteControl") {
				return AutoNoteControl.Serialize((OpenDentBusiness.AutoNoteControl)obj);
			}
			if(objectType=="OpenDentBusiness.Benefit") {
				return Benefit.Serialize((OpenDentBusiness.Benefit)obj);
			}
			if(objectType=="OpenDentBusiness.CanadianNetwork") {
				return CanadianNetwork.Serialize((OpenDentBusiness.CanadianNetwork)obj);
			}
			if(objectType=="OpenDentBusiness.Carrier") {
				return Carrier.Serialize((OpenDentBusiness.Carrier)obj);
			}
			if(objectType=="OpenDentBusiness.CentralConnection") {
				return CentralConnection.Serialize((OpenDentBusiness.CentralConnection)obj);
			}
			if(objectType=="OpenDentBusiness.ChartView") {
				return ChartView.Serialize((OpenDentBusiness.ChartView)obj);
			}
			if(objectType=="OpenDentBusiness.Claim") {
				return Claim.Serialize((OpenDentBusiness.Claim)obj);
			}
			if(objectType=="OpenDentBusiness.ClaimAttach") {
				return ClaimAttach.Serialize((OpenDentBusiness.ClaimAttach)obj);
			}
			if(objectType=="OpenDentBusiness.ClaimCondCodeLog") {
				return ClaimCondCodeLog.Serialize((OpenDentBusiness.ClaimCondCodeLog)obj);
			}
			if(objectType=="OpenDentBusiness.ClaimForm") {
				return ClaimForm.Serialize((OpenDentBusiness.ClaimForm)obj);
			}
			if(objectType=="OpenDentBusiness.ClaimFormItem") {
				return ClaimFormItem.Serialize((OpenDentBusiness.ClaimFormItem)obj);
			}
			if(objectType=="OpenDentBusiness.ClaimPayment") {
				return ClaimPayment.Serialize((OpenDentBusiness.ClaimPayment)obj);
			}
			if(objectType=="OpenDentBusiness.ClaimProc") {
				return ClaimProc.Serialize((OpenDentBusiness.ClaimProc)obj);
			}
			if(objectType=="OpenDentBusiness.ClaimValCodeLog") {
				return ClaimValCodeLog.Serialize((OpenDentBusiness.ClaimValCodeLog)obj);
			}
			if(objectType=="OpenDentBusiness.Clearinghouse") {
				return Clearinghouse.Serialize((OpenDentBusiness.Clearinghouse)obj);
			}
			if(objectType=="OpenDentBusiness.Clinic") {
				return Clinic.Serialize((OpenDentBusiness.Clinic)obj);
			}
			if(objectType=="OpenDentBusiness.ClockEvent") {
				return ClockEvent.Serialize((OpenDentBusiness.ClockEvent)obj);
			}
			if(objectType=="OpenDentBusiness.Commlog") {
				return Commlog.Serialize((OpenDentBusiness.Commlog)obj);
			}
			if(objectType=="OpenDentBusiness.Computer") {
				return Computer.Serialize((OpenDentBusiness.Computer)obj);
			}
			if(objectType=="OpenDentBusiness.ComputerPref") {
				return ComputerPref.Serialize((OpenDentBusiness.ComputerPref)obj);
			}
			if(objectType=="OpenDentBusiness.Contact") {
				return Contact.Serialize((OpenDentBusiness.Contact)obj);
			}
			if(objectType=="OpenDentBusiness.County") {
				return County.Serialize((OpenDentBusiness.County)obj);
			}
			if(objectType=="OpenDentBusiness.CovCat") {
				return CovCat.Serialize((OpenDentBusiness.CovCat)obj);
			}
			if(objectType=="OpenDentBusiness.CovSpan") {
				return CovSpan.Serialize((OpenDentBusiness.CovSpan)obj);
			}
			if(objectType=="OpenDentBusiness.CreditCard") {
				return CreditCard.Serialize((OpenDentBusiness.CreditCard)obj);
			}
			if(objectType=="OpenDentBusiness.CustRefEntry") {
				return CustRefEntry.Serialize((OpenDentBusiness.CustRefEntry)obj);
			}
			if(objectType=="OpenDentBusiness.CustReference") {
				return CustReference.Serialize((OpenDentBusiness.CustReference)obj);
			}
			if(objectType=="OpenDentBusiness.DashboardAR") {
				return DashboardAR.Serialize((OpenDentBusiness.DashboardAR)obj);
			}
			if(objectType=="OpenDentBusiness.Def") {
				return Def.Serialize((OpenDentBusiness.Def)obj);
			}
			if(objectType=="OpenDentBusiness.DeletedObject") {
				return DeletedObject.Serialize((OpenDentBusiness.DeletedObject)obj);
			}
			if(objectType=="OpenDentBusiness.Deposit") {
				return Deposit.Serialize((OpenDentBusiness.Deposit)obj);
			}
			if(objectType=="OpenDentBusiness.DictCustom") {
				return DictCustom.Serialize((OpenDentBusiness.DictCustom)obj);
			}
			if(objectType=="OpenDentBusiness.Disease") {
				return Disease.Serialize((OpenDentBusiness.Disease)obj);
			}
			if(objectType=="OpenDentBusiness.DiseaseDef") {
				return DiseaseDef.Serialize((OpenDentBusiness.DiseaseDef)obj);
			}
			if(objectType=="OpenDentBusiness.DisplayField") {
				return DisplayField.Serialize((OpenDentBusiness.DisplayField)obj);
			}
			if(objectType=="OpenDentBusiness.Document") {
				return Document.Serialize((OpenDentBusiness.Document)obj);
			}
			if(objectType=="OpenDentBusiness.DocumentMisc") {
				return DocumentMisc.Serialize((OpenDentBusiness.DocumentMisc)obj);
			}
			if(objectType=="OpenDentBusiness.DrugManufacturer") {
				return DrugManufacturer.Serialize((OpenDentBusiness.DrugManufacturer)obj);
			}
			if(objectType=="OpenDentBusiness.DrugUnit") {
				return DrugUnit.Serialize((OpenDentBusiness.DrugUnit)obj);
			}
			if(objectType=="OpenDentBusiness.Dunning") {
				return Dunning.Serialize((OpenDentBusiness.Dunning)obj);
			}
			if(objectType=="OpenDentBusiness.EduResource") {
				return EduResource.Serialize((OpenDentBusiness.EduResource)obj);
			}
			if(objectType=="OpenDentBusiness.EhrMeasure") {
				return EhrMeasure.Serialize((OpenDentBusiness.EhrMeasure)obj);
			}
			if(objectType=="OpenDentBusiness.EhrMeasureEvent") {
				return EhrMeasureEvent.Serialize((OpenDentBusiness.EhrMeasureEvent)obj);
			}
			if(objectType=="OpenDentBusiness.EhrProvKey") {
				return EhrProvKey.Serialize((OpenDentBusiness.EhrProvKey)obj);
			}
			if(objectType=="OpenDentBusiness.EhrQuarterlyKey") {
				return EhrQuarterlyKey.Serialize((OpenDentBusiness.EhrQuarterlyKey)obj);
			}
			if(objectType=="OpenDentBusiness.EhrSummaryCcd") {
				return EhrSummaryCcd.Serialize((OpenDentBusiness.EhrSummaryCcd)obj);
			}
			if(objectType=="OpenDentBusiness.ElectID") {
				return ElectID.Serialize((OpenDentBusiness.ElectID)obj);
			}
			if(objectType=="OpenDentBusiness.EmailAddress") {
				return EmailAddress.Serialize((OpenDentBusiness.EmailAddress)obj);
			}
			if(objectType=="OpenDentBusiness.EmailAttach") {
				return EmailAttach.Serialize((OpenDentBusiness.EmailAttach)obj);
			}
			if(objectType=="OpenDentBusiness.EmailMessage") {
				return EmailMessage.Serialize((OpenDentBusiness.EmailMessage)obj);
			}
			if(objectType=="OpenDentBusiness.EmailTemplate") {
				return EmailTemplate.Serialize((OpenDentBusiness.EmailTemplate)obj);
			}
			if(objectType=="OpenDentBusiness.Employee") {
				return Employee.Serialize((OpenDentBusiness.Employee)obj);
			}
			if(objectType=="OpenDentBusiness.Employer") {
				return Employer.Serialize((OpenDentBusiness.Employer)obj);
			}
			if(objectType=="OpenDentBusiness.EobAttach") {
				return EobAttach.Serialize((OpenDentBusiness.EobAttach)obj);
			}
			if(objectType=="OpenDentBusiness.Equipment") {
				return Equipment.Serialize((OpenDentBusiness.Equipment)obj);
			}
			if(objectType=="OpenDentBusiness.ErxLog") {
				return ErxLog.Serialize((OpenDentBusiness.ErxLog)obj);
			}
			if(objectType=="OpenDentBusiness.Etrans") {
				return Etrans.Serialize((OpenDentBusiness.Etrans)obj);
			}
			if(objectType=="OpenDentBusiness.EtransMessageText") {
				return EtransMessageText.Serialize((OpenDentBusiness.EtransMessageText)obj);
			}
			if(objectType=="OpenDentBusiness.Fee") {
				return Fee.Serialize((OpenDentBusiness.Fee)obj);
			}
			if(objectType=="OpenDentBusiness.FeeSched") {
				return FeeSched.Serialize((OpenDentBusiness.FeeSched)obj);
			}
			if(objectType=="OpenDentBusiness.FormPat") {
				return FormPat.Serialize((OpenDentBusiness.FormPat)obj);
			}
			if(objectType=="OpenDentBusiness.Formulary") {
				return Formulary.Serialize((OpenDentBusiness.Formulary)obj);
			}
			if(objectType=="OpenDentBusiness.FormularyMed") {
				return FormularyMed.Serialize((OpenDentBusiness.FormularyMed)obj);
			}
			if(objectType=="OpenDentBusiness.GroupPermission") {
				return GroupPermission.Serialize((OpenDentBusiness.GroupPermission)obj);
			}
			if(objectType=="OpenDentBusiness.Guardian") {
				return Guardian.Serialize((OpenDentBusiness.Guardian)obj);
			}
			if(objectType=="OpenDentBusiness.HL7Def") {
				return HL7Def.Serialize((OpenDentBusiness.HL7Def)obj);
			}
			if(objectType=="OpenDentBusiness.HL7DefField") {
				return HL7DefField.Serialize((OpenDentBusiness.HL7DefField)obj);
			}
			if(objectType=="OpenDentBusiness.HL7DefMessage") {
				return HL7DefMessage.Serialize((OpenDentBusiness.HL7DefMessage)obj);
			}
			if(objectType=="OpenDentBusiness.HL7DefSegment") {
				return HL7DefSegment.Serialize((OpenDentBusiness.HL7DefSegment)obj);
			}
			if(objectType=="OpenDentBusiness.HL7Msg") {
				return HL7Msg.Serialize((OpenDentBusiness.HL7Msg)obj);
			}
			if(objectType=="OpenDentBusiness.ICD9") {
				return ICD9.Serialize((OpenDentBusiness.ICD9)obj);
			}
			if(objectType=="OpenDentBusiness.InsFilingCode") {
				return InsFilingCode.Serialize((OpenDentBusiness.InsFilingCode)obj);
			}
			if(objectType=="OpenDentBusiness.InsFilingCodeSubtype") {
				return InsFilingCodeSubtype.Serialize((OpenDentBusiness.InsFilingCodeSubtype)obj);
			}
			if(objectType=="OpenDentBusiness.InsPlan") {
				return InsPlan.Serialize((OpenDentBusiness.InsPlan)obj);
			}
			if(objectType=="OpenDentBusiness.InsSub") {
				return InsSub.Serialize((OpenDentBusiness.InsSub)obj);
			}
			if(objectType=="OpenDentBusiness.InstallmentPlan") {
				return InstallmentPlan.Serialize((OpenDentBusiness.InstallmentPlan)obj);
			}
			if(objectType=="OpenDentBusiness.JournalEntry") {
				return JournalEntry.Serialize((OpenDentBusiness.JournalEntry)obj);
			}
			if(objectType=="OpenDentBusiness.LabCase") {
				return LabCase.Serialize((OpenDentBusiness.LabCase)obj);
			}
			if(objectType=="OpenDentBusiness.Laboratory") {
				return Laboratory.Serialize((OpenDentBusiness.Laboratory)obj);
			}
			if(objectType=="OpenDentBusiness.LabPanel") {
				return LabPanel.Serialize((OpenDentBusiness.LabPanel)obj);
			}
			if(objectType=="OpenDentBusiness.LabResult") {
				return LabResult.Serialize((OpenDentBusiness.LabResult)obj);
			}
			if(objectType=="OpenDentBusiness.LabTurnaround") {
				return LabTurnaround.Serialize((OpenDentBusiness.LabTurnaround)obj);
			}
			if(objectType=="OpenDentBusiness.Language") {
				return Language.Serialize((OpenDentBusiness.Language)obj);
			}
			if(objectType=="OpenDentBusiness.LanguageForeign") {
				return LanguageForeign.Serialize((OpenDentBusiness.LanguageForeign)obj);
			}
			if(objectType=="OpenDentBusiness.Letter") {
				return Letter.Serialize((OpenDentBusiness.Letter)obj);
			}
			if(objectType=="OpenDentBusiness.LetterMerge") {
				return LetterMerge.Serialize((OpenDentBusiness.LetterMerge)obj);
			}
			if(objectType=="OpenDentBusiness.LetterMergeField") {
				return LetterMergeField.Serialize((OpenDentBusiness.LetterMergeField)obj);
			}
			if(objectType=="OpenDentBusiness.MedicalOrder") {
				return MedicalOrder.Serialize((OpenDentBusiness.MedicalOrder)obj);
			}
			if(objectType=="OpenDentBusiness.Medication") {
				return Medication.Serialize((OpenDentBusiness.Medication)obj);
			}
			if(objectType=="OpenDentBusiness.MedicationPat") {
				return MedicationPat.Serialize((OpenDentBusiness.MedicationPat)obj);
			}
			if(objectType=="OpenDentBusiness.Mount") {
				return Mount.Serialize((OpenDentBusiness.Mount)obj);
			}
			if(objectType=="OpenDentBusiness.MountDef") {
				return MountDef.Serialize((OpenDentBusiness.MountDef)obj);
			}
			if(objectType=="OpenDentBusiness.MountItem") {
				return MountItem.Serialize((OpenDentBusiness.MountItem)obj);
			}
			if(objectType=="OpenDentBusiness.MountItemDef") {
				return MountItemDef.Serialize((OpenDentBusiness.MountItemDef)obj);
			}
			if(objectType=="OpenDentBusiness.Operatory") {
				return Operatory.Serialize((OpenDentBusiness.Operatory)obj);
			}
			if(objectType=="OpenDentBusiness.OrionProc") {
				return OrionProc.Serialize((OpenDentBusiness.OrionProc)obj);
			}
			if(objectType=="OpenDentBusiness.OrthoChart") {
				return OrthoChart.Serialize((OpenDentBusiness.OrthoChart)obj);
			}
			if(objectType=="OpenDentBusiness.PatField") {
				return PatField.Serialize((OpenDentBusiness.PatField)obj);
			}
			if(objectType=="OpenDentBusiness.PatFieldDef") {
				return PatFieldDef.Serialize((OpenDentBusiness.PatFieldDef)obj);
			}
			if(objectType=="OpenDentBusiness.Patient") {
				return Patient.Serialize((OpenDentBusiness.Patient)obj);
			}
			if(objectType=="OpenDentBusiness.PatientNote") {
				return PatientNote.Serialize((OpenDentBusiness.PatientNote)obj);
			}
			if(objectType=="OpenDentBusiness.PatPlan") {
				return PatPlan.Serialize((OpenDentBusiness.PatPlan)obj);
			}
			if(objectType=="OpenDentBusiness.Payment") {
				return Payment.Serialize((OpenDentBusiness.Payment)obj);
			}
			if(objectType=="OpenDentBusiness.PayPeriod") {
				return PayPeriod.Serialize((OpenDentBusiness.PayPeriod)obj);
			}
			if(objectType=="OpenDentBusiness.PayPlan") {
				return PayPlan.Serialize((OpenDentBusiness.PayPlan)obj);
			}
			if(objectType=="OpenDentBusiness.PayPlanCharge") {
				return PayPlanCharge.Serialize((OpenDentBusiness.PayPlanCharge)obj);
			}
			if(objectType=="OpenDentBusiness.PaySplit") {
				return PaySplit.Serialize((OpenDentBusiness.PaySplit)obj);
			}
			if(objectType=="OpenDentBusiness.PerioExam") {
				return PerioExam.Serialize((OpenDentBusiness.PerioExam)obj);
			}
			if(objectType=="OpenDentBusiness.PerioMeasure") {
				return PerioMeasure.Serialize((OpenDentBusiness.PerioMeasure)obj);
			}
			if(objectType=="OpenDentBusiness.Pharmacy") {
				return Pharmacy.Serialize((OpenDentBusiness.Pharmacy)obj);
			}
			if(objectType=="OpenDentBusiness.Phone") {
				return Phone.Serialize((OpenDentBusiness.Phone)obj);
			}
			if(objectType=="OpenDentBusiness.PhoneEmpDefault") {
				return PhoneEmpDefault.Serialize((OpenDentBusiness.PhoneEmpDefault)obj);
			}
			if(objectType=="OpenDentBusiness.PhoneMetric") {
				return PhoneMetric.Serialize((OpenDentBusiness.PhoneMetric)obj);
			}
			if(objectType=="OpenDentBusiness.PhoneNumber") {
				return PhoneNumber.Serialize((OpenDentBusiness.PhoneNumber)obj);
			}
			if(objectType=="OpenDentBusiness.PlannedAppt") {
				return PlannedAppt.Serialize((OpenDentBusiness.PlannedAppt)obj);
			}
			if(objectType=="OpenDentBusiness.Popup") {
				return Popup.Serialize((OpenDentBusiness.Popup)obj);
			}
			if(objectType=="OpenDentBusiness.Pref") {
				return Pref.Serialize((OpenDentBusiness.Pref)obj);
			}
			if(objectType=="OpenDentBusiness.Printer") {
				return Printer.Serialize((OpenDentBusiness.Printer)obj);
			}
			if(objectType=="OpenDentBusiness.ProcApptColor") {
				return ProcApptColor.Serialize((OpenDentBusiness.ProcApptColor)obj);
			}
			if(objectType=="OpenDentBusiness.ProcButton") {
				return ProcButton.Serialize((OpenDentBusiness.ProcButton)obj);
			}
			if(objectType=="OpenDentBusiness.ProcButtonItem") {
				return ProcButtonItem.Serialize((OpenDentBusiness.ProcButtonItem)obj);
			}
			if(objectType=="OpenDentBusiness.ProcCodeNote") {
				return ProcCodeNote.Serialize((OpenDentBusiness.ProcCodeNote)obj);
			}
			if(objectType=="OpenDentBusiness.Procedure") {
				return Procedure.Serialize((OpenDentBusiness.Procedure)obj);
			}
			if(objectType=="OpenDentBusiness.ProcedureCode") {
				return ProcedureCode.Serialize((OpenDentBusiness.ProcedureCode)obj);
			}
			if(objectType=="OpenDentBusiness.ProcGroupItem") {
				return ProcGroupItem.Serialize((OpenDentBusiness.ProcGroupItem)obj);
			}
			if(objectType=="OpenDentBusiness.ProcNote") {
				return ProcNote.Serialize((OpenDentBusiness.ProcNote)obj);
			}
			if(objectType=="OpenDentBusiness.ProcTP") {
				return ProcTP.Serialize((OpenDentBusiness.ProcTP)obj);
			}
			if(objectType=="OpenDentBusiness.Program") {
				return Program.Serialize((OpenDentBusiness.Program)obj);
			}
			if(objectType=="OpenDentBusiness.ProgramProperty") {
				return ProgramProperty.Serialize((OpenDentBusiness.ProgramProperty)obj);
			}
			if(objectType=="OpenDentBusiness.Provider") {
				return Provider.Serialize((OpenDentBusiness.Provider)obj);
			}
			if(objectType=="OpenDentBusiness.ProviderIdent") {
				return ProviderIdent.Serialize((OpenDentBusiness.ProviderIdent)obj);
			}
			if(objectType=="OpenDentBusiness.Question") {
				return Question.Serialize((OpenDentBusiness.Question)obj);
			}
			if(objectType=="OpenDentBusiness.QuestionDef") {
				return QuestionDef.Serialize((OpenDentBusiness.QuestionDef)obj);
			}
			if(objectType=="OpenDentBusiness.QuickPasteCat") {
				return QuickPasteCat.Serialize((OpenDentBusiness.QuickPasteCat)obj);
			}
			if(objectType=="OpenDentBusiness.QuickPasteNote") {
				return QuickPasteNote.Serialize((OpenDentBusiness.QuickPasteNote)obj);
			}
			if(objectType=="OpenDentBusiness.Recall") {
				return Recall.Serialize((OpenDentBusiness.Recall)obj);
			}
			if(objectType=="OpenDentBusiness.RecallTrigger") {
				return RecallTrigger.Serialize((OpenDentBusiness.RecallTrigger)obj);
			}
			if(objectType=="OpenDentBusiness.RecallType") {
				return RecallType.Serialize((OpenDentBusiness.RecallType)obj);
			}
			if(objectType=="OpenDentBusiness.Reconcile") {
				return Reconcile.Serialize((OpenDentBusiness.Reconcile)obj);
			}
			if(objectType=="OpenDentBusiness.RefAttach") {
				return RefAttach.Serialize((OpenDentBusiness.RefAttach)obj);
			}
			if(objectType=="OpenDentBusiness.Referral") {
				return Referral.Serialize((OpenDentBusiness.Referral)obj);
			}
			if(objectType=="OpenDentBusiness.RegistrationKey") {
				return RegistrationKey.Serialize((OpenDentBusiness.RegistrationKey)obj);
			}
			if(objectType=="OpenDentBusiness.ReminderRule") {
				return ReminderRule.Serialize((OpenDentBusiness.ReminderRule)obj);
			}
			if(objectType=="OpenDentBusiness.RepeatCharge") {
				return RepeatCharge.Serialize((OpenDentBusiness.RepeatCharge)obj);
			}
			if(objectType=="OpenDentBusiness.ReplicationServer") {
				return ReplicationServer.Serialize((OpenDentBusiness.ReplicationServer)obj);
			}
			if(objectType=="OpenDentBusiness.ReqNeeded") {
				return ReqNeeded.Serialize((OpenDentBusiness.ReqNeeded)obj);
			}
			if(objectType=="OpenDentBusiness.ReqStudent") {
				return ReqStudent.Serialize((OpenDentBusiness.ReqStudent)obj);
			}
			if(objectType=="OpenDentBusiness.RxAlert") {
				return RxAlert.Serialize((OpenDentBusiness.RxAlert)obj);
			}
			if(objectType=="OpenDentBusiness.RxDef") {
				return RxDef.Serialize((OpenDentBusiness.RxDef)obj);
			}
			if(objectType=="OpenDentBusiness.RxNorm") {
				return RxNorm.Serialize((OpenDentBusiness.RxNorm)obj);
			}
			if(objectType=="OpenDentBusiness.RxPat") {
				return RxPat.Serialize((OpenDentBusiness.RxPat)obj);
			}
			if(objectType=="OpenDentBusiness.Schedule") {
				return Schedule.Serialize((OpenDentBusiness.Schedule)obj);
			}
			if(objectType=="OpenDentBusiness.ScheduleOp") {
				return ScheduleOp.Serialize((OpenDentBusiness.ScheduleOp)obj);
			}
			if(objectType=="OpenDentBusiness.SchoolClass") {
				return SchoolClass.Serialize((OpenDentBusiness.SchoolClass)obj);
			}
			if(objectType=="OpenDentBusiness.SchoolCourse") {
				return SchoolCourse.Serialize((OpenDentBusiness.SchoolCourse)obj);
			}
			if(objectType=="OpenDentBusiness.Screen") {
				return Screen.Serialize((OpenDentBusiness.Screen)obj);
			}
			if(objectType=="OpenDentBusiness.ScreenGroup") {
				return ScreenGroup.Serialize((OpenDentBusiness.ScreenGroup)obj);
			}
			if(objectType=="OpenDentBusiness.ScreenPat") {
				return ScreenPat.Serialize((OpenDentBusiness.ScreenPat)obj);
			}
			if(objectType=="OpenDentBusiness.SecurityLog") {
				return SecurityLog.Serialize((OpenDentBusiness.SecurityLog)obj);
			}
			if(objectType=="OpenDentBusiness.Sheet") {
				return Sheet.Serialize((OpenDentBusiness.Sheet)obj);
			}
			if(objectType=="OpenDentBusiness.SheetDef") {
				return SheetDef.Serialize((OpenDentBusiness.SheetDef)obj);
			}
			if(objectType=="OpenDentBusiness.SheetField") {
				return SheetField.Serialize((OpenDentBusiness.SheetField)obj);
			}
			if(objectType=="OpenDentBusiness.SheetFieldDef") {
				return SheetFieldDef.Serialize((OpenDentBusiness.SheetFieldDef)obj);
			}
			if(objectType=="OpenDentBusiness.SigButDef") {
				return SigButDef.Serialize((OpenDentBusiness.SigButDef)obj);
			}
			if(objectType=="OpenDentBusiness.SigButDefElement") {
				return SigButDefElement.Serialize((OpenDentBusiness.SigButDefElement)obj);
			}
			if(objectType=="OpenDentBusiness.SigElement") {
				return SigElement.Serialize((OpenDentBusiness.SigElement)obj);
			}
			if(objectType=="OpenDentBusiness.SigElementDef") {
				return SigElementDef.Serialize((OpenDentBusiness.SigElementDef)obj);
			}
			if(objectType=="OpenDentBusiness.Signalod") {
				return Signalod.Serialize((OpenDentBusiness.Signalod)obj);
			}
			if(objectType=="OpenDentBusiness.Site") {
				return Site.Serialize((OpenDentBusiness.Site)obj);
			}
			if(objectType=="OpenDentBusiness.Statement") {
				return Statement.Serialize((OpenDentBusiness.Statement)obj);
			}
			if(objectType=="OpenDentBusiness.Supplier") {
				return Supplier.Serialize((OpenDentBusiness.Supplier)obj);
			}
			if(objectType=="OpenDentBusiness.Supply") {
				return Supply.Serialize((OpenDentBusiness.Supply)obj);
			}
			if(objectType=="OpenDentBusiness.SupplyNeeded") {
				return SupplyNeeded.Serialize((OpenDentBusiness.SupplyNeeded)obj);
			}
			if(objectType=="OpenDentBusiness.SupplyOrder") {
				return SupplyOrder.Serialize((OpenDentBusiness.SupplyOrder)obj);
			}
			if(objectType=="OpenDentBusiness.SupplyOrderItem") {
				return SupplyOrderItem.Serialize((OpenDentBusiness.SupplyOrderItem)obj);
			}
			if(objectType=="OpenDentBusiness.Task") {
				return Task.Serialize((OpenDentBusiness.Task)obj);
			}
			if(objectType=="OpenDentBusiness.TaskAncestor") {
				return TaskAncestor.Serialize((OpenDentBusiness.TaskAncestor)obj);
			}
			if(objectType=="OpenDentBusiness.TaskList") {
				return TaskList.Serialize((OpenDentBusiness.TaskList)obj);
			}
			if(objectType=="OpenDentBusiness.TaskNote") {
				return TaskNote.Serialize((OpenDentBusiness.TaskNote)obj);
			}
			if(objectType=="OpenDentBusiness.TaskSubscription") {
				return TaskSubscription.Serialize((OpenDentBusiness.TaskSubscription)obj);
			}
			if(objectType=="OpenDentBusiness.TaskUnread") {
				return TaskUnread.Serialize((OpenDentBusiness.TaskUnread)obj);
			}
			if(objectType=="OpenDentBusiness.TerminalActive") {
				return TerminalActive.Serialize((OpenDentBusiness.TerminalActive)obj);
			}
			if(objectType=="OpenDentBusiness.TimeAdjust") {
				return TimeAdjust.Serialize((OpenDentBusiness.TimeAdjust)obj);
			}
			if(objectType=="OpenDentBusiness.TimeCardRule") {
				return TimeCardRule.Serialize((OpenDentBusiness.TimeCardRule)obj);
			}
			if(objectType=="OpenDentBusiness.ToolButItem") {
				return ToolButItem.Serialize((OpenDentBusiness.ToolButItem)obj);
			}
			if(objectType=="OpenDentBusiness.ToothGridCell") {
				return ToothGridCell.Serialize((OpenDentBusiness.ToothGridCell)obj);
			}
			if(objectType=="OpenDentBusiness.ToothGridCol") {
				return ToothGridCol.Serialize((OpenDentBusiness.ToothGridCol)obj);
			}
			if(objectType=="OpenDentBusiness.ToothGridDef") {
				return ToothGridDef.Serialize((OpenDentBusiness.ToothGridDef)obj);
			}
			if(objectType=="OpenDentBusiness.ToothInitial") {
				return ToothInitial.Serialize((OpenDentBusiness.ToothInitial)obj);
			}
			if(objectType=="OpenDentBusiness.Transaction") {
				return Transaction.Serialize((OpenDentBusiness.Transaction)obj);
			}
			if(objectType=="OpenDentBusiness.TreatPlan") {
				return TreatPlan.Serialize((OpenDentBusiness.TreatPlan)obj);
			}
			if(objectType=="OpenDentBusiness.UserGroup") {
				return UserGroup.Serialize((OpenDentBusiness.UserGroup)obj);
			}
			if(objectType=="OpenDentBusiness.Userod") {
				return Userod.Serialize((OpenDentBusiness.Userod)obj);
			}
			if(objectType=="OpenDentBusiness.UserQuery") {
				return UserQuery.Serialize((OpenDentBusiness.UserQuery)obj);
			}
			if(objectType=="OpenDentBusiness.VaccineDef") {
				return VaccineDef.Serialize((OpenDentBusiness.VaccineDef)obj);
			}
			if(objectType=="OpenDentBusiness.VaccinePat") {
				return VaccinePat.Serialize((OpenDentBusiness.VaccinePat)obj);
			}
			if(objectType=="OpenDentBusiness.Vitalsign") {
				return Vitalsign.Serialize((OpenDentBusiness.Vitalsign)obj);
			}
			if(objectType=="OpenDentBusiness.WikiPage") {
				return WikiPage.Serialize((OpenDentBusiness.WikiPage)obj);
			}
			if(objectType=="OpenDentBusiness.WikiPageHist") {
				return WikiPageHist.Serialize((OpenDentBusiness.WikiPageHist)obj);
			}
			if(objectType=="OpenDentBusiness.ZipCode") {
				return ZipCode.Serialize((OpenDentBusiness.ZipCode)obj);
			}
			#endregion
			throw new NotSupportedException("CallClassSerializer, unsupported class type: "+objectType);
		}

		///<summary>Calls the class deserializer based on the typeName passed in.  Mainly used for deserializing parameters on DtoObjects.  Throws exceptions.</summary>
		public static object CallClassDeserializer(string typeName,string xml) {
			#region Primitive and General Types
			//To add more primitive/general types go to method xCrudGeneratorWebService.Form1.GetPrimGenDeserializerTypes and manually add it there.
			switch(typeName) {
				case "int":
				case "long":
				case "bool":
				case "string":
				case "char":
				case "float":
				case "byte":
				case "double":
				case "DateTime":
				case "List&lt;":
					return aaGeneralTypes.Deserialize(typeName,xml);
			}
			if(typeName.Contains("[")) {//Arrays.
				return aaGeneralTypes.Deserialize(typeName,xml);
			}
			#endregion
			#region Open Dental Classes
			if(typeName=="Account") {
				return Account.Deserialize(xml);
			}
			if(typeName=="AccountingAutoPay") {
				return AccountingAutoPay.Deserialize(xml);
			}
			if(typeName=="Adjustment") {
				return Adjustment.Deserialize(xml);
			}
			if(typeName=="Allergy") {
				return Allergy.Deserialize(xml);
			}
			if(typeName=="AllergyDef") {
				return AllergyDef.Deserialize(xml);
			}
			if(typeName=="Appointment") {
				return Appointment.Deserialize(xml);
			}
			if(typeName=="AppointmentRule") {
				return AppointmentRule.Deserialize(xml);
			}
			if(typeName=="ApptField") {
				return ApptField.Deserialize(xml);
			}
			if(typeName=="ApptFieldDef") {
				return ApptFieldDef.Deserialize(xml);
			}
			if(typeName=="ApptView") {
				return ApptView.Deserialize(xml);
			}
			if(typeName=="ApptViewItem") {
				return ApptViewItem.Deserialize(xml);
			}
			if(typeName=="AutoCode") {
				return AutoCode.Deserialize(xml);
			}
			if(typeName=="AutoCodeCond") {
				return AutoCodeCond.Deserialize(xml);
			}
			if(typeName=="AutoCodeItem") {
				return AutoCodeItem.Deserialize(xml);
			}
			if(typeName=="Automation") {
				return Automation.Deserialize(xml);
			}
			if(typeName=="AutomationCondition") {
				return AutomationCondition.Deserialize(xml);
			}
			if(typeName=="AutoNote") {
				return AutoNote.Deserialize(xml);
			}
			if(typeName=="AutoNoteControl") {
				return AutoNoteControl.Deserialize(xml);
			}
			if(typeName=="Benefit") {
				return Benefit.Deserialize(xml);
			}
			if(typeName=="CanadianNetwork") {
				return CanadianNetwork.Deserialize(xml);
			}
			if(typeName=="Carrier") {
				return Carrier.Deserialize(xml);
			}
			if(typeName=="CentralConnection") {
				return CentralConnection.Deserialize(xml);
			}
			if(typeName=="ChartView") {
				return ChartView.Deserialize(xml);
			}
			if(typeName=="Claim") {
				return Claim.Deserialize(xml);
			}
			if(typeName=="ClaimAttach") {
				return ClaimAttach.Deserialize(xml);
			}
			if(typeName=="ClaimCondCodeLog") {
				return ClaimCondCodeLog.Deserialize(xml);
			}
			if(typeName=="ClaimForm") {
				return ClaimForm.Deserialize(xml);
			}
			if(typeName=="ClaimFormItem") {
				return ClaimFormItem.Deserialize(xml);
			}
			if(typeName=="ClaimPayment") {
				return ClaimPayment.Deserialize(xml);
			}
			if(typeName=="ClaimProc") {
				return ClaimProc.Deserialize(xml);
			}
			if(typeName=="ClaimValCodeLog") {
				return ClaimValCodeLog.Deserialize(xml);
			}
			if(typeName=="Clearinghouse") {
				return Clearinghouse.Deserialize(xml);
			}
			if(typeName=="Clinic") {
				return Clinic.Deserialize(xml);
			}
			if(typeName=="ClockEvent") {
				return ClockEvent.Deserialize(xml);
			}
			if(typeName=="Commlog") {
				return Commlog.Deserialize(xml);
			}
			if(typeName=="Computer") {
				return Computer.Deserialize(xml);
			}
			if(typeName=="ComputerPref") {
				return ComputerPref.Deserialize(xml);
			}
			if(typeName=="Contact") {
				return Contact.Deserialize(xml);
			}
			if(typeName=="County") {
				return County.Deserialize(xml);
			}
			if(typeName=="CovCat") {
				return CovCat.Deserialize(xml);
			}
			if(typeName=="CovSpan") {
				return CovSpan.Deserialize(xml);
			}
			if(typeName=="CreditCard") {
				return CreditCard.Deserialize(xml);
			}
			if(typeName=="CustRefEntry") {
				return CustRefEntry.Deserialize(xml);
			}
			if(typeName=="CustReference") {
				return CustReference.Deserialize(xml);
			}
			if(typeName=="DashboardAR") {
				return DashboardAR.Deserialize(xml);
			}
			if(typeName=="Def") {
				return Def.Deserialize(xml);
			}
			if(typeName=="DeletedObject") {
				return DeletedObject.Deserialize(xml);
			}
			if(typeName=="Deposit") {
				return Deposit.Deserialize(xml);
			}
			if(typeName=="DictCustom") {
				return DictCustom.Deserialize(xml);
			}
			if(typeName=="Disease") {
				return Disease.Deserialize(xml);
			}
			if(typeName=="DiseaseDef") {
				return DiseaseDef.Deserialize(xml);
			}
			if(typeName=="DisplayField") {
				return DisplayField.Deserialize(xml);
			}
			if(typeName=="Document") {
				return Document.Deserialize(xml);
			}
			if(typeName=="DocumentMisc") {
				return DocumentMisc.Deserialize(xml);
			}
			if(typeName=="DrugManufacturer") {
				return DrugManufacturer.Deserialize(xml);
			}
			if(typeName=="DrugUnit") {
				return DrugUnit.Deserialize(xml);
			}
			if(typeName=="Dunning") {
				return Dunning.Deserialize(xml);
			}
			if(typeName=="EduResource") {
				return EduResource.Deserialize(xml);
			}
			if(typeName=="EhrMeasure") {
				return EhrMeasure.Deserialize(xml);
			}
			if(typeName=="EhrMeasureEvent") {
				return EhrMeasureEvent.Deserialize(xml);
			}
			if(typeName=="EhrProvKey") {
				return EhrProvKey.Deserialize(xml);
			}
			if(typeName=="EhrQuarterlyKey") {
				return EhrQuarterlyKey.Deserialize(xml);
			}
			if(typeName=="EhrSummaryCcd") {
				return EhrSummaryCcd.Deserialize(xml);
			}
			if(typeName=="ElectID") {
				return ElectID.Deserialize(xml);
			}
			if(typeName=="EmailAddress") {
				return EmailAddress.Deserialize(xml);
			}
			if(typeName=="EmailAttach") {
				return EmailAttach.Deserialize(xml);
			}
			if(typeName=="EmailMessage") {
				return EmailMessage.Deserialize(xml);
			}
			if(typeName=="EmailTemplate") {
				return EmailTemplate.Deserialize(xml);
			}
			if(typeName=="Employee") {
				return Employee.Deserialize(xml);
			}
			if(typeName=="Employer") {
				return Employer.Deserialize(xml);
			}
			if(typeName=="EobAttach") {
				return EobAttach.Deserialize(xml);
			}
			if(typeName=="Equipment") {
				return Equipment.Deserialize(xml);
			}
			if(typeName=="ErxLog") {
				return ErxLog.Deserialize(xml);
			}
			if(typeName=="Etrans") {
				return Etrans.Deserialize(xml);
			}
			if(typeName=="EtransMessageText") {
				return EtransMessageText.Deserialize(xml);
			}
			if(typeName=="Fee") {
				return Fee.Deserialize(xml);
			}
			if(typeName=="FeeSched") {
				return FeeSched.Deserialize(xml);
			}
			if(typeName=="FormPat") {
				return FormPat.Deserialize(xml);
			}
			if(typeName=="Formulary") {
				return Formulary.Deserialize(xml);
			}
			if(typeName=="FormularyMed") {
				return FormularyMed.Deserialize(xml);
			}
			if(typeName=="GroupPermission") {
				return GroupPermission.Deserialize(xml);
			}
			if(typeName=="Guardian") {
				return Guardian.Deserialize(xml);
			}
			if(typeName=="HL7Def") {
				return HL7Def.Deserialize(xml);
			}
			if(typeName=="HL7DefField") {
				return HL7DefField.Deserialize(xml);
			}
			if(typeName=="HL7DefMessage") {
				return HL7DefMessage.Deserialize(xml);
			}
			if(typeName=="HL7DefSegment") {
				return HL7DefSegment.Deserialize(xml);
			}
			if(typeName=="HL7Msg") {
				return HL7Msg.Deserialize(xml);
			}
			if(typeName=="ICD9") {
				return ICD9.Deserialize(xml);
			}
			if(typeName=="InsFilingCode") {
				return InsFilingCode.Deserialize(xml);
			}
			if(typeName=="InsFilingCodeSubtype") {
				return InsFilingCodeSubtype.Deserialize(xml);
			}
			if(typeName=="InsPlan") {
				return InsPlan.Deserialize(xml);
			}
			if(typeName=="InsSub") {
				return InsSub.Deserialize(xml);
			}
			if(typeName=="InstallmentPlan") {
				return InstallmentPlan.Deserialize(xml);
			}
			if(typeName=="JournalEntry") {
				return JournalEntry.Deserialize(xml);
			}
			if(typeName=="LabCase") {
				return LabCase.Deserialize(xml);
			}
			if(typeName=="Laboratory") {
				return Laboratory.Deserialize(xml);
			}
			if(typeName=="LabPanel") {
				return LabPanel.Deserialize(xml);
			}
			if(typeName=="LabResult") {
				return LabResult.Deserialize(xml);
			}
			if(typeName=="LabTurnaround") {
				return LabTurnaround.Deserialize(xml);
			}
			if(typeName=="Language") {
				return Language.Deserialize(xml);
			}
			if(typeName=="LanguageForeign") {
				return LanguageForeign.Deserialize(xml);
			}
			if(typeName=="Letter") {
				return Letter.Deserialize(xml);
			}
			if(typeName=="LetterMerge") {
				return LetterMerge.Deserialize(xml);
			}
			if(typeName=="LetterMergeField") {
				return LetterMergeField.Deserialize(xml);
			}
			if(typeName=="MedicalOrder") {
				return MedicalOrder.Deserialize(xml);
			}
			if(typeName=="Medication") {
				return Medication.Deserialize(xml);
			}
			if(typeName=="MedicationPat") {
				return MedicationPat.Deserialize(xml);
			}
			if(typeName=="Mount") {
				return Mount.Deserialize(xml);
			}
			if(typeName=="MountDef") {
				return MountDef.Deserialize(xml);
			}
			if(typeName=="MountItem") {
				return MountItem.Deserialize(xml);
			}
			if(typeName=="MountItemDef") {
				return MountItemDef.Deserialize(xml);
			}
			if(typeName=="Operatory") {
				return Operatory.Deserialize(xml);
			}
			if(typeName=="OrionProc") {
				return OrionProc.Deserialize(xml);
			}
			if(typeName=="OrthoChart") {
				return OrthoChart.Deserialize(xml);
			}
			if(typeName=="PatField") {
				return PatField.Deserialize(xml);
			}
			if(typeName=="PatFieldDef") {
				return PatFieldDef.Deserialize(xml);
			}
			if(typeName=="Patient") {
				return Patient.Deserialize(xml);
			}
			if(typeName=="PatientNote") {
				return PatientNote.Deserialize(xml);
			}
			if(typeName=="PatPlan") {
				return PatPlan.Deserialize(xml);
			}
			if(typeName=="Payment") {
				return Payment.Deserialize(xml);
			}
			if(typeName=="PayPeriod") {
				return PayPeriod.Deserialize(xml);
			}
			if(typeName=="PayPlan") {
				return PayPlan.Deserialize(xml);
			}
			if(typeName=="PayPlanCharge") {
				return PayPlanCharge.Deserialize(xml);
			}
			if(typeName=="PaySplit") {
				return PaySplit.Deserialize(xml);
			}
			if(typeName=="PerioExam") {
				return PerioExam.Deserialize(xml);
			}
			if(typeName=="PerioMeasure") {
				return PerioMeasure.Deserialize(xml);
			}
			if(typeName=="Pharmacy") {
				return Pharmacy.Deserialize(xml);
			}
			if(typeName=="Phone") {
				return Phone.Deserialize(xml);
			}
			if(typeName=="PhoneEmpDefault") {
				return PhoneEmpDefault.Deserialize(xml);
			}
			if(typeName=="PhoneMetric") {
				return PhoneMetric.Deserialize(xml);
			}
			if(typeName=="PhoneNumber") {
				return PhoneNumber.Deserialize(xml);
			}
			if(typeName=="PlannedAppt") {
				return PlannedAppt.Deserialize(xml);
			}
			if(typeName=="Popup") {
				return Popup.Deserialize(xml);
			}
			if(typeName=="Pref") {
				return Pref.Deserialize(xml);
			}
			if(typeName=="Printer") {
				return Printer.Deserialize(xml);
			}
			if(typeName=="ProcApptColor") {
				return ProcApptColor.Deserialize(xml);
			}
			if(typeName=="ProcButton") {
				return ProcButton.Deserialize(xml);
			}
			if(typeName=="ProcButtonItem") {
				return ProcButtonItem.Deserialize(xml);
			}
			if(typeName=="ProcCodeNote") {
				return ProcCodeNote.Deserialize(xml);
			}
			if(typeName=="Procedure") {
				return Procedure.Deserialize(xml);
			}
			if(typeName=="ProcedureCode") {
				return ProcedureCode.Deserialize(xml);
			}
			if(typeName=="ProcGroupItem") {
				return ProcGroupItem.Deserialize(xml);
			}
			if(typeName=="ProcNote") {
				return ProcNote.Deserialize(xml);
			}
			if(typeName=="ProcTP") {
				return ProcTP.Deserialize(xml);
			}
			if(typeName=="Program") {
				return Program.Deserialize(xml);
			}
			if(typeName=="ProgramProperty") {
				return ProgramProperty.Deserialize(xml);
			}
			if(typeName=="Provider") {
				return Provider.Deserialize(xml);
			}
			if(typeName=="ProviderIdent") {
				return ProviderIdent.Deserialize(xml);
			}
			if(typeName=="Question") {
				return Question.Deserialize(xml);
			}
			if(typeName=="QuestionDef") {
				return QuestionDef.Deserialize(xml);
			}
			if(typeName=="QuickPasteCat") {
				return QuickPasteCat.Deserialize(xml);
			}
			if(typeName=="QuickPasteNote") {
				return QuickPasteNote.Deserialize(xml);
			}
			if(typeName=="Recall") {
				return Recall.Deserialize(xml);
			}
			if(typeName=="RecallTrigger") {
				return RecallTrigger.Deserialize(xml);
			}
			if(typeName=="RecallType") {
				return RecallType.Deserialize(xml);
			}
			if(typeName=="Reconcile") {
				return Reconcile.Deserialize(xml);
			}
			if(typeName=="RefAttach") {
				return RefAttach.Deserialize(xml);
			}
			if(typeName=="Referral") {
				return Referral.Deserialize(xml);
			}
			if(typeName=="RegistrationKey") {
				return RegistrationKey.Deserialize(xml);
			}
			if(typeName=="ReminderRule") {
				return ReminderRule.Deserialize(xml);
			}
			if(typeName=="RepeatCharge") {
				return RepeatCharge.Deserialize(xml);
			}
			if(typeName=="ReplicationServer") {
				return ReplicationServer.Deserialize(xml);
			}
			if(typeName=="ReqNeeded") {
				return ReqNeeded.Deserialize(xml);
			}
			if(typeName=="ReqStudent") {
				return ReqStudent.Deserialize(xml);
			}
			if(typeName=="RxAlert") {
				return RxAlert.Deserialize(xml);
			}
			if(typeName=="RxDef") {
				return RxDef.Deserialize(xml);
			}
			if(typeName=="RxNorm") {
				return RxNorm.Deserialize(xml);
			}
			if(typeName=="RxPat") {
				return RxPat.Deserialize(xml);
			}
			if(typeName=="Schedule") {
				return Schedule.Deserialize(xml);
			}
			if(typeName=="ScheduleOp") {
				return ScheduleOp.Deserialize(xml);
			}
			if(typeName=="SchoolClass") {
				return SchoolClass.Deserialize(xml);
			}
			if(typeName=="SchoolCourse") {
				return SchoolCourse.Deserialize(xml);
			}
			if(typeName=="Screen") {
				return Screen.Deserialize(xml);
			}
			if(typeName=="ScreenGroup") {
				return ScreenGroup.Deserialize(xml);
			}
			if(typeName=="ScreenPat") {
				return ScreenPat.Deserialize(xml);
			}
			if(typeName=="SecurityLog") {
				return SecurityLog.Deserialize(xml);
			}
			if(typeName=="Sheet") {
				return Sheet.Deserialize(xml);
			}
			if(typeName=="SheetDef") {
				return SheetDef.Deserialize(xml);
			}
			if(typeName=="SheetField") {
				return SheetField.Deserialize(xml);
			}
			if(typeName=="SheetFieldDef") {
				return SheetFieldDef.Deserialize(xml);
			}
			if(typeName=="SigButDef") {
				return SigButDef.Deserialize(xml);
			}
			if(typeName=="SigButDefElement") {
				return SigButDefElement.Deserialize(xml);
			}
			if(typeName=="SigElement") {
				return SigElement.Deserialize(xml);
			}
			if(typeName=="SigElementDef") {
				return SigElementDef.Deserialize(xml);
			}
			if(typeName=="Signalod") {
				return Signalod.Deserialize(xml);
			}
			if(typeName=="Site") {
				return Site.Deserialize(xml);
			}
			if(typeName=="Statement") {
				return Statement.Deserialize(xml);
			}
			if(typeName=="Supplier") {
				return Supplier.Deserialize(xml);
			}
			if(typeName=="Supply") {
				return Supply.Deserialize(xml);
			}
			if(typeName=="SupplyNeeded") {
				return SupplyNeeded.Deserialize(xml);
			}
			if(typeName=="SupplyOrder") {
				return SupplyOrder.Deserialize(xml);
			}
			if(typeName=="SupplyOrderItem") {
				return SupplyOrderItem.Deserialize(xml);
			}
			if(typeName=="Task") {
				return Task.Deserialize(xml);
			}
			if(typeName=="TaskAncestor") {
				return TaskAncestor.Deserialize(xml);
			}
			if(typeName=="TaskList") {
				return TaskList.Deserialize(xml);
			}
			if(typeName=="TaskNote") {
				return TaskNote.Deserialize(xml);
			}
			if(typeName=="TaskSubscription") {
				return TaskSubscription.Deserialize(xml);
			}
			if(typeName=="TaskUnread") {
				return TaskUnread.Deserialize(xml);
			}
			if(typeName=="TerminalActive") {
				return TerminalActive.Deserialize(xml);
			}
			if(typeName=="TimeAdjust") {
				return TimeAdjust.Deserialize(xml);
			}
			if(typeName=="TimeCardRule") {
				return TimeCardRule.Deserialize(xml);
			}
			if(typeName=="ToolButItem") {
				return ToolButItem.Deserialize(xml);
			}
			if(typeName=="ToothGridCell") {
				return ToothGridCell.Deserialize(xml);
			}
			if(typeName=="ToothGridCol") {
				return ToothGridCol.Deserialize(xml);
			}
			if(typeName=="ToothGridDef") {
				return ToothGridDef.Deserialize(xml);
			}
			if(typeName=="ToothInitial") {
				return ToothInitial.Deserialize(xml);
			}
			if(typeName=="Transaction") {
				return Transaction.Deserialize(xml);
			}
			if(typeName=="TreatPlan") {
				return TreatPlan.Deserialize(xml);
			}
			if(typeName=="UserGroup") {
				return UserGroup.Deserialize(xml);
			}
			if(typeName=="Userod") {
				return Userod.Deserialize(xml);
			}
			if(typeName=="UserQuery") {
				return UserQuery.Deserialize(xml);
			}
			if(typeName=="VaccineDef") {
				return VaccineDef.Deserialize(xml);
			}
			if(typeName=="VaccinePat") {
				return VaccinePat.Deserialize(xml);
			}
			if(typeName=="Vitalsign") {
				return Vitalsign.Deserialize(xml);
			}
			if(typeName=="WikiPage") {
				return WikiPage.Deserialize(xml);
			}
			if(typeName=="WikiPageHist") {
				return WikiPageHist.Deserialize(xml);
			}
			if(typeName=="ZipCode") {
				return ZipCode.Deserialize(xml);
			}
			#endregion
			throw new NotSupportedException("CallClassDeserializer, unsupported class type: "+typeName);
		}

		///<summary>Finds the corresponding class, instantiates an instance of that class and invokes the method with the parameters.  Void methods will return null.</summary>
		private static object CallMethod(string classAndMethod,List<object> parameters) {
			string className=classAndMethod.Split('.')[0];
			string methodName=classAndMethod.Split('.')[1];
			#region SClasses
			if(className=="Accounts") {
				return MethodAccounts(methodName,parameters);
			}
			if(className=="AccountingAutoPays") {
				return MethodAccountingAutoPays(methodName,parameters);
			}
			if(className=="Adjustments") {
				return MethodAdjustments(methodName,parameters);
			}
			if(className=="Allergies") {
				return MethodAllergies(methodName,parameters);
			}
			if(className=="AllergyDefs") {
				return MethodAllergyDefs(methodName,parameters);
			}
			if(className=="Appointments") {
				return MethodAppointments(methodName,parameters);
			}
			if(className=="AppointmentRules") {
				return MethodAppointmentRules(methodName,parameters);
			}
			if(className=="ApptFields") {
				return MethodApptFields(methodName,parameters);
			}
			if(className=="ApptFieldDefs") {
				return MethodApptFieldDefs(methodName,parameters);
			}
			if(className=="ApptViews") {
				return MethodApptViews(methodName,parameters);
			}
			if(className=="ApptViewItems") {
				return MethodApptViewItems(methodName,parameters);
			}
			if(className=="AutoCodes") {
				return MethodAutoCodes(methodName,parameters);
			}
			if(className=="AutoCodeConds") {
				return MethodAutoCodeConds(methodName,parameters);
			}
			if(className=="AutoCodeItems") {
				return MethodAutoCodeItems(methodName,parameters);
			}
			if(className=="Automations") {
				return MethodAutomations(methodName,parameters);
			}
			if(className=="AutomationConditions") {
				return MethodAutomationConditions(methodName,parameters);
			}
			if(className=="AutoNotes") {
				return MethodAutoNotes(methodName,parameters);
			}
			if(className=="AutoNoteControls") {
				return MethodAutoNoteControls(methodName,parameters);
			}
			if(className=="Benefits") {
				return MethodBenefits(methodName,parameters);
			}
			if(className=="CanadianNetworks") {
				return MethodCanadianNetworks(methodName,parameters);
			}
			if(className=="Carriers") {
				return MethodCarriers(methodName,parameters);
			}
			if(className=="CentralConnections") {
				return MethodCentralConnections(methodName,parameters);
			}
			if(className=="ChartViews") {
				return MethodChartViews(methodName,parameters);
			}
			if(className=="Claims") {
				return MethodClaims(methodName,parameters);
			}
			if(className=="ClaimAttaches") {
				return MethodClaimAttaches(methodName,parameters);
			}
			if(className=="ClaimCondCodeLogs") {
				return MethodClaimCondCodeLogs(methodName,parameters);
			}
			if(className=="ClaimForms") {
				return MethodClaimForms(methodName,parameters);
			}
			if(className=="ClaimFormItems") {
				return MethodClaimFormItems(methodName,parameters);
			}
			if(className=="ClaimPayments") {
				return MethodClaimPayments(methodName,parameters);
			}
			if(className=="ClaimProcs") {
				return MethodClaimProcs(methodName,parameters);
			}
			if(className=="ClaimValCodeLogs") {
				return MethodClaimValCodeLogs(methodName,parameters);
			}
			if(className=="Clearinghouses") {
				return MethodClearinghouses(methodName,parameters);
			}
			if(className=="Clinics") {
				return MethodClinics(methodName,parameters);
			}
			if(className=="ClockEvents") {
				return MethodClockEvents(methodName,parameters);
			}
			if(className=="Commlogs") {
				return MethodCommlogs(methodName,parameters);
			}
			if(className=="Computers") {
				return MethodComputers(methodName,parameters);
			}
			if(className=="ComputerPrefs") {
				return MethodComputerPrefs(methodName,parameters);
			}
			if(className=="Contacts") {
				return MethodContacts(methodName,parameters);
			}
			if(className=="Counties") {
				return MethodCounties(methodName,parameters);
			}
			if(className=="CovCats") {
				return MethodCovCats(methodName,parameters);
			}
			if(className=="CovSpans") {
				return MethodCovSpans(methodName,parameters);
			}
			if(className=="CreditCards") {
				return MethodCreditCards(methodName,parameters);
			}
			if(className=="CustRefEntries") {
				return MethodCustRefEntries(methodName,parameters);
			}
			if(className=="CustReferences") {
				return MethodCustReferences(methodName,parameters);
			}
			if(className=="DashboardARs") {
				return MethodDashboardARs(methodName,parameters);
			}
			if(className=="Defs") {
				return MethodDefs(methodName,parameters);
			}
			if(className=="DeletedObjects") {
				return MethodDeletedObjects(methodName,parameters);
			}
			if(className=="Deposits") {
				return MethodDeposits(methodName,parameters);
			}
			if(className=="DictCustoms") {
				return MethodDictCustoms(methodName,parameters);
			}
			if(className=="Diseases") {
				return MethodDiseases(methodName,parameters);
			}
			if(className=="DiseaseDefs") {
				return MethodDiseaseDefs(methodName,parameters);
			}
			if(className=="DisplayFields") {
				return MethodDisplayFields(methodName,parameters);
			}
			if(className=="Documents") {
				return MethodDocuments(methodName,parameters);
			}
			if(className=="DocumentMiscs") {
				return MethodDocumentMiscs(methodName,parameters);
			}
			if(className=="DrugManufacturers") {
				return MethodDrugManufacturers(methodName,parameters);
			}
			if(className=="DrugUnits") {
				return MethodDrugUnits(methodName,parameters);
			}
			if(className=="Dunnings") {
				return MethodDunnings(methodName,parameters);
			}
			if(className=="EduResources") {
				return MethodEduResources(methodName,parameters);
			}
			if(className=="EhrMeasures") {
				return MethodEhrMeasures(methodName,parameters);
			}
			if(className=="EhrMeasureEvents") {
				return MethodEhrMeasureEvents(methodName,parameters);
			}
			if(className=="EhrProvKeys") {
				return MethodEhrProvKeys(methodName,parameters);
			}
			if(className=="EhrQuarterlyKeys") {
				return MethodEhrQuarterlyKeys(methodName,parameters);
			}
			if(className=="EhrSummaryCcds") {
				return MethodEhrSummaryCcds(methodName,parameters);
			}
			if(className=="ElectIDs") {
				return MethodElectIDs(methodName,parameters);
			}
			if(className=="EmailAddresses") {
				return MethodEmailAddresses(methodName,parameters);
			}
			if(className=="EmailAttaches") {
				return MethodEmailAttaches(methodName,parameters);
			}
			if(className=="EmailMessages") {
				return MethodEmailMessages(methodName,parameters);
			}
			if(className=="EmailTemplates") {
				return MethodEmailTemplates(methodName,parameters);
			}
			if(className=="Employees") {
				return MethodEmployees(methodName,parameters);
			}
			if(className=="Employers") {
				return MethodEmployers(methodName,parameters);
			}
			if(className=="EobAttaches") {
				return MethodEobAttaches(methodName,parameters);
			}
			if(className=="Equipments") {
				return MethodEquipments(methodName,parameters);
			}
			if(className=="ErxLogs") {
				return MethodErxLogs(methodName,parameters);
			}
			if(className=="Etranss") {
				return MethodEtranss(methodName,parameters);
			}
			if(className=="EtransMessageTexts") {
				return MethodEtransMessageTexts(methodName,parameters);
			}
			if(className=="Fees") {
				return MethodFees(methodName,parameters);
			}
			if(className=="FeeScheds") {
				return MethodFeeScheds(methodName,parameters);
			}
			if(className=="FormPats") {
				return MethodFormPats(methodName,parameters);
			}
			if(className=="Formularies") {
				return MethodFormularies(methodName,parameters);
			}
			if(className=="FormularyMeds") {
				return MethodFormularyMeds(methodName,parameters);
			}
			if(className=="GroupPermissions") {
				return MethodGroupPermissions(methodName,parameters);
			}
			if(className=="Guardians") {
				return MethodGuardians(methodName,parameters);
			}
			if(className=="HL7Defs") {
				return MethodHL7Defs(methodName,parameters);
			}
			if(className=="HL7DefFields") {
				return MethodHL7DefFields(methodName,parameters);
			}
			if(className=="HL7DefMessages") {
				return MethodHL7DefMessages(methodName,parameters);
			}
			if(className=="HL7DefSegments") {
				return MethodHL7DefSegments(methodName,parameters);
			}
			if(className=="HL7Msgs") {
				return MethodHL7Msgs(methodName,parameters);
			}
			if(className=="ICD9s") {
				return MethodICD9s(methodName,parameters);
			}
			if(className=="InsFilingCodes") {
				return MethodInsFilingCodes(methodName,parameters);
			}
			if(className=="InsFilingCodeSubtypes") {
				return MethodInsFilingCodeSubtypes(methodName,parameters);
			}
			if(className=="InsPlans") {
				return MethodInsPlans(methodName,parameters);
			}
			if(className=="InsSubs") {
				return MethodInsSubs(methodName,parameters);
			}
			if(className=="InstallmentPlans") {
				return MethodInstallmentPlans(methodName,parameters);
			}
			if(className=="JournalEntries") {
				return MethodJournalEntries(methodName,parameters);
			}
			if(className=="LabCases") {
				return MethodLabCases(methodName,parameters);
			}
			if(className=="Laboratories") {
				return MethodLaboratories(methodName,parameters);
			}
			if(className=="LabPanels") {
				return MethodLabPanels(methodName,parameters);
			}
			if(className=="LabResults") {
				return MethodLabResults(methodName,parameters);
			}
			if(className=="LabTurnarounds") {
				return MethodLabTurnarounds(methodName,parameters);
			}
			if(className=="Lans") {
				return MethodLans(methodName,parameters);
			}
			if(className=="LanguageForeigns") {
				return MethodLanguageForeigns(methodName,parameters);
			}
			if(className=="Letters") {
				return MethodLetters(methodName,parameters);
			}
			if(className=="LetterMerges") {
				return MethodLetterMerges(methodName,parameters);
			}
			if(className=="LetterMergeFields") {
				return MethodLetterMergeFields(methodName,parameters);
			}
			if(className=="MedicalOrders") {
				return MethodMedicalOrders(methodName,parameters);
			}
			if(className=="Medications") {
				return MethodMedications(methodName,parameters);
			}
			if(className=="MedicationPats") {
				return MethodMedicationPats(methodName,parameters);
			}
			if(className=="Mounts") {
				return MethodMounts(methodName,parameters);
			}
			if(className=="MountDefs") {
				return MethodMountDefs(methodName,parameters);
			}
			if(className=="MountItems") {
				return MethodMountItems(methodName,parameters);
			}
			if(className=="MountItemDefs") {
				return MethodMountItemDefs(methodName,parameters);
			}
			if(className=="Operatories") {
				return MethodOperatories(methodName,parameters);
			}
			if(className=="OrionProcs") {
				return MethodOrionProcs(methodName,parameters);
			}
			if(className=="OrthoCharts") {
				return MethodOrthoCharts(methodName,parameters);
			}
			if(className=="PatFields") {
				return MethodPatFields(methodName,parameters);
			}
			if(className=="PatFieldDefs") {
				return MethodPatFieldDefs(methodName,parameters);
			}
			if(className=="Patients") {
				return MethodPatients(methodName,parameters);
			}
			if(className=="PatientNotes") {
				return MethodPatientNotes(methodName,parameters);
			}
			if(className=="PatPlans") {
				return MethodPatPlans(methodName,parameters);
			}
			if(className=="Payments") {
				return MethodPayments(methodName,parameters);
			}
			if(className=="PayPeriods") {
				return MethodPayPeriods(methodName,parameters);
			}
			if(className=="PayPlans") {
				return MethodPayPlans(methodName,parameters);
			}
			if(className=="PayPlanCharges") {
				return MethodPayPlanCharges(methodName,parameters);
			}
			if(className=="PaySplits") {
				return MethodPaySplits(methodName,parameters);
			}
			if(className=="PerioExams") {
				return MethodPerioExams(methodName,parameters);
			}
			if(className=="PerioMeasures") {
				return MethodPerioMeasures(methodName,parameters);
			}
			if(className=="Pharmacies") {
				return MethodPharmacies(methodName,parameters);
			}
			if(className=="Phones") {
				return MethodPhones(methodName,parameters);
			}
			if(className=="PhoneEmpDefaults") {
				return MethodPhoneEmpDefaults(methodName,parameters);
			}
			if(className=="PhoneMetrics") {
				return MethodPhoneMetrics(methodName,parameters);
			}
			if(className=="PhoneNumbers") {
				return MethodPhoneNumbers(methodName,parameters);
			}
			if(className=="PlannedAppts") {
				return MethodPlannedAppts(methodName,parameters);
			}
			if(className=="Popups") {
				return MethodPopups(methodName,parameters);
			}
			if(className=="Prefs") {
				return MethodPrefs(methodName,parameters);
			}
			if(className=="Printers") {
				return MethodPrinters(methodName,parameters);
			}
			if(className=="ProcApptColors") {
				return MethodProcApptColors(methodName,parameters);
			}
			if(className=="ProcButtons") {
				return MethodProcButtons(methodName,parameters);
			}
			if(className=="ProcButtonItems") {
				return MethodProcButtonItems(methodName,parameters);
			}
			if(className=="ProcCodeNotes") {
				return MethodProcCodeNotes(methodName,parameters);
			}
			if(className=="Procedures") {
				return MethodProcedures(methodName,parameters);
			}
			if(className=="ProcedureCodes") {
				return MethodProcedureCodes(methodName,parameters);
			}
			if(className=="ProcGroupItems") {
				return MethodProcGroupItems(methodName,parameters);
			}
			if(className=="ProcNotes") {
				return MethodProcNotes(methodName,parameters);
			}
			if(className=="ProcTPs") {
				return MethodProcTPs(methodName,parameters);
			}
			if(className=="Programs") {
				return MethodPrograms(methodName,parameters);
			}
			if(className=="ProgramProperties") {
				return MethodProgramProperties(methodName,parameters);
			}
			if(className=="Providers") {
				return MethodProviders(methodName,parameters);
			}
			if(className=="ProviderIdents") {
				return MethodProviderIdents(methodName,parameters);
			}
			if(className=="Questions") {
				return MethodQuestions(methodName,parameters);
			}
			if(className=="QuestionDefs") {
				return MethodQuestionDefs(methodName,parameters);
			}
			if(className=="QuickPasteCats") {
				return MethodQuickPasteCats(methodName,parameters);
			}
			if(className=="QuickPasteNotes") {
				return MethodQuickPasteNotes(methodName,parameters);
			}
			if(className=="Recalls") {
				return MethodRecalls(methodName,parameters);
			}
			if(className=="RecallTriggers") {
				return MethodRecallTriggers(methodName,parameters);
			}
			if(className=="RecallTypes") {
				return MethodRecallTypes(methodName,parameters);
			}
			if(className=="Reconciles") {
				return MethodReconciles(methodName,parameters);
			}
			if(className=="RefAttaches") {
				return MethodRefAttaches(methodName,parameters);
			}
			if(className=="Referrals") {
				return MethodReferrals(methodName,parameters);
			}
			if(className=="RegistrationKeys") {
				return MethodRegistrationKeys(methodName,parameters);
			}
			if(className=="ReminderRules") {
				return MethodReminderRules(methodName,parameters);
			}
			if(className=="RepeatCharges") {
				return MethodRepeatCharges(methodName,parameters);
			}
			if(className=="ReplicationServers") {
				return MethodReplicationServers(methodName,parameters);
			}
			if(className=="ReqNeededs") {
				return MethodReqNeededs(methodName,parameters);
			}
			if(className=="ReqStudents") {
				return MethodReqStudents(methodName,parameters);
			}
			if(className=="RxAlerts") {
				return MethodRxAlerts(methodName,parameters);
			}
			if(className=="RxDefs") {
				return MethodRxDefs(methodName,parameters);
			}
			if(className=="RxNorms") {
				return MethodRxNorms(methodName,parameters);
			}
			if(className=="RxPats") {
				return MethodRxPats(methodName,parameters);
			}
			if(className=="Schedules") {
				return MethodSchedules(methodName,parameters);
			}
			if(className=="ScheduleOps") {
				return MethodScheduleOps(methodName,parameters);
			}
			if(className=="SchoolClasses") {
				return MethodSchoolClasses(methodName,parameters);
			}
			if(className=="SchoolCourses") {
				return MethodSchoolCourses(methodName,parameters);
			}
			if(className=="Screens") {
				return MethodScreens(methodName,parameters);
			}
			if(className=="ScreenGroups") {
				return MethodScreenGroups(methodName,parameters);
			}
			if(className=="ScreenPats") {
				return MethodScreenPats(methodName,parameters);
			}
			if(className=="SecurityLogs") {
				return MethodSecurityLogs(methodName,parameters);
			}
			if(className=="Sheets") {
				return MethodSheets(methodName,parameters);
			}
			if(className=="SheetDefs") {
				return MethodSheetDefs(methodName,parameters);
			}
			if(className=="SheetFields") {
				return MethodSheetFields(methodName,parameters);
			}
			if(className=="SheetFieldDefs") {
				return MethodSheetFieldDefs(methodName,parameters);
			}
			if(className=="SigButDefs") {
				return MethodSigButDefs(methodName,parameters);
			}
			if(className=="SigButDefElements") {
				return MethodSigButDefElements(methodName,parameters);
			}
			if(className=="SigElements") {
				return MethodSigElements(methodName,parameters);
			}
			if(className=="SigElementDefs") {
				return MethodSigElementDefs(methodName,parameters);
			}
			if(className=="Signalods") {
				return MethodSignalods(methodName,parameters);
			}
			if(className=="Sites") {
				return MethodSites(methodName,parameters);
			}
			if(className=="Statements") {
				return MethodStatements(methodName,parameters);
			}
			if(className=="Suppliers") {
				return MethodSuppliers(methodName,parameters);
			}
			if(className=="Supplies") {
				return MethodSupplies(methodName,parameters);
			}
			if(className=="SupplyNeededs") {
				return MethodSupplyNeededs(methodName,parameters);
			}
			if(className=="SupplyOrders") {
				return MethodSupplyOrders(methodName,parameters);
			}
			if(className=="SupplyOrderItems") {
				return MethodSupplyOrderItems(methodName,parameters);
			}
			if(className=="Tasks") {
				return MethodTasks(methodName,parameters);
			}
			if(className=="TaskAncestors") {
				return MethodTaskAncestors(methodName,parameters);
			}
			if(className=="TaskLists") {
				return MethodTaskLists(methodName,parameters);
			}
			if(className=="TaskNotes") {
				return MethodTaskNotes(methodName,parameters);
			}
			if(className=="TaskSubscriptions") {
				return MethodTaskSubscriptions(methodName,parameters);
			}
			if(className=="TaskUnreads") {
				return MethodTaskUnreads(methodName,parameters);
			}
			if(className=="TerminalActives") {
				return MethodTerminalActives(methodName,parameters);
			}
			if(className=="TimeAdjusts") {
				return MethodTimeAdjusts(methodName,parameters);
			}
			if(className=="TimeCardRules") {
				return MethodTimeCardRules(methodName,parameters);
			}
			if(className=="ToolButItems") {
				return MethodToolButItems(methodName,parameters);
			}
			if(className=="ToothGridCells") {
				return MethodToothGridCells(methodName,parameters);
			}
			if(className=="ToothGridCols") {
				return MethodToothGridCols(methodName,parameters);
			}
			if(className=="ToothGridDefs") {
				return MethodToothGridDefs(methodName,parameters);
			}
			if(className=="ToothInitials") {
				return MethodToothInitials(methodName,parameters);
			}
			if(className=="Transactions") {
				return MethodTransactions(methodName,parameters);
			}
			if(className=="TreatPlans") {
				return MethodTreatPlans(methodName,parameters);
			}
			if(className=="UserGroups") {
				return MethodUserGroups(methodName,parameters);
			}
			if(className=="Userods") {
				return MethodUserods(methodName,parameters);
			}
			if(className=="UserQueries") {
				return MethodUserQueries(methodName,parameters);
			}
			if(className=="VaccineDefs") {
				return MethodVaccineDefs(methodName,parameters);
			}
			if(className=="VaccinePats") {
				return MethodVaccinePats(methodName,parameters);
			}
			if(className=="Vitalsigns") {
				return MethodVitalsigns(methodName,parameters);
			}
			if(className=="WikiPages") {
				return MethodWikiPages(methodName,parameters);
			}
			if(className=="WikiPageHists") {
				return MethodWikiPageHists(methodName,parameters);
			}
			if(className=="ZipCodes") {
				return MethodZipCodes(methodName,parameters);
			}
			#endregion
			throw new NotSupportedException("CallMethod, unknown class: "+classAndMethod);
		}

		#region Method Calls

		///<summary></summary>
		private static object MethodAccounts(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			if(methodName=="Insert") {
				return Accounts.Insert((OpenDentBusiness.Account)parameters[0]);
			}
			if(methodName=="Update") {
				Accounts.Update((OpenDentBusiness.Account)parameters[0]);
				return null;
			}
			throw new NotSupportedException("MethodAccounts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodAccountingAutoPays(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAccountingAutoPays, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodAdjustments(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAdjustments, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodAllergies(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAllergies, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodAllergyDefs(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAllergyDefs, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodAppointments(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			if(methodName=="RefreshASAP") {
				return Appointments.RefreshASAP(Convert.ToInt64(parameters[0]),Convert.ToInt64(parameters[1]),Convert.ToInt64(parameters[2]));
			}
			throw new NotSupportedException("MethodAppointments, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodAppointmentRules(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAppointmentRules, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodApptFields(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodApptFields, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodApptFieldDefs(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodApptFieldDefs, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodApptViews(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodApptViews, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodApptViewItems(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodApptViewItems, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodAutoCodes(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAutoCodes, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodAutoCodeConds(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAutoCodeConds, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodAutoCodeItems(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAutoCodeItems, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodAutomations(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAutomations, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodAutomationConditions(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAutomationConditions, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodAutoNotes(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAutoNotes, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodAutoNoteControls(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAutoNoteControls, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodBenefits(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodBenefits, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodCanadianNetworks(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodCanadianNetworks, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodCarriers(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodCarriers, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodCentralConnections(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodCentralConnections, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodChartViews(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodChartViews, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodClaims(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodClaims, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodClaimAttaches(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodClaimAttaches, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodClaimCondCodeLogs(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodClaimCondCodeLogs, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodClaimForms(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodClaimForms, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodClaimFormItems(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodClaimFormItems, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodClaimPayments(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodClaimPayments, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodClaimProcs(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodClaimProcs, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodClaimValCodeLogs(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodClaimValCodeLogs, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodClearinghouses(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodClearinghouses, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodClinics(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodClinics, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodClockEvents(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodClockEvents, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodCommlogs(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodCommlogs, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodComputers(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodComputers, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodComputerPrefs(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodComputerPrefs, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodContacts(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodContacts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodCounties(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodCounties, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodCovCats(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodCovCats, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodCovSpans(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodCovSpans, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodCreditCards(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodCreditCards, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodCustRefEntries(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodCustRefEntries, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodCustReferences(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodCustReferences, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodDashboardARs(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodDashboardARs, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodDefs(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodDefs, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodDeletedObjects(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodDeletedObjects, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodDeposits(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodDeposits, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodDictCustoms(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodDictCustoms, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodDiseases(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodDiseases, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodDiseaseDefs(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodDiseaseDefs, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodDisplayFields(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodDisplayFields, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodDocuments(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodDocuments, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodDocumentMiscs(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodDocumentMiscs, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodDrugManufacturers(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodDrugManufacturers, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodDrugUnits(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodDrugUnits, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodDunnings(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodDunnings, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodEduResources(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodEduResources, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodEhrMeasures(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodEhrMeasures, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodEhrMeasureEvents(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodEhrMeasureEvents, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodEhrProvKeys(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodEhrProvKeys, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodEhrQuarterlyKeys(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodEhrQuarterlyKeys, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodEhrSummaryCcds(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodEhrSummaryCcds, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodElectIDs(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodElectIDs, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodEmailAddresses(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodEmailAddresses, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodEmailAttaches(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodEmailAttaches, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodEmailMessages(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodEmailMessages, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodEmailTemplates(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodEmailTemplates, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodEmployees(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodEmployees, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodEmployers(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodEmployers, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodEobAttaches(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodEobAttaches, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodEquipments(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodEquipments, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodErxLogs(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodErxLogs, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodEtranss(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodEtranss, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodEtransMessageTexts(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodEtransMessageTexts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodFees(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodFees, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodFeeScheds(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodFeeScheds, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodFormPats(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodFormPats, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodFormularies(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodFormularies, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodFormularyMeds(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodFormularyMeds, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodGroupPermissions(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodGroupPermissions, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodGuardians(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodGuardians, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodHL7Defs(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodHL7Defs, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodHL7DefFields(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodHL7DefFields, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodHL7DefMessages(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodHL7DefMessages, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodHL7DefSegments(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodHL7DefSegments, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodHL7Msgs(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodHL7Msgs, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodICD9s(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodICD9s, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodInsFilingCodes(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodInsFilingCodes, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodInsFilingCodeSubtypes(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodInsFilingCodeSubtypes, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodInsPlans(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodInsPlans, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodInsSubs(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodInsSubs, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodInstallmentPlans(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodInstallmentPlans, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodJournalEntries(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodJournalEntries, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodLabCases(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodLabCases, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodLaboratories(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodLaboratories, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodLabPanels(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodLabPanels, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodLabResults(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodLabResults, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodLabTurnarounds(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodLabTurnarounds, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodLans(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodLans, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodLanguageForeigns(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodLanguageForeigns, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodLetters(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodLetters, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodLetterMerges(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodLetterMerges, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodLetterMergeFields(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodLetterMergeFields, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodMedicalOrders(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodMedicalOrders, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodMedications(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodMedications, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodMedicationPats(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodMedicationPats, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodMounts(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodMounts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodMountDefs(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodMountDefs, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodMountItems(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodMountItems, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodMountItemDefs(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodMountItemDefs, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodOperatories(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodOperatories, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodOrionProcs(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodOrionProcs, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodOrthoCharts(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodOrthoCharts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodPatFields(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodPatFields, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodPatFieldDefs(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodPatFieldDefs, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodPatients(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			if(methodName=="GetPat") {
				return Patients.GetPat(Convert.ToInt64(parameters[0]));
			}
			if(methodName=="GetPtDataTable") {
				return Patients.GetPtDataTable(Convert.ToBoolean(parameters[0]),Convert.ToString(parameters[1]),Convert.ToString(parameters[2]),Convert.ToString(parameters[3]),Convert.ToString(parameters[4]),Convert.ToBoolean(parameters[5]),Convert.ToString(parameters[6]),Convert.ToString(parameters[7]),Convert.ToString(parameters[8]),Convert.ToString(parameters[9]),Convert.ToString(parameters[10]),Convert.ToInt64(parameters[11]),Convert.ToBoolean(parameters[12]),Convert.ToBoolean(parameters[13]),Convert.ToInt64(parameters[14]),(System.DateTime)parameters[15],Convert.ToInt64(parameters[16]),Convert.ToString(parameters[17]),Convert.ToString(parameters[18]));
			}
			throw new NotSupportedException("MethodPatients, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodPatientNotes(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodPatientNotes, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodPatPlans(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodPatPlans, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodPayments(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodPayments, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodPayPeriods(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodPayPeriods, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodPayPlans(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodPayPlans, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodPayPlanCharges(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodPayPlanCharges, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodPaySplits(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodPaySplits, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodPerioExams(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodPerioExams, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodPerioMeasures(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodPerioMeasures, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodPharmacies(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodPharmacies, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodPhones(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodPhones, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodPhoneEmpDefaults(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodPhoneEmpDefaults, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodPhoneMetrics(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodPhoneMetrics, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodPhoneNumbers(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodPhoneNumbers, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodPlannedAppts(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodPlannedAppts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodPopups(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodPopups, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodPrefs(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			if(methodName=="RefreshCache") {
				return Prefs.RefreshCache();
			}
			throw new NotSupportedException("MethodPrefs, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodPrinters(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodPrinters, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodProcApptColors(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodProcApptColors, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodProcButtons(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodProcButtons, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodProcButtonItems(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodProcButtonItems, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodProcCodeNotes(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodProcCodeNotes, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodProcedures(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodProcedures, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodProcedureCodes(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodProcedureCodes, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodProcGroupItems(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodProcGroupItems, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodProcNotes(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodProcNotes, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodProcTPs(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodProcTPs, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodPrograms(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodPrograms, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodProgramProperties(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodProgramProperties, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodProviders(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodProviders, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodProviderIdents(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodProviderIdents, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodQuestions(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodQuestions, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodQuestionDefs(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodQuestionDefs, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodQuickPasteCats(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodQuickPasteCats, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodQuickPasteNotes(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodQuickPasteNotes, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodRecalls(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodRecalls, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodRecallTriggers(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodRecallTriggers, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodRecallTypes(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodRecallTypes, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodReconciles(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodReconciles, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodRefAttaches(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodRefAttaches, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodReferrals(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodReferrals, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodRegistrationKeys(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodRegistrationKeys, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodReminderRules(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodReminderRules, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodRepeatCharges(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodRepeatCharges, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodReplicationServers(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodReplicationServers, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodReqNeededs(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodReqNeededs, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodReqStudents(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodReqStudents, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodRxAlerts(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodRxAlerts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodRxDefs(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodRxDefs, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodRxNorms(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodRxNorms, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodRxPats(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodRxPats, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodSchedules(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodSchedules, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodScheduleOps(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodScheduleOps, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodSchoolClasses(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodSchoolClasses, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodSchoolCourses(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodSchoolCourses, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodScreens(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodScreens, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodScreenGroups(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodScreenGroups, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodScreenPats(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodScreenPats, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodSecurityLogs(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodSecurityLogs, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodSheets(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodSheets, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodSheetDefs(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodSheetDefs, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodSheetFields(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodSheetFields, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodSheetFieldDefs(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodSheetFieldDefs, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodSigButDefs(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodSigButDefs, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodSigButDefElements(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodSigButDefElements, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodSigElements(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodSigElements, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodSigElementDefs(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodSigElementDefs, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodSignalods(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodSignalods, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodSites(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodSites, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodStatements(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodStatements, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodSuppliers(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodSuppliers, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodSupplies(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodSupplies, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodSupplyNeededs(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodSupplyNeededs, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodSupplyOrders(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodSupplyOrders, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodSupplyOrderItems(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodSupplyOrderItems, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodTasks(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodTasks, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodTaskAncestors(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodTaskAncestors, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodTaskLists(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodTaskLists, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodTaskNotes(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodTaskNotes, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodTaskSubscriptions(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodTaskSubscriptions, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodTaskUnreads(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodTaskUnreads, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodTerminalActives(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodTerminalActives, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodTimeAdjusts(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodTimeAdjusts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodTimeCardRules(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodTimeCardRules, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodToolButItems(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodToolButItems, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodToothGridCells(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodToothGridCells, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodToothGridCols(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodToothGridCols, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodToothGridDefs(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodToothGridDefs, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodToothInitials(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodToothInitials, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodTransactions(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodTransactions, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodTreatPlans(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodTreatPlans, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodUserGroups(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodUserGroups, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodUserods(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodUserods, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodUserQueries(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodUserQueries, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodVaccineDefs(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodVaccineDefs, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodVaccinePats(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodVaccinePats, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodVitalsigns(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodVitalsigns, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodWikiPages(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodWikiPages, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodWikiPageHists(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodWikiPageHists, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodZipCodes(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodZipCodes, unknown method: "+methodName);
		}

		#endregion

	}
}
