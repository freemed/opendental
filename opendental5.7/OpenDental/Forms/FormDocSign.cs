/*=============================================================================================================
Open Dental GPL license Copyright (C) 2003  Jordan Sparks, DMD.  http://www.open-dent.com,  www.docsparks.com
See header in FormOpenDental.cs for complete text.  Redistributions must retain this text.
===============================================================================================================*/
using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing.Printing;
using System.IO;
using System.Net;
using System.Resources;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using OpenDentBusiness;
using CodeBase;
using OpenDental.Imaging;

namespace OpenDental{
///<summary></summary>
	public class FormDocSign:System.Windows.Forms.Form {
		private OpenDental.UI.Button butOK;
		private OpenDental.UI.Button butCancel;
		private System.ComponentModel.Container components = null;//required by designer
		//<summary></summary>
		//public bool IsNew;
		//private Patient PatCur;
		private TextBox textNote;
		private Label label8;
		private OpenDental.UI.SignatureBox sigBox;
		private OpenDental.UI.Button butClearSig;
		private Label label15;
		private Document DocCur;
		///<summary>This keeps the noteChanged event from erasing the signature when first loading.</summary>
		private bool IsStartingUp;
		private IImageStore imageStore;
		private Label labelInvalidSig;
		private bool SigChanged;
		///<summary>To allow tablet signatures on Windows. Must be added dynamically when the program is not running on Unix so that MONO does not crash.</summary>
		private Topaz.SigPlusNET sigBoxTopaz;
		private OpenDental.UI.Button butTopazSign;
		
		///<summary></summary>
		public FormDocSign(Document docCur,IImageStore imageStore){
			InitializeComponent();
			//Can only allow tablet signatures on Windows, since we use a native dll to handle the tablet interaction.
			if(Environment.OSVersion.Platform==PlatformID.Unix){
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
				Controls.Add(sigBoxTopaz);
				sigBox.SetTabletState(1);//It starts out accepting input. It will be set to 0 if a sig is already present.  It will be set back to 1 if note changes or if user clicks Clear.
			}
			DocCur=docCur;
			this.imageStore=imageStore;
			Lan.F(this);
		}

