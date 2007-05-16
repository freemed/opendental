using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{
///<summary></summary>
	public class FormScheduleBlockEdit : System.Windows.Forms.Form	{
		private System.ComponentModel.Container components = null;
		private OpenDental.UI.Button butCancel;
		private OpenDental.UI.Button butOK;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox textNote;
		private System.Windows.Forms.Label label4;
		private OpenDental.UI.Button butDelete;
		///<summary></summary>
    public bool IsNew;
		private System.Windows.Forms.ListBox listOp;
		private System.Windows.Forms.Label labelOp;
		private System.Windows.Forms.ListBox listType;
		private System.Windows.Forms.Label labelType;
		private ComboBox comboStart;
		private ComboBox comboStop;
		private Schedule SchedCur;

		///<summary></summary>
		public FormScheduleBlockEdit(Schedule schedCur){
			InitializeComponent();
			SchedCur=schedCur;
			Lan.F(this);
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
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormScheduleBlockEdit));
			this.butCancel = new OpenDental.UI.Button();
			this.butOK = new OpenDental.UI.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.textNote = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.butDelete = new OpenDental.UI.Button();
			this.listOp = new System.Windows.Forms.ListBox();
			this.labelOp = new System.Windows.Forms.Label();
			this.listType = new System.Windows.Forms.ListBox();
			this.labelType = new System.Windows.Forms.Label();
			this.comboStart = new System.Windows.Forms.ComboBox();
			this.comboStop = new System.Windows.Forms.ComboBox();
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
			this.butCancel.Location = new System.Drawing.Point(483,243);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75,26);
			this.butCancel.TabIndex = 14;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.CornerRadius = 4F;
			this.butOK.Location = new System.Drawing.Point(483,209);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75,26);
			this.butOK.TabIndex = 12;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(5,40);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(68,16);
			this.label2.TabIndex = 9;
			this.label2.Text = "Stop Time";
			this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(5,14);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(68,16);
			this.label1.TabIndex = 7;
			this.label1.Text = "Start Time";
			this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textNote
			// 
			this.textNote.Location = new System.Drawing.Point(75,64);
			this.textNote.Multiline = true;
			this.textNote.Name = "textNote";
			this.textNote.Size = new System.Drawing.Size(220,113);
			this.textNote.TabIndex = 15;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(9,65);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(64,16);
			this.label4.TabIndex = 16;
			this.label4.Text = "Note";
			this.label4.TextAlign = System.Drawing.ContentAlignment.TopRight;
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
			this.butDelete.Location = new System.Drawing.Point(18,243);
			this.butDelete.Name = "butDelete";
			this.butDelete.Size = new System.Drawing.Size(84,26);
			this.butDelete.TabIndex = 17;
			this.butDelete.Text = "&Delete";
			this.butDelete.Click += new System.EventHandler(this.butDelete_Click);
			// 
			// listOp
			// 
			this.listOp.Location = new System.Drawing.Point(443,43);
			this.listOp.Name = "listOp";
			this.listOp.Size = new System.Drawing.Size(115,134);
			this.listOp.TabIndex = 21;
			// 
			// labelOp
			// 
			this.labelOp.Location = new System.Drawing.Point(443,23);
			this.labelOp.Name = "labelOp";
			this.labelOp.Size = new System.Drawing.Size(128,16);
			this.labelOp.TabIndex = 20;
			this.labelOp.Text = "Operatory";
			this.labelOp.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// listType
			// 
			this.listType.Location = new System.Drawing.Point(313,43);
			this.listType.Name = "listType";
			this.listType.Size = new System.Drawing.Size(115,134);
			this.listType.TabIndex = 19;
			// 
			// labelType
			// 
			this.labelType.Location = new System.Drawing.Point(313,23);
			this.labelType.Name = "labelType";
			this.labelType.Size = new System.Drawing.Size(127,16);
			this.labelType.TabIndex = 18;
			this.labelType.Text = "Blockout Type";
			this.labelType.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// comboStart
			// 
			this.comboStart.FormattingEnabled = true;
			this.comboStart.Location = new System.Drawing.Point(75,11);
			this.comboStart.MaxDropDownItems = 48;
			this.comboStart.Name = "comboStart";
			this.comboStart.Size = new System.Drawing.Size(120,21);
			this.comboStart.TabIndex = 22;
			// 
			// comboStop
			// 
			this.comboStop.FormattingEnabled = true;
			this.comboStop.Location = new System.Drawing.Point(75,37);
			this.comboStop.MaxDropDownItems = 48;
			this.comboStop.Name = "comboStop";
			this.comboStop.Size = new System.Drawing.Size(120,21);
			this.comboStop.TabIndex = 23;
			// 
			// FormScheduleBlockEdit
			// 
			this.AcceptButton = this.butOK;
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.CancelButton = this.butCancel;
			this.ClientSize = new System.Drawing.Size(582,287);
			this.Controls.Add(this.comboStop);
			this.Controls.Add(this.comboStart);
			this.Controls.Add(this.listOp);
			this.Controls.Add(this.labelOp);
			this.Controls.Add(this.listType);
			this.Controls.Add(this.labelType);
			this.Controls.Add(this.butDelete);
			this.Controls.Add(this.textNote);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormScheduleBlockEdit";
			this.ShowInTaskbar = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Edit Blockout";
			this.Load += new System.EventHandler(this.FormScheduleDayEdit_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		private void FormScheduleDayEdit_Load(object sender, System.EventArgs e) {
			listType.Items.Clear();
			for(int i=0;i<DefB.Short[(int)DefCat.BlockoutTypes].Length;i++){
				listType.Items.Add(DefB.Short[(int)DefCat.BlockoutTypes][i].ItemName);
				if(SchedCur.BlockoutType==DefB.Short[(int)DefCat.BlockoutTypes][i].DefNum){
					listType.SelectedIndex=i;
				}
			}
			if(listType.Items.Count==0){
				MsgBox.Show(this,"You must setup blockout types first in Setup-Definitions.");
				DialogResult=DialogResult.Cancel;
				return;
			}
			if(listType.SelectedIndex==-1){
				listType.SelectedIndex=0;
			}
			listOp.Items.Clear();
			listOp.Items.Add(Lan.g(this,"All Ops"));
			listOp.SelectedIndex=0;
			for(int i=0;i<Operatories.ListShort.Length ;i++){
				listOp.Items.Add(Operatories.ListShort[i].Abbrev);
				if(SchedCur.Op==Operatories.ListShort[i].OperatoryNum){
					listOp.SelectedIndex=i+1;
				}
			}
			DateTime time;
			for(int i=0;i<24;i++){
				time=DateTime.Today+TimeSpan.FromHours(7)+TimeSpan.FromMinutes(30*i);
				comboStart.Items.Add(time.ToShortTimeString());
				comboStop.Items.Add(time.ToShortTimeString());
			}
      comboStart.Text=SchedCur.StartTime.ToShortTimeString();
			comboStop.Text=SchedCur.StopTime.ToShortTimeString();
			textNote.Text=SchedCur.Note;
			comboStart.Select(); 
		}

		private void butDelete_Click(object sender, System.EventArgs e) {
			if(MessageBox.Show(Lan.g(this,"Delete Blockout?"),"",MessageBoxButtons.OKCancel)!=DialogResult.OK){
			  return;   
			}
      if(IsNew){
        DialogResult=DialogResult.Cancel; 
      }
      else{ 
        Schedules.Delete(SchedCur);	
      }
      DialogResult=DialogResult.Cancel;
		}

    private void butOK_Click(object sender, System.EventArgs e) { 
		  try{
				SchedCur.StartTime=DateTime.Parse(comboStart.Text);
				SchedCur.StopTime=DateTime.Parse(comboStop.Text);
			}
			catch{
				MessageBox.Show(Lan.g(this,"Incorrect time format"));
				return;
			}
      SchedCur.Note=textNote.Text;
			SchedCur.BlockoutType=DefB.Short[(int)DefCat.BlockoutTypes][listType.SelectedIndex].DefNum;
			if(listOp.SelectedIndex==0){
				SchedCur.Op=0;
			}
			else{
				SchedCur.Op=Operatories.ListShort[listOp.SelectedIndex-1].OperatoryNum;
			}
			try{
				Schedules.InsertOrUpdate(SchedCur,IsNew);
			}
			catch(Exception ex){
				MessageBox.Show(ex.Message);
				return;
			}
			DialogResult=DialogResult.OK;		  
    }

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}   

	}
}






