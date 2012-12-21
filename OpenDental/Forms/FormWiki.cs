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
		public WikiPage PageMaster;
		public WikiPage PageStyle;
		private string AggregateContent;

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
			PageMaster=WikiPages.GetMaster();
			PageStyle=WikiPages.GetStyle();
			LoadWikiPage("Home");
		}

		private void LoadWikiPage(string PageTitle) {
			if(WikiPages.GetByName(PageTitle)==null) {
				if(!MsgBox.Show(this,MsgBoxButtons.YesNo,"That page does not exist. Would you like to create it?")) {
					return;
				}
				FormWikiEdit FormWE=new FormWikiEdit();
				FormWE.WikiPageCur=new WikiPage();
				FormWE.WikiPageCur.IsNew=true;
				FormWE.WikiPageCur.PageTitle=PageTitle;
				FormWE.ShowDialog();
				if(FormWE.DialogResult!=DialogResult.OK) {
					return;
				}
			}
			WikiPageCur=WikiPages.GetByName(PageTitle);
			//WikiPageCur=WikiPages.GetByName(PageTitle);
			//webBrowserWiki.DocumentText=WikiPages.TranslateToXhtml(PageMaster.PageContent
			//.Replace("@@@Title@@@",WikiPageCur.PageTitle).Replace("@@@Style@@@",PageStyle.PageContent).Replace("@@@Content@@@",WikiPageCur.PageContent));
			//WikiPageCur=WikiPages.GetByName(PageTitle);
			AggregateContent=PageMaster.PageContent;
			AggregateContent=AggregateContent.Replace("@@@Title@@@",WikiPageCur.PageTitle);
			AggregateContent=AggregateContent.Replace("@@@Style@@@",PageStyle.PageContent);
			AggregateContent=AggregateContent.Replace("@@@Content@@@",WikiPageCur.PageContent.Clone().ToString());
			webBrowserWiki.DocumentText=WikiPages.TranslateToXhtml(AggregateContent);//WikiPageCur is mysteriously set to null after this line.
			//WikiPageCur=WikiPages.GetByName(PageTitle);
			Text="OD Wiki - "+WikiPageCur.PageTitle;
		}

		private void LayoutToolBar() {
			ToolBarMain.Buttons.Clear();
			ToolBarMain.Buttons.Add(new ODToolBarButton(Lan.g(this,"Setup"),-1,Lan.g(this,"Setup master page and styles."),"Setup"));
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
			//ReloadPage.
		}

		private void Rename_Click() {
			FormWikiRename FormWR=new FormWikiRename();
			FormWR.ShowDialog();
		}

		private void Delete_Click() {
			//Rename page to "_deleted_<pageTitle>"?
			//Or do we want to have a new column IsDeleted?
			//throw new NotImplementedException();
		}

		private void History_Click() {
			//FormWikiHistory
			//throw new NotImplementedException();
		}

		private void Inc_Link_Click() {
			//throw new NotImplementedException();
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
			FormWE.WikiPageCur.PageTitle=FormWR.PageName;
			FormWE.ShowDialog();
			//if dialog OK:
			//ReloadPage.
		}

		private void All_Pages_Click() {
			FormWikiAllPages FormWAP=new FormWikiAllPages();
			FormWAP.ShowDialog();
		}

		private void Search_Click() {
			FormWikiSearch FormWS=new FormWikiSearch();
			FormWS.ShowDialog();
		}

		private void webBrowserWiki_Navigating(object sender,WebBrowserNavigatingEventArgs e) {
			if(e.Url.ToString().Contains("wiki:")) {
				LoadWikiPage(e.Url.ToString().Substring(5));
				e.Cancel=true;//comment out if it doesn't work.
			}
			else if(e.Url.ToString().Contains("http://")){//navigating outside of wiki.
				WikiPageCur=null;
				Text = "OD Wiki - WWW";
			}
			//WikiPageCur=null;
		}

		private void menuItemSetup_Click(object sender,EventArgs e) {

		}

		private void menuItemEdit_Click(object sender,EventArgs e) {
			if(WikiPageCur==null) {//outside webpage i.e. http://www.opendental.com
				return;
			}
			FormWikiEdit FormWE = new FormWikiEdit();
			FormWE.WikiPageCur=WikiPageCur;
			FormWE.Show();
		}

		private void butOK_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

	}
}