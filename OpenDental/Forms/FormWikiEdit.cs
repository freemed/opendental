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
		private string clipboard;

		public FormWikiEdit() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormWikiEdit_Load(object sender,EventArgs e) {
			LayoutToolBar();
			textKeyWords.Text=WikiPageCur.KeyWords;
			textContent.Text=WikiPageCur.PageContent;
		}

		private void LayoutToolBar() {
			ToolBarMain.Buttons.Clear();
			ToolBarMain.Buttons.Add(new ODToolBarButton(Lan.g(this,"Save"),-1,"","Save"));
			ToolBarMain.Buttons.Add(new ODToolBarButton(ODToolBarButtonStyle.Separator));
			ToolBarMain.Buttons.Add(new ODToolBarButton(Lan.g(this,"Cut"),-1,Lan.g(this,""),"Cut"));
			ToolBarMain.Buttons.Add(new ODToolBarButton(Lan.g(this,"Copy"),-1,Lan.g(this,""),"Copy"));
			ToolBarMain.Buttons.Add(new ODToolBarButton(Lan.g(this,"Paste"),-1,Lan.g(this,""),"Paste"));
			ToolBarMain.Buttons.Add(new ODToolBarButton(ODToolBarButtonStyle.Separator));
			ToolBarMain.Buttons.Add(new ODToolBarButton(Lan.g(this,"Int Link"),-1,Lan.g(this,""),"Int Link"));
			ToolBarMain.Buttons.Add(new ODToolBarButton(Lan.g(this,"Ext Link"),-1,Lan.g(this,""),"Ext Link"));
			ToolBarMain.Buttons.Add(new ODToolBarButton(ODToolBarButtonStyle.Separator));
			ToolBarMain.Buttons.Add(new ODToolBarButton(Lan.g(this,"H1"),-1,Lan.g(this,""),"H1"));
			ToolBarMain.Buttons.Add(new ODToolBarButton(Lan.g(this,"H2"),-1,Lan.g(this,""),"H2"));
			ToolBarMain.Buttons.Add(new ODToolBarButton(Lan.g(this,"H3"),-1,Lan.g(this,""),"H3"));
			ToolBarMain.Buttons.Add(new ODToolBarButton(Lan.g(this,"B"),-1,Lan.g(this,""),"Bold"));
			ToolBarMain.Buttons.Add(new ODToolBarButton(Lan.g(this,"I"),-1,Lan.g(this,""),"Italic"));
			ToolBarMain.Buttons.Add(new ODToolBarButton(Lan.g(this,"Color"),-1,Lan.g(this,""),"Color"));
			ToolBarMain.Buttons.Add(new ODToolBarButton(ODToolBarButtonStyle.Separator));
			ToolBarMain.Buttons.Add(new ODToolBarButton(Lan.g(this,"Tab"),-1,Lan.g(this,""),"Tab"));
			ToolBarMain.Buttons.Add(new ODToolBarButton(Lan.g(this,"Table"),-1,Lan.g(this,""),"Table"));
			//ToolBarMain.Buttons.Add(new ODToolBarButton(Lan.g(this,"List"),1,Lan.g(this,""),"List"));
			//ToolBarMain.Buttons.Add(new ODToolBarButton(Lan.g(this,"List"),1,Lan.g(this,""),"List"));
			ToolBarMain.Buttons.Add(new ODToolBarButton(Lan.g(this,"Image"),1,Lan.g(this,""),"Image"));
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
			ValidateWikiPage();
			SaveToDB();
		}

		private void Cut_Click() {
			textContent.Cut();
		}

		private void Copy_Click() {
			textContent.Copy();
		}

		private void Paste_Click() {
			textContent.Paste();
		}

		private void Int_Link_Click() {
			throw new NotImplementedException();
		}

		private void Ext_Link_Click() {
			throw new NotImplementedException();
		}

		private void H1_Click() {
			throw new NotImplementedException();
		}

		private void H2_Click() {
			throw new NotImplementedException();
		}

		private void H3_Click() {
			throw new NotImplementedException();
		}

		private void Bold_Click() {
			throw new NotImplementedException();
		}

		private void Italic_Click() {
			throw new NotImplementedException();
		}

		private void Color_Click() {
			throw new NotImplementedException();
		}

		private void Tab_Click() {
			throw new NotImplementedException();
		}

		private void Table_Click() {
			throw new NotImplementedException();
		}

		private void List_Click() {
			throw new NotImplementedException();
		}

		private void Image_Click() {
			throw new NotImplementedException();
		}

		///<summary>Validates title, content, and keywords. Will return false if PageContent is not XHTML 4.1 compliant.</summary>
		private bool ValidateWikiPage() {
			return true;
			//throw new NotImplementedException();
		}

		private void SaveToDB() {
			//validation not handled here, handled in ValidatePageContent
			WikiPageCur.KeyWords=textKeyWords.Text;
			WikiPageCur.PageContent=textContent.Text;
			//PageCur.UserNum=//TODO:UserCur.UserNum;
			if(WikiPageCur.IsNew) {
				WikiPages.Insert(WikiPageCur);
			}
			else {
				WikiPages.Update(WikiPageCur);
			}
			//throw new NotImplementedException();
		}

		private void butOK_Click(object sender,EventArgs e) {
			if(textContent.Text!=WikiPageCur.PageContent 
				||	WikiPageCur.KeyWords!=textKeyWords.Text 
				||	WikiPageCur.PageContent!=textContent.Text) {
				if(!MsgBox.Show(this,MsgBoxButtons.YesNo,"Would you like to save changes to the current page?")) {
					if(ValidateWikiPage()) {
						SaveToDB();
					}
					else {
						return;//not valid
					}
				}
				//did not want to save changes.
			}
			//No changes detected.
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}
	}
}