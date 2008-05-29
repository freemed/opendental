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
			// UserControlPhonePanel
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F,13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.gridEmp);
			this.Name = "UserControlPhonePanel";
			this.Size = new System.Drawing.Size(240,323);
			this.ResumeLayout(false);

		}

		#endregion

		private OpenDental.UI.ODGrid gridEmp;
	}
}
