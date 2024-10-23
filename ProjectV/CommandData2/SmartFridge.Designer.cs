namespace CommandData2
{
    partial class SmartFridge
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
            this.fridgeTempTitle = new System.Windows.Forms.Label();
            this.freezerTempTitle = new System.Windows.Forms.Label();
            this.fridgeTempLabel = new System.Windows.Forms.Label();
            this.freezerTempLabel = new System.Windows.Forms.Label();
            this.fridgeTempUpButton = new System.Windows.Forms.Button();
            this.fridgeTempDownButton = new System.Windows.Forms.Button();
            this.freezerTempDownButton = new System.Windows.Forms.Button();
            this.freezerTempUpButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // fridgeTempTitle
            // 
            this.fridgeTempTitle.AutoSize = true;
            this.fridgeTempTitle.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.fridgeTempTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.fridgeTempTitle.Location = new System.Drawing.Point(12, 23);
            this.fridgeTempTitle.Name = "fridgeTempTitle";
            this.fridgeTempTitle.Size = new System.Drawing.Size(142, 24);
            this.fridgeTempTitle.TabIndex = 0;
            this.fridgeTempTitle.Text = "FRIDGE TEMP:";
            // 
            // freezerTempTitle
            // 
            this.freezerTempTitle.AutoSize = true;
            this.freezerTempTitle.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.freezerTempTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.freezerTempTitle.Location = new System.Drawing.Point(12, 87);
            this.freezerTempTitle.Name = "freezerTempTitle";
            this.freezerTempTitle.Size = new System.Drawing.Size(162, 24);
            this.freezerTempTitle.TabIndex = 1;
            this.freezerTempTitle.Text = "FREEZER TEMP:";
            // 
            // fridgeTempLabel
            // 
            this.fridgeTempLabel.AutoSize = true;
            this.fridgeTempLabel.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.fridgeTempLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.fridgeTempLabel.Location = new System.Drawing.Point(160, 23);
            this.fridgeTempLabel.Name = "fridgeTempLabel";
            this.fridgeTempLabel.Size = new System.Drawing.Size(41, 24);
            this.fridgeTempLabel.TabIndex = 2;
            this.fridgeTempLabel.Text = "35° ";
            // 
            // freezerTempLabel
            // 
            this.freezerTempLabel.AutoSize = true;
            this.freezerTempLabel.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.freezerTempLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.freezerTempLabel.Location = new System.Drawing.Point(180, 87);
            this.freezerTempLabel.Name = "freezerTempLabel";
            this.freezerTempLabel.Size = new System.Drawing.Size(31, 24);
            this.freezerTempLabel.TabIndex = 3;
            this.freezerTempLabel.Text = "0° ";
            // 
            // fridgeTempUpButton
            // 
            this.fridgeTempUpButton.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.fridgeTempUpButton.Location = new System.Drawing.Point(223, 12);
            this.fridgeTempUpButton.Name = "fridgeTempUpButton";
            this.fridgeTempUpButton.Size = new System.Drawing.Size(75, 23);
            this.fridgeTempUpButton.TabIndex = 4;
            this.fridgeTempUpButton.Text = "▲";
            this.fridgeTempUpButton.UseVisualStyleBackColor = false;
            this.fridgeTempUpButton.Click += new System.EventHandler(this.fridgeTempUpButton_Click);
            // 
            // fridgeTempDownButton
            // 
            this.fridgeTempDownButton.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.fridgeTempDownButton.Location = new System.Drawing.Point(223, 41);
            this.fridgeTempDownButton.Name = "fridgeTempDownButton";
            this.fridgeTempDownButton.Size = new System.Drawing.Size(75, 23);
            this.fridgeTempDownButton.TabIndex = 5;
            this.fridgeTempDownButton.Text = "▼";
            this.fridgeTempDownButton.UseVisualStyleBackColor = false;
            this.fridgeTempDownButton.Click += new System.EventHandler(this.fridgeTempDownButton_Click);
            // 
            // freezerTempDownButton
            // 
            this.freezerTempDownButton.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.freezerTempDownButton.Location = new System.Drawing.Point(223, 99);
            this.freezerTempDownButton.Name = "freezerTempDownButton";
            this.freezerTempDownButton.Size = new System.Drawing.Size(75, 23);
            this.freezerTempDownButton.TabIndex = 7;
            this.freezerTempDownButton.Text = "▼";
            this.freezerTempDownButton.UseVisualStyleBackColor = false;
            this.freezerTempDownButton.Click += new System.EventHandler(this.freezerTempDownButton_Click);
            // 
            // freezerTempUpButton
            // 
            this.freezerTempUpButton.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.freezerTempUpButton.Location = new System.Drawing.Point(223, 70);
            this.freezerTempUpButton.Name = "freezerTempUpButton";
            this.freezerTempUpButton.Size = new System.Drawing.Size(75, 23);
            this.freezerTempUpButton.TabIndex = 6;
            this.freezerTempUpButton.Text = "▲";
            this.freezerTempUpButton.UseVisualStyleBackColor = false;
            this.freezerTempUpButton.Click += new System.EventHandler(this.freezerTempUpButton_Click);
            // 
            // SmartFridge
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(384, 161);
            this.Controls.Add(this.freezerTempDownButton);
            this.Controls.Add(this.freezerTempUpButton);
            this.Controls.Add(this.fridgeTempDownButton);
            this.Controls.Add(this.fridgeTempUpButton);
            this.Controls.Add(this.freezerTempLabel);
            this.Controls.Add(this.fridgeTempLabel);
            this.Controls.Add(this.freezerTempTitle);
            this.Controls.Add(this.fridgeTempTitle);
            this.Name = "SmartFridge";
            this.Text = "Smart Fridge";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label fridgeTempTitle;
        private System.Windows.Forms.Label freezerTempTitle;
        private System.Windows.Forms.Label fridgeTempLabel;
        private System.Windows.Forms.Label freezerTempLabel;
        private System.Windows.Forms.Button fridgeTempUpButton;
        private System.Windows.Forms.Button fridgeTempDownButton;
        private System.Windows.Forms.Button freezerTempDownButton;
        private System.Windows.Forms.Button freezerTempUpButton;
    }
}