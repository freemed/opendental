using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace OpenDentBusiness{
	/// <summary></summary>
	public class Tooth{
		///<summary></summary>
		public Tooth(){
			
		}

		///<summary></summary>
		public static bool IsAnterior(string toothNum){
			if(!IsValidDB(toothNum))
				return false;
			int intTooth=ToInt(toothNum);
			if(intTooth>=6 && intTooth<=11)
				return true;
			if(intTooth>=22 && intTooth<=27)
				return true;
			return false;
		}

		///<summary></summary>
		public static bool IsAnterior(int intTooth){
			string toothNum=FromInt(intTooth);
			return IsAnterior(toothNum);
		}

		///<summary></summary>
		public static bool IsPosterior(string toothNum){
			if(!IsValidDB(toothNum))
				return false;
			int intTooth=ToInt(toothNum);
			if(intTooth>=1 && intTooth<=5)
				return true;
			if(intTooth>=12 && intTooth<=21)
				return true;
			if(intTooth>=28 && intTooth<=32)
				return true;
			return false;
		}

		///<summary></summary>
		public static bool IsPosterior(int intTooth){
			string toothNum=FromInt(intTooth);
			return IsPosterior(toothNum);
		}

		///<summary>toothNum gets validated here.</summary>
		public static bool IsMolar(string toothNum){
			if(!IsValidDB(toothNum))
				return false;
			int intTooth=ToInt(toothNum);
			if(intTooth>=1 && intTooth<=3)
				return true;
			if(intTooth>=14 && intTooth<=19)
				return true;
			if(intTooth>=30 && intTooth<=32)
				return true;
			return false;
		}

		///<summary></summary>
		public static bool IsMolar(int intTooth){
			string toothNum=FromInt(intTooth);
			return IsMolar(toothNum);
		}

		///<summary>toothNum gets validated here. Used for FGC insurance substitutions.</summary>
		public static bool IsSecondMolar(string toothNum) {
			if(!IsValidDB(toothNum))
				return false;
			int intTooth=ToInt(toothNum);
			if(intTooth==2 || intTooth==15 || intTooth==18 || intTooth==31){
				return true;
			}
			return false;
		}

		///<summary></summary>
		public static bool IsPreMolar(string toothNum){
			if(!IsValidDB(toothNum))
				return false;
			int intTooth=ToInt(toothNum);
			if(intTooth==4 
				|| intTooth==5
				|| intTooth==12
				|| intTooth==13
				|| intTooth==20
				|| intTooth==21
				|| intTooth==28
				|| intTooth==29)
				return true;
			return false;
		}

		///<summary></summary>
		public static bool IsPreMolar(int intTooth){
			string toothNum=FromInt(intTooth);
			return IsPreMolar(toothNum);
		}

		///<summary>Sometimes validated by IsValidDB before coming here, otherwise an invalid toothnum .  This should be run on all displayed tooth numbers. It will handle checking for whether user is using international tooth numbers.  All tooth numbers are passed in american values until the very last moment.  Just before display, the string is converted using this method.</summary>
		public static string ToInternat(string toothNum){
			//if not using international tooth numbers, no change.
			if(!PrefB.GetBool("UseInternationalToothNumbers")){
				//((Pref)PrefB.HList[]).ValueString=="0"){
				return toothNum;
			}
			if(toothNum==null || toothNum=="")
				return "";
			int intToothI=0;//the international tooth number we will find
			int intTooth=0;
			try{
				intTooth=ToInt(toothNum);//this gives us the american 1-32. Primary are 4-13,20-29
			}
			catch{
				return "";//for situations where no validation was performed
			}
			if(IsPrimary(toothNum)){
				if(intTooth>=4 && intTooth<=8){//UR= 51-55
					intToothI=59-intTooth;
				}
				else if(intTooth>=9 && intTooth<=13){//UL= 61-65
					intToothI=52+intTooth;
				}
				else if(intTooth>=20 && intTooth<=24){//LL= 71-75
					intToothI=95-intTooth;
				}
				else if(intTooth>=25 && intTooth<=29){//LR= 81-85
					intToothI=56+intTooth;
				}
			}
			else{//adult toothnum
				if(intTooth>=1 && intTooth<=8){//UR= 11-18
					intToothI=19-intTooth;
				}
				else if(intTooth>=9 && intTooth<=16){//UL= 21-28
					intToothI=12+intTooth;
				}
				else if(intTooth>=17 && intTooth<=24){//LL= 31-38
					intToothI=55-intTooth;
				}
				else if(intTooth>=25 && intTooth<=32){//LR= 41-48
					intToothI=16+intTooth;
				}
			}
			return intToothI.ToString();
		}

		///<summary>MUST be validated by IsValidEntry before coming here.  All user entered toothnumbers are run through this method which automatically checks to see if using international toothnumbers.  So the procedurelog class will always contain the american toothnum.</summary>
		public static string FromInternat(string toothNum){
			//if not using international tooth numbers, no change.
			if(((Pref)PrefB.HList["UseInternationalToothNumbers"]).ValueString=="0"){
				return toothNum;
			}
			int intTooth=0;
			int intToothI=Convert.ToInt32(toothNum);
			if(intToothI>=11 && intToothI<=18){//UR perm
				intTooth=19-intToothI;
				return intTooth.ToString();
			}
			if(intToothI>=21 && intToothI<=28){//UL perm
				intTooth=intToothI-12;
				return intTooth.ToString();
			}
			if(intToothI>=31 && intToothI<=38){//LL perm
				intTooth=55-intToothI;
				return intTooth.ToString();
			}
			if(intToothI>=41 && intToothI<=48){//LR perm
				intTooth=intToothI-16;
				return intTooth.ToString();
			}
			if(intToothI>=51 && intToothI<=55){//UR pri
				intTooth=59-intToothI;
				return PermToPri(intTooth.ToString());
			}
			if(intToothI>=61 && intToothI<=65){//UL pri
				intTooth=intToothI-52;
				return PermToPri(intTooth.ToString());
			}
			if(intToothI>=71 && intToothI<=75){//LL pri
				intTooth=95-intToothI;
				return PermToPri(intTooth.ToString());
			}
			if(intToothI>=81 && intToothI<=85){//LR pri
				intTooth=intToothI-56;
				return PermToPri(intTooth.ToString());
			}
			return "";//should never happen
		}
		
		///<summary>The supplied toothNumbers will be a series of tooth numbers separated by commas.  They will be in american format..  For display purposes, ranges will use dashes, and international numbers will be used.</summary>
		public static string FormatRangeForDisplay(string toothNumbers){
			if(toothNumbers==null) {
				return "";
			}
			toothNumbers=toothNumbers.Replace(" ","");//remove all spaces
			if(toothNumbers==""){
				return "";
			}
			string[] toothArray=toothNumbers.Split(',');
			if(toothArray.Length<=2){
				return toothNumbers;//just two numbers separated by comma
			}
			Array.Sort(toothArray,CompareTeethOrdinal);
			StringBuilder strbuild=new StringBuilder();
			//List<string> toothList=new List<string>();
			//strbuild.Append(Tooth.ToInternat(toothArray[0]));//always show the first number
			int currentNum;
			int nextNum;
			int numberInaRow=1;//must have 3 in a row to trigger dash
			for(int i=0;i<toothArray.Length-1;i++){
				//in each loop, we are comparing the current number with the next number
				currentNum=Tooth.ToOrdinal(toothArray[i]);
				nextNum=Tooth.ToOrdinal(toothArray[i+1]);
				if(nextNum-currentNum==1 && currentNum!=16 && currentNum!=32){//if sequential (sequences always break at end of arch)
					numberInaRow++;
				}
				else{
					numberInaRow=1;
				}
				if(numberInaRow<3){//the next number is not sequential,or if it was a sequence, and it's now broken
					if(strbuild.Length>0 && strbuild[strbuild.Length-1]!='-'){
						strbuild.Append(",");
					}
					strbuild.Append(Tooth.ToInternat(toothArray[i]));
				}
				else if(numberInaRow==3){//this way, the dash only gets added exactly once
					strbuild.Append("-");
				}
				//else do nothing
			}
			if(strbuild.Length>0 && strbuild[strbuild.Length-1]!='-') {
				strbuild.Append(",");
			}
			strbuild.Append(toothArray[toothArray.Length-1]);//always show the last number
			return strbuild.ToString();
		}

		///<summary>A generic comparison that puts primary teeth after perm teeth.</summary>
		private static int CompareTeethOrdinal(string toothA,string toothB){
			return ToOrdinal(toothA).CompareTo(ToOrdinal(toothB));
		}

		///<summary>Takes a user entered string and validates/formats it for the database.  Throws an ApplicationException if any formatting errors.  User string can contain spaces, dashes, and commas, too.</summary>
		public static string FormatRangeForDb(string toothNumbers){
			if(toothNumbers==null) {
				return "";
			}
			toothNumbers=toothNumbers.Replace(" ","");//remove all spaces
			if(toothNumbers=="") {
				return "";
			}
			string[] toothArray=toothNumbers.Split(',');//some items will contain dashes
			List<string> toothList=new List<string>();
			string rangebegin;
			string rangeend;
			int beginint;
			int endint;
			//not sure how to handle supernumerary.  Probably just not acceptable.
			for(int i=0;i<toothArray.Length;i++){
				if(toothArray[i].Contains("-")){
					rangebegin=toothArray[i].Split('-')[0];
					rangeend=toothArray[i].Split('-')[1];
					if(!IsValidEntry(rangebegin)) {
						throw new ApplicationException(rangebegin+" "+Lan.g("Tooth","is not a valid tooth number."));
					}
					if(!IsValidEntry(rangeend)) {
						throw new ApplicationException(rangeend+" "+Lan.g("Tooth","is not a valid tooth number."));
					}
					beginint=Tooth.ToOrdinal(FromInternat(rangebegin));
					endint=Tooth.ToInt(FromInternat(rangeend));
					if(endint<beginint){
						throw new ApplicationException("Range specified is impossible.");
					}
					while(beginint<=endint){
						toothList.Add(Tooth.FromOrdinal(beginint));
						beginint++;
					}
				}
				else{
					if(!IsValidEntry(toothArray[i])){
						throw new ApplicationException(toothArray[i]+" "+Lan.g("Tooth","is not a valid tooth number."));
					}
					toothList.Add(Tooth.FromInternat(toothArray[i]));
				}
			}
			toothList.Sort(CompareTeethOrdinal);
			string retVal="";
			for(int i=0;i<toothList.Count;i++){
				if(i>0){
					retVal+=",";
				}
				retVal+=toothList[i];
			}
			return retVal;
		}

		///<summary>Used every time user enters toothNum in procedure box. Must be followed with FromInternat. These are the *ONLY* methods that are designed to accept user input.  Can also handle international toothnum</summary>
		public static bool IsValidEntry(string toothNum){
			//international
			if(PrefB.GetBool("UseInternationalToothNumbers")){
				if(toothNum==null || toothNum=="")
					return false;
				Regex regex=new Regex("^[1-4][1-8]$");//perm teeth: matches firt digit 1-4 and second digit 1-8,9 would be supernumerary?
				if(regex.IsMatch(toothNum))
					return true;
				regex=new Regex("^[5-8][1-5]$");//pri teeth: matches firt digit 5-8 and second digit 1-5
				if(regex.IsMatch(toothNum))
					return true;
				return false;
			}	
			else{//american
				//tooth numbers validated the same as they are in db.
				return IsValidDB(toothNum);
			}
		}

		///<summary>Intended to validate toothNum coming in from database. Will not handle any international tooth nums since all database teeth are in US format.</summary>
		public static bool IsValidDB(string toothNum){
			if(toothNum==null || toothNum=="")
				return false;
			if(Regex.IsMatch(toothNum,"^[A-T]$"))
				return true;
			if(Regex.IsMatch(toothNum,"^[A-T]S$"))//supernumerary
				return true;
			if(!Regex.IsMatch(toothNum,@"^[1-9]\d?$")){//matches 1 or 2 digits, leading 0 not allowed
				return false;
			}
			int intTooth=Convert.ToInt32(toothNum);
			if(intTooth<=32)
				return true;
			if(intTooth>=51 && intTooth<=82)//supernumerary
				return true;	
			return false;
		}

		///<summary></summary>
		public static bool IsSuperNum(string toothNum){
			if(toothNum==null || toothNum=="")
				return false;
			if(Regex.IsMatch(toothNum,"^[A-T]$"))
				return false;
			if(Regex.IsMatch(toothNum,"^[A-T]S$"))//supernumerary
				return true;
			if(!Regex.IsMatch(toothNum,@"^[1-9]\d?$")){//matches 1 or 2 digits, leading 0 not allowed
				return false;
			}
			int intTooth=Convert.ToInt32(toothNum);
			if(intTooth<=32)
				return false;
			if(intTooth>=51 && intTooth<=82)//supernumerary
				return true;	
			return false;
		}

		///<summary>The toothNum should be validated before coming here, but it won't crash if invalid.  Primary or perm are ok.  Empty and null are also ok.</summary>
		public static int ToInt(string toothNum){
			if(toothNum==null || toothNum=="")
				return -1;
			try{
				if(IsPrimary(toothNum)){
					return Convert.ToInt32(PriToPerm(toothNum));
				}
				else{
					return Convert.ToInt32(toothNum);
				}
			}
			catch{
				return -1;
			}
		}

		///<summary></summary>
		public static string FromInt(int intTooth){
			//don't need much error checking.
			string retStr="";
			retStr=intTooth.ToString();
			return retStr;
		}

		///<summary></summary>
		public static bool IsPrimary(string toothNum){
			if(Regex.IsMatch(toothNum,"^[A-T]$")){
				return true;
			}
			if(Regex.IsMatch(toothNum,"^[A-T]S$")){
				return true;
			}
			return false;
		}

		///<summary></summary>
		public static string PermToPri(string toothNum){
			switch(toothNum){
				default: return "";
				case "4": return "A";
				case "5": return "B";
				case "6": return "C";
				case "7": return "D";
				case "8": return "E";
				case "9": return "F";
				case "10": return "G";
				case "11": return "H";
				case "12": return "I";
				case "13": return "J";
				case "20": return "K";
				case "21": return "L";
				case "22": return "M";
				case "23": return "N";
				case "24": return "O";
				case "25": return "P";
				case "26": return "Q";
				case "27": return "R";
				case "28": return "S";
				case "29": return "T";
			}
		}

		///<summary></summary>
		public static string PermToPri(int intTooth){
			string toothNum=FromInt(intTooth);
			return PermToPri(toothNum);
		}

		///<summary></summary>
		public static string PriToPerm(string toothNum){
			switch(toothNum){
				default: return "";
				case "A": return "4";
				case "B": return "5";
				case "C": return "6";
				case "D": return "7";
				case "E": return "8";
				case "F": return "9";
				case "G": return "10";
				case "H": return "11";
				case "I": return "12";
				case "J": return "13";
				case "K": return "20";
				case "L": return "21";
				case "M": return "22";
				case "N": return "23";
				case "O": return "24";
				case "P": return "25";
				case "Q": return "26";
				case "R": return "27";
				case "S": return "28";
				case "T": return "29";
			}
		}
		
		///<summary>Used to put perm and pri into a single array.  1-32 is perm.  33-52 is pri.</summary>
		public static int ToOrdinal(string toothNum){
			//
			if(IsPrimary(toothNum)){
				switch(toothNum){
					default: return -1;
					case "A": return 33;
					case "B": return 34;
					case "C": return 35;
					case "D": return 36;
					case "E": return 37;
					case "F": return 38;
					case "G": return 39;
					case "H": return 40;
					case "I": return 41;
					case "J": return 42;
					case "K": return 43;
					case "L": return 44;
					case "M": return 45;
					case "N": return 46;
					case "O": return 47;
					case "P": return 48;
					case "Q": return 49;
					case "R": return 50;
					case "S": return 51;
					case "T": return 52;
				}
			}
			else{//perm
				return ToInt(toothNum);
			}
		}

		///<summary>Assumes ordinal is valid.</summary>
		public static string FromOrdinal(int ordinal){
			if(ordinal<1 || ordinal>52){
				return "1";//just so it won't crash.
			}
			if(ordinal<33){
				return ordinal.ToString();
			}
			if(ordinal<43){
				return Tooth.PermToPri(ordinal-29);
			}
			return Tooth.PermToPri(ordinal-23);
		}
			
		///<summary></summary>
		public static bool IsMaxillary(int intTooth){
			string toothNum=FromInt(intTooth);
			return IsMaxillary(toothNum);
		}

		///<summary></summary>
		public static bool IsMaxillary(string toothNum){
			if(!IsValidDB(toothNum))
				return false;
			int intTooth=ToInt(toothNum);
			if(intTooth>=1 && intTooth<=16)
				return true;
			return false;
		}

		///<summary>Setting forClaims to true converts V surfaces to either F or B.  toothNum might be empty, and a tidy should still be attempted.  Otherwise, toothNum must be valid.</summary>
		public static string SurfTidy(string surf,string toothNum,bool forClaims){
			//yes... this might be a little more elegant with a regex
			bool isCanadian=CultureInfo.CurrentCulture.Name.Substring(3)=="CA";//en-CA or fr-CA
			//Canadian valid=MOIDBLV
			if(surf==null){
				//MessageBox.Show("null");
				surf="";
			}
      string surfTidy="";
			//bool isPosterior=false;
			//if(toothNum=="" || !IsAnterior(toothNum))
			//	isPosterior=true;
			//bool isAnterior=false;
			//if(toothNum=="" || !IsPosterior(toothNum))
			//	isAnterior=true;
      ArrayList al=new ArrayList();
      for(int i=0;i<surf.Length;i++){
        al.Add(surf.Substring(i,1).ToUpper());
      }
			//M----------------------------------------
      if(al.Contains("M")){
        surfTidy+="M";
      }
			//O-------------------------------------------
			if(toothNum=="" || IsPosterior(toothNum)){
				if(al.Contains("O")){
					surfTidy+="O";
				}
			}
			//I---------------------------------
			if(toothNum=="" || IsAnterior(toothNum)){
				if(al.Contains("I")) {
					surfTidy+="I";
				}
			}
      //D---------------------------------------
      if(al.Contains((string)"D")){
        surfTidy+="D";
      }
			//B/F/V------------------------------------------------
			if(toothNum==""){
				if(al.Contains("B")) {
					surfTidy+="B";
				}
				if(al.Contains("F")) {
					surfTidy+="F";
				}
				if(al.Contains("V")) {
					surfTidy+="V";
				}
			}				
			else if(forClaims){
				if(isCanadian){
					if(IsPosterior(toothNum)) {
						if(al.Contains("B") || al.Contains("V")) {
							surfTidy+="B";
						}
					}
					if(IsAnterior(toothNum)) {
						if(al.Contains("F") || al.Contains("V")) {
							surfTidy+="V";//vestibular
						}
					}
				}
				else{//not Canadian
					if(IsPosterior(toothNum)){
						if(al.Contains("B") || al.Contains("V")) {
							surfTidy+="B";
						}
					}
					if(IsAnterior(toothNum)){
						if(al.Contains("F") || al.Contains("V")) {
							surfTidy+="F";
						}
					}
				}
			}
			else{
				if(al.Contains("V")) {
					surfTidy+="V";
				}
				if(IsPosterior(toothNum)) {
					if(al.Contains("B")) {
						surfTidy+="B";
					}
				}
				if(IsAnterior(toothNum)) {
					if(al.Contains("F")) {
						surfTidy+="F";
					}
				}
			}
			//L-----------------------------------------
      if(al.Contains((string)"L")){
        surfTidy+="L";
      }
      return surfTidy;      
    }


	}
}
