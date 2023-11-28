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
            btnMatrixOutput = new LSHComponent.LshButton();
            btnMatrixInput = new LSHComponent.LshButton();
            matrixFrame1 = new LshMatrix.MatrixFrame();
            pnMioFrame = new Panel();
            menuStrip1 = new MenuStrip();
            파일ToolStripMenuItem = new ToolStripMenuItem();
            보기ToolStripMenuItem = new ToolStripMenuItem();
            장비등록정보확인ToolStripMenuItem = new ToolStripMenuItem();
            pnDlpFrame = new Panel();
            ucCamera1 = new CameraFrame();
            ucCamera2 = new CameraFrame();
            ucCameraControler = new CarmeraControlerFrame();
            btnAddMioFrame = new LSHComponent.LshButton();
            groupBox2 = new GroupBox();
            groupBox3 = new GroupBox();
            cameraFrame2 = new CameraFrame();
            cameraFrame1 = new CameraFrame();
            groupBox4 = new GroupBox();
            groupBox1.SuspendLayout();
            menuStrip1.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox3.SuspendLayout();
            groupBox4.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(btnMatrixOutput);
            groupBox1.Controls.Add(btnMatrixInput);
            groupBox1.Controls.Add(matrixFrame1);
            groupBox1.Font = new Font("맑은 고딕", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            groupBox1.ForeColor = Color.White;
            groupBox1.Location = new Point(12, 37);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(298, 645);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Matrix In/Out";
            // 
            // btnMatrixOutput
            // 
            btnMatrixOutput.BackColor = Color.FromArgb(75, 75, 75);
            btnMatrixOutput.FlatAppearance.BorderColor = Color.White;
            btnMatrixOutput.FlatStyle = FlatStyle.Flat;
            btnMatrixOutput.Font = new Font("맑은 고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            btnMatrixOutput.ForeColor = Color.White;
            btnMatrixOutput.Location = new Point(211, 26);
            btnMatrixOutput.Name = "btnMatrixOutput";
            btnMatrixOutput.Size = new Size(80, 28);
            btnMatrixOutput.TabIndex = 5;
            btnMatrixOutput.Text = "출 력";
            btnMatrixOutput.UseVisualStyleBackColor = false;
            // 
            // btnMatrixInput
            // 
            btnMatrixInput.BackColor = Color.FromArgb(75, 75, 75);
            btnMatrixInput.FlatAppearance.BorderColor = Color.White;
            btnMatrixInput.FlatStyle = FlatStyle.Flat;
            btnMatrixInput.Font = new Font("맑은 고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            btnMatrixInput.ForeColor = Color.White;
            btnMatrixInput.Location = new Point(125, 26);
            btnMatrixInput.Name = "btnMatrixInput";
            btnMatrixInput.Size = new Size(80, 28);
            btnMatrixInput.TabIndex = 4;
            btnMatrixInput.Text = "입 력";
            btnMatrixInput.UseVisualStyleBackColor = false;
            // 
            // matrixFrame1
            // 
            matrixFrame1.Location = new Point(8, 77);
            matrixFrame1.Margin = new Padding(4);
            matrixFrame1.Name = "matrixFrame1";
            matrixFrame1.Size = new Size(283, 561);
            matrixFrame1.TabIndex = 3;
            matrixFrame1.Load += mFrame_Load;
            // 
            // pnMioFrame
            // 
            pnMioFrame.Location = new Point(28, 56);
            pnMioFrame.Name = "pnMioFrame";
            pnMioFrame.Size = new Size(622, 249);
            pnMioFrame.TabIndex = 3;
            // 
            // menuStrip1
            // 
            menuStrip1.BackColor = Color.FromArgb(30, 30, 30);
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
            파일ToolStripMenuItem.ForeColor = Color.White;
            파일ToolStripMenuItem.Name = "파일ToolStripMenuItem";
            파일ToolStripMenuItem.Size = new Size(43, 20);
            파일ToolStripMenuItem.Text = "파일";
            // 
            // 보기ToolStripMenuItem
            // 
            보기ToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { 장비등록정보확인ToolStripMenuItem });
            보기ToolStripMenuItem.ForeColor = Color.White;
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
            pnDlpFrame.Location = new Point(332, 47);
            pnDlpFrame.Name = "pnDlpFrame";
            pnDlpFrame.Size = new Size(656, 318);
            pnDlpFrame.TabIndex = 6;
            // 
            // ucCamera1
            // 
            ucCamera1.BackColor = Color.Transparent;
            ucCamera1.CameraId = 1;
            ucCamera1.CameraName = "엘지";
            ucCamera1.Location = new Point(421, 540);
            ucCamera1.Margin = new Padding(4);
            ucCamera1.Name = "ucCamera1";
            ucCamera1.PresetGroup = null;
            ucCamera1.Protocol = null;
            ucCamera1.Size = new Size(66, 60);
            ucCamera1.TabIndex = 8;
            // 
            // ucCamera2
            // 
            ucCamera2.BackColor = Color.Transparent;
            ucCamera2.CameraId = 2;
            ucCamera2.CameraName = "삼성";
            ucCamera2.Location = new Point(64, 280);
            ucCamera2.Margin = new Padding(4);
            ucCamera2.Name = "ucCamera2";
            ucCamera2.PresetGroup = null;
            ucCamera2.Protocol = null;
            ucCamera2.Size = new Size(66, 60);
            ucCamera2.TabIndex = 9;
            // 
            // ucCameraControler
            // 
            ucCameraControler.BackColor = Color.FromArgb(64, 64, 64);
            ucCameraControler.Location = new Point(7, 27);
            ucCameraControler.Margin = new Padding(4);
            ucCameraControler.Name = "ucCameraControler";
            ucCameraControler.Size = new Size(872, 307);
            ucCameraControler.TabIndex = 0;
            // 
            // btnAddMioFrame
            // 
            btnAddMioFrame.BackColor = Color.FromArgb(75, 75, 75);
            btnAddMioFrame.FlatAppearance.BorderColor = Color.White;
            btnAddMioFrame.FlatStyle = FlatStyle.Flat;
            btnAddMioFrame.Font = new Font("맑은 고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            btnAddMioFrame.ForeColor = Color.White;
            btnAddMioFrame.Location = new Point(570, 22);
            btnAddMioFrame.Name = "btnAddMioFrame";
            btnAddMioFrame.Size = new Size(80, 28);
            btnAddMioFrame.TabIndex = 10;
            btnAddMioFrame.Text = "추 가";
            btnAddMioFrame.UseVisualStyleBackColor = false;
            btnAddMioFrame.Click += btnAddMioFrame_Click_1;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(pnMioFrame);
            groupBox2.Controls.Add(btnAddMioFrame);
            groupBox2.Font = new Font("맑은 고딕", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            groupBox2.ForeColor = Color.White;
            groupBox2.Location = new Point(332, 371);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(656, 311);
            groupBox2.TabIndex = 11;
            groupBox2.TabStop = false;
            groupBox2.Text = "In/Out ";
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(cameraFrame2);
            groupBox3.Controls.Add(cameraFrame1);
            groupBox3.Controls.Add(ucCamera2);
            groupBox3.Controls.Add(ucCamera1);
            groupBox3.Font = new Font("맑은 고딕", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            groupBox3.ForeColor = Color.White;
            groupBox3.Location = new Point(1006, 37);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(886, 645);
            groupBox3.TabIndex = 12;
            groupBox3.TabStop = false;
            groupBox3.Text = "Room";
            // 
            // cameraFrame2
            // 
            cameraFrame2.BackColor = Color.Transparent;
            cameraFrame2.CameraId = 2;
            cameraFrame2.CameraName = "삼성";
            cameraFrame2.Location = new Point(421, 57);
            cameraFrame2.Margin = new Padding(4);
            cameraFrame2.Name = "cameraFrame2";
            cameraFrame2.PresetGroup = null;
            cameraFrame2.Protocol = null;
            cameraFrame2.Size = new Size(66, 60);
            cameraFrame2.TabIndex = 11;
            // 
            // cameraFrame1
            // 
            cameraFrame1.BackColor = Color.Transparent;
            cameraFrame1.CameraId = 2;
            cameraFrame1.CameraName = "삼성";
            cameraFrame1.Location = new Point(784, 280);
            cameraFrame1.Margin = new Padding(4);
            cameraFrame1.Name = "cameraFrame1";
            cameraFrame1.PresetGroup = null;
            cameraFrame1.Protocol = null;
            cameraFrame1.Size = new Size(66, 60);
            cameraFrame1.TabIndex = 10;
            // 
            // groupBox4
            // 
            groupBox4.Controls.Add(ucCameraControler);
            groupBox4.Font = new Font("맑은 고딕", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            groupBox4.ForeColor = Color.White;
            groupBox4.Location = new Point(1006, 688);
            groupBox4.Name = "groupBox4";
            groupBox4.Size = new Size(886, 341);
            groupBox4.TabIndex = 13;
            groupBox4.TabStop = false;
            groupBox4.Text = "Camera Controler";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = Color.FromArgb(30, 30, 30);
            ClientSize = new Size(1904, 1041);
            Controls.Add(groupBox4);
            Controls.Add(groupBox3);
            Controls.Add(groupBox2);
            Controls.Add(pnDlpFrame);
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
            groupBox2.ResumeLayout(false);
            groupBox3.ResumeLayout(false);
            groupBox4.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private GroupBox groupBox1;
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
        private LshMatrix.MatrixFrame matrixFrame1;
        private CarmeraControlerFrame ucCameraControler;
        private LSHComponent.LshButton btnMatrixInput;
        private LSHComponent.LshButton btnMatrixOutput;
        private LSHComponent.LshButton btnAddMioFrame;
        private GroupBox groupBox2;
        private GroupBox groupBox3;
        private CameraFrame cameraFrame1;
        private CameraFrame cameraFrame2;
        private GroupBox groupBox4;
    }
}