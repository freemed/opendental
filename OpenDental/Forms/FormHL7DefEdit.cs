using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;
using OpenDental.UI;

namespace OpenDental {
	/// <summary></summary>
	public partial class FormHL7DefEdit:System.Windows.Forms.Form {
		private FolderBrowserDialog fb;
		public HL7Def HL7DefCur;

		///<summary></summary>
		public FormHL7DefEdit() {
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			Lan.F(this);
		}

		private void FormHL7DefEdit_Load(object sender,EventArgs e) {
			if(!Security.IsAuthorized(Permissions.Setup,true)) {
				gridMain.Enabled=false;
				groupBox1.Enabled=false;
			}
			FillGridMain();
			for(int i=0;i<Enum.GetNames(typeof(ModeTxHL7)).Length;i++) {
				comboModeTx.Items.Add(Lan.g("enumModeTxHL7",Enum.GetName(typeof(ModeTxHL7),i).ToString()));
			}
			textDescription.Text=HL7DefCur.Description;
			checkInternal.Checked=HL7DefCur.IsInternal;
			checkEnabled.Checked=HL7DefCur.IsEnabled;
			textInternalType.Text=HL7DefCur.InternalType;
			textInternalTypeVersion.Text=HL7DefCur.InternalTypeVersion;
			textInPort.Text=HL7DefCur.IncomingPort;
			textInPath.Text=HL7DefCur.IncomingFolder;
			textOutPort.Text=HL7DefCur.OutgoingIpPort;
			textOutPath.Text=HL7DefCur.OutgoingFolder;
			textFieldSep.Text=HL7DefCur.FieldSeparator;
			textRepSep.Text=HL7DefCur.RepetitionSeparator;
			textCompSep.Text=HL7DefCur.ComponentSeparator;
			textSubcompSep.Text=HL7DefCur.SubcomponentSeparator;
			textEscChar.Text=HL7DefCur.EscapeCharacter;
			textNote.Text=HL7DefCur.Note;
			if(HL7DefCur.ModeTx==ModeTxHL7.File) {
				comboModeTx.SelectedIndex=0;
				textInPort.Visible=false;
				textOutPort.Visible=false;
				labelInPort.Visible=false;
				labelInPortEx.Visible=false;
				labelOutPort.Visible=false;
				labelOutPortEx.Visible=false;
				textInPath.Visible=true;
				textOutPath.Visible=true;
				labelInPath.Visible=true;
				butBrowseIn.Visible=true;
				labelOutPath.Visible=true;
				butBrowseOut.Visible=true;
				textInPort.TabStop=false;
				textOutPort.TabStop=false;
				butBrowseIn.TabStop=true;
				butBrowseOut.TabStop=true;
			}
			else { //ModeTxHL7.TcpIp
				comboModeTx.SelectedIndex=1;
				textInPort.Visible=true;
				textOutPort.Visible=true;
				labelInPort.Visible=true;
				labelInPortEx.Visible=true;
				labelOutPort.Visible=true;
				labelOutPortEx.Visible=true;
				textInPath.Visible=false;
				textOutPath.Visible=false;
				labelInPath.Visible=false;
				butBrowseIn.Visible=false;
				labelOutPath.Visible=false;
				butBrowseOut.Visible=false;
				textInPort.TabStop=true;
				textOutPort.TabStop=true;
				butBrowseIn.TabStop=false;
				butBrowseOut.TabStop=false;
			}
			if(HL7DefCur.IsInternal) {
				textDescription.ReadOnly=true;
				textFieldSep.ReadOnly=true;
				textRepSep.ReadOnly=true;
				textCompSep.ReadOnly=true;
				textSubcompSep.ReadOnly=true;
				textEscChar.ReadOnly=true;
			}
		}

