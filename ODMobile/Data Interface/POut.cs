using System;
using System.Collections;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace OpenDentMobile{
	public class POut{
		///<summary></summary>
		public static string PBool (bool myBool){
			if (myBool==true){
				return "1";
			}
			else{
				return "0";
			}
		}

		///<summary></summary>
		public static string PByte (byte myByte){
			return myByte.ToString();
		}

		///<summary>Unlike the main program, this never encapsulates.</summary>
		public static string PDateT(DateTime myDateT){
			if(myDateT.Year<1880) {
				myDateT=DateTime.MinValue;
			}
			try{
				return myDateT.ToString("yyyy-MM-dd HH:mm:ss",CultureInfo.InvariantCulture);//new DateTimeFormatInfo());
			}
			catch{
				return "0001-01-01";
			}
		}

		///<summary>Unlike the main program, this never encapsulates.</summary>
		public static string PDate(DateTime myDate){
			if(myDate.Year<1880) {
				myDate=DateTime.MinValue;
			}
			try{
				//the new DTFormatInfo is to prevent changes in year for Korea
				return myDate.ToString("yyyy-MM-dd",new DateTimeFormatInfo());
			}
			catch{
				return "0001-01-01";
			}
		}

		///<summary></summary>
		public static string PDouble(double myDouble){
			try{
				//because decimal is a comma in Europe, this sends it to db with period instead 
				return myDouble.ToString("f",new NumberFormatInfo());
			}
			catch{
				return "0";
			}
		}

		///<summary></summary>
		public static string PInt (int myInt){
			return myInt.ToString();
		}

		public static string PShort(short myShort) {
			return myShort.ToString();
		}

		///<summary></summary>
		public static string PFloat(float myFloat){
			return myFloat.ToString();
		}

		///<summary>This is different than in the main program.  Escapes all necessary characters.  Semicolons are always removed because they would cause problems with multiple commands in a single statement.</summary>
		public static string PString(string myString){
			if(myString==null) {
				return "";
			}
			StringBuilder strBuild=new StringBuilder();
			for(int i=0;i<myString.Length;i++){
				switch(myString.Substring(i,1)){
					//note. When using binary data, must escape ',",\, and nul(? haven't done nul)
					case "'":  strBuild.Append(@"''");	break;// ' replaced by ''
					//case "\"": strBuild.Append("\\\"");	break;// " replaced by \"
					//case @"\": strBuild.Append(@"\\"); break;//single \ replaced by \\
					//case "\r": strBuild.Append(@"\r"); break;//carriage return(usually followed by new line)
					//case "\n": strBuild.Append(@"\n"); break;//new line
					//case "\t": strBuild.Append(@"\t"); break;//tab
					//unique to the mobile version:
					case ";": break;//don't add the ;
					default: strBuild.Append(myString.Substring(i,1)); break;
				}
			}
			return strBuild.ToString();
		}



	}

	


}










