namespace SerialDataForm
{
    partial class SerialDataForm
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
            this.BaudRateSelection = new System.Windows.Forms.ComboBox();
            this.ConnectButton = new System.Windows.Forms.Button();
            this.RefreshButton = new System.Windows.Forms.Button();
            this.COMPortSelection = new System.Windows.Forms.ComboBox();
            this.SerialConnectionStateText = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // BaudRateSelection
            // 
            this.BaudRateSelection.FormattingEnabled = true;
            this.BaudRateSelection.Location = new System.Drawing.Point(27, 91);
            this.BaudRateSelection.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.BaudRateSelection.Name = "BaudRateSelection";
            this.BaudRateSelection.Size = new System.Drawing.Size(120, 21);
            this.BaudRateSelection.TabIndex = 9;
            // 
            // ConnectButton
            // 
            this.ConnectButton.Location = new System.Drawing.Point(154, 91);
            this.ConnectButton.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.ConnectButton.Name = "ConnectButton";
            this.ConnectButton.Size = new System.Drawing.Size(74, 23);
            this.ConnectButton.TabIndex = 8;
            this.ConnectButton.Text = "Connect";
            this.ConnectButton.UseVisualStyleBackColor = true;
            this.ConnectButton.Click += new System.EventHandler(this.ConnectButton_Click);
            // 
            // RefreshButton
            // 
            this.RefreshButton.Location = new System.Drawing.Point(154, 53);
            this.RefreshButton.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.RefreshButton.Name = "RefreshButton";
            this.RefreshButton.Size = new System.Drawing.Size(74, 23);
            this.RefreshButton.TabIndex = 7;
            this.RefreshButton.Text = "Refresh";
            this.RefreshButton.UseVisualStyleBackColor = true;
            this.RefreshButton.Click += new System.EventHandler(this.RefreshButton_Click);
            // 
            // COMPortSelection
            // 
            this.COMPortSelection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.COMPortSelection.FormattingEnabled = true;
            this.COMPortSelection.Location = new System.Drawing.Point(27, 53);
            this.COMPortSelection.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.COMPortSelection.Name = "COMPortSelection";
            this.COMPortSelection.Size = new System.Drawing.Size(120, 21);
            this.COMPortSelection.TabIndex = 6;
            // 
            // SerialConnectionStateText
            // 
            this.SerialConnectionStateText.Location = new System.Drawing.Point(77, 11);
            this.SerialConnectionStateText.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.SerialConnectionStateText.Name = "SerialConnectionStateText";
            this.SerialConnectionStateText.ReadOnly = true;
            this.SerialConnectionStateText.Size = new System.Drawing.Size(100, 20);
            this.SerialConnectionStateText.TabIndex = 5;
            // 
            // SerialDataForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(256, 125);
            this.Controls.Add(this.BaudRateSelection);
            this.Controls.Add(this.ConnectButton);
            this.Controls.Add(this.RefreshButton);
            this.Controls.Add(this.COMPortSelection);
            this.Controls.Add(this.SerialConnectionStateText);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "SerialDataForm";
            this.Text = "Serial Data Form";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox BaudRateSelection;
        private System.Windows.Forms.Button ConnectButton;
        private System.Windows.Forms.Button RefreshButton;
        private System.Windows.Forms.ComboBox COMPortSelection;
        private System.Windows.Forms.TextBox SerialConnectionStateText;
    }
}

