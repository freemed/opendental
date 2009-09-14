/* Receivables Breakdown Report:
 * Author: Bill MacWilliams
 * Company: Kapricorn Systems Inc.
 * 
 * This report will give you the currect calculated receivables upto the date specified.
 * This report does NOT show anything past the date specified.
 * This will allow you to do a breakdown for any month / year with totals for the month
 * at the bottom of the report and a running receivables at the far right. It also gives
 * your a breakdown by day of your receivables.  Currently this report is for the entire
 * practice.
*/
using System;
using System.Data;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental
{

    ///<summary></summary>
    public class FormRpReceivablesBreakdown : System.Windows.Forms.Form
    {
        private OpenDental.UI.Button butCancel;
        private OpenDental.UI.Button butOK;
        private System.ComponentModel.Container components = null;
        private MonthCalendar date1;
        private Label label1;
        private System.Windows.Forms.ListBox listProv;
        private Label labelProvider;
        private ListBox listClinic;
        private Label labClinic;
        private GroupBox groupInsBox;
        private RadioButton radioWriteoffPay;
        private RadioButton radioWriteoffProc;
        private FormQuery FormQuery2;

        ///<summary></summary>
        public FormRpReceivablesBreakdown()
        {
            InitializeComponent();
            Lan.F(this);
        }

        ///<summary></summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }
        #region Windows Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
					System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormRpReceivablesBreakdown));
					this.butCancel = new OpenDental.UI.Button();
					this.butOK = new OpenDental.UI.Button();
					this.date1 = new System.Windows.Forms.MonthCalendar();
					this.label1 = new System.Windows.Forms.Label();
					this.listProv = new System.Windows.Forms.ListBox();
					this.labelProvider = new System.Windows.Forms.Label();
					this.listClinic = new System.Windows.Forms.ListBox();
					this.labClinic = new System.Windows.Forms.Label();
					this.groupInsBox = new System.Windows.Forms.GroupBox();
					this.radioWriteoffProc = new System.Windows.Forms.RadioButton();
					this.radioWriteoffPay = new System.Windows.Forms.RadioButton();
					this.groupInsBox.SuspendLayout();
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
					this.butCancel.Location = new System.Drawing.Point(468,245);
					this.butCancel.Name = "butCancel";
					this.butCancel.Size = new System.Drawing.Size(75,26);
					this.butCancel.TabIndex = 3;
					this.butCancel.Text = "&Cancel";
					// 
					// butOK
					// 
					this.butOK.AdjustImageLocation = new System.Drawing.Point(0,0);
					this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
					this.butOK.Autosize = true;
					this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
					this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
					this.butOK.CornerRadius = 4F;
					this.butOK.Location = new System.Drawing.Point(378,245);
					this.butOK.Name = "butOK";
					this.butOK.Size = new System.Drawing.Size(75,26);
					this.butOK.TabIndex = 2;
					this.butOK.Text = "&OK";
					this.butOK.Click += new System.EventHandler(this.butOK_Click);
					// 
					// date1
					// 
					this.date1.Location = new System.Drawing.Point(317,43);
					this.date1.Name = "date1";
					this.date1.TabIndex = 4;
					// 
					// label1
					// 
					this.label1.Location = new System.Drawing.Point(314,24);
					this.label1.Name = "label1";
					this.label1.Size = new System.Drawing.Size(230,16);
					this.label1.TabIndex = 5;
					this.label1.Text = "Up to the following date";
					this.label1.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
					// 
					// listProv
					// 
					this.listProv.Location = new System.Drawing.Point(27,45);
					this.listProv.Name = "listProv";
					this.listProv.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
					this.listProv.Size = new System.Drawing.Size(126,147);
					this.listProv.TabIndex = 30;
					// 
					// labelProvider
					// 
					this.labelProvider.Location = new System.Drawing.Point(24,26);
					this.labelProvider.Name = "labelProvider";
					this.labelProvider.Size = new System.Drawing.Size(103,16);
					this.labelProvider.TabIndex = 7;
					this.labelProvider.Text = "Providers";
					this.labelProvider.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
					// 
					// listClinic
					// 
					this.listClinic.FormattingEnabled = true;
					this.listClinic.Location = new System.Drawing.Point(171,45);
					this.listClinic.Name = "listClinic";
					this.listClinic.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
					this.listClinic.Size = new System.Drawing.Size(126,147);
					this.listClinic.TabIndex = 31;
					// 
					// labClinic
					// 
					this.labClinic.Location = new System.Drawing.Point(171,26);
					this.labClinic.Name = "labClinic";
					this.labClinic.Size = new System.Drawing.Size(103,16);
					this.labClinic.TabIndex = 32;
					this.labClinic.Text = "Clinic";
					this.labClinic.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
					// 
					// groupInsBox
					// 
					this.groupInsBox.Controls.Add(this.radioWriteoffProc);
					this.groupInsBox.Controls.Add(this.radioWriteoffPay);
					this.groupInsBox.Location = new System.Drawing.Point(27,204);
					this.groupInsBox.Name = "groupInsBox";
					this.groupInsBox.Size = new System.Drawing.Size(270,66);
					this.groupInsBox.TabIndex = 33;
					this.groupInsBox.TabStop = false;
					this.groupInsBox.Text = "Show Insurance Writeoffs";
					// 
					// radioWriteoffProc
					// 
					this.radioWriteoffProc.Location = new System.Drawing.Point(7,38);
					this.radioWriteoffProc.Name = "radioWriteoffProc";
					this.radioWriteoffProc.Size = new System.Drawing.Size(199,17);
					this.radioWriteoffProc.TabIndex = 1;
					this.radioWriteoffProc.TabStop = true;
					this.radioWriteoffProc.Text = "Using procedure date.";
					this.radioWriteoffProc.UseVisualStyleBackColor = true;
					// 
					// radioWriteoffPay
					// 
					this.radioWriteoffPay.Location = new System.Drawing.Point(7,20);
					this.radioWriteoffPay.Name = "radioWriteoffPay";
					this.radioWriteoffPay.Size = new System.Drawing.Size(240,17);
					this.radioWriteoffPay.TabIndex = 0;
					this.radioWriteoffPay.TabStop = true;
					this.radioWriteoffPay.Text = "Using insurance payment date.";
					this.radioWriteoffPay.UseVisualStyleBackColor = true;
					// 
					// FormRpReceivablesBreakdown
					// 
					this.AcceptButton = this.butOK;
					this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
					this.CancelButton = this.butCancel;
					this.ClientSize = new System.Drawing.Size(567,283);
					this.Controls.Add(this.groupInsBox);
					this.Controls.Add(this.labClinic);
					this.Controls.Add(this.listClinic);
					this.Controls.Add(this.labelProvider);
					this.Controls.Add(this.listProv);
					this.Controls.Add(this.label1);
					this.Controls.Add(this.date1);
					this.Controls.Add(this.butCancel);
					this.Controls.Add(this.butOK);
					this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
					this.MaximizeBox = false;
					this.MinimizeBox = false;
					this.Name = "FormRpReceivablesBreakdown";
					this.ShowInTaskbar = false;
					this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
					this.Text = "Receivables Breakdown";
					this.Load += new System.EventHandler(this.FormRpReceivablesBreakdown_Load);
					this.groupInsBox.ResumeLayout(false);
					this.ResumeLayout(false);

        }
        #endregion
        private void FormRpReceivablesBreakdown_Load(object sender, EventArgs e)
        {
            radioWriteoffPay.Checked = true;
            listProv.Items.Add(Lan.g(this, "Practice"));
            for (int i = 0; i < ProviderC.List.Length; i++)
            {
                listProv.Items.Add(ProviderC.List[i].GetLongDesc());
            }
            listProv.SetSelected(0, true);
            //if(PrefC.GetBool("EasyNoClinics")){
            listClinic.Visible = false;
            labClinic.Visible = false;
            /*}
            else{
                listClinic.Items.Add(Lan.g(this,"Unassigned"));
                listClinic.SetSelected(0,true);
                for(int i=0;i<Clinics.List.Length;i++) {
                    listClinic.Items.Add(Clinics.List[i].Description);
                    listClinic.SetSelected(i+1,true);
                }
            }*/
        }


        private void butOK_Click(object sender, System.EventArgs e)
        {
            string bDate;
            string eDate;
            double rcvStart = 0.0;
            double rcvProd = 0.0;
            double rcvAdj = 0.0;
            double rcvWriteoff = 0.0;
            double rcvPayment = 0.0;
            double rcvInsPayment = 0.0;
            double runningRcv = 0.0;
            double rcvDaily = 0.0;
            string wMonth;
            string wYear;
            string wDay = "01";
            string wDate;
            // Get the year / month and instert the 1st of the month for stop point for calculated running balance
            wYear = date1.SelectionStart.Year.ToString();
            wMonth = date1.SelectionStart.Month.ToString();
            if(wMonth.Length<2){
                wMonth = "0" + wMonth;
            }
            wDate = wYear +"-"+ wMonth +"-"+ wDay;

            Queries.CurReport = new ReportOld();
            //
            // Create temperary tables for sorting data
            //
            DataTable TableCharge = new DataTable();  //charges
            DataTable TableCapWriteoff = new DataTable();  //capComplete writeoffs
            DataTable TableInsWriteoff = new DataTable();  //ins writeoffs
            DataTable TablePay = new DataTable();  //payments - Patient
            DataTable TableIns = new DataTable();  //payments - Ins, added SPK 
            DataTable TableAdj = new DataTable();  //adjustments

            //
            // Main Loop:  This will loop twice 1st loop gets running balance to start of month selected
            //             2nd will break the numbers dow by day and calculate the running balances
            //
            for (int j = 0; j <= 1; j++)
            {
                if (j == 0)
                {
                    bDate = "1800-01-01";
                    eDate = wDate;
                }
                else
                {
                    bDate = wDate;
                    eDate = POut.PDate(date1.SelectionStart.AddDays(1)).Substring(1, 10);// Needed because all Queries are < end date to get correct Starting AR
                }
                string whereProv;//used as the provider portion of the where clauses.
                //each whereProv needs to be set up separately for each query
                string whereProvx;  //Extended for more than 4 names
                whereProv = "";
                if (listProv.SelectedIndices[0] != 0)
                {
                    for (int i = 0; i < listProv.SelectedIndices.Count; i++)
                    {
                        if (i == 0)
                        {
                            whereProv += " AND (";
                        }
                        else
                        {
                            whereProv += "OR ";
                        }
                        whereProv += "procedurelog.ProvNum = "
                            + POut.PLong(ProviderC.List[listProv.SelectedIndices[i] - 1].ProvNum) + " ";
                    }
                    whereProv += ") ";
                }
                Queries.CurReport.Query = "SELECT procedurelog.ProcDate, "
                + "SUM(procedurelog.ProcFee*(CASE procedurelog.UnitQty+procedurelog.BaseUnits WHEN 0 THEN 1 ELSE procedurelog.UnitQty+procedurelog.BaseUnits END)) "
                + "FROM procedurelog "
                + "WHERE procedurelog.ProcDate >= '" + bDate + "' "
                + "AND procedurelog.ProcDate < '" + eDate + "' "
                + "AND procedurelog.ProcStatus = '2' "
                + whereProv
                + "GROUP BY procedurelog.ProcDate "
                + "ORDER BY procedurelog.ProcDate";
                Queries.SubmitTemp(); //create TableTemp
                TableCharge = Queries.TableTemp; //must create datatable obj since Queries.TempTable is static
                whereProv = "";
                if (listProv.SelectedIndices[0] != 0)
                {
                    for (int i = 0; i < listProv.SelectedIndices.Count; i++)
                    {
                        if (i == 0)
                        {
                            whereProv += " AND (";
                        }
                        else
                        {
                            whereProv += "OR ";
                        }
                        whereProv += "claimproc.ProvNum = "
                            + POut.PLong(ProviderC.List[listProv.SelectedIndices[i] - 1].ProvNum) + " ";
                    }
                    whereProv += ") ";
                }
                if (radioWriteoffPay.Checked)
                {
                    Queries.CurReport.Query = "SELECT DateCP, "
                    + "SUM(WriteOff) FROM claimproc WHERE "
                    + "DateCP >= '" + bDate + "' "
                    + "AND DateCP < '" + eDate + "' "
                    + "AND Status = '7' "//CapComplete
                    + whereProv
                    + " GROUP BY DateCP "
                    + "ORDER BY DateCP";
                }
                else
                {
                    Queries.CurReport.Query = "SELECT ProcDate, "
                    + "SUM(WriteOff) FROM claimproc WHERE "
                    + "ProcDate >= '" + bDate + "' "
                    + "AND ProcDate < '" + eDate + "' "
                    + "AND Status = '7' "//CapComplete
                    + whereProv
                    + " GROUP BY ProcDate "
                    + "ORDER BY ProcDate";
                }

                Queries.SubmitTemp(); //create TableTemp
                TableCapWriteoff = Queries.TableTemp.Copy();
                whereProv = "";
                if (listProv.SelectedIndices[0] != 0)
                {
                    for (int i = 0; i < listProv.SelectedIndices.Count; i++)
                    {
                        if (i == 0)
                        {
                            whereProv += " AND (";
                        }
                        else
                        {
                            whereProv += "OR ";
                        }
                        whereProv += "ProvNum = "
                            + POut.PLong(ProviderC.List[listProv.SelectedIndices[i] - 1].ProvNum) + " ";
                    }
                    whereProv += ") ";
                }
                if (radioWriteoffPay.Checked)
                {
                    Queries.CurReport.Query = "SELECT DateCP, "
                    + "SUM(WriteOff) FROM claimproc WHERE "
                    + "DateCP >= '" + bDate + "' "
                    + "AND DateCP < '" + eDate + "' "
                    + "AND (Status = '1' OR Status = 4) "//Recieved or supplemental. Otherwise, it's only an estimate.
                    + whereProv
                    + " GROUP BY DateCP "
                    + "ORDER BY DateCP";
                }
                else
                {
                    Queries.CurReport.Query = "SELECT ProcDate, "
                    + "SUM(WriteOff) FROM claimproc WHERE "
                    + "ProcDate >= '" + bDate + "' "
                    + "AND ProcDate < '" + eDate + "' "
                    + "AND (claimproc.Status=1 OR claimproc.Status=4 OR claimproc.Status=0) " //received or supplemental or notreceived
                    + whereProv
                    + " GROUP BY ProcDate "
                    + "ORDER BY ProcDate";
                }
                Queries.SubmitTemp(); //create TableTemp
                TableInsWriteoff = Queries.TableTemp.Copy();
                whereProv = "";
                if (listProv.SelectedIndices[0] != 0)
                {
                    for (int i = 0; i < listProv.SelectedIndices.Count; i++)
                    {
                        if (i == 0)
                        {
                            whereProv += " AND (";
                        }
                        else
                        {
                            whereProv += "OR ";
                        }
                        whereProv += "paysplit.ProvNum = "
                            + POut.PLong(ProviderC.List[listProv.SelectedIndices[i] - 1].ProvNum) + " ";
                    }
                    whereProv += ") ";
                }
                Queries.CurReport.Query = "SELECT paysplit.DatePay,SUM(paysplit.splitamt) FROM paysplit "
                + "WHERE paysplit.IsDiscount = '0' "
                + "AND paysplit.DatePay >= '" + bDate + "' "
                + "AND paysplit.DatePay < '" + eDate + "' "
                + whereProv
                + " GROUP BY paysplit.DatePay ORDER BY DatePay";
                Queries.SubmitTemp(); //create TableTemp
                TablePay = Queries.TableTemp.Copy(); //must create datatable obj since Queries.TempTable is static
                whereProv = "";
                if (listProv.SelectedIndices[0] != 0)
                {
                    for (int i = 0; i < listProv.SelectedIndices.Count; i++)
                    {
                        if (i == 0)
                        {
                            whereProv += " AND (";
                        }
                        else
                        {
                            whereProv += "OR ";
                        }
                        whereProv += "claimproc.ProvNum = "
                            + POut.PLong(ProviderC.List[listProv.SelectedIndices[i] - 1].ProvNum) + " ";
                    }
                    whereProv += ") ";
                }
                Queries.CurReport.Query = "SELECT claimpayment.CheckDate,SUM(claimproc.InsPayamt) "
                + "FROM claimpayment,claimproc WHERE "
                + "claimproc.ClaimPaymentNum = claimpayment.ClaimPaymentNum "
                + "AND (claimproc.Status=1 OR claimproc.Status=4) "//received or supplemental
                + "AND claimpayment.CheckDate >= '" + bDate + "' "
                + "AND claimpayment.CheckDate < '" + eDate + "' "
                + whereProv
                + " GROUP BY claimpayment.CheckDate ORDER BY checkdate";
                Queries.SubmitTemp(); //create TableIns
                TableIns = Queries.TableTemp; //must create datatable obj since Queries.TempTable is static
                whereProv = "";
                if (listProv.SelectedIndices[0] != 0)
                {
                    for (int i = 0; i < listProv.SelectedIndices.Count; i++)
                    {
                        if (i == 0)
                        {
                            whereProv += " AND (";
                        }
                        else
                        {
                            whereProv += "OR ";
                        }
                        whereProv += "ProvNum = "
                            + POut.PLong(ProviderC.List[listProv.SelectedIndices[i] - 1].ProvNum) + " ";
                    }
                    whereProv += ") ";
                }
                Queries.CurReport.Query = "SELECT adjdate, SUM(adjamt) FROM adjustment WHERE "
                + "adjdate >= '" + bDate + "' "
                + "AND adjdate < '" + eDate + "' "
                + whereProv
                + " GROUP BY adjdate ORDER BY adjdate";
                Queries.SubmitTemp(); //create TableTemp
                TableAdj = Queries.TableTemp; //must create datatable obj since Queries.TempTable is static 

                //1st Loop Calculate running Accounts Receivable upto the 1st of the Month Selected
                //2nd Loop Calculate the Daily Accounts Receivable upto the Date Selected
                //Finaly Generate Report showing the breakdown upto the date specified with totals for what is on the report
                if (j == 0)
                {
                    for (int k = 0; k < TableCharge.Rows.Count; k++)
                    {
                        rcvProd += PIn.PDouble(TableCharge.Rows[k][1].ToString());
                    }
                    for (int k = 0; k < TableCapWriteoff.Rows.Count; k++)
                    {
                        rcvWriteoff += PIn.PDouble(TableCapWriteoff.Rows[k][1].ToString());
                    }
                    for (int k = 0; k < TableInsWriteoff.Rows.Count; k++)
                    {
                        rcvWriteoff += PIn.PDouble(TableInsWriteoff.Rows[k][1].ToString());
                    }
                    for (int k = 0; k < TablePay.Rows.Count; k++)
                    {
                        rcvPayment += PIn.PDouble(TablePay.Rows[k][1].ToString());
                    }
                    for (int k = 0; k < TableIns.Rows.Count; k++)
                    {
                        rcvInsPayment += PIn.PDouble(TableIns.Rows[k][1].ToString());
                    }
                    for (int k = 0; k < TableAdj.Rows.Count; k++)
                    {
                        rcvAdj += PIn.PDouble(TableAdj.Rows[k][1].ToString());
                    }
                    TableCharge.Clear();
                    TableCapWriteoff.Clear();
                    TableInsWriteoff.Clear();
                    TablePay.Clear();
                    TableIns.Clear();
                    TableAdj.Clear();
                    rcvStart = (rcvProd + rcvAdj - rcvWriteoff) - (rcvPayment + rcvInsPayment);
                }
                else
                {
                    rcvAdj = 0.0;
                    rcvInsPayment = 0.0;
                    rcvPayment = 0.0;
                    rcvProd = 0.0;
                    rcvWriteoff = 0.0;
                    rcvDaily = 0.0;
                    runningRcv = rcvStart;
                    Queries.TableQ = new DataTable(null);//new table with 7 columns
                    for (int l = 0; l < 8; l++)
                    { //add columns
                        Queries.TableQ.Columns.Add(new System.Data.DataColumn());//blank columns
                    }
                    Queries.CurReport.ColTotal = new double[Queries.TableQ.Columns.Count];
                    eDate = POut.PDate(date1.SelectionStart).Substring(1, 10);// Reset EndDate to Selected Date
                    DateTime[] dates = new DateTime[(PIn.PDate(eDate) - PIn.PDate(bDate)).Days + 1];
                    for (int i = 0; i < dates.Length; i++)
                    {//usually 31 days in loop
                        dates[i] = PIn.PDate(bDate).AddDays(i);
                        //create new row called 'row' based on structure of TableQ
                        DataRow row = Queries.TableQ.NewRow();
                        row[0] = dates[i].ToShortDateString();
                        for (int k = 0; k < TableCharge.Rows.Count; k++)
                        {
                            if (dates[i] == (PIn.PDate(TableCharge.Rows[k][0].ToString())))
                            {
                                rcvProd += PIn.PDouble(TableCharge.Rows[k][1].ToString());
                            }
                        }
                        for (int k = 0; k < TableCapWriteoff.Rows.Count; k++)
                        {
                            if (dates[i] == (PIn.PDate(TableCapWriteoff.Rows[k][0].ToString())))
                            {
                                rcvWriteoff += PIn.PDouble(TableCapWriteoff.Rows[k][1].ToString());
                            }
                        }
                        for (int k = 0; k < TableAdj.Rows.Count; k++)
                        {
                            if (dates[i] == (PIn.PDate(TableAdj.Rows[k][0].ToString())))
                            {
                                rcvAdj += PIn.PDouble(TableAdj.Rows[k][1].ToString());
                            }
                        }
                        for (int k = 0; k < TableInsWriteoff.Rows.Count; k++)
                        {
                            if (dates[i] == (PIn.PDate(TableInsWriteoff.Rows[k][0].ToString())))
                            {
                                rcvWriteoff += PIn.PDouble(TableInsWriteoff.Rows[k][1].ToString());
                            }
                        }
                        for (int k = 0; k < TablePay.Rows.Count; k++)
                        {
                            if (dates[i] == (PIn.PDate(TablePay.Rows[k][0].ToString())))
                            {
                                rcvPayment += PIn.PDouble(TablePay.Rows[k][1].ToString());
                            }
                        }
                        for (int k = 0; k < TableIns.Rows.Count; k++)
                        {
                            if (dates[i] == (PIn.PDate(TableIns.Rows[k][0].ToString())))
                            {
                                rcvInsPayment += PIn.PDouble(TableIns.Rows[k][1].ToString());
                            }
                        }
                        rcvDaily = (rcvProd + rcvAdj - rcvWriteoff) - (rcvPayment + rcvInsPayment);
                        runningRcv += (rcvProd + rcvAdj - rcvWriteoff) - (rcvPayment + rcvInsPayment);
                        row[1] = rcvProd.ToString("n");
                        row[2] = rcvAdj.ToString("n");
                        row[3] = rcvWriteoff.ToString("n"); 
                        row[4] = rcvPayment.ToString("n");				
                        row[5] = rcvInsPayment.ToString("n");
                        row[6] = rcvDaily.ToString("n");
                        row[7] = runningRcv.ToString("n");
                        Queries.CurReport.ColTotal[1] += rcvProd;
                        Queries.CurReport.ColTotal[2] += rcvAdj;
                        Queries.CurReport.ColTotal[3] += rcvWriteoff;		
                        Queries.CurReport.ColTotal[4] += rcvPayment;
                        Queries.CurReport.ColTotal[5] += rcvInsPayment;
                        Queries.CurReport.ColTotal[6] += rcvDaily;
                        Queries.CurReport.ColTotal[7] = runningRcv;
                        Queries.TableQ.Rows.Add(row);  //adds row to table Q
                        rcvAdj = 0.0;
                        rcvInsPayment = 0.0;
                        rcvPayment = 0.0;
                        rcvProd = 0.0;
                        rcvWriteoff = 0.0;
                    }//END For Loop
                    Queries.CurReport.ColWidth = new int[Queries.TableQ.Columns.Count];
                    Queries.CurReport.ColPos = new int[Queries.TableQ.Columns.Count + 1];
                    Queries.CurReport.ColPos[0] = 0;
                    Queries.CurReport.ColCaption = new string[Queries.TableQ.Columns.Count];
                    Queries.CurReport.ColAlign = new HorizontalAlignment[Queries.TableQ.Columns.Count];
                    FormQuery2 = new FormQuery();
                    FormQuery2.IsReport = true;
                    FormQuery2.ResetGrid();
                    Queries.CurReport.Title = "Receivables Breakdown Report";
                    Queries.CurReport.SubTitle = new string[3];
                    Queries.CurReport.SubTitle[0] = ((Pref)PrefC.HList["PracticeTitle"]).ValueString;
                    whereProv = "Report for: Practice";
                    whereProvx = "";
                    if (listProv.SelectedIndices[0] != 0)
                    {
                        int nameCount = 0;
                        whereProv = "Report Includes:  ";
                        for (int i = 0; i < listProv.SelectedIndices.Count; i++)
                        {
                            if (nameCount < 3)
                            {
                                whereProv += " "+ProviderC.List[listProv.SelectedIndices[i] - 1].GetFormalName()+" /";
                            }
                            else
                            {
                                whereProvx += " "+ProviderC.List[listProv.SelectedIndices[i] - 1].GetFormalName()+" /";
                            }
                            nameCount += 1;
                        }
                        whereProv = whereProv.Substring(0, whereProv.Length-1);
                        if (whereProvx.Length > 0)
                        {
                            whereProvx = whereProvx.Substring(0, whereProvx.Length-1);
                        }
                    }
                    Queries.CurReport.SubTitle[1] = whereProv;
                    Queries.CurReport.SubTitle[2] = whereProvx;
                    Queries.CurReport.ColPos = new int[9];
                    Queries.CurReport.ColCaption = new string[8];
                    Queries.CurReport.ColAlign = new HorizontalAlignment[Queries.TableQ.Columns.Count];
                    Queries.CurReport.ColPos[0] = 1;
                    Queries.CurReport.ColPos[1] = 80;
                    Queries.CurReport.ColPos[2] = 160;
                    Queries.CurReport.ColPos[3] = 260;
                    Queries.CurReport.ColPos[4] = 360;
                    Queries.CurReport.ColPos[5] = 470;
                    Queries.CurReport.ColPos[6] = 570;
                    Queries.CurReport.ColPos[7] = 680;
                    Queries.CurReport.ColPos[8] = 779;
                    Queries.CurReport.ColCaption[0] = "Day";
                    Queries.CurReport.ColCaption[1] = "Production";
                    Queries.CurReport.ColCaption[2] = "Adjustment";
                    Queries.CurReport.ColCaption[3] = "Writeoff";
                    Queries.CurReport.ColCaption[4] = "Payment";
                    Queries.CurReport.ColCaption[5] = "InsPayment";
                    Queries.CurReport.ColCaption[6] = "Daily A/R";
                    Queries.CurReport.ColCaption[7] = "Ending A/R";
                    Queries.CurReport.ColAlign[1] = HorizontalAlignment.Right;
                    Queries.CurReport.ColAlign[2] = HorizontalAlignment.Right;
                    Queries.CurReport.ColAlign[3] = HorizontalAlignment.Right;
                    Queries.CurReport.ColAlign[4] = HorizontalAlignment.Right;
                    Queries.CurReport.ColAlign[5] = HorizontalAlignment.Right;
                    Queries.CurReport.ColAlign[6] = HorizontalAlignment.Right;
                    Queries.CurReport.ColAlign[7] = HorizontalAlignment.Right;
                    Queries.CurReport.Summary = new string[1];
                    Queries.CurReport.Summary[0]
                    = Lan.g(this, "Receivables Calculation: (Production + Adjustments - Writeoffs) - (Payments + Insurance Payments)");
                    FormQuery2.ShowDialog();
                    DialogResult = DialogResult.OK;
                }//END If 
            }// END For Loop
        }//END OK button Clicked
    }
}