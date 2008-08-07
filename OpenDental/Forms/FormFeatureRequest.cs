using System;
using System.Data;
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
using System.Xml.Serialization;
using System.Windows.Forms;
using OpenDentBusiness;
using CodeBase;
using OpenDental.UI;

namespace OpenDental{
	/// <summary>
	/// Summary description for FormBasicTemplate.
	/// </summary>
	public class FormFeatureRequest:System.Windows.Forms.Form {
		private OpenDental.UI.Button butClose;
		private System.Windows.Forms.Label label1;
		private IContainer components;
		private Label label2;
		private Label label5;
		private TextBox textSearch;
		private OpenDental.UI.ODGrid gridMain;
		private OpenDental.UI.Button buttonAdd;
		private Label label4;
		private OpenDental.UI.Button butSearch;
		private ODDataTable table;

		///<summary></summary>
		public FormFeatureRequest()
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormFeatureRequest));
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.textSearch = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.butSearch = new OpenDental.UI.Button();
			this.buttonAdd = new OpenDental.UI.Button();
			this.gridMain = new OpenDental.UI.ODGrid();
			this.butClose = new OpenDental.UI.Button();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(0,0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(100,23);
			this.label1.TabIndex = 0;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(359,7);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(511,16);
			this.label2.TabIndex = 51;
			this.label2.Text = "Vote for your favorite features here.  Please remember that we cannot ever give a" +
    "ny time estimates.";
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(4,5);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(76,18);
			this.label5.TabIndex = 56;
			this.label5.Text = "Search";
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textSearch
			// 
			this.textSearch.Location = new System.Drawing.Point(81,5);
			this.textSearch.Name = "textSearch";
			this.textSearch.Size = new System.Drawing.Size(179,20);
			this.textSearch.TabIndex = 57;
			// 
			// label4
			// 
			this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label4.Location = new System.Drawing.Point(91,633);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(180,18);
			this.label4.TabIndex = 61;
			this.label4.Text = "A search is required first";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// butSearch
			// 
			this.butSearch.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butSearch.Autosize = true;
			this.butSearch.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butSearch.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butSearch.CornerRadius = 4F;
			this.butSearch.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butSearch.Location = new System.Drawing.Point(266,2);
			this.butSearch.Name = "butSearch";
			this.butSearch.Size = new System.Drawing.Size(75,24);
			this.butSearch.TabIndex = 62;
			this.butSearch.Text = "Search";
			this.butSearch.Click += new System.EventHandler(this.butSearch_Click);
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
			this.buttonAdd.Location = new System.Drawing.Point(12,630);
			this.buttonAdd.Name = "buttonAdd";
			this.buttonAdd.Size = new System.Drawing.Size(75,24);
			this.buttonAdd.TabIndex = 60;
			this.buttonAdd.Text = "Add";
			this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
			// 
			// gridMain
			// 
			this.gridMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gridMain.HScrollVisible = false;
			this.gridMain.Location = new System.Drawing.Point(12,28);
			this.gridMain.Name = "gridMain";
			this.gridMain.ScrollValue = 0;
			this.gridMain.Size = new System.Drawing.Size(861,599);
			this.gridMain.TabIndex = 59;
			this.gridMain.Title = "Feature Requests";
			this.gridMain.TranslationName = null;
			this.gridMain.CellDoubleClick += new OpenDental.UI.ODGridClickEventHandler(this.gridMain_CellDoubleClick);
			// 
			// butClose
			// 
			this.butClose.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butClose.Autosize = true;
			this.butClose.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butClose.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butClose.CornerRadius = 4F;
			this.butClose.Location = new System.Drawing.Point(798,630);
			this.butClose.Name = "butClose";
			this.butClose.Size = new System.Drawing.Size(75,24);
			this.butClose.TabIndex = 0;
			this.butClose.Text = "&Close";
			this.butClose.Click += new System.EventHandler(this.butClose_Click);
			// 
			// FormFeatureRequest
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(882,657);
			this.Controls.Add(this.butSearch);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.buttonAdd);
			this.Controls.Add(this.textSearch);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.gridMain);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.butClose);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "FormFeatureRequest";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Feature Requests";
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
			//if(!Synch()){
			//	return;
			//}
			//FillGrid();
		}

		private void butSearch_Click(object sender,EventArgs e) {
			//textConnectionMessage.Text=Lan.g(this,"Attempting to connect to web service......");
			Application.DoEvents();
			//prepare the xml document to send--------------------------------------------------------------------------------------
			XmlWriterSettings settings = new XmlWriterSettings();
			settings.Indent = true;
			settings.IndentChars = ("    ");
			StringBuilder strbuild=new StringBuilder();
			using(XmlWriter writer=XmlWriter.Create(strbuild,settings)){
				writer.WriteStartElement("FeatureRequestGetList");
				writer.WriteStartElement("RegistrationKey");
				writer.WriteString(PrefC.GetString("RegistrationKey"));
				writer.WriteEndElement();
				writer.WriteStartElement("SearchString");
				writer.WriteString(textSearch.Text);
				writer.WriteEndElement();
				writer.WriteEndElement();
			}
			#if DEBUG
				OpenDental.localhost.Service1 updateService=new OpenDental.localhost.Service1();
			#else
				OpenDental.customerUpdates.Service1 updateService=new OpenDental.customerUpdates.Service1();
				updateService.Url=PrefC.GetString("UpdateServerAddress");
			#endif
			//Send the message and get the result-------------------------------------------------------------------------------------
			string result="";
			try {
				result=updateService.FeatureRequestGetList(strbuild.ToString());
			}
			catch(Exception ex) {
				Cursor=Cursors.Default;
				MessageBox.Show("Error: "+ex.Message);
				return;
			}
			//textConnectionMessage.Text=Lan.g(this,"Connection successful.");
			//Application.DoEvents();
			Cursor=Cursors.Default;
			//MessageBox.Show(result);
			XmlDocument doc=new XmlDocument();
			doc.LoadXml(result);
			//Process errors------------------------------------------------------------------------------------------------------------
			XmlNode node=doc.SelectSingleNode("//Error");
			if(node!=null) {
				//textConnectionMessage.Text=node.InnerText;
				MessageBox.Show(node.InnerText,"Error");
				return;
			}
			node=doc.SelectSingleNode("//KeyDisabled");
			if(node==null) {
				//no error, and no disabled message
				if(Prefs.UpdateBool("RegistrationKeyIsDisabled",false)) {//this is one of two places in the program where this happens.
					DataValid.SetInvalid(InvalidType.Prefs);
				}
			}
			else {
				//textConnectionMessage.Text=node.InnerText;
				MessageBox.Show(node.InnerText);
				if(Prefs.UpdateBool("RegistrationKeyIsDisabled",true)) {//this is one of two places in the program where this happens.
					DataValid.SetInvalid(InvalidType.Prefs);
				}
				return;
			}
			//Process a valid return value------------------------------------------------------------------------------------------------
			node=doc.SelectSingleNode("//ResultTable");
			table=new ODDataTable(node.InnerXml);
			FillGrid();
		}

		private void FillGrid(){
			gridMain.BeginUpdate();
			gridMain.Columns.Clear();
			ODGridColumn col=new ODGridColumn(Lan.g("TableRequest","Req#"),40);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableRequest","My Votes"),60);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableRequest","Total Votes"),70);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableRequest","Approval"),90);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableRequest","Description"),500);
			gridMain.Columns.Add(col);
			gridMain.Rows.Clear();
			ODGridRow row;
			for(int i=0;i<table.Rows.Count;i++){
				row=new ODGridRow();
				row.Cells.Add(table.Rows[i]["RequestId"]);
				row.Cells.Add(table.Rows[i]["myVotes"]);
			  row.Cells.Add(table.Rows[i]["totalVotes"]);
				row.Cells.Add(table.Rows[i]["approval"]);
				row.Cells.Add(table.Rows[i]["Description"]);
				gridMain.Rows.Add(row);
			}
			gridMain.EndUpdate();
			//textConnectionMessage.Text="";
		}

		private void buttonAdd_Click(object sender,EventArgs e) {
			if(textSearch.Text==""){
				MsgBox.Show(this,"Please perform a search first.\r\nHint: Type a few letters into the search box.");
				return;
			}

		}

		private void gridMain_CellDoubleClick(object sender,ODGridClickEventArgs e) {
			FormRequestEdit FormR=new FormRequestEdit();
		}

		private void butClose_Click(object sender, System.EventArgs e) {
			Close();
		}

		private void FormUpdate_FormClosing(object sender,FormClosingEventArgs e) {
			
		}

		

	

		

		

		

		

		

		

	

	

		

	

	}

	
}





















