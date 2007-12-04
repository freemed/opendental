using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using OpenDentBusiness;
using System.IO;
using System.Text;
using System.Diagnostics;
using CodeBase;

namespace OpenDental{
	/// <summary>
	/// Summary description for FormBasicTemplate.
	/// </summary>
	public class FormCommunications:System.Windows.Forms.Form {
		private OpenDental.UI.Button butQuest;
		private IContainer components;
		//public Patient PatCur;
		public bool ViewingInRecall;
		private ToolTip toolTip1;
		///<summary>Can be null.</summary>
		//private Referral ReferralCur;

		///<summary></summary>
		public FormCommunications()
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormCommunications));
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.butQuest = new OpenDental.UI.Button();
			this.SuspendLayout();
			// 
			// butQuest
			// 
			this.butQuest.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butQuest.Autosize = true;
			this.butQuest.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butQuest.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butQuest.CornerRadius = 4F;
			this.butQuest.Location = new System.Drawing.Point(20,211);
			this.butQuest.Name = "butQuest";
			this.butQuest.Size = new System.Drawing.Size(90,25);
			this.butQuest.TabIndex = 90;
			this.butQuest.Text = "Questionnaire";
			this.butQuest.Click += new System.EventHandler(this.butQuest_Click);
			// 
			// FormCommunications
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.ClientSize = new System.Drawing.Size(332,267);
			this.Controls.Add(this.butQuest);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormCommunications";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Communications";
			this.Load += new System.EventHandler(this.FormCommunications_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormCommunications_Load(object sender,EventArgs e) {
			//ReferralCur=Referrals.GetReferralForPat(PatCur.PatNum);
			//if(ReferralCur==null){
				//butLetterSimpleRef.Enabled=false;
				//butLabelRef.Enabled=false;
				//butEmailRef.Enabled=false;
			//}
			//else{
			//	textReferral.Text=Referrals.GetNameFL(ReferralCur.ReferralNum);
				//if(ReferralCur.EMail==""){
				//	butEmailRef.Enabled=false;
				//}
			//}
			//if(PatCur.Email=="") {
			//	butEmail.Enabled=false;
			//}
		}

		private void butQuest_Click(object sender,EventArgs e) {
			/*
			FormPat form=new FormPat();
			form.PatNum=PatCur.PatNum;
			form.FormDateTime=DateTime.Now;
			FormFormPatEdit FormP=new FormFormPatEdit();
			FormP.FormPatCur=form;
			FormP.IsNew=true;
			FormP.ShowDialog();
			if(FormP.DialogResult==DialogResult.OK) {
				DialogResult=DialogResult.OK;
			}*/
		}

		public static void PrintStationery(Patient PatCur){
			if(PrefB.GetString("StationaryDocument")==""){
				MsgBox.Show("Stationery","Please setup your stationery document first in Setup | Misc");
				return;
			}
			StringBuilder str = new StringBuilder();
			str.Append(PatCur.FName + " " + PatCur.MiddleI + " " + PatCur.LName + "\r\n");
			str.Append(PatCur.Address + "\r\n");
			if (PatCur.Address2 != "")
				str.Append(PatCur.Address2 + "\r\n");
			str.Append(PatCur.City + ", " + PatCur.State + "  " + PatCur.Zip);
			str.Append("\r\n\r\n\r\n\r\n");
			//date
			str.Append(DateTime.Today.ToLongDateString() + "\r\n\r\n");
			//greeting
			str.Append("Dear ");
			if (PatCur.Salutation != "")
				str.Append(PatCur.Salutation);
			else if (PatCur.Preferred != "")
				str.Append(PatCur.Preferred);
			else
				str.Append(PatCur.FName);
			str.Append(",\r\n\r\n");
			//closing
			str.Append("\r\n\r\nSincerely,\r\n\r\n\r\n\r\n");
			//Clipboard.SetDataObject(textQuery.Text);
			//str.CopyTo;
			//string FinalString = str.CopyTo;
			//textTest.Text = str.ToString();
			Clipboard.SetDataObject(str.ToString());
			try {
				string patFolder=ODFileUtils.CombinePaths(
					FormPath.GetPreferredImagePath(),
					PatCur.ImageFolder.Substring(0, 1),
					PatCur.ImageFolder);
				//string ProgName = @"C:\Program Files\OpenOffice.org 2.0\program\swriter.exe";
				//string ProgName = PrefB.GetString("WordProcessorPath");
				string TheFile=ODFileUtils.CombinePaths(patFolder,"Letter_"+DateTime.Now.ToFileTime()+".doc");
				try{
					File.Copy(ODFileUtils.CombinePaths(FormPath.GetPreferredImagePath(),PrefB.GetString("StationaryDocument")),TheFile);
				}
				catch {
				}
				try {
					Process.Start(TheFile);
				}
				catch {
				}
				Commlog CommlogCur = new Commlog();
				CommlogCur.CommDateTime = DateTime.Now;
				CommlogCur.CommType = Commlogs.GetTypeAuto(CommItemTypeAuto.MISC);
				CommlogCur.PatNum = PatCur.PatNum;
				CommlogCur.Note = Lan.g("Stationery","Letter sent: See Images for this date.");
				Commlogs.Insert(CommlogCur);
			}
			catch{
				MsgBox.Show("Stationery", "Cannot find stationery document. Or another problem exists.");
			}
		}

		private void butStationary_Click(object sender,EventArgs e) {

		}

		

		
		


	}
}





















