using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;
using OpenDental.UI;

namespace OpenDental {
	public partial class FormWiki:Form {
		public WikiPage PageCur;
		public WikiPage PageMaster;
		public WikiPage PageStyle;
		private string targetPage;
		private string pageAggregate;
		private string wikiLocation;
		private int pageCount;
		private bool loading;

		public FormWiki() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormWiki_Load(object sender,EventArgs e) {
			LayoutToolBar();
			LoadLayoutPages();
			WritePage("Home");
			webBrowserWiki.Url=new Uri(wikiLocation+"Home.html");
		}

		private void LayoutToolBar() {
			ToolBarMain.Buttons.Clear();
			ToolBarMain.Buttons.Add(new ODToolBarButton(Lan.g(this,"Setup"),-1,Lan.g(this,"Setup wiki styles and behavior."),"Setup"));
			ToolBarMain.Buttons.Add(new ODToolBarButton(ODToolBarButtonStyle.Separator));
			ToolBarMain.Buttons.Add(new ODToolBarButton(Lan.g(this,"Edit"),-1,"","Edit"));
			ToolBarMain.Buttons.Add(new ODToolBarButton(Lan.g(this,"Rename"),-1,"","Rename"));
			ToolBarMain.Buttons.Add(new ODToolBarButton(Lan.g(this,"Delete"),-1,"","Delete"));
			ToolBarMain.Buttons.Add(new ODToolBarButton(Lan.g(this,"History"),-1,"","History"));
			ToolBarMain.Buttons.Add(new ODToolBarButton(Lan.g(this,"Inc Links"),-1,"","Inc Links"));
			ToolBarMain.Buttons.Add(new ODToolBarButton(ODToolBarButtonStyle.Separator));
			ToolBarMain.Buttons.Add(new ODToolBarButton(Lan.g(this,"Add"),-1,"","Add"));
			ToolBarMain.Buttons.Add(new ODToolBarButton(Lan.g(this,"All Pages"),-1,"","All Pages"));
			ToolBarMain.Buttons.Add(new ODToolBarButton(Lan.g(this,"Search"),-1,"","Search"));
		}

		private void ToolBarMain_ButtonClick(object sender,OpenDental.UI.ODToolBarButtonClickEventArgs e) {
			switch(e.Button.Tag.ToString()) {
				case "Setup":
					Setup_Click();
					break;
				case "Edit":
					Edit_Click();
					break;
				case "Rename":
					Rename_Click();
					break;
				case "Delete":
					Delete_Click();
					break;
				case "History":
					History_Click();
					break;
				case "Inc Links":
					Inc_Link_Click();
					break;
				case "Add":
					Add_Click();
					break;
				case "All Pages":
					All_Pages_Click();
					break;
				case "Search":
					Search_Click();
					break;
			}
		}

		private void Setup_Click() {
			FormWikiSetup FormWS=new FormWikiSetup();
			FormWS.ShowDialog();
		}

		private void Edit_Click() {
			FormWikiEdit FormWE=new FormWikiEdit();
			FormWE.WikiPageCur=PageCur;
			FormWE.ShowDialog();//do we want this to be modal??
			//ReloadPage.
		}

		private void Rename_Click() {
			throw new NotImplementedException();
		}

		private void Delete_Click() {
			throw new NotImplementedException();
		}

		private void History_Click() {
			throw new NotImplementedException();
		}

		private void Inc_Link_Click() {
			throw new NotImplementedException();
		}

		private void Add_Click() {
			FormWikiEdit FormWE=new FormWikiEdit();
			FormWE.WikiPageCur=new WikiPage();
			FormWE.WikiPageCur.IsNew=true;
			FormWE.ShowDialog();//do we want this to be modal??  Yes.
			//ReloadPage.
		}

		private void All_Pages_Click() {
			throw new NotImplementedException();
		}

		private void Search_Click() {
			throw new NotImplementedException();
		}

		private void LoadLayoutPages() {
			PageMaster=WikiPages.GetMaster();
			PageStyle=WikiPages.GetByName("_Style");
			if(PageStyle.PageContent.StartsWith("<style>") && PageStyle.PageContent.EndsWith("</style")) {
				//properly formed
			}
			else {
				PageStyle.PageContent="<style>"+PageStyle.PageContent+"</style>";
			}
			wikiLocation=Environment.GetCommandLineArgs()[0].Substring(0,Environment.GetCommandLineArgs()[0].Length-21)+"Wiki\\";
			if(!System.IO.Directory.Exists(wikiLocation)) {
				System.IO.Directory.CreateDirectory(wikiLocation);
			}
			pageCount=0;
		}

		private void LoadPage(string PageName) {
			PageCur=WikiPages.GetByName(PageName);
			pageAggregate=WikiPages.TranslateToXhtml(PageCur.PageContent);
			pageAggregate=PageMaster.PageContent.Replace("@@@Content@@@",pageAggregate);
			pageAggregate=pageAggregate.Replace("@@@PageTitle@@@",PageCur.PageTitle);
			pageAggregate=pageAggregate.Replace("@@@PageStyle@@@",PageStyle.PageContent);
			this.Text="Wiki - "+PageCur.PageTitle;
			webBrowserWiki.DocumentText=pageAggregate;
		}

