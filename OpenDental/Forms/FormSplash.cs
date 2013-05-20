using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Text;
using System.Windows.Forms;

namespace OpenDental
{   ///<summary></summary>
    public partial class FormSplash : Form
    {
        ///<summary></summary>
        public FormSplash()
        {
            InitializeComponent();
        }

        private void FormSplash_Load(object sender, EventArgs e)
        {
					if(CultureInfo.CurrentCulture.Name.EndsWith("CA")) {//Canadian. en-CA or fr-CA
						BackgroundImage=Properties.Resources.splashCanada;
					}
        }
    }
}