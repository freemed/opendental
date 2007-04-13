using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{
///<summary></summary>
	public class FormScheduleDay : System.Windows.Forms.Form{
		private OpenDental.UI.Button butDefault;
		private System.Windows.Forms.ListBox listTimeBlocks;
		private OpenDental.UI.Button butAddTime;
		private OpenDental.UI.Button butCloseOffice;
		private OpenDental.UI.Button butHoliday;
		private System.Windows.Forms.Label label1;
    private ArrayList ALdefaults;
		private System.Windows.Forms.Label labelDefault;
		private OpenDental.UI.Button butClose;
		private System.ComponentModel.Container components = null;
		private Schedule[] SchedListDay;
		private DateTime SchedCurDate;
		private ScheduleType SchedType;
		private int ProvNum;

		///<summary></summary>
		public FormScheduleDay(DateTime schedCurDate,ScheduleType schedType,int provNum){
			InitializeComponent();
			SchedCurDate=schedCurDate;
			SchedType=schedType;
			ProvNum=provNum;
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormScheduleDay));
			this.listTimeBlocks = new System.Windows.Forms.ListBox();
			this.butClose = new OpenDental.UI.Button();
			this.butDefault = new OpenDental.UI.Button();
			this.butAddTime = new OpenDental.UI.Button();
			this.butCloseOffice = new OpenDental.UI.Button();
			this.butHoliday = new OpenDental.UI.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.labelDefault = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// listTimeBlocks
			// 
			this.listTimeBlocks.Location = new System.Drawing.Point(14,51);
			this.listTimeBlocks.Name = "listTimeBlocks";
			this.listTimeBlocks.Size = new System.Drawing.Size(192,186);
			this.listTimeBlocks.TabIndex = 0;
			this.listTimeBlocks.DoubleClick += new System.EventHandler(this.listTimeBlocks_DoubleClick);
			// 
			// butClose
			// 
			this.butClose.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.butClose.Autosize = true;
			this.butClose.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butClose.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butClose.CornerRadius = 4F;
			this.butClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butClose.Location = new System.Drawing.Point(265,214);
			this.butClose.Name = "butClose";
			this.butClose.Size = new System.Drawing.Size(75,26);
			this.butClose.TabIndex = 2;
			this.butClose.Text = "&Close";
			this.butClose.Click += new System.EventHandler(this.butClose_Click);
			// 
			// butDefault
			// 
			this.butDefault.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butDefault.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.butDefault.Autosize = true;
			this.butDefault.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butDefault.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butDefault.CornerRadius = 4F;
			this.butDefault.Location = new System.Drawing.Point(248,48);
			this.butDefault.Name = "butDefault";
			this.butDefault.Size = new System.Drawing.Size(92,26);
			this.butDefault.TabIndex = 3;
			this.butDefault.Text = "Set To &Default";
			this.butDefault.Click += new System.EventHandler(this.butDefault_Click);
			// 
			// butAddTime
			// 
			this.butAddTime.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butAddTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.butAddTime.Autosize = true;
			this.butAddTime.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butAddTime.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butAddTime.CornerRadius = 4F;
			this.butAddTime.Location = new System.Drawing.Point(244,80);
			this.butAddTime.Name = "butAddTime";
			this.butAddTime.Size = new System.Drawing.Size(96,26);
			this.butAddTime.TabIndex = 4;
			this.butAddTime.Text = "&Add Time Block";
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
			this.butCloseOffice.Location = new System.Drawing.Point(248,112);
			this.butCloseOffice.Name = "butCloseOffice";
			this.butCloseOffice.Size = new System.Drawing.Size(92,26);
			this.butCloseOffice.TabIndex = 5;
			this.butCloseOffice.Text = "C&lose Office";
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
			this.butHoliday.Location = new System.Drawing.Point(248,144);
			this.butHoliday.Name = "butHoliday";
			this.butHoliday.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.butHoliday.Size = new System.Drawing.Size(92,26);
			this.butHoliday.TabIndex = 7;
			this.butHoliday.Text = "Set as &Holiday";
			this.butHoliday.Click += new System.EventHandler(this.butHoliday_Click);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(12,30);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(100,16);
			this.label1.TabIndex = 8;
			this.label1.Text = "Time Blocks:";
			// 
			// labelDefault
			// 
			this.labelDefault.Location = new System.Drawing.Point(130,30);
			this.labelDefault.Name = "labelDefault";
			this.labelDefault.Size = new System.Drawing.Size(66,18);
			this.labelDefault.TabIndex = 9;
			this.labelDefault.Text = "(default)";
			this.labelDefault.Visible = false;
			// 
			// FormScheduleDay
			// 
			this.AcceptButton = this.butClose;
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.CancelButton = this.butClose;
			this.ClientSize = new System.Drawing.Size(352,252);
			this.Controls.Add(this.labelDefault);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.butHoliday);
			this.Controls.Add(this.butCloseOffice);
			this.Controls.Add(this.butAddTime);
			this.Controls.Add(this.butDefault);
			this.Controls.Add(this.butClose);
			this.Controls.Add(this.listTimeBlocks);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormScheduleDay";
			this.ShowInTaskbar = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Edit Day";
			this.Load += new System.EventHandler(this.FormScheduleDay_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormScheduleDay_Load(object sender, System.EventArgs e) {
      FillList();      		
		}

    private void FillList(){
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
      } 
    }

		private void butDefault_Click(object sender, System.EventArgs e) {
		  Schedules.SetAllDefault(SchedCurDate,SchedType,ProvNum);
			FillList();
		}

		private void butCloseOffice_Click(object sender, System.EventArgs e) {
      if(SchedListDay.Length==1 
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
      FillList();
		}

		private void butHoliday_Click(object sender, System.EventArgs e) {
			if(SchedListDay.Length==1 
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
      FillList();		
		}

		private void butAddTime_Click(object sender, System.EventArgs e) {
			Schedules.ConvertFromDefault(SchedCurDate,SchedType,ProvNum);
      Schedule SchedCur=new Schedule();
      SchedCur.SchedDate=SchedCurDate;
      SchedCur.Status=SchedStatus.Open;
			SchedCur.SchedType=SchedType;
			SchedCur.ProvNum=ProvNum;
		  FormScheduleBlockEdit FormSB=new FormScheduleBlockEdit(SchedCur);
      FormSB.IsNew=true;
      FormSB.ShowDialog();
      labelDefault.Visible=false; 
      FillList();
		}

		private void listTimeBlocks_DoubleClick(object sender, System.EventArgs e) {
			if(listTimeBlocks.SelectedIndex==-1){
				return;
			}
			int clickedIndex=listTimeBlocks.SelectedIndex;
      if(Schedules.ConvertFromDefault(SchedCurDate,SchedType,ProvNum)){
				FillList();
			}
      Schedule SchedCur=SchedListDay[clickedIndex];
			FormScheduleBlockEdit FormSB=new FormScheduleBlockEdit(SchedCur);
      FormSB.ShowDialog();
      FillList();
    }

		private void butClose_Click(object sender, System.EventArgs e) {
			Close();
		}

	}
}







