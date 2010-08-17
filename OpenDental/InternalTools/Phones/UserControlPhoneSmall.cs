using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental {
	public partial class UserControlPhoneSmall:UserControl {
		private List<Phone> phoneList;

		public List<Phone> PhoneList {
			set { 
				phoneList = value;
				Invalidate();
			}
		}

		public UserControlPhoneSmall() {
			InitializeComponent();
		}

		private void UserControlPhoneSmall_Paint(object sender,PaintEventArgs e) {
			Graphics g=e.Graphics;
			g.FillRectangle(SystemBrushes.Control,this.Bounds);
			if(phoneList==null){
				return;
			}
			float wh=20f;
			float x=0f;
			float y=0f;
			for(int i=0;i<phoneList.Count;i++){
				using(SolidBrush brush=new SolidBrush(phoneList[i].ColorBar)){
					g.FillRectangle(brush,x*wh,y*wh,wh,wh);
				}
				x++;
				if(x>=7){
					x=0f;
					y++;
				}
			}
			//horiz lines
			for(int i=0;i<3;i++){
				g.DrawLine(Pens.Black,0,i*wh,Width,i*wh);
			}
			g.DrawLine(Pens.Black,0,Height-1,Width,Height-1);
			//vert
			for(int i=0;i<7;i++){
				g.DrawLine(Pens.Black,i*wh,0,i*wh,Height);
			}
			g.DrawLine(Pens.Black,Width-1,0,Width-1,Height);
		}




	}
}
