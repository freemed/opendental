using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental {
	public partial class MapAreaDisplayLabelControl:DraggableControl {

		#region Member not available in designer.

		public MapArea ClinicMapItem=new MapArea();

		#endregion Member not available in designer.

		#region Events

		public event EventHandler MapAreaDisplayLabelChanged;

		#endregion

		public MapAreaDisplayLabelControl() {
			InitializeComponent();
		}

		public MapAreaDisplayLabelControl(MapArea displayLabel,Font font,Color foreColor,Color backColor,Point location,int pixelsPerFoot,bool allowDragging) :this() {
			displayLabel.ItemType=MapItemType.DisplayLabel;
			ClinicMapItem=displayLabel;
			Font=font;
			ForeColor=foreColor;
			BackColor=backColor;
			Location=location;
			Size=GetDrawingSize(this,pixelsPerFoot);
			AllowDragging=allowDragging;			
			Name=ClinicMapItem.MapAreaNum.ToString();
		}

		///<summary>Make the control just tall enough to fit the font and the lesser of the user defined width vs the actual width.</summary>
		public static Size GetDrawingSize(MapAreaDisplayLabelControl displayLabel,int pixelsPerFoot) {
			Size controlSize=MapAreaPanel.GetScreenSize(displayLabel.ClinicMapItem.Width,displayLabel.ClinicMapItem.Height,pixelsPerFoot);
			Size textSize=TextRenderer.MeasureText(displayLabel.ClinicMapItem.Description,displayLabel.Font);
			return new Size(Math.Min(controlSize.Width,textSize.Width),textSize.Height);
		}

		private void MapAreaDisplayLabelControl_Paint(object sender,PaintEventArgs e) {
			Brush brushBack=new SolidBrush(BackColor);
			Brush brushFore=new SolidBrush(ForeColor);
			try {
				e.Graphics.FillRectangle(brushBack,this.ClientRectangle);
				StringFormat stringFormat=new StringFormat(StringFormatFlags.NoWrap);
				stringFormat.Alignment=StringAlignment.Near;
				stringFormat.LineAlignment=StringAlignment.Center;
				Rectangle rcOuter=this.ClientRectangle;
				e.Graphics.DrawString(ClinicMapItem.Description,Font,brushFore,rcOuter,stringFormat);
			}
			catch { }
			finally {
				brushBack.Dispose();
				brushFore.Dispose();
			}
		}

		private void MapAreaDisplayLabelControl_DoubleClick(object sender,System.EventArgs e) {
			//edit this display label item
			FormMapAreaEdit FormEP=new FormMapAreaEdit();
			FormEP.MapItem=this.ClinicMapItem;
			if(FormEP.ShowDialog(this)!=DialogResult.OK) {
				return;
			}
			if(MapAreaDisplayLabelChanged!=null) { //let anyone interested know that this display label was edited
				MapAreaDisplayLabelChanged(this,new EventArgs());
			}
		}
	}
}
