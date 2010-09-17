/*=============================================================================================================
Open Dental GPL license Copyright (C) 2003  Jordan Sparks, DMD.  http://www.open-dent.com,  www.docsparks.com
See header in FormOpenDental.cs for complete text.  Redistributions must retain this text.
===============================================================================================================*/
using System;
using System.Diagnostics;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Text;
using Microsoft.Win32;
using OpenDentBusiness;
using CodeBase;
using SparksToothChart;
using OpenDental.UI;


namespace OpenDental{
///<summary></summary>
	public class FormProcGroup:System.Windows.Forms.Form {
		private System.Windows.Forms.Label label7;
		private OpenDental.UI.Button butOK;
		private OpenDental.UI.Button butCancel;
		private OpenDental.UI.Button butDelete;
		private ODErrorProvider errorProvider2=new ODErrorProvider();
		private OpenDental.ODtextBox textNotes;
		private Label label15;
		private Label label16;
		private TextBox textUser;
		private OpenDental.UI.Button buttonUseAutoNote;
		public List<ClaimProcHist> HistList;
		private ODGrid gridProc;
		private SignatureBoxWrapper signatureBoxWrapper;
		private Label label12;
		private ValidDate textDateEntry;
		private Label label26;
		private ValidDate textProcDate;
		private Label label2;
		public Procedure GroupCur;
		public Procedure GroupOld;
		public List<ProcGroupItem> GroupItemList;
		public List<Procedure> ProcList;

		public FormProcGroup() {
			InitializeComponent();
			Lan.F(this);
		}

		///<summary>Inserts are no longer done within this dialog, but must be done ahead of time from outside.You must specify a procedure to edit, and only the changes that are made in this dialog get saved.  Only used when double click in Account, Chart, TP, and in ContrChart.AddProcedure().  The procedure may be deleted if new, and user hits Cancel.</summary>

		//Constructor from ProcEdit. Lots of this will need to be copied into the new Load function.
		/*public FormProcGroup(long groupNum) {
			GroupCur=Procedures.GetOneProc(groupNum,true);
			ProcGroupItem=ProcGroupItems.Refresh(groupNum);
			//Proc
			InitializeComponent();
			Lan.F(this);
		}*/

		#region Windows Form Designer generated code

