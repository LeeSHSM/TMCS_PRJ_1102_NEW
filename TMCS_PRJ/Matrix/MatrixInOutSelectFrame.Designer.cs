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
            lblOutput = new Label();
            lblInput = new Label();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(lblOutput, 0, 0);
            tableLayoutPanel1.Controls.Add(lblInput, 0, 1);
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
            // lblOutput
            // 
            lblOutput.BorderStyle = BorderStyle.FixedSingle;
            lblOutput.Dock = DockStyle.Fill;
            lblOutput.Location = new Point(0, 0);
            lblOutput.Margin = new Padding(0);
            lblOutput.Name = "lblOutput";
            lblOutput.Size = new Size(150, 52);
            lblOutput.TabIndex = 0;
            lblOutput.Text = "-";
            lblOutput.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblInput
            // 
            lblInput.BorderStyle = BorderStyle.FixedSingle;
            lblInput.Dock = DockStyle.Fill;
            lblInput.Location = new Point(0, 52);
            lblInput.Margin = new Padding(0);
            lblInput.Name = "lblInput";
            lblInput.Size = new Size(150, 98);
            lblInput.TabIndex = 1;
            lblInput.Text = "-";
            lblInput.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // MatrixInOutSelectFrame
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(tableLayoutPanel1);
            Name = "MatrixInOutSelectFrame";
            tableLayoutPanel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private Label lblOutput;
        private Label lblInput;
    }
}
