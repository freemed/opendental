using System;

namespace OpenDentBusiness {
	///<summary>Rows never edited, just added.  Contains all historical versions of each page as well.</summary>
	[Serializable]
	public class WikiPage:TableBase {
		///<summary>Primary key.</summary>
		[CrudColumn(IsPriKey=true)]
		public long WikiPageNum;
		///<summary>FK to userod.UserNum.</summary>
		public long UserNum;
		///<summary>must be unique (except for revisions)</summary>
		public string PageTitle;
		///<summary>Content of page stored in "wiki markup language".  This should never be updated.  Medtext (16M)</summary>
		public string PageContent;
		///<summary>The DateTime that the page was saved to the DB.  User can't edit.</summary>
		[CrudColumn(SpecialType=CrudSpecialColType.DateTEntry)]
		public DateTime DateTimeSaved;

		///<summary></summary>
		public WikiPage Copy() {
			return (WikiPage)MemberwiseClone();
		}

	}
}
