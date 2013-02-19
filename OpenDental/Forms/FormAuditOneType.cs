using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using OpenDental.UI;
using OpenDentBusiness;

namespace OpenDental{
	/// <summary>This form shows all of the security log entries for one fKey item. So far this only applies to a single appointment or a single procedure code.</summary>
	public class FormAuditOneType : System.Windows.Forms.Form{
		private OpenDental.UI.ODGrid grid;
		private long PatNum;
		private Label labelDisclaimer;
		private List <Permissions> PermTypes;
		private long FKey;

		///<summary>Supply the patient, types, and title.</summary>
		public FormAuditOneType(long patNum,List<Permissions> permTypes,string title,long fKey) {
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			Lan.F(this);
			Text=title;
			PatNum=patNum;
			PermTypes=new List<Permissions>(permTypes);
			FKey=fKey;
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormAuditOneType));
			this.grid = new OpenDental.UI.ODGrid();
			this.labelDisclaimer = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// grid
			// 
			this.grid.HScrollVisible = false;
			this.grid.Location = new System.Drawing.Point(8, 21);
			this.grid.Name = "grid";
			this.grid.ScrollValue = 0;
			this.grid.SelectionMode = OpenDental.UI.GridSelectionMode.MultiExtended;
			this.grid.Size = new System.Drawing.Size(888, 602);
			this.grid.TabIndex = 2;
			this.grid.Title = "Audit Trail";
			this.grid.TranslationName = "TableAudit";
			// 
			// labelDisclaimer
			// 
			this.labelDisclaimer.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelDisclaimer.ForeColor = System.Drawing.Color.Firebrick;
			this.labelDisclaimer.Location = new System.Drawing.Point(8, 3);
			this.labelDisclaimer.Name = "labelDisclaimer";
			this.labelDisclaimer.Size = new System.Drawing.Size(780, 15);
			this.labelDisclaimer.TabIndex = 3;
			this.labelDisclaimer.Text = "Changes made to this appointment before the update to 12.3 will not be reflected " +
    "below, but can be found in the regular audit trail.";
			this.labelDisclaimer.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// FormAuditOneType
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(905, 634);
			this.Controls.Add(this.labelDisclaimer);
			this.Controls.Add(this.grid);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.KeyPreview = true;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormAuditOneType";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Audit Trail";
			this.Load += new System.EventHandler(this.FormAuditOneType_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormAuditOneType_Load(object sender, System.EventArgs e) {
			FillGrid();
		}

		private void FillGrid(){
			SecurityLog[] logList=SecurityLogs.Refresh(PatNum,PermTypes,FKey);
			grid.BeginUpdate();
			grid.Columns.Clear();
			ODGridColumn col;
			col=new ODGridColumn(Lan.g("TableAudit","Date Time"),120);
			grid.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableAudit","User"),70);
			grid.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableAudit","Permission"),110);
			grid.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableAudit","Log Text"),569);
			grid.Columns.Add(col);
			grid.Rows.Clear();
			ODGridRow row;
			for(int i=0;i<logList.Length;i++){
				row=new ODGridRow();
				row.Cells.Add(logList[i].LogDateTime.ToShortDateString()+" "+logList[i].LogDateTime.ToShortTimeString());
				row.Cells.Add(Userods.GetUser(logList[i].UserNum).UserName);
				row.Cells.Add(logList[i].PermType.ToString());
				row.Cells.Add(logList[i].LogText);
				grid.Rows.Add(row);
			}
			if(PermTypes.Contains(Permissions.ProcFeeEdit)) {
				labelDisclaimer.Text="Changes made to this procedure fee before the update to 13.2 were not tracked in the audit trail.";
			}
			grid.EndUpdate();
			grid.ScrollToEnd();
		}

		private void butClose_Click(object sender, System.EventArgs e) {
			this.Close();
		}
	


		


	}
}





















