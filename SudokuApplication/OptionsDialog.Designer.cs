namespace SudokuApplication
{
    partial class OptionsDialog
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.validatedSections = new System.Windows.Forms.CheckBox();
            this.wrongNumbers = new System.Windows.Forms.CheckBox();
            this.OK = new System.Windows.Forms.Button();
            this.cancel = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.validatedSections);
            this.groupBox1.Controls.Add(this.wrongNumbers);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(220, 68);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Validation:";
            // 
            // validatedSections
            // 
            this.validatedSections.AutoSize = true;
            this.validatedSections.Location = new System.Drawing.Point(6, 42);
            this.validatedSections.Name = "validatedSections";
            this.validatedSections.Size = new System.Drawing.Size(182, 17);
            this.validatedSections.TabIndex = 4;
            this.validatedSections.Text = "Show validated sections in green";
            this.validatedSections.UseVisualStyleBackColor = true;
            // 
            // wrongNumbers
            // 
            this.wrongNumbers.AutoSize = true;
            this.wrongNumbers.Location = new System.Drawing.Point(6, 19);
            this.wrongNumbers.Name = "wrongNumbers";
            this.wrongNumbers.Size = new System.Drawing.Size(157, 17);
            this.wrongNumbers.TabIndex = 3;
            this.wrongNumbers.Text = "Show wrong numbers in red";
            this.wrongNumbers.UseVisualStyleBackColor = true;
            // 
            // OK
            // 
            this.OK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.OK.Location = new System.Drawing.Point(76, 86);
            this.OK.Name = "OK";
            this.OK.Size = new System.Drawing.Size(75, 23);
            this.OK.TabIndex = 1;
            this.OK.Text = "OK";
            this.OK.UseVisualStyleBackColor = true;
            this.OK.Click += new System.EventHandler(this.OK_Click);
            // 
            // cancel
            // 
            this.cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancel.Location = new System.Drawing.Point(157, 86);
            this.cancel.Name = "cancel";
            this.cancel.Size = new System.Drawing.Size(75, 23);
            this.cancel.TabIndex = 2;
            this.cancel.Text = "Cancel";
            this.cancel.UseVisualStyleBackColor = true;
            this.cancel.Click += new System.EventHandler(this.cancel_Click);
            // 
            // OptionsDialog
            // 
            this.AcceptButton = this.OK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancel;
            this.ClientSize = new System.Drawing.Size(243, 119);
            this.Controls.Add(this.cancel);
            this.Controls.Add(this.OK);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "OptionsDialog";
            this.Text = "Options";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button OK;
        private System.Windows.Forms.Button cancel;
        private System.Windows.Forms.CheckBox wrongNumbers;
        private System.Windows.Forms.CheckBox validatedSections;

    }
}