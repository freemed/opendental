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
			this.components = new System.ComponentModel.Container();
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			this.butOverride = new OpenDental.UI.Button();
			this.gridEmp = new OpenDental.UI.ODGrid();
			this.SuspendLayout();
			// 
			// timer1
			// 
			this.timer1.Interval = 1600;
			this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
			// 
			// butOverride
			// 
			this.butOverride.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butOverride.Autosize = true;
			this.butOverride.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOverride.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOverride.CornerRadius = 4F;
			this.butOverride.Location = new System.Drawing.Point(0,0);
			this.butOverride.Name = "butOverride";
			this.butOverride.Size = new System.Drawing.Size(75,24);
			this.butOverride.TabIndex = 24;
			this.butOverride.Text = "Override";
			this.butOverride.Click += new System.EventHandler(this.butOverride_Click);
			// 
			// gridEmp
			// 
			this.gridEmp.AllowSelection = false;
			this.gridEmp.HScrollVisible = false;
			this.gridEmp.Location = new System.Drawing.Point(0,24);
			this.gridEmp.Name = "gridEmp";
			this.gridEmp.ScrollValue = 0;
			this.gridEmp.Size = new System.Drawing.Size(330,295);
			this.gridEmp.TabIndex = 22;
			this.gridEmp.Title = "Employee";
			this.gridEmp.TranslationName = "TableEmpClock";
			// 
			// UserControlPhonePanel
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F,13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.butOverride);
			this.Controls.Add(this.gridEmp);
			this.Name = "UserControlPhonePanel";
			this.Size = new System.Drawing.Size(330,323);
			this.Load += new System.EventHandler(this.UserControlPhonePanel_Load);
			this.ResumeLayout(false);

		}

		#endregion

		private OpenDental.UI.ODGrid gridEmp;
		private System.Windows.Forms.Timer timer1;
		private OpenDental.UI.Button butOverride;
	}
}
