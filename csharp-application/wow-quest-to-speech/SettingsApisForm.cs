using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace wow_quest_to_speech {
	public partial class SettingsApisForm : Form {
		public AzureAPIConfig AzureAPIConfig { get; set; }
		public AwsAPIConfig AwsAPIConfig { get; set; }

		public SettingsApisForm(AzureAPIConfig preAzureApiConfig, AwsAPIConfig preAwsApiConfig) {
			InitializeComponent();

			AzureAPIConfig = preAzureApiConfig;
			AwsAPIConfig = preAwsApiConfig;

			if (AzureAPIConfig != null) {
				txtAzureKey.Text = AzureAPIConfig.Key;
				comboAzureRegion.SelectedItem = AzureAPIConfig.Region;
			}

			if (AwsAPIConfig != null) {
				txtAwsAccessKey.Text = AwsAPIConfig.AccessKey;
				txtAwsSecretKey.Text = AwsAPIConfig.SecretKey;
				comboAwsRegion.SelectedItem = AwsAPIConfig.Region;
			}

			if (Environment.GetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS") == null) {
				lblGoogle.Text = "Environment variable \"GOOGLE_APPLICATION_CREDENTIALS\" not found";
				lblGoogle.ForeColor = Color.Red;
			} else if(!File.Exists(Environment.GetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS"))) {
				lblGoogle.Text = string.Format("Authentication file {0} not found", Environment.GetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS"));
				lblGoogle.ForeColor = Color.Red;
			} else {
				lblGoogle.Text = "Environment variable and authentication file found";
				lblGoogle.ForeColor = Color.Green;
			}
		}

		private void btnSave_Click(object sender, EventArgs e) {
			if (!string.IsNullOrWhiteSpace(txtAzureKey.Text) && comboAzureRegion.SelectedIndex != -1) {
				AzureAPIConfig = new AzureAPIConfig() {
					Key = txtAzureKey.Text.Trim(),
					Region = comboAzureRegion.SelectedItem.ToString()
				};
			} else {
				AzureAPIConfig = null;
			}
			
			if (!string.IsNullOrWhiteSpace(txtAwsAccessKey.Text) && !string.IsNullOrWhiteSpace(txtAwsSecretKey.Text) && comboAwsRegion.SelectedIndex != -1) {
				AwsAPIConfig = new AwsAPIConfig() {
					AccessKey = txtAwsAccessKey.Text.Trim(),
					SecretKey = txtAwsSecretKey.Text.Trim(),
					Region = comboAwsRegion.SelectedItem.ToString()
				};
			} else {
				AwsAPIConfig = null;
			}
		}
	}
}
