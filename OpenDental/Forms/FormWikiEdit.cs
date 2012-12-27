using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;
using OpenDental.UI;
using CodeBase;
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
			//xml validation----------------------------------------------------------------------------------------------------
			string s=textContent.Text;
			s=s.Replace("&<","&lt;");
			s=s.Replace("&>","&gt;");
			s="<body>"+s+"</body>";
			XmlDocument doc=new XmlDocument();
			StringReader reader=new StringReader(s);
			try {
				doc.Load(reader);
			}
			catch(Exception ex){
				MessageBox.Show(ex.Message);
				return false;
			}
			try{
				//we do it this way to skip checking the main node itself since it's a dummy node.
				ValidateNodes(doc.DocumentElement.ChildNodes);
			}
			catch(Exception ex){
				MessageBox.Show(ex.Message);
				return false;
			}
			//image validation-----------------------------------------------------------------------------------------------------
			string wikiImagePath=GetWikiPath();//this also creates folder if it's missing.
			MatchCollection matches=Regex.Matches(textContent.Text,@"\[\[(img:).*?\]\]");// [[img:myimage.jpg]]
			if(matches.Count>0 && !PrefC.AtoZfolderUsed) {
				MsgBox.Show(this,"Cannot use images in wiki if storing images in database.");
				return false;
			}
			for(int i=0;i<matches.Count;i++) {
				string imgPath=ODFileUtils.CombinePaths(wikiImagePath,matches[i].Value.Substring(6).Trim(']'));
				if(!System.IO.File.Exists(imgPath)) {
					MessageBox.Show("Not allowed to save because file does not exist: "+imgPath);
					return false;
				}
			}
			//No pipes inside internal links.  This validation will be necessary during our conversion from our old wiki.--------------------------------
//todo
			return true;  
		}

		///<summary>Recursive.</summary>
		private void ValidateNodes(XmlNodeList nodes) {
			foreach(XmlNode node in nodes) {
				if(node.NodeType!=XmlNodeType.Element){
					continue;
				}
				List<string> allowedTags=new List<string>();
				allowedTags.Add("a");
				allowedTags.Add("i");
				allowedTags.Add("b");
				allowedTags.Add("h1");
				allowedTags.Add("h1");
				allowedTags.Add("h3");
				switch(node.Name) {
					case "i":
					case "b":
					case "h1":
					case "h2":
					case "h3":
						//no attributes at all allowed on these tags
						if(node.Attributes.Count!=0) {
							throw new ApplicationException("'"+node.Attributes[0].Name+"' attribute is not allowed on <"+node.Name+"> tag.");
						}
						break;
					case "a":
						//only allowed attribute is href
						for(int i=0;i<node.Attributes.Count;i++) {
							if(node.Attributes[i].Name!="href") {
								throw new ApplicationException(node.Attributes[i].Name+" attribute is not allowed on <a> tag.");
							}
							if(node.Attributes[i].InnerText.StartsWith("wiki:")) {
								throw new ApplicationException("wiki: is not allowed in an <a> tag.  Use [[ ]] instead of <a>.");
							}
						}
						break;
					default:
						throw new ApplicationException("<"+node.Name+"> is not one of the allowed tags.");
				}
				ValidateNodes(node.ChildNodes);
			}
		}

		///<summary>Typically returns something similar to \\SERVER\OpenDentImages\Wiki</summary>
		public static string GetWikiPath() {
			string wikiPath;
			if(!PrefC.AtoZfolderUsed) {
				throw new ApplicationException("Must be using AtoZ folders.");
			}
			wikiPath=ODFileUtils.CombinePaths(ImageStore.GetPreferredAtoZpath(),"Wiki");
			if(!Directory.Exists(wikiPath)) {
				Directory.CreateDirectory(wikiPath);
			}
			return wikiPath;
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