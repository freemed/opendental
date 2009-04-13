using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Collections.ObjectModel;
using System.IO;
using System.Xml.Serialization;
using OpenDentBusiness;

namespace OpenDentBusiness.DataAccess {
	public static class FactoryServer<T>
		where T : DataObjectBase, new() {
		/// <summary>
		/// Executes the command, using the DataObjectFactory&lt;T&gt; class and serializes the result
		/// to the stream provided.
		/// </summary>
		/// <param name="stream">The stream the result will be serialized to.</param>
		/// <param name="command">The command to execute.</param>
		public static void ProcessCommand(Stream stream, FactoryTransferObject<T> command) {
			if(command == null){
				throw new ArgumentNullException("command");
			}
			if(stream == null){
				throw new ArgumentNullException("stream");
			}
			Type factoryType = typeof(DataObjectFactory<T>);
			Type returnType = GetReturnType(typeof(T), command.Command);
			MethodInfo method=factoryType.GetMethod(command.Command.ToString(),BindingFlags.Public | BindingFlags.Static,null,command.GetAllParameterTypes(),null);
			object value;
			switch(command.Command) {
				case enumDtoCommand.CreateObjects:
					value = method.Invoke(null, command.GetAllParameters());
					break;
				case enumDtoCommand.CreateObject:
					value = method.Invoke(null, command.GetAllParameters());
					break;
				case enumDtoCommand.WriteObject:
					method.Invoke(null, command.GetAllParameters());
					value = new DtoObjectInsertedAck(DataObjectInfo<T>.GetPrimaryKeys(command.DataObject));
					break;
				case enumDtoCommand.DeleteObject:
					method.Invoke(null, command.GetAllParameters());
					value = new DtoServerAck();
					break;
				default:
					throw new ArgumentOutOfRangeException("command");
			}
			XmlSerializer serializer = new XmlSerializer(returnType);
			serializer.Serialize(stream, value);
		}

		public static Type GetReturnType(Type type,enumDtoCommand command) {
			switch(command) {
				case enumDtoCommand.CreateObjects:
					return typeof(Collection<>).MakeGenericType(type);
				case enumDtoCommand.CreateObject:
					// This returns a T
					return type;
				case enumDtoCommand.WriteObject:
					return typeof(DtoObjectInsertedAck);
				case enumDtoCommand.DeleteObject:
					return typeof(DtoServerAck);
				default:
					throw new ArgumentOutOfRangeException("command");
			}
		}
	}
}
