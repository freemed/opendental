using System;
using System.Collections.Generic;
using System.Text;
using OpenDental;
using System.IO;
using System.Xml.Serialization;
using OpenDentBusiness;

namespace OpenDentBusiness.DataAccess {
	public static class FactoryClient<T>
		where T : DataObjectBase, new() {
		public static object SendRequest(enumDtoCommand command,T dataObject,object[] args) {
			FactoryTransferObject<T> fto = new FactoryTransferObject<T>();
			fto.Command = command;
			fto.DataObject = dataObject;
			fto.Parameters = args;
			string result=RemotingClient.SendAndReceive(fto);//this might throw an exception if server unavailable
			//byte[] buffer = 
			object value;
			//using (MemoryStream memStream = new MemoryStream(buffer)) {
				Type returnType = FactoryServer<T>.GetReturnType(typeof(T), command);
				//XmlSerializer serializer = new XmlSerializer(returnType);
				value = DataTransferObject.Deserialize(result);
					//serializer.Deserialize(memStream);
			//}
			return value;
		}
	}
}
