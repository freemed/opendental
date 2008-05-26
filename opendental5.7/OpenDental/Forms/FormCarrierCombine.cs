using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{
	/// <summary>
	/// Summary description for FormBasicTemplate.
	/// </summary>
	public class FormCarrierCombine : System.Windows.Forms.Form{
		private OpenDental.UI.Button butCancel;
		private OpenDental.UI.Button butOK;
		private OpenDental.Forms.TableCarriers tbCarriers;
		private System.Windows.Forms.Label label1;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		///<summary>After this window closes, this will be the carrierNum of the selected carrier.</summary>
		public int PickedCarrierNum;
		///<summary>Before opening this Form, set the carrierNums to show.</summary>
		public int[] CarrierNums;
		private List<Carrier> carrierList;

		///<summary></summary>
		public FormCarrierCombine()
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormCarrierCombine));
			this.butCancel = new OpenDental.UI.Button();
			this.butOK = new OpenDental.UI.Button();
			this.tbCarriers = new OpenDental.Forms.TableCarriers();
			this.label1 = new System.Windows.Forms.Label();
			this.SuspendLayout();
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
			this.butCancel.Location = new System.Drawing.Point(773,465);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75,26);
			this.butCancel.TabIndex = 0;
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
			this.butOK.Location = new System.Drawing.Point(773,424);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75,26);
			this.butOK.TabIndex = 1;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// tbCarriers
			// 
			this.tbCarriers.BackColor = System.Drawing.SystemColors.Window;
			this.tbCarriers.Location = new System.Drawing.Point(9,42);
			this.tbCarriers.Name = "tbCarriers";
			this.tbCarriers.ScrollValue = 363;
			this.tbCarriers.SelectedIndices = new int[0];
			this.tbCarriers.SelectionMode = System.Windows.Forms.SelectionMode.One;
			this.tbCarriers.Size = new System.Drawing.Size(839,356);
			this.tbCarriers.TabIndex = 2;
			this.tbCarriers.CellDoubleClicked += new OpenDental.ContrTable.CellEventHandler(this.tbCarriers_CellDoubleClicked);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(13,10);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(476,23);
			this.label1.TabIndex = 3;
			this.label1.Text = "Please select the carrier to keep when combining";
			this.label1.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// FormCarrierCombine
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.ClientSize = new System.Drawing.Size(880,511);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.tbCarriers);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.butCancel);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormCarrierCombine";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Combine Carriers";
			this.Load += new System.EventHandler(this.FormCarrierCombine_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormCarrierCombine_Load(object sender, System.EventArgs e) {
			FillGrid();
		}

		private void FillGrid(){
			carrierList=Carriers.GetCarriers(CarrierNums);
			tbCarriers.ResetRows(carrierList.Count);
			tbCarriers.SetGridColor(Color.Gray);
			tbCarriers.SetBackGColor(Color.White);
			for(int i=0;i<carrierList.Count;i++){
				tbCarriers.Cell[0,i]=carrierList[i].CarrierName;
				tbCarriers.Cell[1,i]=carrierList[i].Phone;
				tbCarriers.Cell[2,i]=carrierList[i].Address;
				tbCarriers.Cell[3,i]=carrierList[i].Address2;
				tbCarriers.Cell[4,i]=carrierList[i].City;
				tbCarriers.Cell[5,i]=carrierList[i].State;
				tbCarriers.Cell[6,i]=carrierList[i].Zip;
				tbCarriers.Cell[7,i]=carrierList[i].ElectID;
			}
			tbCarriers.LayoutTables();
		}

		private void tbCarriers_CellDoubleClicked(object sender, OpenDental.CellEventArgs e) {
			PickedCarrierNum=carrierList[e.Row].CarrierNum;
			DialogResult=DialogResult.OK;
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			if(tbCarriers.SelectedRow==-1){
				MessageBox.Show(Lan.g(this,"Please select an item first."));
				return;
			}
			PickedCarrierNum=carrierList[tbCarriers.SelectedRow].CarrierNum;
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		

		

		


	}
}





















