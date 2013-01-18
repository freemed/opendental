package com.opendental.odweb.client.remoting;

import com.google.gwt.http.client.Request;
import com.google.gwt.http.client.RequestBuilder;
import com.google.gwt.http.client.RequestCallback;
import com.google.gwt.http.client.RequestException;
import com.google.gwt.http.client.Response;

/** This class is used to initiate communication to the server.  It's more like a helper class because communicating with the server gets ugly especially since we do it so often. */
public class Db {
	/** This is the callback that every call to the database should listen to.  Even void requests should instantiate this callback in case an error occurs. */
	private static RequestCallbackResult RequestCallback;
	
	/** Every call to the database calls this method.  It will pack up the passed in dto request into a RequestBuilder and send it off to the server.
	 *  If the response from the server is successful, it will call the onSuccess method in the RequestCallback. 
	 *  @param dtoRequest Pass in any serialized DtoGet request. */
	public static void sendRequest(String dtoRequest,RequestCallbackResult requestCallback) {
		RequestCallback=requestCallback;
		RequestBuilder builder=RemotingClient.getRequestBuilder(dtoRequest);
		try {//Try catch is required around http request.
			builder.sendRequest(null, new GetRequest());
		}
		catch (RequestException e) {
			RequestCallback.onError("Error: "+e.getMessage());
		}
	}
	
	private static class GetRequest implements RequestCallback {		
		public void onResponseReceived(Request request, Response response) {
			if(response.getStatusCode()==200) {
				try {
					// TODO Make sure this works with DtoGetVoid.
					Object obj=Serializing.getDeserializedObject(response.getText());
					RequestCallback.onSuccess(obj);
				}
				catch (Exception e) {
					RequestCallback.onError(e.getMessage());//This will be a more specific error.
				}
      }
			else {
				RequestCallback.onError("Error status text: "+response.getStatusText()
      			+"\r\nError status code: "+Integer.toString(response.getStatusCode())
      			+"\r\nError text: "+response.getText());
      }
		}

		@Override
		public void onError(Request request, Throwable exception) {
			RequestCallback.onError(exception.getMessage());
		}
	}
	
	/** Every request sent to the database will result in a call being made to this callback.  This is the callback that every window should listen to when making a call to the database for their result. */
	public interface RequestCallbackResult {
		void onSuccess(Object obj);
		void onError(String error);
	}
	
}
