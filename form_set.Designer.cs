namespace AI_Tetris
{
    partial class form_set
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
            this.label_search_generation = new System.Windows.Forms.Label();
            this.label_search_serial = new System.Windows.Forms.Label();
            this.textBox_generation = new System.Windows.Forms.TextBox();
            this.textBox_Serial = new System.Windows.Forms.TextBox();
            this.button_Cancel = new System.Windows.Forms.Button();
            this.button_Submit = new System.Windows.Forms.Button();
            this.Notice = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label_search_generation
            // 
            this.label_search_generation.AutoSize = true;
            this.label_search_generation.Location = new System.Drawing.Point(-1, 25);
            this.label_search_generation.Name = "label_search_generation";
            this.label_search_generation.Size = new System.Drawing.Size(87, 15);
            this.label_search_generation.TabIndex = 0;
            this.label_search_generation.Text = "학습할 세대";
            // 
            // label_search_serial
            // 
            this.label_search_serial.AutoSize = true;
            this.label_search_serial.Location = new System.Drawing.Point(-1, 54);
            this.label_search_serial.Name = "label_search_serial";
            this.label_search_serial.Size = new System.Drawing.Size(102, 15);
            this.label_search_serial.TabIndex = 1;
            this.label_search_serial.Text = "유전자 인덱스";
            // 
            // textBox_generation
            // 
            this.textBox_generation.Location = new System.Drawing.Point(127, 22);
            this.textBox_generation.Name = "textBox_generation";
            this.textBox_generation.Size = new System.Drawing.Size(130, 25);
            this.textBox_generation.TabIndex = 2;
            // 
            // textBox_Serial
            // 
            this.textBox_Serial.Location = new System.Drawing.Point(127, 51);
            this.textBox_Serial.Name = "textBox_Serial";
            this.textBox_Serial.Size = new System.Drawing.Size(130, 25);
            this.textBox_Serial.TabIndex = 3;
            // 
            // button_Cancel
            // 
            this.button_Cancel.BackColor = System.Drawing.Color.LightCyan;
            this.button_Cancel.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button_Cancel.Location = new System.Drawing.Point(127, 106);
            this.button_Cancel.Name = "button_Cancel";
            this.button_Cancel.Size = new System.Drawing.Size(130, 33);
            this.button_Cancel.TabIndex = 4;
            this.button_Cancel.Text = "취소";
            this.button_Cancel.UseVisualStyleBackColor = false;
            this.button_Cancel.Click += new System.EventHandler(this.button_Cancel_Click);
            // 
            // button_Submit
            // 
            this.button_Submit.BackColor = System.Drawing.Color.LightCyan;
            this.button_Submit.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button_Submit.Location = new System.Drawing.Point(2, 106);
            this.button_Submit.Name = "button_Submit";
            this.button_Submit.Size = new System.Drawing.Size(119, 33);
            this.button_Submit.TabIndex = 5;
            this.button_Submit.Text = "완료";
            this.button_Submit.UseVisualStyleBackColor = false;
            this.button_Submit.Click += new System.EventHandler(this.button_Submit_Click);
            // 
            // Notice
            // 
            this.Notice.AutoSize = true;
            this.Notice.Location = new System.Drawing.Point(12, 79);
            this.Notice.Name = "Notice";
            this.Notice.Size = new System.Drawing.Size(235, 15);
            this.Notice.TabIndex = 6;
            this.Notice.Text = "※인덱스는 0 에서 99 까지입니다.";
            // 
            // form_set
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Azure;
            this.ClientSize = new System.Drawing.Size(269, 151);
            this.Controls.Add(this.Notice);
            this.Controls.Add(this.button_Submit);
            this.Controls.Add(this.button_Cancel);
            this.Controls.Add(this.textBox_Serial);
            this.Controls.Add(this.textBox_generation);
            this.Controls.Add(this.label_search_serial);
            this.Controls.Add(this.label_search_generation);
            this.Name = "form_set";
            this.Text = "인덱스 설정";
            this.Load += new System.EventHandler(this.form_set_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label_search_generation;
        private System.Windows.Forms.Label label_search_serial;
        private System.Windows.Forms.TextBox textBox_generation;
        private System.Windows.Forms.TextBox textBox_Serial;
        private System.Windows.Forms.Button button_Cancel;
        private System.Windows.Forms.Button button_Submit;
        private System.Windows.Forms.Label Notice;
    }
}