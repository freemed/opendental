using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Xml;
using OpenDentBusiness;

namespace OpenDental.Bridges {
	public class EHG_statements {
		///<summary>Generates all the xml up to the point where the first statement would go.</summary>
		public static void GeneratePracticeInfo(XmlWriter writer) {

		}

		public static void GenerateOneStatement(XmlWriter writer,Statement stmt,Patient pat,Family fam,DataSet dataSet){

		}

		///<summary>After statements are added, this adds the necessary closing xml elements.</summary>
		public static void GenerateWrapUp(XmlWriter writer) {

		}

		public static void Send(string data) {

		}


	}
}
