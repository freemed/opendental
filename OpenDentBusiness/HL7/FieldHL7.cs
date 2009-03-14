using System;
using System.Collections.Generic;
using System.Text;

namespace OpenDentBusiness.HL7 {
	public class FieldHL7 {
		///<summary></summary>
		private string fullText;
		public List<ComponentHL7> Components;

		///<summary>Only use this constructor when generating a message instead of parsing a message.</summary>
		internal FieldHL7(){
			fullText="";
			Components=new List<ComponentHL7>();
			ComponentHL7 component;
			component=new ComponentHL7("");
			Components.Add(component);
			//add more components later if needed.
		}

		public FieldHL7(string fieldText) {
			FullText=fieldText;
		}

		public override string ToString() {
			return fullText;
		}

		///<summary>Setting the FullText resets all the child components to the values passed in here.</summary>
		public string FullText {
			get {
				return fullText;
			}
			set {
				fullText=value;
				Components=new List<ComponentHL7>();
				string[] components=fullText.Split(new string[] { "^" },StringSplitOptions.None);//leave empty entries in place
				ComponentHL7 component;
				for(int i=0;i<components.Length;i++) {
					component=new ComponentHL7(components[i]);
					Components.Add(component);
				}
			}
		}

		///<summary></summary>
		public string GetComponentVal(int indexPos) {
			if(indexPos > Components.Count-1) {
				return "";
			}
			return Components[indexPos].ComponentVal;
		}

		///<summary>This also resets the number of components.  And it sets fullText.</summary>
		public void SetVals(params string[] values){
			fullText="";
			Components=new List<ComponentHL7>();
			ComponentHL7 component;
			for(int i=0;i<values.Length;i++) {
				component=new ComponentHL7(values[i]);
				Components.Add(component);
				fullText+=values[i];
				if(i<values.Length-1) {
					fullText+="^";
				}
			}
		}

		 
	}
}
