using System;
using System.Collections;
using OpenDental.DataAccess;

namespace OpenDentBusiness{

	///<summary>Vital sign data for a patient during an Anesthetic, designed to be imported from a networked vital sign monitor.</summary>
	[DataObject("anesthvsdata")]
	public class AnestheticVSData : DataObjectBase {

		
		[DataField("AnesthVSDataNum", PrimaryKey=true, AutoNumber=true)]
		private int anesthVSDataNum;
		bool anesthVSDataNumChanged;
		/// <summary>Primary key.</summary>
		public int AnesthVSDataNum {
			get { return anesthVSDataNum; }
			set { anesthVSDataNum = value; MarkDirty(); anesthVSDataNumChanged = true; }
		}
		public bool AnesthVSDataNumChanged {
			get { return anesthVSDataNumChanged; }
		}

		[DataField("AnestheticRecordNum")]
		private int anestheticRecordNum;
		bool anestheticRecordNumChanged;
		/// <summary>Primary key.</summary>
		public int AnestheticRecordNum
		{
			get { return anestheticRecordNum; }
			set { anestheticRecordNum = value; MarkDirty(); anestheticRecordNumChanged = true; }
		}
		public bool AnestheticRecordNumChanged
		{
			get { return anestheticRecordNumChanged; }
		}
		//F.K. table anestheticrecord
		[DataField("PatNum")]
		private string patNum;
		bool patNumChanged;
		/// <summary>A unique patient</summary>
		public string PatNum
		{
			get { return patNum; }
			set { patNum = value; MarkDirty(); patNumChanged = true; }
		}
		public bool PatNumChanged
		{
			get { return patNumChanged; }
		}

		[DataField("VSMName")]
		private string vSMName;
		bool vSMNameChanged;
		/// <summary>Model name of the networked vital sign monitor</summary>
		public string VSMName {
			get { return vSMName; }
			set { vSMName = value; MarkDirty(); vSMNameChanged = true; }
		}
		public bool vSMChanged {
			get { return vSMNameChanged; }
		}

		[DataField("VSMSerNum")]
		private string vSMSerNum;
		bool vSMSerNumChanged;
		/// <summary>Serial # of the networked monitor</summary>
		
		public string VSMSerNum {
			get { return vSMSerNum; }
			set { vSMSerNum = value; MarkDirty(); vSMSerNumChanged = true; }
		}
		public bool VSMSerNumChanged {
			get { return vSMSerNumChanged; }
		}

		[DataField("BPSys")]
		private string bPSys;
		bool bPSysChanged;
		/// <summary>Systolic BP</summary>
		public string BPSys {
			get { return bPSys; }
			set { bPSys = value; MarkDirty(); bPSysChanged = true; }
		}
		public bool BPSysChanged {
			get { return bPSysChanged; }
		}

		[DataField("BPDias")]
		private string bPDias;
		bool bPDiasChanged;
		/// <summary>Diastolic BP</summary>
		public string BPDias
		{
			get { return bPDias; }
			set { bPDias = value; MarkDirty(); bPDiasChanged = true; }
		}
		public bool BPDiasChanged
		{
			get { return bPDiasChanged; }
		}
			
		[DataField("BPMAP")]
		private string bPMAP;
		bool bPMAPChanged;
		/// <summary>Mean arterial blood pressure</summary>

		public string BPMAP
		{
			get { return bPMAP; }
			set { bPMAP = value; MarkDirty(); bPMAPChanged = true; }
		}
		public bool BPMAPChanged
		{
			get { return bPMAPChanged; }
		}

		[DataField("HR")]
		private string hR;
		bool hRChanged;
		/// <summary>Heart rate</summary>

		public string HR
		{
			get { return hR; }
			set { hR = value; MarkDirty(); hRChanged = true; }
		}
		public bool HRChanged
		{
			get { return hRChanged; }
		}

		[DataField("SpO2")]
		private string spO2;
		bool spO2Changed;
		/// <summary>Oxygen saturation</summary>

		public string SpO2
		{
			get { return spO2; }
			set { spO2 = value; MarkDirty(); spO2Changed = true; }
		}
		public bool SpO2Changed
		{
			get { return spO2Changed; }
		}

		[DataField("EtCO2")]
		private string etCO2;
		bool etCO2Changed;
		/// <summary>End tidal CO2</summary>

		public string EtCO2
		{
			get { return etCO2; }
			set { etCO2 = value; MarkDirty(); etCO2Changed = true; }
		}
		public bool EtCO2Changed
		{
			get { return etCO2Changed; }
		}

		[DataField("Temp")]
		private string temp;
		bool tempChanged;
		/// <summary>Temperature</summary>

		public string Temp
		{
			get { return temp; }
			set { temp = value; MarkDirty(); tempChanged = true; }
		}
		public bool TempChanged
		{
			get { return tempChanged; }
		}

		[DataField("VSTimeStamp")]
		private string vSTimeStamp;
		bool vSTimeStampChanged;
		/// <summary>Time Stamp</summary>

		public string VSTimeStamp
		{
			get { return vSTimeStamp; }
			set { vSTimeStamp = value; MarkDirty(); vSTimeStampChanged = true; }
		}
		public bool VSTimeStampChanged
		{
			get { return vSTimeStampChanged; }
		}

		[DataField("MessageID")]
		private string messageID;
		bool messageIDChanged;
		/// <summary>Time Stamp</summary>

		public string MessageID
		{
			get { return messageID; }
			set { messageID = value; MarkDirty(); messageIDChanged = true; }
		}
		public bool MessageIDChanged
		{
			get { return messageIDChanged; }
		}

				[DataField("HL7Message")]
		private string hL7Message;
		bool hL7MessageChanged;
		/// <summary>Time Stamp</summary>

		public string HL7Message
		{
			get { return hL7Message; }
			set { hL7Message = value; MarkDirty(); hL7MessageChanged = true; }
		}
		public bool HL7MessageChanged
		{
			get { return hL7MessageChanged; }
		}


	}

}









