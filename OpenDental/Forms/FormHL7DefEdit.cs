using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;
using OpenDentBusiness.HL7;
using OpenDental.UI;

namespace OpenDental {
	/// <summary></summary>
	public partial class FormHL7DefEdit:System.Windows.Forms.Form {
		private FolderBrowserDialog fb;
		public HL7Def HL7DefCur;
		public bool IsInternal;

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
				grid1.Enabled=false;
			}
			FillGrid1();
			this.comboModeTx.Items.Add(Lan.g(this,"File"));
			this.comboModeTx.Items.Add(Lan.g(this,"TcpIp"));
			this.textDescription.Text = HL7DefCur.Description;
			this.checkBoxIsInternal.Checked = HL7DefCur.IsInternal;
			this.checkEnabled.Checked = HL7DefCur.IsEnabled;
			this.textInternalType.Text = HL7DefCur.InternalType;
			this.textInternalTypeVersion.Text = HL7DefCur.InternalTypeVersion;
			this.textInPort.Text = HL7DefCur.IncomingPort;
			this.textInPath.Text = HL7DefCur.IncomingFolder;
			this.textOutPort.Text = HL7DefCur.OutgoingIpPort;
			this.textOutPath.Text = HL7DefCur.OutgoingFolder;
			this.textFieldSep.Text = HL7DefCur.FieldSeparator;
			this.textRepSep.Text = HL7DefCur.RepetitionSeparator;
			this.textCompSep.Text = HL7DefCur.ComponentSeparator;
			this.textSubcompSep.Text = HL7DefCur.SubcomponentSeparator;
			this.textEscChar.Text = HL7DefCur.EscapeCharacter;
			this.textNote.Text = HL7DefCur.Note;
			if(HL7DefCur.ModeTx.ToString().CompareTo("File")==0) {
				this.comboModeTx.SelectedIndex=0;
				this.textInPort.ReadOnly = true;
				this.textInPath.ReadOnly = false;
				this.textOutPort.ReadOnly = true;
				this.textOutPath.ReadOnly = false;
				this.butBrowseIn.Enabled = true;
				this.butBrowseOut.Enabled = true;
			}
			else {
				this.comboModeTx.SelectedIndex=1;
				this.textInPort.ReadOnly = false;
				this.textInPath.ReadOnly = true;
				this.textOutPort.ReadOnly = false;
				this.textOutPath.ReadOnly = true;
				this.butBrowseIn.Enabled = false;
				this.butBrowseOut.Enabled = false;
			}
			if(this.IsInternal) {
				this.textDescription.ReadOnly = true;
				this.textFieldSep.ReadOnly = true;
				this.textFieldSep.ReadOnly = true;
				this.textRepSep.ReadOnly = true;
				this.textCompSep.ReadOnly = true;
				this.textSubcompSep.ReadOnly = true;
				this.textEscChar.ReadOnly = true;
			}
		}

		private void FillGrid1() {
			grid1.BeginUpdate();
			grid1.Columns.Clear();
			ODGridColumn col=new ODGridColumn(Lan.g(this,"MessageType"),100);
			grid1.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"EventType"),100);
			grid1.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"InOrOut"),70);
			grid1.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"Note"),100);
			grid1.Columns.Add(col);
			grid1.Rows.Clear();
			ODGridRow row=new ODGridRow();
			row.Cells.Add("ADT");
			row.Cells.Add("A04");
			row.Cells.Add("In");
			row.Cells.Add("Blah");
			grid1.Rows.Add(row);
			grid1.EndUpdate();
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

		private void grid1_CellDoubleClick(object sender,ODGridClickEventArgs e) {
			FormHL7DefMessageEdit FormS=new FormHL7DefMessageEdit();
			FormS.ShowDialog();
		}

		private void comboModeTx_SelectedIndexChanged(object sender,System.EventArgs e) {
			if(this.comboModeTx.SelectedIndex==0) {
				this.textInPort.ReadOnly = true;
				this.textInPath.ReadOnly = false;
				this.textOutPort.ReadOnly = true;
				this.textOutPath.ReadOnly = false;
				this.butBrowseIn.Enabled = true;
				this.butBrowseOut.Enabled = true;
			}
			else if(this.comboModeTx.SelectedIndex==1) {
				this.textInPort.ReadOnly = false;
				this.textInPath.ReadOnly = true;
				this.textOutPort.ReadOnly = false;
				this.textOutPath.ReadOnly = true;
				this.butBrowseIn.Enabled = false;
				this.butBrowseOut.Enabled = false;
			}
		}

		private void butOK_Click(object sender,EventArgs e) {
			if(this.comboModeTx.SelectedIndex==0) {//File mode
				if(this.textInPath.Text!="") {
					if(!Directory.Exists(this.textInPath.Text)) {
						MsgBox.Show(this,"The path for Incoming Folder is invalid.");
						return;
					}
				}
				else {//If file mode selected but path left blank
					MsgBox.Show(this,"The path for Incoming Folder is empty.");
					return;
				}
				if(this.textOutPath.Text!="") {
					if(!Directory.Exists(this.textOutPath.Text)) {
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
				if(this.textInPort.Text=="") {
					MsgBox.Show(this,"The Incoming Port is empty.");
					return;
				}
				if(this.textOutPort.Text=="") {
					MsgBox.Show(this,"The Outgoing Port is empty.");
					return;
				}
			}
			HL7DefCur.Description = this.textDescription.Text;
			HL7DefCur.IsEnabled = this.checkEnabled.Checked;
			HL7DefCur.FieldSeparator = this.textFieldSep.Text;
			HL7DefCur.RepetitionSeparator = this.textRepSep.Text;
			HL7DefCur.ComponentSeparator = this.textCompSep.Text;
			HL7DefCur.SubcomponentSeparator = this.textSubcompSep.Text;
			HL7DefCur.EscapeCharacter = this.textEscChar.Text;
			HL7DefCur.Note = this.textNote.Text;
			if(this.comboModeTx.SelectedIndex==0) {//File mode
				HL7DefCur.IncomingFolder = this.textInPath.Text;
				HL7DefCur.OutgoingFolder = this.textOutPath.Text;
				HL7DefCur.ModeTx = ModeTxHL7.File;
			}
			else {//TcpIp mode
				HL7DefCur.IncomingPort = this.textInPort.Text;
				HL7DefCur.OutgoingIpPort = this.textOutPort.Text;
				HL7DefCur.ModeTx = ModeTxHL7.TcpIp;
			}
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

	}
}