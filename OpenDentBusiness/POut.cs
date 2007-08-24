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

	///<summary>Converts various datatypes into strings formatted correctly for MySQL. "P" was originally short for Parameter because this class was written specifically to replace parameters in the mysql queries. Using strings instead of parameters is much easier to debug.  This will later be rewritten as a System.IConvertible interface on custom mysql types.  I would rather not ever depend on the mysql connector for this so that this program remains very db independent.</summary>
	public class POut{
		public static string PObject(object value) {
			if (value == null)
				return string.Empty;

			Type dataType = value.GetType();

			if (dataType == typeof(string)) {
				return '\'' + PString((string)value) + '\'';
			}
			else if (dataType.IsEnum) {
				return ((int)value).ToString();
			}
			else if (dataType == typeof(Bitmap)) {
				return PBitmap((Bitmap)value);
			}
			else if (dataType == typeof(bool)) {
				return PBool((bool)value);
			}
			else if (dataType == typeof(Byte)) {
				return PByte((Byte)value);
			}
			else if (dataType == typeof(DateTime)) {
				return PDate((DateTime)value);
			}
			else if (dataType == typeof(TimeSpan)) {
				return PTimeSpan((TimeSpan)value);
			}
			else if (dataType == typeof(double)) {
				return PDouble((double)value);
			}
			else if (dataType == typeof(float)) {
				return PFloat((float)value);
			}
			else if (dataType == typeof(int)) {
				return PInt((int)value);
			}
			else if(dataType == typeof(short)) {
				return PShort((short)value);
			}
			else {
				throw new NotSupportedException(string.Format(Resources.DataTypeNotSupportedByPOut, dataType.Name));
			}
		}

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
		public static string PDateT(DateTime myDateT) {
			return PDateT(myDateT,true);
		}

		///<summary></summary>
		public static string PDateT(DateTime myDateT,bool encapsulate){
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
				return "";//this saves zero's to the database
			}
		}

		///<summary>Converts a date to yyyy-MM-dd format which is the format required by MySQL. myDate is the date you want to convert. encapsulate is true for the first overload, making the result look like this: 'yyyy-MM-dd' for MySQL.</summary>
		public static string PDate(DateTime myDate){
			return PDate(myDate,true);
		}

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

		public static string PTimeSpan(TimeSpan myTimeSpan) {
			return PTimeSpan(myTimeSpan, true);
		}

		public static string PTimeSpan(TimeSpan myTimeSpan, bool encapsulate) {
			try {
				string outTimeSpan = myTimeSpan.ToString();
				string frontCap = "'";
				string backCap = "'";
				if (encapsulate) {
					outTimeSpan = frontCap + outTimeSpan + backCap;
				}
				return outTimeSpan;
			}
			catch {
				return "";//this saves zero's to the database
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










