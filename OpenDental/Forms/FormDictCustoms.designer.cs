namespace OpenDental{
	partial class FormDictCustoms {
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormDictCustoms));
			this.butOK = new OpenDental.UI.Button();
			this.gridMain = new OpenDental.UI.ODGrid();
			this.butAdd = new OpenDental.UI.Button();
			this.label4 = new System.Windows.Forms.Label();
			this.textCustom = new System.Windows.Forms.TextBox();
			this.butDelete = new OpenDental.UI.Button();
			this.SuspendLayout();
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.CornerRadius = 4F;
			this.butOK.Location = new System.Drawing.Point(450,446);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75,24);
			this.butOK.TabIndex = 3;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// gridMain
			// 
			this.gridMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gridMain.HScrollVisible = false;
			this.gridMain.Location = new System.Drawing.Point(12,26);
			this.gridMain.Name = "gridMain";
			this.gridMain.ScrollValue = 0;
			this.gridMain.SelectionMode = OpenDental.UI.GridSelectionMode.OneCell;
			this.gridMain.Size = new System.Drawing.Size(218,398);
			this.gridMain.TabIndex = 4;
			this.gridMain.Title = "Custom Words";
			this.gridMain.TranslationName = null;
			this.gridMain.CellLeave += new OpenDental.UI.ODGridClickEventHandler(this.gridMain_CellLeave);
			// 
			// butAdd
			// 
			this.butAdd.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butAdd.Autosize = true;
			this.butAdd.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butAdd.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butAdd.CornerRadius = 4F;
			this.butAdd.Image = global::OpenDental.Properties.Resources.Left;
			this.butAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butAdd.Location = new System.Drawing.Point(245,61);
			this.butAdd.Name = "butAdd";
			this.butAdd.Size = new System.Drawing.Size(75,26);
			this.butAdd.TabIndex = 16;
			this.butAdd.Text = "Add";
			this.butAdd.Click += new System.EventHandler(this.butAdd_Click);
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(334,37);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(176,23);
			this.label4.TabIndex = 15;
			this.label4.Text = "Custom Word";
			this.label4.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// textCustom
			// 
			this.textCustom.Location = new System.Drawing.Point(336,63);
			this.textCustom.Name = "textCustom";
			this.textCustom.Size = new System.Drawing.Size(173,20);
			this.textCustom.TabIndex = 14;
			// 
			// butDelete
			// 
			this.butDelete.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butDelete.Autosize = true;
			this.butDelete.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butDelete.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butDelete.CornerRadius = 4F;
			this.butDelete.Image = global::OpenDental.Properties.Resources.deleteX;
			this.butDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butDelete.Location = new System.Drawing.Point(12,444);
			this.butDelete.Name = "butDelete";
			this.butDelete.Size = new System.Drawing.Size(75,26);
			this.butDelete.TabIndex = 17;
			this.butDelete.Text = "Delete";
			this.butDelete.Click += new System.EventHandler(this.butDelete_Click);
			// 
			// FormDictCustoms
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(537,482);
			this.Controls.Add(this.butDelete);
			this.Controls.Add(this.butAdd);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.textCustom);
			this.Controls.Add(this.gridMain);
			this.Controls.Add(this.butOK);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "FormDictCustoms";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Custom Dictionary";
			this.Load += new System.EventHandler(this.FormDictCustoms_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private OpenDental.UI.Button butOK;
		private UI.ODGrid gridMain;
		private UI.Button butAdd;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox textCustom;
		private UI.Button butDelete;
	}
}