		private void WritePage(string PageName) {
			PageCur=WikiPages.GetByName(PageName);
			pageAggregate=WikiPages.TranslateToXhtml(PageCur.PageContent);
			pageAggregate=PageMaster.PageContent.Replace("@@@Content@@@",pageAggregate);
			pageAggregate=pageAggregate.Replace("@@@PageTitle@@@",PageCur.PageTitle);
			pageAggregate=pageAggregate.Replace("@@@PageStyle@@@",PageStyle.PageContent);
			//if(System.IO.File.Exists(wikiLocation+PageName+".html")) {
			//  pageCount++;
			//  PageName=PageName+"_"+pageCount;
			//  targetPage=PageName;
			//}
			targetPage=PageName;
			System.IO.File.WriteAllText(wikiLocation+PageName+".html",pageAggregate);
		}

		private void webBrowserWiki_Navigating(object sender,WebBrowserNavigatingEventArgs e) {//not reliable enough
			System.IO.File.AppendAllText(wikiLocation+"log2.txt","webBrowserWiki_ProgressChanged\r\n");
			if(((webBrowserWiki.Url!=null && webBrowserWiki.Url.AbsoluteUri.Contains("wiki:"))||webBrowserWiki.StatusText.Contains("wiki:")) && !loading) {
				System.IO.File.AppendAllText(wikiLocation+"log.txt",
					"****************************webBrowserWiki_Navigating*******************************************************"+
					"\r\nDocument      "+webBrowserWiki.Document+
					//"\r\nText:         "+webBrowserWiki.DocumentText+
					"\r\nDocumentTitle:"+webBrowserWiki.DocumentTitle+
					"\r\nIsBusy:       "+webBrowserWiki.IsBusy.ToString()+
					"\r\nReadyState:   "+webBrowserWiki.ReadyState.ToString()+
					"\r\nSite:         "+webBrowserWiki.Site+
					"\r\nStatusText:   "+webBrowserWiki.StatusText+
					"\r\nUrl:          "+webBrowserWiki.Url+"\r\n\r\n"
					);
				this.Text="Wiki - "+PageCur.PageTitle;
				targetPage=webBrowserWiki.StatusText.Substring(5);
				WritePage(targetPage);
				webBrowserWiki.Url=new Uri(wikiLocation+targetPage+".html");
				loading = true;
			}
		}

		private void webBrowserWiki_Navigated(object sender,WebBrowserNavigatedEventArgs e) {
			loading=false;
			System.IO.File.AppendAllText(wikiLocation+"log2.txt","webBrowserWiki_Navigated\r\n");
			System.IO.File.AppendAllText(wikiLocation+"log.txt",
				"******************************webBrowserWiki_Navigated*****************************************************"+
					"\r\nDocument      "+webBrowserWiki.Document+
					//"\r\nText:         "+webBrowserWiki.DocumentText+
					"\r\nDocumentTitle:"+webBrowserWiki.DocumentTitle+
					"\r\nIsBusy:       "+webBrowserWiki.IsBusy.ToString()+
					"\r\nReadyState:   "+webBrowserWiki.ReadyState.ToString()+
					"\r\nSite:         "+webBrowserWiki.Site+
					"\r\nStatusText:   "+webBrowserWiki.StatusText+
					"\r\nUrl:          "+webBrowserWiki.Url+"\r\n\r\n"
				);
		}

		private void menuItemSetup_Click(object sender,EventArgs e) {

		}

		private void menuItemEdit_Click(object sender,EventArgs e) {
			FormWikiEdit FormWE = new FormWikiEdit();
			FormWE.WikiPageCur=PageCur;
			FormWE.Show();
		}

		private void butOK_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		private void webBrowserWiki_ProgressChanged(object sender,WebBrowserProgressChangedEventArgs e) {
			System.IO.File.AppendAllText(wikiLocation+"log2.txt","webBrowserWiki_ProgressChanged:"+webBrowserWiki.Url+"\r\n");
				//+e.CurrentProgress.ToString().PadLeft(5)+"/"+e.MaximumProgress.ToString().PadLeft(5)+"\r\n");
			System.IO.File.AppendAllText(wikiLocation+"log.txt",
				"******************************webBrowserWiki_ProgressChanged_Before*****************************************************"+
					"\r\nDocument      "+webBrowserWiki.Document+
					//"\r\nText:         "+webBrowserWiki.DocumentText+
					"\r\nDocumentTitle:"+webBrowserWiki.DocumentTitle+
					"\r\nIsBusy:       "+webBrowserWiki.IsBusy.ToString()+
					"\r\nReadyState:   "+webBrowserWiki.ReadyState.ToString()+
					"\r\nSite:         "+webBrowserWiki.Site+
					"\r\nStatusText:   "+webBrowserWiki.StatusText+
					"\r\nUrl:          "+webBrowserWiki.Url+"\r\n\r\n"
				);
			if(webBrowserWiki.StatusText.StartsWith("wiki:") && !webBrowserWiki.IsBusy) {
				this.Text="Wiki - "+PageCur.PageTitle;
				targetPage=webBrowserWiki.StatusText.Substring(5);
				WritePage(targetPage);
				webBrowserWiki.Url=new Uri(wikiLocation+targetPage+".html");
			}
			System.IO.File.AppendAllText(wikiLocation+"log.txt",
				"******************************webBrowserWiki_ProgressChanged_Before*****************************************************"+
					"\r\nDocument      "+webBrowserWiki.Document+
					//"\r\nText:         "+webBrowserWiki.DocumentText+
					"\r\nDocumentTitle:"+webBrowserWiki.DocumentTitle+
					"\r\nIsBusy:       "+webBrowserWiki.IsBusy.ToString()+
					"\r\nReadyState:   "+webBrowserWiki.ReadyState.ToString()+
					"\r\nSite:         "+webBrowserWiki.Site+
					"\r\nStatusText:   "+webBrowserWiki.StatusText+
					"\r\nUrl:          "+webBrowserWiki.Url+"\r\n\r\n"
				);
		}

	}
}