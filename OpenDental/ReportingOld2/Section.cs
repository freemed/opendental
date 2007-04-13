using System;

namespace OpenDental.ReportingOld2
{
	///<summary>Every ReportObject in a ODReport must be attached to a Section.</summary>
	public class Section{
		///<summary></summary>
		private string name;
		///<summary></summary>
		private int height;
		///<summary>Width is usually the entire page unless set differently here.</summary>
		private int width;
		///<summary>Specifies which kind, like ReportHeader, or GroupFooter.</summary>
		private AreaSectionKind kind;

		///<summary></summary>
		public Section(AreaSectionKind myKind,int myHeight){
			kind=myKind;
			//name is not user editable, so:
			switch(kind){
				case AreaSectionKind.ReportHeader:
					name="Report Header";
					break;
				case AreaSectionKind.PageHeader:
					name="Page Header";
					break;
				//case AreaSectionKind.GroupHeader:
				case AreaSectionKind.Detail:
					name="Detail";
					break;
				//case AreaSectionKind.GroupFooter:
				case AreaSectionKind.PageFooter:
					name="Page Footer";
					break;
				case AreaSectionKind.ReportFooter:
					name="Report Footer";
					break;
			}
			height=myHeight;
		}

#region Properties
		///<summary>Not user editable.</summary>
		public string Name{
			get{
				return name;
			}
		}
		///<summary></summary>
		public int Height{
			get{
				return height;
			}
			set{
				height=value;
			}
		}
		///<summary></summary>
		public int Width{
			get{
				return width;
			}
			set{
				width=value;
			}
		}
		///<summary></summary>
		public AreaSectionKind Kind{
			get{
				return kind;
			}
			set{
				kind=value;
			}
		}
#endregion


	}


	///<summary>The type of section is used in the Section class.  Only ONE of each type is allowed except for the GroupHeader and GroupFooter which are optional and can have one pair for each group.  The order of the sections is locked and user cannot change.</summary>
	public enum AreaSectionKind{
		///<summary>Printed at the top of the report.</summary>
		ReportHeader,
		///<summary>Printed at the top of each page.</summary>
		PageHeader,
		///<summary>Not implemented yet. Will print at the top of a specific group.</summary>
		GroupHeader,
		///<summary>This is the data of the report and represents one row of data.  This section gets printed once for each record in the datatable.</summary>
		Detail,
		///<summary>Not implemented yet.</summary>
		GroupFooter,
		///<summary>Prints at the bottom of each page, including after the reportFooter</summary>
		PageFooter,
		///<summary>Prints at the bottom of the report, but before the page footer for the last page.</summary>
		ReportFooter,
		
		
		
		
		
		
	}

}












