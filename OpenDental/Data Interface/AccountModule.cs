using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{
	public class AccountModule{
		///<summary>This is just the first version of this function.  It only gets selected parts of the Account refresh.</summary>
		public static DataSet GetAll(int patNum){
			try {
				if(RemotingClient.OpenDentBusinessIsLocal) {
					return AccountModuleB.GetAll(patNum);
				}
				else {
					DtoAccountModuleGetAll dto=new DtoAccountModuleGetAll();
					dto.PatNum=patNum;
					return RemotingClient.ProcessQuery(dto);
				}
			}
			catch(Exception e) {
				MessageBox.Show(e.Message);
				return new DataSet();//It might be better to return null.
			}
		}





	}
}
