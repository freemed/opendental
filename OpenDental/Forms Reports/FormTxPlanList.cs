/*=================================================================
Created by Practice-Web Inc. (R) 2008. http://www.practice-web.com
Retain this text in redistributions.
==================================================================*/
using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Data.OleDb;
using System.Text;
using System.IO;
using System.Drawing.Printing;
using System.Globalization;
using OpenDentBusiness;
namespace OpenDental
{
	/// <summary>
    /// Summary description for FormTxPlanList.
	/// </summary>
	public class FormTxPlanList : System.Windows.Forms.Form
    {
		private DataTable AddrTable;
		private int patientsPrinted;
		private int pagesPrinted;
		private PrintDocument pd;
		private OpenDental.UI.PrintPreview printPreview;
		
		///<summary>Set this list externally before openning the this window.</summary>
        public PatAging[] AgingList;
		private OpenDental.UI.ODGrid gridTxPlanList;
        private OpenDental.UI.Button butClose;
        private OpenDental.UI.Button butLabels;
        private OpenDental.UI.Button butPrint;
        private GroupBox groupBox4;
        private Label label4;
        private TextBox textPostcardMessage;
        private OpenDental.UI.Button butPreview;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public FormTxPlanList()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormTxPlanList));
            this.butClose = new OpenDental.UI.Button();
            this.gridTxPlanList = new OpenDental.UI.ODGrid();
            this.butLabels = new OpenDental.UI.Button();
            this.butPrint = new OpenDental.UI.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textPostcardMessage = new System.Windows.Forms.TextBox();
            this.butPreview = new OpenDental.UI.Button();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // butClose
            // 
            this.butClose.AdjustImageLocation = new System.Drawing.Point(50, 50);
            this.butClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.butClose.Autosize = true;
            this.butClose.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
            this.butClose.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
            this.butClose.CornerRadius = 4F;
            this.butClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.butClose.Location = new System.Drawing.Point(820, 497);
            this.butClose.Name = "butClose";
            this.butClose.Size = new System.Drawing.Size(104, 26);
            this.butClose.TabIndex = 36;
            this.butClose.Text = "&Close";
            this.butClose.Click += new System.EventHandler(this.butClose_Click);
            // 
            // gridTxPlanList
            // 
            this.gridTxPlanList.HScrollVisible = true;
            this.gridTxPlanList.Location = new System.Drawing.Point(12, 12);
            this.gridTxPlanList.Name = "gridTxPlanList";
            this.gridTxPlanList.ScrollValue = 0;
            this.gridTxPlanList.SelectionMode = OpenDental.UI.GridSelectionMode.MultiExtended;
            this.gridTxPlanList.Size = new System.Drawing.Size(656, 511);
            this.gridTxPlanList.TabIndex = 35;
            this.gridTxPlanList.Title = "Tx Plan Analyzer List";
            this.gridTxPlanList.TranslationName = "TableBilling";
            // 
            // butLabels
            // 
            this.butLabels.AdjustImageLocation = new System.Drawing.Point(0, 0);
            this.butLabels.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.butLabels.Autosize = true;
            this.butLabels.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
            this.butLabels.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
            this.butLabels.CornerRadius = 4F;
            this.butLabels.Image = ((System.Drawing.Image)(resources.GetObject("butLabels.Image")));
            this.butLabels.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.butLabels.Location = new System.Drawing.Point(693, 268);
            this.butLabels.Name = "butLabels";
            this.butLabels.Size = new System.Drawing.Size(104, 26);
            this.butLabels.TabIndex = 39;
            this.butLabels.Text = "Labels";
            this.butLabels.Click += new System.EventHandler(this.butLabels_Click);
            // 
            // butPrint
            // 
            this.butPrint.AdjustImageLocation = new System.Drawing.Point(0, 0);
            this.butPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.butPrint.Autosize = true;
            this.butPrint.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
            this.butPrint.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
            this.butPrint.CornerRadius = 4F;
            this.butPrint.Location = new System.Drawing.Point(820, 268);
            this.butPrint.Name = "butPrint";
            this.butPrint.Size = new System.Drawing.Size(104, 26);
            this.butPrint.TabIndex = 38;
            this.butPrint.Text = "R&un Report";
            this.butPrint.Click += new System.EventHandler(this.butPrint_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label4);
            this.groupBox4.Controls.Add(this.textPostcardMessage);
            this.groupBox4.Controls.Add(this.butPreview);
            this.groupBox4.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.groupBox4.Location = new System.Drawing.Point(684, 12);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(248, 248);
            this.groupBox4.TabIndex = 40;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Postcards";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(10, 11);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(158, 19);
            this.label4.TabIndex = 18;
            this.label4.Text = "Message";
            this.label4.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // textPostcardMessage
            // 
            this.textPostcardMessage.AcceptsReturn = true;
            this.textPostcardMessage.Location = new System.Drawing.Point(8, 32);
            this.textPostcardMessage.Multiline = true;
            this.textPostcardMessage.Name = "textPostcardMessage";
            this.textPostcardMessage.Size = new System.Drawing.Size(232, 168);
            this.textPostcardMessage.TabIndex = 17;
            this.textPostcardMessage.Text = resources.GetString("textPostcardMessage.Text");
            // 
            // butPreview
            // 
            this.butPreview.AdjustImageLocation = new System.Drawing.Point(0, 0);
            this.butPreview.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.butPreview.Autosize = true;
            this.butPreview.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
            this.butPreview.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
            this.butPreview.CornerRadius = 4F;
            this.butPreview.Image = ((System.Drawing.Image)(resources.GetObject("butPreview.Image")));
            this.butPreview.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.butPreview.Location = new System.Drawing.Point(135, 208);
            this.butPreview.Name = "butPreview";
            this.butPreview.Size = new System.Drawing.Size(104, 26);
            this.butPreview.TabIndex = 16;
            this.butPreview.Text = "Preview";
            this.butPreview.Click += new System.EventHandler(this.butPreview_Click);
            // 
            // FormTxPlanList
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(943, 568);
            this.Controls.Add(this.butLabels);
            this.Controls.Add(this.butPrint);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.butClose);
            this.Controls.Add(this.gridTxPlanList);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormTxPlanList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tx Plan Analyzer List";
            this.Load += new System.EventHandler(this.FormTxPlanList_Load);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);

		}
		#endregion

        private void FormTxPlanList_Load(object sender, System.EventArgs e)
		{
		}
		// calls this function from TxPlanAnalyzer to create array of patients to display in grid  MSN 05/22/2006
		public void CreateTxPlanList(string procDate,int[] billingTypes,double UnusedPri,double UnusedSec) 
		{
            string command;
            command = "SELECT distinct patient.PatNum," +
                      "patient.LName,patient.FName,patient.MiddleI," +
                      "patient.Address,patient.Address2,patient.City," +
                      "patient.State,patient.Zip,patient.HmPhone,patient.WkPhone," + 
                      "patient.AddrNote " +
                      "FROM  patplan " +
                        "Inner Join patient ON patient.PatNum = patplan.PatNum " +
                        "Inner Join procedurelog ON patplan.PatNum = procedurelog.PatNum " +
                        " Where patplan.ordinal in (1,2) " +
                        " and patient.patstatus = 0 " +
                        "and procedurelog.procstatus = 1 " +
                        "and billingtype in (";
            // Go through Billing types and build SQL IN Clause 
            // For ex BillingType in (40,41)
            for (int i = 0; i <= billingTypes.Length-1; i++)
            {
                command += DefC.Short[(int)DefCat.BillingTypes][billingTypes[i]].DefNum;
                if (i != billingTypes.Length-1)
                    command += ",";
                else
                    command += ") ORDER BY patient.LName,patient.FName";
            }
            AddrTable = General.GetTable(command);
            int PatNum;
            // Variables for Primary Insurance
            double PriMax;
            double PriDed;
            double PriDedRemain;
            double PriUsed;
            double PriPend;
            double PriRemain;

            // Variables for Secondary Insurance
            double SecMax;
            double SecDed;
            double SecDedRemain;
            double SecUsed;
            double SecPend;
            double SecRemain;
            gridTxPlanList.BeginUpdate();
            gridTxPlanList.Columns.Clear();

            // Create Columns
            gridTxPlanList.Columns.Add(new OpenDental.UI.ODGridColumn("Chart #", 50, System.Windows.Forms.HorizontalAlignment.Left));
            gridTxPlanList.Columns.Add(new OpenDental.UI.ODGridColumn("Name", 190, System.Windows.Forms.HorizontalAlignment.Left));
            gridTxPlanList.Columns.Add(new OpenDental.UI.ODGridColumn("TxPln Date", 80, HorizontalAlignment.Left));
            gridTxPlanList.Columns.Add(new OpenDental.UI.ODGridColumn("Th#", 30, HorizontalAlignment.Center));
            gridTxPlanList.Columns.Add(new OpenDental.UI.ODGridColumn("Surf", 45, HorizontalAlignment.Center));
            gridTxPlanList.Columns.Add(new OpenDental.UI.ODGridColumn("Code", 50, HorizontalAlignment.Center));
            gridTxPlanList.Columns.Add(new OpenDental.UI.ODGridColumn("Description", 220, HorizontalAlignment.Left));
            gridTxPlanList.Columns.Add(new OpenDental.UI.ODGridColumn("Fee", 40, HorizontalAlignment.Right));
            gridTxPlanList.Columns.Add(new OpenDental.UI.ODGridColumn("Pat.Pays", 60, HorizontalAlignment.Right));
            gridTxPlanList.Columns.Add(new OpenDental.UI.ODGridColumn("Ins.Pays", 60, HorizontalAlignment.Right));
            gridTxPlanList.Columns.Add(new OpenDental.UI.ODGridColumn("Unused Pri.", 80, System.Windows.Forms.HorizontalAlignment.Center)); // centered for better readbility, SPK
            gridTxPlanList.Columns.Add(new OpenDental.UI.ODGridColumn("Unused Sec.", 80, System.Windows.Forms.HorizontalAlignment.Center)); // centered for better readbility, SPK

            // Create Rows
            gridTxPlanList.Rows.Clear();
            for (int i = 0; i < AddrTable.Rows.Count - 1; i++)
            {
                PatNum = PIn.PInt(AddrTable.Rows[i]["PatNum"].ToString());
                PriMax = 0;
                PriPend = 0;
                PriUsed = 0;
                PriDed = 0;
                PriDedRemain = 0;
                PriRemain = 0;
                SecMax = 0;
                SecPend = 0;
                SecUsed = 0;
                SecDed = 0;
                SecDedRemain = 0;
                SecRemain = 0;
                FillSummary(PatNum, ref PriMax,
                    ref PriPend, ref PriUsed, ref PriDed, ref PriDedRemain, ref PriRemain,
                    ref SecMax, ref SecPend, ref SecUsed, ref SecDed, ref SecDedRemain, 
                    ref SecRemain);
                if (PriRemain > UnusedPri && SecRemain > UnusedSec) {
                    GetPatientTreatments(PatNum,procDate,PriRemain.ToString(),SecRemain.ToString());
                }
                //row.Tag = table.Rows[i];
            }
            gridTxPlanList.EndUpdate();
            gridTxPlanList.ScrollToEnd();
            gridTxPlanList.SetSelected(true);	
        }
        /// <summary>
        /// 
        /// </summary>
        private void GetPatientTreatments(int PatNum,string procDate,string PriRemain, string SecRemain)
        {
            string command;
            DataTable PatientTreatmentTable;
            Patient PatCur;
            OpenDental.UI.ODGridRow row;
            PatCur = Patients.GetPat(PatNum);

            command = "SELECT date_format(procedurelog.DateTP,'%m/%d/%Y') AS ProcDate," +
                      "procedurelog.ToothNum,procedurelog.Surf,procedurecode.ProcCode," +
                      "procedurecode.Descript,procedurelog.ProcFee,claimproc.InsPayEst," +
                      "ifNull(procedurelog.ProcFee,0) - ifNull(claimproc.InsPayEst,0) as PatPays " +
                      "FROM procedurelog,procedurecode,claimproc " +
                      "WHERE procedurelog.PatNum = " + PatNum.ToString() + 
                      " AND claimproc.ProcNum = procedurelog.ProcNum " +
                      "AND procedurelog.CodeNum = procedurecode.CodeNum " +
                      "AND procedurelog.ProcStatus = 1 " +
                      "AND procedurelog.ProcFee > 0 " +
                      "AND procedurelog.DateTP >= '" + procDate + "'";
            // "AND procedurelog.PatNum = claimproc.PatNum " + Removed SPK     
            PatientTreatmentTable = General.GetTable(command);
            for (int i = 0; i <= PatientTreatmentTable.Rows.Count - 1; i++)
            {
                row = new OpenDental.UI.ODGridRow();
                if (i == 0)
                {
                    row.Cells.Add(PatCur.ChartNumber);
                    row.Cells.Add(PatCur.LName + ", " + PatCur.FName + " " + PatCur.MiddleI + "\r\n" + "W: " + PatCur.WkPhone + " H: " + PatCur.WkPhone);
                }
                else
                {
                    row.Cells.Add("");
                    row.Cells.Add("");
                }
                row.Cells.Add(PatientTreatmentTable.Rows[i]["ProcDate"].ToString());
                row.Cells.Add(PatientTreatmentTable.Rows[i]["ToothNum"].ToString());
                row.Cells.Add(PatientTreatmentTable.Rows[i]["Surf"].ToString());
                row.Cells.Add(PatientTreatmentTable.Rows[i]["ProcCode"].ToString());
                row.Cells.Add(PatientTreatmentTable.Rows[i]["Descript"].ToString());
                row.Cells.Add(PatientTreatmentTable.Rows[i]["ProcFee"].ToString());
                row.Cells.Add(PatientTreatmentTable.Rows[i]["PatPays"].ToString());
                row.Cells.Add(PatientTreatmentTable.Rows[i]["InsPayEst"].ToString());
                if (i == 0)
                {
                    row.Cells.Add(PriRemain.ToString());
                    row.Cells.Add(SecRemain.ToString());
                }
                else
                {
                    row.Cells.Add("");
                    row.Cells.Add("");
                }
                gridTxPlanList.Rows.Add(row);
            }
        }

        /// <summary>
        /// FillSummary calculates Primary Remaining and Secondary Remaining
        /// It also returns Annual Maximum, Ben Used, pending, Deductible, and Deductible Remaining
        /// for both primary and secondary benefits
        /// </summary>
        private void FillSummary(int PatNum,
            ref double PriMax, ref double PriPend, ref double PriUsed,
            ref double PriDed, ref double PriDedRemain, ref double PriRemain,
            ref double SecMax, ref double SecPend, ref double SecUsed,
            ref double SecDed, ref double SecDedRemain, ref double SecRemain)
        {
            PatPlan[] PatPlanList;
            InsPlan[] InsPlanList;
            Benefit[] BenefitList;
            ClaimProc[] ClaimProcList;
            Family FamCur;

            double max = 0;
            double ded = 0;
            double dedUsed = 0;
            double remain = 0;
            double pend = 0;
            double used = 0;
            FamCur = Patients.GetFamily(PatNum);
            InsPlanList = InsPlans.Refresh(FamCur);
            PatPlanList = PatPlans.Refresh(PatNum);
            BenefitList = Benefits.Refresh(PatPlanList);
            Claims.Refresh(PatNum);
            ClaimProcList = ClaimProcs.Refresh(PatNum);

            InsPlan PlanCur; //=new InsPlan();
            if (PatPlanList.Length > 0)
            {
                PlanCur = InsPlans.GetPlan(PatPlanList[0].PlanNum, InsPlanList);
                pend = InsPlans.GetPending
                   (ClaimProcList, DateTime.Today,PlanCur, PatPlanList[0].PatPlanNum, -1, BenefitList);
                PriPend = pend;
                used = InsPlans.GetInsUsed
                    (ClaimProcList, DateTime.Today, PlanCur.PlanNum, PatPlanList[0].PatPlanNum, -1, InsPlanList, BenefitList);
                PriUsed = used;
                max = Benefits.GetAnnualMax(BenefitList, PlanCur.PlanNum, PatPlanList[0].PatPlanNum);
                if (max == -1)
                {//if annual max is blank
                    PriMax = 0;
                    PriRemain = 0;
                }
                else
                {
                    remain = max - used - pend;
                    if (remain < 0)
                    {
                        remain = 0;
                    }
                    PriMax = max;
                    PriRemain = remain;
                }
                //deductible:
                ded = Benefits.GetDeductible(BenefitList, PlanCur.PlanNum, PatPlanList[0].PatPlanNum);
                if (ded != -1)
                {
                    PriDed = ded;
                    dedUsed = InsPlans.GetDedUsed
                        (ClaimProcList, DateTime.Today, PlanCur.PlanNum, PatPlanList[0].PatPlanNum, -1, InsPlanList, BenefitList);
                    PriDedRemain = (ded - dedUsed);
                }
            }
            if (PatPlanList.Length > 1)
            {
                PlanCur = InsPlans.GetPlan(PatPlanList[1].PlanNum, InsPlanList);
                pend = InsPlans.GetPending // changed below, SPK
                    (ClaimProcList, DateTime.Today, PlanCur, PatPlanList[1].PatPlanNum, -1, BenefitList);
                SecPend = pend;
                used = InsPlans.GetInsUsed
                    (ClaimProcList, DateTime.Today, PlanCur.PlanNum, PatPlanList[1].PatPlanNum, -1, InsPlanList, BenefitList);
                SecUsed = used;
                max = Benefits.GetAnnualMax(BenefitList, PlanCur.PlanNum, PatPlanList[1].PatPlanNum);
                if (max == -1)
                {
                    SecMax = 0;
                    SecRemain = 0;
                }
                else
                {
                    remain = max - used - pend;
                    if (remain < 0)
                    {
                        remain = 0;
                    }
                    SecMax = max;
                    SecRemain = remain;
                }
                ded = Benefits.GetDeductible(BenefitList, PlanCur.PlanNum, PatPlanList[1].PatPlanNum);
                if (ded != -1)
                {
                    SecDed = ded;
                    dedUsed = InsPlans.GetDedUsed
                        (ClaimProcList, DateTime.Today, PlanCur.PlanNum, PatPlanList[1].PatPlanNum, -1, InsPlanList, BenefitList);
                    SecDedRemain = ded - dedUsed;
                }
            }
        }

       private void butClose_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void butLabels_Click(object sender, System.EventArgs e)
        {
            if (AddrTable.Rows.Count < 1)
            {
                MessageBox.Show(Lan.g(this, "There are no Patients in the list.  Must have at least one to print."));
                return;
            }
            pagesPrinted = 0;
            patientsPrinted = 0;
            pd = new PrintDocument();
            pd.PrintPage += new PrintPageEventHandler(this.pdLabels_PrintPage);
            pd.OriginAtMargins = true;
            pd.DefaultPageSettings.Margins = new Margins(0, 0, 0, 0);
            printPreview = new OpenDental.UI.PrintPreview(PrintSituation.LabelSheet
                , pd, (int)Math.Ceiling((double)AddrTable.Rows.Count / 30));
            //printPreview.Document=pd;
            //printPreview.TotalPages=;
            printPreview.ShowDialog();
        }
        ///<summary>raised for each page to be printed.</summary>
        private void pdLabels_PrintPage(object sender, PrintPageEventArgs ev)
        {
            int totalPages = (int)Math.Ceiling((double)AddrTable.Rows.Count / 30);
            Graphics g = ev.Graphics;
            float yPos = 75;
            float xPos = 50;
            string text = "";
            while (yPos < 1000 && patientsPrinted < AddrTable.Rows.Count)
            {
                text = AddrTable.Rows[patientsPrinted]["FName"].ToString() + " "
                    + AddrTable.Rows[patientsPrinted]["MiddleI"].ToString() + " "
                    + AddrTable.Rows[patientsPrinted]["LName"].ToString() + "\r\n"
                    + AddrTable.Rows[patientsPrinted]["Address"].ToString() + "\r\n";
                if (AddrTable.Rows[patientsPrinted]["Address2"].ToString() != "")
                {
                    text += AddrTable.Rows[patientsPrinted]["Address2"].ToString() + "\r\n";
                }
                text += AddrTable.Rows[patientsPrinted]["City"].ToString() + ", "
                    + AddrTable.Rows[patientsPrinted]["State"].ToString() + "   "
                    + AddrTable.Rows[patientsPrinted]["Zip"].ToString() + "\r\n";
                g.DrawString(text, new Font(FontFamily.GenericSansSerif, 11), Brushes.Black, xPos, yPos);
                //reposition for next label
                xPos += 275;
                if (xPos > 850)
                {//drop a line
                    xPos = 50;
                    yPos += 100;
                }
                patientsPrinted++;
            }
            pagesPrinted++;
            if (pagesPrinted == totalPages)
            {
                ev.HasMorePages = false;
                pagesPrinted = 0;//because it has to print again from the print preview
                patientsPrinted = 0;
            }
            else
            {
                ev.HasMorePages = true;
            }
        }

        private void butPrint_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(Lan.g(this, "This may take few minutes. Continue?"), "Tx Plan Analyzer", MessageBoxButtons.OKCancel)
                != DialogResult.OK)
            {
                return;
            }
            FormQuery FormQuery2 = new FormQuery();
            FormQuery2.IsReport = true;
            Queries.CurReport = new ReportOld();
            Queries.TableQ = new DataTable(null);
            for (int i = 0; i < 12; i++)
            {//add columns
                Queries.TableQ.Columns.Add(new System.Data.DataColumn());//blank columns
            }
            Queries.CurReport.ColTotal = new double[Queries.TableQ.Columns.Count];
            Queries.CurReport.ColWidth = new int[Queries.TableQ.Columns.Count];
            Queries.CurReport.ColPos = new int[Queries.TableQ.Columns.Count+1];
            Queries.CurReport.ColPos[0] = 0;
            Queries.CurReport.ColCaption = new string[Queries.TableQ.Columns.Count];
            Queries.CurReport.ColAlign = new HorizontalAlignment[Queries.TableQ.Columns.Count];
            // Add all Rows
            foreach (OpenDental.UI.ODGridRow gridRow in gridTxPlanList.Rows)
            {
                DataRow row = Queries.TableQ.NewRow();//add first row by hand to get value for temp
                row[0] = gridRow.Cells[0].Text; // Chart #
                row[1] = System.Text.RegularExpressions.Regex.Replace(gridRow.Cells[1].Text, " \r\n", " "); // gridRow.Cells[1].Text; // Patient Name
                row[2] = gridRow.Cells[2].Text; // TxPlan Date
                row[3] = gridRow.Cells[3].Text; // Th #
                row[4] = gridRow.Cells[4].Text; // Surface
                row[5] = gridRow.Cells[5].Text; // Code
                row[6] = gridRow.Cells[6].Text; // Desc
                row[7] = gridRow.Cells[7].Text.PadLeft(7); // Fee
                row[8] = gridRow.Cells[8].Text.PadLeft(6); // Patient Pays
                row[9] = gridRow.Cells[9].Text.PadLeft(6); // Insurance Pays
                row[10] = gridRow.Cells[10].Text.PadLeft(7); // Unused Primary
                row[11] = gridRow.Cells[11].Text.PadLeft(7); // Unused Secondary
                Queries.CurReport.ColTotal[7] += PIn.PDouble(gridRow.Cells[7].ToString());
                Queries.CurReport.ColTotal[8] += PIn.PDouble(gridRow.Cells[8].ToString());
                Queries.CurReport.ColTotal[9] += PIn.PDouble(gridRow.Cells[9].ToString());
                Queries.CurReport.ColTotal[10] += PIn.PDouble(gridRow.Cells[10].ToString());
                Queries.CurReport.ColTotal[11] += PIn.PDouble(gridRow.Cells[11].ToString());
                Queries.TableQ.Rows.Add(row);
          };

            FormQuery2.ResetGrid();//this is a method in FormQuery2;
            Queries.CurReport.Summary = new string[0];
            Queries.CurReport.Title = "Treatment Plans Analyzer Report";
            Queries.CurReport.SubTitle = new string[2];
            Queries.CurReport.SubTitle[0] = ((Pref)PrefC.HList["PracticeTitle"]).ValueString;
            Queries.CurReport.SubTitle[1] = "(by Name and Unused Benefits)";
            Queries.CurReport.ColPos[0] = 00;
            Queries.CurReport.ColPos[1] = 40;
            Queries.CurReport.ColPos[2] = 197;      // TxPlan date
            Queries.CurReport.ColPos[3] = 265;
            Queries.CurReport.ColPos[4] = 290;
            Queries.CurReport.ColPos[5] = 320;
            Queries.CurReport.ColPos[6] = 365;
            Queries.CurReport.ColPos[7] = 525;
            Queries.CurReport.ColPos[8] = 560;
            Queries.CurReport.ColPos[9] = 614;
            Queries.CurReport.ColPos[10] = 668;     
            Queries.CurReport.ColPos[11] = 735;     
            Queries.CurReport.ColPos[12] = 795;
            // Set Report Heading
            Queries.CurReport.ColCaption[0] = "Chart#";
            Queries.CurReport.ColCaption[1] = "Patient Name";
            Queries.CurReport.ColCaption[2] = "TxPln Date";
            Queries.CurReport.ColCaption[3] = "Th#";
            Queries.CurReport.ColCaption[4] = "Surf";
            Queries.CurReport.ColCaption[5] = "Code";
            Queries.CurReport.ColCaption[6] = "Description";
            Queries.CurReport.ColCaption[7] = "Fee ";
            Queries.CurReport.ColCaption[8] = "Pat Pays";
            Queries.CurReport.ColCaption[9] = "Ins Pays ";
            Queries.CurReport.ColCaption[10] = "Unused Pri";
            Queries.CurReport.ColCaption[11] = "Unusd Sec";
            // Set Alignments
            Queries.CurReport.ColAlign[0] = HorizontalAlignment.Left;
            Queries.CurReport.ColAlign[1] = HorizontalAlignment.Left;
            Queries.CurReport.ColAlign[2] = HorizontalAlignment.Left;
            Queries.CurReport.ColAlign[3] = HorizontalAlignment.Center;
            Queries.CurReport.ColAlign[4] = HorizontalAlignment.Center;
            Queries.CurReport.ColAlign[5] = HorizontalAlignment.Center;
            Queries.CurReport.ColAlign[6] = HorizontalAlignment.Left;
            Queries.CurReport.ColAlign[7] = HorizontalAlignment.Right;
            Queries.CurReport.ColAlign[8] = HorizontalAlignment.Right;
            Queries.CurReport.ColAlign[9] = HorizontalAlignment.Right;
            Queries.CurReport.ColAlign[10] = HorizontalAlignment.Right;
            Queries.CurReport.ColAlign[11] = HorizontalAlignment.Right;

            FormQuery2.ShowDialog();
            //DialogResult = DialogResult.OK;
        }
        ///<summary>raised for each page to be printed.</summary>
        private void pdCards_PrintPage(object sender, PrintPageEventArgs ev)
        {
            int totalPages = (int)Math.Ceiling((double)AddrTable.Rows.Count / (double)PrefC.GetInt("RecallPostcardsPerSheet"));
            Graphics g = ev.Graphics;
            float yPos = 0;//these refer to the upper left origin of each postcard
            float xPos = 0;
            string str = "";
            while (yPos < ev.PageBounds.Height - 100 && patientsPrinted < AddrTable.Rows.Count)
            {
                //Return Address--------------------------------------------------------------------------
                if (PrefC.GetBool("RecallCardsShowReturnAdd"))
                {
                    str = PrefC.GetString("PracticeTitle") + "\r\n";
                    g.DrawString(str, new Font(FontFamily.GenericSansSerif, 9, FontStyle.Bold), Brushes.Black, xPos + 45, yPos + 60);
                    str = PrefC.GetString("PracticeAddress") + "\r\n";
                    if (PrefC.GetString("PracticeAddress2") != "")
                    {
                        str += PrefC.GetString("PracticeAddress2") + "\r\n";
                    }
                    str += PrefC.GetString("PracticeCity") + ",  " + PrefC.GetString("PracticeST") + "  " + PrefC.GetString("PracticeZip") + "\r\n";
                    string phone = PrefC.GetString("PracticePhone");
                    if (CultureInfo.CurrentCulture.Name == "en-US" && phone.Length == 10)
                    {
                        str += "(" + phone.Substring(0, 3) + ")" + phone.Substring(3, 3) + "-" + phone.Substring(6);
                    }
                    else
                    {//any other phone format
                        str += phone;
                    }
                    g.DrawString(str, new Font(FontFamily.GenericSansSerif, 8), Brushes.Black, xPos + 45, yPos + 75);
                }
                //			
                str = textPostcardMessage.Text;
                g.DrawString(str, new Font(FontFamily.GenericSansSerif, 10), Brushes.Black, new RectangleF(xPos + 45, yPos + 180, 250, 190));
                //Patient's Address-----------------------------------------------------------------------
                str = AddrTable.Rows[patientsPrinted]["FName"].ToString() + " "
                    + AddrTable.Rows[patientsPrinted]["MiddleI"].ToString() + " "
                    + AddrTable.Rows[patientsPrinted]["LName"].ToString() + "\r\n"
                    + AddrTable.Rows[patientsPrinted]["Address"].ToString() + "\r\n";
                if (AddrTable.Rows[patientsPrinted]["Address2"].ToString() != "")
                {
                    str += AddrTable.Rows[patientsPrinted]["Address2"].ToString() + "\r\n";
                }
                str += AddrTable.Rows[patientsPrinted]["City"].ToString() + ", "
                    + AddrTable.Rows[patientsPrinted]["State"].ToString() + "   "
                    + AddrTable.Rows[patientsPrinted]["Zip"].ToString() + "\r\n";
                g.DrawString(str, new Font(FontFamily.GenericSansSerif, 11), Brushes.Black, xPos + 320, yPos + 240);
                if (PrefC.GetInt("RecallPostcardsPerSheet") == 1)
                {
                    yPos += 400;
                }
                else if (PrefC.GetInt("RecallPostcardsPerSheet") == 3)
                {
                    yPos += 366;
                }
                else
                {//4
                    xPos += 550;
                    if (xPos > 1000)
                    {
                        xPos = 0;
                        yPos += 425;
                    }
                }
                patientsPrinted++;
            }//while
            pagesPrinted++;
            if (pagesPrinted == totalPages)
            {
                ev.HasMorePages = false;
                pagesPrinted = 0;
                patientsPrinted = 0;
            }
            else
            {
                ev.HasMorePages = true;
            }
        }

        private void butPreview_Click(object sender, EventArgs e)
        {
            {
                if (AddrTable.Rows.Count == 0)
                {
                    MessageBox.Show(Lan.g(this, "No rows selected. Must have at least one to print."));
                    return;
                }
                pagesPrinted = 0;
                patientsPrinted = 0;
                pd = new PrintDocument();
                pd.PrintPage += new PrintPageEventHandler(this.pdCards_PrintPage);
                pd.OriginAtMargins = true;
                pd.DefaultPageSettings.Margins = new Margins(0, 0, 0, 0);
                if (PrefC.GetInt("RecallPostcardsPerSheet") == 1)
                {
                    pd.DefaultPageSettings.PaperSize = new PaperSize("Postcard", 400, 600);
                    pd.DefaultPageSettings.Landscape = true;
                }
                else if (PrefC.GetInt("RecallPostcardsPerSheet") == 3)
                {
                    pd.DefaultPageSettings.PaperSize = new PaperSize("Postcard", 850, 1100);
                }
                else
                {//4
                    pd.DefaultPageSettings.PaperSize = new PaperSize("Postcard", 850, 1100);
                    pd.DefaultPageSettings.Landscape = true;
                }
                printPreview = new OpenDental.UI.PrintPreview(PrintSituation.Postcard, pd,
                    (int)Math.Ceiling((double)gridTxPlanList.Rows.Count / (double)PrefC.GetInt("RecallPostcardsPerSheet")));
                printPreview.ShowDialog();
            }
        }
	}
}