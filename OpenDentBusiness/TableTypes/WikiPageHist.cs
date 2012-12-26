using System;

namespace OpenDentBusiness {
	///<summary>Rows never edited, just added.  Contains all historical versions of each page as well.</summary>
	[Serializable]
	public class WikiPageHist:TableBase {
		///<summary>Primary key.</summary>
		[CrudColumn(IsPriKey=true)]
		public long WikiPageNum;
		///<summary>FK to userod.UserNum.</summary>
		public long UserNum;
		///<summary>Must be unique (except for revisions).  The only allowed characters are letters, numbers, and spaces.  When spaces are used, they are replaced with underscores during the translation to html.</summary>
		public string PageTitle;
		///<summary>Content of page stored in "wiki markup language".  This should never be updated.  Medtext (16M)</summary>
		public string PageContent;
		///<summary>The DateTime that the page was saved to the DB.  User can't edit.</summary>
		[CrudColumn(SpecialType=CrudSpecialColType.DateTEntry)]
		public DateTime DateTimeSaved;
		///<summary>This flag will only be set for the revision where the user marked it deleted (the last one).</summary>
		public bool IsDeleted;

		///<summary></summary>
		public WikiPage Copy() {
			return (WikiPage)MemberwiseClone();
		}

	}
}
