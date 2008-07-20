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
			SheetDef sheetDef=SheetsInternal.GetSheetDef(SheetInternalType.LabelPatientMail);
			Sheet sheet=SheetUtil.CreateSheet(sheetDef);
			SheetParameter.SetParameter(sheet,"PatNum",patNum);
			SheetFiller.FillFields(sheet);
			try{
				SheetPrinting.Print(sheet);
			}
			catch(Exception ex){
				MessageBox.Show(ex.Message);
			}
		}

		public void PrintPatientLFAddress(int patNum) {
			SheetDef sheetDef=SheetsInternal.GetSheetDef(SheetInternalType.LabelPatientLFAddress);
			Sheet sheet=SheetUtil.CreateSheet(sheetDef);
			SheetParameter.SetParameter(sheet,"PatNum",patNum);
			SheetFiller.FillFields(sheet);
			try {
				SheetPrinting.Print(sheet);
			}
			catch(Exception ex) {
				MessageBox.Show(ex.Message);
			}
		}

		public void PrintPatientLFChartNumber(int patNum) {
			SheetDef sheetDef=SheetsInternal.GetSheetDef(SheetInternalType.LabelPatientLFChartNumber);
			Sheet sheet=SheetUtil.CreateSheet(sheetDef);
			SheetParameter.SetParameter(sheet,"PatNum",patNum);
			SheetFiller.FillFields(sheet);
			try {
				SheetPrinting.Print(sheet);
			}
			catch(Exception ex) {
				MessageBox.Show(ex.Message);
			}
		}

		public void PrintPatientLFPatNum(int patNum) {
			SheetDef sheetDef=SheetsInternal.GetSheetDef(SheetInternalType.LabelPatientLFPatNum);
			Sheet sheet=SheetUtil.CreateSheet(sheetDef);
			SheetParameter.SetParameter(sheet,"PatNum",patNum);
			SheetFiller.FillFields(sheet);
			try {
				SheetPrinting.Print(sheet);
			}
			catch(Exception ex) {
				MessageBox.Show(ex.Message);
			}
		}

		public void PrintPatRadiograph(int patNum) {
			SheetDef sheetDef=SheetsInternal.GetSheetDef(SheetInternalType.LabelPatientRadiograph);
			Sheet sheet=SheetUtil.CreateSheet(sheetDef);
			SheetParameter.SetParameter(sheet,"PatNum",patNum);
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
			SheetDef sheetDef=SheetsInternal.GetSheetDef(SheetInternalType.LabelCarrier);
			List<Sheet> sheetBatch=SheetUtil.CreateBatch(sheetDef,carrierNums);
			try{
				SheetPrinting.PrintBatch(sheetBatch);
			}
			catch(Exception ex){
				MessageBox.Show(ex.Message);
			}
		}

		///<summary>Have to supply printer name because this can be called multiple times in a loop. Returns false if fails.</summary>
		public void PrintCarrier(int carrierNum){//Carrier carrierCur,string printerName){
			SheetDef sheetDef=SheetsInternal.GetSheetDef(SheetInternalType.LabelCarrier);
			Sheet sheet=SheetUtil.CreateSheet(sheetDef);
			SheetParameter.SetParameter(sheet,"CarrierNum",carrierNum);
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
			SheetDef sheetDef=SheetsInternal.GetSheetDef(SheetInternalType.LabelReferral);
			Sheet sheet=SheetUtil.CreateSheet(sheetDef);
			SheetParameter.SetParameter(sheet,"ReferralNum",referralNum);
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
