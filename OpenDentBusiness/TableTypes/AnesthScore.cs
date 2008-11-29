using System;

namespace OpenDentBusiness
{

	///<summary>A single Anesthesia Score.</summary>
		public class AnesthScore{
		///<summary>Primary key.</summary>
        public int AnesthScoreNum;
        ///<summary>FK to anestheticRecord.AnestheticRecordNum.</summary>
		public int AnestheticRecordNum;
		public int QActivity;
		public int QResp;
		public int QCirc;
		public int QConc;
		public int QColor;
		public int AnesthesiaScore;
		public int DischAmb;
		public int DischWheelChr;
		public int DischAmbulance;
		public int DischCondStable;
		public int DischCondUnstable;

		
	}


}

