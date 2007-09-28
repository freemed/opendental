/*=============================================================================================================
Open Dental GPL license Copyright (C) 2003  Jordan Sparks, DMD.  http://www.open-dent.com,  www.docsparks.com
See header in FormOpenDental.cs for complete text.  Redistributions must retain this text.
===============================================================================================================*/
using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{
///<summary></summary>
	public class FormDefEdit : System.Windows.Forms.Form{
		private System.Windows.Forms.Label labelName;
		private System.Windows.Forms.Label labelValue;
		private System.Windows.Forms.TextBox textName;
		private System.Windows.Forms.TextBox textValue;
		private System.Windows.Forms.Button butColor;
		private System.Windows.Forms.ColorDialog colorDialog1;
		private OpenDental.UI.Button butOK;
		private OpenDental.UI.Button butCancel;
		private System.ComponentModel.Container components = null;// Required designer variable.
		///<summary></summary>
		public bool IsNew;
		private System.Windows.Forms.Label labelColor;
		///<summary></summary>
		public static bool CanEditName;
		///<summary></summary>
		public static bool EnableValue;
		///<summary></summary>
		public static bool EnableColor;
		///<summary></summary>
		public static string HelpText;
		private System.Windows.Forms.CheckBox checkHidden;
		///<summary></summary>
		public static string ValueText;
		private Def DefCur;
		
		///<summary></summary>
		public FormDefEdit(Def defCur){
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormDefEdit));
			this.labelName = new System.Windows.Forms.Label();
			this.labelValue = new System.Windows.Forms.Label();
			this.textName = new System.Windows.Forms.TextBox();
			this.textValue = new System.Windows.Forms.TextBox();
			this.butColor = new System.Windows.Forms.Button();
			this.colorDialog1 = new System.Windows.Forms.ColorDialog();
			this.labelColor = new System.Windows.Forms.Label();
			this.butOK = new OpenDental.UI.Button();
			this.butCancel = new OpenDental.UI.Button();
			this.checkHidden = new System.Windows.Forms.CheckBox();
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
			// labelValue
			// 
			this.labelValue.Location = new System.Drawing.Point(218,22);
			this.labelValue.Name = "labelValue";
			this.labelValue.Size = new System.Drawing.Size(164,16);
			this.labelValue.TabIndex = 1;
			this.labelValue.Text = "Value";
			this.labelValue.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// textName
			// 
			this.textName.Location = new System.Drawing.Point(32,40);
			this.textName.Multiline = true;
			this.textName.Name = "textName";
			this.textName.Size = new System.Drawing.Size(178,64);
			this.textName.TabIndex = 0;
			// 
			// textValue
			// 
			this.textValue.Location = new System.Drawing.Point(210,40);
			this.textValue.MaxLength = 256;
			this.textValue.Multiline = true;
			this.textValue.Name = "textValue";
			this.textValue.Size = new System.Drawing.Size(180,64);
			this.textValue.TabIndex = 1;
			// 
			// butColor
			// 
			this.butColor.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.butColor.Location = new System.Drawing.Point(392,40);
			this.butColor.Name = "butColor";
			this.butColor.Size = new System.Drawing.Size(30,20);
			this.butColor.TabIndex = 2;
			this.butColor.Click += new System.EventHandler(this.butColor_Click);
			// 
			// colorDialog1
			// 
			this.colorDialog1.FullOpen = true;
			// 
			// labelColor
			// 
			this.labelColor.Location = new System.Drawing.Point(389,21);
			this.labelColor.Name = "labelColor";
			this.labelColor.Size = new System.Drawing.Size(74,16);
			this.labelColor.TabIndex = 5;
			this.labelColor.Text = "Color";
			this.labelColor.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.CornerRadius = 4F;
			this.butOK.Location = new System.Drawing.Point(298,131);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75,25);
			this.butOK.TabIndex = 4;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// butCancel
			// 
			this.butCancel.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butCancel.Autosize = true;
			this.butCancel.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butCancel.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butCancel.CornerRadius = 4F;
			this.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butCancel.Location = new System.Drawing.Point(393,131);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75,25);
			this.butCancel.TabIndex = 5;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// checkHidden
			// 
			this.checkHidden.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkHidden.Location = new System.Drawing.Point(432,38);
			this.checkHidden.Name = "checkHidden";
			this.checkHidden.Size = new System.Drawing.Size(99,24);
			this.checkHidden.TabIndex = 3;
			this.checkHidden.Text = "Hidden";
			// 
			// FormDefEdit
			// 
			this.AcceptButton = this.butOK;
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.CancelButton = this.butCancel;
			this.ClientSize = new System.Drawing.Size(558,176);
			this.Controls.Add(this.checkHidden);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.labelColor);
			this.Controls.Add(this.butColor);
			this.Controls.Add(this.textValue);
			this.Controls.Add(this.textName);
			this.Controls.Add(this.labelValue);
			this.Controls.Add(this.labelName);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormDefEdit";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Edit Definition";
			this.Load += new System.EventHandler(this.FormDefEdit_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		private void FormDefEdit_Load(object sender, System.EventArgs e) {
			if(IsNew){
				;//handled from previous form
			}
			if(!CanEditName){
				textName.ReadOnly=true;
				if(!DefCur.IsHidden){
					checkHidden.Enabled=false;//prevent hiding these types of defs
				}
			}
			labelValue.Text=ValueText;
			if(DefCur.Category==DefCat.AdjTypes && !IsNew){
				labelValue.Text="Not allowed to change sign after an adjustment is created.";
				textValue.Visible=false;
			}
			if(!EnableValue){
				labelValue.Visible=false;
				textValue.Visible=false;
			}
			if(!EnableColor){
				labelColor.Visible=false;
				butColor.Visible=false;
			}
			textName.Text=DefCur.ItemName;
			textValue.Text=DefCur.ItemValue;
			butColor.BackColor=DefCur.ItemColor;
			checkHidden.Checked=DefCur.IsHidden;
			//MessageBox.Show(Preferences.Cur.ItemColor.ToString());
		}

		private void butColor_Click(object sender, System.EventArgs e) {
			colorDialog1.Color=butColor.BackColor;
			colorDialog1.ShowDialog();
			butColor.BackColor=colorDialog1.Color;
			//textColor.Text=colorDialog1.Color.Name;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			if(textName.Text==""){
				MsgBox.Show(this,"Name required.");
				return;
			}
			switch((DefCat)DefCur.Category){
				case DefCat.AdjTypes:
					if(textValue.Text!="+" && textValue.Text!="-"){
						MessageBox.Show(Lan.g(this,"Valid values are + or -."));
						return;
					}
					break;
				case DefCat.ApptProcsQuickAdd:
					string[] procs=textValue.Text.Split(',');
					for(int i=0;i<procs.Length;i++){
						if(ProcedureCodes.GetProcCode(procs[i]).ProcCode==null){
							MessageBox.Show(Lan.g(this,"Invalid procedure code or formatting. Valid format example: D1234,D2345,D3456"));
							return;
						}
					}
					//test for not require tooth number if time
					break;
				case DefCat.CommLogTypes:
					if(textValue.Text!="" && textValue.Text!="MISC" && textValue.Text!="APPT" 
						&& textValue.Text!="FIN" && textValue.Text!="RECALL") 
					{
						MessageBox.Show(Lan.g(this,"Valid values are blank,APPT,FIN,RECALL,or MISC."));
						return;
					}
					break;
				case DefCat.RecallUnschedStatus:
					if(textValue.Text.Length > 7){
						MessageBox.Show(Lan.g(this,"Maximum length is 7."));
						return;
					}
					break;
				case DefCat.DiscountTypes:
					int discVal;
					if(textValue.Text=="") break;
					try{
						discVal=System.Convert.ToInt32(textValue.Text);
					}
					catch{
						MessageBox.Show(Lan.g(this,"Not a valid number"));
						return;
					}
					if(discVal < 0 || discVal > 100){
						MessageBox.Show(Lan.g(this,"Valid values are between 0 and 100"));
						return;
					}
					textValue.Text=discVal.ToString();
					break;
				case DefCat.OperatoriesOld:
					if(textValue.Text.Length > 5){
						MessageBox.Show(Lan.g(this,"Maximum length of abbreviation is 5."));
						return;
					}
					break;
				case DefCat.TxPriorities:
					if(textValue.Text.Length > 7){
						MessageBox.Show(Lan.g(this,"Maximum length of abbreviation is 7."));
						return;
					}
					break;
				case DefCat.ImageCats:
					if(textValue.Text=="P" || textValue.Text=="p"){
						textValue.Text="P";
					}
					else if(textValue.Text=="X" || textValue.Text=="x"){
						textValue.Text="X";
					}
					else if(textValue.Text.ToUpper()=="XP" || textValue.Text.ToUpper()=="PX" 
						|| textValue.Text.ToUpper()=="X,P" || textValue.Text.ToUpper()=="P,X")
					{
						textValue.Text="XP";
					}
					else textValue.Text="";
					break;
				case DefCat.FeeSchedNames:
					if(textValue.Text=="C" || textValue.Text=="c") {
						textValue.Text="C";
					}
					else if(textValue.Text=="A" || textValue.Text=="a") {
						textValue.Text="A";
					}
					else textValue.Text="";
					break;
			}//end switch
			DefCur.ItemName=textName.Text;
			if(EnableValue) DefCur.ItemValue=textValue.Text;
			if(EnableColor) DefCur.ItemColor=butColor.BackColor;
			DefCur.IsHidden=checkHidden.Checked;
			if(IsNew){
				Defs.Insert(DefCur);
			}
			else{
				Defs.Update(DefCur);
			}
			DialogResult=DialogResult.OK;
			Close();
		}

		private void button1_Click(object sender, System.EventArgs e) {
			MessageBox.Show(Lan.g(this,EnableColor.ToString()));
		}

	}
}
