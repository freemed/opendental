using System;
using System.Collections;
using System.Data;
using OpenDentBusiness.DataAccess;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.IO;
using Microsoft.Win32;
using System.Runtime.InteropServices;
using System.Reflection;
using System.Windows.Forms;
using MySql.Data.MySqlClient;


namespace OpenDentBusiness {

	///<summary></summary>
	public class AnestheticRecords {
		///<summary>List of all anesthetic records for the current patient.</summary>
		public static AnestheticRecord[] List;

		///<summary>This is the connection that is used by the data adapter for all queries.</summary>
		private static MySqlConnection con;

		//variables for printing functionality
		public PrintDialog printDialog;
		public System.IO.Stream streamToPrint;
		public FileStream fileStream;
		public PrintDocument printDocument;
		public IntPtr thisHandle;
		public string streamType;
		[System.Runtime.InteropServices.DllImportAttribute("gdi32.dll")]
		public static extern bool BitBlt(
			IntPtr hdcDest, // handle to destination DC
			int nXDest, // x-coord of destination upper-left corner
			int nYDest, // y-coord of destination upper-left corner
			int nWidth, // width of destination rectangle
			int nHeight, // height of destination rectangle
			IntPtr hdcSrc, // handle to source DC
			int nXSrc, // x-coordinate of source upper-left corner
			int nYSrc, // y-coordinate of source upper-left corner
			System.Int32 dwRop); // raster operation code

