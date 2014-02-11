using System;
using System.Collections;
using System.Drawing;

namespace OpenDentBusiness {
	///<summary>Currently only used by web applications to create log entries. May be used by Open Dental client application in the future.</summary>
	[Serializable()]
	public class Logod:TableBase {
		///<summary>Primary key.</summary>
		[CrudColumn(IsPriKey=true)]
		public long LogNum;
		///<summary>Timestamp at which time this entry was created.</summary>
		[CrudColumn(SpecialType=CrudSpecialColType.DateTEntry)]
		public DateTime DateTStamp;
		///<summary>The log entry message text.</summary>		
		public string Message;
		///<summary>Call stack at instance of log entry.</summary>		
		public string CallStack;
		///<summary>Function which created the log entry.</summary>
		public string FunctionName;
		///<summary>Indicates if entry was created as a result of a handled exception.</summary>
		public bool IsException;
		///<summary>Extra data that may be helpful.  An example of this would be the serialized Data Transfer Object (DTO) responsible for creating this log entry.</summary>
		public string DataContext;
		///<summary>The log level required in order to create this entry.</summary>
		public LogLevel LogLevelOfEntry;
		///<summary>The currrent log level set for the application.  Will always be less than or equal to LogLevelOfEntry.</summary>
		public LogLevel LogLevelOfApplication;

		///<summary></summary>
		public Logod Copy() {
			return (Logod)this.MemberwiseClone();
		}
	}

	///<summary>0=Information, 1=Error</summary>
	public enum LogLevel {
		///<summary>0 log only errors.</summary>
		Error,
		///<summary>1 most verbose form of logging. Logs all entries all the time.</summary>
		Information
	}

}