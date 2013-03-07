using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;
using OpenDental.UI;

namespace OpenDental {
	public partial class FormWiki:Form {
		public WikiPage WikiPageCur;		
		private List<string> historyNav;
		///<summary>Number of pages back that you are browsing. Current page == 0, Oldest page == historyNav.Length. </summary>
		private int historyNavBack;
		const int FEATURE_DISABLE_NAVIGATION_SOUNDS = 21;
		const int SET_FEATURE_ON_PROCESS = 0x00000002;
		[DllImport("urlmon.dll")]
		[PreserveSig]
		[return: MarshalAs(UnmanagedType.Error)]
		static extern int CoInternetSetFeatureEnabled(int FeatureEntry,[MarshalAs(UnmanagedType.U4)] int dwFlags,bool fEnable);

		public FormWiki() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormWiki_Load(object sender,EventArgs e) {
			//disable the annoying clicking sound when the browser navigates
			CoInternetSetFeatureEnabled(FEATURE_DISABLE_NAVIGATION_SOUNDS,SET_FEATURE_ON_PROCESS,true);
			webBrowserWiki.StatusTextChanged += new EventHandler(WebBrowserWiki_StatusTextChanged);
			Rectangle rectWorkingArea=System.Windows.Forms.Screen.GetWorkingArea(this);
			Top=0;
			Left=Math.Max(0,((rectWorkingArea.Width-960)/2)+rectWorkingArea.Left);
			Width=Math.Min(rectWorkingArea.Width,960);
			Height=rectWorkingArea.Height;
			LayoutToolBar();
			historyNav=new List<string>();
			LoadWikiPage("Home");
		}

		private void WebBrowserWiki_StatusTextChanged(object sender,EventArgs e) {
			//if(webBrowserWiki.StatusText=="") {
			//  return;
			//}
			labelStatus.Text=webBrowserWiki.StatusText;
			if(labelStatus.Text=="Done") {
				labelStatus.Text="";
			}
		}

		private void LoadWikiPage(string pageTitle) {
			//This is called from 11 different places, any time the program needs to refresh a page from the db.
			//It's also called from the browser_Navigating event when a "wiki:" link is clicked.
			WikiPage wpage=WikiPages.GetByTitle(pageTitle);
			if(wpage==null) {
				if(!MsgBox.Show(this,MsgBoxButtons.YesNo,"That page does not exist. Would you like to create it?")) {
					return;
				}
				FormWikiEdit FormWE=new FormWikiEdit();
				FormWE.WikiPageCur=new WikiPage();
				FormWE.WikiPageCur.IsNew=true;
				FormWE.WikiPageCur.PageTitle=pageTitle;
				FormWE.WikiPageCur.PageContent="[["+WikiPageCur.PageTitle+"]]\r\n"//link back
					+"<h1>"+pageTitle+"</h1>\r\n";//page title
				FormWE.ShowDialog();
				if(FormWE.DialogResult!=DialogResult.OK) {
					return;
				}
				wpage=WikiPages.GetByTitle(pageTitle);
			}
			WikiPageCur=wpage;
			webBrowserWiki.DocumentText=WikiPages.TranslateToXhtml(WikiPageCur.PageContent,false);
			Text="Wiki - "+WikiPageCur.PageTitle;
			if(historyNav.Count==0) {//empty history
				historyNav.Add("wiki:"+pageTitle);
			}
			else if(historyNavBack==0 && historyNav[historyNav.Count-1]!="wiki:"+pageTitle) {//current page, check for duplicate
				historyNav.Add("wiki:"+pageTitle);
			}
			else if(historyNav[historyNav.Count-(1+historyNavBack)]!="wiki:"+pageTitle) {//branching from page in history
				historyNav.RemoveRange(historyNav.Count-(1+historyNavBack),historyNavBack);//remove "future" history. branching off in a new direction
				historyNavBack=0;
				historyNav.Add("wiki:"+pageTitle);
			}
		}

