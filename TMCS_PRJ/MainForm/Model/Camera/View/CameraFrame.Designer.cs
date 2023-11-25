namespace LshCamera
{
    partial class CameraFrame
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
            picCamera = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)picCamera).BeginInit();
            SuspendLayout();
            // 
            // picCamera
            // 
            picCamera.BackColor = Color.Transparent;
            picCamera.BackgroundImage = TMCS_PRJ.Properties.Resources.cctv1;
            picCamera.BackgroundImageLayout = ImageLayout.Stretch;
            picCamera.Dock = DockStyle.Fill;
            picCamera.Location = new Point(0, 0);
            picCamera.Name = "picCamera";
            picCamera.Size = new Size(150, 150);
            picCamera.TabIndex = 0;
            picCamera.TabStop = false;
            // 
            // CameraFrame
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Transparent;
            Controls.Add(picCamera);
            Name = "CameraFrame";
            MouseUp += Camera_MouseUp;
            ((System.ComponentModel.ISupportInitialize)picCamera).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox picCamera;
    }
}
