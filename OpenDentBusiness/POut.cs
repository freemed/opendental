using System;
using System.Collections;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
//using System.Windows.Forms;

namespace OpenDentBusiness{

	///<summary>Converts various datatypes into strings formatted correctly for MySQL. "P" was originally short for Parameter because this class was written specifically to replace parameters in the mysql queries. Using strings instead of parameters is much easier to debug.  This will later be rewritten as a System.IConvertible interface on custom mysql types.  I would rather not ever depend on the mysql connector for this so that this program remains very db independent.</summary>
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

		///<summary>Always encapsulates the result, depending on the current database connection.</summary>
		public static string PDateT(DateTime myDateT){
			if(myDateT.Year<1880) {
				myDateT=DateTime.MinValue;
			}
			try{
				string outDate=myDateT.ToString("yyyy-MM-dd HH:mm:ss",CultureInfo.InvariantCulture);//new DateTimeFormatInfo());
				if(DataConnection.DBtype==DatabaseType.Oracle) {
					return "TO_DATE('"+outDate+"','YYYY-MM-DD HH24:MI:SS')";
				}
				return "'"+outDate+"'";
			}
			catch{
				return "";//this actually saves zero's to the database
			}
		}

		public static string PDate(DateTime myDate){
			return PDate(myDate,true);
		}

		///<summary>Converts a date to yyyy-MM-dd format which is the format required by MySQL. myDate is the date you want to convert. preText is text that should be placed prior to the date output text but after the leading encapsulating character (if any). postText is text that should be placed after the date output text but before the ending encapsulating character (if any).</summary>
		public static string PDate(DateTime myDate,bool encapsulate){
			if(myDate.Year<1880) {
				myDate=DateTime.MinValue;
			}
			try{
				//the new DTFormatInfo is to prevent changes in year for Korea
				string outDate=myDate.ToString("yyyy-MM-dd",new DateTimeFormatInfo());
				string frontCap="'";
				string backCap="'";
				if(DataConnection.DBtype==DatabaseType.Oracle){
					frontCap="TO_DATE('";
					backCap="','YYYY-MM-DD')";
				}
				if(encapsulate){
					outDate=frontCap+outDate+backCap;
				}
				return outDate;
			}
			catch{
				//return "0000-00-00";
				return "";//this saves zeros to the database
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

		///<summary></summary>
		public static string PFloat(float myFloat){
			return myFloat.ToString();
		}

		///<summary></summary>
		public static string PString(string myString){
			if(myString==null) {
				return "";
			}
			if(DataConnection.DBtype!=DatabaseType.MySql){
				if(myString.Contains(";")){
					myString=myString.Replace(";","");
				}
				if(myString.Contains("'")) {
					myString=myString.Replace("'","");
				}
				if(myString==null) {
					return "";
				}
			}
			StringBuilder strBuild=new StringBuilder();
			for(int i=0;i<myString.Length;i++){
				switch(myString.Substring(i,1)){
					//note. When using binary data, must escape ',",\, and nul(? haven't done nul)
					case "'": strBuild.Append(@"\'");	break;// ' replaced by \'
					case "\"": strBuild.Append("\\\"");	break;// " replaced by \"
					case @"\": strBuild.Append(@"\\"); break;//single \ replaced by \\
					case "\r": strBuild.Append(@"\r"); break;//carriage return(usually followed by new line)
					case "\n": strBuild.Append(@"\n"); break;//new line
					case "\t": strBuild.Append(@"\t"); break;//tab
					default: strBuild.Append(myString.Substring(i,1)); break;
				}
			}
			//The old slow way of doing it:
			/*string newString="";
			for(int i=0;i<myString.Length;i++){
				switch (myString.Substring(i,1)){
					case "'": newString+=@"\'"; break;
					case @"\": newString+=@"\\"; break;//single \ replaced by \\
					case "\r": newString+=@"\r"; break;//carriage return(usually followed by new line)
					case "\n": newString+=@"\n"; break;//new line
					case "\t": newString+=@"\t"; break;//tab
						//case "%": newString+="\\%"; break;//causes errors because only ambiguous in LIKE clause
						//case "_": newString+="\\_"; break;//see above
					default : newString+=myString.Substring(i,1); break;
				}//end switch
			}//end for*/
			//MessageBox.Show(strBuild.ToString());
			return strBuild.ToString();
		}

		//<summary></summary>
		//public static string PTimee (string myTime){
		//	return DateTime.Parse(myTime).ToString("HH:mm:ss");
		//}

		/*
		///<summary></summary>
		public static string PBitmap(Bitmap bitmap) {
			if(bitmap==null){
				return "";
			}
			MemoryStream stream=new MemoryStream();
			bitmap.Save(stream,ImageFormat.Bmp);
			byte[] rawData=stream.ToArray();
			return Convert.ToBase64String(rawData);
		}*/

		///<summary></summary>
		public static string PBitmap(Bitmap bitmap) {
			if(bitmap==null) {
				return "";
			}
			MemoryStream stream=new MemoryStream();
			bitmap.Save(stream,ImageFormat.Bmp);
			byte[] rawData=stream.ToArray();
			return Convert.ToBase64String(rawData);
		}

		///<summary>Converts the specified wav file into a string representation.  The timing of this is a little different than with the other "P" functions and is only used by the import button in FormSigElementDefEdit.  After that, the wav spends the rest of it's life as a string until "played" or exported.</summary>
		public static string PSound(string filename) {
			if(!File.Exists(filename)) {
				throw new ApplicationException("File does not exist.");
			}
			if(!filename.EndsWith(".wav")){
				throw new ApplicationException("Filename must end with .wav");
			}
			FileStream stream=new FileStream(filename,FileMode.Open,FileAccess.Read,FileShare.ReadWrite);
			byte[] rawData=new byte[stream.Length];
			stream.Read(rawData,0,(int)stream.Length);
			return Convert.ToBase64String(rawData);
		}

		///<summary>The supplied string should already be in safe base64 format, and should not need any special escaping.  The purpose of this function is to enforce that the supplied string meets these requirements.  This is done quickly.</summary>
		public static string Base64(string myString){
			if(myString==null){
				return "";
			}
			if(!Regex.IsMatch(myString,"[A-Z0-9]*")){
				throw new ApplicationException("Characters found that do not match base64 format.");
			}
			return myString;
		}

	}

	


}










