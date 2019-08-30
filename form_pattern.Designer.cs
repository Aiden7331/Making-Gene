namespace AI_Tetris
{
    partial class form_pattern
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
            this.textBox_good = new System.Windows.Forms.TextBox();
            this.label_goodRegulation = new System.Windows.Forms.Label();
            this.label_badRegulation = new System.Windows.Forms.Label();
            this.textBox_bad = new System.Windows.Forms.TextBox();
            this.button_Apply = new System.Windows.Forms.Button();
            this.button_Cancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBox_good
            // 
            this.textBox_good.Location = new System.Drawing.Point(12, 27);
            this.textBox_good.Multiline = true;
            this.textBox_good.Name = "textBox_good";
            this.textBox_good.Size = new System.Drawing.Size(471, 174);
            this.textBox_good.TabIndex = 0;
            // 
            // label_goodRegulation
            // 
            this.label_goodRegulation.AutoSize = true;
            this.label_goodRegulation.Location = new System.Drawing.Point(12, 9);
            this.label_goodRegulation.Name = "label_goodRegulation";
            this.label_goodRegulation.Size = new System.Drawing.Size(72, 15);
            this.label_goodRegulation.TabIndex = 1;
            this.label_goodRegulation.Text = "좋은 규칙";
            // 
            // label_badRegulation
            // 
            this.label_badRegulation.AutoSize = true;
            this.label_badRegulation.Location = new System.Drawing.Point(12, 204);
            this.label_badRegulation.Name = "label_badRegulation";
            this.label_badRegulation.Size = new System.Drawing.Size(72, 15);
            this.label_badRegulation.TabIndex = 2;
            this.label_badRegulation.Text = "나쁜 규칙";
            // 
            // textBox_bad
            // 
            this.textBox_bad.Location = new System.Drawing.Point(12, 222);
            this.textBox_bad.Multiline = true;
            this.textBox_bad.Name = "textBox_bad";
            this.textBox_bad.Size = new System.Drawing.Size(471, 216);
            this.textBox_bad.TabIndex = 3;
            // 
            // button_Apply
            // 
            this.button_Apply.BackColor = System.Drawing.Color.LightCyan;
            this.button_Apply.Location = new System.Drawing.Point(343, 444);
            this.button_Apply.Name = "button_Apply";
            this.button_Apply.Size = new System.Drawing.Size(138, 41);
            this.button_Apply.TabIndex = 4;
            this.button_Apply.Text = "적용";
            this.button_Apply.UseVisualStyleBackColor = false;
            this.button_Apply.Click += new System.EventHandler(this.button_Apply_Click);
            // 
            // button_Cancel
            // 
            this.button_Cancel.BackColor = System.Drawing.Color.LightCyan;
            this.button_Cancel.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button_Cancel.Location = new System.Drawing.Point(199, 444);
            this.button_Cancel.Name = "button_Cancel";
            this.button_Cancel.Size = new System.Drawing.Size(138, 41);
            this.button_Cancel.TabIndex = 5;
            this.button_Cancel.Text = "취소";
            this.button_Cancel.UseVisualStyleBackColor = false;
            this.button_Cancel.Click += new System.EventHandler(this.button_Cancel_Click);
            // 
            // form_pattern
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Azure;
            this.ClientSize = new System.Drawing.Size(493, 497);
            this.Controls.Add(this.button_Cancel);
            this.Controls.Add(this.button_Apply);
            this.Controls.Add(this.textBox_bad);
            this.Controls.Add(this.label_badRegulation);
            this.Controls.Add(this.label_goodRegulation);
            this.Controls.Add(this.textBox_good);
            this.Name = "form_pattern";
            this.Text = "패턴 규칙 설정";
            this.Load += new System.EventHandler(this.form_pattern_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox_good;
        private System.Windows.Forms.Label label_goodRegulation;
        private System.Windows.Forms.Label label_badRegulation;
        private System.Windows.Forms.TextBox textBox_bad;
        private System.Windows.Forms.Button button_Apply;
        private System.Windows.Forms.Button button_Cancel;
    }
}