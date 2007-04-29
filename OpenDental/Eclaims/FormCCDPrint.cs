using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Globalization;
using System.Text;
using System.Windows.Forms;
using OpenDental;
using OpenDentBusiness;
using CodeBase;

/*
 * TODOS: (not in any particular order)
 * *Finish procedure table for EOB.
 * *Finish upper/lower placement notes for bullet 4.
 * *Print claim display messages.
 * *Be sure all fields have been processed in each form, either directory or indirectly.
 * *Merge forms into 1? (see pages 48 and 122 in message formats).
 * *Fix French accented characters which are modified on output. Could move error text to an in-code table, but that doesn't
 *  take care of the etrans.MessateText string.
 * *Add option in UI to print dentist copy.
 * *Display multiple copies of each form when more than 7 procedures are present ((int)(numprocedures/7)+1 forms total).
 * *Finish the only unfinished Dentaide form bullet number.
 * *Handle embedded message text.
 * *Set first insurace info to second insurance and second insurance info to null when this form is 
 * handling an embedded transaction.
 * *Test embedded transactions. This should be first.
 */

namespace OpenDental.Eclaims {
	public partial class FormCCDPrint:Form {

		#region Internal Variables

		PrintDocument pd;
		///<summary>Total pages in the printed document.</summary>
		int totalPages;
		///<summary>Keeps track of the number of pages which have already been completely printed.</summary>
		int pagesPrinted;
		///<summary>Set to true to print page number in upper right-hand corner of the screen.</summary>
		bool printPageNumbers;
		///<summary>Set to true when the document has not been renderd into the local container.</summary>
		bool dirty=true;
		///<summary>This form is printable until there is a critical failure (if one arises).</summary>
		bool printable=true;
		///<summary>English by default (represented by false). Set to true later if using French.</summary>
		bool isFrench=false;
		bool embedded;
		bool assigned;
		bool predetermination;
		///<summary>Keeps track of wether we are printing the dentist or patient form.</summary>
		bool patientCopy=true;
		///<summary>directly related to isFrench. Is Canadian English by default.</summary>
		CultureInfo culture=new CultureInfo("en-CA");
		Font headingFont=new Font(FontFamily.GenericMonospace,10,FontStyle.Bold);
		Font standardUnderline=new Font(FontFamily.GenericMonospace,10,FontStyle.Underline);
		Font standardSmall=new Font(FontFamily.GenericMonospace,8);
		DocumentGenerator doc=new DocumentGenerator();
		string msgType;
		///<summary>Used to represent a single line break (the maximum line hight for the standard font).</summary>
		float verticalLine;
		///<summary>Represents the maximum width of any character in our standard font.</summary>
		float maxCharWidth;
		///<summary>Contains the x-value for the center of the in-doc.bounds print page values.</summary>
		float center;
		///<summary>the x position where the current printing is happening.</summary>
		float x;
		///<summary>A value is assigned to text sometimes so that it can be measured or set to French. Not always needed.</summary>
		string text;
		///<summary>Used to render numbered bullet lists.</summary>
		int bullet=1;
		///<summary>Used to hold a single group of graphical primitives, so that they can be moved together in case they align with the bottom of a page.</summary>
		Pen breakLinePen=new Pen(Pens.Gray.Brush);
		///<summary>The parsed elements/fields of the etrans.MessageText string or embeded message.</summary>
		CCDFieldInputter formData;
		///<summary>Attained from field G42. Tells which form to print.</summary>
		CCDField formId;
		Etrans etrans;
		Patient patient;
		Patient subscriber;
		Patient subscriber2;
		Carrier primaryCarrier;
		Carrier secondaryCarrier;
		Claim claim;
		CanadianClaim canClaim;
		Provider provTreat;
		Provider provBill;
		InsPlan insplan;
		InsPlan insplan2;
		ClaimProc[] claimprocs;
		PatPlan[] patPlansForPatient;
		List<CanadianExtract> missingListAll;
		List<CanadianExtract> missingListDates;

		#endregion

		#region Form Controls

		private void butBack_Click(object sender,EventArgs e) {
			if(printPreviewControl1.StartPage==0)
				return;
			printPreviewControl1.StartPage--;
			labelPage.Text=(printPreviewControl1.StartPage+1).ToString()+" / "+totalPages.ToString();
		}

		private void butFwd_Click(object sender,EventArgs e) {
			if(printPreviewControl1.StartPage==totalPages-1)
				return;
			printPreviewControl1.StartPage++;
			labelPage.Text=(printPreviewControl1.StartPage+1).ToString()+" / "+totalPages.ToString();
		}

		#endregion

		#region Constructors and System Print Handlers

		///<summary>Accepts an etrans entry for Canadian claims only. After the constructor completes, only the secondary insurance db structures can be null. This constructor is used to print embedded transactions of an etrans message text. Set assigned to true in the case of assigned claims.</summary>
		protected FormCCDPrint(Etrans pEtrans,bool pEmbedded,bool pAssigned){
			etrans=pEtrans;
			embedded=pEmbedded;
			assigned=pAssigned;
			patientCopy=!pAssigned;
			Init();
		}

		///<summary>Accepts an etrans entry for Canadian claims only. After the constructor completes, only the secondary insurance db structures can be null. Set assigned to true in the case of assigned claims.</summary>
		public FormCCDPrint(Etrans pEtrans,bool pAssigned) {
			etrans=pEtrans;
			assigned=pAssigned;
			patientCopy=!pAssigned;
			Init();
		}

		///<summary>Accepts an etrans entry for Canadian claims only. After the constructor completes, only the secondary insurance db structures can be null.</summary>
		public FormCCDPrint(Etrans pEtrans){
			etrans=pEtrans;
			Init();
		}

		private void Init(){
			InitializeComponent();
			breakLinePen.Width=2;
			try {
				patient=Patients.GetPat(etrans.PatNum);
				primaryCarrier=Carriers.GetCarrier(etrans.CarrierNum);
				claim=Claims.GetClaim(etrans.ClaimNum);
				canClaim=CanadianClaims.GetForClaim(etrans.ClaimNum);
				provTreat=Providers.GetProv(claim.ProvTreat);
				provBill=Providers.GetProv(claim.ProvBill);
				insplan=InsPlans.GetPlan(claim.PlanNum,new InsPlan[0]);
				if(canClaim.SecondaryCoverage=="Y") {//Secondary coverage?
					secondaryCarrier=Carriers.GetCarrier(etrans.CarrierNum2);
					insplan2=InsPlans.GetPlan(claim.PlanNum2,new InsPlan[0]);
					subscriber2=Patients.GetPat(insplan2.Subscriber);
					if(secondaryCarrier==null || insplan2==null || subscriber2==null) {
						throw new Exception(this.ToString()+".FormCCDPrint: failed to load secondary insurance info!");
					}
				}
				ClaimProc[] claimprocall=ClaimProcs.Refresh(patient.PatNum);
				claimprocs=ClaimProcs.GetForClaim(claimprocall,claim.ClaimNum);
				patPlansForPatient=PatPlans.Refresh(claim.PatNum);
				subscriber=Patients.GetPat(insplan.Subscriber);
				if(subscriber.Language=="fr") {
					isFrench=true;
					culture=new CultureInfo("fr-CA");
				}
				missingListAll=CanadianExtracts.GetForClaim(claim.ClaimNum);
				missingListDates=CanadianExtracts.GetWithDates(missingListAll);
				//Test previously untested structures for existence, so that we do not need to check repeatedly later.
				if(primaryCarrier==null || canClaim==null || provTreat==null || provBill==null || insplan==null || claimprocs == null ||
					patPlansForPatient==null || missingListDates==null) {
					throw new Exception(this.ToString()+".FormCCDPrint: failed to load primary insurance info!");
				}
			}
			catch(Exception e) {
				Logger.openlog.LogMB(e.ToString(),Logger.Severity.ERROR);
				printable=false;
			}
		}

		///<summary>This is the function that must be called to properly print the form.</summary>
		public void Print(){
			try{
				if(etrans.MessageText==null || etrans.MessageText.Length<23) {
					throw new Exception((embedded?"Embedded":"")+"CCD message format too short: "+etrans.MessageText);
				}
				formData=new CCDFieldInputter(etrans.MessageText);//Input the fields of the given message.
				formId=formData.GetFieldById("G42");
				CCDField transactionCode=formData.GetFieldById("A04");
				msgType=((transactionCode==null || transactionCode.valuestr==null)?"00":transactionCode.valuestr);
				predetermination=(msgType=="23"||msgType=="13");//Be sure to list all predetermination response types here!
				//We are required to print 2 copies of the Dentaide form when it is not a predetermination form.
				//Everything else requires only 1 copy. Here we add the stipulation that only a patient form can print 2, copies,
				//so that the dentist form does not create 2 copies of 2 dentist forms which also create 2 copies, resulting in
				//4 total copies.
				int numCopies=((formId.valuestr=="02" && !predetermination && patientCopy)?2:1);
				while(numCopies>0){
					if(!patientCopy){//A dentist copy is to be printed.
						//We cannot simply print two copies of this form, because the dentist and patient forms are slightly different.
						FormCCDPrint patientForm=new FormCCDPrint(etrans.Copy());//Print out a patient copy seperately.
						patientForm.Print();
					}
					//Always print a patient copy, but only print dentist copies for those forms to which it applies.
					if(patientCopy || formId.valuestr=="01" || formId.valuestr=="03" || formId.valuestr=="04" || 
						formId.valuestr=="06" || formId.valuestr=="07") {
						if(printable){
							this.ShowDialog();//Trigger the actual printing process, which calls FormCCDPrint_Load.
							CCDField embeddedTransaction=formData.GetFieldById("G40");
							if(embeddedTransaction!=null){
								Etrans embTrans=etrans.Copy();
								embTrans.MessageText=embeddedTransaction.valuestr;
								FormCCDPrint embeddedForm=new FormCCDPrint(embTrans,assigned);
								embeddedForm.Print();
							}
						}
					}
					numCopies--;
				}
			}catch(Exception ex){
				Logger.openlog.Log(ex.ToString(),Logger.Severity.ERROR);
			}
		}

		///<summary>Prints the Canadian form.</summary>
		private void FormCCDPrint_Load(object sender,EventArgs e){
			try{
				pd=new PrintDocument();
				pd.PrintPage += new PrintPageEventHandler(this.pd_PrintPage);
				pd.DefaultPageSettings.Margins=new Margins(50,50,50,50);//Half-inch all around.
				//This prevents a bug caused by some printer drivers not reporting their papersize.
				//But remember that other countries use A4 paper instead of 8 1/2 x 11.
				if(pd.DefaultPageSettings.PaperSize.Height==0){
					pd.DefaultPageSettings.PaperSize=new PaperSize("default",850,1100);
				}
				pd.PrinterSettings.Duplex=Duplex.Horizontal;//Print double sided when possible, so that forms which are 1-2 pages will
																										//have any signatures on the same piece of paper as the rest of the info.
				#if DEBUG
					printPreviewControl1.Document=pd;//Setting the document causes system to call pd_PrintPage.
				#else
					pd.Print();
					DialogResult=DialogResult.OK;
				#endif
			}catch(Exception ex){
				Logger.openlog.Log(ex.ToString(),Logger.Severity.ERROR);
			}
		}

		///<summary>Called for each page to be printed.</summary>
		private void pd_PrintPage(object sender,System.Drawing.Printing.PrintPageEventArgs e){
			try{
				string code850Chars="1234567890abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ"+
						"¡¢£¥|§¨©ª«¬-®±²³´μ¶¸¹º»¼½¾¿ÀÁÂÃÄÅÆÇÈÉÊËÌÍÎÏÐÑÒÓÔÕÖØÙÚÛÜÝÞßàáâãäåæçèéêëìíîïðñòóôõöøùúûüýþÿ_";
				for(int i=0;i<code850Chars.Length;i++){
					SizeF size=e.Graphics.MeasureString(code850Chars[i].ToString(),doc.standardFont);
					verticalLine=Math.Max(verticalLine,(float)Math.Ceiling(size.Height));
					maxCharWidth=Math.Max(maxCharWidth,(float)Math.Ceiling(size.Width));
				}
				if(dirty){//Only render the document containers the first time through.
					dirty=false;
					doc.bounds=e.MarginBounds;
					center=doc.bounds.X+doc.bounds.Width/2;
					x=doc.StartElement();//Every printed page always starts on the first row and can choose to skip rows later if desired.
					CCDField responseStatus=formData.GetFieldById("G05");
					string status="";
					if(responseStatus!=null && responseStatus.valuestr!=null) {
						status=responseStatus.valuestr.ToUpper();
					}
					if(status=="R"){//Claim Rejection
						PrintClaimRejection(e.Graphics);
					}else if(status=="M" || formId.valuestr=="05"){//Paper Claim
						PrintPaperClaim(e.Graphics);
					}else{
						switch(msgType){
							case "11":
								PrintClaimAck_11(e.Graphics);
								break;
							case "21":
								PrintEOB_21(e.Graphics);
								break;
							case "18":
								PrintResponseToElegibility_18(e.Graphics);
								break;
							case "23":
								PrintPredeterminationEOB_23(e.Graphics);
								break;
							case "13":
								PrintPredeterminationAck_13(e.Graphics);
								break;
							case "24":
								PrintEmail_24(e.Graphics);
								break;
							case "15":
								PrintSummaryReconciliation_15(e.Graphics);
								break;
							case "16":
								PrintPaymentReconciliation_16(e.Graphics);
								break;
							default:
								DefaultPrint(e.Graphics);
								break;
						}
					}
					x=doc.StartElement();//Be sure to end last element always.
					totalPages=doc.CalcTotalPages(e.Graphics);
				}
				e.Graphics.DrawRectangle(Pens.LightGray,e.MarginBounds);//Draw light border for context.
				pagesPrinted++;
				doc.PrintPage(e.Graphics,pagesPrinted);
				if(printPageNumbers){
					text="Page "+pagesPrinted.ToString()+(isFrench?" de ":" of ")+totalPages;
					e.Graphics.DrawString(text,doc.standardFont,Pens.Black.Brush,
						e.MarginBounds.Right-e.Graphics.MeasureString(text,doc.standardFont).Width-4,e.MarginBounds.Top);
				}
				if(pagesPrinted<totalPages){					
					e.HasMorePages=true;
				}else{
					e.HasMorePages=false;
					labelPage.Text=(printPreviewControl1.StartPage+1).ToString()+" / "+totalPages.ToString();
				}
			}catch(Exception ex){
				Logger.openlog.LogMB(ex.ToString(),Logger.Severity.ERROR);
			}
		}

		#endregion

		#region Individual Form Printers

		///<summary>Assumes that the given form is not a rejection or a manual form.</summary>
		private void PrintClaimAck_11(Graphics g){
			switch(formId.valuestr){
				case "03"://Claim ack
					PrintClaimAck(g);
					break;
				case "04"://Employer Certified
					PrintEmployerCertified(g);
					break;
				case "02"://Dentaide
					PrintDentaide(g);
					break;
				default:
					DefaultPrint(g);
					return;
			}
		}

		///<summary>Assumes that the given form is not a rejection or a manual form.</summary>
		private void PrintEOB_21(Graphics g){
			switch(formId.valuestr) {
				case "01"://EOB
					PrintEOB(g);
					break;
				case "02"://Dentaide
					PrintDentaide(g);
					break;
				default:
					DefaultPrint(g);
					break;
			}
		}

		///<summary>Assumes that the given form is not a rejection or a manual form.</summary>
		private void PrintPredeterminationAck_13(Graphics g){
			switch(formId.valuestr){
				case "06"://Predetermination Ack
					PrintPredeterminationAck(g);
					break;
				case "02"://Dentaide
					PrintDentaide(g);
					break;
				default:
					DefaultPrint(g);
					break;
			}
		}

