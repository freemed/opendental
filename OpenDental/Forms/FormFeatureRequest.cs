using System;
using System.Diagnostics;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Xml;
using System.Windows.Forms;
using OpenDentBusiness;
using CodeBase;
using OpenDental.UI;

namespace OpenDental{
	/// <summary>
	/// Summary description for FormBasicTemplate.
	/// </summary>
	public class FormFeatureRequests:System.Windows.Forms.Form {
		private OpenDental.UI.Button butClose;
		private System.Windows.Forms.Label label1;
		private IContainer components;
		private Label label2;
		private Label label5;
		private TextBox textSearch;
		private Label label3;
		private OpenDental.UI.ODGrid gridMain;
		private OpenDental.UI.Button buttonAdd;
		private Label label4;
		private TextBox textConnectionMessage;//OD1

		///<summary></summary>
		public FormFeatureRequests()
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormFeatureRequests));
			this.label1 = new System.Windows.Forms.Label();
			this.textConnectionMessage = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.textSearch = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.gridMain = new OpenDental.UI.ODGrid();
			this.butClose = new OpenDental.UI.Button();
			this.buttonAdd = new OpenDental.UI.Button();
			this.label4 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(0,0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(100,23);
			this.label1.TabIndex = 0;
			// 
			// textConnectionMessage
			// 
			this.textConnectionMessage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textConnectionMessage.BackColor = System.Drawing.SystemColors.Control;
			this.textConnectionMessage.Location = new System.Drawing.Point(506,17);
			this.textConnectionMessage.Name = "textConnectionMessage";
			this.textConnectionMessage.ReadOnly = true;
			this.textConnectionMessage.Size = new System.Drawing.Size(342,20);
			this.textConnectionMessage.TabIndex = 50;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(9,1);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(648,16);
			this.label2.TabIndex = 51;
			this.label2.Text = "Vote for your favorite features here.  Please remember that we cannot ever give a" +
    "ny time estimates.";
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(1,17);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(110,18);
			this.label5.TabIndex = 56;
			this.label5.Text = "Search";
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textSearch
			// 
			this.textSearch.Location = new System.Drawing.Point(112,17);
			this.textSearch.Name = "textSearch";
			this.textSearch.Size = new System.Drawing.Size(179,20);
			this.textSearch.TabIndex = 57;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(394,17);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(110,18);
			this.label3.TabIndex = 58;
			this.label3.Text = "Connection";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// gridMain
			// 
			this.gridMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gridMain.HScrollVisible = true;
			this.gridMain.Location = new System.Drawing.Point(12,40);
			this.gridMain.Name = "gridMain";
			this.gridMain.ScrollValue = 0;
			this.gridMain.Size = new System.Drawing.Size(836,582);
			this.gridMain.TabIndex = 59;
			this.gridMain.Title = "Feature Requests";
			this.gridMain.TranslationName = null;
			// 
			// butClose
			// 
			this.butClose.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butClose.Autosize = true;
			this.butClose.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butClose.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butClose.CornerRadius = 4F;
			this.butClose.Location = new System.Drawing.Point(773,624);
			this.butClose.Name = "butClose";
			this.butClose.Size = new System.Drawing.Size(75,24);
			this.butClose.TabIndex = 0;
			this.butClose.Text = "&Close";
			this.butClose.Click += new System.EventHandler(this.butClose_Click);
			// 
			// buttonAdd
			// 
			this.buttonAdd.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.buttonAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonAdd.Autosize = true;
			this.buttonAdd.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.buttonAdd.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.buttonAdd.CornerRadius = 4F;
			this.buttonAdd.Image = global::OpenDental.Properties.Resources.Add;
			this.buttonAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.buttonAdd.Location = new System.Drawing.Point(12,624);
			this.buttonAdd.Name = "buttonAdd";
			this.buttonAdd.Size = new System.Drawing.Size(75,24);
			this.buttonAdd.TabIndex = 60;
			this.buttonAdd.Text = "Add";
			this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(91,627);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(180,18);
			this.label4.TabIndex = 61;
			this.label4.Text = "Required to search first";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// FormFeatureRequests
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(857,651);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.buttonAdd);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.textSearch);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.textConnectionMessage);
			this.Controls.Add(this.gridMain);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.butClose);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "FormFeatureRequests";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Feature Requests";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.Load += new System.EventHandler(this.FormFeatureRequest_Load);
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormUpdate_FormClosing);
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		private void FormFeatureRequest_Load(object sender, System.EventArgs e) {
			/*
				if(Security.IsAuthorized(Permissions.Setup,true)) {
					butCheck2.Visible=true;
				}
				else {
					textConnectionMessage.Text=Lan.g(this,"Not authorized for")+" "+GroupPermissions.GetDesc(Permissions.Setup);
				}
			*/
			if(!Synch()){
				return;
			}
			FillGrid();
		}

		private bool Synch(){
			Cursor=Cursors.WaitCursor;
			textConnectionMessage.Text=Lan.g(this,"Attempting to connect to web service......");
			Application.DoEvents();
			//prepare the xml document to send--------------------------------------------------------------------------------------
			XmlWriterSettings settings = new XmlWriterSettings();
			settings.Indent = true;
			settings.IndentChars = ("    ");
			StringBuilder strbuild=new StringBuilder();
			using(XmlWriter writer=XmlWriter.Create(strbuild,settings)){
				writer.WriteStartElement("FeatureSynch");
				writer.WriteStartElement("RegistrationKey");
				writer.WriteString(PrefC.GetString("RegistrationKey"));
				writer.WriteEndElement();
				writer.WriteStartElement("SinceDateT");
				writer.WriteString("FeatureRequestLastSynchDateT");
				writer.WriteEndElement();
				writer.WriteEndElement();
			}
			#if DEBUG
				OpenDental.localhost.Service1 updateService=new OpenDental.localhost.Service1();
			#else
				OpenDental.customerUpdates.Service1 updateService=new OpenDental.customerUpdates.Service1();
			#endif
			updateService.Url=PrefC.GetString("UpdateServerAddress");
			//Send the message and get the result--------------------------------------------------------------------------------------
			string result="";
			try {
				result=updateService.RequestUpdate(strbuild.ToString());
			}
			catch(Exception ex) {
				Cursor=Cursors.Default;
				MessageBox.Show("Error: "+ex.Message);
				return false;
			}
			textConnectionMessage.Text=Lan.g(this,"Connection successful.");
			Application.DoEvents();
			Cursor=Cursors.Default;
			//MessageBox.Show(result);
			XmlDocument doc=new XmlDocument();
			doc.LoadXml(result);
			//Process errors------------------------------------------------------------------------------------------------------------
			XmlNode node=doc.SelectSingleNode("//Error");
			if(node!=null) {
				textConnectionMessage.Text=node.InnerText;
				MessageBox.Show(node.InnerText,"Error");
				return false;
			}
			node=doc.SelectSingleNode("//KeyDisabled");
			if(node==null) {
				//no error, and no disabled message
				if(Prefs.UpdateBool("RegistrationKeyIsDisabled",false)) {//this is one of two places in the program where this happens.
					DataValid.SetInvalid(InvalidType.Prefs);
				}
			}
			else {
				textConnectionMessage.Text=node.InnerText;
				MessageBox.Show(node.InnerText);
				if(Prefs.UpdateBool("RegistrationKeyIsDisabled",true)) {//this is one of two places in the program where this happens.
					DataValid.SetInvalid(InvalidType.Prefs);
				}
				return false;
			}
			//Process a valid return value------------------------------------------------------------------------------------------------
			/*node=doc.SelectSingleNode("//BuildAvailable");
			buildAvailable="";
			buildAvailableCode="";
			if(node!=null){
				groupBuild.Visible=true;
				node=doc.SelectSingleNode("//BuildAvailable/Display");
				if(node!=null){
					textBuild.Text=node.InnerText;
				}
				node=doc.SelectSingleNode("//BuildAvailable/MajMinBuildF");
				if(node!=null){
					buildAvailable=node.InnerText;
				}
				node=doc.SelectSingleNode("//BuildAvailable/UpdateCode");
				if(node!=null) {
					buildAvailableCode=node.InnerText;
				}
			}*/
			return true;
		}

		private void FillGrid(){
			gridMain.BeginUpdate();
			gridMain.Columns.Clear();
			ODGridColumn col=new ODGridColumn(Lan.g("TableRequest","Req#"),50);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableRequest","My Votes"),70);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableRequest","Total Votes"),90);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableRequest","Description"),200);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableRequest","Admin Notes"),200);
			gridMain.Columns.Add(col);
			gridMain.Rows.Clear();
			ODGridRow row;
			/*for(int i=0;i<List.Length;i++){
				row=new ODGridRow();
				row.Cells.Add("");
				row.Cells.Add("");
			  
				gridMain.Rows.Add(row);
			}*/
			gridMain.EndUpdate();
		}

		private void buttonAdd_Click(object sender,EventArgs e) {
			if(textSearch.Text==""){
				MsgBox.Show(this,"Please perform a search first.\r\nHint: Type a few letters into the search box.");
				return;
			}

		}

		private void butClose_Click(object sender, System.EventArgs e) {
			Close();
		}

		private void FormUpdate_FormClosing(object sender,FormClosingEventArgs e) {
			
		}

		

		

		

		

		

		

	

	

		

	

	}

	
}





















