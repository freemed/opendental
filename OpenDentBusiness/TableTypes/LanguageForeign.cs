using System;
using System.Collections;

namespace OpenDentBusiness{
	
	///<summary>Will usually only contain translations for a single foreign language, although more are allowed.  The primary key is a combination of the ClassType and the English phrase and the culture.</summary>
	public class LanguageForeign{
		///<summary>A string representing the class where the translation is used.</summary>
		public string ClassType;
		///<summary>The English version of the phrase.  Case sensitive.</summary>
		public string English;
		///<summary>The specific culture name.  Almost always in 5 digit format like this: en-US.</summary>
		public string Culture;
		///<summary>The foreign translation.  Remember we use Unicode-8, so this translation can be in any language, including Russian, Hebrew, and Chinese.</summary>
		public string Translation;
		///<summary>Comments for other translators for the foreign language.</summary>
		public string Comments;

		///<summary></summary>
		public LanguageForeign Copy(){
			LanguageForeign l=new LanguageForeign();
			l.ClassType=ClassType;
			l.English=English;
			l.Culture=Culture;
			l.Translation=Translation;
			l.Comments=Comments;
			return l;
		}

		

	}

	

	

}













