using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Text;
namespace OpenDental
{
	/// <summary>
	/// Summary description for FormSearchPatNotes.
	/// </summary>
	public class FormSearchPatNotes : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label lblSearchPharse;
		private System.Windows.Forms.TextBox textPharse;
		private OpenDental.UI.Button butCancel;
		private OpenDental.UI.Button butOK;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public FormSearchPatNotes()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSearchPatNotes));
            this.lblSearchPharse = new System.Windows.Forms.Label();
            this.textPharse = new System.Windows.Forms.TextBox();
            this.butCancel = new OpenDental.UI.Button();
            this.butOK = new OpenDental.UI.Button();
            this.SuspendLayout();
            // 
            // lblSearchPharse
            // 
            this.lblSearchPharse.AutoSize = true;
            this.lblSearchPharse.Location = new System.Drawing.Point(8, 8);
            this.lblSearchPharse.Name = "lblSearchPharse";
            this.lblSearchPharse.Size = new System.Drawing.Size(142, 13);
            this.lblSearchPharse.TabIndex = 0;
            this.lblSearchPharse.Text = "Enter Search word or phrase";
            // 
            // textPharse
            // 
            this.textPharse.Location = new System.Drawing.Point(8, 24);
            this.textPharse.MaxLength = 40;
            this.textPharse.Multiline = true;
            this.textPharse.Name = "textPharse";
            this.textPharse.Size = new System.Drawing.Size(264, 144);
            this.textPharse.TabIndex = 1;
            this.textPharse.TextChanged += new System.EventHandler(this.textPharse_TextChanged);
            // 
            // butCancel
            // 
            this.butCancel.AdjustImageLocation = new System.Drawing.Point(0, 0);
            this.butCancel.Autosize = true;
            this.butCancel.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
            this.butCancel.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
            this.butCancel.CornerRadius = 4F;
            this.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.butCancel.Location = new System.Drawing.Point(200, 176);
            this.butCancel.Name = "butCancel";
            this.butCancel.Size = new System.Drawing.Size(75, 26);
            this.butCancel.TabIndex = 43;
            this.butCancel.Text = "&Cancel";
            this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
            // 
            // butOK
            // 
            this.butOK.AdjustImageLocation = new System.Drawing.Point(0, 0);
            this.butOK.Autosize = true;
            this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
            this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
            this.butOK.CornerRadius = 4F;
            this.butOK.Enabled = false;
            this.butOK.Location = new System.Drawing.Point(120, 176);
            this.butOK.Name = "butOK";
            this.butOK.Size = new System.Drawing.Size(75, 26);
            this.butOK.TabIndex = 44;
            this.butOK.Text = "&OK";
            this.butOK.Click += new System.EventHandler(this.butOK_Click);
            // 
            // FormSearchPatNotes
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(286, 212);
            this.Controls.Add(this.butOK);
            this.Controls.Add(this.butCancel);
            this.Controls.Add(this.textPharse);
            this.Controls.Add(this.lblSearchPharse);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormSearchPatNotes";
            this.Text = "Search Patient Notes";
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		private void butCancel_Click(object sender, System.EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
		}

		private void butOK_Click(object sender, System.EventArgs e)
		{
			FormQuery FormQuery2;
			string phrase = textPharse.Text.Replace("\t","").Replace("\n","");
			StringBuilder sbSQL = new StringBuilder();
			sbSQL.AppendFormat("SELECT LName,FName,Preferred,PatStatus,Gender,Birthdate,Address,Address2,City,State,zip,HmPhone,Wkphone,",phrase);                                
			sbSQL.AppendFormat("WirelessPhone,Guarantor,PriProv,AddrNote,FamFinUrgNote,MedUrgNote,ApptModNote,DateFirstVisit",phrase);
            sbSQL.AppendFormat(" FROM patient WHERE AddrNote LIKE '%{0}%' ", phrase);
            sbSQL.AppendFormat("or FamFinUrgNote LIKE '%{0}%' ",phrase);
			sbSQL.AppendFormat("or MedUrgNote LIKE '%{0}%' ",phrase);
			sbSQL.AppendFormat("or ApptModNote LIKE '%{0}%' ",phrase);
			sbSQL.AppendFormat("or EmploymentNote LIKE '%{0}%' ",phrase);


			Queries.CurReport=new ReportOld();
			Queries.CurReport.Query= sbSQL.ToString();
			FormQuery2=new FormQuery();
			FormQuery2.IsReport=false;
			FormQuery2.SubmitQuery();	
			FormQuery2.textQuery.Text=Queries.CurReport.Query;					
			FormQuery2.ShowDialog();
		}

		private void textPharse_TextChanged(object sender, System.EventArgs e)
		{
			if(textPharse.Text.Trim().Length > 0)
				butOK.Enabled=true;
			else
				butOK.Enabled = false;
		}
	}
}
