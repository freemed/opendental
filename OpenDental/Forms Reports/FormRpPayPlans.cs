using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Collections;
using System.Globalization;
using System.Drawing.Printing;
using System.Windows.Forms;
using OpenDental.ReportingOld2;
using OpenDental.UI;
using OpenDentBusiness;
using CodeBase;

namespace OpenDental
{
	/// <summary>
	/// Summary description for FormRpApptWithPhones.
	/// </summary>
	public class FormRpPayPlans:System.Windows.Forms.Form {
		private OpenDental.UI.Button butCancel;
		private OpenDental.UI.Button butOK;
		//private int pagesPrinted;
		private ODErrorProvider errorProvider1=new ODErrorProvider();
		//private DataTable BirthdayTable;
		//private int patientsPrinted;
		//private PrintDocument pd;
		//private OpenDental.UI.PrintPreview printPreview;

		///<summary></summary>
		public FormRpPayPlans()
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormRpPayPlans));
			this.butCancel = new OpenDental.UI.Button();
			this.butOK = new OpenDental.UI.Button();
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
			this.butCancel.Location = new System.Drawing.Point(546,216);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75,24);
			this.butCancel.TabIndex = 44;
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
			this.butOK.Location = new System.Drawing.Point(546,176);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75,24);
			this.butOK.TabIndex = 43;
			this.butOK.Text = "Report";
			this.butOK.Click += new System.EventHandler(this.butReport_Click);
			// 
			// FormRpPayPlans
			// 
			this.AcceptButton = this.butOK;
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.CancelButton = this.butCancel;
			this.ClientSize = new System.Drawing.Size(660,264);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.butOK);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "FormRpPayPlans";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Payment Plans Report";
			this.Load += new System.EventHandler(this.FormRpPayPlans_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormRpPayPlans_Load(object sender, System.EventArgs e){
			
		}

		private void butReport_Click(object sender, System.EventArgs e){
			//if(errorProvider1.GetError(textDateFrom) != ""
			//	|| errorProvider1.GetError(textDateTo) != "") 
			//{
			//	MsgBox.Show(this,"Please fix data entry errors first.");
			//	return;
			//}
			//DateTime dateFrom=PIn.PDate(textDateFrom.Text);
			//DateTime dateTo=PIn.PDate(textDateTo.Text);
			//if(dateTo < dateFrom) 
			//{
			//	MsgBox.Show(this,"To date cannot be before From date.");
			//	return;
			//}
			ReportLikeCrystal report=new ReportLikeCrystal();
			report.ReportName=Lan.g(this,"PaymentPlans");
			report.AddTitle(Lan.g(this,"Payment Plans"));
			report.AddSubTitle(PrefC.GetString(PrefName.PracticeTitle));
			report.AddSubTitle(DateTime.Today.ToShortDateString());
			DataTable table=new DataTable();
			//table.Columns.Add("date");
			table.Columns.Add("guarantor");
			table.Columns.Add("ins");
			table.Columns.Add("princ");
			table.Columns.Add("paid");
			table.Columns.Add("due");
			table.Columns.Add("dueTen");
			DataRow row;
			string datesql="CURDATE()";
			if(DataConnection.DBtype==DatabaseType.Oracle){
				datesql="(SELECT CURRENT_DATE FROM dual)";
			}
			string command=@"SELECT FName,LName,MiddleI,PlanNum,Preferred,
				(SELECT SUM(Principal+Interest) FROM payplancharge WHERE payplancharge.PayPlanNum=payplan.PayPlanNum
				AND ChargeDate <= "+datesql+@") ""_accumDue"",
				(SELECT SUM(Principal+Interest) FROM payplancharge WHERE payplancharge.PayPlanNum=payplan.PayPlanNum
				AND ChargeDate <= "+DbHelper.DateAddDay("'"+datesql+"'",POut.Long(PrefC.GetLong(PrefName.PayPlansBillInAdvanceDays)))+@") ""_dueTen"",
				(SELECT SUM(SplitAmt) FROM paysplit WHERE paysplit.PayPlanNum=payplan.PayPlanNum) ""_paid"",
				(SELECT SUM(Principal) FROM payplancharge WHERE payplancharge.PayPlanNum=payplan.PayPlanNum) ""_principal""
				FROM payplan
				LEFT JOIN patient ON patient.PatNum=payplan.Guarantor "
				//WHERE SUBSTRING(Birthdate,6,5) >= '"+dateFrom.ToString("MM-dd")+"' "
				//+"AND SUBSTRING(Birthdate,6,5) <= '"+dateTo.ToString("MM-dd")+"' "
				+"GROUP BY payplan.PayPlanNum ORDER BY LName,FName";
			DataTable raw=Reports.GetTable(command);
			//DateTime payplanDate;
			Patient pat;
			double princ;
			double paid;
			double accumDue;
			double dueTen;
			for(int i=0;i<raw.Rows.Count;i++){
				princ=PIn.Double(raw.Rows[i]["_principal"].ToString());
				paid=PIn.Double(raw.Rows[i]["_paid"].ToString());
				accumDue=PIn.Double(raw.Rows[i]["_accumDue"].ToString());
				dueTen=PIn.Double(raw.Rows[i]["_dueTen"].ToString());
				row=table.NewRow();
				//payplanDate=PIn.PDate(raw.Rows[i]["PayPlanDate"].ToString());
				//row["date"]=raw.Rows[i]["PayPlanDate"].ToString();//payplanDate.ToShortDateString();
				pat=new Patient();
				pat.LName=raw.Rows[i]["LName"].ToString();
				pat.FName=raw.Rows[i]["FName"].ToString();
				pat.MiddleI=raw.Rows[i]["MiddleI"].ToString();
				pat.Preferred=raw.Rows[i]["Preferred"].ToString();
				row["guarantor"]=pat.GetNameLF();
				if(raw.Rows[i]["Preferred"].ToString()=="0"){
					row["ins"]="";
				}
				else{
					row["ins"]="X";
				}
				row["princ"]=princ.ToString("f");
				row["paid"]=paid.ToString("f");
				row["due"]=(accumDue-paid).ToString("f");
				row["dueTen"]=(dueTen-paid).ToString("f");
				table.Rows.Add(row);
			}
			report.ReportTable=table;
			//report.AddColumn("Date",90,FieldValueType.Date);			
			report.AddColumn("Guarantor",160,FieldValueType.String);
			report.AddColumn("Ins",40,FieldValueType.String);
			report.GetLastRO(ReportObjectKind.TextObject).TextAlign=ContentAlignment.MiddleCenter;
			report.GetLastRO(ReportObjectKind.FieldObject).TextAlign=ContentAlignment.MiddleCenter;
			report.AddColumn("Princ",100,FieldValueType.String);
			report.GetLastRO(ReportObjectKind.TextObject).TextAlign=ContentAlignment.MiddleRight;
			report.GetLastRO(ReportObjectKind.FieldObject).TextAlign=ContentAlignment.MiddleRight;
			report.AddColumn("Paid",100,FieldValueType.String);
			report.GetLastRO(ReportObjectKind.TextObject).TextAlign=ContentAlignment.MiddleRight;
			report.GetLastRO(ReportObjectKind.FieldObject).TextAlign=ContentAlignment.MiddleRight;
			report.AddColumn("Due Now",100,FieldValueType.String);
			report.GetLastRO(ReportObjectKind.TextObject).TextAlign=ContentAlignment.MiddleRight;
			report.GetLastRO(ReportObjectKind.FieldObject).TextAlign=ContentAlignment.MiddleRight;
			report.AddColumn("Due in "+PrefC.GetLong(PrefName.PayPlansBillInAdvanceDays).ToString()
				+" Days",100,FieldValueType.String);
			report.GetLastRO(ReportObjectKind.TextObject).TextAlign=ContentAlignment.MiddleRight;
			report.GetLastRO(ReportObjectKind.FieldObject).TextAlign=ContentAlignment.MiddleRight;
			//report.GetLastRO(ReportObjectKind.FieldObject).FormatString="d";
			report.AddPageNum();
			//report.SubmitQuery();
			//report.ReportTable=Patients.GetBirthdayList(dateFrom,dateTo);
			//if(!report.SubmitQuery()){
			//	return;
			//}
			FormReportLikeCrystal FormR=new FormReportLikeCrystal(report);
			FormR.ShowDialog();
			DialogResult=DialogResult.OK;
		}

		

		













		

		

		
	}
}
