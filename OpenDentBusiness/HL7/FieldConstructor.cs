using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using OpenDental;
using OpenDental.UI;
using OpenDentBusiness.UI;


namespace OpenDentBusiness.HL7 {
	///<summary>This is the engine that will construct our outgoing HL7 message fields.</summary>
	public class FieldConstructor {

		public static string GenerateDFT(HL7Def def,string fieldName,Patient pat,Provider prov,Procedure proc,Patient guar,Appointment apt,int sequenceNum,string eventType,string pdfDescription) {
			//big long list of fieldnames that we support
			switch(fieldName){
				case "apt.AptNum":
					return apt.AptNum.ToString();
				case "dateTime.Now":
					return gDTM(DateTime.Now,14);
				case "eventType":
					return eventType;
				case "guar.addressCityStateZip":
					return gConcat(def.ComponentSeparator,guar.Address,guar.Address2,guar.City,guar.State,guar.Zip);
				case "guar.birthdateTime":
					return gDTM(guar.Birthdate,8);
				case "guar.Gender":
					return gIS(guar);
				case "guar.HmPhone":
					return gXTN(guar.HmPhone,10);
				case "guar.nameLFM":
					return gConcat(def.ComponentSeparator,guar.LName,guar.FName,guar.MiddleI);
				case "guar.PatNum":
					return guar.PatNum.ToString();
				case "guar.SSN":
					return guar.SSN;
				case "guar.WkPhone":
					return gXTN(guar.WkPhone,10);
				case "messageType":
					return gConcat(def.ComponentSeparator,"DFT",eventType);
				case "pat.addressCityStateZip":
					return gConcat(def.ComponentSeparator,pat.Address,pat.Address2,pat.City,pat.State,pat.Zip);
				case "pat.birthdateTime":
					return gDTM(pat.Birthdate,8);
				case "pat.ChartNumber":
					return pat.ChartNumber;
				case "pat.Gender":
					return gIS(pat);
				case "pat.HmPhone":
					return gXTN(pat.HmPhone,10);
				case "pat.nameLFM":
					return gConcat(def.ComponentSeparator,pat.LName,pat.FName,pat.MiddleI);
				case "pat.PatNum":
					return pat.PatNum.ToString();
				case "pat.Position":
					return gPos(pat);
				case "pat.Race":
					return gRace(pat);
				case "pat.SSN":
					return pat.SSN;
				case "pat.WkPhone":
					return gXTN(pat.WkPhone,10);
				case "pdfDescription":
					return pdfDescription;
				case "pdfDataAsBase64":
					return gPDF(apt,pdfDescription);
				case "proc.DiagnosticCode":
					return proc.DiagnosticCode;
				case "proc.procDateTime":
					return gDTM(proc.ProcDate,14);
				case "proc.ProcFee":
					return proc.ProcFee.ToString("F2");
				case "proc.toothSurfRange":
					return gTreatArea(def.ComponentSeparator,proc);
				case "proccode.ProcCode":
					return gProcCode(proc);
				case "prov.provIdNameLFM":
					return gConcat(def.ComponentSeparator,prov.EcwID,prov.LName,prov.FName,prov.MI);
				case "separators^~\\&":
					return gSep(def);
				case "sequenceNum":
					return sequenceNum.ToString();
				default:
					return "";
			}
		}

		//Send in component separator for this def and the values in the order they should be in the message.
		private static string gConcat(string componentSep,params string[] vals) {
			string retVal="";
			if(vals.Length==1) {
				return retVal=vals[0];//this allows us to pass in all components for the field as one long string: comp1^comp2^comp3
			}
			for(int i=0;i<vals.Length;i++) {
				if(i>0) {
					retVal+=componentSep;
				}
				retVal+=vals[i];
			}
			return retVal;
		}

		private static string gSep(HL7Def def) {
			return def.ComponentSeparator+def.RepetitionSeparator+def.EscapeCharacter+def.SubcomponentSeparator;
		}

		private static string gDTM(DateTime dt,int precisionDigits) {
			switch(precisionDigits) {
				case 8:
					return dt.ToString("yyyyMMdd");
				case 14:
					return dt.ToString("yyyyMMddHHmmss");
				default:
					return "";
			}
		}

		private static string gIS(Patient pat) {
			if(pat.Gender==PatientGender.Female) {
				return "F";
			}
			if(pat.Gender==PatientGender.Male) {
				return "M";
			}
			return "U";
		}

		private static string gPDF(Appointment apt,string pdfDescription) {
			if(pdfDescription=="treatment") {
				return "";
			}
			else {//must be "progressnotes"
				return "";
			}
		}

