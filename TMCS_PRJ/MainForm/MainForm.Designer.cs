﻿namespace TMCS_PRJ
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
            lblTest = new Label();
            btnMatrixInput = new Button();
            pnMatrixFrame = new Panel();
            btnAddMioFrame = new Button();
            bbbb = new Button();
            pnMioFrame = new Panel();
            menuStrip1 = new MenuStrip();
            파일ToolStripMenuItem = new ToolStripMenuItem();
            보기ToolStripMenuItem = new ToolStripMenuItem();
            장비등록정보확인ToolStripMenuItem = new ToolStripMenuItem();
            pnDlpFrame = new Panel();
            samsungCamera1 = new SamsungCamera();
            lgCamera1 = new LGCamera();
            carmeraControler1 = new CarmeraControler();
            groupBox1.SuspendLayout();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(btnMatrixOutput);
            groupBox1.Controls.Add(lblTest);
            groupBox1.Controls.Add(btnMatrixInput);
            groupBox1.Controls.Add(pnMatrixFrame);
            groupBox1.Location = new Point(12, 37);
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
            // lblTest
            // 
            lblTest.AutoSize = true;
            lblTest.Location = new Point(101, 555);
            lblTest.Name = "lblTest";
            lblTest.Size = new Size(39, 15);
            lblTest.TabIndex = 4;
            lblTest.Text = "label1";
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
            pnMatrixFrame.Size = new Size(286, 326);
            pnMatrixFrame.TabIndex = 0;
            // 
            // btnAddMioFrame
            // 
            btnAddMioFrame.Location = new Point(316, 291);
            btnAddMioFrame.Name = "btnAddMioFrame";
            btnAddMioFrame.Size = new Size(75, 23);
            btnAddMioFrame.TabIndex = 1;
            btnAddMioFrame.Text = "출력추가!!";
            btnAddMioFrame.UseVisualStyleBackColor = true;
            btnAddMioFrame.Click += btnAddMioFrame_Click;
            // 
            // bbbb
            // 
            bbbb.Location = new Point(397, 291);
            bbbb.Name = "bbbb";
            bbbb.Size = new Size(75, 23);
            bbbb.TabIndex = 2;
            bbbb.Text = "button1";
            bbbb.UseVisualStyleBackColor = true;
            bbbb.Click += bbbb_Click;
            // 
            // pnMioFrame
            // 
            pnMioFrame.Location = new Point(316, 320);
            pnMioFrame.Name = "pnMioFrame";
            pnMioFrame.Size = new Size(204, 149);
            pnMioFrame.TabIndex = 3;
            // 
            // menuStrip1
            // 
            menuStrip1.BackColor = Color.Silver;
            menuStrip1.ImageScalingSize = new Size(20, 20);
            menuStrip1.Items.AddRange(new ToolStripItem[] { 파일ToolStripMenuItem, 보기ToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1199, 24);
            menuStrip1.TabIndex = 5;
            menuStrip1.Text = "menuStrip1";
            // 
            // 파일ToolStripMenuItem
            // 
            파일ToolStripMenuItem.Name = "파일ToolStripMenuItem";
            파일ToolStripMenuItem.Size = new Size(43, 20);
            파일ToolStripMenuItem.Text = "파일";
            // 
            // 보기ToolStripMenuItem
            // 
            보기ToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { 장비등록정보확인ToolStripMenuItem });
            보기ToolStripMenuItem.Name = "보기ToolStripMenuItem";
            보기ToolStripMenuItem.Size = new Size(43, 20);
            보기ToolStripMenuItem.Text = "보기";
            // 
            // 장비등록정보확인ToolStripMenuItem
            // 
            장비등록정보확인ToolStripMenuItem.Name = "장비등록정보확인ToolStripMenuItem";
            장비등록정보확인ToolStripMenuItem.Size = new Size(170, 22);
            장비등록정보확인ToolStripMenuItem.Text = "장비등록정보확인";
            장비등록정보확인ToolStripMenuItem.Click += 장비등록정보확인ToolStripMenuItem_Click;
            // 
            // pnDlpFrame
            // 
            pnDlpFrame.Location = new Point(326, 37);
            pnDlpFrame.Name = "pnDlpFrame";
            pnDlpFrame.Size = new Size(474, 235);
            pnDlpFrame.TabIndex = 6;
            // 
            // samsungCamera1
            // 
            samsungCamera1.CameraId = 33;
            samsungCamera1.CameraName = null;
            samsungCamera1.InputPort = 0;
            samsungCamera1.Location = new Point(840, 59);
            samsungCamera1.Name = "samsungCamera1";
            samsungCamera1.OutputPort = 0;
            samsungCamera1.Size = new Size(93, 50);
            samsungCamera1.TabIndex = 7;
            samsungCamera1.Load += Camera_Load;
            samsungCamera1.MouseDown += Camera_MouseDown;
            // 
            // lgCamera1
            // 
            lgCamera1.CameraId = 99;
            lgCamera1.CameraName = null;
            lgCamera1.InputPort = 0;
            lgCamera1.Location = new Point(840, 173);
            lgCamera1.Name = "lgCamera1";
            lgCamera1.OutputPort = 0;
            lgCamera1.Size = new Size(90, 80);
            lgCamera1.TabIndex = 10;
            lgCamera1.Load += Camera_Load;
            lgCamera1.MouseDown += Camera_MouseDown;
            // 
            // carmeraControler1
            // 
            carmeraControler1.BackColor = Color.Black;
            carmeraControler1.Location = new Point(546, 320);
            carmeraControler1.Name = "carmeraControler1";
            carmeraControler1.Size = new Size(387, 280);
            carmeraControler1.TabIndex = 11;
            carmeraControler1.Load += carmeraControler_Load;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Control;
            ClientSize = new Size(1199, 696);
            Controls.Add(carmeraControler1);
            Controls.Add(lgCamera1);
            Controls.Add(samsungCamera1);
            Controls.Add(pnDlpFrame);
            Controls.Add(pnMioFrame);
            Controls.Add(bbbb);
            Controls.Add(btnAddMioFrame);
            Controls.Add(groupBox1);
            Controls.Add(menuStrip1);
            DoubleBuffered = true;
            MainMenuStrip = menuStrip1;
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "TMCS_PRJ";
            FormClosing += MainForm_FormClosing;
            Load += MainForm_Load;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
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
        private MenuStrip menuStrip1;
        private ToolStripMenuItem 파일ToolStripMenuItem;
        private ToolStripMenuItem 보기ToolStripMenuItem;
        private ToolStripMenuItem 장비등록정보확인ToolStripMenuItem;
        private Panel pnDlpFrame;
        private Label lblTest;
        private SamsungCamera samsungCamera1;
        private LGCamera lgCamera1;
        private CarmeraControler carmeraControler1;
    }
}