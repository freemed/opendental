namespace OpenDental{
	partial class FormGraphEmployeeTime {
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

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			this.panel1 = new System.Windows.Forms.Panel();
			this.labelDate = new System.Windows.Forms.Label();
			this.buttonLeft = new OpenDental.UI.Button();
			this.buttonRight = new OpenDental.UI.Button();
			this.butCancel = new OpenDental.UI.Button();
			this.butPrint = new OpenDental.UI.Button();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.panel1.Location = new System.Drawing.Point(34,53);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(666,542);
			this.panel1.TabIndex = 5;
			this.panel1.Visible = false;
			// 
			// labelDate
			// 
			this.labelDate.Font = new System.Drawing.Font("Microsoft Sans Serif",10F,System.Drawing.FontStyle.Bold,System.Drawing.GraphicsUnit.Point,((byte)(0)));
			this.labelDate.Location = new System.Drawing.Point(261,21);
			this.labelDate.Name = "labelDate";
			this.labelDate.Size = new System.Drawing.Size(203,23);
			this.labelDate.TabIndex = 6;
			this.labelDate.Text = "Monday, January 1st";
			this.labelDate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// buttonLeft
			// 
			this.buttonLeft.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.buttonLeft.Autosize = true;
			this.buttonLeft.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.buttonLeft.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.buttonLeft.CornerRadius = 4F;
			this.buttonLeft.Image = global::OpenDental.Properties.Resources.Left;
			this.buttonLeft.Location = new System.Drawing.Point(235,22);
			this.buttonLeft.Name = "buttonLeft";
			this.buttonLeft.Size = new System.Drawing.Size(22,22);
			this.buttonLeft.TabIndex = 8;
			this.buttonLeft.UseVisualStyleBackColor = true;
			this.buttonLeft.Click += new System.EventHandler(this.buttonLeft_Click);
			// 
			// buttonRight
			// 
			this.buttonRight.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.buttonRight.Autosize = true;
			this.buttonRight.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.buttonRight.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.buttonRight.CornerRadius = 4F;
			this.buttonRight.Image = global::OpenDental.Properties.Resources.Right;
			this.buttonRight.Location = new System.Drawing.Point(467,22);
			this.buttonRight.Name = "buttonRight";
			this.buttonRight.Size = new System.Drawing.Size(22,22);
			this.buttonRight.TabIndex = 7;
			this.buttonRight.UseVisualStyleBackColor = true;
			this.buttonRight.Click += new System.EventHandler(this.buttonRight_Click);
			// 
			// butCancel
			// 
			this.butCancel.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butCancel.Autosize = true;
			this.butCancel.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butCancel.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butCancel.CornerRadius = 4F;
			this.butCancel.Location = new System.Drawing.Point(625,629);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75,24);
			this.butCancel.TabIndex = 2;
			this.butCancel.Text = "Close";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// butPrint
			// 
			this.butPrint.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butPrint.Autosize = true;
			this.butPrint.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butPrint.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butPrint.CornerRadius = 4F;
			this.butPrint.Location = new System.Drawing.Point(376,629);
			this.butPrint.Name = "butPrint";
			this.butPrint.Size = new System.Drawing.Size(75,24);
			this.butPrint.TabIndex = 9;
			this.butPrint.Text = "Print";
			this.butPrint.Click += new System.EventHandler(this.butPrint_Click);
			// 
			// FormGraphEmployeeTime
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(725,665);
			this.Controls.Add(this.butPrint);
			this.Controls.Add(this.buttonLeft);
			this.Controls.Add(this.buttonRight);
			this.Controls.Add(this.labelDate);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.butCancel);
			this.Name = "FormGraphEmployeeTime";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Graph Employee Time";
			this.Load += new System.EventHandler(this.FormGraphEmployeeTime_Load);
			this.Paint += new System.Windows.Forms.PaintEventHandler(this.FormGraphEmployeeTime_Paint);
			this.ResumeLayout(false);

		}

		#endregion

		private OpenDental.UI.Button butCancel;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Label labelDate;
		private OpenDental.UI.Button buttonRight;
		private OpenDental.UI.Button buttonLeft;
		private OpenDental.UI.Button butPrint;
	}
}