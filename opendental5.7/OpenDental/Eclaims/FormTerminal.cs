using System;
using System.Diagnostics;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using RS232;

namespace OpenDental.Eclaims
{
	/// <summary>
	/// Summary description for FormTerminal.
	/// </summary>
	public class FormTerminal : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.TextBox textTx;
		private System.Windows.Forms.TextBox textRx;
		private System.Windows.Forms.TextBox textProgress;
		private System.Windows.Forms.Label label3;
		private Rs232 moRS232;
		///<summary>The character array of received bytes.</summary>
		private StringBuilder RxBuff;
		///<summary>CTRL-A. Start Of Header?</summary>
		private const char SOH=(char)1;
		///<summary>CTRL-D. End Of Transmission</summary>
		private const char EOT=(char)4;
		///<summary>CTRL-F. Positive ACKnowledgement</summary>
		private const char ACK=(char)6;
		///<summary>CTRL-U. Negative AcKnowledgement</summary>
		private const char NAK=(char)21;//
		///<summary>CTRL-X. CANcel</summary>
		private const char CAN=(char)24;

		/// <summary></summary>
		public FormTerminal()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			moRS232 = new Rs232();
			moRS232.Port=1;
			moRS232.BaudRate=9600;
			moRS232.DataBit=8;
			moRS232.StopBit=Rs232.DataStopBit.StopBit_1;
			moRS232.Parity=Rs232.DataParity.Parity_None;
			moRS232.Timeout=1500;
			moRS232.CommEvent+=new Rs232.CommEventHandler(moRS232_CommEvent);
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormTerminal));
			this.label1 = new System.Windows.Forms.Label();
			this.textTx = new System.Windows.Forms.TextBox();
			this.textRx = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.textProgress = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(25,0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(100,20);
			this.label1.TabIndex = 0;
			this.label1.Text = "Sent";
			this.label1.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// textTx
			// 
			this.textTx.Location = new System.Drawing.Point(25,22);
			this.textTx.Multiline = true;
			this.textTx.Name = "textTx";
			this.textTx.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textTx.Size = new System.Drawing.Size(452,28);
			this.textTx.TabIndex = 1;
			// 
			// textRx
			// 
			this.textRx.Location = new System.Drawing.Point(25,83);
			this.textRx.Multiline = true;
			this.textRx.Name = "textRx";
			this.textRx.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textRx.Size = new System.Drawing.Size(452,41);
			this.textRx.TabIndex = 3;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(25,61);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(100,20);
			this.label2.TabIndex = 2;
			this.label2.Text = "Received";
			this.label2.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// textProgress
			// 
			this.textProgress.Location = new System.Drawing.Point(25,157);
			this.textProgress.Multiline = true;
			this.textProgress.Name = "textProgress";
			this.textProgress.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textProgress.Size = new System.Drawing.Size(452,418);
			this.textProgress.TabIndex = 6;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(25,135);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(100,20);
			this.label3.TabIndex = 5;
			this.label3.Text = "Progress";
			this.label3.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// FormTerminal
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.ClientSize = new System.Drawing.Size(637,612);
			this.Controls.Add(this.textProgress);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.textRx);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.textTx);
			this.Controls.Add(this.label1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "FormTerminal";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Modem Terminal";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.FormTerminal_Closing);
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		private void button1_Click(object sender, System.EventArgs e) {
			

		}

		/// <summary></summary>
		public void OpenConnection(int port){
			moRS232.Port=port;
			moRS232.Open();
      moRS232.Dtr=true;
      moRS232.Rts=true;
			moRS232.EnableEvents();
			textProgress.Text+="Modem Connection Opened\r\n";
			RxBuff=new StringBuilder();
		}

		/// <summary></summary>
		public void CloseConnection(){
			textProgress.Text+="Modem Connection Closed\r\n";
			ScrollProgress();
			moRS232.Close();
		}

		/// <summary></summary>
		public void Dial(string phone){
			moRS232.PurgeBuffer(Rs232.PurgeBuffers.TxClear | Rs232.PurgeBuffers.RXClear);
			string str="ATDT"+phone+"\r\n";
			textTx.Text+=str;
			textProgress.Text+="Dialed: "+phone+"\r\n";
			moRS232.Write(str);
		}

		/// <summary></summary>
		public void Send(string str){
			moRS232.PurgeBuffer(Rs232.PurgeBuffers.TxClear | Rs232.PurgeBuffers.RXClear);
			str+="\r\n";
			textTx.Text+=str;
			textProgress.Text+="Sent: "+str;
			ScrollProgress();
			moRS232.Write(str);
		}

		///<summary></summary>
		public void Send(byte[] block){
			moRS232.PurgeBuffer(Rs232.PurgeBuffers.TxClear | Rs232.PurgeBuffers.RXClear);
			textTx.Text+="**block**\r\n";
			textProgress.Text+="Sent block\r\n";
			ScrollProgress();
			moRS232.Write(block);
		}

		///<summary></summary>
		public void Send(char singleChar){
			moRS232.PurgeBuffer(Rs232.PurgeBuffers.TxClear | Rs232.PurgeBuffers.RXClear);
			if(IsSpecialCode(singleChar)){
				textTx.Text+=DisplaySpecialCode(singleChar)+"\r\n";
				textProgress.Text+="Sent "+DisplaySpecialCode(singleChar)+"\r\n";
			}
			else{
				textTx.Text+=singleChar.ToString()+"\r\n";
				textProgress.Text+="Sent char "+singleChar.ToString()+"\r\n";
			}
			ScrollProgress();
			moRS232.Write(new byte[]{(byte)singleChar});
		}

		///<summary>Throws an exception if expected text not received within timeout period.</summary>
		public void WaitFor(string expectedText,double timeoutMS){
			if(timeoutMS>60000){
				throw new Exception("Not allowed to wait longer than 60 seconds");
			}
			textProgress.Text+="Waiting for '"+expectedText+"'\r\n";
			ScrollProgress();
			DateTime startTime=DateTime.Now;
			while(startTime.AddMilliseconds(timeoutMS)>DateTime.Now){
				if(RxBuff.ToString().IndexOf(expectedText)!=-1){
					textProgress.Text+="Receieved: "+expectedText+"'\r\n";
					ScrollProgress();
					return;
				}
				Application.DoEvents();
			}
			throw new Exception("Timed out waiting for "+expectedText
				+". Actual text received so far: '"+CharsToString(RxBuff)+"'");
		}

		///<summary>Throws an exception if expected text not received within timeout period. Returns the response received out of the given expectedReplies.</summary>
		public string WaitFor(string expectedText1,string expectedText2,double timeoutMS){
			if(timeoutMS>60000){
				throw new Exception("Not allowed to wait longer than 60 seconds");
			}
			textProgress.Text+="Waiting for '"+expectedText1
				+"' or '"+expectedText2+"'\r\n";
			ScrollProgress();
			//textProgress.Refresh();
			DateTime startTime=DateTime.Now;
			while(startTime.AddMilliseconds(timeoutMS)>DateTime.Now){
				if(RxBuff.ToString().IndexOf(expectedText1)!=-1){
					textProgress.Text+="Receieved: "+expectedText1+"'\r\n";
					ScrollProgress();
					return expectedText1;
				}
				if(RxBuff.ToString().IndexOf(expectedText2)!=-1){
					textProgress.Text+="Receieved: "+expectedText2+"'\r\n";
					ScrollProgress();
					return expectedText2;
				}
				Application.DoEvents();
			}
			throw new Exception("Timed out waiting for "+expectedText1
				+" or "+expectedText2
				+". Actual text received so far: '"+CharsToString(RxBuff)+"'");
		}

		///<summary>Throws an exception if expected byte not received within timeout period.</summary>
		public void WaitFor(byte expectedByte,double timeoutMS){
			if(timeoutMS>60000){
				throw new Exception("Not allowed to wait longer than 60 seconds");
			}
			textProgress.Text+="Waiting for byte "+expectedByte.ToString()
				+"\r\n";
			ScrollProgress();
			DateTime startTime=DateTime.Now;
			while(startTime.AddMilliseconds(timeoutMS)>DateTime.Now){
				for(int i=0;i<RxBuff.Length;i++){
					if(RxBuff[i]==(char)expectedByte){
						if(IsSpecialCode((char)expectedByte)){
							textProgress.Text+="Receieved expected byte: "
								+DisplaySpecialCode((char)expectedByte)+"'\r\n";
						}
						else{
							textProgress.Text+="Receieved expected byte: "+expectedByte.ToString()+"'\r\n";
						}
						ScrollProgress();
						return;
					}
				}
				Application.DoEvents();
			}
			throw new Exception("Timed out waiting for byte "+expectedByte.ToString()
				+". Actual text received so far: '"+CharsToString(RxBuff)+"'");
		}

		///<summary>Gets a single byte from the RxBuff. Throws exception if no byte received within the given time. Does not clear this byte from the RxBuff in any way.  That's up to the calling code.</summary>
		public byte GetOneByte(double timeoutMS){
			if(timeoutMS>60000){
				throw new Exception("Not allowed to wait longer than 60 seconds");
			}
			DateTime startTime=DateTime.Now;
			while(startTime.AddMilliseconds(timeoutMS)>DateTime.Now){
				if(RxBuff.Length>0){
					if(IsSpecialCode(RxBuff[RxBuff.Length-1])){
						textProgress.Text+="Receieved: byte "
							+DisplaySpecialCode(RxBuff[RxBuff.Length-1])+"\r\n";
					}
					else{
						textProgress.Text+="Receieved: byte "+RxBuff[RxBuff.Length-1].ToString()+"'\r\n";
					}
					ScrollProgress();
					return (byte)RxBuff[RxBuff.Length-1];
				}
				Application.DoEvents();
			}
			throw new Exception("Timed out.  No bytes received yet.");
		}

		///<summary>Gets a precise number of bytes from RxBuff. Throws an error if not received in time.  Also, make sure to clear RxBuff before and after using this function.</summary>
		public byte[] GetBytes(int numberOfBytes, double timeoutMS){
			if(timeoutMS>60000){
				throw new Exception("Not allowed to wait longer than 60 seconds");
			}
			DateTime startTime=DateTime.Now;
			while(startTime.AddMilliseconds(timeoutMS)>DateTime.Now){
				if(RxBuff.Length>=numberOfBytes){
					textProgress.Text+="Receieved block\r\n";
					ScrollProgress();
					byte[] retVal=new byte[numberOfBytes];
					for(int i=0;i<numberOfBytes;i++){
						retVal[i]=(byte)RxBuff[i];
					}
					return retVal;
				}
				Application.DoEvents();
			}
			throw new Exception("Timed out.  "+numberOfBytes.ToString()+" bytes not received yet.");
		}

		///<summary></summary>
		public void ClearRxBuff(){
			RxBuff=new StringBuilder();
		}

		///<summary>Receives all text within the given timespan.</summary>
		public string Receive(double timeoutMS){
			if(timeoutMS>20000){
				throw new Exception("Not allowed to wait longer than 20 seconds");
			}
			textProgress.Text+="Receiving...\r\n";
			ScrollProgress();
			DateTime startTime=DateTime.Now;
			while(startTime.AddMilliseconds(timeoutMS)>DateTime.Now){
				Application.DoEvents();
			}
			textProgress.Text+="Receieved: "+CharsToString(RxBuff)+"'\r\n";
			ScrollProgress();
			return CharsToString(RxBuff);
		}

		private void ScrollProgress(){
			textProgress.SelectionStart=textProgress.Text.Length;
			textProgress.ScrollToCaret();
		}

		///<summary>Converts the char array to a display string. Any of the 5 special chars are transformed into meaningful display strings.</summary>
		private string CharsToString(StringBuilder inputChars){
			StringBuilder strBuilder=new StringBuilder();
			for(int i=0;i<inputChars.Length;i++){
				if(IsSpecialCode(inputChars[i])){
					strBuilder.Append(DisplaySpecialCode(inputChars[i]));
				}
				else{
					strBuilder.Append(inputChars[i]);
				}
			}
			return strBuilder.ToString();
		}

		///<summary></summary>
		public void Pause(double ms){
			if(ms>20000){
				throw new Exception("Not allowed to pause longer than 20 seconds");
			}
			textProgress.Text+="Pausing for "+ms.ToString()+" ms\r\n";
			ScrollProgress();
			DateTime startTime=DateTime.Now;
			while(startTime.AddMilliseconds(ms)>DateTime.Now){
				Application.DoEvents();
			}
			textProgress.Text+="Done pausing\r\n";
			ScrollProgress();
			//textProgress.Refresh();
		}

		///<summary>Transmits a file using Xmodem protocol.</summary>
		public void UploadXmodem(string filePath){
			textProgress.Text+="Sending "+filePath+" by Xmodem\r\n";
			//divide file into 128 byte packets
			byte[][] bytes;
			using(FileStream fs=File.Open(filePath,FileMode.Open,FileAccess.Read)){
				int numberPackets=(int)Math.Ceiling((double)fs.Length/128);
				bytes=new byte[numberPackets][];
				byte[] buffer;//this will usually be 128 bytes long, except for the last loop
				for(int i=0;i<numberPackets;i++){
					buffer=new byte[128];
					fs.Read(buffer,0,128);
					bytes[i]=new byte[128];
					buffer.CopyTo(bytes[i],0);
				}
			}			
			//GetPacketNumber. If greater than 255, repeatedly subtract 256
			//1's complement = 255-packet#
			//checksum=sum of all bytes. If greater than 255, repeatedly subtract 256
			//Actual send:
			//wait for NAK:
			WaitFor((byte)NAK,50000);
			//if wait longer than given time, then throw timout exception
			byte[] block;
			byte packetNumber;
			byte response;
			for(int i=0;i<bytes.GetLength(0);i++){
			SendPacket:// (block):
				block=new byte[132];
				//1: SOH byte
				block[0]=(byte)SOH;
				//2: packet Number
				packetNumber=(byte)Math.IEEERemainder((double)i,(double)256);
				block[1]=packetNumber;
				//3: 1's complement of packet number
				block[2]=(byte)(255-packetNumber);
				//4: the packet
				bytes[i].CopyTo(block,3);
				//5: checksum
				block[131]=GetCheckSum(block);
				Send(block);
			GetResponse:
				ClearRxBuff();
				response=GetOneByte(40000);
				if(response==(byte)CAN){
					throw new Exception("Transfer cancelled by receiver");
				}
				else if(response==(byte)NAK){
					goto SendPacket;//resend
				}
				else if(response!=(byte)ACK){//if anything other than ACK received
					goto GetResponse;//get another byte
				}
				//Note: the gotos will not result in any infinite loops, because even in the worst case
				//scenario, the sender will give up and quit sending responses, resulting in a timeout.
			}
			//Once all blocks sent, sent EOT
			Send(EOT);
			//If receive NAK, send another EOT
			WaitFor((byte)NAK,40000);
			Send(EOT);
			//If receive ACK, then done.
			WaitFor((byte)ACK,40000);
			//Note.  Allowed to send a CAN byte (2 is better) between blocks to cancel upload
		}

		private byte GetCheckSum(byte[] input){
			byte retVal=0;
			for(int i=0;i<input.Length;i++){
				retVal+=input[i];
			}
			return (byte)Math.IEEERemainder((double)retVal,(double)256);
		}

		///<summary>Receives a file using Xmodem protocol.</summary>
		public void DownloadXmodem(string filePath){
			textProgress.Text+="Retrieving "+filePath+" by Xmodem\r\n";
			//send ACK
			int attempts=0;
			ClearRxBuff();
			while(attempts<5){
				attempts++;
				Send(ACK);
				try{
					GetOneByte(5000);
					break;
				}
				catch{
					continue;
				}
				//if no response within given time, then try sending another ACK
				//Send up to about 5 ACKs before giving up
			}
			byte packetNumber=0;
			byte[] block;
			while(true){//response){
				ClearRxBuff();
				//receive a block 132 bytes long
				block=GetBytes(132,30000);
				//1: verify SOH. If not present, send NAK.
				if(block[0]!=(byte)SOH){
					Send(NAK);
					continue;
				}
				//2: verify packet number incrementing correctly. If wrong, send CAN
				if(block[1]!=packetNumber){
					Send(CAN);
					
				}
				//3: verify 1's complement of packet number. If wrong, send CAN

				//4: temporarily store the packet of 128 bytes
				//5: validate the checksum.
				//If checksum correct, then add packet to the file. Send ACK
				//If incorrect, send NAK and prepare to receive this block again.
				//If EOT received instead of SOH, send NAK
				//If receive another EOT, send ACK
				



			}
			//transfer complete
			//Note: Allowed to to cancel at any time by sending a CAN byte (2 is better)

			/*
			byte response;
			for(int i=0;i<bytes.GetLength(0);i++){
			SendPacket:// (block):
				block=new byte[132];
				//1: SOH byte
				block[0]=(byte)SOH;
				//2: packet Number
				packetNumber=(byte)Math.IEEERemainder((double)i,(double)256);
				block[1]=packetNumber;
				//3: 1's complement of packet number
				block[2]=(byte)(255-packetNumber);
				//4: the packet
				bytes[i].CopyTo(block,3);
				//5: checksum
				block[131]=GetCheckSum(block);
				Send(block);
			GetResponse:*/
		}

		///<summary>Events raised when a communication event occurs.</summary>
		private void moRS232_CommEvent(Rs232 source,Rs232.EventMasks Mask){
			if((Mask & Rs232.EventMasks.RxChar) > 0){
				StringBuilder strBuilder=new StringBuilder();
				//loop through each new char and handle it.
				for(int i=0;i<source.InputStream.Length;i++){
					RxBuff.Append((char)source.InputStream[i]);
					if(IsSpecialCode((char)source.InputStream[i])){
						strBuilder.Append(DisplaySpecialCode((char)source.InputStream[i]));
					}
					else{
						strBuilder.Append((char)source.InputStream[i]);
					}
				}
				textRx.Text+=strBuilder.ToString();
			}
    }

		private bool IsSpecialCode(char inputChar){
			if((int)inputChar>=32){
				return false;
			}
			return true;
			/*
			if(inputChar==SOH
				|| inputChar==EOT
				|| inputChar==ACK
				|| inputChar==NAK
				|| inputChar==CAN)
			{
				return true;
			}
			return false;*/
		}

		///<summary>Test if IsSpecialCode first.  Then, this returns a string representation for display purposes only.</summary>
		private string DisplaySpecialCode(char inputChar){
			if(inputChar==SOH){
				return "<SOH>";
			}
			if(inputChar==EOT){
				return "<EOT>";
			}
			if(inputChar==ACK){
				return "<ACK>";
			}
			if(inputChar==NAK){
				return "<NAK>";
			}
			if(inputChar==CAN){
				return "<CAN>";
			}
			if((int)inputChar==10//Line Feed
				|| (int)inputChar==13)//Carriage Return
			{
				return inputChar.ToString();
			}
			if((int)inputChar<32){
				return "<"+((int)inputChar).ToString()+">";
			}
			return "";
		}

		///<summary>Converts any string to an acceptable format for modem ASCII. Converts to all caps and strips off all invalid characters. Optionally shortens the string to the specified length and/or makes sure the string is long enough by padding with spaces.</summary>
		public string Sout(string intputStr,int maxL,int minL){
			string retStr=intputStr.ToUpper();
			//Debug.Write(retStr+",");
			retStr=Regex.Replace(retStr,//replaces characters in this input string
				//Allowed: !"&'()+,-./;?=(space)
				"[^\\w!\"&'\\(\\)\\+,-\\./;\\?= ]",//[](any single char)^(that is not)\w(A-Z or 0-9) or one of the above chars.
				"");
			//retStr=Regex.Replace(retStr,"[_]","");//replaces _
			if(maxL!=-1){
				if(retStr.Length>maxL){
					retStr=retStr.Substring(0,maxL);
				}
			}
			if(minL!=-1){
				if(retStr.Length<minL){
					retStr=retStr.PadRight(minL,' ');
				}
			}
			//Debug.WriteLine(retStr);
			return retStr;
		}

		///<summary>Converts any string to an acceptable format for modem ASCII. Converts to all caps and strips off all invalid characters. Optionally shortens the string to the specified length and/or makes sure the string is long enough by padding with spaces.</summary>
		public string Sout(string str,int maxL){
			return Sout(str,maxL,-1);
		}

		///<summary>Converts any string to an acceptable format for modem ASCII. Converts to all caps and strips off all invalid characters. Optionally shortens the string to the specified length and/or makes sure the string is long enough by padding with spaces.</summary>
		public string Sout(string str){
			return Sout(str,-1,-1);
		}

		private void FormTerminal_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
			CloseConnection();
		}


	}
}

