		private static string gPos(Patient pat) {
			switch(pat.Position) {
				case PatientPosition.Single:
					return "Single";
				case PatientPosition.Married:
					return "Married";
				case PatientPosition.Divorced:
					return "Divorced";
				case PatientPosition.Widowed:
					return "Widowed";
				case PatientPosition.Child:
					return "Single";
				default:
					return "Single";
			}
		}

		private static string gProcCode(Procedure proc) {
			string retVal="";
			ProcedureCode procCode=ProcedureCodes.GetProcCode(proc.CodeNum);
			if(procCode.ProcCode.Length>5 && procCode.ProcCode.StartsWith("D")) {
				retVal=procCode.ProcCode.Substring(0,5);//Remove suffix from all D codes.
			}
			else {
				retVal=procCode.ProcCode;
			}
			return retVal;
		}

		private static string gRace(Patient pat) {
			switch(pat.Race) {
				case PatientRace.AmericanIndian:
					return "American Indian Or Alaska Native";
				case PatientRace.Asian:
					return "Asian";
				case PatientRace.HawaiiOrPacIsland:
					return "Native Hawaiian or Other Pacific";
				case PatientRace.AfricanAmerican:
					return "Black or African American";
				case PatientRace.White:
					return "White";
				case PatientRace.HispanicLatino:
					return "Hispanic";
				case PatientRace.Other:
					return "Other Race";
				default:
					return "Other Race";
			}
		}

		private static string gTreatArea(string componentSep,Procedure proc) {
			string retVal="";
			ProcedureCode procCode=ProcedureCodes.GetProcCode(proc.CodeNum);
			if(procCode.TreatArea==TreatmentArea.ToothRange) {
				retVal=proc.ToothRange;
			}
			else if(procCode.TreatArea==TreatmentArea.Surf) {//probably not necessary
				retVal=gConcat(componentSep,Tooth.ToInternat(proc.ToothNum),Tooth.SurfTidyForClaims(proc.Surf,proc.ToothNum));
			}
			else {
				retVal=gConcat(componentSep,Tooth.ToInternat(proc.ToothNum),proc.Surf);
			}
			return retVal;
		}

		///<summary>XTN is a phone number.</summary>
		private static string gXTN(string phone,int numDigits) {
			string retVal="";
			for(int i=0;i<phone.Length;i++) {
				if(Char.IsNumber(phone,i)) {
					if(retVal=="" && phone.Substring(i,1)=="1") {
						continue;//skip leading 1.
					}
					retVal+=phone.Substring(i,1);
				}
				if(retVal.Length==numDigits) {
					return retVal;
				}
			}
			//never made it to 10
			return "";
		}

