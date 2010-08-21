using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental {
	public partial class PhoneTile:UserControl {
		private Phone phoneCur;
		public TimeSpan TimeDelta;
		///<summary></summary>
		[Category("Property Changed"),Description("Event raised when user wants to go to a patient or related object.")]
		public event EventHandler GoToChanged=null;
		///<summary></summary>
		[Category("Property Changed"),Description("Event raised when certain controls are selected on this tile related to menu events.")]
		public event EventHandler SelectedTileChanged=null;
		///<summary>Object passed in from parent form.  Event will be fired from that form.</summary>
		public ContextMenuStrip MenuNumbers;
		///<summary>Object passed in from parent form.  Event will be fired from that form.</summary>
		public ContextMenuStrip MenuStatus;
		private bool layoutHorizontal;

		public PhoneTile() {
			InitializeComponent();
		}

		public Phone PhoneCur {
			get {
				return phoneCur; 
			}
			set {
				if(DesignMode) {
					return;
				}
				phoneCur = value;
				if(phoneCur==null) {
					pictureWebCam.Image=null;//or just make it not visible?
					pictureInUse.Visible=false;
					labelExtensionName.Text="";
					labelStatusAndNote.Text="";
					labelTime.Text="";
					labelTime.BackColor=this.BackColor;
					labelCustomer.Text="";
				}
				else {
					if(phoneCur.ClockStatus==ClockStatusEnum.Home
						|| phoneCur.ClockStatus==ClockStatusEnum.None
						|| phoneCur.ClockStatus==ClockStatusEnum.Off)
					{
						pictureWebCam.Image=null;
					}
					else if(phoneCur.ClockStatus==ClockStatusEnum.Break
						|| phoneCur.ClockStatus==ClockStatusEnum.Lunch)
					{
						Bitmap bmp=new Bitmap(pictureWebCam.Width,pictureWebCam.Height);
						Graphics g=Graphics.FromImage(bmp);
						try{
							g.FillRectangle(SystemBrushes.Control,0,0,bmp.Width,bmp.Height);
							string strStat=phoneCur.ClockStatus.ToString();
							if(phoneCur.ClockStatus==ClockStatusEnum.None){
								strStat="";
							}
							SizeF sizef=g.MeasureString(strStat,labelStatusAndNote.Font);
							g.DrawString(strStat,labelStatusAndNote.Font,SystemBrushes.GrayText,(bmp.Width-sizef.Width)/2,(bmp.Height-sizef.Height)/2);
							pictureWebCam.Image=(Image)bmp.Clone();
						}
						finally{
							g.Dispose();
							g=null;
							bmp.Dispose();
							bmp=null;
						}
					}
					else{
						pictureWebCam.Image=PIn.Bitmap(phoneCur.WebCamImage);
					}
					if(phoneCur.Description=="") {
						pictureInUse.Visible=false;
					}
					else {
						pictureInUse.Visible=true;
					}
					labelExtensionName.Text=phoneCur.Extension.ToString()+" - "+phoneCur.EmployeeName;
					string str="";
					if(phoneCur.ClockStatus!=ClockStatusEnum.None) {
						str+=phoneCur.ClockStatus.ToString();
					}
					labelStatusAndNote.Text=str;
					DateTime dateTimeStart=phoneCur.DateTimeStart;
					if(dateTimeStart.Date==DateTime.Today) {
						TimeSpan span=DateTime.Now-dateTimeStart+TimeDelta;
						DateTime timeOfDay=DateTime.Today+span;
						labelTime.Text=timeOfDay.ToString("H:mm:ss");
					}
					else {
						labelTime.Text="";
					}
					labelTime.BackColor=phoneCur.ColorBar;
					labelCustomer.Text=phoneCur.CustomerNumber;
				}
			}
		}

		[Category("Layout"),Description("Set true for horizontal layout and false for vertical.")]
		public bool LayoutHorizontal{
			get{
				return layoutHorizontal;
			}
			set{
				layoutHorizontal=value;
				if(layoutHorizontal){
					pictureWebCam.Location=new Point(0,0);
					pictureInUse.Location=new Point(52,10);
					labelExtensionName.Location=new Point(73,11);
					labelStatusAndNote.Location=new Point(182,4);
					labelStatusAndNote.TextAlign=ContentAlignment.MiddleLeft;
					labelStatusAndNote.Height=31;
					labelTime.Location=new Point(273,11);
					labelTime.Size=new Size(56,16);
					labelCustomer.Location=new Point(331,11);
					labelCustomer.Size=new Size(147,16);
					labelCustomer.TextAlign=ContentAlignment.MiddleLeft;
				}
				else{//vertical
					pictureWebCam.Location=new Point(51,3);
					pictureInUse.Location=new Point(14,43);
					labelExtensionName.Location=new Point(37,43);
					labelStatusAndNote.Location=new Point(0,61);
					labelStatusAndNote.TextAlign=ContentAlignment.MiddleCenter;
					labelStatusAndNote.Size=new Size(150,16);
					labelTime.Location=new Point(0,81);
					labelTime.Size=new Size(150,16);
					labelCustomer.Location=new Point(0,99);
					labelCustomer.Size=new Size(150,16);
					labelCustomer.TextAlign=ContentAlignment.MiddleCenter;
				}
			}
		}

		protected override Size DefaultSize {
			get {
				if(layoutHorizontal){
					return new Size(595,37);
				}
				else{//vertical
					return new Size(150,122);
				}
			}
		}
		
		private void labelCustomer_MouseClick(object sender,MouseEventArgs e) {
			if((e.Button & MouseButtons.Right)==MouseButtons.Right) {
				return;
			}
			OnGoToChanged();
		}

		protected void OnGoToChanged() {
			if(GoToChanged!=null) {
				GoToChanged(this,new EventArgs());
			}
		}

		private void labelCustomer_MouseUp(object sender,MouseEventArgs e) {
			if(e.Button!=MouseButtons.Right) {
				return;
			}
			if(phoneCur==null) {
				return;
			}
			OnSelectedTileChanged();
			MenuNumbers.Show(labelCustomer,e.Location);	
		}

		private void labelStatusAndNote_MouseUp(object sender,MouseEventArgs e) {
			if(e.Button!=MouseButtons.Right) {
				return;
			}
			if(phoneCur==null) {
				return;
			}
			if(phoneCur.EmployeeNum==0) {
				return;
			}
			OnSelectedTileChanged();
			MenuStatus.Show(labelStatusAndNote,e.Location);		
		}

		protected void OnSelectedTileChanged() {
			if(SelectedTileChanged!=null) {
				SelectedTileChanged(this,new EventArgs());
			}
		}

		


	}
}
