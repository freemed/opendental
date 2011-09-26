/*=============================================================================================================
Open Dental GPL license Copyright (C) 2003  Jordan Sparks, DMD.  http://www.open-dent.com,  www.docsparks.com
See header in FormOpenDental.cs for complete text.  Redistributions must retain this text.
===============================================================================================================*/
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Data;
using System.Globalization;
using System.Windows.Forms;
using OpenDentBusiness;
using OpenDentBusiness.UI;

namespace OpenDental {
	///<summary></summary>
	public class ContrApptSheet:System.Windows.Forms.UserControl {
		private System.ComponentModel.Container components = null;// Required designer variable.
		///<summary></summary>
		public Bitmap Shadow;
		///<summary></summary>
		public bool IsScrolling=false;
		///<summary>This gets set externally each time the module is selected.  It is the background schedule for the entire period.  Includes all types.</summary>
		public List<Schedule> SchedListPeriod;

		///<summary></summary>
		public ContrApptSheet() {
			InitializeComponent();// This call is required by the Windows.Forms Form Designer.
		}

		///<summary></summary>
		protected override void Dispose(bool disposing) {
			if(disposing) {
				if(components != null) {
					components.Dispose();
				}
				if(Shadow!=null) {
					Shadow.Dispose();
					Shadow=null;
				}
			}
			base.Dispose(disposing);
		}

		#region Component Designer generated code

		private void InitializeComponent() {
			this.SuspendLayout();
			// 
			// ContrApptSheet
			// 
			this.Name = "ContrApptSheet";
			this.Size = new System.Drawing.Size(482,540);
			this.Load += new System.EventHandler(this.ContrApptSheet_Load);
			this.Layout += new System.Windows.Forms.LayoutEventHandler(this.ContrApptSheet_Layout);
			this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ContrApptSheet_MouseMove);
			this.ResumeLayout(false);

		}
		#endregion

		private void ContrApptSheet_Load(object sender,System.EventArgs e) {

		}

		private void ContrApptSheet_Layout(object sender,System.Windows.Forms.LayoutEventArgs e) {
			Height=ApptDrawing.LineH*24*ApptDrawing.RowsPerHr;
			Width=(int)(ApptDrawing.TimeWidth*2+ApptDrawing.ProvWidth*ApptDrawing.ProvCount+ApptDrawing.ColWidth*ApptDrawing.ColCount);
		}

		///<summary></summary>
		protected override void OnPaint(PaintEventArgs pea) {
			DrawShadow();
		}

		///<summary></summary>
		public void CreateShadow() {
			if(Shadow!=null) {
				Shadow.Dispose();
				Shadow=null;
			}
			if(Width<2)
				return;
			Shadow=new Bitmap(Width,Height);
			if(ApptDrawing.RowsPerIncr==0)
				ApptDrawing.RowsPerIncr=1;
			if(ApptDrawing.SchedListPeriod==null) {
				return;//not sure if this is necessary
			}
			using(Graphics g=Graphics.FromImage(Shadow)) {
				ApptDrawing.ApptSheetWidth=Width;
				ApptDrawing.ApptSheetHeight=Height;
				ApptDrawing.DrawAllButAppts(g,true,new DateTime(2011,1,1,0,0,0),new DateTime(2011,1,1,0,0,0),ApptDrawing.VisProvs.Count,0,8);
			}
		}

		///<summary></summary>
		public void DrawShadow() {
			if(Shadow!=null) {
				Graphics grfx2=this.CreateGraphics();
				grfx2.DrawImage(Shadow,0,0);
				grfx2.Dispose();
			}
		}

		private void ContrApptSheet_MouseMove(object sender,MouseEventArgs e) {
			//Debug.WriteLine("ContrApptSheet_MouseMove:"+DateTime.Now.ToLongTimeString()+", loc:"+e.Location.ToString());
		}

	}
}
