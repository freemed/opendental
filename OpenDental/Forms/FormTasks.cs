using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using OpenDental.UI;
using OpenDentBusiness;

namespace OpenDental{
	/// <summary></summary>
	public class FormTasks:System.Windows.Forms.Form {
		//private System.ComponentModel.IContainer components;
		///<summary>After closing, if this is not zero, then it will jump to the object specified in GotoKeyNum.</summary>
		public TaskObjectType GotoType;
		private UserControlTasks userControlTasks1;
		private Timer timer1;
		private IContainer components;
		///<summary>After closing, if this is not zero, then it will jump to the specified patient.</summary>
		public long GotoKeyNum;

	
		///<summary></summary>
		public FormTasks()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			//Lan.F(this);
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				//if(components != null)
				//{
				//	components.Dispose();
				//}
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormTasks));
			this.userControlTasks1 = new OpenDental.UserControlTasks();
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			this.SuspendLayout();
			// 
			// userControlTasks1
			// 
			this.userControlTasks1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.userControlTasks1.Location = new System.Drawing.Point(0,0);
			this.userControlTasks1.Name = "userControlTasks1";
			this.userControlTasks1.Size = new System.Drawing.Size(885,671);
			this.userControlTasks1.TabIndex = 0;
			this.userControlTasks1.GoToChanged += new System.EventHandler(this.userControlTasks1_GoToChanged);
			// 
			// timer1
			// 
			this.timer1.Enabled = true;
			this.timer1.Interval = 60000;
			this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
			// 
			// FormTasks
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.ClientSize = new System.Drawing.Size(885,671);
			this.Controls.Add(this.userControlTasks1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "FormTasks";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Tasks";
			this.Load += new System.EventHandler(this.FormTasks_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormTasks_Load(object sender,EventArgs e) {
			userControlTasks1.InitializeOnStartup();
		}
		
		private void userControlTasks1_GoToChanged(object sender,EventArgs e) {
			GotoType=userControlTasks1.GotoType;
			GotoKeyNum=userControlTasks1.GotoKeyNum;
			DialogResult=DialogResult.OK;
		}

		private void timer1_Tick(object sender,EventArgs e) {
			userControlTasks1.RefreshTasks();
			//this quick and dirty refresh is not as intelligent as the one used when tasks are docked.
			//Sound notification of new task is controlled from main form completely
			//independently of this visual refresh.
		}

		

		

		

		

		
	



	}
}





















