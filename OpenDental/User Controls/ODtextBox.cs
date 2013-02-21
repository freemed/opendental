using System;
using System.ComponentModel;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;
using OpenDentBusiness;
using NHunspell;
using System.Text.RegularExpressions;
using OpenDental.UI;
using System.Drawing;
using System.Runtime.InteropServices;

namespace OpenDental
{
	/// <summary>This is used instead of a regular textbox when quickpaste functionality is needed.</summary>
	public class ODtextBox : RichTextBox{//System.ComponentModel.Component
		private System.Windows.Forms.ContextMenu contextMenu;
		private IContainer components;// Required designer variable.
		private static Hunspell HunspellGlobal;//We create this object one time for every instance of this textbox control within the entire program.
		private QuickPasteType quickPasteType;
		private List<string> ListCorrect;
		private List<string> ListIncorrect;
		private Graphics BufferGraphics;
		private Timer timer1;
		private Point PositionOfClick;
		private Group ReplWord;
		private bool spellCheckIsEnabled;//set to true in constructor

		///<summary>Set true to enable spell checking in this control.</summary>
		[Category("Behavior"),Description("Set true to enable spell checking.")]
		[DefaultValue(true)]
		public bool SpellCheckIsEnabled {
			get {
				return spellCheckIsEnabled;
			}
			set {
				spellCheckIsEnabled=value;
			}
		}

		/*public ODtextBox(System.ComponentModel.IContainer container)
		{
			///
			/// Required for Windows.Forms Class Composition Designer support
			///
			container.Add(this);
			InitializeComponent();

		}*/

		///<summary></summary>
		public ODtextBox(){
			InitializeComponent();// Required for Windows.Forms Class Composition Designer support
			spellCheckIsEnabled=true;
			this.AcceptsTab=true;//Causes CR to not also trigger OK button on a form when that button is set as AcceptButton on the form.
			this.DetectUrls=false;
			if(System.ComponentModel.LicenseManager.UsageMode!=System.ComponentModel.LicenseUsageMode.Designtime
				&& HunspellGlobal==null)
			{
				HunspellGlobal=new Hunspell(Properties.Resources.en_US_aff,Properties.Resources.en_US_dic);
			}
			ListCorrect=new List<string>();
			ListCorrect.Add("\n");
			ListCorrect.Add("\t");
			ListIncorrect=new List<string>();
			EventHandler onClick=new EventHandler(menuItem_Click);
			MenuItem menuItem;
			contextMenu.MenuItems.Add("",onClick);//These five menu items will hold the suggested spelling for misspelled words.  If no misspelled words, they will not be visible.
			contextMenu.MenuItems.Add("",onClick);
			contextMenu.MenuItems.Add("",onClick);
			contextMenu.MenuItems.Add("",onClick);
			contextMenu.MenuItems.Add("",onClick);
			contextMenu.MenuItems.Add("-");
			contextMenu.MenuItems.Add("Add to Dictionary",onClick);
			contextMenu.MenuItems.Add("Disable Spell Check",onClick);
			contextMenu.MenuItems.Add("-");
			menuItem=new MenuItem(Lan.g(this,"Insert Date"),onClick,Shortcut.CtrlD);
			contextMenu.MenuItems.Add(menuItem);
			menuItem=new MenuItem(Lan.g(this,"Insert Quick Note"),onClick,Shortcut.CtrlQ);
			contextMenu.MenuItems.Add(menuItem);
			contextMenu.MenuItems.Add("-");
			menuItem=new MenuItem(Lan.g(this,"Cut"),onClick,Shortcut.CtrlX);
			contextMenu.MenuItems.Add(menuItem);
			menuItem=new MenuItem(Lan.g(this,"Copy"),onClick,Shortcut.CtrlC);
			contextMenu.MenuItems.Add(menuItem);
			menuItem=new MenuItem(Lan.g(this,"Paste"),onClick,Shortcut.CtrlV);
			contextMenu.MenuItems.Add(menuItem);
		}

