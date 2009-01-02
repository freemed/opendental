namespace OpenDentMobile {
	partial class FormPatientSelect {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.MainMenu mainMenu1;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if(disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			this.mainMenu1 = new System.Windows.Forms.MainMenu();
			this.listView = new System.Windows.Forms.ListView();
			this.colLName = new System.Windows.Forms.ColumnHeader();
			this.colFName = new System.Windows.Forms.ColumnHeader();
			this.colPhone = new System.Windows.Forms.ColumnHeader();
			this.textLName = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.butSearch = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// listView
			// 
			this.listView.Activation = System.Windows.Forms.ItemActivation.OneClick;
			this.listView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.listView.Columns.Add(this.colLName);
			this.listView.Columns.Add(this.colFName);
			this.listView.Columns.Add(this.colPhone);
			this.listView.Font = new System.Drawing.Font("Tahoma",8F,System.Drawing.FontStyle.Regular);
			this.listView.FullRowSelect = true;
			this.listView.Location = new System.Drawing.Point(0,23);
			this.listView.Name = "listView";
			this.listView.Size = new System.Drawing.Size(240,245);
			this.listView.TabIndex = 0;
			this.listView.View = System.Windows.Forms.View.Details;
			this.listView.ItemActivate += new System.EventHandler(this.listView_ItemActivate);
			// 
			// colLName
			// 
			this.colLName.Text = "LName";
			this.colLName.Width = 70;
			// 
			// colFName
			// 
			this.colFName.Text = "FName";
			this.colFName.Width = 70;
			// 
			// colPhone
			// 
			this.colPhone.Text = "Phone";
			this.colPhone.Width = 85;
			// 
			// textLName
			// 
			this.textLName.Location = new System.Drawing.Point(66,1);
			this.textLName.Name = "textLName";
			this.textLName.Size = new System.Drawing.Size(86,21);
			this.textLName.TabIndex = 1;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(0,3);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(62,18);
			this.label1.Text = "LName";
			this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// butSearch
			// 
			this.butSearch.Location = new System.Drawing.Point(158,1);
			this.butSearch.Name = "butSearch";
			this.butSearch.Size = new System.Drawing.Size(72,20);
			this.butSearch.TabIndex = 3;
			this.butSearch.Text = "Search";
			this.butSearch.Click += new System.EventHandler(this.butSearch_Click);
			// 
			// FormPatientSelect
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(96F,96F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.AutoScroll = true;
			this.ClientSize = new System.Drawing.Size(240,268);
			this.Controls.Add(this.butSearch);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.textLName);
			this.Controls.Add(this.listView);
			this.Font = new System.Drawing.Font("Tahoma",8F,System.Drawing.FontStyle.Regular);
			this.KeyPreview = true;
			this.Menu = this.mainMenu1;
			this.Name = "FormPatientSelect";
			this.Text = "Select Patient";
			this.Load += new System.EventHandler(this.Form1_Load);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ListView listView;
		private System.Windows.Forms.ColumnHeader colLName;
		private System.Windows.Forms.ColumnHeader colPhone;
		private System.Windows.Forms.ColumnHeader colFName;
		private System.Windows.Forms.TextBox textLName;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button butSearch;
	}
}

