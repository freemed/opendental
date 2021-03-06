/*=============================================================================================================
Open Dental GPL license Copyright (C) 2003  Jordan Sparks, DMD.  http://www.open-dent.com,  www.docsparks.com
See header in FormOpenDental.cs for complete text.  Redistributions must retain this text.
===============================================================================================================*/
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using OpenDentBusiness;
using OpenDentBusiness.UI;

namespace OpenDental {

	///<summary></summary>
	public class ContrApptSingle:System.Windows.Forms.UserControl {
		private System.ComponentModel.Container components = null;// Required designer variable.
		///<summary>Set on mouse down or from Appt module</summary>
		public static long ClickedAptNum;
		/// <summary>This is not the best place for this, but changing it now would cause bugs.  Set manually</summary>
		public static long SelectedAptNum;
		///<summary>True if this control is on the pinboard</summary>
		public bool ThisIsPinBoard;
		/////<summary>Stores the shading info for the provider bars on the left of the appointments module</summary>
		//public static int[][] ProvBar;
		///<summary>Stores the background bitmap for this control</summary>
		public Bitmap Shadow;
		private Font baseFont=new Font("Arial",8);
		private Font boldFont=new Font("Arial",8,FontStyle.Bold);
		///<summary>The actual slashes and Xs showing for the current view.</summary>
		public string PatternShowing;
		///<summary>This is a datarow that stores most of the info necessary to draw appt.  It comes from the table obtained in Appointments.GetPeriodApptsTable().</summary>
		public DataRow DataRoww;
		///<summary>This table contains all appointment fields for all appointments in the period. It's obtained in Appointments.GetApptFields().</summary>
		public DataTable TableApptFields;
		///<summary>This table contains all appointment fields for all appointments in the period. It's obtained in Appointments.GetApptFields().</summary>
		public DataTable TablePatFields;
		///<summary>Indicator that account has procedures with no ins claim</summary>
		public bool FamHasInsNotSent;
		///<Summary>Will show the highlight around the edges.  For now, this is only used for pinboard.  The ordinary selected appt is set with SelectedAptNum.</Summary>
		public bool IsSelected;


		///<summary></summary>
		public ContrApptSingle() {
			InitializeComponent();// This call is required by the Windows.Forms Form Designer.
			//Info=new InfoApt();
		}

		///<summary></summary>
		protected override void Dispose(bool disposing) {
			if(disposing) {
				if(components != null) {
					components.Dispose();
				}
				if(Shadow!=null) {
					Shadow.Dispose();
				}
			}
			base.Dispose(disposing);
		}

		#region Component Designer generated code

		private void InitializeComponent() {
			// 
			// ContrApptSingle
			// 
			this.Name = "ContrApptSingle";
			this.Size = new System.Drawing.Size(85,72);
			this.Load += new System.EventHandler(this.ContrApptSingle_Load);
			this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ContrApptSingle_MouseDown);

		}
		#endregion

		///<summary></summary>
		protected override void OnPaint(PaintEventArgs pea) {
		}

		///<summary>It is planned to move some of this logic to OnPaint and use a true double buffer.</summary>
		public void CreateShadow() {
			if(this.Parent is ContrApptSheet) {
				bool isVisible=false;
				for(int j=0;j<ApptDrawing.VisOps.Count;j++) {
					if(DataRoww["Op"].ToString()==ApptDrawing.VisOps[j].OperatoryNum.ToString()) {
						isVisible=true;
					}
				}
				if(!isVisible) {
					return;
				}
			}
			if(Shadow!=null) {
				Shadow.Dispose();
				Shadow=null;
			}
			if(Width<4) {
				return;
			}
			if(Height<4) {
				return;
			}
			Shadow=new Bitmap(Width,Height);
			if(PatternShowing==null) {
				PatternShowing="";
			}
			using(Graphics g=Graphics.FromImage(Shadow)) {
				ApptSingleDrawing.DrawEntireAppt(g,DataRoww,PatternShowing,Width,Height,IsSelected,ThisIsPinBoard,SelectedAptNum,
					ApptViewItemL.ApptRows,ApptViewItemL.ApptViewCur,TableApptFields,TablePatFields,8,false);
				this.BackgroundImage=Shadow;
			}
		}

		private void ContrApptSingle_Load(object sender,System.EventArgs e) {
		}

		private void ContrApptSingle_MouseDown(object sender,System.Windows.Forms.MouseEventArgs e) {
			ClickedAptNum=PIn.Long(DataRoww["AptNum"].ToString());
		}
	}//end class
}//end namespace
