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
			SheetFiller.FillFields(sheet);
			try{
				SheetPrinting.Print(sheet);
			}
			catch(Exception ex){
				MessageBox.Show(ex.Message);
			}
		}

		public void PrintPatientLFAddress(int patNum) {
			Sheet sheet=SheetsInternal.LabelPatientLFAddress;
			sheet.SetParameter("PatNum",patNum);
			SheetFiller.FillFields(sheet);
			try {
				SheetPrinting.Print(sheet);
			}
			catch(Exception ex) {
				MessageBox.Show(ex.Message);
			}
		}

		public void PrintPatientLFChartNumber(int patNum) {
			Sheet sheet=SheetsInternal.LabelPatientLFChartNumber;
			sheet.SetParameter("PatNum",patNum);
			SheetFiller.FillFields(sheet);
			try {
				SheetPrinting.Print(sheet);
			}
			catch(Exception ex) {
				MessageBox.Show(ex.Message);
			}
		}

		public void PrintPatientLFPatNum(int patNum) {
			Sheet sheet=SheetsInternal.LabelPatientLFChartNumber;
			sheet.SetParameter("PatNum",patNum);
			SheetFiller.FillFields(sheet);
			try {
				SheetPrinting.Print(sheet);
			}
			catch(Exception ex) {
				MessageBox.Show(ex.Message);
			}
		}

		public void PrintPatRadiograph(int patNum) {
			Sheet sheet=SheetsInternal.LabelPatientRadiograph;
			sheet.SetParameter("PatNum",patNum);
			SheetFiller.FillFields(sheet);
			try {
				SheetPrinting.Print(sheet);
			}
			catch(Exception ex) {
				MessageBox.Show(ex.Message);
			}
		}

		///<summary></summary>
		public void PrintCarriers(List<int> carrierNums){
			Sheet sheet=SheetsInternal.LabelCarrier;
			List<Sheet> sheetBatch=SheetUtil.CreateBatch(sheet,carrierNums);
			try{
				SheetPrinting.PrintBatch(sheetBatch);
			}
			catch(Exception ex){
				MessageBox.Show(ex.Message);
			}
		}

		///<summary>Have to supply printer name because this can be called multiple times in a loop. Returns false if fails.</summary>
		public void PrintCarrier(int carrierNum){//Carrier carrierCur,string printerName){
			Sheet sheet=SheetsInternal.LabelCarrier;
			sheet.SetParameter("CarrierNum",carrierNum);
			SheetFiller.FillFields(sheet);
			try {
				SheetPrinting.Print(sheet);
			}
			catch(Exception ex) {
				MessageBox.Show(ex.Message);
			}
		}

		///<summary></summary>
		public void PrintReferral(int referralNum) {
			Sheet sheet=SheetsInternal.LabelReferral;
			sheet.SetParameter("ReferralNum",referralNum);
			SheetFiller.FillFields(sheet);
			try {
				SheetPrinting.Print(sheet);
			}
			catch(Exception ex) {
				MessageBox.Show(ex.Message);
			}
		}

		

	}

}
