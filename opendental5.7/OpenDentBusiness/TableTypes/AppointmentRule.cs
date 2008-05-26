using System;
using System.Collections;

namespace OpenDentBusiness{
	
	///<summary>For now, the rule is simple. It simply blocks all double booking of the specified code range.  The double booking would have to be for the same provider.  This can later be extended to provide more complex rules, such as partial double booking, time limitations, etc.</summary>
	public class AppointmentRule{
		///<summary>Primary key.</summary>
		public int AppointmentRuleNum;
		///<summary>The description of the rule which will be displayed to the user.</summary>
		public string RuleDesc;
		///<summary>The procedure code of the start of the range.</summary>
		public string CodeStart;
		///<summary>The procedure code of the end of the range.</summary>
		public string CodeEnd;
		///<summary>Usually true.  But this does allow you to turn off a rule temporarily without losing the settings.</summary>
		public bool IsEnabled;

		///<summary>Returns a copy of this AppointmentRule.</summary>
		public AppointmentRule Copy(){
			AppointmentRule a=new AppointmentRule();
			a.AppointmentRuleNum=AppointmentRuleNum;
			a.RuleDesc=RuleDesc;
			a.CodeStart=CodeStart;
			a.CodeEnd=CodeEnd;
			a.IsEnabled=IsEnabled;
			return a;
		}

	}
	


}













