using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Interop.QBFC10;

namespace OpenDental.Bridges {
	///<summary>Contains all logic for QuickBook connections and requests to the QB company file.</summary>
	public class QuickBooks {
		private static QBSessionManager sessionManager;
		private static IMsgSetRequest requestMsgSet;
		private static IMsgSetResponse responseMsgSet;
		private static bool sessionBegun;
		private static bool connectionOpen;

		///<summary></summary>
		public QuickBooks() {

		}
		
		///<summary>Creates a new QB connection and begins the session.  Session will be left open until CloseConnection is called.  Major and minor version refer to the implementation version of the paticular QB request you are trying to run.  The connection will fail if the version you pass in does not support the type of request you are trying to run.</summary>
		public static void OpenConnection(short majorVer,short minorVer,string companyPath) {
			sessionManager=new QBSessionManager();
			//Create the message set request object to hold our request.
			requestMsgSet=sessionManager.CreateMsgSetRequest("US",majorVer,minorVer);
			requestMsgSet.Attributes.OnError=ENRqOnError.roeContinue;
			//Connect to QuickBooks and begin a session
			sessionManager.OpenConnection("","Open Dental");
			connectionOpen=true;
			sessionManager.BeginSession(companyPath,ENOpenMode.omDontCare);
			sessionBegun=true;
		}

		///<summary>Runs the request that has been built.  QB connection must be open before calling this method.</summary>
		public static void DoRequests() {
			if(!connectionOpen) {
				return;
			}
			responseMsgSet=sessionManager.DoRequests(requestMsgSet);
		}

		///<summary>Ends the session and then closes the connection.</summary>
		public static void CloseConnection() {
			if(sessionBegun) {
				sessionManager.EndSession();
				sessionBegun=false;
			}
			if(connectionOpen) {
				sessionManager.CloseConnection();
				connectionOpen=false;
			}
		}
		
		///<summary>Simplest connection test to QB.  Users have to connect to their QB company file with OD and QB running at the same time the first time they connect.  This is just a simple tool to let them get this connection out of the way.  QB will prompt the user to set permissions / access rights for OD and then from there on QB does not need to be open in the background.</summary>
		public static string TestConnection(string companyPath) {
			try {
				OpenConnection(1,0,companyPath);
				//Send the empty request and get the response from QuickBooks.
				responseMsgSet=sessionManager.DoRequests(requestMsgSet);
				CloseConnection();
				return "Connection to QuickBooks was successful.";
			}
			catch(Exception e) {
				if(sessionBegun) {
					sessionManager.EndSession();
				}
				if(connectionOpen) {
					sessionManager.CloseConnection();
				}
				return "Error: "+e.Message;
			}
		}

		///<summary>Adds an account query to the request message.  A QB connection must be open before calling this method. Requires connection with version 8.0</summary>
		public static void QueryListOfAccounts() {
			if(!connectionOpen) {
				return;
			}
			//Build the account query add append it to the request message.
			IAccountQuery AccountQueryRq=requestMsgSet.AppendAccountQueryRq();
			//Filters
			AccountQueryRq.ORAccountListQuery.AccountListFilter.ActiveStatus.SetValue(ENActiveStatus.asActiveOnly);
		}

		///<summary>Returns list of all active accounts.  QueryListOfAccounts must have been part of your request.</summary>
		public static List<string> GetListOfAccounts() {
			List<string> accountList=new List<string>();
			if(responseMsgSet==null) {
				return accountList;
			}
			IResponseList responseList=responseMsgSet.ResponseList;
			if(responseList==null) {
				return accountList;
			}
			//Loop through the list to pick out the AccountQueryRs section.
			for(int i=0;i<responseList.Count;i++) {
				IResponse response = responseList.GetAt(i);
				//Check the status code of the response, 0=ok, >0 is warning.
				if(response.StatusCode >= 0) {
					//The request-specific response is in the details, make sure we have some.
					if(response.Detail!=null) {
						//Make sure the response is the type we're expecting.
						ENResponseType responseType=(ENResponseType)response.Type.GetValue();
						if(responseType==ENResponseType.rtAccountQueryRs) {
							//Upcast to more specific type here, this is safe because we checked with response.Type check above.
							IAccountRetList AccountRetList=(IAccountRetList)response.Detail;
							for(int j=0;j<AccountRetList.Count;j++) {
								IAccountRet AccountRet=AccountRetList.GetAt(j);
								if(AccountRet.FullName!=null) {
									accountList.Add(AccountRet.FullName.GetValue());
								}
							}
						}
					}
				}
			}
			return accountList;
		}

