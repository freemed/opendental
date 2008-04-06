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
			else if(type==typeof(DtoCovCatRefresh)) {
				return CovCatB.Refresh();
			}
			else if(type==typeof(DtoPrefRefresh)) {
				return PrefD.Refresh();
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
			if(type==typeof(DtoGeneralNonQ)) {
				return GeneralB.NonQ(((DtoGeneralNonQ)dto).Command,((DtoGeneralNonQ)dto).GetInsertID);
			}
			else if(type==typeof(DtoLogin)) {
				return OpenDentalService.Login(((DtoLogin)dto).Database,((DtoLogin)dto).OdUser,((DtoLogin)dto).OdPassHash);
			}
			else if(type==typeof(DtoMiscDataMakeABackup)) {
				return MiscDataB.MakeABackup();
			}
			else{
				throw new Exception("OpenDentServer.BusinessLayer.ProcessCommand(dto) is missing a case for "
					+dto.GetType().ToString());
			}
		}
	}
}
