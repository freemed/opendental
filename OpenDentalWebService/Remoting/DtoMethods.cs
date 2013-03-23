using System;
using System.Collections.Generic;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class DtoMethods {
		///<summary>Processes any type of data transfer object by calling the desired method.</summary>
		public static object ProcessDtoObject(DataTransferObject dto) {
			string classAndMethod=dto.MethodName;
			List<object> parameters=new List<object>();
			for(int i=0;i<dto.Params.Count;i++) {
				parameters.Add(dto.Params[i].Obj);
			}
			return CallMethod(classAndMethod,parameters);
		}

		///<summary>Calls the class serializer for any supported object, primitive or not.  objectType must be fully qualified unless it's a primitive, then either way is fine.  Ex: Sytem.In32 or int or OpenDentBusiness.Account or List&lt;OpenDentBusiness.Account&gt;.  Throws exceptions if the object or class is not supported yet.</summary>
		public static string CallClassSerializer(string objectType,Object obj) {
			#region Primitive and General Types
			//To add more primitive/general types go to method xCrudGeneratorWebService.Form1.GetPrimGenSerializerTypes and manually add it there.
			switch(objectType) {
				case "int":
				case "System.Int32":
				case "long":
				case "System.Int64":
				case "bool":
				case "System.Boolean":
				case "string":
				case "System.String":
				case "char":
				case "System.Char":
				case "Single":
				case "System.Single":
				case "byte":
				case "System.Byte":
				case "double":
				case "System.Double":
				case "DataTable":
					return aaGeneralTypes.Serialize(objectType,obj);
			}
			if(objectType.StartsWith("List")) {//Lists.
				return aaGeneralTypes.SerializeList(objectType,obj);
			}
			if(objectType.Contains("[")) {//Arrays.
				return aaGeneralTypes.SerializeArray(objectType,obj);
			}
			#endregion
			#region Open Dental Classes
			if(objectType=="OpenDentBusiness.Allergy") {
				return Allergy.Serialize((OpenDentBusiness.Allergy)obj);
			}
			if(objectType=="OpenDentBusiness.Appointment") {
				return Appointment.Serialize((OpenDentBusiness.Appointment)obj);
			}
			if(objectType=="OpenDentBusiness.Disease") {
				return Disease.Serialize((OpenDentBusiness.Disease)obj);
			}
			if(objectType=="OpenDentBusiness.LabPanel") {
				return LabPanel.Serialize((OpenDentBusiness.LabPanel)obj);
			}
			if(objectType=="OpenDentBusiness.Medication") {
				return Medication.Serialize((OpenDentBusiness.Medication)obj);
			}
			if(objectType=="OpenDentBusiness.Patient") {
				return Patient.Serialize((OpenDentBusiness.Patient)obj);
			}
			if(objectType=="OpenDentBusiness.Pref") {
				return Pref.Serialize((OpenDentBusiness.Pref)obj);
			}
			if(objectType=="OpenDentBusiness.Statement") {
				return Statement.Serialize((OpenDentBusiness.Statement)obj);
			}
			if(objectType=="OpenDentBusiness.Userod") {
				return Userod.Serialize((OpenDentBusiness.Userod)obj);
			}
			#endregion
			throw new NotSupportedException("CallClassSerializer, unsupported class type: "+objectType);
		}

		///<summary>Calls the class deserializer based on the typeName passed in.  Mainly used for deserializing parameters on DtoObjects.  Throws exceptions.</summary>
		public static object CallClassDeserializer(string typeName,string xml) {
			#region Primitive and General Types
			//To add more primitive/general types go to method xCrudGeneratorWebService.Form1.GetPrimGenDeserializerTypes and manually add it there.
			switch(typeName) {
				case "int":
				case "long":
				case "bool":
				case "string":
				case "char":
				case "float":
				case "byte":
				case "double":
				case "DateTime":
				case "List&lt;":
					return aaGeneralTypes.Deserialize(typeName,xml);
			}
			if(typeName.Contains("[")) {//Arrays.
				return aaGeneralTypes.Deserialize(typeName,xml);
			}
			#endregion
			#region Open Dental Classes
			if(typeName=="Allergy") {
				return Allergy.Deserialize(xml);
			}
			if(typeName=="Appointment") {
				return Appointment.Deserialize(xml);
			}
			if(typeName=="Disease") {
				return Disease.Deserialize(xml);
			}
			if(typeName=="LabPanel") {
				return LabPanel.Deserialize(xml);
			}
			if(typeName=="Medication") {
				return Medication.Deserialize(xml);
			}
			if(typeName=="Patient") {
				return Patient.Deserialize(xml);
			}
			if(typeName=="Pref") {
				return Pref.Deserialize(xml);
			}
			if(typeName=="Statement") {
				return Statement.Deserialize(xml);
			}
			if(typeName=="Userod") {
				return Userod.Deserialize(xml);
			}
			#endregion
			throw new NotSupportedException("CallClassDeserializer, unsupported class type: "+typeName);
		}

		///<summary>Finds the corresponding class, instantiates an instance of that class and invokes the method with the parameters.  Void methods will return null.</summary>
		private static object CallMethod(string classAndMethod,List<object> parameters) {
			string className=classAndMethod.Split('.')[0];
			string methodName=classAndMethod.Split('.')[1];
			#region SClasses
			if(className=="Allergies") {
				return MethodAllergies(methodName,parameters);
			}
			if(className=="Appointments") {
				return MethodAppointments(methodName,parameters);
			}
			if(className=="Diseases") {
				return MethodDiseases(methodName,parameters);
			}
			if(className=="LabPanels") {
				return MethodLabPanels(methodName,parameters);
			}
			if(className=="Medications") {
				return MethodMedications(methodName,parameters);
			}
			if(className=="Patients") {
				return MethodPatients(methodName,parameters);
			}
			if(className=="Prefs") {
				return MethodPrefs(methodName,parameters);
			}
			if(className=="Statements") {
				return MethodStatements(methodName,parameters);
			}
			if(className=="Userods") {
				return MethodUserods(methodName,parameters);
			}
			#endregion
			throw new NotSupportedException("CallMethod, unknown class: "+classAndMethod);
		}

		#region Method Calls

		///<summary></summary>
		private static object MethodAllergies(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			if(methodName=="GetActiveAllergiesPatientPortal") {
				return Allergies.GetActiveAllergiesPatientPortal(Convert.ToInt64(parameters[0]));
			}
			throw new NotSupportedException("MethodAllergies, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodAppointments(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			if(methodName=="RefreshASAP") {
				return Appointments.RefreshASAP(Convert.ToInt64(parameters[0]),Convert.ToInt64(parameters[1]),Convert.ToInt64(parameters[2]));
			}
			if(methodName=="GetScheduleAsImage") {
				return Appointments.GetScheduleAsImage((System.DateTime)parameters[0]);
			}
			throw new NotSupportedException("MethodAppointments, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodDiseases(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			if(methodName=="GetActiveDiseasesPatientPortal") {
				return Diseases.GetActiveDiseasesPatientPortal(Convert.ToInt64(parameters[0]));
			}
			throw new NotSupportedException("MethodDiseases, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodLabPanels(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			if(methodName=="GetAllPatientPortal") {
				return LabPanels.GetAllPatientPortal(Convert.ToInt64(parameters[0]));
			}
			throw new NotSupportedException("MethodLabPanels, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodMedications(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			if(methodName=="GetAllMedNamesPatientPortal") {
				return Medications.GetAllMedNamesPatientPortal(Convert.ToInt64(parameters[0]));
			}
			throw new NotSupportedException("MethodMedications, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodPatients(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			if(methodName=="GetPat") {
				return Patients.GetPat(Convert.ToInt64(parameters[0]));
			}
			if(methodName=="GetPtDataTable") {
				return Patients.GetPtDataTable(Convert.ToBoolean(parameters[0]),Convert.ToString(parameters[1]),Convert.ToString(parameters[2]),Convert.ToString(parameters[3]),Convert.ToString(parameters[4]),Convert.ToBoolean(parameters[5]),Convert.ToString(parameters[6]),Convert.ToString(parameters[7]),Convert.ToString(parameters[8]),Convert.ToString(parameters[9]),Convert.ToString(parameters[10]),Convert.ToInt64(parameters[11]),Convert.ToBoolean(parameters[12]),Convert.ToBoolean(parameters[13]),Convert.ToInt64(parameters[14]),(System.DateTime)parameters[15],Convert.ToInt64(parameters[16]),Convert.ToString(parameters[17]),Convert.ToString(parameters[18]));
			}
			if(methodName=="GetOnePatientPortal") {
				return Patients.GetOnePatientPortal(Convert.ToString(parameters[0]),Convert.ToString(parameters[1]));
			}
			if(methodName=="GetFamilyPatientPortal") {
				return Patients.GetFamilyPatientPortal(Convert.ToInt64(parameters[0]));
			}
			throw new NotSupportedException("MethodPatients, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodPrefs(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			if(methodName=="RefreshCache") {
				return Prefs.RefreshCache();
			}
			throw new NotSupportedException("MethodPrefs, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodStatements(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodStatements, unknown method: "+methodName);
		}

		///<summary></summary>
		private static object MethodUserods(string methodName,List<object> parameters) {
			//These Method[class] methods will be auto generated based on the methods in the classes within the OpenDentalWebService > Data Interface > S classes.
			throw new NotSupportedException("MethodUserods, unknown method: "+methodName);
		}

		#endregion

	}
}
