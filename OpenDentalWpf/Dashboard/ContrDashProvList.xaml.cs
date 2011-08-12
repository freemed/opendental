using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using OpenDentBusiness;

namespace OpenDentalWpf {
	

	/// <summary></summary>
	public partial class ContrDashProvList:UserControl {
		DataTable table;

		public ContrDashProvList() {
			InitializeComponent();
		}

		public void FillData() {
			GetData();
			FillScreen();
		}

		private void GetData() {
			string command;
			command="SELECT provider.ProvNum,SUM(ProcFee) production "
				+"FROM provider "
				+"LEFT JOIN procedurelog ON procedurelog.ProvNum=provider.ProvNum "
				+"AND ProcDate="+POut.Date(DateTime.Today)+" "
				+"GROUP BY provider.ProvNum";
			table=Reports.GetTable(command);
		}

		private void FillScreen() {
			List<GridRowObj> ListProv=new List<GridRowObj>();
			GridRowObj row;
			for(int i=0;i<table.Rows.Count;i++){
				row=new GridRowObj();
				row.ProvName=Providers.GetAbbr(PIn.Long(table.Rows[i]["ProvNum"].ToString()));
				System.Drawing.Color c1=Providers.GetColor(PIn.Long(table.Rows[i]["ProvNum"].ToString()));
				row.ProvColor=Color.FromArgb(c1.A,c1.R,c1.G,c1.B);
				row.Production=PIn.Decimal(table.Rows[i]["production"].ToString()).ToString("c0");
				row.Income=(0).ToString("c0");
				ListProv.Add(row);
			}
			//Style style=new Style(typeof(TextBlock));
			//style.Setters.Add(new Setter(TextBlock.HorizontalAlignmentProperty,HorizontalAlignment.Right));
			//((DataGridTextColumn)gridMain.Columns[2]).ElementStyle=style;
			//((DataGridTextColumn)gridMain.Columns[2]).Binding=new Binding("Production"); 
			//((DataGridTextColumn)gridMain.Columns[2]).Binding.StringFormat="c0";
			gridMain.ItemsSource=ListProv;
		}

			/// <summary></summary>
		private class GridRowObj {
			/// <summary>Abbr</summary>
			public string ProvName { get; set; }
			/// <summary>May have to tweak this if Media.Color is different than Drawing.Color</summary>
			public Color ProvColor { get; set; }
			/// <summary>Abbr</summary>
			public string Production { get; set; }
			/// <summary></summary>
			public string Income { get; set; }
		}

	}

	
	

}
