namespace AI_Tetris
{
    partial class form_help
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
            this.label1 = new System.Windows.Forms.Label();
            this.label_Maker = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("문체부 제목 돋음체", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.Location = new System.Drawing.Point(10, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(430, 112);
            this.label1.TabIndex = 0;
            this.label1.Text = "실행될 유전정보 및 계산식은 \r\nmeta_info.txt 파일에 저장됩니다.\r\n적응도(점수)와 가중치 정보는 \r\ngenetic_info.txt 파" +
    "일에 저장됩니다.";
            // 
            // label_Maker
            // 
            this.label_Maker.AutoSize = true;
            this.label_Maker.Font = new System.Drawing.Font("굴림", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label_Maker.Location = new System.Drawing.Point(12, 137);
            this.label_Maker.Name = "label_Maker";
            this.label_Maker.Size = new System.Drawing.Size(296, 17);
            this.label_Maker.TabIndex = 1;
            this.label_Maker.Text = "만든이 - 성공회대학교 홍민석, 박찬영";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(0, 15);
            this.label3.TabIndex = 2;
            // 
            // form_help
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Azure;
            this.ClientSize = new System.Drawing.Size(533, 159);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label_Maker);
            this.Controls.Add(this.label1);
            this.Name = "form_help";
            this.Text = "form_help";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label_Maker;
        private System.Windows.Forms.Label label3;
    }
}