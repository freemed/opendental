using Microsoft.CSharp;
//using Microsoft.Vsa;
using System.CodeDom.Compiler;
using System;
using System.Collections;
using System.Collections.Generic;
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
		public int TotalPages;
		private OpenDental.UI.ODToolBar ToolBarMain;
		private System.Windows.Forms.ImageList imageListMain;
		private System.Windows.Forms.PrintPreviewControl previewContr;
		///<summary></summary>
		public PrintDocument Document;
		private SignatureBox sigBox;
		private Panel panelSig;
		private Label label1;
		private OpenDental.UI.Button butCancel;
		private OpenDental.UI.Button butOK;
		private OpenDental.UI.Button butTopazSign;
		private OpenDental.UI.Button butClearSig;
		private Label labelInvalidSig;
		///<summary>Used to display Topaz signatures on Windows. Is added dynamically to avoid native code references crashing MONO.</summary>
		private Topaz.SigPlusNET sigBoxTopaz;
		private bool SigChanged;
		public TreatPlan TPcur;
		///<summary>Must be sorted by primary key.</summary>
		private List<ProcTP> proctpList;

		///<summary></summary>
		public FormTPsign(){
			InitializeComponent();//Required for Windows Form Designer support
			if(Environment.OSVersion.Platform==PlatformID.Unix) {
				butTopazSign.Visible=false;
			}
			else{
				//Add signature box for Topaz signatures.
				sigBoxTopaz=new Topaz.SigPlusNET();
				sigBoxTopaz.Location=sigBox.Location;//this puts both boxes in the same spot.
				sigBoxTopaz.Name="sigBoxTopaz";
				sigBoxTopaz.Size=new System.Drawing.Size(362,79);
				sigBoxTopaz.TabIndex=92;
				sigBoxTopaz.Text="sigPlusNET1";
				sigBoxTopaz.Visible=false;
				panelSig.Controls.Add(sigBoxTopaz);
				sigBox.SetTabletState(1);//It starts out accepting input. It will be set to 0 if a sig is already present.  It will be set back to 1 if note changes or if user clicks Clear.
			}
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
			this.labelInvalidSig = new System.Windows.Forms.Label();
			this.butTopazSign = new OpenDental.UI.Button();
			this.butClearSig = new OpenDental.UI.Button();
			this.butCancel = new OpenDental.UI.Button();
			this.butOK = new OpenDental.UI.Button();
			this.label1 = new System.Windows.Forms.Label();
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
			this.panelSig.Controls.Add(this.labelInvalidSig);
			this.panelSig.Controls.Add(this.butTopazSign);
			this.panelSig.Controls.Add(this.butClearSig);
			this.panelSig.Controls.Add(this.butCancel);
			this.panelSig.Controls.Add(this.butOK);
			this.panelSig.Controls.Add(this.label1);
			this.panelSig.Controls.Add(this.sigBox);
			this.panelSig.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panelSig.Location = new System.Drawing.Point(0,562);
			this.panelSig.Name = "panelSig";
			this.panelSig.Size = new System.Drawing.Size(842,92);
			this.panelSig.TabIndex = 92;
			// 
			// labelInvalidSig
			// 
			this.labelInvalidSig.BackColor = System.Drawing.SystemColors.Window;
			this.labelInvalidSig.Font = new System.Drawing.Font("Microsoft Sans Serif",8.25F,System.Drawing.FontStyle.Regular,System.Drawing.GraphicsUnit.Point,((byte)(0)));
			this.labelInvalidSig.Location = new System.Drawing.Point(251,13);
			this.labelInvalidSig.Name = "labelInvalidSig";
			this.labelInvalidSig.Size = new System.Drawing.Size(196,59);
			this.labelInvalidSig.TabIndex = 99;
			this.labelInvalidSig.Text = "Invalid Signature -  Document or note has changed since it was signed.";
			this.labelInvalidSig.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// butTopazSign
			// 
			this.butTopazSign.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butTopazSign.Autosize = true;
			this.butTopazSign.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butTopazSign.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butTopazSign.CornerRadius = 4F;
			this.butTopazSign.Location = new System.Drawing.Point(537,35);
			this.butTopazSign.Name = "butTopazSign";
			this.butTopazSign.Size = new System.Drawing.Size(81,25);
			this.butTopazSign.TabIndex = 98;
			this.butTopazSign.Text = "Sign Topaz";
			this.butTopazSign.UseVisualStyleBackColor = true;
			this.butTopazSign.Click += new System.EventHandler(this.butTopazSign_Click);
			// 
			// butClearSig
			// 
			this.butClearSig.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butClearSig.Autosize = true;
			this.butClearSig.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butClearSig.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butClearSig.CornerRadius = 4F;
			this.butClearSig.Location = new System.Drawing.Point(537,4);
			this.butClearSig.Name = "butClearSig";
			this.butClearSig.Size = new System.Drawing.Size(81,25);
			this.butClearSig.TabIndex = 97;
			this.butClearSig.Text = "Clear Sig";
			this.butClearSig.Click += new System.EventHandler(this.butClearSig_Click);
			// 
			// butCancel
			// 
			this.butCancel.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butCancel.Autosize = true;
			this.butCancel.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butCancel.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butCancel.CornerRadius = 4F;
			this.butCancel.Location = new System.Drawing.Point(741,57);
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
			this.butOK.Location = new System.Drawing.Point(741,25);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75,25);
			this.butOK.TabIndex = 93;
			this.butOK.Text = "OK";
			this.butOK.UseVisualStyleBackColor = true;
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
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
			// sigBox
			// 
			this.sigBox.Location = new System.Drawing.Point(162,3);
			this.sigBox.Name = "sigBox";
			this.sigBox.Size = new System.Drawing.Size(362,79);
			this.sigBox.TabIndex = 91;
			this.sigBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.sigBox_MouseUp);
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
			this.Load += new System.EventHandler(this.FormTPsign_Load);
			this.Layout += new System.Windows.Forms.LayoutEventHandler(this.FormReport_Layout);
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormTPsign_FormClosing);
			this.panelSig.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormTPsign_Load(object sender, System.EventArgs e) {
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
			labelInvalidSig.Visible=false;
			sigBox.Visible=true;
			proctpList=ProcTPs.RefreshForTP(TPcur.TreatPlanNum);
			if(TPcur.SigIsTopaz){
				if(TPcur.Signature!="") {
					if(Environment.OSVersion.Platform!=PlatformID.Unix) {
						sigBox.Visible=false;
						sigBoxTopaz.Visible=true;
						sigBoxTopaz.ClearTablet();
						sigBoxTopaz.SetSigCompressionMode(0);
						sigBoxTopaz.SetEncryptionMode(0);
						string keystring=TreatPlans.GetHashString(TPcur,proctpList);
						sigBoxTopaz.SetKeyString(keystring);
						//"0000000000000000");
						//sigBoxTopaz.SetAutoKeyData(ProcCur.Note+ProcCur.UserNum.ToString());
						sigBoxTopaz.SetEncryptionMode(2);//high encryption
						sigBoxTopaz.SetSigCompressionMode(2);//high compression
						sigBoxTopaz.SetSigString(TPcur.Signature);
						sigBoxTopaz.Refresh();
						if(sigBoxTopaz.NumberOfTabletPoints()==0) {
							labelInvalidSig.Visible=true;
						}
					}
				}
			}
			else {
				if(TPcur.Signature!="") {
					sigBox.Visible=true;
					sigBoxTopaz.Visible=false;
					sigBox.ClearTablet();
					//sigBox.SetSigCompressionMode(0);
					//sigBox.SetEncryptionMode(0);
					sigBox.SetKeyString(TreatPlans.GetHashString(TPcur,proctpList));
					//"0000000000000000");
					//sigBox.SetAutoKeyData(ProcCur.Note+ProcCur.UserNum.ToString());
					//sigBox.SetEncryptionMode(2);//high encryption
					//sigBox.SetSigCompressionMode(2);//high compression
					sigBox.SetSigString(TPcur.Signature);
					if(sigBox.NumberOfTabletPoints()==0) {
						labelInvalidSig.Visible=true;
					}
					sigBox.SetTabletState(0);//not accepting input.  To accept input, change the note, or clear the sig.
				}
			}
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
			previewContr.Height=ClientSize.Height-panelSig.Height-ToolBarMain.Height;
		}

		/*//I don't think we need this:
		///<summary></summary>
		private void FillSignature() {
			textNote.Text="";
			sigBox.ClearTablet();
			if(!panelNote.Visible) {
				return;
			}
			DataRow obj=(DataRow)TreeDocuments.SelectedNode.Tag;
			textNote.Text=selectionDoc.Note;
			sigBox.Visible=true;
			sigBox.SetTabletState(0);//never accepts input here
			labelInvalidSig.Visible=false;
			//Topaz box is not supported in Unix, since the required dll is Windows native.
			if(Environment.OSVersion.Platform!=PlatformID.Unix) {
				sigBoxTopaz.Location=sigBox.Location;//this puts both boxes in the same spot.
				sigBoxTopaz.Visible=false;
				((Topaz.SigPlusNET)sigBoxTopaz).SetTabletState(0);
			}
			//A machine running Unix will have selectionDoc.SigIsTopaz set to false here, because the visibility of the panelNote
			//will be set to false in the case of Unix and SigIsTopaz. Therefore, the else part of this if-else clause is always
			//run on Unix systems.
			if(selectionDoc.SigIsTopaz) {
				if(selectionDoc.Signature!=null && selectionDoc.Signature!="") {
					sigBox.Visible=false;
					sigBoxTopaz.Visible=true;
					((Topaz.SigPlusNET)sigBoxTopaz).ClearTablet();
					((Topaz.SigPlusNET)sigBoxTopaz).SetSigCompressionMode(0);
					((Topaz.SigPlusNET)sigBoxTopaz).SetEncryptionMode(0);
					((Topaz.SigPlusNET)sigBoxTopaz).SetKeyString(GetHashString(selectionDoc));
					((Topaz.SigPlusNET)sigBoxTopaz).SetEncryptionMode(2);//high encryption
					((Topaz.SigPlusNET)sigBoxTopaz).SetSigCompressionMode(2);//high compression
					((Topaz.SigPlusNET)sigBoxTopaz).SetSigString(selectionDoc.Signature);
					if(((Topaz.SigPlusNET)sigBoxTopaz).NumberOfTabletPoints() == 0) {
						labelInvalidSig.Visible=true;
					}
				}
			}
			else {
				sigBox.ClearTablet();
				if(selectionDoc.Signature!=null && selectionDoc.Signature!="") {
					sigBox.Visible=true;
					sigBoxTopaz.Visible=false;
					sigBox.SetKeyString(GetHashString(selectionDoc));
					sigBox.SetSigString(selectionDoc.Signature);
					if(sigBox.NumberOfTabletPoints()==0) {
						labelInvalidSig.Visible=true;
					}
					sigBox.SetTabletState(0);//not accepting input.
				}
			}
		}*/

		private void ToolBarMain_ButtonClick(object sender, OpenDental.UI.ODToolBarButtonClickEventArgs e) {
			//MessageBox.Show(e.Button.Tag.ToString());
			switch(e.Button.Tag.ToString()){
				//case "Print":
				//	OnPrint_Click();
				//	break;
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

		private void butClearSig_Click(object sender,EventArgs e) {
			sigBox.ClearTablet();
			sigBox.Visible=true;
			if(Environment.OSVersion.Platform!=PlatformID.Unix) {
				sigBoxTopaz.ClearTablet();
				sigBoxTopaz.Visible=false;//until user explicitly starts it.
			}
			sigBox.SetTabletState(1);//on-screen box is now accepting input.
			SigChanged=true;
			labelInvalidSig.Visible=false;
		}

		private void butTopazSign_Click(object sender,EventArgs e) {
			sigBox.Visible=false;
			sigBoxTopaz.Visible=true;
			sigBoxTopaz.SetTabletState(1);
			SigChanged=true;
			labelInvalidSig.Visible=false;
		}

		private void sigBox_MouseUp(object sender,MouseEventArgs e) {
			//this is done on mouse up so that the initial pen capture won't be delayed.
			if(sigBox.GetTabletState()==1//if accepting input.
				&& !SigChanged)//and sig not changed yet
			{
				//sigBox handles its own pen input.
				SigChanged=true;
			}
		}

		private void SaveSignature() {
			if(SigChanged) {
				//This check short-circuits so that sigBoxTopaz.Visible will not be checked in MONO ever.
				if(Environment.OSVersion.Platform!=PlatformID.Unix && sigBoxTopaz.Visible) {
					TPcur.SigIsTopaz=true;
					if(sigBoxTopaz.NumberOfTabletPoints()==0) {
						TPcur.Signature="";
						return;
					}
					sigBoxTopaz.SetSigCompressionMode(0);
					sigBoxTopaz.SetEncryptionMode(0);
					sigBoxTopaz.SetKeyString(TreatPlans.GetHashString(TPcur,proctpList));
					//"0000000000000000");
					//sigBoxTopaz.SetAutoKeyData(ProcCur.Note+ProcCur.UserNum.ToString());
					sigBoxTopaz.SetEncryptionMode(2);
					sigBoxTopaz.SetSigCompressionMode(2);
					TPcur.Signature=sigBoxTopaz.GetSigString();
				}
				else {
					TPcur.SigIsTopaz=false;
					if(sigBox.NumberOfTabletPoints()==0) {
						TPcur.Signature="";
						return;
					}
					//sigBox.SetSigCompressionMode(0);
					//sigBox.SetEncryptionMode(0);
					sigBox.SetKeyString(TreatPlans.GetHashString(TPcur,proctpList));
					//"0000000000000000");
					//sigBox.SetAutoKeyData(ProcCur.Note+ProcCur.UserNum.ToString());
					//sigBox.SetEncryptionMode(2);
					//sigBox.SetSigCompressionMode(2);
					TPcur.Signature=sigBox.GetSigString();
				}
			}
		}

		private void butOK_Click(object sender,EventArgs e) {
			SaveSignature();
			TreatPlans.Update(TPcur);
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		private void FormTPsign_FormClosing(object sender,FormClosingEventArgs e) {
			sigBoxTopaz.Dispose();
		}

		

	
	

		

		

		

		


	}
}
