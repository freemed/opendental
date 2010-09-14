using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace OpenDental.UI {
	///<summary>Wraps the Topaz SigPlusNET control and the alternate SignatureBox control.  Also includes both needed buttons.  Should vastly simplify using signature boxes throughout the program.</summary>
	public partial class SignatureBoxWrapper:UserControl {
		private bool sigChanged;
		private bool allowTopaz;
		private Control sigBoxTopaz;
		private string labelText;

		public SignatureBoxWrapper() {
			InitializeComponent();
			allowTopaz=(Environment.OSVersion.Platform!=PlatformID.Unix && !CodeBase.ODEnvironment.Is64BitOperatingSystem());
			sigBox.SetTabletState(1);
			if(!allowTopaz) {
				butTopazSign.Visible=false;
				sigBox.Visible=true;
			}
			else{
				//Add signature box for Topaz signatures.
				sigBoxTopaz=CodeBase.TopazWrapper.GetTopaz();
				sigBoxTopaz.Location=sigBox.Location;//this puts both boxes in the same spot.
				sigBoxTopaz.Name="sigBoxTopaz";
				sigBoxTopaz.Size=sigBox.Size;//new System.Drawing.Size(362,79);
				sigBox.Anchor=(AnchorStyles)(AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right);
				sigBoxTopaz.TabIndex=92;
				sigBoxTopaz.Text="sigPlusNET1";
				sigBoxTopaz.Visible=false;
				sigBoxTopaz.Leave+=new EventHandler(sigBoxTopaz_Leave);
				Controls.Add(sigBoxTopaz);
				//It starts out accepting input. It will be set to 0 if a sig is already present.  It will be set back to 1 if note changes or if user clicks Clear.
				CodeBase.TopazWrapper.SetTopazState(sigBoxTopaz,1);
				butTopazSign.BringToFront();
				butClearSig.BringToFront();
			}
		}

		///<summary>Usually "Invalid Signature", but this can be changed for different situations.</summary>
		public string LabelText{
			get{
				return labelText;
			}
			set{
				labelText=value;
				labelInvalidSig.Text=value;
			}
		}

		public void FillSignature(bool sigIsTopaz,string keyData,string signature){
			labelInvalidSig.Visible=false;
			sigBox.Visible=true;
			if(sigIsTopaz){
				if(signature!=""){
					if(allowTopaz){
						sigBox.Visible=false;
						sigBoxTopaz.Visible=true;
						CodeBase.TopazWrapper.ClearTopaz(sigBoxTopaz);
						CodeBase.TopazWrapper.SetTopazCompressionMode(sigBoxTopaz,0);
						CodeBase.TopazWrapper.SetTopazEncryptionMode(sigBoxTopaz,0);
						CodeBase.TopazWrapper.SetTopazKeyString(sigBoxTopaz,"0000000000000000");
						CodeBase.TopazWrapper.SetTopazEncryptionMode(sigBoxTopaz,2);//high encryption
						CodeBase.TopazWrapper.SetTopazCompressionMode(sigBoxTopaz,2);//high compression
						CodeBase.TopazWrapper.SetTopazSigString(sigBoxTopaz,signature);
						//older items may have been signed with zeros due to a bug.  We still want to show the sig in that case.
						//but if a sig is not showing, then set the key string to try to get it to show.
						if(CodeBase.TopazWrapper.GetTopazNumberOfTabletPoints(sigBoxTopaz)==0) {
							CodeBase.TopazWrapper.SetTopazAutoKeyData(sigBoxTopaz,keyData);
							CodeBase.TopazWrapper.SetTopazSigString(sigBoxTopaz,signature);
						}
						//if still not showing, then it must be invalid
						if(CodeBase.TopazWrapper.GetTopazNumberOfTabletPoints(sigBoxTopaz)==0) {
							labelInvalidSig.Visible=true;
						}
					}
				}
			}
			else{
				if(signature!=null && signature!="") {
					sigBox.Visible=true;
					sigBoxTopaz.Visible=false;
					sigBox.ClearTablet();
					//sigBox.SetSigCompressionMode(0);
					//sigBox.SetEncryptionMode(0);
					sigBox.SetKeyString("0000000000000000");
					sigBox.SetAutoKeyData(keyData);
					//sigBox.SetEncryptionMode(2);//high encryption
					//sigBox.SetSigCompressionMode(2);//high compression
					sigBox.SetSigString(signature);
					if(sigBox.NumberOfTabletPoints()==0) {
						labelInvalidSig.Visible=true;
					}
					sigBox.SetTabletState(0);//not accepting input.  To accept input, change the note, or clear the sig.
				}
			}
		}

		///<summary></summary>
		public bool GetSigChanged(){
			return sigChanged;
		}

		///<summary>This should NOT be used unless GetSigChanged returns true.</summary>
		public bool GetSigIsTopaz(){
			if(allowTopaz && sigBoxTopaz.Visible){
				return true;
			}
			return false;
		}

		/*
		///<summary></summary>
		public bool GetSigIsValid(){
			if(labelInvalidSig.Visible){
				return false;
			}
			return true;
		}*/

		///<summary>This should happen a lot before the box is signed.  Once it's signed, if this happens, then the signature will be invalidated.  The user would have to clear the invalidation manually.  This "invalidation" is just a visual cue; nothing actually happens to the data.</summary>
		public void SetInvalid(){
			if(allowTopaz && sigBoxTopaz.Visible){
				if(CodeBase.TopazWrapper.GetTopazNumberOfTabletPoints(sigBoxTopaz)==0){
					return;//no need to do anything because no signature
				}
			}
			else{
				if(sigBox.NumberOfTabletPoints()==0) {
					return;//no need to do anything because no signature
				}
			}
			labelInvalidSig.Visible=true;	
		}

		public bool IsValid{
			get { return (!labelInvalidSig.Visible); }
		}

		public bool SigIsBlank{
			get{ 
				if(allowTopaz && sigBoxTopaz.Visible){
					return(CodeBase.TopazWrapper.GetTopazNumberOfTabletPoints(sigBoxTopaz)==0);
				}
				return(sigBox.NumberOfTabletPoints()==0);
			}
		}

		///<summary>This should NOT be used unless GetSigChanged returns true.</summary>
		public string GetSignature(string keyData){
			//Topaz boxes are written in Windows native code.
			if(allowTopaz && sigBoxTopaz.Visible){
				//ProcCur.SigIsTopaz=true;
				if(CodeBase.TopazWrapper.GetTopazNumberOfTabletPoints(sigBoxTopaz)==0){
					return "";
				}
				CodeBase.TopazWrapper.SetTopazCompressionMode(sigBoxTopaz,0);
				CodeBase.TopazWrapper.SetTopazEncryptionMode(sigBoxTopaz,0);
				CodeBase.TopazWrapper.SetTopazKeyString(sigBoxTopaz,"0000000000000000");
				CodeBase.TopazWrapper.SetTopazAutoKeyData(sigBoxTopaz,keyData);
				CodeBase.TopazWrapper.SetTopazEncryptionMode(sigBoxTopaz,2);
				CodeBase.TopazWrapper.SetTopazCompressionMode(sigBoxTopaz,2);
				return CodeBase.TopazWrapper.GetTopazString(sigBoxTopaz);
			}
			else{
				//ProcCur.SigIsTopaz=false;
				if(sigBox.NumberOfTabletPoints()==0) {
					return "";
				}
				//sigBox.SetSigCompressionMode(0);
				//sigBox.SetEncryptionMode(0);
				sigBox.SetKeyString("0000000000000000");
				sigBox.SetAutoKeyData(keyData);
				//sigBox.SetEncryptionMode(2);
				//sigBox.SetSigCompressionMode(2);
				return sigBox.GetSigString();
			}
		}

		private void butClearSig_Click(object sender,EventArgs e) {
			sigBox.ClearTablet();
			sigBox.Visible=true;
			if(allowTopaz) {
				CodeBase.TopazWrapper.ClearTopaz(sigBoxTopaz);
				sigBoxTopaz.Visible=false;//until user explicitly starts it.
			}
			sigBox.SetTabletState(1);//on-screen box is now accepting input.
			sigChanged=true;
			labelInvalidSig.Visible=false;
			//To be able to use this control in FormProcEdit, we need to connect this to an event.
			//ProcCur.UserNum=Security.CurUser.UserNum;
			//textUser.Text=Userods.GetName(ProcCur.UserNum);
		}

		private void butTopazSign_Click(object sender,EventArgs e) {
			//this button is not even visible if Topaz is not allowed
			sigBox.Visible=false;
			sigBoxTopaz.Visible=true;
			if(allowTopaz){
				CodeBase.TopazWrapper.ClearTopaz(sigBoxTopaz);
				CodeBase.TopazWrapper.SetTopazState(sigBoxTopaz,1);
			}
			sigChanged=true;
			labelInvalidSig.Visible=false;
			sigBoxTopaz.Focus();
			//To be able to use this control in FormProcEdit, we need to connect this to an event.
			//ProcCur.UserNum=Security.CurUser.UserNum;
			//textUser.Text=Userods.GetName(ProcCur.UserNum);
		}

		private void sigBox_MouseUp(object sender,MouseEventArgs e) {
			//this is done on mouse up so that the initial pen capture won't be delayed.
			if(sigBox.GetTabletState()==1//if accepting input.
				&& !sigChanged)//and sig not changed yet
			{
				//sigBox handles its own pen input.
				sigChanged=true;
				//To be able to use this control in FormProcEdit, we need to connect this to an event.
				//ProcCur.UserNum=Security.CurUser.UserNum;
				//textUser.Text=Userods.GetName(ProcCur.UserNum);
			}
		}

		private void sigBoxTopaz_Leave(object sender,EventArgs e) {
			if(sigBox.GetTabletState()==1) {//if accepting input.
				CodeBase.TopazWrapper.SetTopazState(sigBoxTopaz,0);
			}
		}

		///<summary>Must set width and height of control and run FillSignature first.</summary>
		public Bitmap GetSigImage(){
			Bitmap sigBitmap=new Bitmap(Width-2,Height-2);
			//no outline
			if(allowTopaz && sigBoxTopaz.Visible){
				sigBoxTopaz.DrawToBitmap(sigBitmap,new Rectangle(0,0,Width-2,Height-2));//GetBitmap would probably work.
			}
			else{
				sigBitmap=(Bitmap)sigBox.GetSigImage(false);
			}
			return sigBitmap;
		}

		









	}
}
