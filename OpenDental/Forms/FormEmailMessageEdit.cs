using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.Collections;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Windows.Forms;
//using OpenDental.Reporting;
//using Indy.Sockets.IndySMTP;
//using Indy.Sockets.IndyMessage;
using OpenDentBusiness;
using CodeBase;

namespace OpenDental{
	/// <summary>
	/// Summary description for FormBasicTemplate.
	/// </summary>
	public class FormEmailMessageEdit : System.Windows.Forms.Form{
		private OpenDental.UI.Button butCancel;
		/// <summary>Required designer variable.</summary>
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox textSubject;
		private System.Windows.Forms.TextBox textToAddress;
		private System.Windows.Forms.TextBox textFromAddress;
		private OpenDental.UI.Button butSend;
		private System.Windows.Forms.TextBox textMsgDateTime;
		private OpenDental.UI.Button butDeleteTemplate;
		private OpenDental.UI.Button butAdd;
		private System.Windows.Forms.Label label4;
		private OpenDental.ODtextBox textBodyText;
		private System.Windows.Forms.ListBox listTemplates;
		private OpenDental.UI.Button butInsert;
		private System.Windows.Forms.Label labelSent;
		///<summary></summary>
		public bool IsNew;
		private bool templatesChanged;
		private System.Windows.Forms.Panel panelTemplates;
		private bool messageChanged;
		private OpenDental.UI.Button butSave;
		private OpenDental.UI.Button butDelete;
		private OpenDental.UI.Button butAttach;
		private ListBox listAttachments;
		private ContextMenu contextMenuAttachments;
		private MenuItem menuItemOpen;
		private MenuItem menuItemRename;
		private MenuItem menuItemRemove;
		//private int PatNum;
		private EmailMessage MessageCur;