		///<summary>Most recent date *first*. </summary>
		public static void Refresh(int patNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),patNum);
				return;
			}
			string command =
				"SELECT * FROM anestheticrecord"
				+ " WHERE PatNum = '" + patNum.ToString() + "'"
				+ " ORDER BY anestheticrecord.AnestheticDate DESC";
			DataTable table = Db.GetTable(command);
			List = new AnestheticRecord[table.Rows.Count];
			for(int i = 0;i < table.Rows.Count;i++) {
				List[i] = new AnestheticRecord();
				List[i].AnestheticRecordNum = PIn.PInt(table.Rows[i][0].ToString());
				List[i].PatNum = PIn.PInt(table.Rows[i][1].ToString());
				List[i].AnestheticDate = PIn.PDateT(table.Rows[i][2].ToString());
				List[i].ProvNum = PIn.PInt(table.Rows[i][3].ToString());
			}

		}
		public AnestheticRecords Copy() {
			//No need to check RemotingRole; no call to db.
			return (AnestheticRecords)this.MemberwiseClone();
		}
		///<summary></summary>
		///
		public static void Update(AnestheticRecord Cur) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),Cur);
				return;
			}
			string command = "UPDATE anestheticrecord SET "
				+ "PatNum = '" + POut.PInt(Cur.PatNum) + "'"
				+ ",AnestheticDate = " + POut.PDateT(Cur.AnestheticDate) + "'"
				+ ",ProvNum = '" + POut.PInt(Cur.ProvNum) + "'"
				+ " WHERE AnestheticRecordNum = '" + POut.PInt(Cur.AnestheticRecordNum) + "'";
			Db.NonQ(command);
		}

		///<summary>Creates a new AnestheticRecord in the db</summary>
		public static void Insert(AnestheticRecord Cur) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),Cur);
				return;
			}
			if(PrefC.RandomKeys) {
				Cur.AnestheticRecordNum = MiscData.GetKey("anestheticrecord","AnestheticRecordNum");
			}
			string command = "INSERT INTO anestheticrecord (";
			if(PrefC.RandomKeys) {
				command += "AnestheticRecordNum,";
			}
			command += "PatNum,AnestheticDate,ProvNum"
				+ ") VALUES(";
			if(PrefC.RandomKeys) {
				command += "'" + POut.PInt(Cur.AnestheticRecordNum) + "', ";
			}
			command +=
				"'" + POut.PInt(Cur.PatNum) + "', "
				+ POut.PDateT(Cur.AnestheticDate) + ", "
				+ "'" + POut.PInt(Cur.ProvNum) + "')";
			if(PrefC.RandomKeys) {
				Db.NonQ(command);
			}
			else {
				Cur.AnestheticRecordNum = Db.NonQ(command,true);
			}
		}

		///<summary>Creates a corresponding AnestheticData record in the db</summary>
		public static void InsertAnestheticData(AnestheticRecord Cur) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),Cur);
				return;
			}
			if(PrefC.RandomKeys) {
				Cur.AnestheticRecordNum = MiscData.GetKey("anestheticrecord","AnestheticRecordNum");
			}
			string command = "INSERT INTO anestheticdata (";
			if(PrefC.RandomKeys) {
				command += "AnestheticRecordNum,";
			}
			command += "AnestheticRecordNum"
				+ ") VALUES(";
			if(PrefC.RandomKeys) {
				command += "'" + POut.PInt(Cur.AnestheticRecordNum) + "', ";
			}
			command +=
				"" + POut.PInt(Cur.AnestheticRecordNum) + ")";
			if(PrefC.RandomKeys) {
				Db.NonQ(command);
			}
			else {
				Cur.AnestheticRecordNum = Db.NonQ(command,true);
			}
		}
		///<summary>Deletes an Anesthetic Record and the corresponding Anesthetic Data</summary>
		public static void Delete(AnestheticRecord Cur) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),Cur);
				return;
			}
			string command = "DELETE FROM anestheticrecord WHERE AnestheticRecordNum = '" + Cur.AnestheticRecordNum.ToString() + "'";
			Db.NonQ(command);
			string command2 = "DELETE FROM anestheticdata WHERE AnestheticRecordNum = '" + Cur.AnestheticRecordNum.ToString() + "'";
			Db.NonQ(command2);
		}

		/// <summary>/// Gets the Anesthetic Record number from the anestheticrecord table./// </summary>

		public static int GetRecordNum(int patnum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetInt(MethodBase.GetCurrentMethod(),patnum);
			}
			MySqlCommand cmd = new MySqlCommand();
			con = new MySqlConnection(DataSettings.ConnectionString);
			cmd.Connection = con;
			if(con.State == ConnectionState.Open)
				con.Close();
			con.Open();
			cmd.CommandText = "SELECT AnestheticRecordNum FROM anestheticrecord WHERE PatNum = '" + patnum.ToString() + "'";    /*"SELECT Max(AnestheticRecordNum) FROM anestheticrecord a, patient p where a.Patnum = p.Patnum and p.patnum = " + patnum + "";*/
			cmd.Connection = con;
			int anestheticRecordNum = Convert.ToInt32(cmd.ExecuteScalar());
			con.Close();
			return anestheticRecordNum;

		}

		/// <summary>/// Returns the date shown in the listAnesthetic.SelectedItem so it can be used to pull the correct AnestheticRecordCur from the db/// </summary>

		public static int GetRecordNumByDate(string AnestheticDateCur) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetInt(MethodBase.GetCurrentMethod(),AnestheticDateCur);
			}
			DateTime anestheticDate = Convert.ToDateTime(AnestheticDateCur);
			//need to format so it matches DateTime format as that's what's in the db; yyyy/MM/dd hh:mm:ss tt is what's displayed in listAnesthetic.SelectedItem
			string newdate = anestheticDate.ToString("yyyy-MM-dd HH:mm:ss");

			MySqlCommand cmd = new MySqlCommand();
			con = new MySqlConnection(DataSettings.ConnectionString);
			cmd.Connection = con;
			if(con.State == ConnectionState.Open)
				con.Close();
			con.Open();
			cmd.CommandText = "SELECT AnestheticRecordNum FROM anestheticrecord WHERE AnestheticDate = '" + (newdate) + "'";
			cmd.Connection = con;
			int anestheticRecordNum = Convert.ToInt32(cmd.ExecuteScalar());
			con.Close();
			return anestheticRecordNum;

		}
		public AnestheticRecord GetAnestheticData(int anestheticRecordNum) {
			//No need to check RemotingRole; no call to db.
			AnestheticRecord retVal = null;
			for(int i = 0;i < List.Length;i++) {
				if(List[i].AnestheticRecordNum == anestheticRecordNum) {
					retVal = List[i].Copy();
				}
			}
			return retVal;
		}

		///<summary>Creates an Anesthesia Score record in the db</summary>
		public static void InsertAnesthScore(int AnestheticRecordNum,int QActivity,int QResp,int QCirc,int QConc,int QColor,int AnesthesiaScore,int DischAmb,int DischWheelChr,int DischAmbulance,int DischCondStable,int DischCondUnstable) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),AnestheticRecordNum,QActivity,QResp,QCirc,QConc,QColor,AnesthesiaScore,DischAmb,DischWheelChr,DischAmbulance,DischCondStable,DischCondUnstable);
				return;
			}
			string command = "INSERT INTO anesthscore(AnestheticRecordNum,QActivity,QResp,QCirc,QConc,QColor,AnesthesiaScore,DischAmb,DischWheelChr,DischAmbulance,DischCondStable,DischCondUnstable) VALUES('" + AnestheticRecordNum + "','" + QActivity + "','" + QResp + "','" + QCirc + "','" + QConc + "','" + QColor + "','" + AnesthesiaScore + "','" + DischAmb + "','" + DischWheelChr + "','" + DischAmbulance + "','" + DischCondStable + "','" + DischCondUnstable + "'" + ")";
			Db.NonQ(command);
		}

		public static void UpdateAnesthScore(int AnestheticRecordNum,int QActivity,int QResp,int QCirc,int QConc,int QColor,int AnesthesiaScore,int DischAmb,int DischWheelChr,int DischAmbulance,int DischCondStable,int DischCondUnstable) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),AnestheticRecordNum,QActivity,QResp,QCirc,QConc,QColor,AnesthesiaScore,DischAmb,DischWheelChr,DischAmbulance,DischCondStable,DischCondUnstable);
				return;
			}
			string command = "UPDATE anesthscore SET "
				+ "QActivity = '" + POut.PInt(QActivity) + "'"
				+ ",QResp = '" + POut.PInt(QResp) + "'"
				+ ",QCirc = '" + POut.PInt(QCirc) + "'"
				+ ",QConc = '" + POut.PInt(QConc) + "'"
				+ ",QColor = '" + POut.PInt(QColor) + "'"
				+ ",AnesthesiaScore = '" + POut.PInt(AnesthesiaScore) + "'"
				+ ",DischAmb = '" + POut.PInt(DischAmb) + "'"
				+ ",DischWheelChr = '" + POut.PInt(DischWheelChr) + "'"
				+ ",DischAmbulance = '" + POut.PInt(DischAmbulance) + "'"
				+ ",DischCondStable = '" + POut.PInt(DischCondStable) + "'"
				+ ",DischCondUnstable = '" + POut.PInt(DischCondUnstable) + "'"
				+ " WHERE AnestheticRecordNum = '" + POut.PInt(AnestheticRecordNum) + "'";
			Db.NonQ(command);
		}

		public static int GetAnesthScore(int AnestheticRecordNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetInt(MethodBase.GetCurrentMethod(),AnestheticRecordNum);
			}
			MySqlCommand cmd = new MySqlCommand();
			con = new MySqlConnection(DataSettings.ConnectionString);
			cmd.Connection = con;
			if(con.State == ConnectionState.Open)
				con.Close();
			con.Open();
			cmd.CommandText = "SELECT AnesthesiaScore FROM anesthscore WHERE AnestheticRecordNum = '" + AnestheticRecordNum + "'";
			cmd.Connection = con;
			int anesthScore = Convert.ToInt32(cmd.ExecuteScalar());
			con.Close();
			return anesthScore;
		}

		public static int GetScoreRecordNum(int AnestheticRecordNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetInt(MethodBase.GetCurrentMethod(),AnestheticRecordNum);
			}
			MySqlCommand cmd = new MySqlCommand();
			con = new MySqlConnection(DataSettings.ConnectionString);
			cmd.Connection = con;
			if(con.State == ConnectionState.Open)
				con.Close();
			con.Open();
			cmd.CommandText = "SELECT AnestheticRecordNum FROM anesthscore WHERE AnestheticRecordNum = '" + AnestheticRecordNum + "'";
			cmd.Connection = con;
			int anestheticRecordNum = Convert.ToInt32(cmd.ExecuteScalar());
			con.Close();
			if(anestheticRecordNum == 0) {
				return 0;
			}
			else return anestheticRecordNum;

		}

		public static string GetAnesthCloseTime(int AnestheticRecordNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetString(MethodBase.GetCurrentMethod(),AnestheticRecordNum);
			}
			MySqlCommand cmd = new MySqlCommand();
			con = new MySqlConnection(DataSettings.ConnectionString);
			cmd.Connection = con;
			if(con.State == ConnectionState.Open)
				con.Close();
			con.Open();
			cmd.CommandText = "SELECT AnesthClose FROM anestheticdata WHERE AnestheticRecordNum = '" + AnestheticRecordNum + "'";
			cmd.Connection = con;
			string anesthClose = Convert.ToString(cmd.ExecuteScalar());
			con.Close();
			return anesthClose;

		}

		public static string GetAnesthDate(int AnestheticRecordNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetString(MethodBase.GetCurrentMethod(),AnestheticRecordNum);
			}
			MySqlCommand cmd = new MySqlCommand();
			con = new MySqlConnection(DataSettings.ConnectionString);
			cmd.Connection = con;
			if(con.State == ConnectionState.Open)
				con.Close();
			con.Open();
			cmd.CommandText = "SELECT AnestheticDate FROM anestheticrecord WHERE AnestheticRecordNum = '" + AnestheticRecordNum + "'";
			cmd.Connection = con;
			string anestheticDate = Convert.ToString(cmd.ExecuteScalar());
			DateTime anesthDate = Convert.ToDateTime(anestheticDate);
			string anestheticdate = anesthDate.ToString("MM/dd/yyyy");
			con.Close();
			return anestheticdate;
		}

		public static int GetAnesthProvType(int ProvNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetInt(MethodBase.GetCurrentMethod(),ProvNum);
			}
			MySqlCommand cmd = new MySqlCommand();
			con = new MySqlConnection(DataSettings.ConnectionString);
			cmd.Connection = con;
			if(con.State == ConnectionState.Open)
				con.Close();
			con.Open();
			cmd.CommandText = "SELECT AnesthProvType FROM provider WHERE ProvNum = '" + ProvNum + "'";
			cmd.Connection = con;
			string provType = Convert.ToString(cmd.ExecuteScalar());
			int anesthProvType = Convert.ToInt32(provType);
			con.Close();
			return anesthProvType;
		}

		public static DataTable GetAnesthScoreTable(int anestheticRecordNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetTable(MethodBase.GetCurrentMethod(),anestheticRecordNum);
			}
			string command = "SELECT * "
				+ " FROM anesthscore"
				+ " WHERE AnestheticRecordNum = " + POut.PInt(anestheticRecordNum);
			DataTable table = Db.GetTable(command);
			return table;
		}

					public void printDocument_PrintPage(object sender, PrintPageEventArgs e){
				System.IO.StreamReader streamReader = new StreamReader(this.streamToPrint);
				System.Drawing.Image image = System.Drawing.Image.FromStream(this.streamToPrint);
				int x = e.MarginBounds.X;
				int y = e.MarginBounds.Y;
				int width = image.Width;
				int height = image.Height;
				if  ((width / e.MarginBounds.Width) > (height / e.MarginBounds.Height))
					{
						width =  image.Width * e.MarginBounds.Width /image.Width; 
						height = image.Height * e.MarginBounds.Width/image.Width; 
					}
				else
					{
						width = image.Width * e.MarginBounds.Width / image.Height;
						height = image.Height; //e.MarginBounds.Height;
					}
				System.Drawing.Rectangle destRect = new System.Drawing.Rectangle(x, y, width, height);
				e.Graphics.DrawImage(image, destRect, 0, 0, image.Width,image.Height,System.Drawing.GraphicsUnit.Pixel);
		}
		///<summary>Prints the contents of the active window</summary>
		public void PrintWindow(IntPtr thisHandle){
			string tempFile = Path.GetTempPath() + "PrintPage.jpg";
			CaptureWindowToFile(thisHandle, tempFile, System.Drawing.Imaging.ImageFormat.Jpeg) ;
			FileStream fileStream = new FileStream(tempFile, FileMode.Open, FileAccess.Read);
			StartPrint(fileStream,"Image");
			fileStream.Close();
			if (System.IO.File.Exists(tempFile)){
					System.IO.File.Delete(tempFile);
				}
		}

		public void StartPrint(Stream streamToPrint, string streamType) {
			printDocument = new PrintDocument();
			this.printDocument.PrintPage += new PrintPageEventHandler(printDocument_PrintPage);
			this.streamToPrint = streamToPrint;
			this.streamType = streamType;
			System.Windows.Forms.PrintDialog PrintDialog1 = new PrintDialog();
			PrintDialog1.AllowSomePages = true;
			PrintDialog1.ShowHelp = true;
			PrintDialog1.Document = printDocument;
			PrintDialog1.UseEXDialog = true; //needed because PrintDialog was not showing on 64 bit Vista systems
				if (PrintDialog1.ShowDialog() == DialogResult.OK){
						try {
								this.printDocument.Print();
							}
						catch {
								MessageBox.Show("That printer was not found. Please check connections or try another printer");
							}
					}
		}

		public static void CaptureWindowToFile(IntPtr handle, string filename, ImageFormat format) {
			Image img = CaptureWindow(handle);
			img.Save(filename,format);
		}

		public static Image CaptureWindow(IntPtr handle) {
			// get the hDC of the target window
			IntPtr hdcSrc = User32.GetWindowDC(handle);
			// get the size
			User32.RECT windowRect = new User32.RECT();
			User32.GetWindowRect(handle,ref windowRect);
			int width = windowRect.right - windowRect.left;
			int height = windowRect.bottom - windowRect.top;
			// create a device context we can copy to
			IntPtr hdcDest = GDI32.CreateCompatibleDC(hdcSrc);
			// create a bitmap we can copy it to,
			// using GetDeviceCaps to get the width/height
			IntPtr hBitmap = GDI32.CreateCompatibleBitmap(hdcSrc,width,height); 
			// select the bitmap object
			IntPtr hOld = GDI32.SelectObject(hdcDest,hBitmap);
			// bitblt over
			GDI32.BitBlt(hdcDest,0,0,width,height,hdcSrc,0,0,GDI32.SRCCOPY);
			// restore selection
			GDI32.SelectObject(hdcDest,hOld);
			// clean up 
			GDI32.DeleteDC(hdcDest);
			User32.ReleaseDC(handle,hdcSrc);
			// get a .NET image object for it
			Image img = Image.FromHbitmap(hBitmap);
			// free up the Bitmap object
			GDI32.DeleteObject(hBitmap);
				return img;
			}

			private class GDI32 {
				public const int SRCCOPY = 0x00CC0020; // BitBlt dwRop parameter
					[DllImport("gdi32.dll")]
						public static extern bool BitBlt(IntPtr hObject,int nXDest,int nYDest,
							int nWidth,int nHeight,IntPtr hObjectSource,
							int nXSrc,int nYSrc,int dwRop);
					[DllImport("gdi32.dll")]
						public static extern IntPtr CreateCompatibleBitmap(IntPtr hDC,int nWidth, 
							int nHeight);
					[DllImport("gdi32.dll")]
						public static extern IntPtr CreateCompatibleDC(IntPtr hDC);
					[DllImport("gdi32.dll")]
						public static extern bool DeleteDC(IntPtr hDC);
					[DllImport("gdi32.dll")]
						public static extern bool DeleteObject(IntPtr hObject);
					[DllImport("gdi32.dll")]
						public static extern IntPtr SelectObject(IntPtr hDC,IntPtr hObject);
				}

				private class User32 {
					[StructLayout(LayoutKind.Sequential)]
					
				public struct RECT {
					public int left;
					public int top;
					public int right;
					public int bottom;
				}
					[DllImport("user32.dll")]
				public static extern IntPtr GetDesktopWindow();
					[DllImport("user32.dll")]
				public static extern IntPtr GetWindowDC(IntPtr hWnd);
					[DllImport("user32.dll")]
				public static extern IntPtr ReleaseDC(IntPtr hWnd,IntPtr hDC);
					[DllImport("user32.dll")]
				public static extern IntPtr GetWindowRect(IntPtr hWnd,ref RECT rect);
			}
	
	}
}