		/////<summary>Creates a new .pdf file containing all of the procedures attached to this appointment and 
		/////returns the contents of the .pdf file as a base64 encoded string.</summary>
		//private static string GenerateProceduresIntoPdf(Appointment apt) {
		//  MigraDoc.DocumentObjectModel.Document doc=new MigraDoc.DocumentObjectModel.Document();
		//  doc.DefaultPageSetup.PageWidth=Unit.FromInch(8.5);
		//  doc.DefaultPageSetup.PageHeight=Unit.FromInch(11);
		//  doc.DefaultPageSetup.TopMargin=Unit.FromInch(.5);
		//  doc.DefaultPageSetup.LeftMargin=Unit.FromInch(.5);
		//  doc.DefaultPageSetup.RightMargin=Unit.FromInch(.5);
		//  MigraDoc.DocumentObjectModel.Section section=doc.AddSection();
		//  MigraDoc.DocumentObjectModel.Font headingFont=CreateFont(13,true);
		//  MigraDoc.DocumentObjectModel.Font bodyFontx=CreateFont(9,false);
		//  string text;
		//  //Heading---------------------------------------------------------------------------------------------------------------
		//  #region printHeading
		//  Paragraph par=section.AddParagraph();
		//  ParagraphFormat parformat=new ParagraphFormat();
		//  parformat.Alignment=ParagraphAlignment.Center;
		//  parformat.Font=CreateFont(10,true);
		//  par.Format=parformat;
		//  text=Lans.g("FieldConstructor","procedures").ToUpper();
		//  par.AddFormattedText(text,headingFont);
		//  par.AddLineBreak();
		//  Patient pat=Patients.GetPat(apt.PatNum);
		//  text=pat.GetNameFLFormal();
		//  par.AddFormattedText(text,headingFont);
		//  par.AddLineBreak();
		//  text=DateTime.Now.ToShortDateString();
		//  par.AddFormattedText(text,headingFont);
		//  par.AddLineBreak();
		//  par.AddLineBreak();
		//  #endregion
		//  //Procedure List--------------------------------------------------------------------------------------------------------
		//  #region Procedure List
		//  ODGrid gridProg=new ODGrid();
		//  //this.Controls.Add(gridProg);//Only added temporarily so that printing will work. Removed at end with Dispose().
		//  gridProg.BeginUpdate();
		//  gridProg.Columns.Clear();
		//  ODGridColumn col;
		//  List<DisplayField> fields=DisplayFields.GetForCategory(DisplayFieldCategory.None);
		//  for(int i=0;i<fields.Count;i++) {
		//    if(fields[i].InternalName=="User" || fields[i].InternalName=="Signed") {
		//      continue;
		//    }
		//    if(fields[i].Description=="") {
		//      col=new ODGridColumn(fields[i].InternalName,fields[i].ColumnWidth);
		//    }
		//    else {
		//      col=new ODGridColumn(fields[i].Description,fields[i].ColumnWidth);
		//    }
		//    if(fields[i].InternalName=="Amount") {
		//      col.TextAlign=HorizontalAlignment.Right;
		//    }
		//    if(fields[i].InternalName=="ADA Code") {
		//      col.TextAlign=HorizontalAlignment.Center;
		//    }
		//    gridProg.Columns.Add(col);
		//  }
		//  gridProg.NoteSpanStart=2;
		//  gridProg.NoteSpanStop=7;
		//  gridProg.Rows.Clear();
		//  List<Procedure> procsForDay=Procedures.GetProcsForPatByDate(apt.PatNum,apt.AptDateTime);
		//  for(int i=0;i<procsForDay.Count;i++) {
		//    Procedure proc=procsForDay[i];
		//    ProcedureCode procCode=ProcedureCodes.GetProcCodeFromDb(proc.CodeNum);
		//    Provider prov=Providers.GetProv(proc.ProvNum);
		//    Userod usr=Userods.GetUser(proc.UserNum);
		//    ODGridRow row=new ODGridRow();
		//    row.ColorLborder=System.Drawing.Color.Black;
		//    for(int f=0;f<fields.Count;f++) {
		//      switch(fields[f].InternalName) {
		//        case "Date":
		//          row.Cells.Add(proc.ProcDate.Date.ToShortDateString());
		//          break;
		//        case "Time":
		//          row.Cells.Add(proc.ProcDate.ToString("h:mm")+proc.ProcDate.ToString("%t").ToLower());
		//          break;
		//        case "Th":
		//          row.Cells.Add(proc.ToothNum);
		//          break;
		//        case "Surf":
		//          row.Cells.Add(proc.Surf);
		//          break;
		//        case "Dx":
		//          row.Cells.Add(proc.Dx.ToString());
		//          break;
		//        case "Description":
		//          row.Cells.Add((procCode.LaymanTerm!="")?procCode.LaymanTerm:procCode.Descript);
		//          break;
		//        case "Stat":
		//          row.Cells.Add(Lans.g("enumProcStat",proc.ProcStatus.ToString()));
		//          break;
		//        case "Prov":
		//          if(prov.Abbr.Length>5) {
		//            row.Cells.Add(prov.Abbr.Substring(0,5));
		//          }
		//          else {
		//            row.Cells.Add(prov.Abbr);
		//          }
		//          break;
		//        case "Amount":
		//          row.Cells.Add(proc.ProcFee.ToString("F"));
		//          break;
		//        case "ADA Code":
		//          if(procCode.ProcCode.Length>5 && procCode.ProcCode.StartsWith("D")) {
		//            row.Cells.Add(procCode.ProcCode.Substring(0,5));//Remove suffix from all D codes.
		//          }
		//          else {
		//            row.Cells.Add(procCode.ProcCode);
		//          }
		//          break;
		//        case "User":
		//          row.Cells.Add(usr!=null?usr.UserName:"");
		//          break;
		//      }
		//    }
		//    row.Note=proc.Note;
		//    //Row text color.
		//    switch(proc.ProcStatus) {
		//      case ProcStat.TP:
		//        row.ColorText=DefC.Long[(int)DefCat.ProgNoteColors][0].ItemColor;
		//        break;
		//      case ProcStat.C:
		//        row.ColorText=DefC.Long[(int)DefCat.ProgNoteColors][1].ItemColor;
		//        break;
		//      case ProcStat.EC:
		//        row.ColorText=DefC.Long[(int)DefCat.ProgNoteColors][2].ItemColor;
		//        break;
		//      case ProcStat.EO:
		//        row.ColorText=DefC.Long[(int)DefCat.ProgNoteColors][3].ItemColor;
		//        break;
		//      case ProcStat.R:
		//        row.ColorText=DefC.Long[(int)DefCat.ProgNoteColors][4].ItemColor;
		//        break;
		//      case ProcStat.D:
		//        row.ColorText=System.Drawing.Color.Black;
		//        break;
		//      case ProcStat.Cn:
		//        row.ColorText=DefC.Long[(int)DefCat.ProgNoteColors][22].ItemColor;
		//        break;
		//    }
		//    row.ColorBackG=System.Drawing.Color.White;
		//    if(proc.ProcDate.Date==DateTime.Today) {
		//      row.ColorBackG=DefC.Long[(int)DefCat.MiscColors][6].ItemColor;
		//    }
		//    gridProg.Rows.Add(row);
		//  }
		//  DrawGrid(section,gridProg);
		//  #endregion
		//  MigraDoc.Rendering.PdfDocumentRenderer pdfRenderer=new MigraDoc.Rendering.PdfDocumentRenderer(true,PdfFontEmbedding.Always);
		//  pdfRenderer.Document=doc;
		//  pdfRenderer.RenderDocument();
		//  MemoryStream ms=new MemoryStream();
		//  pdfRenderer.PdfDocument.Save(ms);
		//  byte[] pdfBytes=ms.GetBuffer();
		//  string pdfDataStr=Convert.ToBase64String(pdfBytes);
		//  ms.Dispose();
		//  return pdfDataStr;
		//}

