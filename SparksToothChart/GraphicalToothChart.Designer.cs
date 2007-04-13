namespace SparksToothChart {
	partial class GraphicalToothChart {
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GraphicalToothChart));
			this.pictBox = new System.Windows.Forms.PictureBox();
			((System.ComponentModel.ISupportInitialize)(this.pictBox)).BeginInit();
			this.SuspendLayout();
			// 
			// pictBox
			// 
			this.pictBox.Image = ((System.Drawing.Image)(resources.GetObject("pictBox.Image")));
			this.pictBox.Location = new System.Drawing.Point(0,0);
			this.pictBox.Name = "pictBox";
			this.pictBox.Size = new System.Drawing.Size(410,307);
			this.pictBox.TabIndex = 0;
			this.pictBox.TabStop = false;
			// 
			// GraphicalToothChart
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F,13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.pictBox);
			this.Name = "GraphicalToothChart";
			this.Size = new System.Drawing.Size(544,351);
			((System.ComponentModel.ISupportInitialize)(this.pictBox)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		

	}
}
