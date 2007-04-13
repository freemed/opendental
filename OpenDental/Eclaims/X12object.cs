using System;
using System.Collections;
using System.IO;
using System.Windows.Forms;

namespace OpenDental.Eclaims
{
	///<summary>Encapsulates one entire X12 Interchange object, including multiple functional groups and transaction sets. It does not care what type of transactions are contained.  It just stores them.  It does not inherit either.  It is up to other classes to use this as needed.</summary>
	public class X12object{
		///<summary>usually *,:,and ~</summary>
		public X12Separators Separators;
		///<summary>A collection of X12FunctionalGroups.</summary>
		public ArrayList functGroups;
		

		///<summary>Takes raw text from a file and converts it into an X12Object.</summary>
		public X12object(string fileName){
			string row="";
			using(StreamReader sr=new StreamReader(fileName)){
				row=sr.ReadLine();
				//Get separator date from the first row (ISA)
				if(row.Substring(0,3)!="ISA"){
					throw new Exception("ISA not found");
				}
				Separators=new X12Separators();
				Separators.Element=row.Substring(3,1);
				Separators.Subelement=row.Substring(104,1);
				Separators.Segment=row.Substring(105,1);
				functGroups=new ArrayList();
				X12Segment segment;
				row=sr.ReadLine();
				while(row!=null){
					segment=new X12Segment(row,Separators);
					if(segment.SegmentID=="IEA"){//if end of interchange
						//do nothing
					}
					if(segment.SegmentID=="GS"){//if new functional group
						functGroups.Add(new X12FunctionalGroup(segment));
					}
					else if(segment.SegmentID=="GE"){//if end of functional group
						//do nothing
					}
					else if(segment.SegmentID=="ST"){//if new transaction set
						if(LastGroup().Transactions==null){
							LastGroup().Transactions=new ArrayList();
						}
						LastGroup().Transactions.Add(new X12Transaction(segment));
					}
					else if(segment.SegmentID=="SE"){//if end of transaction
						//do nothing
					}
					else{//it must be a detail segment within a transaction.
						if(LastTransaction().Segments==null){
							LastTransaction().Segments=new ArrayList();
						}
						LastTransaction().Segments.Add(segment);
					}
					row=sr.ReadLine();
				}
			}//using streamReader on filename
		}

		private X12FunctionalGroup LastGroup(){
			return (X12FunctionalGroup)functGroups[functGroups.Count-1];
		}

		private X12Transaction LastTransaction(){
			return (X12Transaction)LastGroup().Transactions[LastGroup().Transactions.Count-1];
		}

		

	}


	///<summary></summary>
	public class X12FunctionalGroup{
		///<summary>A collection of X12Transactions</summary>
		public ArrayList Transactions;
		///<summary>The segment that identifies this functional group</summary>
		public X12Segment Header;

		///<summary>Supply the functional group header(GS) when creating this object.</summary>
		public X12FunctionalGroup(X12Segment header){
			Header=header.Copy();
		}
	}


	///<summary></summary>
	public class X12Transaction{
		///<summary>A collection of all the X12Segments for this transaction, in the order they originally appeared.</summary>
		public ArrayList Segments;

		///<summary>Supply the transaction header(ST) when creating this object.</summary>
		public X12Transaction(X12Segment header){
			
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
		///<summary>usually *</summary>
		public string Element;
		///<summary>usually :</summary>
		public string Subelement;
		///<summary>usually ~</summary>
		public string Segment;
	}

}












