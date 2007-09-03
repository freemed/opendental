using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace OpenDental.Eclaims
{
	///<summary>Encapsulates one entire X12 Interchange object, including multiple functional groups and transaction sets. It does not care what type of transactions are contained.  It just stores them.  It does not inherit either.  It is up to other classes to use this as needed.</summary>
	public class X12object{
		///<summary>usually *,:,and ~</summary>
		public X12Separators Separators;
		///<summary>A collection of X12FunctionalGroups.</summary>
		public List<X12FunctionalGroup> FunctGroups;
		///<summary>All segments for the entiremessage.</summary>
		public List<X12Segment> Segments;

		public static bool IsX12(string messageText){
			if(messageText==null || messageText.Length<106){
				return false;
			}
			if(messageText.Substring(0,3)=="ISA"){
				return true;
			}
			return false;
		}

		///<summary>This override is never explicitly used.</summary>
		protected X12object(){

		}

		///<summary>Takes raw text and converts it into an X12Object.  Pass it in as a string array.</summary>
		public X12object(string messageText){
			messageText=messageText.Replace("\r","");
			messageText=messageText.Replace("\n","");
			if(messageText.Substring(0,3)!="ISA"){
				throw new ApplicationException("ISA not found");
			}
			Separators=new X12Separators();
			Separators.Element=messageText.Substring(3,1);
			Separators.Subelement=messageText.Substring(104,1);
			Separators.Segment=messageText.Substring(105,1);
			string[] messageRows=messageText.Split(new string[] {Separators.Segment},StringSplitOptions.None);
			FunctGroups=new List<X12FunctionalGroup>();
			Segments=new List<X12Segment>();
			string row;
			X12Segment segment;
			for(int i=1;i<messageRows.Length;i++){
				row=messageRows[i];
				segment=new X12Segment(row,Separators);
				Segments.Add(segment);
				if(segment.SegmentID=="IEA"){//if end of interchange
					//do nothing
				}
				if(segment.SegmentID=="GS"){//if new functional group
					FunctGroups.Add(new X12FunctionalGroup(segment));
				}
				else if(segment.SegmentID=="GE"){//if end of functional group
					//do nothing
				}
				else if(segment.SegmentID=="ST"){//if new transaction set
					if(LastGroup().Transactions==null){
						LastGroup().Transactions=new List<X12Transaction>();
					}
					LastGroup().Transactions.Add(new X12Transaction(segment));
				}
				else if(segment.SegmentID=="SE"){//if end of transaction
					//do nothing
				}
				else{//it must be a detail segment within a transaction.
					if(LastTransaction().Segments==null){
						LastTransaction().Segments=new List<X12Segment>();
					}
					LastTransaction().Segments.Add(segment);
				}
				//row=sr.ReadLine();
			}
		}

		public bool Is997() {
			//There is only one transaction set (ST/SE) per functional group (GS/GE), but I think there can be multiple functional groups
			//if acking multiple 
			if(this.FunctGroups.Count!=1) {
				return false;
			}
			if(this.FunctGroups[0].Transactions[0].Header.Get(1)=="997") {
				return true;
			}
			return false;
		}

		private X12FunctionalGroup LastGroup(){
			return (X12FunctionalGroup)FunctGroups[FunctGroups.Count-1];
		}

		private X12Transaction LastTransaction(){
			return (X12Transaction)LastGroup().Transactions[LastGroup().Transactions.Count-1];
		}

		

	}


	///<summary>GS/GE combination. Contained within an interchange control combination (ISA/IEA). Contains at least one transaction (ST/SE). </summary>
	public class X12FunctionalGroup{
		///<summary>A collection of X12Transactions</summary>
		public List<X12Transaction> Transactions;
		///<summary>The segment that identifies this functional group</summary>
		public X12Segment Header;

		///<summary>Supply the functional group header(GS) when creating this object.</summary>
		public X12FunctionalGroup(X12Segment header){
			Header=header.Copy();
		}
	}


	///<summary>ST/SE combination.  Containted within functional group (GS/GE).  In claims, there will be one transaction per carrier.</summary>
	public class X12Transaction{
		///<summary>A collection of all the X12Segments for this transaction, in the order they originally appeared.</summary>
		public List<X12Segment> Segments;
		///<summary>The segment that identifies this functional group</summary>
		public X12Segment Header;

		///<summary>Supply the transaction header(ST) when creating this object.</summary>
		public X12Transaction(X12Segment header){
			Header=header.Copy();
		}

		public X12Segment GetSegmentByID(string segID){
			for(int i=0;i<Segments.Count;i++){
				if(Segments[i].SegmentID==segID){
					return Segments[i];
				}
			}
			return null;
		}
	}


	///<summary>An X12 segment is a single row of the text file.</summary>
	public class X12Segment{
		///<summary>Usually 2 or 3 letters. Can also be found at Elements[0].</summary>
		public string SegmentID;
		///<summary></summary>
		public string[] Elements;
		///<summary></summary>
		private X12Separators Separators;

		///<summary></summary>
		public X12Segment(string rawText,X12Separators separators){
			Separators=separators;
			//first, remove the segment terminator
			rawText=rawText.Replace(separators.Segment,"");
			//then, split the row into elements, eliminating the DataElementSeparator
			Elements=rawText.Split(Char.Parse(separators.Element));
			SegmentID=Elements[0];
		}

		private X12Segment(){

		}

		///<summary>Returns a copy of this segement</summary>
		public X12Segment Copy(){
			X12Segment retVal=new X12Segment();
			retVal.SegmentID=SegmentID;
			retVal.Elements=(string[])Elements.Clone();//shallow copy is fine since just strings.
			return retVal;
		}

		///<summary>Returns the string representation of the given element within this segment. If the element does not exist, as can happen with optional elements, then "" is returned.</summary>
		public string Get(int elementPosition){
			if(Elements.Length<=elementPosition){
				return "";
			}
			return Elements[elementPosition];
		}

		///<summary>Returns the string representation of the given element,subelement within this segment. If the element or subelement does not exist, as can happen with optional elements, then "" is returned.</summary>
		public string Get(int elementPosition,int subelementPosition){
			if(Elements.Length<=elementPosition){
				return "";
			}
			string[] subelements=Elements[elementPosition].Split(Char.Parse(Separators.Subelement));
			if(subelements.Length<=subelementPosition){
				return "";
			}
			return subelements[subelementPosition];
		}

	}

	///<summary></summary>
	public struct X12Separators{
		///<summary>usually ~</summary>
		public string Segment;
		///<summary>usually *</summary>
		public string Element;
		///<summary>usually :</summary>
		public string Subelement;
	}



}












