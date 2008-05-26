using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace OpenDentBusiness {
	internal class Lan {
		///<summary>This is a stub for the language translator until it's moved out of the main application and into the business layer.</summary>
		internal static string g(string sender,string text){
			return text;
		}

		///<summary>This had to be added because SilverLight does not allow globally setting the current culture format.</summary>
		public static string GetShortDateTimeFormat(){
			if(CultureInfo.CurrentCulture.Name=="en-US"){
				//DateTimeFormatInfo formatinfo=(DateTimeFormatInfo)CultureInfo.CurrentCulture.DateTimeFormat.Clone();
				//formatinfo.ShortDatePattern="MM/dd/yyyy";
				//return formatinfo;
				return "MM/dd/yyyy";
			}
			else{
				return CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern;
			}
		}
	}
}
