namespace TMCS_PRJ
{
    partial class MainForm
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
            groupBox1 = new GroupBox();
            btnMatrixOutput = new Button();
            btnMatrixInput = new Button();
            pnMatrixFrame = new Panel();
            btnConnect = new Button();
            bbbb = new Button();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(btnMatrixOutput);
            groupBox1.Controls.Add(btnMatrixInput);
            groupBox1.Controls.Add(pnMatrixFrame);
            groupBox1.Location = new Point(12, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(298, 684);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "groupBox1";
            // 
            // btnMatrixOutput
            // 
            btnMatrixOutput.Location = new Point(217, 22);
            btnMatrixOutput.Name = "btnMatrixOutput";
            btnMatrixOutput.Size = new Size(75, 23);
            btnMatrixOutput.TabIndex = 2;
            btnMatrixOutput.Text = "button2";
            btnMatrixOutput.UseVisualStyleBackColor = true;
            btnMatrixOutput.Click += btnMatrixOutput_Click;
            // 
            // btnMatrixInput
            // 
            btnMatrixInput.Location = new Point(136, 22);
            btnMatrixInput.Name = "btnMatrixInput";
            btnMatrixInput.Size = new Size(75, 23);
            btnMatrixInput.TabIndex = 1;
            btnMatrixInput.Text = "button1";
            btnMatrixInput.UseVisualStyleBackColor = true;
            btnMatrixInput.Click += btnMatrixInput_Click;
            // 
            // pnMatrixFrame
            // 
            pnMatrixFrame.Location = new Point(6, 59);
            pnMatrixFrame.Name = "pnMatrixFrame";
            pnMatrixFrame.Size = new Size(286, 300);
            pnMatrixFrame.TabIndex = 0;
            // 
            // btnConnect
            // 
            btnConnect.Location = new Point(522, 265);
            btnConnect.Name = "btnConnect";
            btnConnect.Size = new Size(75, 23);
            btnConnect.TabIndex = 1;
            btnConnect.Text = "button1";
            btnConnect.UseVisualStyleBackColor = true;
            btnConnect.Click += btnConnect_Click;
            // 
            // bbbb
            // 
            bbbb.Location = new Point(544, 359);
            bbbb.Name = "bbbb";
            bbbb.Size = new Size(75, 23);
            bbbb.TabIndex = 2;
            bbbb.Text = "button1";
            bbbb.UseVisualStyleBackColor = true;
            bbbb.Click += bbbb_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1163, 740);
            Controls.Add(bbbb);
            Controls.Add(btnConnect);
            Controls.Add(groupBox1);
            Name = "MainForm";
            Text = "Form1";
            Load += MainForm_Load;
            groupBox1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox1;
        private Button btnMatrixOutput;
        private Button btnMatrixInput;
        private Panel pnMatrixFrame;
        private Button btnConnect;
        private Button bbbb;
    }
}