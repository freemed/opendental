using System;
using System.Data;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{
	/// <summary>
	/// Summary description for FormBasicTemplate.
	/// </summary>
	public class FormFeeSchedTools : System.Windows.Forms.Form{
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label1;
		private OpenDental.UI.Button butCopy;
		private OpenDental.UI.Button butClose;
		private System.Windows.Forms.ComboBox comboCopyFrom;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.GroupBox groupBox2;
		private OpenDental.UI.Button butClear;
		private System.Windows.Forms.GroupBox groupBox3;
		private OpenDental.UI.Button butIncrease;
		private System.Windows.Forms.TextBox textPercent;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.GroupBox groupBox4;
		private System.Windows.Forms.RadioButton radioDollar;
		private System.Windows.Forms.RadioButton radioDime;
		private System.Windows.Forms.RadioButton radioPenny;
		private GroupBox groupBox5;
		private OpenDental.UI.Button butExport;
		private OpenDental.UI.Button butImport;
		private GroupBox groupBox6;
		private Label label4;
		private OpenDental.UI.Button butUpdate;
		private Label label5;
		private GroupBox groupBox7;
		private OpenDental.UI.Button butIns;
		private Label label6;
		///<summary>The defNum of the fee schedule that is currently displayed in the main window.</summary>
		private int SchedNum;

		///<summary>Supply the fee schedule num(DefNum) to which all these changes will apply</summary>
		public FormFeeSchedTools(int schedNum)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			Lan.F(this);
			SchedNum=schedNum;
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormFeeSchedTools));
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.butCopy = new OpenDental.UI.Button();
			this.comboCopyFrom = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.butClear = new OpenDental.UI.Button();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.label5 = new System.Windows.Forms.Label();
			this.groupBox4 = new System.Windows.Forms.GroupBox();
			this.radioPenny = new System.Windows.Forms.RadioButton();
			this.radioDime = new System.Windows.Forms.RadioButton();
			this.radioDollar = new System.Windows.Forms.RadioButton();
			this.label3 = new System.Windows.Forms.Label();
			this.butIncrease = new OpenDental.UI.Button();
			this.textPercent = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.groupBox5 = new System.Windows.Forms.GroupBox();
			this.butImport = new OpenDental.UI.Button();
			this.butExport = new OpenDental.UI.Button();
			this.groupBox6 = new System.Windows.Forms.GroupBox();
			this.label4 = new System.Windows.Forms.Label();
			this.butUpdate = new OpenDental.UI.Button();
			this.butClose = new OpenDental.UI.Button();
			this.groupBox7 = new System.Windows.Forms.GroupBox();
			this.butIns = new OpenDental.UI.Button();
			this.label6 = new System.Windows.Forms.Label();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.groupBox4.SuspendLayout();
			this.groupBox5.SuspendLayout();
			this.groupBox6.SuspendLayout();
			this.groupBox7.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.butCopy);
			this.groupBox1.Controls.Add(this.comboCopyFrom);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox1.Location = new System.Drawing.Point(15,80);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(205,99);
			this.groupBox1.TabIndex = 2;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Copy";
			// 
			// butCopy
			// 
			this.butCopy.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butCopy.Autosize = true;
			this.butCopy.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butCopy.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butCopy.CornerRadius = 4F;
			this.butCopy.Location = new System.Drawing.Point(10,66);
			this.butCopy.Name = "butCopy";
			this.butCopy.Size = new System.Drawing.Size(75,26);
			this.butCopy.TabIndex = 4;
			this.butCopy.Text = "Copy";
			this.butCopy.Click += new System.EventHandler(this.butCopy_Click);
			// 
			// comboCopyFrom
			// 
			this.comboCopyFrom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboCopyFrom.Location = new System.Drawing.Point(11,40);
			this.comboCopyFrom.Name = "comboCopyFrom";
			this.comboCopyFrom.Size = new System.Drawing.Size(180,21);
			this.comboCopyFrom.TabIndex = 0;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(10,20);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(100,18);
			this.label1.TabIndex = 3;
			this.label1.Text = "From";
			this.label1.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.butClear);
			this.groupBox2.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox2.Location = new System.Drawing.Point(15,11);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(205,59);
			this.groupBox2.TabIndex = 3;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Clear";
			// 
			// butClear
			// 
			this.butClear.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butClear.Autosize = true;
			this.butClear.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butClear.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butClear.CornerRadius = 4F;
			this.butClear.Location = new System.Drawing.Point(10,25);
			this.butClear.Name = "butClear";
			this.butClear.Size = new System.Drawing.Size(75,26);
			this.butClear.TabIndex = 4;
			this.butClear.Text = "Clear";
			this.butClear.Click += new System.EventHandler(this.butClear_Click);
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.label5);
			this.groupBox3.Controls.Add(this.groupBox4);
			this.groupBox3.Controls.Add(this.label3);
			this.groupBox3.Controls.Add(this.butIncrease);
			this.groupBox3.Controls.Add(this.textPercent);
			this.groupBox3.Controls.Add(this.label2);
			this.groupBox3.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox3.Location = new System.Drawing.Point(236,11);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(205,168);
			this.groupBox3.TabIndex = 4;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Increase by %";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(92,143);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(69,13);
			this.label5.TabIndex = 11;
			this.label5.Text = "(or decrease)";
			// 
			// groupBox4
			// 
			this.groupBox4.Controls.Add(this.radioPenny);
			this.groupBox4.Controls.Add(this.radioDime);
			this.groupBox4.Controls.Add(this.radioDollar);
			this.groupBox4.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox4.Location = new System.Drawing.Point(13,47);
			this.groupBox4.Name = "groupBox4";
			this.groupBox4.Size = new System.Drawing.Size(169,78);
			this.groupBox4.TabIndex = 10;
			this.groupBox4.TabStop = false;
			this.groupBox4.Text = "Round to nearest";
			// 
			// radioPenny
			// 
			this.radioPenny.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.radioPenny.Location = new System.Drawing.Point(14,52);
			this.radioPenny.Name = "radioPenny";
			this.radioPenny.Size = new System.Drawing.Size(104,17);
			this.radioPenny.TabIndex = 2;
			this.radioPenny.Text = "$.01";
			// 
			// radioDime
			// 
			this.radioDime.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.radioDime.Location = new System.Drawing.Point(14,35);
			this.radioDime.Name = "radioDime";
			this.radioDime.Size = new System.Drawing.Size(104,17);
			this.radioDime.TabIndex = 1;
			this.radioDime.Text = "$.10";
			// 
			// radioDollar
			// 
			this.radioDollar.Checked = true;
			this.radioDollar.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.radioDollar.Location = new System.Drawing.Point(14,18);
			this.radioDollar.Name = "radioDollar";
			this.radioDollar.Size = new System.Drawing.Size(104,17);
			this.radioDollar.TabIndex = 0;
			this.radioDollar.TabStop = true;
			this.radioDollar.Text = "$1";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(92,24);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(109,19);
			this.label3.TabIndex = 6;
			this.label3.Text = "for example: 5";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// butIncrease
			// 
			this.butIncrease.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butIncrease.Autosize = true;
			this.butIncrease.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butIncrease.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butIncrease.CornerRadius = 4F;
			this.butIncrease.Location = new System.Drawing.Point(11,135);
			this.butIncrease.Name = "butIncrease";
			this.butIncrease.Size = new System.Drawing.Size(75,26);
			this.butIncrease.TabIndex = 4;
			this.butIncrease.Text = "Increase";
			this.butIncrease.Click += new System.EventHandler(this.butIncrease_Click);
			// 
			// textPercent
			// 
			this.textPercent.Location = new System.Drawing.Point(42,23);
			this.textPercent.Name = "textPercent";
			this.textPercent.Size = new System.Drawing.Size(46,20);
			this.textPercent.TabIndex = 5;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(3,23);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(38,19);
			this.label2.TabIndex = 5;
			this.label2.Text = "%";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// groupBox5
			// 
			this.groupBox5.Controls.Add(this.butImport);
			this.groupBox5.Controls.Add(this.butExport);
			this.groupBox5.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox5.Location = new System.Drawing.Point(15,188);
			this.groupBox5.Name = "groupBox5";
			this.groupBox5.Size = new System.Drawing.Size(205,59);
			this.groupBox5.TabIndex = 5;
			this.groupBox5.TabStop = false;
			this.groupBox5.Text = "Export/Import";
			// 
			// butImport
			// 
			this.butImport.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butImport.Autosize = true;
			this.butImport.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butImport.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butImport.CornerRadius = 4F;
			this.butImport.Location = new System.Drawing.Point(116,25);
			this.butImport.Name = "butImport";
			this.butImport.Size = new System.Drawing.Size(75,26);
			this.butImport.TabIndex = 5;
			this.butImport.Text = "Import";
			this.butImport.Click += new System.EventHandler(this.butImport_Click);
			// 
			// butExport
			// 
			this.butExport.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butExport.Autosize = true;
			this.butExport.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butExport.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butExport.CornerRadius = 4F;
			this.butExport.Location = new System.Drawing.Point(10,25);
			this.butExport.Name = "butExport";
			this.butExport.Size = new System.Drawing.Size(75,26);
			this.butExport.TabIndex = 4;
			this.butExport.Text = "Export";
			this.butExport.Click += new System.EventHandler(this.butExport_Click);
			// 
			// groupBox6
			// 
			this.groupBox6.Controls.Add(this.label4);
			this.groupBox6.Controls.Add(this.butUpdate);
			this.groupBox6.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox6.Location = new System.Drawing.Point(236,188);
			this.groupBox6.Name = "groupBox6";
			this.groupBox6.Size = new System.Drawing.Size(205,59);
			this.groupBox6.TabIndex = 6;
			this.groupBox6.TabStop = false;
			this.groupBox6.Text = "Global Update Fees";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(91,32);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(72,13);
			this.label4.TabIndex = 5;
			this.label4.Text = "for all patients";
			// 
			// butUpdate
			// 
			this.butUpdate.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butUpdate.Autosize = true;
			this.butUpdate.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butUpdate.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butUpdate.CornerRadius = 4F;
			this.butUpdate.Location = new System.Drawing.Point(10,25);
			this.butUpdate.Name = "butUpdate";
			this.butUpdate.Size = new System.Drawing.Size(75,26);
			this.butUpdate.TabIndex = 4;
			this.butUpdate.Text = "Update";
			this.butUpdate.Click += new System.EventHandler(this.butUpdate_Click);
			// 
			// butClose
			// 
			this.butClose.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butClose.Autosize = true;
			this.butClose.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butClose.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butClose.CornerRadius = 4F;
			this.butClose.Location = new System.Drawing.Point(406,319);
			this.butClose.Name = "butClose";
			this.butClose.Size = new System.Drawing.Size(75,26);
			this.butClose.TabIndex = 0;
			this.butClose.Text = "&Cancel";
			this.butClose.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// groupBox7
			// 
			this.groupBox7.Controls.Add(this.butIns);
			this.groupBox7.Controls.Add(this.label6);
			this.groupBox7.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox7.Location = new System.Drawing.Point(15,256);
			this.groupBox7.Name = "groupBox7";
			this.groupBox7.Size = new System.Drawing.Size(205,98);
			this.groupBox7.TabIndex = 7;
			this.groupBox7.TabStop = false;
			this.groupBox7.Text = "Check Ins Plan Fees";
			// 
			// butIns
			// 
			this.butIns.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butIns.Autosize = true;
			this.butIns.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butIns.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butIns.CornerRadius = 4F;
			this.butIns.Location = new System.Drawing.Point(116,62);
			this.butIns.Name = "butIns";
			this.butIns.Size = new System.Drawing.Size(75,26);
			this.butIns.TabIndex = 4;
			this.butIns.Text = "Go";
			this.butIns.Click += new System.EventHandler(this.butIns_Click);
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(6,16);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(196,47);
			this.label6.TabIndex = 5;
			this.label6.Text = "This tool will help make sure your insurance plans have the right fee schedules s" +
    "et.";
			// 
			// FormFeeSchedTools
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.ClientSize = new System.Drawing.Size(506,370);
			this.Controls.Add(this.groupBox7);
			this.Controls.Add(this.groupBox6);
			this.Controls.Add(this.groupBox5);
			this.Controls.Add(this.groupBox3);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.butClose);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormFeeSchedTools";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Fee Schedule Tools";
			this.Load += new System.EventHandler(this.FormFeeSchedTools_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.groupBox3.ResumeLayout(false);
			this.groupBox3.PerformLayout();
			this.groupBox4.ResumeLayout(false);
			this.groupBox5.ResumeLayout(false);
			this.groupBox6.ResumeLayout(false);
			this.groupBox6.PerformLayout();
			this.groupBox7.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormFeeSchedTools_Load(object sender, System.EventArgs e) {
			for(int i=0;i<DefB.Short[(int)DefCat.FeeSchedNames].Length;i++){
				comboCopyFrom.Items.Add(DefB.Short[(int)DefCat.FeeSchedNames][i].ItemName);
			}
		}

		private void butClear_Click(object sender, System.EventArgs e) {
			if(!MsgBox.Show(this,true,"This will clear all values from the current fee schedule showing in the main window.  Are you sure you want to continue?")){
				return;
			}
			Fees.ClearFeeSched(SchedNum);
			DialogResult=DialogResult.OK;
		}

		private void butCopy_Click(object sender, System.EventArgs e) {
			if(comboCopyFrom.SelectedIndex==-1){
				MsgBox.Show(this,"Please pick a fee schedule first.");
				return;
			}
			if(!MsgBox.Show(this,true,"This will overwrite all values of the current fee schedule showing in the main window.  Are you sure you want to continue?")){
				return;
			}
			//clear current
			Fees.ClearFeeSched(SchedNum);
			//copy any values over
			Fees.CopyFees(comboCopyFrom.SelectedIndex,SchedNum);
			DialogResult=DialogResult.OK;
		}

		private void butIncrease_Click(object sender, System.EventArgs e) {
			int percent=0;
			if(textPercent.Text==""){
				MsgBox.Show(this,"Please enter a percent first.");
				return;
			}
			try{
				percent=System.Convert.ToInt32(textPercent.Text);
			}
			catch{
				MsgBox.Show(this,"Percent is not a valid number.");
				return;
			}
			if(percent<-99 || percent>99){
				MsgBox.Show(this,"Percent must be between -99 and 99.");
				return;
			}
			if(!MsgBox.Show(this,true,"This will overwrite all values of the current fee schedule showing in the main window.  For this reason, you should be working on a copy.  Are you sure you want to continue?")){
				return;
			}
			
			int round=0;
			if(radioDime.Checked){
				round=1;
			}
			if(radioPenny.Checked){
				round=2;
			}
			Fees.Increase(DefB.GetOrder(DefCat.FeeSchedNames,SchedNum),percent,round);
			DialogResult=DialogResult.OK;
		}

		private void butExport_Click(object sender,EventArgs e) {
			Cursor=Cursors.WaitCursor;
			SaveFileDialog Dlg=new SaveFileDialog();
			if(Directory.Exists(PrefB.GetString("ExportPath"))){
				Dlg.InitialDirectory=PrefB.GetString("ExportPath");
			}else if(Directory.Exists("C:\\")){
				Dlg.InitialDirectory="C:\\";
			}
			Dlg.FileName="Fees"+DefB.GetName(DefCat.FeeSchedNames,SchedNum)+".txt";
			if(Dlg.ShowDialog()!=DialogResult.OK){
				Cursor=Cursors.Default;
				return;
			}
			//MessageBox.Show(Dlg.FileName);//includes full path
			//OverwritePrompt is already set to true
			DataTable table=ProcedureCodes.GetProcTable("","","",new int[0],SchedNum,0,0);
			double fee;
			using(StreamWriter sr=File.CreateText(Dlg.FileName)){
				for(int i=0;i<table.Rows.Count;i++){
					sr.Write(PIn.PString(table.Rows[i]["ProcCode"].ToString())+"\t");
					fee=PIn.PDouble(table.Rows[i]["FeeAmt1"].ToString());
					if(fee!=-1) {
						sr.Write(fee.ToString("n"));
					}
					sr.Write("\t");
					sr.Write(PIn.PString(table.Rows[i]["AbbrDesc"].ToString())+"\t");
					sr.WriteLine(PIn.PString(table.Rows[i]["Descript"].ToString()));
				}
			}
			Cursor=Cursors.Default;
			DialogResult=DialogResult.OK;
		}

		private void butImport_Click(object sender,EventArgs e) {
			if(!MsgBox.Show(this,true,"If you want a clean slate, the current fee schedule should be cleared first.  When imported, any fees that are found in the text file will overwrite values of the current fee schedule showing in the main window.  Are you sure you want to continue?")) {
				return;
			}
			Cursor=Cursors.WaitCursor;
			OpenFileDialog Dlg=new OpenFileDialog();
			if(Directory.Exists(PrefB.GetString("ExportPath"))) {
				Dlg.InitialDirectory=PrefB.GetString("ExportPath");
			}
			else if(Directory.Exists("C:\\")) {
				Dlg.InitialDirectory="C:\\";
			}
			if(Dlg.ShowDialog()!=DialogResult.OK) {
				Cursor=Cursors.Default;
				return;
			}
			if(!File.Exists(Dlg.FileName)){
				Cursor=Cursors.Default;
				MsgBox.Show(this,"File not found");
				return;
			}
			string[] fields;
			double fee;
			int schedI=DefB.GetOrder(DefCat.FeeSchedNames,SchedNum);
			using(StreamReader sr=new StreamReader(Dlg.FileName)){
				string line=sr.ReadLine();
				while(line!=null){
					fields=line.Split(new string[1] {"\t"},StringSplitOptions.None);
					if(fields.Length>1 && fields[1]!=""){//skips blank fees
						fee=PIn.PDouble(fields[1]);
						Fees.Import(fields[0],fee,schedI);
					}
					line=sr.ReadLine();
				}
			}
			Cursor=Cursors.Default;
			DialogResult=DialogResult.OK;
		}

		private void butUpdate_Click(object sender,EventArgs e) {
			if(!MsgBox.Show(this,true,"All treatment planned procedures for all patients will be updated.  Only the fee will be updated, not the insurance estimate.  It might take a few minutes.  Continue?")) {
				return;
			}
			Cursor=Cursors.WaitCursor;
			int rowsChanged=Procedures.GlobalUpdateFees();
			Cursor=Cursors.Default;
			MessageBox.Show(Lan.g(this,"Fees changed: ")+rowsChanged.ToString());
			DialogResult=DialogResult.OK;
		}

		private void butIns_Click(object sender,EventArgs e) {
			FormFeesForIns FormF=new FormFeesForIns();
			FormF.ShowDialog();
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		

		

		

		


		

	}
}





















