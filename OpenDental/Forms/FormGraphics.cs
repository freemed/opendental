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
		private OpenDental.UI.ODGrid gridFormats;
		private OpenGLWinFormsControl.PixelFormatValue[] formats=new OpenGLWinFormsControl.PixelFormatValue[0];
		private int currentFormatNum=0;
		private CheckBox checkAllFormats;
		private System.Windows.Forms.Button buttonAutoFormat;
		private bool refreshAllowed=false;

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
			this.checkDoubleBuffering=new System.Windows.Forms.CheckBox();
			this.group3DToothChart=new System.Windows.Forms.GroupBox();
			this.buttonAutoFormat=new System.Windows.Forms.Button();
			this.checkAllFormats=new System.Windows.Forms.CheckBox();
			this.gridFormats=new OpenDental.UI.ODGrid();
			this.label1=new System.Windows.Forms.Label();
			this.butOK=new OpenDental.UI.Button();
			this.butCancel=new OpenDental.UI.Button();
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
			this.checkHardwareAccel.CheckedChanged+=new System.EventHandler(this.checkHardwareAccel_CheckedChanged);
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
			// checkDoubleBuffering
			// 
			this.checkDoubleBuffering.AutoSize=true;
			this.checkDoubleBuffering.Location=new System.Drawing.Point(6,43);
			this.checkDoubleBuffering.Name="checkDoubleBuffering";
			this.checkDoubleBuffering.Size=new System.Drawing.Size(127,17);
			this.checkDoubleBuffering.TabIndex=4;
			this.checkDoubleBuffering.Text="Use Double-Buffering";
			this.checkDoubleBuffering.UseVisualStyleBackColor=true;
			this.checkDoubleBuffering.CheckedChanged+=new System.EventHandler(this.checkDoubleBuffering_CheckedChanged);
			// 
			// group3DToothChart
			// 
			this.group3DToothChart.Controls.Add(this.buttonAutoFormat);
			this.group3DToothChart.Controls.Add(this.checkAllFormats);
			this.group3DToothChart.Controls.Add(this.gridFormats);
			this.group3DToothChart.Controls.Add(this.label1);
			this.group3DToothChart.Controls.Add(this.checkHardwareAccel);
			this.group3DToothChart.Controls.Add(this.checkDoubleBuffering);
			this.group3DToothChart.Location=new System.Drawing.Point(47,67);
			this.group3DToothChart.Name="group3DToothChart";
			this.group3DToothChart.Size=new System.Drawing.Size(833,300);
			this.group3DToothChart.TabIndex=5;
			this.group3DToothChart.TabStop=false;
			this.group3DToothChart.Text="Options For 3D Tooth Chart";
			// 
			// buttonAutoFormat
			// 
			this.buttonAutoFormat.Location=new System.Drawing.Point(6,268);
			this.buttonAutoFormat.Name="buttonAutoFormat";
			this.buttonAutoFormat.Size=new System.Drawing.Size(127,23);
			this.buttonAutoFormat.TabIndex=6;
			this.buttonAutoFormat.Text="Auto-Select Format";
			this.buttonAutoFormat.UseVisualStyleBackColor=true;
			this.buttonAutoFormat.Click+=new System.EventHandler(this.buttonAutoFormat_Click);
			// 
			// checkAllFormats
			// 
			this.checkAllFormats.AutoSize=true;
			this.checkAllFormats.Location=new System.Drawing.Point(188,19);
			this.checkAllFormats.Name="checkAllFormats";
			this.checkAllFormats.Size=new System.Drawing.Size(107,17);
			this.checkAllFormats.TabIndex=9;
			this.checkAllFormats.Text="Show All Formats";
			this.checkAllFormats.UseVisualStyleBackColor=true;
			this.checkAllFormats.CheckedChanged+=new System.EventHandler(this.checkAllFormats_CheckedChanged);
			// 
			// gridFormats
			// 
			this.gridFormats.HScrollVisible=false;
			this.gridFormats.Location=new System.Drawing.Point(6,85);
			this.gridFormats.Name="gridFormats";
			this.gridFormats.ScrollValue=0;
			this.gridFormats.Size=new System.Drawing.Size(821,180);
			this.gridFormats.TabIndex=8;
			this.gridFormats.Title=null;
			this.gridFormats.TranslationName=null;
			this.gridFormats.CellClick+=new OpenDental.UI.ODGridClickEventHandler(this.gridFormats_CellClick);
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
			// butOK
			// 
			this.butOK.AdjustImageLocation=new System.Drawing.Point(0,0);
			this.butOK.Anchor=((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom|System.Windows.Forms.AnchorStyles.Right)));
			this.butOK.Autosize=true;
			this.butOK.BtnShape=OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle=OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.CornerRadius=4F;
			this.butOK.Location=new System.Drawing.Point(678,373);
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
			this.butCancel.Location=new System.Drawing.Point(780,373);
			this.butCancel.Name="butCancel";
			this.butCancel.Size=new System.Drawing.Size(75,26);
			this.butCancel.TabIndex=0;
			this.butCancel.Text="&Cancel";
			this.butCancel.Click+=new System.EventHandler(this.butCancel_Click);
			// 
			// FormGraphics
			// 
			this.AutoScaleBaseSize=new System.Drawing.Size(5,13);
			this.ClientSize=new System.Drawing.Size(892,424);
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
			gridFormats.BeginUpdate();
			gridFormats.Columns.Clear();
			ODGridColumn col=new ODGridColumn(Lan.g(this,"FormatNum"),80);
			gridFormats.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"OpenGL"),60);
			gridFormats.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"Windowed"),80);
			gridFormats.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"Bitmapped"),80);
			gridFormats.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"Palette"),75);
			gridFormats.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"Accelerated"),80);
			gridFormats.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"Buffered"),75);
			gridFormats.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"ColorBits"),75);
			gridFormats.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"DepthBits"),75);
			gridFormats.Columns.Add(col);
			gridFormats.EndUpdate();
			ComputerPref computerPrefs=ComputerPrefs.GetForLocalComputer();
			checkHardwareAccel.Checked=computerPrefs.GraphicsUseHardware;
			checkDoubleBuffering.Checked=computerPrefs.GraphicsDoubleBuffering;
			currentFormatNum=computerPrefs.PreferredPixelFormatNum;
			checkSimpleChart.Checked=computerPrefs.GraphicsSimple;//Must be set last. Sets initial visibility.
			refreshAllowed=true;
			RefreshFormats();
		}

		private void UpdateFormatGrid() {
			int selectionIndex=-1;
			gridFormats.BeginUpdate();
			gridFormats.Rows.Clear();
			for(int i=0;i<formats.Length;i++) {
				ODGridRow row=new ODGridRow();
				row.Cells.Add(formats[i].formatNumber.ToString());
				row.Cells.Add(OpenGLWinFormsControl.FormatSupportsOpenGL(formats[i].pfd)?"Yes":"No");
				row.Cells.Add(OpenGLWinFormsControl.FormatSupportsWindow(formats[i].pfd)?"Yes":"No");
				row.Cells.Add(OpenGLWinFormsControl.FormatSupportsBitmap(formats[i].pfd)?"Yes":"No");
				row.Cells.Add(OpenGLWinFormsControl.FormatUsesPalette(formats[i].pfd)?"Yes":"No");
				row.Cells.Add(OpenGLWinFormsControl.FormatSupportsAcceleration(formats[i].pfd)?"Yes":"No");
				row.Cells.Add(OpenGLWinFormsControl.FormatSupportsDoubleBuffering(formats[i].pfd)?"Yes":"No");
				row.Cells.Add(formats[i].pfd.cColorBits.ToString());
				row.Cells.Add(formats[i].pfd.cDepthBits.ToString());
				gridFormats.Rows.Add(row);
				if(formats[i].formatNumber==currentFormatNum) {
					selectionIndex=i;
				}
			}
			gridFormats.EndUpdate();
			if(selectionIndex>=0) {
				gridFormats.SetSelected(selectionIndex,true);
			}
		}

		private void RefreshFormats() {
			if(!refreshAllowed){
				return;
			}
			this.Cursor=Cursors.WaitCursor;
			OpenGLWinFormsControl contextFinder=new OpenGLWinFormsControl();
			//Get raw formats.
			Gdi.PIXELFORMATDESCRIPTOR[] rawformats=OpenGLWinFormsControl.GetPixelFormats(contextFinder.GetHDC());
			if(checkAllFormats.Checked){
				formats=new OpenGLWinFormsControl.PixelFormatValue[rawformats.Length];
				for(int i=0;i<rawformats.Length;i++) {
					formats[i]=new OpenGLWinFormsControl.PixelFormatValue();
					formats[i].formatNumber=i+1;
					formats[i].pfd=rawformats[i];
				}
			}else{
				//Prioritize formats as requested by the user.
				formats=OpenGLWinFormsControl.PrioritizePixelFormats(rawformats,checkDoubleBuffering.Checked,checkHardwareAccel.Checked);
			}
			contextFinder.Dispose();
			//Update the format grid to reflect possible changes in formats.
			UpdateFormatGrid();
			this.Cursor=Cursors.Default;
		}

		private void checkSimpleChart_CheckedChanged(object sender,EventArgs e) {
			if(Environment.OSVersion.Platform==PlatformID.Unix){//Force simple mode on Unix systems.
				checkSimpleChart.Checked=true;
				checkSimpleChart.Enabled=false;
			}
			for(int i=0;i<group3DToothChart.Controls.Count;i++){
				group3DToothChart.Controls[i].Enabled=!checkSimpleChart.Checked;
			}
			checkHardwareAccel.Enabled&=!checkAllFormats.Checked;
			checkDoubleBuffering.Enabled&=!checkAllFormats.Checked;
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			OpenGLWinFormsControl contextTester=new OpenGLWinFormsControl();
			try{
				contextTester.TaoInitializeContexts(currentFormatNum);
			}catch(Exception ex){
				MessageBox.Show(Lan.g(this,"Please choose a different pixel format, the selected pixel format will not support the 3D tooth chart on this computer: "+ex.Message));
				contextTester.Dispose();
				return;
			}
			contextTester.Dispose();
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

		private void gridFormats_CellClick(object sender,ODGridClickEventArgs e) {
			currentFormatNum=Convert.ToInt32(gridFormats.Rows[e.Row].Cells[0].Text);
		}

		private void checkDoubleBuffering_CheckedChanged(object sender,EventArgs e) {
			RefreshFormats();
		}

		private void checkHardwareAccel_CheckedChanged(object sender,EventArgs e) {
			RefreshFormats();
		}

		private void checkAllFormats_CheckedChanged(object sender,EventArgs e) {
			checkHardwareAccel.Enabled=!checkAllFormats.Checked;
			checkDoubleBuffering.Enabled=!checkAllFormats.Checked;
			RefreshFormats();
		}

		private void buttonAutoFormat_Click(object sender,EventArgs e) {
			OpenGLWinFormsControl autoFormat=new OpenGLWinFormsControl();
			OpenGLWinFormsControl.PixelFormatValue pfv=OpenGLWinFormsControl.ChoosePixelFormatEx(autoFormat.GetHDC());
			autoFormat.Dispose();
			currentFormatNum=pfv.formatNumber;
			RefreshFormats();
		}

	}
}





















