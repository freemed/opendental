using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using OpenDentBusiness;
using CodeBase;
using Tao.OpenGl;
using Tao.Platform.Windows;
using OpenDental.UI;

namespace OpenDental{
	/// <summary>
	/// Summary description for FormBasicTemplate.
	/// </summary>
	public class FormGraphics : System.Windows.Forms.Form{
		private OpenDental.UI.Button butCancel;
		private OpenDental.UI.Button butOK;
		private CheckBox checkHardwareAccel;
		private CheckBox checkSimpleChart;
		private CheckBox checkDoubleBuffering;
		private GroupBox group3DToothChart;
		private Label label1;
		private System.Windows.Forms.Button buttonRefreshFormats;
		private OpenDental.UI.ODGrid gridFormats;
		private OpenGLWinFormsControl.PixelFormatValue[] formats=new OpenGLWinFormsControl.PixelFormatValue[0];
		private int currentFormatNum=0;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		///<summary></summary>
		public FormGraphics(){
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			Lan.F(this);
			
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
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
			System.ComponentModel.ComponentResourceManager resources=new System.ComponentModel.ComponentResourceManager(typeof(FormGraphics));
			this.checkHardwareAccel=new System.Windows.Forms.CheckBox();
			this.checkSimpleChart=new System.Windows.Forms.CheckBox();
			this.butOK=new OpenDental.UI.Button();
			this.butCancel=new OpenDental.UI.Button();
			this.checkDoubleBuffering=new System.Windows.Forms.CheckBox();
			this.group3DToothChart=new System.Windows.Forms.GroupBox();
			this.gridFormats=new OpenDental.UI.ODGrid();
			this.buttonRefreshFormats=new System.Windows.Forms.Button();
			this.label1=new System.Windows.Forms.Label();
			this.group3DToothChart.SuspendLayout();
			this.SuspendLayout();
			// 
			// checkHardwareAccel
			// 
			this.checkHardwareAccel.Location=new System.Drawing.Point(6,19);
			this.checkHardwareAccel.Name="checkHardwareAccel";
			this.checkHardwareAccel.Size=new System.Drawing.Size(176,18);
			this.checkHardwareAccel.TabIndex=2;
			this.checkHardwareAccel.Text="Hardware Acceleration";
			this.checkHardwareAccel.UseVisualStyleBackColor=true;
			// 
			// checkSimpleChart
			// 
			this.checkSimpleChart.Location=new System.Drawing.Point(47,30);
			this.checkSimpleChart.Name="checkSimpleChart";
			this.checkSimpleChart.Size=new System.Drawing.Size(176,18);
			this.checkSimpleChart.TabIndex=3;
			this.checkSimpleChart.Text="Use Simple Tooth Chart";
			this.checkSimpleChart.UseVisualStyleBackColor=true;
			this.checkSimpleChart.CheckedChanged+=new System.EventHandler(this.checkSimpleChart_CheckedChanged);
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation=new System.Drawing.Point(0,0);
			this.butOK.Anchor=((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom|System.Windows.Forms.AnchorStyles.Right)));
			this.butOK.Autosize=true;
			this.butOK.BtnShape=OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle=OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.CornerRadius=4F;
			this.butOK.Location=new System.Drawing.Point(464,364);
			this.butOK.Name="butOK";
			this.butOK.Size=new System.Drawing.Size(75,26);
			this.butOK.TabIndex=1;
			this.butOK.Text="&OK";
			this.butOK.Click+=new System.EventHandler(this.butOK_Click);
			// 
			// butCancel
			// 
			this.butCancel.AdjustImageLocation=new System.Drawing.Point(0,0);
			this.butCancel.Anchor=((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom|System.Windows.Forms.AnchorStyles.Right)));
			this.butCancel.Autosize=true;
			this.butCancel.BtnShape=OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butCancel.BtnStyle=OpenDental.UI.enumType.XPStyle.Silver;
			this.butCancel.CornerRadius=4F;
			this.butCancel.Location=new System.Drawing.Point(566,364);
			this.butCancel.Name="butCancel";
			this.butCancel.Size=new System.Drawing.Size(75,26);
			this.butCancel.TabIndex=0;
			this.butCancel.Text="&Cancel";
			this.butCancel.Click+=new System.EventHandler(this.butCancel_Click);
			// 
			// checkDoubleBuffering
			// 
			this.checkDoubleBuffering.AutoSize=true;
			this.checkDoubleBuffering.Location=new System.Drawing.Point(6,43);
			this.checkDoubleBuffering.Name="checkDoubleBuffering";
			this.checkDoubleBuffering.Size=new System.Drawing.Size(127,17);
			this.checkDoubleBuffering.TabIndex=4;
			this.checkDoubleBuffering.Text="Use Double-Buffering";
			this.checkDoubleBuffering.UseVisualStyleBackColor=true;
			// 
			// group3DToothChart
			// 
			this.group3DToothChart.Controls.Add(this.gridFormats);
			this.group3DToothChart.Controls.Add(this.buttonRefreshFormats);
			this.group3DToothChart.Controls.Add(this.label1);
			this.group3DToothChart.Controls.Add(this.checkHardwareAccel);
			this.group3DToothChart.Controls.Add(this.checkDoubleBuffering);
			this.group3DToothChart.Location=new System.Drawing.Point(47,67);
			this.group3DToothChart.Name="group3DToothChart";
			this.group3DToothChart.Size=new System.Drawing.Size(594,271);
			this.group3DToothChart.TabIndex=5;
			this.group3DToothChart.TabStop=false;
			this.group3DToothChart.Text="Options For 3D Tooth Chart";
			// 
			// gridFormats
			// 
			this.gridFormats.HScrollVisible=false;
			this.gridFormats.Location=new System.Drawing.Point(9,85);
			this.gridFormats.Name="gridFormats";
			this.gridFormats.ScrollValue=0;
			this.gridFormats.Size=new System.Drawing.Size(579,146);
			this.gridFormats.TabIndex=8;
			this.gridFormats.Title=null;
			this.gridFormats.TranslationName=null;
			this.gridFormats.CellClick+=new OpenDental.UI.ODGridClickEventHandler(this.gridFormats_CellClick);
			// 
			// buttonRefreshFormats
			// 
			this.buttonRefreshFormats.Location=new System.Drawing.Point(9,237);
			this.buttonRefreshFormats.Name="buttonRefreshFormats";
			this.buttonRefreshFormats.Size=new System.Drawing.Size(100,23);
			this.buttonRefreshFormats.TabIndex=7;
			this.buttonRefreshFormats.Text="Refresh Formats";
			this.buttonRefreshFormats.UseVisualStyleBackColor=true;
			this.buttonRefreshFormats.Click+=new System.EventHandler(this.buttonRefreshFormats_Click);
			// 
			// label1
			// 
			this.label1.AutoSize=true;
			this.label1.Location=new System.Drawing.Point(6,68);
			this.label1.Name="label1";
			this.label1.Size=new System.Drawing.Size(166,13);
			this.label1.TabIndex=6;
			this.label1.Text="List of Available Graphics Formats";
			// 
			// FormGraphics
			// 
			this.AutoScaleBaseSize=new System.Drawing.Size(5,13);
			this.ClientSize=new System.Drawing.Size(678,415);
			this.Controls.Add(this.group3DToothChart);
			this.Controls.Add(this.checkSimpleChart);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.butCancel);
			this.Icon=((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox=false;
			this.MinimizeBox=false;
			this.Name="FormGraphics";
			this.ShowInTaskbar=false;
			this.StartPosition=System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text="Graphics Preferences";
			this.Load+=new System.EventHandler(this.FormGraphics_Load);
			this.group3DToothChart.ResumeLayout(false);
			this.group3DToothChart.PerformLayout();
			this.ResumeLayout(false);

		}
		#endregion

		private void FormGraphics_Load(object sender,EventArgs e) {
			ComputerPref computerPrefs=ComputerPrefs.GetForLocalComputer();
			checkHardwareAccel.Checked=computerPrefs.GraphicsUseHardware;
			checkDoubleBuffering.Checked=computerPrefs.GraphicsDoubleBuffering;
			currentFormatNum=computerPrefs.PreferredPixelFormatNum;
			checkSimpleChart.Checked=computerPrefs.GraphicsSimple;//Must be after checkHardwareAccel is set. Sets initial visibility.
			gridFormats.BeginUpdate();
			gridFormats.Columns.Clear();
			ODGridColumn col=new ODGridColumn(Lan.g(this,"FormatNum"),80);
			gridFormats.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"Accelerated"),80);
			gridFormats.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"Buffering"),80);
			gridFormats.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"ColorBits"),65);
			gridFormats.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"DepthBits"),65);
			gridFormats.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"Bitmap"),65);
			gridFormats.Columns.Add(col);
			gridFormats.EndUpdate();
		}

		private void checkSimpleChart_CheckedChanged(object sender,EventArgs e) {
			if(Environment.OSVersion.Platform==PlatformID.Unix){//Force simple mode on Unix systems.
				checkSimpleChart.Checked=true;
				checkSimpleChart.Enabled=false;
			}
			for(int i=0;i<group3DToothChart.Controls.Count;i++){
				group3DToothChart.Controls[i].Enabled=!checkSimpleChart.Checked;
			}
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			ComputerPref computerPrefs=ComputerPrefs.GetForLocalComputer();
			computerPrefs.GraphicsUseHardware=checkHardwareAccel.Checked;
			computerPrefs.GraphicsSimple=checkSimpleChart.Checked;
			computerPrefs.GraphicsDoubleBuffering=checkDoubleBuffering.Checked;
			computerPrefs.PreferredPixelFormatNum=currentFormatNum;
			ComputerPrefs.Update(computerPrefs);
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		private void buttonRefreshFormats_Click(object sender,EventArgs e) {
			OpenGLWinFormsControl contextFinder=new OpenGLWinFormsControl();
			//Get raw formats.
			Gdi.PIXELFORMATDESCRIPTOR[] rawformats=OpenGLWinFormsControl.GetPixelFormats(contextFinder.GetHDC(),1000);
			//Prioritize formats as requested by the user.
			formats=OpenGLWinFormsControl.PrioritizePixelFormats(rawformats,32,32,checkDoubleBuffering.Checked,checkHardwareAccel.Checked);
			contextFinder.Dispose();
			//Update the format grid to reflect possible changes in formats.
			UpdateFormatGrid();
		}

		private void UpdateFormatGrid(){
			int selectionIndex=-1;
			gridFormats.BeginUpdate();
			gridFormats.Rows.Clear();
			for(int i=0;i<formats.Length;i++){
				ODGridRow row=new ODGridRow();
				row.Cells.Add(formats[i].formatNumber.ToString());
				row.Cells.Add(OpenGLWinFormsControl.FormatSupportsAcceleration(formats[i].pfd)?"Yes":"No");
				row.Cells.Add(OpenGLWinFormsControl.FormatSupportsDoubleBuffering(formats[i].pfd)?"Yes":"No");
				row.Cells.Add(formats[i].pfd.cColorBits.ToString());
				row.Cells.Add(formats[i].pfd.cDepthBits.ToString());
				row.Cells.Add(OpenGLWinFormsControl.FormatSupportsBitmap(formats[i].pfd)?"Yes":"No");
				gridFormats.Rows.Add(row);
				if(formats[i].formatNumber==currentFormatNum){
					selectionIndex=i;
				}
			}
			gridFormats.EndUpdate();
			if(selectionIndex>=0){
				gridFormats.SetSelected(selectionIndex,true);
			}
		}

		private void gridFormats_CellClick(object sender,ODGridClickEventArgs e) {
			currentFormatNum=Convert.ToInt32(gridFormats.Rows[e.Row].Cells[0].Text);
		}		


	}
}





















