/*
using System;

namespace OpenDentBusiness
{

	///<summary>Anesthetic data from a single Anesthetic Record.</summary>
	public class AnestheticData
	{
		///<summary>Primary key.</summary>
		public long AnestheticDataNum;
		///<summary>FK to anestheticRecord.AnestheticRecordNum.</summary>
		public long AnestheticRecordNum;
		public string AnesthOpen;
		public string AnesthClose;
		public string SurgOpen;
		public string SurgClose;
		public string Anesthetist; //data from OD provider list
		public string Surgeon; //data from OD provider list
		public string Asst; //data from OD provider list
		public string Circulator; //data from OD provider list
		public string VSMName;
		public string VSMSerNum;
		public string ASA;
		public string ASA_EModifier;
		public long O2LMin;
		public long N2OLMin;
		public bool RteNasCan;
		public bool RteNasHood;
		public bool RteETT;
		public bool MedRouteIVCath;
		public bool MedRouteIVButtFly;
		public bool MedRouteIM;
		public bool MedRoutePO;
		public bool MedRouteNasal;
		public bool MedRouteRectal;
		public string IVSite;
		public long IVGauge;
		public bool IVSideR;
		public bool IVSideL;
		public long IVAtt;
		public string IVF;
		public long IVFVol;
		public bool MonBP;
		public bool MonSpO2;
		public bool MonEtCO2;
		public bool MonPrecordial;
		public bool MonTemp;
		public bool MonEKG;
		public string Notes;
		public long PatWgt;
		public bool WgtUnitsLbs;
		public bool WgtUnitsKgs;
		public long PatHgt;
		public bool HgtUnitsIn;
		public bool HgtUnitsCm;
		public string EscortName;
		public string EscortCellNum;
		public string EscortRel;
		public string NPOTime;
		public string Signature;
		public bool SigIsTopaz;

		///<summary>Returns a copy of the Anesthetic Data.</summary>
		public AnestheticData Copy()
		{
			return (AnestheticData)this.MemberwiseClone();

		}



	}

}

*/