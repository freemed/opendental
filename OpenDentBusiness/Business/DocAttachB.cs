using System;
using System.Data;
using System.Diagnostics;
using System.Collections.Generic;
using System.Text;

namespace OpenDentBusiness {
	internal class DocAttachB{
		///<summary></summary>
		internal static void Insert(DocAttach attach) {
			if(PrefB.RandomKeys) {
				attach.DocAttachNum=MiscDataB.GetKey("docattach","DocAttachNum");
			}
			string command="INSERT INTO docattach (";
			if(PrefB.RandomKeys) {
				command+="DocAttachNum,";
			}
			command+="PatNum, DocNum) VALUES(";
			if(PrefB.RandomKeys) {
				command+="'"+POut.PInt(attach.DocAttachNum)+"', ";
			}
			command+=
				 "'"+POut.PInt(attach.PatNum)+"', "
				+"'"+POut.PInt(attach.DocNum)+"')";
			DataConnection dcon=new DataConnection();
			if(PrefB.RandomKeys) {
				dcon.NonQ(command);
			}
			else {
				dcon.NonQ(command,true);
				attach.DocAttachNum=dcon.InsertID;
			}
		}


		
		
		






	}



}
