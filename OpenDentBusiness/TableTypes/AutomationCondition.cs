using System;
using System.Collections;
using System.Drawing;

namespace OpenDentBusiness{
	///<summary></summary>
	[Serializable]
	public class AutomationCondition:TableBase{
		///<summary>Primary key.</summary>
		[CrudColumn(IsPriKey=true)]
		public long AutomationConditionNum;


		/*
		///<summary></summary>
		public string Description;
		///<summary>FK to restaurant.RestaurantNum.</summary>
		public long RestaurantNum;
		///<summary>Enum:HotDogToppings Ketchup, Mustard, Relish.</summary>
		public HotDogToppings Toppings;
		///<summary>For example, 6 or 12. Decimals allowed.</summary>
		public double Length;
		///<summary>Set to true to indicate your favorite dog.</summary>
		public bool IsFavorite;
		///<summary>The color of the carrying bag.</summary>
		public Color BagColor;*/

		///<summary></summary>
		public AutomationCondition Clone() {
			return (AutomationCondition)this.MemberwiseClone();
		}

	}
}