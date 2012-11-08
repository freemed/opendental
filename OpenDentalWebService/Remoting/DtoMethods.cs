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

		///<summary>Calls the classes deserializer based on the typeName passed in.  Mainly used for deserializing parameters on DtoObjects.  Throws exceptions.</summary>
		public static object CallClassDeserializer(string typeName,string xml) {
			try {
				#region Primitive and General Types
				//To add more primitive/general types go to method xCrudGeneratorWebService.Form1.GetPrimGenTypes and manually add it there.
				if(typeName=="long") {
					return aaGeneralTypes.Deserialize(typeName,xml);
				}
				#endregion
				#region Open Dental Classes
				if(typeName=="OpenDentBusiness.Account") {
					return Account.Deserialize(xml);
				}
				if(typeName=="OpenDentBusiness.AccountingAutoPay") {
					return AccountingAutoPay.Deserialize(xml);
				}
				if(typeName=="OpenDentBusiness.Adjustment") {
					return Adjustment.Deserialize(xml);
				}
				if(typeName=="OpenDentBusiness.Allergy") {
					return Allergy.Deserialize(xml);
				}
				if(typeName=="OpenDentBusiness.AllergyDef") {
					return AllergyDef.Deserialize(xml);
				}
				if(typeName=="OpenDentBusiness.Appointment") {
					return Appointment.Deserialize(xml);
				}
				if(typeName=="OpenDentBusiness.AppointmentRule") {
					return AppointmentRule.Deserialize(xml);
				}
				if(typeName=="OpenDentBusiness.ApptField") {
					return ApptField.Deserialize(xml);
				}
				if(typeName=="OpenDentBusiness.ApptFieldDef") {
					return ApptFieldDef.Deserialize(xml);
				}
				if(typeName=="OpenDentBusiness.ApptView") {
					return ApptView.Deserialize(xml);
				}
				if(typeName=="OpenDentBusiness.ApptViewItem") {
					return ApptViewItem.Deserialize(xml);
				}
				if(typeName=="OpenDentBusiness.AutoCode") {
					return AutoCode.Deserialize(xml);
				}
				if(typeName=="OpenDentBusiness.AutoCodeCond") {
					return AutoCodeCond.Deserialize(xml);
				}
				if(typeName=="OpenDentBusiness.AutoCodeItem") {
					return AutoCodeItem.Deserialize(xml);
				}
				if(typeName=="OpenDentBusiness.Automation") {
					return Automation.Deserialize(xml);
				}
				if(typeName=="OpenDentBusiness.AutomationCondition") {
					return AutomationCondition.Deserialize(xml);
				}
				if(typeName=="OpenDentBusiness.AutoNote") {
					return AutoNote.Deserialize(xml);
				}
				if(typeName=="OpenDentBusiness.AutoNoteControl") {
					return AutoNoteControl.Deserialize(xml);
				}
				if(typeName=="OpenDentBusiness.Benefit") {
					return Benefit.Deserialize(xml);
				}
				if(typeName=="OpenDentBusiness.CanadianNetwork") {
					return CanadianNetwork.Deserialize(xml);
				}
				if(typeName=="OpenDentBusiness.Carrier") {
					return Carrier.Deserialize(xml);
				}
				if(typeName=="OpenDentBusiness.CentralConnection") {
					return CentralConnection.Deserialize(xml);
				}
				if(typeName=="OpenDentBusiness.ChartView") {
					return ChartView.Deserialize(xml);
				}
				if(typeName=="OpenDentBusiness.Claim") {
					return Claim.Deserialize(xml);
				}
				if(typeName=="OpenDentBusiness.ClaimAttach") {
					return ClaimAttach.Deserialize(xml);
				}
				if(typeName=="OpenDentBusiness.ClaimCondCodeLog") {
					return ClaimCondCodeLog.Deserialize(xml);
				}
				if(typeName=="OpenDentBusiness.ClaimForm") {
					return ClaimForm.Deserialize(xml);
				}
				if(typeName=="OpenDentBusiness.ClaimFormItem") {
					return ClaimFormItem.Deserialize(xml);
				}
				if(typeName=="OpenDentBusiness.ClaimPayment") {
					return ClaimPayment.Deserialize(xml);
				}
				if(typeName=="OpenDentBusiness.ClaimProc") {
					return ClaimProc.Deserialize(xml);
				}
				if(typeName=="OpenDentBusiness.ClaimValCodeLog") {
					return ClaimValCodeLog.Deserialize(xml);
				}
				if(typeName=="OpenDentBusiness.Clearinghouse") {
					return Clearinghouse.Deserialize(xml);
				}
				if(typeName=="OpenDentBusiness.Clinic") {
					return Clinic.Deserialize(xml);
				}
				if(typeName=="OpenDentBusiness.ClockEvent") {
					return ClockEvent.Deserialize(xml);
				}
				if(typeName=="OpenDentBusiness.Commlog") {
					return Commlog.Deserialize(xml);
				}
				if(typeName=="OpenDentBusiness.Computer") {
					return Computer.Deserialize(xml);
				}
				if(typeName=="OpenDentBusiness.ComputerPref") {
					return ComputerPref.Deserialize(xml);
				}
				if(typeName=="OpenDentBusiness.Contact") {
					return Contact.Deserialize(xml);
				}
				if(typeName=="OpenDentBusiness.County") {
					return County.Deserialize(xml);
				}
				if(typeName=="OpenDentBusiness.CovCat") {
					return CovCat.Deserialize(xml);
				}
				if(typeName=="OpenDentBusiness.CovSpan") {
					return CovSpan.Deserialize(xml);
				}
				if(typeName=="OpenDentBusiness.CreditCard") {
					return CreditCard.Deserialize(xml);
				}
				if(typeName=="OpenDentBusiness.CustRefEntry") {
					return CustRefEntry.Deserialize(xml);
				}
				if(typeName=="OpenDentBusiness.CustReference") {
					return CustReference.Deserialize(xml);
				}
				if(typeName=="OpenDentBusiness.DashboardAR") {
					return DashboardAR.Deserialize(xml);
				}
				if(typeName=="OpenDentBusiness.Def") {
					return Def.Deserialize(xml);
				}
				if(typeName=="OpenDentBusiness.DeletedObject") {
					return DeletedObject.Deserialize(xml);
				}
				if(typeName=="OpenDentBusiness.Deposit") {
					return Deposit.Deserialize(xml);
				}
				if(typeName=="OpenDentBusiness.DictCustom") {
					return DictCustom.Deserialize(xml);
				}
				if(typeName=="OpenDentBusiness.Disease") {
					return Disease.Deserialize(xml);
				}
				if(typeName=="OpenDentBusiness.DiseaseDef") {
					return DiseaseDef.Deserialize(xml);
				}
				if(typeName=="OpenDentBusiness.DisplayField") {
					return DisplayField.Deserialize(xml);
				}
				if(typeName=="OpenDentBusiness.Document") {
					return Document.Deserialize(xml);
				}
				if(typeName=="OpenDentBusiness.DocumentMisc") {
					return DocumentMisc.Deserialize(xml);
				}
				if(typeName=="OpenDentBusiness.DrugManufacturer") {
					return DrugManufacturer.Deserialize(xml);
				}
				if(typeName=="OpenDentBusiness.DrugUnit") {
					return DrugUnit.Deserialize(xml);
				}
				if(typeName=="OpenDentBusiness.Dunning") {
					return Dunning.Deserialize(xml);
				}
				if(typeName=="OpenDentBusiness.EduResource") {
					return EduResource.Deserialize(xml);
				}
				if(typeName=="OpenDentBusiness.EhrMeasure") {
					return EhrMeasure.Deserialize(xml);
				}
				if(typeName=="OpenDentBusiness.EhrMeasureEvent") {
					return EhrMeasureEvent.Deserialize(xml);
				}
				if(typeName=="OpenDentBusiness.EhrProvKey") {
					return EhrProvKey.Deserialize(xml);
				}
				if(typeName=="OpenDentBusiness.EhrQuarterlyKey") {
					return EhrQuarterlyKey.Deserialize(xml);
				}
				if(typeName=="OpenDentBusiness.EhrSummaryCcd") {
					return EhrSummaryCcd.Deserialize(xml);
				}
				if(typeName=="OpenDentBusiness.ElectID") {
					return ElectID.Deserialize(xml);
				}
				if(typeName=="OpenDentBusiness.EmailAttach") {
					return EmailAttach.Deserialize(xml);
				}
				if(typeName=="OpenDentBusiness.EmailMessage") {
					return EmailMessage.Deserialize(xml);
				}
				if(typeName=="OpenDentBusiness.EmailTemplate") {
					return EmailTemplate.Deserialize(xml);
				}
				if(typeName=="OpenDentBusiness.Employee") {
					return Employee.Deserialize(xml);
				}
				if(typeName=="OpenDentBusiness.Employer") {
					return Employer.Deserialize(xml);
				}
				if(typeName=="OpenDentBusiness.EobAttach") {
					return EobAttach.Deserialize(xml);
				}
				if(typeName=="OpenDentBusiness.Equipment") {
					return Equipment.Deserialize(xml);
				}
				if(typeName=="OpenDentBusiness.ErxLog") {
					return ErxLog.Deserialize(xml);
				}
				if(typeName=="OpenDentBusiness.Etrans") {
					return Etrans.Deserialize(xml);
				}
				if(typeName=="OpenDentBusiness.EtransMessageText") {
					return EtransMessageText.Deserialize(xml);
				}
				if(typeName=="OpenDentBusiness.Fee") {
					return Fee.Deserialize(xml);
				}
				if(typeName=="OpenDentBusiness.FeeSched") {
					return FeeSched.Deserialize(xml);
				}
				if(typeName=="OpenDentBusiness.FormPat") {
					return FormPat.Deserialize(xml);
				}
				if(typeName=="OpenDentBusiness.Formulary") {
					return Formulary.Deserialize(xml);
				}
				if(typeName=="OpenDentBusiness.FormularyMed") {
					return FormularyMed.Deserialize(xml);
				}
				if(typeName=="OpenDentBusiness.GroupPermission") {
					return GroupPermission.Deserialize(xml);
				}
				if(typeName=="OpenDentBusiness.Guardian") {
					return Guardian.Deserialize(xml);
				}
				if(typeName=="OpenDentBusiness.HL7Def") {
					return HL7Def.Deserialize(xml);
				}
				if(typeName=="OpenDentBusiness.HL7DefField") {
					return HL7DefField.Deserialize(xml);
				}
				if(typeName=="OpenDentBusiness.HL7DefMessage") {
					return HL7DefMessage.Deserialize(xml);
				}
				if(typeName=="OpenDentBusiness.HL7DefSegment") {
					return HL7DefSegment.Deserialize(xml);
				}
				if(typeName=="OpenDentBusiness.HL7Msg") {
					return HL7Msg.Deserialize(xml);
				}
				if(typeName=="OpenDentBusiness.ICD9") {
					return ICD9.Deserialize(xml);
				}
				if(typeName=="OpenDentBusiness.InsFilingCode") {
					return InsFilingCode.Deserialize(xml);
				}
				if(typeName=="OpenDentBusiness.InsFilingCodeSubtype") {
					return InsFilingCodeSubtype.Deserialize(xml);
				}
				if(typeName=="OpenDentBusiness.InsPlan") {
					return InsPlan.Deserialize(xml);
				}
				if(typeName=="OpenDentBusiness.InsSub") {
					return InsSub.Deserialize(xml);
				}
				if(typeName=="OpenDentBusiness.InstallmentPlan") {
					return InstallmentPlan.Deserialize(xml);
				}
				if(typeName=="OpenDentBusiness.JournalEntry") {
					return JournalEntry.Deserialize(xml);
				}
				if(typeName=="OpenDentBusiness.LabCase") {
					return LabCase.Deserialize(xml);
				}
				if(typeName=="OpenDentBusiness.Laboratory") {
					return Laboratory.Deserialize(xml);
				}
				if(typeName=="OpenDentBusiness.LabPanel") {
					return LabPanel.Deserialize(xml);
				}
				if(typeName=="OpenDentBusiness.LabResult") {
					return LabResult.Deserialize(xml);
				}
				if(typeName=="OpenDentBusiness.LabTurnaround") {
					return LabTurnaround.Deserialize(xml);
				}
				if(typeName=="OpenDentBusiness.Language") {
					return Language.Deserialize(xml);
				}
				if(typeName=="OpenDentBusiness.LanguageForeign") {
					return LanguageForeign.Deserialize(xml);
				}
				if(typeName=="OpenDentBusiness.Letter") {
					return Letter.Deserialize(xml);
				}
				if(typeName=="OpenDentBusiness.LetterMerge") {
					return LetterMerge.Deserialize(xml);
				}
				if(typeName=="OpenDentBusiness.LetterMergeField") {
					return LetterMergeField.Deserialize(xml);
				}
				if(typeName=="OpenDentBusiness.MedicalOrder") {
					return MedicalOrder.Deserialize(xml);
				}
				if(typeName=="OpenDentBusiness.Medication") {
					return Medication.Deserialize(xml);
				}
				if(typeName=="OpenDentBusiness.MedicationPat") {
					return MedicationPat.Deserialize(xml);
				}
				if(typeName=="OpenDentBusiness.Mount") {
					return Mount.Deserialize(xml);
				}
				if(typeName=="OpenDentBusiness.MountDef") {
					return MountDef.Deserialize(xml);
				}
				if(typeName=="OpenDentBusiness.MountItem") {
					return MountItem.Deserialize(xml);
				}
				if(typeName=="OpenDentBusiness.MountItemDef") {
					return MountItemDef.Deserialize(xml);
				}
				if(typeName=="OpenDentBusiness.Operatory") {
					return Operatory.Deserialize(xml);
				}
				if(typeName=="OpenDentBusiness.OrionProc") {
					return OrionProc.Deserialize(xml);
				}
				if(typeName=="OpenDentBusiness.OrthoChart") {
					return OrthoChart.Deserialize(xml);
				}
				if(typeName=="OpenDentBusiness.PatField") {
					return PatField.Deserialize(xml);
				}
				if(typeName=="OpenDentBusiness.PatFieldDef") {
					return PatFieldDef.Deserialize(xml);
				}
				if(typeName=="OpenDentBusiness.Patient") {
					return Patient.Deserialize(xml);
				}
				if(typeName=="OpenDentBusiness.PatientNote") {
					return PatientNote.Deserialize(xml);
				}
				if(typeName=="OpenDentBusiness.PatPlan") {
					return PatPlan.Deserialize(xml);
				}
				if(typeName=="OpenDentBusiness.Payment") {
					return Payment.Deserialize(xml);
				}
				if(typeName=="OpenDentBusiness.PayPeriod") {
					return PayPeriod.Deserialize(xml);
				}
				if(typeName=="OpenDentBusiness.PayPlan") {
					return PayPlan.Deserialize(xml);
				}
				if(typeName=="OpenDentBusiness.PayPlanCharge") {
					return PayPlanCharge.Deserialize(xml);
				}
				if(typeName=="OpenDentBusiness.PaySplit") {
					return PaySplit.Deserialize(xml);
				}
				if(typeName=="OpenDentBusiness.PerioExam") {
					return PerioExam.Deserialize(xml);
				}
				if(typeName=="OpenDentBusiness.PerioMeasure") {
					return PerioMeasure.Deserialize(xml);
				}
				if(typeName=="OpenDentBusiness.Pharmacy") {
					return Pharmacy.Deserialize(xml);
				}
				if(typeName=="OpenDentBusiness.Phone") {
					return Phone.Deserialize(xml);
				}
				if(typeName=="OpenDentBusiness.PhoneEmpDefault") {
					return PhoneEmpDefault.Deserialize(xml);
				}
				if(typeName=="OpenDentBusiness.PhoneMetric") {
					return PhoneMetric.Deserialize(xml);
				}
				if(typeName=="OpenDentBusiness.PhoneNumber") {
					return PhoneNumber.Deserialize(xml);
				}
				if(typeName=="OpenDentBusiness.PlannedAppt") {
					return PlannedAppt.Deserialize(xml);
				}
				if(typeName=="OpenDentBusiness.Popup") {
					return Popup.Deserialize(xml);
				}
				if(typeName=="OpenDentBusiness.Pref") {
					return Pref.Deserialize(xml);
				}
				if(typeName=="OpenDentBusiness.Printer") {
					return Printer.Deserialize(xml);
				}
				if(typeName=="OpenDentBusiness.ProcApptColor") {
					return ProcApptColor.Deserialize(xml);
				}
				if(typeName=="OpenDentBusiness.ProcButton") {
					return ProcButton.Deserialize(xml);
				}
				if(typeName=="OpenDentBusiness.ProcButtonItem") {
					return ProcButtonItem.Deserialize(xml);
				}
				if(typeName=="OpenDentBusiness.ProcCodeNote") {
					return ProcCodeNote.Deserialize(xml);
				}
				if(typeName=="OpenDentBusiness.Procedure") {
					return Procedure.Deserialize(xml);
				}
				if(typeName=="OpenDentBusiness.ProcedureCode") {
					return ProcedureCode.Deserialize(xml);
				}
				if(typeName=="OpenDentBusiness.ProcGroupItem") {
					return ProcGroupItem.Deserialize(xml);
				}
				if(typeName=="OpenDentBusiness.ProcNote") {
					return ProcNote.Deserialize(xml);
				}
				if(typeName=="OpenDentBusiness.ProcTP") {
					return ProcTP.Deserialize(xml);
				}
				if(typeName=="OpenDentBusiness.Program") {
					return Program.Deserialize(xml);
				}
				if(typeName=="OpenDentBusiness.ProgramProperty") {
					return ProgramProperty.Deserialize(xml);
				}
				if(typeName=="OpenDentBusiness.Provider") {
					return Provider.Deserialize(xml);
				}
				if(typeName=="OpenDentBusiness.ProviderIdent") {
					return ProviderIdent.Deserialize(xml);
				}
				if(typeName=="OpenDentBusiness.Question") {
					return Question.Deserialize(xml);
				}
				if(typeName=="OpenDentBusiness.QuestionDef") {
					return QuestionDef.Deserialize(xml);
				}
				if(typeName=="OpenDentBusiness.QuickPasteCat") {
					return QuickPasteCat.Deserialize(xml);
				}
				if(typeName=="OpenDentBusiness.QuickPasteNote") {
					return QuickPasteNote.Deserialize(xml);
				}
				if(typeName=="OpenDentBusiness.Recall") {
					return Recall.Deserialize(xml);
				}
				if(typeName=="OpenDentBusiness.RecallTrigger") {
					return RecallTrigger.Deserialize(xml);
				}
				if(typeName=="OpenDentBusiness.RecallType") {
					return RecallType.Deserialize(xml);
				}
				if(typeName=="OpenDentBusiness.Reconcile") {
					return Reconcile.Deserialize(xml);
				}
				if(typeName=="OpenDentBusiness.RefAttach") {
					return RefAttach.Deserialize(xml);
				}
				if(typeName=="OpenDentBusiness.Referral") {
					return Referral.Deserialize(xml);
				}
				if(typeName=="OpenDentBusiness.RegistrationKey") {
					return RegistrationKey.Deserialize(xml);
				}
				if(typeName=="OpenDentBusiness.ReminderRule") {
					return ReminderRule.Deserialize(xml);
				}
				if(typeName=="OpenDentBusiness.RepeatCharge") {
					return RepeatCharge.Deserialize(xml);
				}
				if(typeName=="OpenDentBusiness.ReplicationServer") {
					return ReplicationServer.Deserialize(xml);
				}
				if(typeName=="OpenDentBusiness.ReqNeeded") {
					return ReqNeeded.Deserialize(xml);
				}
				if(typeName=="OpenDentBusiness.ReqStudent") {
					return ReqStudent.Deserialize(xml);
				}
				if(typeName=="OpenDentBusiness.RxAlert") {
					return RxAlert.Deserialize(xml);
				}
				if(typeName=="OpenDentBusiness.RxDef") {
					return RxDef.Deserialize(xml);
				}
				if(typeName=="OpenDentBusiness.RxNorm") {
					return RxNorm.Deserialize(xml);
				}
				if(typeName=="OpenDentBusiness.RxPat") {
					return RxPat.Deserialize(xml);
				}
				if(typeName=="OpenDentBusiness.Schedule") {
					return Schedule.Deserialize(xml);
				}
				if(typeName=="OpenDentBusiness.ScheduleOp") {
					return ScheduleOp.Deserialize(xml);
				}
				if(typeName=="OpenDentBusiness.SchoolClass") {
					return SchoolClass.Deserialize(xml);
				}
				if(typeName=="OpenDentBusiness.SchoolCourse") {
					return SchoolCourse.Deserialize(xml);
				}
				if(typeName=="OpenDentBusiness.Screen") {
					return Screen.Deserialize(xml);
				}
				if(typeName=="OpenDentBusiness.ScreenGroup") {
					return ScreenGroup.Deserialize(xml);
				}
				if(typeName=="OpenDentBusiness.ScreenPat") {
					return ScreenPat.Deserialize(xml);
				}
				if(typeName=="OpenDentBusiness.SecurityLog") {
					return SecurityLog.Deserialize(xml);
				}
				if(typeName=="OpenDentBusiness.Sheet") {
					return Sheet.Deserialize(xml);
				}
				if(typeName=="OpenDentBusiness.SheetDef") {
					return SheetDef.Deserialize(xml);
				}
				if(typeName=="OpenDentBusiness.SheetField") {
					return SheetField.Deserialize(xml);
				}
				if(typeName=="OpenDentBusiness.SheetFieldDef") {
					return SheetFieldDef.Deserialize(xml);
				}
				if(typeName=="OpenDentBusiness.SigButDef") {
					return SigButDef.Deserialize(xml);
				}
				if(typeName=="OpenDentBusiness.SigButDefElement") {
					return SigButDefElement.Deserialize(xml);
				}
				if(typeName=="OpenDentBusiness.SigElement") {
					return SigElement.Deserialize(xml);
				}
				if(typeName=="OpenDentBusiness.SigElementDef") {
					return SigElementDef.Deserialize(xml);
				}
				if(typeName=="OpenDentBusiness.Signalod") {
					return Signalod.Deserialize(xml);
				}
				if(typeName=="OpenDentBusiness.Site") {
					return Site.Deserialize(xml);
				}
				if(typeName=="OpenDentBusiness.Statement") {
					return Statement.Deserialize(xml);
				}
				if(typeName=="OpenDentBusiness.Supplier") {
					return Supplier.Deserialize(xml);
				}
				if(typeName=="OpenDentBusiness.Supply") {
					return Supply.Deserialize(xml);
				}
				if(typeName=="OpenDentBusiness.SupplyNeeded") {
					return SupplyNeeded.Deserialize(xml);
				}
				if(typeName=="OpenDentBusiness.SupplyOrder") {
					return SupplyOrder.Deserialize(xml);
				}
				if(typeName=="OpenDentBusiness.SupplyOrderItem") {
					return SupplyOrderItem.Deserialize(xml);
				}
				if(typeName=="OpenDentBusiness.Task") {
					return Task.Deserialize(xml);
				}
				if(typeName=="OpenDentBusiness.TaskAncestor") {
					return TaskAncestor.Deserialize(xml);
				}
				if(typeName=="OpenDentBusiness.TaskList") {
					return TaskList.Deserialize(xml);
				}
				if(typeName=="OpenDentBusiness.TaskNote") {
					return TaskNote.Deserialize(xml);
				}
				if(typeName=="OpenDentBusiness.TaskSubscription") {
					return TaskSubscription.Deserialize(xml);
				}
				if(typeName=="OpenDentBusiness.TaskUnread") {
					return TaskUnread.Deserialize(xml);
				}
				if(typeName=="OpenDentBusiness.TerminalActive") {
					return TerminalActive.Deserialize(xml);
				}
				if(typeName=="OpenDentBusiness.TimeAdjust") {
					return TimeAdjust.Deserialize(xml);
				}
				if(typeName=="OpenDentBusiness.TimeCardRule") {
					return TimeCardRule.Deserialize(xml);
				}
				if(typeName=="OpenDentBusiness.ToolButItem") {
					return ToolButItem.Deserialize(xml);
				}
				if(typeName=="OpenDentBusiness.ToothGridCell") {
					return ToothGridCell.Deserialize(xml);
				}
				if(typeName=="OpenDentBusiness.ToothGridCol") {
					return ToothGridCol.Deserialize(xml);
				}
				if(typeName=="OpenDentBusiness.ToothGridDef") {
					return ToothGridDef.Deserialize(xml);
				}
				if(typeName=="OpenDentBusiness.ToothInitial") {
					return ToothInitial.Deserialize(xml);
				}
				if(typeName=="OpenDentBusiness.Transaction") {
					return Transaction.Deserialize(xml);
				}
				if(typeName=="OpenDentBusiness.TreatPlan") {
					return TreatPlan.Deserialize(xml);
				}
				if(typeName=="OpenDentBusiness.UserGroup") {
					return UserGroup.Deserialize(xml);
				}
				if(typeName=="OpenDentBusiness.Userod") {
					return Userod.Deserialize(xml);
				}
				if(typeName=="OpenDentBusiness.UserQuery") {
					return UserQuery.Deserialize(xml);
				}
				if(typeName=="OpenDentBusiness.VaccineDef") {
					return VaccineDef.Deserialize(xml);
				}
				if(typeName=="OpenDentBusiness.VaccinePat") {
					return VaccinePat.Deserialize(xml);
				}
				if(typeName=="OpenDentBusiness.Vitalsign") {
					return Vitalsign.Deserialize(xml);
				}
				if(typeName=="OpenDentBusiness.ZipCode") {
					return ZipCode.Deserialize(xml);
				}
				#endregion
			}
			catch {
				throw new Exception("CallClassDeserializer, error deserializing class type: "+typeName);
			}
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
			throw new NotSupportedException("MethodAccounts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodAdjustments(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAccounts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodAllergies(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAccounts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodAllergyDefs(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAccounts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodAppointments(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAccounts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodAppointmentRules(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAccounts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodApptFields(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAccounts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodApptFieldDefs(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAccounts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodApptViews(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAccounts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodApptViewItems(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAccounts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodAutoCodes(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAccounts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodAutoCodeConds(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAccounts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodAutoCodeItems(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAccounts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodAutomations(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAccounts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodAutomationConditions(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAccounts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodAutoNotes(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAccounts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodAutoNoteControls(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAccounts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodBenefits(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAccounts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodCanadianNetworks(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAccounts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodCarriers(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAccounts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodCentralConnections(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAccounts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodChartViews(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAccounts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodClaims(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAccounts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodClaimAttaches(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAccounts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodClaimCondCodeLogs(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAccounts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodClaimForms(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAccounts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodClaimFormItems(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAccounts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodClaimPayments(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAccounts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodClaimProcs(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAccounts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodClaimValCodeLogs(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAccounts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodClearinghouses(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAccounts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodClinics(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAccounts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodClockEvents(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAccounts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodCommlogs(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAccounts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodComputers(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAccounts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodComputerPrefs(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAccounts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodContacts(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAccounts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodCounties(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAccounts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodCovCats(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAccounts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodCovSpans(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAccounts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodCreditCards(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAccounts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodCustRefEntries(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAccounts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodCustReferences(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAccounts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodDashboardARs(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAccounts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodDefs(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAccounts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodDeletedObjects(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAccounts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodDeposits(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAccounts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodDictCustoms(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAccounts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodDiseases(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAccounts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodDiseaseDefs(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAccounts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodDisplayFields(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAccounts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodDocuments(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAccounts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodDocumentMiscs(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAccounts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodDrugManufacturers(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAccounts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodDrugUnits(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAccounts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodDunnings(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAccounts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodEduResources(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAccounts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodEhrMeasures(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAccounts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodEhrMeasureEvents(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAccounts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodEhrProvKeys(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAccounts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodEhrQuarterlyKeys(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAccounts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodEhrSummaryCcds(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAccounts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodElectIDs(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAccounts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodEmailAttaches(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAccounts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodEmailMessages(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAccounts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodEmailTemplates(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAccounts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodEmployees(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAccounts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodEmployers(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAccounts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodEobAttaches(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAccounts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodEquipments(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAccounts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodErxLogs(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAccounts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodEtranss(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAccounts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodEtransMessageTexts(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAccounts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodFees(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAccounts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodFeeScheds(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAccounts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodFormPats(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAccounts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodFormularies(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAccounts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodFormularyMeds(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAccounts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodGroupPermissions(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAccounts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodGuardians(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAccounts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodHL7Defs(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAccounts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodHL7DefFields(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAccounts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodHL7DefMessages(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAccounts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodHL7DefSegments(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAccounts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodHL7Msgs(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAccounts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodICD9s(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAccounts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodInsFilingCodes(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAccounts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodInsFilingCodeSubtypes(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAccounts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodInsPlans(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAccounts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodInsSubs(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAccounts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodInstallmentPlans(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAccounts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodJournalEntries(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAccounts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodLabCases(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAccounts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodLaboratories(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAccounts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodLabPanels(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAccounts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodLabResults(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAccounts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodLabTurnarounds(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAccounts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodLans(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAccounts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodLanguageForeigns(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAccounts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodLetters(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAccounts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodLetterMerges(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAccounts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodLetterMergeFields(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAccounts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodMedicalOrders(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAccounts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodMedications(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAccounts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodMedicationPats(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAccounts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodMounts(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAccounts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodMountDefs(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAccounts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodMountItems(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAccounts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodMountItemDefs(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAccounts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodOperatories(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAccounts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodOrionProcs(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAccounts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodOrthoCharts(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAccounts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodPatFields(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAccounts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodPatFieldDefs(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAccounts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodPatients(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			if(methodName=="GetPtDataTable") {
				return Patients.GetPtDataTable();
			}
			throw new NotSupportedException("MethodAccounts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodPatientNotes(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAccounts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodPatPlans(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAccounts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodPayments(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAccounts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodPayPeriods(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAccounts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodPayPlans(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAccounts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodPayPlanCharges(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAccounts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodPaySplits(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAccounts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodPerioExams(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAccounts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodPerioMeasures(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAccounts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodPharmacies(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAccounts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodPhones(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAccounts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodPhoneEmpDefaults(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAccounts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodPhoneMetrics(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAccounts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodPhoneNumbers(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAccounts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodPlannedAppts(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAccounts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodPopups(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAccounts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodPrefs(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAccounts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodPrinters(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAccounts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodProcApptColors(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAccounts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodProcButtons(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAccounts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodProcButtonItems(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAccounts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodProcCodeNotes(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAccounts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodProcedures(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAccounts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodProcedureCodes(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAccounts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodProcGroupItems(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAccounts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodProcNotes(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAccounts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodProcTPs(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAccounts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodPrograms(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAccounts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodProgramProperties(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAccounts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodProviders(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAccounts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodProviderIdents(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAccounts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodQuestions(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAccounts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodQuestionDefs(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAccounts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodQuickPasteCats(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAccounts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodQuickPasteNotes(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAccounts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodRecalls(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAccounts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodRecallTriggers(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAccounts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodRecallTypes(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAccounts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodReconciles(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAccounts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodRefAttaches(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAccounts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodReferrals(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAccounts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodRegistrationKeys(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAccounts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodReminderRules(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAccounts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodRepeatCharges(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAccounts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodReplicationServers(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAccounts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodReqNeededs(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAccounts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodReqStudents(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAccounts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodRxAlerts(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAccounts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodRxDefs(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAccounts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodRxNorms(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAccounts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodRxPats(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAccounts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodSchedules(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAccounts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodScheduleOps(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAccounts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodSchoolClasses(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAccounts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodSchoolCourses(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAccounts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodScreens(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAccounts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodScreenGroups(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAccounts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodScreenPats(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAccounts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodSecurityLogs(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAccounts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodSheets(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAccounts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodSheetDefs(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAccounts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodSheetFields(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAccounts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodSheetFieldDefs(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAccounts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodSigButDefs(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAccounts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodSigButDefElements(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAccounts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodSigElements(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAccounts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodSigElementDefs(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAccounts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodSignalods(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAccounts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodSites(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAccounts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodStatements(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAccounts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodSuppliers(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAccounts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodSupplies(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAccounts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodSupplyNeededs(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAccounts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodSupplyOrders(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAccounts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodSupplyOrderItems(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAccounts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodTasks(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAccounts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodTaskAncestors(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAccounts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodTaskLists(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAccounts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodTaskNotes(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAccounts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodTaskSubscriptions(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAccounts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodTaskUnreads(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAccounts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodTerminalActives(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAccounts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodTimeAdjusts(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAccounts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodTimeCardRules(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAccounts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodToolButItems(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAccounts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodToothGridCells(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAccounts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodToothGridCols(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAccounts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodToothGridDefs(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAccounts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodToothInitials(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAccounts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodTransactions(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAccounts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodTreatPlans(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAccounts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodUserGroups(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAccounts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodUserods(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAccounts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodUserQueries(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAccounts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodVaccineDefs(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAccounts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodVaccinePats(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAccounts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodVitalsigns(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAccounts, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodZipCodes(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodAccounts, unknown method: "+methodName);
		}

		#endregion

	}
}
