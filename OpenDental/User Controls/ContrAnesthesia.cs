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
		///<summary>The index in Anesthesics.List of the currently selected anesthetic.</summary>
		private int selectedAnestheticRecord;

		public AnestheticRecord AnestheticRecordCur;
		///<summary>The index in Anesthetics.List of the currently selected anesthetic.</summary>
		public int SelectedAnestheticRecord{
			get{
				return selectedAnestheticRecord;
			}
				set{
				selectedAnestheticRecord=value;
			}
		}

		///<summary>Used in LoadData.</summary>

		///<summary>Saves the cur exam measurements to the db by looping through each tooth and deciding whether the data for that tooth has changed.  If so, it either updates or inserts a measurement.  It won't delete a measurement if all values for that tooth are cleared, but just sets that measurement to all -1's.</summary>
		public void SaveCurAnestheticRecord(int anestheticRecordNum)
		{
		}
		///<summary></summary>
		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);
			Focus();
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
















