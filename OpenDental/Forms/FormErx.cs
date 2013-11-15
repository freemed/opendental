using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental {
	public partial class FormErx:Form {

		private string browseToUrl="";
		public Provider prov;
		public Employee emp;
		public Patient pat;

		public FormErx() {
			InitializeComponent();
			Lan.F(this);
			SHDocVw.WebBrowser axBrowser=(SHDocVw.WebBrowser)browser.ActiveXInstance;
			//axBrowser.NewWindow2+=new SHDocVw.DWebBrowserEvents2_NewWindow3EventHandler(axBrowser_NewWindow2);
			axBrowser.NewWindow3+=new SHDocVw.DWebBrowserEvents2_NewWindow3EventHandler(axBrowser_NewWindow3);
		}

		public FormErx(string url) {
			InitializeComponent();
			Lan.F(this);
			browseToUrl=url;
		}

		private void FormErx_Load(object sender,EventArgs e) {
			Cursor=Cursors.WaitCursor;//work on this later if needed.
			Application.DoEvents();
			if(browseToUrl!="") { //Use the window as a simple web browswer when a URL is passed in.
				Text="";
				browser.Navigate(browseToUrl);
				return;
			}
			ComposeNewRx();
		}

		///<summary>Uses the public prov, emp and pat variables to build a new prescription and load it within browser control. Loads the compose tab in NewCrop's web interface.</summary>
		private void ComposeNewRx() {
			string clickThroughXml="";// ErxXml.BuildClickThroughXml(prov,emp,pat);
#if DEBUG //To make capturing the XML easier.
			string tempFile=Path.GetTempFileName()+".txt";
			File.WriteAllText(tempFile,clickThroughXml);
			File.Delete(tempFile);//Put a break point here to capture XML.
#endif
			string xmlBase64=System.Web.HttpUtility.HtmlEncode(Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes(clickThroughXml)));
			xmlBase64=xmlBase64.Replace("+","%2B");//A common base 64 character which needs to be escaped within URLs.
			xmlBase64=xmlBase64.Replace("/","%2F");//A common base 64 character which needs to be escaped within URLs.
			xmlBase64=xmlBase64.Replace("=","%3D");//Base 64 strings usually end in '=', which could mean a new parameter definition within the URL so we escape.
			String postdata="RxInput=base64:"+xmlBase64;
			byte[] PostDataBytes=System.Text.Encoding.UTF8.GetBytes(postdata);
			string additionalHeaders="Content-Type: application/x-www-form-urlencoded\r\n";
#if DEBUG
			string newCropUrl="http://preproduction.newcropaccounts.com/interfaceV7/rxentry.aspx";
#else //Debug
			string newCropUrl="https://secure.newcropaccounts.com/interfacev7/rxentry.aspx";
#endif
			browser.Navigate(newCropUrl,"",PostDataBytes,additionalHeaders);
		}

		///<summary>This event fires when a link is clicked within the webbrowser control which opens in a new window.</summary>
		private void browser_NewWindow(object sender,CancelEventArgs e) {
			//By default, new windows launched by clicking a link from within the webbrowser control, open in Internet Explorer, even if the system default is another web browser such as Mozilla.
			//We had a problem with cookies not being carried over from our webbrowser control into Internet Explorer when a link is clicked.
			//To preserve cookies, we intercept the new window creation, cancel it, then launch the destination URL in a new window within OD.
			string destinationUrl=browser.StatusText;//This is the URL of the page that is supposed to open in a new window. For example, the "ScureScripts Drug History" link.
			if(Regex.IsMatch(destinationUrl,"^.*javascript\\:.*$",RegexOptions.IgnoreCase)) {
				return;
			}
			e.Cancel=true;//Cancel Internet Explorer from launching.
			FormErx browserWindowNew=new FormErx(destinationUrl);//Open the page in a new window, but stay inside of OD.
			browserWindowNew.WindowState=FormWindowState.Normal;
			browserWindowNew.ShowDialog();
		}

		///<summary>This event fires when a javascript snippet calls window.open() to open a URL in a new browser window. When window.open() is called, our browser_NewWindow() event function does not fire.</summary>
		//private void axBrowser_NewWindow2(object sender,AxSHDocVw.DWebBrowserEvents2_NewWindow2Event e) {
		//  Form1 frmWB;
		//  frmWB = new Form1();
		//  frmWB.axWebBrowser1.RegisterAsBrowser = true;
		//  e.ppDisp = frmWB.axWebBrowser1.Application;
		//  frmWB.Visible = true;
		//}

		void axBrowser_NewWindow3(ref object ppDisp,ref bool Cancel,uint dwFlags,string bstrUrlContext,string bstrUrl) {
			
		}

		public void browser_DocumentCompleted(object sender,WebBrowserDocumentCompletedEventArgs e) {
			Cursor=Cursors.Default;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

	}
}