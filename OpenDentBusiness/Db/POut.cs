using System;
using System.Collections;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using OpenDentBusiness.Properties;
//using System.Windows.Forms;

namespace OpenDentBusiness{

	///<summary>Converts various datatypes into strings formatted correctly for MySQL. "P" was originally short for Parameter because this class was written specifically to replace parameters in the mysql queries. Using strings instead of parameters is much easier to debug. I would rather not ever depend on the mysql connector for this because the authors of the connector have been known to suddenly change its behavior.</summary>
	public class POut{

		///<summary></summary>
		public static string Bool (bool myBool){
			if (myBool==true){
				return "1";
			}
			else{
				return "0";
			}
		}

		///<summary></summary>
		public static string Byte (byte myByte){
			return myByte.ToString();
		}

		///<summary>Always encapsulates the result, depending on the current database connection.</summary>
		public static string DateT(DateTime myDateT) {
			return DateT(myDateT,true);
		}

		///<summary></summary>
		public static string DateT(DateTime myDateT,bool encapsulate){
			if(myDateT.Year<1880) {
				myDateT=DateTime.MinValue;
			}
			try{
				string outDate=myDateT.ToString("yyyy-MM-dd HH:mm:ss",CultureInfo.InvariantCulture);//new DateTimeFormatInfo());
				string frontCap="'";
				string backCap="'";
				if(DataConnection.DBtype==DatabaseType.Oracle) {
					frontCap="TO_DATE('";
					backCap="','YYYY-MM-DD HH24:MI:SS')";
				}
				if(encapsulate) {
					outDate=frontCap+outDate+backCap;
				}
				return outDate;
			}
			catch{
				return "";//this saves zero's to a mysql database
			}
		}

		///<summary>Converts a date to yyyy-MM-dd format which is the format required by MySQL. myDate is the date you want to convert. encapsulate is true for the first overload, making the result look like this: 'yyyy-MM-dd' for MySQL.</summary>
		public static string Date(DateTime myDate){
			return Date(myDate,true);
		}

		public static string Date(DateTime myDate,bool encapsulate){
			//js I commented this out Jan 2010 because we do not want this method to behave unexpectedly.
			//As a result, we have already had one bug in the recall, and we might have more.
			//But this must not be reverted.
			//if(myDate.Year<1880) {
			//	myDate=DateTime.MinValue;
			//}
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

		///<summary>Timespans that might be invalid time of day.  Can be + or - and can be up to 800+ hours.  Stored in Oracle as varchar2.  Never encapsulates</summary>
		public static string TSpan(TimeSpan myTimeSpan) {
			if(myTimeSpan==System.TimeSpan.Zero) {
				return "00:00:00"; ;
			}
			try {
				string retval="";
				if(myTimeSpan < System.TimeSpan.Zero) {
					retval+="-";
					myTimeSpan=myTimeSpan.Duration();
				}
				int hours=(myTimeSpan.Days*24)+myTimeSpan.Hours;
				retval+=hours.ToString().PadLeft(2,'0')+":"+myTimeSpan.Minutes.ToString().PadLeft(2,'0')+":"+myTimeSpan.Seconds.ToString().PadLeft(2,'0');
				return retval;
			} 
			catch {
				return "00:00:00";
			}
		}

		///<summary>Timespans that are guaranteed to always be a valid time of day.  No negatives or hours over 24.  Stored in Oracle as datetime.  Encapsulated by default.</summary>
		public static string Time(TimeSpan myTimeSpan) {
			return POut.Time(myTimeSpan,true);
		}

		///<summary>Timespans that are guaranteed to always be a valid time of day.  No negatives or hours over 24.  Stored in Oracle as datetime.  Encapsulated by default.</summary>
		public static string Time(TimeSpan myTimeSpan,bool encapsulate) {
			string retval=myTimeSpan.Hours.ToString().PadLeft(2,'0')+":"+myTimeSpan.Minutes.ToString().PadLeft(2,'0')+":"+myTimeSpan.Seconds.ToString().PadLeft(2,'0');
			if(encapsulate) {
				if(DataConnection.DBtype==DatabaseType.MySql) {
					return "'"+retval+"'";
				}
				else {//Oracle
					return "TO_TIMESTAMP('"+retval+"','HH24:MI:SS')";
				}
			}
			else {
				return retval;
			}
		}

		///<summary></summary>
		public static string Double(double myDouble){
			try{
				//because decimal is a comma in Europe, this sends it to db with period instead 
				return myDouble.ToString("f",new NumberFormatInfo());
			}
			catch{
				return "0";
			}
		}

		///<summary></summary>
		public static string Long (long myLong){
			return myLong.ToString();
		}

		///<summary></summary>
		public static string Int(int myInt) {
			return myInt.ToString();
		}

		//public static string Short(short myShort) {
		//	return myShort.ToString();
		//}

		///<summary></summary>
		public static string Float(float myFloat){
			return myFloat.ToString();
		}

		///<summary>Escapes all necessary characters.</summary>
		public static string String(string myString){
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
		public static string Bitmap(System.Drawing.Bitmap bitmap) {
			if(bitmap==null) {
				return "";
			}
			using(MemoryStream stream=new MemoryStream()) {
				bitmap.Save(stream,ImageFormat.Png);//was Bmp, so there will be a mix of different kinds.
				byte[] rawData=stream.ToArray();
				return Convert.ToBase64String(rawData);
			}
		}

		///<summary>Converts the specified wav file into a string representation.  The timing of this is a little different than with the other "P" functions and is only used by the import button in FormSigElementDefEdit.  After that, the wav spends the rest of it's life as a string until "played" or exported.</summary>
		public static string Sound(string filename) {
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










