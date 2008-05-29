namespace OpenDental {
	partial class UserControlPhonePanel {
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
			this.gridEmp = new OpenDental.UI.ODGrid();
			this.butRefresh = new OpenDental.UI.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// gridEmp
			// 
			this.gridEmp.AllowSelection = false;
			this.gridEmp.HScrollVisible = false;
			this.gridEmp.Location = new System.Drawing.Point(0,25);
			this.gridEmp.Name = "gridEmp";
			this.gridEmp.ScrollValue = 0;
			this.gridEmp.Size = new System.Drawing.Size(240,297);
			this.gridEmp.TabIndex = 22;
			this.gridEmp.Title = "Employee";
			this.gridEmp.TranslationName = "TableEmpClock";
			// 
			// butRefresh
			// 
			this.butRefresh.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butRefresh.Autosize = true;
			this.butRefresh.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butRefresh.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butRefresh.CornerRadius = 4F;
			this.butRefresh.Location = new System.Drawing.Point(0,1);
			this.butRefresh.Name = "butRefresh";
			this.butRefresh.Size = new System.Drawing.Size(75,23);
			this.butRefresh.TabIndex = 23;
			this.butRefresh.Text = "Refresh";
			this.butRefresh.UseVisualStyleBackColor = true;
			this.butRefresh.Click += new System.EventHandler(this.butRefresh_Click);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(81,5);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(156,16);
			this.label1.TabIndex = 24;
			this.label1.Text = "Still working on better refresh";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// UserControlPhonePanel
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F,13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.label1);
			this.Controls.Add(this.butRefresh);
			this.Controls.Add(this.gridEmp);
			this.Name = "UserControlPhonePanel";
			this.Size = new System.Drawing.Size(240,323);
			this.Load += new System.EventHandler(this.UserControlPhonePanel_Load);
			this.ResumeLayout(false);

		}

		#endregion

		private OpenDental.UI.ODGrid gridEmp;
		private OpenDental.UI.Button butRefresh;
		private System.Windows.Forms.Label label1;
	}
}
