///Dennis: There are far too many intricacies for the Enterprise library  all of which would be an overkill if througly explored.
///At this stge all I needed was simple logging to a flat files. If needed the more features could be added later.
/// The following code has being pieced together from the web uses the of Enterprise Library Logging Application Block without the hassle of configuration 
///setting in the config file. It shows two listeners, a flat file 
///listener and the event log listener. Errors go to the Event Log - i.e if the code pertaining to it is uncomented.
///and everything else to file
///To compile this code the Enterprise library should installed from http://www.microsoft.com/downloads/details.aspx?familyid=1643758B-2986-47F7-B529-3E41584B6CE5&displaylang=en



using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using Microsoft.Practices.EnterpriseLibrary.Logging.Formatters;
using Microsoft.Practices.EnterpriseLibrary.Logging.TraceListeners;
using Microsoft.Practices.EnterpriseLibrary.Logging.Filters;

namespace WebHostSynch {

	public static class Logger {
		/// <summary>
		/// Log writer
		/// </summary>
		static readonly LogWriter writer;

		/// <summary>
		/// Static constructor
		/// </summary>
		static Logger() {
			string LogFile = ConfigurationManager.AppSettings["LogFile"].ToString();

			// formatter
			TextFormatter formatter = new TextFormatter("[{timestamp}] [{machine}] {category}  \t: {message}");

			// listeners
			FlatFileTraceListener logFileListener = new FlatFileTraceListener(LogFile,"","",formatter);
			FormattedEventLogTraceListener logEventListener = new FormattedEventLogTraceListener("Enterprise Library Logging",formatter);

			// Sources
			LogSource mainLogSource = new LogSource("MainLogSource",SourceLevels.All);
			mainLogSource.Listeners.Add(logFileListener);

			LogSource errorLogSource = new LogSource("ErrorLogSource",SourceLevels.Error);
			errorLogSource.Listeners.Add(logEventListener);

			// empty source
			LogSource nonExistantLogSource = new LogSource("Empty");

			// trace sources
			IDictionary<string,LogSource> traceSources = new Dictionary<string,LogSource>();
			traceSources.Add("Error",errorLogSource);
			traceSources.Add("Warning",mainLogSource);
			traceSources.Add("Information",mainLogSource);


			// log writer
			writer = new LogWriter(new ILogFilter[0],traceSources,mainLogSource,nonExistantLogSource,
				errorLogSource,"Error",false,true);
		}


		/// <summary>
		/// Writes an Error to the log. Dennis - uncomment method below to enable Error()
		/// </summary>
		/// <param name="message">Error Message</param>
		/*
		public static void Error(string message)
		{ 
			Write(message,TraceEventType.Error);
		}
		*/
		/// <summary>
		/// dennis - uncomment method below to enable Warning()
		/// Writes a Warning to the log. 
		/// </summary>
		/// <param name="message">Warning Message</param>
		/*
		public static void Warning(string message)
		{  
			Write(message,TraceEventType.Warning);
		}
		*/
		/// <summary>
		/// Writes an Information to the log.
		/// </summary>
		/// <param name="message">Information Message</param>
		public static void Information(string message) {
			StackFrame stFrame = new StackFrame(1,true);
			string Filename = " Filename: " + stFrame.GetFileName().Substring(stFrame.GetFileName().LastIndexOf(@"\")+1);
			string MethodName =" Method: "+ stFrame.GetMethod();
			string LineNumber =" LineNumber: "+ stFrame.GetFileLineNumber();
			message = message + Filename + MethodName + LineNumber;
			Write(message,TraceEventType.Information);
		}

		/// <summary>
		/// Writes a message to the log using the specified category.
		/// </summary>
		/// <param name="message"> Message to log</param>
		/// <param name="category">Message category. e.g. 'Error','Warning','Information'</param>
		private static void Write(string message,string category) {
			LogEntry entry = new LogEntry();

			entry.Categories.Add(category);
			entry.Message = message;

			writer.Write(entry);
		}

		/// <summary>
		/// Writes a message to the log using the specified
		/// category.
		/// </summary>
		/// <param name="message"></param>
		/// <param name="category"></param>
		private static void Write(string message,TraceEventType severity) {
			LogEntry entry = new LogEntry();

			entry.Categories.Add(severity.ToString());
			entry.Message = message;
			entry.Severity = severity;
			writer.Write(entry);
		}
	}
}