		//public static MigraDoc.DocumentObjectModel.Font CreateFont(float fsize,bool isBold) {
		//  MigraDoc.DocumentObjectModel.Font font=new MigraDoc.DocumentObjectModel.Font();
		//  //if(fontFamily==FontFamily.GenericSansSerif){
		//  font.Name="Arial";
		//  //}
		//  //if(fontFamily==FontFamily.GenericSerif){
		//  //	font.Name="Times";
		//  //}
		//  font.Size=Unit.FromPoint(fsize);
		//  font.Bold=isBold;
		//  return font;
		//}

		//public static MigraDoc.DocumentObjectModel.Font CreateFont(float fsize,bool isBold,System.Drawing.Color color) {
		//  MigraDoc.DocumentObjectModel.Color colorx=ConvertColor(color);
		//  MigraDoc.DocumentObjectModel.Font font=new MigraDoc.DocumentObjectModel.Font();
		//  //if(fontFamily.==FontFamily.GenericSansSerif) {
		//  font.Name="Arial";
		//  //}
		//  //if(fontFamily==FontFamily.GenericSerif) {
		//  //	font.Name="Times";
		//  //}
		//  font.Size=Unit.FromPoint(fsize);
		//  font.Bold=isBold;
		//  font.Color=colorx;
		//  return font;
		//}

		//public static MigraDoc.DocumentObjectModel.Color ConvertColor(System.Drawing.Color color) {
		//  byte a=color.A;
		//  byte r=color.R;
		//  byte g=color.G;
		//  byte b=color.B;
		//  return new MigraDoc.DocumentObjectModel.Color(a,r,g,b);
		//}

