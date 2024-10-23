namespace CommandData2
{
    partial class SmartVacuum
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
            this.SuspendLayout();
            // 
            // powerButton
            // 
            this.powerButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.powerButton.BackColor = System.Drawing.Color.DimGray;
            this.powerButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.powerButton.ForeColor = System.Drawing.Color.LawnGreen;
            this.powerButton.Location = new System.Drawing.Point(75, 64);
            this.powerButton.Name = "powerButton";
            this.powerButton.Size = new System.Drawing.Size(75, 75);
            this.powerButton.TabIndex = 0;
            this.powerButton.Text = "POWER";
            this.powerButton.UseVisualStyleBackColor = false;
            this.powerButton.Click += new System.EventHandler(this.powerButton_Click);
            // 
            // statusTitle
            // 
            this.statusTitle.AutoSize = true;
            this.statusTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.statusTitle.Location = new System.Drawing.Point(47, 141);
            this.statusTitle.Name = "statusTitle";
            this.statusTitle.Size = new System.Drawing.Size(67, 17);
            this.statusTitle.TabIndex = 2;
            this.statusTitle.Text = "STATUS:";
            // 
            // statusTextBox
            // 
            this.statusTextBox.BackColor = System.Drawing.Color.Maroon;
            this.statusTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.statusTextBox.Location = new System.Drawing.Point(111, 139);
            this.statusTextBox.Name = "statusTextBox";
            this.statusTextBox.Size = new System.Drawing.Size(100, 22);
            this.statusTextBox.TabIndex = 3;
            this.statusTextBox.Text = "OFF";
            // 
            // SmartVacuum
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkGray;
            this.ClientSize = new System.Drawing.Size(234, 211);
            this.Controls.Add(this.statusTextBox);
            this.Controls.Add(this.statusTitle);
            this.Controls.Add(this.powerButton);
            this.Name = "SmartVacuum";
            this.Text = "Smart Vacuum";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button powerButton;
        private System.Windows.Forms.Label statusTitle;
        private System.Windows.Forms.TextBox statusTextBox;
    }
}