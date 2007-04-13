using System;
using System.Collections;
using System.Drawing;

namespace OpenDentBusiness{

	/// <summary>This defines the items that will be available for clicking when composing a manual message.  Also, these are referred to in the button definitions as a sequence of elements.</summary>
	public class SigElementDef{
		///<summary>Primary key.</summary>
		public int SigElementDefNum;
		///<summary>If this element should cause a button to light up, this would be the row.  0 means none.</summary>
		public int LightRow;
		///<summary>If a light row is set, this is the color it will turn when triggered.  Ack sets it back to white.  Note that color and row can be in two separate elements of the same signal.</summary>
		public Color LightColor;
		///<summary>Enum:SignalElementType  0=User,1=Extra,2=Message.</summary>
		public SignalElementType SigElementType;
		///<summary>The text that shows for the element, like the user name or the two word message.  No long text is stored here.</summary>
		public string SigText;
		///<summary>The sound to play for this element.  Wav file stored in the database in string format until "played".  If empty string, then no sound.</summary>
		public string Sound;
		///<summary>The order of this element within the list of the same type.</summary>
		public int ItemOrder;

		///<summary></summary>
		public SigElementDef Copy() {
			SigElementDef s=new SigElementDef();
			s.SigElementDefNum=SigElementDefNum;
			s.LightRow=LightRow;
			s.LightColor=LightColor;
			s.SigElementType=SigElementType;
			s.SigText=SigText;
			s.Sound=Sound;
			s.ItemOrder=ItemOrder;
			return s;
		}

		
		
	}

		



		
	

	

	


}










