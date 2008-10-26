namespace OpenDental.Forms
{
	partial class FormAnestheticMedsInventory
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormAnestheticMedsInventory));
			this.SuspendLayout();
			// 
			// FormAnesthMedsInventory
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(765, 318);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "FormAnesthMedsInventory";
			this.Text = "Anesthetic Medication Inventory";
			this.ResumeLayout(false);

		}

		#endregion
        private OpenDental.UI.Button butClose;
        private OpenDental.UI.ODGrid gridAnesthMedsInventory;
        private OpenDental.UI.Button butAdd;
	}
}