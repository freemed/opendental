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

namespace OpenDentalWebService {
	///<summary>Summary description for ServiceMain</summary>
	[WebService(Namespace = "http://www.opendental.com/OpenDentalWebService/")]
	[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
	[ToolboxItem(false)]
	public class ServiceMain:System.Web.Services.WebService {
		private OpenDentBusiness.DataConnection con=null;

		///<summary>Pass in a serialized dto.  It returns the desired object in xml or a dto exception which must be deserialized by the client.  We return an XmlDocument because returning a string will encode the XML thus the less than and greater than symbols get translated to &lt; and &gt;</summary>
		[WebMethod]
		[ScriptMethod(UseHttpGet=true)]
		public XmlDocument ProcessRequest(string dtoString) {
			#region DEBUG
			#if DEBUG
			//TODO Remove from DEBUG and enhance to use an xml config file?
			if(con==null) {
				//Create database connection for testing purposes:
				con=new OpenDentBusiness.DataConnection();
				con.SetDb("localhost","development131","root","","","",DatabaseType.MySql);
			}
			#endif
			#endregion
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
					//case "DtoGetTableLow":
					//  return "";
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
						return null;
					#endregion
					#region DtoGetObject
					case "DtoGetObject":
						DtoGetObject dtoGetObject=(DtoGetObject)dto;
						//TODO: Check credentials.
						xdoc.LoadXml(DtoMethods.CallClassSerializer(dtoGetObject.ObjectType,DtoMethods.ProcessDtoObject(dto)));
						throw new NotSupportedException("ProcessRequest, DtoGetObject not supported yet.");
						//break;
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