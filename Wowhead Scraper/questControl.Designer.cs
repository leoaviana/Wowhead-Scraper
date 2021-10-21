namespace Wowhead_Scraper
{
    partial class questControl
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
            this.components = new System.ComponentModel.Container();
            this.beginId = new System.Windows.Forms.TextBox();
            this.Parse = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.type = new System.Windows.Forms.ComboBox();
            this.lblObjID = new System.Windows.Forms.Label();
            this.lblLastID = new System.Windows.Forms.Label();
            this.EndId = new System.Windows.Forms.TextBox();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.mItem = new System.Windows.Forms.RadioButton();
            this.sItem = new System.Windows.Forms.RadioButton();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.chkAllowObjectives = new System.Windows.Forms.CheckBox();
            this.chkRewardText = new System.Windows.Forms.CheckBox();
            this.chkQuestName = new System.Windows.Forms.CheckBox();
            this.rItem = new System.Windows.Forms.RadioButton();
            this.status = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // beginId
            // 
            this.beginId.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.beginId.Location = new System.Drawing.Point(65, 152);
            this.beginId.Name = "beginId";
            this.beginId.Size = new System.Drawing.Size(87, 20);
            this.beginId.TabIndex = 0;
            this.beginId.Text = "0";
            this.beginId.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.beginId_KeyPress);
            // 
            // Parse
            // 
            this.Parse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.Parse.Location = new System.Drawing.Point(107, 203);
            this.Parse.Name = "Parse";
            this.Parse.Size = new System.Drawing.Size(198, 23);
            this.Parse.TabIndex = 1;
            this.Parse.Text = "Get";
            this.Parse.UseVisualStyleBackColor = true;
            this.Parse.Click += new System.EventHandler(this.Parse_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Type:";
            // 
            // type
            // 
            this.type.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.type.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.type.FormattingEnabled = true;
            this.type.Items.AddRange(new object[] {
            "Basic Quest"});
            this.type.Location = new System.Drawing.Point(52, 15);
            this.type.Name = "type";
            this.type.Size = new System.Drawing.Size(232, 21);
            this.type.TabIndex = 3;
            // 
            // lblObjID
            // 
            this.lblObjID.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lblObjID.AutoSize = true;
            this.lblObjID.Location = new System.Drawing.Point(12, 155);
            this.lblObjID.Name = "lblObjID";
            this.lblObjID.Size = new System.Drawing.Size(53, 13);
            this.lblObjID.TabIndex = 4;
            this.lblObjID.Text = "Object Id:";
            // 
            // lblLastID
            // 
            this.lblLastID.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblLastID.AutoSize = true;
            this.lblLastID.Location = new System.Drawing.Point(281, 155);
            this.lblLastID.Name = "lblLastID";
            this.lblLastID.Size = new System.Drawing.Size(42, 13);
            this.lblLastID.TabIndex = 10;
            this.lblLastID.Text = "Last Id:";
            this.lblLastID.Visible = false;
            // 
            // EndId
            // 
            this.EndId.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.EndId.Location = new System.Drawing.Point(328, 152);
            this.EndId.Name = "EndId";
            this.EndId.Size = new System.Drawing.Size(87, 20);
            this.EndId.TabIndex = 9;
            this.EndId.Text = "0";
            this.EndId.Visible = false;
            this.EndId.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.EndId_KeyPress);
            // 
            // progressBar1
            // 
            this.progressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar1.Location = new System.Drawing.Point(3, 274);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(415, 23);
            this.progressBar1.TabIndex = 11;
            // 
            // mItem
            // 
            this.mItem.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mItem.AutoSize = true;
            this.mItem.Location = new System.Drawing.Point(184, 110);
            this.mItem.Name = "mItem";
            this.mItem.Size = new System.Drawing.Size(89, 17);
            this.mItem.TabIndex = 12;
            this.mItem.Text = "Multiple Items";
            this.mItem.UseVisualStyleBackColor = true;
            this.mItem.CheckedChanged += new System.EventHandler(this.mItem_CheckedChanged);
            // 
            // sItem
            // 
            this.sItem.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.sItem.AutoSize = true;
            this.sItem.Checked = true;
            this.sItem.Location = new System.Drawing.Point(15, 110);
            this.sItem.Name = "sItem";
            this.sItem.Size = new System.Drawing.Size(77, 17);
            this.sItem.TabIndex = 13;
            this.sItem.TabStop = true;
            this.sItem.Text = "Single Item";
            this.sItem.UseVisualStyleBackColor = true;
            this.sItem.CheckedChanged += new System.EventHandler(this.sItem_CheckedChanged);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // chkAllowObjectives
            // 
            this.chkAllowObjectives.AutoSize = true;
            this.chkAllowObjectives.Checked = true;
            this.chkAllowObjectives.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAllowObjectives.Location = new System.Drawing.Point(15, 65);
            this.chkAllowObjectives.Name = "chkAllowObjectives";
            this.chkAllowObjectives.Size = new System.Drawing.Size(98, 17);
            this.chkAllowObjectives.TabIndex = 14;
            this.chkAllowObjectives.Text = "Add Objectives";
            this.chkAllowObjectives.UseVisualStyleBackColor = true;
            // 
            // chkRewardText
            // 
            this.chkRewardText.AutoSize = true;
            this.chkRewardText.Checked = true;
            this.chkRewardText.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkRewardText.Enabled = false;
            this.chkRewardText.Location = new System.Drawing.Point(152, 65);
            this.chkRewardText.Name = "chkRewardText";
            this.chkRewardText.Size = new System.Drawing.Size(109, 17);
            this.chkRewardText.TabIndex = 15;
            this.chkRewardText.Text = "Add Reward Text";
            this.chkRewardText.UseVisualStyleBackColor = true;
            // 
            // chkQuestName
            // 
            this.chkQuestName.AutoSize = true;
            this.chkQuestName.Checked = true;
            this.chkQuestName.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkQuestName.Location = new System.Drawing.Point(312, 65);
            this.chkQuestName.Name = "chkQuestName";
            this.chkQuestName.Size = new System.Drawing.Size(107, 17);
            this.chkQuestName.TabIndex = 16;
            this.chkQuestName.Tag = "";
            this.chkQuestName.Text = "Add Quest Name";
            this.chkQuestName.UseVisualStyleBackColor = true;
            // 
            // rItem
            // 
            this.rItem.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rItem.AutoSize = true;
            this.rItem.Location = new System.Drawing.Point(315, 110);
            this.rItem.Name = "rItem";
            this.rItem.Size = new System.Drawing.Size(103, 17);
            this.rItem.TabIndex = 17;
            this.rItem.Text = "Load Range File";
            this.rItem.UseVisualStyleBackColor = true;
            this.rItem.CheckedChanged += new System.EventHandler(this.RItem_CheckedChanged);
            // 
            // status
            // 
            this.status.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.status.AutoSize = true;
            this.status.Location = new System.Drawing.Point(193, 258);
            this.status.Name = "status";
            this.status.Size = new System.Drawing.Size(24, 13);
            this.status.TabIndex = 18;
            this.status.Text = "0/0";
            this.status.TextChanged += new System.EventHandler(this.status_TextChanged);
            // 
            // questControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.status);
            this.Controls.Add(this.rItem);
            this.Controls.Add(this.chkQuestName);
            this.Controls.Add(this.chkRewardText);
            this.Controls.Add(this.chkAllowObjectives);
            this.Controls.Add(this.sItem);
            this.Controls.Add(this.mItem);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.lblLastID);
            this.Controls.Add(this.EndId);
            this.Controls.Add(this.lblObjID);
            this.Controls.Add(this.type);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Parse);
            this.Controls.Add(this.beginId);
            this.Name = "questControl";
            this.Size = new System.Drawing.Size(422, 299);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Resize += new System.EventHandler(this.questControl_Resize);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox beginId;
        private System.Windows.Forms.Button Parse;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox type;
        private System.Windows.Forms.Label lblObjID;
        private System.Windows.Forms.Label lblLastID;
        private System.Windows.Forms.TextBox EndId;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.RadioButton mItem;
        private System.Windows.Forms.RadioButton sItem;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.CheckBox chkAllowObjectives;
        private System.Windows.Forms.CheckBox chkRewardText;
        private System.Windows.Forms.CheckBox chkQuestName;
        private System.Windows.Forms.RadioButton rItem;
        private System.Windows.Forms.Label status;
    }
}

