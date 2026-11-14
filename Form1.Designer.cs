namespace ClientInstaller
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            button1 = new Button();
            progressBar2 = new ProgressBar();
            label2 = new Label();
            label3 = new Label();
            SuspendLayout();
            // 
            // button1
            // 
            button1.BackColor = Color.Transparent;
            button1.ForeColor = SystemColors.ActiveCaptionText;
            button1.Location = new Point(419, 134);
            button1.Name = "button1";
            button1.Size = new Size(357, 96);
            button1.TabIndex = 0;
            button1.Text = "Start Installation";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // progressBar2
            // 
            progressBar2.ForeColor = Color.DarkGreen;
            progressBar2.Location = new Point(419, 29);
            progressBar2.Name = "progressBar2";
            progressBar2.Size = new Size(357, 40);
            progressBar2.TabIndex = 2;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.ForeColor = Color.Crimson;
            label2.Location = new Point(467, 411);
            label2.Name = "label2";
            label2.Size = new Size(321, 30);
            label2.TabIndex = 4;
            label2.Text = "Requires Fallout.dat not included.";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(521, 88);
            label3.Name = "label3";
            label3.Size = new Size(169, 30);
            label3.TabIndex = 5;
            label3.Text = "Ready to install...";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(12F, 30F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            ClientSize = new Size(800, 450);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(progressBar2);
            Controls.Add(button1);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "Form1";
            Text = "Installer";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button1;
        private ProgressBar progressBar2;
        private Label label2;
        private Label label3;
    }
}
