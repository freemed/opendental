using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Drawing;


namespace OpenDental.Reporting.Allocators.MyAllocator1
{
	/// <summary>
	/// Graphic As Such:
	///                                       _____________
	///                                      | Patient ID  |
	/// _____________________________________|_____________|
	/// |            |                       |             |   
	/// | Guarantor  | Painter, Matt         |    1001     |
	/// |____________|_______________________|_____________|
	/// | Accounts:  | 1 Painter, Matt       |    1001     |
	/// |            | 2 Hummel, Robbie      |    1001     |
	/// |            | 3 Kramer, Chris       |    1002     |
	/// |            | 4 Moore, E'twan       |    1045     |
	/// |____________|_______________________|_____________|
	/// 
	/// Returns Null if _Guarantor = 0
	/// 
	/// </summary>
	class GuarantorAccounts_Graphic 
	{
		private int _Guarantor;
		private int[] _colWidths;
		private int[] _rowHeights;
		public GuarantorAccounts_Graphic(int Guarantor)
		{
			_Guarantor = Guarantor;
		}
		private DataTable GetTable()
		{
			if (_Guarantor == 0)
				return null;
			string cmd = "SELECT PatNum, FName, LName FROM Patient WHERE Guarantor = " + _Guarantor + " ORDER BY PatNum ";
			DataTable rawTbl = General.GetTable(cmd);
			DataTable TblReturn = new DataTable();
			DataColumn dc1 = new DataColumn();
			DataColumn dc2 = new DataColumn();
			DataColumn dc3 = new DataColumn();
			TblReturn.Columns.Add(dc1);
			TblReturn.Columns.Add(dc2);
			TblReturn.Columns.Add(dc3);

			// 1st Row
			DataRow newRow = TblReturn.NewRow();
			newRow[0] = "";
			newRow[1] = "";
			newRow[2] = "Patient ID";
			TblReturn.Rows.Add(newRow);

			// 2nd Row
			newRow = TblReturn.NewRow();
			newRow[0] = "Guarantor";
			newRow[1] = "Not Found";
			newRow[2] = "";
			TblReturn.Rows.Add(newRow);

			if (rawTbl != null && rawTbl.Rows.Count != 0)
			{
				for (int i = 0; i < rawTbl.Rows.Count; i++)
				{
					newRow = TblReturn.NewRow();
					if (i == 0)
						newRow[0] = "Accounts:";
					newRow[1] = i+" "+ rawTbl.Rows[i][2].ToString() + ", "+rawTbl.Rows[i][1].ToString();
					newRow[2] = rawTbl.Rows[i][0].ToString();
					TblReturn.Rows.Add(newRow);

					if (rawTbl.Rows[i][0].ToString() == _Guarantor.ToString()) //Set info in row that says Guarantor
					{
						TblReturn.Rows[1][1] = rawTbl.Rows[i][2].ToString() + ", " + rawTbl.Rows[i][1].ToString(); //Name
						TblReturn.Rows[1][2] = newRow[2].ToString(); //Number
					}


				}
			}
			return TblReturn;
		}

		private int[] CalcuateColumnWidths(DataTable source, Font f1, Graphics e)
		{
			int [] frval = new int[source.Columns.Count];
			if (source != null && source.Rows.Count != 0)
			{
				for (int i= 0 ; i<source.Columns.Count; i++)
					for (int j=0; j<source.Rows.Count; j++)
					frval[i] = (int) (frval[i] > e.MeasureString(source.Rows[j][i].ToString(),f1).Width ? frval[i] 
						: e.MeasureString(source.Rows[j][i].ToString(),f1).Width);

					
			}
			return frval;
		}
		private int[] CalcuateRowHeights(DataTable source, Font f1) //, Graphics e)
		{
			int CellPading = 1;
			int CellWalls = 1;	
			int[] frval = new int[source.Rows.Count];
			if (source != null && source.Rows.Count != 0)
			{
				for (int iRow = 0; iRow < source.Rows.Count; iRow++)
					for (int jCol = 0; jCol < source.Columns.Count; jCol++)
					{
						string[] splits = source.Rows[iRow][jCol].ToString().Split(new char[] { '\n' });
						int h1 = 0;
						if (splits != null)
							h1 = (int)(splits.Length * ((int)f1.GetHeight() + 1) + CellPading * 2 + CellWalls);
						frval[iRow] = (h1 > frval[iRow] ? h1 : frval[iRow]);
					}
			}
			return frval;
		}
		
