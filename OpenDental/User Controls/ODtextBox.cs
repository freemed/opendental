using System;
using System.ComponentModel;
using System.Collections;
using System.Diagnostics;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental
{
	/// <summary>
	/// Summary description for ODtextBox.
	/// </summary>
	public class ODtextBox : System.Windows.Forms.TextBox{//System.ComponentModel.Component
		private System.Windows.Forms.ContextMenu contextMenu;
		private System.ComponentModel.Container components = null;// Required designer variable.
		private QuickPasteType quickPasteType;

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
			this.AcceptsReturn = true;
			this.ContextMenu = this.contextMenu;
			this.Multiline = true;
			this.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;

		}
		#endregion

		///<summary></summary>
		[Category("Behavior"),
			Description("This will determine which category of Quick Paste notes opens first.")
		]
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
					IDataObject iData=Clipboard.GetDataObject();
					int caret=SelectionStart;
					if(!iData.GetDataPresent(DataFormats.Text)){
						MessageBox.Show(Lan.g(this,"Could not retrieve data off the clipboard."));
						break;
					}
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
			base.OnKeyUp (e);
			int originalLength=Text.Length;
			int originalCaret=SelectionStart;
			Text=QuickPasteNotes.Substitute(Text,quickPasteType);
			if(Text.Length!=originalLength){
				SelectionStart=originalCaret+Text.Length-originalLength;
			}
			//then CtrlQ
			if(e.KeyCode==Keys.Q && e.Modifiers==Keys.Control){
				ShowFullDialog();
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






















