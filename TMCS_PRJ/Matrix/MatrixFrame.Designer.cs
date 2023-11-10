namespace TMCS_PRJ
{
    partial class MatrixFrame
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
            dgvMatrixChannelList = new DataGridView();
            cms = new ContextMenuStrip(components);
            이름바꾸기ToolStripMenuItem = new ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)dgvMatrixChannelList).BeginInit();
            cms.SuspendLayout();
            SuspendLayout();
            // 
            // dgvMatrixChannelList
            // 
            dgvMatrixChannelList.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvMatrixChannelList.Dock = DockStyle.Fill;
            dgvMatrixChannelList.Location = new Point(0, 0);
            dgvMatrixChannelList.Margin = new Padding(0);
            dgvMatrixChannelList.Name = "dgvMatrixChannelList";
            dgvMatrixChannelList.RowTemplate.Height = 25;
            dgvMatrixChannelList.Size = new Size(150, 150);
            dgvMatrixChannelList.TabIndex = 0;
            // 
            // cms
            // 
            cms.Items.AddRange(new ToolStripItem[] { 이름바꾸기ToolStripMenuItem });
            cms.Name = "cmx";
            cms.Size = new Size(135, 26);
            // 
            // 이름바꾸기ToolStripMenuItem
            // 
            이름바꾸기ToolStripMenuItem.Name = "이름바꾸기ToolStripMenuItem";
            이름바꾸기ToolStripMenuItem.Size = new Size(134, 22);
            이름바꾸기ToolStripMenuItem.Text = "이름바꾸기";
            이름바꾸기ToolStripMenuItem.Click += 이름바꾸기ToolStripMenuItem_Click;
            // 
            // MatrixFrame
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(dgvMatrixChannelList);
            Name = "MatrixFrame";
            ((System.ComponentModel.ISupportInitialize)dgvMatrixChannelList).EndInit();
            cms.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dgvMatrixChannelList;
        private ContextMenuStrip cms;
        private ToolStripMenuItem 이름바꾸기ToolStripMenuItem;
    }
}
