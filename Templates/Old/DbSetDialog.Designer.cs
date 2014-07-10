namespace RgenLib.Templates {
    partial class DbSetDialog {
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
            this.classesList = new System.Windows.Forms.CheckedListBox();
            this.generateButton = new System.Windows.Forms.Button();
            this.baseclassCombo = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // classesList
            // 
            this.classesList.FormattingEnabled = true;
            this.classesList.Location = new System.Drawing.Point(12, 42);
            this.classesList.Name = "classesList";
            this.classesList.Size = new System.Drawing.Size(390, 229);
            this.classesList.TabIndex = 0;
            // 
            // generateButton
            // 
            this.generateButton.Location = new System.Drawing.Point(287, 290);
            this.generateButton.Name = "generateButton";
            this.generateButton.Size = new System.Drawing.Size(114, 24);
            this.generateButton.TabIndex = 1;
            this.generateButton.Text = "Generate";
            this.generateButton.UseVisualStyleBackColor = true;
            // 
            // baseclassCombo
            // 
            this.baseclassCombo.DisplayMember = "Name";
            this.baseclassCombo.FormattingEnabled = true;
            this.baseclassCombo.Location = new System.Drawing.Point(63, 11);
            this.baseclassCombo.Name = "baseclassCombo";
            this.baseclassCombo.Size = new System.Drawing.Size(338, 21);
            this.baseclassCombo.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Base";
            // 
            // DbSetDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(414, 322);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.baseclassCombo);
            this.Controls.Add(this.generateButton);
            this.Controls.Add(this.classesList);
            this.Name = "DbSetDialog";
            this.Text = "DbSetDialog";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckedListBox classesList;
        private System.Windows.Forms.Button generateButton;
        private System.Windows.Forms.ComboBox baseclassCombo;
        private System.Windows.Forms.Label label1;
    }
}