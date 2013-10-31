namespace OpenDental{
	partial class FormPhoneGraphDateEdit {
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormPhoneGraphDateEdit));
			this.butCancel = new OpenDental.UI.Button();
			this.gridGraph = new OpenDental.UI.ODGrid();
			this.label11 = new System.Windows.Forms.Label();
			this.butEditSchedule = new OpenDental.UI.Button();
			this.SuspendLayout();
			// 
			// butCancel
			// 
			this.butCancel.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butCancel.Autosize = true;
			this.butCancel.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butCancel.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butCancel.CornerRadius = 4F;
			this.butCancel.Location = new System.Drawing.Point(483, 556);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75, 24);
			this.butCancel.TabIndex = 2;
			this.butCancel.Text = "&Close";
			this.butCancel.Click += new System.EventHandler(this.butClose_Click);
			// 
			// gridGraph
			// 
			this.gridGraph.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gridGraph.HScrollVisible = false;
			this.gridGraph.Location = new System.Drawing.Point(12, 31);
			this.gridGraph.Name = "gridGraph";
			this.gridGraph.ScrollValue = 0;
			this.gridGraph.Size = new System.Drawing.Size(546, 519);
			this.gridGraph.TabIndex = 48;
			this.gridGraph.Title = "";
			this.gridGraph.TranslationName = "TablePhoneGraphDate";
			this.gridGraph.CellClick += new OpenDental.UI.ODGridClickEventHandler(this.gridGraph_CellClick);
			// 
			// label11
			// 
			this.label11.Location = new System.Drawing.Point(12, 9);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(546, 19);
			this.label11.TabIndex = 49;
			this.label11.Text = "Click \'Set Graph Status\' column for a given employee to create an override for th" +
    "is date.";
			this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// butEditSchedule
			// 
			this.butEditSchedule.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butEditSchedule.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.butEditSchedule.Autosize = true;
			this.butEditSchedule.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butEditSchedule.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butEditSchedule.CornerRadius = 4F;
			this.butEditSchedule.Location = new System.Drawing.Point(12, 556);
			this.butEditSchedule.Name = "butEditSchedule";
			this.butEditSchedule.Size = new System.Drawing.Size(92, 24);
			this.butEditSchedule.TabIndex = 50;
			this.butEditSchedule.Text = "&Edit Schedule";
			this.butEditSchedule.Click += new System.EventHandler(this.butEditSchedule_Click);
			// 
			// FormPhoneGraphDateEdit
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(570, 592);
			this.Controls.Add(this.butEditSchedule);
			this.Controls.Add(this.label11);
			this.Controls.Add(this.gridGraph);
			this.Controls.Add(this.butCancel);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "FormPhoneGraphDateEdit";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Phone Graph Edits";
			this.Load += new System.EventHandler(this.FormPhoneGraphDateEdit_Load);
			this.ResumeLayout(false);

		}

		#endregion

		private OpenDental.UI.Button butCancel;
		private UI.ODGrid gridGraph;
		private System.Windows.Forms.Label label11;
		private UI.Button butEditSchedule;
	}
}