		#region Entities (payees)
		//We are not currently using entities
		/*
		///<summary>Adds an entity query to the request message.  A QB connection must be open before calling this method.</summary>
		public static void QueryEntities() {
			if(!connectionOpen) {
				return;
			}
			//Build the entity query add append it to the request message.
			IEntityQuery EntityQueryRq=requestMsgSet.AppendEntityQueryRq();
			//Filters
			EntityQueryRq.ORListQuery.ListFilter.ActiveStatus.SetValue(ENActiveStatus.asActiveOnly);
		}

		///<summary>Returns list of all active Customers, Employees, Vendors and "Other" entities.  QueryListOfAccounts must have been part of your request.</summary>
		public static List<string> GetListOfEntities() {
			List<string> entityList=new List<string>();
			if(responseMsgSet==null) {
				return entityList;
			}
			IResponseList responseList=responseMsgSet.ResponseList;
			if(responseList==null) {
				return entityList;
			}
			//Loop through the list to pick out the EntityQueryRs section.
			for(int i=0;i<responseList.Count;i++) {
				IResponse response=responseList.GetAt(i);
				//Check the status code of the response, 0=ok, >0 is warning.
				if(response.StatusCode >= 0) {
					//The request-specific response is in the details, make sure we have some.
					if(response.Detail!=null) {
						//Make sure the response is the type we're expecting.
						ENResponseType responseType=(ENResponseType)response.Type.GetValue();
						if(responseType==ENResponseType.rtEntityQueryRs) {
							//Upcast to more specific type here, this is safe because we checked with response.Type check above.
							IOREntityRetList EntityRetList=(IOREntityRetList)response.Detail;
							for(int j=0;j<EntityRetList.Count;j++) {
								IOREntityRet EntityRet=EntityRetList.GetAt(j);
								if(EntityRet.CustomerRet!=null && EntityRet.CustomerRet.FullName!=null) {
									entityList.Add(EntityRet.CustomerRet.FullName.GetValue());
								}
								if(EntityRet.EmployeeRet!=null && EntityRet.EmployeeRet.Name!=null) {
									entityList.Add(EntityRet.EmployeeRet.Name.GetValue());
								}
								if(EntityRet.VendorRet!=null && EntityRet.VendorRet.Name!=null) {
									entityList.Add(EntityRet.VendorRet.Name.GetValue());
								}
								if(EntityRet.OtherNameRet!=null && EntityRet.OtherNameRet.Name!=null) {
									entityList.Add(EntityRet.OtherNameRet.Name.GetValue());
								}
							}
						}
					}
				}
			}
			return entityList;
		}
		 */
		#endregion

	}
}


/* The template for most functions that makes a QB connection:
 ****************************************************************
  public static string TestConnection(string companyPath) {
			try {
				OpenConnection(companyPath);
				//Build method query request here:
				//Send the request and get the response from QuickBooks.
				responseMsgSet=sessionManager.DoRequests(requestMsgSet);
				CloseConnection();
				return "";
			}
			catch(Exception e) {
				if(sessionBegun) {
					sessionManager.EndSession();
				}
				if(connectionOpen) {
					sessionManager.CloseConnection();
				}
				return "Error: "+e.Message;
			}
		}
	}
 ****************************************************************
*/