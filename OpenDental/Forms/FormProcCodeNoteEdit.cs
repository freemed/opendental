using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{
	/// <summary></summary>
	public class FormProcCodeNoteEdit : System.Windows.Forms.Form{
		private OpenDental.UI.Button butCancel;
		private OpenDental.UI.Button butOK;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private ListBox listProv;
		private Label label15;
		private Button butSlider;
		private TableTimeBar tbTime;
		private Label label11;
		private TextBox textTime2;
		private Label label6;
		private ODtextBox textNote;
		private Label label10;
		public bool IsNew;
		private StringBuilder strBTime;
		public ProcCodeNote NoteCur;
		private bool mouseIsDown;
		private Point mouseOrigin;
		private OpenDental.UI.Button butDelete;
		private Point sliderOrigin;

		///<summary></summary>
		public FormProcCodeNoteEdit()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			Lan.F(this);
			tbTime.CellClicked += new OpenDental.ContrTable.CellEventHandler(tbTime_CellClicked);
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormProcCodeNoteEdit));
			this.listProv = new System.Windows.Forms.ListBox();
			this.label15 = new System.Windows.Forms.Label();
			this.butSlider = new System.Windows.Forms.Button();
			this.label11 = new System.Windows.Forms.Label();
			this.textTime2 = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.label10 = new System.Windows.Forms.Label();
			this.textNote = new OpenDental.ODtextBox();
			this.tbTime = new OpenDental.TableTimeBar();
			this.butOK = new OpenDental.UI.Button();
			this.butCancel = new OpenDental.UI.Button();
			this.butDelete = new OpenDental.UI.Button();
			this.SuspendLayout();
			// 
			// listProv
			// 
			this.listProv.FormattingEnabled = true;
			this.listProv.Location = new System.Drawing.Point(92,51);
			this.listProv.Name = "listProv";
			this.listProv.Size = new System.Drawing.Size(120,199);
			this.listProv.TabIndex = 2;
			// 
			// label15
			// 
			this.label15.Location = new System.Drawing.Point(89,30);
			this.label15.Name = "label15";
			this.label15.Size = new System.Drawing.Size(100,18);
			this.label15.TabIndex = 47;
			this.label15.Text = "Provider";
			this.label15.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// butSlider
			// 
			this.butSlider.BackColor = System.Drawing.SystemColors.ControlDark;
			this.butSlider.Location = new System.Drawing.Point(14,56);
			this.butSlider.Name = "butSlider";
			this.butSlider.Size = new System.Drawing.Size(12,15);
			this.butSlider.TabIndex = 50;
			this.butSlider.UseVisualStyleBackColor = false;
			this.butSlider.MouseDown += new System.Windows.Forms.MouseEventHandler(this.butSlider_MouseDown);
			this.butSlider.MouseMove += new System.Windows.Forms.MouseEventHandler(this.butSlider_MouseMove);
			this.butSlider.MouseUp += new System.Windows.Forms.MouseEventHandler(this.butSlider_MouseUp);
			// 
			// label11
			// 
			this.label11.Location = new System.Drawing.Point(78,621);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(102,16);
			this.label11.TabIndex = 52;
			this.label11.Text = "Minutes";
			// 
			// textTime2
			// 
			this.textTime2.Location = new System.Drawing.Point(12,617);
			this.textTime2.Name = "textTime2";
			this.textTime2.Size = new System.Drawing.Size(60,20);
			this.textTime2.TabIndex = 51;
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(4,1);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(53,39);
			this.label6.TabIndex = 48;
			this.label6.Text = "Time Pattern";
			this.label6.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// label10
			// 
			this.label10.Location = new System.Drawing.Point(91,260);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(148,14);
			this.label10.TabIndex = 53;
			this.label10.Text = "Note";
			this.label10.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// textNote
			// 
			this.textNote.AcceptsReturn = true;
			this.textNote.Location = new System.Drawing.Point(92,278);
			this.textNote.Multiline = true;
			this.textNote.Name = "textNote";
			this.textNote.QuickPasteType = OpenDentBusiness.QuickPasteType.Procedure;
			this.textNote.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textNote.Size = new System.Drawing.Size(540,219);
			this.textNote.TabIndex = 54;
			// 
			// tbTime
			// 
			this.tbTime.BackColor = System.Drawing.SystemColors.Window;
			this.tbTime.Location = new System.Drawing.Point(12,51);
			this.tbTime.Name = "tbTime";
			this.tbTime.ScrollValue = 150;
			this.tbTime.SelectedIndices = new int[0];
			this.tbTime.SelectionMode = System.Windows.Forms.SelectionMode.None;
			this.tbTime.Size = new System.Drawing.Size(15,561);
			this.tbTime.TabIndex = 49;
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.CornerRadius = 4F;
			this.butOK.Location = new System.Drawing.Point(602,567);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75,26);
			this.butOK.TabIndex = 1;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// butCancel
			// 
			this.butCancel.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butCancel.Autosize = true;
			this.butCancel.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butCancel.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butCancel.CornerRadius = 4F;
			this.butCancel.Location = new System.Drawing.Point(602,608);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75,26);
			this.butCancel.TabIndex = 0;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// butDelete
			// 
			this.butDelete.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butDelete.Autosize = true;
			this.butDelete.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butDelete.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butDelete.CornerRadius = 4F;
			this.butDelete.Image = global::OpenDental.Properties.Resources.deleteX;
			this.butDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butDelete.Location = new System.Drawing.Point(207,608);
			this.butDelete.Name = "butDelete";
			this.butDelete.Size = new System.Drawing.Size(82,26);
			this.butDelete.TabIndex = 55;
			this.butDelete.Text = "Delete";
			this.butDelete.Click += new System.EventHandler(this.butDelete_Click);
			// 
			// FormProcCodeNoteEdit
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.ClientSize = new System.Drawing.Size(689,646);
			this.Controls.Add(this.butDelete);
			this.Controls.Add(this.textNote);
			this.Controls.Add(this.label10);
			this.Controls.Add(this.butSlider);
			this.Controls.Add(this.tbTime);
			this.Controls.Add(this.label11);
			this.Controls.Add(this.textTime2);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.label15);
			this.Controls.Add(this.listProv);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.butCancel);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormProcCodeNoteEdit";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Edit ProcedureCode Note";
			this.Load += new System.EventHandler(this.FormProcCodeNoteEdit_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		private void FormProcCodeNoteEdit_Load(object sender,EventArgs e) {
			strBTime=new StringBuilder(NoteCur.ProcTime);
			for(int i=0;i<Providers.List.Length;i++){
				listProv.Items.Add(Providers.GetNameLF(Providers.List[i].ProvNum));
				if(NoteCur.ProvNum==Providers.List[i].ProvNum){
					listProv.SelectedIndex=i;
				}
			}
			textNote.Text=NoteCur.Note;
			FillTime();
		}

		private void FillTime() {
			for(int i=0;i<strBTime.Length;i++) {
				tbTime.Cell[0,i]=strBTime.ToString(i,1);
				tbTime.BackGColor[0,i]=Color.White;
			}
			for(int i=strBTime.Length;i<tbTime.MaxRows;i++) {
				tbTime.Cell[0,i]="";
				tbTime.BackGColor[0,i]=Color.FromName("Control");
			}
			tbTime.Refresh();
			butSlider.Location=new Point(tbTime.Location.X+2
				,(tbTime.Location.Y+strBTime.Length*14+1));
			textTime2.Text=(strBTime.Length*ContrApptSheet.MinPerIncr).ToString();
		}

		private void tbTime_CellClicked(object sender,CellEventArgs e) {
			if(e.Row<strBTime.Length) {
				if(strBTime[e.Row]=='/') {
					strBTime.Replace('/','X',e.Row,1);
				}
				else {
					strBTime.Replace(strBTime[e.Row],'/',e.Row,1);
				}
			}
			FillTime();
		}

		private void butSlider_MouseDown(object sender,System.Windows.Forms.MouseEventArgs e) {
			mouseIsDown=true;
			mouseOrigin=new Point(e.X+butSlider.Location.X
				,e.Y+butSlider.Location.Y);
			sliderOrigin=butSlider.Location;

		}

		private void butSlider_MouseMove(object sender,System.Windows.Forms.MouseEventArgs e) {
			if(!mouseIsDown)
				return;
			//tempPoint represents the new location of button of smooth dragging.
			Point tempPoint=new Point(sliderOrigin.X
				,sliderOrigin.Y+(e.Y+butSlider.Location.Y)-mouseOrigin.Y);
			int step=(int)(Math.Round((Decimal)(tempPoint.Y-tbTime.Location.Y)/14));
			if(step==strBTime.Length)
				return;
			if(step<1)
				return;
			if(step>tbTime.MaxRows-1)
				return;
			if(step>strBTime.Length) {
				strBTime.Append('/');
			}
			if(step<strBTime.Length) {
				strBTime.Remove(step,1);
			}
			FillTime();
		}

		private void butSlider_MouseUp(object sender,System.Windows.Forms.MouseEventArgs e) {
			mouseIsDown=false;
		}

		private void butDelete_Click(object sender,EventArgs e) {
			if(IsNew){
				DialogResult=DialogResult.Cancel;
				return;
			}
			if(!MsgBox.Show(this,true,"Delete?")){
				return;
			}
			ProcCodeNotes.Delete(NoteCur.ProcCodeNoteNum);
			DialogResult=DialogResult.OK;
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			if(listProv.SelectedIndex==-1){
				MsgBox.Show(this,"Please select a provider first.");
				return;
			}
			NoteCur.ProcTime=strBTime.ToString();
			NoteCur.ProvNum=Providers.List[listProv.SelectedIndex].ProvNum;
			NoteCur.Note=textNote.Text;
			if(IsNew){
				ProcCodeNotes.Insert(NoteCur);
			}
			else{
				ProcCodeNotes.Update(NoteCur);
			}
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		

		


	}
}





















