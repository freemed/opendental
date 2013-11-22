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
		private TextBox textSentOrReceived;
		private Label label5;
		private WebBrowser webBrowser;
		private UI.Button butEncryptAndSend;
		private UI.Button butRawMessage;
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
			this.textSentOrReceived = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.butEncryptAndSend = new OpenDental.UI.Button();
			this.webBrowser = new System.Windows.Forms.WebBrowser();
			this.butDecrypt = new OpenDental.UI.Button();
			this.buttonFuchsMailDMF = new OpenDental.UI.Button();
			this.buttonFuchsMailDSF = new OpenDental.UI.Button();
			this.butAttach = new OpenDental.UI.Button();
			this.butDelete = new OpenDental.UI.Button();
			this.butSave = new OpenDental.UI.Button();
			this.textBodyText = new OpenDental.ODtextBox();
			this.butSend = new OpenDental.UI.Button();
			this.butCancel = new OpenDental.UI.Button();
			this.butRawMessage = new OpenDental.UI.Button();
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
			this.textSubject.TabIndex = 2;
			// 
			// textToAddress
			// 
			this.textToAddress.Location = new System.Drawing.Point(278, 62);
			this.textToAddress.Name = "textToAddress";
			this.textToAddress.Size = new System.Drawing.Size(328, 20);
			this.textToAddress.TabIndex = 3;
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
			this.textFromAddress.TabIndex = 4;
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
			this.textMsgDateTime.ForeColor = System.Drawing.Color.Red;
			this.textMsgDateTime.Location = new System.Drawing.Point(278, 25);
			this.textMsgDateTime.Name = "textMsgDateTime";
			this.textMsgDateTime.Size = new System.Drawing.Size(253, 13);
			this.textMsgDateTime.TabIndex = 12;
			this.textMsgDateTime.Text = "Unsent";
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
			this.listTemplates.TabIndex = 0;
			this.listTemplates.TabStop = false;
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
			this.panelTemplates.TabIndex = 0;
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
			this.butInsert.TabIndex = 2;
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
			this.butDeleteTemplate.TabIndex = 3;
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
			this.butAdd.TabIndex = 1;
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
			this.listAttachments.TabIndex = 0;
			this.listAttachments.TabStop = false;
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
			// butEncryptAndSend
			// 
			this.butEncryptAndSend.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butEncryptAndSend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butEncryptAndSend.Autosize = true;
			this.butEncryptAndSend.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butEncryptAndSend.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butEncryptAndSend.CornerRadius = 4F;
			this.butEncryptAndSend.Location = new System.Drawing.Point(659, 635);
			this.butEncryptAndSend.Name = "butEncryptAndSend";
			this.butEncryptAndSend.Size = new System.Drawing.Size(106, 25);
			this.butEncryptAndSend.TabIndex = 8;
			this.butEncryptAndSend.Text = "Encrypt and Send";
			this.butEncryptAndSend.Click += new System.EventHandler(this.butEncryptAndSend_Click);
			// 
			// webBrowser
			// 
			this.webBrowser.AllowNavigation = false;
			this.webBrowser.AllowWebBrowserDrop = false;
			this.webBrowser.Location = new System.Drawing.Point(226, 348);
			this.webBrowser.MinimumSize = new System.Drawing.Size(20, 20);
			this.webBrowser.Name = "webBrowser";
			this.webBrowser.ScriptErrorsSuppressed = true;
			this.webBrowser.Size = new System.Drawing.Size(46, 25);
			this.webBrowser.TabIndex = 0;
			this.webBrowser.TabStop = false;
			this.webBrowser.Visible = false;
			this.webBrowser.WebBrowserShortcutsEnabled = false;
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
			this.butDecrypt.TabIndex = 7;
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
			this.butAttach.TabIndex = 5;
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
			this.butDelete.TabIndex = 11;
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
			this.butSave.TabIndex = 6;
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
			this.textBodyText.Location = new System.Drawing.Point(278, 109);
			this.textBodyText.Name = "textBodyText";
			this.textBodyText.QuickPasteType = OpenDentBusiness.QuickPasteType.Email;
			this.textBodyText.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
			this.textBodyText.Size = new System.Drawing.Size(649, 517);
			this.textBodyText.TabIndex = 1;
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
			this.butSend.TabIndex = 9;
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
			this.butCancel.TabIndex = 10;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// butRawMessage
			// 
			this.butRawMessage.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butRawMessage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.butRawMessage.Autosize = true;
			this.butRawMessage.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butRawMessage.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butRawMessage.CornerRadius = 4F;
			this.butRawMessage.Location = new System.Drawing.Point(138, 635);
			this.butRawMessage.Name = "butRawMessage";
			this.butRawMessage.Size = new System.Drawing.Size(89, 26);
			this.butRawMessage.TabIndex = 36;
			this.butRawMessage.Text = "Raw Message";
			this.butRawMessage.Visible = false;
			this.butRawMessage.Click += new System.EventHandler(this.butRawMessage_Click);
			// 
			// FormEmailMessageEdit
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(941, 672);
			this.Controls.Add(this.butRawMessage);
			this.Controls.Add(this.butEncryptAndSend);
			this.Controls.Add(this.webBrowser);
			this.Controls.Add(this.textSentOrReceived);
			this.Controls.Add(this.label5);
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
			if(!Security.IsAuthorized(Permissions.Setup,true)) {
				butAdd.Enabled=false;
				butDeleteTemplate.Enabled=false;
			}
			Cursor=Cursors.WaitCursor;
			RefreshAll();
			Cursor=Cursors.Default;
		}

		private void RefreshAll() {
			if(MessageCur.SentOrReceived!=EmailSentOrReceived.Neither) {//sent or received (not composing)
				panelTemplates.Visible=false;
				textMsgDateTime.Text=MessageCur.MsgDateTime.ToString();
				textMsgDateTime.ForeColor=Color.Black;
				butAttach.Enabled=false;
				butEncryptAndSend.Enabled=false;//not allowed to send again.
				butSend.Enabled=false;//not allowed to send again.
				butSave.Enabled=false;//not allowed to save changes.
				butCancel.Text="Close";//When opening an email from FormEmailInbox, the email status will change to read automatically, and changing the text on the cancel button helps convey that to the user.
			}
			textSentOrReceived.Text=MessageCur.SentOrReceived.ToString();
			textFromAddress.Text=MessageCur.FromAddress;
			textToAddress.Text=MessageCur.ToAddress;
			textSubject.Text=MessageCur.Subject;
			textBodyText.Text=MessageCur.BodyText;
			FillList();
			if(PrefC.GetBool(PrefName.FuchsOptionsOn)) {
				buttonFuchsMailDMF.Visible=true;
				buttonFuchsMailDSF.Visible=true;
			}			
			labelDecrypt.Visible=false;
			butDecrypt.Visible=false;
			if(MessageCur.SentOrReceived==EmailSentOrReceived.ReceivedEncrypted) {
				labelDecrypt.Visible=true;
				butDecrypt.Visible=true;
			}
			//For all email received types, we disable most of the controls and put the form into a mostly read-only state.
			//There is no reason a user should ever edit a received message. The user can copy the content and send a new email if needed (perhaps we will have forward capabilities in the future).
			if(MessageCur.SentOrReceived==EmailSentOrReceived.ReceivedEncrypted ||
				MessageCur.SentOrReceived==EmailSentOrReceived.ReceivedDirect ||
				MessageCur.SentOrReceived==EmailSentOrReceived.ReadDirect ||
				MessageCur.SentOrReceived==EmailSentOrReceived.Received ||
				MessageCur.SentOrReceived==EmailSentOrReceived.Read ||
				MessageCur.SentOrReceived==EmailSentOrReceived.WebMailReceived ||
				MessageCur.SentOrReceived==EmailSentOrReceived.WebMailRecdRead)
			{
				butRawMessage.Visible=true;
				textBodyText.ReadOnly=true;
				textBodyText.SpellCheckIsEnabled=false;//Prevents slowness when resizing the window, because the spell checker runs each time the resize event is fired.
				//If an html body is received, then we display the body using a webbrowser control, so the user sees the message formatted as intended.
				if(MessageCur.BodyText.Trim().StartsWith("<html>")) {
					textBodyText.Visible=false;
					webBrowser.DocumentText=MessageCur.BodyText;
					webBrowser.Location=textBodyText.Location;
					webBrowser.Size=textBodyText.Size;
					webBrowser.Anchor=textBodyText.Anchor;
					webBrowser.Visible=true;
				}
			}
			FillAttachments();
			textBodyText.Select();
		}

		private void FillAttachments(){
			listAttachments.Items.Clear();
			for(int i=0;i<MessageCur.Attachments.Count;i++){
				listAttachments.Items.Add(MessageCur.Attachments[i].DisplayedFileName);
			}
			if(MessageCur.Attachments.Count>0) {
				listAttachments.SelectedIndex=0;
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
			if(!Security.IsAuthorized(Permissions.Setup)) {
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
			string attachPath=EmailMessages.GetEmailAttachPath();
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
				if(MessageCur.MsgDateTime.Year>1880){//if sent or received
					menuItemRename.Enabled=false;
					menuItemRemove.Enabled=false;
				}
				else{//composing
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
			string strFilePathAttach=ODFileUtils.CombinePaths(EmailMessages.GetEmailAttachPath(),MessageCur.Attachments[listAttachments.SelectedIndex].ActualFileName);
			try{
				if(EhrCCD.IsCcdEmailAttachment(MessageCur.Attachments[listAttachments.SelectedIndex])) {
					string strTextXml=File.ReadAllText(strFilePathAttach);
					if(EhrCCD.IsCCD(strTextXml)) {
						bool isReconcile=false;
						if(MessageCur.SentOrReceived==EmailSentOrReceived.ReadDirect || MessageCur.SentOrReceived==EmailSentOrReceived.ReceivedDirect) {
							isReconcile=true;//Do not show reconcile options when user is composing message or when viewing message that was sent. Only allow reconcile if received via Direct.
						}
						FormEhrSummaryOfCare.DisplayCCD(strTextXml,isReconcile);
						return;
					}	
				}
				//We have to create a copy of the file because the name is different.
				//There is also a high probability that the attachment no longer exists if
				//the A to Z folders are disabled, since the file will have originally been
				//placed in the temporary directory.
				string tempFile=ODFileUtils.CombinePaths(Path.GetTempPath(),MessageCur.Attachments[listAttachments.SelectedIndex].DisplayedFileName);
				File.Copy(strFilePathAttach,tempFile,true);
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

		private void butRawMessage_Click(object sender,EventArgs e) {
			MsgBoxCopyPaste msgbox=new MsgBoxCopyPaste(MessageCur.RawEmailIn);
			msgbox.ShowDialog();
		}

		private void butDecrypt_Click(object sender,EventArgs e) {
			if(!EmailMessages.IsDirectAddressTrusted(MessageCur.FromAddress)) {//Not trusted yet.
				string strTrustMessage=Lan.g(this,"The sender address must be added to your trusted addresses before you can decrypt the email")
					+". "+Lan.g(this,"Add")+" "+MessageCur.FromAddress+" "+Lan.g(this,"to trusted addresses")+"?";
				if(MessageBox.Show(strTrustMessage,"",MessageBoxButtons.OKCancel)==DialogResult.OK) {
					Cursor=Cursors.WaitCursor;
					EmailMessages.TryAddTrustDirect(MessageCur.FromAddress);
					Cursor=Cursors.Default;
					if(!EmailMessages.IsDirectAddressTrusted(MessageCur.FromAddress)) {
						MsgBox.Show(this,"Failed to trust sender because a valid certificate could not be located.");
						return;
					}
				}
			}
			Cursor=Cursors.WaitCursor;
			EmailAddress emailAddress=GetEmailAddress();
			try {
				MessageCur=EmailMessages.ProcessRawEmailMessage(MessageCur.BodyText,MessageCur.EmailMessageNum,emailAddress);//If decryption is successful, sets status to ReceivedDirect.
				//The Direct message was decrypted.
				EmailMessages.UpdateSentOrReceivedRead(MessageCur);//Mark read, because we are already viewing the message within the current window.					
				RefreshAll();
			}
			catch(Exception ex) {
				MessageBox.Show(Lan.g(this,"Decryption failed.")+"\r\n"+ex.Message);
				//Error=InvalidEncryption: means that someone used the wrong certificate when sending the email to this inbox, and we tried to decrypt with a different certificate.
				//Error=NoTrustedRecipients: means the sender is not added to the trust anchors in mmc.
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

		private EmailAddress GetEmailAddress() {
			if(PatCur==null) {//can happen if sending deposit slip by email
				return EmailAddresses.GetByClinic(0);//gets the practice default address
			}
			return EmailAddresses.GetByClinic(PatCur.ClinicNum);
		}

		private void butEncryptAndSend_Click(object sender,EventArgs e) {
			//this will not be available if already sent.
			if(textFromAddress.Text==""
				|| textToAddress.Text=="") {
				MessageBox.Show("Addresses not allowed to be blank.");
				return;
			}
			EmailAddress emailAddressFrom=GetEmailAddress();
			if(emailAddressFrom.SMTPserver=="") {
				MsgBox.Show(this,"The email address in email setup must have an SMTP server.");
				return;
			}
			Cursor=Cursors.WaitCursor;
			MessageCur.SentOrReceived=EmailSentOrReceived.SentDirect;
			SaveMsg();
			try {
				string strErrors=EmailMessages.SendEmailDirect(MessageCur,emailAddressFrom);
				if(strErrors!="") {
					Cursor=Cursors.Default;
					MessageBox.Show(strErrors);
					return;
				}
				else {
					MsgBox.Show(this,"Sent");
				}
			}
			catch(Exception ex) {
				Cursor=Cursors.Default;
				MessageBox.Show(ex.Message);
				return;
			}
			Cursor=Cursors.Default;
			DialogResult=DialogResult.OK;
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
			if(EhrCCD.HasCcdEmailAttachment(MessageCur)) {
				MsgBox.Show(this,"The email has a summary of care attachment which may contain sensitive patient data.  Use the Encrypt and Send button instead.");
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
				EmailMessages.SendEmailUnsecure(MessageCur,emailAddress);
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





















