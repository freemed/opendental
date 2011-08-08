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
	/// <summary></summary>
	public partial class WinDashboard:Window {

		public WinDashboard() {
			InitializeComponent();
		}

		private void Window_Loaded(object sender,RoutedEventArgs e) {
			contrDashProvList.FillData();
			contrDashProdInc.FillData();
			contrDashNewPat.FillData();
		}

		private void Window_Activated(object sender,EventArgs e) {
			
		}

		



	}
}

