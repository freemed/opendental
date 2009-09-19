using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using OpenDental.UI;
using OpenDentBusiness;

namespace OpenDental.ReportingOld2{
	/// <summary>
	/// Summary description for FormBasicTemplate.
	/// </summary>
	public class FormParameterInput : System.Windows.Forms.Form{
		private OpenDental.UI.Button butCancel;
		private OpenDental.UI.Button butOK;
		private OpenDental.UI.ContrMultInput MultInput2;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		///<summary></summary>
		public FormParameterInput()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			Lan.C("All", new System.Windows.Forms.Control[] {
				butOK,
				butCancel,
			});
			//MultInput2.MultInputItems=new MultInputItemCollection();
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormParameterInput));
			this.butCancel = new OpenDental.UI.Button();
			this.butOK = new OpenDental.UI.Button();
			this.MultInput2 = new OpenDental.UI.ContrMultInput();
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
			this.butCancel.Location = new System.Drawing.Point(597,237);
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
			this.butOK.Location = new System.Drawing.Point(507,237);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75,26);
			this.butOK.TabIndex = 1;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// MultInput2
			// 
			this.MultInput2.Location = new System.Drawing.Point(12,10);
			this.MultInput2.Name = "MultInput2";
			this.MultInput2.Size = new System.Drawing.Size(660,204);
			this.MultInput2.TabIndex = 2;
			this.MultInput2.SizeChanged += new System.EventHandler(this.MultInput2_SizeChanged);
			// 
			// FormParameterInput
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.CancelButton = this.butCancel;
			this.ClientSize = new System.Drawing.Size(700,277);
			this.Controls.Add(this.MultInput2);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.butCancel);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormParameterInput";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Enter Parameters";
			this.Load += new System.EventHandler(this.FormParameterInput_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormParameterInput_Load(object sender, System.EventArgs e) {
			/*
			MultInput2.AddInputItem("You should put in some text in the box to the right",ParameterValueKind.StringParameter,"");
			MultInput2.AddInputItem("Prompt",ParameterValueKind.StringParameter,"");
			MultInput2.AddInputItem("Yet another prompt for info",ParameterValueKind.StringParameter,"");
			MultInput2.AddInputItem("Testing checkbox",ParameterValueKind.BooleanParameter,false);
			MultInput2.AddInputItem("Testing Date",ParameterValueKind.DateParameter,DateTime.Today);
			MultInput2.AddInputItem("Testing Number",ParameterValueKind.NumberParameter,(double)0);
			MultInput2.AddInputItem("Select Account Colors",ParameterValueKind.DefParameter,null,DefCat.AccountColors);
			MultInput2.AddInputItem("Select Dental Specialty",ParameterValueKind.EnumParameter,null,EnumType.DentalSpecialty);*/
		}

		///<summary></summary>
		public void AddInputItem(string myPromptingText,FieldValueType myValueType,ArrayList myCurrentValues,EnumType myEnumerationType,DefCat myDefCategory,ReportFKType myFKType){
			MultInput2.AddInputItem(myPromptingText,myValueType,myCurrentValues,myEnumerationType,myDefCategory,myFKType);
		}

		///<summary>After this form closes, use this method to retrieve the data that the user entered.</summary>
		public ArrayList GetCurrentValues(int itemIndex){
			return MultInput2.GetCurrentValues(itemIndex);
		}

		private void MultInput2_SizeChanged(object sender, System.EventArgs e) {
			Height=MultInput2.Bottom+90;
			Refresh();//this should trigger another layout
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			//MessageBox.Show(MultInput2.MultInputItems[0].CurrentValue.ToString());
			if(!MultInput2.AllFieldsAreValid()){
				MessageBox.Show(Lan.g(this,"Please fix data entry errors first."));
				return;
			}
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			//comboBox1.DroppedDown
			DialogResult=DialogResult.Cancel;
		}

	

		

		


	}
}





















