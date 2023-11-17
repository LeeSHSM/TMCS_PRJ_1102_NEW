namespace TMCS_PRJ
{
    partial class CarmeraControler
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
            SuspendLayout();
            // 
            // btnRight
            // 
            btnRight.BackColor = Color.White;
            btnRight.BackgroundImage = Properties.Resources.RightArrow;
            btnRight.BackgroundImageLayout = ImageLayout.Zoom;
            btnRight.Location = new Point(212, 105);
            btnRight.Margin = new Padding(0);
            btnRight.Name = "btnRight";
            btnRight.Size = new Size(60, 60);
            btnRight.TabIndex = 0;
            btnRight.UseVisualStyleBackColor = false;
            // 
            // btnUp
            // 
            btnUp.BackColor = Color.White;
            btnUp.BackgroundImage = Properties.Resources.UpArrow;
            btnUp.BackgroundImageLayout = ImageLayout.Zoom;
            btnUp.Location = new Point(152, 45);
            btnUp.Margin = new Padding(0);
            btnUp.Name = "btnUp";
            btnUp.Size = new Size(60, 60);
            btnUp.TabIndex = 1;
            btnUp.UseVisualStyleBackColor = false;
            // 
            // btnLeft
            // 
            btnLeft.BackColor = Color.White;
            btnLeft.BackgroundImage = Properties.Resources.LeftArrow;
            btnLeft.BackgroundImageLayout = ImageLayout.Zoom;
            btnLeft.Location = new Point(92, 105);
            btnLeft.Margin = new Padding(0);
            btnLeft.Name = "btnLeft";
            btnLeft.Size = new Size(60, 60);
            btnLeft.TabIndex = 2;
            btnLeft.UseVisualStyleBackColor = false;
            // 
            // btnBot
            // 
            btnBot.BackColor = Color.White;
            btnBot.BackgroundImage = Properties.Resources.BotArrow;
            btnBot.BackgroundImageLayout = ImageLayout.Zoom;
            btnBot.Location = new Point(152, 165);
            btnBot.Margin = new Padding(0);
            btnBot.Name = "btnBot";
            btnBot.Size = new Size(60, 60);
            btnBot.TabIndex = 3;
            btnBot.UseVisualStyleBackColor = false;
            // 
            // CarmeraControler
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Black;
            Controls.Add(btnBot);
            Controls.Add(btnLeft);
            Controls.Add(btnUp);
            Controls.Add(btnRight);
            Name = "CarmeraControler";
            Size = new Size(387, 280);
            ResumeLayout(false);
        }

        #endregion

        private Button btnRight;
        private Button btnUp;
        private Button btnLeft;
        private Button btnBot;
    }
}
