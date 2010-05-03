using System;
using System.Collections.Generic;
using System.Text;
using OpenDentBusiness;

namespace UnitTests {
	public class CarrierT {
		public static Carrier CreateCarrier(string suffix){
			Carrier carrier=new Carrier();
			carrier.CarrierName="Carrier"+suffix;
			Carriers.Insert(carrier);
			return carrier;
		}




	}
}
