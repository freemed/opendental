using System;

namespace OpenDentBusiness {
	///<summary>For integrated wiki feature content.</summary>
	[Serializable]
	public class WikiPage:TableBase {
		///<summary>Primary key.</summary>
		[CrudColumn(IsPriKey=true)]
		public long WikiPageNum;
		///<summary>FK to userod.UserNum.</summary>
		public long UserNum;
		///<summary>Title to be displayed and referenced.</summary>
		public string PageTitle;
		///<summary>Content of page stored in "wiki markup language". This should never be updated.</summary>
		public string PageContent;
		///<summary>The DateTime that the page was saved to the DB.</summary>
		public DateTime DateTimeSaved;

		///<summary></summary>
		public WikiPage Copy() {
			return (WikiPage)MemberwiseClone();
		}

	}
}
