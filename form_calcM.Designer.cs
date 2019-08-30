namespace AI_Tetris
{
    partial class form_calcM
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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.Setting = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.Manual1 = new System.Windows.Forms.Label();
            this.Manual2 = new System.Windows.Forms.Label();
            this.Current_formula = new System.Windows.Forms.Label();
            this.Notice_Board = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(15, 246);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(483, 62);
            this.textBox1.TabIndex = 0;
            // 
            // Setting
            // 
            this.Setting.BackColor = System.Drawing.Color.LightCyan;
            this.Setting.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Setting.Location = new System.Drawing.Point(505, 246);
            this.Setting.Name = "Setting";
            this.Setting.Size = new System.Drawing.Size(111, 62);
            this.Setting.TabIndex = 1;
            this.Setting.Text = "설 정";
            this.Setting.UseVisualStyleBackColor = false;
            this.Setting.Click += new System.EventHandler(this.Setting_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.LightCyan;
            this.button2.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button2.Location = new System.Drawing.Point(505, 12);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(111, 61);
            this.button2.TabIndex = 2;
            this.button2.Text = "계산식\r\n초기화";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // Manual1
            // 
            this.Manual1.AutoSize = true;
            this.Manual1.Font = new System.Drawing.Font("굴림", 12F);
            this.Manual1.Location = new System.Drawing.Point(12, 12);
            this.Manual1.Name = "Manual1";
            this.Manual1.Size = new System.Drawing.Size(328, 120);
            this.Manual1.TabIndex = 3;
            this.Manual1.Text = "F - 현재 높이                 \r\nS - 블록사이 빈공간(수직)\r\nT - 밀착공간(수직)\r\nO - 현재 블록의 평균높이\r\nf - " +
    "블록을 쌓았을 경우의 평균 높이\r\ns - 블록을 쌓았을 경우의 측면 수";
            // 
            // Manual2
            // 
            this.Manual2.AutoSize = true;
            this.Manual2.Font = new System.Drawing.Font("굴림", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Manual2.ForeColor = System.Drawing.Color.LightCoral;
            this.Manual2.Location = new System.Drawing.Point(13, 311);
            this.Manual2.Name = "Manual2";
            this.Manual2.Size = new System.Drawing.Size(616, 34);
            this.Manual2.TabIndex = 4;
            this.Manual2.Text = "※ 가중치는 W로 입력해주세요. 가중치 번호는 내부적으로 자동완성합니다.\r\n소괄호 사용 가능하며, 입력하지 않은 가중치는 계산되지 않습니다.";
            // 
            // Current_formula
            // 
            this.Current_formula.AutoSize = true;
            this.Current_formula.Location = new System.Drawing.Point(12, 146);
            this.Current_formula.Name = "Current_formula";
            this.Current_formula.Size = new System.Drawing.Size(117, 15);
            this.Current_formula.TabIndex = 5;
            this.Current_formula.Text = "설정된 계산식 \"\"";
            // 
            // Notice_Board
            // 
            this.Notice_Board.AutoSize = true;
            this.Notice_Board.Location = new System.Drawing.Point(12, 228);
            this.Notice_Board.Name = "Notice_Board";
            this.Notice_Board.Size = new System.Drawing.Size(167, 15);
            this.Notice_Board.TabIndex = 6;
            this.Notice_Board.Text = "계산식을 입력해주세요.";
            // 
            // form_calcM
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Azure;
            this.ClientSize = new System.Drawing.Size(628, 354);
            this.Controls.Add(this.Notice_Board);
            this.Controls.Add(this.Current_formula);
            this.Controls.Add(this.Manual2);
            this.Controls.Add(this.Manual1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.Setting);
            this.Controls.Add(this.textBox1);
            this.Name = "form_calcM";
            this.Text = "form_calculateModel";
            this.Load += new System.EventHandler(this.form_calculateModel_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button Setting;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label Manual1;
        private System.Windows.Forms.Label Manual2;
        private System.Windows.Forms.Label Current_formula;
        private System.Windows.Forms.Label Notice_Board;
    }
}