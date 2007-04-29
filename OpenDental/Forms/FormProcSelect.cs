using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{
	/// <summary></summary>
	public class FormProcSelect : System.Windows.Forms.Form{
		private OpenDental.UI.Button butCancel;
		private OpenDental.UI.Button butOK;
		private System.Windows.Forms.Label label1;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private int PatNum;
		private OpenDental.TableProcSelect tbProcs;
		private Procedure[] ProcedureList;
		///<summary></summary>
		public int SelectedProcNum;

		///<summary>This form only displays completed procedures to pick from.</summary>
		public FormProcSelect(int patNum)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			Lan.F(this);
			PatNum=patNum;
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormProcSelect));
			this.butCancel = new OpenDental.UI.Button();
			this.butOK = new OpenDental.UI.Button();
			this.tbProcs = new OpenDental.TableProcSelect();
			this.label1 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// butCancel
			// 
			this.butCancel.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butCancel.Autosize = true;
			this.butCancel.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butCancel.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butCancel.CornerRadius = 4F;
			this.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butCancel.Location = new System.Drawing.Point(637,513);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75,26);
			this.butCancel.TabIndex = 0;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.CornerRadius = 4F;
			this.butOK.Location = new System.Drawing.Point(637,472);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75,26);
			this.butOK.TabIndex = 1;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// tbProcs
			// 
			this.tbProcs.BackColor = System.Drawing.SystemColors.Window;
			this.tbProcs.Location = new System.Drawing.Point(40,44);
			this.tbProcs.Name = "tbProcs";
			this.tbProcs.ScrollValue = 1;
			this.tbProcs.SelectedIndices = new int[0];
			this.tbProcs.SelectionMode = System.Windows.Forms.SelectionMode.One;
			this.tbProcs.Size = new System.Drawing.Size(559,494);
			this.tbProcs.TabIndex = 2;
			this.tbProcs.CellDoubleClicked += new OpenDental.ContrTable.CellEventHandler(this.tbProcs_CellDoubleClicked);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(40,8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(582,23);
			this.label1.TabIndex = 3;
			this.label1.Text = "Select a procedure from the list";
			this.label1.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// FormProcSelect
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.ClientSize = new System.Drawing.Size(764,564);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.tbProcs);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.butCancel);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormProcSelect";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Select Procedure";
			this.Load += new System.EventHandler(this.FormProcSelect_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormProcSelect_Load(object sender, System.EventArgs e) {
			Procedure[] entireList=Procedures.Refresh(PatNum);
			ArrayList AL=new ArrayList();
			for(int i=0;i<entireList.Length;i++){
				if(entireList[i].ProcStatus==ProcStat.C){
					AL.Add(entireList[i]);
				}
			}
			ProcedureList=new Procedure[AL.Count];
			AL.CopyTo(ProcedureList);
			tbProcs.ResetRows(ProcedureList.Length);
			tbProcs.SetGridColor(Color.LightGray);
			for(int i=0;i<ProcedureList.Length;i++){
				tbProcs.Cell[0,i]=ProcedureList[i].ProcDate.ToShortDateString();
				tbProcs.Cell[1,i]=Providers.GetAbbr(ProcedureList[i].ProvNum);
				tbProcs.Cell[2,i]=ProcedureCodes.GetStringProcCode(ProcedureList[i].CodeNum);
				tbProcs.Cell[3,i]=Tooth.ToInternat(ProcedureList[i].ToothNum);
				tbProcs.Cell[4,i]=ProcedureCodes.GetProcCode(ProcedureList[i].CodeNum).Descript;
				tbProcs.Cell[5,i]=ProcedureList[i].ProcFee.ToString("F");
			}
			tbProcs.LayoutTables();
		}

		private void tbProcs_CellDoubleClicked(object sender, OpenDental.CellEventArgs e) {
			SelectedProcNum=ProcedureList[e.Row].ProcNum;
			DialogResult=DialogResult.OK;
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			if(tbProcs.SelectedRow==-1){
				MsgBox.Show(this,"Please select an item first.");
				return;
			}
			SelectedProcNum=ProcedureList[tbProcs.SelectedRow].ProcNum;
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		

	


	}
}





















