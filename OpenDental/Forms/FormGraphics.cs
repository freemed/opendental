using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using OpenDentBusiness;
using CodeBase;
using Microsoft.DirectX.Direct3D;
using Tao.OpenGl;
using Tao.Platform.Windows;
using OpenDental.UI;
using SparksToothChart;

namespace OpenDental{
	/// <summary>
	/// Summary description for FormBasicTemplate.
	/// </summary>
	public class FormGraphics : System.Windows.Forms.Form{
		private OpenDental.UI.Button butCancel;
		private OpenDental.UI.Button butOK;
		private CheckBox checkHardwareAccel;
		private CheckBox checkDoubleBuffering;
		private GroupBox group3DToothChart;
		private OpenDental.UI.ODGrid gridFormats;
		private OpenGLWinFormsControl.PixelFormatValue[] formats=new OpenGLWinFormsControl.PixelFormatValue[0];
		private ToothChartDirectX.DirectXDeviceFormat[] xformats=new ToothChartDirectX.DirectXDeviceFormat[0];
		private int selectedFormatNum=0;
		private string selectedDirectXFormat="";
		private CheckBox checkAllFormats;
		//private bool refreshAllowed=false;
		private RadioButton radioSimpleChart;
		private RadioButton radioOpenGLChart;
		private GroupBox groupFilters;
		private Label label1;
		private Label label2;
		private Label label4;
		private TextBox textSelected;
		private Label label3;
		private RadioButton radioDirectXChart;

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
			this.checkDoubleBuffering=new System.Windows.Forms.CheckBox();
			this.group3DToothChart=new System.Windows.Forms.GroupBox();
			this.label4=new System.Windows.Forms.Label();
			this.textSelected=new System.Windows.Forms.TextBox();
			this.label3=new System.Windows.Forms.Label();
			this.label2=new System.Windows.Forms.Label();
			this.label1=new System.Windows.Forms.Label();
			this.groupFilters=new System.Windows.Forms.GroupBox();
			this.checkAllFormats=new System.Windows.Forms.CheckBox();
			this.radioSimpleChart=new System.Windows.Forms.RadioButton();
			this.radioOpenGLChart=new System.Windows.Forms.RadioButton();
			this.radioDirectXChart=new System.Windows.Forms.RadioButton();
			this.gridFormats=new OpenDental.UI.ODGrid();
			this.butOK=new OpenDental.UI.Button();
			this.butCancel=new OpenDental.UI.Button();
			this.group3DToothChart.SuspendLayout();
			this.groupFilters.SuspendLayout();
			this.SuspendLayout();
			// 
			// checkHardwareAccel
			// 
			this.checkHardwareAccel.Location=new System.Drawing.Point(6,19);
			this.checkHardwareAccel.Name="checkHardwareAccel";
			this.checkHardwareAccel.Size=new System.Drawing.Size(282,18);
			this.checkHardwareAccel.TabIndex=2;
			this.checkHardwareAccel.Text="Hardware Acceleration (checked by default)";
			this.checkHardwareAccel.UseVisualStyleBackColor=true;
			this.checkHardwareAccel.Click+=new System.EventHandler(this.checkHardwareAccel_Click);
			// 
			// checkDoubleBuffering
			// 
			this.checkDoubleBuffering.Location=new System.Drawing.Point(6,42);
			this.checkDoubleBuffering.Name="checkDoubleBuffering";
			this.checkDoubleBuffering.Size=new System.Drawing.Size(282,17);
			this.checkDoubleBuffering.TabIndex=4;
			this.checkDoubleBuffering.Text="Use Double-Buffering";
			this.checkDoubleBuffering.UseVisualStyleBackColor=true;
			this.checkDoubleBuffering.Click+=new System.EventHandler(this.checkDoubleBuffering_Click);
			// 
			// group3DToothChart
			// 
			this.group3DToothChart.Controls.Add(this.label4);
			this.group3DToothChart.Controls.Add(this.textSelected);
			this.group3DToothChart.Controls.Add(this.label3);
			this.group3DToothChart.Controls.Add(this.label2);
			this.group3DToothChart.Controls.Add(this.label1);
			this.group3DToothChart.Controls.Add(this.groupFilters);
			this.group3DToothChart.Controls.Add(this.gridFormats);
			this.group3DToothChart.Location=new System.Drawing.Point(28,91);
			this.group3DToothChart.Name="group3DToothChart";
			this.group3DToothChart.Size=new System.Drawing.Size(833,455);
			this.group3DToothChart.TabIndex=5;
			this.group3DToothChart.TabStop=false;
			this.group3DToothChart.Text="Options For 3D Tooth Chart";
			// 
			// label4
			// 
			this.label4.Location=new System.Drawing.Point(60,195);
			this.label4.Name="label4";
			this.label4.Size=new System.Drawing.Size(608,16);
			this.label4.TabIndex=15;
			this.label4.Text=" Formats are listed from most recommended on top to least recommended on bottom.";
			// 
			// textSelected
			// 
			this.textSelected.Location=new System.Drawing.Point(6,192);
			this.textSelected.Name="textSelected";
			this.textSelected.ReadOnly=true;
			this.textSelected.Size=new System.Drawing.Size(53,20);
			this.textSelected.TabIndex=14;
			// 
			// label3
			// 
			this.label3.Location=new System.Drawing.Point(3,174);
			this.label3.Name="label3";
			this.label3.Size=new System.Drawing.Size(159,16);
			this.label3.TabIndex=13;
			this.label3.Text="Currently selected format number";
			// 
			// label2
			// 
			this.label2.Location=new System.Drawing.Point(6,131);
			this.label2.Name="label2";
			this.label2.Size=new System.Drawing.Size(818,45);
			this.label2.TabIndex=12;
			this.label2.Text=resources.GetString("label2.Text");
			// 
			// label1
			// 
			this.label1.Location=new System.Drawing.Point(9,18);
			this.label1.Name="label1";
			this.label1.Size=new System.Drawing.Size(818,20);
			this.label1.TabIndex=11;
			this.label1.Text="Most users will never need to change any of these options.  These are only used w"+
					"hen the 3D tooth chart is not working properly.";
			// 
			// groupFilters
			// 
			this.groupFilters.Controls.Add(this.checkHardwareAccel);
			this.groupFilters.Controls.Add(this.checkDoubleBuffering);
			this.groupFilters.Controls.Add(this.checkAllFormats);
			this.groupFilters.Location=new System.Drawing.Point(6,39);
			this.groupFilters.Name="groupFilters";
			this.groupFilters.Size=new System.Drawing.Size(295,88);
			this.groupFilters.TabIndex=10;
			this.groupFilters.TabStop=false;
			this.groupFilters.Text="Filters for list below";
			// 
			// checkAllFormats
			// 
			this.checkAllFormats.Location=new System.Drawing.Point(6,64);
			this.checkAllFormats.Name="checkAllFormats";
			this.checkAllFormats.Size=new System.Drawing.Size(282,17);
			this.checkAllFormats.TabIndex=9;
			this.checkAllFormats.Text="Show All Formats";
			this.checkAllFormats.UseVisualStyleBackColor=true;
			this.checkAllFormats.Click+=new System.EventHandler(this.checkAllFormats_Click);
			// 
			// radioSimpleChart
			// 
			this.radioSimpleChart.Location=new System.Drawing.Point(34,36);
			this.radioSimpleChart.Name="radioSimpleChart";
			this.radioSimpleChart.Size=new System.Drawing.Size(146,19);
			this.radioSimpleChart.TabIndex=6;
			this.radioSimpleChart.TabStop=true;
			this.radioSimpleChart.Text="Use Simple Tooth Chart";
			this.radioSimpleChart.UseVisualStyleBackColor=true;
			this.radioSimpleChart.Click+=new System.EventHandler(this.radioSimpleChart_Click);
			// 
			// radioOpenGLChart
			// 
			this.radioOpenGLChart.Location=new System.Drawing.Point(34,56);
			this.radioOpenGLChart.Name="radioOpenGLChart";
			this.radioOpenGLChart.Size=new System.Drawing.Size(242,19);
			this.radioOpenGLChart.TabIndex=7;
			this.radioOpenGLChart.TabStop=true;
			this.radioOpenGLChart.Text="Use OpenGL Tooth Chart (phasing out)";
			this.radioOpenGLChart.UseVisualStyleBackColor=true;
			this.radioOpenGLChart.Click+=new System.EventHandler(this.radioOpenGLChart_Click);
			// 
			// radioDirectXChart
			// 
			this.radioDirectXChart.Location=new System.Drawing.Point(34,15);
			this.radioDirectXChart.Name="radioDirectXChart";
			this.radioDirectXChart.Size=new System.Drawing.Size(233,19);
			this.radioDirectXChart.TabIndex=8;
			this.radioDirectXChart.TabStop=true;
			this.radioDirectXChart.Text="Use DirectX Tooth Chart (recommended)";
			this.radioDirectXChart.UseVisualStyleBackColor=true;
			this.radioDirectXChart.Click+=new System.EventHandler(this.radioDirectXChart_Click);
			// 
			// gridFormats
			// 
			this.gridFormats.HScrollVisible=false;
			this.gridFormats.Location=new System.Drawing.Point(6,226);
			this.gridFormats.Name="gridFormats";
			this.gridFormats.ScrollValue=0;
			this.gridFormats.Size=new System.Drawing.Size(821,223);
			this.gridFormats.TabIndex=8;
			this.gridFormats.Title="Available Graphics Formats";
			this.gridFormats.TranslationName=null;
			this.gridFormats.CellClick+=new OpenDental.UI.ODGridClickEventHandler(this.gridFormats_CellClick);
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation=new System.Drawing.Point(0,0);
			this.butOK.Anchor=((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom|System.Windows.Forms.AnchorStyles.Right)));
			this.butOK.Autosize=true;
			this.butOK.BtnShape=OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle=OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.CornerRadius=4F;
			this.butOK.Location=new System.Drawing.Point(684,557);
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
			this.butCancel.Location=new System.Drawing.Point(786,557);
			this.butCancel.Name="butCancel";
			this.butCancel.Size=new System.Drawing.Size(75,26);
			this.butCancel.TabIndex=0;
			this.butCancel.Text="&Cancel";
			this.butCancel.Click+=new System.EventHandler(this.butCancel_Click);
			// 
			// FormGraphics
			// 
			this.AutoScaleBaseSize=new System.Drawing.Size(5,13);
			this.ClientSize=new System.Drawing.Size(892,594);
			this.Controls.Add(this.radioDirectXChart);
			this.Controls.Add(this.radioOpenGLChart);
			this.Controls.Add(this.radioSimpleChart);
			this.Controls.Add(this.group3DToothChart);
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
			this.groupFilters.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormGraphics_Load(object sender,EventArgs e) {
			if(Environment.OSVersion.Platform==PlatformID.Unix) {//Force simple mode on Unix systems.
				MsgBox.Show(this,"Linux users must use simple tooth chart.");
				radioDirectXChart.Enabled=false;
				radioOpenGLChart.Enabled=false;
				group3DToothChart.Enabled=false;
				return;
			}
			ComputerPref computerPref=ComputerPrefs.GetForLocalComputer();
			checkHardwareAccel.Checked=computerPref.GraphicsUseHardware;
			checkDoubleBuffering.Checked=computerPref.GraphicsDoubleBuffering;
			selectedFormatNum=computerPref.PreferredPixelFormatNum;
			selectedDirectXFormat=computerPref.DirectXFormat;
			textSelected.Text="";
			if(computerPref.GraphicsSimple==DrawingMode.Simple2D){
				radioSimpleChart.Checked=true;
				group3DToothChart.Enabled=false;
			}
			else if(computerPref.GraphicsSimple==DrawingMode.DirectX){
				radioDirectXChart.Checked=true;
				group3DToothChart.Enabled=true;
				groupFilters.Enabled=false;
			}
			else{//OpenGL
				radioOpenGLChart.Checked=true;
				group3DToothChart.Enabled=true;
				groupFilters.Enabled=true;
			}
			RefreshFormats();
		}

