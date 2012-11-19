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
using OpenDentBusiness;

namespace OpenDentalWebService {
	///<summary>Summary description for ServiceMain</summary>
	[WebService(Namespace = "http://www.opendental.com/OpenDentalWebService/")]
	[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
	[ToolboxItem(false)]
	public class ServiceMain:System.Web.Services.WebService {

		///<summary>Pass in a serialized dto.  It returns a dto which must be deserialized by the client.</summary>
		[WebMethod]
		[ScriptMethod(UseHttpGet=true)]
		public string ProcessRequest(string dtoString) {
			#region DEBUG
#if DEBUG
			//System.Threading.Thread.Sleep(100);//to test slowness issues with web service.
#endif
			#endregion
			DataTransferObject dto=null;
			try {
				dto=DataTransferObject.Deserialize(dtoString);
			}
			catch(Exception e) {
				DtoException dtoEx=new DtoException();
				dtoEx.Message="Error deserializing the request:\r\n";
				if(e.InnerException==null) {
					dtoEx.Message+=e.Message;
				}
				else {
					dtoEx.Message+=e.InnerException.Message;
				}
				return dtoEx.Serialize();
			}
			try {
				switch(dto.Type) {
					#region DtoGetTable
					case "DtoGetTable":
						DtoGetTable dtoGetTable=(DtoGetTable)dto;
						//TODO: Check credentials.
						DataTable table=(DataTable)DtoMethods.ProcessDtoObject(dto);
						return aaGeneralTypes.Serialize("DataT",table);
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
						return aaGeneralTypes.Serialize("System.Int64",(long)DtoMethods.ProcessDtoObject(dto));
					#endregion
					#region DtoGetInt
					case "DtoGetInt":
						DtoGetInt dtoGetInt=(DtoGetInt)dto;
						//TODO: Check credentials.
						return aaGeneralTypes.Serialize("System.Int32",(int)DtoMethods.ProcessDtoObject(dto));
					#endregion
					#region DtoGetVoid
					case "DtoGetVoid":
						DtoGetVoid dtoGetVoid=(DtoGetVoid)dto;
						//TODO: Check credentials.
						DtoMethods.ProcessDtoObject(dto);
						return "";
					#endregion
					#region DtoGetObject
					case "DtoGetObject":
						DtoGetObject dtoGetObject=(DtoGetObject)dto;
						//TODO: Check credentials.
						return DtoMethods.CallClassSerializer(dtoGetObject.ObjectType,DtoMethods.ProcessDtoObject(dto));
						throw new NotSupportedException("ProcessRequest, DtoGetObject ObjectType not supported yet.");
					#endregion
					#region DtoGetString
					case "DtoGetString":
						DtoGetString dtoGetString=(DtoGetString)dto;
						//TODO: Check credentials.
						return aaGeneralTypes.Serialize("System.String",(string)DtoMethods.ProcessDtoObject(dto));
					#endregion
					#region DtoGetBool
					case "DtoGetBool":
						DtoGetBool dtoGetBool=(DtoGetBool)dto;
						//TODO: Check credentials.
						return aaGeneralTypes.Serialize("System.Boolean",(string)DtoMethods.ProcessDtoObject(dto));
					#endregion
					#region Default DtoUnknown
					default:
						throw new NotSupportedException("ProcessRequest, dto type not supported.");
					#endregion
				}
			}
			catch(Exception e) {
				DtoException dtoEx=new DtoException();
				dtoEx.Message="Error processing the request:\r\n";
				if(e.InnerException==null) {
					dtoEx.Message+=e.Message;
				}
				else {
					dtoEx.Message+=e.InnerException.Message;
				}
				return dtoEx.Serialize();
			}
		}
	}
}