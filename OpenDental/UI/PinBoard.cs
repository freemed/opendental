using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental.UI {
	public partial class PinBoard:Control {
		private List<ContrApptSingle> apptList;
		private int selectedIndex;
		//private bool MouseIsDown;
		///<summary></summary>
		[Category("Property Changed"),Description("Event raised when user _clicks_ to select a different appointment.")]
		public event EventHandler SelectedIndexChanged=null;

		public PinBoard() {
			InitializeComponent();
			apptList=new List<ContrApptSingle>();
		}

		///<Summary>Do not make changes her to the list.  This is for read-only purposes.</Summary>
		public List<ContrApptSingle> ApptList{
			get{
				return apptList;
			}
		}

		public ContrApptSingle SelectedAppt{
			get{
				if(selectedIndex==-1){
					return null;
				}
				return apptList[selectedIndex];
			}
		}

		public int SelectedIndex{
			get{
				return selectedIndex;
			}
		}
		/*
			set{
				selectedIndex=value;
				for(int i=0;i<apptList.Count;i++){
					if(i==selectedIndex){
						apptList[i].IsSelected=true;
					}
					else{
						apptList[i].IsSelected=false;
					}
				}
				Invalidate();
			}
		}*/

		protected void OnSelectedIndexChanged() {
			if(SelectedIndexChanged!=null) {
				SelectedIndexChanged(this,new EventArgs());
			}
		}

		public void SelectedSetNone(){
			selectedIndex=-1;
			for(int i=0;i<apptList.Count;i++) {
				apptList[i].IsSelected=false;
			}
			Invalidate();
		}

		///<Summary>Supply a datarow that contains all the database values needed for the appointment that is being added.</Summary>
		public void AddAppointment(DataRow row){
			ContrApptSingle PinApptSingle=new ContrApptSingle();
			PinApptSingle.ThisIsPinBoard=true;
			PinApptSingle.DataRoww=row;
			PinApptSingle.SetSize();
			PinApptSingle.Width=Width-2;
			PinApptSingle.IsSelected=true;
			PinApptSingle.Location=new Point(0,13*apptList.Count);
			apptList.Add(PinApptSingle);
			selectedIndex=apptList.Count-1;
			Invalidate();
		}

		public void ClearSelected(){
			if(selectedIndex==-1){
				return;
			}
			apptList.RemoveAt(selectedIndex);
			if(apptList.Count>=selectedIndex+1){
				//no change to selectedIndex
				apptList[selectedIndex].IsSelected=true;
			}
			else if(apptList.Count>0){
				//select the last one
				selectedIndex=apptList.Count-1;
				apptList[selectedIndex].IsSelected=true;
			}
			else{
				selectedIndex=-1;
			}
			//reset locations
			for(int i=0;i<apptList.Count;i++) {
				apptList[i].Location=new Point(0,13*i);
			}
			Invalidate();
		}

		/*
		///<Summary>Sets all appointments to specified value.</Summary>
		public void SetSelected(bool setValue){
			selectedIndices.Clear();
			if(setValue) {//select all
				for(int i=0;i<apptList.Count;i++) {
					selectedIndices.Add(i);
					apptList[i].IsSelected=true;
				}
			}
			else{//deselect all

			}
			Invalidate();
		}*/

		protected override void OnPaint(PaintEventArgs pe) {
			Graphics g=pe.Graphics;
			g.Clear(Color.White);
			g.DrawRectangle(Pens.Black,0,0,Width-1,Height-1);
			for(int i=0;i<apptList.Count;i++){
				apptList[i].CreateShadow();
				g.DrawImage(apptList[i].Shadow,0,i*13);
			}
			if(apptList.Count==0){
				StringFormat format=new StringFormat();
				format.Alignment=StringAlignment.Center;
				format.LineAlignment=StringAlignment.Center;
				g.DrawString(Lan.g(this,"Drag Appointments to this PinBoard"),Font,Brushes.Gray,new RectangleF(0,0,Width,Height),format);
			}
			base.OnPaint(pe);
		}

		#region Mouse
		protected override void OnMouseDown(MouseEventArgs e) {
			//figure out which appt mouse is on.  Start at end and work backwards
			int index=-1;
			for(int i=apptList.Count-1;i>=0;i--){
				if(e.Y<apptList[i].Top || e.Y>apptList[i].Bottom){
					continue;
				}
				index=i;
				break;
			}
			if(index==-1){
				base.OnMouseDown(e);
				return;
			}
			if(index==selectedIndex){//no change
				base.OnMouseDown(e);
				return;//for now.
			}
			selectedIndex=index;
			for(int i=0;i<apptList.Count;i++){
				if(i==selectedIndex){
					apptList[i].IsSelected=true;
				}
				else{
					apptList[i].IsSelected=false;
				}
			}			
			Invalidate();
			OnSelectedIndexChanged();
			base.OnMouseDown(e);
		}

		protected override void OnMouseLeave(EventArgs e) {
			//MouseIsDown=false;
			base.OnMouseLeave(e);
		}

		protected override void OnMouseUp(MouseEventArgs e) {
			//MouseIsDown=false;
			base.OnMouseUp(e);
		}
		#endregion
	}
}
