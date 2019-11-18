namespace wow_quest_to_speech {
	partial class SettingsVoicesForm {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if (disposing && (components != null)) {
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
			this.btnSave = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.listVoices = new System.Windows.Forms.ListView();
			this.columnHeaderName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeaderGender = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeaderModule = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeaderLangCode = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.comboLangCodes = new System.Windows.Forms.ComboBox();
			this.lblNumTotalSelected = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.btnTestVoice = new System.Windows.Forms.Button();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// btnSave
			// 
			this.btnSave.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnSave.Location = new System.Drawing.Point(330, 459);
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(100, 28);
			this.btnSave.TabIndex = 2;
			this.btnSave.Text = "Save";
			this.btnSave.UseVisualStyleBackColor = true;
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnCancel.Location = new System.Drawing.Point(240, 459);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(84, 28);
			this.btnCancel.TabIndex = 1;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			// 
			// listVoices
			// 
			this.listVoices.CheckBoxes = true;
			this.listVoices.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderName,
            this.columnHeaderGender,
            this.columnHeaderModule,
            this.columnHeaderLangCode});
			this.listVoices.FullRowSelect = true;
			this.listVoices.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.listVoices.HideSelection = false;
			this.listVoices.Location = new System.Drawing.Point(6, 65);
			this.listVoices.MultiSelect = false;
			this.listVoices.Name = "listVoices";
			this.listVoices.Size = new System.Drawing.Size(406, 370);
			this.listVoices.TabIndex = 0;
			this.listVoices.UseCompatibleStateImageBehavior = false;
			this.listVoices.View = System.Windows.Forms.View.Details;
			this.listVoices.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.listVoices_ItemChecked);
			// 
			// columnHeaderName
			// 
			this.columnHeaderName.Text = "Name";
			this.columnHeaderName.Width = 232;
			// 
			// columnHeaderGender
			// 
			this.columnHeaderGender.Text = "Gender";
			this.columnHeaderGender.Width = 75;
			// 
			// columnHeaderModule
			// 
			this.columnHeaderModule.Text = "Module";
			this.columnHeaderModule.Width = 86;
			// 
			// columnHeaderLangCode
			// 
			this.columnHeaderLangCode.Text = "LangCode";
			this.columnHeaderLangCode.Width = 0;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.comboLangCodes);
			this.groupBox1.Controls.Add(this.lblNumTotalSelected);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.listVoices);
			this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.groupBox1.Location = new System.Drawing.Point(12, 12);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(418, 441);
			this.groupBox1.TabIndex = 40;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Voices selection";
			// 
			// comboLangCodes
			// 
			this.comboLangCodes.FormattingEnabled = true;
			this.comboLangCodes.Location = new System.Drawing.Point(228, 15);
			this.comboLangCodes.Name = "comboLangCodes";
			this.comboLangCodes.Size = new System.Drawing.Size(184, 24);
			this.comboLangCodes.Sorted = true;
			this.comboLangCodes.TabIndex = 2;
			this.comboLangCodes.SelectedIndexChanged += new System.EventHandler(this.comboLangCodes_SelectedIndexChanged);
			// 
			// lblNumTotalSelected
			// 
			this.lblNumTotalSelected.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblNumTotalSelected.Location = new System.Drawing.Point(225, 47);
			this.lblNumTotalSelected.Name = "lblNumTotalSelected";
			this.lblNumTotalSelected.Size = new System.Drawing.Size(187, 16);
			this.lblNumTotalSelected.TabIndex = 1;
			this.lblNumTotalSelected.Text = "(Total selected 0)";
			this.lblNumTotalSelected.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(6, 46);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(128, 16);
			this.label2.TabIndex = 1;
			this.label2.Text = "Select voices to use";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(6, 18);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(123, 16);
			this.label1.TabIndex = 1;
			this.label1.Text = "Show by Language";
			// 
			// btnTestVoice
			// 
			this.btnTestVoice.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnTestVoice.Location = new System.Drawing.Point(12, 459);
			this.btnTestVoice.Name = "btnTestVoice";
			this.btnTestVoice.Size = new System.Drawing.Size(100, 28);
			this.btnTestVoice.TabIndex = 37;
			this.btnTestVoice.TabStop = false;
			this.btnTestVoice.Text = "Test voice";
			this.btnTestVoice.UseVisualStyleBackColor = true;
			this.btnTestVoice.Click += new System.EventHandler(this.btnTestVoice_Click);
			// 
			// SettingsVoicesForm
			// 
			this.AcceptButton = this.btnSave;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(442, 499);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.btnTestVoice);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnSave);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "SettingsVoicesForm";
			this.ShowIcon = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Settings Voices";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SettingsVoicesForm_FormClosing);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.Button btnSave;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.ListView listVoices;
		private System.Windows.Forms.ColumnHeader columnHeaderName;
		private System.Windows.Forms.ColumnHeader columnHeaderGender;
		private System.Windows.Forms.ColumnHeader columnHeaderModule;
		private System.Windows.Forms.ColumnHeader columnHeaderLangCode;
		private System.Windows.Forms.Button btnTestVoice;
		private System.Windows.Forms.ComboBox comboLangCodes;
		private System.Windows.Forms.Label lblNumTotalSelected;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
	}
}