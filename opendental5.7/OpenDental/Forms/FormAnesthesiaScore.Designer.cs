namespace OpenDental.Forms
{
	partial class FormAnesthesiaScore
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormAnesthesiaScore));
			this.SuspendLayout();
			// 
			// FormAnesthesiaScore
			// 
			this.ClientSize = new System.Drawing.Size(776, 439);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "FormAnesthesiaScore";
			this.Text = "Post anesthesia score";
			this.Load += new System.EventHandler(this.FormAnesthesiaScore_Load);
			this.ResumeLayout(false);

		}

		#endregion
	}
}
