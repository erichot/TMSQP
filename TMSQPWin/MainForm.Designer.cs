namespace TMSQPWin
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
            button1 = new Button();
            listBox1 = new ListBox();
            label1 = new Label();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Font = new Font("Verdana", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            button1.Location = new Point(388, 337);
            button1.Name = "button1";
            button1.Size = new Size(325, 75);
            button1.TabIndex = 2;
            button1.Text = "Start to notify by email";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // listBox1
            // 
            listBox1.Font = new Font("Verdana", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            listBox1.FormattingEnabled = true;
            listBox1.ItemHeight = 22;
            listBox1.Items.AddRange(new object[] { "END-APR-2024", "END-MAR-2024", "END-FEB-2024", "END-JAN-2024", "END-DEC-2023", "END-NOV-2023", "END-OCT-2023" });
            listBox1.Location = new Point(24, 12);
            listBox1.Name = "listBox1";
            listBox1.Size = new Size(319, 400);
            listBox1.TabIndex = 3;
            // 
            // label1
            // 
            label1.BorderStyle = BorderStyle.Fixed3D;
            label1.Font = new Font("Verdana", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(388, 12);
            label1.Name = "label1";
            label1.Size = new Size(304, 34);
            label1.TabIndex = 4;
            label1.Text = "END-APR-2024";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(9F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(766, 430);
            Controls.Add(label1);
            Controls.Add(listBox1);
            Controls.Add(button1);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "MainForm";
            Text = "Thermos QPay Notification Helper";
            ResumeLayout(false);
        }

        #endregion
        private Button button1;
        private ListBox listBox1;
        private Label label1;
    }
}