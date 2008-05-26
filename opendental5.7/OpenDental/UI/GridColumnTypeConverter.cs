/*using System;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Diagnostics;
using System.Globalization;
using System.Windows.Forms;

namespace OpenDental.UI{
	/// <summary>Serializes GridColumns for design time support.</summary>
	public class GridColumnTypeConverter:ExpandableObjectConverter{

		///<summary></summary>
		public GridColumnTypeConverter(){
			// TODO: Add constructor logic here
		}
	
		///<summary></summary>
		public override bool CanConvertFrom(ITypeDescriptorContext context,Type t){
			if(t==typeof(string)){
				return true;
			}
			MessageBox.Show("Error 3");
			return base.CanConvertFrom(context,t);
		}

		///<summary></summary>
		public override bool CanConvertTo(ITypeDescriptorContext context,Type destType){
			if(destType==typeof(InstanceDescriptor) || destType==typeof(string)){
				return true;
			}
			MessageBox.Show("Error 4");
			return base.CanConvertTo(context,destType);
		}	
	
		///<summary></summary>
		public override object ConvertFrom(ITypeDescriptorContext context,CultureInfo info,object value){
			if(value is string){
				string str=(string)value;
				try{
					string[] elements=str.Split(new char[] {';'});
					if(elements.Length==2){//old style conversion with no textAlign
						return new ODGridColumn(elements[0],int.Parse(elements[1]));
					}
					else{
						HorizontalAlignment align=HorizontalAlignment.Left;
						switch(elements[2]){
							case "Left":
								align=HorizontalAlignment.Left;
								break;
							case "Center":
								align=HorizontalAlignment.Center;
								break;
							case "Right":
								align=HorizontalAlignment.Right;
								break;
						}
						return new ODGridColumn(elements[0],int.Parse(elements[1]),align);
					}
				}
				catch{
					return new ODGridColumn();
				}
			}
			return base.ConvertFrom(context,info,value);
	{
		 
	}
		}

		///<summary></summary>
		public override object ConvertTo(ITypeDescriptorContext context,CultureInfo info,object value,Type destType){
			if((destType==typeof(string))){
				ODGridColumn gc=new ODGridColumn();
				try{
					gc=(ODGridColumn)value;
				}
				catch{
					MessageBox.Show("Error 1: "+value.GetType().ToString());
				}
				return gc.Heading+";"+gc.ColWidth.ToString()+";"+gc.TextAlign.ToString();
			}
			else if(destType==typeof(InstanceDescriptor)){
				try{
					return new InstanceDescriptor(
						typeof(ODGridColumn).GetConstructor(new Type[]{typeof(string),typeof(int),typeof(HorizontalAlignment)}),
						new object[]{((ODGridColumn)value).Heading,((ODGridColumn)value).ColWidth,((ODGridColumn)value).TextAlign},
						true);
				}
				catch(Exception e){
					MessageBox.Show("Error 2:"+e.Message);
				}
			}
			return base.ConvertTo(context,info,value,destType);
		}

	




	}
}

*/












