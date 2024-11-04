namespace MqttGuiApp
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TextBox txtTopic;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.ListBox lstMessages;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.txtTopic = new System.Windows.Forms.TextBox();
            this.btnConnect = new System.Windows.Forms.Button();
            this.lstMessages = new System.Windows.Forms.ListBox();
            this.SuspendLayout();

            // txtTopic
            this.txtTopic.Location = new System.Drawing.Point(12, 12);
            this.txtTopic.Name = "txtTopic";
            this.txtTopic.Size = new System.Drawing.Size(260, 20);
            this.txtTopic.TabIndex = 0;

            // btnConnect
            this.btnConnect.Location = new System.Drawing.Point(12, 38);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(260, 23);
            this.btnConnect.TabIndex = 1;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);

            // lstMessages
            this.lstMessages.FormattingEnabled = true;
            this.lstMessages.Location = new System.Drawing.Point(12, 67);
            this.lstMessages.Name = "lstMessages";
            this.lstMessages.Size = new System.Drawing.Size(260, 173);
            this.lstMessages.TabIndex = 2;

            // Form1
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.lstMessages);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.txtTopic);
            this.Name = "Form1";
            this.Text = "MQTT GUI";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
