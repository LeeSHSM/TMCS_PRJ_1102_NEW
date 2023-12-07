namespace TMCS_PRJ
{
    partial class EquipmentStatusForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            lblIp = new Label();
            label1 = new Label();
            label2 = new Label();
            CameraStatus = new Label();
            label4 = new Label();
            matrixStatus = new Label();
            label3 = new Label();
            cameraInfo = new Label();
            SuspendLayout();
            // 
            // lblIp
            // 
            lblIp.AutoSize = true;
            lblIp.Location = new Point(94, 22);
            lblIp.Name = "lblIp";
            lblIp.Size = new Size(39, 15);
            lblIp.TabIndex = 0;
            lblIp.Text = "label1";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(26, 22);
            label1.Name = "label1";
            label1.Size = new Size(62, 15);
            label1.TabIndex = 1;
            label1.Text = "MatrixIP : ";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(26, 160);
            label2.Name = "label2";
            label2.Size = new Size(158, 15);
            label2.TabIndex = 3;
            label2.Text = "Camera AmxServer Status : ";
            // 
            // CameraStatus
            // 
            CameraStatus.AutoSize = true;
            CameraStatus.Location = new Point(180, 160);
            CameraStatus.Name = "CameraStatus";
            CameraStatus.Size = new Size(76, 15);
            CameraStatus.TabIndex = 2;
            CameraStatus.Text = "접속시도중...";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(26, 53);
            label4.Name = "label4";
            label4.Size = new Size(126, 15);
            label4.TabIndex = 4;
            label4.Text = "Matrix Server Status : ";
            // 
            // matrixStatus
            // 
            matrixStatus.AutoSize = true;
            matrixStatus.Location = new Point(158, 53);
            matrixStatus.Name = "matrixStatus";
            matrixStatus.Size = new Size(76, 15);
            matrixStatus.TabIndex = 5;
            matrixStatus.Text = "접속시도중...";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(26, 131);
            label3.Name = "label3";
            label3.Size = new Size(170, 15);
            label3.TabIndex = 7;
            label3.Text = "Camera AmxServer Ip / Port : ";
            // 
            // cameraInfo
            // 
            cameraInfo.AutoSize = true;
            cameraInfo.Location = new Point(202, 131);
            cameraInfo.Name = "cameraInfo";
            cameraInfo.Size = new Size(76, 15);
            cameraInfo.TabIndex = 6;
            cameraInfo.Text = "접속시도중...";
            // 
            // EquipmentStatusForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(label3);
            Controls.Add(cameraInfo);
            Controls.Add(matrixStatus);
            Controls.Add(label4);
            Controls.Add(label2);
            Controls.Add(CameraStatus);
            Controls.Add(label1);
            Controls.Add(lblIp);
            Name = "EquipmentStatusForm";
            Text = "EquipmentStatusForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblIp;
        private Label label1;
        private Label label2;
        private Label CameraStatus;
        private Label label4;
        private Label matrixStatus;
        private Label label3;
        private Label cameraInfo;
    }
}