using System;
using System.Drawing;
using System.Drawing.Printing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using OpenDental.ReportingOld2;
using OpenDentBusiness;

namespace OpenDental
{
	/// <summary>
	/// Summary description for FormRpApptWithPhones.
	/// </summary>
	public class FormRpRouting : System.Windows.Forms.Form
	{
		private OpenDental.UI.Button butAll;
		private System.Windows.Forms.ListBox listProv;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private OpenDental.ValidDate textDate;
		private OpenDental.UI.Button butCancel;
		private OpenDental.UI.Button butOK;
		private OpenDental.UI.Button butDisplayed;
		private OpenDental.UI.Button butToday;
		//<summary>This list of appointments gets filled.  Each appointment will result in one page when printing.</summary>
		//private List<Appointment> Appts;
		//private int pagesPrinted;
		//private PrintDocument pd;
		//private OpenDental.UI.PrintPreview printPreview;
		///<summary>The date that the user selected.</summary>
		private DateTime date;
		///<summary>If set externally beforehand, then the user will not see any choices, and only a routing slip for the one appt will print.</summary>
		public long AptNum;
		/// <summary>If ApptNum is set, then this should be set also.</summary>
		public long SheetDefNum;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		///<summary></summary>
		public FormRpRouting()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			Lan.F(this);
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormRpRouting));
			this.butAll = new OpenDental.UI.Button();
			this.listProv = new System.Windows.Forms.ListBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.textDate = new OpenDental.ValidDate();
			this.butCancel = new OpenDental.UI.Button();
			this.butOK = new OpenDental.UI.Button();
			this.butToday = new OpenDental.UI.Button();
			this.butDisplayed = new OpenDental.UI.Button();
			this.SuspendLayout();
			// 
			// butAll
			// 
			this.butAll.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butAll.Autosize = true;
			this.butAll.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butAll.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butAll.CornerRadius = 4F;
			this.butAll.Location = new System.Drawing.Point(28,243);
			this.butAll.Name = "butAll";
			this.butAll.Size = new System.Drawing.Size(75,26);
			this.butAll.TabIndex = 34;
			this.butAll.Text = "&All";
			this.butAll.Click += new System.EventHandler(this.butAll_Click);
			// 
			// listProv
			// 
			this.listProv.Location = new System.Drawing.Point(27,41);
			this.listProv.Name = "listProv";
			this.listProv.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			this.listProv.Size = new System.Drawing.Size(120,186);
			this.listProv.TabIndex = 33;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(27,18);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(104,16);
			this.label1.TabIndex = 32;
			this.label1.Text = "Providers";
			this.label1.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(183,43);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(82,18);
			this.label2.TabIndex = 37;
			this.label2.Text = "Date";
			this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textDate
			// 
			this.textDate.Location = new System.Drawing.Point(269,41);
			this.textDate.Name = "textDate";
			this.textDate.Size = new System.Drawing.Size(100,20);
			this.textDate.TabIndex = 43;
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
			this.butCancel.Location = new System.Drawing.Point(447,244);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75,26);
			this.butCancel.TabIndex = 44;
			this.butCancel.Text = "&Cancel";
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
			this.butOK.Location = new System.Drawing.Point(447,204);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75,26);
			this.butOK.TabIndex = 43;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// butToday
			// 
			this.butToday.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butToday.Autosize = true;
			this.butToday.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butToday.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butToday.CornerRadius = 4F;
			this.butToday.Location = new System.Drawing.Point(427,40);
			this.butToday.Name = "butToday";
			this.butToday.Size = new System.Drawing.Size(96,23);
			this.butToday.TabIndex = 46;
			this.butToday.Text = "Today";
			this.butToday.Click += new System.EventHandler(this.butToday_Click);
			// 
			// butDisplayed
			// 
			this.butDisplayed.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butDisplayed.Autosize = true;
			this.butDisplayed.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butDisplayed.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butDisplayed.CornerRadius = 4F;
			this.butDisplayed.Location = new System.Drawing.Point(427,67);
			this.butDisplayed.Name = "butDisplayed";
			this.butDisplayed.Size = new System.Drawing.Size(96,23);
			this.butDisplayed.TabIndex = 45;
			this.butDisplayed.Text = "Displayed";
			this.butDisplayed.Click += new System.EventHandler(this.butDisplayed_Click);
			// 
			// FormRpRouting
			// 
			this.AcceptButton = this.butOK;
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.CancelButton = this.butCancel;
			this.ClientSize = new System.Drawing.Size(562,292);
			this.Controls.Add(this.butToday);
			this.Controls.Add(this.butDisplayed);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.textDate);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.butAll);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.listProv);
			this.Controls.Add(this.label1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "FormRpRouting";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Routing Slips";
			this.Load += new System.EventHandler(this.FormRpRouting_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		private void FormRpRouting_Load(object sender, System.EventArgs e){
			if(AptNum!=0){
				/*
				SheetDef sheetDef=null;
				if(SheetDefNum==0) {
					sheetDef=SheetsInternal.GetSheetDef(SheetInternalType.RoutingSlip);
				}
				else {
					sheetDef=SheetDefs.GetSheetDef(SheetDefNum);
				}
				Sheet sheet=SheetUtil.CreateSheet(sheetDef);
				SheetParameter.SetParameter(sheet,"AptNum",AptNum);
				SheetFiller.FillFields(sheet);
				using(Graphics g=this.CreateGraphics()) {
					SheetUtil.CalculateHeights(sheet,g);
				}
				FormSheetFillEdit FormS=new FormSheetFillEdit(sheet);
				FormS.ShowDialog();*/

				
				List<long> aptNums=new List<long>();
				aptNums.Add(AptNum);
				PrintRoutingSlips(aptNums,SheetDefNum);
				DialogResult=DialogResult.OK;
				return;
			}
			for(int i=0;i<ProviderC.List.Length;i++){
				listProv.Items.Add(ProviderC.List[i].GetLongDesc());
				listProv.SetSelected(i,true);
			}
			textDate.Text=DateTime.Today.ToShortDateString();
		}

		private void butToday_Click(object sender, System.EventArgs e) {
			textDate.Text=DateTime.Today.ToShortDateString();
		}

		private void butDisplayed_Click(object sender, System.EventArgs e) {
			textDate.Text=AppointmentL.DateSelected.ToShortDateString();
		}

		private void butAll_Click(object sender, System.EventArgs e){
			for(int i=0;i<listProv.Items.Count;i++){
				listProv.SetSelected(i,true);
			}
		}

		private void butOK_Click(object sender, System.EventArgs e){
			//validate user input
			if(textDate.errorProvider1.GetError(textDate) != "")	{
				MsgBox.Show(this,"Please fix data entry errors first.");
				return;
			}
			if(textDate.Text.Length==0){
				MessageBox.Show(Lan.g(this,"Date is required."));
				return;
			}
			date=PIn.Date(textDate.Text);
			if(listProv.SelectedIndices.Count==0){
				MessageBox.Show(Lan.g(this,"You must select at least one provider."));
				return;
			}
			List<long> provNums=new List<long>();
			for(int i=0;i<listProv.SelectedIndices.Count;i++) {
				provNums.Add(ProviderC.List[listProv.SelectedIndices[i]].ProvNum);
			}
			List<long> aptNums=Appointments.GetRouting(date,provNums);
			SheetDef sheetDef;
			List<SheetDef> customSheetDefs=SheetDefs.GetCustomForType(SheetTypeEnum.RoutingSlip);
			if(customSheetDefs.Count==0){
				sheetDef=SheetsInternal.GetSheetDef(SheetInternalType.RoutingSlip);
			}
			else{
				sheetDef=customSheetDefs[0];//Instead of doing this, we could give the user a list to pick from on this form.
				//SheetDefs.GetFieldsAndParameters(sheetDef);
			}
			PrintRoutingSlips(aptNums,sheetDef.SheetDefNum);
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, EventArgs e){
			DialogResult=DialogResult.Cancel;
		}

		/// <summary>Specify a sheetDefNum of 0 for the internal Routing slip.</summary>
		private void PrintRoutingSlips(List<long> aptNums,long sheetDefNum) {
			SheetDef sheetDef;
			if(sheetDefNum==0){
				sheetDef=SheetsInternal.GetSheetDef(SheetInternalType.RoutingSlip);
			}
			else{
				sheetDef=SheetDefs.GetSheetDef(sheetDefNum);//includes fields and parameters
			}
			List<Sheet> sheetBatch=SheetUtil.CreateBatch(sheetDef,aptNums);
			try {
				SheetPrinting.PrintBatch(sheetBatch);
			}
			catch(Exception ex) {
				MessageBox.Show(ex.Message);
			}
		}

	



		

		
	}
}