		///<summary>Clean up any resources being used.</summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
				if(BufferGraphics!=null) {//Dispose before bitmap.
					BufferGraphics.Dispose();
					BufferGraphics=null;
				}
				//We do not dispose the hunspell object because it will be automatially disposed of when the program closes.
			}
			base.Dispose( disposing );
		}


		#region Component Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.contextMenu = new System.Windows.Forms.ContextMenu();
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			this.SuspendLayout();
			// 
			// contextMenu
			// 
			this.contextMenu.Popup += new System.EventHandler(this.contextMenu_Popup);
			// 
			// timer1
			// 
			this.timer1.Interval = 500;
			this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
			// 
			// ODtextBox
			// 
			this.ContextMenu = this.contextMenu;
			this.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
			this.VScroll += new System.EventHandler(this.ODtextBox_VScroll);
			this.ResumeLayout(false);

		}
		#endregion

		///<summary></summary>
		[Category("Behavior"),Description("This will determine which category of Quick Paste notes opens first.")]
		public QuickPasteType QuickPasteType{
			get{
				return quickPasteType;
			}
			set{
				quickPasteType=value;
			}
		}

		private void contextMenu_Popup(object sender, System.EventArgs e) {
			if(SelectionLength==0) {
				contextMenu.MenuItems[12].Enabled=false;//cut
				contextMenu.MenuItems[13].Enabled=false;//copy
			}
			else {
				contextMenu.MenuItems[12].Enabled=true;
				contextMenu.MenuItems[13].Enabled=true;
			}
			if(!IsOnMisspelled(PositionOfClick) || !PrefC.GetBool(PrefName.SpellCheckIsEnabled)) {//did not click on a misspelled word OR spell check is disabled
				contextMenu.MenuItems[0].Visible=false;//suggestion 1
				contextMenu.MenuItems[1].Visible=false;//suggestion 2
				contextMenu.MenuItems[2].Visible=false;//suggestion 3
				contextMenu.MenuItems[3].Visible=false;//suggestion 4
				contextMenu.MenuItems[4].Visible=false;//suggestion 5
				contextMenu.MenuItems[5].Visible=false;//contextMenu separator
				contextMenu.MenuItems[6].Visible=false;//Add to Dictionary
				contextMenu.MenuItems[7].Visible=false;//Disable Spell Check
				contextMenu.MenuItems[8].Visible=false;//separator
			}
			else if(IsOnMisspelled(PositionOfClick) && PrefC.GetBool(PrefName.SpellCheckIsEnabled)) {//clicked on or near a misspelled word AND spell check is enabled
				List<string> suggestions=SpellSuggest();
				if(suggestions.Count==0) {//no suggestions
					contextMenu.MenuItems[0].Text=Lan.g(this,"No Spelling Suggestions");
					contextMenu.MenuItems[0].Visible=true;
					contextMenu.MenuItems[0].Enabled=false;//suggestion 1 set to "No Spelling Suggestions"
					contextMenu.MenuItems[1].Visible=false;//suggestion 2
					contextMenu.MenuItems[2].Visible=false;//suggestion 3
					contextMenu.MenuItems[3].Visible=false;//suggestion 4
					contextMenu.MenuItems[4].Visible=false;//suggestion 5
				}
				else {
					for(int i=0;i<5;i++) {//Only display first 5 suggestions if available
						if(i>=suggestions.Count) {
							contextMenu.MenuItems[i].Visible=false;
							continue;
						}
						contextMenu.MenuItems[i].Text=suggestions[i];
						contextMenu.MenuItems[i].Visible=true;
						contextMenu.MenuItems[i].Enabled=true;
					}
				}
				contextMenu.MenuItems[5].Visible=true;//contextMenu separator, will display whether or not there is a suggestion for the misspelled word
				contextMenu.MenuItems[6].Visible=true;//Add to Dictionary
				contextMenu.MenuItems[7].Visible=true;//Disable Spell Check
				contextMenu.MenuItems[8].Visible=true;//contextMenu separator
			}
		}

		///<summary>Determines whether the right click was on a misspelled word.  Also sets the start and end index of chars to be replaced in text.</summary>
		private bool IsOnMisspelled(Point PositionOfClick) {
			MatchCollection words=GetWords();
			int charIndex=this.GetCharIndexFromPosition(PositionOfClick);
			Point charLocation=this.GetPositionFromCharIndex(charIndex);
			if(PositionOfClick.Y<charLocation.Y-2 || PositionOfClick.Y>charLocation.Y+this.FontHeight+2) {//this is the closest char but they were not very close when they right clicked
			  return false;
			}
			char c=this.GetCharFromPosition(PositionOfClick);
			if(c=='\n') {//if closest char is a new line char, then assume not on a misspelled word
			  return false;
			}
			foreach(Match m in words) {
			  Group word=m.Groups[1];//Group 0 is the entire match, group 1 is our word, group 2 is our word without the first character (determined by parentheses).
			  int startIndex=word.Index;//word.Index is relative to Text.
			  int endIndex=startIndex+word.Length-1;
			  if(charIndex>=startIndex && charIndex<=endIndex) {//this is our word
					ReplWord=word;
					break;
			  }
			}
			if(ReplWord==null) {
				return false;
			}
			if(ListIncorrect.Contains(ReplWord.Value)) {
				return true;
			}
			return false;
		}

		private List<string> SpellSuggest() {
			List<string> suggestions=HunspellGlobal.Suggest(ReplWord.Value);
			return suggestions;
		}

		protected override void OnMouseDown(MouseEventArgs e) {
			if(!this.Focused) {
				this.Focus();
			}
			base.OnMouseDown(e);
			PositionOfClick=new Point(e.X,e.Y);
		}

		private void menuItem_Click(object sender,System.EventArgs e) {
			switch(contextMenu.MenuItems.IndexOf((MenuItem)sender)){
				case 0:
				case 1:
				case 2:
				case 3:
				case 4:
					int originalCaret=this.SelectionStart;
					this.Text=this.Text.Remove(ReplWord.Index,ReplWord.Value.Length);
					this.Text=this.Text.Insert(ReplWord.Index,contextMenu.MenuItems[contextMenu.MenuItems.IndexOf((MenuItem)sender)].Text);
					if(this.Text.Length<=originalCaret) {
						this.SelectionStart=this.Text.Length;
					}
					else {
						this.SelectionStart=originalCaret;
					}
					timer1.Start();
					break;
				//case 5 is separator
				case 6://Add to dict
					string newWord=ReplWord.Value;
					//guaranteed to not already exist in custom dictionary, or it wouldn't be underlined.
					DictCustom word=new DictCustom();
					word.WordText=newWord;
					DictCustoms.Insert(word);
					DataValid.SetInvalid(InvalidType.DictCustoms);
					ListIncorrect.Remove(ReplWord.Value);
					ListCorrect.Add(ReplWord.Value);
					timer1.Start();
					break;
				case 7://Disable spell check
					if(!MsgBox.Show(this,MsgBoxButtons.OKCancel,"This will disable spell checking.  To re-enable, go to Setup | Spell Check and check the \"Spell Check Enabled\" box.")) {
						break;
					}
					Prefs.UpdateBool(PrefName.SpellCheckIsEnabled,false);
					DataValid.SetInvalid(InvalidType.Prefs);
					Bitmap BitmapOverlay=new Bitmap(this.Width,this.Height);//Clear wavy lines
					BufferGraphics=Graphics.FromImage(BitmapOverlay);
					BufferGraphics.Clear(Color.Transparent);//We don't want to overwrite the text in the rich text box.
					ClearWavyLines(BufferGraphics);
					Graphics graphicsTextBox=Graphics.FromHwnd(this.Handle);
					graphicsTextBox.DrawImageUnscaled(BitmapOverlay,0,0);
					graphicsTextBox.Dispose();
					BitmapOverlay.Dispose();
					break;
				//case 8 is separator
				case 9:
					InsertDate();
					break;
				case 10://Insert Quick Note
					ShowFullDialog();
					break;
				//case 11 is separator
				case 12://cut
					Clipboard.SetDataObject(SelectedText);
					int caretPos=SelectionStart;
					Text=Text.Remove(SelectionStart,SelectionLength);
					SelectionStart=caretPos;
					SelectionLength=0;
					break;
				case 13://copy
					Clipboard.SetDataObject(SelectedText);
					break;
				case 14://paste
					if(!Clipboard.ContainsText()) {
					  MsgBox.Show(this,"There is no text on the clipboard.");
					  break;
					}
					int caret=SelectionStart;
					IDataObject iData=Clipboard.GetDataObject();					
					if(SelectionLength>0){
						Text=Text.Remove(SelectionStart,SelectionLength);
						SelectionLength=0;
					}
					string strPaste=(string)iData.GetData(DataFormats.Text); 
					Text=Text.Insert(caret,strPaste);
					//MaxLength is not enforced by the RichTextBox.  It allows us to set the Text value to a longer length, so we have to handle it manually.
					if(Text.Length>MaxLength) {
						Text=Text.Substring(0,MaxLength);
					}
					SelectionStart=caret+strPaste.Length;
					break;
			}
		}

		private void timer1_Tick(object sender,EventArgs e) {
			timer1.Stop();
			CheckSpelling();
		}

		private void ODtextBox_VScroll(object sender,EventArgs e) {
			timer1.Stop();
			timer1.Start();
		}

		protected override void OnKeyDown(KeyEventArgs e) {
			base.OnKeyDown(e);
			if(!PrefC.GetBool(PrefName.SpellCheckIsEnabled)) {//Only spell check if enabled
				return;
			}
			Bitmap BitmapOverlay=new Bitmap(this.Width,this.Height);
			BufferGraphics=Graphics.FromImage(BitmapOverlay);
			BufferGraphics.Clear(Color.Transparent);//We don't want to overwrite the text in the rich text box.
			ClearWavyLines(BufferGraphics);
			Graphics graphicsTextBox=Graphics.FromHwnd(this.Handle);
			graphicsTextBox.DrawImageUnscaled(BitmapOverlay,0,0);
			graphicsTextBox.Dispose();
			BitmapOverlay.Dispose();
		}

		private void ClearWavyLines(Graphics bufferGraphics) {
			string[] words=Regex.Split(Text,"([\\s])");
			int startInd=0;
			for(int i=0;i<words.Length;i++) {//go through each word and use GetPositionFromCharIndex to determine the y position to draw our white box
				Point start=this.GetPositionFromCharIndex(startInd);//get pos of character at startInd, y could be negative if scroll bar active
				start.Y=start.Y+this.FontHeight;
				if(start.Y<0) {
					startInd=startInd+words[i].Length;
					continue;
				}
				if(start.Y>=this.Height) {
					break;
				}
				Rectangle wavyLineArea=new Rectangle(start.X,start.Y,this.Width,2);
			  bufferGraphics.FillRectangle(Brushes.White,wavyLineArea);
				startInd=startInd+words[i].Length;
			}
		}
		
		///<summary></summary>
		protected override void OnKeyUp(KeyEventArgs e) {
			base.OnKeyUp(e);
			timer1.Stop();
			int originalLength=base.Text.Length;
			int originalCaret=base.SelectionStart;
			string newText=QuickPasteNotes.Substitute(Text,quickPasteType);
			if(base.Text!=newText) {
				base.Text=newText;
				SelectionStart=originalCaret+Text.Length-originalLength;
			}
			//then CtrlQ
			if(e.KeyCode==Keys.Q && e.Modifiers==Keys.Control) {
				ShowFullDialog();
			}
			timer1.Start();
		}

		private void CheckSpelling() {
			if(!PrefC.GetBool(PrefName.SpellCheckIsEnabled)) {//Only spell check if enabled
				return;
			}
			Bitmap BitmapOverlay=new Bitmap(this.Width,this.Height);
			BufferGraphics=Graphics.FromImage(BitmapOverlay);
			BufferGraphics.Clear(Color.Transparent);//We don't want to overwrite the text in the rich text box.
			ClearWavyLines(BufferGraphics);
			SpellCheck();
			Graphics graphicsTextBox=Graphics.FromHwnd(this.Handle);
			graphicsTextBox.DrawImageUnscaled(BitmapOverlay,0,0);
			graphicsTextBox.Dispose();
			BitmapOverlay.Dispose();
		}

		///<summary>Performs spell checking against indiviudal words against the English USA dictionary.</summary>
		private void SpellCheck() {
			//Matches a word containing at least one alpha-numeric and might include punctuation in the middle of the word.
			MatchCollection words=GetWords();
			foreach(Match m in words) {
				Group word=m.Groups[1];//Group 0 is the entire match, group 1 is our word, group 2 is our word without the first character (determined by parentheses).
				bool correct=false;
				if(ListCorrect.Contains(word.Value)) {
					continue;//Spelled correctly
				}
				int startIndex=word.Index;//word.Index is relative to Text.
				int endIndex=startIndex+word.Length;//One spot past the end of the word, because DrawWave() draws to the beginning of the character of the endIndex.
				if(ListIncorrect.Contains(word.Value)) {
					DrawWave(startIndex,endIndex);
					continue;//Spelled incorrectly
				}
				for(int w=0;w<DictCustoms.Listt.Count;w++) {//compare to custom word list
					if(DictCustoms.Listt[w].WordText.ToLower()==word.Value.ToLower()) {//convert to lower case before comparing
						correct=true;
						break;
					}
				}
				if(!correct) {//Not in custom dictionary, so spell check
					correct=HunspellGlobal.Spell(word.Value);
				}
				if(!correct) {
					DrawWave(startIndex,endIndex);
					ListIncorrect.Add(word.Value);
				}
				else {//if it gets here, the word was spelled correctly, determined by comparing to the custom word list and/or the hunspell dict
					ListCorrect.Add(word.Value);
				}
			}
		}

		///<summary>Determines individual words by matching regular expression pattern.  Pattern is 0 or more non word characters, followed by a word character (defined as [a-zA-Z0-9_]), followed by 0 or 1 (non-white space chars followed by a word char), followed by 0 or more non-word chars.  So a word has to begin and end with a word character (could be the same char) and in between those chars there can be any number of chars as long as they are not white space.</summary>
		private MatchCollection GetWords() {
			return Regex.Matches(Text,@"\W*(\w([\S-[/\\]]*\w)?)\W*",RegexOptions.Multiline|RegexOptions.CultureInvariant);
		}

		private void DrawWave(int startIndex,int endIndex) {
			Point start=this.GetPositionFromCharIndex(startIndex);//accounts for scroll position
			Point end=this.GetPositionFromCharIndex(endIndex);//accounts for scroll position
			start.Y=start.Y+this.FontHeight;//move from top of line to bottom of line
			end.Y=end.Y+this.FontHeight;//move from top of line to bottom of line
			if(start.Y<=4 || start.Y>=this.Height) {//Don't draw lines for text which is currently not visible.
				return;
			}
			Pen pen=Pens.Red;
			if(end.Y>start.Y) {//Mispelled word spans multiple lines
				Point tempEnd=start;
				tempEnd.X=this.Width;
				while(tempEnd.Y<=end.Y) {
					if((tempEnd.X-start.X)>4) {//Only draw wavy line if mispelled word is at least 4 pixels wide, otherwise draw straight line
						ArrayList pl=new ArrayList();
						for(int i=start.X;i<=(tempEnd.X-2);i=i+4) {
							pl.Add(new Point(i,start.Y));
							pl.Add(new Point(i+2,start.Y+1));
						}
						Point[] p=(Point[])pl.ToArray(typeof(Point));
						BufferGraphics.DrawLines(pen,p);
					}
					else {
						BufferGraphics.DrawLine(pen,start,end);
					}
					start.X=1;
					start.Y=start.Y+this.FontHeight;
					tempEnd.Y=start.Y;
					if(tempEnd.Y==end.Y) {//We incremented to the next line and this is the last line of the mispelled word
						tempEnd.X=end.X;
					}
					else {//not the last line of mispelled word, so draw wavy line to end of this line
						tempEnd.X=this.Width;
					}
				}
			}
			else {
				if((end.X-start.X)>4) {
					ArrayList pl=new ArrayList();
					for(int i=start.X;i<=(end.X-2);i=i+4) {
						pl.Add(new Point(i,start.Y));
						pl.Add(new Point(i+2,start.Y+1));
					}
					Point[] p=(Point[])pl.ToArray(typeof(Point));
					BufferGraphics.DrawLines(pen,p);
				}
				else {
					BufferGraphics.DrawLine(pen,start,end);
				}
			}
		}

		private void ShowFullDialog(){
			FormQuickPaste FormQ=new FormQuickPaste();
			FormQ.TextToFill=this;
			FormQ.QuickType=quickPasteType;
			FormQ.ShowDialog();
		}

		private void InsertDate(){
			int caret=SelectionStart;
			string strPaste=DateTime.Today.ToShortDateString();
			Text=Text.Insert(caret,strPaste);
			SelectionStart=caret+strPaste.Length;
		}

	}

}






















