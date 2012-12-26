using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;
using OpenDental.UI;
using System.Xml;
using System.Text.RegularExpressions;

namespace OpenDental {
	public partial class FormWikiEdit:Form {
		public WikiPage WikiPageCur;
		private string AggregateContent;

		public FormWikiEdit() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormWikiEdit_Load(object sender,EventArgs e) {
			ResizeControls();
			LayoutToolBar();
			LayoutToolBar();
			Text = "Wiki Edit - "+WikiPageCur.PageTitle;
			textContent.Text=WikiPageCur.PageContent;
			textContent.SelectionStart=textContent.TextLength;
			textContent.SelectionLength=0;
			LoadWikiPage();
		}

		private void LoadWikiPage() {
			webBrowserWiki.DocumentText=WikiPages.TranslateToXhtml(textContent.Text);
		}

		private void ResizeControls() {
			int topborder=30;
			//text resize
			textContent.Top=topborder;
			textContent.Height=ClientSize.Height-topborder;
			textContent.Left=0;
			textContent.Width=ClientSize.Width/2-2;
			//Browser resize
			webBrowserWiki.Top=topborder;
			webBrowserWiki.Height=ClientSize.Height-topborder;
			webBrowserWiki.Left=ClientSize.Width/2+2;
			webBrowserWiki.Width=ClientSize.Width/2-2;
			//Toolbar resize
			ToolBarMain.Width=ClientSize.Width/2-2;
			//Button move
			butRefresh.Left=ClientSize.Width/2+2;
		}

		private void FormWikiEdit_SizeChanged(object sender,EventArgs e) {
			ResizeControls();
		}

		private void butRefresh_Click(object sender,EventArgs e) {
			if(!ValidateWikiPage()) {
				return;
			}
				webBrowserWiki.AllowNavigation=true;
			LoadWikiPage();
		}

		private void webBrowserWiki_Navigated(object sender,WebBrowserNavigatedEventArgs e) {
			webBrowserWiki.AllowNavigation=false;
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
			ToolBarMain.Buttons.Add(new ODToolBarButton(Lan.g(this,"Table"),-1,"","Table"));
			ToolBarMain.Buttons.Add(new ODToolBarButton(Lan.g(this,"Image"),1,"","Image"));
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
				case "Table": 
					Table_Click(); 
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
			int tempStart=textContent.SelectionStart;
			FormWikiAllPages FormWAPSelect = new FormWikiAllPages();
			FormWAPSelect.IsSelectMode=true;
			FormWAPSelect.ShowDialog();
			if(FormWAPSelect.DialogResult==DialogResult.OK) {
				textContent.Paste("[["+FormWAPSelect.SelectedWikiPage.PageTitle+"]]");
				textContent.SelectionStart=tempStart+FormWAPSelect.SelectedWikiPage.PageTitle.Length+4;
			}
			else {
				textContent.Paste("[[]]");
				textContent.SelectionStart=tempStart+2;
			}
			textContent.Focus();
			textContent.SelectionLength=0;

		}

		private void Ext_Link_Click() {
			int tempStart=textContent.SelectionStart;
			textContent.Paste("<a href=\"\"></a>");
			textContent.Focus();
			textContent.SelectionStart=tempStart+11;
			textContent.SelectionLength=0;
		}

		private void H1_Click() {
			int tempStart=textContent.SelectionStart;
			int tempLength=textContent.SelectionLength;
			textContent.Paste("<h1>"+textContent.SelectedText+"</h1>");
			textContent.Focus();
			if(tempLength==0) {//nothing selected, place cursor in middle of new tags
				textContent.SelectionStart=tempStart+4;
			}
			else {
			textContent.SelectionStart=tempStart;
			textContent.SelectionLength=tempLength+9;
			}
		}

		private void H2_Click() {
			int tempStart=textContent.SelectionStart;
			int tempLength=textContent.SelectionLength;
			textContent.Paste("<h2>"+textContent.SelectedText+"</h2>");
			textContent.Focus();
			if(tempLength==0) {//nothing selected, place cursor in middle of new tags
				textContent.SelectionStart=tempStart+4;
			}
			else {
				textContent.SelectionStart=tempStart;
				textContent.SelectionLength=tempLength+9;
			}
		}

		private void H3_Click() {
			int tempStart=textContent.SelectionStart;
			int tempLength=textContent.SelectionLength;
			textContent.Paste("<h3>"+textContent.SelectedText+"</h3>");
			textContent.Focus();
			if(tempLength==0) {//nothing selected, place cursor in middle of new tags
				textContent.SelectionStart=tempStart+4;
			}
			else {
				textContent.SelectionStart=tempStart;
				textContent.SelectionLength=tempLength+9;
			}
		}

