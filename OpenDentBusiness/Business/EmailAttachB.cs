using System;
using System.Data;
using System.Collections.Generic;
using System.Text;

namespace OpenDentBusiness {
	internal class EmailAttachB{
		internal static void Insert(EmailAttach attach){
			if(PrefB.RandomKeys) {
				attach.EmailAttachNum=MiscDataB.GetKey("emailattach","EmailAttachNum");
			}
			string command= "INSERT INTO emailattach (";
			if(PrefB.RandomKeys) {
				command+="EmailAttachNum,";
			}
			command+="EmailMessageNum, DisplayedFileName, ActualFileName) VALUES(";
			if(PrefB.RandomKeys) {
				command+="'"+POut.PInt(attach.EmailAttachNum)+"', ";
			}
			command+=
				 "'"+POut.PInt   (attach.EmailMessageNum)+"', "
				+"'"+POut.PString(attach.DisplayedFileName)+"', "
				+"'"+POut.PString(attach.ActualFileName)+"')";
			//MessageBox.Show(cmd.CommandText);
			DataConnection dcon=new DataConnection();
			dcon.NonQ(command);
		}
		
	



	}



}