		///<summary></summary>
		protected override void Dispose( bool disposing ){
			if( disposing ){
				if(components != null){
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
			this.textNote = new System.Windows.Forms.TextBox();
			this.label8 = new System.Windows.Forms.Label();
			this.label15 = new System.Windows.Forms.Label();
			this.labelInvalidSig = new System.Windows.Forms.Label();
			this.sigBox = new OpenDental.UI.SignatureBox();
			this.butClearSig = new OpenDental.UI.Button();
			this.butCancel = new OpenDental.UI.Button();
			this.butOK = new OpenDental.UI.Button();
			this.butTopazSign = new OpenDental.UI.Button();
			this.SuspendLayout();
			// 
			// textNote
			// 
			this.textNote.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.textNote.Location = new System.Drawing.Point(39,0);
			this.textNote.MaxLength = 255;
			this.textNote.Multiline = true;
			this.textNote.Name = "textNote";
			this.textNote.Size = new System.Drawing.Size(302,85);
			this.textNote.TabIndex = 17;
			this.textNote.TextChanged += new System.EventHandler(this.textNote_TextChanged);
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(0,2);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(33,45);
			this.label8.TabIndex = 16;
			this.label8.Text = "Note";
			this.label8.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label15
			// 
			this.label15.Location = new System.Drawing.Point(370,2);
			this.label15.Name = "label15";
			this.label15.Size = new System.Drawing.Size(85,25);
			this.label15.TabIndex = 88;
			this.label15.Text = "Signature";
			this.label15.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// labelInvalidSig
			// 
			this.labelInvalidSig.BackColor = System.Drawing.SystemColors.Window;
			this.labelInvalidSig.Font = new System.Drawing.Font("Microsoft Sans Serif",8.25F,System.Drawing.FontStyle.Regular,System.Drawing.GraphicsUnit.Point,((byte)(0)));
			this.labelInvalidSig.Location = new System.Drawing.Point(550,10);
			this.labelInvalidSig.Name = "labelInvalidSig";
			this.labelInvalidSig.Size = new System.Drawing.Size(196,59);
			this.labelInvalidSig.TabIndex = 95;
			this.labelInvalidSig.Text = "Invalid Signature -  Document or note has changed since it was signed.";
			this.labelInvalidSig.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// sigBox
			// 
			this.sigBox.Location = new System.Drawing.Point(457,0);
			this.sigBox.Name = "sigBox";
			this.sigBox.Size = new System.Drawing.Size(362,79);
			this.sigBox.TabIndex = 91;
			this.sigBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.sigBox_MouseUp);
			// 
			// butClearSig
			// 
			this.butClearSig.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butClearSig.Autosize = true;
			this.butClearSig.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butClearSig.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butClearSig.CornerRadius = 4F;
			this.butClearSig.Location = new System.Drawing.Point(370,25);
			this.butClearSig.Name = "butClearSig";
			this.butClearSig.Size = new System.Drawing.Size(81,25);
			this.butClearSig.TabIndex = 90;
			this.butClearSig.Text = "Clear Sig";
			this.butClearSig.Click += new System.EventHandler(this.butClearSig_Click);
			// 
			// butCancel
			// 
			this.butCancel.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butCancel.Autosize = true;
			this.butCancel.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butCancel.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butCancel.CornerRadius = 4F;
			this.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butCancel.Location = new System.Drawing.Point(837,50);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75,25);
			this.butCancel.TabIndex = 4;
			this.butCancel.Text = "Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.CornerRadius = 4F;
			this.butOK.Location = new System.Drawing.Point(837,19);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75,25);
			this.butOK.TabIndex = 3;
			this.butOK.Text = "OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// butTopazSign
			// 
			this.butTopazSign.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butTopazSign.Autosize = true;
			this.butTopazSign.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butTopazSign.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butTopazSign.CornerRadius = 4F;
			this.butTopazSign.Location = new System.Drawing.Point(370,56);
			this.butTopazSign.Name = "butTopazSign";
			this.butTopazSign.Size = new System.Drawing.Size(81,25);
			this.butTopazSign.TabIndex = 96;
			this.butTopazSign.Text = "Topaz";
			this.butTopazSign.UseVisualStyleBackColor = true;
			this.butTopazSign.Click += new System.EventHandler(this.butTopazSign_Click);
			// 
			// FormDocSign
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.BackColor = System.Drawing.SystemColors.Control;
			this.ClientSize = new System.Drawing.Size(913,84);
			this.Controls.Add(this.butTopazSign);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.labelInvalidSig);
			this.Controls.Add(this.sigBox);
			this.Controls.Add(this.butClearSig);
			this.Controls.Add(this.label15);
			this.Controls.Add(this.textNote);
			this.Controls.Add(this.label8);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormDocSign";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "Signature";
			this.Load += new System.EventHandler(this.FormDocSign_Load);
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormDocSign_FormClosing);
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		///<summary></summary>
		public void FormDocSign_Load(object sender, System.EventArgs e){
			IsStartingUp=true;
			textNote.Text=DocCur.Note;
			labelInvalidSig.Visible=false;
			sigBox.Visible=true;
			if(DocCur.SigIsTopaz) {
				if(DocCur.Signature!="") {
					if(Environment.OSVersion.Platform!=PlatformID.Unix){
						sigBox.Visible=false;
						sigBoxTopaz.Visible=true;
						sigBoxTopaz.ClearTablet();
						sigBoxTopaz.SetSigCompressionMode(0);
						sigBoxTopaz.SetEncryptionMode(0);
						sigBoxTopaz.SetKeyString(imageStore.GetHashString(DocCur));
							//"0000000000000000");
						//sigBoxTopaz.SetAutoKeyData(ProcCur.Note+ProcCur.UserNum.ToString());
						sigBoxTopaz.SetEncryptionMode(2);//high encryption
						sigBoxTopaz.SetSigCompressionMode(2);//high compression
						sigBoxTopaz.SetSigString(DocCur.Signature);
						sigBoxTopaz.Refresh();
						if(sigBoxTopaz.NumberOfTabletPoints()==0) {
							labelInvalidSig.Visible=true;
						}
					}
				}
			}
			else {
				if(DocCur.Signature!="") {
					sigBox.Visible=true;
					sigBoxTopaz.Visible=false;
					sigBox.ClearTablet();
					//sigBox.SetSigCompressionMode(0);
					//sigBox.SetEncryptionMode(0);
					sigBox.SetKeyString(imageStore.GetHashString(DocCur));
						//"0000000000000000");
					//sigBox.SetAutoKeyData(ProcCur.Note+ProcCur.UserNum.ToString());
					//sigBox.SetEncryptionMode(2);//high encryption
					//sigBox.SetSigCompressionMode(2);//high compression
					sigBox.SetSigString(DocCur.Signature);
					if(sigBox.NumberOfTabletPoints()==0) {
						labelInvalidSig.Visible=true;
					}
					sigBox.SetTabletState(0);//not accepting input.  To accept input, change the note, or clear the sig.
				}
			}
			IsStartingUp=false;
		}

		private void butClearSig_Click(object sender,EventArgs e) {
			sigBox.ClearTablet();
			sigBox.Visible=true;
			if(Environment.OSVersion.Platform!=PlatformID.Unix){
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

		private void textNote_TextChanged(object sender,EventArgs e) {
			if(!IsStartingUp//so this happens only if user changes the note
				&& !SigChanged)//and the original signature is still showing.
			{
				sigBox.Visible=true;
				sigBox.ClearTablet();
				if(Environment.OSVersion.Platform!=PlatformID.Unix){
					sigBoxTopaz.ClearTablet();
					sigBoxTopaz.Visible=false;//until user explicitly starts it.
				}
				sigBox.SetTabletState(1);//on-screen box is now accepting input.
				SigChanged=true;
				labelInvalidSig.Visible=false;
			}
		}

		private void SaveSignature() {
			if(SigChanged) {
				//This check short-circuits so that sigBoxTopaz.Visible will not be checked in MONO ever.
				if(Environment.OSVersion.Platform!=PlatformID.Unix && sigBoxTopaz.Visible) {
					DocCur.SigIsTopaz=true;
					if(sigBoxTopaz.NumberOfTabletPoints()==0) {
						DocCur.Signature="";
						return;
					}
					sigBoxTopaz.SetSigCompressionMode(0);
					sigBoxTopaz.SetEncryptionMode(0);
					sigBoxTopaz.SetKeyString(imageStore.GetHashString(DocCur));
						//"0000000000000000");
					//sigBoxTopaz.SetAutoKeyData(ProcCur.Note+ProcCur.UserNum.ToString());
					sigBoxTopaz.SetEncryptionMode(2);
					sigBoxTopaz.SetSigCompressionMode(2);
					DocCur.Signature=sigBoxTopaz.GetSigString();
				}
				else {
					DocCur.SigIsTopaz=false;
					if(sigBox.NumberOfTabletPoints()==0) {
						DocCur.Signature="";
						return;
					}
					//sigBox.SetSigCompressionMode(0);
					//sigBox.SetEncryptionMode(0);
					sigBox.SetKeyString(imageStore.GetHashString(DocCur));
						//"0000000000000000");
					//sigBox.SetAutoKeyData(ProcCur.Note+ProcCur.UserNum.ToString());
					//sigBox.SetEncryptionMode(2);
					//sigBox.SetSigCompressionMode(2);
					DocCur.Signature=sigBox.GetSigString();
				}
			}
		}

		private void butOK_Click(object sender, System.EventArgs e){
			DocCur.Note=textNote.Text;
			SaveSignature();
			Documents.Update(DocCur);
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		private void FormDocSign_FormClosing(object sender,FormClosingEventArgs e) {
			sigBoxTopaz.Dispose();
		}

		

		

		

		
		
	}
}