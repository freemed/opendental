using System;
using System.Collections;
using System.Drawing;
//using System.Drawing.Imaging;
using System.IO;
using System.Text;
//using System.Windows.Forms;

namespace OpenDentBusiness{
	
	/*=========================================================================================
	=================================== class PIn ===========================================*/
	///<summary>"P" stands for Parameter.  Converts strings coming in from user input into the appropriate type.</summary>
	public class PIn{
		///<summary></summary>
		public static Bitmap Bitmap(string myString) {
			if(myString==null || myString.Length<0x32) {//Bitmaps require a minimum length for header info.
				return null;
			}
			try {
				byte[] rawData=Convert.FromBase64String(myString);
				MemoryStream stream=new MemoryStream(rawData);
				System.Drawing.Bitmap image=new System.Drawing.Bitmap(stream);
				return image;
			}
			catch {
				return null;
			}
		}

		///<summary></summary>
		public static bool Bool (string myString){
			return myString=="1";
		}

		///<summary></summary>
		public static byte Byte (string myString){
			if(myString==""){
				return 0;
			}
			else{
				return System.Convert.ToByte(myString);
			}
		}

		///<summary>Some versions of MySQL return a GROUP_CONCAT as a string, and other versions return it as a byte array.  This method handles either way, making it work smoothly with different versions.</summary>
		public static string ByteArray(object obj) {
			if(obj.GetType()==typeof(Byte[])) {
				Byte[] bytes=(Byte[])obj;
				StringBuilder strbuild=new StringBuilder();
				for(int i=0;i<bytes.Length;i++) {
					strbuild.Append((char)bytes[i]);
				}
				return strbuild.ToString();
			}
			else {//string
				return obj.ToString();
			}
		}

		///<summary></summary>
		public static DateTime Date(string myString){
			if(myString=="" || myString==null)
				return DateTime.MinValue;
			try{
				return (DateTime.Parse(myString));
				//return DateTime.Parse(myString,CultureInfo.InvariantCulture);
			}
			catch{
				return DateTime.MinValue;
			}
		}

		///<summary></summary>
		public static DateTime DateT(string myString){
			if(myString=="")
				return DateTime.MinValue;
			//if(myString=="0000-00-00 00:00:00")//useless
			//	return DateTime.MinValue;
			try{
				return (DateTime.Parse(myString));
			}
			catch{
				return DateTime.MinValue;
			}
		}
		
		///<summary>If blank or invalid, returns 0. Otherwise, parses.</summary>
		public static decimal Decimal (string myString){
			if(myString==""){
				return 0;
			}
			else{
				try{
					return System.Convert.ToDecimal(myString);
				}
				catch{
					//MessageBox.Show("Error converting "+myString+" to decimal");
					return 0;
				}
			}
		}

		///<summary>If blank or invalid, returns 0. Otherwise, parses.</summary>
		public static double Double(string myString) {
			if(myString=="") {
				return 0;
			}
			else {
				try {
					return System.Convert.ToDouble(myString);
				}
				catch {
					//MessageBox.Show("Error converting "+myString+" to double");
					return 0;
				}
			}
		}

		///<summary></summary>
		public static int Int(string myString) {
			if(myString=="") {
				return 0;
			}
			else {
				return System.Convert.ToInt32(myString);
			}
		}

		///<summary></summary>
		public static float Float(string myString) {
			if(myString=="") {
				return 0;
			}
			return System.Convert.ToSingle(myString);
		}

		///<summary></summary>
		public static long Long (string myString){
			if(myString==""){
				return 0;
			}
			else{
				return System.Convert.ToInt64(myString);
			}
		}

		///<summary></summary>
		public static short Short(string myString) {
			if(myString == "") {
				return 0;
			}
			else {
				return System.Convert.ToInt16(myString);
			}
		}

		///<summary>Saves the string representation of a sound into a .wav file.  The timing of this is different than with the other "P" functions, and is only used by the export button in FormSigElementDefEdit</summary>
		public static void Sound(string sound,string filename) {
			if(!filename.EndsWith(".wav")) {
				throw new ApplicationException("Filename must end with .wav");
			}
			byte[] rawData=Convert.FromBase64String(sound);
			FileStream stream=new FileStream(filename,FileMode.Create,FileAccess.Write);
			stream.Write(rawData,0,rawData.Length);
		}
		
		///<summary>Currently does nothing.</summary>
		public static string String (string myString){
			return myString;
		}

		///<summary>Timespans that might be invalid time of day.  Can be + or - and can be up to 800+ hours.  Stored in Oracle as varchar2.</summary>
		public static TimeSpan TSpan(string myString) {
			if(string.IsNullOrEmpty(myString)) {
				return System.TimeSpan.Zero;
			}
			try {
				if(DataConnection.DBtype==DatabaseType.Oracle) {
					//return System.TimeSpan.Parse(myString); //Does not work. Confuses hours with days and an exception is thrown in our large timespan test.
					bool isNegative=false;
					if(myString.StartsWith("-")) {
						isNegative=true;
						myString=myString.Substring(1);//remove the '-'
					}
					string[] timeValues=myString.Split(new char[] { ':' });
					if(timeValues.Length!=3) {
						return System.TimeSpan.Zero;
					}
					TimeSpan retval=new TimeSpan(PIn.Int(timeValues[0]),PIn.Int(timeValues[1]),PIn.Int(timeValues[2]));
					if(isNegative) {
						return retval.Negate();
					}
					return retval;
				}
				else {//mysql
					return (System.TimeSpan.Parse(myString));
				}
			}
			catch {
				return System.TimeSpan.Zero;
			}
		}

		///<summary>Used for Timespans that are guaranteed to always be a valid time of day.  No negatives or hours over 24.  Stored in Oracle as datetime.</summary>
		public static TimeSpan Time(string myString) {
			if(string.IsNullOrEmpty(myString)) {
				return System.TimeSpan.Zero;
			}
			try {
				if(DataConnection.DBtype==DatabaseType.Oracle) {
					return DateTime.Parse(myString).TimeOfDay;
				}
				else {//mysql
					return (System.TimeSpan.Parse(myString));
				}
			}
			catch {
				return System.TimeSpan.Zero;
			}
		}
		
	
		

		


	}

	


}










