namespace CommandData2
{
    partial class SmartThermostat
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
            this.displayPanel = new System.Windows.Forms.Panel();
            this.temperatureLabel = new System.Windows.Forms.Label();
            this.temperatureTitle = new System.Windows.Forms.Label();
            this.tempUpButton = new System.Windows.Forms.Button();
            this.tempDownButton = new System.Windows.Forms.Button();
            this.statusTitleLabel = new System.Windows.Forms.Label();
            this.statusPanel = new System.Windows.Forms.Panel();
            this.statusLabel = new System.Windows.Forms.Label();
            this.powerTitle = new System.Windows.Forms.Label();
            this.powerButton = new System.Windows.Forms.Button();
            this.displayPanel.SuspendLayout();
            this.statusPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // displayPanel
            // 
            this.displayPanel.BackColor = System.Drawing.Color.YellowGreen;
            this.displayPanel.Controls.Add(this.temperatureLabel);
            this.displayPanel.Controls.Add(this.temperatureTitle);
            this.displayPanel.Location = new System.Drawing.Point(45, 32);
            this.displayPanel.Name = "displayPanel";
            this.displayPanel.Size = new System.Drawing.Size(237, 140);
            this.displayPanel.TabIndex = 0;
            // 
            // temperatureLabel
            // 
            this.temperatureLabel.AutoSize = true;
            this.temperatureLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 48F);
            this.temperatureLabel.Location = new System.Drawing.Point(52, 45);
            this.temperatureLabel.Name = "temperatureLabel";
            this.temperatureLabel.Size = new System.Drawing.Size(130, 73);
            this.temperatureLabel.TabIndex = 1;
            this.temperatureLabel.Text = "72°";
            // 
            // temperatureTitle
            // 
            this.temperatureTitle.AutoSize = true;
            this.temperatureTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.temperatureTitle.Location = new System.Drawing.Point(3, 9);
            this.temperatureTitle.Name = "temperatureTitle";
            this.temperatureTitle.Size = new System.Drawing.Size(129, 24);
            this.temperatureTitle.TabIndex = 0;
            this.temperatureTitle.Text = "Temperature: ";
            // 
            // tempUpButton
            // 
            this.tempUpButton.Location = new System.Drawing.Point(341, 64);
            this.tempUpButton.Name = "tempUpButton";
            this.tempUpButton.Size = new System.Drawing.Size(64, 23);
            this.tempUpButton.TabIndex = 1;
            this.tempUpButton.Text = "▲";
            this.tempUpButton.UseVisualStyleBackColor = true;
            this.tempUpButton.Click += new System.EventHandler(this.tempUpButton_Click);
            // 
            // tempDownButton
            // 
            this.tempDownButton.Location = new System.Drawing.Point(341, 120);
            this.tempDownButton.Name = "tempDownButton";
            this.tempDownButton.Size = new System.Drawing.Size(64, 23);
            this.tempDownButton.TabIndex = 2;
            this.tempDownButton.Text = "▼";
            this.tempDownButton.UseVisualStyleBackColor = true;
            this.tempDownButton.Click += new System.EventHandler(this.tempDownButton_Click);
            // 
            // statusTitleLabel
            // 
            this.statusTitleLabel.AutoSize = true;
            this.statusTitleLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.statusTitleLabel.Location = new System.Drawing.Point(41, 175);
            this.statusTitleLabel.Name = "statusTitleLabel";
            this.statusTitleLabel.Size = new System.Drawing.Size(60, 20);
            this.statusTitleLabel.TabIndex = 3;
            this.statusTitleLabel.Text = "Status:";
            // 
            // statusPanel
            // 
            this.statusPanel.BackColor = System.Drawing.Color.YellowGreen;
            this.statusPanel.Controls.Add(this.statusLabel);
            this.statusPanel.Location = new System.Drawing.Point(99, 175);
            this.statusPanel.Name = "statusPanel";
            this.statusPanel.Size = new System.Drawing.Size(78, 20);
            this.statusPanel.TabIndex = 4;
            // 
            // statusLabel
            // 
            this.statusLabel.AutoSize = true;
            this.statusLabel.Location = new System.Drawing.Point(11, 4);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(22, 13);
            this.statusLabel.TabIndex = 0;
            this.statusLabel.Text = "OK";
            // 
            // powerTitle
            // 
            this.powerTitle.AutoSize = true;
            this.powerTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.powerTitle.Location = new System.Drawing.Point(41, 198);
            this.powerTitle.Name = "powerTitle";
            this.powerTitle.Size = new System.Drawing.Size(72, 20);
            this.powerTitle.TabIndex = 5;
            this.powerTitle.Text = "ON/OFF:";
            // 
            // powerButton
            // 
            this.powerButton.Location = new System.Drawing.Point(110, 198);
            this.powerButton.Name = "powerButton";
            this.powerButton.Size = new System.Drawing.Size(47, 23);
            this.powerButton.TabIndex = 6;
            this.powerButton.Text = "Power";
            this.powerButton.UseVisualStyleBackColor = true;
            this.powerButton.Click += new System.EventHandler(this.powerButton_Click);
            // 
            // SmartThermostat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(434, 251);
            this.Controls.Add(this.powerButton);
            this.Controls.Add(this.powerTitle);
            this.Controls.Add(this.statusPanel);
            this.Controls.Add(this.statusTitleLabel);
            this.Controls.Add(this.tempDownButton);
            this.Controls.Add(this.tempUpButton);
            this.Controls.Add(this.displayPanel);
            this.Name = "SmartThermostat";
            this.Text = "Smart Thermostat ";
            this.displayPanel.ResumeLayout(false);
            this.displayPanel.PerformLayout();
            this.statusPanel.ResumeLayout(false);
            this.statusPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel displayPanel;
        private System.Windows.Forms.Label temperatureLabel;
        private System.Windows.Forms.Label temperatureTitle;
        private System.Windows.Forms.Button tempUpButton;
        private System.Windows.Forms.Button tempDownButton;
        private System.Windows.Forms.Label statusTitleLabel;
        private System.Windows.Forms.Panel statusPanel;
        private System.Windows.Forms.Label statusLabel;
        private System.Windows.Forms.Label powerTitle;
        private System.Windows.Forms.Button powerButton;
    }
}