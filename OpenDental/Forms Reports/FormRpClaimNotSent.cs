using System;
using System.Data;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{
///<summary></summary>
	public class FormRpClaimNotSent : System.Windows.Forms.Form{
		private OpenDental.UI.Button butCancel;
		private OpenDental.UI.Button butOK;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.RadioButton radioRange;
		private System.Windows.Forms.RadioButton radioSingle;
		private System.Windows.Forms.MonthCalendar date2;
		private System.Windows.Forms.MonthCalendar date1;
		private System.Windows.Forms.Label labelTO;
		private System.ComponentModel.Container components = null;
		private FormQuery FormQuery2;

		///<summary></summary>
		public FormRpClaimNotSent(){
			InitializeComponent();
			Lan.F(this);
		}

		///<summary></summary>
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormRpClaimNotSent));
			this.butCancel = new OpenDental.UI.Button();
			this.butOK = new OpenDental.UI.Button();
			this.panel1 = new System.Windows.Forms.Panel();
			this.radioRange = new System.Windows.Forms.RadioButton();
			this.radioSingle = new System.Windows.Forms.RadioButton();
			this.date2 = new System.Windows.Forms.MonthCalendar();
			this.date1 = new System.Windows.Forms.MonthCalendar();
			this.labelTO = new System.Windows.Forms.Label();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// butCancel
			// 
			this.butCancel.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butCancel.Autosize = true;
			this.butCancel.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butCancel.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butCancel.CornerRadius = 4F;
			this.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butCancel.Location = new System.Drawing.Point(523,328);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75,26);
			this.butCancel.TabIndex = 4;
			this.butCancel.Text = "&Cancel";
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.CornerRadius = 4F;
			this.butOK.Location = new System.Drawing.Point(523,294);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75,26);
			this.butOK.TabIndex = 3;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.radioRange);
			this.panel1.Controls.Add(this.radioSingle);
			this.panel1.Location = new System.Drawing.Point(19,16);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(104,60);
			this.panel1.TabIndex = 0;
			// 
			// radioRange
			// 
			this.radioRange.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.radioRange.Location = new System.Drawing.Point(8,32);
			this.radioRange.Name = "radioRange";
			this.radioRange.Size = new System.Drawing.Size(88,24);
			this.radioRange.TabIndex = 1;
			this.radioRange.Text = "Date Range";
			// 
			// radioSingle
			// 
			this.radioSingle.Checked = true;
			this.radioSingle.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.radioSingle.Location = new System.Drawing.Point(8,8);
			this.radioSingle.Name = "radioSingle";
			this.radioSingle.Size = new System.Drawing.Size(88,24);
			this.radioSingle.TabIndex = 0;
			this.radioSingle.TabStop = true;
			this.radioSingle.Text = "Single Date";
			this.radioSingle.CheckedChanged += new System.EventHandler(this.radioSingle_CheckedChanged);
			// 
			// date2
			// 
			this.date2.Location = new System.Drawing.Point(291,112);
			this.date2.Name = "date2";
			this.date2.TabIndex = 2;
			this.date2.Visible = false;
			// 
			// date1
			// 
			this.date1.Location = new System.Drawing.Point(35,112);
			this.date1.Name = "date1";
			this.date1.TabIndex = 1;
			// 
			// labelTO
			// 
			this.labelTO.Location = new System.Drawing.Point(217,120);
			this.labelTO.Name = "labelTO";
			this.labelTO.Size = new System.Drawing.Size(70,23);
			this.labelTO.TabIndex = 16;
			this.labelTO.Text = "TO";
			this.labelTO.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.labelTO.Visible = false;
			// 
			// FormRpClaimNotSent
			// 
			this.AcceptButton = this.butOK;
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.CancelButton = this.butCancel;
			this.ClientSize = new System.Drawing.Size(616,366);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.date2);
			this.Controls.Add(this.date1);
			this.Controls.Add(this.labelTO);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormRpClaimNotSent";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Claims Not Sent Report";
			this.Load += new System.EventHandler(this.FormClaimNotSent_Load);
			this.panel1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion
		private void FormClaimNotSent_Load(object sender, System.EventArgs e) {
			date1.SelectionStart=DateTime.Today;
			date2.SelectionStart=DateTime.Today;		
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			ReportSimpleGrid report=new ReportSimpleGrid();
			report.Query="SELECT claim.dateservice,claim.claimnum,claim.claimtype,claim.claimstatus,"
				+"CONCAT(CONCAT(CONCAT(CONCAT(patient.LName,', '),patient.FName),' '),patient.MiddleI),carrier.CarrierName,claim.claimfee "
				+"FROM patient,claim,insplan,carrier "
				+"WHERE patient.patnum=claim.patnum AND insplan.plannum=claim.plannum "
				+"AND insplan.CarrierNum=carrier.CarrierNum "	
				+"AND (claim.claimstatus = 'U' OR claim.claimstatus = 'H' OR  claim.claimstatus = 'W')";
			if(radioRange.Checked==true){	
				report.Query
					+=" AND claim.dateservice >= '" + date1.SelectionStart.ToString("yyyy-MM-dd")+"' "
					+"AND claim.dateservice <= '" + date2.SelectionStart.ToString("yyyy-MM-dd")+"'";
			}
			else{
				report.Query
					+=" AND claim.dateservice = '" + date1.SelectionStart.ToString("yyyy-MM-dd")+"'";
			}
			FormQuery2=new FormQuery(report);
			FormQuery2.IsReport=true;
			DataTable tempT=report.GetTempTable();
			report.TableQ=new DataTable(null);//new table no name
			for(int i=0;i<6;i++){//add columns
				report.TableQ.Columns.Add(new System.Data.DataColumn());//blank columns
			}
			report.InitializeColumns();
			for(int i=0;i<tempT.Rows.Count;i++) {//loop through data rows
				DataRow row=report.TableQ.NewRow();//create new row called 'row' based on structure of TableQ
				row[0]=(PIn.PDate(tempT.Rows[i][0].ToString())).ToShortDateString();//claim dateservice
				if(PIn.PString(tempT.Rows[i][2].ToString())=="P")
          row[1]="Primary";
				if(PIn.PString(tempT.Rows[i][2].ToString())=="S")
          row[1]="Secondary";
				if(PIn.PString(tempT.Rows[i][2].ToString())=="PreAuth")
          row[1]="PreAuth";
				if(PIn.PString(tempT.Rows[i][2].ToString())=="Other")
          row[1]="Other";
				if(tempT.Rows[i][3].ToString().Equals("H"))
				  row[2]="Holding";//Claim Status
				else if(tempT.Rows[i][3].ToString().Equals("W"))
					row[2]="WaitQ";//Claim Status, added SPK 7/15/04
				else
				  row[2]="Unsent";//Claim Status
				row[3]=tempT.Rows[i][4];//Patient name
				row[4]=tempT.Rows[i][5];//Ins Carrier
				row[5]=PIn.PDouble(tempT.Rows[i][6].ToString()).ToString("F");//claim fee
				report.ColTotal[5]+=PIn.PDouble(tempT.Rows[i][6].ToString());
				report.TableQ.Rows.Add(row);
      }
			FormQuery2.ResetGrid();//this is a method in FormQuery2;	
			report.Title="Claims Not Sent";
			report.SubTitle.Add(((Pref)PrefC.HList["PracticeTitle"]).ValueString);
			if(radioRange.Checked==true){
				report.SubTitle.Add(date1.SelectionStart.ToString("d")+" - "+date2.SelectionStart.ToString("d"));
			}
			else{
				report.SubTitle.Add(date1.SelectionStart.ToString("d"));
			}
			report.ColPos[0]=20;
			report.ColPos[1]=145;
			report.ColPos[2]=270;
			report.ColPos[3]=395;
			report.ColPos[4]=520;
	    report.ColPos[5]=645;
			report.ColPos[6]=770;
			report.ColCaption[0]="Date";
			report.ColCaption[1]="Type";
			report.ColCaption[2]="Claim Status";
			report.ColCaption[3]="Patient Name";
			report.ColCaption[4]="Insurance Carrier";
			report.ColCaption[5]="Amount";
			report.ColAlign[5]=HorizontalAlignment.Right;
			FormQuery2.ShowDialog();
			DialogResult=DialogResult.OK;
		}
		private void radioSingle_CheckedChanged(object sender, System.EventArgs e) {
			if(radioSingle.Checked==true){
				date2.Visible=false;
				labelTO.Visible=false;
			}
			else{
				date2.Visible=true;
				labelTO.Visible=true;
			}	
		}
	}
}
