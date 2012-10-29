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
			DataTransferObject dto=DataTransferObject.Deserialize(dtoString);
			try {
				switch(dto.Type) {
					#region DtoGetTable
					case "DtoGetTable":
						return "";
					#endregion
					#region DtoGetTableLow
					case "DtoGetTableLow":
						return "";
					#endregion
					#region DtoGetDS
					case "DtoGetDS":
						return "";
					#endregion
					#region DtoGetLong
					case "DtoGetLong":
						return "";
					#endregion
					#region DtoGetInt
					case "DtoGetInt":
						DtoGetInt dtoGetInt=(DtoGetInt)dto;
						//Check the credentials here somehow?//Userods.CheckCredentials(dtoGetInt.Credentials);//will throw exception if fails.
						object result=DtoMethods.ProcessDtoObject(dtoGetInt);
						return ((int)result).ToString();
						//string className=dtoGetInt.MethodName.Split('.')[0];
						//string methodName=dtoGetInt.MethodName.Split('.')[1];
						//string assemb=Assembly.GetAssembly(typeof(Db)).FullName;//any OpenDentBusiness class will do.
						//Type classType=Type.GetType("OpenDentBusiness."+className+","+assemb);
						//DtoObject[] parameters=dtoGetInt.Params;
						//Type[] paramTypes=DtoObject.GenerateTypes(parameters,assemb);
						//MethodInfo methodInfo=classType.GetMethod(methodName,paramTypes);
						//if(methodInfo==null) {
						//  throw new ApplicationException("Method not found with "+parameters.Length.ToString()+" parameters: "+dtoGetInt.MethodName);
						//}
						//object[] paramObjs=DtoObject.GenerateObjects(parameters);
						//int intResult=(int)methodInfo.Invoke(null,paramObjs);
						//return intResult.ToString();
					#endregion
					#region DtoGetVoid
					case "DtoGetVoid":
						return "";
					#endregion
					#region DtoGetObject
					case "DtoGetObject":
						//DtoGetObject dtoGetObject=(DtoGetObject)dto;
						//string className=dtoGetObject.MethodName.Split('.')[0];
						//string methodName=dtoGetObject.MethodName.Split('.')[1];
						//if(className != "Security" || methodName != "LogInWeb") {//because credentials will be checked inside that method
						//  Userods.CheckCredentials(dtoGetObject.Credentials);//will throw exception if fails.
						//}
						//string assemb=Assembly.GetAssembly(typeof(Db)).FullName;//any OpenDentBusiness class will do.
						//Type classType=Type.GetType("OpenDentBusiness."+className+","+assemb);
						//DtoObject[] parameters=dtoGetObject.Params;
						//Type[] paramTypes=DtoObject.GenerateTypes(parameters,assemb);
						//MethodInfo methodInfo=classType.GetMethod(methodName,paramTypes);
						//if(methodInfo==null) {
						//  throw new ApplicationException("Method not found with "+parameters.Length.ToString()+" parameters: "+dtoGetObject.MethodName);
						//}
						//if(className=="Security" && methodName=="LogInWeb") {
						//  string mappedPath=Server.MapPath(".");
						//  parameters[2]=new DtoObject(mappedPath,typeof(string));//because we can't access this variable from within OpenDentBusiness.
						//  RemotingClient.RemotingRole=RemotingRole.ServerWeb;
						//}
						//object[] paramObjs=DtoObject.GenerateObjects(parameters);
						//Object objResult=methodInfo.Invoke(null,paramObjs);
						//Type returnType=methodInfo.ReturnType;
						//return XmlConverter.Serialize(returnType,objResult);
						return "";
					#endregion
					#region DtoGetString
					case "DtoGetString":
						return "";
					#endregion
					#region DtoGetBool
					case "DtoGetBool":
						return "";
					#endregion
					#region Default DtoUnknown
					default:
						throw new NotSupportedException("Dto type not supported.");
					#endregion
				}
			}
			catch(Exception e) {
				DtoException exception=new DtoException();
				if(e.InnerException==null) {
					exception.Message=e.Message;
				}
				else {
					exception.Message=e.InnerException.Message;
				}
				return exception.Serialize();
			}
		}
	}
}