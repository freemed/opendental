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
					pictureWebCam.Image=PIn.Bitmap(phoneCur.WebCamImage);
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
