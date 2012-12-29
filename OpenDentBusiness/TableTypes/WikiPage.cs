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
		///<summary>Must be unique.  The only allowed characters are letters, numbers, and spaces.  When spaces are used, they are replaced with underscores during the translation to html.</summary>
		public string PageTitle;
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

		public WikiPageHist ToWikiPageHist() {
			WikiPageHist retVal=new WikiPageHist();
			retVal.WikiPageNum=-1;//todo:handle this -1, shouldn't be a problem since we always get pages by Title.
			retVal.UserNum=UserNum;
			retVal.PageTitle=PageTitle;
			retVal.PageContent=PageContent;
			retVal.DateTimeSaved=DateTimeSaved;
			retVal.IsDeleted=false;//we know this becuase wikipages do not exists unless they are not deleted.
			return retVal;
		}

	}
}
