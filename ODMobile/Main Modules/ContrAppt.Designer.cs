namespace OpenDentMobile {
	partial class ContrAppt {
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			this.panel1 = new System.Windows.Forms.Panel();
			this.dateTPicker = new System.Windows.Forms.DateTimePicker();
			this.butFwd = new System.Windows.Forms.Button();
			this.butBack = new System.Windows.Forms.Button();
			this.gridMain = new OpenDentMobile.UI.ODGrid();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.panel1.AutoScroll = true;
			this.panel1.Controls.Add(this.gridMain);
			this.panel1.Location = new System.Drawing.Point(0,22);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(240,246);
			// 
			// dateTPicker
			// 
			this.dateTPicker.CustomFormat = "dddd, MM/dd/yyyy";
			this.dateTPicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.dateTPicker.Location = new System.Drawing.Point(20,0);
			this.dateTPicker.Name = "dateTPicker";
			this.dateTPicker.Size = new System.Drawing.Size(162,22);
			this.dateTPicker.TabIndex = 2;
			this.dateTPicker.ValueChanged += new System.EventHandler(this.dateTPicker_ValueChanged);
			// 
			// butFwd
			// 
			this.butFwd.Location = new System.Drawing.Point(182,1);
			this.butFwd.Name = "butFwd";
			this.butFwd.Size = new System.Drawing.Size(20,20);
			this.butFwd.TabIndex = 3;
			this.butFwd.Text = ">";
			this.butFwd.Click += new System.EventHandler(this.butFwd_Click);
			// 
			// butBack
			// 
			this.butBack.Location = new System.Drawing.Point(0,1);
			this.butBack.Name = "butBack";
			this.butBack.Size = new System.Drawing.Size(20,20);
			this.butBack.TabIndex = 4;
			this.butBack.Text = "<";
			this.butBack.Click += new System.EventHandler(this.butBack_Click);
			// 
			// gridMain
			// 
			this.gridMain.Location = new System.Drawing.Point(0,0);
			this.gridMain.Name = "gridMain";
			this.gridMain.SelectionMode = OpenDentMobile.UI.GridSelectionMode.One;
			this.gridMain.Size = new System.Drawing.Size(218,210);
			this.gridMain.TabIndex = 0;
			this.gridMain.Text = "odGrid1";
			this.gridMain.WrapText = true;
			this.gridMain.CellClick += new OpenDentMobile.UI.ODGridClickEventHandler(this.gridMain_CellClick);
			// 
			// ContrAppt
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(96F,96F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.Controls.Add(this.butBack);
			this.Controls.Add(this.butFwd);
			this.Controls.Add(this.dateTPicker);
			this.Controls.Add(this.panel1);
			this.Name = "ContrAppt";
			this.Size = new System.Drawing.Size(240,268);
			this.panel1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel panel1;
		private OpenDentMobile.UI.ODGrid gridMain;
		private System.Windows.Forms.DateTimePicker dateTPicker;
		private System.Windows.Forms.Button butFwd;
		private System.Windows.Forms.Button butBack;

	}
}
