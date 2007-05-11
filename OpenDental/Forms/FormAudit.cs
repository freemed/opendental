using System;
using System.Drawing;
using System.Drawing.Printing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using OpenDental.UI;
using OpenDentBusiness;

namespace OpenDental{
	/// <summary>
	/// Summary description for FormBasicTemplate.
	/// </summary>
	public class FormAudit : System.Windows.Forms.Form{
		private OpenDental.UI.ODGrid grid;
		private System.Windows.Forms.Label label2;
		private OpenDental.ValidDate textDateFrom;
		private OpenDental.ValidDate textDateTo;
		private System.Windows.Forms.Label label3;
		private OpenDental.UI.Button butRefresh;
		private ComboBox comboPermission;
		private Label label1;
		private Label label4;
		private Label label5;
		private ComboBox comboUser;
		private TextBox textPatient;
		private OpenDental.UI.Button butFind;
		private OpenDental.UI.Button butAll;
		///<summary>The selected patNum.  Can be 0 to include all.</summary>
		private int PatNum;
		private OpenDental.UI.Button butCurrent;
		private OpenDental.UI.Button butPrint;
		///<summary>This gets set externally beforehand.  Lets user quickly select audit trail for current patient.</summary>
		public int CurPatNum;
		private PrintDocument pd;
		private int linesPrinted;
		private OpenDental.UI.PrintPreview printPreview;

