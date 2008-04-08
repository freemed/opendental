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
				return DataCore.GetTable(((DtoGeneralGetTable)dto).Command);
			}
			else if(type==typeof(DtoGeneralGetTableLow)) {
				return DataCore.GetTableLow(((DtoGeneralGetTableLow)dto).Command);
			}
			else if(type==typeof(DtoGeneralGetDataSet)) {
				return DataCore.GetDataSet(((DtoGeneralGetDataSet)dto).Commands);
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
				return DataCore.NonQ(((DtoGeneralNonQ)dto).Command,((DtoGeneralNonQ)dto).GetInsertID);
			}
			else if(type==typeof(DtoLogin)) {
				return OpenDentalService.Login(((DtoLogin)dto).Database,((DtoLogin)dto).OdUser,((DtoLogin)dto).OdPassHash);
			}
			else if(type==typeof(DtoMiscDataMakeABackup)) {
				return MiscData.MakeABackup();
			}
			else{
				throw new Exception("OpenDentServer.BusinessLayer.ProcessCommand(dto) is missing a case for "
					+dto.GetType().ToString());
			}
		}
	}
}
