using System;
using System.Diagnostics;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{
///<summary></summary>
	public class FormSchedPractice : System.Windows.Forms.Form{
		private System.ComponentModel.Container components = null;
		private OpenDental.UI.Button butClose;
    Color ClosedColor;//will be Def "Closed Practice" Color
    Color HolidayColor;//will be Def "Holiday" Color
		private OpenDental.ContrCalendar cal;
    Color OpenColor; //will be Def "Open" Color
		private Schedule[] SchedListMonth;
		private System.Windows.Forms.ListBox listProv;
		private System.Windows.Forms.Label labelProv;
		///<summary></summary>
		private ScheduleType SchedType;
		private int ProvNum;
		//private User user;

		///<summary></summary>
		public FormSchedPractice(ScheduleType schedType){
			InitializeComponent();
			SchedType=schedType;
			Lan.F(this);
		}

		///<summary></summary>
		protected override void Dispose( bool disposing ){
			if(disposing){
				if(components != null){
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSchedPractice));
			this.butClose = new OpenDental.UI.Button();
			this.cal = new OpenDental.ContrCalendar();
			this.listProv = new System.Windows.Forms.ListBox();
			this.labelProv = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// butClose
			// 
			this.butClose.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butClose.Autosize = true;
			this.butClose.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butClose.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butClose.CornerRadius = 4F;
			this.butClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butClose.Location = new System.Drawing.Point(818,644);
			this.butClose.Name = "butClose";
			this.butClose.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.butClose.Size = new System.Drawing.Size(75,26);
			this.butClose.TabIndex = 1;
			this.butClose.Text = "&Close";
			this.butClose.Click += new System.EventHandler(this.butClose_Click);
			// 
			// cal
			// 
			this.cal.BackColor = System.Drawing.SystemColors.Control;
			this.cal.Location = new System.Drawing.Point(128,6);
			this.cal.Name = "cal";
			this.cal.SelectedDate = new System.DateTime(2007,4,2,0,0,0,0);
			this.cal.Size = new System.Drawing.Size(764,632);
			this.cal.TabIndex = 2;
			this.cal.DoubleClick += new System.EventHandler(this.cal_DoubleClick);
			this.cal.ChangeMonth += new OpenDental.ContrCalendar.EventHandler(this.cal_ChangeMonth);
			// 
			// listProv
			// 
			this.listProv.Location = new System.Drawing.Point(4,38);
			this.listProv.Name = "listProv";
			this.listProv.Size = new System.Drawing.Size(120,303);
			this.listProv.TabIndex = 21;
			this.listProv.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listProv_MouseDown);
			// 
			// labelProv
			// 
			this.labelProv.Location = new System.Drawing.Point(4,12);
			this.labelProv.Name = "labelProv";
			this.labelProv.Size = new System.Drawing.Size(116,23);
			this.labelProv.TabIndex = 20;
			this.labelProv.Text = "Providers";
			this.labelProv.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// FormSchedPractice
			// 
			this.AcceptButton = this.butClose;
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.CancelButton = this.butClose;
			this.ClientSize = new System.Drawing.Size(900,674);
			this.Controls.Add(this.listProv);
			this.Controls.Add(this.labelProv);
			this.Controls.Add(this.cal);
			this.Controls.Add(this.butClose);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormSchedPractice";
			this.ShowInTaskbar = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Practice Schedule";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.FormSchedPractice_Closing);
			this.Load += new System.EventHandler(this.FormSchedPractice_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormSchedPractice_Load(object sender, System.EventArgs e) {
			/*if(PermissionsOld.AuthorizationRequired("Practice Schedule")){
				user=Users.Authenticate("Practice Schedule");
				if(!UserPermissions.IsAuthorized("Practice Schedule",user)){
					MsgBox.Show(this,"You do not have permission for this feature");
					DialogResult=DialogResult.Cancel;
					return;
				}	
			}*/
			if(SchedType==ScheduleType.Practice){
				this.Text=Lan.g(this,"Practice Schedule");
				labelProv.Visible=false;
				listProv.Visible=false;
			}
			else if(SchedType==ScheduleType.Provider){
				this.Text=Lan.g(this,"Provider Schedules");
				listProv.Items.Clear();	
				for(int i=0;i<Providers.List.Length;i++){
					listProv.Items.Add(Providers.List[i].Abbr);
				}
				listProv.SelectedIndex=0;
			}
      SchedDefaults.Refresh();
			OpenColor=DefB.Long[(int)DefCat.AppointmentColors][0].ItemColor;
			ClosedColor=DefB.Long[(int)DefCat.AppointmentColors][1].ItemColor;
			HolidayColor=DefB.Long[(int)DefCat.AppointmentColors][4].ItemColor;
			cal.SelectedDate=DateTime.Today;
      GetScheduleData();
			cal.Invalidate();
		}

		private void cal_ChangeMonth(object sender, System.EventArgs e) {
      GetScheduleData();
		}

		private void listProv_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e) {
			GetScheduleData();
			cal.Invalidate();
		}

    private void GetScheduleData(){
			ProvNum=0;
			if(SchedType==ScheduleType.Provider){
				ProvNum=Providers.List[listProv.SelectedIndex].ProvNum;
			}
      SchedListMonth=Schedules.RefreshMonth(cal.SelectedDate
				,SchedType,ProvNum);
			//Schedules.RefreshDay(cal.SelectedDate);//?
			SchedDefault[] schedDefForType=SchedDefaults.GetForType(SchedType,ProvNum);
			//if(SchedType==SchedType.
      cal.ResetList();
      bool HasSchedDefault;
      bool HasScheduleData; 
      for(int i=1;i<cal.List.Length;i++){//loop through each day
        HasSchedDefault=false;
        HasScheduleData=false;
        for(int j=0;j<SchedListMonth.Length;j++){
          if(cal.List[i].Date==SchedListMonth[j].SchedDate){
            if(SchedListMonth[j].Status==SchedStatus.Open){ 
              cal.AddText(i,SchedListMonth[j].StartTime.ToShortTimeString()+" - "
								+SchedListMonth[j].StopTime.ToShortTimeString());
              cal.List[i].color=OpenColor; 
              if(SchedListMonth[j].Note==""){
              }
              else{
                cal.AddText(i,SchedListMonth[j].Note);
              }              
            }
            else if(SchedListMonth[j].Status == SchedStatus.Holiday){
              if(SchedListMonth[j].Note==""){                
              }
              else{  
                cal.AddText(i,SchedListMonth[j].Note);
              }
              cal.ChangeColor(i,HolidayColor);
            }
            else{
              if(SchedListMonth[j].Note==""){            
              }
              else{ 
                cal.AddText(i,SchedListMonth[j].Note);                
              }
              cal.ChangeColor(i,ClosedColor);              
            }
            HasScheduleData=true;            
          }       
			  }
				//Debug.WriteLine(HasScheduleData);
        if(!HasScheduleData){//use defaults instead
					for(int j=0;j<schedDefForType.Length;j++){
						if((int)cal.List[i].Date.DayOfWeek==schedDefForType[j].DayOfWeek){
							cal.AddText(i,schedDefForType[j].StartTime.ToShortTimeString()+" - "
								+schedDefForType[j].StopTime.ToShortTimeString());
							HasSchedDefault=true;
              cal.ChangeColor(i,OpenColor); 
						}
					}
					if(!HasSchedDefault){
						cal.List[i].color=ClosedColor;
					} 
        }
      }//day loop
    }

		private void cal_DoubleClick(object sender, System.EventArgs e) {
			FormScheduleDay FormSD2=new FormScheduleDay(cal.SelectedDate,SchedType,ProvNum);
      FormSD2.ShowDialog();
      GetScheduleData();
			cal.Invalidate();
		}

		private void butClose_Click(object sender, System.EventArgs e) {
			Close();
		}

		private void FormSchedPractice_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
			//The daily schedules is refreshed everytime the Appointment screen is refreshed. Not with LocalData
			//SecurityLogs.MakeLogEntry("Practice Schedule","Altered Practice Schedule",user);
		}

		

		

		

		

	}
}