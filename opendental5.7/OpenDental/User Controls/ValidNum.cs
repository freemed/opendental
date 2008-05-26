using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using CodeBase;

namespace OpenDental{
///<summary>this differs slightly from ValidNumber.  Use this when default is 0 instead of blank.</summary>
	public class ValidNum : System.Windows.Forms.TextBox{
		private System.ComponentModel.Container components = null;
		public ODErrorProvider errorProvider1=new ODErrorProvider();
		///<summary></summary>
		private int maxVal=255;
		///<summary></summary>
		private int minVal=0;

		///<summary></summary>
		public ValidNum(){
			InitializeComponent();
  	}

		///<summary></summary>
		protected override void Dispose( bool disposing ){
			if( disposing ){
				if(components != null){
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.SuspendLayout();
			// 
			// ValidNum
			// 
			this.Validated += new System.EventHandler(this.ValidNum_Validated);
			this.Validating += new System.ComponentModel.CancelEventHandler(this.ValidNum_Validating);
			this.ResumeLayout(false);

		}
		#endregion

		///<summary>The minumum value that this number can be set to without generating an error.</summary>
		public int MinVal{
			get{
				return minVal;
			}
			set{
				minVal=value;
			}
		}

		///<summary>The maximum value that this number can be set to without generating an error.</summary>
		public int MaxVal{
			get{
				return maxVal;
			}
			set{
				maxVal=value;
			}
		}

		private void ValidNum_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
			string myMessage="";
			try{
				if(Text==""){
					Text="0";
				}
				if(System.Convert.ToInt32(this.Text)>MaxVal)
					throw new Exception("Number must be less than "+(MaxVal+1).ToString());
				if(System.Convert.ToInt32(this.Text)<minVal)
					throw new Exception("Number must be greater than or equal to "+(minVal).ToString());
				errorProvider1.SetError(this,"");
			}
			catch(Exception ex){
				if(ex.Message=="Input string was not in a correct format."){
					myMessage="Must be a number. No letters or symbols allowed";
				}
				else{
					myMessage=ex.Message;
				}
				this.errorProvider1.SetError(this,myMessage);
			}
		}

		private void ValidNum_Validated(object sender, System.EventArgs e) {			
			//FormValid=true;
		}


	}
}
