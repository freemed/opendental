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
using System.Windows.Markup;
using System.Windows.Xps.Packaging;
//using swf=System.Windows.Forms;

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

		private void butPrint_Click(object sender,RoutedEventArgs e) {
			PrintDialog dlg=new PrintDialog();
			if(dlg.ShowDialog()!=true){
				return;
			}
			FixedDocument document=new FixedDocument();
			document.DocumentPaginator.PageSize=new Size(dlg.PrintableAreaWidth, dlg.PrintableAreaHeight);
			Canvas canvas1=PrintHelper.GetCanvas(document);
			//set up a grid for printing that's the same as the main grid except for the bottom section with the buttons
			Grid gridPrint=new Grid();
			gridPrint.Width=603;
			gridPrint.Height=603;
			//3 columns 
			gridPrint.ColumnDefinitions.Add(new ColumnDefinition());
			ColumnDefinition colDef=new ColumnDefinition();
			colDef.Width=new GridLength(3);
			gridPrint.ColumnDefinitions.Add(colDef);
			gridPrint.ColumnDefinitions.Add(new ColumnDefinition());
			//3 rows
			gridPrint.RowDefinitions.Add(new RowDefinition());
			RowDefinition rowDef=new RowDefinition();
			rowDef.Height=new GridLength(3);
			gridPrint.RowDefinitions.Add(rowDef);
			gridPrint.RowDefinitions.Add(new RowDefinition());
			//draw rectangles to separate sections
			Rectangle rect;
			rect=new Rectangle();
			rect.Fill=Brushes.LightGray;
			rect.Width=3;
			rect.Height=300;
			Grid.SetRow(rect,0);
			Grid.SetColumn(rect,1);
			gridPrint.Children.Add(rect);
			rect=new Rectangle();
			rect.Fill=Brushes.LightGray;
			rect.Width=3;
			rect.Height=3;
			Grid.SetRow(rect,1);
			Grid.SetColumn(rect,1);
			gridPrint.Children.Add(rect);
			rect=new Rectangle();
			rect.Fill=Brushes.LightGray;
			rect.Width=300;
			rect.Height=3;
			Grid.SetRow(rect,1);
			Grid.SetColumn(rect,2);
			gridPrint.Children.Add(rect);
			rect=new Rectangle();
			rect.Fill=Brushes.LightGray;
			rect.Width=3;
			rect.Height=300;
			Grid.SetRow(rect,2);
			Grid.SetColumn(rect,1);
			gridPrint.Children.Add(rect);
			//add the grid to the canvas
			canvas1.Children.Add(gridPrint);
			double center=canvas1.Width/2d;
			Canvas.SetLeft(gridPrint,(canvas1.Width/2d)-(gridPrint.Width/2));
			//draw a rectangle around the entire grid
			rect=new Rectangle();
			rect.Stroke=Brushes.DarkGray;
			rect.StrokeThickness=1;
			rect.Width=603;
			rect.Height=603;
			Canvas.SetLeft(rect,(canvas1.Width/2d)-(rect.Width/2));
			canvas1.Children.Add(rect);
			//add the three dashboard controls
			gridMain.Children.Remove(contrDashProvList);
			gridPrint.Children.Add(contrDashProvList);
			gridMain.Children.Remove(contrDashProdInc);
			gridPrint.Children.Add(contrDashProdInc);
			gridMain.Children.Remove(contrDashNewPat);
			gridPrint.Children.Add(contrDashNewPat);
			//Canvas.SetTop(contrDashProdInc,
			#if DEBUG
				WinPrintPreview pp=new WinPrintPreview();
				pp.Owner=this;
				pp.Document=document;
				//warning! Only use the print preview in debug.  It will crash if your mouse moves into the top toolbar.
				pp.ShowDialog();
			#else
				dlg.PrintDocument(document.DocumentPaginator,"Dashboard");
			#endif
			//myPanel.Measure(new Size(dialog.PrintableAreaWidth,dialog.PrintableAreaHeight));
			//myPanel.Arrange(new Rect(new Point(0, 0),myPanel.DesiredSize));
			//dlg.PrintVisual(gridMain,"Dashboard");
			gridPrint.Children.Remove(contrDashProvList);
			gridMain.Children.Add(contrDashProvList);
			gridPrint.Children.Remove(contrDashProdInc);
			gridMain.Children.Add(contrDashProdInc);
			gridPrint.Children.Remove(contrDashNewPat);
			gridMain.Children.Add(contrDashNewPat);
		}

		private void butClose_Click(object sender,RoutedEventArgs e) {
			Close();
		}



	}
}

