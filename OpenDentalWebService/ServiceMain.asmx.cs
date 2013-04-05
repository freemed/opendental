using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Xml;
using OpenDentBusiness;
using OpenDentalWebService.Remoting;

namespace OpenDentalWebService {
	///<summary>Summary description for ServiceMain</summary>
	[WebService(Namespace = "http://www.opendental.com/OpenDentalWebService/")]
	[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
	[ToolboxItem(false)]
	public class ServiceMain:System.Web.Services.WebService {
		private OpenDentBusiness.DataConnection conLocal=null;
		private OpenDentBusiness.DataConnection conHQ=null;

		[WebMethod]
		///<summary>Customers will be able to host their own web services.  In order to access their local db, they will simply leave out the id parameter in the URLs of their login page.</summary>
		public string ProcessLoginRequestLocal(string textUserName,string textPassword) {
			//The customer is hosting their own web service.  The database settings will be located in a config file locally.
			//The database connection will always be null the first time the web service is accessed.
			if(conLocal==null) {
				//Create a connection to the database.  The same database connector that is used for the web app can be used here because the service is local.
				conLocal=DbConnection.GetConnectionLocal();
			}
			//Check the credentials that were passed in and dictate whether a patient portal page should be returned or the Web Application.
			return "";
		}

		[WebMethod]
		///<summary>Only used at HQ.  Users will be able to launch our login page as log as the URL is preceeded with an id parameter.  When the id parameter is present, this method will be called on the web service at HQ.</summary>
		public string ProcessLoginRequestHQ(string custID,string textUserName,string textPassword) {
			//The database connection will always be null the first time the web service is accessed.
			if(conHQ==null) {
				//Create a connection to the database at HQ.
				conHQ=DbConnection.GetConnectionHQ();
			}
			//Check the credentials in our internal db at HQ and dictate whether a patient portal page should be returned or the Web Application home page.
			return "";
		}

		///<summary>Pass in a serialized dto.  It returns the desired object in xml or a dto exception which must be deserialized by the client.  We return an XmlDocument because returning a string will encode the XML thus the less than and greater than symbols get translated to &lt; and &gt;</summary>
		[WebMethod]
		[ScriptMethod(UseHttpGet=true)]
		public XmlDocument ProcessRequest(string dtoString) {
			//The database connection will always be null the first time the web service is accessed.
			if(conLocal==null) {
				//Create a connection to the database.  This connection will always be local, even if used at HQ because that would mean our local service would be hosting the data.
				conLocal=DbConnection.GetConnectionLocal();
			}
			DataTransferObject dto=null;
			XmlDocument xdoc=new XmlDocument();
			try {
				dto=DataTransferObject.Deserialize(dtoString);
			}
			catch(Exception e) {
				DtoException dtoEx=new DtoException();
				dtoEx.Type="DtoException";
				dtoEx.Message="Error deserializing the request:\r\n";
				if(e.InnerException==null) {
					dtoEx.Message+=e.Message;
				}
				else {
					dtoEx.Message+=e.InnerException.Message;
				}
				xdoc.LoadXml(dtoEx.Serialize());
				return xdoc;
			}
			try {
				switch(dto.Type) {
					#region DtoGetTable
					case "DtoGetTable":
						DtoGetTable dtoGetTable=(DtoGetTable)dto;
						//TODO: Check credentials.
						DataTable table=(DataTable)DtoMethods.ProcessDtoObject(dto);
						xdoc.LoadXml(aaGeneralTypes.Serialize("DataTable",table));
						break;
					#endregion
					#region DtoGetTableLow
					case "DtoGetTableLow":
						DtoGetTableLow dtoGetTableLow=(DtoGetTableLow)dto;
						//TODO: Check credentials.
						DataTable tableLow=(DataTable)Reports.GetTable((string)dto.Params[0].Obj);
						xdoc.LoadXml(aaGeneralTypes.Serialize("DataTable",tableLow));
						break;
					#endregion
					#region DtoGetDS
					//case "DtoGetDS":
					//  return "";
					#endregion
					#region DtoGetLong
					case "DtoGetLong":
						DtoGetLong dtoGetLong=(DtoGetLong)dto;
						//TODO: Check credentials.
						xdoc.LoadXml(aaGeneralTypes.Serialize("System.Int64",(long)DtoMethods.ProcessDtoObject(dto)));
						break;
					#endregion
					#region DtoGetInt
					case "DtoGetInt":
						DtoGetInt dtoGetInt=(DtoGetInt)dto;
						//TODO: Check credentials.
						xdoc.LoadXml(aaGeneralTypes.Serialize("System.Int32",(int)DtoMethods.ProcessDtoObject(dto)));
						break;
					#endregion
					#region DtoGetVoid
					case "DtoGetVoid":
						DtoGetVoid dtoGetVoid=(DtoGetVoid)dto;
						//TODO: Check credentials.
						DtoMethods.ProcessDtoObject(dto);
						xdoc.LoadXml("<ack />");
						break;
					#endregion
					#region DtoGetObject
					case "DtoGetObject":
						DtoGetObject dtoGetObject=(DtoGetObject)dto;
						//TODO: Check credentials.
						xdoc.LoadXml(DtoMethods.CallClassSerializer(dtoGetObject.ObjectType,DtoMethods.ProcessDtoObject(dto)));
						//throw new NotSupportedException("ProcessRequest, DtoGetObject not supported yet.");
						break;
					#endregion
					#region DtoGetString
					case "DtoGetString":
						DtoGetString dtoGetString=(DtoGetString)dto;
						//TODO: Check credentials.
						xdoc.LoadXml(aaGeneralTypes.Serialize("System.String",(string)DtoMethods.ProcessDtoObject(dto)));
						break;
					#endregion
					#region DtoGetBool
					case "DtoGetBool":
						DtoGetBool dtoGetBool=(DtoGetBool)dto;
						//TODO: Check credentials.
						xdoc.LoadXml(aaGeneralTypes.Serialize("System.Boolean",(string)DtoMethods.ProcessDtoObject(dto)));
						break;
					#endregion
					#region Default DtoUnknown
					default:
						throw new NotSupportedException("ProcessRequest, dto type not supported.");
					#endregion
				}
			}
			catch(Exception e) {
				DtoException dtoEx=new DtoException();
				dtoEx.Type="DtoException";
				dtoEx.Message="Error processing the request:\r\n";
				if(e.InnerException==null) {
					dtoEx.Message+=e.Message;
				}
				else {
					dtoEx.Message+=e.InnerException.Message;
				}
				xdoc.LoadXml(dtoEx.Serialize());
				return xdoc;
			}
			return xdoc;
		}
	}
}