		///<summary>Assumes that the given form is not a rejection or a manual form.</summary>
		private void PrintPredeterminationEOB_23(Graphics g){
			switch(formId.valuestr){
				case "07"://Predetermination EOB
					PrintEOB(g);
					break;
				case "02"://Dentaide
					PrintDentaide(g);
					break;
				default:
					DefaultPrint(g);
					break;
			}
		}

		///<summary>Assumes that the given form is not a rejection or a manual form.</summary>
		private void PrintResponseToElegibility_18(Graphics g){
			switch(formId.valuestr){
				case "03"://Claim ack
					PrintClaimAck(g);
					break;
				case "04"://Employer Certified
					PrintEmployerCertified(g);
					break;
				case "02"://Dentaide
					PrintDentaide(g);
					break;
				case "08"://Eligibility form
					PrintEligibility(g);
					break;
				default:
					DefaultPrint(g);
					return;
			}
		}

		private void PrintEmail_24(Graphics g){
			text=isFrench?"RÉPONSE PAR COURRIER ÉLECTRONIQUE":"E-MAIL";
			doc.DrawString(g,text,center-g.MeasureString(text,headingFont).Width/2,0,headingFont);
			x=doc.StartElement(verticalLine);
			PrintTransactionDate(g,x,0);
			PrintOfficeNumber(g,x+250,0);
			CCDField field=formData.GetFieldById("G54");
			doc.DrawField(g,field.GetFieldName(isFrench),field.valuestr,true,x+500,0);//REFERENCE
			x=doc.StartElement(verticalLine);
			field=formData.GetFieldById("G49");
			doc.DrawField(g,field.GetFieldName(isFrench),field.valuestr,true,x,0);//TO
			x=doc.StartElement();
			field=formData.GetFieldById("G50");
			doc.DrawField(g,field.GetFieldName(isFrench),field.valuestr,true,x,0);//FROM
			x=doc.StartElement();
			field=formData.GetFieldById("G51");
			doc.DrawField(g,field.GetFieldName(isFrench),field.valuestr,true,x,0);//SUBJECT
			x=doc.StartElement(verticalLine);
			doc.HorizontalLine(g,breakLinePen,doc.bounds.Left,doc.bounds.Right,0);
			x=doc.StartElement();
			doc.DrawString(g,isFrench?"LIGNE":"LINE",x,0);
			text="MESSAGE";
			CCDField indicator=formData.GetFieldById("A11");
			if(indicator!=null && indicator.valuestr!=null && indicator.valuestr!="N"){
				text+=" - ("+(isFrench?"CE MESSAGE A ÉTÉ EN OUTRE ENVOYÉ À L'EMAIL ADDRESS CI-DESSUS"://TODO: get proper french trans.
					"THIS MESSAGE WAS ALSO SENT TO THE EMAIL ADDRESS ABOVE")+")";
			}
			float lineCol=x+55;
			doc.DrawString(g,text,lineCol,0);
			x=doc.StartElement(verticalLine);
			CCDField[] noteLines=formData.GetFieldsById("G53");//BODY OF MESSAGE
			if(noteLines!=null){
				for(int i=0;i<noteLines.Length;i++){
					x=doc.StartElement();
					doc.DrawString(g,(i+1).ToString().PadLeft(2,'0'),x,0);
					if(noteLines[i]!=null && noteLines[i].valuestr!=null) {
						doc.DrawString(g,noteLines[i].valuestr,lineCol,0,doc.standardFont);
					}
				}
			}
		}

		private void PrintSummaryReconciliation_15(Graphics g){
			text=isFrench?"RÉCONCILIATION DE RÉSUMÉ":"SUMMARY RECONCILIATION";
			doc.DrawString(g,text,center-g.MeasureString(text,headingFont).Width/2,0,headingFont);
			doc.StartElement(verticalLine);
			PrintDentalOfficeClaimReferenceNo(g,x,0);
			doc.StartElement();
			PrintOfficeNumber(g,x,0);
			doc.StartElement();
			PrintCarrierClaimNo(g,x,0);
			doc.StartElement();
			CCDField field=formData.GetFieldById("G34");//Payment reference
			if(field!=null){
				doc.DrawField(g,field.GetFieldName(isFrench),field.valuestr,true,x,0);
			}
			doc.StartElement();
			field=formData.GetFieldById("G35");//Payment date
			if(field!=null){
				doc.DrawField(g,field.GetFieldName(isFrench),DateNumToPrintDate(field.valuestr),true,x,0);
			}
			doc.StartElement();
			field=formData.GetFieldById("G36");//Payment amount
			if(field!=null && field.valuestr!=null){
				doc.DrawString(g,field.GetFieldName(isFrench)+": "+NumStrToDollars(field.valuestr),x,0,headingFont);
			}
			doc.StartElement();
			field=formData.GetFieldById("G33");//Payment adjustment amount
			if(field!=null){
				doc.DrawField(g,field.GetFieldName(isFrench),NumStrToDollars(field.valuestr),true,x,0);
			}
			doc.StartElement();
			doc.HorizontalLine(g,breakLinePen,doc.bounds.Left,doc.bounds.Right,0);
			doc.StartElement();
			CCDField[] cdaProviderNumbers=formData.GetFieldsById("B01");
			CCDField[] carrierIdentificationNumbers=formData.GetFieldsById("A05");
			CCDField[] officeSequenceNumbers=formData.GetFieldsById("A02");
			CCDField[] transactionReferenceNumbers=formData.GetFieldsById("G01");
			CCDField[] transactionPayments=formData.GetFieldsById("G38");
			float cdaProviderNumCol=x;
			float cdaProviderNumColWidth=100;
			float carrierIdentificationNumCol=cdaProviderNumCol+cdaProviderNumColWidth;
			float carrierIdentificationNumColWidth=145;
			float officeSequenceNumCol=carrierIdentificationNumCol+carrierIdentificationNumColWidth;
			float officeSequenceNumColWidth=165;
			float transactionReferenceNumCol=officeSequenceNumCol+officeSequenceNumColWidth;
			float transactionReferenceNumColWidth=150;
			float transactionPaymentCol=transactionReferenceNumCol+transactionReferenceNumColWidth;
			doc.DrawString(g,isFrench?"NO DU\nDENTISTE":"UNIQUE\nID NO",cdaProviderNumCol,0,headingFont);
			doc.DrawString(g,isFrench?"IDENTIFICATION\nDE PORTEUR":
				"CARRIER\nIDENTIFICATION",carrierIdentificationNumCol,0,headingFont);
			doc.DrawString(g,isFrench?"NO DE TRANSACTION\nDU CABINET":
				"DENTAL OFFICE\nCLAIM REFERENCE",officeSequenceNumCol,0,headingFont);
			doc.DrawString(g,isFrench?"NO DE RÉFÉRENCE\nDE TRANSACTION":
				"CARRIER CLAIM\nNUMBER",transactionReferenceNumCol,0,headingFont);
			doc.DrawString(g,isFrench?"PAIEMENT DE\nTRANSACTION":"TRANSACTION\nPAYMENT",transactionPaymentCol,0,headingFont);
			for(int i=0;i<cdaProviderNumbers.Length;i++){
				doc.StartElement();
				doc.DrawString(g,cdaProviderNumbers[i].valuestr,cdaProviderNumCol,0);
				doc.DrawString(g,carrierIdentificationNumbers[i].valuestr,carrierIdentificationNumCol,0);
				doc.DrawString(g,officeSequenceNumbers[i+1].valuestr,officeSequenceNumCol,0);
				doc.DrawString(g,transactionReferenceNumbers[i+1].valuestr,transactionReferenceNumCol,0);
				doc.DrawString(g,NumStrToDollars(transactionPayments[i].valuestr),transactionPaymentCol,0);
			}
			doc.StartElement();
			doc.HorizontalLine(g,breakLinePen,doc.bounds.Left,doc.bounds.Right,0);
			doc.StartElement();
			PrintNoteList(g);
			doc.StartElement();
			doc.HorizontalLine(g,breakLinePen,doc.bounds.Left,doc.bounds.Right,0);
			doc.StartElement();
			PrintErrorList(g);
		}
		
		private void PrintPaymentReconciliation_16(Graphics g){
			text=isFrench?"RÉCONCILIATION DE PAIEMENT":"PAYMENT RECONCILIATION";
			doc.DrawString(g,text,center-g.MeasureString(text,headingFont).Width/2,0,headingFont);
			doc.StartElement(verticalLine);
			PrintDentalOfficeClaimReferenceNo(g,x,0);
			doc.StartElement();
			CCDField field=formData.GetFieldById("B04");
			if(field!=null){
				doc.DrawField(g,field.GetFieldName(isFrench),field.valuestr,true,x,0);
			}
			doc.StartElement();
			PrintCarrierClaimNo(g,x,0);
			doc.StartElement();
			field=formData.GetFieldById("G34");//Payment reference
			if(field!=null) {
				doc.DrawField(g,field.GetFieldName(isFrench),field.valuestr,true,x,0);
			}
			doc.StartElement();
			field=formData.GetFieldById("G35");//Payment date
			if(field!=null) {
				doc.DrawField(g,field.GetFieldName(isFrench),DateNumToPrintDate(field.valuestr),true,x,0);
			}
			doc.StartElement();
			field=formData.GetFieldById("G36");//Payment amount
			if(field!=null && field.valuestr!=null) {
				doc.DrawString(g,field.GetFieldName(isFrench)+": "+NumStrToDollars(field.valuestr),x,0,headingFont);
			}
			doc.StartElement();
			field=formData.GetFieldById("G33");//Payment adjustment amount
			if(field!=null) {
				doc.DrawField(g,field.GetFieldName(isFrench),NumStrToDollars(field.valuestr),true,x,0);
			}
			doc.StartElement();
			doc.HorizontalLine(g,breakLinePen,doc.bounds.Left,doc.bounds.Right,0);
			doc.StartElement();
			CCDField[] cdaProviderNumbers=formData.GetFieldsById("B01");
			CCDField[] providerOfficeNumbers=formData.GetFieldsById("B02");
			CCDField[] billingProviderNumbers=formData.GetFieldsById("B03");
			CCDField[] carrierIdentificationNumbers=formData.GetFieldsById("A05");
			CCDField[] officeSequenceNumbers=formData.GetFieldsById("A02");
			CCDField[] transactionReferenceNumbers=formData.GetFieldsById("G01");
			CCDField[] transactionPayments=formData.GetFieldsById("G38");
			float cdaProviderNumCol=x;
			float cdaProviderNumColWidth=75;
			float providerOfficeNumCol=cdaProviderNumCol+cdaProviderNumColWidth;
			float providerOfficeNumColWidth=75;
			float billingProviderNumCol=providerOfficeNumCol+providerOfficeNumColWidth;
			float billingProviderNumColWidth=120;
			float carrierIdentificationNumCol=billingProviderNumCol+billingProviderNumColWidth;
			float carrierIdentificationNumColWidth=125;
			float officeSequenceNumCol=carrierIdentificationNumCol+carrierIdentificationNumColWidth;
			float officeSequenceNumColWidth=140;
			float transactionReferenceNumCol=officeSequenceNumCol+officeSequenceNumColWidth;
			float transactionReferenceNumColWidth=125;
			float transactionPaymentCol=transactionReferenceNumCol+transactionReferenceNumColWidth;
			Font temp=doc.standardFont;
			doc.standardFont=new Font(standardSmall.FontFamily,9,FontStyle.Bold);
			doc.DrawString(g,isFrench?"NO DU\nDENTISTE":"UNIQUE\nID NO",cdaProviderNumCol,0);
			doc.DrawString(g,isFrench?"NOMBRE\nD'OFFICE":"OFFICE\nNUMBER",providerOfficeNumCol,0);
			doc.DrawString(g,isFrench?"FOURNISSEUR\nDE FACTURATION":"BILLING\nPROVIDER",billingProviderNumCol,0);
			doc.DrawString(g,isFrench?"IDENTIFICATION\nDE PORTEUR":
				"CARRIER\nIDENTIFICATION",carrierIdentificationNumCol,0);
			doc.DrawString(g,isFrench?"NO DE TRANSACTION\nDU CABINET":
				"DENTAL OFFICE\nCLAIM REFERENCE",officeSequenceNumCol,0);
			doc.DrawString(g,isFrench?"NO DE RÉFÉRENCE\nDE TRANSACTION":
				"CARRIER CLAIM\nNUMBER",transactionReferenceNumCol,0);
			doc.DrawString(g,isFrench?"PAIEMENT DE\nTRANSACTION":"TRANSACTION\nPAYMENT",transactionPaymentCol,0);
			doc.standardFont=standardSmall;
			for(int i=0;i<cdaProviderNumbers.Length;i++){
				doc.StartElement();
				doc.DrawString(g,cdaProviderNumbers[i].valuestr,cdaProviderNumCol,0);
				doc.DrawString(g,providerOfficeNumbers[i].valuestr,providerOfficeNumCol,0);
				doc.DrawString(g,billingProviderNumbers[i].valuestr,billingProviderNumCol,0);
				doc.DrawString(g,carrierIdentificationNumbers[i].valuestr,carrierIdentificationNumCol,0);
				doc.DrawString(g,officeSequenceNumbers[i+1].valuestr,officeSequenceNumCol,0);
				doc.DrawString(g,transactionReferenceNumbers[i+1].valuestr,transactionReferenceNumCol,0);
				doc.DrawString(g,NumStrToDollars(transactionPayments[i].valuestr),transactionPaymentCol,0);
			}
			doc.standardFont=temp;
			doc.StartElement();
			doc.HorizontalLine(g,breakLinePen,doc.bounds.Left,doc.bounds.Right,0);
			doc.StartElement();
			PrintNoteList(g);
			doc.StartElement();
			doc.HorizontalLine(g,breakLinePen,doc.bounds.Left,doc.bounds.Right,0);
			doc.StartElement();
			PrintErrorList(g);
		}

		private void PrintEligibility(Graphics g){
			PrintCarrier(g);
			doc.StartElement(verticalLine);
			text=(isFrench?"Acceptabilité":"Eligibility").ToUpper();
			doc.DrawString(g,text,center-g.MeasureString(text,headingFont).Width/2,0,headingFont);
			doc.StartElement(verticalLine);
			PrintDentalOfficeClaimReferenceNo(g,x,0);
			doc.StartElement();
			PrintUniqueIdNo(g,x,0);
			doc.StartElement();
			PrintOfficeNumber(g,x,0);
			doc.StartElement();
			PrintCarrierClaimNo(g,x,0);
			doc.StartElement(verticalLine);
			PrintDisposition(g,x,0);
			doc.StartElement(verticalLine);
			doc.HorizontalLine(g,breakLinePen,doc.bounds.Left,doc.bounds.Right,0);
			doc.StartElement();
			CCDField[] displayMessages=formData.GetFieldsById("G32");
			doc.DrawString(g,"MESSAGES ("+displayMessages.Length+")",x,0,headingFont);
			doc.StartElement(verticalLine);
			for(int i=0;i<displayMessages.Length;i++){//displayMessages.Length<=250
				doc.StartElement();
				doc.DrawString(g,(i+1).ToString().PadLeft(3,'0'),x,0);
				doc.DrawString(g,displayMessages[i].valuestr,x+50,0);
			}
			doc.StartElement();
			doc.HorizontalLine(g,breakLinePen,doc.bounds.Left,doc.bounds.Right,0);
			PrintErrorList(g);
		}

