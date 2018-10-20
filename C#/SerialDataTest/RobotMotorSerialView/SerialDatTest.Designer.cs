namespace RobotMotorSerialView
{
    partial class SerialDatTest
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
            this.Test1Btn = new System.Windows.Forms.Button();
            this.Test2Button = new System.Windows.Forms.Button();
            this.Test3Button = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Test1Btn
            // 
            this.Test1Btn.Location = new System.Drawing.Point(12, 12);
            this.Test1Btn.Name = "Test1Btn";
            this.Test1Btn.Size = new System.Drawing.Size(75, 23);
            this.Test1Btn.TabIndex = 2;
            this.Test1Btn.Text = "Test 1";
            this.Test1Btn.UseVisualStyleBackColor = true;
            this.Test1Btn.Click += new System.EventHandler(this.Test1Btn_Click);
            // 
            // Test2Button
            // 
            this.Test2Button.Location = new System.Drawing.Point(12, 50);
            this.Test2Button.Name = "Test2Button";
            this.Test2Button.Size = new System.Drawing.Size(75, 23);
            this.Test2Button.TabIndex = 3;
            this.Test2Button.Text = "Test 2";
            this.Test2Button.UseVisualStyleBackColor = true;
            this.Test2Button.Click += new System.EventHandler(this.Test2Button_Click);
            // 
            // Test3Button
            // 
            this.Test3Button.Location = new System.Drawing.Point(12, 89);
            this.Test3Button.Name = "Test3Button";
            this.Test3Button.Size = new System.Drawing.Size(75, 23);
            this.Test3Button.TabIndex = 4;
            this.Test3Button.Text = "Test 3";
            this.Test3Button.UseVisualStyleBackColor = true;
            this.Test3Button.Click += new System.EventHandler(this.Test3Button_Click);
            // 
            // SerialDatTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.Test3Button);
            this.Controls.Add(this.Test2Button);
            this.Controls.Add(this.Test1Btn);
            this.Name = "SerialDatTest";
            this.Text = "RobotMotorSerialView";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button Test1Btn;
        private System.Windows.Forms.Button Test2Button;
        private System.Windows.Forms.Button Test3Button;
    }
}