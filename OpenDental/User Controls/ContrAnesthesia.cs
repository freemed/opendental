using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Data;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental
{
	///<summary>Summary description for ContrAnesthesia.</summary>
	public class ContrAnesthesia : System.Windows.Forms.Control{
		///<summary>Required designer variable.</summary>
		private System.ComponentModel.Container components = null;
		///<summary>This data array gets filled when loading an anesthetic. It is altered as the user makes changes, and then the results are saved to the db by reading from this array.</summary>
		///<summary>The index in Anesthesics.List of the currently selected exam.</summary>
		private int selectedAnestheticRecord;
		public AnestheticRecord AnestheticRecordCur;
		///<summary>The index in Anesthetics.List of the currently selected anesthetic.</summary>
		public int SelectedAnestheticRecord{
			get{
				return SelectedAnestheticRecord;
			}
			//set{
				//selectedAnestheticRecord=value;
			//}
		}

		///<summary></summary>
		protected override Size DefaultSize{
			get{
				return new Size(590,665);
			}
		}

		///<summary></summary>
		public ContrAnesthesia(){
			
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
		
		}



}
