		private void PrintPaperClaim(Graphics g){
			text=isFrench?"RÉCLAMATION DE PAPIER ORDINAIRE":"PLAIN PAPER CLAIM";
			doc.DrawString(g,text,center-g.MeasureString(text,headingFont).Width/2,0,headingFont);
			x=doc.StartElement(verticalLine);
			PrintTransactionDate(g,x,0);
			float rightCol=x+400;
			PrintCarrierClaimNo(g,rightCol,0);
			x=doc.StartElement();
			doc.DrawString(g,isFrench?"NOMBRE DE PRÉDÉTERMINATION:":"PREDETERMINATION NO:",rightCol,0);
			doc.StartElement();
			PrintDentistName(g,x,0);
			PrintUniqueIdNo(g,rightCol,0);
			x=doc.StartElement();
			PrintDentistAddress(g,x,0);
			PrintOfficeNumber(g,rightCol,0);
			PrintDentistPhone(g,rightCol,verticalLine);			
			x=doc.StartElement();
			PrintDentalOfficeClaimReferenceNo(g,x,0);
			doc.DrawString(g,isFrench?"VÉRIFICATION D'OFFICE":"OFFICE VERIFICATION:",rightCol,0);
			x=doc.StartElement(verticalLine);
			doc.HorizontalLine(g,breakLinePen,doc.bounds.Left,doc.bounds.Right,0);
			x=doc.StartElement();
			PrintPatientName(g,x,0);
			PrintPatientBirthday(g,rightCol,0);
			x=doc.StartElement();
			doc.DrawField(g,isFrench?"NO DE DIVISION/SECTION":"DIVISION/SECTION NO",insplan.DivisionNo,true,x,0);//Field C11
			PrintDependantNo(g,rightCol,0,true);
			doc.StartElement();
			PrintInsuredAddress(g,x,0,true);
			x=doc.StartElement(verticalLine);
			doc.HorizontalLine(g,breakLinePen,doc.bounds.Left,doc.bounds.Right,0);
			doc.StartElement();
			PrintProcedureListAck(g,GetPayableToString(canClaim.PayeeCode.ToString()));
			x=doc.StartElement();
			doc.DrawString(g,isFrench?"C'est un rapport précis des services assurés et de tous les honoraires E. payable et OE.":
				"This is an accurate statement of services performed and the total fee payable E. & OE.",x,0,standardSmall);
			x=doc.StartElement();
			doc.DrawString(g,isFrench?"AUTORISATION PATIENTE DE VERSER L'AVANTAGE SUR LE DENTISTE:":
				"PATIENT AUTHORIZATION TO PAY BENEFIT TO DENTIST:",x,0);
			x=doc.StartElement(verticalLine);
			doc.HorizontalLine(g,breakLinePen,doc.bounds.Left,doc.bounds.Right,0);
			doc.StartElement();
			float rightMidCol=400.0f;
			text=isFrench?"RENSEIGNEMENTS SUR L'ASSURANCE":"INSURANCE INFORMATION";
			doc.DrawString(g,text,x,0);
			text=isFrench?"PREMIER ASSUREUR":"PRIMARY COVERAGE";
			float leftMidCol=center-g.MeasureString(text,doc.standardFont).Width/2;
			doc.DrawString(g,text,leftMidCol,0);
			if(canClaim.SecondaryCoverage=="Y") {
				text=isFrench?"SECOND ASSUREUR":"SECONDARY COVERAGE";
				doc.DrawString(g,text,rightMidCol,0);
			}
			x=doc.StartElement(verticalLine);
			text=isFrench?"ASSUREUR/ADMINIST. RÉGIME:":"CARRIER/PLAN ADMINISTRATOR:";
			doc.DrawString(g,text,x,0);
			text=primaryCarrier.CarrierName.ToUpper();//Field A05
			doc.DrawString(g,text,leftMidCol,0);
			if(canClaim.SecondaryCoverage=="Y") {
				text=secondaryCarrier.CarrierName.ToUpper();
				doc.DrawString(g,text,rightMidCol,0);
			}
			x=doc.StartElement();
			doc.DrawString(g,isFrench?"ADRESSE DE PORTEUR:":"CARRIER ADDRESS:",x,0);
			PrintAddress(g,leftMidCol,0,primaryCarrier.Address,primaryCarrier.Address2,
				primaryCarrier.City+" "+primaryCarrier.State+" "+primaryCarrier.Zip);
			if(canClaim.SecondaryCoverage=="Y"){
				PrintAddress(g,rightMidCol,0,secondaryCarrier.Address,secondaryCarrier.Address2,
					secondaryCarrier.City+" "+secondaryCarrier.State+" "+secondaryCarrier.Zip);
			}
			x=doc.StartElement();
			text=isFrench?"NO DE POLICE:":"POLICY NO:";
			doc.DrawString(g,text,x,0);
			text=insplan.GroupNum;//Field C01
			doc.DrawString(g,text,leftMidCol,0);
			if(canClaim.SecondaryCoverage=="Y") {
				text=insplan2.GroupNum;//Field E02
				doc.DrawString(g,text,rightMidCol,0);
			}
			x=doc.StartElement();
			text=isFrench?"TITULAIRE:":"INSURED/MEMBER NAME:";
			doc.DrawString(g,text,x,0);
			text=subscriber.GetNameFLFormal();
			doc.DrawString(g,text,leftMidCol,0);
			if(canClaim.SecondaryCoverage=="Y") {
				text=subscriber2.GetNameFLFormal();
				doc.DrawString(g,text,rightMidCol,0);
			}
			x=doc.StartElement();
			doc.DrawString(g,isFrench?"DATE DE NAISSANCE:":"BIRTHDATE:",x,0);
			doc.DrawString(g,DateToString(subscriber.Birthdate,"MMM dd, yyyy"),leftMidCol,0);//Field D01
			if(subscriber2!=null) {
				doc.DrawString(g,DateToString(subscriber2.Birthdate,"MMM dd, yyyy"),rightMidCol,0);//Field E04
			}
			x=doc.StartElement();
			doc.DrawString(g,isFrench?"NO DU TITULAIRE/CERTIFICAT:":"INSURANCE/CERTIFICATE NO:",x,0);
			doc.DrawString(g,insplan.SubscriberID,leftMidCol,0);//Fields D01 and D11
			if(insplan2!=null) {
				doc.DrawString(g,insplan2.SubscriberID,rightMidCol,0);//Fields E04 and E07
			}
			x=doc.StartElement();
			doc.DrawString(g,isFrench?"EMPLOYEUR:":"EMPLOYER:",x,0);
			doc.DrawString(g,subscriber.EmployerNum==0?"":Employers.GetName(subscriber.EmployerNum),leftMidCol,0);
			if(canClaim.SecondaryCoverage=="Y"){
				doc.DrawString(g,subscriber2.EmployerNum==0?"":Employers.GetName(subscriber2.EmployerNum),rightMidCol,0);
			}
			x=doc.StartElement();
			doc.DrawString(g,isFrench?"ADRESSE:":"ADDRESS:",x,0);
			PrintSubscriberAddress(g,leftMidCol,0,true);
			PrintSubscriberAddress(g,rightMidCol,0,false);
			x=doc.StartElement();
			doc.DrawString(g,isFrench?"PARENTÉ AVEC PATIENT:":"RELATIONSHIP TO PATIENT:",x,0);
			text="";
			switch(Canadian.GetRelationshipCode(claim.PatRelat)) {//Field C03
				case "1":
					text=isFrench?"Soi-même":"Self";
					break;
				case "2":
					text=isFrench?"Époux(se)":"Spouse";
					break;
				case "3":
					text="Parent";//Same in French and English.
					break;
				case "4":
					text=isFrench?"Conjoint(e)":"Common Law Spouse";
					break;
				case "5":
					text=isFrench?"Autre":"Other";
					break;
				default:
					break;
			}
			doc.DrawString(g,text,leftMidCol,0);
			if(canClaim.SecondaryCoverage=="Y") {
				text="";
				switch(Canadian.GetRelationshipCode(claim.PatRelat2)) {//Field E06
					case "1":
						text=isFrench?"Époux(se)":"Spouse";
						break;
					case "2":
						text=isFrench?"Époux(se)":"Spouse";
						break;
					case "3":
						text="Parent";//Same in French and English.
						break;
					//"4" does not apply.
					case "5":
						text=isFrench?"Autre":"Other";
						break;
					default:
						break;
				}
				doc.DrawString(g,text,rightMidCol,0);
			}
			x=doc.StartElement(verticalLine);
			doc.HorizontalLine(g,breakLinePen,doc.bounds.Left,doc.bounds.Right,0);
			doc.DrawString(g,isFrench?"RENSEIGNEMENTS SUR LE PATIENT":"PATIENT INFORMATION",x,0);
			doc.StartElement(verticalLine);
			//bullet 1
			doc.PushX(x);
			Rectangle saveBounds=doc.bounds;
			doc.bounds=new Rectangle(doc.bounds.X,doc.bounds.Y,(int)(center-doc.bounds.Left),doc.bounds.Height);
			x+=doc.DrawString(g,"1. ",x,0).Width;
			doc.DrawString(g,isFrench?"Personne à charge:":"If dependant, indicate:",x,0);
			x+=doc.DrawString(g,isFrench?"Étudiant":"Student",x,verticalLine).Width;
			float underlineWidth=g.MeasureString("***",doc.standardFont).Width;
			x+=doc.HorizontalLine(g,Pens.Black,x,x+underlineWidth,2*verticalLine).Width;
			x+=doc.DrawString(g,isFrench?" Handicapé":" Handicapped",x,verticalLine).Width;
			doc.HorizontalLine(g,Pens.Black,x,x+underlineWidth,2*verticalLine);
			x=doc.PopX();
			//bullet 2
			doc.DrawString(g,isFrench?"2. Nom de l'institution qu'il fréquente:":"2. Name of student's school:",x,2*verticalLine);
			//bullet 3
			doc.DrawString(g,isFrench?"3. Le traitement résulte-t-il d'un accident? Oui Non":
				"3. Is treatment resulting from an\naccident? Yes No",x,4*verticalLine);
			doc.DrawString(g,isFrench?"Si Oui, donner date:":"If yes, give date:",x,6*verticalLine);
			//bullet 4
			doc.DrawString(g,isFrench?"4. S'agit-il de la première mise en bouche d'une couronne, prothèse ou pont? Oui Non":
					"4. Is this an initial placement for crown, denture or bridge? Yes No",x,8*verticalLine);
			doc.DrawString(g,isFrench?"Si le non, donnent la date du placement initial:":
				"If no, give date of initial placement:",x,11*verticalLine);
			float patientCol2=doc.bounds.Right;
			doc.bounds=saveBounds;
			//bullet 5
			SizeF size1=doc.DrawString(g,isFrench?"5. S'agit-il d'un traitement en vue de soins d'orthodontie? Oui Non":
				"5. Is treatment for orthodontic\npurposes? Yes No",patientCol2,0);
			doc.DrawString(g,isFrench?"6. Je comprends que les honoraires énumérés dans cette réclamation ne "+
				"peuvent être couverts près ou peuvent excéder mes avantages de plan. Je comprends que je "+
				"suis financièrement responsable à mon dentiste de la quantité entière de traitement. "+
				"J'autorise le dégagement de tous les information ou disques priés en ce qui concerne cette "+
				"réclamation à l'assureur/à administrateur de plan, et certifie que l'information fournie "+
				"est, corrige, et accomplis au meilleur de ma connaissance. La signature de l'assuré:":
				"6. I understand that the fees listed in this claim may not be covered by or may exceed my plan "+
				"benefits. I understand that I am financially responsible to my dentist for the entire "+
				"treatment amount. I authorize the release of any information or records requested in respect "+
				"of this claim to the insurer/plan administrator, and certify that the information given is, "+
				"correct, and complete to the best of my knowledge. Insured's Signature:",patientCol2,size1.Height);
			doc.StartElement(verticalLine);
			float yoff=0;
			yoff+=doc.HorizontalLine(g,breakLinePen,doc.bounds.Left,doc.bounds.Right,yoff).Height;
			yoff+=doc.DrawString(g,isFrench?"INSTRUCTION POUR DES COMMENTAIRES DE SUBMISSION/DENTIST:":
				"INSTRUCTION FOR SUBMISSION/DENTIST'S COMMENTS:",x,breakLinePen.Width).Height;
			yoff+=verticalLine;
			yoff+=doc.HorizontalLine(g,breakLinePen,doc.bounds.Left,doc.bounds.Right,yoff).Height;
			yoff+=doc.DrawString(g,isFrench?"CERTIFICATION DE LA POLITIQUE HOLDER/EMPLOYER:":
				"POLICY HOLDER/EMPLOYER CERTIFICATION:",x,yoff).Height;
			doc.PushX(x);
			x+=doc.DrawString(g,"1. ",x,yoff).Width;
			size1=doc.DrawString(g,isFrench?"Assurance de date débutée":"Date Coverage Commenced",x,yoff);
			float xLineEnd=center;
			yoff+=size1.Height+doc.HorizontalLine(g,Pens.Black,x+size1.Width,xLineEnd,yoff+size1.Height).Height;
			x=doc.ResetX();
			x+=doc.DrawString(g,"2. ",x,yoff).Width;
			size1=doc.DrawString(g,isFrench?"Personne à charge de date couverte":"Date Dependent Covered",x,yoff);
			yoff+=size1.Height+doc.HorizontalLine(g,Pens.Black,x+size1.Width,xLineEnd,yoff+size1.Height).Height;
			x=doc.ResetX();
			x+=doc.DrawString(g,"3. ",x,yoff).Width;
			size1=doc.DrawString(g,isFrench?"Date terminée":"Date Terminated",x,yoff);
			yoff+=verticalLine+doc.HorizontalLine(g,Pens.Black,x+size1.Width,xLineEnd,yoff+verticalLine).Height;
			size1=doc.DrawString(g,"Position",x,yoff);
			yoff+=verticalLine+doc.HorizontalLine(g,Pens.Black,x+size1.Width,xLineEnd,yoff+verticalLine).Height;
			size1=doc.DrawString(g,"Date",x,yoff);
			yoff+=verticalLine+doc.HorizontalLine(g,Pens.Black,x+size1.Width,xLineEnd,yoff+verticalLine).Height;
			x=doc.ResetX();
			x+=doc.DrawString(g,"4. ",x,yoff).Width;
			size1=doc.DrawString(g,isFrench?"La politique/contractant a autorisé la signature":
				"Policy/Contract Holder Authorized Signature",x,yoff);
			doc.HorizontalLine(g,Pens.Black,x+size1.Width,doc.bounds.Right-10,yoff+size1.Height);
			x=doc.PopX();
			doc.StartElement();
		}

		///<summary>For printing basic information about unknown/unsupported message formats (for debugging, etc.).</summary>
		private void DefaultPrint(Graphics g) {
			x=doc.StartElement(verticalLine);
			text=isFrench?"ERREUR NON SOUTENUE DE FORMAT DE MESSAGE":"UNSUPPORTED MESSAGE FORMAT ERROR";
			doc.DrawString(g,text,center-g.MeasureString(text,headingFont).Width/2,0,headingFont);
			x=doc.StartElement(verticalLine);
			CCDField[] loadedFields=formData.GetLoadedFields();
			if(loadedFields!=null){
				for(int i=0;i<loadedFields.Length;i++){
					if(loadedFields[i]!=null){
						x=doc.StartElement();
						if(loadedFields[i].fieldId!=null && loadedFields[i].fieldId.Length>0){
							text=loadedFields[i].fieldId;
							doc.DrawString(g,text,x,0);
						}
						CCDField field=loadedFields[i];
						doc.DrawField(g,field.GetFieldName(isFrench),field.valuestr,true,x+30,0);
					}
				}
			}
		}

		private void PrintClaimRejection(Graphics g){
			PrintCarrier(g);
			x=doc.StartElement(verticalLine);
			string keyword=(predetermination?"PREDETERMINATION":"CLAIM");
			text=isFrench?"REFUS D'UNE DEMANDE DE PRESTATIONS":(keyword+" REJECTION NOTICE");
			doc.DrawString(g,text,center-g.MeasureString(text,headingFont).Width/2,0,headingFont);
			x=doc.StartElement(verticalLine);
			PrintClaimAckBody(g,"",true);
			CCDField[] errors=formData.GetFieldsById("G08");
			if(errors!=null && errors.Length>0){
				doc.DrawString(g,isFrench?"ERREURS":"ERRORS",x,0);
				for(int i=0;i<errors.Length;i++){
					x=doc.StartElement();
					doc.DrawString(g,errors[i].valuestr.PadLeft(3,'0'),x,0);
					doc.DrawString(g,CCDerror.message(Convert.ToInt32(errors[i].valuestr),isFrench),x+80,0);
				}
				x=doc.StartElement(verticalLine);
				doc.HorizontalLine(g,breakLinePen,doc.bounds.Left,doc.bounds.Right,0);
			}
			x=doc.StartElement();
			text=isFrench?"VEUILLEZ CORRIGER LES ERREURS AVANT DE RESOUMETTRE LA DEMANDE.":
				"PLEASE CORRECT ERRORS AS SHOWN, PRIOR TO RE-SUBMITTING THE "+keyword+".";
			doc.DrawString(g,text,center-g.MeasureString(text,headingFont).Width/2,0,headingFont);
		}

