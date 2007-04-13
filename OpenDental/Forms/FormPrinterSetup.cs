using System;
using System.Drawing;
using System.Drawing.Printing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{
///<summary></summary>
	public class FormPrinterSetup : System.Windows.Forms.Form{
		private System.Windows.Forms.Label label1;
		private OpenDental.UI.Button butOK;
		private OpenDental.UI.Button butCancel;
		private System.Windows.Forms.ComboBox comboDefault;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.ComboBox comboLabelSheet;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.ComboBox comboLabelSingle;
		private System.Windows.Forms.ComboBox comboPostcard;
		private System.Windows.Forms.ComboBox comboClaim;
		private System.Windows.Forms.ComboBox comboRx;
		private System.Windows.Forms.ComboBox comboStatement;
		private System.Windows.Forms.ComboBox comboTPPerio;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.CheckBox checkDefault;
		private System.Windows.Forms.CheckBox checkClaim;
		private System.Windows.Forms.CheckBox checkLabelSheet;
		private System.Windows.Forms.CheckBox checkLabelSingle;
		private System.Windows.Forms.CheckBox checkPostcard;
		private System.Windows.Forms.CheckBox checkRx;
		private System.Windows.Forms.CheckBox checkTPPerio;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.CheckBox checkAppointments;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.ComboBox comboAppointments;
		private System.Windows.Forms.CheckBox checkStatement;
		private System.Windows.Forms.Panel panelSimple;
		private System.Windows.Forms.CheckBox checkSimple;
		private System.ComponentModel.Container components = null;

		///<summary></summary>
		public FormPrinterSetup(){
			InitializeComponent();
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormPrinterSetup));
			this.label1 = new System.Windows.Forms.Label();
			this.butOK = new OpenDental.UI.Button();
			this.butCancel = new OpenDental.UI.Button();
			this.comboDefault = new System.Windows.Forms.ComboBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.comboLabelSheet = new System.Windows.Forms.ComboBox();
			this.label4 = new System.Windows.Forms.Label();
			this.comboClaim = new System.Windows.Forms.ComboBox();
			this.label5 = new System.Windows.Forms.Label();
			this.comboRx = new System.Windows.Forms.ComboBox();
			this.label6 = new System.Windows.Forms.Label();
			this.comboTPPerio = new System.Windows.Forms.ComboBox();
			this.label7 = new System.Windows.Forms.Label();
			this.comboStatement = new System.Windows.Forms.ComboBox();
			this.label8 = new System.Windows.Forms.Label();
			this.comboLabelSingle = new System.Windows.Forms.ComboBox();
			this.label9 = new System.Windows.Forms.Label();
			this.comboPostcard = new System.Windows.Forms.ComboBox();
			this.label10 = new System.Windows.Forms.Label();
			this.checkDefault = new System.Windows.Forms.CheckBox();
			this.checkClaim = new System.Windows.Forms.CheckBox();
			this.checkLabelSheet = new System.Windows.Forms.CheckBox();
			this.checkLabelSingle = new System.Windows.Forms.CheckBox();
			this.checkPostcard = new System.Windows.Forms.CheckBox();
			this.checkRx = new System.Windows.Forms.CheckBox();
			this.checkTPPerio = new System.Windows.Forms.CheckBox();
			this.label11 = new System.Windows.Forms.Label();
			this.checkAppointments = new System.Windows.Forms.CheckBox();
			this.label12 = new System.Windows.Forms.Label();
			this.comboAppointments = new System.Windows.Forms.ComboBox();
			this.checkStatement = new System.Windows.Forms.CheckBox();
			this.checkSimple = new System.Windows.Forms.CheckBox();
			this.panelSimple = new System.Windows.Forms.Panel();
			this.panelSimple.SuspendLayout();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(19,6);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(438,18);
			this.label1.TabIndex = 2;
			this.label1.Text = "These settings only apply to this workstation";
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.CornerRadius = 4F;
			this.butOK.Location = new System.Drawing.Point(550,270);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75,26);
			this.butOK.TabIndex = 2;
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
			this.butCancel.Location = new System.Drawing.Point(550,304);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75,26);
			this.butCancel.TabIndex = 3;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// comboDefault
			// 
			this.comboDefault.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboDefault.Location = new System.Drawing.Point(208,38);
			this.comboDefault.MaxDropDownItems = 30;
			this.comboDefault.Name = "comboDefault";
			this.comboDefault.Size = new System.Drawing.Size(253,21);
			this.comboDefault.TabIndex = 4;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(4,34);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(195,19);
			this.label2.TabIndex = 5;
			this.label2.Text = "Default";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(3,217);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(195,19);
			this.label3.TabIndex = 7;
			this.label3.Text = "Statements";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// comboLabelSheet
			// 
			this.comboLabelSheet.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboLabelSheet.Location = new System.Drawing.Point(202,111);
			this.comboLabelSheet.MaxDropDownItems = 30;
			this.comboLabelSheet.Name = "comboLabelSheet";
			this.comboLabelSheet.Size = new System.Drawing.Size(253,21);
			this.comboLabelSheet.TabIndex = 6;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(4,87);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(195,19);
			this.label4.TabIndex = 9;
			this.label4.Text = "Claims";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// comboClaim
			// 
			this.comboClaim.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboClaim.Location = new System.Drawing.Point(202,85);
			this.comboClaim.MaxDropDownItems = 30;
			this.comboClaim.Name = "comboClaim";
			this.comboClaim.Size = new System.Drawing.Size(253,21);
			this.comboClaim.TabIndex = 8;
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(4,113);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(195,19);
			this.label5.TabIndex = 11;
			this.label5.Text = "Labels - Sheet";
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// comboRx
			// 
			this.comboRx.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboRx.Location = new System.Drawing.Point(202,190);
			this.comboRx.MaxDropDownItems = 30;
			this.comboRx.Name = "comboRx";
			this.comboRx.Size = new System.Drawing.Size(253,21);
			this.comboRx.TabIndex = 10;
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(4,192);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(195,19);
			this.label6.TabIndex = 13;
			this.label6.Text = "Rx\'s";
			this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// comboTPPerio
			// 
			this.comboTPPerio.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboTPPerio.Location = new System.Drawing.Point(202,242);
			this.comboTPPerio.MaxDropDownItems = 30;
			this.comboTPPerio.Name = "comboTPPerio";
			this.comboTPPerio.Size = new System.Drawing.Size(253,21);
			this.comboTPPerio.TabIndex = 12;
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(4,243);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(195,19);
			this.label7.TabIndex = 15;
			this.label7.Text = "Treatment Plans and Perio";
			this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// comboStatement
			// 
			this.comboStatement.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboStatement.Location = new System.Drawing.Point(202,216);
			this.comboStatement.MaxDropDownItems = 30;
			this.comboStatement.Name = "comboStatement";
			this.comboStatement.Size = new System.Drawing.Size(253,21);
			this.comboStatement.TabIndex = 14;
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(4,139);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(195,19);
			this.label8.TabIndex = 17;
			this.label8.Text = "Labels - Single";
			this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// comboLabelSingle
			// 
			this.comboLabelSingle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboLabelSingle.Location = new System.Drawing.Point(202,137);
			this.comboLabelSingle.MaxDropDownItems = 30;
			this.comboLabelSingle.Name = "comboLabelSingle";
			this.comboLabelSingle.Size = new System.Drawing.Size(253,21);
			this.comboLabelSingle.TabIndex = 16;
			// 
			// label9
			// 
			this.label9.Location = new System.Drawing.Point(4,165);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(195,19);
			this.label9.TabIndex = 19;
			this.label9.Text = "Postcards";
			this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// comboPostcard
			// 
			this.comboPostcard.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboPostcard.Location = new System.Drawing.Point(202,163);
			this.comboPostcard.MaxDropDownItems = 30;
			this.comboPostcard.Name = "comboPostcard";
			this.comboPostcard.Size = new System.Drawing.Size(253,21);
			this.comboPostcard.TabIndex = 18;
			// 
			// label10
			// 
			this.label10.Location = new System.Drawing.Point(457,5);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(116,23);
			this.label10.TabIndex = 20;
			this.label10.Text = "Prompt";
			this.label10.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// checkDefault
			// 
			this.checkDefault.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkDefault.Location = new System.Drawing.Point(467,37);
			this.checkDefault.Name = "checkDefault";
			this.checkDefault.Size = new System.Drawing.Size(24,15);
			this.checkDefault.TabIndex = 21;
			// 
			// checkClaim
			// 
			this.checkClaim.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkClaim.Location = new System.Drawing.Point(467,89);
			this.checkClaim.Name = "checkClaim";
			this.checkClaim.Size = new System.Drawing.Size(24,15);
			this.checkClaim.TabIndex = 22;
			// 
			// checkLabelSheet
			// 
			this.checkLabelSheet.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkLabelSheet.Location = new System.Drawing.Point(467,115);
			this.checkLabelSheet.Name = "checkLabelSheet";
			this.checkLabelSheet.Size = new System.Drawing.Size(24,15);
			this.checkLabelSheet.TabIndex = 23;
			// 
			// checkLabelSingle
			// 
			this.checkLabelSingle.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkLabelSingle.Location = new System.Drawing.Point(467,141);
			this.checkLabelSingle.Name = "checkLabelSingle";
			this.checkLabelSingle.Size = new System.Drawing.Size(24,15);
			this.checkLabelSingle.TabIndex = 24;
			// 
			// checkPostcard
			// 
			this.checkPostcard.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkPostcard.Location = new System.Drawing.Point(467,167);
			this.checkPostcard.Name = "checkPostcard";
			this.checkPostcard.Size = new System.Drawing.Size(24,15);
			this.checkPostcard.TabIndex = 25;
			// 
			// checkRx
			// 
			this.checkRx.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkRx.Location = new System.Drawing.Point(467,193);
			this.checkRx.Name = "checkRx";
			this.checkRx.Size = new System.Drawing.Size(24,15);
			this.checkRx.TabIndex = 26;
			// 
			// checkTPPerio
			// 
			this.checkTPPerio.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkTPPerio.Location = new System.Drawing.Point(467,245);
			this.checkTPPerio.Name = "checkTPPerio";
			this.checkTPPerio.Size = new System.Drawing.Size(24,15);
			this.checkTPPerio.TabIndex = 27;
			// 
			// label11
			// 
			this.label11.Location = new System.Drawing.Point(512,53);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(100,126);
			this.label11.TabIndex = 28;
			this.label11.Text = "It is recommended to use the prompt option for most functions.  But if you are us" +
    "ing a default, it is not necessary to check the box.";
			// 
			// checkAppointments
			// 
			this.checkAppointments.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkAppointments.Location = new System.Drawing.Point(467,63);
			this.checkAppointments.Name = "checkAppointments";
			this.checkAppointments.Size = new System.Drawing.Size(24,15);
			this.checkAppointments.TabIndex = 31;
			// 
			// label12
			// 
			this.label12.Location = new System.Drawing.Point(4,61);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(195,19);
			this.label12.TabIndex = 30;
			this.label12.Text = "Appointments";
			this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// comboAppointments
			// 
			this.comboAppointments.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboAppointments.Location = new System.Drawing.Point(202,59);
			this.comboAppointments.MaxDropDownItems = 30;
			this.comboAppointments.Name = "comboAppointments";
			this.comboAppointments.Size = new System.Drawing.Size(253,21);
			this.comboAppointments.TabIndex = 29;
			// 
			// checkStatement
			// 
			this.checkStatement.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkStatement.Location = new System.Drawing.Point(467,219);
			this.checkStatement.Name = "checkStatement";
			this.checkStatement.Size = new System.Drawing.Size(24,15);
			this.checkStatement.TabIndex = 32;
			// 
			// checkSimple
			// 
			this.checkSimple.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkSimple.Location = new System.Drawing.Point(49,309);
			this.checkSimple.Name = "checkSimple";
			this.checkSimple.Size = new System.Drawing.Size(440,24);
			this.checkSimple.TabIndex = 33;
			this.checkSimple.Text = "This is too complicated.  Show me the simple interface.";
			this.checkSimple.Click += new System.EventHandler(this.checkSimple_Click);
			// 
			// panelSimple
			// 
			this.panelSimple.Controls.Add(this.comboTPPerio);
			this.panelSimple.Controls.Add(this.label3);
			this.panelSimple.Controls.Add(this.comboLabelSingle);
			this.panelSimple.Controls.Add(this.label6);
			this.panelSimple.Controls.Add(this.comboPostcard);
			this.panelSimple.Controls.Add(this.label7);
			this.panelSimple.Controls.Add(this.label10);
			this.panelSimple.Controls.Add(this.label8);
			this.panelSimple.Controls.Add(this.checkDefault);
			this.panelSimple.Controls.Add(this.checkClaim);
			this.panelSimple.Controls.Add(this.checkStatement);
			this.panelSimple.Controls.Add(this.label9);
			this.panelSimple.Controls.Add(this.comboAppointments);
			this.panelSimple.Controls.Add(this.label12);
			this.panelSimple.Controls.Add(this.checkAppointments);
			this.panelSimple.Controls.Add(this.label11);
			this.panelSimple.Controls.Add(this.checkTPPerio);
			this.panelSimple.Controls.Add(this.checkRx);
			this.panelSimple.Controls.Add(this.checkPostcard);
			this.panelSimple.Controls.Add(this.checkLabelSingle);
			this.panelSimple.Controls.Add(this.checkLabelSheet);
			this.panelSimple.Controls.Add(this.comboStatement);
			this.panelSimple.Controls.Add(this.comboRx);
			this.panelSimple.Controls.Add(this.label5);
			this.panelSimple.Controls.Add(this.comboClaim);
			this.panelSimple.Controls.Add(this.label4);
			this.panelSimple.Controls.Add(this.comboLabelSheet);
			this.panelSimple.Controls.Add(this.label2);
			this.panelSimple.Location = new System.Drawing.Point(6,5);
			this.panelSimple.Name = "panelSimple";
			this.panelSimple.Size = new System.Drawing.Size(620,269);
			this.panelSimple.TabIndex = 34;
			// 
			// FormPrinterSetup
			// 
			this.AcceptButton = this.butOK;
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.CancelButton = this.butCancel;
			this.ClientSize = new System.Drawing.Size(654,346);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.comboDefault);
			this.Controls.Add(this.panelSimple);
			this.Controls.Add(this.checkSimple);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormPrinterSetup";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Printer Setup";
			this.Load += new System.EventHandler(this.FormPrinterSetup_Load);
			this.panelSimple.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormPrinterSetup_Load(object sender, System.EventArgs e) {
			checkSimple.Checked=PrefB.GetBool("EasyHidePrinters");
			SetSimple();
			SetControls(this);
		}

		///<summary>recursive</summary>
		private void SetControls(Control c){
			foreach(Control control in c.Controls){
				SetControls(control);
				if(control==checkSimple){
					continue;
				}
				if(control.GetType()==typeof(ComboBox)){
					FillCombo((ComboBox)control);
				}
				if(control.GetType()==typeof(CheckBox)){
					FillCheck((CheckBox)control);
				}
			}
		}

		private void FillCombo(ComboBox combo){
			PrintSituation sit=GetSit(combo);
			Printer printerForSit=Printers.GetForSit(sit);
			string printerName="";
			if(printerForSit!=null){
				printerName=printerForSit.PrinterName;
			}
			combo.Items.Clear();
			if(combo==comboDefault){
				combo.Items.Add(Lan.g(this,"Windows default"));
			}
			else{
				combo.Items.Add(Lan.g(this,"default"));
			}
			for(int i=0;i<PrinterSettings.InstalledPrinters.Count;i++){
				combo.Items.Add(PrinterSettings.InstalledPrinters[i]);
				if(printerName==PrinterSettings.InstalledPrinters[i]){
					combo.SelectedIndex=i+1;
				}
      }
			if(combo.SelectedIndex==-1){
				combo.SelectedIndex=0;
			}
		}

		private void FillCheck(CheckBox check){
			PrintSituation sit=GetSit(check);
			Printer printerForSit=Printers.GetForSit(sit);
			if(printerForSit==null){
				check.Checked=false;
				return;
			}
			check.Checked=printerForSit.DisplayPrompt;
		}

		private PrintSituation GetSit(Control contr){
			PrintSituation sit=PrintSituation.Default;
			switch(contr.Name){
				default:
					MessageBox.Show("error. "+contr.Name);
					break;
				case "comboDefault":
				case "checkDefault":
					sit=PrintSituation.Default;
					break;
				case "comboAppointments":
				case "checkAppointments":
					sit=PrintSituation.Appointments;
					break;
				case "comboClaim":
				case "checkClaim":
					sit=PrintSituation.Claim;
					break;
				case "comboLabelSheet":
				case "checkLabelSheet":
					sit=PrintSituation.LabelSheet;
					break;
				case "comboLabelSingle":
				case "checkLabelSingle":
					sit=PrintSituation.LabelSingle;
					break;
				case "comboPostcard":
				case "checkPostcard":
					sit=PrintSituation.Postcard;
					break;
				case "comboRx":
				case "checkRx":
					sit=PrintSituation.Rx;
					break;
				case "comboStatement":
				case "checkStatement":
					sit=PrintSituation.Statement;
					break;
				case "comboTPPerio":
				case "checkTPPerio":
					sit=PrintSituation.TPPerio;
					break;
			}
			return sit;
		}

		///<summary>Sets the simple hide based on the status of the checkbox</summary>
		private void SetSimple(){
			panelSimple.Visible=!checkSimple.Checked;
		}

		private void checkSimple_Click(object sender, System.EventArgs e) {

			SetSimple();
		}

		private void butOK_Click(object sender, System.EventArgs e){
			string compName=SystemInformation.ComputerName;
			if(checkSimple.Checked && !PrefB.GetBool("EasyHidePrinters")){
				//if user clicked the simple option
				if(!MsgBox.Show(this,true,"Warning! You have selected the easy view option.  This will clear all printing preferences for all computers.  Are you sure you wish to continue?")){
					return;
				}
				Printers.ClearAll();
				Printers.Refresh();
				string printerName="";
				if(comboDefault.SelectedIndex==0){
					printerName="";
				}
				else{
					printerName=PrinterSettings.InstalledPrinters[comboDefault.SelectedIndex-1];
				}
				Printers.PutForSit(PrintSituation.Default,compName,printerName,true);
			}
			else for(int i=0;i<Enum.GetValues(typeof(PrintSituation)).Length;i++){
				//loop through each printSituation
				string printerName="";
				bool isChecked=false;
				//PrintSituation sit=PrintSituation.Default;
				//first: main Default, since not in panel Simple
				if(i==0){//printSituation.Default
					if(comboDefault.SelectedIndex==0){
						printerName="";
					}
					else{
						printerName=PrinterSettings.InstalledPrinters[comboDefault.SelectedIndex-1];
					}
				}
				foreach(Control control in panelSimple.Controls){
					if(control.GetType()!=typeof(ComboBox)//skip anything but comboBoxes and CheckBoxes
						&& control.GetType()!=typeof(CheckBox))
					{
						continue;
					}
					//so only two controls out of all will be used in each Enum loop
					if(GetSit(control)!=(PrintSituation)i){
						continue;
					}
					if(control.GetType()==typeof(ComboBox)){
						if(((ComboBox)control).SelectedIndex==0){
							printerName="";
						}
						else{
							printerName=PrinterSettings.InstalledPrinters[((ComboBox)control).SelectedIndex-1];
						}
					}
					else{//checkBox
						isChecked=((CheckBox)control).Checked;
					}
				}
				Printers.PutForSit((PrintSituation)i,compName,printerName,isChecked);
			}
			DataValid.SetInvalid(InvalidTypes.Computers);
			if(checkSimple.Checked!=PrefB.GetBool("EasyHidePrinters")){
				Prefs.UpdateBool("EasyHidePrinters",checkSimple.Checked);
				DataValid.SetInvalid(InvalidTypes.Prefs);
			}
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		

	}
}
