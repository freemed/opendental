package com.opendental.odweb.client.remoting;

public class SerializeStringEscapes {
	
	/** Escapes common characters used in XML from the passed in String. */
	public static String EscapeForXml(String myString) {
		StringBuilder strBuild=new StringBuilder();
		int length=myString.length();
		for(int i=0;i<length;i++) {
			String character=myString.substring(i,i+1);
			if(character.equals("<")) { 
				strBuild.append("&lt;"); 
				continue;
			}
			else if(character.equals(">")) { 
				strBuild.append("&gt;"); 
				continue; 
			}
			else if(character.equals("\"")) { 
				strBuild.append("&quot;"); 
				continue; 
			}
			else if(character.equals("\'")) { 
				strBuild.append("&#039;"); 
				continue;
			}
			else if(character.equals("&")) { 
				strBuild.append("&amp;"); 
				continue; 
			}
			strBuild.append(character);
		}
    return strBuild.toString();
	}
	
	
}
