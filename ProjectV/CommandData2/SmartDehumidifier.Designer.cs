namespace CommandData2
{
    partial class SmartDehumidifier
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.powerButton = new System.Windows.Forms.Button();
            this.statusTitle = new System.Windows.Forms.Label();
            this.statusTextBox = new System.Windows.Forms.TextBox();
            this.humidityTitle = new System.Windows.Forms.Label();
            this.humidityLabel = new System.Windows.Forms.Label();
            this.waterLevelTitle = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // powerButton
            // 
            this.powerButton.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.powerButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.powerButton.ForeColor = System.Drawing.Color.DarkGreen;
            this.powerButton.Location = new System.Drawing.Point(96, 12);
            this.powerButton.Name = "powerButton";
            this.powerButton.Size = new System.Drawing.Size(75, 39);
            this.powerButton.TabIndex = 0;
            this.powerButton.Text = "POWER";
            this.powerButton.UseVisualStyleBackColor = false;
            this.powerButton.Click += new System.EventHandler(this.powerButton_Click);
            // 
            // statusTitle
            // 
            this.statusTitle.AutoSize = true;
            this.statusTitle.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.statusTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.statusTitle.Location = new System.Drawing.Point(61, 54);
            this.statusTitle.Name = "statusTitle";
            this.statusTitle.Size = new System.Drawing.Size(67, 17);
            this.statusTitle.TabIndex = 1;
            this.statusTitle.Text = "STATUS:";
            // 
            // statusTextBox
            // 
            this.statusTextBox.BackColor = System.Drawing.Color.Maroon;
            this.statusTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.statusTextBox.Location = new System.Drawing.Point(131, 51);
            this.statusTextBox.Name = "statusTextBox";
            this.statusTextBox.Size = new System.Drawing.Size(100, 22);
            this.statusTextBox.TabIndex = 4;
            this.statusTextBox.Text = "OFF";
            // 
            // humidityTitle
            // 
            this.humidityTitle.AutoSize = true;
            this.humidityTitle.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.humidityTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.humidityTitle.Location = new System.Drawing.Point(51, 80);
            this.humidityTitle.Name = "humidityTitle";
            this.humidityTitle.Size = new System.Drawing.Size(77, 17);
            this.humidityTitle.TabIndex = 5;
            this.humidityTitle.Text = "HUMIDITY:";
            // 
            // humidityLabel
            // 
            this.humidityLabel.AutoSize = true;
            this.humidityLabel.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.humidityLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.humidityLabel.Location = new System.Drawing.Point(133, 80);
            this.humidityLabel.Name = "humidityLabel";
            this.humidityLabel.Size = new System.Drawing.Size(36, 17);
            this.humidityLabel.TabIndex = 6;
            this.humidityLabel.Text = "40%";
            // 
            // waterLevelTitle
            // 
            this.waterLevelTitle.AutoSize = true;
            this.waterLevelTitle.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.waterLevelTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.waterLevelTitle.Location = new System.Drawing.Point(19, 104);
            this.waterLevelTitle.Name = "waterLevelTitle";
            this.waterLevelTitle.Size = new System.Drawing.Size(109, 17);
            this.waterLevelTitle.TabIndex = 7;
            this.waterLevelTitle.Text = "WATER LEVEL:";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(131, 100);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(100, 23);
            this.progressBar1.Step = 1;
            this.progressBar1.TabIndex = 8;
            this.progressBar1.Value = 40;
            // 
            // SmartDehumidifier
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(284, 136);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.waterLevelTitle);
            this.Controls.Add(this.humidityLabel);
            this.Controls.Add(this.humidityTitle);
            this.Controls.Add(this.statusTextBox);
            this.Controls.Add(this.statusTitle);
            this.Controls.Add(this.powerButton);
            this.Name = "SmartDehumidifier";
            this.Text = "Smart Dehumidifier";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button powerButton;
        private System.Windows.Forms.Label statusTitle;
        private System.Windows.Forms.TextBox statusTextBox;
        private System.Windows.Forms.Label humidityTitle;
        private System.Windows.Forms.Label humidityLabel;
        private System.Windows.Forms.Label waterLevelTitle;
        private System.Windows.Forms.ProgressBar progressBar1;
    }
}