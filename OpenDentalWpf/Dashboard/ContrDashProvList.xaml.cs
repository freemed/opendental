using System;
using System.Collections.Generic;
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

namespace OpenDentalWpf {
	/// <summary>Used in the dashboard for a list of providers.</summary>
	public class DashProvider {
		/// <summary>Abbr</summary>
		public string ProvName {get;set;}
		/// <summary>May have to tweak this if Media.Color is different than Drawing.Color</summary>
		public Color ProvColor {get;set;}
		/// <summary>Abbr-Name</summary>
		public decimal Production {get;set;}
		/// <summary>Abbr-Name</summary>
		public decimal Income {get;set;}

		public DashProvider(string provName,Color provColor,decimal production,decimal income) {
			ProvName=provName;
			ProvColor=provColor;
			Production=production;
			Income=income;
		}
	}

	/// <summary></summary>
	public partial class ContrDashProvList:UserControl {
		List<DashProvider> ListProv;

		public ContrDashProvList() {
			InitializeComponent();
		}

		public void FillData() {
			GetData();
			FillScreen();
		}

		private void GetData() {
			//simulated for now
			ListProv=new List<DashProvider>();
			ListProv.Add(new DashProvider("Doc1",Colors.Blue,3000,2200));
			ListProv.Add(new DashProvider("Hyg1",Colors.Blue,1200,1300));
			
		}

		private void FillScreen() {
			//gridMain.ItemsSource=ListProv;
			DataTemplate template=new DataTemplate(typeof(DashProvider));
			//template.
			gridMain.RowDetailsTemplate=template;
		}

		

	}
}
