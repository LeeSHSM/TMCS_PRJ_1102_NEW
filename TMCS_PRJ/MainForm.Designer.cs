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
            btnAddMioFrame = new Button();
            bbbb = new Button();
            pnMioFrame = new Panel();
            lblTest = new Label();
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
            btnMatrixOutput.Text = "출 력";
            btnMatrixOutput.UseVisualStyleBackColor = true;
            btnMatrixOutput.Click += btnMatrixOutput_Click;
            // 
            // btnMatrixInput
            // 
            btnMatrixInput.Location = new Point(136, 22);
            btnMatrixInput.Name = "btnMatrixInput";
            btnMatrixInput.Size = new Size(75, 23);
            btnMatrixInput.TabIndex = 1;
            btnMatrixInput.Text = "입 력";
            btnMatrixInput.UseVisualStyleBackColor = true;
            btnMatrixInput.Click += btnMatrixInput_Click;
            // 
            // pnMatrixFrame
            // 
            pnMatrixFrame.Location = new Point(6, 59);
            pnMatrixFrame.Name = "pnMatrixFrame";
            pnMatrixFrame.Size = new Size(292, 326);
            pnMatrixFrame.TabIndex = 0;
            // 
            // btnAddMioFrame
            // 
            btnAddMioFrame.Location = new Point(316, 12);
            btnAddMioFrame.Name = "btnAddMioFrame";
            btnAddMioFrame.Size = new Size(75, 23);
            btnAddMioFrame.TabIndex = 1;
            btnAddMioFrame.Text = "출력추가!!";
            btnAddMioFrame.UseVisualStyleBackColor = true;
            btnAddMioFrame.Click += btnAddMioFrame_Click;
            // 
            // bbbb
            // 
            bbbb.Location = new Point(409, 12);
            bbbb.Name = "bbbb";
            bbbb.Size = new Size(75, 23);
            bbbb.TabIndex = 2;
            bbbb.Text = "button1";
            bbbb.UseVisualStyleBackColor = true;
            // 
            // pnMioFrame
            // 
            pnMioFrame.Location = new Point(316, 71);
            pnMioFrame.Name = "pnMioFrame";
            pnMioFrame.Size = new Size(701, 428);
            pnMioFrame.TabIndex = 3;
            // 
            // lblTest
            // 
            lblTest.AutoSize = true;
            lblTest.Location = new Point(542, 613);
            lblTest.Name = "lblTest";
            lblTest.Size = new Size(39, 15);
            lblTest.TabIndex = 4;
            lblTest.Text = "label1";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1470, 940);
            Controls.Add(lblTest);
            Controls.Add(pnMioFrame);
            Controls.Add(bbbb);
            Controls.Add(btnAddMioFrame);
            Controls.Add(groupBox1);
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Form1";
            Load += MainForm_Load;
            groupBox1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private GroupBox groupBox1;
        private Button btnMatrixOutput;
        private Button btnMatrixInput;
        private Panel pnMatrixFrame;
        private Button btnAddMioFrame;
        private Button bbbb;
        private Panel pnMioFrame;
        private Label lblTest;
    }
}