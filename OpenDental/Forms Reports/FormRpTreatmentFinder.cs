using System;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using OpenDental.UI;
using OpenDentBusiness;


namespace OpenDental{
///<summary></summary>
	public class FormRpTreatmentFinder:System.Windows.Forms.Form {
		private OpenDental.UI.Button butCancel;
		private OpenDental.UI.Button butOK;
		private System.ComponentModel.Container components = null;
		private Label label1;
		private CheckBox checkIncludeNoIns;
		private UI.ODGrid gridMain;
		private GroupBox groupBox1;
		private FormQuery FormQuery2;
		private UI.Button butPrint;
		private UI.Button butRefresh;
		private PrintDocument pd;
		private bool headingPrinted;
		private int headingPrintH;
		private int pagesPrinted;

		///<summary></summary>
		public FormRpTreatmentFinder() {
			InitializeComponent();
			Lan.F(this);
		}

		///<summary></summary>
		protected override void Dispose( bool disposing ){
			if( disposing ){
				if(components != null){
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code

		private void InitializeComponent(){
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormRpTreatmentFinder));
			this.label1 = new System.Windows.Forms.Label();
			this.checkIncludeNoIns = new System.Windows.Forms.CheckBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.butRefresh = new OpenDental.UI.Button();
			this.gridMain = new OpenDental.UI.ODGrid();
			this.butCancel = new OpenDental.UI.Button();
			this.butOK = new OpenDental.UI.Button();
			this.butPrint = new OpenDental.UI.Button();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(287,18);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(567,49);
			this.label1.TabIndex = 29;
			this.label1.Text = resources.GetString("label1.Text");
			// 
			// checkIncludeNoIns
			// 
			this.checkIncludeNoIns.Location = new System.Drawing.Point(21,19);
			this.checkIncludeNoIns.Name = "checkIncludeNoIns";
			this.checkIncludeNoIns.Size = new System.Drawing.Size(242,18);
			this.checkIncludeNoIns.TabIndex = 30;
			this.checkIncludeNoIns.Text = "Include patients without insurance";
			this.checkIncludeNoIns.UseVisualStyleBackColor = true;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.butRefresh);
			this.groupBox1.Controls.Add(this.checkIncludeNoIns);
			this.groupBox1.Location = new System.Drawing.Point(12,9);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(269,74);
			this.groupBox1.TabIndex = 33;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "View";
			// 
			// butRefresh
			// 
			this.butRefresh.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.butRefresh.Autosize = true;
			this.butRefresh.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butRefresh.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butRefresh.CornerRadius = 4F;
			this.butRefresh.Location = new System.Drawing.Point(21,43);
			this.butRefresh.Name = "butRefresh";
			this.butRefresh.Size = new System.Drawing.Size(86,26);
			this.butRefresh.TabIndex = 32;
			this.butRefresh.Text = "&Refresh";
			this.butRefresh.Click += new System.EventHandler(this.butRefresh_Click);
			// 
			// gridMain
			// 
			this.gridMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gridMain.HScrollVisible = true;
			this.gridMain.Location = new System.Drawing.Point(3,89);
			this.gridMain.Name = "gridMain";
			this.gridMain.ScrollValue = 0;
			this.gridMain.SelectionMode = OpenDental.UI.GridSelectionMode.MultiExtended;
			this.gridMain.Size = new System.Drawing.Size(852,525);
			this.gridMain.TabIndex = 31;
			this.gridMain.Title = "Treatment Finder";
			this.gridMain.TranslationName = "TableTreatmentFinder";
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
			this.butCancel.Location = new System.Drawing.Point(779,644);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75,24);
			this.butCancel.TabIndex = 4;
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
			this.butOK.Location = new System.Drawing.Point(698,644);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75,24);
			this.butOK.TabIndex = 3;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// butPrint
			// 
			this.butPrint.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butPrint.Autosize = true;
			this.butPrint.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butPrint.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butPrint.CornerRadius = 4F;
			this.butPrint.Image = global::OpenDental.Properties.Resources.butPrintSmall;
			this.butPrint.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butPrint.Location = new System.Drawing.Point(387,644);
			this.butPrint.Name = "butPrint";
			this.butPrint.Size = new System.Drawing.Size(87,24);
			this.butPrint.TabIndex = 34;
			this.butPrint.Text = "Print List";
			this.butPrint.Click += new System.EventHandler(this.butPrint_Click);
			// 
			// FormRpTreatmentFinder
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.ClientSize = new System.Drawing.Size(858,672);
			this.Controls.Add(this.butPrint);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.gridMain);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.butOK);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.Name = "FormRpTreatmentFinder";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Treatment Finder";
			this.Load += new System.EventHandler(this.FormRpTreatmentFinder_Load);
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormRpTreatmentFinder_Load(object sender, System.EventArgs e) {
			//DateTime today=DateTime.Today;
			//will start out 1st through 30th of previous month
			//date1.SelectionStart=new DateTime(today.Year,today.Month,1).AddMonths(-1);
			//date2.SelectionStart=new DateTime(today.Year,today.Month,1).AddDays(-1);
			FillGrid();
		}

		private void FillGrid() {
			gridMain.BeginUpdate();
			gridMain.Columns.Clear();
			ODGridColumn col=new ODGridColumn(Lan.g("TableTreatmentFinder","LName"),100);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableTreatmentFinder","FName"),100);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableTreatmentFinder","Annual Max"),100);
			col.TextAlign=HorizontalAlignment.Right;
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableTreatmentFinder","Amount Used"),100);
			col.TextAlign=HorizontalAlignment.Right;
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableTreatmentFinder","Amount Remaining"),140);
			col.TextAlign=HorizontalAlignment.Right;
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableTreatmentFinder","Treatment Plan"),125);
			col.TextAlign=HorizontalAlignment.Right;
			gridMain.Columns.Add(col);
			gridMain.Rows.Clear();
			DataTable table=Patients.GetTreatmentFinderList(checkIncludeNoIns.Checked);
			ODGridRow row;
			for(int i=0;i<table.Rows.Count;i++) {
			  row=new ODGridRow();
			  for(int j=0;j<table.Columns.Count;j++) {
			    row.Cells.Add(table.Rows[i][j].ToString());
			  }
			  gridMain.Rows.Add(row);
			}
			gridMain.EndUpdate();
		}

		private void butPrint_Click(object sender,EventArgs e) {
			pagesPrinted=0;
			pd=new PrintDocument();
			pd.PrintPage += new PrintPageEventHandler(this.pd_PrintPage);
			pd.DefaultPageSettings.Margins=new Margins(25,25,40,40);
			//pd.OriginAtMargins=true;
			pd.DefaultPageSettings.Landscape=true;
			if(pd.DefaultPageSettings.PaperSize.Height==0) {
				pd.DefaultPageSettings.PaperSize=new PaperSize("default",850,1100);
			}
			headingPrinted=false;
			try {
				#if DEBUG
					FormRpPrintPreview pView = new FormRpPrintPreview();
					pView.printPreviewControl2.Document=pd;
					pView.ShowDialog();
				#else
					if(PrinterL.SetPrinter(pd,PrintSituation.Default)) {
						pd.Print();
					}
				#endif
			}
			catch {
				MessageBox.Show(Lan.g(this,"Printer not available"));
			}
		}
		
		private void pd_PrintPage(object sender,System.Drawing.Printing.PrintPageEventArgs e) {
			Rectangle bounds=e.MarginBounds;
				//new Rectangle(50,40,800,1035);//Some printers can handle up to 1042
			Graphics g=e.Graphics;
			string text;
			Font headingFont=new Font("Arial",13,FontStyle.Bold);
			Font subHeadingFont=new Font("Arial",10,FontStyle.Bold);
			int yPos=bounds.Top;
			int center=bounds.X+bounds.Width/2;
			#region printHeading
			if(!headingPrinted) {
				text=Lan.g(this,"Treatment Finder");
				g.DrawString(text,headingFont,Brushes.Black,center-g.MeasureString(text,headingFont).Width/2,yPos);
				yPos+=(int)g.MeasureString(text,headingFont).Height;
				if(checkIncludeNoIns.Checked) {
					text="Include patients without insurance";
				}
				else {
					text="Only patients with insurance";
				}
				g.DrawString(text,subHeadingFont,Brushes.Black,center-g.MeasureString(text,subHeadingFont).Width/2,yPos);
				yPos+=20;
				headingPrinted=true;
				headingPrintH=yPos;
			}
			#endregion
			int totalPages=gridMain.GetNumberOfPages(bounds,headingPrintH);
			yPos=gridMain.PrintPage(g,pagesPrinted,bounds,headingPrintH);
			pagesPrinted++;
			if(pagesPrinted < totalPages) {
				e.HasMorePages=true;
			}
			else {
				e.HasMorePages=false;
			}
			g.Dispose();
		}

		private void butRefresh_Click(object sender,EventArgs e) {
			FillGrid();
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			//DialogResult=DialogResult.Cancel;
			Close();
		}

		private void butOK_Click(object sender, System.EventArgs e) {
      ReportSimpleGrid report=new ReportSimpleGrid();
      report.Query=@"
DROP TABLE IF EXISTS tempused;
DROP TABLE IF EXISTS tempplanned;
DROP TABLE IF EXISTS tempannualmax;

CREATE TABLE tempused(
PatPlanNum bigint unsigned NOT NULL,
AmtUsed double NOT NULL,
PRIMARY KEY (PatPlanNum));

CREATE TABLE tempplanned(
PatNum bigint unsigned NOT NULL,
AmtPlanned double NOT NULL,
PRIMARY KEY (PatNum));

CREATE TABLE tempannualmax(
PlanNum bigint unsigned NOT NULL,
AnnualMax double NOT NULL,
PRIMARY KEY (PlanNum));

INSERT INTO tempused
SELECT patplan.PatPlanNum,
SUM(IFNULL(claimproc.InsPayAmt,0))
FROM claimproc
LEFT JOIN patplan ON patplan.PatNum = claimproc.PatNum
AND patplan.PlanNum = claimproc.PlanNum
WHERE claimproc.Status IN (1, 3, 4)
AND claimproc.ProcDate BETWEEN makedate(year(curdate()), 1)
AND makedate(year(curdate())+1, 1) /*current calendar year*/
GROUP BY patplan.PatPlanNum;

INSERT INTO tempplanned
SELECT PatNum, SUM(ProcFee)
FROM procedurelog
WHERE ProcStatus = 1 /*treatment planned*/
GROUP BY PatNum;

INSERT INTO tempannualmax
SELECT benefit.PlanNum, benefit.MonetaryAmt
FROM benefit, covcat
WHERE covcat.CovCatNum = benefit.CovCatNum
AND benefit.BenefitType = 5 /* limitation */
AND (covcat.EbenefitCat=1 OR ISNULL(covcat.EbenefitCat))
AND benefit.MonetaryAmt <> 0
GROUP BY benefit.PlanNum
ORDER BY benefit.PlanNum;

SELECT patient.LName, patient.FName,
tempannualmax.AnnualMax $AnnualMax,
tempused.AmtUsed $AmountUsed,
tempannualmax.AnnualMax-IFNULL(tempused.AmtUsed,0) $AmtRemaining,
tempplanned.AmtPlanned $TreatmentPlan
FROM patient
LEFT JOIN tempplanned ON tempplanned.PatNum=patient.PatNum
LEFT JOIN patplan ON patient.PatNum=patplan.PatNum
LEFT JOIN tempused ON tempused.PatPlanNum=patplan.PatPlanNum
LEFT JOIN tempannualmax ON tempannualmax.PlanNum=patplan.PlanNum
	AND tempannualmax.AnnualMax>0
	/*AND tempannualmax.AnnualMax-tempused.AmtUsed>0*/
WHERE tempplanned.AmtPlanned>0 ";
      if(!checkIncludeNoIns.Checked){//if we don't want patients without insurance
        report.Query+="AND AnnualMax > 0 ";
      }
      report.Query+=@"
AND patient.PatStatus =0
ORDER BY tempplanned.AmtPlanned DESC;
DROP TABLE tempused;
DROP TABLE tempplanned;
DROP TABLE tempannualmax;";
      FormQuery2=new FormQuery(report);
      FormQuery2.textTitle.Text="Treatment Finder";
      //FormQuery2.IsReport=true;
      //FormQuery2.SubmitReportQuery();			
      FormQuery2.SubmitQuery();
      FormQuery2.ShowDialog();
			//DialogResult=DialogResult.OK;
		}

		





	}
}
