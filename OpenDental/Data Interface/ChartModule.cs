using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{
	public class ChartModule{
		///<summary>This is just the first version of this function.  It only gets selected parts of the Chart refresh.</summary>
		public static DataSet GetAll(int patNum, bool isAuditMode) {
			try {
				if(RemotingClient.OpenDentBusinessIsLocal) {
					return ChartModuleB.GetAll(patNum,isAuditMode);
				}
				else {
					DtoChartModuleGetAll dto=new DtoChartModuleGetAll();
					dto.PatNum=patNum;
					dto.IsAuditMode=isAuditMode;
					return RemotingClient.ProcessQuery(dto);
				}
			}
			catch(Exception e) {
				MessageBox.Show(e.Message);
				return null;
			}
		}




	}
}
