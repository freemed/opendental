using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using OpenDentBusiness;

namespace OpenDentServer {
	public class BusinessLayer {

		///<summary>DataSet cannot be null.</summary>
		public static DataSet ProcessQuery(DtoQueryBase dto) {
			if(dto.GetType()==typeof(DtoGeneralGetTable)) {
				return GeneralB.GetTable(((DtoGeneralGetTable)dto).Command);
			}
			else if(dto.GetType()==typeof(DtoGeneralGetTableLow)) {
				return GeneralB.GetTableLow(((DtoGeneralGetTableLow)dto).Command);
			}
			else if(dto.GetType()==typeof(DtoGeneralGetDataSet)) {
				return GeneralB.GetDataSet(((DtoGeneralGetDataSet)dto).Commands);
			}
			else if(dto.GetType()==typeof(DtoAccountModuleGetAll)) {
				return AccountModuleB.GetAll(((DtoAccountModuleGetAll)dto).PatNum);
			}
			else if(dto.GetType()==typeof(DtoChartModuleGetAll)) {
				return ChartModuleB.GetAll(((DtoChartModuleGetAll)dto).PatNum,((DtoChartModuleGetAll)dto).IsAuditMode);
			}
			else if(dto.GetType()==typeof(DtoCovCatRefresh)) {
				return CovCatB.Refresh();
			}
			else if(dto.GetType()==typeof(DtoDefRefresh)) {
				return DefB.Refresh();
			}
			else if(dto.GetType()==typeof(DtoPrefRefresh)) {
				return PrefB.Refresh();
			}
			else if(dto.GetType()==typeof(DtoProcedureRefresh)) {
				return ProcedureB.Refresh(((DtoProcedureRefresh)dto).PatNum);
			}
			else if(dto.GetType()==typeof(DtoUserodRefresh)) {
				return UserodB.Refresh();
			}


			else{
				throw new Exception("OpenDentServer.BusinessLayer.ProcessObject(dto) is missing a case for "
					+dto.GetType().ToString());
			}
		}

		public static int ProcessCommand(DtoCommandBase dto) {
			if(dto.GetType()==typeof(DtoDefInsert)) {
				return DefB.Insert(((DtoDefInsert)dto).DefCur);
			}
			else if(dto.GetType()==typeof(DtoDefUpdate)) {
				return DefB.Update(((DtoDefUpdate)dto).DefCur);
			}
			else if(dto.GetType()==typeof(DtoDocumentInsert)) {
				return DocumentB.Insert(((DtoDocumentInsert)dto).Doc,((DtoDocumentInsert)dto).PatLF,((DtoDocumentInsert)dto).PatNum);
			}
			else if(dto.GetType()==typeof(DtoDocumentUpdate)) {
				return DocumentB.Update(((DtoDocumentUpdate)dto).Doc);
			}
			else if(dto.GetType()==typeof(DtoEmailMessageUpdate)) {
				return EmailMessageB.Update(((DtoEmailMessageUpdate)dto).Message);
			}
			else if(dto.GetType()==typeof(DtoGeneralNonQ)) {
				return GeneralB.NonQ(((DtoGeneralNonQ)dto).Command,((DtoGeneralNonQ)dto).GetInsertID);
			}
			else if(dto.GetType()==typeof(DtoLogin)) {
				return OpenDentalService.Login(((DtoLogin)dto).Database,((DtoLogin)dto).OdUser,((DtoLogin)dto).OdPassHash);
			}
			else if(dto.GetType()==typeof(DtoMiscDataMakeABackup)) {
				return MiscDataB.MakeABackup();
			}
			else if(dto.GetType()==typeof(DtoProcedureInsert)) {
				return ProcedureB.Insert(((DtoProcedureInsert)dto).Proc);
			}
			else if(dto.GetType()==typeof(DtoProcedureUpdate)) {
				return ProcedureB.Update(((DtoProcedureUpdate)dto).Proc,((DtoProcedureUpdate)dto).OldProc);
			}
			else if(dto.GetType()==typeof(DtoProcedureDelete)) {
				return ProcedureB.Delete(((DtoProcedureDelete)dto).ProcNum);
			}
			else if(dto.GetType()==typeof(DtoProcedureUpdateAptNum)) {
				return ProcedureB.UpdateAptNum(((DtoProcedureUpdateAptNum)dto).ProcNum,((DtoProcedureUpdateAptNum)dto).NewAptNum);
			}
			else if(dto.GetType()==typeof(DtoProcedureUpdatePlannedAptNum)) {
				return ProcedureB.UpdatePlannedAptNum(((DtoProcedureUpdatePlannedAptNum)dto).ProcNum,
					((DtoProcedureUpdatePlannedAptNum)dto).NewPlannedAptNum);
			}
			else if(dto.GetType()==typeof(DtoProcedureUpdatePriority)) {
				return ProcedureB.UpdatePriority(((DtoProcedureUpdatePriority)dto).ProcNum,
					((DtoProcedureUpdatePriority)dto).NewPriority);
			}
			else if(dto.GetType()==typeof(DtoProcedureUpdateFee)) {
				return ProcedureB.UpdateFee(((DtoProcedureUpdateFee)dto).ProcNum,((DtoProcedureUpdateFee)dto).NewFee);
			}
			



			else{
				throw new Exception("OpenDentServer.BusinessLayer.ProcessCommand(dto) is missing a case for "
					+dto.GetType().ToString());
			}
		}


			
			


			//switch(dto.DTO_type){
			//	case "ProcedureInsert":


			//}

			//return null;
		//}

	}
}
