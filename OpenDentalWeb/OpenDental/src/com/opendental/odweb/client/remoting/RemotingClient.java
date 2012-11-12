package com.opendental.odweb.client.remoting;

import com.google.gwt.http.client.RequestBuilder;
import com.google.gwt.http.client.URL;

//import com.google.gwt.http.client.Request;
//import com.google.gwt.http.client.RequestBuilder;
//import com.google.gwt.http.client.RequestCallback;
//import com.google.gwt.http.client.RequestException;
//import com.google.gwt.http.client.Response;
//import com.google.gwt.http.client.URL;
//import com.opendental.odweb.client.request.HttpRequestTest;
//import com.opendental.odweb.client.ui.MsgBox;

public class RemotingClient {
	// TODO Dynamically set the ServerURI.
	public static String ServerURI="http://localhost:52441/ServiceMain.asmx";
	
	/** Returns a basic GET request builder. */
	public static RequestBuilder GetRequestBuilder(String dtoString) {
		String url=ServerURI;
		url=URL.encode(url+"/ProcessRequest?dtoString="+dtoString);
		return new RequestBuilder(RequestBuilder.GET, url);
	}
	
//	/**  */
//	public static Object ProcessGetTable(DtoGetObject dto) throws Exception {
//		try {
//			return null;
//		}
//		catch(Exception ex) {
//			DtoException exception=(DtoException)DataTransferObject.Deserialize(result);
//			throw new Exception(exception.Message);
//		}
//	}

//	/**  */
//	public static int ProcessGetInt(DtoGetInt dto) throws Exception {
//		String result="";
//		try {
//			result=SendAndReceive(dto);
//			return Integer.getInteger(result);
//		}
//		catch(Exception ex) {
//			DtoException exception=(DtoException)DataTransferObject.Deserialize(result);
//			throw new Exception(exception.Message);
//		}
//	}
	
//	/**  */
//	public static void ProcessGetVoid(DtoGetVoid dto) throws Exception {
//		String result="";
//		try {
//			result=SendAndReceive(dto);
//		}
//		catch(Exception ex) {
//			DtoException exception=(DtoException)DataTransferObject.Deserialize(result);
//			throw new Exception(exception.Message);
//		}
//	}
	
//	/**  */
//	public static Object ProcessGetObject(DtoGetObject dto) throws Exception {
//		String result="";
//		try {
//			result=SendAndReceive(dto);
//			return result;
//		}
//		catch(Exception ex) {
//			DtoException exception=(DtoException)DataTransferObject.Deserialize(result);
//			throw new Exception(exception.Message);
//		}
//	}
	
//	/**  */
//	static String SendAndReceive(DataTransferObject dto){
//		String dtoString=dto.Serialize();
//		//Make a connection to the server here?  Probably not due to asynchronous calls. 
//		String url=HttpRequestTest.WEBSERVICE_URL;
//		url=URL.encode(url+"/ProcessRequest?dtoString="+dtoString);
//		RequestBuilder builder=new RequestBuilder(RequestBuilder.GET, url);
//		try {//Try catch is required around http request.
//			builder.sendRequest(null, new RequestCallback(){
//				public void onResponseReceived(Request request,Response response) {	
//					if (200 == response.getStatusCode()) {
//						//What to do with the result text?
//						MsgBox.Show(response.getText());
//					}	else {
//						MsgBox.Show("Error status text: "+ response.getStatusText()
//							+"\r\nError status code:"+Integer.toString(response.getStatusCode())
//							+"\r\nError text: "+response.getText());
//					}
//				}
//				public void onError(Request request, Throwable exception) {		
//					MsgBox.Show("Error: "+exception.getMessage());
//				}
//			});
//		}
//		catch (RequestException e) {
//			e.printStackTrace();
//		}
//		return "";
//	}
	
	
}
