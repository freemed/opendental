using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace OpenDental.UI {
	public partial class ListBoxClickable:ListBox {
		///<summary>Will be -1 unless mouse is currently hovering over an item.</summary>
		private int hotItem=-1;

		public ListBoxClickable() {
			InitializeComponent();
			this.DrawMode=DrawMode.OwnerDrawFixed;
			this.ItemHeight=15;
			this.SelectionMode=SelectionMode.None;
		}

		protected override void OnMouseMove(MouseEventArgs e) {
			base.OnMouseMove(e);
			int hotPrevious=hotItem;
			hotItem=this.IndexFromPoint(e.Location);
			if(hotItem!=hotPrevious) {
				if(hotPrevious!=-1) {
					this.Invalidate(this.GetItemRectangle(hotPrevious));
				}
				if(hotItem!=-1) {
					this.Invalidate(this.GetItemRectangle(hotItem));
				}
			}
		}

		protected override void OnMouseLeave(EventArgs e) {
			base.OnMouseLeave(e);
			int hotPrevious=hotItem;
			hotItem=-1;
			if(hotItem!=hotPrevious) {
				if(hotPrevious!=-1) {
					this.Invalidate(this.GetItemRectangle(hotPrevious));
				}
				if(hotItem!=-1) {
					this.Invalidate(this.GetItemRectangle(hotItem));
				}
			}
		}

		protected override void OnMouseDown(MouseEventArgs e) {
			base.OnMouseDown(e);
		}

		protected override void OnDrawItem(DrawItemEventArgs e) {
			base.OnDrawItem(e);
			e.Graphics.FillRectangle(new SolidBrush(Color.White),e.Bounds);
			SolidBrush brush=new SolidBrush(Color.Black);
			if(hotItem==e.Index) {
				brush=new SolidBrush(Color.Firebrick);
			}
			Font font=new Font(e.Font,FontStyle.Underline);
			if(e.Index<=this.Items.Count-1){//prevents index of 0 from attempting to draw in designer.
				e.Graphics.DrawString(this.Items[e.Index].ToString(),font,brush,e.Bounds);
			}
		}
		


	}
}

