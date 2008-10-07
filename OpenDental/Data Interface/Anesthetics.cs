using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{
	///<summary></summary>
	public class Anesthetics {

		///<summary>Refresh Anesthetic Data</summary>
		/*public static AnestheticData Refresh() {
			string command = "SELECT * FROM anestheticdata ORDER by AnestheticDataNum";
			return FillFromCommand(command);
		}*/

		///<summary>Gets one anesthetic data sheet from database</summary>
		/*public static AnestheticData GetOne(int anestheticDataNum) {
			string command="SELECT * FROM anestheticdata WHERE AnestheticDataNum="+POut.PInt(anestheticDataNum);
			return FillFromCommand(command);
		}*/

		/*private static AnestheticData FillFromCommand(string command){
			
			}*/
		

}
		///<summary></summary>
		/*public static void Insert(AnestheticData AnestheticDataNum){
			if(PrefC.RandomKeys) {
				anestheticData.AnestheticDataNum=MiscData.GetKey("anestheticDataNum","AnestheticDataNum");
			}
			string command="INSERT INTO anestheticdata (";
			if(PrefC.RandomKeys) {
				command+="AnestheticDataNum,";
			}

			if(PrefC.RandomKeys) {
				General.NonQ(command);
			}
			else {
				anestheticDataNum.AnestheticDataNum=General.NonQ(command,true);
			}
		}*/

		///<summary></summary>
		/*public static void Update(AnestheticData anestheticData){
			string command= "UPDATE anestheticData SET " 
				+ "AnesthOpen = '"    +POut.PString(anestheticData.AnesthOpen )+"'"
				//+ ",Phone = '"         +POut.PString(lab.Phone)+"'"
				//+ ",Notes = '"         +POut.PString(lab.Notes)+"'"
				//+ ",LabSlip = '"       +POut.PString(lab.LabSlip)+"'"
				+" WHERE AnestheticDataNum = '" +POut.PInt(anestheticData.AnestheticDataNum)+"'";
 			General.NonQ(command);
		}*/


	
		
}

















