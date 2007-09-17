using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;
using OpenDental;
using OpenDental.UI;

namespace UnitTests {
	public partial class FormUnitTests:Form {
		public FormUnitTests() {
			InitializeComponent();
		}

		private void FormUnitTests_Load(object sender,EventArgs e) {
			BenefitComputeRenewDate();
			ToothFormatRanges();
			//LabDueDate();
			textResults.Text+="Done.";
			textResults.SelectionStart=textResults.Text.Length;
		}

		private void BenefitComputeRenewDate(){
			DateTime asofDate=new DateTime(2006,3,19);
			bool isCalendarYear=true;
			DateTime insStartDate=new DateTime(2003,3,1);
			DateTime result=BenefitB.ComputeRenewDate(asofDate,isCalendarYear,insStartDate);
			if(result!=new DateTime(2006,1,1)){
				textResults.Text+="BenefitComputeRenewDate 1 failed.\r\n";
			}
			isCalendarYear=false;//for the remaining tests
			//earlier in same month
			result=BenefitB.ComputeRenewDate(asofDate,isCalendarYear,insStartDate);
			if(result!=new DateTime(2006,3,1)) {
				textResults.Text+="BenefitComputeRenewDate 2 failed.\r\n";
			}
			//earlier month in year
			asofDate=new DateTime(2006,5,1);
			result=BenefitB.ComputeRenewDate(asofDate,isCalendarYear,insStartDate);
			if(result!=new DateTime(2006,3,1)) {
				textResults.Text+="BenefitComputeRenewDate 3 failed.\r\n";
			}
			asofDate=new DateTime(2006,12,1);
			result=BenefitB.ComputeRenewDate(asofDate,isCalendarYear,insStartDate);
			if(result!=new DateTime(2006,3,1)) {
				textResults.Text+="BenefitComputeRenewDate 4 failed.\r\n";
			}
			//later month in year
			asofDate=new DateTime(2006,2,1);
			result=BenefitB.ComputeRenewDate(asofDate,isCalendarYear,insStartDate);
			if(result!=new DateTime(2005,3,1)) {
				textResults.Text+="BenefitComputeRenewDate 5 failed.\r\n";
			}
			asofDate=new DateTime(2006,2,12);
			result=BenefitB.ComputeRenewDate(asofDate,isCalendarYear,insStartDate);
			if(result!=new DateTime(2005,3,1)) {
				textResults.Text+="BenefitComputeRenewDate 6 failed.\r\n";
			}
		}

		private void ToothFormatRanges(){
			PrefB.HList=new Hashtable();
			Pref pref=new Pref();
			pref.PrefName="UseInternationalToothNumbers";
			pref.ValueString="0";
			PrefB.HList.Add(pref.PrefName,pref);
			//for display----------------------------------------------------------------
			string inputrange="1,2,3,4,5,7,8,9,11,12,15,16,17,18,21,22,23";
			string result=Tooth.FormatRangeForDisplay(inputrange);
			string desired="1-5,7-9,11,12,15,16,17,18,21-23";
			if(result!=desired){
				textResults.Text+="ToothFormatRangeForDisplay failed.  Desired: "+desired+" Result: "+result+"\r\n";
			}
			inputrange="2,4,5,7,8,9,11,12,13,14,15,16,17,18,19,20,21,22,23,25";
			result=Tooth.FormatRangeForDisplay(inputrange);
			desired="2,4,5,7-9,11-16,17-23,25";
			if(result!=desired) {
				textResults.Text+="ToothFormatRangeForDisplay failed.  Desired: "+desired+" Result: "+result+"\r\n";
			}
			inputrange="4,5,2, 7,8,9,11 ,13,12,14,15,16,17,18 ,20, 21,22,23,19,25";
			result=Tooth.FormatRangeForDisplay(inputrange);
			desired="2,4,5,7-9,11-16,17-23,25";
			if(result!=desired) {
				textResults.Text+="ToothFormatRangeForDisplay failed.  Desired: "+desired+" Result: "+result+"\r\n";
			}
			//for database------------------------------------------------------------------
			inputrange="1-5,7-9,11,12,15,16,17,18,21-23";
			result=Tooth.FormatRangeForDb(inputrange);
			desired="1,2,3,4,5,7,8,9,11,12,15,16,17,18,21,22,23";
			if(result!=desired) {
				textResults.Text+="ToothFormatRangeForDb failed.  Desired: "+desired+" Result: "+result+"\r\n";
			}
			inputrange="2,4,5,7-9,11-16,17-23,25";
			result=Tooth.FormatRangeForDb(inputrange);
			desired="2,4,5,7,8,9,11,12,13,14,15,16,17,18,19,20,21,22,23,25";
			if(result!=desired) {
				textResults.Text+="ToothFormatRangeForDb failed.  Desired: "+desired+" Result: "+result+"\r\n";
			}
			inputrange="4,2,5,7-9 ,11-16,25 ,  17-23";
			result=Tooth.FormatRangeForDb(inputrange);
			desired="2,4,5,7,8,9,11,12,13,14,15,16,17,18,19,20,21,22,23,25";
			if(result!=desired) {
				textResults.Text+="ToothFormatRangeForDb failed.  Desired: "+desired+" Result: "+result+"\r\n";
			}
			//we still haven't tested really bad input.
		}


		/*
		private void LabDueDate(){
			DateTime startdate=new DateTime(2007,5,3);//this is a Thursday
			DateTime result=LabTurnarounds.ComputeDueDate(startdate,1);
			DateTime desired=new DateTime(2007,5,4,17,0,0);//Friday, 5pm
			if(result!=desired) {
				textResults.Text+="LabDueDate failed.  Desired: "+desired.ToString()+" Result: "+result.ToString()+"\r\n";
			}
			result=LabTurnarounds.ComputeDueDate(startdate,2);
			desired=new DateTime(2007,5,7,17,0,0);//Monday, 5pm
			if(result!=desired) {
				textResults.Text+="LabDueDate failed.  Desired: "+desired.ToString()+" Result: "+result.ToString()+"\r\n";
			}
			result=LabTurnarounds.ComputeDueDate(startdate,5);
			desired=new DateTime(2007,5,10,17,0,0);//Thurs, 5pm
			if(result!=desired) {
				textResults.Text+="LabDueDate failed.  Desired: "+desired.ToString()+" Result: "+result.ToString()+"\r\n";
			}
			result=LabTurnarounds.ComputeDueDate(startdate,10);
			desired=new DateTime(2007,5,17,17,0,0);//Thurs, 5pm
			if(result!=desired) {
				textResults.Text+="LabDueDate failed.  Desired: "+desired.ToString()+" Result: "+result.ToString()+"\r\n";
			}
		}*/


	}
}