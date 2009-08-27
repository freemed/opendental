using System;

namespace OpenDentBusiness
{

	///<summary>A single Anesthesia Score.</summary>
		public class AnesthScore{
		///<summary>Primary key.</summary>
        public long AnesthScoreNum;
        ///<summary>FK to anestheticRecord.AnestheticRecordNum.</summary>
		public long AnestheticRecordNum;
		public long QActivity;
		public long QResp;
		public long QCirc;
		public long QConc;
		public long QColor;
		public long AnesthesiaScore;
		public long DischAmb;
		public long DischWheelChr;
		public long DischAmbulance;
		public long DischCondStable;
		public long DischCondUnstable;

		
	}


}

