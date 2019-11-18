namespace wow_quest_to_speech {
	partial class SettingsApisForm {
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
			this.grpAzure = new System.Windows.Forms.GroupBox();
			this.comboAzureRegion = new System.Windows.Forms.ComboBox();
			this.txtAzureKey = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnSave = new System.Windows.Forms.Button();
			this.grpGoogle = new System.Windows.Forms.GroupBox();
			this.lblGoogle = new System.Windows.Forms.Label();
			this.grpAws = new System.Windows.Forms.GroupBox();
			this.comboAwsRegion = new System.Windows.Forms.ComboBox();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.txtAwsSecretKey = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.txtAwsAccessKey = new System.Windows.Forms.TextBox();
			this.grpAzure.SuspendLayout();
			this.grpGoogle.SuspendLayout();
			this.grpAws.SuspendLayout();
			this.SuspendLayout();
			// 
			// grpAzure
			// 
			this.grpAzure.Controls.Add(this.comboAzureRegion);
			this.grpAzure.Controls.Add(this.txtAzureKey);
			this.grpAzure.Controls.Add(this.label2);
			this.grpAzure.Controls.Add(this.label1);
			this.grpAzure.Location = new System.Drawing.Point(12, 12);
			this.grpAzure.Name = "grpAzure";
			this.grpAzure.Size = new System.Drawing.Size(280, 85);
			this.grpAzure.TabIndex = 0;
			this.grpAzure.TabStop = false;
			this.grpAzure.Text = "Microsoft Azure";
			// 
			// comboAzureRegion
			// 
			this.comboAzureRegion.FormattingEnabled = true;
			this.comboAzureRegion.Items.AddRange(new object[] {
            "westus",
            "westus2",
            "eastus",
            "eastus2",
            "eastasia",
            "southeastasia",
            "northeurope",
            "westeurope"});
			this.comboAzureRegion.Location = new System.Drawing.Point(134, 58);
			this.comboAzureRegion.Name = "comboAzureRegion";
			this.comboAzureRegion.Size = new System.Drawing.Size(140, 21);
			this.comboAzureRegion.TabIndex = 2;
			// 
			// txtAzureKey
			// 
			this.txtAzureKey.Location = new System.Drawing.Point(6, 32);
			this.txtAzureKey.Name = "txtAzureKey";
			this.txtAzureKey.Size = new System.Drawing.Size(268, 20);
			this.txtAzureKey.TabIndex = 1;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(6, 61);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(112, 13);
			this.label2.TabIndex = 0;
			this.label2.Text = "API Endpoint (Region)";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(3, 16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(45, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "API Key";
			// 
			// btnCancel
			// 
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnCancel.Location = new System.Drawing.Point(102, 304);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(84, 28);
			this.btnCancel.TabIndex = 38;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			// 
			// btnSave
			// 
			this.btnSave.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnSave.Location = new System.Drawing.Point(192, 304);
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(100, 28);
			this.btnSave.TabIndex = 39;
			this.btnSave.Text = "Save";
			this.btnSave.UseVisualStyleBackColor = true;
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			// 
			// grpGoogle
			// 
			this.grpGoogle.Controls.Add(this.lblGoogle);
			this.grpGoogle.Location = new System.Drawing.Point(12, 103);
			this.grpGoogle.Name = "grpGoogle";
			this.grpGoogle.Size = new System.Drawing.Size(280, 65);
			this.grpGoogle.TabIndex = 40;
			this.grpGoogle.TabStop = false;
			this.grpGoogle.Text = "Google Cloud";
			// 
			// lblGoogle
			// 
			this.lblGoogle.Location = new System.Drawing.Point(6, 16);
			this.lblGoogle.Name = "lblGoogle";
			this.lblGoogle.Size = new System.Drawing.Size(268, 46);
			this.lblGoogle.TabIndex = 0;
			// 
			// grpAws
			// 
			this.grpAws.Controls.Add(this.comboAwsRegion);
			this.grpAws.Controls.Add(this.label4);
			this.grpAws.Controls.Add(this.label5);
			this.grpAws.Controls.Add(this.txtAwsSecretKey);
			this.grpAws.Controls.Add(this.label3);
			this.grpAws.Controls.Add(this.txtAwsAccessKey);
			this.grpAws.Location = new System.Drawing.Point(12, 174);
			this.grpAws.Name = "grpAws";
			this.grpAws.Size = new System.Drawing.Size(280, 124);
			this.grpAws.TabIndex = 41;
			this.grpAws.TabStop = false;
			this.grpAws.Text = "Amazon AWS";
			// 
			// comboAwsRegion
			// 
			this.comboAwsRegion.FormattingEnabled = true;
			this.comboAwsRegion.Items.AddRange(new object[] {
            "ap-east-1",
            "ap-northeast-1",
            "ap-northeast-2",
            "ap-northeast-3",
            "ap-south-1",
            "ap-southest-1",
            "ap-southest-2",
            "ca-central-1",
            "cn-north-1",
            "cn-north-west-1",
            "eu-central-1",
            "eu-north-1",
            "eu-west-1",
            "eu-west-2",
            "eu-west-3",
            "me-south-1",
            "sa-east-1",
            "us-east-1",
            "us-east-2",
            "us-west-1",
            "us-west-2",
            "us-gov-cloud-east-1",
            "us-gov-cloud-west1"});
			this.comboAwsRegion.Location = new System.Drawing.Point(134, 97);
			this.comboAwsRegion.Name = "comboAwsRegion";
			this.comboAwsRegion.Size = new System.Drawing.Size(140, 21);
			this.comboAwsRegion.TabIndex = 2;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(3, 55);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(79, 13);
			this.label4.TabIndex = 0;
			this.label4.Text = "API Secret Key";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(6, 100);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(112, 13);
			this.label5.TabIndex = 0;
			this.label5.Text = "API Endpoint (Region)";
			// 
			// txtAwsSecretKey
			// 
			this.txtAwsSecretKey.Location = new System.Drawing.Point(6, 71);
			this.txtAwsSecretKey.Name = "txtAwsSecretKey";
			this.txtAwsSecretKey.Size = new System.Drawing.Size(268, 20);
			this.txtAwsSecretKey.TabIndex = 1;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(3, 16);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(83, 13);
			this.label3.TabIndex = 0;
			this.label3.Text = "API Access Key";
			// 
			// txtAwsAccessKey
			// 
			this.txtAwsAccessKey.Location = new System.Drawing.Point(6, 32);
			this.txtAwsAccessKey.Name = "txtAwsAccessKey";
			this.txtAwsAccessKey.Size = new System.Drawing.Size(268, 20);
			this.txtAwsAccessKey.TabIndex = 1;
			// 
			// SettingsApisForm
			// 
			this.AcceptButton = this.btnSave;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(304, 344);
			this.Controls.Add(this.grpAws);
			this.Controls.Add(this.grpGoogle);
			this.Controls.Add(this.grpAzure);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnSave);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "SettingsApisForm";
			this.ShowIcon = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "API Settings";
			this.grpAzure.ResumeLayout(false);
			this.grpAzure.PerformLayout();
			this.grpGoogle.ResumeLayout(false);
			this.grpAws.ResumeLayout(false);
			this.grpAws.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox grpAzure;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.TextBox txtAzureKey;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox comboAzureRegion;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button btnSave;
		private System.Windows.Forms.GroupBox grpGoogle;
		private System.Windows.Forms.Label lblGoogle;
		private System.Windows.Forms.GroupBox grpAws;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox txtAwsAccessKey;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox txtAwsSecretKey;
		private System.Windows.Forms.ComboBox comboAwsRegion;
		private System.Windows.Forms.Label label5;
	}
}