		private void PrintDentaide(Graphics g){
			printPageNumbers=true;
			int headerHeight=(int)verticalLine;
			doc.bounds=new Rectangle(doc.bounds.X,doc.bounds.Y+headerHeight,doc.bounds.Width,
				doc.bounds.Height-headerHeight);//Reset the doc.bounds so that the page numbers are on a row alone.
			if(predetermination){
				//TODO: Required to print a second copy.
			}
			//TODO: The first page of this form may contain seven (7) procedure codes. For predeterminations, if more
			//than seven (7) procedures are to be performed, a second page must be printed containing only the
			//details of the procedure codes in excess of the first seven (7) procedures.
			x=doc.StartElement();
			text=isFrench?"FORMULAIRE DENTAIDE":"DENTAIDE FORM";
			doc.DrawString(g,text,center-g.MeasureString(text,headingFont).Width/2,0,headingFont);
			float rightCol=x+625.0f;
			x=doc.StartElement(verticalLine);
			text=DateToString(etrans.DateTimeTrans,"yyyy MM dd");
			SizeF size1=doc.DrawString(g,isFrench?"DATE DE TRANSMISSION: ":"DATE SUBMITTED: ",x,0);
			doc.DrawString(g,text,x+size1.Width,0);
			CCDField fieldG01=formData.GetFieldById("G01");
			float rightMidCol=400.0f;
			if(fieldG01!=null && fieldG01.valuestr!=null) {
				if(msgType=="23") {//Predetermination EOB
					doc.DrawField(g,isFrench?"NO DU PLAN DE TRAITEMENT":"PREDETERMINATION NO",fieldG01.valuestr,true,rightMidCol,0);
				}
				else {
					doc.DrawField(g,isFrench?"NO DE TRANSACTION DE DENTAIDE":"DENTAIDE NO",fieldG01.valuestr,true,rightMidCol,0);
				}
			}
			x=doc.StartElement();
			PrintUniqueIdNo(g,rightMidCol,0);
			x=doc.StartElement();
			PrintDentistName(g,x,0);
			PrintOfficeNumber(g,rightMidCol,0);
			x=doc.StartElement();
			PrintDentistAddress(g,x,0);
			size1=PrintDentistPhone(g,rightMidCol,0);
			//TODO: check that this is the correct patient number to be printing.
			SizeF size2=PrintDependantNo(g,rightMidCol,size1.Height,true,"PATIENT'S OFFICE ACCOUNT NO","NO DE DOSSIER DU PATIENT");
			PrintDentalOfficeClaimReferenceNo(g,rightMidCol,size1.Height+size2.Height);
			x=doc.StartElement();
			PrintPatientName(g,x,0);
			PrintPatientBirthday(g,rightMidCol,0);
			PrintPatientSex(g,rightCol,0);
			x=doc.StartElement(verticalLine);
			doc.HorizontalLine(g,breakLinePen,doc.bounds.Left,doc.bounds.Right,0);
			x=doc.StartElement();
			size1=PrintComment(g,x,0);
			if(size1.Height>0){
				x=doc.StartElement(verticalLine);
				doc.HorizontalLine(g,breakLinePen,doc.bounds.Left,doc.bounds.Right,0);
			}
			x=doc.StartElement();
			text=isFrench?"RENSEIGNEMENTS SUR L'ASSURANCE":"INSURANCE INFORMATION";
			doc.DrawString(g,text,x,0);
			text=isFrench?"PREMIER ASSUREUR":"PRIMARY COVERAGE";
			float leftMidCol=center-g.MeasureString(text,doc.standardFont).Width/2;
			doc.DrawString(g,text,leftMidCol,0);
			if(canClaim.SecondaryCoverage=="Y"){
				text=isFrench?"SECOND ASSUREUR":"SECONDARY COVERAGE";
				doc.DrawString(g,text,rightCol,0);
			}
			x=doc.StartElement(verticalLine);
			text=isFrench?"ASSUREUR/ADMINIST. RÉGIME:":"CARRIER/PLAN ADMINISTRATOR:";
			doc.DrawString(g,text,x,0);
			text=primaryCarrier.CarrierName.ToUpper();//Field A05
			doc.DrawString(g,text,leftMidCol,0);
			if(canClaim.SecondaryCoverage=="Y"){
				text=secondaryCarrier.CarrierName.ToUpper();
				doc.DrawString(g,text,rightCol,0);
			}
			x=doc.StartElement();
			text=isFrench?"NO DE POLICE:":"POLICY NO:";
			doc.DrawString(g,text,x,0);
			text=insplan.GroupNum;//Field C01
			doc.DrawString(g,text,leftMidCol,0);
			if(canClaim.SecondaryCoverage=="Y"){
				text=insplan2.GroupNum;//Field E02
				doc.DrawString(g,text,rightCol,0);
			}
			x=doc.StartElement();
			if(insplan.DivisionNo.Length>0){
				doc.DrawString(g,isFrench?"NO DE DIVISION/SECTION:":"DIV/SECTION NO:",x,0);
				doc.DrawString(g,insplan.DivisionNo,leftMidCol,0);
			}
			if(canClaim.SecondaryCoverage=="Y" && insplan2.DivisionNo.Length>0){
				doc.DrawString(g,isFrench?"NO DE DIVISION/SECTION:":"DIV/SECTION NO:",x,0);
				doc.DrawString(g,insplan2.DivisionNo,rightCol,0);
			}
			x=doc.StartElement();
			text=isFrench?"TITULAIRE:":"INSURED/MEMBER NAME:";
			doc.DrawString(g,text,x,0);
			text=subscriber.GetNameFLFormal();
			doc.DrawString(g,text,leftMidCol,0);
			if(canClaim.SecondaryCoverage=="Y"){
				text=subscriber2.GetNameFLFormal();
				doc.DrawString(g,text,rightCol,0);
			}
			x=doc.StartElement();
			doc.DrawString(g,isFrench?"ADRESSE:":"ADDRESS:",x,0);
			PrintSubscriberAddress(g,leftMidCol,0,true);
			PrintSubscriberAddress(g,rightMidCol,0,false);
			x=doc.StartElement();
			doc.DrawString(g,isFrench?"DATE DE NAISSANCE:":"BIRTHDATE:",x,0);
			doc.DrawString(g,DateToString(subscriber.Birthdate,"yyyy MM dd"),leftMidCol,0);//Field D01
			if(subscriber2!=null){
				doc.DrawString(g,DateToString(subscriber2.Birthdate,"yyyy MM dd"),rightMidCol,0);//Field E04
			}
			x=doc.StartElement();
			doc.DrawString(g,isFrench?"NO DU TITULAIRE/CERTIFICAT:":"INSURANCE/CERTIFICATE NO:",x,0);
			doc.DrawString(g,insplan.SubscriberID,leftMidCol,0);//Fields D01 and D11
			if(insplan2!=null){
				doc.DrawString(g,insplan2.SubscriberID,rightMidCol,0);//Fields E04 and E07
			}
			x=doc.StartElement();
			doc.DrawString(g,isFrench?"SÉQUENCE:":"SEQUENCE:",x,0);
			doc.DrawString(g,insplan.DentaideCardSequence.ToString().PadLeft(2,'0'),leftMidCol,0);
			if(insplan2!=null){
				doc.DrawString(g,insplan2.DentaideCardSequence.ToString().PadLeft(2,'0'),rightMidCol,0);
			}
			x=doc.StartElement();
			doc.DrawString(g,isFrench?"PARENTÉ AVEC PATIENT:":"RELATIONSHIP TO PATIENT:",x,0);
			text="";
			switch(Canadian.GetRelationshipCode(claim.PatRelat)){//Field C03
				case "1":
					text=isFrench?"Soi-même":"Self";
					break;
				case "2":
					text=isFrench?"Époux(se)":"Spouse";
					break;
				case "3":
					text="Parent";//Same in French and English.
					break;
				case "4":
					text=isFrench?"Conjoint(e)":"Common Law Spouse";
					break;
				case "5":
					text=isFrench?"Autre":"Other";
					break;
				default:
					break;
			}
			doc.DrawString(g,text,leftMidCol,0);
			if(canClaim.SecondaryCoverage=="Y"){
				text="";
				switch(Canadian.GetRelationshipCode(claim.PatRelat2)){//Field E06
					case "1":
						text=isFrench?"Époux(se)":"Spouse";
						break;
					case "2":
						text=isFrench?"Époux(se)":"Spouse";
						break;
					case "3":
						text="Parent";//Same in French and English.
						break;
					//"4" does not apply.
					case "5":
						text=isFrench?"Autre":"Other";
						break;
					default:
						break;
				}
				doc.DrawString(g,text,rightMidCol,0);
			}
			//Spaces don't show up with underline, so we will have to underline manually.
			float underlineWidth=g.MeasureString("***",doc.standardFont).Width;
			bool dependant=true;
			string isStudent="   ";
			string isHandicapped="   ";
			bool stud=false;
			if(Canadian.GetRelationshipCode(claim.PatRelat)=="3"){
				text="";//Used for school name.
				switch(canClaim.EligibilityCode){//Field C09
					case 1://Patient is a full-time student.
						isStudent=isFrench?"Oui":"Yes";
						stud=true;
						text=patient.SchoolName;
						break;
					case 2://Patient is disabled.
						isHandicapped=isFrench?"Oui":"Yes";
						break;
					case 3://Patient is a disabled student.
						isStudent=isFrench?"Oui":"Yes";
						stud=true;
						text=patient.SchoolName;
						isHandicapped=isFrench?"Oui":"Yes";
						break;
					default://Not applicable
						dependant=false;
						break;
				}
			}else if(canClaim.EligibilityCode==2){
				isHandicapped=isFrench?"Oui":"Yes";
			}else if(canClaim.EligibilityCode==3){
				isStudent=isFrench?"Oui":"Yes";
				isHandicapped=isFrench?"Oui":"Yes";
				stud=true;
			}else{
				dependant=false;//Does not apply.
			}
			if(dependant){
				x=doc.StartElement(verticalLine);
				doc.PushX(x);
				x+=doc.DrawString(g,isFrench?"HANDICAPÉ":"HANDICAPPED",x,0).Width;
				float isHandicappedHeight=doc.DrawString(g,isHandicapped,x,0).Height;
				doc.HorizontalLine(g,Pens.Black,x,x+underlineWidth,isHandicappedHeight);
				x+=underlineWidth;
				x+=doc.DrawString(g,isFrench?" ÉTUDIANT À PLEIN TEMPS":" FULL-TIME STUDENT",x,0).Width;
				float isStudentHeight=doc.DrawString(g,isStudent,x,0).Height;
				doc.HorizontalLine(g,Pens.Black,x,x+underlineWidth,isStudentHeight);
				x+=underlineWidth;
				if(stud){
					x=doc.StartElement();
					text=canClaim.SchoolName;
					doc.DrawField(g,isFrench?"NOM DE L'INSTITUTION":"NAME OF STUDENT'S SCHOOL",text,stud,x,0);
				}
				x=doc.StartElement(verticalLine);
				x=doc.PopX();
			}
			x=doc.StartElement();
			doc.HorizontalLine(g,breakLinePen,doc.bounds.Left,doc.bounds.Right,0);
			x=doc.StartElement();
			doc.DrawString(g,isFrench?"RENSEIGNEMENTS SUR LES SOINS":"TREATMENT INFORMATION",x,0);
			x=doc.StartElement(verticalLine);
			bullet=1;
			PrintAccidentBullet(g,isFrench?"Le traitement résulte-t-il d'un accident?":"Is treatment resulting from an accident?");
			x=doc.StartElement();
			if(canClaim.IsInitialLower!="X" || canClaim.IsInitialUpper!="X"){//Don't show this bullet if it does not apply.
				x+=doc.DrawString(g,bullet.ToString()+". ",x,0).Width;
				bullet++;
				doc.PushX(x);//Begin indentation.
				doc.DrawString(g,isFrench?"S'agit-il de la première mise en bouche d'une couronne, prothèse ou pont?":
					"Is this an initial placement for crown, denture or bridge?",x,0);
				x=doc.StartElement();
				if(canClaim.IsInitialUpper!="X"){//Field F15 - Avoid invalid upper initial placement data.
					doc.DrawString(g,isFrench?"Maxillaire: ":"Maxillary: ",x,0);
					x+=120;
					doc.PushX(x);//Begin second indentation.
					if(canClaim.IsInitialUpper=="N"){
						doc.DrawString(g,isFrench?"Non":"No",x,0);
						x=doc.StartElement();
						text=GetMaterialDescription(canClaim.MaxProsthMaterial);//Field F20
						doc.DrawField(g,isFrench?"Matériau":"Material",text,true,x,0);
						x=doc.StartElement();
						text=DateToString(canClaim.DateInitialUpper,"yyyy mm dd");//Field F04
						doc.DrawField(g,"Date",text,true,x,0);
					}else{
						doc.DrawString(g,isFrench?"Oui":"Yes",x,0);
					}
					x=doc.PopX();//End second indentation.
				}
				x=doc.StartElement();
				if(canClaim.IsInitialLower!="X"){//Field F18 - Avoid invalid lower initial placement data.
					doc.DrawString(g,isFrench?"Mandibule: ":"Mandibular: ",x,0);
					x+=120;
					doc.PushX(x);//Begin second indentation.
					if(canClaim.IsInitialLower=="N"){
						doc.DrawString(g,isFrench?"Non":"No",x,0);
						x=doc.StartElement();
						text=GetMaterialDescription(canClaim.MandProsthMaterial);//Field F21
						doc.DrawField(g,isFrench?"Matériau":"Material",text,true,x,0);
						x=doc.StartElement();
						text=DateToString(canClaim.DateInitialLower,"yyyy mm dd");//Field F19
						doc.DrawField(g,"Date",text,true,x,0);
					}else{
						doc.DrawString(g,isFrench?"Oui":"Yes",x,0);
					}
					x=doc.PopX();//End second indentation.
				}
				x=doc.StartElement();
				PrintMissingToothList(g);
				x=doc.PopX();//End first indentation.			
			}
			x=doc.StartElement();
			size1=doc.DrawString(g,bullet.ToString()+". "+(isFrench?"S'agit-il d'un traitement en vue de soins d'orthodontie?":
				"Is treatment for orthodontic purposes? "),x,0);
			bullet++;
			if(claim.IsOrtho){//Field F05
				doc.DrawString(g,isFrench?"Oui":"Yes",x+size1.Width,0);
			}else{
				doc.DrawString(g,isFrench?"Non":"No",x+size1.Width,0);
			}
			x=doc.StartElement();
			if(predetermination){
				x+=doc.DrawString(g,bullet.ToString()+". ",x,0).Width;
				bullet++;
				doc.PushX(x);//Begin indentation.
				doc.DrawString(g,isFrench?"S'il s'agit d'un plan de traitement d'orthodontie, indiquer":
					"For orthodontic treatment plan, please indicate:",x,0);
				x=doc.StartElement();
				text="";//TODO: Fill this with the data corresponding to field F30.
				doc.DrawField(g,isFrench?"Durée du traitement":"Duration of treatment",text,false,x,0);
				//TODO: finish the rest of this bullet right here.
				x=doc.StartElement(verticalLine);
				x=doc.PopX();//End indentation.
			}
			x=doc.StartElement();
			doc.HorizontalLine(g,breakLinePen,doc.bounds.Left,doc.bounds.Right,0);
			x=doc.StartElement();
			doc.DrawString(g,isFrench?"Validation du titulaire/employeur":"Policy holder / employer certification",x,0);
			x=doc.StartElement(verticalLine);
			underlineWidth=g.MeasureString("**************",doc.standardFont).Width;
			size1=doc.DrawString(g,isFrench?"1. Entrée en vigueur de couverture:":"1. Date coverage commenced:",x,0);
			doc.HorizontalLine(g,Pens.Black,x+size1.Width,x+size1.Width+underlineWidth,size1.Height);
			x=doc.StartElement();
			size1=doc.DrawString(g,isFrench?"2. Entrée en vigueur de personne à charge:":"2. Date dependant covered:",x,0);
			doc.HorizontalLine(g,Pens.Black,x+size1.Width,x+size1.Width+underlineWidth,size1.Height);
			x=doc.StartElement();
			size1=doc.DrawString(g,isFrench?"3. Terminaison:":"3. Date terminated:",x,0);
			doc.HorizontalLine(g,Pens.Black,x+size1.Width,x+size1.Width+underlineWidth,size1.Height);
			x=doc.StartElement(verticalLine);
			doc.PushX(x);
			x+=doc.DrawString(g,isFrench?"Signature de personne autorisée:":"Authorized signature:",x,0).Width;
			x+=doc.HorizontalLine(g,Pens.Black,x,x+150,verticalLine).Width+10;
			x+=doc.DrawString(g,isFrench?"Fonction:":"Position:",x,0).Width;
			x+=doc.HorizontalLine(g,Pens.Black,x,x+100,verticalLine).Width+10;
			x=doc.StartElement();
			x+=doc.DrawString(g,"Date:",x,0).Width;
			doc.HorizontalLine(g,Pens.Black,x,x+150,verticalLine);
			x=doc.PopX();
			x=doc.StartElement(verticalLine);
			doc.HorizontalLine(g,breakLinePen,doc.bounds.Left,doc.bounds.Right,0);
			x=doc.StartElement();
			float yoff=0;
			yoff+=doc.DrawString(g,isFrench?"Signature du patient et du dentiste":"Patient's and Dentist's signature",x,yoff).Height;
			yoff+=verticalLine;
			yoff+=doc.DrawString(g,isFrench?"Je déclare qu’à ma connaissance les renseignements donnés sont "+
				"véridiques, exacts et complets. J’autorise la divulgation à l’assureur, ou au Centre "+
				"Dentaide, ou à leurs mandataires de tout renseignement ou dossier relatif à cette "+
				"demande de prestations. Je conviens de rebourser à l’assureur toute somme "+
				"débourséeindûment à mon égard. Je m;engage à verser au dentiste la portion non "+
				"assurée des frais et honoraires demandés pour les soins décrits ci-après.":
				"I certify that to my knowledge this information is truthful, accurate and complete. "+
				"I authorize the disclosure to the insurer, or Centre Dentaide, or their agents of any "+
				"information or file related to this claim. I agree to repay the insurer for any sum "+
				"paid on my behalf, and to pay the dentist the required fees for the uninsured portion "+
				"of the treatment described hereinafter.",x,yoff).Height;
			yoff+=2*verticalLine;
			yoff+=doc.HorizontalLine(g,Pens.Black,x,x+400,yoff).Height;
			yoff+=doc.DrawString(g,isFrench?"Signature du patient (ou parent/tuteur)":
				"Signature of patient (or parent/guardian)",x,yoff).Height;
			yoff+=verticalLine;
			yoff+=doc.DrawString(g,isFrench?"La présente constitue une description exacte des soins exécutés "+
				"et des honoraires demandés ou, dans le cas d'un plan de traitement, des traitements "+
				"devant être exécutés et des honoraires s'y rapportant, sauf erreur ou omission.":
				"The above and the treatments described below are an accurate statement of services "+
				"performed and fees charged, or of services to be performed and fees to be charged in "+
				"the case of a treatment plan, except errors and omissions.",x,yoff).Height;
			yoff+=verticalLine*2;
			yoff+=doc.HorizontalLine(g,Pens.Black,x,x+400,yoff).Height;
			yoff+=doc.DrawString(g,isFrench?"Signature du dentiste":"Dentist's signature",x,yoff).Height;
			yoff+=verticalLine;
			yoff+=doc.HorizontalLine(g,breakLinePen,doc.bounds.Left,doc.bounds.Right,yoff).Height;
			x=doc.StartElement();
			size1=doc.DrawString(g,isFrench?"Date du traitement: ":"Date of Service: ",x,0);
			text=DateToString(etrans.DateTimeTrans,"yyyy MM dd");
			doc.DrawString(g,text,x+size1.Width,0);
			x=doc.StartElement(verticalLine);
			bool isEOB=msgType=="21" || msgType=="23";
			CCDField[] noteNumbers=formData.GetFieldsById("G45");//Used to looking up note reference numbers.
			CCDField[] noteTexts=formData.GetFieldsById("G26");
			//The rest of the CCDField[] object should all be the same length, since they come bundled together as part 
			//of each procedure.
			CCDField[] procedureLineNumbers=formData.GetFieldsById("F07");
			CCDField[] eligibleAmounts=formData.GetFieldsById("G12");
			CCDField[] eligibleLabAmounts=formData.GetFieldsById("G43");
			CCDField[] deductibleAmounts=formData.GetFieldsById("G13");
			CCDField[] eligiblePercentages=formData.GetFieldsById("G14");
			CCDField[] dentaidePayAmounts=formData.GetFieldsById("G15");
			CCDField[] explainationNoteNumbers1=formData.GetFieldsById("G16");
			CCDField[] explainationNoteNumbers2=formData.GetFieldsById("G17");
			float noteCol=x;
			float noteColWidth=(isEOB?65:0);
			float procedureCol=noteCol+noteColWidth;
			float procedureColWidth=40;
			float toothCol=procedureCol+procedureColWidth;
			float toothColWidth=45;
			float surfaceCol=toothCol+toothColWidth;
			float surfaceColWidth=55;
			float feeCol=surfaceCol+surfaceColWidth;
			float feeColWidth=75;
			float eligibleFeeCol=feeCol+feeColWidth;
			float eligibleFeeColWidth=(isEOB?75:0);
			float labCol=eligibleFeeCol+eligibleFeeColWidth;
			float labColWidth=75;
			float eligibleLabCol=labCol+labColWidth;
			float eligibleLabColWidth=(isEOB?75:0);
			float deductibleCol=eligibleLabCol+eligibleLabColWidth;
			float deductibleColWidth=(isEOB?75:0);
			float percentCoveredCol=deductibleCol+deductibleColWidth;
			float percentCoveredColWidth=(isEOB?90:0);
			float dentaidePaysCol=percentCoveredCol+percentCoveredColWidth;
			float dentaidePaysColWidth=(isEOB?65:0);
			float endNoteCol=dentaidePaysCol+dentaidePaysColWidth;
			Font tempFont=doc.standardFont;
			doc.standardFont=standardSmall;
			if(isEOB){
				doc.DrawString(g,"Note",noteCol,0);
				doc.DrawString(g,isFrench?"Admissible":"Eligible",eligibleFeeCol,0);
				doc.DrawString(g,isFrench?"Admissible":"Eligible",eligibleLabCol,0);
				doc.DrawString(g,isFrench?"Franchise":"Deductible",deductibleCol,0);
				doc.DrawString(g,isFrench?"%Couvertrem.":"Percent\nCovered",percentCoveredCol,0);
				doc.DrawString(g,isFrench?"Dentaide":"Detaide\nPays",dentaidePaysCol,0);
			}
			doc.DrawString(g,isFrench?"Acte":"Proc",procedureCol,0);
			doc.DrawString(g,isFrench?"Dent":"Tooth",toothCol,0);
			doc.DrawString(g,"Surface",surfaceCol,0);
			doc.DrawString(g,isFrench?"Honoraires":"Fee",feeCol,0);
			doc.DrawString(g,isFrench?"Labo.":"Lab",labCol,0);
			double totalFee=0;
			double totalLab=0;
			double totalPaid=0;
			for(int i=0;i<=7;i++){
				x=doc.StartElement();
				if(isEOB){
					if(procedureLineNumbers!=null){
						for(int j=0;j<procedureLineNumbers.Length;j++){
							if(Convert.ToInt32(procedureLineNumbers[j].valuestr)==i){
								//For any i!=0, there will only be one matching carrier procedure, by definition.
								size1=new SizeF(0,0);
								int noteIndex=Convert.ToInt32(explainationNoteNumbers1[j].valuestr);
								if(noteIndex>0){
									size1=doc.DrawString(g,noteNumbers[noteIndex].valuestr,noteCol,0);
								}
								noteIndex=Convert.ToInt32(explainationNoteNumbers2[j].valuestr);
								if(noteIndex>0){
									doc.DrawString(g," "+noteNumbers[noteIndex].valuestr,noteCol+size1.Width,0);
								}
								text=eligibleAmounts[j].valuestr;
								text=text.Substring(0,3).TrimStart('0')+text.Substring(3,1)+"."+text.Substring(4,2);
								doc.DrawString(g,text,eligibleFeeCol,0);
								text=eligibleLabAmounts[j].valuestr;
								text=text.Substring(0,3).TrimStart('0')+text.Substring(3,1)+"."+text.Substring(4,2);
								doc.DrawString(g,text,eligibleLabCol,0);
								text=deductibleAmounts[j].valuestr;
								text=text.Substring(0,2).TrimStart('0')+text.Substring(2,1)+"."+text.Substring(3,2);
								doc.DrawString(g,text,deductibleCol,0);
								text=eligiblePercentages[j].valuestr.TrimStart('0')+"%";
								doc.DrawString(g,text,percentCoveredCol,0);
								text=dentaidePayAmounts[j].valuestr;
								text=text.Substring(0,3).TrimStart('0')+text.Substring(3,1)+"."+text.Substring(4,2);
								doc.DrawString(g,text,dentaidePaysCol,0);
								totalPaid+=Convert.ToDouble(text);
							}
						}
					}
				}
				//The following code assumes that procedures and associated labs were sent out in the
				//same order that they were returned from the query.
				//Print info for procedure i if it exists.
				for(int k=0,n=0;k<claimprocs.Length;k++){
					Procedure proc;
					ClaimProc claimproc=claimprocs[k];
					if(claimproc.ProcNum!=0){//Is this a valid claim procedure?
						proc=Procedures.GetOneProc(claimproc.ProcNum,true);
						if(proc.ProcNumLab==0){//We are only looking for non-lab procedures.
							n++;
							if(n==i){//Procedure found.
								text=claimproc.CodeSent.PadLeft(5,'0');//Field F08
								doc.DrawString(g,text,procedureCol,0);
								text=Tooth.ToInternat(proc.ToothNum);//Field F10
								doc.DrawString(g,text,toothCol,0);
								text=Tooth.SurfTidy(proc.Surf,proc.ToothNum,true);//Field F11
								doc.DrawString(g,text,surfaceCol,0);
								text=proc.ProcFee.ToString("F");//Field F12
								doc.DrawString(g,text,feeCol,0);
								totalFee+=proc.ProcFee;
								//Find the lab fee associated with the above procedure.
								for(int m=0;m<claimprocs.Length;m++) {
									ClaimProc claimlab=claimprocs[m];
									if(claimlab.ProcNum!=0){//Is this a valid claim procedure/lab fee?
										Procedure lab=Procedures.GetOneProc(claimlab.ProcNum,true);
										if(lab.ProcNumLab==proc.ProcNum){//This lab fee is for the above procedure.
											text=lab.ProcFee.ToString("F");//Field F13
											doc.DrawString(g,text,labCol,0);
											totalLab+=lab.ProcFee;
										}
									}
								}
							}
						}
					}
				}
			}
			doc.standardFont=new Font(doc.standardFont.FontFamily,doc.standardFont.Size+1,FontStyle.Bold);
			x=doc.StartElement(verticalLine);
			doc.DrawString(g,"TOTAL:",toothCol,0);
			if(isEOB){
				doc.DrawString(g,totalPaid.ToString("F"),dentaidePaysCol,0);
			}
			doc.DrawString(g,totalFee.ToString("F"),feeCol,0);
			doc.DrawString(g,totalLab.ToString("F"),labCol,0);
			doc.standardFont=tempFont;
			if(isEOB){
				doc.StartElement();
				doc.HorizontalLine(g,breakLinePen,doc.bounds.Left,doc.bounds.Right,0);
				doc.DrawString(g,"NOTE",x,0);
				doc.StartElement(verticalLine);
				for(int j=0;j<noteNumbers.Length;j++){
					doc.StartElement();
					doc.DrawString(g,noteNumbers[j].valuestr,noteCol,0);
					doc.DrawString(g,noteTexts[j].valuestr,100,0);
				}
			}
		}

