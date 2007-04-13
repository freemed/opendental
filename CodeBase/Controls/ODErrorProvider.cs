using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace CodeBase {
	public class ODErrorProvider {

		///<summary>Made public so an owning control or form can set the desired error color.</summary>
		public Color errorColor=Color.Red;
		///<summary>Our list of controls and their associated errors.</summary>
		private ArrayList controls=new ArrayList();
		///<summary>List of associated error messages for the controls.</summary>
		private ArrayList controlMessages=new ArrayList();
		///<summary>Stores old background colors for text-boxes, so that when the error is cleared, the box can be returned to its original state.</summary>
		private ArrayList controlColors=new ArrayList();

		///<summary>Sets the error description string for the specified control. If the string length is greater than zero, then an error is visually indicated to the user. If the string length is zero or null, there is considered to be no error associated with the given control. </summary>
		public void SetError(Control control,string value){
			int controlIndex=controls.IndexOf(control);//control can be null.
			if(controlIndex==-1){//Control not found?
				//Add the control and the error text.
				controls.Add(control);
				controlMessages.Add(value);
				if(control!=null){
					controlColors.Add(control.ForeColor);
				}else{
					controlColors.Add(Color.White);
				}
				controlIndex=controls.Count-1;
			}else{//Control was found.
				//Update the error text.
				controlMessages[controlIndex]=value;
			}
			//At this point, controlIndex is always set to a valid index.
			if(control!=null){
				if(value==null || value==""){//Clear the error condition.
					control.ForeColor=(Color)controlColors[controlIndex];
				}else{//Set the error condition.
					control.ForeColor=errorColor;
				}
			}
		}

		///<summary>Returns the current error description string for the specified control, or the empty string if the control has no error message set. </summary>
		public string GetError(Control control){
			int controlIndex=controls.IndexOf(control);
			if(controlIndex==-1){
				//No errors are associated with controls which have not had errors set yet.
				return "";
			}
			string value=(string)controlMessages[controlIndex];
			if(value==null){
				return "";
			}
			return value;
		}

	}
}
