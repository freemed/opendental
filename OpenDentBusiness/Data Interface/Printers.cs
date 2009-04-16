using System;
using System.Collections;
using System.Data;
using System.Drawing.Printing;

namespace OpenDentBusiness{

	///<summary>Handles all the business logic for printers.  Used heavily by the UI.  Every single function that makes changes to the database must be completely autonomous and do ALL validation itself.</summary>
	public class Printers{
		///<summary>List of all printers.</summary>
		private static Printer[] list;
		
		///<summary></summary>
		public static void Refresh(){
			string command="SELECT * FROM printer";
			list=RefreshAndFill(command);
		}

		///<summary></summary>
		public static Printer GetOnePrinter(PrintSituation sit,int compNum){
			string command="SELECT * FROM printer WHERE "
				+"PrintSit = '"      +POut.PInt((int)sit)+"' "
				+"AND ComputerNum ='"+POut.PInt(compNum)+"'";
			Printer[] printerList=RefreshAndFill(command);
			if(printerList.Length==0){
				return null;
			}
			return printerList[0];
		}

		private static Printer[] RefreshAndFill(string command){
 			DataTable table=General.GetTable(command);
			Printer[] pList=new Printer[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++){
				pList[i]=new Printer();
				pList[i].PrinterNum    = PIn.PInt   (table.Rows[i][0].ToString());
				pList[i].ComputerNum   = PIn.PInt   (table.Rows[i][1].ToString());
				pList[i].PrintSit      =(PrintSituation)PIn.PInt(table.Rows[i][2].ToString());
				pList[i].PrinterName   = PIn.PString(table.Rows[i][3].ToString());
				pList[i].DisplayPrompt = PIn.PBool  (table.Rows[i][4].ToString());
			}
			return pList;
		}

		///<summary></summary>
		private static void Insert(Printer cur){
			if(PrefC.RandomKeys){
				cur.PrinterNum=MiscData.GetKey("printer","PrinterNum");
			}
			string command= "INSERT INTO printer (";
			if(PrefC.RandomKeys){
				command+="PrinterNum,";
			}
			command+="ComputerNum,PrintSit,PrinterName,"
				+"DisplayPrompt) VALUES(";
			if(PrefC.RandomKeys){
				command+="'"+POut.PInt(cur.PrinterNum)+"', ";
			}
			command+=
				 "'"+POut.PInt   (cur.ComputerNum)+"', "
				+"'"+POut.PInt   ((int)cur.PrintSit)+"', "
				+"'"+POut.PString(cur.PrinterName)+"', "
				+"'"+POut.PBool  (cur.DisplayPrompt)+"')";
			//MessageBox.Show(string command);
 			if(PrefC.RandomKeys){
				General.NonQ(command);
			}
			else{
 				cur.PrinterNum=General.NonQ(command,true);
			}
		}

		///<summary></summary>
		private static void Update(Printer cur){
			string command="UPDATE printer SET "
				+"ComputerNum = '"   +POut.PInt   (cur.ComputerNum)+"' "
				+",PrintSit = '"     +POut.PInt   ((int)cur.PrintSit)+"' "
				+",PrinterName = '"  +POut.PString(cur.PrinterName)+"' "
				+",DisplayPrompt = '"+POut.PBool  (cur.DisplayPrompt)+"' "
				+"WHERE PrinterNum = '"+POut.PInt(cur.PrinterNum)+"'";
 			General.NonQ(command);
		}

		///<summary></summary>
		private static void Delete(Printer cur){
			string command="DELETE FROM printer "
				+"WHERE PrinterNum = "+POut.PInt(cur.PrinterNum);
			General.NonQ(command);
		}

		public static bool PrinterIsInstalled(string name){
			for(int i=0;i<PrinterSettings.InstalledPrinters.Count;i++){
				if(PrinterSettings.InstalledPrinters[i]==name){
					return true;
				}
			}
			return false;
		}

		///<summary>Gets the set printer whether or not it is valid.</summary>
		public static Printer GetForSit(PrintSituation sit){
			if(list==null) {
				Refresh();
			}
			Computer compCur=Computers.GetCur();
			for(int i=0;i<list.Length;i++){
				if(list[i].ComputerNum==compCur.ComputerNum
					&& list[i].PrintSit==sit)
				{
					return list[i];
				}
			}
			return null;
		}

		///<summary>Either does an insert or an update to the database if need to create a Printer object.  Or it also deletes a printer object if needed.</summary>
		public static void PutForSit(PrintSituation sit,string computerName, string printerName,bool displayPrompt){
			//Computer[] compList=Computers.Refresh();
			//Computer compCur=Computers.GetCur();
			string command="SELECT ComputerNum FROM computer "
				+"WHERE CompName = '"+POut.PString(computerName)+"'";
 			DataTable table=General.GetTable(command);
			if(table.Rows.Count==0){
				return;//computer not yet entered in db.
			}
			int compNum=PIn.PInt(table.Rows[0][0].ToString());
			Printer existing=GetOnePrinter(sit,compNum);   //GetForSit(sit);
			if(printerName=="" && !displayPrompt){//then should not be an entry in db
				if(existing!=null){//need to delete Printer
					Delete(existing);
				}
			}
			else if(existing==null){
				Printer cur=new Printer();
				cur.ComputerNum=compNum;
				cur.PrintSit=sit;
				cur.PrinterName=printerName;
				cur.DisplayPrompt=displayPrompt;
				Insert(cur);
			}
			else{
				existing.PrinterName=printerName;
				existing.DisplayPrompt=displayPrompt;
				Update(existing);
			}
		}

		///<summary>Called from FormPrinterSetup if user selects the easy option.  Since the other options will be hidden, we have to clear them.  User should be sternly warned before this happens.</summary>
		public static void ClearAll(){
			//first, delete all entries
			string command="DELETE FROM printer";
 			General.NonQ(command);
			//then, add one printer for each computer. Default and show prompt
			Computers.Refresh();
			Printer cur;
			for(int i=0;i<Computers.List.Length;i++){
				cur=new Printer();
				cur.ComputerNum=Computers.List[i].ComputerNum;
				cur.PrintSit=PrintSituation.Default;
				cur.PrinterName="";
				cur.DisplayPrompt=true;
				Insert(cur);
			}
		}

	}
}