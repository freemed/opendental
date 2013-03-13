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
		//Get the URL's protocol.
		ServerURI=Window.Location.getProtocol()+"//";
		//Get the URL's host and port name.
		ServerURI+=Window.Location.getHost();
		//Now that we have the full URL, we need to tell it to send requests to ServiceMain.asmx
		ServerURI+="/ServiceMain.asmx";
	}
	
}
