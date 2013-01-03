using System;

namespace OpenDentBusiness {
	///<summary>Rows never edited, just added.  Contains all only newest versions of each page.</summary>
	[Serializable]
	public class WikiPage:TableBase {
		///<summary>Primary key.</summary>
		[CrudColumn(IsPriKey=true)]
		public long WikiPageNum;
		///<summary>FK to userod.UserNum.</summary>
		public long UserNum;
		///<summary>Must be unique.  Any character is allowed except: \r, \n, and ".  Needs to be tested, especially with apostrophes.</summary>
		public string PageTitle;
		///<summary>Automatically filled from the [[Keywords:]] tab in the PageContent field.</summary>
		public string KeyWords;
		///<summary>Content of page stored in "wiki markup language".  This should never be updated.  Medtext (16M)</summary>
		public string PageContent;
		///<summary>The DateTime that the page was saved to the DB.  User can't edit.</summary>
		[CrudColumn(SpecialType=CrudSpecialColType.DateTEntry)]
		public DateTime DateTimeSaved;
		//<summary>Deprecated.  Remove this.  When used in wikipagehist, this flag will only be set for the revision where the user marked it deleted (the last one).</summary>
		//public bool IsDeleted;

		///<summary></summary>
		public WikiPage Copy() {
			return (WikiPage)MemberwiseClone();
		}

		

	}
}