		///<summary>Contains different header and footer based on wether or not this is a patient copy.</summary>
		private void PrintClaimAck(Graphics g){
			PrintCarrier(g);
			x=doc.StartElement(verticalLine);
			if(patientCopy){
				text=isFrench?"ACCUSÉ DE RÉCEPTION D'UN DEMANDE DE PRESTATIONS - COPIE DU PATIENT":
											"CLAIM ACKNOWLEDGEMENT - PATIENT COPY";
			}else{
				text=isFrench?"ACCUSÉ DE RÉCEPTION D'UN DEMANDE DE PRESTATIONS - COPIE DE DENTISTE":
										"CLAIM ACKNOWLEDGEMENT - DENTIST COPY";
			}
			doc.DrawString(g,text,center-g.MeasureString(text,headingFont).Width/2,0,headingFont);
			x=doc.StartElement(verticalLine);
			text=isFrench?
				"Nous avons utilisé les renseignements du présent formulaire pour traiter votre demande par"+
				"ordinateur. Veuillez en vérifier l'exactitude et aviser votre dentiste en cas d'erreur. "+
				"Prière de ne pas poster à l'assureur/administrateur du régime.":
				"The information contained on this form has been used to process your claim electronically. "+
				"Please verify the accuracy of this data and report any discrepancies to your dental office. "+
				"Do not mail this form to the insurer/plan administrator.";
			PrintClaimAckBody(g,text,false);
			if(isFrench){
				text="La présente demande de prestations a été transmise par ordinateur.".ToUpper();
				doc.DrawString(g,text,center-g.MeasureString(text,headingFont).Width/2,0,headingFont);
				x=doc.StartElement();
				text="Elle sert de reçu seulement.".ToUpper();
			}else{
				text="THIS CLAIM HAS BEEN SUBMITTED ELECTRONICALLY - THIS IS A RECEIPT ONLY";
			}
			doc.DrawString(g,text,center-g.MeasureString(text,headingFont).Width/2,0,headingFont);
		}

		private void PrintPredeterminationAck(Graphics g){
			PrintCarrier(g);
			x=doc.StartElement(verticalLine);
			if(patientCopy){
				text=isFrench?"RECONNAISSANCE DE PRÉDÉTERMINATION - COPIE DU PATIENT":
											"PREDETERMINATION ACKNOWLEDGMENT - PATIENT COPY";
			}else{
				text=isFrench?"RECONNAISSANCE DE PRÉDÉTERMINATION - COPIE DE DENTISTE":
											"PREDETERMINATION ACKNOWLEDGMENT - DENTIST COPY";
			}
			doc.DrawString(g,text,center-g.MeasureString(text,headingFont).Width/2,0,headingFont);
			x=doc.StartElement(verticalLine);
			text=isFrench?
				"Nous avons utilisé les renseignements du présent formulaire pour traiter votre demande par"+
				"ordinateur. Veuillez en vérifier l'exactitude et aviser votre dentiste en cas d'erreur. "+
				"Prière de ne pas poster à l'assureur/administrateur du régime.":
				"The information contained on this form has been used to process your claim electronically. "+
				"Please verify the accuracy of this data and report any discrepancies to your dental office. "+
				"Do not mail this form to the insurer/plan administrator.";
			PrintClaimAckBody(g,text,false);
			if(isFrench){
				text="La présente demande de prestations a été transmise par ordinateur.".ToUpper();
				doc.DrawString(g,text,center-g.MeasureString(text,headingFont).Width/2,0,headingFont);
				x=doc.StartElement();
				text="Elle sert de reçu seulement.".ToUpper();
			}else{
				text="THIS PREDETERMINATION HAS BEEN SUBMITTED ELECTRONICALLY - THIS IS A RECEIPT ONLY";
			}
			doc.DrawString(g,text,center-g.MeasureString(text,headingFont).Width/2,0,headingFont);
		}

