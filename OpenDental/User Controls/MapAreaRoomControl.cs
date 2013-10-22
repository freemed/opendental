using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental {
	public partial class MapAreaRoomControl:DraggableControl {

		#region Member not available in designer.
		
		public MapArea MapAreaItem=new MapArea();		

		#endregion

		#region Properties available in designer.

		[Category("Employee Info")]
		[Description("Primary Key From employee Table")]
		public long EmployeeNum { get; set; }		

		[Category("Employee Info")]
		[Description("Employee's Name")]
		public string EmployeeName { get; set; }
		
		[Category("Employee Info")]
		[Description("Employee's Phone Extension #")]
		public string Extension { get; set; }
		
		[Category("Employee Info")]
		[Description("Elapsed Time Since Last Status Change")]		
		public string Elapsed { get; set; }
		
		[Category("Employee Info")]
		[Description("Current Employee Status")]		
		public string Status { get; set; }
		
		[Category("Employee Info")]
		[Description("Image Indicating Employee's Current Phone Status")]		
		public Image PhoneImage { get; set; }

		///<summary>Set when flashing starts so we know what inner color to go back to.</summary>
		private Color _innerColorRestore=Color.FromArgb(128,Color.Red);
		private Color DefaultOuterColor=Color.Red;
		[Category("Appearance")]
		[Description("Exterior Border Color")]
		public Color OuterColor
		{ 
			get {
				return DefaultOuterColor; 
			} 
			set { 
				DefaultOuterColor=value; 
				Invalidate(); 
			} 
		}

		///<summary>Set when flashing starts so we know what outer color to go back to.</summary>
		private Color _outerColorRestore=Color.Red;
		private Color DefaultInnerColor=Color.FromArgb(128,Color.Red);
		[Category("Appearance")]
		[Description("Interior Fill Color")]
		public Color InnerColor {
			get {
				return DefaultInnerColor; 
			} 
			set { 
				DefaultInnerColor=value;
				Invalidate(); 
			} 
		}

		private bool IsEmpty=false;
		[Category("Appearance")]
		[Description("No Extension Assigned")]
		public bool Empty {
			get {
				return IsEmpty;
			}
			set {
				IsEmpty=value;
				Invalidate();
			}
		}
		
		private bool _allowEdit=false;
		[Category("Behavior")]
		[Description("Double-click will open editor")]
		public bool AllowEdit {
			get {
				return _allowEdit;
			}
			set {
				_allowEdit=value;
			}
		}

		public bool IsFlashing {
			get {
				return timerFlash.Enabled;
			}
		}

		#endregion

		#region Events

		public event EventHandler MapAreaRoomChanged;
		
		#endregion

		#region Ctor

		///<summary>Default. Must be called by all other ctors as we will call InitializeComponent here.</summary>
		public MapAreaRoomControl() {
			InitializeComponent();
		}

		///<summary>Takes all required fields as input. Suggest using this version when adding a cubicle to a ClinicMapPanel.</summary>
		public MapAreaRoomControl(MapArea cubicle,string elapsed,string employeeName,long employeeNum,string extension,string status,Font font,Color innerColor,Color outerColor,Color backColor,Point location,Size size,Image phoneImage,bool allowDragging,bool allowEdit) :this() {
			cubicle.ItemType=MapItemType.Room;
			MapAreaItem=cubicle;
			Elapsed = elapsed;
			EmployeeName = employeeName;
			EmployeeNum = employeeNum;
			Extension = extension;
			Status = status; 
			Font = font;
			Location = location;
			Size=size;
			InnerColor = innerColor;
			OuterColor = outerColor;
			BackColor=backColor;
			PhoneImage = phoneImage;
			AllowDragging=allowDragging;
			AllowEdit=allowEdit;
			Name=MapAreaItem.MapAreaNum.ToString();
		}

		#endregion

		#region Drawing

		public void StartFlashing() {
			if(IsFlashing) { //already on
				return;
			}
			//save the colors
			_outerColorRestore=OuterColor;
			_innerColorRestore=InnerColor;
			timerFlash.Start();
		}

		public void StopFlashing() {
			if(!IsFlashing) { //already off
				return;
			}
			timerFlash.Stop();
			OuterColor=_outerColorRestore;
			InnerColor=_innerColorRestore;
		}

		private void timerFlash_Tick(object sender,EventArgs e) {
			//flip inner and outer colors
			if(OuterColor==_outerColorRestore) {
				OuterColor=_innerColorRestore;
				InnerColor=_outerColorRestore;
			}
			else {
				OuterColor=_outerColorRestore;
				InnerColor=_innerColorRestore;
			}
		}

		private void MapAreaRoomControl_Paint(object sender,PaintEventArgs e) {
			Brush brushInner=new SolidBrush(Empty?Color.FromArgb(20,Color.Gray):InnerColor);
			Brush brushText=new SolidBrush(Empty?Color.FromArgb(128,Color.Gray):ForeColor);
			Pen penOuter=new Pen(Empty?Color.FromArgb(128,Color.Gray):OuterColor,10);			
			try {
				Rectangle rcOuter=this.ClientRectangle;
				//clear control canvas
				e.Graphics.Clear(this.BackColor);
				//draw border
				e.Graphics.DrawRectangle(penOuter,rcOuter);
				//deflate to drawable region
				rcOuter.Inflate(-5,-5);
				//fill interior
				e.Graphics.FillRectangle(brushInner,rcOuter);
				StringFormat stringFormat=new StringFormat(StringFormatFlags.NoWrap);
				stringFormat.Alignment=StringAlignment.Center;
				stringFormat.LineAlignment=StringAlignment.Center;
				if(this.Empty) { //empty room so gray out and return
					e.Graphics.DrawString("EMPTY",Font,brushText,rcOuter,stringFormat);
					return;
				}
				//4 rows of data
				int rowHeight=rcOuter.Height/4;
				//row 1 - employee name
				FitText(EmployeeName,Font,brushText,new RectangleF(rcOuter.X,rcOuter.Y,rcOuter.Width,rowHeight),stringFormat,e.Graphics);
				//row 2 (left) - employee extension
				FitText(Extension,Font,brushText,new RectangleF(rcOuter.X,rcOuter.Y+rowHeight,rcOuter.Width/2,rowHeight),stringFormat,e.Graphics);
				//row 2 (right) - phone icon
				if(PhoneImage!=null) {
					using(Bitmap bitmap=new Bitmap(PhoneImage)) {//center the image on the right-hand side of row 2
						Rectangle rectImage=new Rectangle(rcOuter.X+(rcOuter.Width/2),rcOuter.Y+rowHeight,rcOuter.Width/2,rowHeight);
						if(bitmap.Height<rectImage.Height) {
							rectImage.Y-=(bitmap.Height-rectImage.Height)/2;
						}
						if(bitmap.Width<rectImage.Width) {
							rectImage.X-=(bitmap.Width-rectImage.Width)/2;
						}
						e.Graphics.DrawImageUnscaled(PhoneImage,rectImage);
					}					
				}
				//row 3 - elapsed time
				FitText(Elapsed,Font,brushText,new RectangleF(rcOuter.X,rcOuter.Y+(rowHeight*2),rcOuter.Width,rowHeight),stringFormat,e.Graphics);
				//row 4 - employee status
				FitText(Status,Font,brushText,new RectangleF(rcOuter.X,rcOuter.Y+(rowHeight*3),rcOuter.Width,rowHeight),stringFormat,e.Graphics);
			}
			catch { }
			finally {
				brushInner.Dispose();
				brushText.Dispose();
				penOuter.Dispose();
			}
		}

		///<summary>Replaces Graphics.DrawString. Finds a suitable font size to fit the text to the bounding rectangle.</summary>
		public static void FitText(string text,Font font,Brush brush,RectangleF rectF,StringFormat stringFormat,Graphics graphics) {
			float emSize=font.Size;
			while(true) {
				using(Font newFont=new Font(font.FontFamily,emSize,font.Style)) {
					Size size=TextRenderer.MeasureText(text,newFont);
					if(size.Width<rectF.Width //does our new font fit?
						|| (font.Size-emSize)>4) //only allow maximum of 4 point reduction
					{
						graphics.DrawString(text,newFont,brush,rectF,stringFormat);
						return;
					}
				}
				//text didn't fit so decrement the font size and try again
				emSize-=.1F;
			}			
		}

		#endregion

		#region Mouse events

		private void MapAreaRoomControl_DoubleClick(object sender,EventArgs e) {
			if(!AllowEdit) {
				return;
			}
			//edit this room
			FormMapAreaEdit FormEP=new FormMapAreaEdit();
			FormEP.MapItem=this.MapAreaItem;
			if(FormEP.ShowDialog(this)!=DialogResult.OK) {
				return;
			}
			if(MapAreaRoomChanged!=null) { //let anyone interested know that this cubicle was edited
				MapAreaRoomChanged(this,new EventArgs());
			}
		}

		#endregion
	}
}
