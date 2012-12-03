package com.opendental.odweb.client.remoting;

/** The username and password are internal to OD.  They are not the MySQL username and password. 
 * We might use a different method of verifying credentials and permissions... this will probably change.*/
public class Credentials {
	/**  */
	public String Username;
	/**  */
	public String Password;
	
	/** Constructor that will set the Username and Password to the passed in values */
	public Credentials(String username,String password) {
		Username=username;
		Password=password;
	}
	
}
