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
			this.textContent.TextChanged += new System.EventHandler(this.textContent_TextChanged);
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
			//RefreshHtml();
			textContent.Focus();
		}

		private void FillNumbers(int rowCount) {
			StringBuilder strb=new StringBuilder();
			for(int i=1;i<rowCount+10;i++) {
				strb.Append(i.ToString());
				strb.Append("\r\n");
			}
			textNumbers.Text=strb.ToString();
		}

		private void RefreshHtml() {
			webBrowserWiki.AllowNavigation=true;
			try {
				webBrowserWiki.DocumentText=WikiPages.TranslateToXhtml(textContent.Text,true);
			}
			catch(Exception ex) {
				//don't refresh
			}
			textContent.Focus();
		}

		private void ResizeControls() {
			int topborder=53;
			//textNumbers resize
			textNumbers.Top=topborder;
			textNumbers.Height=ClientSize.Height-topborder;
			//text resize
			textContent.Top=topborder;
			textContent.Height=ClientSize.Height-topborder;
			textContent.Left=32;
			textContent.Width=ClientSize.Width/2-2-textContent.Left;
			//Browser resize
			webBrowserWiki.Top=topborder;
			webBrowserWiki.Height=ClientSize.Height-topborder;
			webBrowserWiki.Left=ClientSize.Width/2+2;
			webBrowserWiki.Width=ClientSize.Width/2-2;
			//Toolbar resize
			ToolBarMain.Width=ClientSize.Width;
			LayoutToolBars();
			//Button move
			//butRefresh.Left=ClientSize.Width/2+2;
		}

		private void FormWikiEdit_SizeChanged(object sender,EventArgs e) {
			ResizeControls();
		}

		private void textContent_TextChanged(object sender,EventArgs e) {
			RefreshHtml();
		}

		private void webBrowserWiki_Navigated(object sender,WebBrowserNavigatedEventArgs e) {
			webBrowserWiki.AllowNavigation=false;
		}

		private void LayoutToolBars() {
			ToolBarMain.Buttons.Clear();
			//Refresh no longer needed.
			ToolBarMain.Buttons.Add(new ODToolBarButton(Lan.g(this,"Save"),1,"","Save"));
			ToolBarMain.Buttons.Add(new ODToolBarButton(Lan.g(this,"Cancel"),2,"","Cancel"));
			ToolBarMain.Buttons.Add(new ODToolBarButton(ODToolBarButtonStyle.Separator));
			ToolBarMain.Buttons.Add(new ODToolBarButton(Lan.g(this,"Int Link"),7,"","Int Link"));
			ToolBarMain.Buttons.Add(new ODToolBarButton(Lan.g(this,"File"),7,"","File Link"));
			ToolBarMain.Buttons.Add(new ODToolBarButton(Lan.g(this,"Folder"),7,"","Folder Link"));
			ToolBarMain.Buttons.Add(new ODToolBarButton(Lan.g(this,"Ext Link"),8,"","Ext Link"));
			ToolBarMain.Buttons.Add(new ODToolBarButton(ODToolBarButtonStyle.Separator));
			ToolBarMain.Buttons.Add(new ODToolBarButton(Lan.g(this,"Heading1"),9,"","H1"));
			ToolBarMain.Buttons.Add(new ODToolBarButton(Lan.g(this,"Heading2"),10,"","H2"));
			ToolBarMain.Buttons.Add(new ODToolBarButton(Lan.g(this,"Heading3"),11,"","H3"));
			ToolBarMain.Buttons.Add(new ODToolBarButton(ODToolBarButtonStyle.Separator));
			ToolBarMain.Buttons.Add(new ODToolBarButton(Lan.g(this,"Table"),15,"","Table"));
			ToolBarMain.Buttons.Add(new ODToolBarButton(Lan.g(this,"Image"),16,"","Image"));
			toolBar2.Buttons.Clear();
			toolBar2.Buttons.Add(new ODToolBarButton(Lan.g(this,"Cut"),3,"","Cut"));
			toolBar2.Buttons.Add(new ODToolBarButton(Lan.g(this,"Copy"),4,"","Copy"));
			toolBar2.Buttons.Add(new ODToolBarButton(Lan.g(this,"Paste"),5,"","Paste"));
			toolBar2.Buttons.Add(new ODToolBarButton(ODToolBarButtonStyle.Separator));
			toolBar2.Buttons.Add(new ODToolBarButton(Lan.g(this,"Undo"),6,"","Undo"));
			toolBar2.Buttons.Add(new ODToolBarButton(ODToolBarButtonStyle.Separator));
			toolBar2.Buttons.Add(new ODToolBarButton(Lan.g(this,"Bold"),12,"","Bold"));
			toolBar2.Buttons.Add(new ODToolBarButton(Lan.g(this,"Italic"),13,"","Italic"));
			toolBar2.Buttons.Add(new ODToolBarButton(Lan.g(this,"Color"),14,"","Color"));
		}

		private void ToolBarMain_ButtonClick(object sender,OpenDental.UI.ODToolBarButtonClickEventArgs e) {
			switch(e.Button.Tag.ToString()) {
				case "Save":
					Save_Click();
					break;
				case "Cancel":
					Cancel_Click();
					break;
				case "Int Link": 
					Int_Link_Click(); 
					break;
				case "File Link":
					File_Link_Click();
					break;
				case "Folder Link":
					Folder_Link_Click();
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
				case "Table": 
					Table_Click(); 
					break;
				case "Image":
					Image_Click();
					break;
			}
		}

		private void toolBar2_ButtonClick(object sender,ODToolBarButtonClickEventArgs e) {
			switch(e.Button.Tag.ToString()) {
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
				case "Bold":
					Bold_Click();
					break;
				case "Italic":
					Italic_Click();
					break;
				case "Color":
					Color_Click();
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
			WikiPageCur.PageContent=textContent.Text;
			//Fix case on all internal links
			MatchCollection matches=Regex.Matches(WikiPageCur.PageContent,@"\[\[.+?\]\]");
			foreach(Match match in matches) {
				if(match.Value.StartsWith("[[img:")
					|| match.Value.StartsWith("[[keywords:")
					|| match.Value.StartsWith("[[file:")
					|| match.Value.StartsWith("[[folder:")
					|| match.Value.StartsWith("[[color:")) 
				{
					continue;//we don't care about these.  We are only checking internal links
				}
				//Get the pagename of the link
				string oldTitle=match.Value.Substring(2,match.Value.Length-4);
				string newTitle=WikiPages.GetTitle(oldTitle);
				if(oldTitle==newTitle) {//casing matches
					continue;
				}
				if(newTitle=="") {//broken link, leave alone
					continue;
				}
				WikiPageCur.PageContent=WikiPageCur.PageContent.Replace("[["+oldTitle+"]]","[["+newTitle+"]]");
			}
			WikiPageCur.UserNum=Security.CurUser.UserNum;
			Regex regex=new Regex(@"\[\[(keywords:).+?\]\]");//only grab first match
			Match m=regex.Match(textContent.Text);
			WikiPageCur.KeyWords=m.Value.Replace("[[keywords:","").TrimEnd(']');//will be empty string if no match
			WikiPages.InsertAndArchive(WikiPageCur);
			DialogResult=DialogResult.OK;
		}

		private void Cancel_Click() {
			DialogResult=DialogResult.Cancel;
		}

		private void Cut_Click() {
			textContent.Cut();
			textContent.Focus();
			//RefreshHtml();
		}

		private void Copy_Click() {
			textContent.Copy();
			textContent.Focus();
			//RefreshHtml();
		}

		private void Paste_Click() {
			textContent.Paste();
			textContent.Focus();
			//RefreshHtml();
		}

		private void Undo_Click() {
			textContent.Undo();
			textContent.Focus();
			//RefreshHtml();
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
			//RefreshHtml();
		}

		private void File_Link_Click() {
			FormWikiFileFolder formWFF=new FormWikiFileFolder();
			formWFF.ShowDialog();
			if(formWFF.DialogResult!=DialogResult.OK) {
				return;
			}
			textContent.Paste("[[file:"+formWFF.SelectedLink+"]]");
			//RefreshHtml();
		}

		private void Folder_Link_Click() {
			FormWikiFileFolder formWFF=new FormWikiFileFolder();
			formWFF.IsFolderMode=true;
			formWFF.ShowDialog();
			if(formWFF.DialogResult!=DialogResult.OK) {
				return;
			}
			textContent.Paste("[[folder:"+formWFF.SelectedLink+"]]");
			//RefreshHtml();
		}

		private void Ext_Link_Click() {
			FormWikiExternalLink FormWEL=new FormWikiExternalLink();
			FormWEL.ShowDialog();
			int tempStart=textContent.SelectionStart;
			if(FormWEL.DialogResult!=DialogResult.OK) {
				textContent.Paste("<a href=\"\"></a>");
				textContent.SelectionStart=tempStart+11;
				textContent.SelectionLength=0;
				return;
			}
			textContent.Paste("<a href=\""+FormWEL.URL+"\">"+FormWEL.DisplayText+"</a>");
			textContent.Focus();
			//RefreshHtml();
		}

		private void H1_Click() {
			int tempStart=textContent.SelectionStart;
			int tempLength=textContent.SelectionLength;
			string s="<h1>"+textContent.SelectedText+"</h1>";
			textContent.Paste(s);
			textContent.Focus();
			if(tempLength==0) {//nothing selected, place cursor in middle of new tags
				textContent.SelectionStart=tempStart+4+tempLength;
			}
			else {
				textContent.SelectionStart=tempStart+s.Length;
				textContent.SelectionLength=0;
			}
			//RefreshHtml();
		}

		private void H2_Click() {
			int tempStart=textContent.SelectionStart;
			int tempLength=textContent.SelectionLength;
			string s="<h2>"+textContent.SelectedText+"</h2>";
			textContent.Paste(s);
			textContent.Focus();
			if(tempLength==0) {//nothing selected, place cursor in middle of new tags
				textContent.SelectionStart=tempStart+4+tempLength;
			}
			else {
				textContent.SelectionStart=tempStart+s.Length;
				textContent.SelectionLength=0;
			}
			//RefreshHtml();
		}

		private void H3_Click() {
			int tempStart=textContent.SelectionStart;
			int tempLength=textContent.SelectionLength;
			string s="<h3>"+textContent.SelectedText+"</h3>";
			textContent.Paste(s);
			textContent.Focus();
			if(tempLength==0) {//nothing selected, place cursor in middle of new tags
				textContent.SelectionStart=tempStart+4+tempLength;
			}
			else {
				textContent.SelectionStart=tempStart+s.Length;
				textContent.SelectionLength=0;
			}
			//RefreshHtml();
		}

		private void Bold_Click() {
			int tempStart=textContent.SelectionStart;
			int tempLength=textContent.SelectionLength;
			string s="<b>"+textContent.SelectedText+"</b>";
			textContent.Paste(s);
			textContent.Focus();
			if(tempLength==0) {//nothing selected, place cursor in middle of new tags
				textContent.SelectionStart=tempStart+3+tempLength;
			}
			else {
				textContent.SelectionStart=tempStart+s.Length;
				textContent.SelectionLength=0;
			}
			//RefreshHtml();
		}

		private void Italic_Click() {
			int tempStart=textContent.SelectionStart;
			int tempLength=textContent.SelectionLength;
			string s="<i>"+textContent.SelectedText+"</i>";
			textContent.Paste(s);
			textContent.Focus();
			if(tempLength==0) {//nothing selected, place cursor in middle of new tags
				textContent.SelectionStart=tempStart+3+tempLength;
			}
			else {
				textContent.SelectionStart=tempStart+s.Length;
				textContent.SelectionLength=0;
			}
			//RefreshHtml();
		}

		private void Color_Click() {
			int tempStart=textContent.SelectionStart;
			int tempLength=textContent.SelectionLength;
			string s="[[color:red|"+textContent.SelectedText+"]]";
			textContent.Paste(s);
			textContent.Focus();
			if(tempLength==0) {//nothing selected, place cursor in middle of new tags
				textContent.SelectionStart=tempStart+12+tempLength;
			}
			else {
				textContent.SelectionStart=tempStart+s.Length;
				textContent.SelectionLength=0;
			}
			//RefreshHtml();
		}

		///<summary>Insert table boilerplate. Pastes selected text into the first cell.</summary>
		private void Table_Click() {
			FormWikiTableEdit FormWTE = new FormWikiTableEdit();
			MatchCollection matches=Regex.Matches(textContent.Text,@"\{\|(.+?)\|\}",RegexOptions.Singleline);
			if(matches.Count!=0) {
				foreach(Match match in matches) {
					if(textContent.SelectionStart > textContent.Text.IndexOf(match.Value)
						&& textContent.SelectionStart > textContent.Text.IndexOf(match.Value)-match.Value.Length) 
					{
						//cursor is inside of this table
						FormWTE.TableMarkup=match.Value;
						FormWTE.ShowDialog();
						if(FormWTE.DialogResult!=DialogResult.OK) {
							return;
						}
						textContent.Text=textContent.Text.Replace(match.Value,FormWTE.TableMarkup);//always safe because dialog result was OK.
						return;
					}
				}
				//Cursor is not in any existing table markup.
			}
			FormWTE.ShowDialog();//blank/new table code is inside of form.
			if(FormWTE.DialogResult!=DialogResult.OK){
				return;
			}
			textContent.SelectionLength=0;
			textContent.Paste(FormWTE.TableMarkup);
			textContent.Focus();
		}

		private void Image_Click() {
			FormWikiImages FormWI = new FormWikiImages();
			FormWI.ShowDialog();
			if(FormWI.DialogResult!=DialogResult.OK) {
				return;
			}
			textContent.Paste("[[img:"+FormWI.SelectedImageName+"]]");
			//webBrowserWiki.AllowNavigation=true;
			//RefreshHtml();
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
						MessageBox.Show(Lan.g(this,"Not allowed to save because image does not exist: ")+imgPath);
						return false;
					}
				}
			}
			//spacing around bullets-----------------------------------------------------------------------------------------------
			string[] lines=textContent.Text.Split(new string[] { "\r\n" },StringSplitOptions.None);
			for(int i=0;i<lines.Length;i++) {
				if(lines[i].Trim().StartsWith("*")) {
					if(!lines[i].StartsWith("*")) {
						MsgBox.Show(this,"Stars used for lists may not have a space before them.");
						return false;
					}
					if(lines[i].Trim().StartsWith("* ")) {
						MsgBox.Show(this,"Stars used for lists may not have a space after them.");
						return false;
					}
				}
				if(lines[i].Trim().StartsWith("#")) {
					if(!lines[i].StartsWith("#")) {
						MsgBox.Show(this,"Hashes used for lists may not have a space before them.");
						return false;
					}
					if(lines[i].Trim().StartsWith("# ")) {
						MsgBox.Show(this,"Hashes used for lists may not have a space after them.");
						return false;
					}
				}
			}
			//Invalid characters inside of various tags--------------------------------------------
			matches=Regex.Matches(textContent.Text,@"\[\[.*?\]\]");
			foreach(Match match in matches) {
				if(match.Value.Contains("\"")) {
					MessageBox.Show(Lan.g(this,"Link cannot contain double quotes: ")+match.Value);
					return false;
				}
				//This is not needed because our regex doesn't even catch them if the span a line break.  It's just interpreted as plain text.
				//if(match.Value.Contains("\r") || match.Value.Contains("\n")) {
				//	MessageBox.Show(Lan.g(this,"Link cannot contain carriage returns: ")+match.Value);
				//	return false;
				//}
				if(match.Value.StartsWith("[[img:")
					|| match.Value.StartsWith("[[keywords:")
					|| match.Value.StartsWith("[[file:")
					|| match.Value.StartsWith("[[folder:")
					|| match.Value.StartsWith("[[color:")) 
				{
					//other tags
				}
				else {
					if(match.Value.Contains("|")) {
						MessageBox.Show(Lan.g(this,"Internal link cannot contain a pipe character:")+match.Value);
						return false;
					}
				}
			}
			//Table markup rigorously formatted----------------------------------------------------------------------
			//{|
			//!Width="100"|Column Heading 1!!Width="150"|Column Heading 2!!Width=""|Column Heading 3
			//|- 
			//|Cell 1||Cell 2||Cell 3 
			//|-
			//|Cell A||Cell B||Cell C 
			//|}
			matches=Regex.Matches(s,@"\{\|(.+?)\|\}",RegexOptions.Singleline);
			foreach(Match match in matches) {
				lines=match.Value.Split(new string[] {"\r\n"},StringSplitOptions.None);
				if(lines[0]!="{|") {
					MsgBox.Show(this,"The first line of a table markup section must be exactly {|, with no additional characters.");
					return false;
				}
				if(!lines[1].StartsWith("!")) {
					MsgBox.Show(this,"The second line of a table markup section must start with ! to indicate column headers.");
					return false;
				}
				if(lines[1].StartsWith("! ")) {
					MsgBox.Show(this,"In the table, at line 2, there cannot be a space after the first !");
					return false;
				}
				//Any garbage in the header will just show in the cell rather than being processed.
				for(int i=2;i<lines.Length-1;i++) {//loop through the lines after the header
					if((i % 2)==0) {//even row
						if(lines[i]!="|-") {
							MessageBox.Show(Lan.g(this,"In the table, at line ")+(i+1).ToString()+Lan.g(this,", |- was expected, but this was found instead:")+lines[i]);
							return false;
						}
					}
					else {//odd row
						if(!lines[i].StartsWith("|")) {
							MessageBox.Show(Lan.g(this,"Table rows must start with |.  At line ")+(i+1).ToString()+Lan.g(this,", this was found instead:")+lines[i]);
							return false;
						}
						//if(lines[i].StartsWith("| ")) {
						//	MessageBox.Show(Lan.g(this,"In the table, at line ")+(i+1).ToString()+Lan.g(this,", there cannot be a space after the first |."));
						//	return false;
						//}
						//lines[i].in
						//I guess we don't really care what they put in a row.  We can just interpret garbage as a single cell.
					}
				}
				if(lines[lines.Length-1]!="|}") {
					MsgBox.Show(this,"The last line of a table markup section must be exactly |}, with no additional characters.");
					return false;
				}
			}
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
					//case "table"://table tag no longer allowed
					//case "tr":
					//case "td":
						//no attributes at all allowed on these tags
						if(node.Attributes.Count!=0) {
							throw new ApplicationException("'"+node.Attributes[0].Name+"' attribute is not allowed on <"+node.Name+"> tag.");
						}
						break;
					/*case "th":
						//only allowed attribute is width
						for(int i=0;i<node.Attributes.Count;i++) {
							if(node.Attributes[i].Name!="width") {
								throw new ApplicationException(node.Attributes[i].Name+" attribute is not allowed on <th> tag.");
							}
							if(node.Attributes[i].InnerText.Contains("%")) {
								throw new ApplicationException("Percentage is not allowed in <th> width attribute.");
							}
						}
						break;*/
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