using System;
using System.Collections;
using System.Data;

namespace OpenDentBusiness{
	///<summary></summary>
	public class EmailAttaches{

		public static void Insert(EmailAttach attach) {
			if(PrefC.RandomKeys) {
				attach.EmailAttachNum=MiscData.GetKey("emailattach","EmailAttachNum");
			}
			string command= "INSERT INTO emailattach (";
			if(PrefC.RandomKeys) {
				command+="EmailAttachNum,";
			}
			command+="EmailMessageNum, DisplayedFileName, ActualFileName) VALUES(";
			if(PrefC.RandomKeys) {
				command+="'"+POut.PInt(attach.EmailAttachNum)+"', ";
			}
			command+=
				 "'"+POut.PInt(attach.EmailMessageNum)+"', "
				+"'"+POut.PString(attach.DisplayedFileName)+"', "
				+"'"+POut.PString(attach.ActualFileName)+"')";
			if(PrefC.RandomKeys) {
				General.NonQ(command);
			}
			else{
				attach.EmailAttachNum=General.NonQ(command,true);
			}
		}


	}

	


}









