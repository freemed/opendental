using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace OpenDental.UI {
	///<summary>Wraps the Topaz SigPlusNET control and the alternate SignatureBox control.  Also includes both needed buttons.  Should vastly simplify using signature boxes throughout the program.</summary>
	public partial class SignatureBoxWrapper:UserControl {
		

		public SignatureBoxWrapper() {
			InitializeComponent();
			//this.DefaultSize=new Size(362,79);
		}
	}
}
