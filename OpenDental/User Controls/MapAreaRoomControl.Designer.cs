namespace OpenDental {
	partial class MapAreaRoomControl {
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if(disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			this.components = new System.ComponentModel.Container();
			this.timerFlash = new System.Windows.Forms.Timer(this.components);
			this.SuspendLayout();
			// 
			// timerFlash
			// 
			this.timerFlash.Interval = 300;
			this.timerFlash.Tick += new System.EventHandler(this.timerFlash_Tick);
			// 
			// MapAreaRoomControl
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.DoubleBuffered = true;
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Name = "MapAreaRoomControl";
			this.Size = new System.Drawing.Size(180, 163);
			this.Paint += new System.Windows.Forms.PaintEventHandler(this.MapAreaRoomControl_Paint);
			this.DoubleClick += new System.EventHandler(this.MapAreaRoomControl_DoubleClick);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Timer timerFlash;


	}
}
