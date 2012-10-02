package com.opendental.odweb.client.remoting;

/** OpenDentBusiness and all the DA classes are designed to throw an exception if something goes wrong.  
 * If using OpenDentBusiness through the remote server, then the server catches the exception and passes it back to the main program using this DTO.  
 * The client then turns it back into an exception so that it behaves just as if OpenDentBusiness was getting called locally. */
public class DtoException extends DataTransferObject {
	public String Message;
}