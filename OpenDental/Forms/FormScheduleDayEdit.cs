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
		private OpenDental.UI.Button butHoliday;
		//private ArrayList ALdefaults;
		private OpenDental.UI.Button butClose;
		private System.ComponentModel.Container components = null;
		//private Schedule[] SchedListDay;
		private DateTime SchedCurDate;
		//private ScheduleType SchedType;
		private OpenDental.UI.ODGrid gridMain;
		private Label labelDate;
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
			this.butClose = new OpenDental.UI.Button();
			this.butAddTime = new OpenDental.UI.Button();
			this.butCloseOffice = new OpenDental.UI.Button();
			this.butHoliday = new OpenDental.UI.Button();
			this.gridMain = new OpenDental.UI.ODGrid();
			this.labelDate = new System.Windows.Forms.Label();
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
			this.butClose.Location = new System.Drawing.Point(684,649);
			this.butClose.Name = "butClose";
			this.butClose.Size = new System.Drawing.Size(75,26);
			this.butClose.TabIndex = 2;
			this.butClose.Text = "&Close";
			this.butClose.Click += new System.EventHandler(this.butClose_Click);
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
			this.butAddTime.Location = new System.Drawing.Point(667,367);
			this.butAddTime.Name = "butAddTime";
			this.butAddTime.Size = new System.Drawing.Size(92,26);
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
			this.butCloseOffice.Location = new System.Drawing.Point(667,399);
			this.butCloseOffice.Name = "butCloseOffice";
			this.butCloseOffice.Size = new System.Drawing.Size(92,26);
			this.butCloseOffice.TabIndex = 5;
			this.butCloseOffice.Text = "Delete";
			this.butCloseOffice.Click += new System.EventHandler(this.butCloseOffice_Click);
			// 
			// butHoliday
			// 
			this.butHoliday.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butHoliday.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.butHoliday.Autosize = true;
			this.butHoliday.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butHoliday.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butHoliday.CornerRadius = 4F;
			this.butHoliday.Location = new System.Drawing.Point(667,431);
			this.butHoliday.Name = "butHoliday";
			this.butHoliday.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.butHoliday.Size = new System.Drawing.Size(92,26);
			this.butHoliday.TabIndex = 7;
			this.butHoliday.Text = "Set as &Holiday";
			this.butHoliday.Click += new System.EventHandler(this.butHoliday_Click);
			// 
			// gridMain
			// 
			this.gridMain.HScrollVisible = false;
			this.gridMain.Location = new System.Drawing.Point(12,38);
			this.gridMain.Name = "gridMain";
			this.gridMain.ScrollValue = 0;
			this.gridMain.Size = new System.Drawing.Size(525,635);
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
			this.labelDate.Text = "label1";
			// 
			// FormScheduleDayEdit
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.ClientSize = new System.Drawing.Size(771,687);
			this.Controls.Add(this.labelDate);
			this.Controls.Add(this.gridMain);
			this.Controls.Add(this.butHoliday);
			this.Controls.Add(this.butCloseOffice);
			this.Controls.Add(this.butAddTime);
			this.Controls.Add(this.butClose);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormScheduleDayEdit";
			this.ShowInTaskbar = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Edit Day";
			this.Load += new System.EventHandler(this.FormScheduleDay_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormScheduleDay_Load(object sender, System.EventArgs e) {
			labelDate.Text=SchedCurDate.ToString("dddd")+" "+SchedCurDate.ToShortDateString();
      FillGrid();      		
		}

    private void FillGrid(){
			//SchedList=Schedules.GetPeriod
			gridMain.BeginUpdate();
			gridMain.Columns.Clear();
			ODGridColumn col=new ODGridColumn(Lan.g("TableSchedDay",""),100);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableSchedDay",""),100);
			gridMain.Columns.Add(col);
			 
			gridMain.Rows.Clear();
			ODGridRow row;
			for(int i=0;i<0;i++){
				row=new ODGridRow();
				row.Cells.Add("");
				row.Cells.Add("");
			  
				gridMain.Rows.Add(row);
			}
			gridMain.EndUpdate();
			/*
			Schedule[] SchedListAll=Schedules.RefreshDay(SchedCurDate); 
      SchedListDay=Schedules.GetForType(SchedListAll,SchedType,ProvNum);
      SchedDefaults.Refresh();
			SchedDefault[] schedDefForType=SchedDefaults.GetForType(SchedType,ProvNum);
      listTimeBlocks.Items.Clear(); 
      ALdefaults=new ArrayList();
      labelDefault.Visible=false; 
      if(SchedListDay.Length==0){
				//show defaults instead of user-entered list
        for(int i=0;i<schedDefForType.Length;i++){
          if((int)SchedCurDate.DayOfWeek==schedDefForType[i].DayOfWeek){
            ALdefaults.Add(schedDefForType[i]); 
            listTimeBlocks.Items.Add(schedDefForType[i].StartTime.ToShortTimeString()+" - "
              +schedDefForType[i].StopTime.ToShortTimeString());
          }  
        }
        labelDefault.Visible=true;     
      }
      else{//show the list of user-entered schedule items 
        if(SchedListDay.Length==1 && SchedListDay[0].Status==SchedStatus.Closed){
          listTimeBlocks.Items.Add("Office Closed "+SchedListDay[0].Note);
        }
        else if(SchedListDay.Length==1 && SchedListDay[0].Status==SchedStatus.Holiday){
          listTimeBlocks.Items.Add("Holiday: "+SchedListDay[0].Note);
        } 
        else{  
					for(int i=0;i<SchedListDay.Length;i++){
						listTimeBlocks.Items.Add(SchedListDay[i].StartTime.ToShortTimeString()+" - "
							+SchedListDay[i].StopTime.ToShortTimeString());
					}
        } 
      } */

    }

		private void butDefault_Click(object sender, System.EventArgs e) {
		  //Schedules.SetAllDefault(SchedCurDate,SchedType,ProvNum);
			//FillList();
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

		private void butClose_Click(object sender, System.EventArgs e) {
			Close();
		}

	}
}