		private void InitializeComponent(){
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormProcGroup));
			this.label7 = new System.Windows.Forms.Label();
			this.label15 = new System.Windows.Forms.Label();
			this.label16 = new System.Windows.Forms.Label();
			this.textUser = new System.Windows.Forms.TextBox();
			this.label12 = new System.Windows.Forms.Label();
			this.label26 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.textProcDate = new OpenDental.ValidDate();
			this.signatureBoxWrapper = new OpenDental.UI.SignatureBoxWrapper();
			this.gridProc = new OpenDental.UI.ODGrid();
			this.textDateEntry = new OpenDental.ValidDate();
			this.buttonUseAutoNote = new OpenDental.UI.Button();
			this.textNotes = new OpenDental.ODtextBox();
			this.butDelete = new OpenDental.UI.Button();
			this.butCancel = new OpenDental.UI.Button();
			this.butOK = new OpenDental.UI.Button();
			this.SuspendLayout();
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(60,78);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(73,16);
			this.label7.TabIndex = 0;
			this.label7.Text = "&Notes";
			this.label7.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label15
			// 
			this.label15.Location = new System.Drawing.Point(20,278);
			this.label15.Name = "label15";
			this.label15.Size = new System.Drawing.Size(110,41);
			this.label15.TabIndex = 79;
			this.label15.Text = "Signature /\r\nInitials";
			this.label15.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label16
			// 
			this.label16.Location = new System.Drawing.Point(60,55);
			this.label16.Name = "label16";
			this.label16.Size = new System.Drawing.Size(73,16);
			this.label16.TabIndex = 80;
			this.label16.Text = "User";
			this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textUser
			// 
			this.textUser.Location = new System.Drawing.Point(135,52);
			this.textUser.Name = "textUser";
			this.textUser.ReadOnly = true;
			this.textUser.Size = new System.Drawing.Size(119,20);
			this.textUser.TabIndex = 101;
			// 
			// label12
			// 
			this.label12.Location = new System.Drawing.Point(10,34);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(125,14);
			this.label12.TabIndex = 96;
			this.label12.Text = "Date Entry";
			this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label26
			// 
			this.label26.Location = new System.Drawing.Point(213,34);
			this.label26.Name = "label26";
			this.label26.Size = new System.Drawing.Size(125,18);
			this.label26.TabIndex = 97;
			this.label26.Text = "(for security)";
			this.label26.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(37,14);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(96,14);
			this.label2.TabIndex = 101;
			this.label2.Text = "Procedure Date";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textProcDate
			// 
			this.textProcDate.Location = new System.Drawing.Point(133,12);
			this.textProcDate.Name = "textProcDate";
			this.textProcDate.ReadOnly = true;
			this.textProcDate.Size = new System.Drawing.Size(76,20);
			this.textProcDate.TabIndex = 100;
			// 
			// signatureBoxWrapper
			// 
			this.signatureBoxWrapper.BackColor = System.Drawing.SystemColors.ControlDark;
			this.signatureBoxWrapper.LabelText = "Invalid Signature";
			this.signatureBoxWrapper.Location = new System.Drawing.Point(135,276);
			this.signatureBoxWrapper.Name = "signatureBoxWrapper";
			this.signatureBoxWrapper.Size = new System.Drawing.Size(364,81);
			this.signatureBoxWrapper.TabIndex = 194;
			// 
			// gridProc
			// 
			this.gridProc.HScrollVisible = true;
			this.gridProc.Location = new System.Drawing.Point(26,367);
			this.gridProc.Name = "gridProc";
			this.gridProc.ScrollValue = 0;
			this.gridProc.SelectionMode = OpenDental.UI.GridSelectionMode.MultiExtended;
			this.gridProc.Size = new System.Drawing.Size(575,257);
			this.gridProc.TabIndex = 193;
			this.gridProc.Title = "Procedures";
			this.gridProc.TranslationName = "TableProg";
			// 
			// textDateEntry
			// 
			this.textDateEntry.Location = new System.Drawing.Point(133,32);
			this.textDateEntry.Name = "textDateEntry";
			this.textDateEntry.ReadOnly = true;
			this.textDateEntry.Size = new System.Drawing.Size(76,20);
			this.textDateEntry.TabIndex = 95;
			// 
			// buttonUseAutoNote
			// 
			this.buttonUseAutoNote.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.buttonUseAutoNote.Autosize = true;
			this.buttonUseAutoNote.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.buttonUseAutoNote.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.buttonUseAutoNote.CornerRadius = 4F;
			this.buttonUseAutoNote.Location = new System.Drawing.Point(419,42);
			this.buttonUseAutoNote.Name = "buttonUseAutoNote";
			this.buttonUseAutoNote.Size = new System.Drawing.Size(80,24);
			this.buttonUseAutoNote.TabIndex = 106;
			this.buttonUseAutoNote.Text = "Auto Note";
			this.buttonUseAutoNote.Click += new System.EventHandler(this.buttonUseAutoNote_Click);
			// 
			// textNotes
			// 
			this.textNotes.AcceptsReturn = true;
			this.textNotes.AcceptsTab = true;
			this.textNotes.Location = new System.Drawing.Point(135,72);
			this.textNotes.Multiline = true;
			this.textNotes.Name = "textNotes";
			this.textNotes.QuickPasteType = OpenDentBusiness.QuickPasteType.Procedure;
			this.textNotes.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textNotes.Size = new System.Drawing.Size(364,200);
			this.textNotes.TabIndex = 1;
			this.textNotes.TextChanged += new System.EventHandler(this.textNotes_TextChanged);
			// 
			// butDelete
			// 
			this.butDelete.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.butDelete.Autosize = true;
			this.butDelete.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butDelete.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butDelete.CornerRadius = 4F;
			this.butDelete.Image = global::OpenDental.Properties.Resources.deleteX;
			this.butDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butDelete.Location = new System.Drawing.Point(26,648);
			this.butDelete.Name = "butDelete";
			this.butDelete.Size = new System.Drawing.Size(83,24);
			this.butDelete.TabIndex = 8;
			this.butDelete.Text = "&Delete";
			this.butDelete.Click += new System.EventHandler(this.butDelete_Click);
			// 
			// butCancel
			// 
			this.butCancel.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.butCancel.Autosize = true;
			this.butCancel.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butCancel.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butCancel.CornerRadius = 4F;
			this.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butCancel.Location = new System.Drawing.Point(525,648);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(76,24);
			this.butCancel.TabIndex = 13;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.CornerRadius = 4F;
			this.butOK.Location = new System.Drawing.Point(443,648);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(76,24);
			this.butOK.TabIndex = 12;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// FormProcGroup
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.ClientSize = new System.Drawing.Size(627,696);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.textProcDate);
			this.Controls.Add(this.signatureBoxWrapper);
			this.Controls.Add(this.label26);
			this.Controls.Add(this.gridProc);
			this.Controls.Add(this.textDateEntry);
			this.Controls.Add(this.buttonUseAutoNote);
			this.Controls.Add(this.label12);
			this.Controls.Add(this.textUser);
			this.Controls.Add(this.textNotes);
			this.Controls.Add(this.label16);
			this.Controls.Add(this.label15);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.butDelete);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.butOK);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormProcGroup";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Procedure Group Note";
			this.Load += new System.EventHandler(this.FormProcGroup_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		//Load function from FormProcEdit (FormProcInfo_Load)... (Single procedure - but form looks like I want)
		private void FormProcGroup_Load(object sender, System.EventArgs e){
			GroupOld=GroupCur.Copy();
			textProcDate.Text=GroupCur.ProcDate.ToShortDateString();
			textDateEntry.Text=GroupCur.DateEntryC.ToShortDateString();
			textUser.Text=Security.CurUser.UserName;
			textNotes.Text=GroupCur.Note;
			FillProcedures();
			string keyData=GetSignatureKey();
			signatureBoxWrapper.FillSignature(GroupCur.SigIsTopaz,keyData,GroupCur.Signature);
			signatureBoxWrapper.BringToFront();
		}

		private void FillProcedures(){
			gridProc.BeginUpdate();
			gridProc.Columns.Clear();
			ODGridColumn col=new ODGridColumn(Lan.g("TableGroupProcs","Date"),68);
			gridProc.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableGroupProcs","Th"),25);
			gridProc.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableGroupProcs","Surf"),40);
			gridProc.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableGroupProcs","Description"),247);
			gridProc.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableGroupProcs","Stat"),25);
			gridProc.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableGroupProcs","Prov"),42);
			gridProc.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableGroupProcs","Amount"),48,HorizontalAlignment.Right);
			gridProc.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableGroupProcs","ADA Code"),60,HorizontalAlignment.Center);
			gridProc.Columns.Add(col);
			gridProc.Rows.Clear();
			ODGridRow row;
			for(int i=0;i<ProcList.Count;i++){
				row=new ODGridRow();			
				row.Cells.Add(ProcList[i].ProcDate.ToShortDateString());
				row.Cells.Add(ProcList[i].ToothNum.ToString());
				row.Cells.Add(ProcList[i].Surf.ToString());
				row.Cells.Add(ProcedureCodes.GetLaymanTerm(ProcList[i].CodeNum));
				row.Cells.Add(ProcList[i].ProcStatus.ToString());
				row.Cells.Add(Providers.GetAbbr(ProcList[i].ProvNum));
				row.Cells.Add(ProcList[i].ProcFee.ToString("F"));
				row.Cells.Add(ProcedureCodes.GetStringProcCode(ProcList[i].CodeNum));
				gridProc.Rows.Add(row);
			}
			gridProc.EndUpdate();
		}

		private void buttonUseAutoNote_Click(object sender,EventArgs e) {
			FormAutoNoteCompose FormA=new FormAutoNoteCompose();
			FormA.ShowDialog();
			if(FormA.DialogResult==DialogResult.OK) {
				textNotes.AppendText(FormA.CompletedNote);
			}
		}

		private void textNotes_TextChanged(object sender,EventArgs e) {
			signatureBoxWrapper.SetInvalid();
		}

		private string GetSignatureKey(){
			string keyData=GroupCur.ProcDate.ToShortDateString();
			keyData+=GroupCur.DateEntryC.ToShortDateString();
			keyData+=Security.CurUser.UserName;
			keyData+=GroupCur.Note;
			for(int i=0;i<GroupItemList.Count;i++){
				keyData+=GroupItemList[i].ProcGroupItemNum.ToString();
			}
			return keyData;
		}

		private void SaveSignature(){
			if(signatureBoxWrapper.GetSigChanged()){
					string keyData=GetSignatureKey();
					GroupCur.Signature=signatureBoxWrapper.GetSignature(keyData);
					GroupCur.SigIsTopaz=signatureBoxWrapper.GetSigIsTopaz();
			}
		}

		private void butDelete_Click(object sender, System.EventArgs e) {
			bool result=MsgBox.Show(this,MsgBoxButtons.YesNo,"Are you sure you want delete this procedure group note?");
			if(result){
				Procedures.Delete(GroupCur.ProcNum);
				for(int i=0;i<GroupItemList.Count;i++){
					ProcGroupItems.Delete(GroupItemList[i].ProcGroupItemNum);
				}
				DialogResult=DialogResult.Cancel;
			}
			return;
		}		

		private void butOK_Click(object sender,System.EventArgs e) {
			GroupCur.Note=textNotes.Text;
			if(textNotes.Text.Trim()==""){
				MsgBox.Show(this,"You cannot save a blank message. Please enter something into the notes field.");
				return;
			}
			if(!signatureBoxWrapper.IsValid){
				MsgBox.Show(this,"Your signature is invalid. Please sign and click OK again.");
				return;
			}
			try {
				SaveSignature();
			}
			catch(Exception ex){
				MessageBox.Show(Lan.g(this,"Error saving signature.")+"\r\n"+ex.Message);
			}
			Procedures.Update(GroupCur,GroupOld);
      DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,System.EventArgs e) {
			if(GroupCur.IsNew){
				Procedures.Delete(GroupCur.ProcNum);
				for(int i=0;i<GroupItemList.Count;i++){
					ProcGroupItems.Delete(GroupItemList[i].ProcGroupItemNum);
				}
			}
			DialogResult=DialogResult.Cancel;
		}










	}
}
