using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class Reconcile {

		///<summary></summary>
		public static string Serialize(OpenDentBusiness.Reconcile reconcile) {
			StringBuilder sb=new StringBuilder();
			sb.Append("<Reconcile>");
			sb.Append("<ReconcileNum>").Append(reconcile.ReconcileNum).Append("</ReconcileNum>");
			sb.Append("<AccountNum>").Append(reconcile.AccountNum).Append("</AccountNum>");
			sb.Append("<StartingBal>").Append(reconcile.StartingBal).Append("</StartingBal>");
			sb.Append("<EndingBal>").Append(reconcile.EndingBal).Append("</EndingBal>");
			sb.Append("<DateReconcile>").Append(reconcile.DateReconcile.ToString("yyyyMMddHHmmss")).Append("</DateReconcile>");
			sb.Append("<IsLocked>").Append((reconcile.IsLocked)?1:0).Append("</IsLocked>");
			sb.Append("</Reconcile>");
			return sb.ToString();
		}

		///<summary></summary>
		public static OpenDentBusiness.Reconcile Deserialize(string xml) {
			OpenDentBusiness.Reconcile reconcile=new OpenDentBusiness.Reconcile();
			using(XmlReader reader=XmlReader.Create(new StringReader(xml))) {
				reader.MoveToContent();
				while(reader.Read()) {
					//Only detect start elements.
					if(!reader.IsStartElement()) {
						continue;
					}
					switch(reader.Name) {
						case "ReconcileNum":
							reconcile.ReconcileNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "AccountNum":
							reconcile.AccountNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "StartingBal":
							reconcile.StartingBal=System.Convert.ToDouble(reader.ReadContentAsString());
							break;
						case "EndingBal":
							reconcile.EndingBal=System.Convert.ToDouble(reader.ReadContentAsString());
							break;
						case "DateReconcile":
							reconcile.DateReconcile=DateTime.ParseExact(reader.ReadContentAsString(),"yyyyMMddHHmmss",null);
							break;
						case "IsLocked":
							reconcile.IsLocked=reader.ReadContentAsString()!="0";
							break;
					}
				}
			}
			return reconcile;
		}


	}
}