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
            btnRight = new Button();
            btnUp = new Button();
            btnLeft = new Button();
            btnBot = new Button();
            tableLayoutPanel1 = new TableLayoutPanel();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // btnRight
            // 
            btnRight.BackColor = Color.White;
            btnRight.BackgroundImage = TMCS_PRJ.Properties.Resources.RightArrow;
            btnRight.BackgroundImageLayout = ImageLayout.Zoom;
            btnRight.Dock = DockStyle.Fill;
            btnRight.Location = new Point(100, 50);
            btnRight.Margin = new Padding(0);
            btnRight.Name = "btnRight";
            btnRight.Size = new Size(50, 50);
            btnRight.TabIndex = 0;
            btnRight.UseVisualStyleBackColor = false;
            // 
            // btnUp
            // 
            btnUp.BackColor = Color.White;
            btnUp.BackgroundImage = TMCS_PRJ.Properties.Resources.UpArrow;
            btnUp.BackgroundImageLayout = ImageLayout.Zoom;
            btnUp.Dock = DockStyle.Fill;
            btnUp.Location = new Point(50, 0);
            btnUp.Margin = new Padding(0);
            btnUp.Name = "btnUp";
            btnUp.Size = new Size(50, 50);
            btnUp.TabIndex = 1;
            btnUp.UseVisualStyleBackColor = false;
            // 
            // btnLeft
            // 
            btnLeft.BackColor = Color.White;
            btnLeft.BackgroundImage = TMCS_PRJ.Properties.Resources.LeftArrow;
            btnLeft.BackgroundImageLayout = ImageLayout.Zoom;
            btnLeft.Dock = DockStyle.Fill;
            btnLeft.Location = new Point(0, 50);
            btnLeft.Margin = new Padding(0);
            btnLeft.Name = "btnLeft";
            btnLeft.Size = new Size(50, 50);
            btnLeft.TabIndex = 2;
            btnLeft.UseVisualStyleBackColor = false;
            // 
            // btnBot
            // 
            btnBot.BackColor = Color.White;
            btnBot.BackgroundImage = TMCS_PRJ.Properties.Resources.BotArrow;
            btnBot.BackgroundImageLayout = ImageLayout.Zoom;
            btnBot.Dock = DockStyle.Fill;
            btnBot.Location = new Point(50, 100);
            btnBot.Margin = new Padding(0);
            btnBot.Name = "btnBot";
            btnBot.Size = new Size(50, 50);
            btnBot.TabIndex = 3;
            btnBot.UseVisualStyleBackColor = false;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 3;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel1.Controls.Add(btnLeft, 0, 1);
            tableLayoutPanel1.Controls.Add(btnBot, 1, 2);
            tableLayoutPanel1.Controls.Add(btnRight, 2, 1);
            tableLayoutPanel1.Controls.Add(btnUp, 1, 0);
            tableLayoutPanel1.Location = new Point(16, 15);
            tableLayoutPanel1.Margin = new Padding(5);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 3;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel1.Size = new Size(150, 150);
            tableLayoutPanel1.TabIndex = 4;
            // 
            // CarmeraControler
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Black;
            Controls.Add(tableLayoutPanel1);
            Name = "CarmeraControler";
            Size = new Size(325, 267);
            tableLayoutPanel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Button btnRight;
        private Button btnUp;
        private Button btnLeft;
        private Button btnBot;
        private TableLayoutPanel tableLayoutPanel1;
    }
}