		private void LayoutToolBar() {
			ToolBarMain.Buttons.Clear();
			ToolBarMain.Buttons.Add(new ODToolBarButton(Lan.g(this,"Back"),0,"","Back"));
			ToolBarMain.Buttons.Add(new ODToolBarButton(Lan.g(this,"Forward"),1,"","Forward"));
			ToolBarMain.Buttons.Add(new ODToolBarButton(Lan.g(this,"Setup"),2,Lan.g(this,"Setup master page and styles."),"Setup"));
			ToolBarMain.Buttons.Add(new ODToolBarButton(ODToolBarButtonStyle.Separator));
			ToolBarMain.Buttons.Add(new ODToolBarButton(Lan.g(this,"Home"),3,"","Home"));
			ToolBarMain.Buttons.Add(new ODToolBarButton(Lan.g(this,"Edit"),4,"","Edit"));
			ToolBarMain.Buttons.Add(new ODToolBarButton(Lan.g(this,"Print"),5,"","Print"));
			ToolBarMain.Buttons.Add(new ODToolBarButton(Lan.g(this,"Rename"),6,"","Rename"));
			ToolBarMain.Buttons.Add(new ODToolBarButton(Lan.g(this,"Delete"),7,"","Delete"));
			ToolBarMain.Buttons.Add(new ODToolBarButton(Lan.g(this,"History"),8,"","History"));
			ToolBarMain.Buttons.Add(new ODToolBarButton(Lan.g(this,"Incoming Links"),9,"","Inc Links"));
			ToolBarMain.Buttons.Add(new ODToolBarButton(ODToolBarButtonStyle.Separator));
			ToolBarMain.Buttons.Add(new ODToolBarButton(Lan.g(this,"Add"),10,"","Add"));
			//ToolBarMain.Buttons.Add(new ODToolBarButton(Lan.g(this,"All Pages"),11,"","All Pages"));
			ToolBarMain.Buttons.Add(new ODToolBarButton(Lan.g(this,"Search"),12,"","Search"));
		}

