namespace wow_quest_to_speech {
	partial class MainForm {
		/// <summary>
		/// Erforderliche Designervariable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Verwendete Ressourcen bereinigen.
		/// </summary>
		/// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
		protected override void Dispose(bool disposing) {
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Vom Windows Form-Designer generierter Code

		/// <summary>
		/// Erforderliche Methode für die Designerunterstützung.
		/// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
		/// </summary>
		private void InitializeComponent() {
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.trayIcon = new System.Windows.Forms.NotifyIcon(this.components);
			this.lblVersion = new System.Windows.Forms.Label();
			this.linkGithub = new System.Windows.Forms.LinkLabel();
			this.grpModules = new System.Windows.Forms.GroupBox();
			this.checkModuleGoogle = new System.Windows.Forms.CheckBox();
			this.checkModuleAzure = new System.Windows.Forms.CheckBox();
			this.checkModuleAws = new System.Windows.Forms.CheckBox();
			this.checkModuleWindows = new System.Windows.Forms.CheckBox();
			this.btnStartStop = new System.Windows.Forms.Button();
			this.btnSettingsVoices = new System.Windows.Forms.Button();
			this.btnSettingsApis = new System.Windows.Forms.Button();
			this.grpSettings = new System.Windows.Forms.GroupBox();
			this.grpActionLog = new System.Windows.Forms.GroupBox();
			this.txtActionLog = new System.Windows.Forms.TextBox();
			this.grpModules.SuspendLayout();
			this.grpSettings.SuspendLayout();
			this.grpActionLog.SuspendLayout();
			this.SuspendLayout();
			// 
			// trayIcon
			// 
			this.trayIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("trayIcon.Icon")));
			this.trayIcon.Text = "Quest-To-Speech";
			this.trayIcon.DoubleClick += new System.EventHandler(this.trayIcon_DoubleClick);
			// 
			// lblVersion
			// 
			this.lblVersion.AutoSize = true;
			this.lblVersion.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblVersion.Location = new System.Drawing.Point(11, 301);
			this.lblVersion.Name = "lblVersion";
			this.lblVersion.Size = new System.Drawing.Size(53, 12);
			this.lblVersion.TabIndex = 32;
			this.lblVersion.Text = "v0.1.0-beta";
			// 
			// linkGithub
			// 
			this.linkGithub.AutoSize = true;
			this.linkGithub.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.linkGithub.Location = new System.Drawing.Point(225, 301);
			this.linkGithub.Name = "linkGithub";
			this.linkGithub.Size = new System.Drawing.Size(70, 12);
			this.linkGithub.TabIndex = 33;
			this.linkGithub.TabStop = true;
			this.linkGithub.Text = "View on GitHub";
			this.linkGithub.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkGithub_LinkClicked);
			// 
			// grpModules
			// 
			this.grpModules.Controls.Add(this.checkModuleGoogle);
			this.grpModules.Controls.Add(this.checkModuleAzure);
			this.grpModules.Controls.Add(this.checkModuleAws);
			this.grpModules.Controls.Add(this.checkModuleWindows);
			this.grpModules.Location = new System.Drawing.Point(12, 12);
			this.grpModules.Name = "grpModules";
			this.grpModules.Size = new System.Drawing.Size(182, 120);
			this.grpModules.TabIndex = 34;
			this.grpModules.TabStop = false;
			this.grpModules.Text = "Enable Modules";
			// 
			// checkModuleGoogle
			// 
			this.checkModuleGoogle.AutoSize = true;
			this.checkModuleGoogle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.checkModuleGoogle.Location = new System.Drawing.Point(6, 71);
			this.checkModuleGoogle.Name = "checkModuleGoogle";
			this.checkModuleGoogle.Size = new System.Drawing.Size(110, 20);
			this.checkModuleGoogle.TabIndex = 0;
			this.checkModuleGoogle.Text = "Google Cloud";
			this.checkModuleGoogle.UseVisualStyleBackColor = true;
			this.checkModuleGoogle.CheckedChanged += new System.EventHandler(this.checkModuleGoogle_CheckedChanged);
			// 
			// checkModuleAzure
			// 
			this.checkModuleAzure.AutoSize = true;
			this.checkModuleAzure.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.checkModuleAzure.Location = new System.Drawing.Point(6, 45);
			this.checkModuleAzure.Name = "checkModuleAzure";
			this.checkModuleAzure.Size = new System.Drawing.Size(118, 20);
			this.checkModuleAzure.TabIndex = 0;
			this.checkModuleAzure.Text = "Microsoft Azure";
			this.checkModuleAzure.UseVisualStyleBackColor = true;
			this.checkModuleAzure.CheckedChanged += new System.EventHandler(this.checkModuleAzure_CheckedChanged);
			// 
			// checkModuleAws
			// 
			this.checkModuleAws.AutoSize = true;
			this.checkModuleAws.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.checkModuleAws.Location = new System.Drawing.Point(6, 97);
			this.checkModuleAws.Name = "checkModuleAws";
			this.checkModuleAws.Size = new System.Drawing.Size(110, 20);
			this.checkModuleAws.TabIndex = 0;
			this.checkModuleAws.Text = "Amazon AWS";
			this.checkModuleAws.UseVisualStyleBackColor = true;
			this.checkModuleAws.CheckedChanged += new System.EventHandler(this.checkModuleAws_CheckedChanged);
			// 
			// checkModuleWindows
			// 
			this.checkModuleWindows.AutoSize = true;
			this.checkModuleWindows.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.checkModuleWindows.Location = new System.Drawing.Point(6, 19);
			this.checkModuleWindows.Name = "checkModuleWindows";
			this.checkModuleWindows.Size = new System.Drawing.Size(82, 20);
			this.checkModuleWindows.TabIndex = 0;
			this.checkModuleWindows.Text = "Windows";
			this.checkModuleWindows.UseVisualStyleBackColor = true;
			this.checkModuleWindows.CheckedChanged += new System.EventHandler(this.checkModuleWindows_CheckedChanged);
			// 
			// btnStartStop
			// 
			this.btnStartStop.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnStartStop.Location = new System.Drawing.Point(200, 17);
			this.btnStartStop.Name = "btnStartStop";
			this.btnStartStop.Size = new System.Drawing.Size(92, 34);
			this.btnStartStop.TabIndex = 35;
			this.btnStartStop.Text = "Start";
			this.btnStartStop.UseVisualStyleBackColor = true;
			this.btnStartStop.Click += new System.EventHandler(this.btnStartStop_Click);
			// 
			// btnSettingsVoices
			// 
			this.btnSettingsVoices.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnSettingsVoices.Location = new System.Drawing.Point(6, 19);
			this.btnSettingsVoices.Name = "btnSettingsVoices";
			this.btnSettingsVoices.Size = new System.Drawing.Size(80, 23);
			this.btnSettingsVoices.TabIndex = 35;
			this.btnSettingsVoices.Text = "Voices";
			this.btnSettingsVoices.UseVisualStyleBackColor = true;
			this.btnSettingsVoices.Click += new System.EventHandler(this.btnSettingsVoices_Click);
			// 
			// btnSettingsApis
			// 
			this.btnSettingsApis.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnSettingsApis.Location = new System.Drawing.Point(6, 46);
			this.btnSettingsApis.Name = "btnSettingsApis";
			this.btnSettingsApis.Size = new System.Drawing.Size(80, 23);
			this.btnSettingsApis.TabIndex = 35;
			this.btnSettingsApis.Text = "APIs";
			this.btnSettingsApis.UseVisualStyleBackColor = true;
			this.btnSettingsApis.Click += new System.EventHandler(this.btnSettingsApis_Click);
			// 
			// grpSettings
			// 
			this.grpSettings.Controls.Add(this.btnSettingsApis);
			this.grpSettings.Controls.Add(this.btnSettingsVoices);
			this.grpSettings.Location = new System.Drawing.Point(200, 57);
			this.grpSettings.Name = "grpSettings";
			this.grpSettings.Size = new System.Drawing.Size(92, 75);
			this.grpSettings.TabIndex = 36;
			this.grpSettings.TabStop = false;
			this.grpSettings.Text = "Settings";
			// 
			// grpActionLog
			// 
			this.grpActionLog.Controls.Add(this.txtActionLog);
			this.grpActionLog.Location = new System.Drawing.Point(12, 138);
			this.grpActionLog.Name = "grpActionLog";
			this.grpActionLog.Size = new System.Drawing.Size(280, 160);
			this.grpActionLog.TabIndex = 37;
			this.grpActionLog.TabStop = false;
			this.grpActionLog.Text = "Action log";
			// 
			// txtActionLog
			// 
			this.txtActionLog.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtActionLog.Font = new System.Drawing.Font("Consolas", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtActionLog.Location = new System.Drawing.Point(6, 19);
			this.txtActionLog.Multiline = true;
			this.txtActionLog.Name = "txtActionLog";
			this.txtActionLog.ReadOnly = true;
			this.txtActionLog.Size = new System.Drawing.Size(268, 135);
			this.txtActionLog.TabIndex = 0;
			this.txtActionLog.TextChanged += new System.EventHandler(this.txtSpeechLog_TextChanged);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(304, 319);
			this.Controls.Add(this.grpActionLog);
			this.Controls.Add(this.grpSettings);
			this.Controls.Add(this.btnStartStop);
			this.Controls.Add(this.grpModules);
			this.Controls.Add(this.linkGithub);
			this.Controls.Add(this.lblVersion);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.Name = "MainForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Quest-to-Speech";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.WTSMainForm_FormClosing);
			this.Load += new System.EventHandler(this.WTSMainForm_Load);
			this.Resize += new System.EventHandler(this.WTSMainForm_Resize);
			this.grpModules.ResumeLayout(false);
			this.grpModules.PerformLayout();
			this.grpSettings.ResumeLayout(false);
			this.grpActionLog.ResumeLayout(false);
			this.grpActionLog.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.NotifyIcon trayIcon;
		private System.Windows.Forms.Label lblVersion;
		private System.Windows.Forms.LinkLabel linkGithub;
		private System.Windows.Forms.GroupBox grpModules;
		private System.Windows.Forms.CheckBox checkModuleGoogle;
		private System.Windows.Forms.CheckBox checkModuleAzure;
		private System.Windows.Forms.CheckBox checkModuleAws;
		private System.Windows.Forms.CheckBox checkModuleWindows;
		private System.Windows.Forms.Button btnStartStop;
		private System.Windows.Forms.Button btnSettingsVoices;
		private System.Windows.Forms.Button btnSettingsApis;
		private System.Windows.Forms.GroupBox grpSettings;
		private System.Windows.Forms.GroupBox grpActionLog;
		private System.Windows.Forms.TextBox txtActionLog;
	}
}

