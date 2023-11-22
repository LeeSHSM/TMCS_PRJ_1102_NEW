namespace LshDlp
{
    internal partial class DlpFrame
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
            components = new System.ComponentModel.Container();
            picDlpFrame = new PictureBox();
            cms = new ContextMenuStrip(components);
            출력포트확인ToolStripMenuItem = new ToolStripMenuItem();
            출력포트변경ToolStripMenuItem = new ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)picDlpFrame).BeginInit();
            cms.SuspendLayout();
            SuspendLayout();
            // 
            // picDlpFrame
            // 
            picDlpFrame.Dock = DockStyle.Fill;
            picDlpFrame.Location = new Point(0, 0);
            picDlpFrame.Margin = new Padding(0);
            picDlpFrame.Name = "picDlpFrame";
            picDlpFrame.Size = new Size(150, 150);
            picDlpFrame.TabIndex = 0;
            picDlpFrame.TabStop = false;
            // 
            // cms
            // 
            cms.Items.AddRange(new ToolStripItem[] { 출력포트확인ToolStripMenuItem, 출력포트변경ToolStripMenuItem });
            cms.Name = "cms";
            cms.Size = new Size(181, 70);
            // 
            // 출력포트확인ToolStripMenuItem
            // 
            출력포트확인ToolStripMenuItem.Name = "출력포트확인ToolStripMenuItem";
            출력포트확인ToolStripMenuItem.Size = new Size(180, 22);
            출력포트확인ToolStripMenuItem.Text = "출력포트 확인하기";
            출력포트확인ToolStripMenuItem.Click += 출력포트확인ToolStripMenuItem_Click;
            // 
            // 출력포트변경ToolStripMenuItem1
            // 
            출력포트변경ToolStripMenuItem.Name = "출력포트변경ToolStripMenuItem";
            출력포트변경ToolStripMenuItem.Size = new Size(180, 22);
            출력포트변경ToolStripMenuItem.Text = "출력포트 변경";
            // 
            // DlpFrame
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(picDlpFrame);
            Name = "DlpFrame";
            ((System.ComponentModel.ISupportInitialize)picDlpFrame).EndInit();
            cms.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private PictureBox picDlpFrame;
        private ContextMenuStrip cms;
        private ToolStripMenuItem 출력포트확인ToolStripMenuItem;
        private ToolStripMenuItem 출력포트변경ToolStripMenuItem;
    }
}
