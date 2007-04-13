using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{
///<summary></summary>
	public class FormSchedDefault : System.Windows.Forms.Form{
		private OpenDental.UI.Button butClose;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label7;
		private OpenDental.ContrSchedGrid contrGrid;
		private System.ComponentModel.Container components = null;
		private OpenDental.UI.Button butAdd;
		private Point mousePos;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label11;
		private bool changed;
		private System.Windows.Forms.Label labelProv;
		private System.Windows.Forms.ListBox listProv;
		private OpenDental.UI.Button butEditTypes;
		private ScheduleType SchedType;
		//private User user;

		///<summary></summary>
		public FormSchedDefault(ScheduleType schedType){
			InitializeComponent();
			Lan.F(this);
			SchedType=schedType;
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSchedDefault));
			this.butClose = new OpenDental.UI.Button();
			this.contrGrid = new OpenDental.ContrSchedGrid();
			this.label11 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.butAdd = new OpenDental.UI.Button();
			this.label8 = new System.Windows.Forms.Label();
			this.labelProv = new System.Windows.Forms.Label();
			this.listProv = new System.Windows.Forms.ListBox();
			this.butEditTypes = new OpenDental.UI.Button();
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
			this.butClose.Location = new System.Drawing.Point(807,640);
			this.butClose.Name = "butClose";
			this.butClose.Size = new System.Drawing.Size(75,26);
			this.butClose.TabIndex = 1;
			this.butClose.Text = "&Close";
			this.butClose.Click += new System.EventHandler(this.butClose_Click);
			// 
			// contrGrid
			// 
			this.contrGrid.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))),((int)(((byte)(224)))),((int)(((byte)(224)))));
			this.contrGrid.Location = new System.Drawing.Point(163,39);
			this.contrGrid.Name = "contrGrid";
			this.contrGrid.Size = new System.Drawing.Size(702,577);
			this.contrGrid.TabIndex = 3;
			this.contrGrid.DoubleClick += new System.EventHandler(this.contrGrid_DoubleClick);
			this.contrGrid.Click += new System.EventHandler(this.contrGrid_Click);
			this.contrGrid.MouseDown += new System.Windows.Forms.MouseEventHandler(this.contrGrid_MouseDown);
			// 
			// label11
			// 
			this.label11.Location = new System.Drawing.Point(295,23);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(80,14);
			this.label11.TabIndex = 4;
			this.label11.Text = "Monday";
			this.label11.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(745,23);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(80,14);
			this.label2.TabIndex = 5;
			this.label2.Text = "Saturday";
			this.label2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(655,23);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(80,14);
			this.label3.TabIndex = 6;
			this.label3.Text = "Friday";
			this.label3.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(568,23);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(80,14);
			this.label4.TabIndex = 7;
			this.label4.Text = "Thursday";
			this.label4.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(479,23);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(80,14);
			this.label5.TabIndex = 8;
			this.label5.Text = "Wednesday";
			this.label5.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(383,23);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(80,14);
			this.label6.TabIndex = 9;
			this.label6.Text = "Tuesday";
			this.label6.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(205,23);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(80,14);
			this.label7.TabIndex = 10;
			this.label7.Text = "Sunday";
			this.label7.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// butAdd
			// 
			this.butAdd.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butAdd.Autosize = true;
			this.butAdd.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butAdd.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butAdd.CornerRadius = 4F;
			this.butAdd.Image = global::OpenDental.Properties.Resources.Add;
			this.butAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butAdd.Location = new System.Drawing.Point(625,640);
			this.butAdd.Name = "butAdd";
			this.butAdd.Size = new System.Drawing.Size(106,26);
			this.butAdd.TabIndex = 15;
			this.butAdd.Text = "&Add Block";
			this.butAdd.Click += new System.EventHandler(this.butAdd_Click);
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(199,641);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(378,23);
			this.label8.TabIndex = 16;
			this.label8.Text = "(double click on a time block in the grid above to edit)";
			// 
			// labelProv
			// 
			this.labelProv.Location = new System.Drawing.Point(9,24);
			this.labelProv.Name = "labelProv";
			this.labelProv.Size = new System.Drawing.Size(134,23);
			this.labelProv.TabIndex = 17;
			this.labelProv.Text = "Providers";
			this.labelProv.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// listProv
			// 
			this.listProv.Location = new System.Drawing.Point(9,51);
			this.listProv.Name = "listProv";
			this.listProv.Size = new System.Drawing.Size(120,303);
			this.listProv.TabIndex = 18;
			this.listProv.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listProv_MouseDown);
			// 
			// butEditTypes
			// 
			this.butEditTypes.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butEditTypes.Autosize = true;
			this.butEditTypes.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butEditTypes.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butEditTypes.CornerRadius = 4F;
			this.butEditTypes.Location = new System.Drawing.Point(10,363);
			this.butEditTypes.Name = "butEditTypes";
			this.butEditTypes.Size = new System.Drawing.Size(89,26);
			this.butEditTypes.TabIndex = 19;
			this.butEditTypes.Text = "Edit Types";
			this.butEditTypes.Click += new System.EventHandler(this.butEditTypes_Click);
			// 
			// FormSchedDefault
			// 
			this.AcceptButton = this.butClose;
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.CancelButton = this.butClose;
			this.ClientSize = new System.Drawing.Size(901,690);
			this.Controls.Add(this.butEditTypes);
			this.Controls.Add(this.listProv);
			this.Controls.Add(this.labelProv);
			this.Controls.Add(this.label8);
			this.Controls.Add(this.butAdd);
			this.Controls.Add(this.butClose);
			this.Controls.Add(this.contrGrid);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label11);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormSchedDefault";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Default Schedule";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.FormSchedDefault_Closing);
			this.Load += new System.EventHandler(this.FormSchedDefault_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormSchedDefault_Load(object sender, System.EventArgs e) {
			/*if(PermissionsOld.AuthorizationRequired("Practice Default Schedule")){
				user=Users.Authenticate("Practice Default Schedule");
				if(!UserPermissions.IsAuthorized("Practice Default Schedule",user)){
					MsgBox.Show(this,"You do not have permission for this feature");
					DialogResult=DialogResult.Cancel;
					return;
				}	
			}*/
			if(SchedType==ScheduleType.Practice){
				this.Text=Lan.g(this,"Default Practice Schedule");
				labelProv.Visible=false;
				listProv.Visible=false;
				butEditTypes.Visible=false;
			}
			else if(SchedType==ScheduleType.Provider){
				this.Text=Lan.g(this,"Default Provider Schedules");
				butEditTypes.Visible=false;
				listProv.Items.Clear();	
				for(int i=0;i<Providers.List.Length;i++){
					listProv.Items.Add(Providers.List[i].Abbr);
				}
				listProv.SelectedIndex=0;
			}
			else if(SchedType==ScheduleType.Blockout){
				this.Text=Lan.g(this,"Default Blockout Schedule");
				labelProv.Visible=false;
				listProv.Visible=false;
			}
			FillGrid();
		}

		private void FillGrid(){
			SchedDefaults.Refresh();
			contrGrid.SchedType=SchedType;
			if(SchedType==ScheduleType.Provider){
				contrGrid.ProvNum=Providers.List[listProv.SelectedIndex].ProvNum;
			}
			//contrGrid.ArrayBlocks=SchedDefaults.List;
			contrGrid.BackColor=DefB.Long[(int)DefCat.AppointmentColors][1].ItemColor;
			contrGrid.Refresh();
		}

		private void listProv_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e) {
			//selected index already set
			if(listProv.SelectedIndex==-1){
				return;
			}
			FillGrid();
		}

		private void butEditTypes_Click(object sender, System.EventArgs e) {
			FormDefinitions FormD=new FormDefinitions(DefCat.BlockoutTypes);
			FormD.ShowDialog();
			FillGrid();
		}

		private void butAdd_Click(object sender, System.EventArgs e) {
			SchedDefault schedDefaultCur=new SchedDefault();
			schedDefaultCur.SchedType=SchedType;
			if(SchedType==ScheduleType.Provider){
				schedDefaultCur.ProvNum=Providers.List[listProv.SelectedIndex].ProvNum;
			}
			FormSchedDefaultBlockEdit FormBE=new FormSchedDefaultBlockEdit(schedDefaultCur);
			FormBE.IsNew=true;
			FormBE.ShowDialog();
			if(FormBE.DialogResult!=DialogResult.OK){
				return;
			}
			changed=true;
			FillGrid();
		}

		private void contrGrid_Click(object sender, System.EventArgs e) {
			
		}

		private void contrGrid_DoubleClick(object sender, System.EventArgs e) {
      int tempDay=(int)Math.Floor((double)(mousePos.X-contrGrid.NumW)/(double)contrGrid.ColW);
			if(tempDay==7)
				return;
			if(tempDay==-1){
				return;
			}
			int tempOpI
				=(int)Math.Floor((mousePos.X-contrGrid.NumW-(tempDay*contrGrid.ColW))/contrGrid.opW);
      int tempMin=(int)((mousePos.Y-Math.Floor((double)mousePos.Y/(double)contrGrid.RowH/6)*contrGrid.RowH*6)/contrGrid.RowH)*10;
			int tempHr=(int)Math.Floor((double)mousePos.Y/(double)contrGrid.RowH/(double)6);
			TimeSpan tempSpan=new TimeSpan(tempHr,tempMin,0);
			//MessageBox.Show(tempDay.ToString()+","+tempHr.ToString()+":"+tempMin.ToString());
			for(int i=0;i<SchedDefaults.List.Length;i++){
				if(SchedType==ScheduleType.Practice){//for practice
					if(SchedDefaults.List[i].SchedType!=ScheduleType.Practice){
						continue;//only use practice blocks
					}
				}
				if(SchedType==ScheduleType.Provider){//for providers
					if(SchedDefaults.List[i].SchedType!=ScheduleType.Provider){
						continue;//only use prov blocks
					}
					if(SchedDefaults.List[i].ProvNum!=Providers.List[listProv.SelectedIndex].ProvNum){
						continue;//only use blocks for this prov
					}
				}
				if(SchedType==ScheduleType.Blockout){//for blockouts
					//only use blockout blocks
					if(SchedDefaults.List[i].SchedType!=ScheduleType.Blockout){
						continue;
					}
					//if op is zero (any), then don't filter
					if(SchedDefaults.List[i].Op!=0){
						if(Operatories.GetOrder(SchedDefaults.List[i].Op)!=tempOpI){
							continue;
						}
					}
				}
				if(tempDay==SchedDefaults.List[i].DayOfWeek
					&& tempSpan >= SchedDefaults.List[i].StartTime.TimeOfDay 
					&& tempSpan < SchedDefaults.List[i].StopTime.TimeOfDay)
				{
					FormSchedDefaultBlockEdit FormBE=new FormSchedDefaultBlockEdit(SchedDefaults.List[i]);
					FormBE.ShowDialog();
					if(FormBE.DialogResult!=DialogResult.OK){
						return;
					}
					changed=true;
					FillGrid();
					return;
				}
			}
		}

		private void contrGrid_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e) {
			mousePos=new Point(e.X,e.Y);
		}

		private void butClose_Click(object sender, System.EventArgs e) {
			Close();
		}

		private void FormSchedDefault_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
			if(changed){
				DataValid.SetInvalid(InvalidTypes.Sched);
			}
			//SecurityLogs.MakeLogEntry("Practice Default Schedule","Altered Schedule Defaults",user);	
		}

		

		

	}
}
