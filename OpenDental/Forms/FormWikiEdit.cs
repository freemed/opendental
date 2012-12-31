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
			//LayoutToolBar();
			Text = "Wiki Edit - "+WikiPageCur.PageTitle;
			textContent.Text=WikiPageCur.PageContent;
			textContent.SelectionStart=textContent.Text.Length;
			textContent.SelectionLength=0;
			string[] strArray=new string[1];
			strArray[0]="\r\n";
			int rowCount=textContent.Text.Split(strArray,StringSplitOptions.None).Length;
			FillNumbers(rowCount);
			LoadWikiPage();
		}

		private void FillNumbers(int rowCount) {
			StringBuilder strb=new StringBuilder();
			for(int i=0;i<rowCount+10;i++) {
				strb.Append(i.ToString());
				strb.Append("\r\n");
			}
			textNumbers.Text=strb.ToString();
		}

		private void LoadWikiPage() {
			webBrowserWiki.DocumentText=WikiPages.TranslateToXhtml(textContent.Text);
		}

		private void ResizeControls() {
			int topborder=30;
			//textNumbers resize
			textNumbers.Height=ClientSize.Height-topborder;
			//text resize
			textContent.Top=topborder;
			textContent.Height=ClientSize.Height-topborder;
			textContent.Left=28;
			textContent.Width=ClientSize.Width/2-2-textContent.Left;
			//Browser resize
			webBrowserWiki.Top=topborder;
			webBrowserWiki.Height=ClientSize.Height-topborder;
			webBrowserWiki.Left=ClientSize.Width/2+2;
			webBrowserWiki.Width=ClientSize.Width/2-2;
			//Toolbar resize
			ToolBarMain.Width=butRefresh.Left-ToolBarMain.Left-5;//ClientSize.Width/2-2;
			LayoutToolBar();
			//Button move
			//butRefresh.Left=ClientSize.Width/2+2;
		}

		private void FormWikiEdit_SizeChanged(object sender,EventArgs e) {
			ResizeControls();
		}

		private void butRefresh_Click(object sender,EventArgs e) {
			if(!ValidateWikiPage(false)) {
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
			ToolBarMain.Buttons.Add(new ODToolBarButton(Lan.g(this,"Save"),0,"","Save"));
			ToolBarMain.Buttons.Add(new ODToolBarButton(Lan.g(this,"Cancel"),1,"","Cancel"));
			ToolBarMain.Buttons.Add(new ODToolBarButton(ODToolBarButtonStyle.Separator));
			ToolBarMain.Buttons.Add(new ODToolBarButton(Lan.g(this,"Cut"),2,"","Cut"));
			ToolBarMain.Buttons.Add(new ODToolBarButton(Lan.g(this,"Copy"),3,"","Copy"));
			ToolBarMain.Buttons.Add(new ODToolBarButton(Lan.g(this,"Paste"),4,"","Paste"));
			ToolBarMain.Buttons.Add(new ODToolBarButton(Lan.g(this,"Undo"),5,"","Undo"));
			ToolBarMain.Buttons.Add(new ODToolBarButton(ODToolBarButtonStyle.Separator));
			ToolBarMain.Buttons.Add(new ODToolBarButton(Lan.g(this,"Int Link"),6,"","Int Link"));
			ToolBarMain.Buttons.Add(new ODToolBarButton(Lan.g(this,"Ext Link"),7,"","Ext Link"));
			ToolBarMain.Buttons.Add(new ODToolBarButton(ODToolBarButtonStyle.Separator));
			ToolBarMain.Buttons.Add(new ODToolBarButton(Lan.g(this,"Heading1"),8,"","H1"));
			ToolBarMain.Buttons.Add(new ODToolBarButton(Lan.g(this,"Heading2"),9,"","H2"));
			ToolBarMain.Buttons.Add(new ODToolBarButton(Lan.g(this,"Heading3"),10,"","H3"));
			ToolBarMain.Buttons.Add(new ODToolBarButton(Lan.g(this,"Bold"),11,"","Bold"));
			ToolBarMain.Buttons.Add(new ODToolBarButton(Lan.g(this,"Italic"),12,"","Italic"));
			ToolBarMain.Buttons.Add(new ODToolBarButton(Lan.g(this,"Color"),13,"","Color"));
			ToolBarMain.Buttons.Add(new ODToolBarButton(ODToolBarButtonStyle.Separator));
			ToolBarMain.Buttons.Add(new ODToolBarButton(Lan.g(this,"Table"),14,"","Table"));
			ToolBarMain.Buttons.Add(new ODToolBarButton(Lan.g(this,"Image"),15,"","Image"));
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
				case "Undo":
					Undo_Click();
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

		private void menuItemCut_Click(object sender,EventArgs e) {
			Cut_Click(); 
		}

		private void menuItemCopy_Click(object sender,EventArgs e) {
			Copy_Click(); 
		}

		private void menuItemPaste_Click(object sender,EventArgs e) {
			Paste_Click();
		}

		private void menuItemUndo_Click(object sender,EventArgs e) {
			Undo_Click();
		}

		private void Save_Click() {
			if(!ValidateWikiPage(true)) {
				return;
			}
			//WikiPageCur.KeyWords=textKeyWords.Text;
			WikiPageCur.PageContent=textContent.Text;
			WikiPageCur.UserNum=Security.CurUser.UserNum;
			WikiPages.InsertAndArchive(WikiPageCur);
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

		private void Undo_Click() {
			textContent.Undo();
			textContent.Focus();
		}

		private void Int_Link_Click() {
			int tempStart=textContent.SelectionStart;
			FormWikiAllPages FormWAPSelect = new FormWikiAllPages();
			FormWAPSelect.IsSelectMode=true;
			FormWAPSelect.ShowDialog();
			if(FormWAPSelect.DialogResult==DialogResult.OK) {
				if(FormWAPSelect.SelectedWikiPage==null) {
					textContent.Paste("[[]]");
					textContent.SelectionStart=tempStart+2;
				}
				else {
					textContent.Paste(("[["+FormWAPSelect.SelectedWikiPage.PageTitle+"]]"));
					textContent.SelectionStart=tempStart+FormWAPSelect.SelectedWikiPage.PageTitle.Length+4;
				}
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
				//.Text=textContent.Text.Substring(0,tempStart)+"<b>"+textContent.SelectedText+"</b>"+textContent.Text.Substring(tempStart+tempLength);
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

		///<summary>Validates content, and keywords.  isForSaving can be false if just validating for refresh.</summary>
		private bool ValidateWikiPage(bool isForSaving) {
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
			string wikiImagePath=WikiPages.GetWikiPath();//this also creates folder if it's missing.
			MatchCollection matches=Regex.Matches(textContent.Text,@"\[\[(img:).*?\]\]");// [[img:myimage.jpg]]
			if(matches.Count>0 && !PrefC.AtoZfolderUsed) {
				MsgBox.Show(this,"Cannot use images in wiki if storing images in database.");
				return false;
			}
			if(isForSaving) {
				for(int i=0;i<matches.Count;i++) {
					string imgPath=ODFileUtils.CombinePaths(wikiImagePath,matches[i].Value.Substring(6).Trim(']'));
					if(!System.IO.File.Exists(imgPath)) {
						MessageBox.Show("Not allowed to save because file does not exist: "+imgPath);
						return false;
					}
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
				switch(node.Name) {
					case "i":
					case "b":
					case "h1":
					case "h2":
					case "h3":
					case "table":
					case "tr":
					case "td":
					case "th":
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