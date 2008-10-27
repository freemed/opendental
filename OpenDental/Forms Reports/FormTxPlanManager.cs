/*=================================================================
Created by Practice-Web Inc. (R) 2008. http://www.practice-web.com
Retain this text in redistributions.
==================================================================*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using OpenDentBusiness;
using OpenDental.UI;

namespace OpenDental
{
	/// <summary>
	/// Summary description for FormTxPlanManager.
	/// </summary>
	public class FormTxPlanManager : System.Windows.Forms.Form
	{
        private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.ListBox listBillType;
		private OpenDental.UI.Button butAll;
		private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblOutstanding;
		private OpenDental.UI.Button butCreateList;
		private OpenDental.UI.Button butCancel;
		private System.Windows.Forms.DateTimePicker dtpDate;
		private System.Windows.Forms.CheckBox checkSecondary;
        private System.Windows.Forms.CheckBox checkPrimary;
        private TextBox textSecondary;
        private TextBox textPrimary;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public FormTxPlanManager()
		{
			InitializeComponent();
			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormTxPlanManager));
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.textSecondary = new System.Windows.Forms.TextBox();
            this.textPrimary = new System.Windows.Forms.TextBox();
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.checkSecondary = new System.Windows.Forms.CheckBox();
            this.checkPrimary = new System.Windows.Forms.CheckBox();
            this.lblOutstanding = new System.Windows.Forms.Label();
            this.listBillType = new System.Windows.Forms.ListBox();
            this.butAll = new OpenDental.UI.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.butCancel = new OpenDental.UI.Button();
            this.butCreateList = new OpenDental.UI.Button();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.textSecondary);
            this.groupBox2.Controls.Add(this.textPrimary);
            this.groupBox2.Controls.Add(this.dtpDate);
            this.groupBox2.Controls.Add(this.checkSecondary);
            this.groupBox2.Controls.Add(this.checkPrimary);
            this.groupBox2.Controls.Add(this.lblOutstanding);
            this.groupBox2.Controls.Add(this.listBillType);
            this.groupBox2.Controls.Add(this.butAll);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.groupBox2.Location = new System.Drawing.Point(8, 8);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(281, 376);
            this.groupBox2.TabIndex = 25;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Filter";
            // 
            // textSecondary
            // 
            this.textSecondary.Location = new System.Drawing.Point(189, 94);
            this.textSecondary.Name = "textSecondary";
            this.textSecondary.Size = new System.Drawing.Size(74, 20);
            this.textSecondary.TabIndex = 32;
            this.textSecondary.Text = "0.00";
            // 
            // textPrimary
            // 
            this.textPrimary.Location = new System.Drawing.Point(189, 68);
            this.textPrimary.Name = "textPrimary";
            this.textPrimary.Size = new System.Drawing.Size(74, 20);
            this.textPrimary.TabIndex = 31;
            this.textPrimary.Text = "100.00";
            // 
            // dtpDate
            // 
            this.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDate.Location = new System.Drawing.Point(13, 42);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.Size = new System.Drawing.Size(97, 20);
            this.dtpDate.TabIndex = 30;
            // 
            // checkSecondary
            // 
            this.checkSecondary.Location = new System.Drawing.Point(13, 96);
            this.checkSecondary.Name = "checkSecondary";
            this.checkSecondary.Size = new System.Drawing.Size(178, 17);
            this.checkSecondary.TabIndex = 28;
            this.checkSecondary.Text = "Secondary Unused Benefits >";
            // 
            // checkPrimary
            // 
            this.checkPrimary.Checked = true;
            this.checkPrimary.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkPrimary.Location = new System.Drawing.Point(13, 67);
            this.checkPrimary.Name = "checkPrimary";
            this.checkPrimary.Size = new System.Drawing.Size(170, 22);
            this.checkPrimary.TabIndex = 26;
            this.checkPrimary.Text = "Primary Unused Benefits >";
            // 
            // lblOutstanding
            // 
            this.lblOutstanding.AutoSize = true;
            this.lblOutstanding.Location = new System.Drawing.Point(6, 24);
            this.lblOutstanding.Name = "lblOutstanding";
            this.lblOutstanding.Size = new System.Drawing.Size(258, 13);
            this.lblOutstanding.TabIndex = 24;
            this.lblOutstanding.Text = "Include anyone with outstanding treatment plan since";
            this.lblOutstanding.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // listBillType
            // 
            this.listBillType.Location = new System.Drawing.Point(10, 142);
            this.listBillType.Name = "listBillType";
            this.listBillType.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.listBillType.Size = new System.Drawing.Size(254, 186);
            this.listBillType.TabIndex = 2;
            // 
            // butAll
            // 
            this.butAll.AdjustImageLocation = new System.Drawing.Point(0, 0);
            this.butAll.Autosize = true;
            this.butAll.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
            this.butAll.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
            this.butAll.CornerRadius = 4F;
            this.butAll.Location = new System.Drawing.Point(93, 334);
            this.butAll.Name = "butAll";
            this.butAll.Size = new System.Drawing.Size(86, 26);
            this.butAll.TabIndex = 15;
            this.butAll.Text = "&All";
            this.butAll.Click += new System.EventHandler(this.butAll_Click);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(10, 123);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(147, 16);
            this.label2.TabIndex = 14;
            this.label2.Text = "Billing Types:";
            // 
            // butCancel
            // 
            this.butCancel.AdjustImageLocation = new System.Drawing.Point(0, 0);
            this.butCancel.Autosize = true;
            this.butCancel.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
            this.butCancel.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
            this.butCancel.CornerRadius = 4F;
            this.butCancel.Location = new System.Drawing.Point(158, 390);
            this.butCancel.Name = "butCancel";
            this.butCancel.Size = new System.Drawing.Size(86, 26);
            this.butCancel.TabIndex = 27;
            this.butCancel.Text = "&Cancel";
            this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
            // 
            // butCreateList
            // 
            this.butCreateList.AdjustImageLocation = new System.Drawing.Point(0, 0);
            this.butCreateList.Autosize = true;
            this.butCreateList.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
            this.butCreateList.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
            this.butCreateList.CornerRadius = 4F;
            this.butCreateList.Location = new System.Drawing.Point(65, 390);
            this.butCreateList.Name = "butCreateList";
            this.butCreateList.Size = new System.Drawing.Size(87, 26);
            this.butCreateList.TabIndex = 26;
            this.butCreateList.Text = "Create &List";
            this.butCreateList.Click += new System.EventHandler(this.butCreateList_Click);
            // 
            // FormTxPlanManager
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(301, 421);
            this.Controls.Add(this.butCancel);
            this.Controls.Add(this.butCreateList);
            this.Controls.Add(this.groupBox2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormTxPlanManager";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tx Plan Analyzer";
            this.Load += new System.EventHandler(this.FormTxPlanManager_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

		}
		#endregion

		private void butCancel_Click(object sender, System.EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
		}

		private void butAll_Click(object sender, System.EventArgs e)
		{
			for(int i=0;i<listBillType.Items.Count;i++)
			{
				listBillType.SetSelected(i,true);
			}
		}

		private void FormTxPlanManager_Load(object sender, System.EventArgs e)
		{
			for(int i=0;i<DefC.Short[(int)DefCat.BillingTypes].Length;i++)
			{
                listBillType.Items.Add(DefC.Short[(int)DefCat.BillingTypes][i].ItemName);
			}
			string[] selectedBillTypes=((Pref)PrefC.HList["BillingSelectBillingTypes"]).ValueString.Split(',');
			for(int i=0;i<selectedBillTypes.Length;i++)
			{
				try
				{
					if(Convert.ToInt32(selectedBillTypes[i])<listBillType.Items.Count)
						listBillType.SetSelected(Convert.ToInt32(selectedBillTypes[i]),true);
				}
				catch{}
			}
			if(listBillType.SelectedIndices.Count==0)
				listBillType.SelectedIndex=0;

			dtpDate.Text  = DateTime.Today.AddYears(-1).ToShortDateString();
		}

		private void butCreateList_Click(object sender, System.EventArgs e)
		{
            if (MessageBox.Show(Lan.g(this, "This may take few minutes. Continue?"), "Tx Plan Analyzer", MessageBoxButtons.OKCancel)
                != DialogResult.OK)
            {
                return;
            }
            FormTxPlanList formTxList = new FormTxPlanList();
			int[] billingTypes=new int[listBillType.SelectedIndices.Count];
			for(int i=0;i<billingTypes.Length;i++)
			{
				billingTypes[i]=listBillType.SelectedIndices[i];
			}
			if(listBillType.SelectedIndices.Count==0)
				listBillType.SelectedIndex=0;
			// converting date in required format CCYY-MM-DD
			string date = dtpDate.Value.ToString("u").Substring(0,10);
			double UnusedPri=0;
            double UnusedSec = -1;
            if (checkPrimary.Checked)
                UnusedPri = PIn.PDouble(textPrimary.Text);
            if (checkSecondary.Checked)
                UnusedSec = PIn.PDouble(textSecondary.Text);
            formTxList.CreateTxPlanList(date, billingTypes, UnusedPri, UnusedSec);
			formTxList.ShowDialog();
		}
	}
}
