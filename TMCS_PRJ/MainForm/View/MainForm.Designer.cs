using LshCamera;

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
            matrixFrame1 = new LshMatrix.MatrixFrame();
            btnMatrixOutput = new Button();
            btnMatrixInput = new Button();
            btnAddMioFrame = new Button();
            pnMioFrame = new Panel();
            menuStrip1 = new MenuStrip();
            파일ToolStripMenuItem = new ToolStripMenuItem();
            보기ToolStripMenuItem = new ToolStripMenuItem();
            장비등록정보확인ToolStripMenuItem = new ToolStripMenuItem();
            pnDlpFrame = new Panel();
            ucCamera1 = new CameraFrame();
            ucCamera2 = new CameraFrame();
            button1 = new Button();
            ucCameraControler = new CarmeraControlerFrame();
            groupBox1.SuspendLayout();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(matrixFrame1);
            groupBox1.Controls.Add(btnMatrixOutput);
            groupBox1.Controls.Add(btnMatrixInput);
            groupBox1.Location = new Point(12, 37);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(298, 684);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "groupBox1";
            // 
            // matrixFrame1
            // 
            matrixFrame1.Location = new Point(6, 51);
            matrixFrame1.Name = "matrixFrame1";
            matrixFrame1.Size = new Size(286, 474);
            matrixFrame1.TabIndex = 3;
            matrixFrame1.Load += mFrame_Load;
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
            // pnMioFrame
            // 
            pnMioFrame.Location = new Point(316, 320);
            pnMioFrame.Name = "pnMioFrame";
            pnMioFrame.Size = new Size(484, 175);
            pnMioFrame.TabIndex = 3;
            // 
            // menuStrip1
            // 
            menuStrip1.BackColor = Color.Silver;
            menuStrip1.ImageScalingSize = new Size(20, 20);
            menuStrip1.Items.AddRange(new ToolStripItem[] { 파일ToolStripMenuItem, 보기ToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1904, 24);
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
            // ucCamera1
            // 
            ucCamera1.BackColor = Color.Transparent;
            ucCamera1.CameraId = 1;
            ucCamera1.CameraName = "엘지";
            ucCamera1.Location = new Point(1074, 470);
            ucCamera1.Name = "ucCamera1";
            ucCamera1.Protocol = null;
            ucCamera1.Size = new Size(51, 45);
            ucCamera1.TabIndex = 8;
            // 
            // ucCamera2
            // 
            ucCamera2.BackColor = Color.Transparent;
            ucCamera2.CameraId = 2;
            ucCamera2.CameraName = "삼성";
            ucCamera2.Location = new Point(1074, 398);
            ucCamera2.Name = "ucCamera2";
            ucCamera2.Protocol = null;
            ucCamera2.Size = new Size(51, 45);
            ucCamera2.TabIndex = 9;
            // 
            // button1
            // 
            button1.Location = new Point(616, 581);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 10;
            button1.Text = "button1";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // ucCameraControler
            // 
            ucCameraControler.BackColor = Color.FromArgb(64, 64, 64);
            ucCameraControler.Location = new Point(975, 569);
            ucCameraControler.Name = "ucCameraControler";
            ucCameraControler.Size = new Size(917, 460);
            ucCameraControler.TabIndex = 0;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Control;
            ClientSize = new Size(1904, 1041);
            Controls.Add(ucCameraControler);
            Controls.Add(button1);
            Controls.Add(ucCamera2);
            Controls.Add(ucCamera1);
            Controls.Add(pnDlpFrame);
            Controls.Add(pnMioFrame);
            Controls.Add(btnAddMioFrame);
            Controls.Add(groupBox1);
            Controls.Add(menuStrip1);
            DoubleBuffered = true;
            MainMenuStrip = menuStrip1;
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "TMCS";
            FormClosing += MainForm_FormClosing;
            Load += MainForm_Load;
            KeyDown += MainForm_KeyDown;
            groupBox1.ResumeLayout(false);
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private GroupBox groupBox1;
        private Button btnMatrixOutput;
        private Button btnMatrixInput;
        private Button btnAddMioFrame;
        private Panel pnMioFrame;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem 파일ToolStripMenuItem;
        private ToolStripMenuItem 보기ToolStripMenuItem;
        private ToolStripMenuItem 장비등록정보확인ToolStripMenuItem;
        private Panel pnDlpFrame;
        private CarmeraControlerFrame carmeraControler1;
        private CameraFrame camera_ViscaType1;
        private CameraFrame camera_ViscaType2;
        private CameraFrame ucCamera1;
        private CameraFrame ucCamera2;
        private Button button1;
        private LshMatrix.MatrixFrame matrixFrame1;
        private CarmeraControlerFrame ucCameraControler;
    }
}