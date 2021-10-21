namespace Wowhead_Scraper
{
    partial class IdRangeFrm
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
            this.txtServer = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Get = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtUser = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtPass = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtWorld = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtTable = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtRowName = new System.Windows.Forms.TextBox();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.lblStatus = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txtServer
            // 
            this.txtServer.Location = new System.Drawing.Point(100, 19);
            this.txtServer.Name = "txtServer";
            this.txtServer.Size = new System.Drawing.Size(100, 20);
            this.txtServer.TabIndex = 0;
            this.txtServer.Text = "127.0.0.1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "MySQL Server";
            // 
            // Get
            // 
            this.Get.Location = new System.Drawing.Point(358, 124);
            this.Get.Name = "Get";
            this.Get.Size = new System.Drawing.Size(75, 23);
            this.Get.TabIndex = 2;
            this.Get.Text = "Get";
            this.Get.UseVisualStyleBackColor = true;
            this.Get.Click += new System.EventHandler(this.Get_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "MySQL User:";
            // 
            // txtUser
            // 
            this.txtUser.Location = new System.Drawing.Point(100, 45);
            this.txtUser.Name = "txtUser";
            this.txtUser.Size = new System.Drawing.Size(100, 20);
            this.txtUser.TabIndex = 3;
            this.txtUser.Text = "root";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 74);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "MySQL Pass:";
            // 
            // txtPass
            // 
            this.txtPass.Location = new System.Drawing.Point(100, 71);
            this.txtPass.Name = "txtPass";
            this.txtPass.Size = new System.Drawing.Size(100, 20);
            this.txtPass.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 144);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(85, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "World database:";
            // 
            // txtWorld
            // 
            this.txtWorld.Location = new System.Drawing.Point(100, 141);
            this.txtWorld.Name = "txtWorld";
            this.txtWorld.Size = new System.Drawing.Size(100, 20);
            this.txtWorld.TabIndex = 7;
            this.txtWorld.Text = "world";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(268, 22);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(37, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Table:";
            // 
            // txtTable
            // 
            this.txtTable.Location = new System.Drawing.Point(349, 19);
            this.txtTable.Name = "txtTable";
            this.txtTable.Size = new System.Drawing.Size(100, 20);
            this.txtTable.TabIndex = 9;
            this.txtTable.Text = "item_template";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(268, 51);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(75, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "Id Row Name:";
            // 
            // txtRowName
            // 
            this.txtRowName.Location = new System.Drawing.Point(349, 48);
            this.txtRowName.Name = "txtRowName";
            this.txtRowName.Size = new System.Drawing.Size(100, 20);
            this.txtRowName.TabIndex = 11;
            this.txtRowName.Text = "entry";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(12, 186);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(437, 23);
            this.progressBar1.TabIndex = 13;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(216, 191);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(24, 13);
            this.lblStatus.TabIndex = 14;
            this.lblStatus.Text = "0/0";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 103);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(67, 13);
            this.label7.TabIndex = 16;
            this.label7.Text = "MySQL Port:";
            // 
            // txtPort
            // 
            this.txtPort.Location = new System.Drawing.Point(100, 100);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(100, 20);
            this.txtPort.TabIndex = 15;
            this.txtPort.Text = "3306";
            // 
            // IdRangeFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(462, 212);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtPort);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtRowName);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtTable);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtWorld);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtPass);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtUser);
            this.Controls.Add(this.Get);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtServer);
            this.Name = "IdRangeFrm";
            this.Text = "Get IDs from Table Database";
            this.Load += new System.EventHandler(this.IdRangeFrm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtServer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button Get;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtUser;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtPass;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtWorld;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtTable;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtRowName;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtPort;
    }
}