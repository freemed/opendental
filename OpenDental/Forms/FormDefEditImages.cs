/*=============================================================================================================
Open Dental GPL license Copyright (C) 2003  Jordan Sparks, DMD.  http://www.open-dent.com,  www.docsparks.com
See header in FormOpenDental.cs for complete text.  Redistributions must retain this text.
===============================================================================================================*/
using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{
///<summary></summary>
	public class FormDefEditImages:System.Windows.Forms.Form {
		private System.Windows.Forms.Label labelName;
		private System.Windows.Forms.TextBox textName;
		private System.Windows.Forms.ColorDialog colorDialog1;
		private OpenDental.UI.Button butOK;
		private OpenDental.UI.Button butCancel;
		private System.ComponentModel.Container components = null;// Required designer variable.
		///<summary></summary>
		public bool IsNew;
		private System.Windows.Forms.CheckBox checkHidden;
		private Def DefCur;
		private CheckBox checkT;
		private CheckBox checkS;
		private CheckBox checkP;
		private CheckBox checkX;
		private GroupBox groupBox1;
		
		///<summary></summary>
		public FormDefEditImages(Def defCur) {
			InitializeComponent();// Required for Windows Form Designer support
			Lan.F(this);
			DefCur=defCur.Copy();
		}

		///<summary></summary>
		protected override void Dispose( bool disposing ){
			if( disposing ){
				if(components != null){
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormDefEditImages));
			this.labelName = new System.Windows.Forms.Label();
			this.textName = new System.Windows.Forms.TextBox();
			this.colorDialog1 = new System.Windows.Forms.ColorDialog();
			this.butOK = new OpenDental.UI.Button();
			this.butCancel = new OpenDental.UI.Button();
			this.checkHidden = new System.Windows.Forms.CheckBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.checkX = new System.Windows.Forms.CheckBox();
			this.checkP = new System.Windows.Forms.CheckBox();
			this.checkS = new System.Windows.Forms.CheckBox();
			this.checkT = new System.Windows.Forms.CheckBox();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// labelName
			// 
			this.labelName.Location = new System.Drawing.Point(47,24);
			this.labelName.Name = "labelName";
			this.labelName.Size = new System.Drawing.Size(150,16);
			this.labelName.TabIndex = 0;
			this.labelName.Text = "Name";
			this.labelName.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// textName
			// 
			this.textName.Location = new System.Drawing.Point(32,40);
			this.textName.Name = "textName";
			this.textName.Size = new System.Drawing.Size(178,20);
			this.textName.TabIndex = 0;
			// 
			// colorDialog1
			// 
			this.colorDialog1.FullOpen = true;
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.CornerRadius = 4F;
			this.butOK.Location = new System.Drawing.Point(376,159);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75,25);
			this.butOK.TabIndex = 4;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
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
			this.butCancel.Location = new System.Drawing.Point(471,159);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75,25);
			this.butCancel.TabIndex = 5;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// checkHidden
			// 
			this.checkHidden.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkHidden.Location = new System.Drawing.Point(449,38);
			this.checkHidden.Name = "checkHidden";
			this.checkHidden.Size = new System.Drawing.Size(99,24);
			this.checkHidden.TabIndex = 3;
			this.checkHidden.Text = "Hidden";
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.checkT);
			this.groupBox1.Controls.Add(this.checkS);
			this.groupBox1.Controls.Add(this.checkP);
			this.groupBox1.Controls.Add(this.checkX);
			this.groupBox1.Location = new System.Drawing.Point(226,22);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(215,100);
			this.groupBox1.TabIndex = 7;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Usage";
			// 
			// checkX
			// 
			this.checkX.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkX.Location = new System.Drawing.Point(8,19);
			this.checkX.Name = "checkX";
			this.checkX.Size = new System.Drawing.Size(201,18);
			this.checkX.TabIndex = 4;
			this.checkX.Text = "Show in Chart module";
			// 
			// checkP
			// 
			this.checkP.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkP.Location = new System.Drawing.Point(8,37);
			this.checkP.Name = "checkP";
			this.checkP.Size = new System.Drawing.Size(201,18);
			this.checkP.TabIndex = 5;
			this.checkP.Text = "Patient Pictures (only one)";
			// 
			// checkS
			// 
			this.checkS.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkS.Location = new System.Drawing.Point(8,55);
			this.checkS.Name = "checkS";
			this.checkS.Size = new System.Drawing.Size(201,18);
			this.checkS.TabIndex = 6;
			this.checkS.Text = "Statements (only one)";
			// 
			// checkT
			// 
			this.checkT.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkT.Location = new System.Drawing.Point(8,73);
			this.checkT.Name = "checkT";
			this.checkT.Size = new System.Drawing.Size(201,18);
			this.checkT.TabIndex = 7;
			this.checkT.Text = "Graphical Tooth Charts (only one)";
			// 
			// FormDefEditImages
			// 
			this.AcceptButton = this.butOK;
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.CancelButton = this.butCancel;
			this.ClientSize = new System.Drawing.Size(558,196);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.checkHidden);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.textName);
			this.Controls.Add(this.labelName);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormDefEditImages";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Edit Definition";
			this.Load += new System.EventHandler(this.FormDefEdit_Load);
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		private void FormDefEdit_Load(object sender,System.EventArgs e) {
			textName.Text=DefCur.ItemName;
			//textValue.Text=DefCur.ItemValue;
			if(DefCur.ItemValue.Contains("X")) {
				checkX.Checked=true;
			}
			if(DefCur.ItemValue.Contains("P")) {
				checkP.Checked=true;
			}
			if(DefCur.ItemValue.Contains("S")) {
				checkS.Checked=true;
			}
			if(DefCur.ItemValue.Contains("T")) {
				checkT.Checked=true;
			}
			checkHidden.Checked=DefCur.IsHidden;
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			if(textName.Text==""){
				MsgBox.Show(this,"Name required.");
				return;
			}
			DefCur.ItemName=textName.Text;
			string itemVal="";
			if(checkX.Checked) {
				itemVal+="X";
			}
			if(checkP.Checked) {
				itemVal+="P";
			}
			if(checkS.Checked) {
				itemVal+="S";
			}
			if(checkT.Checked) {
				itemVal+="T";
			}
			DefCur.ItemValue=itemVal;
			DefCur.IsHidden=checkHidden.Checked;
			if(IsNew){
				Defs.Insert(DefCur);
			}
			else{
				Defs.Update(DefCur);
			}
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		

	

	}
}
