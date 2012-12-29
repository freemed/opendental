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
		public WikiPage WikiPageCur;		
		private List<string> historyNav;

		public FormWiki() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormWiki_Load(object sender,EventArgs e) {
			//WindowState=FormWindowState.Maximized;
			Rectangle tempWorkAreaRect=System.Windows.Forms.Screen.GetWorkingArea(this);
			Top=0;
			Left=Math.Max(0,(tempWorkAreaRect.Width-960)/2);
			Width=Math.Min(tempWorkAreaRect.Width,960);
			Height=tempWorkAreaRect.Height;
			//Height=SystemInformation.PrimaryMonitorSize.Height;
			LayoutToolBar();
			historyNav=new List<string>();
			LoadWikiPage("Home");//TODO:replace with dynamic PrefC.Getstring(PrefName.WikiHomePage)
		}

		private void LoadWikiPage(string pageTitle) {
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
/* This makes no sense once we have wikipagehist.  Deleted page history will be totally ignored,and will not prevent adding that page again later.
//If trying to navigate to a page that has been deleted, insert a copy of the page into the DB and unflag it as deleted.
if(wpage.IsDeleted) {
	if(!MsgBox.Show(this,MsgBoxButtons.YesNo,"That page has been deleted. Would you like to un-delete it?")) {
		return;
	}
	wpage.UserNum=Security.CurUser.UserNum;
	wpage.IsDeleted=false;
	WikiPages.Insert(wpage);
}*/
			WikiPageCur=wpage;
			webBrowserWiki.DocumentText=WikiPages.TranslateToXhtml(WikiPageCur.PageContent);//does not insert page title.
			Text="Wiki - "+WikiPageCur.PageTitle;
			if(historyNav.Count==0 || historyNav[historyNav.Count-1]!=pageTitle) {
				historyNav.Add(pageTitle);
			}
		}

		private void LayoutToolBar() {
			ToolBarMain.Buttons.Clear();
			ToolBarMain.Buttons.Add(new ODToolBarButton(Lan.g(this,"Back"),-1,"","Back"));
			ToolBarMain.Buttons.Add(new ODToolBarButton(Lan.g(this,"Setup"),-1,Lan.g(this,"Setup master page and styles."),"Setup"));
			ToolBarMain.Buttons.Add(new ODToolBarButton(ODToolBarButtonStyle.Separator));
			ToolBarMain.Buttons.Add(new ODToolBarButton(Lan.g(this,"Home"),-1,"","Home"));
			ToolBarMain.Buttons.Add(new ODToolBarButton(Lan.g(this,"Edit"),-1,"","Edit"));
			ToolBarMain.Buttons.Add(new ODToolBarButton(Lan.g(this,"Rename"),-1,"","Rename"));
			ToolBarMain.Buttons.Add(new ODToolBarButton(Lan.g(this,"Delete"),-1,"","Delete"));
			ToolBarMain.Buttons.Add(new ODToolBarButton(Lan.g(this,"History"),-1,"","History"));
			ToolBarMain.Buttons.Add(new ODToolBarButton(Lan.g(this,"Incoming Links"),-1,"","Inc Links"));
			ToolBarMain.Buttons.Add(new ODToolBarButton(ODToolBarButtonStyle.Separator));
			ToolBarMain.Buttons.Add(new ODToolBarButton(Lan.g(this,"Add"),-1,"","Add"));
			ToolBarMain.Buttons.Add(new ODToolBarButton(Lan.g(this,"All Pages"),-1,"","All Pages"));
			ToolBarMain.Buttons.Add(new ODToolBarButton(Lan.g(this,"Search"),-1,"","Search"));
		}

		private void ToolBarMain_ButtonClick(object sender,OpenDental.UI.ODToolBarButtonClickEventArgs e) {
			switch(e.Button.Tag.ToString()) {
				case "Back":
					Back_Click();
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
			if(historyNav.Count<2) {//should always be 1 or greater
				MsgBox.Show(this,"No more history");
				return;
			}
			historyNav.RemoveAt(historyNav.Count-1);//remove the current page from history
			LoadWikiPage(historyNav[historyNav.Count-1]);
		}

		private void Setup_Click() {
			FormWikiSetup FormWS=new FormWikiSetup();
			FormWS.ShowDialog();
			if(FormWS.DialogResult!=DialogResult.OK) {
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
			WikiPages.Rename(WikiPageCur.PageTitle,FormWR.PageTitle);
			LoadWikiPage(FormWR.PageTitle);
		}

		private void Delete_Click() {
			//MessageBox.Show("not yet functional");
			if(WikiPageCur==null) {
				return;
			}
			if(WikiPageCur.PageTitle=="Home") {//TODO:replace with dynamic PrefC.Getstring(PrefName.WikiHomePage)
				MsgBox.Show(this,"Cannot delete homepage."); 
					return;
			}
			WikiPages.Delete(WikiPageCur.PageTitle);
			LoadWikiPage("Home");//TODO:replace with dynamic PrefC.Getstring(PrefName.WikiHomePage)*/
		}

		private void History_Click() {
			if(WikiPageCur==null) {
				return;
			}
			FormWikiHistory FormWH = new FormWikiHistory();
			FormWH.PageTitleCur=WikiPageCur.PageTitle;
			FormWH.ShowDialog();
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
		}

		private void webBrowserWiki_Navigating(object sender,WebBrowserNavigatingEventArgs e) {
			if(e.Url.ToString().StartsWith("wiki:")) {
				LoadWikiPage(e.Url.ToString().Substring(5));
				e.Cancel=true;
			}
			else if(e.Url.ToString().Contains("http://")){//navigating outside of wiki.
				WikiPageCur=null;
				Text = "Wiki - WWW";
			}
			//WikiPageCur=null;
		}

		//private void menuItemEdit_Click(object sender,EventArgs e) {
		//  if(WikiPageCur==null) {//outside webpage i.e. http://www.opendental.com
		//    return;
		//  }
		//  FormWikiEdit FormWE = new FormWikiEdit();
		//  FormWE.WikiPageCur=WikiPageCur;
		//  FormWE.Show();
		//}

		private void butOK_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

	}
}