namespace AnalyzerGUI
{
    partial class AnalyzerForm
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
            this.ConnectDisconnectButton = new System.Windows.Forms.Button();
            this.ResultTextBox = new System.Windows.Forms.TextBox();
            this.SendResultButton = new System.Windows.Forms.Button();
            this.QueryButton = new System.Windows.Forms.Button();
            this.AnalyzerReadOnlyTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // ConnectDisconnectButton
            // 
            this.ConnectDisconnectButton.Location = new System.Drawing.Point(16, 15);
            this.ConnectDisconnectButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ConnectDisconnectButton.Name = "ConnectDisconnectButton";
            this.ConnectDisconnectButton.Size = new System.Drawing.Size(161, 28);
            this.ConnectDisconnectButton.TabIndex = 0;
            this.ConnectDisconnectButton.Text = "Connect";
            this.ConnectDisconnectButton.UseVisualStyleBackColor = true;
            this.ConnectDisconnectButton.Click += new System.EventHandler(this.ConnectDisconnectButton_Click);
            // 
            // ResultTextBox
            // 
            this.ResultTextBox.Location = new System.Drawing.Point(16, 64);
            this.ResultTextBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ResultTextBox.Name = "ResultTextBox";
            this.ResultTextBox.Size = new System.Drawing.Size(160, 22);
            this.ResultTextBox.TabIndex = 1;
            // 
            // SendResultButton
            // 
            this.SendResultButton.Location = new System.Drawing.Point(16, 112);
            this.SendResultButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.SendResultButton.Name = "SendResultButton";
            this.SendResultButton.Size = new System.Drawing.Size(161, 28);
            this.SendResultButton.TabIndex = 2;
            this.SendResultButton.Text = "Result";
            this.SendResultButton.UseVisualStyleBackColor = true;
            this.SendResultButton.Click += new System.EventHandler(this.SendResultButton_Click);
            // 
            // QueryButton
            // 
            this.QueryButton.Location = new System.Drawing.Point(16, 176);
            this.QueryButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.QueryButton.Name = "QueryButton";
            this.QueryButton.Size = new System.Drawing.Size(161, 28);
            this.QueryButton.TabIndex = 3;
            this.QueryButton.Text = "Query";
            this.QueryButton.UseVisualStyleBackColor = true;
            this.QueryButton.Click += new System.EventHandler(this.QueryButton_Click);
            // 
            // AnalyzerReadOnlyTextBox
            // 
            this.AnalyzerReadOnlyTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.AnalyzerReadOnlyTextBox.Location = new System.Drawing.Point(227, 15);
            this.AnalyzerReadOnlyTextBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.AnalyzerReadOnlyTextBox.Multiline = true;
            this.AnalyzerReadOnlyTextBox.Name = "AnalyzerReadOnlyTextBox";
            this.AnalyzerReadOnlyTextBox.ReadOnly = true;
            this.AnalyzerReadOnlyTextBox.Size = new System.Drawing.Size(359, 237);
            this.AnalyzerReadOnlyTextBox.TabIndex = 4;
            // 
            // AnalyzerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(603, 294);
            this.Controls.Add(this.AnalyzerReadOnlyTextBox);
            this.Controls.Add(this.QueryButton);
            this.Controls.Add(this.SendResultButton);
            this.Controls.Add(this.ResultTextBox);
            this.Controls.Add(this.ConnectDisconnectButton);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "AnalyzerForm";
            this.Text = "Analyzer";
            this.Load += new System.EventHandler(this.AnalyzerForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.Button ConnectDisconnectButton;
        private System.Windows.Forms.TextBox ResultTextBox;
        private System.Windows.Forms.Button SendResultButton;
        private System.Windows.Forms.Button QueryButton;
        internal System.Windows.Forms.TextBox AnalyzerReadOnlyTextBox;
    }
}
