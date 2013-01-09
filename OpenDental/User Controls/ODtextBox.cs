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

namespace OpenDental
{
	/// <summary>This is used instead of a regular textbox when quickpaste functionality is needed.</summary>
	public class ODtextBox : RichTextBox{//System.ComponentModel.Component
		private System.Windows.Forms.ContextMenu contextMenu;
		private System.ComponentModel.Container components = null;// Required designer variable.
		private static Hunspell hunspell;//We create this object one time for every instance of this textbox control within the entire program.
		private QuickPasteType quickPasteType;
		private List<string> correctList;
		private List<string> incorrectList;
		private Bitmap bitmap;//TODO: dispose when control is destroyed.
		private Graphics bufferGraphics;//TODO: dispose when control is destroyed.
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
			if(hunspell==null) {
				hunspell=new Hunspell(Properties.Resources.en_US_aff,Properties.Resources.en_US_dic);
			}
			
			correctList=new List<string>();
			incorrectList=new List<string>();
			EventHandler onClick=new EventHandler(menuItem_Click);
			MenuItem menuItem;
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
				if(bufferGraphics!=null) {//Dispose before bitmap.
					bufferGraphics.Dispose();
					bufferGraphics=null;
				}
				if(bitmap!=null) {
					bitmap.Dispose();
					bitmap=null;
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
			this.contextMenu = new System.Windows.Forms.ContextMenu();
			// 
			// contextMenu
			// 
			this.contextMenu.Popup += new System.EventHandler(this.contextMenu_Popup);
			// 
			// ODtextBox
			// 
			//this.AcceptsReturn = true;
			this.ContextMenu = this.contextMenu;
			this.Multiline = true;
			this.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
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
			if(SelectionLength==0){
				contextMenu.MenuItems[3].Enabled=false;//cut
				contextMenu.MenuItems[4].Enabled=false;//copy
			}
			else{
				contextMenu.MenuItems[3].Enabled=true;
				contextMenu.MenuItems[4].Enabled=true;
			}
		}

		protected override void OnMouseDown(MouseEventArgs e) {
			if(!this.Focused) {
				this.Focus();
			}
			base.OnMouseDown(e);
		}

		private void menuItem_Click(object sender,System.EventArgs e) {
			switch(contextMenu.MenuItems.IndexOf((MenuItem)sender)){
				case 0:
					InsertDate();
					break;
				case 1://Insert Quick Note
					ShowFullDialog();
					break;
				//case 2 is separator
				case 3://cut
					Clipboard.SetDataObject(SelectedText);
					int caretPos=SelectionStart;
					Text=Text.Remove(SelectionStart,SelectionLength);
					SelectionStart=caretPos;
					SelectionLength=0;
					break;
				case 4://copy
					Clipboard.SetDataObject(SelectedText);
					break;
				case 5://paste
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
					SelectionStart=caret+strPaste.Length;
					break;
			}
		}

		///<summary></summary>
		protected override void OnKeyUp(KeyEventArgs e) {
			base.OnKeyUp(e);
			int originalLength=base.Text.Length;
			int originalCaret=base.SelectionStart;
			string newText=QuickPasteNotes.Substitute(Text,quickPasteType);
			if(base.Text!=newText) {
				base.Text=newText;
				SelectionStart=originalCaret+Text.Length-originalLength;
			}
			//then CtrlQ
			if(e.KeyCode==Keys.Q && e.Modifiers==Keys.Control){
				ShowFullDialog();
			}
			if(this.bitmap==null) {
				this.bitmap=new Bitmap(this.Width,this.Height);
				bufferGraphics=Graphics.FromImage(this.bitmap);
				bufferGraphics.Clear(Color.Transparent);//We don't want to overwrite the text in the rich text box.
			}			
			ClearWavyLines(bufferGraphics);
			checkSpelling();
			Graphics textBoxGraphics=Graphics.FromHwnd(this.Handle);
			Application.DoEvents();
			textBoxGraphics.DrawImageUnscaled(bitmap,0,0);
			textBoxGraphics.Dispose();
		}

		private void ShowFullDialog(){
			FormQuickPaste FormQ=new FormQuickPaste();
			FormQ.TextToFill=this;
			FormQ.QuickType=quickPasteType;
			FormQ.ShowDialog();
		}

		private void ClearWavyLines(Graphics bufferGraphics) {
			Point start=this.GetPositionFromCharIndex(0);
			int numLines=(this.Height/this.FontHeight)+1;
			int y=start.Y+this.FontHeight;//Start below the first line of text.
			for(int i=0;i<numLines;i++) {
				Rectangle wavyLineArea=new Rectangle(start.X,y,this.Width,3);
				bufferGraphics.FillRectangle(Brushes.White,wavyLineArea);
				y+=this.FontHeight;
			}
		}

		private void checkSpelling() {
			int curPos=base.SelectionStart;
			string[] words=Regex.Split(Text,"([\\s])");
			if(words.Length==0) {//deleted all text, just return
				return;
			}
			string[] noPunctWords=new string[words.Length];
			for(int i=0;i<words.Length;i++) {
				noPunctWords[i]=Regex.Replace(words[i],"[\\p{P}\\p{S}-['-]]","");
			}
			Clear();
			for(int i=0;i<noPunctWords.Length;i++) {
				int textLength=Text.Length;//length before appending word
				this.AppendText(words[i]);
				bool correct=false;
				if(noPunctWords[i].Length==0) {
					continue;
				}
				if(correctList.Contains(words[i])) {
					continue;
				}
				if(incorrectList.Contains(words[i])) {
					CustomPaint(textLength,textLength+words[i].Length);
					continue;
				}
				for(int w=0;w<DictCustoms.Listt.Count;w++) {//compare to custom word list
					if(DictCustoms.Listt[w].WordText.ToLower()==noPunctWords[i].ToLower()) {//convert to lower case before comparing
						correct=true;
						break;
					}
				}
				if(!correct) {//Not in custom dictionary, so spell check
					correct=hunspell.Spell(noPunctWords[i]);
				}
				if(!correct) {
					CustomPaint(textLength,textLength+words[i].Length);
					incorrectList.Add(words[i]);
				}
				else {//if it gets here, the word was spelled correctly, determined by comparing to the custom word list and/or the hunspell dict
					correctList.Add(words[i]);
				}
			}
			base.SelectionStart=curPos;
			return;
		}

		private void CustomPaint(int startIndex,int endIndex) {
			Point start=this.GetPositionFromCharIndex(startIndex);
			Point end=this.GetPositionFromCharIndex(endIndex);
			start.Y=start.Y+this.FontHeight;
			end.Y=start.Y;
			DrawWave(start,end);
		}

		private void DrawWave(Point start,Point end) {
			Pen pen=Pens.Red;
			if((end.X-start.X)>4) {
				ArrayList pl=new ArrayList();
				for(int i=start.X;i<=(end.X-2);i=i+4) {
					pl.Add(new Point(i,start.Y));
					pl.Add(new Point(i+2,start.Y+2));
				}
				Point[] p=(Point[])pl.ToArray(typeof(Point));
				bufferGraphics.DrawLines(pen,p);
			}
			else {
				bufferGraphics.DrawLine(pen,start,end);
			}
		}

		private void InsertDate(){
			int caret=SelectionStart;
			string strPaste=DateTime.Today.ToShortDateString();
			Text=Text.Insert(caret,strPaste);
			SelectionStart=caret+strPaste.Length;
		}

		




	}
}






















