namespace ProjectV
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.ComboBox comboBoxDoors;
        private System.Windows.Forms.Label lblConnectionStatus; // Add this line if missing
        private System.Windows.Forms.Label lblDoorStatus;
        private System.Windows.Forms.Button btnLock;
        private System.Windows.Forms.Button btnUnlock;

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
            comboBoxDoors = new ComboBox();
            lblConnectionStatus = new Label(); // Initialize lblConnectionStatus
            lblDoorStatus = new Label();
            btnLock = new Button();
            btnUnlock = new Button();
            SuspendLayout();

            // 
            // comboBoxDoors
            // 
            comboBoxDoors.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxDoors.Items.AddRange(new object[] { "Front Door", "Garage Door" });
            comboBoxDoors.Location = new System.Drawing.Point(120, 20);
            comboBoxDoors.Name = "comboBoxDoors";
            comboBoxDoors.Size = new System.Drawing.Size(200, 23);
            comboBoxDoors.SelectedIndexChanged += comboBoxDoors_SelectedIndexChanged;

            // 
            // lblConnectionStatus
            // 
            lblConnectionStatus.AutoSize = true;
            lblConnectionStatus.Location = new System.Drawing.Point(120, 50);
            lblConnectionStatus.Name = "lblConnectionStatus";
            lblConnectionStatus.Size = new System.Drawing.Size(100, 15);
            lblConnectionStatus.Text = "Disconnected";
            lblConnectionStatus.ForeColor = System.Drawing.Color.Red;

            // 
            // lblDoorStatus
            // 
            lblDoorStatus.AutoSize = true;
            lblDoorStatus.Location = new System.Drawing.Point(120, 80);
            lblDoorStatus.Name = "lblDoorStatus";
            lblDoorStatus.Size = new System.Drawing.Size(100, 15);
            lblDoorStatus.Text = "Status: Unknown";

            // 
            // btnLock
            // 
            btnLock.Location = new System.Drawing.Point(50, 120);
            btnLock.Name = "btnLock";
            btnLock.Size = new System.Drawing.Size(100, 30);
            btnLock.Text = "Lock";
            btnLock.UseVisualStyleBackColor = true;
            btnLock.Click += btnLock_Click;

            // 
            // btnUnlock
            // 
            btnUnlock.Location = new System.Drawing.Point(220, 120);
            btnUnlock.Name = "btnUnlock";
            btnUnlock.Size = new System.Drawing.Size(100, 30);
            btnUnlock.Text = "Unlock";
            btnUnlock.UseVisualStyleBackColor = true;
            btnUnlock.Click += btnUnlock_Click;

            // 
            // Form1
            // 
            ClientSize = new System.Drawing.Size(400, 200);
            Controls.Add(comboBoxDoors);
            Controls.Add(lblConnectionStatus); // Ensure lblConnectionStatus is added to controls
            Controls.Add(lblDoorStatus);
            Controls.Add(btnLock);
            Controls.Add(btnUnlock);
            Text = "Locker Control";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
