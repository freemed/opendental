using System;
using System.Text;

namespace OpenDentalWebService {
	public class SerializeStringEscapes {

		///<summary>Escapes common characters used in XML from the passed in String.</summary>
		public static string EscapeForXml(string myString) {
			StringBuilder strBuild=new StringBuilder();
			int length=myString.Length;
			for(int i=0;i<length;i++) {
				String character=myString.Substring(i,1);
				if(character.Equals("<")) {
					strBuild.Append("&lt;");
					continue;
				}
				else if(character.Equals(">")) {
					strBuild.Append("&gt;");
					continue;
				}
				else if(character.Equals("\"")) {
					strBuild.Append("&quot;");
					continue;
				}
				else if(character.Equals("\'")) {
					strBuild.Append("&#039;");
					continue;
				}
				else if(character.Equals("&")) {
					strBuild.Append("&amp;");
					continue;
				}
				strBuild.Append(character);
			}
			return strBuild.ToString();
		}


	}
}