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
		private ReplaceWord ReplWord;
		private bool spellCheckIsEnabled;

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
				contextMenu.MenuItems[11].Enabled=false;//cut
				contextMenu.MenuItems[12].Enabled=false;//copy
			}
			else {
				contextMenu.MenuItems[11].Enabled=true;
				contextMenu.MenuItems[12].Enabled=true;
			}
			if(!IsOnMisspelled(PositionOfClick)) {//did not click on a misspelled word
				contextMenu.MenuItems[0].Visible=false;//suggestion 1
				contextMenu.MenuItems[1].Visible=false;//suggestion 2
				contextMenu.MenuItems[2].Visible=false;//suggestion 3
				contextMenu.MenuItems[3].Visible=false;//suggestion 4
				contextMenu.MenuItems[4].Visible=false;//suggestion 5
				contextMenu.MenuItems[5].Visible=false;//contextMenu separator
				contextMenu.MenuItems[6].Visible=false;//Add to Dictionary
				contextMenu.MenuItems[7].Visible=false;//separator
			}
			else if(IsOnMisspelled(PositionOfClick)) {//clicked on or near a misspelled word
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
				contextMenu.MenuItems[6].Visible=true;
				contextMenu.MenuItems[7].Visible=true;
			}
		}

		///<summary>Determines whether the right click was on a misspelled word.</summary>
		private bool IsOnMisspelled(Point PositionOfClick) {
			string[] words=Regex.Split(Text,"([\\s])");
			if(words.Length==0) {//no text so just return false, not on a misspelled word
				return false;
			}
			int charIndex=this.GetCharIndexFromPosition(PositionOfClick);
			Point charLocation=this.GetPositionFromCharIndex(charIndex);
			if(PositionOfClick.Y<charLocation.Y-2 || PositionOfClick.Y>charLocation.Y+this.FontHeight+2) {//this is the closest char but they were not very close when they right clicked
				return false;
			}
			char c=this.GetCharFromPosition(PositionOfClick);
			if(c=='\n') {//if closest char is a new line char, then assume not on a misspelled word
				return false;
			}
			int startIndex=0;
			ReplWord=new ReplaceWord();
			for(int i=0;i<words.Length;i++) {//after this for loop we should always have the word closest to where the user clicked.
				if(words[i].Length==0) {
					continue;
				}
				int endIndex=startIndex+words[i].Length-1;
				if(charIndex>=startIndex && charIndex<=endIndex) {//this is our word
					ReplWord.Word=words[i];
					ReplWord.StartIndex=startIndex;
					ReplWord.EndIndex=endIndex;
					break;
				}
				startIndex=startIndex+words[i].Length;
			}
			if(ListIncorrect.Contains(ReplWord.Word)) {
				return true;
			}
			return false;
		}

		private List<string> SpellSuggest() {
			List<string> suggestions=HunspellGlobal.Suggest(ReplWord.Word);
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
					this.Text=this.Text.Remove(ReplWord.StartIndex,ReplWord.Word.Length);
					this.Text=this.Text.Insert(ReplWord.StartIndex,contextMenu.MenuItems[contextMenu.MenuItems.IndexOf((MenuItem)sender)].Text);
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
					string newWord=Regex.Replace(ReplWord.Word,"[\\s]|[\\p{P}\\p{S}-['-]]","");//don't allow words with spaces or punctuation except ' and - in them
					//guaranteed to not already exist in custom dictionary, or it wouldn't be underlined.
					DictCustom word=new DictCustom();
					word.WordText=newWord;
					DictCustoms.Insert(word);
					DataValid.SetInvalid(InvalidType.DictCustoms);
					ListIncorrect.Remove(ReplWord.Word);
					ListCorrect.Add(ReplWord.Word);
					timer1.Start();
					break;
				//case 7 is separator
				case 8:
					InsertDate();
					break;
				case 9://Insert Quick Note
					ShowFullDialog();
					break;
				//case 10 is separator
				case 11://cut
					Clipboard.SetDataObject(SelectedText);
					int caretPos=SelectionStart;
					Text=Text.Remove(SelectionStart,SelectionLength);
					SelectionStart=caretPos;
					SelectionLength=0;
					break;
				case 12://copy
					Clipboard.SetDataObject(SelectedText);
					break;
				case 13://paste
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

		private void SpellCheck() {
			string[] words=Regex.Split(Text,"([\\s])");
			if(words.Length==0) {//deleted all text, just return
				return;
			}
			string[] noPunctWords=new string[words.Length];
			for(int i=0;i<words.Length;i++) {
				noPunctWords[i]=Regex.Replace(words[i],"[\\p{P}\\p{S}-['-]]","");
			}
			int startPos=0;
			for(int i=0;i<noPunctWords.Length;i++) {
				bool correct=false;
				if(noPunctWords[i].Length==0) {//noPunctWord's length might be zero but the length with punctuation could be more than zero, so move startPos forward the length of word
					startPos=startPos+words[i].Length;
					continue;
				}
				if(ListCorrect.Contains(words[i])) {
					startPos=startPos+words[i].Length;
					continue;
				}
				if(ListIncorrect.Contains(words[i])) {
					DrawWave(startPos,startPos+words[i].Length);
					startPos=startPos+words[i].Length;
					continue;
				}
				for(int w=0;w<DictCustoms.Listt.Count;w++) {//compare to custom word list
					if(DictCustoms.Listt[w].WordText.ToLower()==noPunctWords[i].ToLower()) {//convert to lower case before comparing
						correct=true;
						break;
					}
				}
				if(!correct) {//Not in custom dictionary, so spell check
					correct=HunspellGlobal.Spell(noPunctWords[i]);
				}
				if(!correct) {
					DrawWave(startPos,startPos+words[i].Length);
					ListIncorrect.Add(words[i]);
				}
				else {//if it gets here, the word was spelled correctly, determined by comparing to the custom word list and/or the hunspell dict
					ListCorrect.Add(words[i]);
				}
				startPos=startPos+words[i].Length;
			}
			return;
		}

		private void DrawWave(int startIndex,int endIndex) {
			Point start=this.GetPositionFromCharIndex(startIndex);//accounts for scroll position
			Point end=this.GetPositionFromCharIndex(endIndex);//accounts for scroll position
			start.Y=start.Y+this.FontHeight;//move from top of line to bottom of line
			end.Y=start.Y;//move from top of line to bottom of line
			if(start.Y<=4 || start.Y>=this.Height) {//Don't draw lines for text which is currently not visible.
				return;
			}
			Pen pen=Pens.Red;
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

		private class ReplaceWord {
			public string Word="";
			public int StartIndex=0;
			public int EndIndex=0;
		}

	}

}






















