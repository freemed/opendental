using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTests {
	class ToothT {
		/*
		private void ToothFormatRanges(){
			PrefC.DictRef=new Dictionary<string,Pref>();
			Pref pref=new Pref();
			pref.PrefName="UseInternationalToothNumbers";
			pref.ValueString="0";
			PrefC.DictRef.Add(pref.PrefName,pref);
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
			textResults.Text+="ToothFormatRanges complete.\r\n";
			//we still haven't tested really bad input.
		}*/
	}
}
