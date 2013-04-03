package com.opendental.opendentbusiness.remoting;

import com.google.gwt.http.client.RequestBuilder;
import com.google.gwt.http.client.URL;
import com.google.gwt.user.client.Window;

public class RemotingClient {
	private static String ServerURI;
	
	/** Returns a basic GET request builder. */
	public static RequestBuilder getRequestBuilder(String dtoString) {
		String url=getServerURI();
		url=URL.encode(url+"/ProcessRequest?dtoString="+dtoString);
		return new RequestBuilder(RequestBuilder.GET, url);
	}
	
	public static String getServerURI() {
		if(ServerURI==null) {
			setServerURI();
		}
		return ServerURI;
	}

	/** Dynamically sets the ServerURI with the path that the web service is running on by using the host URL and port. */
	private static void setServerURI() {
		//Get the URL's protocol.  https:
		ServerURI=Window.Location.getProtocol()+"//";
		//Get the URL's host and port name.  opendental.com:123456
		ServerURI+=Window.Location.getHost();
		//Get the current Path.  /OpenDentalWebService/GWT/OpenDental/OpenDentalWeb.html
		String path=Window.Location.getPath();
		//The base directory where ServiceMain.asmx should live is where the GWT folder is located.  Strip everything off of the path from the GWT folder forward.
		int gwtIndex=path.indexOf("GWT");
		ServerURI+=path.substring(0, gwtIndex);
		//Now that we have the base URL, we need to tell it to send requests to ServiceMain.asmx
		ServerURI+="ServiceMain.asmx";
	}
	
}
