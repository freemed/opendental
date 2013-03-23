package com.opendental.opendentbusiness.remoting;

import com.google.gwt.http.client.Request;
import com.google.gwt.http.client.RequestBuilder;
import com.google.gwt.http.client.RequestCallback;
import com.google.gwt.http.client.RequestException;
import com.google.gwt.http.client.Response;
import com.opendental.opendentbusiness.remoting.RemotingClient;
import com.opendental.opendentbusiness.remoting.Serializing;
import com.opendental.opendentbusiness.remoting.Serializing.DeserializeCallbackResult;

/** This class is used to initiate communication to the server.  It's more like a helper class because communicating with the server gets ugly, especially since we do it so often. */
public class RequestHelper {
	/** This is the callback that every call to the database should listen to.  Even void requests should instantiate this callback in case an error occurs. */
	private static RequestCallbackResult RequestCallback;
	/** This is a callback that will call the RequestCallback.  Sounds redundant but it is necessary because deserializing might take a while and so we need to use a RepeatingCommand.
	 *  We have to use a repeating command within the Serialize class due to computation that has the possibility to tie up the main thread for more than 10 seconds.  */
	private static DeserializeCallbackResult DeserializeCallback;
	
	/** Every call to the database calls this method.  It will pack up the passed in dto request into a RequestBuilder and send it off to the server.
	 *  If the response from the server is successful, it will call the onSuccess method in the RequestCallbackResult. 
	 *  @param dtoRequest Pass in any serialized DtoGet request.
	 *  @param requestCallback The callback that will have it's onSucces() called once the server returns a result. 
	 * @throws RequestException sendRequest can throw exceptions. */
	public static void sendRequest(String dtoRequest,RequestCallbackResult requestCallback) throws RequestException {
		RequestCallback=requestCallback;
		DeserializeCallback=new Deserialize_Callback();
		RequestBuilder builder=RemotingClient.getRequestBuilder(dtoRequest);
		builder.sendRequest(null, new GetRequest());
	}
	
	private static class GetRequest implements RequestCallback {		
		public void onResponseReceived(Request request, Response response) {
			if(response.getStatusCode()==200) {
				try {
					Serializing.getDeserializedObject(response.getText(),DeserializeCallback);
				}
				catch (Exception e) {
					DeserializeCallback.onError(e.getMessage());
				}
			}
			else {
				DeserializeCallback.onError("Error status text: "+response.getStatusText()
      			+"\r\nError status code: "+Integer.toString(response.getStatusCode())
      			+"\r\nError text: "+response.getText());
			}
		}

		public void onError(Request request, Throwable exception) {
			DeserializeCallback.onError(exception.getMessage());
		}
	}
	
	/** Hides the loading bar and calls the request callback's onSuccess method. */
	private static class Deserialize_Callback implements DeserializeCallbackResult {
		public void onComplete(Object obj) {
			RequestCallback.onSuccess(obj);
		}

		public void onError(String error) {
			RequestCallback.onError(error);
		}		
	}
	
	/** Every request sent to the database will result in a call being made to this callback.
	 *  This is the callback that every window should listen to when making a call to the database for their result.
	 *  onSuccess() will be called with the object that the calling method should return.  Null can be returned. */
	public interface RequestCallbackResult {
		void onSuccess(Object obj);
		void onError(String error);
	}
	
}
