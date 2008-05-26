using System;

namespace OpenDentBusiness
{

	///<summary>Anesthetic data from an Anesthetic Record.</summary>
	public class AnestheticData{
		///<summary>Primary key.</summary>
        public int AnestheticRecordNum;
        ///<summary>FK to patient.PatNum.</summary>
		public int AnesthOpen;
		public int AnesthClose;
		public int SurgOpen;
		public int SurgClose;
		public string Anesthetist; //data from OD provider list
		public string Surgeon; //data from OD provider list
		public int Asst; //data from OD provider list
		public int Circulator; //data from OD provider list
		public int AnestheticMed;
		public int Dose;
		public DateTime DoseTimeStamp;
		public string VSM;
		public string VSMSerialNum;
		public string BP;
		public int HR;
		public int SPO2;
		public int EtCO2;
		public int Temp;
		public string ASA;
		public int InhO2;
		public int O2Lmin;
		public int InhN20;
		public int N20Lmin;
		public bool RteNasHood;
		public bool RteNasCan;
		public bool RteETT;
		public bool IVCath;
		public bool IVButFly;
		public string IVSite;
		public int IVGauge;
		public bool IVSiteR;
		public bool IVSiteL;
		public int IVAtt;
		public string IVF;
		public int IVFVol;
		public string Notes;
		public string PatWgt;
		public string PatHgt;
		public string EscortName;
		public string EscortRel;
		public string NPOTime;
		//public string SigBox;

	}





}

