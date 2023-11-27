namespace LshCamera
{
    partial class CarmeraControlerFrame
    {
        /// <summary> 
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 구성 요소 디자이너에서 생성한 코드

        /// <summary> 
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            tblArrow = new TableLayoutPanel();
            tableLayoutPanel1 = new TableLayoutPanel();
            btnLeft = new Button();
            btnDown = new Button();
            btnRight = new Button();
            btnUp = new Button();
            tableLayoutPanel3 = new TableLayoutPanel();
            pnPreeset = new Panel();
            tableLayoutPanel4 = new TableLayoutPanel();
            lblPanTiltSpeed = new Label();
            lblZoomSpeed = new Label();
            label2 = new Label();
            scrollbarZoomSpeed = new VScrollBar();
            scrollbarPanTiltSpeed = new VScrollBar();
            label1 = new Label();
            tblMainBackground = new TableLayoutPanel();
            tblArrow.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel3.SuspendLayout();
            tableLayoutPanel4.SuspendLayout();
            tblMainBackground.SuspendLayout();
            SuspendLayout();
            // 
            // tblArrow
            // 
            tblArrow.ColumnCount = 2;
            tblArrow.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 250F));
            tblArrow.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tblArrow.Controls.Add(tableLayoutPanel1, 0, 0);
            tblArrow.Controls.Add(tableLayoutPanel3, 1, 0);
            tblArrow.Dock = DockStyle.Fill;
            tblArrow.Location = new Point(3, 3);
            tblArrow.Margin = new Padding(0);
            tblArrow.Name = "tblArrow";
            tblArrow.RowCount = 1;
            tblArrow.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tblArrow.Size = new Size(860, 230);
            tblArrow.TabIndex = 8;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 3;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel1.Controls.Add(btnLeft, 0, 1);
            tableLayoutPanel1.Controls.Add(btnDown, 1, 2);
            tableLayoutPanel1.Controls.Add(btnRight, 2, 1);
            tableLayoutPanel1.Controls.Add(btnUp, 1, 0);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(3, 3);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 3;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel1.Size = new Size(244, 224);
            tableLayoutPanel1.TabIndex = 4;
            // 
            // btnLeft
            // 
            btnLeft.BackColor = Color.White;
            btnLeft.BackgroundImage = TMCS_PRJ.Properties.Resources.LeftArrow;
            btnLeft.BackgroundImageLayout = ImageLayout.Zoom;
            btnLeft.Dock = DockStyle.Fill;
            btnLeft.Location = new Point(0, 74);
            btnLeft.Margin = new Padding(0);
            btnLeft.Name = "btnLeft";
            btnLeft.Size = new Size(81, 74);
            btnLeft.TabIndex = 2;
            btnLeft.TabStop = false;
            btnLeft.UseVisualStyleBackColor = false;
            // 
            // btnDown
            // 
            btnDown.BackColor = Color.White;
            btnDown.BackgroundImage = TMCS_PRJ.Properties.Resources.BotArrow;
            btnDown.BackgroundImageLayout = ImageLayout.Zoom;
            btnDown.Dock = DockStyle.Fill;
            btnDown.Location = new Point(81, 148);
            btnDown.Margin = new Padding(0);
            btnDown.Name = "btnDown";
            btnDown.Size = new Size(81, 76);
            btnDown.TabIndex = 3;
            btnDown.TabStop = false;
            btnDown.UseVisualStyleBackColor = false;
            // 
            // btnRight
            // 
            btnRight.BackColor = Color.White;
            btnRight.BackgroundImage = TMCS_PRJ.Properties.Resources.RightArrow;
            btnRight.BackgroundImageLayout = ImageLayout.Zoom;
            btnRight.Dock = DockStyle.Fill;
            btnRight.Location = new Point(162, 74);
            btnRight.Margin = new Padding(0);
            btnRight.Name = "btnRight";
            btnRight.Size = new Size(82, 74);
            btnRight.TabIndex = 0;
            btnRight.TabStop = false;
            btnRight.UseVisualStyleBackColor = false;
            // 
            // btnUp
            // 
            btnUp.BackColor = Color.White;
            btnUp.BackgroundImage = TMCS_PRJ.Properties.Resources.UpArrow;
            btnUp.BackgroundImageLayout = ImageLayout.Zoom;
            btnUp.Dock = DockStyle.Fill;
            btnUp.Location = new Point(81, 0);
            btnUp.Margin = new Padding(0);
            btnUp.Name = "btnUp";
            btnUp.Size = new Size(81, 74);
            btnUp.TabIndex = 1;
            btnUp.TabStop = false;
            btnUp.UseVisualStyleBackColor = false;
            // 
            // tableLayoutPanel3
            // 
            tableLayoutPanel3.ColumnCount = 2;
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 23.5F));
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 76.5F));
            tableLayoutPanel3.Controls.Add(pnPreeset, 1, 0);
            tableLayoutPanel3.Controls.Add(tableLayoutPanel4, 0, 0);
            tableLayoutPanel3.Dock = DockStyle.Fill;
            tableLayoutPanel3.Location = new Point(250, 0);
            tableLayoutPanel3.Margin = new Padding(0);
            tableLayoutPanel3.Name = "tableLayoutPanel3";
            tableLayoutPanel3.RowCount = 1;
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel3.Size = new Size(610, 230);
            tableLayoutPanel3.TabIndex = 5;
            // 
            // pnPreeset
            // 
            pnPreeset.BackColor = Color.FromArgb(224, 224, 224);
            pnPreeset.Dock = DockStyle.Fill;
            pnPreeset.Location = new Point(146, 3);
            pnPreeset.Name = "pnPreeset";
            pnPreeset.Size = new Size(461, 224);
            pnPreeset.TabIndex = 11;
            // 
            // tableLayoutPanel4
            // 
            tableLayoutPanel4.ColumnCount = 2;
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel4.Controls.Add(lblPanTiltSpeed, 0, 2);
            tableLayoutPanel4.Controls.Add(lblZoomSpeed, 0, 2);
            tableLayoutPanel4.Controls.Add(label2, 1, 0);
            tableLayoutPanel4.Controls.Add(scrollbarZoomSpeed, 1, 1);
            tableLayoutPanel4.Controls.Add(scrollbarPanTiltSpeed, 0, 1);
            tableLayoutPanel4.Controls.Add(label1, 0, 0);
            tableLayoutPanel4.Dock = DockStyle.Fill;
            tableLayoutPanel4.Location = new Point(0, 0);
            tableLayoutPanel4.Margin = new Padding(0);
            tableLayoutPanel4.Name = "tableLayoutPanel4";
            tableLayoutPanel4.RowCount = 3;
            tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tableLayoutPanel4.Size = new Size(143, 230);
            tableLayoutPanel4.TabIndex = 7;
            // 
            // lblPanTiltSpeed
            // 
            lblPanTiltSpeed.AutoSize = true;
            lblPanTiltSpeed.BackColor = Color.FromArgb(75, 75, 75);
            lblPanTiltSpeed.BorderStyle = BorderStyle.FixedSingle;
            lblPanTiltSpeed.Dock = DockStyle.Fill;
            lblPanTiltSpeed.ForeColor = Color.White;
            lblPanTiltSpeed.Location = new Point(8, 203);
            lblPanTiltSpeed.Margin = new Padding(8, 3, 8, 3);
            lblPanTiltSpeed.Name = "lblPanTiltSpeed";
            lblPanTiltSpeed.Size = new Size(55, 24);
            lblPanTiltSpeed.TabIndex = 11;
            lblPanTiltSpeed.Text = "-";
            lblPanTiltSpeed.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblZoomSpeed
            // 
            lblZoomSpeed.AutoSize = true;
            lblZoomSpeed.BackColor = Color.FromArgb(75, 75, 75);
            lblZoomSpeed.BorderStyle = BorderStyle.FixedSingle;
            lblZoomSpeed.Dock = DockStyle.Fill;
            lblZoomSpeed.ForeColor = Color.White;
            lblZoomSpeed.Location = new Point(79, 203);
            lblZoomSpeed.Margin = new Padding(8, 3, 8, 3);
            lblZoomSpeed.Name = "lblZoomSpeed";
            lblZoomSpeed.Size = new Size(56, 24);
            lblZoomSpeed.TabIndex = 10;
            lblZoomSpeed.Text = "-";
            lblZoomSpeed.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.FromArgb(75, 75, 75);
            label2.BorderStyle = BorderStyle.FixedSingle;
            label2.Dock = DockStyle.Fill;
            label2.ForeColor = Color.White;
            label2.Location = new Point(79, 3);
            label2.Margin = new Padding(8, 3, 8, 3);
            label2.Name = "label2";
            label2.Size = new Size(56, 34);
            label2.TabIndex = 9;
            label2.Text = "zoom";
            label2.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // scrollbarZoomSpeed
            // 
            scrollbarZoomSpeed.Dock = DockStyle.Fill;
            scrollbarZoomSpeed.LargeChange = 1;
            scrollbarZoomSpeed.Location = new Point(79, 43);
            scrollbarZoomSpeed.Margin = new Padding(8, 3, 8, 3);
            scrollbarZoomSpeed.Maximum = 24;
            scrollbarZoomSpeed.Minimum = 1;
            scrollbarZoomSpeed.Name = "scrollbarZoomSpeed";
            scrollbarZoomSpeed.ScaleScrollBarForDpiChange = false;
            scrollbarZoomSpeed.Size = new Size(56, 154);
            scrollbarZoomSpeed.TabIndex = 7;
            scrollbarZoomSpeed.Value = 1;
            // 
            // scrollbarPanTiltSpeed
            // 
            scrollbarPanTiltSpeed.Dock = DockStyle.Fill;
            scrollbarPanTiltSpeed.LargeChange = 1;
            scrollbarPanTiltSpeed.Location = new Point(8, 43);
            scrollbarPanTiltSpeed.Margin = new Padding(8, 3, 8, 3);
            scrollbarPanTiltSpeed.Maximum = 24;
            scrollbarPanTiltSpeed.Minimum = 1;
            scrollbarPanTiltSpeed.Name = "scrollbarPanTiltSpeed";
            scrollbarPanTiltSpeed.ScaleScrollBarForDpiChange = false;
            scrollbarPanTiltSpeed.Size = new Size(55, 154);
            scrollbarPanTiltSpeed.TabIndex = 6;
            scrollbarPanTiltSpeed.Value = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.FromArgb(75, 75, 75);
            label1.BorderStyle = BorderStyle.FixedSingle;
            label1.Dock = DockStyle.Fill;
            label1.ForeColor = Color.White;
            label1.Location = new Point(8, 3);
            label1.Margin = new Padding(8, 3, 8, 3);
            label1.Name = "label1";
            label1.Size = new Size(55, 34);
            label1.TabIndex = 8;
            label1.Text = "PanTilt";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // tblMainBackground
            // 
            tblMainBackground.ColumnCount = 1;
            tblMainBackground.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tblMainBackground.Controls.Add(tblArrow, 0, 0);
            tblMainBackground.Dock = DockStyle.Fill;
            tblMainBackground.Location = new Point(0, 0);
            tblMainBackground.Margin = new Padding(0);
            tblMainBackground.Name = "tblMainBackground";
            tblMainBackground.Padding = new Padding(3);
            tblMainBackground.RowCount = 2;
            tblMainBackground.RowStyles.Add(new RowStyle(SizeType.Percent, 45F));
            tblMainBackground.RowStyles.Add(new RowStyle(SizeType.Percent, 55F));
            tblMainBackground.Size = new Size(866, 518);
            tblMainBackground.TabIndex = 10;
            // 
            // CarmeraControlerFrame
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(64, 64, 64);
            Controls.Add(tblMainBackground);
            Name = "CarmeraControlerFrame";
            Size = new Size(866, 518);
            tblArrow.ResumeLayout(false);
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel3.ResumeLayout(false);
            tableLayoutPanel4.ResumeLayout(false);
            tableLayoutPanel4.PerformLayout();
            tblMainBackground.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private Label ll;
        private TableLayoutPanel tblArrow;
        private TableLayoutPanel tableLayoutPanel1;
        private Button btnLeft;
        private Button btnDown;
        private Button btnRight;
        private Button btnUp;
        private TableLayoutPanel tableLayoutPanel3;
        private TableLayoutPanel tableLayoutPanel4;
        private Label lblPanTiltSpeed;
        private Label lblZoomSpeed;
        private Label label2;
        private VScrollBar scrollbarZoomSpeed;
        private VScrollBar scrollbarPanTiltSpeed;
        private Label label1;
        private TableLayoutPanel tblMainBackground;
        private Panel pnPreeset;
    }
}
