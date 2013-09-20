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
using System.Xml;
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
		private OpenDental.UI.Button buttonFuchsMailDSF;
		private OpenDental.UI.Button buttonFuchsMailDMF;
		//private int PatNum;
		private EmailMessage MessageCur;
		private Label labelDecrypt;
		private UI.Button butDecrypt;
		private UI.Button butShowXhtml;
		private TextBox textSentOrReceived;
		private Label label5;
		///<summary>Used when attaching to get AtoZ folder, and when sending to get Clinic.</summary>
		private Patient PatCur;

		///<summary></summary>
		public FormEmailMessageEdit(EmailMessage messageCur){
			InitializeComponent();// Required for Windows Form Designer support
			Lan.F(this);
			MessageCur=messageCur.Copy();
			PatCur=Patients.GetPat(messageCur.PatNum);//we could just as easily pass this in.
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
			this.labelDecrypt = new System.Windows.Forms.Label();
			this.butDecrypt = new OpenDental.UI.Button();
			this.buttonFuchsMailDMF = new OpenDental.UI.Button();
			this.buttonFuchsMailDSF = new OpenDental.UI.Button();
			this.butAttach = new OpenDental.UI.Button();
			this.butDelete = new OpenDental.UI.Button();
			this.butSave = new OpenDental.UI.Button();
			this.textBodyText = new OpenDental.ODtextBox();
			this.butSend = new OpenDental.UI.Button();
			this.butCancel = new OpenDental.UI.Button();
			this.butShowXhtml = new OpenDental.UI.Button();
			this.textSentOrReceived = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.panelTemplates.SuspendLayout();
			this.SuspendLayout();
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(210, 85);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(69, 14);
			this.label2.TabIndex = 3;
			this.label2.Text = "Subject:";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textSubject
			// 
			this.textSubject.Location = new System.Drawing.Point(278, 83);
			this.textSubject.Name = "textSubject";
			this.textSubject.Size = new System.Drawing.Size(328, 20);
			this.textSubject.TabIndex = 1;
			// 
			// textToAddress
			// 
			this.textToAddress.Location = new System.Drawing.Point(278, 62);
			this.textToAddress.Name = "textToAddress";
			this.textToAddress.Size = new System.Drawing.Size(328, 20);
			this.textToAddress.TabIndex = 2;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(206, 66);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(71, 14);
			this.label1.TabIndex = 9;
			this.label1.Text = "To:";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textFromAddress
			// 
			this.textFromAddress.Location = new System.Drawing.Point(278, 41);
			this.textFromAddress.Name = "textFromAddress";
			this.textFromAddress.Size = new System.Drawing.Size(328, 20);
			this.textFromAddress.TabIndex = 10;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(206, 45);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(71, 14);
			this.label3.TabIndex = 11;
			this.label3.Text = "From:";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textMsgDateTime
			// 
			this.textMsgDateTime.BackColor = System.Drawing.SystemColors.Control;
			this.textMsgDateTime.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.textMsgDateTime.Location = new System.Drawing.Point(278, 25);
			this.textMsgDateTime.Name = "textMsgDateTime";
			this.textMsgDateTime.Size = new System.Drawing.Size(253, 13);
			this.textMsgDateTime.TabIndex = 12;
			// 
			// labelSent
			// 
			this.labelSent.Location = new System.Drawing.Point(207, 24);
			this.labelSent.Name = "labelSent";
			this.labelSent.Size = new System.Drawing.Size(71, 14);
			this.labelSent.TabIndex = 13;
			this.labelSent.Text = "Date / Time:";
			this.labelSent.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(8, 7);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(124, 14);
			this.label4.TabIndex = 18;
			this.label4.Text = "E-mail Template";
			this.label4.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// listTemplates
			// 
			this.listTemplates.Location = new System.Drawing.Point(10, 26);
			this.listTemplates.Name = "listTemplates";
			this.listTemplates.Size = new System.Drawing.Size(164, 277);
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
			this.panelTemplates.Location = new System.Drawing.Point(8, 9);
			this.panelTemplates.Name = "panelTemplates";
			this.panelTemplates.Size = new System.Drawing.Size(180, 370);
			this.panelTemplates.TabIndex = 24;
			// 
			// butInsert
			// 
			this.butInsert.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butInsert.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.butInsert.Autosize = true;
			this.butInsert.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butInsert.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butInsert.CornerRadius = 4F;
			this.butInsert.Image = global::OpenDental.Properties.Resources.Right;
			this.butInsert.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butInsert.Location = new System.Drawing.Point(102, 305);
			this.butInsert.Name = "butInsert";
			this.butInsert.Size = new System.Drawing.Size(74, 26);
			this.butInsert.TabIndex = 23;
			this.butInsert.Text = "Insert";
			this.butInsert.Click += new System.EventHandler(this.butInsert_Click);
			// 
			// butDeleteTemplate
			// 
			this.butDeleteTemplate.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butDeleteTemplate.Autosize = true;
			this.butDeleteTemplate.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butDeleteTemplate.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butDeleteTemplate.CornerRadius = 4F;
			this.butDeleteTemplate.Image = global::OpenDental.Properties.Resources.deleteX;
			this.butDeleteTemplate.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butDeleteTemplate.Location = new System.Drawing.Point(7, 339);
			this.butDeleteTemplate.Name = "butDeleteTemplate";
			this.butDeleteTemplate.Size = new System.Drawing.Size(75, 26);
			this.butDeleteTemplate.TabIndex = 21;
			this.butDeleteTemplate.Text = "Delete";
			this.butDeleteTemplate.Click += new System.EventHandler(this.butDeleteTemplate_Click);
			// 
			// butAdd
			// 
			this.butAdd.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butAdd.Autosize = true;
			this.butAdd.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butAdd.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butAdd.CornerRadius = 4F;
			this.butAdd.Image = global::OpenDental.Properties.Resources.Add;
			this.butAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butAdd.Location = new System.Drawing.Point(7, 305);
			this.butAdd.Name = "butAdd";
			this.butAdd.Size = new System.Drawing.Size(75, 26);
			this.butAdd.TabIndex = 19;
			this.butAdd.Text = "&Add";
			this.butAdd.Click += new System.EventHandler(this.butAdd_Click);
			// 
			// listAttachments
			// 
			this.listAttachments.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.listAttachments.Location = new System.Drawing.Point(612, 21);
			this.listAttachments.Name = "listAttachments";
			this.listAttachments.Size = new System.Drawing.Size(315, 82);
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
			// labelDecrypt
			// 
			this.labelDecrypt.Location = new System.Drawing.Point(5, 401);
			this.labelDecrypt.Name = "labelDecrypt";
			this.labelDecrypt.Size = new System.Drawing.Size(267, 59);
			this.labelDecrypt.TabIndex = 31;
			this.labelDecrypt.Text = "Previous attempts to decrypt this message have failed.\r\nDecryption usually fails " +
    "when your private decryption key is not installed on the local computer.\r\nUse th" +
    "e Decrypt button to try again.";
			this.labelDecrypt.Visible = false;
			// 
			// butDecrypt
			// 
			this.butDecrypt.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butDecrypt.Autosize = true;
			this.butDecrypt.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butDecrypt.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butDecrypt.CornerRadius = 4F;
			this.butDecrypt.Location = new System.Drawing.Point(8, 463);
			this.butDecrypt.Name = "butDecrypt";
			this.butDecrypt.Size = new System.Drawing.Size(75, 25);
			this.butDecrypt.TabIndex = 32;
			this.butDecrypt.Text = "Decrypt";
			this.butDecrypt.Visible = false;
			this.butDecrypt.Click += new System.EventHandler(this.butDecrypt_Click);
			// 
			// buttonFuchsMailDMF
			// 
			this.buttonFuchsMailDMF.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.buttonFuchsMailDMF.Autosize = true;
			this.buttonFuchsMailDMF.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.buttonFuchsMailDMF.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.buttonFuchsMailDMF.CornerRadius = 4F;
			this.buttonFuchsMailDMF.Location = new System.Drawing.Point(197, 156);
			this.buttonFuchsMailDMF.Name = "buttonFuchsMailDMF";
			this.buttonFuchsMailDMF.Size = new System.Drawing.Size(75, 22);
			this.buttonFuchsMailDMF.TabIndex = 30;
			this.buttonFuchsMailDMF.Text = "To DMF";
			this.buttonFuchsMailDMF.Visible = false;
			this.buttonFuchsMailDMF.Click += new System.EventHandler(this.buttonFuchsMailDMF_Click);
			// 
			// buttonFuchsMailDSF
			// 
			this.buttonFuchsMailDSF.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.buttonFuchsMailDSF.Autosize = true;
			this.buttonFuchsMailDSF.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.buttonFuchsMailDSF.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.buttonFuchsMailDSF.CornerRadius = 4F;
			this.buttonFuchsMailDSF.Location = new System.Drawing.Point(197, 128);
			this.buttonFuchsMailDSF.Name = "buttonFuchsMailDSF";
			this.buttonFuchsMailDSF.Size = new System.Drawing.Size(75, 22);
			this.buttonFuchsMailDSF.TabIndex = 29;
			this.buttonFuchsMailDSF.Text = "To DSF";
			this.buttonFuchsMailDSF.Visible = false;
			this.buttonFuchsMailDSF.Click += new System.EventHandler(this.buttonFuchsMailDSF_Click);
			// 
			// butAttach
			// 
			this.butAttach.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butAttach.Autosize = true;
			this.butAttach.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butAttach.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butAttach.CornerRadius = 4F;
			this.butAttach.Location = new System.Drawing.Point(612, 0);
			this.butAttach.Name = "butAttach";
			this.butAttach.Size = new System.Drawing.Size(75, 20);
			this.butAttach.TabIndex = 27;
			this.butAttach.Text = "Attach...";
			this.butAttach.Click += new System.EventHandler(this.butAttach_Click);
			// 
			// butDelete
			// 
			this.butDelete.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.butDelete.Autosize = true;
			this.butDelete.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butDelete.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butDelete.CornerRadius = 4F;
			this.butDelete.Image = global::OpenDental.Properties.Resources.deleteX;
			this.butDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butDelete.Location = new System.Drawing.Point(8, 635);
			this.butDelete.Name = "butDelete";
			this.butDelete.Size = new System.Drawing.Size(75, 26);
			this.butDelete.TabIndex = 26;
			this.butDelete.Text = "Delete";
			this.butDelete.Click += new System.EventHandler(this.butDelete_Click);
			// 
			// butSave
			// 
			this.butSave.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.butSave.Autosize = true;
			this.butSave.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butSave.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butSave.CornerRadius = 4F;
			this.butSave.Location = new System.Drawing.Point(278, 635);
			this.butSave.Name = "butSave";
			this.butSave.Size = new System.Drawing.Size(75, 25);
			this.butSave.TabIndex = 25;
			this.butSave.Text = "Save";
			this.butSave.Click += new System.EventHandler(this.butSave_Click);
			// 
			// textBodyText
			// 
			this.textBodyText.AcceptsTab = true;
			this.textBodyText.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBodyText.DetectUrls = false;
			this.textBodyText.Location = new System.Drawing.Point(278, 104);
			this.textBodyText.Name = "textBodyText";
			this.textBodyText.QuickPasteType = OpenDentBusiness.QuickPasteType.Email;
			this.textBodyText.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
			this.textBodyText.Size = new System.Drawing.Size(649, 517);
			this.textBodyText.TabIndex = 0;
			this.textBodyText.Text = "";
			this.textBodyText.TextChanged += new System.EventHandler(this.textBodyText_TextChanged);
			// 
			// butSend
			// 
			this.butSend.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butSend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butSend.Autosize = true;
			this.butSend.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butSend.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butSend.CornerRadius = 4F;
			this.butSend.Location = new System.Drawing.Point(771, 635);
			this.butSend.Name = "butSend";
			this.butSend.Size = new System.Drawing.Size(75, 25);
			this.butSend.TabIndex = 2;
			this.butSend.Text = "&Send";
			this.butSend.Click += new System.EventHandler(this.butSend_Click);
			// 
			// butCancel
			// 
			this.butCancel.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butCancel.Autosize = true;
			this.butCancel.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butCancel.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butCancel.CornerRadius = 4F;
			this.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butCancel.Location = new System.Drawing.Point(852, 635);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75, 25);
			this.butCancel.TabIndex = 3;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// butShowXhtml
			// 
			this.butShowXhtml.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butShowXhtml.Autosize = true;
			this.butShowXhtml.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butShowXhtml.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butShowXhtml.CornerRadius = 4F;
			this.butShowXhtml.Location = new System.Drawing.Point(531, 636);
			this.butShowXhtml.Name = "butShowXhtml";
			this.butShowXhtml.Size = new System.Drawing.Size(75, 25);
			this.butShowXhtml.TabIndex = 33;
			this.butShowXhtml.Text = "Show xhtml";
			this.butShowXhtml.Visible = false;
			this.butShowXhtml.Click += new System.EventHandler(this.butShowXhtml_Click);
			// 
			// textSentOrReceived
			// 
			this.textSentOrReceived.BackColor = System.Drawing.SystemColors.Control;
			this.textSentOrReceived.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.textSentOrReceived.Location = new System.Drawing.Point(278, 6);
			this.textSentOrReceived.Name = "textSentOrReceived";
			this.textSentOrReceived.Size = new System.Drawing.Size(253, 13);
			this.textSentOrReceived.TabIndex = 34;
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(194, 5);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(84, 14);
			this.label5.TabIndex = 35;
			this.label5.Text = "Sent/Received:";
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// FormEmailMessageEdit
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(941, 672);
			this.Controls.Add(this.textSentOrReceived);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.butShowXhtml);
			this.Controls.Add(this.butDecrypt);
			this.Controls.Add(this.labelDecrypt);
			this.Controls.Add(this.buttonFuchsMailDMF);
			this.Controls.Add(this.buttonFuchsMailDSF);
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
			this.MinimumSize = new System.Drawing.Size(875, 575);
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
			RefreshAll();
		}

		private void RefreshAll() {
			if(MessageCur.SentOrReceived==EmailSentOrReceived.Neither) {
				labelSent.Visible=false;
				textMsgDateTime.Text=Lan.g(this,"Unsent");
				textMsgDateTime.ForeColor=Color.Red;
			}
			else {//already sent or received
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
			FillList();
			if(PrefC.GetBool(PrefName.FuchsOptionsOn)) {
				buttonFuchsMailDMF.Visible=true;
				buttonFuchsMailDSF.Visible=true;
			}
			textSentOrReceived.Text=MessageCur.SentOrReceived.ToString();
			if(MessageCur.SentOrReceived==EmailSentOrReceived.ReceivedEncrypted) {
				labelDecrypt.Visible=true;
				butDecrypt.Visible=true;
			}
			else {
				labelDecrypt.Visible=false;
				butDecrypt.Visible=false;
			}
			//For all email received types, we disable most of the controls and put the form into a mostly read-only state.
			//There is no reason a user should ever edit a received message. The user can copy the content and send a new email if needed (perhaps we will have forward capabilities in the future).
			if(MessageCur.SentOrReceived==EmailSentOrReceived.ReceivedEncrypted ||
				MessageCur.SentOrReceived==EmailSentOrReceived.ReceivedDirect ||
				MessageCur.SentOrReceived==EmailSentOrReceived.ReadDirect ||
				MessageCur.SentOrReceived==EmailSentOrReceived.Received ||
				MessageCur.SentOrReceived==EmailSentOrReceived.Read ||
				MessageCur.SentOrReceived==EmailSentOrReceived.WebMailReceived ||
				MessageCur.SentOrReceived==EmailSentOrReceived.WebMailRecdRead) {
				textBodyText.ReadOnly=true;
				textBodyText.SpellCheckIsEnabled=false;//Prevents slowness when resizing the window, because the spell checker runs each time the resize event is fired.
				butSave.Visible=false;
				butSend.Visible=false;
				butAttach.Visible=false;//We do not allow changing the attachments on an email which was received.
				butCancel.Text="Close";//When opening an email from FormEmailInbox, the email status will change to read automatically, and changing the text on the cancel button helps convey that to the user.
			}
			//Attachments are being created for received direct messages, but nothing else yet.
			if(MessageCur.SentOrReceived==EmailSentOrReceived.ReceivedEncrypted ||
				MessageCur.SentOrReceived==EmailSentOrReceived.Received ||
				MessageCur.SentOrReceived==EmailSentOrReceived.Read ||
				MessageCur.SentOrReceived==EmailSentOrReceived.WebMailReceived ||
				MessageCur.SentOrReceived==EmailSentOrReceived.WebMailRecdRead) {
				listAttachments.Visible=false;//We are not able to receive incoming attachments for these types of messages yet.
			}
			if(listAttachments.Visible) {
				MessageCur.Attachments=EmailAttaches.GetForEmail(MessageCur.EmailMessageNum);
			}
			FillAttachments();
			if((MessageCur.SentOrReceived==EmailSentOrReceived.ReceivedDirect || MessageCur.SentOrReceived==EmailSentOrReceived.ReadDirect) && EHR.FormSummaryOfCare.IsCCD(textBodyText.Text)) {
				butShowXhtml.Visible=true;
			}
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
			EmailTemplates.RefreshCache();
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
			EmailTemplates.RefreshCache();
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
			EmailTemplates.RefreshCache();
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

		///<summary>Deprecated.  Use EhrEmail.GetEmailAttachPath().  This is a stupid place for this, but keeping it around because it's used from many places.</summary>
		public static string GetAttachPath(){
			string attachPath;
			if(PrefC.AtoZfolderUsed) {
				attachPath=ODFileUtils.CombinePaths(ImageStore.GetPreferredAtoZpath(),"EmailAttachments");
				if(!Directory.Exists(attachPath)) {
					Directory.CreateDirectory(attachPath);
				}
			}
			else{
				//For users who have the A to Z folders disabled, there is no defined image path, so we
				//have to use a temp path.  This means that the attachments might be available immediately afterward,
				//but probably not later.
				attachPath=Path.GetTempPath();
			}
			return attachPath;
		}

		private void butAttach_Click(object sender,EventArgs e) {
			OpenFileDialog dlg=new OpenFileDialog();
			dlg.Multiselect=true;
			//PatCur=Patients.GetPat(MessageCur.PatNum);
			if(PatCur.ImageFolder!=""){
				if(PrefC.AtoZfolderUsed){
					dlg.InitialDirectory=ODFileUtils.CombinePaths(ImageStore.GetPreferredAtoZpath(),
																																				PatCur.ImageFolder.Substring(0,1).ToUpper(),
																																				PatCur.ImageFolder);
				}
				else{
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
			try{
				for(int i=0;i<dlg.FileNames.Length;i++){
					//copy the file
					newName=DateTime.Now.ToString("yyyyMMdd")+"_"+DateTime.Now.TimeOfDay.Ticks.ToString()+rnd.Next(1000).ToString()+Path.GetExtension(dlg.FileNames[i]);
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
				string tempFile
					=ODFileUtils.CombinePaths(Path.GetTempPath(),MessageCur.Attachments[listAttachments.SelectedIndex].DisplayedFileName);
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

		private void buttonFuchsMailDSF_Click(object sender,EventArgs e) {
			textSubject.Text="Statement to DSF";
			textBodyText.Text="For accounting, sent statment to skimom@springfielddental.net"+textBodyText.Text;
			textToAddress.Text="skimom@springfielddental.net";
			messageChanged=false;
		}

		private void buttonFuchsMailDMF_Click(object sender,EventArgs e) {
			textToAddress.Text="smilecouple@yahoo.com";
			textSubject.Text="Statement to DMF";
			textBodyText.Text="For accounting, sent statment to smilecouple@yahoo.com"+textBodyText.Text;
			messageChanged=false;

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

		private void butDecrypt_Click(object sender,EventArgs e) {
			Cursor=Cursors.WaitCursor;
			EmailAddress emailAddress=GetEmailAddress();
			try {
				MessageCur=EhrEmail.ProcessRawEmailMessage(MessageCur.BodyText,MessageCur.EmailMessageNum,emailAddress);//If successful, sets status to ReceivedDirect.
				MessageCur.SentOrReceived=EmailSentOrReceived.ReadDirect;//Because we are already viewing the message within the current window.
				EmailMessages.Update(MessageCur);
				RefreshAll();
			}
			catch(Exception ex) {
				MessageBox.Show(ex.Message);
			}
			Cursor=Cursors.Default;
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
			}
			else {
				EmailMessages.Update(MessageCur);
			}
		}

		private void butShowXhtml_Click(object sender,EventArgs e) {
			try {
				EHR.FormSummaryOfCare.DisplayCCD(MessageCur.BodyText);
			}
			catch(Exception ex) {
				MessageBox.Show(Lan.g(this,"Failed to display.")+" "+ex.Message);
			}
		}

		private EmailAddress GetEmailAddress() {
			if(PatCur==null) {//can happen if sending deposit slip by email
				return EmailAddresses.GetByClinic(0);//gets the practice default address
			}
			return EmailAddresses.GetByClinic(PatCur.ClinicNum);
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
			EmailAddress emailAddress=GetEmailAddress();
			if(emailAddress.SMTPserver==""){
				MsgBox.Show(this,"The email address in email setup must have an SMTP server.");
				return;
			}
			Cursor=Cursors.WaitCursor;
			MessageCur.SentOrReceived=EmailSentOrReceived.Sent;
			SaveMsg();
			try{
				SendEmail(MessageCur,emailAddress);
				MsgBox.Show(this,"Sent");
			}
			catch(Exception ex){
				Cursor=Cursors.Default;
				MessageBox.Show(ex.Message);
				return;
			}
			Cursor=Cursors.Default;
			//MessageCur.MsgDateTime=DateTime.Now;
			DialogResult=DialogResult.OK;
		}

		/// <summary>This is used from wherever email needs to be sent throughout the program.</summary>
		public static void SendEmail(EmailMessage emailMessage,EmailAddress emailAddress){
			if(emailAddress.ServerPort==465) {//implicit
				//uses System.Web.Mail, which is marked as deprecated, but still supports implicit
				System.Web.Mail.MailMessage message = new System.Web.Mail.MailMessage();
				message.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpserver",emailAddress.SMTPserver);
				message.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpserverport","465");
				message.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendusing","2");//sendusing: cdoSendUsingPort, value 2, for sending the message using the network.
				message.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpauthenticate","1");//0=anonymous,1=clear text auth,2=context
				message.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendusername",emailAddress.EmailUsername);
				message.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendpassword",emailAddress.EmailPassword);
				//if(PrefC.GetBool(PrefName.EmailUseSSL)) {
				message.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpusessl","true");//false was also tested and does not work
				message.From=emailMessage.FromAddress;
				message.To=emailMessage.ToAddress;
				message.Subject=emailMessage.Subject;
				message.Body=emailMessage.BodyText;
				//message.Cc=;
				//message.Bcc=;
				//message.UrlContentBase=;
				//message.UrlContentLocation=;
				message.BodyEncoding=System.Text.Encoding.UTF8;
				message.BodyFormat=System.Web.Mail.MailFormat.Text;//or .Html
				string attachPath=GetAttachPath();
				System.Web.Mail.MailAttachment attach;
				//foreach (string sSubstr in sAttach.Split(delim)){
				for(int i=0;i<emailMessage.Attachments.Count;i++) {
					attach=new System.Web.Mail.MailAttachment(ODFileUtils.CombinePaths(attachPath,emailMessage.Attachments[i].ActualFileName));
					//no way to set displayed filename
					message.Attachments.Add(attach);
				}
				System.Web.Mail.SmtpMail.SmtpServer=emailAddress.SMTPserver+":465";//"smtp.gmail.com:465";
				System.Web.Mail.SmtpMail.Send(message);
			}
			else {//explicit default port 587 
				SmtpClient client=new SmtpClient(emailAddress.SMTPserver,emailAddress.ServerPort);
				//The default credentials are not used by default, according to: 
				//http://msdn2.microsoft.com/en-us/library/system.net.mail.smtpclient.usedefaultcredentials.aspx
				client.Credentials=new NetworkCredential(emailAddress.EmailUsername,emailAddress.EmailPassword);
				client.DeliveryMethod=SmtpDeliveryMethod.Network;
				client.EnableSsl=emailAddress.UseSSL;
				client.Timeout=180000;//3 minutes
				MailMessage message=new MailMessage();
				Attachment attach;
				message.From=new MailAddress(emailMessage.FromAddress);
				message.To.Add(emailMessage.ToAddress);
				message.Subject=emailMessage.Subject;
				message.Body=emailMessage.BodyText;
				message.IsBodyHtml=false;
				string attachPath=GetAttachPath();
				for(int i=0;i<emailMessage.Attachments.Count;i++) {
					attach=new Attachment(ODFileUtils.CombinePaths(attachPath,emailMessage.Attachments[i].ActualFileName));
					//@"C:\OpenDentalData\EmailAttachments\1");
					attach.Name=emailMessage.Attachments[i].DisplayedFileName;
					//"canadian.gif";
					message.Attachments.Add(attach);
				}
				client.Send(message);
			}
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		private void FormEmailMessageEdit_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
			if(templatesChanged){
				DataValid.SetInvalid(InvalidType.Email);
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





















