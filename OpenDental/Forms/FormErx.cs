using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental {
	public partial class FormErx:Form {

		public Provider prov;
		public Employee emp;
		public Patient pat;

		public FormErx() {
			InitializeComponent();
			browser.DocumentCompleted+=new WebBrowserDocumentCompletedEventHandler(browser_DocumentCompleted);
			Lan.F(this);
		}

		private void FormErx_Load(object sender,EventArgs e) {
			string clickThroughXml=ErxXml.BuildClickThroughXml(prov,emp,pat);
			string xmlBase64=System.Web.HttpUtility.HtmlEncode(Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes(clickThroughXml)));
			xmlBase64=xmlBase64.Replace("+","%2B");//A common base 64 character which needs to be escaped within URLs.
			xmlBase64=xmlBase64.Replace("/","%2F");//A common base 64 character which needs to be escaped within URLs.
			xmlBase64=xmlBase64.Replace("=","%3D");//Base 64 strings usually end in '=', which could mean a new parameter definition within the URL so we escape.
			String postdata="RxInput=base64:"+xmlBase64;
			byte[] PostDataBytes=System.Text.Encoding.UTF8.GetBytes(postdata);
			string additionalHeaders="Content-Type: application/x-www-form-urlencoded\r\n";
			Cursor=Cursors.WaitCursor;//work on this later if needed.
			Application.DoEvents();
			browser.Navigate("http://preproduction.newcropaccounts.com/InterfaceV7/RxEntry.aspx","",PostDataBytes,additionalHeaders);
		}

		public void browser_DocumentCompleted(object sender,WebBrowserDocumentCompletedEventArgs e) {
			Cursor=Cursors.Default;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

	}
}