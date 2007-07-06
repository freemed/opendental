using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using OpenDentBusiness;

namespace OpenDental.DataAccess {
	public class FactoryTransferObject<T> : DataTransferObject 
		where T: DataObjectBase, new() {

		private string command;

		/// <summary>
		/// The command to execute.
		/// </summary>
		public string Command {
			get { return command; }
			set { command = value; }
		}

		private T dataObject;
		/// <summary>
		/// The DataObject, if it is a required argument of the command. Else, null.
		/// </summary>
		public T DataObject {
			get { return dataObject; }
			set { dataObject = value; }
		}

		private object[] parameters;

		/// <summary>
		/// The parameters for the command, excluding the DataObject itself.
		/// </summary>
		public object[] Parameters {
			get { return parameters; }
			set { parameters = value; }
		}

		public object[] GetAllParameters() {
			int argumentCount = Parameters.Length;
			int argumentOffset = 0;
			if (DataObject != null) {
				argumentCount++;
				argumentOffset++;
			}

			object[] arguments = new object[argumentCount];
			if (DataObject != null) {
				arguments[0] = DataObject;
			}

			for (int i = 0; i < Parameters.Length; i++) {
				arguments[argumentOffset + i] = Parameters[i];
			}

			return arguments;
		}

		public Type[] GetAllParameterTypes() {
			object[] parameters = GetAllParameters();
			Type[] parameterTypes = new Type[parameters.Length];

			for (int i = 0; i < parameterTypes.Length; i++)
				parameterTypes[i] = parameters[i].GetType();

			return parameterTypes;
		}
	}
}