		private void ToolBarMain_ButtonClick(object sender,OpenDental.UI.ODToolBarButtonClickEventArgs e) {
			switch(e.Button.Tag.ToString()) {
				case "Back":
					Back_Click();
					break;
				case "Forward":
					Forward_Click();
					break;
				case "Setup":
					Setup_Click();
					break;
				case "Home":
					Home_Click();
					break;
				case "Edit":
					Edit_Click();
					break;
				case "Print":
					Print_Click();
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

		private void Back_Click() {
			if(historyNavBack<historyNav.Count-1) {
				historyNavBack++;
			}
			NavToHistory();
			//if(historyNav.Count<2) {//should always be 1 or greater
			//  MsgBox.Show(this,"No more history");
			//  return;
			//}
			//string pageName=historyNav[historyNav.Count-2];//-1 is the last/current page.
			//if(pageName.StartsWith("wiki:")) {
			//  pageName=pageName.Substring(5);
			//  WikiPage wpage=WikiPages.GetByTitle(pageName);
			//  if(wpage==null) {
			//    MessageBox.Show("'"+historyNav[historyNav.Count-2]+"' page does not exist.");//very rare
			//    return;
			//  }
			//  historyNav.RemoveAt(historyNav.Count-1);//remove the current page from history
			//  LoadWikiPage(pageName);//because it's a duplicate, it won't add it again to the list.
			//}
			//else if(pageName.StartsWith("http://")) {//www
			//  //historyNav.RemoveAt(historyNav.Count-1);//remove the current page from history
			//  //no need to set the text because the Navigating event will fire and take care of that.
			//  webBrowserWiki.Navigate(pageName);//adds new page to history
			//}
			//else {
			//  //?
			//}
		}

		private void Forward_Click() {
			if(historyNavBack>0) {
				historyNavBack--;
			}
			NavToHistory();
		}

		///<summary>Loads page from history based on historyCurIndex.</summary> 
		private void NavToHistory() {
			if(historyNavBack<0 || historyNavBack>historyNav.Count-1) {
				//This should never happen.
				MsgBox.Show(this,"Invalid history index.");
				return;
			}
			string pageName=historyNav[historyNav.Count-(1+historyNavBack)];//-1 is the last/current page.
			if(pageName.StartsWith("wiki:")) {
				pageName=pageName.Substring(5);
				WikiPage wpage=WikiPages.GetByTitle(pageName);
				if(wpage==null) {
					MessageBox.Show("'"+historyNav[historyNav.Count-(1+historyNavBack)]+"' page does not exist.");//very rare
					return;
				}
				LoadWikiPage(pageName);//because it's a duplicate, it won't add it again to the list.
			}
			else if(pageName.StartsWith("http://")) {//www
				//no need to set the text because the Navigating event will fire and take care of that.
				webBrowserWiki.Navigate(pageName);
			}
			else {
				//?
			}
		}

		private void Setup_Click() {
			FormWikiSetup FormWS=new FormWikiSetup();
			FormWS.ShowDialog();
			if(FormWS.DialogResult!=DialogResult.OK) {
				return;
			}
			if(WikiPageCur==null) {//if browsing the WWW
				return;
			}
			LoadWikiPage(WikiPageCur.PageTitle);
		}

		private void Home_Click() {
			LoadWikiPage("Home");//TODO later:replace with dynamic PrefC.Getstring(PrefName.WikiHomePage)
		}

		private void Edit_Click() {
			if(WikiPageCur==null) {
				return;
			}
			FormWikiEdit FormWE=new FormWikiEdit();
			FormWE.WikiPageCur=WikiPageCur;
			FormWE.ShowDialog();
			if(FormWE.DialogResult!=DialogResult.OK) {
				return;
			}
			LoadWikiPage(FormWE.WikiPageCur.PageTitle);
		}

		private void Print_Click() {
			if(WikiPageCur==null) {
				return;
			}
			webBrowserWiki.ShowPrintDialog();
		}

		private void Rename_Click() {
			if(WikiPageCur==null) {
				return;
			}
			FormWikiRename FormWR=new FormWikiRename();
			FormWR.PageTitle=WikiPageCur.PageTitle;
			FormWR.ShowDialog();
			if(FormWR.DialogResult!=DialogResult.OK) {
				return;
			}
			WikiPages.Rename(WikiPageCur,FormWR.PageTitle);
			LoadWikiPage(FormWR.PageTitle);
		}

		private void Delete_Click() {
			if(WikiPageCur==null) {
				return;
			}
			if(WikiPageCur.PageTitle=="Home") {
				MsgBox.Show(this,"Cannot delete homepage."); 
				return;
			}
			if(!MsgBox.Show(this,MsgBoxButtons.OKCancel,"Delete this wiki page?  It will still be available from the Search window if needed.")) {
				return;
			}
			WikiPages.Delete(WikiPageCur.PageTitle);
			LoadWikiPage("Home");
		}

		private void History_Click() {
			if(WikiPageCur==null) {
				return;
			}
			FormWikiHistory FormWH = new FormWikiHistory();
			FormWH.PageTitleCur=WikiPageCur.PageTitle;
			FormWH.ShowDialog();
			LoadWikiPage(FormWH.PageTitleCur);
			//if(FormWH.DialogResult!=DialogResult.OK) {
			//	return;
			//}
			//Nothing to do here.
		}

		private void Inc_Link_Click() {
			if(WikiPageCur==null) {
				return;
			}
			FormWikiIncomingLinks FormWIC = new FormWikiIncomingLinks();
			FormWIC.PageTitleCur=WikiPageCur.PageTitle;
			FormWIC.ShowDialog();
			if(FormWIC.DialogResult!=DialogResult.OK) {
				return;
			}
			LoadWikiPage(FormWIC.JumpToPage.PageTitle);
		}

		private void Add_Click() {
			FormWikiRename FormWR=new FormWikiRename();
			FormWR.ShowDialog();
			if(FormWR.DialogResult!=DialogResult.OK) {
				return;
			}
			FormWikiEdit FormWE=new FormWikiEdit();
			FormWE.WikiPageCur=new WikiPage();
			FormWE.WikiPageCur.IsNew=true;
			FormWE.WikiPageCur.PageTitle=FormWR.PageTitle;
			FormWE.WikiPageCur.PageContent="[["+WikiPageCur.PageTitle+"]]\r\n"//link back
				+"<h1>"+FormWR.PageTitle+"</h1>\r\n";//page title
			FormWE.ShowDialog();
			if(FormWE.DialogResult!=DialogResult.OK) {
				return;
			}
			LoadWikiPage(FormWE.WikiPageCur.PageTitle);
		}

		private void All_Pages_Click() {
			FormWikiAllPages FormWAP=new FormWikiAllPages();
			FormWAP.ShowDialog();
			if(FormWAP.DialogResult!=DialogResult.OK) {
				return;
			}
			LoadWikiPage(FormWAP.SelectedWikiPage.PageTitle);
		}

		private void Search_Click() {
			FormWikiSearch FormWS=new FormWikiSearch();
			FormWS.ShowDialog();
			if(FormWS.DialogResult!=DialogResult.OK) {
				return;
			}
			if(FormWS.wikiPageTitleSelected=="") {
				return;
			}
			LoadWikiPage(FormWS.wikiPageTitleSelected);
		}

		private void webBrowserWiki_Navigating(object sender,WebBrowserNavigatingEventArgs e) {
			//For debugging, we need to remember that the following happens when you click on an internal link:
			//1. This method fires. url includes 'wiki:'
			//2. This causes LoadWikiPage method to fire.  It loads the document text.
			//3. Which causes this method to fire again.  url is "about:blank".
			//This doesn't seem to be a problem.  We wrote it so that it won't go into an infinite loop, but it's something to be aware of.
			if(e.Url.ToString()=="about:blank") {
				//webBrowserWiki.DocumentText was set externally. We want to ignore this situation.
			}
			else if(e.Url.ToString().StartsWith("about:")){//malformed URL.
					e.Cancel=true;
					return;
			}
			else if(e.Url.ToString().StartsWith("wiki:")) {//user clicked on an internal link
				LoadWikiPage(e.Url.ToString().Substring(5));
				e.Cancel=true;
				return;
			}
			else if(e.Url.ToString().Contains("wikifile:")) {
				string fileName=e.Url.ToString().Substring(e.Url.ToString().LastIndexOf("wikifile:")+9).Replace("/","\\");
				if(!File.Exists(fileName)) {
					MessageBox.Show(Lan.g(this,"File does not exist: ")+fileName);
					e.Cancel=true;
					return;
				}
				try {
					System.Diagnostics.Process.Start(fileName);
				}
				catch(Exception ex) { }
				e.Cancel=true;
				return;
			}
			else if(e.Url.ToString().Contains("folder:")) {
				string folderName=e.Url.ToString().Substring(e.Url.ToString().LastIndexOf("wikifile:")+7).Replace("/","\\");
				if(!Directory.Exists(folderName)) {
					MessageBox.Show(Lan.g(this,"Folder does not exist: ")+folderName);
					e.Cancel=true;
					return;
				}
				try {
					System.Diagnostics.Process.Start(folderName);
				}
				catch(Exception ex) { }
				e.Cancel=true;
				return;
			}
			else if(e.Url.ToString().StartsWith("http://")){//navigating outside of wiki, either by clicking a link or using back button.
				WikiPageCur=null;//this effectively disables most of the toolbar buttons
				Text = "Wiki - WWW";
				if(historyNav.Count==0){//empty history
					historyNav.Add(e.Url.ToString());
				}
				else if(historyNavBack==0 && historyNav[historyNav.Count-1]!=e.Url.ToString()) {//current page, check for duplicate
					historyNav.Add(e.Url.ToString());
				}
				else if(historyNav[historyNav.Count-(1+historyNavBack)]!=e.Url.ToString()) {//branching from page in history
					historyNav.RemoveRange(historyNav.Count-(1+historyNavBack),historyNavBack);//remove "future" history. branching off in a new direction
					historyNavBack=0;
					historyNav.Add(e.Url.ToString());
				}
			}
		}

		private void butOK_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

	}
}