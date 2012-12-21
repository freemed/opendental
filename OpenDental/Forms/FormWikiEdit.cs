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
	public partial class FormWikiEdit:Form {
		public WikiPage WikiPageCur;

		public FormWikiEdit() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormWikiEdit_Load(object sender,EventArgs e) {
			/*if(WikiPageCur.IsNew) {
				FormWikiRename FormWR=new FormWikiRename();
				FormWR.ShowDialog();
				if(FormWR.DialogResult!=DialogResult.OK) {
					Close();
				}
				WikiPageCur.PageTitle=FormWR.PageName;
			}*/
			Text = "Wiki Edit - "+WikiPageCur.PageTitle;
			LayoutToolBar();
			//textKeyWords.Text=WikiPageCur.KeyWords;
			textContent.Text=WikiPageCur.PageContent;
		}

		private void LayoutToolBar() {
			ToolBarMain.Buttons.Clear();
			ToolBarMain.Buttons.Add(new ODToolBarButton(Lan.g(this,"Save"),-1,"","Save"));
			ToolBarMain.Buttons.Add(new ODToolBarButton(Lan.g(this,"Cancel"),-1,"","Cancel"));
			ToolBarMain.Buttons.Add(new ODToolBarButton(ODToolBarButtonStyle.Separator));
			ToolBarMain.Buttons.Add(new ODToolBarButton(Lan.g(this,"Cut"),-1,"","Cut"));
			ToolBarMain.Buttons.Add(new ODToolBarButton(Lan.g(this,"Copy"),-1,"","Copy"));
			ToolBarMain.Buttons.Add(new ODToolBarButton(Lan.g(this,"Paste"),-1,"","Paste"));
			ToolBarMain.Buttons.Add(new ODToolBarButton(ODToolBarButtonStyle.Separator));
			ToolBarMain.Buttons.Add(new ODToolBarButton(Lan.g(this,"Int Link"),-1,"","Int Link"));
			ToolBarMain.Buttons.Add(new ODToolBarButton(Lan.g(this,"Ext Link"),-1,"","Ext Link"));
			ToolBarMain.Buttons.Add(new ODToolBarButton(ODToolBarButtonStyle.Separator));
			ToolBarMain.Buttons.Add(new ODToolBarButton(Lan.g(this,"H1"),-1,"","H1"));
			ToolBarMain.Buttons.Add(new ODToolBarButton(Lan.g(this,"H2"),-1,"","H2"));
			ToolBarMain.Buttons.Add(new ODToolBarButton(Lan.g(this,"H3"),-1,"","H3"));
			ToolBarMain.Buttons.Add(new ODToolBarButton(Lan.g(this,"B"),-1,"","Bold"));
			ToolBarMain.Buttons.Add(new ODToolBarButton(Lan.g(this,"I"),-1,"","Italic"));
			ToolBarMain.Buttons.Add(new ODToolBarButton(Lan.g(this,"Color"),-1,"","Color"));
			ToolBarMain.Buttons.Add(new ODToolBarButton(ODToolBarButtonStyle.Separator));
			ToolBarMain.Buttons.Add(new ODToolBarButton(Lan.g(this,"Tab"),-1,"","Tab"));
			ToolBarMain.Buttons.Add(new ODToolBarButton(Lan.g(this,"Table"),-1,"","Table"));
			//ToolBarMain.Buttons.Add(new ODToolBarButton(Lan.g(this,"List"),1,"","ListOrdered"));
			//ToolBarMain.Buttons.Add(new ODToolBarButton(Lan.g(this,"List"),1,"","ListUnordered"));
			ToolBarMain.Buttons.Add(new ODToolBarButton(Lan.g(this,"Image"),1,"","Image"));
			/*
			button.Style=ODToolBarButtonStyle.Label;
			ToolBarMain.Buttons.Add(button);
			ToolBarMain.Buttons.Add(new ODToolBarButton("",2,"Go Forward One Page","Fwd"));
			ToolBarMain.Buttons.Add(new ODToolBarButton(ODToolBarButtonStyle.Separator));*/
		}

		private void ToolBarMain_ButtonClick(object sender,OpenDental.UI.ODToolBarButtonClickEventArgs e) {
			switch(e.Button.Tag.ToString()) {
				case "Save":
					Save_Click();
					break;
				case "Cancel":
					Cancel_Click();
					break;
				case "Cut": 
					Cut_Click(); 
					break;
				case "Copy": 
					Copy_Click(); 
					break;
				case "Paste": 
					Paste_Click(); 
					break;
				case "Int Link": 
					Int_Link_Click(); 
					break;
				case "Ext Link": 
					Ext_Link_Click(); 
					break;
				case "H1": 
					H1_Click(); 
					break;
				case "H2": 
					H2_Click(); 
					break;
				case "H3": 
					H3_Click(); 
					break;
				case "Bold": 
					Bold_Click(); 
					break;
				case "Italic": 
					Italic_Click(); 
					break;
				case "Color": 
					Color_Click(); 
					break;
				case "Tab": 
					Tab_Click(); 
					break;
				case "Table": 
					Table_Click(); 
					break;
				case "List": 
					List_Click();
					break;
				case "Image":
					Image_Click();
					break;
			}
		}

		private void Save_Click() {
			if(!ValidateWikiPage()) {
				return;
			}
			//WikiPageCur.KeyWords=textKeyWords.Text;
			WikiPageCur.PageContent=textContent.Text;
			WikiPageCur.UserNum=Security.CurUser.UserNum;
			WikiPages.Insert(WikiPageCur);
			DialogResult=DialogResult.OK;
		}

		private void Cancel_Click() {
			DialogResult=DialogResult.Cancel;
		}

		private void Cut_Click() {
			textContent.Cut();
			textContent.Focus();
		}

		private void Copy_Click() {
			textContent.Copy();
			textContent.Focus();
		}

		private void Paste_Click() {
			textContent.Paste();
			textContent.Focus();
		}

		private void Int_Link_Click() {
			//throw new NotImplementedException();
		}

		private void Ext_Link_Click() {
			//throw new NotImplementedException();
		}

		private void H1_Click() {
			int tempStart=textContent.SelectionStart;
			int tempLength=textContent.SelectionLength+9;
			textContent.Paste("<h1>"+textContent.SelectedText+"</h1>");
			textContent.Focus();
			textContent.SelectionStart=tempStart;
			textContent.SelectionLength=tempLength;
		}

		private void H2_Click() {
			int tempStart=textContent.SelectionStart;
			int tempLength=textContent.SelectionLength+9;
			textContent.Paste("<h2>"+textContent.SelectedText+"</h2>");
			textContent.Focus();
			textContent.SelectionStart=tempStart;
			textContent.SelectionLength=tempLength;
		}

		private void H3_Click() {
			int tempStart=textContent.SelectionStart;
			int tempLength=textContent.SelectionLength+9;
			textContent.Paste("<h3>"+textContent.SelectedText+"</h3>");
			textContent.Focus();
			textContent.SelectionStart=tempStart;
			textContent.SelectionLength=tempLength;
		}

		private void Bold_Click() {
			int tempStart=textContent.SelectionStart;
			int tempLength=textContent.SelectionLength+7;
			textContent.Paste("<b>"+textContent.SelectedText+"</b>");
			textContent.Focus();
			textContent.SelectionStart=tempStart;
			textContent.SelectionLength=tempLength;
		}

		private void Italic_Click() {
			int tempStart=textContent.SelectionStart;
			int tempLength=textContent.SelectionLength+7;
			textContent.Paste("<i>"+textContent.SelectedText+"</i>");
			textContent.Focus();
			textContent.SelectionStart=tempStart;
			textContent.SelectionLength=tempLength;
		}

		private void Color_Click() {
			//throw new NotImplementedException();
			textContent.Focus();
		}

		private void Tab_Click() {
			Tab();
			textContent.Focus();
			//throw new NotImplementedException();
		}

		private void Table_Click() {
			//throw new NotImplementedException();
		}

		private void List_Click() {
			//throw new NotImplementedException();
		}

		private void Image_Click() {
			//throw new NotImplementedException();
		}

		private void textContent_KeyPress(object sender,KeyPressEventArgs e) {
			switch(e.KeyChar) {
				case (char)Keys.Tab:
					Tab();
					e.Handled=true;
					break;
			}
		}

		private void Tab() {
			textContent.Paste("     ");
		}

		///<summary>Validates content, and keywords. Will return false if PageContent is not XHTML 4.1 compliant.</summary>
		private bool ValidateWikiPage() {
			return true;
			//throw new NotImplementedException();
		}

		private void FormWikiEdit_FormClosing(object sender,FormClosingEventArgs e) {
			//handles both the Cancel button and the user clicking on the x
			if(DialogResult==DialogResult.OK) {
				return;
			}
			if(textContent.Text!=WikiPageCur.PageContent){
				if(MsgBox.Show(this,MsgBoxButtons.YesNo,"Unsaved changes will be lost. Would you like to continue?")) {
					e.Cancel=true;
				}
			}
		}

		
		

	}
}