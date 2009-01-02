using System;
using System.Collections;
using System.Drawing;
//using System.Drawing.Imaging;
using System.IO;
using System.Text;
//using System.Windows.Forms;

namespace OpenDentMobile{
	
	public class PIn{
		///<summary></summary>
		public static bool PBool (string myString){
			return myString=="1";
		}

		///<summary></summary>
		public static byte PByte (string myString){
			if(myString==""){
				return 0;
			}
			else{
				return System.Convert.ToByte(myString);
			}
		}

		///<summary></summary>
		public static DateTime PDate(string myString){
			if(myString=="")
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
		public static DateTime PDateT(string myString){
			if(myString==""){
				return DateTime.MinValue;
			}
			//if(myString=="0000-00-00 00:00:00")//useless
			//	return DateTime.MinValue;
			try{
				return (DateTime.Parse(myString));
			}
			catch{
				return DateTime.MinValue;
			}
		}

		public static TimeSpan PTimeSpan(string myString) {
			if (string.IsNullOrEmpty(myString)) {
				return TimeSpan.MinValue;
			}
			try {
				return (TimeSpan.Parse(myString));
			}
			catch {
				return TimeSpan.MinValue;
			}
		}

		///<summary>If blank or invalid, returns 0. Otherwise, parses.</summary>
		public static double PDouble (string myString){
			if(myString==""){
				return 0;
			}
			else{
				try{
					return System.Convert.ToDouble(myString);
				}
				catch{
					//MessageBox.Show("Error converting "+myString+" to double");
					return 0;
				}
			}
		}

		///<summary></summary>
		public static int PInt (string myString){
			if(myString==""){
				return 0;
			}
			else{
				return System.Convert.ToInt32(myString);
			}
		}

		///<summary></summary>
		public static short PShort(string myString) {
			if(myString == "") {
				return 0;
			}
			else {
				return System.Convert.ToInt16(myString);
			}
		}

		///<summary></summary>
		public static float PFloat(string myString){
			if(myString==""){
				return 0;
			}
			//try{
				return System.Convert.ToSingle(myString);
			//}
			//catch{
			//	return 0;
			//}
		}

		///<summary>Currently does nothing.</summary>
		public static string PString (string myString){
			return myString;
		}



	}

	


}










