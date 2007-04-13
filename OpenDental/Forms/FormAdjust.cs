/*=============================================================================================================
Open Dental GPL license Copyright (C) 2003  Jordan Sparks, DMD.  http://www.open-dent.com,  www.docsparks.com
See header in FormOpenDental.cs for complete text.  Redistributions must retain this text.
===============================================================================================================*/
using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{
	///<summary></summary>
	public class FormAdjust : System.Windows.Forms.Form{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label2;
		private System.ComponentModel.Container components = null;// Required designer variable.
		///<summary></summary>
		public bool IsNew;
		private OpenDental.UI.Button butOK;
		private OpenDental.UI.Button butCancel;
		private System.Windows.Forms.Label label3;
		private OpenDental.UI.Button butDelete;
		private OpenDental.ValidDouble textAmount;
		private System.Windows.Forms.ListBox listProvider;
		private System.Windows.Forms.ListBox listTypePos;
		private System.Windows.Forms.ListBox listTypeNeg;
		private ArrayList PosIndex=new ArrayList();
		private OpenDental.ODtextBox textNote;
		private ArrayList NegIndex=new ArrayList();
		private Patient PatCur;
		private OpenDental.ValidDate textProcDate;
		private System.Windows.Forms.Label label7;
		private OpenDental.ValidDate textAdjDate;
		private Adjustment AdjustmentCur;
		private OpenDental.ValidDate textDateEntry;
		private System.Windows.Forms.Label label8;
		///<summary></summary>
		private DateTime dateLimit=DateTime.MinValue;
		//<summary>Keeps track of current server time so that user cannot bypass security by altering workstation clock.  Sometimes we compare to nowDate, but sometimes, we're just interested in the date of the adjustment.</summary>
		//private DateTime nowDate;

		///<summary></summary>
		public FormAdjust(Patient patCur,Adjustment adjustmentCur){
			InitializeComponent();
			PatCur=patCur;
			AdjustmentCur=adjustmentCur;
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

		private void InitializeComponent(){
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormAdjust));
			this.label1 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.butOK = new OpenDental.UI.Button();
			this.butCancel = new OpenDental.UI.Button();
			this.textAdjDate = new OpenDental.ValidDate();
			this.label3 = new System.Windows.Forms.Label();
			this.butDelete = new OpenDental.UI.Button();
			this.textAmount = new OpenDental.ValidDouble();
			this.listProvider = new System.Windows.Forms.ListBox();
			this.listTypePos = new System.Windows.Forms.ListBox();
			this.listTypeNeg = new System.Windows.Forms.ListBox();
			this.textNote = new OpenDental.ODtextBox();
			this.textProcDate = new OpenDental.ValidDate();
			this.label7 = new System.Windows.Forms.Label();
			this.textDateEntry = new OpenDental.ValidDate();
			this.label8 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(7,54);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(104,16);
			this.label1.TabIndex = 0;
			this.label1.Text = "Adjustment Date";
			this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(175,333);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(100,16);
			this.label4.TabIndex = 3;
			this.label4.Text = "Note";
			this.label4.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(11,102);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(100,16);
			this.label5.TabIndex = 4;
			this.label5.Text = "Amount";
			this.label5.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(282,14);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(167,16);
			this.label6.TabIndex = 5;
			this.label6.Text = "Additions";
			this.label6.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(10,126);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(100,16);
			this.label2.TabIndex = 10;
			this.label2.Text = "Provider";
			this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.CornerRadius = 4F;
			this.butOK.Location = new System.Drawing.Point(614,433);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75,26);
			this.butOK.TabIndex = 6;
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
			this.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butCancel.Location = new System.Drawing.Point(614,471);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75,26);
			this.butCancel.TabIndex = 7;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// textAdjDate
			// 
			this.textAdjDate.Location = new System.Drawing.Point(112,52);
			this.textAdjDate.Name = "textAdjDate";
			this.textAdjDate.Size = new System.Drawing.Size(80,20);
			this.textAdjDate.TabIndex = 0;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(495,14);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(182,16);
			this.label3.TabIndex = 16;
			this.label3.Text = "Subtractions";
			this.label3.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// butDelete
			// 
			this.butDelete.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butDelete.Autosize = true;
			this.butDelete.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butDelete.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butDelete.CornerRadius = 4F;
			this.butDelete.Location = new System.Drawing.Point(24,469);
			this.butDelete.Name = "butDelete";
			this.butDelete.Size = new System.Drawing.Size(75,26);
			this.butDelete.TabIndex = 17;
			this.butDelete.Text = "&Delete";
			this.butDelete.Click += new System.EventHandler(this.butDelete_Click);
			// 
			// textAmount
			// 
			this.textAmount.Location = new System.Drawing.Point(112,100);
			this.textAmount.Name = "textAmount";
			this.textAmount.Size = new System.Drawing.Size(68,20);
			this.textAmount.TabIndex = 1;
			// 
			// listProvider
			// 
			this.listProvider.Location = new System.Drawing.Point(112,124);
			this.listProvider.Name = "listProvider";
			this.listProvider.Size = new System.Drawing.Size(100,95);
			this.listProvider.TabIndex = 2;
			// 
			// listTypePos
			// 
			this.listTypePos.Location = new System.Drawing.Point(266,34);
			this.listTypePos.Name = "listTypePos";
			this.listTypePos.Size = new System.Drawing.Size(202,264);
			this.listTypePos.TabIndex = 3;
			this.listTypePos.SelectedIndexChanged += new System.EventHandler(this.listTypePos_SelectedIndexChanged);
			// 
			// listTypeNeg
			// 
			this.listTypeNeg.Location = new System.Drawing.Point(482,34);
			this.listTypeNeg.Name = "listTypeNeg";
			this.listTypeNeg.Size = new System.Drawing.Size(206,264);
			this.listTypeNeg.TabIndex = 4;
			this.listTypeNeg.SelectedIndexChanged += new System.EventHandler(this.listTypeNeg_SelectedIndexChanged);
			// 
			// textNote
			// 
			this.textNote.AcceptsReturn = true;
			this.textNote.Location = new System.Drawing.Point(176,354);
			this.textNote.Multiline = true;
			this.textNote.Name = "textNote";
			this.textNote.QuickPasteType = OpenDentBusiness.QuickPasteType.Adjustment;
			this.textNote.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textNote.Size = new System.Drawing.Size(355,140);
			this.textNote.TabIndex = 0;
			// 
			// textProcDate
			// 
			this.textProcDate.Location = new System.Drawing.Point(112,76);
			this.textProcDate.Name = "textProcDate";
			this.textProcDate.Size = new System.Drawing.Size(80,20);
			this.textProcDate.TabIndex = 19;
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(7,78);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(104,16);
			this.label7.TabIndex = 18;
			this.label7.Text = "Procedure Date";
			this.label7.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textDateEntry
			// 
			this.textDateEntry.Location = new System.Drawing.Point(112,28);
			this.textDateEntry.Name = "textDateEntry";
			this.textDateEntry.ReadOnly = true;
			this.textDateEntry.Size = new System.Drawing.Size(80,20);
			this.textDateEntry.TabIndex = 21;
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(7,30);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(104,16);
			this.label8.TabIndex = 20;
			this.label8.Text = "Entry Date";
			this.label8.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// FormAdjust
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.CancelButton = this.butCancel;
			this.ClientSize = new System.Drawing.Size(731,528);
			this.Controls.Add(this.textDateEntry);
			this.Controls.Add(this.label8);
			this.Controls.Add(this.textProcDate);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.textNote);
			this.Controls.Add(this.listTypeNeg);
			this.Controls.Add(this.listTypePos);
			this.Controls.Add(this.listProvider);
			this.Controls.Add(this.textAmount);
			this.Controls.Add(this.butDelete);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.textAdjDate);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormAdjust";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Edit Adjustment";
			this.Load += new System.EventHandler(this.FormAdjust_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		private void FormAdjust_Load(object sender, System.EventArgs e) {
			if(IsNew){
				if(!Security.IsAuthorized(Permissions.AdjustmentCreate)){
					DialogResult=DialogResult.Cancel;
					return;
				}
			}
			else{
				if(!Security.IsAuthorized(Permissions.AdjustmentEdit,AdjustmentCur.DateEntry)){
					butOK.Enabled=false;
					butDelete.Enabled=false;
				}
			}
			textDateEntry.Text=AdjustmentCur.DateEntry.ToShortDateString();
			textAdjDate.Text=AdjustmentCur.AdjDate.ToShortDateString();
			textProcDate.Text=AdjustmentCur.ProcDate.ToShortDateString();
			if(DefB.GetValue(DefCat.AdjTypes,AdjustmentCur.AdjType)=="+"){//pos
				textAmount.Text=AdjustmentCur.AdjAmt.ToString("F");
			}
			else{//neg
				textAmount.Text=(-AdjustmentCur.AdjAmt).ToString("F");//shows without the neg sign
			}
			for(int i=0;i<Providers.List.Length;i++){
				this.listProvider.Items.Add(Providers.List[i].Abbr);
				if(Providers.List[i].ProvNum==AdjustmentCur.ProvNum)
					listProvider.SelectedIndex=i;
			}				
			for(int i=0;i<DefB.Short[1].Length;i++){//temp.AdjType
				if(DefB.Short[1][i].ItemValue=="+"){
					PosIndex.Add(i);
					listTypePos.Items.Add(DefB.Short[1][i].ItemName);
					if(DefB.Short[1][i].DefNum==AdjustmentCur.AdjType)
						listTypePos.SelectedIndex=PosIndex.Count-1;
				}
				else if(DefB.Short[1][i].ItemValue=="-"){
					NegIndex.Add(i);
					listTypeNeg.Items.Add(DefB.Short[1][i].ItemName);
					if(DefB.Short[1][i].DefNum==AdjustmentCur.AdjType)
						listTypeNeg.SelectedIndex=NegIndex.Count-1;
				}
			}
			//this.listProvNum.SelectedIndex=(int)temp.ProvNum;
			this.textNote.Text=AdjustmentCur.AdjNote;
		}

		private void listTypePos_SelectedIndexChanged(object sender, System.EventArgs e) {
			if(listTypePos.SelectedIndex!=-1) listTypeNeg.SelectedIndex=-1;
		}

		private void listTypeNeg_SelectedIndexChanged(object sender, System.EventArgs e) {
			if(listTypeNeg.SelectedIndex!=-1)	listTypePos.SelectedIndex=-1;
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			if( textAdjDate.errorProvider1.GetError(textAdjDate)!=""
				|| textProcDate.errorProvider1.GetError(textProcDate)!=""
				|| textAmount.errorProvider1.GetError(textAmount)!=""
				){
				MsgBox.Show(this,"Please fix data entry errors first.");
				return;
			}
			if(textAmount.Text==""){
				MessageBox.Show(Lan.g(this,"Please enter an amount."));	
				return;
			}
			if(listTypeNeg.SelectedIndex==-1 && listTypePos.SelectedIndex==-1){
				MsgBox.Show(this,"Please select a type first.");
				return;
			}
			//DateEntry not allowed to change
			AdjustmentCur.AdjDate=PIn.PDate(textAdjDate.Text);
			AdjustmentCur.ProcDate=PIn.PDate(textProcDate.Text);
			if(listProvider.SelectedIndex==-1)
				AdjustmentCur.ProvNum=PatCur.PriProv;
			else
				AdjustmentCur.ProvNum=Providers.List[this.listProvider.SelectedIndex].ProvNum;
			if(listTypePos.SelectedIndex!=-1){
				AdjustmentCur.AdjType
					=DefB.Short[(int)DefCat.AdjTypes][(int)PosIndex[listTypePos.SelectedIndex]].DefNum;
			}
			if(listTypeNeg.SelectedIndex!=-1){
				AdjustmentCur.AdjType
					=DefB.Short[(int)DefCat.AdjTypes][(int)NegIndex[listTypeNeg.SelectedIndex]].DefNum;
			}
			if(DefB.GetValue(DefCat.AdjTypes,AdjustmentCur.AdjType)=="+"){//pos
				AdjustmentCur.AdjAmt=PIn.PDouble(textAmount.Text);
			}
			else{//neg
				AdjustmentCur.AdjAmt=-PIn.PDouble(textAmount.Text);
			}
			AdjustmentCur.AdjNote=textNote.Text;
			try{
				Adjustments.InsertOrUpdate(AdjustmentCur,IsNew);
			}
			catch(Exception ex){//even though it doesn't currently throw any exceptions
				MessageBox.Show(ex.Message);
				return;
			}
			if(IsNew){
				SecurityLogs.MakeLogEntry(Permissions.AdjustmentCreate,AdjustmentCur.PatNum,
					Patients.GetLim(AdjustmentCur.PatNum).GetNameLF()+", "
					+AdjustmentCur.AdjAmt.ToString("c"));
			}
			else{
				SecurityLogs.MakeLogEntry(Permissions.AdjustmentEdit,AdjustmentCur.PatNum,
					Patients.GetLim(AdjustmentCur.PatNum).GetNameLF()+", "
					+AdjustmentCur.AdjAmt.ToString("c"));
			}
			DialogResult=DialogResult.OK;
		}

		private void butDelete_Click(object sender, System.EventArgs e) {
			if(IsNew){
				DialogResult=DialogResult.Cancel;
			}
			else{
				SecurityLogs.MakeLogEntry(Permissions.AdjustmentEdit,AdjustmentCur.PatNum,
					"Delete for patient: "
					+Patients.GetLim(AdjustmentCur.PatNum).GetNameLF()+", "
					+AdjustmentCur.AdjAmt.ToString("c"));
				Adjustments.Delete(AdjustmentCur);
				DialogResult=DialogResult.OK;
			}
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

	}

	///<summary></summary>
	public struct AdjustmentItem{
		///<summary></summary>
		public string ItemText;
		///<summary></summary>
		public int ItemIndex;
	}

}