		private void FillGridMain() {
			gridMain.BeginUpdate();
			gridMain.Columns.Clear();
			ODGridColumn col=new ODGridColumn(Lan.g(this,"Message"),120);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"Segment Name"),100);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"Note"),100);
			gridMain.Columns.Add(col);
			gridMain.Rows.Clear();
			for(int i=0;i<HL7DefCur.hl7DefMessages.Count;i++) {
				ODGridRow row=new ODGridRow();
				row.Cells.Add(HL7DefCur.hl7DefMessages[i].MessageType.ToString()+"-"+HL7DefCur.hl7DefMessages[i].EventType.ToString()+", "+HL7DefCur.hl7DefMessages[i].InOrOut.ToString());
				row.Cells.Add("");
				row.Cells.Add(HL7DefCur.hl7DefMessages[i].Note);
				row.Tag=HL7DefCur.hl7DefMessages[i];
				gridMain.Rows.Add(row);
				for(int j=0;j<HL7DefCur.hl7DefMessages[i].hl7DefSegments.Count;j++) {
					row=new ODGridRow();
					row.Cells.Add("");
					row.Cells.Add(HL7DefCur.hl7DefMessages[i].hl7DefSegments[j].SegmentName.ToString());
					row.Cells.Add(HL7DefCur.hl7DefMessages[i].hl7DefSegments[j].Note);
					row.Tag=HL7DefCur.hl7DefMessages[i];
					gridMain.Rows.Add(row);
				}
			}
			gridMain.EndUpdate();
		}

		///<summary>Returns the given path with the local OS path separators as necessary.</summary>
		public static string FixDirSeparators(string path) {
			if(Environment.OSVersion.Platform==PlatformID.Unix) {
				path.Replace('\\',Path.DirectorySeparatorChar);
			}
			else {//Windows
				path.Replace('/',Path.DirectorySeparatorChar);
			}
			return path;
		}

		private void butBrowseIn_Click(object sender,EventArgs e) {
			if(fb.ShowDialog()==DialogResult.OK) {
				textInPath.Text=fb.SelectedPath;
			}
		}

		private void butBrowseOut_Click(object sender,EventArgs e) {
			if(fb.ShowDialog()==DialogResult.OK) {
				textOutPath.Text=fb.SelectedPath;
			}
		}

		private void gridMain_CellDoubleClick(object sender,ODGridClickEventArgs e) {
			FormHL7DefMessageEdit FormS=new FormHL7DefMessageEdit();
			FormS.HL7DefMes=(HL7DefMessage)gridMain.Rows[e.Row].Tag;
			FormS.ShowDialog();
			FillGridMain();
		}

		private void comboModeTx_SelectedIndexChanged(object sender,System.EventArgs e) {
			if(comboModeTx.SelectedIndex==0) {
				textInPort.Visible=false;
				textOutPort.Visible=false;
				labelInPort.Visible=false;
				labelInPortEx.Visible=false;
				labelOutPort.Visible=false;
				labelOutPortEx.Visible=false;
				textInPath.Visible=true;
				textOutPath.Visible=true;
				labelInPath.Visible=true;
				butBrowseIn.Visible=true;
				labelOutPath.Visible=true;
				butBrowseOut.Visible=true;
				textInPort.TabStop=false;
				textOutPort.TabStop=false;
				butBrowseIn.TabStop=true;
				butBrowseOut.TabStop=true;
			}
			else if(comboModeTx.SelectedIndex==1) {
				comboModeTx.SelectedIndex=1;
				textInPort.Visible=true;
				textOutPort.Visible=true;
				labelInPort.Visible=true;
				labelInPortEx.Visible=true;
				labelOutPort.Visible=true;
				labelOutPortEx.Visible=true;
				textInPath.Visible=false;
				textOutPath.Visible=false;
				labelInPath.Visible=false;
				butBrowseIn.Visible=false;
				labelOutPath.Visible=false;
				butBrowseOut.Visible=false;
				textInPort.TabStop=true;
				textOutPort.TabStop=true;
				butBrowseIn.TabStop=false;
				butBrowseOut.TabStop=false;
			}
		}

		private void butOK_Click(object sender,EventArgs e) {
			if(comboModeTx.SelectedIndex==0) {//File mode
				if(textInPath.Text!="") {
					if(!Directory.Exists(textInPath.Text)) {
						MsgBox.Show(this,"The path for Incoming Folder is invalid.");
						return;
					}
				}
				else {//If file mode selected but path left blank
					MsgBox.Show(this,"The path for Incoming Folder is empty.");
					return;
				}
				if(textOutPath.Text!="") {
					if(!Directory.Exists(textOutPath.Text)) {
						MsgBox.Show(this,"The path for Outgoing Folder is invalid.");
						return;
					}
				}
				else {
					MsgBox.Show(this,"The path for Outgoing Folder is empty.");
					return;
				}
			}
			else {//TcpIp mode
				if(textInPort.Text=="") {
					MsgBox.Show(this,"The Incoming Port is empty.");
					return;
				}
				if(textOutPort.Text=="") {
					MsgBox.Show(this,"The Outgoing Port is empty.");
					return;
				}
			}
			HL7DefCur.Description=textDescription.Text;
			HL7DefCur.IsEnabled=checkEnabled.Checked;
			HL7DefCur.FieldSeparator=textFieldSep.Text;
			HL7DefCur.RepetitionSeparator=textRepSep.Text;
			HL7DefCur.ComponentSeparator=textCompSep.Text;
			HL7DefCur.SubcomponentSeparator=textSubcompSep.Text;
			HL7DefCur.EscapeCharacter=textEscChar.Text;
			HL7DefCur.Note=textNote.Text;
			if(comboModeTx.SelectedIndex==0) {//File mode
				HL7DefCur.IncomingFolder=textInPath.Text;
				HL7DefCur.OutgoingFolder=textOutPath.Text;
				HL7DefCur.ModeTx=ModeTxHL7.File;
			}
			else {//TcpIp mode
				HL7DefCur.IncomingPort=textInPort.Text;
				HL7DefCur.OutgoingIpPort=textOutPort.Text;
				HL7DefCur.ModeTx=ModeTxHL7.TcpIp;
			}
			if(HL7DefCur.IsNew) {
				//TODO: Insert def and all sub-objects where IsNew.
				HL7DefCur.IsNew=false;
			}
			else {
				//TODO: Update def and all sub-objects.
			}
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}
	}
}