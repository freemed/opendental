using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using OpenDentBusiness;
using OpenDental.UI;

namespace OpenDental{
///<summary></summary>
	public class FormScheduleDayEdit:System.Windows.Forms.Form {
		private OpenDental.UI.Button butAddTime;
		private OpenDental.UI.Button butCloseOffice;
		//private ArrayList ALdefaults;
		private OpenDental.UI.Button butCancel;
		private System.ComponentModel.Container components = null;
		//private Schedule[] SchedListDay;
		private DateTime SchedCurDate;
		//private ScheduleType SchedType;
		private OpenDental.UI.ODGrid gridMain;
		private Label labelDate;
		private GroupBox groupBox3;
		private Label label1;
		private ListBox listProv;
		private OpenDental.UI.Button butAll;
		private OpenDental.UI.Button butOK;
		//private int ProvNum;
		private List<Schedule> SchedList;

		///<summary></summary>
		public FormScheduleDayEdit(DateTime schedCurDate){
			InitializeComponent();
			SchedCurDate=schedCurDate;
			//SchedType=schedType;
			//ProvNum=provNum;
			Lan.F(this);
		}

		///<summary></summary>
		protected override void Dispose(bool disposing){
			if(disposing){
				if(components != null){
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormScheduleDayEdit));
			this.butCancel = new OpenDental.UI.Button();
			this.butAddTime = new OpenDental.UI.Button();
			this.butCloseOffice = new OpenDental.UI.Button();
			this.gridMain = new OpenDental.UI.ODGrid();
			this.labelDate = new System.Windows.Forms.Label();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.listProv = new System.Windows.Forms.ListBox();
			this.label1 = new System.Windows.Forms.Label();
			this.butAll = new OpenDental.UI.Button();
			this.butOK = new OpenDental.UI.Button();
			this.groupBox3.SuspendLayout();
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
			this.butCancel.Location = new System.Drawing.Point(606,647);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75,26);
			this.butCancel.TabIndex = 2;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// butAddTime
			// 
			this.butAddTime.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butAddTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.butAddTime.Autosize = true;
			this.butAddTime.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butAddTime.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butAddTime.CornerRadius = 4F;
			this.butAddTime.Image = global::OpenDental.Properties.Resources.Add;
			this.butAddTime.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butAddTime.Location = new System.Drawing.Point(14,404);
			this.butAddTime.Name = "butAddTime";
			this.butAddTime.Size = new System.Drawing.Size(80,26);
			this.butAddTime.TabIndex = 4;
			this.butAddTime.Text = "&Add";
			this.butAddTime.Click += new System.EventHandler(this.butAddTime_Click);
			// 
			// butCloseOffice
			// 
			this.butCloseOffice.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butCloseOffice.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.butCloseOffice.Autosize = true;
			this.butCloseOffice.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butCloseOffice.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butCloseOffice.CornerRadius = 4F;
			this.butCloseOffice.Image = global::OpenDental.Properties.Resources.deleteX;
			this.butCloseOffice.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butCloseOffice.Location = new System.Drawing.Point(517,503);
			this.butCloseOffice.Name = "butCloseOffice";
			this.butCloseOffice.Size = new System.Drawing.Size(80,26);
			this.butCloseOffice.TabIndex = 5;
			this.butCloseOffice.Text = "Delete";
			this.butCloseOffice.Click += new System.EventHandler(this.butDelete_Click);
			// 
			// gridMain
			// 
			this.gridMain.HScrollVisible = false;
			this.gridMain.Location = new System.Drawing.Point(12,38);
			this.gridMain.Name = "gridMain";
			this.gridMain.ScrollValue = 0;
			this.gridMain.Size = new System.Drawing.Size(461,635);
			this.gridMain.TabIndex = 8;
			this.gridMain.Title = "Edit Day";
			this.gridMain.TranslationName = null;
			// 
			// labelDate
			// 
			this.labelDate.Font = new System.Drawing.Font("Microsoft Sans Serif",8.25F,System.Drawing.FontStyle.Bold,System.Drawing.GraphicsUnit.Point,((byte)(0)));
			this.labelDate.Location = new System.Drawing.Point(10,12);
			this.labelDate.Name = "labelDate";
			this.labelDate.Size = new System.Drawing.Size(263,23);
			this.labelDate.TabIndex = 9;
			this.labelDate.Text = "labelDate";
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.butAll);
			this.groupBox3.Controls.Add(this.label1);
			this.groupBox3.Controls.Add(this.listProv);
			this.groupBox3.Controls.Add(this.butAddTime);
			this.groupBox3.Location = new System.Drawing.Point(503,38);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(178,447);
			this.groupBox3.TabIndex = 12;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Add Time Block";
			// 
			// listProv
			// 
			this.listProv.FormattingEnabled = true;
			this.listProv.Location = new System.Drawing.Point(14,67);
			this.listProv.Name = "listProv";
			this.listProv.Size = new System.Drawing.Size(102,316);
			this.listProv.TabIndex = 6;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(11,23);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(166,18);
			this.label1.TabIndex = 7;
			this.label1.Text = "Select One or More Providers";
			// 
			// butAll
			// 
			this.butAll.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butAll.Autosize = true;
			this.butAll.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butAll.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butAll.CornerRadius = 4F;
			this.butAll.Location = new System.Drawing.Point(14,42);
			this.butAll.Name = "butAll";
			this.butAll.Size = new System.Drawing.Size(53,20);
			this.butAll.TabIndex = 8;
			this.butAll.Text = "All";
			this.butAll.Click += new System.EventHandler(this.butAll_Click);
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.CornerRadius = 4F;
			this.butOK.Location = new System.Drawing.Point(605,615);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75,26);
			this.butOK.TabIndex = 13;
			this.butOK.Text = "OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// FormScheduleDayEdit
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.ClientSize = new System.Drawing.Size(718,687);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.groupBox3);
			this.Controls.Add(this.labelDate);
			this.Controls.Add(this.gridMain);
			this.Controls.Add(this.butCloseOffice);
			this.Controls.Add(this.butCancel);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormScheduleDayEdit";
			this.ShowInTaskbar = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Edit Day";
			this.Load += new System.EventHandler(this.FormScheduleDay_Load);
			this.groupBox3.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormScheduleDay_Load(object sender, System.EventArgs e) {
			labelDate.Text=SchedCurDate.ToString("dddd")+" "+SchedCurDate.ToShortDateString();
			SchedList=Schedules.RefreshDayEdit(SchedCurDate);//only does this on startup
			for(int i=0;i<Providers.List.Length;i++){
				listProv.Items.Add(Providers.List[i].Abbr);
				listProv.SetSelected(i,true);
			}
      FillGrid();      		
		}

    private void FillGrid(){
			//do not refresh from db
			gridMain.BeginUpdate();
			gridMain.Columns.Clear();
			ODGridColumn col=new ODGridColumn(Lan.g("TableSchedDay","Prov"),100);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableSchedDay","Start Time"),100);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableSchedDay","Stop Time"),100);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableSchedDay","Note"),100);
			gridMain.Columns.Add(col);
			gridMain.Rows.Clear();
			ODGridRow row;
			for(int i=0;i<SchedList.Count;i++){
				row=new ODGridRow();
				row.Cells.Add(Providers.GetAbbr(SchedList[i].ProvNum));
				row.Cells.Add(SchedList[i].StartTime.ToShortTimeString());
				row.Cells.Add(SchedList[i].StopTime.ToShortTimeString());
				row.Cells.Add(SchedList[i].Note);
				gridMain.Rows.Add(row);
			}
			gridMain.EndUpdate();
    }

		private void butAll_Click(object sender,EventArgs e) {
			for(int i=0;i<listProv.Items.Count;i++){
				listProv.SetSelected(i,true);
			}
		}

		private void butCloseOffice_Click(object sender, System.EventArgs e) {
      /*if(SchedListDay.Length==1 
				&& SchedListDay[0].Status==SchedStatus.Closed)
			{
        MessageBox.Show(Lan.g(this,"Day is already Closed."));         
        return;
      }
      if(SchedListDay.Length > 0){
				for(int i=0;i<SchedListDay.Length;i++){
					Schedules.Delete(SchedListDay[i]);
				}
      } 
			Schedule SchedCur=new Schedule();
      SchedCur.SchedDate=SchedCurDate;
      SchedCur.Status=SchedStatus.Closed;
			SchedCur.SchedType=SchedType;
			SchedCur.ProvNum=ProvNum;
		  FormScheduleBlockEdit FormSB=new FormScheduleBlockEdit(SchedCur);
      FormSB.IsNew=true;
      FormSB.ShowDialog();
      FillList();*/
		}

		private void butHoliday_Click(object sender, System.EventArgs e) {
			/*if(SchedListDay.Length==1 
				&& SchedListDay[0].Status==SchedStatus.Holiday){
        MessageBox.Show(Lan.g(this,"Day is already a Holiday."));         
        return;
      }
      Schedules.SetAllDefault(SchedCurDate,SchedType,ProvNum);
			FillList();
		  Schedule SchedCur=new Schedule();
      SchedCur.SchedDate=SchedCurDate;
      SchedCur.Status=SchedStatus.Holiday;
			SchedCur.SchedType=SchedType;
			SchedCur.ProvNum=ProvNum;
		  FormScheduleBlockEdit FormSB=new FormScheduleBlockEdit(SchedCur);
      FormSB.IsNew=true;
      FormSB.ShowDialog();
      FillList();	*/	
		}

		private void butAddTime_Click(object sender, System.EventArgs e) {
			


			/*Schedules.ConvertFromDefault(SchedCurDate,SchedType,ProvNum);
      Schedule SchedCur=new Schedule();
      SchedCur.SchedDate=SchedCurDate;
      SchedCur.Status=SchedStatus.Open;
			SchedCur.SchedType=SchedType;
			SchedCur.ProvNum=ProvNum;
		  FormScheduleBlockEdit FormSB=new FormScheduleBlockEdit(SchedCur);
      FormSB.IsNew=true;
      FormSB.ShowDialog();
      labelDefault.Visible=false; 
      FillList();*/
		}

		private void listTimeBlocks_DoubleClick(object sender, System.EventArgs e) {
			/*if(listTimeBlocks.SelectedIndex==-1){
				return;
			}
			int clickedIndex=listTimeBlocks.SelectedIndex;
      if(Schedules.ConvertFromDefault(SchedCurDate,SchedType,ProvNum)){
				FillList();
			}
      Schedule SchedCur=SchedListDay[clickedIndex];
			FormScheduleBlockEdit FormSB=new FormScheduleBlockEdit(SchedCur);
      FormSB.ShowDialog();
      FillList();*/
    }

		private void butDelete_Click(object sender,EventArgs e) {
			if(gridMain.SelectedIndices.Length==0){
				SchedList.Clear();
				return;
			}
			//loop backwards:
			for(int i=gridMain.SelectedIndices.Length-1;i>=0;i--){
				SchedList.RemoveAt(gridMain.SelectedIndices[i]);
			}
			FillGrid();
		}

		private void butOK_Click(object sender,EventArgs e) {
			//save changes to list here



			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		

		

		

	}
}