		private void PrintEmployerCertified(Graphics g){
			PrintCarrier(g);
			x=doc.StartElement(verticalLine);
			if(patientCopy){
				text=isFrench?"VALIDATION PAR L'EMPLOYEUR/ADMINISTRATEUR DU RÉGIME - COPIE DU PATIENT":
					"EMPLOYER/PLAN ADMINISTRATOR CERTIFIED FORM - PATIENT COPY";
			}else{
				text=isFrench?"VALIDATION PAR L'EMPLOYEUR/ADMINISTRATEUR DU RÉGIME - COPIE DE DENTISTE":
					"EMPLOYER/PLAN ADMINISTRATOR CERTIFIED FORM - DENTIST COPY";
			}
			doc.DrawString(g,text,center-g.MeasureString(text,headingFont).Width/2,0,headingFont);
			x=doc.StartElement(verticalLine);
			text=isFrench?
				"Nous avons utilisé les renseignements du présent formulaire pour traiter votre demande par"+
				"ordinateur. Veuillez en vérifier l'exactitude et aviser votre dentiste en cas d'erreur.":
				"The information contained on this form has been used to process your claim electronically. "+
				"Please verify the accuracy of this data and report any discrepancies to your dental office.";
			PrintClaimAckBody(g,text,false);
			doc.DrawString(g,isFrench?"VALIDATION DU TITULAIRE/EMPLOYEUR":"POLICYHOLDER/EMPLOYER - CERTIFICATION",x,0);
			x=doc.StartElement(verticalLine);
			SizeF size=doc.DrawString(g,isFrench?"EMPLOYEUR: ":"EMPLOYER: ",x,0);
			doc.HorizontalLine(g,Pens.Black,x+size.Width,doc.bounds.Right,size.Height);
			x=doc.StartElement(verticalLine/2);
			size=doc.DrawString(g,isFrench?"ENTRÉE EN VIGUEUR DE COUVERTURE: ":"DATE COVERAGE COMMENCED: ",x,0);
			doc.HorizontalLine(g,Pens.Black,x+size.Width,doc.bounds.Right,size.Height);
			x=doc.StartElement(verticalLine/2);
			size=doc.DrawString(g,isFrench?"ENTRÉE EN VIGUEUR DE COUVERTURE DE PERSONNE À CHARGE: ":"DATE DEPENDANT COVERED: ",x,0);
			doc.HorizontalLine(g,Pens.Black,x+size.Width,doc.bounds.Right,size.Height);
			x=doc.StartElement(verticalLine/2);
			size=doc.DrawString(g,isFrench?"DATE DE TERMINAISON: ":"DATE TERMINATED: ",x,0);
			doc.HorizontalLine(g,Pens.Black,x+size.Width,doc.bounds.Right,size.Height);
			x=doc.StartElement(verticalLine/2);
			size=doc.DrawString(g,isFrench?"SIGNATURE DE PERSONNE AUTORISÉE: ":"SIGNATURE OF AUTHORIZED OFFICIAL: ",x,0);
			doc.HorizontalLine(g,Pens.Black,x+size.Width,doc.bounds.Right,size.Height);
			x=doc.StartElement(verticalLine/2);
			size=doc.DrawString(g,isFrench?"DATE D'AUTORISATION: ":"AUTHORIZATION DATE: ",x,0);
			doc.HorizontalLine(g,Pens.Black,x+size.Width,doc.bounds.Right,size.Height);
			x=doc.StartElement(verticalLine);
			text=isFrench?"LA PRÉSENTE DEMANDE A ÉTÉ TRANSMISE PAR ORDINATEUR À:":"THIS CLAIM HAS BEEN SUBMITTED ELECTRONICALLY TO:";
			doc.DrawString(g,text,center-g.MeasureString(text,headingFont).Width/2,0,headingFont);
			x=doc.StartElement();
			PrintCarrier(g);
			x=doc.StartElement();
			text=isFrench?"VEUILLEZ LA FAIRE VALIDER PAR VOTRE. EMPLOYEUR":"PLEASE TAKE THIS FORM TO YOUR EMPLOYER FOR CERTIFICATION";
			doc.DrawString(g,text,center-g.MeasureString(text,headingFont).Width/2,0,headingFont);
		}

		///<summary>Does the actual work for printing claims. When graphicObjects is null, returns the required graphicObjects after calculating them. In normal use, this function should be called twice for each time the form is rendered, once to calculate the graphical primitives, then once more to actually render to form for printing.</summary>
		private void PrintClaimAckBody(Graphics g,string centralDisclaimer,bool rejection){
			PrintTransactionDate(g,x,0);
			PrintCarrierClaimNo(g,x+400,0);
			x=doc.StartElement(verticalLine);
			PrintDisposition(g,x,0);
			x=doc.StartElement(verticalLine);
			PrintDentistName(g,x,0);
			PrintUniqueIdNo(g,x+400,0);
			x=doc.StartElement();
			PrintDentistPhone(g,x,0);
			PrintOfficeNumber(g,x+400,0);
			x=doc.StartElement();
			PrintDentalOfficeClaimReferenceNo(g,x,0);
			x=doc.StartElement(verticalLine);
			doc.HorizontalLine(g,breakLinePen,doc.bounds.Left,doc.bounds.Right,0);
			x=doc.StartElement();
			PrintPatientName(g,x,0);
			PrintPatientBirthday(g,x+400,0);
			x=doc.StartElement();
			PrintPolicyNo(g,x,0,true);
			PrintDivisionSectionNo(g,x+400,0);
			x=doc.StartElement();
			PrintCertificateNo(g,x,0,true);
			PrintCardNo(g,x+250,0);
			PrintDependantNo(g,x+400,0,true);
			x=doc.StartElement(verticalLine);
			PrintInsuredMember(g,x,0);
			x=doc.StartElement();
			PrintInsuredAddress(g,x,0,true);
			x=doc.StartElement(verticalLine);
			doc.HorizontalLine(g,breakLinePen,doc.bounds.Left,doc.bounds.Right,0);
			x=doc.StartElement();
			//Field F01 - Not visible in predetermination
			PrintProcedureListAck(g,predetermination?"":GetPayableToString(canClaim.PayeeCode.ToString()));
			if(!rejection){
				x=doc.StartElement(verticalLine);
				doc.DrawString(g,centralDisclaimer,x,0);
				x=doc.StartElement(verticalLine);
				doc.HorizontalLine(g,breakLinePen,doc.bounds.Left,doc.bounds.Right,0);
				x=doc.StartElement();
				doc.DrawString(g,isFrench?"RENSEIGNEMENTS SUR LE PATIENT":"PATIENT INFORMATION",x,0);
				x=doc.StartElement(verticalLine);
				bullet=1;
				PrintDependencyBullet(g);
				x=doc.StartElement();
				PrintSecondaryPolicyBullet(g);
				x=doc.StartElement();
				PrintAccidentBullet(g);
				x=doc.StartElement();
				PrintInitialPlacementBullet(g);
				x=doc.StartElement();
				PrintToothExtractionBullet(g);
			}
			x=doc.StartElement(verticalLine);
			doc.HorizontalLine(g,breakLinePen,doc.bounds.Left,doc.bounds.Right,0);
			x=doc.StartElement();
		}

		///<summary>Prints the list of procedures performed for the patient on the document in question.</summary>
		private void PrintProcedureListAck(Graphics g,string payableToStr) {
			float procedureCodeCol=x;
			float procedureDescriptionCol=procedureCodeCol+100;
			float procedureToothCol=procedureDescriptionCol+350;
			float procedureSurfaceCol=procedureToothCol+40;
			float procedureDateCol=procedureSurfaceCol+60;
			float procedureDateColWidth=predetermination?0:100;
			float procedureChargeCol=procedureDateCol+procedureDateColWidth;
			x=doc.StartElement();
			doc.DrawString(g,isFrench?"ACTE":"PROCEDURE",procedureCodeCol,0);
			doc.DrawString(g,"DESCRIPTION",procedureDescriptionCol,0);//Same in both languages.
			doc.DrawString(g,isFrench?"D#":"TH#",procedureToothCol,0);
			doc.DrawString(g,"SURF",procedureSurfaceCol,0);//Same in both languages.
			if(!predetermination){
				doc.DrawString(g,"DATE",procedureDateCol,0);//Same in both languages.
			}
			doc.DrawString(g,isFrench?"HONORAIRES":"CHARGE",procedureChargeCol,0);
			x=doc.StartElement();
			//TODO: Ensure that the ordering of the procedures meets the Canadian standard.
			Procedure proc;
			for(int i=0;i<this.claimprocs.Length;i++){
				ClaimProc claimproc=claimprocs[i];
				if(claimproc.ProcNum!=0) {//Is this a valid procedure?
					proc=Procedures.GetOneProc(claimproc.ProcNum,true);
					text=claimproc.CodeSent.PadLeft(5,'0');//Field F08 - TODO check padding needed
					doc.DrawString(g,text,procedureCodeCol,0);
					text=ProcedureCodes.GetProcCode(proc.CodeNum).Descript;
					//TODO: clip description when too long.
					doc.DrawString(g,text,procedureDescriptionCol,0);
					text=Tooth.ToInternat(proc.ToothNum);//Field F10
					doc.DrawString(g,text,procedureToothCol,0);
					text=Tooth.SurfTidy(proc.Surf,proc.ToothNum,true);//Field F11
					doc.DrawString(g,text,procedureSurfaceCol,0);
					if(!predetermination) {//Used to remove service dates in a predetermination ack.
						text=proc.ProcDate.ToShortDateString();//Field F09
						doc.DrawString(g,text,procedureDateCol,0);
					}
					text=proc.ProcFee.ToString("F");//Field F12
					doc.DrawString(g,text,procedureChargeCol,0);
					x=doc.StartElement();
				}
			}
			x=doc.StartElement(verticalLine);
			doc.DrawField(g,isFrench?"DESTINATAIRE DU PAIEMENT":"BENEFIT AMOUNT PAYABLE TO",payableToStr,false,x,0);
			text=isFrench?"TOTAL DEMANDÉ ":"TOTAL SUBMITTED ";
			doc.DrawString(g,text,procedureChargeCol-g.MeasureString(text,doc.standardFont).Width,0);
			text=claim.ClaimFee.ToString("F");
			doc.DrawString(g,text,procedureChargeCol,0);
		}

		///<summary>Prints the EOB header. Left in its own function, since the header is expected to be printed on each respective page of output.</summary>
		private void PrintEOBHeader(Graphics g){
			PrintCarrier(g);
			x=doc.StartElement(verticalLine);
			if(predetermination){
				if(patientCopy) {
					text=isFrench?"DÉTAIL DES PRESTATIONS D'UN PLAN DE TRAITEMENT - COPIE DU PATIENT":
						"PREDETERMINATION EXPLANATION OF BENEFITS - PATIENT COPY";
				}else{
					text=isFrench?"DÉTAIL DES PRESTATIONS D'UN PLAN DE TRAITEMENT - COPIE DE DENTISTE":
						"PREDETERMINATION EXPLANATION OF BENEFITS - DENTIST COPY";
				}
			}else{
				if(patientCopy){
					text=isFrench?"DÉTAIL DES PRESTATIONS - COPIE DU PATIENT":"EXPLANATION OF BENEFITS - PATIENT COPY";
				}else{
					text=isFrench?"DÉTAIL DES PRESTATIONS - COPIE DE DENTISTE":"EXPLANATION OF BENEFITS - DENTIST COPY";
				}
			}
			doc.DrawString(g,text,center-g.MeasureString(text,headingFont).Width/2,0,headingFont);
			x=doc.StartElement(verticalLine);
			float rightCol=450;
			PrintVertificationNo(g,x+rightCol,0);
			x=doc.StartElement();
			PrintDentistName(g,x,0);
			PrintUniqueIdNo(g,x+rightCol,0);
			x=doc.StartElement();
			PrintDentalOfficeClaimReferenceNo(g,x,0);
			PrintOfficeNumber(g,x+rightCol,0);
			x=doc.StartElement();
			PrintPolicyNo(g,x,0,true);
			PrintDivisionSectionNo(g,x+rightCol,0);
			x=doc.StartElement();
			PrintCertificateNo(g,x,0,true);
			float midCol=270;
			PrintCardNo(g,x+midCol,0);
			PrintDependantNo(g,x+rightCol,0,true);
			x=doc.StartElement();
			PrintInsuredMember(g,x,0);
			PrintSubscriberBirthday(g,x+rightCol,0,true);
			x=doc.StartElement();
			PrintPatientName(g,x,0);
			PrintPatientBirthday(g,x+rightCol,0);
			x=doc.StartElement();
			PrintRelationshipToSubscriber(g,x,0,true);
			x=doc.StartElement(verticalLine);
			doc.HorizontalLine(g,breakLinePen,doc.bounds.Left,doc.bounds.Right,0);
		}

		private void PrintEOB(Graphics g){
			PrintEOBHeader(g);
			x=doc.StartElement();
			PrintCarrierClaimNo(g,x,0);
			PrintTransactionDate(g,x+450,0);
			x=doc.StartElement(verticalLine);
			PrintProcedureListEOB(g,GetPayableToString(canClaim.PayeeCode.ToString()));
			x=doc.StartElement(verticalLine);
			doc.HorizontalLine(g,breakLinePen,doc.bounds.Left,doc.bounds.Right,0);
			if(!predetermination){
				x=doc.StartElement();
				bullet=1;
				PrintDependencyBullet(g);
				x=doc.StartElement();
				PrintSecondaryPolicyBullet(g);
				x=doc.StartElement();
				PrintAccidentBullet(g);
				x=doc.StartElement();
				PrintInitialPlacementBullet(g);
				x=doc.StartElement();
				PrintToothExtractionBullet(g);
				x=doc.StartElement(verticalLine);
				doc.HorizontalLine(g,breakLinePen,doc.bounds.Left,doc.bounds.Right,0);
			}
			x=doc.StartElement();
			CCDField[] notes=formData.GetFieldsById("G26");//Get all G26s (will match the number in field G11).
			if(notes.Length>0){
				doc.DrawString(g,"Notes:",x,0,headingFont);
				x=doc.StartElement();
				for(int i=0;i<notes.Length;i++){
					doc.DrawString(g,i.ToString().PadLeft(2,'0')+notes[i].valuestr,x,0);
					x=doc.StartElement();
					//TODO: reprint header on each printed page where the notes overflow.
				}
				x=doc.StartElement(verticalLine);
				doc.HorizontalLine(g,breakLinePen,doc.bounds.Left,doc.bounds.Right,0);
				x=doc.StartElement();
			}
			if(isFrench){
				text="La présente nous a été transmise par ordinateur par votre dentiste.";
				doc.DrawString(g,text,center-g.MeasureString(text,headingFont).Width/2,0,headingFont);
				x=doc.StartElement();
				text="Veuillez la conserver.";
				doc.DrawString(g,text,center-g.MeasureString(text,headingFont).Width/2,0,headingFont);
				x=doc.StartElement();
				text="Pour tout renseignement, veuillez vous adresser à "+
					(predetermination?"votre assureur.":"l'assureur/administrateur du régime.");
				doc.DrawString(g,text,center-g.MeasureString(text,headingFont).Width/2,0,headingFont);
				x=doc.StartElement();
				text="Les honoraires non remboursables sont déductibles de l'impôt.";
				doc.DrawString(g,text,center-g.MeasureString(text,headingFont).Width/2,0,headingFont);
				x=doc.StartElement();
			}else{
				text="This "+(predetermination?"Predetermination":"Claim")+
					" Has Been Submitted Electronically on Your Behalf By Your Dentist.";
				doc.DrawString(g,text,center-g.MeasureString(text,headingFont).Width/2,0,headingFont);
				x=doc.StartElement();
				text="Please Direct Any Inquiries to Your Insurer/Plan Administrator.";
				doc.DrawString(g,text,center-g.MeasureString(text,headingFont).Width/2,0,headingFont);
				x=doc.StartElement();
				text="Expenses Not Payable May be Considered for Income Tax Purposes.";
				doc.DrawString(g,text,center-g.MeasureString(text,headingFont).Width/2,0,headingFont);
				x=doc.StartElement();
				text="Please Retain Copy.";
				doc.DrawString(g,text,center-g.MeasureString(text,headingFont).Width/2,0,headingFont);
				x=doc.StartElement();
			}
		}

