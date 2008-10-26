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
		private OpenDental.UI.Button butAddAnesthMeds;
		private OpenDental.UI.Button butClose;
		private OpenDental.UI.Button butCancel;
		private OpenDental.UI.Button butAnesthMedIntake;
		private Label labelIntakeNewMeds;
        private ODGrid gridAnesthMedsInventory;
        private OpenDental.UI.Button butAdjustQtys;
        private List<AnesthMed> listAnestheticMeds;

		
		public FormAnestheticMedsInventory(){
			InitializeComponent();
			Lan.F(this);
            FillGrid();
		}

		private void InitializeComponent(){

            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormAnestheticMedsInventory));
            this.groupAnestheticMeds = new System.Windows.Forms.GroupBox();
            this.labelIntakeNewMeds = new System.Windows.Forms.Label();
            this.gridAnesthMedsInventory = new OpenDental.UI.ODGrid();
            this.butAdjustQtys = new OpenDental.UI.Button();
            this.butAnesthMedIntake = new OpenDental.UI.Button();
            this.butClose = new OpenDental.UI.Button();
            this.butCancel = new OpenDental.UI.Button();
            this.butAddAnesthMeds = new OpenDental.UI.Button();
            this.groupAnestheticMeds.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupAnestheticMeds
            // 
            this.groupAnestheticMeds.Controls.Add(this.gridAnesthMedsInventory);
            this.groupAnestheticMeds.Controls.Add(this.butAdjustQtys);
            this.groupAnestheticMeds.Controls.Add(this.labelIntakeNewMeds);
            this.groupAnestheticMeds.Controls.Add(this.butAnesthMedIntake);
            this.groupAnestheticMeds.Controls.Add(this.butClose);
            this.groupAnestheticMeds.Controls.Add(this.butCancel);
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
            // gridAnesthMedsInventory
            // 
            this.gridAnesthMedsInventory.HScrollVisible = false;
            this.gridAnesthMedsInventory.Location = new System.Drawing.Point(117, 28);
            this.gridAnesthMedsInventory.Name = "gridAnesthMedsInventory";
            this.gridAnesthMedsInventory.ScrollValue = 0;
            this.gridAnesthMedsInventory.Size = new System.Drawing.Size(580, 300);
            this.gridAnesthMedsInventory.TabIndex = 144;
            this.gridAnesthMedsInventory.Title = "Anesthetic Medication Inventory";
            this.gridAnesthMedsInventory.TranslationName = "TableAnesthMedsInventory";
            this.gridAnesthMedsInventory.CellDoubleClick += new OpenDental.UI.ODGridClickEventHandler(this.gridAnesthMedsInventory_CellDoubleClick);
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
            this.groupAnestheticMeds.ResumeLayout(false);
            this.ResumeLayout(false);

		}

        private void FormAnestheticMedsInventory_Load(object sender, System.EventArgs e){

            FillGrid();
        }

        private void FillGrid(){

            listAnestheticMeds = AnestheticMeds.CreateObjects();
            gridAnesthMedsInventory.BeginUpdate();
            gridAnesthMedsInventory.Columns.Clear();
            ODGridColumn col = new ODGridColumn(Lan.g("TableAnesthMedsInventory", "Anesthetic Medication"), 200);
            gridAnesthMedsInventory.Columns.Add(col);
            col = new ODGridColumn(Lan.g("TableAnesthMedsInventory", "How Supplied"), 200);
            gridAnesthMedsInventory.Columns.Add(col);
            col = new ODGridColumn(Lan.g("TableAnesthMedsInventory", "Quantity on Hand"), 180);
            gridAnesthMedsInventory.Columns.Add(col);
            gridAnesthMedsInventory.Rows.Clear();
            ODGridRow row;
            for (int i = 0; i < listAnestheticMeds.Count; i++)
            {
                row = new ODGridRow();
                row.Cells.Add(listAnestheticMeds[i].AnesthMedName);
                row.Cells.Add(listAnestheticMeds[i].AnesthHowSupplied);
                row.Cells.Add(listAnestheticMeds[i].QtyOnHand);
                gridAnesthMedsInventory.Rows.Add(row);
            }
            gridAnesthMedsInventory.EndUpdate();
            
        }

		private void butAddAnesthMeds_Click(object sender, EventArgs e){

			FormAnestheticMedsEdit FormME = new FormAnestheticMedsEdit();
			//FormE.AnestheticMedsCur = new AnestheticMeds();
			//FormI.IsNew = true;
			FormME.ShowDialog();
			FillGrid();
		}

		private void butCancel_Click(object sender, EventArgs e){

			DialogResult = DialogResult.Cancel;
		}

		private void gridAnesthMedsInventory_CellContentClick(object sender, DataGridViewCellEventArgs e){

		}

		private void butAnesthMedIntake_Click(object sender, EventArgs e){
			
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

		private void butAdjustQtys_Click(object sender, EventArgs e){

			Userod curUser = Security.CurUser;

			if (GroupPermissions.HasPermission(curUser.UserGroupNum, Permissions.AnesthesiaControlMeds))
			{

				FormAnestheticMedsAdjQtys FormA = new FormAnestheticMedsAdjQtys();
				FormA.ShowDialog();
				return;
			}

			else
			{

				MessageBox.Show(this, "You must be an administrator to unlock this action");
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

        private void gridAnesthMedsInventory_CellDoubleClick(object sender, ODGridClickEventArgs e)
        {
            FormAnestheticMedsEdit FormME =new FormAnestheticMedsEdit();
			FormME.Med=listAnestheticMeds[e.Row];
			FormME.ShowDialog();
            if (FormME.DialogResult == DialogResult.OK)
            {
                FillGrid();
            }
        }
	}
}
