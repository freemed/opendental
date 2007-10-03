using System;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Collections;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental {
	/// <summary>
	/// Summary description for FormCheckDatabase.
	/// </summary>
	public class FormDatabaseMaintenance:System.Windows.Forms.Form {
		private OpenDental.UI.Button butClose;
		private System.Windows.Forms.TextBox textBox1;
		private OpenDental.UI.Button buttonCheck;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private System.Drawing.Printing.PrintDocument pd2;
		private TextBox textLog;
		private Label label1;
		//private Queries Queries2;
		//private string logData;
		//DataConnection dcon;
		DataTable table;
		private CheckBox checkShow;
		private OpenDental.UI.Button butPrint;
		string command;

		///<summary></summary>
		public FormDatabaseMaintenance() {
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			Lan.C(this,new System.Windows.Forms.Control[]{
				this.textBox1,
				//this.textBox2
			}); //*Ann
			Lan.F(this);
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose(bool disposing) {
			if(disposing) {
				if(components != null) {
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
		private void InitializeComponent() {
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormDatabaseMaintenance));
			this.butClose = new OpenDental.UI.Button();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.buttonCheck = new OpenDental.UI.Button();
			this.pd2 = new System.Drawing.Printing.PrintDocument();
			this.textLog = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.checkShow = new System.Windows.Forms.CheckBox();
			this.butPrint = new OpenDental.UI.Button();
			this.SuspendLayout();
			// 
			// butClose
			// 
			this.butClose.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butClose.Autosize = true;
			this.butClose.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butClose.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butClose.CornerRadius = 4F;
			this.butClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butClose.Location = new System.Drawing.Point(787,631);
			this.butClose.Name = "butClose";
			this.butClose.Size = new System.Drawing.Size(87,26);
			this.butClose.TabIndex = 0;
			this.butClose.Text = "&Close";
			this.butClose.Click += new System.EventHandler(this.butClose_Click);
			// 
			// textBox1
			// 
			this.textBox1.BackColor = System.Drawing.SystemColors.Control;
			this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.textBox1.Location = new System.Drawing.Point(27,12);
			this.textBox1.Multiline = true;
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(847,24);
			this.textBox1.TabIndex = 1;
			this.textBox1.Text = "This tool will check the entire database for any improper settings, inconsistenci" +
    "es, or corruption.  If any problems are found, they will be fixed.";
			// 
			// buttonCheck
			// 
			this.buttonCheck.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.buttonCheck.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonCheck.Autosize = true;
			this.buttonCheck.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.buttonCheck.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.buttonCheck.CornerRadius = 4F;
			this.buttonCheck.Location = new System.Drawing.Point(668,631);
			this.buttonCheck.Name = "buttonCheck";
			this.buttonCheck.Size = new System.Drawing.Size(87,26);
			this.buttonCheck.TabIndex = 5;
			this.buttonCheck.Text = "C&heck Now";
			this.buttonCheck.Click += new System.EventHandler(this.buttonCheck_Click);
			// 
			// textLog
			// 
			this.textLog.Font = new System.Drawing.Font("Courier New",8.25F,System.Drawing.FontStyle.Regular,System.Drawing.GraphicsUnit.Point,((byte)(0)));
			this.textLog.Location = new System.Drawing.Point(27,77);
			this.textLog.Multiline = true;
			this.textLog.Name = "textLog";
			this.textLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textLog.Size = new System.Drawing.Size(847,543);
			this.textLog.TabIndex = 14;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(24,53);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(338,20);
			this.label1.TabIndex = 15;
			this.label1.Text = "Log (automatically saved in RepairLog.txt)";
			this.label1.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// checkShow
			// 
			this.checkShow.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkShow.Location = new System.Drawing.Point(27,35);
			this.checkShow.Name = "checkShow";
			this.checkShow.Size = new System.Drawing.Size(847,20);
			this.checkShow.TabIndex = 16;
			this.checkShow.Text = "Show me everything in the log  (only for advanced users)";
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
			this.butPrint.Location = new System.Drawing.Point(298,631);
			this.butPrint.Name = "butPrint";
			this.butPrint.Size = new System.Drawing.Size(87,26);
			this.butPrint.TabIndex = 18;
			this.butPrint.Text = "Print";
			this.butPrint.Click += new System.EventHandler(this.butPrint_Click);
			// 
			// FormDatabaseMaintenance
			// 
			this.AcceptButton = this.buttonCheck;
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.CancelButton = this.butClose;
			this.ClientSize = new System.Drawing.Size(895,667);
			this.Controls.Add(this.butPrint);
			this.Controls.Add(this.checkShow);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.textLog);
			this.Controls.Add(this.buttonCheck);
			this.Controls.Add(this.textBox1);
			this.Controls.Add(this.butClose);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormDatabaseMaintenance";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Database Maintenance";
			this.Load += new System.EventHandler(this.FormDatabaseMaintenance_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		private void butClose_Click(object sender,System.EventArgs e) {
			Close();
		}

		private void FormDatabaseMaintenance_Load(object sender,System.EventArgs e) {

		}

		private void buttonCheck_Click(object sender,System.EventArgs e) {
			Cursor=Cursors.WaitCursor;
			StringBuilder strB=new StringBuilder();
			strB.Append('-',90);
			textLog.Text=DateTime.Now.ToString()+strB.ToString()+"\r\n";
			Application.DoEvents();
			if(!MySQLTables()) {
				Cursor=Cursors.Default;
				return;
			}
			Application.DoEvents();
			if(!OracleSequences()) {
				Cursor=Cursors.Default;
				return;
			}
			Application.DoEvents();
			DatesNoZeros();
			Application.DoEvents();
			DecimalValues();
			Application.DoEvents();
			//Now, methods that apply to specific tables----------------------------------------------------------------------------
			AppointmentsNoPattern();
			Application.DoEvents();
			ClaimPlanNum2NotValid();
			Application.DoEvents();
			ClaimDeleteWithInvalidPlanNums();
			Application.DoEvents();
			ClaimWriteoffSum();
			Application.DoEvents();
			ClaimPaymentCheckAmt();//also fixes resulting deposit misbalances.
			Application.DoEvents();
			ClaimPaymentDeleteWithNoSplits();
			Application.DoEvents();
			ClaimProcDeleteWithInvalidClaimNum();
			Application.DoEvents();
			ClaimProcDeleteWithInvalidPlanNum();
			Application.DoEvents();
			ClaimProcDeleteWithInvalidProcNum();
			Application.DoEvents();
			ClaimProcEstNoBillIns();
			Application.DoEvents();
			ClaimProcProvNumMissing();
			Application.DoEvents();
			ClaimProcStatusNotMatchClaim();
			Application.DoEvents();
			ClaimProcWithInvalidClaimPaymentNum();
			Application.DoEvents();
			ClaimProcWriteOffNegative();
			Application.DoEvents();
			ClockEventInFuture();
			Application.DoEvents();
			DocumentWithNoCategory();
			Application.DoEvents();
			InsPlanCheckNoCarrier();
			Application.DoEvents();
			MedicationPatDeleteWithInvalidMedNum();
			Application.DoEvents();
			PatientBadGuarantor();
			Application.DoEvents();
			PatientPriProvMissing();
			Application.DoEvents();
			PatientUnDeleteWithBalance();
			Application.DoEvents();
			PayPlanChargeGuarantorMatch();
			Application.DoEvents();
			PatPlanOrdinalTwoToOne();
			Application.DoEvents();
			PayPlanSetGuarantorToPatForIns();
			Application.DoEvents();
			PaySplitDeleteWithInvalidPayNum();
			Application.DoEvents();
			PreferenceDateDepositsStarted();
			Application.DoEvents();
			PreferencePracticeBillingType();
			Application.DoEvents();
			PreferencePracticeProv();
			Application.DoEvents();
			ProcedurelogCodeNumZero();
			Application.DoEvents();
			ProcedurelogAttachedToWrongAppts();
			Application.DoEvents();
			ProcedurelogProvNumMissing();
			Application.DoEvents();
			ProcedurelogToothNums();
			Application.DoEvents();
			ProcedurelogUndeleteAttachedToClaim();
			Application.DoEvents();
			ProviderHiddenWithClaimPayments();
			Application.DoEvents();
			RecallDeleteDuplicate();
			Application.DoEvents();
			SchedulesDeleteShort();
			Application.DoEvents();
			SchedulesDeleteProvClosed();
			Application.DoEvents();
			SignalInFuture();
			Application.DoEvents();
			textLog.Text+=Lan.g(this,"Done");
			SaveLogToFile();
			//textLog.ScrollToCaret
			Cursor=Cursors.Default;
		}

		private bool MySQLTables() {
			if(FormChooseDatabase.DBtype!=DatabaseType.MySql) {
				return true;
			}
			command="SHOW TABLES";
			table=General.GetTable(command);
			string[] tableNames=new string[table.Rows.Count];
			int lastRow;
			bool allOK=true;
			//DialogResult result;
			//ArrayList corruptTables=new ArrayList();
			for(int i=0;i<table.Rows.Count;i++) {
				tableNames[i]=table.Rows[i][0].ToString();
			}
			for(int i=0;i<tableNames.Length;i++) {
				command="CHECK TABLE "+tableNames[i];
				table=General.GetTable(command);
				lastRow=table.Rows.Count-1;
				if(table.Rows[lastRow][3].ToString()!="OK") {
					textLog.Text+=Lan.g(this,"Corrupt file found for table")+" "+tableNames[i]+"\r\n";
					allOK=false;
					command="REPAIR TABLE "+tableNames[i];
					DataTable tableResults=General.GetTable(command);
					//we always log the results of a repair table, regardless of whether user wants to show all.
					textLog.Text+=Lan.g(this,"Repair log:")+"\r\n";
					for(int t=0;t<tableResults.Rows.Count;t++) {
						for(int j=0;j<tableResults.Columns.Count;j++) {
							textLog.Text+=tableResults.Rows[t][j].ToString()+",";
						}
						textLog.Text+="\r\n";
					}
				}
			}
			if(allOK) {
				if(checkShow.Checked) {
					textLog.Text+=Lan.g(this,"No corrupted tables.")+"\r\n";
				}
			}
			else {
				textLog.Text+=Lan.g(this,"Corrupted files probably fixed.  Look closely at the log.  Also, run again to be sure they were really fixed.")+"\r\n";
				return false;//no other checks should be done until we can successfully get past this.
			}
			return true;
		}

		private bool OracleSequences() {
			if(FormChooseDatabase.DBtype!=DatabaseType.Oracle) {
				return true;
			}
			bool changesWereMade=false;
			//Strings are grouped in pairs as tablename, then auto-incrementColumnName.
			string[] autoIncCols={
				"account","AccountNum",
				"accountingautopay","AccountingAutoPayNum",
				"adjustment","AdjNum",
				"appointment","AptNum",
				"appointmentrule","AppointmentRuleNum",
				"apptview","ApptViewNum",
				"apptviewitem","ApptViewItemNum",
				"autocode","AutoCodeNum",
				"autocodecond","AutoCodeCondNum",
				"autocodeitem","AutoCodeItemNum",
				"benefit","BenefitNum",
				//canadianclaim: no autoinc cols.
				"canadianextract","CanadianExtractNum",
				"canadiannetwork","CanadianNetworkNum",
				"carrier","CarrierNum",
				"claim","ClaimNum",
				"claimform","ClaimFormNum",
				"claimformitem","ClaimFormItemNum",
				"claimpayment","ClaimPaymentNum",
				"claimproc","ClaimProcNum",
				"clearinghouse","ClearinghouseNum",
				"clinic","ClinicNum",
				"clockevent","ClockEventNum",
				"commlog","CommlogNum",
				"computer","ComputerNum",
				"computerpref","ComputerPrefNum",
				"contact","ContactNum",
				//county: has no auto-inc cols.
				"covcat","CovCatNum",
				"covspan","CovSpanNum",
				"definition","DefNum",
				"deposit","DepositNum",
				"disease","DiseaseNum",
				"diseasedef","DiseaseDefNum",
				"docattach","DocAttachNum",
				"document","DocNum",
				"dunning","DunningNum",
				"electid","ElectIDNum",
				"emailattach","EmailAttachNum",
				"emailmessage","EmailMessageNum",
				"emailtemplate","EmailTemplateNum",
				"employee","EmployeeNum",
				"employer","EmployerNum",
				"etrans","EtransNum",
				"fee","FeeNum",
				"formpat","FormPatNum",
				//graphicassembly is obsolete
				//graphicelement is obsolete
				//graphicpoint is obsolete.
				//graphicshape is obsolete.
				//graphictype is obsolete.
				"grouppermission","GroupPermNum",
				"insplan","PlanNum",
				"instructor","InstructorNum",
				"journalentry","JournalEntryNum",
				//language: has no auto-inc col.
				//languageforeign: has no auto-inc col.
				"labcase","LabCaseNum",
				"laboratory","LaboratoryNum",
				"labturnaround","LabTurnaroundNum",
				"letter","LetterNum",
				"lettermerge","LetterMergeNum",
				"lettermergefield","FieldNum",
				"medication","MedicationNum",
				"medicationpat","MedicationPatNum",
				"mount","MountNum",
				"mountdef","MountDefNum",
				"mountitem","MountItemNum",
				"mountitemdef","MountItemDefNum",
				"operatory","OperatoryNum",
				"patfield","PatFieldNum",
				"patfielddef","PatFieldDefNum",
				"patient","PatNum",
				//patientnote: has no auto-inc col.
				"patplan","PatPlanNum",
				"payment","PayNum",
				"payperiod","PayPeriodNum",
				"payplan","PayPlanNum",
				"payplancharge","PayPlanChargeNum",
				"paysplit","SplitNum",
				"perioexam","PerioExamNum",
				"periomeasure","PerioMeasureNum",
				//preference: has no auto-inc col.
				"printer","PrinterNum",
				"procbutton","ProcButtonNum",
				"procbuttonitem","ProcButtonItemNum",
				"procedurecode","CodeNum",
				"procedurelog","ProcNum",
				"proclicense","ProcLicenseNum",
				"procnote","ProcNoteNum",
				"proctp","ProcTPNum",
				"program","ProgramNum",
				"programproperty","ProgramPropertyNum",
				"provider","ProvNum",
				"providerident","ProviderIdentNum",
				"question","QuestionNum",
				"questiondef","QuestionDefNum",
				"quickpastecat","QuickPasteCatNum",
				"quickpastenote","QuickPasteNoteNum",
				"recall","RecallNum",
				"reconcile","ReconcileNum",
				"refattach","RefAttachNum",
				"referral","ReferralNum",
				"registrationkey","RegistrationKeyNum",
				"repeatcharge","RepeatChargeNum",
				"reqneeded","ReqNeededNum",
				"rxalert","RxAlertNum",
				"rxdef","RxDefNum",
				"rxpat","RxNum",
				"scheddefault","SchedDefaultNum",
				"schedule","ScheduleNum",
				//school: has no auto-inc col.
				"schoolclass","SchoolClassNum",
				"schoolcourse","SchoolCourseNum",
				"screen","ScreenNum",
				"screengroup","ScreenGroupNum",
				"securitylog","SecurityLogNum",
				"sigbutdef","SigButDefNum",
				"sigbutdefelement","ElementNum",
				"sigelement","SigElementNum",
				"sigelementdef","SigElementDefNum",
				"signal","SignalNum",
				"task","TaskNum",
				"tasklist","TaskListNum",
				"terminalactive","TerminalActiveNum",
				"timeadjust","TimeAdjustNum",
				"toolbutitem","ToolButItemNum",
				"toothinitial","ToothInitialNum",
				"transaction","TransactionNum",
				"treatplan","TreatPlanNum",
				"usergroup","UserGroupNum",
				"userod","UserNum",
				"userquery","QueryNum",
				"zipcode","ZipCodeNum",
			};
			for(int i=0;i<autoIncCols.Length;i+=2) {
				string tablename=autoIncCols[i];
				string autoIncColName=autoIncCols[i+1];
				//Calculate the next available primary key value after the last available record.
				string command="SELECT "+autoIncColName+" FROM "+tablename+" ORDER BY "+autoIncColName+" DESC";
				DataTable table;
				try {
					table=General.GetTable(command);
				}
				catch {
					textLog.Text+="FAILED to lookup primary key '"+autoIncColName+"' from table '"+tablename+"'"+"\r\n";
					return false;
				}
				int nextPrimaryKey=1;
				if(table.Rows.Count>0) {
					nextPrimaryKey=PIn.PInt(table.Rows[0][0].ToString())+1;
				}
				string sequenceName=(tablename+"Seq").ToUpper();//Always stored as upper case by Oracle.
				string triggerName=(tablename+"Trig").ToUpper();
				//Verify the sequence.
				command="SELECT LAST_NUMBER FROM user_sequences WHERE SEQUENCE_NAME='"+sequenceName+"'";
				try {
					table=General.GetTable(command);
				}
				catch {
					textLog.Text+="FAILED to lookup existing sequence: '"+sequenceName+"'\r\n";
				}
				int last_number=0;
				if(table.Rows.Count>0) {//The sequence already exists.
					last_number=PIn.PInt(table.Rows[0][0].ToString());
					command="DROP SEQUENCE "+sequenceName;
					try {
						General.NonQ(command);
					}
					catch {
						textLog.Text+="FAILED to drop sequence "+sequenceName+"\r\n";
						return false;
					}
				}
				changesWereMade|=(last_number<nextPrimaryKey);
				command="CREATE SEQUENCE "+sequenceName+" START WITH "+nextPrimaryKey.ToString()+" INCREMENT BY 1 NOMAXVALUE";
				try {
					General.NonQ(command);
				}
				catch {
					textLog.Text+="FAILED to create sequence "+sequenceName+"\r\n";
					return false;
				}
				//Always just recreate the trigger so that it matches the sequence. This command wipes out the old trigger or adds a new
				//trigger if one did not previously exist. This will not count as a change.
				DataConnection.splitStrings=false;
				command="CREATE OR REPLACE TRIGGER "+triggerName+" BEFORE INSERT ON "+tablename+" FOR EACH ROW BEGIN IF "
					+"(:new."+autoIncColName+" IS NULL) THEN SELECT "+sequenceName+".nextval INTO :new."+autoIncColName
					+" FROM dual; END IF; UPDATE preference Set ValueString=:new."+autoIncColName
					+" WHERE PrefName='OracleInsertId'; END;";
				try {
					General.NonQ(command);
					DataConnection.splitStrings=true;//Must turn split strings back on after command is over
				}
				catch {
					textLog.Text+="FAILED to setup trigger "+triggerName+"\r\n";
					DataConnection.splitStrings=true;//Must turn split strings back on after command is over, even in failure.
					return false;
				}
			}
			if(!changesWereMade) {
				if(checkShow.Checked) {
					textLog.Text+=Lan.g(this,"Oracle sequences and triggers run.  No changes made.")+"\r\n";
				}
			}
			else {
				textLog.Text+=Lan.g(this,"Oracle sequences and triggers finished successfully.")+"\r\n";
			}
			return true;
		}

		private void DatesNoZeros() {
			if(FormChooseDatabase.DBtype==DatabaseType.Oracle) {
				return;//This check is not valid for Oracle, because each of the following fields are defined as non-null,
				//and 0000-00-00 is not a valid Oracle date.
			}
			string[] commands=new string[]
			{
				"UPDATE adjustment SET AdjDate='0001-01-01' WHERE AdjDate='0000-00-00'"
				,"UPDATE adjustment SET DateEntry='1980-01-01' WHERE DateEntry<'1980'"
				,"UPDATE appointment SET AptDateTime='0001-01-01 00:00:00' WHERE AptDateTime LIKE '0000-00-00%'"
				,"UPDATE claim SET DateService='0001-01-01' WHERE DateService='0000-00-00'"
				,"UPDATE claim SET DateSent='0001-01-01' WHERE DateSent='0000-00-00'"
				,"UPDATE claim SET DateReceived='0001-01-01' WHERE DateReceived='0000-00-00'"
				,"UPDATE claim SET PriorDate='0001-01-01' WHERE PriorDate='0000-00-00'"
				,"UPDATE claim SET AccidentDate='0001-01-01' WHERE AccidentDate='0000-00-00'"
				,"UPDATE claim SET OrthoDate='0001-01-01' WHERE OrthoDate='0000-00-00'"
				,"UPDATE claimpayment SET CheckDate='0001-01-01' WHERE CheckDate='0000-00-00'"
				,"UPDATE claimproc SET DateCP='0001-01-01' WHERE DateCP='0000-00-00'"
				,"UPDATE claimproc SET ProcDate='0001-01-01' WHERE ProcDate='0000-00-00'"
				,"UPDATE insplan SET DateEffective='0001-01-01' WHERE DateEffective='0000-00-00'"
				,"UPDATE insplan SET DateTerm='0001-01-01' WHERE DateTerm='0000-00-00'"
				,"UPDATE patient SET Birthdate='0001-01-01' WHERE Birthdate='0000-00-00'"
				,"UPDATE patient SET DateFirstVisit='0001-01-01' WHERE DateFirstVisit='0000-00-00'"
				,"UPDATE procedurelog SET ProcDate='0001-01-01 00:00:00' WHERE ProcDate LIKE '0000-00-00%'"
				,"UPDATE procedurelog SET DateOriginalProsth='0001-01-01' WHERE DateOriginalProsth='0000-00-00'"
				,"UPDATE procedurelog SET DateEntryC='0001-01-01' WHERE DateEntryC='0000-00-00'"
				,"UPDATE recall SET DateDueCalc='0001-01-01' WHERE DateDueCalc='0000-00-00'"
				,"UPDATE recall SET DateDue='0001-01-01' WHERE DateDue='0000-00-00'"
				,"UPDATE recall SET DatePrevious='0001-01-01' WHERE DatePrevious='0000-00-00'"
				,"UPDATE schedule SET SchedDate='0001-01-01' WHERE SchedDate='0000-00-00'"
				,"UPDATE signal SET DateViewing='0001-01-01' WHERE DateViewing='0000-00-00'"
				,"UPDATE signal SET SigDateTime='0001-01-01 00:00:00' WHERE SigDateTime LIKE '0000-00-00%'"
				,"UPDATE signal SET AckTime='0001-01-01 00:00:00' WHERE AckTime LIKE '0000-00-00%'"
			};
			int rowsChanged=General.NonQ(commands);
			if(rowsChanged !=0 || checkShow.Checked) {
				textLog.Text+=Lan.g(this,"Dates fixed. Rows changed:")+" "+rowsChanged.ToString()+"\r\n";
			}
		}

		private void DecimalValues() {
			//Holds columns to be checked. Strings are in pairs in the following order: table-name,column-name
			string[] decimalCols=new string[] {
				"patient","EstBalance"
			};
			int decimalPlacessToRoundTo=8;
			int numberFixed=0;
			for(int i=0;i<decimalCols.Length;i+=2) {
				string tablename=decimalCols[i];
				string colname=decimalCols[i+1];
				string command="UPDATE "+tablename+" SET "+colname+"=ROUND("+colname+","+decimalPlacessToRoundTo
					+") WHERE "+colname+"!=ROUND("+colname+","+decimalPlacessToRoundTo+")";
				numberFixed+=General.NonQ(command);
			}
			if(numberFixed>0 || checkShow.Checked) {
				textLog.Text+=Lan.g(this,"Decimal values fixed: ")+numberFixed.ToString()+"\r\n";
			}
		}

		//Methods that apply to specific tables----------------------------------------------------------------------------------

		private void AppointmentsNoPattern() {
			command=@"SELECT AptNum FROM appointment WHERE Pattern=''";
			table=General.GetTable(command);
			for(int i=0;i<table.Rows.Count;i++) {
				//detach all procedures
				command="UPDATE procedurelog SET AptNum=0 WHERE AptNum="+table.Rows[i][0].ToString();
				General.NonQ(command);
				command="UPDATE procedurelog SET PlannedAptNum=0 WHERE PlannedAptNum="+table.Rows[i][0].ToString();
				General.NonQ(command);
				command="DELETE FROM appointment WHERE AptNum="+table.Rows[i][0].ToString();
				General.NonQ(command);
			}
			int numberFixed=table.Rows.Count;
			if(numberFixed!=0 && !checkShow.Checked) {
				textLog.Text+=Lan.g(this,"Appointments deleted with zero length: ")+numberFixed.ToString()+"\r\n";
			}
		}

		private void ClaimPlanNum2NotValid() {
			//This fixes a slight database inconsistency that might cause an error when trying to open the send claims window. 
			command="UPDATE claim SET PlanNum2=0 WHERE PlanNum2 !=0 AND NOT EXISTS( SELECT * FROM insplan "
				+"WHERE claim.PlanNum2=insplan.PlanNum)";
			int numberFixed=General.NonQ(command);
			if(numberFixed>0 || checkShow.Checked) {
				textLog.Text+=Lan.g(this,"Claims with invalid PlanNum2 fixed: ")+numberFixed.ToString()+"\r\n";
			}
		}

		private void ClaimDeleteWithInvalidPlanNums() {
			command=@"SELECT ClaimNum,PatNum FROM claim
				LEFT JOIN insplan ON claim.PlanNum=insplan.PlanNum
				WHERE insplan.PlanNum IS NULL";
			table=General.GetTable(command);
			Patient Lim;
			int numberFixed=0;
			for(int i=0;i<table.Rows.Count;i++) {
				command="DELETE FROM claim WHERE ClaimNum="+table.Rows[i][0].ToString();
				General.NonQ(command);
				Lim=Patients.GetLim(PIn.PInt(table.Rows[i][1].ToString()));
				textLog.Text+=Lan.g(this,"Claim with invalid PlanNum deleted for ")+Lim.GetNameLF()+"\r\n";
				numberFixed++;
			}
			if(numberFixed!=0 && !checkShow.Checked) {
				textLog.Text+=Lan.g(this,"Claims deleted due to invalid PlanNum: ")+numberFixed.ToString()+"\r\n";
			}
		}

		private void ClaimWriteoffSum() {
			//Sums for each claim---------------------------------------------------------------------
			if(FormChooseDatabase.DBtype==DatabaseType.Oracle) {
				return;
			}
			command=@"SELECT claim.ClaimNum,SUM(claimproc.WriteOff) sumwo,claim.WriteOff
				FROM claim,claimproc
				WHERE claim.ClaimNum=claimproc.ClaimNum
				GROUP BY claim.ClaimNum
				HAVING sumwo-claim.WriteOff > .01
				OR sumwo-claim.WriteOff < -.01";
			table=General.GetTable(command);
			for(int i=0;i<table.Rows.Count;i++) {
				command="UPDATE claim SET WriteOff='"+POut.PDouble(PIn.PDouble(table.Rows[i]["sumwo"].ToString()))+"' "
					+"WHERE ClaimNum="+table.Rows[i]["ClaimNum"].ToString();
				General.NonQ(command);
			}
			int numberFixed=table.Rows.Count;
			if(numberFixed>0 || checkShow.Checked) {
				textLog.Text+=Lan.g(this,"Claim writeoff sums fixed: ")+numberFixed.ToString()+"\r\n";
			}
		}

		///<Summary>also fixes resulting deposit misbalances.</Summary>
		private void ClaimPaymentCheckAmt() {
			//because of the way this is grouped, it will just get one of many patients for each
			command=@"SELECT claimproc.ClaimPaymentNum,ROUND(SUM(InsPayAmt),2) _sumpay,ROUND(CheckAmt,2) _checkamt
				FROM claimpayment,claimproc
				WHERE claimpayment.ClaimPaymentNum=claimproc.ClaimPaymentNum
				GROUP BY claimproc.ClaimPaymentNum
				HAVING _sumpay!=_checkamt";
			table=General.GetTable(command);
			for(int i=0;i<table.Rows.Count;i++) {
				//if(i==0){
				//	textLog.Text+=Lan.g(this,"PRINT THIS FOR REFERENCE. Claim Payments fixed:")+"\r\n";
				//}
				//textLog.Text+="\r\n";
				command="UPDATE claimpayment SET CheckAmt='"+POut.PDouble(PIn.PDouble(table.Rows[i]["_sumpay"].ToString()))+"' "
					+"WHERE ClaimPaymentNum="+table.Rows[i]["ClaimPaymentNum"].ToString();
				General.NonQ(command);
			}
			int numberFixed=table.Rows.Count;
			if(numberFixed>0 || checkShow.Checked) {
				textLog.Text+=Lan.g(this,"Claim payment sums fixed: ")+numberFixed.ToString()+"\r\n";
			}
			//now deposits which were affected by the changes above--------------------------------------------------
			command=@"SELECT DepositNum,deposit.Amount,DateDeposit,
				IFNULL((SELECT SUM(CheckAmt) FROM claimpayment WHERE claimpayment.DepositNum=deposit.DepositNum GROUP BY deposit.DepositNum),0)
				+IFNULL((SELECT SUM(PayAmt) FROM payment WHERE payment.DepositNum=deposit.DepositNum GROUP BY deposit.DepositNum),0) _sum
				FROM deposit
				HAVING ROUND(_sum,2) != ROUND(deposit.Amount,2)";
			table=General.GetTable(command);
			for(int i=0;i<table.Rows.Count;i++) {
				if(i==0) {
					textLog.Text+=Lan.g(this,"PRINT THIS FOR REFERENCE. Deposit sums recalculated:")+"\r\n";
				}
				DateTime date=PIn.PDate(table.Rows[i]["DateDeposit"].ToString());
				Double oldval=PIn.PDouble(table.Rows[i]["Amount"].ToString());
				Double newval=PIn.PDouble(table.Rows[i]["_sum"].ToString());
				textLog.Text+=date.ToShortDateString()+" "+Lan.g(this,"OldSum:")+oldval.ToString("c")
					+", "+Lan.g(this,"NewSum:")+newval.ToString("c")+"\r\n";
				command="UPDATE deposit SET Amount='"+POut.PDouble(PIn.PDouble(table.Rows[i]["_sum"].ToString()))+"' "
					+"WHERE DepositNum="+table.Rows[i]["DepositNum"].ToString();
				General.NonQ(command);
			}
			numberFixed=table.Rows.Count;
			if(numberFixed>0 || checkShow.Checked) {
				textLog.Text+=Lan.g(this,"Deposit sums fixed: ")+numberFixed.ToString()+"\r\n";
			}
		}

		private void ClaimPaymentDeleteWithNoSplits() {
			command="DELETE FROM claimpayment WHERE NOT EXISTS("
				+"SELECT * FROM claimproc WHERE claimpayment.ClaimPaymentNum=claimproc.ClaimPaymentNum)";
			int numberFixed=General.NonQ(command);
			if(numberFixed>0 || checkShow.Checked) {
				textLog.Text+=Lan.g(this,"Claim payments with no splits removed: ")+numberFixed.ToString()+"\r\n";
			}
		}

		private void ClaimProcDeleteWithInvalidClaimNum() {
			command="DELETE FROM claimproc WHERE claimproc.ClaimNum!=0 "
				+"AND NOT EXISTS(SELECT * FROM claim WHERE claim.ClaimNum=claimproc.ClaimNum)";
			int numberFixed=General.NonQ(command);
			if(numberFixed>0 || checkShow.Checked) {
				textLog.Text+=Lan.g(this,"Claimprocs deleted due to invalid ClaimNum: ")+numberFixed.ToString()+"\r\n";
			}
		}

		private void ClaimProcDeleteWithInvalidPlanNum() {
			command=@"SELECT ClaimProcNum,PatNum FROM claimproc
				LEFT JOIN insplan ON claimproc.PlanNum=insplan.PlanNum
				WHERE insplan.PlanNum IS NULL";
			table=General.GetTable(command);
			Patient Lim;
			int numberFixed=0;
			for(int i=0;i<table.Rows.Count;i++) {
				command="DELETE FROM claimproc WHERE ClaimProcNum="+table.Rows[i][0].ToString();
				General.NonQ(command);
				Lim=Patients.GetLim(PIn.PInt(table.Rows[i][1].ToString()));
				textLog.Text+=Lan.g(this,"Claimproc with invalid PlanNum deleted for ")+Lim.GetNameLF()+"\r\n";
				numberFixed++;
			}
			if(numberFixed!=0 && !checkShow.Checked) {
				textLog.Text+=Lan.g(this,"ClaimProcs deleted due to invalid PlanNum: ")+numberFixed.ToString()+"\r\n";
			}
		}

		private void ClaimProcDeleteWithInvalidProcNum() {
			//These seem to pop up quite regularly due to the program forgetting to delete them
			command="DELETE FROM claimproc WHERE ProcNum>0 AND NOT EXISTS(SELECT * FROM procedurelog "
				+"WHERE claimproc.ProcNum=procedurelog.ProcNum)";
			int numberFixed=General.NonQ(command);
			if(numberFixed>0 || checkShow.Checked) {
				textLog.Text+=Lan.g(this,"Estimates deleted for procedures that no longer exist: ")+numberFixed.ToString()+"\r\n";
			}
		}

		private void ClaimProcEstNoBillIns() {
			command="UPDATE claimproc SET InsPayEst=0 WHERE NoBillIns=1 AND InsPayEst !=0";
			int numberFixed=General.NonQ(command);
			if(numberFixed>0 || checkShow.Checked) {
				textLog.Text+=Lan.g(this,"Claimproc estimates set to zero because marked NoBillIns: ")+numberFixed.ToString()+"\r\n";
			}
		}

		private void ClaimProcProvNumMissing() {
			command="UPDATE claimproc SET ProvNum="+PrefB.GetString("PracticeDefaultProv")+" WHERE ProvNum=0";
			int numberFixed=General.NonQ(command);
			if(numberFixed>0 || checkShow.Checked) {
				textLog.Text+=Lan.g(this,"ClaimProcs with missing provnums fixed: ")+numberFixed.ToString()+"\r\n";
			}
		}

		private void ClaimProcStatusNotMatchClaim() {
			if(FormChooseDatabase.DBtype==DatabaseType.Oracle) {
				return;
			}
			command=@"UPDATE claimproc,claim
				SET claimproc.Status=1
				WHERE claimproc.ClaimNum=claim.ClaimNum
				AND claim.ClaimStatus='R'
				AND claimproc.Status=0";
			int numberFixed=General.NonQ(command);
			if(numberFixed>0 || checkShow.Checked) {
				textLog.Text+=Lan.g(this,"ClaimProcs with status not matching claim fixed: ")+numberFixed.ToString()+"\r\n";
			}
		}

		private void ClaimProcWithInvalidClaimPaymentNum() {
			command=@"UPDATE claimproc SET ClaimPaymentNum=0 WHERE claimpaymentnum !=0 AND NOT EXISTS(
				SELECT * FROM claimpayment WHERE claimpayment.ClaimPaymentNum=claimproc.ClaimPaymentNum)";
			int numberFixed=General.NonQ(command);
			if(numberFixed>0 || checkShow.Checked) {
				textLog.Text+=Lan.g(this,"ClaimProcs with with invalid ClaimPaymentNumber fixed: ")+numberFixed.ToString()+"\r\n";
			}
		}

		private void ClaimProcWriteOffNegative() {
			command=@"UPDATE claimproc SET WriteOff = -WriteOff WHERE WriteOff < 0";
			int numberFixed=General.NonQ(command);
			if(numberFixed>0 || checkShow.Checked) {
				textLog.Text+=Lan.g(this,"Negative writeoffs fixed: ")+numberFixed.ToString()+"\r\n";
			}
		}

		private void ClockEventInFuture() {
			command=@"UPDATE clockevent SET TimeDisplayed=TimeEntered WHERE TimeDisplayed > NOW()";
			if(FormChooseDatabase.DBtype==DatabaseType.Oracle) {
				command=@"UPDATE clockevent SET TimeDisplayed=TimeEntered WHERE TimeDisplayed > "
					+POut.PDateT(MiscData.GetNowDateTime());
			}
			int numberFixed=General.NonQ(command);
			if(numberFixed>0 || checkShow.Checked) {
				textLog.Text+=Lan.g(this,"Timecard entries fixed: ")+numberFixed.ToString()+"\r\n";
			}
		}

		private void DocumentWithNoCategory() {
			command="SELECT DocNum FROM document WHERE DocCategory=0";
			table=General.GetTable(command);
			for(int i=0;i<table.Rows.Count;i++) {
				command="UPDATE document SET DocCategory="+POut.PInt(DefB.Short[(int)DefCat.ImageCats][0].DefNum)
					+" WHERE DocNum="+table.Rows[i][0].ToString();
				General.NonQ(command);
			}
			int numberFixed=table.Rows.Count;
			if(numberFixed>0||checkShow.Checked) {
				textLog.Text+=Lan.g(this,"Images with no category fixed: ")+numberFixed.ToString()+"\r\n";
			}
		}

		private void InsPlanCheckNoCarrier() {
			//Gets a list of insurance plans that do not have a carrier attached. The list should be blank. If not, then you need to go to the plan listed and add a carrier. Missing carriers will cause the send claims function to give an error.
			command="SELECT Subscriber FROM insplan WHERE CarrierNum=0";
			table=General.GetTable(command);
			if(table.Rows.Count==0) {
				if(checkShow.Checked) {
					textLog.Text+=Lan.g(this,"Insurance plans checked for missing CarrierNums.")+"\r\n";
					return;
				}
			}
			Patient Lim;
			for(int i=0;i<table.Rows.Count;i++) {
				Lim=Patients.GetLim(PIn.PInt(table.Rows[i][0].ToString()));
				textLog.Text+=Lan.g(this,"Warning!")+" "+Lim.GetNameFL()+" "+Lan.g(this,"has an insurance plan that does not have a carrier assigned.  Please fix this.")+"\r\n";
			}
		}

		private void MedicationPatDeleteWithInvalidMedNum() {
			command="DELETE FROM medicationpat WHERE NOT EXISTS(SELECT * FROM medication "
				+"WHERE medication.MedicationNum=medicationpat.MedicationNum)";
			int numberFixed=General.NonQ(command);
			if(numberFixed>0 || checkShow.Checked) {
				textLog.Text+=Lan.g(this,"Medications deleted because no defition exists for them: ")+numberFixed.ToString()+"\r\n";
			}
		}

		private void PatientBadGuarantor() {
			command="SELECT p.PatNum FROM patient p LEFT JOIN patient p2 ON p.Guarantor = p2.PatNum WHERE p2.PatNum IS NULL";
			table=General.GetTable(command);
			for(int i=0;i<table.Rows.Count;i++) {
				command="UPDATE patient SET Guarantor=PatNum WHERE PatNum="+table.Rows[i][0].ToString();
				General.NonQ(command);
			}
			int numberFixed=table.Rows.Count;
			if(numberFixed>0 || checkShow.Checked) {
				textLog.Text+=Lan.g(this,"Patients with invalid Guarantors fixed: ")+numberFixed.ToString()+"\r\n";
			}
		}

		private void PatientPriProvMissing() {
			//previous versions of the program just dealt gracefully with missing provnum.
			//From now on, we can assum priprov is not missing, making coding easier.
			command=@"UPDATE patient SET PriProv="+PrefB.GetString("PracticeDefaultProv")+" WHERE PriProv=0";
			int numberFixed=General.NonQ(command);
			if(numberFixed>0 || checkShow.Checked) {
				textLog.Text+=Lan.g(this,"Patient pri provs fixed: ")+numberFixed.ToString()+"\r\n";
			}
		}

		private void PatientUnDeleteWithBalance() {
			command="SELECT PatNum FROM patient	WHERE PatStatus=4 "
				+"AND (Bal_0_30 !=0	OR Bal_31_60 !=0 OR Bal_61_90 !=0	OR BalOver90 !=0 OR InsEst !=0 OR BalTotal !=0)";
			table=General.GetTable(command);
			if(table.Rows.Count==0 && checkShow.Checked) {
				textLog.Text+=Lan.g(this,"No balances found for deleted patients.")+"\r\n";
				return;
			}
			Patient pat;
			Patient old;
			for(int i=0;i<table.Rows.Count;i++) {
				pat=Patients.GetPat(PIn.PInt(table.Rows[i][0].ToString()));
				old=pat.Copy();
				pat.LName=pat.LName+Lan.g(this,"DELETED");
				pat.PatStatus=PatientStatus.Archived;
				Patients.Update(pat,old);
				textLog.Text+=Lan.g(this,"Warning!  Patient:")+" "+old.GetNameFL()+" "
					+Lan.g(this,"was previously marked as deleted, but was found to have a balance. Patient has been changed to Archive status.  The account will need to be cleared, and the patient deleted again.")+"\r\n";
			}
		}

		private void PatPlanOrdinalTwoToOne() {
			command="SELECT PatPlanNum FROM patplan patplan1 WHERE Ordinal=2 AND NOT EXISTS("
				+"SELECT * FROM patplan patplan2 WHERE patplan1.PatNum=patplan2.PatNum AND patplan2.Ordinal=1)";
			table=General.GetTable(command);
			for(int i=0;i<table.Rows.Count;i++) {
				command="UPDATE patplan SET Ordinal=1 WHERE PatPlanNum="+table.Rows[i][0].ToString();
				General.NonQ(command);
			}
			int numberFixed=table.Rows.Count;
			if(numberFixed>0 || checkShow.Checked) {
				textLog.Text+=Lan.g(this,"PatPlan ordinals changed from 2 to 1 if no primary ins: ")+numberFixed.ToString()+"\r\n";
			}
		}

		private void PayPlanChargeGuarantorMatch() {
			if(FormChooseDatabase.DBtype==DatabaseType.Oracle) {
				return;
			}
			int numberFixed=0;
			command="UPDATE payplancharge,payplan SET payplancharge.Guarantor=payplan.Guarantor "
				+"WHERE payplan.PayPlanNum=payplancharge.PayPlanNum "
				+"AND payplancharge.Guarantor != payplan.Guarantor";
			numberFixed+=General.NonQ(command);
			command="UPDATE payplancharge,payplan SET payplancharge.PatNum=payplan.PatNum "
				+"WHERE payplan.PayPlanNum=payplancharge.PayPlanNum "
				+"AND payplancharge.PatNum != payplan.PatNum";
			numberFixed+=General.NonQ(command);
			if(numberFixed>0 || checkShow.Checked) {
				textLog.Text+=Lan.g(this,"PayPlanCharge guarantors and pats set to match payplan guarantors and pats: ")
					+numberFixed.ToString()+"\r\n";
			}
		}

		private void PayPlanSetGuarantorToPatForIns() {
			command="UPDATE payplan SET Guarantor=PatNum WHERE PlanNum>0 AND Guarantor != PatNum";
			int numberFixed=General.NonQ(command);
			if(numberFixed>0 || checkShow.Checked) {
				textLog.Text+=Lan.g(this,"PayPlan Guarantors set to PatNum if used for insurance tracking: ")
					+numberFixed.ToString()+"\r\n";
			}
		}

		private void PaySplitDeleteWithInvalidPayNum() {
			command="DELETE FROM paysplit WHERE NOT EXISTS(SELECT * FROM payment WHERE paysplit.PayNum=payment.PayNum)";
			int numberFixed=General.NonQ(command);
			if(numberFixed>0 || checkShow.Checked) {
				textLog.Text+=Lan.g(this,"Paysplits deleted due to invalid PayNum: ")+numberFixed.ToString()+"\r\n";
			}
		}

		private void PreferenceDateDepositsStarted() {
			//If the program locks up when trying to create a deposit slip, it's because someone removed the start date from the deposit edit window. Run this query to get back in.
			DateTime date=PrefB.GetDate("DateDepositsStarted");
			if(date<DateTime.Now.AddMonths(-1)) {
				command="UPDATE preference SET ValueString="+POut.PDate(DateTime.Today.AddDays(-21))
					+" WHERE PrefName='DateDepositsStarted'";
				General.NonQ(command);
				DataValid.SetInvalid(InvalidTypes.Prefs);
				textLog.Text+=Lan.g(this,"Deposit start date reset.")+"\r\n";
			}
			else if(checkShow.Checked) {
				textLog.Text+=Lan.g(this,"Deposit start date checked.")+"\r\n";
			}
		}

		private void PreferencePracticeBillingType() {
			command="SELECT valuestring FROM preference WHERE prefname = 'PracticeDefaultBillType'";
			table=General.GetTable(command);
			if(table.Rows[0][0].ToString()!="") {
				if(checkShow.Checked) {
					textLog.Text+=Lan.g(this,"Default practice billing type verified.")+"\r\n";
				}
				return;
			}
			textLog.Text+=Lan.g(this,"No default billing type set.");
			command="SELECT defnum FROM definition WHERE category = 4 AND ishidden = 0 ORDER BY itemorder";
			table=General.GetTable(command);
			command="UPDATE preference SET valuestring='"+table.Rows[0][0].ToString()+"' WHERE prefname='PracticeDefaultBillType'";
			General.NonQ(command);
			textLog.Text+="  "+Lan.g(this,"Fixed.")+"\r\n";
		}

		private void PreferencePracticeProv() {
			command="SELECT valuestring FROM preference WHERE prefname = 'PracticeDefaultProv'";
			table=General.GetTable(command);
			if(table.Rows[0][0].ToString()!="") {
				if(checkShow.Checked) {
					textLog.Text+=Lan.g(this,"Default practice provider verified.")+"\r\n";
				}
				return;
			}
			textLog.Text+=Lan.g(this,"No default provider set.");
			if(FormChooseDatabase.DBtype==DatabaseType.Oracle) {
				command="SELECT ProvNum FROM provider WHERE IsHidden=0 ORDER BY itemorder";
			}
			else {//MySQL
				command="SELECT provnum FROM provider WHERE IsHidden=0 ORDER BY itemorder LIMIT 1";
			}
			table=General.GetTable(command);
			command="UPDATE preference SET valuestring = '"+table.Rows[0][0].ToString()+"' WHERE prefname = 'PracticeDefaultProv'";
			General.NonQ(command);
			textLog.Text+="  "+Lan.g(this,"Fixed.")+"\r\n";
		}

		private void ProcedurelogCodeNumZero() {
			command="DELETE FROM procedurelog WHERE CodeNum=0";
			int numberFixed=General.NonQ(command);
			if(numberFixed>0 || checkShow.Checked) {
				textLog.Text+=Lan.g(this,"Procedures deleted with CodeNum=0: ")+numberFixed.ToString()+"\r\n";
			}
		}

		private void ProcedurelogAttachedToWrongAppts() {
			if(FormChooseDatabase.DBtype==DatabaseType.Oracle) {
				return;
			}
			command="UPDATE appointment,procedurelog SET procedurelog.AptNum=0 "
				+"WHERE procedurelog.AptNum=appointment.AptNum AND procedurelog.PatNum != appointment.PatNum";
			int numberFixed=General.NonQ(command);
			if(numberFixed>0 || checkShow.Checked) {
				textLog.Text+=Lan.g(this,"Procedures detached from appointments: ")+numberFixed.ToString()+"\r\n";
			}
		}

		private void ProcedurelogProvNumMissing() {
			command="UPDATE procedurelog SET ProvNum="+PrefB.GetString("PracticeDefaultProv")+" WHERE ProvNum=0";
			int numberFixed=General.NonQ(command);
			if(numberFixed>0 || checkShow.Checked) {
				textLog.Text+=Lan.g(this,"Procedures with missing provnums fixed: ")+numberFixed.ToString()+"\r\n";
			}
		}

		private void ProcedurelogToothNums() {
			Patient Lim=null;
			string toothNum;
			int numberFixed=0;
			command="SELECT procnum,toothnum,patnum FROM procedurelog";
			table=General.GetTable(command);
			for(int i=0;i<table.Rows.Count;i++) {
				toothNum=table.Rows[i][1].ToString();
				if(toothNum=="")
					continue;
				if(Tooth.IsValidDB(toothNum)) {
					continue;
				}
				if(checkShow.Checked) {
					Lim=Patients.GetLim(Convert.ToInt32(table.Rows[i][2].ToString()));
				}
				if(string.CompareOrdinal(toothNum,"a")>=0 && string.CompareOrdinal(toothNum,"t")<=0) {
					command="UPDATE procedurelog SET ToothNum = '"+toothNum.ToUpper()+"' WHERE ProcNum = "+table.Rows[i][0].ToString();
					General.NonQ(command);
					if(checkShow.Checked) {
						textLog.Text+=Lim.GetNameLF()+" "+toothNum+" - "+toothNum.ToUpper()+"\r\n";
					}
					numberFixed++;
				}
				else {
					command="UPDATE procedurelog SET ToothNum = '1' WHERE ProcNum = "+table.Rows[i][0].ToString();
					General.NonQ(command);
					if(checkShow.Checked) {
						textLog.Text+=Lim.GetNameLF()+" "+toothNum+" - 1\r\n";
					}
					numberFixed++;
				}
			}
			if(numberFixed!=0 || checkShow.Checked) {
				textLog.Text+=Lan.g(this,"Check for invalid tooth numbers complete.  Records checked: ")
					+table.Rows.Count.ToString()+". "+Lan.g(this,"Records fixed: ")+numberFixed.ToString()+"\r\n";
			}
		}

		private void ProcedurelogUndeleteAttachedToClaim() {
			if(FormChooseDatabase.DBtype==DatabaseType.Oracle) {
				return;
			}
			command=@"UPDATE procedurelog,claimproc         
				SET procedurelog.ProcStatus=2
				WHERE procedurelog.ProcNum=claimproc.ProcNum
				AND procedurelog.ProcStatus=6
				AND claimproc.ClaimNum!=0";
			int numberFixed=General.NonQ(command);
			if(numberFixed>0 || checkShow.Checked) {
				textLog.Text+=Lan.g(this,"Procedures undeleted because found attached to claims: ")+numberFixed.ToString()+"\r\n";
			}
		}

		private void ProviderHiddenWithClaimPayments() {
			command=@"SELECT MAX(claimproc.ProcDate),provider.ProvNum
				FROM claimproc,provider
				WHERE claimproc.ProvNum=provider.ProvNum
				AND provider.IsHidden=1
				AND claimproc.InsPayAmt>0
				GROUP BY provider.ProvNum";
			table=General.GetTable(command);
			if(table.Rows.Count==0) {
				if(checkShow.Checked) {
					textLog.Text+=Lan.g(this,"Hidden providers checked for claim payments.")+"\r\n";
					return;
				}
			}
			Provider prov;
			for(int i=0;i<table.Rows.Count;i++) {
				prov=Providers.GetProv(PIn.PInt(table.Rows[i][1].ToString()));
				textLog.Text+=Lan.g(this,"Warning!  Hidden provider ")+" "+prov.Abbr+" "
					+Lan.g(this,"has claim payments entered as recently as ")
					+PIn.PDate(table.Rows[i][0].ToString()).ToShortDateString()
					+Lan.g(this,".  This data will be missing on income reports.")+"\r\n";
			}
		}

		private void RecallDeleteDuplicate() {
			//command="SELECT COUNT(*) AS repetitions,PatNum FROM recall GROUP BY PatNum HAVING repetitions >1";
			command="SELECT COUNT(*),PatNum FROM recall GROUP BY PatNum HAVING COUNT(*) >1";
			table=General.GetTable(command);
			int numberFound=table.Rows.Count;
			//we're going to do one patient at a time.
			DataTable tableRecalls;
			for(int i=0;i<table.Rows.Count;i++) {
				command="SELECT RecallNum FROM recall WHERE PatNum="+table.Rows[i][1].ToString();
				tableRecalls=General.GetTable(command);
				command="DELETE FROM recall WHERE ";
				for(int r=0;r<tableRecalls.Rows.Count-1;r++) {//we ignore the last row
					if(r>0) {
						command+="OR ";
					}
					command+="RecallNum="+tableRecalls.Rows[r][0].ToString()+" ";
				}
				General.NonQ(command);
				//pats+=table.Rows[i][1].ToString();
			}
			if(numberFound>0) {
				Recalls.SynchAllPatients();
			}
			if(numberFound>0 || checkShow.Checked) {
				textLog.Text+=Lan.g(this,"Duplicate recall entries fixed: ")+numberFound.ToString()+"\r\n";
			}
		}

		private void SchedulesDeleteShort() {
			int numberFixed=0;
			Schedule[] schedList=Schedules.RefreshAll();
			for(int i=0;i<schedList.Length;i++) {
				if(schedList[i].Status!=SchedStatus.Open) {
					continue;//closed and holiday statuses do not use starttime and stoptime
				}
				if(schedList[i].StopTime.TimeOfDay-schedList[i].StartTime.TimeOfDay<new TimeSpan(0,5,0)) {
					Schedules.Delete(schedList[i]);
					numberFixed++;
				}
			}
			if(numberFixed>0 || checkShow.Checked) {
				textLog.Text+=Lan.g(this,"Schedule blocks fixed: ")+numberFixed.ToString()+"\r\n";
			}
			//DataValid.SetInvalid(InvalidTypes.Sched);
		}

		private void SchedulesDeleteProvClosed() {
			command="DELETE FROM schedule WHERE SchedType=1 AND Status=1";//type=prov,status=closed
			int numberFixed=General.NonQ(command);
			if(numberFixed>0||checkShow.Checked) {
				textLog.Text+=Lan.g(this,"Schedules deleted that were causing printing issues: ")+numberFixed.ToString()+"\r\n";
			}
		}

		private void SignalInFuture() {
			command=@"DELETE FROM signal WHERE SigDateTime > NOW() OR AckTime > NOW()";
			if(FormChooseDatabase.DBtype==DatabaseType.Oracle) {
				string nowDateTime=POut.PDateT(MiscData.GetNowDateTime());
				command=@"DELETE FROM signal WHERE SigDateTime > "+nowDateTime+" OR AckTime > "+nowDateTime;
			}
			int numberFixed=General.NonQ(command);
			if(numberFixed>0 || checkShow.Checked) {
				textLog.Text+=Lan.g(this,"Signal entries deleted: ")+numberFixed.ToString()+"\r\n";
			}
		}






		private void SaveLogToFile() {
			FileStream fs=new FileStream("RepairLog.txt",FileMode.Append,FileAccess.Write,FileShare.Read);
			StreamWriter sw=new StreamWriter(fs);
			sw.WriteLine(textLog.Text);
			sw.Close();
			sw=null;
			fs.Close();
			fs=null;
		}

		private void butPrint_Click(object sender,EventArgs e) {
			pd2 = new PrintDocument();
			pd2.PrintPage += new PrintPageEventHandler(this.pd2_PrintPage);
			pd2.DefaultPageSettings.Margins=new Margins(40,50,50,60);
			//pagesPrinted=0;
			//linesPrinted=0;
			try {
				pd2.Print();
			}
			catch {
				MessageBox.Show("Printer not available");
			}
		}

		private void pd2_PrintPage(object sender,PrintPageEventArgs ev) {//raised for each page to be printed.
			int yPos = ev.MarginBounds.Top;
			int xPos=ev.MarginBounds.Left;
			ev.Graphics.DrawString(textLog.Text,new Font("Courier New",10),Brushes.Black,xPos,yPos);
			ev.HasMorePages = false;
		}






	}
}