		///<summary></summary>
		public FormAudit()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			Lan.F(this);
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormAudit));
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.comboPermission = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.comboUser = new System.Windows.Forms.ComboBox();
			this.textPatient = new System.Windows.Forms.TextBox();
			this.butCurrent = new OpenDental.UI.Button();
			this.butAll = new OpenDental.UI.Button();
			this.butFind = new OpenDental.UI.Button();
			this.butRefresh = new OpenDental.UI.Button();
			this.textDateFrom = new OpenDental.ValidDate();
			this.textDateTo = new OpenDental.ValidDate();
			this.grid = new OpenDental.UI.ODGrid();
			this.butPrint = new OpenDental.UI.Button();
			this.SuspendLayout();
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(0,8);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(75,14);
			this.label2.TabIndex = 45;
			this.label2.Text = "From Date";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(9,30);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(64,13);
			this.label3.TabIndex = 46;
			this.label3.Text = "To Date";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// comboPermission
			// 
			this.comboPermission.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboPermission.FormattingEnabled = true;
			this.comboPermission.Location = new System.Drawing.Point(262,4);
			this.comboPermission.MaxDropDownItems = 40;
			this.comboPermission.Name = "comboPermission";
			this.comboPermission.Size = new System.Drawing.Size(163,21);
			this.comboPermission.TabIndex = 50;
			this.comboPermission.SelectionChangeCommitted += new System.EventHandler(this.comboPermission_SelectionChangeCommitted);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(179,8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(82,13);
			this.label1.TabIndex = 51;
			this.label1.Text = "Permission";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(426,8);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(82,13);
			this.label4.TabIndex = 52;
			this.label4.Text = "Patient";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(179,29);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(82,13);
			this.label5.TabIndex = 55;
			this.label5.Text = "User";
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// comboUser
			// 
			this.comboUser.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboUser.FormattingEnabled = true;
			this.comboUser.Location = new System.Drawing.Point(262,25);
			this.comboUser.MaxDropDownItems = 40;
			this.comboUser.Name = "comboUser";
			this.comboUser.Size = new System.Drawing.Size(163,21);
			this.comboUser.TabIndex = 54;
			this.comboUser.SelectionChangeCommitted += new System.EventHandler(this.comboUser_SelectionChangeCommitted);
			// 
			// textPatient
			// 
			this.textPatient.Location = new System.Drawing.Point(509,4);
			this.textPatient.Name = "textPatient";
			this.textPatient.Size = new System.Drawing.Size(216,20);
			this.textPatient.TabIndex = 56;
			// 
			// butCurrent
			// 
			this.butCurrent.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butCurrent.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butCurrent.Autosize = true;
			this.butCurrent.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butCurrent.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butCurrent.CornerRadius = 4F;
			this.butCurrent.Location = new System.Drawing.Point(509,24);
			this.butCurrent.Name = "butCurrent";
			this.butCurrent.Size = new System.Drawing.Size(63,24);
			this.butCurrent.TabIndex = 59;
			this.butCurrent.Text = "Current";
			this.butCurrent.Click += new System.EventHandler(this.butCurrent_Click);
			// 
			// butAll
			// 
			this.butAll.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butAll.Autosize = true;
			this.butAll.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butAll.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butAll.CornerRadius = 4F;
			this.butAll.Location = new System.Drawing.Point(662,24);
			this.butAll.Name = "butAll";
			this.butAll.Size = new System.Drawing.Size(63,24);
			this.butAll.TabIndex = 58;
			this.butAll.Text = "All";
			this.butAll.Click += new System.EventHandler(this.butAll_Click);
			// 
			// butFind
			// 
			this.butFind.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butFind.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butFind.Autosize = true;
			this.butFind.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butFind.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butFind.CornerRadius = 4F;
			this.butFind.Location = new System.Drawing.Point(585,24);
			this.butFind.Name = "butFind";
			this.butFind.Size = new System.Drawing.Size(63,24);
			this.butFind.TabIndex = 57;
			this.butFind.Text = "Find";
			this.butFind.Click += new System.EventHandler(this.butFind_Click);
			// 
			// butRefresh
			// 
			this.butRefresh.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butRefresh.Autosize = true;
			this.butRefresh.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butRefresh.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butRefresh.CornerRadius = 4F;
			this.butRefresh.Location = new System.Drawing.Point(814,0);
			this.butRefresh.Name = "butRefresh";
			this.butRefresh.Size = new System.Drawing.Size(82,26);
			this.butRefresh.TabIndex = 49;
			this.butRefresh.Text = "Refresh";
			this.butRefresh.Click += new System.EventHandler(this.butRefresh_Click);
			// 
			// textDateFrom
			// 
			this.textDateFrom.Location = new System.Drawing.Point(79,5);
			this.textDateFrom.Name = "textDateFrom";
			this.textDateFrom.Size = new System.Drawing.Size(90,20);
			this.textDateFrom.TabIndex = 47;
			// 
			// textDateTo
			// 
			this.textDateTo.Location = new System.Drawing.Point(79,26);
			this.textDateTo.Name = "textDateTo";
			this.textDateTo.Size = new System.Drawing.Size(90,20);
			this.textDateTo.TabIndex = 48;
			// 
			// grid
			// 
			this.grid.HScrollVisible = false;
			this.grid.Location = new System.Drawing.Point(8,54);
			this.grid.Name = "grid";
			this.grid.ScrollValue = 0;
			this.grid.SelectionMode = OpenDental.UI.GridSelectionMode.MultiExtended;
			this.grid.Size = new System.Drawing.Size(889,578);
			this.grid.TabIndex = 2;
			this.grid.Title = "Audit Trail";
			this.grid.TranslationName = "TableAudit";
			// 
			// butPrint
			// 
			this.butPrint.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butPrint.Autosize = true;
			this.butPrint.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butPrint.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butPrint.CornerRadius = 4F;
			this.butPrint.Image = global::OpenDental.Properties.Resources.butPrint;
			this.butPrint.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butPrint.Location = new System.Drawing.Point(814,26);
			this.butPrint.Name = "butPrint";
			this.butPrint.Size = new System.Drawing.Size(82,26);
			this.butPrint.TabIndex = 60;
			this.butPrint.Text = "Print";
			this.butPrint.Click += new System.EventHandler(this.butPrint_Click);
			// 
			// FormAudit
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.ClientSize = new System.Drawing.Size(905,634);
			this.Controls.Add(this.butPrint);
			this.Controls.Add(this.butCurrent);
			this.Controls.Add(this.butAll);
			this.Controls.Add(this.butFind);
			this.Controls.Add(this.textPatient);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.comboUser);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.comboPermission);
			this.Controls.Add(this.butRefresh);
			this.Controls.Add(this.textDateFrom);
			this.Controls.Add(this.textDateTo);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.grid);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.KeyPreview = true;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormAudit";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Audit Trail";
			this.Load += new System.EventHandler(this.FormAudit_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		private void FormAudit_Load(object sender, System.EventArgs e) {
			textDateFrom.Text=DateTime.Today.AddDays(-10).ToShortDateString();
			textDateTo.Text=DateTime.Today.ToShortDateString();
			for(int i=0;i<Enum.GetNames(typeof(Permissions)).Length;i++){
				if(i==0){
					comboPermission.Items.Add(Lan.g(this,"All"));
				}
				else{
					comboPermission.Items.Add(Enum.GetNames(typeof(Permissions))[i]);
				}
			}
			comboPermission.SelectedIndex=0;
			comboUser.Items.Add(Lan.g(this,"All"));
			comboUser.SelectedIndex=0;
			for(int i=0;i<Userods.Listt.Count;i++){
				comboUser.Items.Add(Userods.Listt[i].UserName);
			}
			PatNum=0;
			FillGrid();
		}

		private void comboUser_SelectionChangeCommitted(object sender,EventArgs e) {
			FillGrid();
		}

		private void comboPermission_SelectionChangeCommitted(object sender,EventArgs e) {
			FillGrid();
		}

		private void butCurrent_Click(object sender,EventArgs e) {
			PatNum=CurPatNum;
			if(PatNum==0){
				textPatient.Text="";
			}
			else{
				textPatient.Text=Patients.GetLim(PatNum).GetNameLF();
			}
			FillGrid();
		}

		private void butFind_Click(object sender,EventArgs e) {
			FormPatientSelect FormP=new FormPatientSelect();
			FormP.ShowDialog();
			if(FormP.DialogResult!=DialogResult.OK){
				return;
			}
			PatNum=FormP.SelectedPatNum;
			textPatient.Text=Patients.GetLim(PatNum).GetNameLF();
			FillGrid();
		}

		private void butAll_Click(object sender,EventArgs e) {
			PatNum=0;
			textPatient.Text="";
			FillGrid();
		}

		private void FillGrid(){
			int userNum=0;
			if(comboUser.SelectedIndex>0){
				userNum=Userods.Listt[comboUser.SelectedIndex-1].UserNum;
			}
			SecurityLog[] logList=SecurityLogs.Refresh(PIn.PDate(textDateFrom.Text),PIn.PDate(textDateTo.Text),
				(Permissions)comboPermission.SelectedIndex,PatNum,userNum);
			grid.BeginUpdate();
			grid.Columns.Clear();
			ODGridColumn col=new ODGridColumn(Lan.g("TableAudit","Date Time"),120);
			grid.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableAudit","User"),70);
			grid.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableAudit","Permission"),110);
			grid.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableAudit","Log Text"),570);
			grid.Columns.Add(col);
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

		private void butRefresh_Click(object sender, System.EventArgs e) {
			if( textDateFrom.Text=="" 
				|| textDateTo.Text==""
				|| textDateFrom.errorProvider1.GetError(textDateFrom)!=""
				|| textDateTo.errorProvider1.GetError(textDateTo)!=""
				){
				MsgBox.Show(this,"Please fix data entry errors first.");
				return;
			}
			FillGrid();
		}

		private void butPrint_Click(object sender,EventArgs e) {
			linesPrinted=0;
			pd=new PrintDocument();
			pd.PrintPage += new PrintPageEventHandler(this.pd_PrintPage);
			pd.DefaultPageSettings.Margins=new Margins(0,0,0,0);
			pd.OriginAtMargins=true;
			if(pd.DefaultPageSettings.PaperSize.Height==0) {
				pd.DefaultPageSettings.PaperSize=new PaperSize("default",850,1100);
			}
			#if DEBUG
			printPreview=new PrintPreview(PrintSituation.Default,pd,1);
			printPreview.ShowDialog();
			#else
				try {
					if(Printers.SetPrinter(pd,PrintSituation.Default)) {
						pd.Print();
					}
				}
				catch {
					MessageBox.Show(Lan.g(this,"Printer not available"));
				}
			#endif
		}

		///<summary>raised for each page to be printed.</summary>
		private void pd_PrintPage(object sender,PrintPageEventArgs e) {
			Graphics g=e.Graphics;
			float yPos=75;
			float xPos=55;
			string str;
			Font font=new Font(FontFamily.GenericSansSerif,8);
			Font fontTitle=new Font(FontFamily.GenericSansSerif,11,FontStyle.Bold);
			Font fontHeader=new Font(FontFamily.GenericSansSerif,8,FontStyle.Bold);
			SolidBrush brush=new SolidBrush(Color.Black);
			Pen pen=new Pen(Color.Black);
			//Title
			str=Text;
			g.DrawString(str,fontTitle,brush,xPos,yPos);
			yPos+=30;
			//define columns
			//ODGridColumn col=new ODGridColumn(Lan.g("TableAudit","Date Time"),120);
			//col=new ODGridColumn(Lan.g("TableAudit","User"),70);
			//col=new ODGridColumn(Lan.g("TableAudit","Permission"),110);
			//col=new ODGridColumn(Lan.g("TableAudit","Log Text"),570);
			int[] colW=new int[4];
			colW[0]=120;//Date Time
			colW[1]=70;//User
			colW[2]=110;//Permission
			colW[3]=470;//Log Text
			int[] colPos=new int[colW.Length+1];
			colPos[0]=30;
			for(int i=1;i<colPos.Length;i++) {
				colPos[i]=colPos[i-1]+colW[i-1];
			}
			string[] ColCaption=new string[4];
			ColCaption[0]=Lan.g("TableAudit","Date Time");
			ColCaption[1]=Lan.g("TableAudit","User");
			ColCaption[2]=Lan.g("TableAudit","Permission");
			ColCaption[3]=Lan.g("TableAudit","Log Text");
			//column headers-----------------------------------------------------------------------------------------
			e.Graphics.FillRectangle(Brushes.LightGray,colPos[0],yPos,colPos[colPos.Length-1]-colPos[0],18);
			e.Graphics.DrawRectangle(pen,colPos[0],yPos,colPos[colPos.Length-1]-colPos[0],18);
			for(int i=1;i<colPos.Length;i++) {
				e.Graphics.DrawLine(new Pen(Color.Black),colPos[i],yPos,colPos[i],yPos+18);
			}
			//Prints the Column Titles
			for(int i=0;i<ColCaption.Length;i++) {
				e.Graphics.DrawString(ColCaption[i],fontHeader,brush,colPos[i]+2,yPos+1);
			}
			yPos+=18;
			while(yPos < e.PageBounds.Height-75-50-32-16 && linesPrinted < grid.Rows.Count) {
				for(int i=0;i<colPos.Length-1;i++) {
					e.Graphics.DrawString(grid.Rows[linesPrinted].Cells[i].Text,font,brush
						,new RectangleF(colPos[i]+2,yPos,colPos[i+1]-colPos[i]-5,font.GetHeight(e.Graphics)));
				}
				//Column lines		
				for(int i=0;i<colPos.Length;i++) {
					e.Graphics.DrawLine(Pens.Gray,colPos[i],yPos+16,colPos[i],yPos);
				}
				linesPrinted++;
				yPos+=16;
				e.Graphics.DrawLine(new Pen(Color.Gray),colPos[0],yPos,colPos[colPos.Length-1],yPos);
			}
			if(linesPrinted==grid.Rows.Count) {
				e.HasMorePages=false;
				linesPrinted=0;//so that after the print preview, it will still print.
			}
			else {
				e.HasMorePages=true;
			}
		}

		

		

		

	


		


	}
}





















