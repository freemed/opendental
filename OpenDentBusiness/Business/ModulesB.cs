using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace OpenDentBusiness {
	public class ModulesB {
		public static DataSet GetAccount(int patNum){
			string command="SELECT CommDateTime,CommType,Mode,SentOrReceived,Note,CommlogNum "
				+"FROM commlog WHERE PatNum ='"+POut.PInt(patNum)+"' ORDER BY CommDateTime";
			DataConnection dcon=new DataConnection();
			DataTable tRaw=dcon.GetTable(command);
			DataSet retVal=new DataSet();
			DataTable tComm=new DataTable("Commlog");
			tComm.Columns.Add("CommDateTime",typeof(DateTime));
			tComm.Columns.Add("CommType");//1
			tComm.Columns.Add("Mode");//2
			tComm.Columns.Add("SentOrReceived");//3
			tComm.Columns.Add("Note");//4
			tComm.Columns.Add("CommlogNum");//5
			tComm.Columns.Add("EmailMessageNum");//6
			tComm.Columns.Add("FormPatNum");//7
			DataRow row;
			//Commlog------------------------------------------------------------------------------------------
			for(int i=0;i<tRaw.Rows.Count;i++){
				if((CommItemType)PIn.PInt(tRaw.Rows[i][1].ToString())==CommItemType.StatementSent){
					continue;
				}
				row=tComm.NewRow();
				row[0]=PIn.PDateT(tRaw.Rows[i][0].ToString());
				row[1]=Lan.g("enumCommItemType",((CommItemType)PIn.PInt(tRaw.Rows[i][1].ToString())).ToString());
				row[2]=Lan.g("enumCommItemMode",((CommItemMode)PIn.PInt(tRaw.Rows[i][2].ToString())).ToString());
				row[3]=Lan.g("enumCommSentOrReceived",((CommSentOrReceived)PIn.PInt(tRaw.Rows[i][3].ToString())).ToString());
				row[4]=tRaw.Rows[i][3].ToString();
				row[5]=tRaw.Rows[i][4].ToString();
				row[6]="0";
				row[7]="0";
				tComm.Rows.Add(row);
			}
			//emailmessage---------------------------------------------------------------------------------------
			command="SELECT MsgDateTime,SentOrReceived,Subject,EmailMessageNum "
				+"FROM emailmessage WHERE PatNum ='"+POut.PInt(patNum)+"' ORDER BY MsgDateTime";
			tRaw=dcon.GetTable(command);
			for(int i=0;i<tRaw.Rows.Count;i++) {
				row=tComm.NewRow();
				row[0]=PIn.PDateT(tRaw.Rows[i][0].ToString());
				//row[1]="";//type
				row[2]=Lan.g("enumCommItemMode",CommItemMode.Email.ToString());
				if(tRaw.Rows[i][1].ToString()=="0"){
					row[3]=Lan.g("ModuleAccount","Unsent");
				}
				else{
					row[3]=Lan.g("enumCommSentOrReceived",((CommSentOrReceived)PIn.PInt(tRaw.Rows[i][1].ToString())).ToString());
				}
				row[4]=tRaw.Rows[i][2].ToString();//note
				row[5]="0";
				row[6]=tRaw.Rows[i][3].ToString();
				row[7]="0";
				tComm.Rows.Add(row);
			}
			//formpat---------------------------------------------------------------------------------------
			command="SELECT FormDateTime,FormPatNum "
				+"FROM formpat WHERE PatNum ='"+POut.PInt(patNum)+"' ORDER BY FormDateTime";
			tRaw=dcon.GetTable(command);
			for(int i=0;i<tRaw.Rows.Count;i++) {
				row=tComm.NewRow();
				row[0]=PIn.PDateT(tRaw.Rows[i][0].ToString());
				row[1]=Lan.g("ModuleAccount","Form");//type
				row[2]="";//mode
				row[3]=Lan.g("enumCommSentOrReceived","Received");
				row[4]="";//note
				row[5]="0";
				row[6]="0";
				row[7]=tRaw.Rows[i][1].ToString();
				tComm.Rows.Add(row);
			}
			DataView view = tComm.DefaultView;
			view.Sort = "CommDateTime";
			tComm = view.ToTable();
			retVal.Tables.Add(tComm);
			return retVal;
		}

	}


	public class DtoModulesGetAccount:DtoQueryBase {
		public int PatNum;
	}

}
