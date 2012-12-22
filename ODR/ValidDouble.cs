using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using CodeBase;

namespace ODR{
///<summary></summary>
	public class ValidDouble:System.Windows.Forms.TextBox {
		public ErrorProvider errorProvider1=new ErrorProvider();
		///<summary></summary>
		public Double MaxVal=100000000;
		///<summary></summary>
		public Double MinVal=-100000000;

		///<summary></summary>
		public ValidDouble(){
			InitializeComponent();
		}

		#region Component Designer generated code

		private void InitializeComponent(){
			this.SuspendLayout();
			// 
			// ValidDouble
			// 
			this.Validating += new System.ComponentModel.CancelEventHandler(this.ValidDouble_Validating);
			this.ResumeLayout(false);

		}
		#endregion

		private void ValidDouble_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
			string myMessage="";
			try{
				if(Text==""){
					errorProvider1.SetError(this,"");
					return;//Text="0";
				}
				if(System.Convert.ToDouble(this.Text)>MaxVal)
					throw new Exception("Number must be less than "+(MaxVal+1).ToString());
				if(System.Convert.ToDouble(this.Text)<MinVal)
					throw new Exception("Number must be greater than or equal to "+(MinVal).ToString());
				errorProvider1.SetError(this,"");
			}
			catch(Exception ex){
				if(ex.Message=="Input string was not in a correct format."){
					myMessage="Must be a number. No letters or symbols allowed";
				}
				else{
					myMessage=ex.Message;
				}	
				errorProvider1.SetError(this,myMessage);
			}			
		}


	}
}