		private void PrintProcedureListEOB(Graphics g,string payableToStr) {
			Font tempFont=doc.standardFont;
			doc.standardFont=standardSmall;
			float procedureCodeCol=x;
			float procedureDescriptionCol=procedureCodeCol+50;
			float procedureToothCol=procedureDescriptionCol+200;
			float procedureDateCol=procedureToothCol+40;
			float procedureDateColWidth=predetermination?0:90;
			float procedureChargeCol=procedureDateCol+procedureDateColWidth;
			float procedureEligibleCol=procedureChargeCol+75;
			float procedureDeductCol=procedureEligibleCol+50;
			float procedureAtCol=procedureDeductCol+50;
			float procedureBenefitCol=procedureAtCol+50;
			float procedureNotesCol=procedureBenefitCol+70;
			x=doc.StartElement();
			doc.DrawString(g,isFrench?"ACTE":"PROC",procedureCodeCol,0);
			doc.DrawString(g,"DESCRIPTION",procedureDescriptionCol,0);//Same in both languages.
			doc.DrawString(g,isFrench?"D#":"TH#",procedureToothCol,0);
			if(!predetermination) {
				doc.DrawString(g,"DATE",procedureDateCol,0);//Same in both languages.
			}
			doc.DrawString(g,isFrench?"HONS":"CHARGE",procedureChargeCol,0);
			doc.DrawString(g,isFrench?"ADMIS":"ELIG",procedureEligibleCol,0);
			doc.DrawString(g,isFrench?"FRANCH":"DEDUCT",procedureDeductCol,0);
			doc.DrawString(g,isFrench?"%":"AT",procedureAtCol,0);
			doc.DrawString(g,isFrench?"PREST":"BENEFIT",procedureBenefitCol,0);
			doc.DrawString(g,"NOTES",procedureNotesCol,0);
			//TODO: Finish implementing when procedure number (different than code) is available.
			Procedure proc;
			for(int i=0;i<this.claimprocs.Length;i++) {
				ClaimProc claimproc=claimprocs[i];
				if(claimproc.ProcNum!=0) {//Is this a valid procedure?
					x=doc.StartElement();
					proc=Procedures.GetOneProc(claimproc.ProcNum,true);
					text=claimproc.CodeSent.PadLeft(5,'0');//Field F08 - TODO check padding needed
					doc.DrawString(g,text,procedureCodeCol,0);
					text=ProcedureCodes.GetProcCode(proc.CodeNum).Descript;
					//TODO: clip description when too long.
					doc.DrawString(g,text,procedureDescriptionCol,0);
					text=Tooth.ToInternat(proc.ToothNum);//Field F10
					doc.DrawString(g,text,procedureToothCol,0);
					if(!predetermination) {//Used to remove service dates in a predetermination ack.
						text=proc.ProcDate.ToShortDateString();//Field F09
						doc.DrawString(g,text,procedureDateCol,0);
					}
					text=proc.ProcFee.ToString("F");//Field F12
					doc.DrawString(g,text,procedureChargeCol,0);
					//TODO: add remaining fields.
				}
			}
			if(predetermination){
			}else{
			}
			/*doc.DrawField(g,"BENEFIT AMOUNT PAYABLE TO","DESTINATAIRE DU PAIEMENT",payableToStr,false,x,y);
			doc.DrawString(g,isFrench?"TOTAL DEMANDÉ":"TOTAL SUBMITTED",x+490,y);
			text=claim.ClaimFee.ToString("F");
			doc.DrawString(g,text,procedureChargeCol,y);*/
			doc.standardFont=tempFont;
		}

		#endregion

		#region Printing Helper Functions

		///<summary>Prints carrier name centered on current form row followed by a space.</summary>
		private void PrintCarrier(Graphics g){
			text=primaryCarrier.CarrierName.ToUpper();//Field A05
			doc.DrawString(g,text,center-g.MeasureString(text,headingFont).Width/2,0,headingFont);
		}

		///<summary>For EOBs only.</summary>
		private SizeF PrintVertificationNo(Graphics g,float X,float Y){
			CCDField vertificationNo=formData.GetFieldById("G30");//Present in EOBs.
			return doc.DrawField(g,vertificationNo.GetFieldName(isFrench),vertificationNo.valuestr,false,X,Y);
		}

		private SizeF PrintTransactionDate(Graphics g,float X,float Y){
			text=IsValidDate(etrans.DateTimeTrans)?etrans.DateTimeTrans.ToString("MMM dd, yyyy",culture):"";
			return doc.DrawField(g,"DATE",text,true,X,Y);//Only print reasonable transaction dates.
		}

		private SizeF PrintCarrierClaimNo(Graphics g,float X,float Y){
			CCDField[] carrierClaimNos=formData.GetFieldsById("G01");
			if(carrierClaimNos==null || carrierClaimNos.Length==0){
				throw new Exception("Field G01 does not exist in transaction, cannot print carrier claim number.");
			}
			return doc.DrawField(g,carrierClaimNos[0].GetFieldName(isFrench),carrierClaimNos[0].valuestr,false,X,Y);
		}

		private SizeF PrintDisposition(Graphics g,float X,float Y) {
			CCDField disposition=formData.GetFieldById("G07");
			return doc.DrawField(g,disposition.GetFieldName(isFrench),disposition.valuestr,false,X,Y);
		}

		private SizeF PrintComment(Graphics g,float X,float Y) {
			CCDField comment=formData.GetFieldById("G07");
			return doc.DrawField(g,isFrench?"COMMENTAIRES":"COMMENT",comment.valuestr,false,X,Y);
		}

		private SizeF PrintDentistName(Graphics g,float X,float Y) {
			//Treatment provider should match that retrieved from the CDA provider number in field B01.
			text=provTreat.LName+", "+provTreat.FName+" "+provTreat.MI+" "+provTreat.Suffix;
			return doc.DrawField(g,isFrench?"DENTISTE":"DENTIST",text,false,X,Y);
		}

		private SizeF PrintDentistPhone(Graphics g,float X,float Y){
			text=PrefB.GetString("PracticePhone");
			if(text.Length==10) {//May need to format for nice appearance.
				text=text.Substring(0,3)+"-"+text.Substring(3,3)+"-"+text.Substring(6,4);
			}
			return doc.DrawField(g,isFrench?"NO DE TÉLÉPHONE":"TELEPHONE",text,false,X,Y);
		}

		private SizeF PrintDentistAddress(Graphics g,float X,float Y){
			SizeF size1=doc.DrawString(g,isFrench?"ADRESSE: ":"ADDRESS: ",X,Y);
			SizeF size2=PrintAddress(g,X+size1.Width,Y,PrefB.GetString("PracticeAddress"),PrefB.GetString("PracticeAddress2"),
				PrefB.GetString("PracticeCity")+", "+PrefB.GetString("PracticeST")+" "+PrefB.GetString("PracticeZip"));
			return new SizeF(size1.Width+size2.Width,Math.Max(size1.Height,size2.Height));
		}

		private SizeF PrintUniqueIdNo(Graphics g,float X,float Y) {
			CCDField uniqueIdNo=formData.GetFieldById("B01");
			return doc.DrawField(g,uniqueIdNo.GetFieldName(isFrench),uniqueIdNo.valuestr,false,X,Y);
		}

		private SizeF PrintOfficeNumber(Graphics g,float X,float Y) {
			CCDField cdaOfficeNumber=formData.GetFieldById("B02");
			return doc.DrawField(g,cdaOfficeNumber.GetFieldName(isFrench),cdaOfficeNumber.valuestr,false,X,Y);
		}

		private SizeF PrintDentalOfficeClaimReferenceNo(Graphics g,float X,float Y) {
			CCDField[] dentalOfficeClaimReferenceNos=formData.GetFieldsById("A02");
			if(dentalOfficeClaimReferenceNos==null || dentalOfficeClaimReferenceNos.Length==0){
				throw new Exception("There are no instances of field A02 to read, cannot print dental office claim reference number.");
			}
			return doc.DrawField(g,dentalOfficeClaimReferenceNos[0].GetFieldName(isFrench),
				dentalOfficeClaimReferenceNos[0].valuestr,false,X,Y);
		}

		private SizeF PrintPatientName(Graphics g,float X,float Y) {
			return doc.DrawField(g,"PATIENT",patient.GetNameFLFormal(),true,X,Y);//Fields C06,C07,C08
		}

		private SizeF PrintPatientBirthday(Graphics g,float X,float Y) {
			text=IsValidDate(patient.Birthdate)?patient.Birthdate.ToString("MMM d, yyyy",culture):"";
			return doc.DrawField(g,isFrench?"DATE DE NAISSANCE":"BIRTHDATE",text,true,X,Y);//Field C05
		}

		private SizeF PrintPatientSex(Graphics g,float X,float Y){
			switch(patient.Gender){
				case PatientGender.Male:
					text="M";
					break;
				default:
					text="F";
					break;
			}
			return doc.DrawField(g,isFrench?"SEXE":"SEX",text,true,X,Y);
		}

		private SizeF PrintPolicyNo(Graphics g,float X,float Y,bool primary){
			text="";
			if(primary){
				text=insplan.GroupNum;//Field C01
			}else if(canClaim.SecondaryCoverage=="Y"){
				text=insplan2.GroupNum;//Field E02
			}
			return doc.DrawField(g,isFrench?"NO DE POLICE":"POLICY NO",text,true,X,Y);
		}

		private SizeF PrintDivisionSectionNo(Graphics g,float X,float Y){
			return doc.DrawField(g,isFrench?"NO DE DIVISION/SECTION":"DIVISION/SECTION NO",insplan.DivisionNo,false,X,Y);//Field C11
		}

		private SizeF PrintCertificateNo(Graphics g,float X,float Y,bool primary) {
			if(primary){
				text=insplan.SubscriberID;//Field C02
			}else if(canClaim.SecondaryCoverage=="Y"){
				text=insplan2.SubscriberID;//Field E03
			}
			return doc.DrawField(g,isFrench?"NO DE CERTIFICAT":"CERTIFICATE NO",text,true,X,Y);
			//TODO: For NIHB claims where SubscriberID=="", print the Band (Field C13) and Family (Field C14) numbers. (primary only?)
		}

		///<summary>Print "sequence" number.</summary>
		private SizeF PrintCardNo(Graphics g,float X,float Y) {
			text=(insplan.DentaideCardSequence.ToString());//Field D11
			return doc.DrawField(g,isFrench?"NO DE CARTE":"CARD NO",text=="0"?"":text,false,X,Y);
		}

		private SizeF PrintDependantNo(Graphics g,float X,float Y,bool primary) {
			return PrintDependantNo(g,X,Y,primary,"DEPENDANT NO","NO DE PERSONNE À CHARGE");
		}

		private SizeF PrintDependantNo(Graphics g,float X,float Y,bool primary,string fieldText,string frenchFieldText){
			text="";
			if(primary) {
				text=PatPlans.GetPatID(patPlansForPatient,claim.PlanNum);//Field C17
			}
			else {
				if(canClaim.SecondaryCoverage=="Y") {
					text=PatPlans.GetPatID(patPlansForPatient,claim.PlanNum2);//Field E17
				}
				else if(canClaim.SecondaryCoverage=="X") {
					text=isFrench?"INCONNU":"UNKNOWN";
				}
			}
			return doc.DrawField(g,isFrench?frenchFieldText:fieldText,text,true,X,Y);//Field C17
		}

		private SizeF PrintInsuredMember(Graphics g,float X,float Y){
			text=subscriber.GetNameFLFormal();
			return doc.DrawField(g,isFrench?"TITULAIRE":"INSURED/MEMBER",text,true,X,Y);//Fields D02,D03,D04
		}

		private SizeF PrintSubscriberAddress(Graphics g,float X,float Y,bool primary) {
			string line1="";
			string line2="";
			string line3="";
			Patient sub=primary?subscriber:subscriber2;
			if(sub!=null){
				//Primary: Fields D05,D06,D07,D08,D09
				//Secondary: Fields E11,E12,E13,E14,E15
				line1=sub.Address;
				line2=sub.Address2;
				line3=sub.City+", "+sub.State+" "+sub.Zip;
			}
			return PrintAddress(g,X,Y,line1,line2,line3);
		}

		private SizeF PrintInsuredAddress(Graphics g,float X,float Y,bool primary) {
			SizeF size1=doc.DrawString(g,isFrench?"ADRESSE: ":"ADDRESS: ",X,Y);
			SizeF size2=PrintSubscriberAddress(g,X+size1.Width,Y,primary);
			return new SizeF(size1.Width+size2.Width,Math.Max(size1.Height,size2.Height));
		}

		private SizeF PrintRelationshipToSubscriber(Graphics g,float X,float Y,bool useCaps) {
			text=GetPatientRelationshipString(claim.PatRelat);//Field C03
			string engStr="Relationship to subscriber";
			string frStr="Parenté avec titulaire";
			string label=isFrench?frStr:engStr;
			return doc.DrawField(g,useCaps?label.ToUpper():label.ToLower(),text,true,X,Y);
		}

		private SizeF PrintSubscriberBirthday(Graphics g,float X,float Y,bool useCaps) {
			text=IsValidDate(subscriber.Birthdate)?subscriber.Birthdate.ToString("MMM d, yyyy",culture):"";
			string engStr="BIRTHDATE";
			string frStr="DATE DE NAISSANCE";
			string label=isFrench?frStr:engStr;
			return doc.DrawField(g,useCaps?label.ToUpper():label.ToLower(),text,true,X,Y);
		}

		///<summary>Prints a three-line address. Each line is underlined and the address is printed without a label.</summary>
		private SizeF PrintAddress(Graphics g,float X,float Y,string line1,string line2,string line3) {
			string address=line1+"\n"+line2+"\n"+line3;
			float lineWidth=Math.Max(150.0f,g.MeasureString(address,doc.standardFont).Width);
			float yoff=0;
			doc.DrawString(g,line1,X,Y+yoff,doc.standardFont);
			yoff+=verticalLine;
			yoff+=doc.HorizontalLine(g,Pens.Black,X,X+lineWidth,yoff).Height;
			doc.DrawString(g,line2,X,Y+yoff,doc.standardFont);
			yoff+=verticalLine;
			yoff+=doc.HorizontalLine(g,Pens.Black,X,X+lineWidth,yoff).Height;
			doc.DrawString(g,line3,X,Y+yoff,doc.standardFont);
			yoff+=verticalLine;
			yoff+=doc.HorizontalLine(g,Pens.Black,X,X+lineWidth,yoff).Height;
			return new SizeF(lineWidth,yoff);
		}

