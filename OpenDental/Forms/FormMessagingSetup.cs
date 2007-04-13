using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{
	/// <summary>
	/// Summary description for FormBasicTemplate.
	/// </summary>
	public class FormMessagingSetup : System.Windows.Forms.Form{
		private OpenDental.UI.Button butCancel;
		private ListBox listMessages;
		private Label label5;
		private ListBox listExtras;
		private Label label4;
		private ListBox listToFrom;
		private Label label1;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private SigElementDef[] ListUser;
		private SigElementDef[] ListExtras;
		private OpenDental.UI.Button butAdd;
		private OpenDental.UI.Button butUp;
		private OpenDental.UI.Button butDown;
		private SigElementDef[] ListMessages;

		///<summary></summary>
		public FormMessagingSetup()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			Lan.F(this);
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
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

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMessagingSetup));
			this.listMessages = new System.Windows.Forms.ListBox();
			this.label5 = new System.Windows.Forms.Label();
			this.listExtras = new System.Windows.Forms.ListBox();
			this.label4 = new System.Windows.Forms.Label();
			this.listToFrom = new System.Windows.Forms.ListBox();
			this.label1 = new System.Windows.Forms.Label();
			this.butCancel = new OpenDental.UI.Button();
			this.butAdd = new OpenDental.UI.Button();
			this.butUp = new OpenDental.UI.Button();
			this.butDown = new OpenDental.UI.Button();
			this.SuspendLayout();
			// 
			// listMessages
			// 
			this.listMessages.FormattingEnabled = true;
			this.listMessages.Location = new System.Drawing.Point(192,38);
			this.listMessages.Name = "listMessages";
			this.listMessages.Size = new System.Drawing.Size(98,368);
			this.listMessages.TabIndex = 18;
			this.listMessages.DoubleClick += new System.EventHandler(this.listMessages_DoubleClick);
			this.listMessages.Click += new System.EventHandler(this.listMessages_Click);
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(190,19);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(100,16);
			this.label5.TabIndex = 17;
			this.label5.Text = "Messages";
			this.label5.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// listExtras
			// 
			this.listExtras.FormattingEnabled = true;
			this.listExtras.Location = new System.Drawing.Point(111,38);
			this.listExtras.Name = "listExtras";
			this.listExtras.Size = new System.Drawing.Size(75,368);
			this.listExtras.TabIndex = 16;
			this.listExtras.DoubleClick += new System.EventHandler(this.listExtras_DoubleClick);
			this.listExtras.Click += new System.EventHandler(this.listExtras_Click);
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(109,19);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(78,16);
			this.label4.TabIndex = 15;
			this.label4.Text = "Extras";
			this.label4.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// listToFrom
			// 
			this.listToFrom.FormattingEnabled = true;
			this.listToFrom.Location = new System.Drawing.Point(30,38);
			this.listToFrom.Name = "listToFrom";
			this.listToFrom.Size = new System.Drawing.Size(75,368);
			this.listToFrom.TabIndex = 12;
			this.listToFrom.DoubleClick += new System.EventHandler(this.listToFrom_DoubleClick);
			this.listToFrom.Click += new System.EventHandler(this.listToFrom_Click);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(28,19);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(78,16);
			this.label1.TabIndex = 11;
			this.label1.Text = "Users";
			this.label1.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// butCancel
			// 
			this.butCancel.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butCancel.Autosize = true;
			this.butCancel.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butCancel.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butCancel.CornerRadius = 4F;
			this.butCancel.Location = new System.Drawing.Point(365,379);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75,26);
			this.butCancel.TabIndex = 0;
			this.butCancel.Text = "&Close";
			this.butCancel.Click += new System.EventHandler(this.butClose_Click);
			// 
			// butAdd
			// 
			this.butAdd.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.butAdd.Autosize = true;
			this.butAdd.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butAdd.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butAdd.CornerRadius = 4F;
			this.butAdd.Image = global::OpenDental.Properties.Resources.Add;
			this.butAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butAdd.Location = new System.Drawing.Point(365,241);
			this.butAdd.Name = "butAdd";
			this.butAdd.Size = new System.Drawing.Size(75,26);
			this.butAdd.TabIndex = 19;
			this.butAdd.Text = "Add";
			this.butAdd.Click += new System.EventHandler(this.butAdd_Click);
			// 
			// butUp
			// 
			this.butUp.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butUp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.butUp.Autosize = true;
			this.butUp.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butUp.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butUp.CornerRadius = 4F;
			this.butUp.Image = global::OpenDental.Properties.Resources.up;
			this.butUp.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butUp.Location = new System.Drawing.Point(365,273);
			this.butUp.Name = "butUp";
			this.butUp.Size = new System.Drawing.Size(75,26);
			this.butUp.TabIndex = 20;
			this.butUp.Text = "Up";
			this.butUp.Click += new System.EventHandler(this.butUp_Click);
			// 
			// butDown
			// 
			this.butDown.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.butDown.Autosize = true;
			this.butDown.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butDown.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butDown.CornerRadius = 4F;
			this.butDown.Image = global::OpenDental.Properties.Resources.down;
			this.butDown.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butDown.Location = new System.Drawing.Point(365,305);
			this.butDown.Name = "butDown";
			this.butDown.Size = new System.Drawing.Size(75,26);
			this.butDown.TabIndex = 21;
			this.butDown.Text = "Down";
			this.butDown.Click += new System.EventHandler(this.butDown_Click);
			// 
			// FormMessagingSetup
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.ClientSize = new System.Drawing.Size(492,430);
			this.Controls.Add(this.butDown);
			this.Controls.Add(this.butUp);
			this.Controls.Add(this.butAdd);
			this.Controls.Add(this.listMessages);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.listExtras);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.listToFrom);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.butCancel);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormMessagingSetup";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Messaging Setup";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMessagingSetup_FormClosing);
			this.Load += new System.EventHandler(this.FormMessagingSetup_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormMessagingSetup_Load(object sender,EventArgs e) {
			FillLists();
		}

		private void FillLists(){
			SigElementDefs.Refresh();
			ListUser=SigElementDefs.GetSubList(SignalElementType.User);
			ListExtras=SigElementDefs.GetSubList(SignalElementType.Extra);
			ListMessages=SigElementDefs.GetSubList(SignalElementType.Message);
			listToFrom.Items.Clear();
			for(int i=0;i<ListUser.Length;i++){
				listToFrom.Items.Add(ListUser[i].SigText);
			}
			listExtras.Items.Clear();
			for(int i=0;i<ListExtras.Length;i++) {
				listExtras.Items.Add(ListExtras[i].SigText);
			}
			listMessages.Items.Clear();
			for(int i=0;i<ListMessages.Length;i++) {
				listMessages.Items.Add(ListMessages[i].SigText);
			}
		}

		private void listToFrom_Click(object sender,EventArgs e) {
			listExtras.SelectedIndex=-1;
			listMessages.SelectedIndex=-1;
		}

		private void listExtras_Click(object sender,EventArgs e) {
			listToFrom.SelectedIndex=-1;
			listMessages.SelectedIndex=-1;
		}

		private void listMessages_Click(object sender,EventArgs e) {
			listToFrom.SelectedIndex=-1;
			listExtras.SelectedIndex=-1;
		}

		private void butAdd_Click(object sender,EventArgs e) {
			FormSigElementDefEdit FormS=new FormSigElementDefEdit();
			FormS.ElementCur=new SigElementDef();
			FormS.ElementCur.LightColor=Color.White;
			//default is user
			if(listExtras.SelectedIndex!=-1){
				FormS.ElementCur.SigElementType=SignalElementType.Extra;
			}
			if(listMessages.SelectedIndex!=-1) {
				FormS.ElementCur.SigElementType=SignalElementType.Message;
			}
			FormS.IsNew=true;
			FormS.ShowDialog();
			if(FormS.DialogResult!=DialogResult.OK){
				return;
			}
			//set the order
			SigElementDef element=FormS.ElementCur.Copy();
			if(element.SigElementType==SignalElementType.User){
				element.ItemOrder=ListUser.Length;
				SigElementDefs.Update(element);
			}
			else if(element.SigElementType==SignalElementType.Extra) {
				element.ItemOrder=ListExtras.Length;
				SigElementDefs.Update(element);
			}
			else if(element.SigElementType==SignalElementType.Message) {
				element.ItemOrder=ListMessages.Length;
				SigElementDefs.Update(element);
			}
			FillLists();
			//Select the item
			for(int i=0;i<ListUser.Length;i++){
				if(ListUser[i].SigElementDefNum==element.SigElementDefNum){
					listToFrom.SelectedIndex=i;
				}
			}
			for(int i=0;i<ListExtras.Length;i++) {
				if(ListExtras[i].SigElementDefNum==element.SigElementDefNum) {
					listExtras.SelectedIndex=i;
				}
			}
			for(int i=0;i<ListMessages.Length;i++) {
				if(ListMessages[i].SigElementDefNum==element.SigElementDefNum) {
					listMessages.SelectedIndex=i;
				}
			}
		}

		private void listToFrom_DoubleClick(object sender,EventArgs e) {
			if(listToFrom.SelectedIndex==-1){
				return;
			}
			FormSigElementDefEdit FormS=new FormSigElementDefEdit();
			FormS.ElementCur=ListUser[listToFrom.SelectedIndex];
			FormS.ShowDialog();
			if(FormS.DialogResult!=DialogResult.OK) {
				return;
			}
			FillLists();
			//not possible to change ItemOrder here.
		}

		private void listExtras_DoubleClick(object sender,EventArgs e) {
			if(listExtras.SelectedIndex==-1) {
				return;
			}
			FormSigElementDefEdit FormS=new FormSigElementDefEdit();
			FormS.ElementCur=ListExtras[listExtras.SelectedIndex];
			FormS.ShowDialog();
			if(FormS.DialogResult!=DialogResult.OK) {
				return;
			}
			FillLists();
		}

		private void listMessages_DoubleClick(object sender,EventArgs e) {
			if(listMessages.SelectedIndex==-1) {
				return;
			}
			FormSigElementDefEdit FormS=new FormSigElementDefEdit();
			FormS.ElementCur=ListMessages[listMessages.SelectedIndex];
			FormS.ShowDialog();
			if(FormS.DialogResult!=DialogResult.OK) {
				return;
			}
			FillLists();
		}

		private void butUp_Click(object sender,EventArgs e) {
			if(listToFrom.SelectedIndex==-1
				&& listExtras.SelectedIndex==-1
				&& listMessages.SelectedIndex==-1)
			{
				MsgBox.Show(this,"Please select an item first.");
				return;
			}
			int selected;
			if(listToFrom.SelectedIndex!=-1){
				selected=listToFrom.SelectedIndex;
				if(selected==0) {
					return;
				}
				SigElementDefs.MoveUp(selected,ListUser);
				FillLists();
				listToFrom.SelectedIndex=selected-1;
			}
			else if(listExtras.SelectedIndex!=-1) {
				selected=listExtras.SelectedIndex;
				if(selected==0) {
					return;
				}
				SigElementDefs.MoveUp(selected,ListExtras);
				FillLists();
				listExtras.SelectedIndex=selected-1;
			}
			else if(listMessages.SelectedIndex!=-1) {
				selected=listMessages.SelectedIndex;
				if(selected==0) {
					return;
				}
				SigElementDefs.MoveUp(selected,ListMessages);
				FillLists();
				listMessages.SelectedIndex=selected-1;
			}
		}

		private void butDown_Click(object sender,EventArgs e) {
			if(listToFrom.SelectedIndex==-1
				&& listExtras.SelectedIndex==-1
				&& listMessages.SelectedIndex==-1) {
				MsgBox.Show(this,"Please select an item first.");
				return;
			}
			int selected;
			if(listToFrom.SelectedIndex!=-1) {
				selected=listToFrom.SelectedIndex;
				if(selected==listToFrom.Items.Count-1) {
					return;
				}
				SigElementDefs.MoveDown(selected,ListUser);
				FillLists();
				listToFrom.SelectedIndex=selected+1;
			}
			else if(listExtras.SelectedIndex!=-1) {
				selected=listExtras.SelectedIndex;
				if(selected==listExtras.Items.Count-1) {
					return;
				}
				SigElementDefs.MoveDown(selected,ListExtras);
				FillLists();
				listExtras.SelectedIndex=selected+1;
			}
			else if(listMessages.SelectedIndex!=-1) {
				selected=listMessages.SelectedIndex;
				if(selected==listMessages.Items.Count-1) {
					return;
				}
				SigElementDefs.MoveDown(selected,ListMessages);
				FillLists();
				listMessages.SelectedIndex=selected+1;
			}
		}

		private void butClose_Click(object sender, System.EventArgs e) {
			Close();
		}

		private void FormMessagingSetup_FormClosing(object sender,FormClosingEventArgs e) {
			DataValid.SetInvalid(InvalidTypes.ClearHouses);//messaging shares with clearinghouses.
		}

		

		

	

		


	}
}





