		private void Bold_Click() {
			int tempStart=textContent.SelectionStart;
			int tempLength=textContent.SelectionLength;
			textContent.Paste("<b>"+textContent.SelectedText+"</b>");
			textContent.Focus();
			if(tempLength==0) {//nothing selected, place cursor in middle of new tags
				textContent.SelectionStart=tempStart+3;
			}
			else {
				textContent.SelectionStart=tempStart;
				textContent.SelectionLength=tempLength+7;
			}
		}

		private void Italic_Click() {
			int tempStart=textContent.SelectionStart;
			int tempLength=textContent.SelectionLength;
			textContent.Paste("<i>"+textContent.SelectedText+"</i>");
			textContent.Focus();
			if(tempLength==0) {//nothing selected, place cursor in middle of new tags
				textContent.SelectionStart=tempStart+3;
			}
			else {
				textContent.SelectionStart=tempStart;
				textContent.SelectionLength=tempLength+7;
			}
		}

		private void Color_Click() {
			int tempStart=textContent.SelectionStart;
			int tempLength=textContent.SelectionLength;
			textContent.Paste("{{color|red|"+textContent.SelectedText+"}}");//(tempLength>0?textContent.SelectedText:"")+"}}");
			textContent.Focus();
			textContent.SelectionStart=tempStart+tempLength+12;
			textContent.SelectionLength=0;
			//if(tempLength==0) {//nothing selected, place cursor in middle of new tags
			//  textContent.SelectionStart=tempStart+12;
			//  textContent.SelectionLength=0;
			//}
			//else {
			//  textContent.SelectionStart=tempStart+tempLength-3;
			//  textContent.SelectionLength=0;
			//}
			textContent.Focus();
		}

		///<summary>Insert table boilderplate. Pastes selected text into the first cell.</summary>
		private void Table_Click() {
			int tempStart=textContent.SelectionStart;
			int tempLength=textContent.SelectionLength;
			textContent.Paste(
@"<table>
  <tr>
    <th>Header1</th>
    <th>Header2</th>
  </tr>
  <tr>
    <td>"+textContent.SelectedText+@"</td>
    <td></td>
  </tr>
  <tr>
    <td></td>
    <td></td>
  </tr>
  <tr>
    <td></td>
    <td></td>
  </tr>
</table>");
			textContent.SelectionStart=tempStart+86;
			textContent.SelectionLength=tempLength;
			textContent.Focus();
		}

		private void Image_Click() {
			//todo
		}

		private void textContent_KeyPress(object sender,KeyPressEventArgs e) {
			switch(e.KeyChar) {
				case (char)Keys.Tab:
					textContent.Paste("     ");
					//Tab();
					e.Handled=true;
					break;
			}
		}

		///<summary>Validates content, and keywords.</summary>
		private bool ValidateWikiPage() {
			return true;
			string valText=textContent.Text;
			valText=valText.Replace("&<","&lt;").Replace("&>","&gt;");
			//TODO:XML Validator...
			//Allowed Tags: a, i, b, h1, h2, h3
			//common dissalowed tags: div, span, p, body, h4, h5, etc...
			string rootImagePath = "\\Ryan\\storage\\images\\";
			MatchCollection matches = Regex.Matches(valText,"\\[\\[(img:).*?\\]\\]");
			for(int i=0;i<matches.Count;i++) {
				if(!System.IO.File.Exists(rootImagePath+matches[i].Value.Substring(6).Trim(']'))) {
					//file doesn't exist. Throw error?
					return false;
				}
			}
			//foreach(Match imgTag in matches) {
			//  if(!System.IO.File.Exists(rootImagePath+imgTag.Value.Substring(6).Trim(']'))){
			//    //file doesn't exist. Throw error?
			//    return false;
			//  }
			//}
			return true;
			//todo
			//strategy:
			//validate xml portions
			//   convert &< and &> to &lt; and &gt;  (this change won't be stored in db, but is just for this loop)
			//   Run result through a C# XML validator. 
			//   Make a list of allowed tags here.  No tags allowed unless they are on this list.
			//   Check for disallowed attributes on each tag.
			//   Do not allow "wiki:" inside of an <a> tag
			//other validation
			//   Do not allow pipes inside internal links. This validation will be necessary during our conversion from our old wiki.
			//   Each image that is referred to must exist before page can close
			//   
		}

		private void FormWikiEdit_FormClosing(object sender,FormClosingEventArgs e) {
			//handles both the Cancel button and the user clicking on the x
			if(DialogResult==DialogResult.OK) {
				return;
			}
			if(textContent.Text!=WikiPageCur.PageContent){
				if(!MsgBox.Show(this,MsgBoxButtons.YesNo,"Unsaved changes will be lost. Would you like to continue?")) {
					e.Cancel=true;
				}
			}
		}

		
		

	}
}