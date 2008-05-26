using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{
	///<summary></summary>
	public class LabelSingle{
		//private PrintDocument pd;
		//private Patient Pat;
		//private Carrier CarrierCur;
		//private Referral ReferralCur;

		///<summary></summary>
		public LabelSingle(){
			
		}

		///<summary></summary>
		public void PrintPat(int patNum){
			Sheet sheet=SheetsInternal.LabelPatientMail;
			sheet.SetParameter("PatNum",patNum);
			try{
				sheet.Print();
			}
			catch(Exception ex){
				MessageBox.Show(ex.Message);
			}
		}

		public void PrintPatientLFAddress(int patNum) {
			Sheet sheet=SheetsInternal.LabelPatientLFAddress;
			sheet.SetParameter("PatNum",patNum);
			try {
				sheet.Print();
			}
			catch(Exception ex) {
				MessageBox.Show(ex.Message);
			}
		}

		public void PrintPatientLFChartNumber(int patNum) {
			Sheet sheet=SheetsInternal.LabelPatientLFChartNumber;
			sheet.SetParameter("PatNum",patNum);
			try {
				sheet.Print();
			}
			catch(Exception ex) {
				MessageBox.Show(ex.Message);
			}
		}

		public void PrintPatientLFPatNum(int patNum) {
			Sheet sheet=SheetsInternal.LabelPatientLFChartNumber;
			sheet.SetParameter("PatNum",patNum);
			try {
				sheet.Print();
			}
			catch(Exception ex) {
				MessageBox.Show(ex.Message);
			}
		}

		public void PrintPatRadiograph(int patNum) {
			Sheet sheet=SheetsInternal.LabelPatientRadiograph;
			sheet.SetParameter("PatNum",patNum);
			try {
				sheet.Print();
			}
			catch(Exception ex) {
				MessageBox.Show(ex.Message);
			}
		}

		///<summary></summary>
		public void PrintCarriers(List<int> carrierNums){
			//PrintDocument pd=new PrintDocument();//only used to pass printerName
			//if(!Printers.SetPrinter(pd,PrintSituation.LabelSingle)) {
			//	return;
			//}
			Sheet sheet=SheetsInternal.LabelCarrier;
			sheet.SetParameter("CarrierNum",carrierNums);
			try{
				sheet.Print(true);
				//foreach(int carrierNum in carrierNums){
				//	sheet=SheetsInternal.LabelCarrier;
				//	sheet.SetParameter("CarrierNum",carrierNum);
				//	sheet.Print(pd.PrinterSettings.PrinterName);
				//}
			}
			catch(Exception ex){
				MessageBox.Show(ex.Message);
			}
		}

		///<summary>Have to supply printer name because this can be called multiple times in a loop. Returns false if fails.</summary>
		public void PrintCarrier(int carrierNum){//Carrier carrierCur,string printerName){
			Sheet sheet=SheetsInternal.LabelCarrier;
			sheet.SetParameter("CarrierNum",carrierNum);
			try {
				sheet.Print();
			}
			catch(Exception ex) {
				MessageBox.Show(ex.Message);
			}
		}

		///<summary></summary>
		public void PrintReferral(int referralNum) {
			Sheet sheet=SheetsInternal.LabelReferral;
			sheet.SetParameter("ReferralNum",referralNum);
			try {
				sheet.Print();
			}
			catch(Exception ex) {
				MessageBox.Show(ex.Message);
			}
		}

		

	}

}
