namespace TMCS_PRJ
{
    partial class MatrixInOutSelectFrame
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
            tableLayoutPanel1 = new TableLayoutPanel();
            mcOutput = new MatrixChannel();
            mcInput = new MatrixChannel();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(mcOutput, 0, 0);
            tableLayoutPanel1.Controls.Add(mcInput, 0, 1);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Margin = new Padding(0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 35F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 65F));
            tableLayoutPanel1.Size = new Size(150, 150);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // mcOutput
            // 
            mcOutput.AutoSize = true;
            mcOutput.BorderStyle = BorderStyle.FixedSingle;
            mcOutput.ChannelName = null;
            mcOutput.ChannelType = null;
            mcOutput.Dock = DockStyle.Fill;
            mcOutput.Location = new Point(0, 0);
            mcOutput.Margin = new Padding(0);
            mcOutput.Name = "mcOutput";
            mcOutput.Port = 0;
            mcOutput.RouteNo = 0;
            mcOutput.Size = new Size(150, 52);
            mcOutput.TabIndex = 0;
            mcOutput.Text = "-";
            mcOutput.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // mcInput
            // 
            mcInput.AutoSize = true;
            mcInput.BorderStyle = BorderStyle.FixedSingle;
            mcInput.ChannelName = null;
            mcInput.ChannelType = null;
            mcInput.Dock = DockStyle.Fill;
            mcInput.Location = new Point(0, 52);
            mcInput.Margin = new Padding(0);
            mcInput.Name = "mcInput";
            mcInput.Port = 0;
            mcInput.RouteNo = 0;
            mcInput.Size = new Size(150, 98);
            mcInput.TabIndex = 1;
            mcInput.Text = "-";
            mcInput.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // MatrixInOutSelectFrame
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(tableLayoutPanel1);
            Name = "MatrixInOutSelectFrame";
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private MatrixChannel mcOutput;
        private MatrixChannel mcInput;
    }
}
