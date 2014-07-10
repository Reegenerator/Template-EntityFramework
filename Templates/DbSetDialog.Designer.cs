namespace Templates {
    partial class DbsetDialog {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.GenSetByTypeCheck = new System.Windows.Forms.CheckBox();
            this.GenerateButton = new System.Windows.Forms.Button();
            this.BaseclassCombo = new System.Windows.Forms.ListBox();
            this.CandidateList = new System.Windows.Forms.CheckedListBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabControl.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPage1);
            this.tabControl.Controls.Add(this.tabPage2);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(526, 437);
            this.tabControl.TabIndex = 3;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.GenSetByTypeCheck);
            this.tabPage1.Controls.Add(this.GenerateButton);
            this.tabPage1.Controls.Add(this.BaseclassCombo);
            this.tabPage1.Controls.Add(this.CandidateList);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(518, 411);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "DbSet Properties";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // GenSetByTypeCheck
            // 
            this.GenSetByTypeCheck.AutoSize = true;
            this.GenSetByTypeCheck.Checked = true;
            this.GenSetByTypeCheck.CheckState = System.Windows.Forms.CheckState.Checked;
            this.GenSetByTypeCheck.Location = new System.Drawing.Point(24, 371);
            this.GenSetByTypeCheck.Name = "GenSetByTypeCheck";
            this.GenSetByTypeCheck.Size = new System.Drawing.Size(142, 17);
            this.GenSetByTypeCheck.TabIndex = 6;
            this.GenSetByTypeCheck.Text = "Generate GetSetByType";
            this.GenSetByTypeCheck.UseVisualStyleBackColor = true;
            // 
            // GenerateButton
            // 
            this.GenerateButton.Location = new System.Drawing.Point(408, 371);
            this.GenerateButton.Name = "GenerateButton";
            this.GenerateButton.Size = new System.Drawing.Size(104, 37);
            this.GenerateButton.TabIndex = 5;
            this.GenerateButton.Text = "Generate";
            this.GenerateButton.UseVisualStyleBackColor = true;
            // 
            // BaseclassCombo
            // 
            this.BaseclassCombo.FormattingEnabled = true;
            this.BaseclassCombo.Location = new System.Drawing.Point(6, 6);
            this.BaseclassCombo.Name = "BaseclassCombo";
            this.BaseclassCombo.Size = new System.Drawing.Size(120, 342);
            this.BaseclassCombo.TabIndex = 4;
            // 
            // CandidateList
            // 
            this.CandidateList.FormattingEnabled = true;
            this.CandidateList.Location = new System.Drawing.Point(132, 6);
            this.CandidateList.Name = "CandidateList";
            this.CandidateList.Size = new System.Drawing.Size(378, 349);
            this.CandidateList.TabIndex = 3;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(518, 411);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // DbsetDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(526, 437);
            this.Controls.Add(this.tabControl);
            this.Name = "DbsetDialog";
            this.Text = "DbsetDialog";
            this.tabControl.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Button GenerateButton;
        private System.Windows.Forms.ListBox BaseclassCombo;
        private System.Windows.Forms.CheckedListBox CandidateList;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.CheckBox GenSetByTypeCheck;

    }
}