namespace Taxi
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.keywordsTbx = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.expo2Tbx = new System.Windows.Forms.TextBox();
            this.expo1Tbx = new System.Windows.Forms.TextBox();
            this.maxoccurencesTbx = new System.Windows.Forms.TextBox();
            this.minOccurencesTbx = new System.Windows.Forms.TextBox();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Location = new System.Drawing.Point(10, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(337, 78);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "CleanData";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(3, 46);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(145, 23);
            this.button2.TabIndex = 0;
            this.button2.Text = "Generate Features";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(3, 16);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(145, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Clean DB";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.keywordsTbx);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.expo2Tbx);
            this.groupBox3.Controls.Add(this.expo1Tbx);
            this.groupBox3.Controls.Add(this.maxoccurencesTbx);
            this.groupBox3.Controls.Add(this.minOccurencesTbx);
            this.groupBox3.Controls.Add(this.button4);
            this.groupBox3.Controls.Add(this.button3);
            this.groupBox3.Location = new System.Drawing.Point(10, 97);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(337, 263);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Learn";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(29, 41);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 13);
            this.label5.TabIndex = 13;
            this.label5.Text = "Keywords";
            // 
            // keywordsTbx
            // 
            this.keywordsTbx.Location = new System.Drawing.Point(115, 41);
            this.keywordsTbx.Margin = new System.Windows.Forms.Padding(2);
            this.keywordsTbx.Name = "keywordsTbx";
            this.keywordsTbx.Size = new System.Drawing.Size(217, 20);
            this.keywordsTbx.TabIndex = 12;
            this.keywordsTbx.Text = "method6;method4";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(30, 136);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(37, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Expo2";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(30, 113);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Expo1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(30, 86);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "MaxOccurences";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(30, 63);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "MinOccurences";
            // 
            // expo2Tbx
            // 
            this.expo2Tbx.Location = new System.Drawing.Point(116, 132);
            this.expo2Tbx.Margin = new System.Windows.Forms.Padding(2);
            this.expo2Tbx.Name = "expo2Tbx";
            this.expo2Tbx.Size = new System.Drawing.Size(217, 20);
            this.expo2Tbx.TabIndex = 5;
            this.expo2Tbx.Text = "2;4;5;7;8;10;12;15";
            // 
            // expo1Tbx
            // 
            this.expo1Tbx.Location = new System.Drawing.Point(116, 109);
            this.expo1Tbx.Margin = new System.Windows.Forms.Padding(2);
            this.expo1Tbx.Name = "expo1Tbx";
            this.expo1Tbx.Size = new System.Drawing.Size(217, 20);
            this.expo1Tbx.TabIndex = 4;
            this.expo1Tbx.Text = "-2;-1;0;1";
            // 
            // maxoccurencesTbx
            // 
            this.maxoccurencesTbx.Location = new System.Drawing.Point(116, 86);
            this.maxoccurencesTbx.Margin = new System.Windows.Forms.Padding(2);
            this.maxoccurencesTbx.Name = "maxoccurencesTbx";
            this.maxoccurencesTbx.Size = new System.Drawing.Size(217, 20);
            this.maxoccurencesTbx.TabIndex = 3;
            this.maxoccurencesTbx.Text = "500000";
            // 
            // minOccurencesTbx
            // 
            this.minOccurencesTbx.Location = new System.Drawing.Point(116, 63);
            this.minOccurencesTbx.Margin = new System.Windows.Forms.Padding(2);
            this.minOccurencesTbx.Name = "minOccurencesTbx";
            this.minOccurencesTbx.Size = new System.Drawing.Size(217, 20);
            this.minOccurencesTbx.TabIndex = 2;
            this.minOccurencesTbx.Text = "5";
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(5, 204);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(326, 23);
            this.button4.TabIndex = 1;
            this.button4.Text = "Error";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(5, 233);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(327, 23);
            this.button3.TabIndex = 0;
            this.button3.Text = "Predict";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(359, 362);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.Text = "ECML/PKDD 15: Taxi Trajectory Prediction (I)";
            this.groupBox1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox expo2Tbx;
        private System.Windows.Forms.TextBox expo1Tbx;
        private System.Windows.Forms.TextBox maxoccurencesTbx;
        private System.Windows.Forms.TextBox minOccurencesTbx;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox keywordsTbx;
    }
}

