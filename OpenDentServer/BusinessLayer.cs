using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using OpenDentBusiness;

namespace OpenDentServer {
	public class BusinessLayer {

		///<summary>DataSet cannot be null.</summary>
		public static DataSet ProcessQuery(DtoQueryBase dto) {
			Type type = dto.GetType();

			if(type==typeof(DtoGeneralGetTable)) {
				return GeneralB.GetTable(((DtoGeneralGetTable)dto).Command);
			}
			else if(type==typeof(DtoGeneralGetTableLow)) {
				return GeneralB.GetTableLow(((DtoGeneralGetTableLow)dto).Command);
			}
			else if(type==typeof(DtoGeneralGetDataSet)) {
				return GeneralB.GetDataSet(((DtoGeneralGetDataSet)dto).Commands);
			}
			else if(type==typeof(DtoAccountModuleGetAll)) {
				return AccountModuleB.GetAll(((DtoAccountModuleGetAll)dto).PatNum,((DtoAccountModuleGetAll)dto).ViewingInRecall);
			}
			else if(type==typeof(DtoChartModuleGetAll)) {
				return ChartModuleB.GetAll(((DtoChartModuleGetAll)dto).PatNum,((DtoChartModuleGetAll)dto).IsAuditMode);
			}
			else if(type==typeof(DtoCovCatRefresh)) {
				return CovCatB.Refresh();
			}
			else if(type==typeof(DtoDefRefresh)) {
				return DefB.Refresh();
			}
			else if(type==typeof(DtoPrefRefresh)) {
				return PrefB.Refresh();
			}
			else if(type==typeof(DtoProcedureRefresh)) {
				return ProcedureB.Refresh(((DtoProcedureRefresh)dto).PatNum);
			}
			else if(type==typeof(DtoUserodRefresh)) {
				return UserodB.Refresh();
			}
			else{
				throw new Exception("OpenDentServer.BusinessLayer.ProcessObject(dto) is missing a case for "
					+dto.GetType().ToString());
			}
		}

		public static int ProcessCommand(DtoCommandBase dto) {
			Type type = dto.GetType();

			if(type==typeof(DtoDefInsert)) {
				return DefB.Insert(((DtoDefInsert)dto).DefCur);
			}
			else if(type==typeof(DtoDefUpdate)) {
				return DefB.Update(((DtoDefUpdate)dto).DefCur);
			}
			else if(type==typeof(DtoDocumentInsert)) {
				return DocumentB.Insert(((DtoDocumentInsert)dto).Doc,((DtoDocumentInsert)dto).PatLF,((DtoDocumentInsert)dto).PatNum);
			}
			else if(type==typeof(DtoDocumentUpdate)) {
				return DocumentB.Update(((DtoDocumentUpdate)dto).Doc);
			}
			else if(type==typeof(DtoEmailMessageUpdate)) {
				return EmailMessageB.Update(((DtoEmailMessageUpdate)dto).Message);
			}
			else if(type==typeof(DtoGeneralNonQ)) {
				return GeneralB.NonQ(((DtoGeneralNonQ)dto).Command,((DtoGeneralNonQ)dto).GetInsertID);
			}
			else if(type==typeof(DtoLogin)) {
				return OpenDentalService.Login(((DtoLogin)dto).Database,((DtoLogin)dto).OdUser,((DtoLogin)dto).OdPassHash);
			}
			else if(type==typeof(DtoMiscDataMakeABackup)) {
				return MiscDataB.MakeABackup();
			}
			else if(type==typeof(DtoProcedureInsert)) {
				return ProcedureB.Insert(((DtoProcedureInsert)dto).Proc);
			}
			else if(type==typeof(DtoProcedureUpdate)) {
				return ProcedureB.Update(((DtoProcedureUpdate)dto).Proc,((DtoProcedureUpdate)dto).OldProc);
			}
			else if(type==typeof(DtoProcedureDelete)) {
				return ProcedureB.Delete(((DtoProcedureDelete)dto).ProcNum);
			}
			else if(type==typeof(DtoProcedureUpdateAptNum)) {
				return ProcedureB.UpdateAptNum(((DtoProcedureUpdateAptNum)dto).ProcNum,((DtoProcedureUpdateAptNum)dto).NewAptNum);
			}
			else if(type==typeof(DtoProcedureUpdatePlannedAptNum)) {
				return ProcedureB.UpdatePlannedAptNum(((DtoProcedureUpdatePlannedAptNum)dto).ProcNum,
					((DtoProcedureUpdatePlannedAptNum)dto).NewPlannedAptNum);
			}
			else if(type==typeof(DtoProcedureUpdatePriority)) {
				return ProcedureB.UpdatePriority(((DtoProcedureUpdatePriority)dto).ProcNum,
					((DtoProcedureUpdatePriority)dto).NewPriority);
			}
			else if(type==typeof(DtoProcedureUpdateFee)) {
				return ProcedureB.UpdateFee(((DtoProcedureUpdateFee)dto).ProcNum,((DtoProcedureUpdateFee)dto).NewFee);
			}
			else{
				throw new Exception("OpenDentServer.BusinessLayer.ProcessCommand(dto) is missing a case for "
					+dto.GetType().ToString());
			}
		}
	}
}
