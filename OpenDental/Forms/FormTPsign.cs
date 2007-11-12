using Microsoft.CSharp;
//using Microsoft.Vsa;
using System.CodeDom.Compiler;
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Reflection;
using System.Windows.Forms;
using OpenDentBusiness;
using OpenDental.UI;

namespace OpenDental{
	///<summary></summary>
	public class FormTPsign : System.Windows.Forms.Form{
		private System.ComponentModel.IContainer components;
		private System.Windows.Forms.PrintDialog printDialog2;
		///<summary></summary>
		private int TotalPages;
		private OpenDental.UI.ODToolBar ToolBarMain;
		private System.Windows.Forms.ImageList imageListMain;
		private System.Windows.Forms.PrintPreviewControl previewContr;
		///<summary></summary>
		private PrintDocument Document;
		private SignatureBox sigBox;
		private Panel panelSig;
		private Label label1;
		private OpenDental.UI.Button butCancel;
		private OpenDental.UI.Button butOK;
		///<summary></summary>
		//private PrintSituation Sit;

		///<summary></summary>
		public FormTPsign(PrintDocument document,int totalPages,TreatPlan TPcur){
			InitializeComponent();// Required for Windows Form Designer support
			//Sit=sit;
			Document=document;
			TotalPages=totalPages;
		}

