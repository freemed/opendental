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
	public class FormContactEdit : System.Windows.Forms.Form{
		private OpenDental.UI.Button butCancel;
		private OpenDental.UI.Button butOK;
		private OpenDental.UI.Button butDelete;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.TextBox textLName;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox textFName;
		private System.Windows.Forms.TextBox textWkPhone;
		private System.Windows.Forms.TextBox textFax;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox textNotes;
		private System.Windows.Forms.ListBox listCategory;
		private System.Windows.Forms.Label label6;
		///<summary></summary>
		public bool IsNew;
		public Contact ContactCur;

		///<summary></summary>
		public FormContactEdit()
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormContactEdit));
			this.butCancel = new OpenDental.UI.Button();
			this.butOK = new OpenDental.UI.Button();
			this.butDelete = new OpenDental.UI.Button();
			this.textLName = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.textFName = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.textWkPhone = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.textFax = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.textNotes = new System.Windows.Forms.TextBox();
			this.listCategory = new System.Windows.Forms.ListBox();
			this.label6 = new System.Windows.Forms.Label();
			this.SuspendLayout();
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
			this.butCancel.Location = new System.Drawing.Point(591,450);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75,25);
			this.butCancel.TabIndex = 0;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.CornerRadius = 4F;
			this.butOK.Location = new System.Drawing.Point(591,411);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75,25);
			this.butOK.TabIndex = 1;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// butDelete
			// 
			this.butDelete.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butDelete.Autosize = true;
			this.butDelete.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butDelete.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butDelete.CornerRadius = 4F;
			this.butDelete.Location = new System.Drawing.Point(42,450);
			this.butDelete.Name = "butDelete";
			this.butDelete.Size = new System.Drawing.Size(75,25);
			this.butDelete.TabIndex = 2;
			this.butDelete.Text = "&Delete";
			this.butDelete.Click += new System.EventHandler(this.butDelete_Click);
			// 
			// textLName
			// 
			this.textLName.Location = new System.Drawing.Point(215,147);
			this.textLName.Name = "textLName";
			this.textLName.Size = new System.Drawing.Size(205,20);
			this.textLName.TabIndex = 3;
			this.textLName.TextChanged += new System.EventHandler(this.textLName_TextChanged);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(64,150);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(151,17);
			this.label1.TabIndex = 4;
			this.label1.Text = "Last Name";
			this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(63,177);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(151,17);
			this.label2.TabIndex = 6;
			this.label2.Text = "First Name (optional)";
			this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textFName
			// 
			this.textFName.Location = new System.Drawing.Point(215,173);
			this.textFName.Name = "textFName";
			this.textFName.Size = new System.Drawing.Size(205,20);
			this.textFName.TabIndex = 5;
			this.textFName.TextChanged += new System.EventHandler(this.textFName_TextChanged);
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(61,203);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(151,17);
			this.label3.TabIndex = 8;
			this.label3.Text = "Wk Phone";
			this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textWkPhone
			// 
			this.textWkPhone.Location = new System.Drawing.Point(215,199);
			this.textWkPhone.Name = "textWkPhone";
			this.textWkPhone.Size = new System.Drawing.Size(205,20);
			this.textWkPhone.TabIndex = 7;
			this.textWkPhone.TextChanged += new System.EventHandler(this.textWkPhone_TextChanged);
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(61,228);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(151,17);
			this.label4.TabIndex = 10;
			this.label4.Text = "Fax";
			this.label4.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textFax
			// 
			this.textFax.Location = new System.Drawing.Point(215,225);
			this.textFax.Name = "textFax";
			this.textFax.Size = new System.Drawing.Size(205,20);
			this.textFax.TabIndex = 9;
			this.textFax.TextChanged += new System.EventHandler(this.textFax_TextChanged);
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(63,250);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(151,17);
			this.label5.TabIndex = 12;
			this.label5.Text = "Notes";
			this.label5.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textNotes
			// 
			this.textNotes.Location = new System.Drawing.Point(215,251);
			this.textNotes.Multiline = true;
			this.textNotes.Name = "textNotes";
			this.textNotes.Size = new System.Drawing.Size(449,120);
			this.textNotes.TabIndex = 11;
			// 
			// listCategory
			// 
			this.listCategory.Location = new System.Drawing.Point(215,46);
			this.listCategory.Name = "listCategory";
			this.listCategory.Size = new System.Drawing.Size(120,95);
			this.listCategory.TabIndex = 13;
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(62,48);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(151,17);
			this.label6.TabIndex = 14;
			this.label6.Text = "Category";
			this.label6.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// FormContactEdit
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.CancelButton = this.butCancel;
			this.ClientSize = new System.Drawing.Size(717,493);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.listCategory);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.textNotes);
			this.Controls.Add(this.textFax);
			this.Controls.Add(this.textWkPhone);
			this.Controls.Add(this.textFName);
			this.Controls.Add(this.textLName);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.butDelete);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.butCancel);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormContactEdit";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Edit Contact";
			this.Load += new System.EventHandler(this.FormContactEdit_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		private void FormContactEdit_Load(object sender, System.EventArgs e) {
			for(int i=0;i<DefB.Short[(int)DefCat.ContactCategories].Length;i++){
				listCategory.Items.Add(DefB.Short[(int)DefCat.ContactCategories][i].ItemName);
				if(ContactCur.Category==DefB.Short[(int)DefCat.ContactCategories][i].DefNum){
					listCategory.SelectedIndex=i;
				}
			}
			textLName.Text=ContactCur.LName;
			textFName.Text=ContactCur.FName;
			textWkPhone.Text=ContactCur.WkPhone;
			textFax.Text=ContactCur.Fax;
			textNotes.Text=ContactCur.Notes;
		}

		private void butDelete_Click(object sender, System.EventArgs e) {
			if(MessageBox.Show(Lan.g(this,"Delete contact"),"",MessageBoxButtons.OKCancel)!=DialogResult.OK){
				return;
			}
			if(IsNew){
				DialogResult=DialogResult.Cancel;
			}
			else{
				Contacts.Delete(ContactCur);
				DialogResult=DialogResult.OK;
			}
		}

		private void textLName_TextChanged(object sender, System.EventArgs e) {
			if(textLName.Text.Length==1){
				textLName.Text=textLName.Text.ToUpper();
				textLName.SelectionStart=1;
			}
		}

		private void textFName_TextChanged(object sender, System.EventArgs e) {
			if(textFName.Text.Length==1){
				textFName.Text=textFName.Text.ToUpper();
				textFName.SelectionStart=1;
			}
		}

		private void textWkPhone_TextChanged(object sender, System.EventArgs e) {
			int cursor=textWkPhone.SelectionStart;
			int length=textWkPhone.Text.Length;
			textWkPhone.Text=TelephoneNumbers.AutoFormat(textWkPhone.Text);
			if(textWkPhone.Text.Length>length)
				cursor++;
			textWkPhone.SelectionStart=cursor;		
		}

		private void textFax_TextChanged(object sender, System.EventArgs e) {
			int cursor=textFax.SelectionStart;
			int length=textFax.Text.Length;
			textFax.Text=TelephoneNumbers.AutoFormat(textFax.Text);
			if(textFax.Text.Length>length)
				cursor++;
			textFax.SelectionStart=cursor;		
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			if(textLName.Text==""){
				MessageBox.Show(Lan.g(this,"Last Name cannot be blank."));
				return;
			}
			//a category will always be selected because of the manner in which Contact is accessed
			ContactCur.Category=DefB.Short[(int)DefCat.ContactCategories][listCategory.SelectedIndex].DefNum;
			ContactCur.LName=textLName.Text;
			ContactCur.FName=textFName.Text;
			ContactCur.WkPhone=textWkPhone.Text;
			ContactCur.Fax=textFax.Text;
			ContactCur.Notes=textNotes.Text;
			if(IsNew){
				Contacts.Insert(ContactCur);
			}
			else{
				Contacts.Update(ContactCur);
			}
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		

		

		

		

		

		


	}
}





















