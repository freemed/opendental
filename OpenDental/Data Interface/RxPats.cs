using System;
using System.Collections;
using System.Data;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{
	///<summary></summary>
	public class RxPats {
		///<summary></summary>
		public static RxPat[] Refresh(int patNum) {
			string command="SELECT * FROM rxpat"
				+" WHERE PatNum = '"+POut.PInt(patNum)+"'"
				+" ORDER BY RxDate";
			DataTable table=General.GetTable(command);
			RxPat[] List=new RxPat[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++) {
				List[i]=new RxPat();
				List[i].RxNum      = PIn.PInt(table.Rows[i][0].ToString());
				List[i].PatNum     = PIn.PInt(table.Rows[i][1].ToString());
				List[i].RxDate     = PIn.PDate(table.Rows[i][2].ToString());
				List[i].Drug       = PIn.PString(table.Rows[i][3].ToString());
				List[i].Sig        = PIn.PString(table.Rows[i][4].ToString());
				List[i].Disp       = PIn.PString(table.Rows[i][5].ToString());
				List[i].Refills    = PIn.PString(table.Rows[i][6].ToString());
				List[i].ProvNum    = PIn.PInt(table.Rows[i][7].ToString());
				List[i].Notes      = PIn.PString(table.Rows[i][8].ToString());
			}
			return List;
		}

		///<summary></summary>
		public static RxPat GetRx(int rxNum) {
			string command="SELECT * FROM rxpat"
				+" WHERE RxNum = "+POut.PInt(rxNum);
			DataTable table=General.GetTable(command);
			RxPat rx=new RxPat();
			rx.RxNum      = PIn.PInt(table.Rows[0][0].ToString());
			rx.PatNum     = PIn.PInt(table.Rows[0][1].ToString());
			rx.RxDate     = PIn.PDate(table.Rows[0][2].ToString());
			rx.Drug       = PIn.PString(table.Rows[0][3].ToString());
			rx.Sig        = PIn.PString(table.Rows[0][4].ToString());
			rx.Disp       = PIn.PString(table.Rows[0][5].ToString());
			rx.Refills    = PIn.PString(table.Rows[0][6].ToString());
			rx.ProvNum    = PIn.PInt(table.Rows[0][7].ToString());
			rx.Notes      = PIn.PString(table.Rows[0][8].ToString());
			return rx;
		}

		///<summary></summary>
		public static void Update(RxPat rx) {
			string command= "UPDATE rxpat SET " 
				+ "PatNum = '"      +POut.PInt   (rx.PatNum)+"'"
				+ ",RxDate = "     +POut.PDate  (rx.RxDate)
				+ ",Drug = '"       +POut.PString(rx.Drug)+"'"
				+ ",Sig = '"        +POut.PString(rx.Sig)+"'"
				+ ",Disp = '"       +POut.PString(rx.Disp)+"'"
				+ ",Refills = '"    +POut.PString(rx.Refills)+"'"
				+ ",ProvNum = '"    +POut.PInt   (rx.ProvNum)+"'"
				+ ",Notes = '"      +POut.PString(rx.Notes)+"'"
				+" WHERE RxNum = '" +POut.PInt   (rx.RxNum)+"'";
			General.NonQ(command);
		}

		///<summary></summary>
		public static void Insert(RxPat rx) {
			if(PrefB.RandomKeys) {
				rx.RxNum=MiscData.GetKey("rxpat","RxNum");
			}
			string command="INSERT INTO rxpat (";
			if(PrefB.RandomKeys) {
				command+="RxNum,";
			}
			command+="PatNum,RxDate,Drug,Sig,Disp,Refills,ProvNum,Notes) VALUES(";
			if(PrefB.RandomKeys) {
				command+="'"+POut.PInt(rx.RxNum)+"', ";
			}
			command+=
				 "'"+POut.PInt   (rx.PatNum)+"', "
				+POut.PDate  (rx.RxDate)+", "
				+"'"+POut.PString(rx.Drug)+"', "
				+"'"+POut.PString(rx.Sig)+"', "
				+"'"+POut.PString(rx.Disp)+"', "
				+"'"+POut.PString(rx.Refills)+"', "
				+"'"+POut.PInt   (rx.ProvNum)+"', "
				+"'"+POut.PString(rx.Notes)+"')";
			if(PrefB.RandomKeys) {
				General.NonQ(command);
			}
			else{
				rx.RxNum=General.NonQ(command,true);
			}
		}

		///<summary></summary>
		public static void Delete(int rxNum) {
			string command= "DELETE from rxpat WHERE RxNum = '"+POut.PInt(rxNum)+"'";
			General.NonQ(command);
		}



	
	

		
	}

	


}













