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
	public class FormCanadianExtract : System.Windows.Forms.Form{
		private OpenDental.UI.Button butCancel;
		private OpenDental.UI.Button butOK;
		private TextBox textToothNum;
		private Label labelToothNum;
		private Label label2;
		private Label label3;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		///<summary>Set this value externally.</summary>
		public CanadianExtract Cur;
		private ValidDate textDateExtracted;
		///<summary>If true, then only the date will show.  Used to change date for multiple teeth at once.</summary>
		public bool IsMulti;

		///<summary></summary>
		public FormCanadianExtract()
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormCanadianExtract));
			this.textToothNum = new System.Windows.Forms.TextBox();
			this.labelToothNum = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.butOK = new OpenDental.UI.Button();
			this.butCancel = new OpenDental.UI.Button();
			this.textDateExtracted = new OpenDental.ValidDate();
			this.SuspendLayout();
			// 
			// textToothNum
			// 
			this.textToothNum.Location = new System.Drawing.Point(131,22);
			this.textToothNum.Name = "textToothNum";
			this.textToothNum.Size = new System.Drawing.Size(55,20);
			this.textToothNum.TabIndex = 0;
			// 
			// labelToothNum
			// 
			this.labelToothNum.Location = new System.Drawing.Point(12,22);
			this.labelToothNum.Name = "labelToothNum";
			this.labelToothNum.Size = new System.Drawing.Size(115,19);
			this.labelToothNum.TabIndex = 3;
			this.labelToothNum.Text = "Tooth Number";
			this.labelToothNum.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(12,54);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(115,19);
			this.label2.TabIndex = 4;
			this.label2.Text = "Date Extracted";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(235,55);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(156,19);
			this.label3.TabIndex = 7;
			this.label3.Text = "(Required for prosthesis)";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.CornerRadius = 4F;
			this.butOK.Location = new System.Drawing.Point(228,130);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75,26);
			this.butOK.TabIndex = 3;
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
			this.butCancel.Location = new System.Drawing.Point(318,130);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75,26);
			this.butCancel.TabIndex = 2;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// textDateExtracted
			// 
			this.textDateExtracted.Location = new System.Drawing.Point(131,54);
			this.textDateExtracted.Name = "textDateExtracted";
			this.textDateExtracted.Size = new System.Drawing.Size(100,20);
			this.textDateExtracted.TabIndex = 8;
			// 
			// FormCanadianExtract
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.ClientSize = new System.Drawing.Size(416,179);
			this.Controls.Add(this.textDateExtracted);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.labelToothNum);
			this.Controls.Add(this.textToothNum);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.butCancel);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormCanadianExtract";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Edit Missing Tooth";
			this.Load += new System.EventHandler(this.FormCanadianExtract_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		private void FormCanadianExtract_Load(object sender,EventArgs e) {
			if(IsMulti){
				textToothNum.Visible=false;
				labelToothNum.Visible=false;
			}
			else{
				textToothNum.Text=Tooth.ToInternat(Cur.ToothNum);
			}
			if(Cur.DateExtraction.Year<1880){
				textDateExtracted.Text="";
			}
			else{
				textDateExtracted.Text=Cur.DateExtraction.ToShortDateString();
			}
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			if(!IsMulti){
				if(textToothNum.Text=="" || !Tooth.IsValidEntry(textToothNum.Text)) {
					MsgBox.Show(this,"Tooth number invalid.");
					return;
				}
			}
			//if IsMulti, it's ok to have date be blank, minval
			if(textDateExtracted.errorProvider1.GetError(textDateExtracted)!=""){
				MsgBox.Show(this,"Date invalid.");
				return;
			}
			if(!IsMulti){
				Cur.ToothNum=Tooth.FromInternat(textToothNum.Text);
			}
			Cur.DateExtraction=PIn.PDate(textDateExtracted.Text);
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		



	}
}





















