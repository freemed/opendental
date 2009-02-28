using System;
using System.Collections.Generic;
using System.Text;

namespace OpenDentHL7 {
	public class FieldHL7 {
		///<summary></summary>
		public string FullText;
		public List<ComponentHL7> Components;

		public FieldHL7(string fieldText) {
			FullText=fieldText;
			Components=new List<ComponentHL7>();
			string[] components=fieldText.Split(new string[] { "^" },StringSplitOptions.None);
			ComponentHL7 component;
			for(int i=0;i<components.Length;i++) {
				component=new ComponentHL7(components[i]);
				Components.Add(component);
			}
		}

		public override string ToString() {
			return FullText;
		}

		///<summary></summary>
		public string GetComponentVal(int indexPos) {
			if(indexPos > Components.Count-1) {
				return "";
			}
			return Components[indexPos].ComponentVal;
		}
	}
}