		//public static void DrawGrid(Section section,ODGrid grid) {
		//  //first, calculate width of dummy column that will push the grid over just enough to center it on the page.
		//  double pageW=0;
		//  if(CultureInfo.CurrentCulture.Name=="en-US") {
		//    pageW=850;
		//  }
		//  //don't know about Canada
		//  else {
		//    pageW=830;
		//  }
		//  //in 100ths/inch
		//  double widthAllColumns=(double)grid.WidthAllColumns/.96;
		//  double lmargin=section.Document.DefaultPageSetup.LeftMargin.Inch*100;
		//  double dummyColW=(pageW-widthAllColumns)/2-lmargin;
		//  Table table=new Table();
		//  Column col;
		//  col=table.AddColumn(Unit.FromInch(dummyColW/100));
		//  for(int i=0;i<grid.Columns.Count;i++) {
		//    col=table.AddColumn(Unit.FromInch((double)grid.Columns[i].ColWidth/96));
		//    col.LeftPadding=Unit.FromInch(.01);
		//    col.RightPadding=Unit.FromInch(.01);
		//  }
		//  Row row;
		//  row=table.AddRow();
		//  row.HeadingFormat=true;
		//  row.TopPadding=Unit.FromInch(0);
		//  row.BottomPadding=Unit.FromInch(-.01);
		//  Cell cell;
		//  Paragraph par;
		//  //dummy column:
		//  cell=row.Cells[0];
		//  //cell.Shading.Color=Colors.LightGray;
		//  //format dummy cell?
		//  MigraDoc.DocumentObjectModel.Font fontHead=new MigraDoc.DocumentObjectModel.Font("Arial",Unit.FromPoint(8.5));
		//  fontHead.Bold=true;
		//  for(int i=0;i<grid.Columns.Count;i++) {
		//    cell = row.Cells[i+1];
		//    par=cell.AddParagraph();
		//    par.AddFormattedText(grid.Columns[i].Heading,fontHead);
		//    par.Format.Alignment=ParagraphAlignment.Center;
		//    cell.Format.Alignment=ParagraphAlignment.Center;
		//    cell.Borders.Width=Unit.FromPoint(1);
		//    cell.Borders.Color=Colors.Black;
		//    cell.Shading.Color=Colors.LightGray;
		//  }
		//  MigraDoc.DocumentObjectModel.Font fontBody=null;//=new MigraDoc.DocumentObjectModel.Font("Arial",Unit.FromPoint(8.5));
		//  bool isBold;
		//  System.Drawing.Color color;
		//  int edgeRows=1;
		//  for(int i=0;i<grid.Rows.Count;i++,edgeRows++) {
		//    row=table.AddRow();
		//    row.TopPadding=Unit.FromInch(.01);
		//    row.BottomPadding=Unit.FromInch(0);
		//    for(int j=0;j<grid.Columns.Count;j++) {
		//      cell = row.Cells[j+1];
		//      par=cell.AddParagraph();
		//      if(grid.Rows[i].Cells[j].Bold==YN.Unknown) {
		//        isBold=grid.Rows[i].Bold;
		//      }
		//      else if(grid.Rows[i].Cells[j].Bold==YN.Yes) {
		//        isBold=true;
		//      }
		//      else {// if(grid.Rows[i].Cells[j].Bold==YN.No){
		//        isBold=false;
		//      }
		//      if(grid.Rows[i].Cells[j].ColorText==System.Drawing.Color.Empty) {
		//        color=grid.Rows[i].ColorText;
		//      }
		//      else {
		//        color=grid.Rows[i].Cells[j].ColorText;
		//      }
		//      fontBody=CreateFont(8.5f,isBold,color);
		//      par.AddFormattedText(grid.Rows[i].Cells[j].Text,fontBody);
		//      if(grid.Columns[j].TextAlign==HorizontalAlignment.Center) {
		//        cell.Format.Alignment=ParagraphAlignment.Center;
		//      }
		//      if(grid.Columns[j].TextAlign==HorizontalAlignment.Left) {
		//        cell.Format.Alignment=ParagraphAlignment.Left;
		//      }
		//      if(grid.Columns[j].TextAlign==HorizontalAlignment.Right) {
		//        cell.Format.Alignment=ParagraphAlignment.Right;
		//      }
		//      cell.Borders.Color=new MigraDoc.DocumentObjectModel.Color(180,180,180);
		//      if(grid.Rows[i].ColorLborder!=System.Drawing.Color.Empty) {
		//        cell.Borders.Bottom.Color=ConvertColor(grid.Rows[i].ColorLborder);
		//      }
		//    }
		//    if(grid.Rows[i].Note!=null && grid.Rows[i].Note!="" && grid.NoteSpanStop>0 && grid.NoteSpanStart<grid.Columns.Count) {
		//      row=table.AddRow();
		//      row.TopPadding=Unit.FromInch(.01);
		//      row.BottomPadding=Unit.FromInch(0);
		//      cell=row.Cells[grid.NoteSpanStart+1];
		//      par=cell.AddParagraph();
		//      par.AddFormattedText(grid.Rows[i].Note,fontBody);
		//      cell.Format.Alignment=ParagraphAlignment.Left;
		//      cell.Borders.Color=new MigraDoc.DocumentObjectModel.Color(180,180,180);
		//      cell.MergeRight=grid.Columns.Count-1-grid.NoteSpanStart;
		//      edgeRows++;
		//    }
		//  }
		//  table.SetEdge(1,0,grid.Columns.Count,edgeRows,Edge.Box,MigraDoc.DocumentObjectModel.BorderStyle.Single,1,Colors.Black);
		//  section.Add(table);
		//}
	}
}
