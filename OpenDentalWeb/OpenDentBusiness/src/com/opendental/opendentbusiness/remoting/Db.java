package com.opendental.opendentbusiness.remoting;

import com.google.gwt.http.client.Request;
import com.google.gwt.http.client.RequestBuilder;
import com.google.gwt.http.client.RequestCallback;
import com.google.gwt.http.client.RequestException;
import com.google.gwt.http.client.Response;
import com.google.gwt.user.client.ui.Image;
import com.google.gwt.user.client.ui.PopupPanel;
import com.opendental.opendentbusiness.remoting.Serializing.DeserializeCallbackResult;

/** This class is used to initiate communication to the server.  It's more like a helper class because communicating with the server gets ugly, especially since we do it so often. */
public class Db {
	/** This is the callback that every call to the database should listen to.  Even void requests should instantiate this callback in case an error occurs. */
	private static RequestCallbackResult RequestCallback;
	/** This is a callback that will call the RequestCallback.  Sounds redundant but it is necessary because deserializing might take a while and so we need to use a RepeatingCommand.
	 *  We have to use a repeating command within the Serialize class due to computation that has the possibility to tie up the main thread for more than 10 seconds.  */
	private static DeserializeCallbackResult DeserializeCallback;
	/** Simple popup that will have a loading bar so the user knows the program hasn't frozen on them. */
	private static PopupPanel Popup;
	
	/** Simple popup panel showing a loading bar.  The loading spinner was too jittery when deserializing so I went with the bar. */
	private static void showLoading() {
		Popup=new PopupPanel();
		Popup.add(new Image("images/loadingSpinner.gif"));
		Popup.setGlassEnabled(true);
		Popup.center();
		Popup.show();
	}
	
	private static void hideLoading() {
		if(Popup!=null) {
			Popup.hide();
			Popup=null;
		}
	}
	
	/** Every call to the database calls this method.  It will pack up the passed in dto request into a RequestBuilder and send it off to the server.
	 *  If the response from the server is successful, it will call the onSuccess method in the RequestCallbackResult. 
	 *  @param dtoRequest Pass in any serialized DtoGet request.
	 *  @param requestCallback The callback that will have it's onSucces() called once the server returns a result. */
	public static void sendRequest(String dtoRequest,RequestCallbackResult requestCallback) {
		RequestCallback=requestCallback;
		DeserializeCallback=new Deserialize_Callback();
		RequestBuilder builder=RemotingClient.getRequestBuilder(dtoRequest);
		try {//Try catch is required around http request.
			builder.sendRequest(null, new GetRequest());
		}
		catch (RequestException e) {
			//MsgBox.show("Send request error: "+e.getMessage());
		}
	}
	
	private static class GetRequest implements RequestCallback {		
		public void onResponseReceived(Request request, Response response) {
			if(response.getStatusCode()==200) {
				try {
					showLoading();
					Serializing.getDeserializedObject(response.getText(),DeserializeCallback);
				}
				catch (Exception e) {
					hideLoading();
					//MsgBox.show(e.getMessage());//This will be a more specific error.
				}
			}
			else {
				hideLoading();
				//MsgBox.show("Error status text: "+response.getStatusText()
      	//		+"\r\nError status code: "+Integer.toString(response.getStatusCode())
      	//		+"\r\nError text: "+response.getText());
			}
		}

		public void onError(Request request, Throwable exception) {
			//MsgBox.show(exception.getMessage());
		}
	}
	
	/** Hides the loading bar and calls the request callback's onSuccess method. */
	private static class Deserialize_Callback implements DeserializeCallbackResult {
		public void onComplete(Object obj) {
			hideLoading();
			RequestCallback.onSuccess(obj);
		}		
	}
	
	/** Every request sent to the database will result in a call being made to this callback.
	 *  This is the callback that every window should listen to when making a call to the database for their result.
	 *  onSuccess() will be called with the object that the calling method should return.  Null can be returned. */
	public interface RequestCallbackResult {
		void onSuccess(Object obj);
	}
	
}