		/// <summary>Clean up any resources being used.</summary>
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormTPsign));
			this.printDialog2 = new System.Windows.Forms.PrintDialog();
			this.imageListMain = new System.Windows.Forms.ImageList(this.components);
			this.previewContr = new System.Windows.Forms.PrintPreviewControl();
			this.panelSig = new System.Windows.Forms.Panel();
			this.label1 = new System.Windows.Forms.Label();
			this.butCancel = new OpenDental.UI.Button();
			this.butOK = new OpenDental.UI.Button();
			this.sigBox = new OpenDental.UI.SignatureBox();
			this.ToolBarMain = new OpenDental.UI.ODToolBar();
			this.panelSig.SuspendLayout();
			this.SuspendLayout();
			// 
			// imageListMain
			// 
			this.imageListMain.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListMain.ImageStream")));
			this.imageListMain.TransparentColor = System.Drawing.Color.Transparent;
			this.imageListMain.Images.SetKeyName(0,"");
			this.imageListMain.Images.SetKeyName(1,"");
			this.imageListMain.Images.SetKeyName(2,"");
			// 
			// previewContr
			// 
			this.previewContr.AutoZoom = false;
			this.previewContr.Location = new System.Drawing.Point(10,41);
			this.previewContr.Name = "previewContr";
			this.previewContr.Size = new System.Drawing.Size(806,423);
			this.previewContr.TabIndex = 6;
			// 
			// panelSig
			// 
			this.panelSig.Controls.Add(this.butCancel);
			this.panelSig.Controls.Add(this.butOK);
			this.panelSig.Controls.Add(this.label1);
			this.panelSig.Controls.Add(this.sigBox);
			this.panelSig.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panelSig.Location = new System.Drawing.Point(0,554);
			this.panelSig.Name = "panelSig";
			this.panelSig.Size = new System.Drawing.Size(842,100);
			this.panelSig.TabIndex = 92;
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif",9F,System.Drawing.FontStyle.Bold,System.Drawing.GraphicsUnit.Point,((byte)(0)));
			this.label1.Location = new System.Drawing.Point(7,4);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(153,41);
			this.label1.TabIndex = 92;
			this.label1.Text = "Please Sign Here --->";
			this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// butCancel
			// 
			this.butCancel.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butCancel.Autosize = true;
			this.butCancel.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butCancel.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butCancel.CornerRadius = 4F;
			this.butCancel.Location = new System.Drawing.Point(658,53);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75,25);
			this.butCancel.TabIndex = 94;
			this.butCancel.Text = "Cancel";
			this.butCancel.UseVisualStyleBackColor = true;
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.CornerRadius = 4F;
			this.butOK.Location = new System.Drawing.Point(562,53);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75,25);
			this.butOK.TabIndex = 93;
			this.butOK.Text = "OK";
			this.butOK.UseVisualStyleBackColor = true;
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// sigBox
			// 
			this.sigBox.Location = new System.Drawing.Point(162,3);
			this.sigBox.Name = "sigBox";
			this.sigBox.Size = new System.Drawing.Size(394,91);
			this.sigBox.TabIndex = 91;
			// 
			// ToolBarMain
			// 
			this.ToolBarMain.Dock = System.Windows.Forms.DockStyle.Top;
			this.ToolBarMain.ImageList = this.imageListMain;
			this.ToolBarMain.Location = new System.Drawing.Point(0,0);
			this.ToolBarMain.Name = "ToolBarMain";
			this.ToolBarMain.Size = new System.Drawing.Size(842,25);
			this.ToolBarMain.TabIndex = 5;
			this.ToolBarMain.ButtonClick += new OpenDental.UI.ODToolBarButtonClickEventHandler(this.ToolBarMain_ButtonClick);
			// 
			// FormTPsign
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.ClientSize = new System.Drawing.Size(842,654);
			this.Controls.Add(this.panelSig);
			this.Controls.Add(this.ToolBarMain);
			this.Controls.Add(this.previewContr);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "FormTPsign";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Report";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.Layout += new System.Windows.Forms.LayoutEventHandler(this.FormReport_Layout);
			this.Load += new System.EventHandler(this.FormPrintPreview_Load);
			this.panelSig.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormPrintPreview_Load(object sender, System.EventArgs e) {
			LayoutToolBar();
			ToolBarMain.Buttons["FullPage"].Pushed=true;
			previewContr.Location=new Point(0,ToolBarMain.Bottom);
			previewContr.Size=new Size(ClientRectangle.Width,ClientRectangle.Height-ToolBarMain.Height-panelSig.Height);
			if(Document.DefaultPageSettings.PaperSize.Height==0) {
				Document.DefaultPageSettings.PaperSize=new PaperSize("default",850,1100);
			}
			SetSize();
			previewContr.Document=Document;
			ToolBarMain.Buttons["PageNum"].Text=(previewContr.StartPage+1).ToString()
				+" / "+TotalPages.ToString();
		}

		private void SetSize(){
			if(ToolBarMain.Buttons["FullPage"].Pushed){
				//if document fits within window, then don't zoom it bigger; leave it at 100%
				if(Document.DefaultPageSettings.PaperSize.Height<previewContr.ClientSize.Height
					&& Document.DefaultPageSettings.PaperSize.Width<previewContr.ClientSize.Width) {
					previewContr.Zoom=1;
				}
				//if document ratio is taller than screen ratio, shrink by height.
				else if(Document.DefaultPageSettings.PaperSize.Height
					/Document.DefaultPageSettings.PaperSize.Width
					> previewContr.ClientSize.Height / previewContr.ClientSize.Width) {
					previewContr.Zoom=((double)previewContr.ClientSize.Height
						/(double)Document.DefaultPageSettings.PaperSize.Height);
				}
				//otherwise, shrink by width
				else {
					previewContr.Zoom=((double)previewContr.ClientSize.Width
						/(double)Document.DefaultPageSettings.PaperSize.Width);
				}
			}
			else{//100%
				previewContr.Zoom=1;
			}
		}

		///<summary>Causes the toolbar to be laid out again.</summary>
		public void LayoutToolBar(){
			ToolBarMain.Buttons.Clear();
			//ToolBarMain.Buttons.Add(new ODToolBarButton(Lan.g(this,"Print"),0,"","Print"));
			//ToolBarMain.Buttons.Add(new ODToolBarButton(ODToolBarButtonStyle.Separator));
			ToolBarMain.Buttons.Add(new ODToolBarButton("",1,"Go Back One Page","Back"));
			ODToolBarButton button=new ODToolBarButton("",-1,"","PageNum");
			button.Style=ODToolBarButtonStyle.Label;
			ToolBarMain.Buttons.Add(button);
			ToolBarMain.Buttons.Add(new ODToolBarButton("",2,"Go Forward One Page","Fwd"));
			button=new ODToolBarButton(Lan.g(this,"FullPage"),-1,Lan.g(this,"FullPage"),"FullPage");
			button.Style=ODToolBarButtonStyle.ToggleButton;
			ToolBarMain.Buttons.Add(button);
			button=new ODToolBarButton(Lan.g(this,"100%"),-1,Lan.g(this,"100%"),"100");
			button.Style=ODToolBarButtonStyle.ToggleButton;
			ToolBarMain.Buttons.Add(button);
			//ToolBarMain.Buttons.Add(new ODToolBarButton(ODToolBarButtonStyle.Separator));
			//ToolBarMain.Buttons.Add(new ODToolBarButton(Lan.g(this,"Close"),-1,"Close This Window","Close"));
		}

		private void FormReport_Layout(object sender, System.Windows.Forms.LayoutEventArgs e) {
			previewContr.Width=ClientSize.Width;	
		}

		private void ToolBarMain_ButtonClick(object sender, OpenDental.UI.ODToolBarButtonClickEventArgs e) {
			//MessageBox.Show(e.Button.Tag.ToString());
			switch(e.Button.Tag.ToString()){
				case "Print":
					OnPrint_Click();
					break;
				case "Back":
					OnBack_Click();
					break;
				case "Fwd":
					OnFwd_Click();
					break;
				case "FullPage":
					OnFullPage_Click();
					break;
				case "100":
					On100_Click();
					break;
				//case "Close":
				//	OnClose_Click();
				//	break;
			}
		}

		private void OnPrint_Click() {
			if(!Printers.SetPrinter(Document,PrintSituation.TPPerio)){
				return;
			}
			if(Document.OriginAtMargins){
				//In the sheets framework,we had to set margins to 20 because of a bug in their preview control.
				//We now need to set it back to 0 for the actual printing.
				//Hopefully, this doesn't break anything else.
				Document.DefaultPageSettings.Margins=new Margins(0,0,0,0);
			}
			try{
				Document.Print();
			}
			catch(Exception e){
				MessageBox.Show(Lan.g(this,"Error: ")+e.Message);
			}
			DialogResult=DialogResult.OK;
		}

		private void OnClose_Click() {
			this.Close();
		}

		private void OnBack_Click(){
			if(previewContr.StartPage==0) return;
			previewContr.StartPage--;
			ToolBarMain.Buttons["PageNum"].Text=(previewContr.StartPage+1).ToString()
				+" / "+TotalPages.ToString();
			ToolBarMain.Invalidate();
		}

		private void OnFwd_Click(){
			//if(printPreviewControl2.StartPage==totalPages-1) return;
			previewContr.StartPage++;
			ToolBarMain.Buttons["PageNum"].Text=(previewContr.StartPage+1).ToString()
				+" / "+TotalPages.ToString();
			ToolBarMain.Invalidate();
		}

		private void OnFullPage_Click(){
			ToolBarMain.Buttons["100"].Pushed=!ToolBarMain.Buttons["FullPage"].Pushed;
			ToolBarMain.Invalidate();
			SetSize();
		}

		private void On100_Click(){
			ToolBarMain.Buttons["FullPage"].Pushed=!ToolBarMain.Buttons["100"].Pushed;
			ToolBarMain.Invalidate();
			SetSize();
		}

		private void butOK_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

	
	

		

		

		

		


	}
}
