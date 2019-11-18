using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.ListView;

namespace wow_quest_to_speech {
	public static class Prompt {
		public static string InputDialog(string dialogCaption, string inputDescription, string initialValue) {
			Label label1 = new Label();
			TextBox txtText = new TextBox();
			Button btnSubmit = new Button();
			Button btnCancel = new Button();

			Form prompt = new Form() {
				Name = "PromptInputDialog",
				Text = dialogCaption,
				StartPosition = FormStartPosition.CenterScreen,
				FormBorderStyle = FormBorderStyle.FixedToolWindow,
				Cursor = Cursors.Default,
				ShowIcon = false,
				ShowInTaskbar = false,
				AcceptButton = btnSubmit,
				CancelButton = btnCancel
			};

			prompt.SuspendLayout();

			label1.AutoSize = true;
			label1.Location = new System.Drawing.Point(12, 9);
			label1.Name = "lblText";
			label1.Size = new System.Drawing.Size(111, 13);
			label1.TabIndex = 999;
			label1.Text = inputDescription;

			txtText.Location = new System.Drawing.Point(15, 25);
			txtText.Name = "txtText";
			txtText.Size = new System.Drawing.Size(320, 20);
			txtText.TabIndex = 0;
			txtText.Text = initialValue;

			btnSubmit.Location = new System.Drawing.Point(222, 50);
			btnSubmit.Name = "btnSubmit";
			btnSubmit.Size = new System.Drawing.Size(113, 23);
			btnSubmit.TabIndex = 1;
			btnSubmit.Text = "OK";
			btnSubmit.UseVisualStyleBackColor = true;
			btnSubmit.DialogResult = DialogResult.OK;
			btnSubmit.Click += (sender, e) => { prompt.Close(); };

			btnCancel.Location = new System.Drawing.Point(99, 50);
			btnCancel.Name = "btnCancel";
			btnCancel.Size = new System.Drawing.Size(113, 23);
			btnCancel.TabIndex = 2;
			btnCancel.Text = "Cancel";
			btnCancel.UseVisualStyleBackColor = true;
			btnCancel.DialogResult = DialogResult.Cancel;
			btnCancel.Click += (sender, e) => { prompt.Close(); };

			prompt.AcceptButton = btnSubmit;
			prompt.CancelButton = btnSubmit;
			prompt.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			prompt.AutoScaleMode = AutoScaleMode.Font;
			prompt.ClientSize = new System.Drawing.Size(346, 83);
			prompt.Controls.Add(txtText);
			prompt.Controls.Add(label1);
			prompt.Controls.Add(btnSubmit);
			prompt.ResumeLayout(false);
			prompt.PerformLayout();

			return prompt.ShowDialog() == DialogResult.OK ? txtText.Text.Trim() : null;
		}
	}
}
