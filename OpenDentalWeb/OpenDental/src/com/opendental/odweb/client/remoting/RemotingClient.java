package com.opendental.odweb.client.remoting;

import com.google.gwt.http.client.RequestBuilder;
import com.google.gwt.http.client.URL;

public class RemotingClient {
	// TODO Dynamically set the ServerURI.
	public static String ServerURI="http://localhost:52441/ServiceMain.asmx";
	
	/** Returns a basic GET request builder. */
	public static RequestBuilder GetRequestBuilder(String dtoString) {
		String url=ServerURI;
		url=URL.encode(url+"/ProcessRequest?dtoString="+dtoString);
		return new RequestBuilder(RequestBuilder.GET, url);
	}
	
	
}