		private bool PrintDependencies(Graphics g,bool fillOut){
			string isStudent="   ";
			string isHandicapped="   ";
			bool stud=false;
			text="";//Used for school name.
			switch(canClaim.EligibilityCode) {//Field C09
				case 1://Patient is a full-time student.
					isStudent=isFrench?"Oui":"Yes";
					stud=true;
					text=patient.SchoolName;
					break;
				case 2://Patient is disabled.
					isHandicapped=isFrench?"Oui":"Yes";
					break;
				case 3://Patient is a disabled student.
					isStudent=isFrench?"Oui":"Yes";
					stud=true;
					text=patient.SchoolName;
					isHandicapped=isFrench?"Oui":"Yes";
					break;
				default:
					return false;//This bullet is not applicable
			}
			if(!fillOut){
				isStudent="   ";
				isHandicapped="   ";
			}
			x+=doc.DrawString(g,isFrench?"Personne à charge: Étudiant":"If dependant, indicate: Student",x,0).Width;
			float isStudentHeight=doc.DrawString(g,isStudent,x,0).Height;
			//Spaces don't show up with underline, so we will have to underline manually.
			float underlineWidth=g.MeasureString("***",doc.standardFont).Width;
			doc.HorizontalLine(g,Pens.Black,x,x+underlineWidth,isStudentHeight);
			x+=underlineWidth;
			x+=doc.DrawString(g,isFrench?" Handicapé":" Handicapped",x,0).Width;
			float isHandicappedHeight=doc.DrawString(g,isHandicapped,x,0).Height;
			doc.HorizontalLine(g,Pens.Black,x,x+underlineWidth,isHandicappedHeight);
			return stud;
		}

		private void PrintDependencyBullet(Graphics g) {
			x+=doc.DrawString(g,bullet.ToString()+". ",x,0).Width;
			bullet++;
			doc.PushX(x);//Save indentation x-value for this list number.
			PrintRelationshipToSubscriber(g,x,0,false);
			PrintSubscriberBirthday(g,x+450,0,false);
			x=doc.StartElement();
			bool stud=PrintDependencies(g,true);
			if(stud){
				x=doc.StartElement();
				text=canClaim.SchoolName;
				doc.DrawField(g,isFrench?"Étudiant: Nom de l'institution qu'il fréquente":
					"If Student, Name of student's school",text,stud,x,0);
			}
			x=doc.PopX();//End indentation.
		}

		private void PrintSecondaryPolicyBullet(Graphics g){
			x+=doc.DrawString(g,bullet.ToString()+". ",x,0).Width;
			bullet++;
			doc.PushX(x);//Save indentation spot for this bullet point.
			text=isFrench?	"A-t-il droit à des prestations ou services dans un autre régime de soins dentaires,"+
											"régime collectif ou gouvernemental? ":
											"Are any Dental Benefits or services provided under any other group insurance or "+
											"dental plan, WCB or government plan? ";
			if(canClaim.SecondaryCoverage!="N"){//Field E18
				doc.DrawString(g,text+(isFrench?"Oui":"Yes"),x,0);
				x=doc.StartElement();
				PrintPolicyNo(g,x,0,false);
				PrintDependantNo(g,x+400,0,false);
				x=doc.StartElement();
				text="";
				if(canClaim.SecondaryCoverage=="Y"){
					text=IsValidDate(subscriber2.Birthdate)?subscriber2.Birthdate.ToString("MMM d, yyyy"):"";//Field E04
				}
				doc.DrawField(g,isFrench?"Date de naissance du titulaire":"Insured/Member Date of Birth",text,true,x,0);
				PrintCertificateNo(g,x+400,0,false);
				x=doc.StartElement();
				text="";
				if(canClaim.SecondaryCoverage=="Y"){//Field E01
					text=secondaryCarrier.CarrierName;
				}
				doc.DrawField(g,isFrench?"Nom de l'assureur/administrateur":"Name of Insurer/Plan Administrator",text,true,x,0);
				x=doc.StartElement();
				text="";
				if(canClaim.SecondaryCoverage=="Y"){
					text=GetPatientRelationshipString(claim.PatRelat2);//Field E06
				}
				doc.DrawField(g,isFrench?"Parenté avec patient":"Relationship to Patient",text,true,x,0);
			}else{
				doc.DrawString(g,text+(isFrench?"Non":"No"),x,0);
			}
			x=doc.PopX();//End indentation.
		}

		private void PrintAccidentBullet(Graphics g){
			PrintAccidentBullet(g,isFrench?"Y a-t-il un traitement requis par suite d'un accident?":
				"Is any treatment required as the result of an accident?");
		}

		private void PrintAccidentBullet(Graphics g,string questionStr){
			x+=doc.DrawString(g,bullet.ToString()+". ",x,0).Width;
			bullet++;
			doc.PushX(x);//Begin indentation.
			x+=doc.DrawString(g,questionStr+" ",x,0).Width;
			if(!IsValidDate(claim.AccidentDate)) {//Field F02 - No accident claimed.
				doc.DrawString(g,isFrench?"Non":"No",x,0);
			}
			else {
				doc.DrawString(g,isFrench?"Oui":"Yes",x,0);
				x=doc.StartElement();
				x+=doc.DrawField(g,isFrench?"Si Oui, donner date":"If yes, give date",
					claim.AccidentDate.ToString("MMM d, yyyy",culture)+" ",true,x,0).Width;
				doc.DrawString(g,isFrench?"et détails à part:":"and details separately:",x,0);
				x=doc.StartElement();
				doc.DrawString(g,claim.ClaimNote,x,0);
			}
			x=doc.PopX();//End indentation.
		}

		private void PrintInitialPlacementBullet(Graphics g){
			if(canClaim.IsInitialLower=="X" && canClaim.IsInitialUpper=="X") {//Don't show this bullet if it does not apply.
				return;
			}
			x+=doc.DrawString(g,bullet.ToString()+". ",x,0).Width;
			bullet++;
			doc.PushX(x);//Begin indentation.
			doc.DrawString(g,isFrench?"Prothèse, couronne ou pont: est-ce la première mise en bouche?":
				"If Denture, crown or bridge, Is this the initial placement?",x,0);
			x=doc.StartElement();
			if(canClaim.IsInitialUpper!="X") {//Field F15 - Avoid invalid upper initial placement data.
				doc.DrawString(g,isFrench?"Maxillaire: ":"Upper: ",x,0);
				x+=120;
				doc.PushX(x);//Begin second indentation.
				if(canClaim.IsInitialUpper=="N"){
					doc.DrawString(g,isFrench?"Non":"No",x,0);
					x=doc.StartElement();
					text=GetMaterialDescription(canClaim.MaxProsthMaterial);//Field F20
					doc.DrawField(g,isFrench?"Matériau initial":"Initial Material",text,true,x,0);
					x=doc.StartElement();
					text=canClaim.DateInitialUpper.ToString("yyyy mm dd",culture);//Field F04
					doc.DrawField(g,isFrench?"Date de mise en bouche":"Placement Date",
						IsValidDate(canClaim.DateInitialUpper)?text:"",true,x,0);
					x=doc.StartElement();
					text="";//Remove later
					//text=GetProcedureTypeDescription();//TODO: Field F16
					doc.DrawField(g,isFrench?"Motif du remplacement":"Reason for replacement",text,true,x,0);
				}
				else {
					doc.DrawString(g,isFrench?"Oui":"Yes",x,0);
				}
				x=doc.PopX();//End second indentation.
			}
			x=doc.StartElement();
			if(canClaim.IsInitialLower!="X"){//Field F18 - Avoid invalid lower initial placement data.
				doc.DrawString(g,isFrench?"Mandibule: ":"Lower: ",x,0);
				x+=120;
				doc.PushX(x);//Begin second indentation.
				if(canClaim.IsInitialLower=="N"){
					doc.DrawString(g,isFrench?"Non":"No",x,0);
					x=doc.StartElement();
					text=GetMaterialDescription(canClaim.MandProsthMaterial);//Field F21
					doc.DrawField(g,isFrench?"Matériau initial":"Initial Material",text,true,x,0);
					x=doc.StartElement();
					text=canClaim.DateInitialLower.ToString("yyyy mm dd",culture);//Field F19
					doc.DrawField(g,isFrench?"Date de mise en bouche":"Placement Date",
						IsValidDate(canClaim.DateInitialLower)?text:"",true,x,0);
					x=doc.StartElement();
					text="";//Remove later
					//text=GetProcedureTypeDescription();//TODO: Field F16
					doc.DrawField(g,isFrench?"Motif du remplacement":"Reason for replacement",text,true,x,0);
				}
				else {
					doc.DrawString(g,isFrench?"Oui":"Yes",x,0);
				}
				x=doc.PopX();//End second indentation.
			}
			x=doc.PopX();//End first indentation.
		}

		private void PrintToothExtractionBullet(Graphics g) {
			x+=doc.DrawString(g,bullet.ToString()+". ",x,0).Width;
			bullet++;
			doc.PushX(x);//Begin indentation.
			x+=doc.DrawString(g,isFrench?"S'agit-il d'un traitement en vue de soins d'orthodontie? ":
				"Is any treatment provided for orthodontic purposes? ",x,0).Width;
			if(claim.IsOrtho){//Field F05
				doc.DrawString(g,isFrench?"Oui":"Yes",x,0);
				x=doc.StartElement();
				PrintMissingToothList(g);
			}
			else {
				doc.DrawString(g,isFrench?"Non":"No",x,0);
			}
			x=doc.PopX();//End indentation.
		}

		private void PrintMissingToothList(Graphics g){
			if(canClaim.IsInitialUpper=="Y" || canClaim.IsInitialLower=="Y"){//Only print extractions when F15 or F18 are "Yes"
				string title=(isFrench?"D#":"TH")+"  DATE(YYYYMMDD)\t";
				float titleWidth=g.MeasureString(title,doc.standardFont).Width;
				int cycleOrthoDateCount=(int)((doc.bounds.Right-x)/titleWidth);
				for(int i=0;i<Math.Min(cycleOrthoDateCount,missingListDates.Count);i++) {
					x+=doc.DrawString(g,title,x,0).Width;
				}
				int j=0;
				for(int i=0;i<missingListDates.Count;i++) {//Count specified by field F22
					if(j%cycleOrthoDateCount==0) {
						x=doc.StartElement();
					}
					if(IsValidDate(missingListDates[i].DateExtraction)){//Tooth is considered unextracted if it doesn't have a date.
						float thWidth=doc.DrawString(g,Tooth.ToInternat(missingListDates[i].ToothNum).PadLeft(2,' ')+" ",x,0).Width;//Field F23
						text=" "+missingListDates[i].DateExtraction.ToString("yyyy MM dd",culture);
						doc.DrawString(g,text,x+thWidth,0);
						x+=titleWidth;
						j++;
					}
				}
			}
		}

		private void PrintNoteList(Graphics g) {
			CCDField[] noteTexts=formData.GetFieldsById("G26");
			doc.DrawString(g,"NOTES ("+noteTexts.Length.ToString()+")",x,0,headingFont);
			doc.StartElement(verticalLine);
			for(int i=0;i<noteTexts.Length;i++) {//noteTexts.Length<=32
				doc.StartElement();
				doc.DrawString(g,(i+1).ToString().PadLeft(2,'0'),x,0);
				doc.DrawString(g,noteTexts[i].valuestr,x+50,0);
			}
		}

		private void PrintErrorList(Graphics g) {
			CCDField[] errorCodes=formData.GetFieldsById("G08");
			doc.DrawString(g,(isFrench?"ERREURS (":"ERRORS (")+errorCodes.Length.ToString()+")",x,0,headingFont);
			doc.StartElement(verticalLine);
			for(int i=0;i<errorCodes.Length;i++) {//errorCodes.Length<=10
				doc.StartElement();
				doc.DrawString(g,(i+1).ToString().PadLeft(2,'0'),x,0);
				doc.DrawString(g,CCDerror.message(Convert.ToInt32(errorCodes[i].valuestr),isFrench),x+50,0);
			}
		}

		#endregion

		#region Printing Information Translators

		///<summary>Input string is expected to have the form 'YYYYMMDD'.</summary>
		private string DateNumToPrintDate(string number){
			DateTime dt=new DateTime(Convert.ToInt32(number.Substring(0,4)),Convert.ToInt32(number.Substring(4,2)),
				Convert.ToInt32(number.Substring(6,2)));
			return dt.ToShortDateString();
		}

		private string DateToString(DateTime dt,string format){
			return(IsValidDate(dt)?dt.ToString(format,culture):"");
		}

		private bool IsValidDate(DateTime dt){
			return(//dt!=null && //no allowed
				dt.Year>1900);
		}

		///<summary>The given number must be in the format of: [+-]?[0-9]*</summary>
		private string NumStrToDollars(string number){
			string sign="";
			if(number.Length>0 && (number[0]=='+' || number[0]=='-')){
				sign=number[0].ToString();
				number=number.Substring(1,number.Length-1);
			}
			number=number.PadLeft(3,'0');//Guarantee at least 3 digits of length (1 for dollar, 2 for cents).
			return sign+number.Substring(0,number.Length-3).TrimStart('0')+number.Substring(number.Length-3,1)+"."+
				number.Substring(number.Length-2,2);
		}

		///<summary>Convert a payee code from field F01 into a readable string.</summary>
		private string GetPayableToString(string payeeCode) {
			//TODO: check translations. English to french translations provided by google translator.
			switch(payeeCode) {
				case "1":
					return isFrench?"TITULAIRE":"INSURED/MEMBER";
				case "2":
					return isFrench?"TIERS":"OTHER THIRD PARTY";
				//			case "3": Reserved by CDA net.
				case "4":
					return isFrench?"DENTISTE":"DENTIST";
				default:
					break;
			}
			return "";
		}

		///<summary>Convert a patient relationship enum value into a human-readable, CDA required string.</summary>
		private string GetPatientRelationshipString(Relat relat){
			switch(Canadian.GetRelationshipCode(relat)){
				case "1":
					return isFrench?"Soi-même":"Self";
				case "2":
					return isFrench?"Époux(se)":"Spouse";
				case "3":
					return isFrench?"Enfant":"Child";
				case "4":
					return isFrench?"Conjoint(e)":"Common Law Spouse";
				case "5":
					return isFrench?"Autre":"Other";
				default:
					break;
			}
			return "";
		}

		///<summary>Convet a code from fields F20 and F21 into a human-readable string.</summary>
		private string GetMaterialDescription(int materialCode) {
			//TODO: check translations. English to french translations provided by google translator.
			switch(materialCode) {
				case 1:
					return isFrench?"Pont fixe":"Fixed Bridge";
				case 2:
					return isFrench?"Pont du Maryland":"Maryland Bridge";
				case 3:
					return isFrench?"Dentier (acrylique)":"Denture (Acrylic)";
				case 4:
					return isFrench?"Dentier (cobalt de chrome)":"Denture (Chrome Cobalt)";
				case 5:
					return isFrench?"Implant (fixé)":"Implant (Fixed)";
				case 6:
					return isFrench?"Implant (démontable)":"Implant (Removable)";
				case 7:
					return isFrench?"Couronne":"Crown";
				default:
					break;
			}
			return "";
		}

		///<summary>Convert type code from field F16 into a human-readable string.</summary>
		private string GetProcedureTypeDescription(string procedureTypeCode) {
			//TODO: check translations. English to french translations provided by google translator.
			switch(procedureTypeCode) {
				case "A":
					return isFrench?"Réparation d'un service ou d'une installation antérieur.":
						"Repair of a prior service or installation.";
				case "B":
					return isFrench?"Placement ou service provisoire.":"Temporary placement or service.";
				case "C":
					return isFrench?"Service pour la correction de TMJ.":"Service for correction of TMJ.";
				case "E":
					return isFrench?"Le service est un implant ou est exécuté en même temps que des implants.":
						"Service is an implant or is performed in conjunction with implants.";
				case "L":
					return isFrench?"Appareil perdu.":"Appliance lost.";
				case "S":
					return isFrench?"Appareil volé.":"Appliance stolen.";
				case "X":
					return isFrench?"Circonstances anormales.":"Abnormal circumstances.";
				default:
					break;
			}
			return "";
		}

		#endregion

	}
}