		///<summary></summary>
		public FormEmailMessageEdit(EmailMessage messageCur){
			InitializeComponent();// Required for Windows Form Designer support
			//PatNum=patNum;
			Lan.F(this);
			MessageCur=messageCur.Copy();
			listAttachments.ContextMenu=contextMenuAttachments;
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormEmailMessageEdit));
			this.label2 = new System.Windows.Forms.Label();
			this.textSubject = new System.Windows.Forms.TextBox();
			this.textToAddress = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.textFromAddress = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.textMsgDateTime = new System.Windows.Forms.TextBox();
			this.labelSent = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.listTemplates = new System.Windows.Forms.ListBox();
			this.panelTemplates = new System.Windows.Forms.Panel();
			this.butInsert = new OpenDental.UI.Button();
			this.butDeleteTemplate = new OpenDental.UI.Button();
			this.butAdd = new OpenDental.UI.Button();
			this.listAttachments = new System.Windows.Forms.ListBox();
			this.contextMenuAttachments = new System.Windows.Forms.ContextMenu();
			this.menuItemOpen = new System.Windows.Forms.MenuItem();
			this.menuItemRename = new System.Windows.Forms.MenuItem();
			this.menuItemRemove = new System.Windows.Forms.MenuItem();
			this.butAttach = new OpenDental.UI.Button();
			this.butDelete = new OpenDental.UI.Button();
			this.butSave = new OpenDental.UI.Button();
			this.textBodyText = new OpenDental.ODtextBox();
			this.butSend = new OpenDental.UI.Button();
			this.butCancel = new OpenDental.UI.Button();
			this.panelTemplates.SuspendLayout();
			this.SuspendLayout();
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(210,64);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(69,14);
			this.label2.TabIndex = 3;
			this.label2.Text = "Subject:";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textSubject
			// 
			this.textSubject.Location = new System.Drawing.Point(278,62);
			this.textSubject.Name = "textSubject";
			this.textSubject.Size = new System.Drawing.Size(328,20);
			this.textSubject.TabIndex = 0;
			// 
			// textToAddress
			// 
			this.textToAddress.Location = new System.Drawing.Point(278,41);
			this.textToAddress.Name = "textToAddress";
			this.textToAddress.Size = new System.Drawing.Size(328,20);
			this.textToAddress.TabIndex = 8;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(206,45);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(71,14);
			this.label1.TabIndex = 9;
			this.label1.Text = "To:";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textFromAddress
			// 
			this.textFromAddress.Location = new System.Drawing.Point(278,20);
			this.textFromAddress.Name = "textFromAddress";
			this.textFromAddress.Size = new System.Drawing.Size(328,20);
			this.textFromAddress.TabIndex = 10;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(206,24);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(71,14);
			this.label3.TabIndex = 11;
			this.label3.Text = "From:";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textMsgDateTime
			// 
			this.textMsgDateTime.BackColor = System.Drawing.SystemColors.Control;
			this.textMsgDateTime.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.textMsgDateTime.Location = new System.Drawing.Point(278,4);
			this.textMsgDateTime.Name = "textMsgDateTime";
			this.textMsgDateTime.Size = new System.Drawing.Size(253,13);
			this.textMsgDateTime.TabIndex = 12;
			// 
			// labelSent
			// 
			this.labelSent.Location = new System.Drawing.Point(207,3);
			this.labelSent.Name = "labelSent";
			this.labelSent.Size = new System.Drawing.Size(71,14);
			this.labelSent.TabIndex = 13;
			this.labelSent.Text = "Date / Time:";
			this.labelSent.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(8,7);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(124,14);
			this.label4.TabIndex = 18;
			this.label4.Text = "E-mail Template";
			this.label4.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// listTemplates
			// 
			this.listTemplates.Location = new System.Drawing.Point(10,26);
			this.listTemplates.Name = "listTemplates";
			this.listTemplates.Size = new System.Drawing.Size(164,277);
			this.listTemplates.TabIndex = 17;
			this.listTemplates.DoubleClick += new System.EventHandler(this.listTemplates_DoubleClick);
			// 
			// panelTemplates
			// 
			this.panelTemplates.Controls.Add(this.butInsert);
			this.panelTemplates.Controls.Add(this.butDeleteTemplate);
			this.panelTemplates.Controls.Add(this.butAdd);
			this.panelTemplates.Controls.Add(this.label4);
			this.panelTemplates.Controls.Add(this.listTemplates);
			this.panelTemplates.Location = new System.Drawing.Point(8,9);
			this.panelTemplates.Name = "panelTemplates";
			this.panelTemplates.Size = new System.Drawing.Size(180,370);
			this.panelTemplates.TabIndex = 24;
			// 
			// butInsert
			// 
			this.butInsert.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butInsert.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.butInsert.Autosize = true;
			this.butInsert.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butInsert.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butInsert.CornerRadius = 4F;
			this.butInsert.Image = global::OpenDental.Properties.Resources.Right;
			this.butInsert.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butInsert.Location = new System.Drawing.Point(102,305);
			this.butInsert.Name = "butInsert";
			this.butInsert.Size = new System.Drawing.Size(74,26);
			this.butInsert.TabIndex = 23;
			this.butInsert.Text = "Insert";
			this.butInsert.Click += new System.EventHandler(this.butInsert_Click);
			// 
			// butDeleteTemplate
			// 
			this.butDeleteTemplate.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butDeleteTemplate.Autosize = true;
			this.butDeleteTemplate.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butDeleteTemplate.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butDeleteTemplate.CornerRadius = 4F;
			this.butDeleteTemplate.Image = global::OpenDental.Properties.Resources.deleteX;
			this.butDeleteTemplate.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butDeleteTemplate.Location = new System.Drawing.Point(7,339);
			this.butDeleteTemplate.Name = "butDeleteTemplate";
			this.butDeleteTemplate.Size = new System.Drawing.Size(75,26);
			this.butDeleteTemplate.TabIndex = 21;
			this.butDeleteTemplate.Text = "Delete";
			this.butDeleteTemplate.Click += new System.EventHandler(this.butDeleteTemplate_Click);
			// 
			// butAdd
			// 
			this.butAdd.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butAdd.Autosize = true;
			this.butAdd.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butAdd.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butAdd.CornerRadius = 4F;
			this.butAdd.Image = global::OpenDental.Properties.Resources.Add;
			this.butAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butAdd.Location = new System.Drawing.Point(7,305);
			this.butAdd.Name = "butAdd";
			this.butAdd.Size = new System.Drawing.Size(75,26);
			this.butAdd.TabIndex = 19;
			this.butAdd.Text = "&Add";
			this.butAdd.Click += new System.EventHandler(this.butAdd_Click);
			// 
			// listAttachments
			// 
			this.listAttachments.Location = new System.Drawing.Point(612,26);
			this.listAttachments.Name = "listAttachments";
			this.listAttachments.Size = new System.Drawing.Size(315,56);
			this.listAttachments.TabIndex = 28;
			this.listAttachments.DoubleClick += new System.EventHandler(this.listAttachments_DoubleClick);
			this.listAttachments.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listAttachments_MouseDown);
			// 
			// contextMenuAttachments
			// 
			this.contextMenuAttachments.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItemOpen,
            this.menuItemRename,
            this.menuItemRemove});
			this.contextMenuAttachments.Popup += new System.EventHandler(this.contextMenuAttachments_Popup);
			// 
			// menuItemOpen
			// 
			this.menuItemOpen.Index = 0;
			this.menuItemOpen.Text = "Open";
			this.menuItemOpen.Click += new System.EventHandler(this.menuItemOpen_Click);
			// 
			// menuItemRename
			// 
			this.menuItemRename.Index = 1;
			this.menuItemRename.Text = "Rename";
			this.menuItemRename.Click += new System.EventHandler(this.menuItemRename_Click);
			// 
			// menuItemRemove
			// 
			this.menuItemRemove.Index = 2;
			this.menuItemRemove.Text = "Remove";
			this.menuItemRemove.Click += new System.EventHandler(this.menuItemRemove_Click);
			// 
			// butAttach
			// 
			this.butAttach.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butAttach.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.butAttach.Autosize = true;
			this.butAttach.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butAttach.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butAttach.CornerRadius = 4F;
			this.butAttach.Location = new System.Drawing.Point(612,2);
			this.butAttach.Name = "butAttach";
			this.butAttach.Size = new System.Drawing.Size(75,22);
			this.butAttach.TabIndex = 27;
			this.butAttach.Text = "Attach...";
			this.butAttach.Click += new System.EventHandler(this.butAttach_Click);
			// 
			// butDelete
			// 
			this.butDelete.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.butDelete.Autosize = true;
			this.butDelete.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butDelete.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butDelete.CornerRadius = 4F;
			this.butDelete.Image = global::OpenDental.Properties.Resources.deleteX;
			this.butDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butDelete.Location = new System.Drawing.Point(8,628);
			this.butDelete.Name = "butDelete";
			this.butDelete.Size = new System.Drawing.Size(75,26);
			this.butDelete.TabIndex = 26;
			this.butDelete.Text = "Delete";
			this.butDelete.Click += new System.EventHandler(this.butDelete_Click);
			// 
			// butSave
			// 
			this.butSave.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.butSave.Autosize = true;
			this.butSave.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butSave.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butSave.CornerRadius = 4F;
			this.butSave.Location = new System.Drawing.Point(278,629);
			this.butSave.Name = "butSave";
			this.butSave.Size = new System.Drawing.Size(75,25);
			this.butSave.TabIndex = 25;
			this.butSave.Text = "Save";
			this.butSave.Click += new System.EventHandler(this.butSave_Click);
			// 
			// textBodyText
			// 
			this.textBodyText.AcceptsReturn = true;
			this.textBodyText.Location = new System.Drawing.Point(278,83);
			this.textBodyText.Multiline = true;
			this.textBodyText.Name = "textBodyText";
			this.textBodyText.QuickPasteType = OpenDentBusiness.QuickPasteType.Email;
			this.textBodyText.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textBodyText.Size = new System.Drawing.Size(649,537);
			this.textBodyText.TabIndex = 22;
			this.textBodyText.TextChanged += new System.EventHandler(this.textBodyText_TextChanged);
			// 
			// butSend
			// 
			this.butSend.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butSend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butSend.Autosize = true;
			this.butSend.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butSend.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butSend.CornerRadius = 4F;
			this.butSend.Location = new System.Drawing.Point(764,629);
			this.butSend.Name = "butSend";
			this.butSend.Size = new System.Drawing.Size(75,25);
			this.butSend.TabIndex = 2;
			this.butSend.Text = "&Send";
			this.butSend.Click += new System.EventHandler(this.butSend_Click);
			// 
			// butCancel
			// 
			this.butCancel.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butCancel.Autosize = true;
			this.butCancel.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butCancel.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butCancel.CornerRadius = 4F;
			this.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butCancel.Location = new System.Drawing.Point(852,629);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75,25);
			this.butCancel.TabIndex = 3;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// FormEmailMessageEdit
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.ClientSize = new System.Drawing.Size(941,670);
			this.Controls.Add(this.listAttachments);
			this.Controls.Add(this.butAttach);
			this.Controls.Add(this.butDelete);
			this.Controls.Add(this.butSave);
			this.Controls.Add(this.panelTemplates);
			this.Controls.Add(this.textBodyText);
			this.Controls.Add(this.textMsgDateTime);
			this.Controls.Add(this.textFromAddress);
			this.Controls.Add(this.textToAddress);
			this.Controls.Add(this.textSubject);
			this.Controls.Add(this.labelSent);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.butSend);
			this.Controls.Add(this.butCancel);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormEmailMessageEdit";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Edit E-mail Message";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.FormEmailMessageEdit_Closing);
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormEmailMessageEdit_FormClosing);
			this.Load += new System.EventHandler(this.FormEmailMessageEdit_Load);
			this.panelTemplates.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		private void FormEmailMessageEdit_Load(object sender, System.EventArgs e) {
			if(MessageCur.SentOrReceived==CommSentOrReceived.Neither){
				labelSent.Visible=false;
				textMsgDateTime.Text=Lan.g(this,"Unsent");
				textMsgDateTime.ForeColor=Color.Red;
			}
			else{//already sent
				panelTemplates.Visible=false;
				textMsgDateTime.Text=MessageCur.MsgDateTime.ToString();
				butAttach.Enabled=false;
				butSend.Enabled=false;//not allowed to send again.
				butSave.Enabled=false;//not allowed to save changes.
			}
			textFromAddress.Text=MessageCur.FromAddress;
			textToAddress.Text=MessageCur.ToAddress;
			textSubject.Text=MessageCur.Subject;
			textBodyText.Text=MessageCur.BodyText;
			FillAttachments();
			FillList();
		}

		private void FillAttachments(){
			listAttachments.Items.Clear();
			for(int i=0;i<MessageCur.Attachments.Count;i++){
				listAttachments.Items.Add(MessageCur.Attachments[i].DisplayedFileName);
			}
		}

		private void FillList(){
			listTemplates.Items.Clear();
			for(int i=0;i<EmailTemplates.List.Length;i++){
				listTemplates.Items.Add(EmailTemplates.List[i].Subject);
			}
		}

		private void listTemplates_DoubleClick(object sender, System.EventArgs e) {
			if(listTemplates.SelectedIndex==-1){
				return;
			}
			FormEmailTemplateEdit FormE=new FormEmailTemplateEdit();
			FormE.ETcur=EmailTemplates.List[listTemplates.SelectedIndex];
			FormE.ShowDialog();
			if(FormE.DialogResult!=DialogResult.OK){
				return;
			}
			EmailTemplates.Refresh();
			templatesChanged=true;
			FillList();
		}

		private void butAdd_Click(object sender, System.EventArgs e) {
			FormEmailTemplateEdit FormE=new FormEmailTemplateEdit();
			FormE.IsNew=true;
			FormE.ETcur=new EmailTemplate();
			FormE.ShowDialog();
			if(FormE.DialogResult!=DialogResult.OK){
				return;
			}
			EmailTemplates.Refresh();
			templatesChanged=true;
			FillList();
		}

		private void butDeleteTemplate_Click(object sender, System.EventArgs e) {
			if(listTemplates.SelectedIndex==-1){
				MessageBox.Show(Lan.g(this,"Please select an item first."));
				return;
			}
			if(MessageBox.Show(Lan.g(this,"Delete e-mail template?"),"",MessageBoxButtons.OKCancel)
				!=DialogResult.OK){
				return;
			}
			EmailTemplates.Delete(EmailTemplates.List[listTemplates.SelectedIndex]);
			EmailTemplates.Refresh();
			templatesChanged=true;
			FillList();
		}

		private void butInsert_Click(object sender, System.EventArgs e) {
			if(listTemplates.SelectedIndex==-1){
				MessageBox.Show(Lan.g(this,"Please select an item first."));
				return;
			}
			if(messageChanged){
				if(MessageBox.Show(Lan.g(this,"Replace exising e-mail text with text from the template?"),"",MessageBoxButtons.OKCancel)
					!=DialogResult.OK){
					return;
				}
			}
			textSubject.Text=EmailTemplates.List[listTemplates.SelectedIndex].Subject;
			textBodyText.Text=EmailTemplates.List[listTemplates.SelectedIndex].BodyText;
			messageChanged=false;
		}

		private void textBodyText_TextChanged(object sender, System.EventArgs e) {
			messageChanged=true;
		}

		private string GetAttachPath(){
			string attachPath;
			if(PrefB.UsingAtoZfolder) {
				attachPath=ODFileUtils.CombinePaths(FormPath.GetPreferredImagePath(),@"EmailAttachments");
			}else{
				//For users which have the A to Z folders disabled, there is no defined image path, so we
				//have to use another path. Since we have chosen the temporary directory, this means that
				//email attachements for a particular email may or may not be viewable later (i.e. in case
				//someone cleaned out their temporary files to conserve disk space).
				attachPath=Path.GetTempPath();
			}
			return attachPath;
		}

		private void butAttach_Click(object sender,EventArgs e) {
			OpenFileDialog dlg=new OpenFileDialog();
			dlg.Multiselect=true;
			Patient PatCur=Patients.GetPat(MessageCur.PatNum);
			if(PatCur.ImageFolder!=""){
				if(PrefB.UsingAtoZfolder){
					dlg.InitialDirectory=ODFileUtils.CombinePaths(new string[] {	FormPath.GetPreferredImagePath(),
																																				PatCur.ImageFolder.Substring(0,1).ToUpper(),
																																				PatCur.ImageFolder});
				}else{
					//Use the OS default directory for this type of file viewer.
					dlg.InitialDirectory="";
				}
			}
			if(dlg.ShowDialog()!=DialogResult.OK){
				return;
			}
			Random rnd=new Random();
			string newName;
			EmailAttach attach;
			string attachPath=GetAttachPath();
			if(!Directory.Exists(attachPath)){ 
				Directory.CreateDirectory(attachPath);
			}
			try{
				for(int i=0;i<dlg.FileNames.Length;i++){
					//copy the file
					newName=DateTime.Now.ToString("yyyyMMdd")+"_"+DateTime.Now.TimeOfDay.Ticks.ToString()+rnd.Next(1000).ToString();
					File.Copy(dlg.FileNames[i],ODFileUtils.CombinePaths(attachPath,newName));
					//create the attachment
					attach=new EmailAttach();
					attach.DisplayedFileName=Path.GetFileName(dlg.FileNames[i]);
					attach.ActualFileName=newName;
					MessageCur.Attachments.Add(attach);
				}
			}
			catch(Exception ex){
				MessageBox.Show(ex.Message);
			}
			FillAttachments();
		}

		private void contextMenuAttachments_Popup(object sender,EventArgs e) {
			if(listAttachments.SelectedIndex==-1) {
				menuItemOpen.Enabled=false;
				menuItemRename.Enabled=false;
				menuItemRemove.Enabled=false;
			}
			else{
				menuItemOpen.Enabled=true;
				if(MessageCur.MsgDateTime.Year>1880){//if sent
					menuItemRename.Enabled=false;
					menuItemRemove.Enabled=false;
				}
				else{
					menuItemRename.Enabled=true;
					menuItemRemove.Enabled=true;
				}
			}
		}

		private void menuItemOpen_Click(object sender,EventArgs e) {
			OpenFile();
		}

		private void menuItemRename_Click(object sender,EventArgs e) {
			InputBox input=new InputBox(Lan.g(this,"Filename"));
			input.textResult.Text=MessageCur.Attachments[listAttachments.SelectedIndex].DisplayedFileName;
			input.ShowDialog();
			if(input.DialogResult!=DialogResult.OK){
				return;
			}
			MessageCur.Attachments[listAttachments.SelectedIndex].DisplayedFileName=input.textResult.Text;
			FillAttachments();
		}

		private void menuItemRemove_Click(object sender,EventArgs e) {
			MessageCur.Attachments.RemoveAt(listAttachments.SelectedIndex);
			FillAttachments();
		}

		/*
		private void butRemove_Click(object sender,EventArgs e) {
			if(listAttachments.SelectedIndex==-1){
				MsgBox.Show(this,"Please select an item first.");
				return;
			}

			FillAttachments();
		}

		private void butRename_Click(object sender,EventArgs e) {
			if(listAttachments.SelectedIndex==-1) {
				MsgBox.Show(this,"Please select an item first.");
				return;
			}
			InputBox input=new InputBox(Lan.g(this,"Filename"));
			input.textResult.Text=
			FillAttachments();
		}

		private void butOpen_Click(object sender,EventArgs e) {
			if(listAttachments.SelectedIndex==-1) {
				MsgBox.Show(this,"Please select an item first.");
				return;
			}
			OpenFile();
		}*/

		private void listAttachments_DoubleClick(object sender,EventArgs e) {
			if(listAttachments.SelectedIndex==-1) {
				return;
			}
			OpenFile();
		}

		private void OpenFile(){
			//We have to create a copy of the file because the name is different.
			//There is also a high probability that the attachment no longer exists if
			//the A to Z folders are disabled, since the file will have originally been
			//placed in the temporary directory.
			string attachPath=GetAttachPath();
			try{
				string tempFile=ODFileUtils.CombinePaths(	Path.GetTempPath(),
																									MessageCur.Attachments[listAttachments.SelectedIndex].DisplayedFileName);
				File.Copy(
					ODFileUtils.CombinePaths(attachPath,MessageCur.Attachments[listAttachments.SelectedIndex].ActualFileName),
					tempFile,
					true);
				Process.Start(tempFile);
			}
			catch(Exception ex){
				MessageBox.Show(ex.Message);
			}
		}

		private void listAttachments_MouseDown(object sender,MouseEventArgs e) {
			//A right click also needs to select an items so that the context menu will work properly.
			if(e.Button==MouseButtons.Right) {
				int clickedIndex=listAttachments.IndexFromPoint(e.X,e.Y);
				if(clickedIndex!=-1) {
					listAttachments.SelectedIndex=clickedIndex;
				}
			}
		}

		private void butDelete_Click(object sender,EventArgs e) {
			if(IsNew){
				DialogResult=DialogResult.Cancel;
				//It will be deleted in the FormClosing() Event.
			}
			else{
				if(MsgBox.Show(this,true,"Delete this email?")){
					EmailMessages.Delete(MessageCur);
					DialogResult=DialogResult.OK;
				}
			}
		}

		private void butSave_Click(object sender,EventArgs e) {
			//this will not be available if already sent.
			SaveMsg();
			DialogResult=DialogResult.OK;
		}

		private void SaveMsg(){
			//allowed to save message with invalid fields, so no validation here.  Only validate when sending.
			MessageCur.FromAddress=textFromAddress.Text;
			MessageCur.ToAddress=textToAddress.Text;
			MessageCur.Subject=textSubject.Text;
			MessageCur.BodyText=textBodyText.Text;
			MessageCur.MsgDateTime=DateTime.Now;
			//Notice that SentOrReceived does not change here.
			if(IsNew) {
				EmailMessages.Insert(MessageCur);
				/*Commlog CommlogCur=new Commlog();
				CommlogCur.PatNum=MessageCur.PatNum;
				CommlogCur.CommDateTime=DateTime.Now;
				CommlogCur.CommType=CommItemType.Misc;
				CommlogCur.EmailMessageNum=MessageCur.EmailMessageNum;
				CommlogCur.Mode=CommItemMode.Email;
				CommlogCur.SentOrReceived=CommSentOrReceived.Sent;
				CommlogCur.Note=MessageCur.Subject;
				Commlogs.Insert(CommlogCur);*/
			}
			else {
				EmailMessages.Update(MessageCur);
			}
		}

		///<summary></summary>
		private void butSend_Click(object sender, System.EventArgs e) {
			//this will not be available if already sent.
			if(textFromAddress.Text==""
				|| textToAddress.Text=="")
			{
				MessageBox.Show("Addresses not allowed to be blank.");
				return;
			}
			if(((Pref)PrefB.HList["EmailSMTPserver"]).ValueString==""){
				MsgBox.Show(this,"You need to enter an SMTP server name in e-mail setup before you can send e-mail.");
				return;
			}
			Cursor=Cursors.WaitCursor;
			SmtpClient client=new SmtpClient(PrefB.GetString("EmailSMTPserver"),PrefB.GetInt("EmailPort"));
			//The default credentials are not used by default, according to: 
			//http://msdn2.microsoft.com/en-us/library/system.net.mail.smtpclient.usedefaultcredentials.aspx
			client.Credentials=new NetworkCredential(PrefB.GetString("EmailUsername"),PrefB.GetString("EmailPassword"));
			client.DeliveryMethod=SmtpDeliveryMethod.Network;
			MailMessage message=new MailMessage();
			Attachment attach;
			try{
				message.From=new MailAddress(textFromAddress.Text);
				message.To.Add(textToAddress.Text);//this might fail
				message.Subject=textSubject.Text;
				message.Body=textBodyText.Text;
				message.IsBodyHtml=false;
				string attachPath=GetAttachPath();
				for(int i=0;i<MessageCur.Attachments.Count;i++){
					attach=new Attachment(ODFileUtils.CombinePaths(attachPath,MessageCur.Attachments[i].ActualFileName));
							//@"C:\OpenDentalData\EmailAttachments\1");
					attach.Name=MessageCur.Attachments[i].DisplayedFileName;
						//"canadian.gif";
					message.Attachments.Add(attach);
				}
				client.Send(message);
			}
			catch(System.Exception ex){
				Cursor=Cursors.Default;
				MessageBox.Show(ex.Message);
				return;
			}
			MsgBox.Show(this,"Sent");
			Cursor=Cursors.Default;
			//MessageCur.MsgDateTime=DateTime.Now;
			MessageCur.SentOrReceived=CommSentOrReceived.Sent;
			SaveMsg();
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		private void FormEmailMessageEdit_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
			if(templatesChanged){
				DataValid.SetInvalid(InvalidTypes.Email);
			}
		}

		private void FormEmailMessageEdit_FormClosing(object sender,FormClosingEventArgs e) {
			if(DialogResult==DialogResult.OK){
				return;
			}
			if(IsNew){
				EmailMessages.Delete(MessageCur);
			}
		}

		

		

		
		

		

		

		

		

		

		


	}
}





















