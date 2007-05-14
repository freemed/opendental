using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{
///<summary></summary>
	public class FormScheduleEdit : System.Windows.Forms.Form	{
		private System.ComponentModel.Container components = null;
		private OpenDental.UI.Button butCancel;
		private OpenDental.UI.Button butOK;
		private System.Windows.Forms.TextBox textStop;
		private System.Windows.Forms.TextBox textStart;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox textNote;
		private System.Windows.Forms.Label label4;
		//<summary></summary>
		//public bool IsNew;
		public Schedule SchedCur;

		///<summary></summary>
		public FormScheduleEdit(){
			InitializeComponent();
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormScheduleEdit));
			this.butCancel = new OpenDental.UI.Button();
			this.butOK = new OpenDental.UI.Button();
			this.textStop = new System.Windows.Forms.TextBox();
			this.textStart = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.textNote = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
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
			this.butCancel.Location = new System.Drawing.Point(335,188);
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
			this.butOK.Location = new System.Drawing.Point(247,188);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75,26);
			this.butOK.TabIndex = 12;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// textStop
			// 
			this.textStop.Location = new System.Drawing.Point(75,36);
			this.textStop.Name = "textStop";
			this.textStop.Size = new System.Drawing.Size(102,20);
			this.textStop.TabIndex = 8;
			// 
			// textStart
			// 
			this.textStart.Location = new System.Drawing.Point(75,10);
			this.textStart.Name = "textStart";
			this.textStart.Size = new System.Drawing.Size(102,20);
			this.textStart.TabIndex = 6;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(5,38);
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
			this.textNote.Location = new System.Drawing.Point(75,62);
			this.textNote.Multiline = true;
			this.textNote.Name = "textNote";
			this.textNote.Size = new System.Drawing.Size(231,89);
			this.textNote.TabIndex = 15;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(9,63);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(64,16);
			this.label4.TabIndex = 16;
			this.label4.Text = "Note";
			this.label4.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// FormScheduleEdit
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.ClientSize = new System.Drawing.Size(430,233);
			this.Controls.Add(this.textNote);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.textStop);
			this.Controls.Add(this.textStart);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormScheduleEdit";
			this.ShowInTaskbar = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Edit Schedule";
			this.Load += new System.EventHandler(this.FormScheduleDayEdit_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		private void FormScheduleDayEdit_Load(object sender, System.EventArgs e) {
			textStart.Text=SchedCur.StartTime.ToShortTimeString();
      textStop.Text=SchedCur.StopTime.ToShortTimeString();
			textNote.Text=SchedCur.Note;
			//DateTime twelve=PIn.PDateT("12 AM");
			if(SchedCur.StartTime.TimeOfDay==PIn.PDateT("12 AM").TimeOfDay 
				&& SchedCur.StopTime.TimeOfDay==PIn.PDateT("12 AM").TimeOfDay)
			{ 
				//.SchedType==ScheduleType.Practice){
				textStop.Visible=false;
				textStart.Visible=false;
				label1.Visible=false;
				label2.Visible=false;
				textNote.Select();
			}
			else{
				textStart.Select();
			}
		}

    private void butOK_Click(object sender, System.EventArgs e) { 
      if(textStart.Visible){   
			  try{
					DateTime.Parse(textStart.Text);
					DateTime.Parse(textStop.Text);
				}
				catch{
					MessageBox.Show(Lan.g(this,"Incorrect time format"));
					return;
				}
			}
			SchedCur.StartTime=DateTime.Parse(textStart.Text);
			SchedCur.StopTime=DateTime.Parse(textStop.Text);
      SchedCur.Note=textNote.Text;
			DialogResult=DialogResult.OK;		  
    }

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}   

	}
}






