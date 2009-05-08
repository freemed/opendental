using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{
	///<summary>Handles database commands for the language table in the database.</summary>
	public class Lan{
		///<summary>stub</summary>
		private static bool itemInserted;

		//strings-----------------------------------------------
		///<summary>Converts a string to the current language.</summary>
		public static string g(string classType,string text) {
			string retVal=Lans.ConvertString(classType,text);
			if(itemInserted) {
				Lans.Refresh();
			}
			return retVal;
		}

		///<summary>Converts a string to the current language.</summary>
		public static string g(System.Object sender,string text) {
			string retVal=Lans.ConvertString(sender.GetType().Name,text);
			if(itemInserted) {
				Lans.Refresh();
			}
			return retVal;
		}

		//menuItems---------------------------------------------
		///<summary>C is for control. Translates the text of this control to another language.</summary>
		public static void C(string classType,System.Windows.Forms.MenuItem mi) {
			mi.Text=Lans.ConvertString(classType,mi.Text);
			if(itemInserted) {
				Lans.Refresh();
			}
		}

		///<summary></summary>
		public static void C(System.Windows.Forms.Control sender,System.Windows.Forms.MenuItem mi) {
			mi.Text=Lans.ConvertString(sender.GetType().Name,mi.Text);
			if(itemInserted) {
				Lans.Refresh();
			}
		}

		//controls-----------------------------------------------
		///<summary></summary>
		public static void C(string classType,System.Windows.Forms.Control[] contr) {
			for(int i=0;i<contr.Length;i++) {
				contr[i].Text=Lans.ConvertString(classType,contr[i].Text);
			}
			if(itemInserted) {
				Lans.Refresh();
			}
		}

		///<summary></summary>
		public static void C(System.Windows.Forms.Control sender,System.Windows.Forms.Control[] contr) {
			for(int i=0;i<contr.Length;i++) {
				if(contr[i].GetType()==typeof(UI.ODGrid)) {
					((UI.ODGrid)contr[i]).Title=Lans.ConvertString(((UI.ODGrid)contr[i]).TranslationName,((UI.ODGrid)contr[i]).Title);
					foreach(UI.ODGridColumn col in ((UI.ODGrid)contr[i]).Columns) {
						col.Heading=Lans.ConvertString(((UI.ODGrid)contr[i]).TranslationName,col.Heading);
					}
					continue;
				}
				contr[i].Text=Lans.ConvertString(sender.GetType().Name,contr[i].Text);
			}
			if(itemInserted) {
				Lans.Refresh();
			}
		}

		//forms----------------------------------------------------------------------------------------
		///<summary>F is for Form. Translates the following controls on the entire form: title Text, labels, buttons, groupboxes, checkboxes, radiobuttons.  Can include a list of controls to exclude. Also puts all the correct controls into the All category (OK,Cancel,Close,Delete,etc).</summary>
		public static void F(System.Windows.Forms.Form sender) {
			F(sender,new System.Windows.Forms.Control[] { });
		}

		///<summary>F is for Form. Translates the following controls on the entire form: title Text, labels, buttons, groupboxes, checkboxes, radiobuttons.  Can include a list of controls to exclude. Also puts all the correct controls into the All category (OK,Cancel,Close,Delete,etc).</summary>
		public static void F(Form sender,Control[] exclusions) {
			if(CultureInfo.CurrentCulture.Name=="en-US") {
				return;
			}
			//first translate the main title Text on the form:
			if(!Contains(exclusions,sender)) {
				sender.Text=Lans.ConvertString(sender.GetType().Name,sender.Text);
			}
			//then launch the recursive function for all child controls
			Fchildren(sender,sender,exclusions);
			if(itemInserted) {
				Lans.Refresh();
			}
		}

		///<summary>Returns true if the contrToFind exists in the supplied contrArray. Used to check the exclusion list of F.</summary>
		private static bool Contains(Control[] contrArray,Control contrToFind) {
			for(int i=0;i<contrArray.Length;i++) {
				if(contrArray[i]==contrToFind) {
					return true;
				}
			}
			return false;
		}

		///<summary>Called from F and also recursively. Translates all children of the given control except those in the exclusions list.</summary>
		private static void Fchildren(Form sender,Control parent,Control[] exclusions) {
			foreach(Control contr in parent.Controls) {
				if(contr.GetType()==typeof(UI.ODGrid)) {
					((UI.ODGrid)contr).Title=Lans.ConvertString(((UI.ODGrid)contr).TranslationName,((UI.ODGrid)contr).Title);
					foreach(UI.ODGridColumn col in ((UI.ODGrid)contr).Columns) {
						col.Heading=Lans.ConvertString(((UI.ODGrid)contr).TranslationName,col.Heading);
					}
					continue;
				}
				//any controls with children of their own.
				if(contr.HasChildren) {
					Fchildren(sender,contr,exclusions);
				}
				//ignore any controls except the types we are interested in
				if(contr.GetType()!=typeof(TextBox)
					&& contr.GetType()!=typeof(Button)
					&& contr.GetType()!=typeof(OpenDental.UI.Button)
					&& contr.GetType()!=typeof(Label)
					&& contr.GetType()!=typeof(GroupBox)
					&& contr.GetType()!=typeof(CheckBox)
					&& contr.GetType()!=typeof(RadioButton)) {
					continue;
				}
				if(contr.Text=="") {
					continue;
				}
				if(!Contains(exclusions,contr)) {
					if(contr.Text=="OK"
						|| contr.Text=="&OK"
						|| contr.Text=="Cancel"
						|| contr.Text=="&Cancel"
						|| contr.Text=="Close"
						|| contr.Text=="&Close"
						|| contr.Text=="Add"
						|| contr.Text=="&Add"
						|| contr.Text=="Delete"
						|| contr.Text=="&Delete"
						|| contr.Text=="Up"
						|| contr.Text=="&Up"
						|| contr.Text=="Down"
						|| contr.Text=="&Down"
						|| contr.Text=="Print"
						|| contr.Text=="&Print"
						//|| contr.Text==""
						) {
						contr.Text=Lans.ConvertString("All",contr.Text);
					}
					else {
						contr.Text=Lans.ConvertString(sender.GetType().Name,contr.Text);
					}
				}
			}
		}

	}

	

	

}













