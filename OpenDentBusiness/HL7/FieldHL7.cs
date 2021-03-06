﻿using System;
using System.Collections.Generic;
using System.Text;

namespace OpenDentBusiness.HL7 {
	public class FieldHL7 {
		///<summary></summary>
		private string fullText;
		///<summary>Not often used. Some HL7 fields are allowed to "repeat" multiple times. For example, in immunization messaging export (VXU messages), PID-3 repeats twice, once for patient ID and once for SSN.</summary>
		private List<FieldHL7> _listRepeatFields=new List<FieldHL7>();
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
				StringBuilder sb=new StringBuilder();
				sb.Append(fullText);
				for(int i=0;i<_listRepeatFields.Count;i++) {
					sb.Append("~");//Field repitition separator.  Always before each repeat field, even if fullText is blank.
					sb.Append(_listRepeatFields[i].FullText);
				}
				return sb.ToString();
			}
			set {
				fullText=value;
				Components=new List<ComponentHL7>();
				//In the future, we could first split by ~ to get the repeating fields as needed, then split each field instance by the ^ character.
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
			if(values.Length==1) {
				FullText=values[0];//this allows us to pass in all components for the field as one long string: comp1^comp2^comp3
				return;
			}
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

		///<summary>Not often used. Some HL7 fields are allowed to "repeat" multiple times. For example, in immunization messaging export (VXU messages), PID-3 repeats twice, once for patient ID and once for SSN.</summary>
		public void RepeatVals(params string[] values) {
			FieldHL7 field=new FieldHL7();
			field.SetVals(values);
			_listRepeatFields.Add(field);
		}
		 
	}
}