		public override void DrawMe(System.Drawing.Graphics e)
		{
			Font f1 = new Font("Times New Roman", 10F);
			DataTable Source = GetTable();
			if (Source == null)
				return; // don't draw.
			DRAWCell dcell = new DRAWCell();
			DRAWText dtext = new DRAWText();
			_colWidths = CalcuateColumnWidths(Source, f1, e); // important to have set
			_rowHeights = CalcuateRowHeights(Source, f1); // e);
			int Y_CurrentRow = 0;
			Point TextOffset = new Point(2, 2); // 1 pixel padding, 1 pixel 
			// Row 1
			{
				//Col 1
				//dcell.SETBORDER_STYLE_ALL(BorderStyle.White);
				//dcell.CellSize = GetCellSize(Source, 0, 0, f1);
				//dcell.Location = new Point(0,0);
				//dcell.DrawMe(e); // Paint to e
				////Col 2
				//dcell.CellSize = GetCellSize(Source, 0, 1, f1);
				//dcell.Location = new Point(0,0);
				//dcell.DrawMe(e); // Paint to e
				////Col 3
				dcell.SETBORDER_STYLE_ALL(BorderStyle.BlackLine);
				dcell.CellSize = GetCellSize(Source, 0, 2, f1);
				dcell.Location = new Point((int)(_colWidths[0]+_colWidths[1]), Y_CurrentRow);
				dtext.Text = Source.Rows[0][2].ToString();
				dtext.Location = new Point( dcell.Location.X + TextOffset.X,dcell.Location.Y+TextOffset.Y);
				dtext.TextFont = f1;
				dcell.DrawMe(e);
				dtext.DrawMe(e);



			}

			// Remaining Rows
			dtext.TextFont = f1;
			Y_CurrentRow = dcell.Location.Y + dcell.CellSize.Height;
			
			for (int iRow = 1; iRow < Source.Rows.Count; iRow++)
			{
				for (int jCols = 0; jCols < Source.Columns.Count; jCols++)
				{
					dcell.CellSize = GetCellSize(Source, 0, 1, f1);
					dtext.Text = Source.Rows[iRow][jCols].ToString();
					dcell.Location.X = (int) (jCols ==0 ? 0:
						(jCols == 1 ? _colWidths[0] : _colWidths[0] + _colWidths[1]));
						//(int) GetCell_XOffset(iRow, jCols);
					dcell.Location.Y = Y_CurrentRow;
					dcell.CellSize = GetCellSize(Source,iRow, jCols, f1);
					dtext.Location = new Point(dcell.Location.X + TextOffset.X, dcell.Location.Y + TextOffset.Y);
					dcell.DrawMe(e);
					dtext.DrawMe(e);

				}
				Y_CurrentRow = dcell.Location.Y + dcell.CellSize.Height;
			}


		}

		private float GetCell_XOffset(int row, int col)
		{

			int CellPading = 1;
			int CellWalls = 1;
			//if (_colWidths == null)
			//    _colWidths = CalcuateColumnWidths(Source, f1, e);
			//LocationF.Y = row * f1.GetHeight() + CellPading * 2 + CellWalls;  // if height =10, 1 pixel above and below, Line above takes 1 pixel, but lower line overlaps with next draw.
			//Location.X = col * _colWidths[col] + CellPading * 2 + CellWalls;
			return col * _colWidths[col] + CellPading * 2 + CellWalls;
		}
		private Size GetCellSize(DataTable Source, int row, int col, Font f1)
		{
			Size srval = new Size();
			
			int CellPading = 1;
			int CellWalls = 1;			
			srval.Width = (int) _colWidths[col] +1; // will exception if _colWidths not set
			srval.Height = (Source.Rows[row][col].ToString().Split(new char[] { '\n' }).Length) * ((int) f1.GetHeight() + 1) + CellPading * 2 + CellWalls;
			return srval;
		}

	}
}
