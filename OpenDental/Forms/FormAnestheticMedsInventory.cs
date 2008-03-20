using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;
using OpenDental.UI;

namespace OpenDental
{
	public partial class FormAnestheticMedsInventory : Form{
		private DataGridViewTextBoxColumn AnestheticMed;
		private DataGridViewTextBoxColumn HowSupplied;
		private DataGridViewTextBoxColumn QtyOnHand;
		private GroupBox groupAnestheticMeds;
		private OpenDental.UI.Button butDelAnesthMeds;
		private OpenDental.UI.Button butAddAnesthMeds;
		private OpenDental.UI.Button butClose;
		private OpenDental.UI.Button butCancel;
		private DataGridView gridAnesthMeds;
		
		public FormAnestheticMedsInventory()
		{
			InitializeComponent();
			Lan.F(this);

		}

		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormAnestheticMedsInventory));
			this.gridAnesthMeds = new System.Windows.Forms.DataGridView();
			this.AnestheticMed = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.HowSupplied = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.QtyOnHand = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.groupAnestheticMeds = new System.Windows.Forms.GroupBox();
			this.butClose = new OpenDental.UI.Button();
			this.butCancel = new OpenDental.UI.Button();
			this.butDelAnesthMeds = new OpenDental.UI.Button();
			this.butAddAnesthMeds = new OpenDental.UI.Button();
			((System.ComponentModel.ISupportInitialize)(this.gridAnesthMeds)).BeginInit();
			this.groupAnestheticMeds.SuspendLayout();
			this.SuspendLayout();
			// 
			// gridAnesthMeds
			// 
			this.gridAnesthMeds.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.gridAnesthMeds.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.AnestheticMed,
            this.HowSupplied,
            this.QtyOnHand});
			this.gridAnesthMeds.Location = new System.Drawing.Point(106, 34);
			this.gridAnesthMeds.Name = "gridAnesthMeds";
			this.gridAnesthMeds.Size = new System.Drawing.Size(582, 307);
			this.gridAnesthMeds.TabIndex = 0;
			this.gridAnesthMeds.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridAnesthMeds_CellContentClick);
			// 
			// AnestheticMed
			// 
			this.AnestheticMed.HeaderText = "Anesthetic";
			this.AnestheticMed.Name = "AnestheticMed";
			this.AnestheticMed.Width = 240;
			// 
			// HowSupplied
			// 
			this.HowSupplied.HeaderText = "How supplied";
			this.HowSupplied.Name = "HowSupplied";
			this.HowSupplied.Width = 160;
			// 
			// QtyOnHand
			// 
			this.QtyOnHand.HeaderText = "Quantity on hand";
			this.QtyOnHand.Name = "QtyOnHand";
			this.QtyOnHand.Width = 140;
			// 
			// groupAnestheticMeds
			// 
			this.groupAnestheticMeds.Controls.Add(this.butClose);
			this.groupAnestheticMeds.Controls.Add(this.butCancel);
			this.groupAnestheticMeds.Controls.Add(this.gridAnesthMeds);
			this.groupAnestheticMeds.Controls.Add(this.butDelAnesthMeds);
			this.groupAnestheticMeds.Controls.Add(this.butAddAnesthMeds);
			this.groupAnestheticMeds.Location = new System.Drawing.Point(24, 24);
			this.groupAnestheticMeds.Name = "groupAnestheticMeds";
			this.groupAnestheticMeds.Size = new System.Drawing.Size(705, 403);
			this.groupAnestheticMeds.TabIndex = 1;
			this.groupAnestheticMeds.TabStop = false;
			this.groupAnestheticMeds.Text = "Anesthetic Medications";
			// 
			// butClose
			// 
			this.butClose.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butClose.Autosize = true;
			this.butClose.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butClose.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butClose.CornerRadius = 4F;
			this.butClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butClose.Location = new System.Drawing.Point(598, 360);
			this.butClose.Name = "butClose";
			this.butClose.Size = new System.Drawing.Size(90, 26);
			this.butClose.TabIndex = 140;
			this.butClose.Text = "Save and Close";
			this.butClose.UseVisualStyleBackColor = true;
			// 
			// butCancel
			// 
			this.butCancel.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butCancel.Autosize = true;
			this.butCancel.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butCancel.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butCancel.CornerRadius = 4F;
			this.butCancel.Image = global::OpenDental.Properties.Resources.deleteX;
			this.butCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butCancel.Location = new System.Drawing.Point(526, 360);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(66, 26);
			this.butCancel.TabIndex = 139;
			this.butCancel.Text = "Cancel";
			this.butCancel.UseVisualStyleBackColor = true;
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// butDelAnesthMeds
			// 
			this.butDelAnesthMeds.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butDelAnesthMeds.Autosize = true;
			this.butDelAnesthMeds.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butDelAnesthMeds.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butDelAnesthMeds.CornerRadius = 4F;
			this.butDelAnesthMeds.Image = global::OpenDental.Properties.Resources.deleteX;
			this.butDelAnesthMeds.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butDelAnesthMeds.Location = new System.Drawing.Point(13, 80);
			this.butDelAnesthMeds.Name = "butDelAnesthMeds";
			this.butDelAnesthMeds.Size = new System.Drawing.Size(82, 26);
			this.butDelAnesthMeds.TabIndex = 76;
			this.butDelAnesthMeds.Text = "Delete";
			this.butDelAnesthMeds.UseVisualStyleBackColor = true;
			// 
			// butAddAnesthMeds
			// 
			this.butAddAnesthMeds.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butAddAnesthMeds.Autosize = true;
			this.butAddAnesthMeds.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butAddAnesthMeds.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butAddAnesthMeds.CornerRadius = 4F;
			this.butAddAnesthMeds.Image = global::OpenDental.Properties.Resources.Add;
			this.butAddAnesthMeds.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butAddAnesthMeds.Location = new System.Drawing.Point(13, 48);
			this.butAddAnesthMeds.Name = "butAddAnesthMeds";
			this.butAddAnesthMeds.Size = new System.Drawing.Size(82, 26);
			this.butAddAnesthMeds.TabIndex = 75;
			this.butAddAnesthMeds.Text = "Add";
			this.butAddAnesthMeds.UseVisualStyleBackColor = true;
			this.butAddAnesthMeds.Click += new System.EventHandler(this.butAddAnesthMeds_Click);
			// 
			// FormAnestheticMedsInventory
			// 
			this.ClientSize = new System.Drawing.Size(755, 451);
			this.Controls.Add(this.groupAnestheticMeds);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "FormAnestheticMedsInventory";
			this.Text = "Anesthetic Medication Inventory";
			((System.ComponentModel.ISupportInitialize)(this.gridAnesthMeds)).EndInit();
			this.groupAnestheticMeds.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		private void butAddAnesthMeds_Click(object sender, EventArgs e)
		{
			FormAnestheticMedsEdit FormE = new FormAnestheticMedsEdit();
			//FormE.AnestheticMedsCur = new AnestheticMeds();
			//FormI.IsNew = true;
			FormE.ShowDialog();
			//FillGrid();
		}

		private void butCancel_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
		}

		private void gridAnesthMeds_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{

		}
	}
}
