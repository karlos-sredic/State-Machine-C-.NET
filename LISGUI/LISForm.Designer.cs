namespace LISGUI
{
    partial class LISForm
    {
        private System.ComponentModel.IContainer components = null;

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
            this.StartStopButton = new System.Windows.Forms.Button();
            this.LISReadOnlyTextBox = new System.Windows.Forms.TextBox();
            this.PushTextBox = new System.Windows.Forms.TextBox();
            this.PushButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // StartStopButton
            // 
            this.StartStopButton.Location = new System.Drawing.Point(16, 15);
            this.StartStopButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.StartStopButton.Name = "StartStopButton";
            this.StartStopButton.Size = new System.Drawing.Size(129, 28);
            this.StartStopButton.TabIndex = 0;
            this.StartStopButton.Text = "Start";
            this.StartStopButton.UseVisualStyleBackColor = true;
            this.StartStopButton.Click += new System.EventHandler(this.StartStopButton_Click);
            // 
            // LISReadOnlyTextBox
            // 
            this.LISReadOnlyTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LISReadOnlyTextBox.Location = new System.Drawing.Point(196, 15);
            this.LISReadOnlyTextBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.LISReadOnlyTextBox.Multiline = true;
            this.LISReadOnlyTextBox.Name = "LISReadOnlyTextBox";
            this.LISReadOnlyTextBox.ReadOnly = true;
            this.LISReadOnlyTextBox.Size = new System.Drawing.Size(392, 292);
            this.LISReadOnlyTextBox.TabIndex = 1;
            // 
            // PushTextBox
            // 
            this.PushTextBox.Location = new System.Drawing.Point(16, 68);
            this.PushTextBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.PushTextBox.Name = "PushTextBox";
            this.PushTextBox.Size = new System.Drawing.Size(128, 22);
            this.PushTextBox.TabIndex = 2;
            // 
            // PushButton
            // 
            this.PushButton.Location = new System.Drawing.Point(16, 113);
            this.PushButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.PushButton.Name = "PushButton";
            this.PushButton.Size = new System.Drawing.Size(129, 28);
            this.PushButton.TabIndex = 3;
            this.PushButton.Text = "Push";
            this.PushButton.UseVisualStyleBackColor = true;
            this.PushButton.Click += new System.EventHandler(this.PushButton_Click);
            // 
            // LISForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(605, 322);
            this.Controls.Add(this.PushButton);
            this.Controls.Add(this.PushTextBox);
            this.Controls.Add(this.LISReadOnlyTextBox);
            this.Controls.Add(this.StartStopButton);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "LISForm";
            this.Text = "LIS";
            this.Load += new System.EventHandler(this.LISForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.Button StartStopButton;
        internal System.Windows.Forms.TextBox LISReadOnlyTextBox;
        private System.Windows.Forms.TextBox PushTextBox;
        private System.Windows.Forms.Button PushButton;
    }
}
