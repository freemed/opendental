using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace OpenDental{
///<summary></summary>
	public class FormRpPrintPreview : System.Windows.Forms.Form{
		///<summary></summary>
		public System.Windows.Forms.PrintPreviewControl printPreviewControl2;
		private OpenDental.UI.Button button1;
		private System.ComponentModel.Container components = null;

		///<summary></summary>
		public FormRpPrintPreview(){
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

		private void InitializeComponent(){
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormRpPrintPreview));
			this.printPreviewControl2 = new System.Windows.Forms.PrintPreviewControl();
			this.button1 = new OpenDental.UI.Button();
			this.SuspendLayout();
			// 
			// printPreviewControl2
			// 
			this.printPreviewControl2.AutoZoom = false;
			this.printPreviewControl2.Location = new System.Drawing.Point(0,0);
			this.printPreviewControl2.Name = "printPreviewControl2";
			this.printPreviewControl2.Size = new System.Drawing.Size(842,538);
			this.printPreviewControl2.TabIndex = 7;
			this.printPreviewControl2.Zoom = 1;
			// 
			// button1
			// 
			this.button1.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.button1.Autosize = true;
			this.button1.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.button1.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.button1.CornerRadius = 4F;
			this.button1.Location = new System.Drawing.Point(323,709);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(75,23);
			this.button1.TabIndex = 8;
			this.button1.Text = "next page";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// FormRpPrintPreview
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.AutoScroll = true;
			this.ClientSize = new System.Drawing.Size(842,746);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.printPreviewControl2);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "FormRpPrintPreview";
			this.ShowInTaskbar = false;
			this.Text = "FormRpPrintPreview";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.Load += new System.EventHandler(this.FormRpPrintPreview_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void button1_Click(object sender, System.EventArgs e) {
			printPreviewControl2.StartPage++;
		}

		private void FormRpPrintPreview_Load(object sender, System.EventArgs e) {
			button1.Location=new Point(this.ClientRectangle.Width-100,this.ClientRectangle.Height-30);
			printPreviewControl2.Height=this.ClientRectangle.Height-40;
			printPreviewControl2.Width=this.ClientRectangle.Width;
			printPreviewControl2.Zoom=(double)printPreviewControl2.ClientSize.Height
				/1100;
		}


	}
}
