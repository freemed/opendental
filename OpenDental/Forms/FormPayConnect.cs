using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental {
	public partial class FormPayConnect:Form {

		private Payment PaymentCur;
		private Patient PatCur;
		private string amountInit;
		private PayConnectService.Status status;
		private MagstripCardParser parser=null;

		public FormPayConnect(Payment payment,Patient pat,string amount) {
			InitializeComponent();
			Lan.F(this);
			PaymentCur=payment;
			PatCur=pat;
			amountInit=amount;
		}

		private void FormPayConnect_Load(object sender,EventArgs e) {
			this.textNameOnCard.Text=PatCur.GetNameFL();
			this.textZipCode.Text=PatCur.Zip;
			this.textAmount.Text=amountInit;
		}

		private void textCardNumber_KeyPress(object sender,KeyPressEventArgs e) {
			if(String.IsNullOrEmpty(textCardNumber.Text)) {
				return;
			}
			if(textCardNumber.Text.StartsWith("%") && textCardNumber.Text.EndsWith("?") && e.KeyChar == 13) {
				e.Handled=true;
				ParseSwipedCard(textCardNumber.Text);
			}
		}

		private void ParseSwipedCard(string data) {
			Clear();
			try {
				parser=new MagstripCardParser(data);
			}
			catch(MagstripCardParseException) {
				MessageBox.Show(this,"Could not read card, please try again.","Card Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
			}
			if(parser!=null) {
				textCardNumber.Text=parser.AccountNumber;
				textExpDate.Text=parser.ExpirationMonth.ToString().PadLeft(2,'0')+(parser.ExpirationYear%10).ToString().PadLeft(2,'0');
				textNameOnCard.Text=parser.AccountName;
				GetNextControl(textNameOnCard,true).Focus();//Move forward to the next control in the tab order.
			}
		}

		private void Clear() {
			textCardNumber.Text="";
			textExpDate.Text="";
			textNameOnCard.Text="";
			textSecurityCode.Text="";
			textZipCode.Text="";
			textAmount.Text="";
		}

		///<summary>Only call after the form is closed and the DialogResult is DialogResult.OK.</summary>
		public string AmountCharged{
			get { return textAmount.Text; }
		}

		///<summary>Only call after the form is closed and the DialogResult is DialogResult.OK.</summary>
		public PayConnectService.Status PaymentStatus{
			get{ return status; }
		}

		private bool VerifyData(out int expYear,out int expMonth){
			expYear=0;
			expMonth=0;
			// Consider adding more advanced verification methods using PayConnect validation requests.
			if(textCardNumber.Text.Trim().Length<5){
				MsgBox.Show(this,"Invalid Card Number.");
				return false;
			}
			if(Regex.IsMatch(textExpDate.Text,@"^\d\d[/\- ]\d\d$")){//08/07 or 08-07 or 08 07
				expYear=Convert.ToInt32("20"+textExpDate.Text.Substring(3,2));
				expMonth=Convert.ToInt32(textExpDate.Text.Substring(0,2));
			}
			else if(Regex.IsMatch(textExpDate.Text,@"^\d{4}$")){//0807
				expYear=Convert.ToInt32("20"+textExpDate.Text.Substring(2,2));
				expMonth=Convert.ToInt32(textExpDate.Text.Substring(0,2));
			}  
			else {
			  MsgBox.Show(this,"Expiration format invalid.");
				return false;
			}
			if(textNameOnCard.Text.Trim()==""){
				MsgBox.Show(this,"Name On Card required.");
				return false;
			}
			if(!Regex.IsMatch(textAmount.Text,"^[0-9]+$") && !Regex.IsMatch(textAmount.Text,"^[0-9]*\\.[0-9]+$")){
				MsgBox.Show(this,"Invalid amount.");
				return false;
			}
			return true;
		}

		private void butOK_Click(object sender,EventArgs e) {
			Cursor=Cursors.WaitCursor;
			int expYear;
			int expMonth;
			if(!VerifyData(out expYear,out expMonth)){
				Cursor=Cursors.Default;
				return;
			}
			status=Bridges.PayConnect.ProcessCreditCard(PaymentCur.PayNum,Convert.ToDecimal(textAmount.Text),
				textCardNumber.Text,expYear,expMonth,textNameOnCard.Text,textSecurityCode.Text,textZipCode.Text,(parser!=null?parser.Track2:null));
			if(status.code!=0){//error in transaction
				Cursor=Cursors.Default;
				DialogResult=DialogResult.Cancel;
				return;
			}
			Cursor=Cursors.Default;
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

	}

	public class MagstripCardParser {

		private const char TRACK_SEPARATOR='?';
		private const char FIELD_SEPARATOR='^';
		private string _inputStripeStr;
		private string _track1Data;
		private string _track2Data;
		private string _track3Data;
		private bool _needsParsing;
		private bool _hasTrack1;
		private bool _hasTrack2;
		private bool _hasTrack3;
		private string _accountHolder;
		private string _firstName;
		private string _lastName;
		private string _accountNumber;
		private int _expMonth;
		private int _expYear;

		public MagstripCardParser(string trackString) {
			_inputStripeStr=trackString;
			_needsParsing=true;
			Parse();
		}

		#region Properties
		public bool HasTrack1 {
			get { return _hasTrack1; }
		}

		public bool HasTrack2 {
			get { return _hasTrack2; }
		}

		public bool HasTrack3 {
			get { return _hasTrack3; }
		}

		public string Track1 {
			get { return _track1Data; }
		}

		public string Track2 {
			get { return _track2Data; }
		}

		public string Track3 {
			get { return _track3Data; }
		}

		public string TrackData {
			get { return _track1Data+_track2Data+_track3Data; }
		}

		public string AccountName {
			get { return _accountHolder; }
		}

		public string FirstName {
			get { return _firstName; }
		}

		public string LastName {
			get { return _lastName; }
		}

		public string AccountNumber {
			get { return _accountNumber; }
		}

		public int ExpirationMonth {
			get { return _expMonth; }
		}

		public int ExpirationYear {
			get { return _expYear; }
		}
		#endregion

		protected void Parse() {
			if(!_needsParsing) {
				return;
			}
			try {
				//Example: Track 1 Data Only
				//%B1234123412341234^CardUser/John^030510100000019301000000877000000?
				//Key off of the presence of "^" but not "="
				//Example: Track 2 Data Only
				//;1234123412341234=0305101193010877?
				//Key off of the presence of "=" but not "^"
				//Determine the presence of special characters
				string[] tracks=_inputStripeStr.Split(new char[] { TRACK_SEPARATOR },StringSplitOptions.RemoveEmptyEntries);
				if(tracks.Length>0) {
					_hasTrack1=true;
					_track1Data=tracks[0];
				}
				if(tracks.Length>1) {
					_hasTrack2=true;
					_track2Data=tracks[1];
				}
				if(tracks.Length>2) {
					_hasTrack3=true;
					_track3Data=tracks[2];
				}
				if(_hasTrack1) {
					ParseTrack1();
				}
				if(_hasTrack2) {
					ParseTrack2();
				}
				if(_hasTrack3) {
					ParseTrack3();
				}
			}
			catch(MagstripCardParseException) {
				throw;
			}
			catch(Exception ex) {
				throw new MagstripCardParseException(ex);
			}
			_needsParsing=false;
		}

		private void ParseTrack1() {
			if(String.IsNullOrEmpty(_track1Data)) {
				throw new MagstripCardParseException("Track 1 data is empty.");
			}
			string[] parts=_track1Data.Split(new char[] { FIELD_SEPARATOR },StringSplitOptions.None);
			if(parts.Length!=3) {
				throw new MagstripCardParseException("Missing last field separator (^) in track 1 data.");
			}
			_accountNumber=CreditCardUtils.StripNonDigits(parts[0]);
			if(!String.IsNullOrEmpty(parts[1])) {
				_accountHolder=parts[1].Trim();
			}
			if(!String.IsNullOrEmpty(_accountHolder)) {
				int nameDelim=_accountHolder.IndexOf("/");
				if(nameDelim>-1) {
					_lastName=_accountHolder.Substring(0,nameDelim);
					_firstName=_accountHolder.Substring(nameDelim+1);
				}
			}
			//date format: YYMM
			string expDate=parts[2].Substring(0,4);
			_expYear=ParseExpireYear(expDate);
			_expMonth=ParseExpireMonth(expDate);
		}

		private void ParseTrack2() {
			if(String.IsNullOrEmpty(_track2Data)) {
				throw new MagstripCardParseException("Track 2 data is empty.");
			}
			if(_track2Data.StartsWith(";")) {
				_track2Data=_track2Data.Substring(1);
			}
			//may have already parsed this info out if track 1 data present
			if(String.IsNullOrEmpty(_accountNumber) || (_expMonth==0 || _expYear==0)) {
				//Track 2 only cards
				//Ex: ;1234123412341234=0305101193010877?
				int sepIndex=_track2Data.IndexOf('=');
				if(sepIndex<0) {
					throw new MagstripCardParseException("Invalid track 2 data.");
				}
				string[] parts=_track2Data.Split(new char[] { '=' },StringSplitOptions.RemoveEmptyEntries);
				if(parts.Length!=2) {
					throw new MagstripCardParseException("Missing field separator (=) in track 2 data.");
				}
				if(String.IsNullOrEmpty(_accountNumber)) {
					_accountNumber=CreditCardUtils.StripNonDigits(parts[0]);
				}
				if(_expMonth==0 || _expYear==0) {
					//date format: YYMM
					string expDate=parts[1].Substring(0,4);
					_expYear=ParseExpireYear(expDate);
					_expMonth=ParseExpireMonth(expDate);
				}
			}
		}

		private void ParseTrack3() {
			//not implemented
		}

		private int ParseExpireMonth(string s) {
			s=CreditCardUtils.StripNonDigits(s);
			if(!ValidateExpiration(s)) {
				return 0;
			}
			if(s.Length>4) {
				s=s.Substring(0,4);
			}
			return int.Parse(s.Substring(2,2));
		}

		private int ParseExpireYear(string s) {
			s=CreditCardUtils.StripNonDigits(s);
			if(!ValidateExpiration(s)) {
				return 0;
			}
			if(s.Length>4) {
				s=s.Substring(0,4);
			}
			int y=int.Parse(s.Substring(0,2));
			if(y>80) {
				y+=1900;
			}
			else {
				y+=2000;
			}
			return y;
		}

		private bool ValidateExpiration(string s) {
			if(String.IsNullOrEmpty(s)) {
				return false;
			}
			if(s.Length<4) {
				return false;
			}
			return true;
		}

	}

	static class CreditCardUtils {

		private static bool IsValidVisaNumber(string ccNum) {
			char[] number=ccNum.ToCharArray();
			int len=number.Length;
			if(len!=16 && len!=13) {
				return false;
			}
			if(number[0]!='4') {
				return false;
			}
			return CheckMOD10(ccNum);
		}

		private static bool IsValidMasterCardNumber(string ccNum) {
			char[] number=ccNum.ToCharArray();
			int len=number.Length;
			if(len!=16) {
				return false;
			}
			if(number[0]!='5' || number[1]=='0' || number[1]>'5') {
				return false;
			}
			return CheckMOD10(ccNum);
		}

		private static bool IsValidAmexNumber(string ccNum) {
			char[] number=ccNum.ToCharArray();
			int len=number.Length;
			if(len!=15) {
				return false;
			}
			if(number[0]!='3' || (number[1]!='4' && number[1]!='7')) {
				return false;
			}
			return CheckMOD10(ccNum);
		}

		private static bool IsValidDinersNumber(string ccNum) {
			char[] number=ccNum.ToCharArray();
			int len=number.Length;
			if(len!=14) {
				return false;
			}
			if(number[0]!='3' || (number[1]!='0' && number[1]!='6' && number[1]!='8') || number[1]=='0' && number[2]>'5') {
				return false;
			}
			return CheckMOD10(ccNum);
		}

		private static bool IsValidDiscoverNumber(string ccNum) {
			char[] number=ccNum.ToCharArray();
			int len=number.Length;
			if(len!=16) {
				return false;
			}
			if(number[0]!='6' || number[1]!='0' || number[2]!='1' || number[3]!='1') {
				return false;
			}
			return CheckMOD10(ccNum);
		}

		///<summary>Strips non-digit characters from a string. Returns the modified string, or null if 's' is null.</summary>
		public static string StripNonDigits(string s) {
			return StripNonDigits(s,new char[] { });
		}

		///<summary>Strips non-digit characters from a string. The variable s is the string to strip. The allowed array must contain characters that should not be stripped. Returns the modified string, or null if 's' is null.</summary>
		public static string StripNonDigits(string s,char[] allowed) {
			if(s==null) {
				return null;
			}
			StringBuilder buff=new StringBuilder(s);
			StripNonDigits(buff,allowed);
			return buff.ToString();
		}

		///<summary>Strips non-digit characters from a string. The variable s is the string to strip.</summary>
		public static void StripNonDigits(StringBuilder s) {
			StripNonDigits(s,new char[] { });
		}

		///<summary>Strips non-digit characters from a string. The variable s is the string to strip. The allowed array must contain the characters that should not be stripped.</summary>
		public static void StripNonDigits(StringBuilder s,char[] allowed) {
			for(int i=0;i<s.Length;i++) {
				if(!Char.IsDigit(s[i]) && !ContainsCharacter(s[i],allowed)) {
					s.Remove(i,1);
					i--;
				}
			}
		}

		///<summary>Searches a character array for the presence of the given character. Variable c is the character to search for. The search array is the array to search in. Returns true if the character is present in the array.  false otherwise.</summary>
		public static bool ContainsCharacter(char c,char[] search) {
			foreach(char x in search) {
				if(c==x) {
					return true;
				}
			}
			return false;
		}

		///<summary>Performs a MOD10 check against a string. This is the check that is used to validate credit card numbers, but can be used on other numbers, as well. The algorithm is: Starting with the least significant digit, sum all odd-numbered digits; separately, sum two times each even-numbered digit (if this is greater than 10, bring it into single-digit range by subtracting 9). Then add both totals and divide by 10. If there is no remainder then the value passes the check. The variable value is the value to check, which must be all digits. Returns true iff the value passes the MOD10 check.</summary>
		public static bool CheckMOD10(string value) {
			if(value==null) {
				throw new NullReferenceException("Value is null.");
			}
			value=StripNonDigits(value);
			int sum=0;
			int count=0;
			for(int i=value.Length-1;i>=0;i--) {
				count++;
				int digit=int.Parse(value.Substring(i,1));
				if((count%2)==1) {
					sum+=digit;
				}
				else {
					int tmp=digit*2;
					if(tmp>=10) {
						tmp-=9;
					}
					sum+=tmp;
				}
			}
			return ((sum%10)==0);
		}

		///<summary>Return bool if value passed in is numeric only</summary>
		public static bool IsNumeric(string str) {
			if(str==null) {
				return false;
			}
			Regex objNotWholePattern=new Regex("[^0-9]");
			return !objNotWholePattern.IsMatch(str);
		}

	}

	public class MagstripCardParseException:Exception {

		public MagstripCardParseException(Exception cause)
			: base(cause.Message,cause) {
		}

		public MagstripCardParseException(string msg)
			: base(msg) {
		}

		public MagstripCardParseException(string msg,Exception cause)
			: base(msg,cause) {
		}

	}
}