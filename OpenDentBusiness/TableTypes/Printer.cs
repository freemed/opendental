using System;
using System.Collections;

namespace OpenDentBusiness{
	
	///<summary>One printer selection for one situation for one computer.</summary>
	public class Printer{
		///<summary>Primary key.</summary>
		public int PrinterNum;
		///<summary>FK to computer.ComputerNum.  This will be changed some day to refer to the computername, because it would make more sense as a key than a cryptic number.</summary>
		public int ComputerNum;
		///<summary>Enum:PrintSituation One of about 10 different situations where printing takes place.  If no printer object exists for a situation, then a default is used and a prompt is displayed.</summary>
		public PrintSituation PrintSit;
		///<summary>The name of the printer as set from the specified computer.</summary>
		public string PrinterName;
		///<summary>If true, then user will be prompted for printer.  Otherwise, print directly with little user interaction.</summary>
		public bool DisplayPrompt;

		/*//<summary>Returns a copy of the clearinghouse.</summary>
    public ClaimForm Copy(){
			ClaimForm cf=new ClaimForm();
			cf.ClaimFormNum=ClaimFormNum;
			cf.Description=Description;
			return cf;
		}*/

		
	}



	



}









