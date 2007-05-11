using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using OpenDental.UI;
using OpenDentBusiness;

namespace OpenDental{
	/// <summary>
	/// Summary description for FormBasicTemplate.
	/// </summary>
	public class FormAuditOneType : System.Windows.Forms.Form{
		private OpenDental.UI.ODGrid grid;
		private int PatNum;
		private Permissions[] PermTypes;

		///<summary>Supply the patient, types, and title.</summary>
		public FormAuditOneType(int patNum,Permissions[] permTypes,string title)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			Lan.F(this);
			Text=title;
			PatNum=patNum;
			PermTypes=(Permissions[])permTypes.Clone();
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
			this.SuspendLayout();
			// 
			// grid
			// 
			this.grid.Columns.Add(new OpenDental.UI.ODGridColumn("Date Time",120,System.Windows.Forms.HorizontalAlignment.Left));
			this.grid.Columns.Add(new OpenDental.UI.ODGridColumn("User",70,System.Windows.Forms.HorizontalAlignment.Left));
			this.grid.Columns.Add(new OpenDental.UI.ODGridColumn("Permission",110,System.Windows.Forms.HorizontalAlignment.Left));
			this.grid.Columns.Add(new OpenDental.UI.ODGridColumn("Log Text",569,System.Windows.Forms.HorizontalAlignment.Left));
			this.grid.HScrollVisible = false;
			this.grid.Location = new System.Drawing.Point(8,12);
			this.grid.Name = "grid";
			this.grid.ScrollValue = 0;
			this.grid.SelectionMode = OpenDental.UI.GridSelectionMode.MultiExtended;
			this.grid.Size = new System.Drawing.Size(888,611);
			this.grid.TabIndex = 2;
			this.grid.Title = "Audit Trail";
			this.grid.TranslationName = "TableAudit";
			// 
			// FormAuditOneType
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.ClientSize = new System.Drawing.Size(905,634);
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
			SecurityLog[] logList=SecurityLogs.Refresh(PatNum,PermTypes);
			grid.BeginUpdate();
			grid.Rows.Clear();
			ODGridRow row;
			for(int i=0;i<logList.Length;i++){
				row=new ODGridRow();
				row.Cells.Add(logList[i].LogDateTime.ToShortDateString()+" "+logList[i].LogDateTime.ToShortTimeString());
				row.Cells.Add(UserodB.GetUser(logList[i].UserNum).UserName);
				row.Cells.Add(logList[i].PermType.ToString());
				row.Cells.Add(logList[i].LogText);
				grid.Rows.Add(row);
			}
			grid.EndUpdate();
			grid.ScrollToEnd();
		}

		private void butClose_Click(object sender, System.EventArgs e) {
			this.Close();
		}
	


		


	}
}





















