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
	public partial class FormAnestheticMedsInventory : Form
	{
		private GroupBox groupAnestheticMeds;
		public bool IsNew;
		private OpenDental.UI.Button butEditAnesthMeds;
		private OpenDental.UI.Button butAddAnesthMeds;
		private OpenDental.UI.Button butClose;
		private OpenDental.UI.Button butCancel;
		private OpenDental.UI.Button butAnesthMedIntake;
		private Label labelIntakeNewMeds;
		private OpenDental.UI.Button butAdjustQtys;
		private DataGridViewTextBoxColumn AnestheticMed;
		private DataGridViewTextBoxColumn HowSupplied;
		private DataGridViewTextBoxColumn QtyOnHand;
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
			this.labelIntakeNewMeds = new System.Windows.Forms.Label();
			this.butAdjustQtys = new OpenDental.UI.Button();
			this.butAnesthMedIntake = new OpenDental.UI.Button();
			this.butClose = new OpenDental.UI.Button();
			this.butCancel = new OpenDental.UI.Button();
			this.butEditAnesthMeds = new OpenDental.UI.Button();
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
			this.AnestheticMed.HeaderText = "Anesthetic medication";
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
			this.QtyOnHand.HeaderText = "Quantity on hand (mL)";
			this.QtyOnHand.Name = "QtyOnHand";
			this.QtyOnHand.Width = 140;
			// 
			// groupAnestheticMeds
			// 
			this.groupAnestheticMeds.Controls.Add(this.butAdjustQtys);
			this.groupAnestheticMeds.Controls.Add(this.labelIntakeNewMeds);
			this.groupAnestheticMeds.Controls.Add(this.butAnesthMedIntake);
			this.groupAnestheticMeds.Controls.Add(this.butClose);
			this.groupAnestheticMeds.Controls.Add(this.butCancel);
			this.groupAnestheticMeds.Controls.Add(this.gridAnesthMeds);
			this.groupAnestheticMeds.Controls.Add(this.butEditAnesthMeds);
			this.groupAnestheticMeds.Controls.Add(this.butAddAnesthMeds);
			this.groupAnestheticMeds.Location = new System.Drawing.Point(24, 24);
			this.groupAnestheticMeds.Name = "groupAnestheticMeds";
			this.groupAnestheticMeds.Size = new System.Drawing.Size(705, 439);
			this.groupAnestheticMeds.TabIndex = 1;
			this.groupAnestheticMeds.TabStop = false;
			this.groupAnestheticMeds.Text = "Anesthetic Medications";
			// 
			// labelIntakeNewMeds
			// 
			this.labelIntakeNewMeds.Location = new System.Drawing.Point(248, 360);
			this.labelIntakeNewMeds.Name = "labelIntakeNewMeds";
			this.labelIntakeNewMeds.Size = new System.Drawing.Size(272, 26);
			this.labelIntakeNewMeds.TabIndex = 142;
			this.labelIntakeNewMeds.Text = "This button should only be used after anesthetic  medications are added to the li" +
				"st above";
			this.labelIntakeNewMeds.Click += new System.EventHandler(this.labelIntakeNewMeds_Click);
			// 
			// butAdjustQtys
			// 
			this.butAdjustQtys.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butAdjustQtys.Autosize = true;
			this.butAdjustQtys.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butAdjustQtys.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butAdjustQtys.CornerRadius = 4F;
			this.butAdjustQtys.Image = global::OpenDental.Properties.Resources.Add;
			this.butAdjustQtys.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butAdjustQtys.Location = new System.Drawing.Point(106, 392);
			this.butAdjustQtys.Name = "butAdjustQtys";
			this.butAdjustQtys.Size = new System.Drawing.Size(136, 26);
			this.butAdjustQtys.TabIndex = 143;
			this.butAdjustQtys.Text = "Adjust Qty on hand";
			this.butAdjustQtys.UseVisualStyleBackColor = true;
			this.butAdjustQtys.Click += new System.EventHandler(this.butAdjustQtys_Click);
			// 
			// butAnesthMedIntake
			// 
			this.butAnesthMedIntake.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butAnesthMedIntake.Autosize = true;
			this.butAnesthMedIntake.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butAnesthMedIntake.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butAnesthMedIntake.CornerRadius = 4F;
			this.butAnesthMedIntake.Image = global::OpenDental.Properties.Resources.Add;
			this.butAnesthMedIntake.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butAnesthMedIntake.Location = new System.Drawing.Point(106, 360);
			this.butAnesthMedIntake.Name = "butAnesthMedIntake";
			this.butAnesthMedIntake.Size = new System.Drawing.Size(136, 26);
			this.butAnesthMedIntake.TabIndex = 141;
			this.butAnesthMedIntake.Text = "Intake new meds";
			this.butAnesthMedIntake.UseVisualStyleBackColor = true;
			this.butAnesthMedIntake.Click += new System.EventHandler(this.butAnesthMedIntake_Click);
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
			this.butClose.Click += new System.EventHandler(this.butClose_Click);
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
			// butEditAnesthMeds
			// 
			this.butEditAnesthMeds.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butEditAnesthMeds.Autosize = true;
			this.butEditAnesthMeds.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butEditAnesthMeds.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butEditAnesthMeds.CornerRadius = 4F;
			this.butEditAnesthMeds.Image = global::OpenDental.Properties.Resources.butCopy;
			this.butEditAnesthMeds.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butEditAnesthMeds.Location = new System.Drawing.Point(13, 80);
			this.butEditAnesthMeds.Name = "butEditAnesthMeds";
			this.butEditAnesthMeds.Size = new System.Drawing.Size(82, 26);
			this.butEditAnesthMeds.TabIndex = 76;
			this.butEditAnesthMeds.Text = "Edit     ";
			this.butEditAnesthMeds.UseVisualStyleBackColor = true;
			this.butEditAnesthMeds.Click += new System.EventHandler(this.butEditAnesthMeds_Click);
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
			this.butAddAnesthMeds.Text = "Add New";
			this.butAddAnesthMeds.UseVisualStyleBackColor = true;
			this.butAddAnesthMeds.Click += new System.EventHandler(this.butAddAnesthMeds_Click);
			// 
			// FormAnestheticMedsInventory
			// 
			this.ClientSize = new System.Drawing.Size(755, 488);
			this.Controls.Add(this.groupAnestheticMeds);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "FormAnestheticMedsInventory";
			this.Text = "Anesthetic Medication Inventory";
			((System.ComponentModel.ISupportInitialize)(this.gridAnesthMeds)).EndInit();
			this.groupAnestheticMeds.ResumeLayout(false);
			this.ResumeLayout(false);

		}


		private void FormAnestheticMedsInventory_Load(object sender, System.EventArgs e)
		{


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

		private void butAnesthMedIntake_Click(object sender, EventArgs e)
		{
			
				if (!Security.IsAuthorized(Permissions.AnesthesiaIntakeMeds))
				{

					butAnesthMedIntake.Enabled = false;
					return;
				}
			
			else
			{
				FormAnestheticMedsIntake FormI = new FormAnestheticMedsIntake();
				FormI.ShowDialog();
				
				
			
			} 
			
		}

		private void labelIntakeNewMeds_Click(object sender, EventArgs e)
		{

		}

		private void butAdjustQtys_Click(object sender, EventArgs e)
		{
			if (Security.IsAuthorized(Permissions.AnesthesiaControlMeds)){
			    FormAnestheticMedsAdjQtys FormA = new FormAnestheticMedsAdjQtys();
			    FormA.ShowDialog();
                
			}

			else {
                
                return;

			}

		}

		private void butClose_Click(object sender, EventArgs e)
		{

		}

		private void butEditAnesthMeds_Click(object sender, EventArgs e)
		{

		}

		private void butDelAnesthMeds_Click(object sender, EventArgs e)
		{

		}
	}
}
