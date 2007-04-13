using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{
///<summary></summary>
	public class FormSchedDefaultBlockEdit : System.Windows.Forms.Form{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.ListBox listDay;
		private System.Windows.Forms.TextBox textStart;
		private System.Windows.Forms.TextBox textStop;
		private OpenDental.UI.Button butOK;
		private OpenDental.UI.Button butCancel;
		private System.ComponentModel.Container components = null;
		///<summary></summary>
		public bool IsNew;
		private OpenDental.UI.Button butDelete;
		private System.Windows.Forms.ListBox listType;
		private System.Windows.Forms.Label labelType;
		private System.Windows.Forms.ListBox listOp;
		private System.Windows.Forms.Label labelOp;
		private SchedDefault SchedDefaultCur;

		///<summary></summary>
		public FormSchedDefaultBlockEdit(SchedDefault schedDefaultCur){
			InitializeComponent();
			Lan.F(this);
			SchedDefaultCur=schedDefaultCur;
			listDay.Items.Clear();
			listDay.Items.AddRange(CultureInfo.CurrentCulture.DateTimeFormat.DayNames);
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSchedDefaultBlockEdit));
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.listDay = new System.Windows.Forms.ListBox();
			this.textStart = new System.Windows.Forms.TextBox();
			this.textStop = new System.Windows.Forms.TextBox();
			this.butOK = new OpenDental.UI.Button();
			this.butCancel = new OpenDental.UI.Button();
			this.butDelete = new OpenDental.UI.Button();
			this.listType = new System.Windows.Forms.ListBox();
			this.labelType = new System.Windows.Forms.Label();
			this.listOp = new System.Windows.Forms.ListBox();
			this.labelOp = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8,12);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(98,16);
			this.label1.TabIndex = 0;
			this.label1.Text = "Start Time";
			this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(6,36);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(100,16);
			this.label2.TabIndex = 1;
			this.label2.Text = "Stop Time";
			this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(22,72);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(84,16);
			this.label3.TabIndex = 2;
			this.label3.Text = "Day";
			this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// listDay
			// 
			this.listDay.Location = new System.Drawing.Point(108,72);
			this.listDay.Name = "listDay";
			this.listDay.Size = new System.Drawing.Size(102,95);
			this.listDay.TabIndex = 2;
			// 
			// textStart
			// 
			this.textStart.Location = new System.Drawing.Point(108,8);
			this.textStart.Name = "textStart";
			this.textStart.Size = new System.Drawing.Size(100,20);
			this.textStart.TabIndex = 0;
			// 
			// textStop
			// 
			this.textStop.Location = new System.Drawing.Point(108,34);
			this.textStop.Name = "textStop";
			this.textStop.Size = new System.Drawing.Size(100,20);
			this.textStop.TabIndex = 1;
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.CornerRadius = 4F;
			this.butOK.Location = new System.Drawing.Point(309,194);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75,26);
			this.butOK.TabIndex = 3;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
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
			this.butCancel.Location = new System.Drawing.Point(398,194);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75,26);
			this.butCancel.TabIndex = 5;
			this.butCancel.Text = "&Cancel";
			// 
			// butDelete
			// 
			this.butDelete.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.butDelete.Autosize = true;
			this.butDelete.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butDelete.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butDelete.CornerRadius = 4F;
			this.butDelete.Image = global::OpenDental.Properties.Resources.deleteX;
			this.butDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butDelete.Location = new System.Drawing.Point(13,194);
			this.butDelete.Name = "butDelete";
			this.butDelete.Size = new System.Drawing.Size(86,26);
			this.butDelete.TabIndex = 7;
			this.butDelete.Text = "&Delete";
			this.butDelete.Click += new System.EventHandler(this.butDelete_Click);
			// 
			// listType
			// 
			this.listType.Location = new System.Drawing.Point(227,34);
			this.listType.Name = "listType";
			this.listType.Size = new System.Drawing.Size(115,134);
			this.listType.TabIndex = 9;
			// 
			// labelType
			// 
			this.labelType.Location = new System.Drawing.Point(227,14);
			this.labelType.Name = "labelType";
			this.labelType.Size = new System.Drawing.Size(127,16);
			this.labelType.TabIndex = 8;
			this.labelType.Text = "Blockout Type";
			this.labelType.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// listOp
			// 
			this.listOp.Location = new System.Drawing.Point(357,35);
			this.listOp.Name = "listOp";
			this.listOp.Size = new System.Drawing.Size(115,134);
			this.listOp.TabIndex = 11;
			// 
			// labelOp
			// 
			this.labelOp.Location = new System.Drawing.Point(357,15);
			this.labelOp.Name = "labelOp";
			this.labelOp.Size = new System.Drawing.Size(128,16);
			this.labelOp.TabIndex = 10;
			this.labelOp.Text = "Operatory";
			this.labelOp.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// FormSchedDefaultBlockEdit
			// 
			this.AcceptButton = this.butOK;
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.CancelButton = this.butCancel;
			this.ClientSize = new System.Drawing.Size(486,231);
			this.Controls.Add(this.listOp);
			this.Controls.Add(this.labelOp);
			this.Controls.Add(this.listType);
			this.Controls.Add(this.labelType);
			this.Controls.Add(this.butDelete);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.textStop);
			this.Controls.Add(this.textStart);
			this.Controls.Add(this.listDay);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormSchedDefaultBlockEdit";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Edit Block";
			this.Load += new System.EventHandler(this.FormBlockEdit_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		private void FormBlockEdit_Load(object sender, System.EventArgs e) {
			textStart.Text=SchedDefaultCur.StartTime.ToShortTimeString();
			textStop.Text=SchedDefaultCur.StopTime.ToShortTimeString();
			listDay.SelectedIndex=SchedDefaultCur.DayOfWeek;
			if(SchedDefaultCur.SchedType==ScheduleType.Blockout){
				listType.Items.Clear();
				for(int i=0;i<DefB.Short[(int)DefCat.BlockoutTypes].Length;i++){
					listType.Items.Add(DefB.Short[(int)DefCat.BlockoutTypes][i].ItemName);
					if(SchedDefaultCur.BlockoutType==DefB.Short[(int)DefCat.BlockoutTypes][i].DefNum){
						listType.SelectedIndex=i;
					}
				}
				if(listType.Items.Count==0){
					MsgBox.Show(this,"You must setup blockout types first using the button at the left.");
					DialogResult=DialogResult.Cancel;
					return;
				}
				if(listType.SelectedIndex==-1){
					listType.SelectedIndex=0;
				}
				listOp.Items.Clear();
				listOp.Items.Add(Lan.g(this,"All Ops"));
				listOp.SelectedIndex=0;
				for(int i=0;i<Operatories.ListShort.Length;i++){
					listOp.Items.Add(Operatories.ListShort[i].Abbrev);
					if(SchedDefaultCur.Op==Operatories.ListShort[i].OperatoryNum){
						listOp.SelectedIndex=i+1;
					}
				}
			}
			else{
				labelType.Visible=false;
				listType.Visible=false;
				labelOp.Visible=false;
				listOp.Visible=false;
			}
		}
		
		private void butDelete_Click(object sender, System.EventArgs e) {
			if(MessageBox.Show(Lan.g(this,"Delete?"),"",MessageBoxButtons.OKCancel)!=DialogResult.OK){
				return;
			}
			if(IsNew){
				DialogResult=DialogResult.Cancel;
			}
			else{
				SchedDefaults.Delete(SchedDefaultCur);
				DialogResult=DialogResult.OK;
			}
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			try{
				SchedDefaultCur.StartTime=DateTime.Parse(textStart.Text);
				SchedDefaultCur.StopTime=DateTime.Parse(textStop.Text);
			}
			catch{
				MessageBox.Show(Lan.g(this,"Incorrect time format"));
				return;
			}
			SchedDefaultCur.DayOfWeek=listDay.SelectedIndex;
			if(SchedDefaultCur.SchedType==ScheduleType.Blockout){
				SchedDefaultCur.BlockoutType
					=DefB.Short[(int)DefCat.BlockoutTypes][listType.SelectedIndex].DefNum;
				if(listOp.SelectedIndex==0){
					SchedDefaultCur.Op=0;
				}
				else{
					SchedDefaultCur.Op=Operatories.ListShort[listOp.SelectedIndex-1].OperatoryNum;
				}
			}
			try{
				SchedDefaults.InsertOrUpdate(SchedDefaultCur,IsNew);
			}
			catch(Exception ex){
				MessageBox.Show(ex.Message);
				return;
			}
			DialogResult=DialogResult.OK;
		}

	}
}