		private void FillGrid() {
			int selectionIndex=-1;
			gridFormats.BeginUpdate();
			gridFormats.Rows.Clear();
			if(this.radioDirectXChart.Checked){
				textSelected.Text="";
				gridFormats.BeginUpdate();
				gridFormats.Columns.Clear();
				ODGridColumn col=new ODGridColumn(Lan.g(this,"FormatNum"),80);
				gridFormats.Columns.Add(col);
				col=new ODGridColumn(Lan.g(this,"Adapter"),60);
				gridFormats.Columns.Add(col);
				col=new ODGridColumn(Lan.g(this,"Accelerated"),80);
				gridFormats.Columns.Add(col);
				col=new ODGridColumn(Lan.g(this,"Buffered"),75);
				gridFormats.Columns.Add(col);
				col=new ODGridColumn(Lan.g(this,"ColorBits"),75);
				gridFormats.Columns.Add(col);
				col=new ODGridColumn(Lan.g(this,"ColorFormat"),75);
				gridFormats.Columns.Add(col);
				col=new ODGridColumn(Lan.g(this,"DepthBits"),75);
				gridFormats.Columns.Add(col);
				col=new ODGridColumn(Lan.g(this,"DepthFormat"),75);
				gridFormats.Columns.Add(col);
				col=new ODGridColumn(Lan.g(this,"Antialiasing"),75);
				gridFormats.Columns.Add(col);
				gridFormats.EndUpdate();
				for(int i=0;i<xformats.Length;i++) {
					ODGridRow row=new ODGridRow();
					row.Cells.Add((i+1).ToString());
					row.Cells.Add(xformats[i].adapter.Adapter.ToString());
					row.Cells.Add(xformats[i].deviceType==DeviceType.Hardware?"Yes":"No");//Supports hardware acceleration?
					row.Cells.Add("Yes");//Supports double-buffering. All DirectX devices support double-buffering as required.
					row.Cells.Add(ToothChartDirectX.GetFormatBitCount(xformats[i].backBufferFormat).ToString());//Color bits.
					row.Cells.Add(Enum.GetName(typeof(Format),xformats[i].backBufferFormat));
					row.Cells.Add(ToothChartDirectX.GetDepthFormatBitCount(xformats[i].depthStencilFormat).ToString());//Depth buffer bits.
					row.Cells.Add(Enum.GetName(typeof(DepthFormat),xformats[i].depthStencilFormat));
					row.Cells.Add(ToothChartDirectX.GetMultiSampleNumberForType(xformats[i].maxMultiSampleType).ToString());
					gridFormats.Rows.Add(row);
					if(xformats[i].ToString()==selectedDirectXFormat) {
					  selectionIndex=i;
						textSelected.Text=(i+1).ToString();
					}
				}
			}else if(this.radioOpenGLChart.Checked){
				textSelected.Text=selectedFormatNum.ToString();
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
					if(formats[i].formatNumber==selectedFormatNum) {
						selectionIndex=i;
					}
				}
			}
			gridFormats.EndUpdate();
			if(selectionIndex>=0) {
				gridFormats.SetSelected(selectionIndex,true);
			}
		}

		///<Summary>Get all formats for the grid based on the current filters.</Summary>
		private void RefreshFormats() {
			this.Cursor=Cursors.WaitCursor;
			if(this.radioDirectXChart.Checked){
				xformats=ToothChartDirectX.GetStandardDeviceFormats();
			}else if(this.radioOpenGLChart.Checked){
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
			}
			//Update the format grid to reflect possible changes in formats.
			FillGrid();
			this.Cursor=Cursors.Default;
		}

		private void radioSimpleChart_Click(object sender,EventArgs e) {
			group3DToothChart.Enabled=false;
		}

		private void radioDirectXChart_Click(object sender,EventArgs e) {
			group3DToothChart.Enabled=true;
			groupFilters.Enabled=false;
			RefreshFormats();
		}

		private void radioOpenGLChart_Click(object sender,EventArgs e) {
			group3DToothChart.Enabled=true;
			groupFilters.Enabled=true;
			RefreshFormats();
		}

		private void checkHardwareAccel_Click(object sender,EventArgs e) {
			RefreshFormats();
		}

		private void checkDoubleBuffering_Click(object sender,EventArgs e) {
			RefreshFormats();
		}

		private void checkAllFormats_Click(object sender,EventArgs e) {
			checkHardwareAccel.Enabled=!checkAllFormats.Checked;
			checkDoubleBuffering.Enabled=!checkAllFormats.Checked;
			RefreshFormats();
		}

		private void gridFormats_CellClick(object sender,ODGridClickEventArgs e) {
			int formatNum=Convert.ToInt32(gridFormats.Rows[e.Row].Cells[0].Text);
			textSelected.Text=formatNum.ToString();
			if(radioDirectXChart.Checked) {
				selectedDirectXFormat=xformats[formatNum-1].ToString();
			}else if(this.radioOpenGLChart.Checked){
				selectedFormatNum=formatNum;
			}
		}

		private void butOK_Click(object sender,System.EventArgs e) {
			ComputerPref computerPref=ComputerPrefs.GetForLocalComputer();
			if(radioDirectXChart.Checked) {
				computerPref.GraphicsSimple=DrawingMode.DirectX;
				computerPref.DirectXFormat=selectedDirectXFormat;
				//TODO: test DirectX device creation works?
			}
			else if(radioSimpleChart.Checked) {
				computerPref.GraphicsSimple=DrawingMode.Simple2D;
			}
			else { //OpenGL
				OpenGLWinFormsControl contextTester=new OpenGLWinFormsControl();
				try {
					if(contextTester.TaoInitializeContexts(selectedFormatNum)!=selectedFormatNum) {
						throw new Exception(Lan.g(this,"Could not initialize pixel format ")+selectedFormatNum.ToString()+".");
					}
				} catch(Exception ex) {
					MessageBox.Show(Lan.g(this,"Please choose a different pixel format, the selected pixel format will not support the 3D tooth chart on this computer: "+ex.Message));
					contextTester.Dispose();
					return;
				}
				contextTester.Dispose();
				computerPref.GraphicsUseHardware=checkHardwareAccel.Checked;
				computerPref.GraphicsDoubleBuffering=checkDoubleBuffering.Checked;
				computerPref.PreferredPixelFormatNum=selectedFormatNum;
				computerPref.GraphicsSimple=DrawingMode.OpenGL;
			}
			ComputerPrefs.Update(computerPref);
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		
		

	}
}





















