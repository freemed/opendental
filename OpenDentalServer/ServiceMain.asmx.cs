using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml;
using System.Xml.Serialization;
using OpenDentBusiness;
using OpenDentBusiness.DataAccess;

namespace OpenDentalServer {
	/// <summary></summary>
	[WebService(Namespace="http://www.open-dent.com/OpenDentalServer")]
	[WebServiceBinding(ConformsTo=WsiProfiles.BasicProfile1_1)]
	[ToolboxItem(false)]
	public class ServiceMain:System.Web.Services.WebService {

		/// <summary>Pass in a serialized dto.  It returns a dto which must be deserialized by the client.</summary>
		[WebMethod]
		public string ProcessRequest(string dtoString) {
			DataTransferObject dto=DataTransferObject.Deserialize(dtoString);
			//XmlSerializer serializer;
			try {
				Type type = dto.GetType();
				if(type == typeof(DtoGetTable)) {
					DtoGetTable dtoGetTable=(DtoGetTable)dto;
					Userods.CheckCredentials(dtoGetTable.Credentials);//will throw exception if fails.
					string className=dtoGetTable.MethodName.Split('.')[0];
					string methodName=dtoGetTable.MethodName.Split('.')[1];
					string assemb=Assembly.GetAssembly(typeof(Db)).FullName;//any OpenDentBusiness class will do.
					Type classType=Type.GetType("OpenDentBusiness."+className+","+assemb);
					DtoObject[] parameters=dtoGetTable.Params;
					Type[] paramTypes=DtoObject.GenerateTypes(parameters,assemb);
					MethodInfo methodInfo=classType.GetMethod(methodName,paramTypes);
					object[] paramObjs=DtoObject.GenerateObjects(parameters);
					DataTable dt=(DataTable)methodInfo.Invoke(null,paramObjs);
					String response=XmlConverter.TableToXml(dt);
					return response;
				}
				else if(type == typeof(DtoGetTableLow)) {
					DtoGetTableLow dtoGetTableLow=(DtoGetTableLow)dto;
					Userods.CheckCredentials(dtoGetTableLow.Credentials);//will throw exception if fails.
					DtoObject[] parameters=dtoGetTableLow.Params;
					object[] paramObjs=DtoObject.GenerateObjects(parameters);
					DataTable dt=Reports.GetTable((string)paramObjs[0]);
					String response=XmlConverter.TableToXml(dt);
					return response;
				}
				else if(type == typeof(DtoGetDS)) {
					DtoGetDS dtoGetDS=(DtoGetDS)dto;
					Userods.CheckCredentials(dtoGetDS.Credentials);//will throw exception if fails.
					string className=dtoGetDS.MethodName.Split('.')[0];
					string methodName=dtoGetDS.MethodName.Split('.')[1];
					string assemb=Assembly.GetAssembly(typeof(Db)).FullName;//any OpenDentBusiness class will do.
					Type classType=Type.GetType("OpenDentBusiness."+className+","+assemb);
					DtoObject[] parameters=dtoGetDS.Params;
					Type[] paramTypes=DtoObject.GenerateTypes(parameters,assemb);
					MethodInfo methodInfo=classType.GetMethod(methodName,paramTypes);
					object[] paramObjs=DtoObject.GenerateObjects(parameters);
					DataSet ds=(DataSet)methodInfo.Invoke(null,paramObjs);
					String response=XmlConverter.DsToXml(ds);
					return response;
				}
				else if(type == typeof(DtoGetLong)) {
					DtoGetLong dtoGetLong=(DtoGetLong)dto;
					Userods.CheckCredentials(dtoGetLong.Credentials);//will throw exception if fails.
					string className=dtoGetLong.MethodName.Split('.')[0];
					string methodName=dtoGetLong.MethodName.Split('.')[1];
					string assemb=Assembly.GetAssembly(typeof(Db)).FullName;//any OpenDentBusiness class will do.
					Type classType=Type.GetType("OpenDentBusiness."+className+","+assemb);
					DtoObject[] parameters=dtoGetLong.Params;
					Type[] paramTypes=DtoObject.GenerateTypes(parameters,assemb);
					MethodInfo methodInfo=classType.GetMethod(methodName,paramTypes);
					object[] paramObjs=DtoObject.GenerateObjects(parameters);
					long longResult=(long)methodInfo.Invoke(null,paramObjs);
					return longResult.ToString();
				}
				else if(type == typeof(DtoGetInt)) {
					DtoGetInt dtoGetInt=(DtoGetInt)dto;
					Userods.CheckCredentials(dtoGetInt.Credentials);//will throw exception if fails.
					string className=dtoGetInt.MethodName.Split('.')[0];
					string methodName=dtoGetInt.MethodName.Split('.')[1];
					string assemb=Assembly.GetAssembly(typeof(Db)).FullName;//any OpenDentBusiness class will do.
					Type classType=Type.GetType("OpenDentBusiness."+className+","+assemb);
					DtoObject[] parameters=dtoGetInt.Params;
					Type[] paramTypes=DtoObject.GenerateTypes(parameters,assemb);
					MethodInfo methodInfo=classType.GetMethod(methodName,paramTypes);
					object[] paramObjs=DtoObject.GenerateObjects(parameters);
					int intResult=(int)methodInfo.Invoke(null,paramObjs);
					return intResult.ToString();
				}
				else if(type == typeof(DtoGetVoid)) {
					DtoGetVoid dtoGetVoid=(DtoGetVoid)dto;
					Userods.CheckCredentials(dtoGetVoid.Credentials);//will throw exception if fails.
					string className=dtoGetVoid.MethodName.Split('.')[0];
					string methodName=dtoGetVoid.MethodName.Split('.')[1];
					string assemb=Assembly.GetAssembly(typeof(Db)).FullName;//any OpenDentBusiness class will do.
					Type classType=Type.GetType("OpenDentBusiness."+className+","+assemb);
					DtoObject[] parameters=dtoGetVoid.Params;
					Type[] paramTypes=DtoObject.GenerateTypes(parameters,assemb);
					MethodInfo methodInfo=classType.GetMethod(methodName,paramTypes);
					object[] paramObjs=DtoObject.GenerateObjects(parameters);
					methodInfo.Invoke(null,paramObjs);
					return "0";
				}
				else if(type == typeof(DtoGetObject)) {
					DtoGetObject dtoGetObject=(DtoGetObject)dto;
					string className=dtoGetObject.MethodName.Split('.')[0];
					string methodName=dtoGetObject.MethodName.Split('.')[1];
					if(className != "Security" || methodName != "LogInWeb") {//because credentials will be checked inside that method
						Userods.CheckCredentials(dtoGetObject.Credentials);//will throw exception if fails.
					}
					string assemb=Assembly.GetAssembly(typeof(Db)).FullName;//any OpenDentBusiness class will do.
					Type classType=Type.GetType("OpenDentBusiness."+className+","+assemb);
					DtoObject[] parameters=dtoGetObject.Params;
					Type[] paramTypes=DtoObject.GenerateTypes(parameters,assemb);
					MethodInfo methodInfo=classType.GetMethod(methodName,paramTypes);
					if(className=="Security" && methodName=="LogInWeb") {
						string mappedPath=Server.MapPath(".");
						parameters[2]=new DtoObject(mappedPath);//because we can't access this variable from within OpenDentBusiness.
						RemotingClient.RemotingRole=RemotingRole.ServerWeb;
					}
					object[] paramObjs=DtoObject.GenerateObjects(parameters);
					Object objResult=methodInfo.Invoke(null,paramObjs);
					Type returnType=methodInfo.ReturnType;
					return XmlConverter.Serialize(returnType,objResult);
				}
				else if(type == typeof(DtoGetString)) {
					DtoGetString dtoGetString=(DtoGetString)dto;
					Userods.CheckCredentials(dtoGetString.Credentials);//will throw exception if fails.
					string className=dtoGetString.MethodName.Split('.')[0];
					string methodName=dtoGetString.MethodName.Split('.')[1];
					string assemb=Assembly.GetAssembly(typeof(Db)).FullName;//any OpenDentBusiness class will do.
					Type classType=Type.GetType("OpenDentBusiness."+className+","+assemb);
					DtoObject[] parameters=dtoGetString.Params;
					Type[] paramTypes=DtoObject.GenerateTypes(parameters,assemb);
					MethodInfo methodInfo=classType.GetMethod(methodName,paramTypes);
					object[] paramObjs=DtoObject.GenerateObjects(parameters);
					string strResult=(string)methodInfo.Invoke(null,paramObjs);
					return strResult;
				}
				else if(type == typeof(DtoGetBool)) {
					DtoGetBool dtoGetBool=(DtoGetBool)dto;
					Userods.CheckCredentials(dtoGetBool.Credentials);//will throw exception if fails.
					string className=dtoGetBool.MethodName.Split('.')[0];
					string methodName=dtoGetBool.MethodName.Split('.')[1];
					string assemb=Assembly.GetAssembly(typeof(Db)).FullName;//any OpenDentBusiness class will do.
					Type classType=Type.GetType("OpenDentBusiness."+className+","+assemb);
					DtoObject[] parameters=dtoGetBool.Params;
					Type[] paramTypes=DtoObject.GenerateTypes(parameters,assemb);
					MethodInfo methodInfo=classType.GetMethod(methodName,paramTypes);
					object[] paramObjs=DtoObject.GenerateObjects(parameters);
					bool boolResult=(bool)methodInfo.Invoke(null,paramObjs);
					return boolResult.ToString();
				}
				else {
					throw new NotSupportedException("Dto type not supported: "+type.FullName);
				}
			}
			catch(Exception e) {
				DtoException exception = new DtoException();
				if(e.InnerException==null) {
					exception.Message = e.Message;
				}
				else {
					exception.Message = e.InnerException.Message;
				}
				return exception.Serialize();
			}
		}

		

	}
}
