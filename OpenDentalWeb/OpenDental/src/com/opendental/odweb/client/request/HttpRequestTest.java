package com.opendental.odweb.client.request;

import com.google.gwt.http.client.Request;
import com.google.gwt.http.client.RequestBuilder;
import com.google.gwt.http.client.RequestCallback;
import com.google.gwt.http.client.RequestException;
import com.google.gwt.http.client.Response;
import com.google.gwt.http.client.URL;
import com.opendental.odweb.client.ui.MsgBox;

// TODO Delete the entire request package before releasing.

public class HttpRequestTest {
	
	public static final String WEBSERVICE_URL="http://localhost:52441/ServiceMain.asmx";
	public static String Result;
	
	public static String GetHelloWorld() {
		String url=WEBSERVICE_URL;//+"/HelloWorld";
		url=URL.encode(url);
		return SendCommand(url);
	}
	
	public static String SendCommand(String url) {
		//Send request to server and catch any errors.
	    RequestBuilder builder = new RequestBuilder(RequestBuilder.GET, url);
	    try {
	    	builder.sendRequest(null, new RequestCallbackString());
	    } catch (RequestException e) {
	    	return e.getMessage();
	    }
	    return Result;
	}
	
	private static class RequestCallbackString implements RequestCallback {
		
		public void onResponseReceived(Request request, Response response) {
			if (200 == response.getStatusCode()) {
	            Result=response.getText();
	        } else {
	        	MsgBox msg=new MsgBox("Error status text: "+ response.getStatusText()
	        			+"\r\nError status code:"+Integer.toString(response.getStatusCode())
	        			+"\r\nError text: "+response.getText());
		        msg.show();
	        }
		}
		
		public void onError(Request request, Throwable exception) {
			MsgBox msg=new MsgBox("Error: "+exception.getMessage());
	        msg.show();
		}
		
	}
	
	
}
