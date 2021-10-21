namespace Wowhead_Scraper
{
    partial class MainForm
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.questControl1 = new Wowhead_Scraper.questControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.itemControl1 = new Wowhead_Scraper.itemControl();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.generateIdRangeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(0, 33);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(631, 284);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage1.Controls.Add(this.questControl1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(623, 258);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Quest Scraper";
            // 
            // questControl1
            // 
            this.questControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.questControl1.Location = new System.Drawing.Point(3, 3);
            this.questControl1.Name = "questControl1";
            this.questControl1.Size = new System.Drawing.Size(617, 252);
            this.questControl1.TabIndex = 0;
            this.questControl1.Load += new System.EventHandler(this.QuestControl1_Load_1);
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage2.Controls.Add(this.itemControl1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(623, 258);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Item Scraper";
            // 
            // itemControl1
            // 
            this.itemControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.itemControl1.Location = new System.Drawing.Point(3, 3);
            this.itemControl1.Name = "itemControl1";
            this.itemControl1.Size = new System.Drawing.Size(617, 252);
            this.itemControl1.TabIndex = 0;
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.generateIdRangeToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // generateIdRangeToolStripMenuItem
            // 
            this.generateIdRangeToolStripMenuItem.Name = "generateIdRangeToolStripMenuItem";
            this.generateIdRangeToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.generateIdRangeToolStripMenuItem.Text = "Generate Id Range";
            this.generateIdRangeToolStripMenuItem.Click += new System.EventHandler(this.generateIdRangeToolStripMenuItem_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(631, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(631, 317);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.tabControl1);
            this.Name = "MainForm";
            this.Text = "Wowhead Scraper";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private questControl questControl1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private itemControl itemControl1;
        private System.Windows.Forms.ToolStripMenuItem generateIdRangeToolStripMenuItem;
    }
}