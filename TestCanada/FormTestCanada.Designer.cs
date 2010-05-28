namespace TestCanada {
	partial class FormTestCanada {
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

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormTestCanada));
			this.butObjects = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.butNewDb = new System.Windows.Forms.Button();
			this.butClear = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.butProcedures = new System.Windows.Forms.Button();
			this.label3 = new System.Windows.Forms.Label();
			this.butScripts = new System.Windows.Forms.Button();
			this.textResults = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// butObjects
			// 
			this.butObjects.Location = new System.Drawing.Point(12,64);
			this.butObjects.Name = "butObjects";
			this.butObjects.Size = new System.Drawing.Size(87,23);
			this.butObjects.TabIndex = 10;
			this.butObjects.Text = "+ Objects";
			this.butObjects.UseVisualStyleBackColor = true;
			this.butObjects.Click += new System.EventHandler(this.butObjects_Click);
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(106,14);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(505,18);
			this.label2.TabIndex = 9;
			this.label2.Text = "The scripts are all designed so that this will not normally be necessary except f" +
    "or new versions.";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// butNewDb
			// 
			this.butNewDb.Location = new System.Drawing.Point(12,12);
			this.butNewDb.Name = "butNewDb";
			this.butNewDb.Size = new System.Drawing.Size(87,23);
			this.butNewDb.TabIndex = 8;
			this.butNewDb.Text = "Fresh Db";
			this.butNewDb.UseVisualStyleBackColor = true;
			this.butNewDb.Click += new System.EventHandler(this.butNewDb_Click);
			// 
			// butClear
			// 
			this.butClear.Location = new System.Drawing.Point(12,38);
			this.butClear.Name = "butClear";
			this.butClear.Size = new System.Drawing.Size(87,23);
			this.butClear.TabIndex = 11;
			this.butClear.Text = "Clear";
			this.butClear.UseVisualStyleBackColor = true;
			this.butClear.Click += new System.EventHandler(this.butClear_Click);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(106,66);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(505,18);
			this.label1.TabIndex = 12;
			this.label1.Text = "Dentists, Carriers, Patients, InsPlans.";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// butProcedures
			// 
			this.butProcedures.Location = new System.Drawing.Point(12,90);
			this.butProcedures.Name = "butProcedures";
			this.butProcedures.Size = new System.Drawing.Size(87,23);
			this.butProcedures.TabIndex = 13;
			this.butProcedures.Text = "+ Procedures";
			this.butProcedures.UseVisualStyleBackColor = true;
			this.butProcedures.Click += new System.EventHandler(this.butProcedures_Click);
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(105,118);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(650,31);
			this.label3.TabIndex = 13;
			this.label3.Text = resources.GetString("label3.Text");
			// 
			// butScripts
			// 
			this.butScripts.Location = new System.Drawing.Point(12,116);
			this.butScripts.Name = "butScripts";
			this.butScripts.Size = new System.Drawing.Size(87,23);
			this.butScripts.TabIndex = 15;
			this.butScripts.Text = "+ Scripts";
			this.butScripts.UseVisualStyleBackColor = true;
			this.butScripts.Click += new System.EventHandler(this.butScripts_Click);
			// 
			// textResults
			// 
			this.textResults.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textResults.Location = new System.Drawing.Point(12,149);
			this.textResults.Multiline = true;
			this.textResults.Name = "textResults";
			this.textResults.Size = new System.Drawing.Size(759,456);
			this.textResults.TabIndex = 16;
			// 
			// FormTestCanada
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F,13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(783,617);
			this.Controls.Add(this.textResults);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.butScripts);
			this.Controls.Add(this.butProcedures);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.butClear);
			this.Controls.Add(this.butObjects);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.butNewDb);
			this.Name = "FormTestCanada";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "FormTestCanada";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button butObjects;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button butNewDb;
		private System.Windows.Forms.Button butClear;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button butProcedures;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Button butScripts;
		private System.Windows.Forms.TextBox textResults;
	}
}

