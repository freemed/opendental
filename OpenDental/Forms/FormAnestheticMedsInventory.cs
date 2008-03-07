using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;
using OpenDental.UI;

namespace OpenDental.Forms
{
	public partial class FormAnestheticMedsInventory : Form{
		
		public FormAnestheticMedsInventory()
		{
			InitializeComponent();

		}

		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormAnestheticMedsInventory));
			this.SuspendLayout();
			// 
			// FormAnestheticMedsInventory
			// 
			this.ClientSize = new System.Drawing.Size(836, 339);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "FormAnestheticMedsInventory";
			this.Text = "Anesthetic Medication Inventory";
			this.ResumeLayout(false);

		}
	}
}
