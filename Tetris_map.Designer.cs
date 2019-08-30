namespace AI_Tetris
{
    partial class Tetris_Map
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

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.Map = new System.Windows.Forms.Panel();
            this.Control_Moving = new System.Windows.Forms.Timer(this.components);
            this.Drawing_Map = new System.Windows.Forms.Timer(this.components);
            this.label_score = new System.Windows.Forms.Label();
            this.label_Height = new System.Windows.Forms.Label();
            this.Evaluation = new System.Windows.Forms.Panel();
            this.label_H_Difference = new System.Windows.Forms.Label();
            this.label_FutureHeight = new System.Windows.Forms.Label();
            this.label_Side = new System.Windows.Forms.Label();
            this.label_Final = new System.Windows.Forms.Label();
            this.label_AverageHeight = new System.Windows.Forms.Label();
            this.label_Adjacent = new System.Windows.Forms.Label();
            this.label_Empty = new System.Windows.Forms.Label();
            this.Program_Indicator = new System.Windows.Forms.Timer(this.components);
            this.Game = new System.Windows.Forms.Button();
            this.label_Search = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label_Serial = new System.Windows.Forms.Label();
            this.label_Weight = new System.Windows.Forms.Label();
            this.label_Generation = new System.Windows.Forms.Label();
            this.label_Gene = new System.Windows.Forms.Label();
            this.Gene_Generator = new System.Windows.Forms.Button();
            this.Evaluate_Gene = new System.Windows.Forms.Button();
            this.System_Log = new System.Windows.Forms.ListBox();
            this.Heredity = new System.Windows.Forms.Timer(this.components);
            this.Evaluate_Generation = new System.Windows.Forms.Button();
            this.Learning_heredity = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.Fast_Down = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.ToolStripMenuItem_set = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_calculateModel = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStrip_Pattern = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_help = new System.Windows.Forms.ToolStripMenuItem();
            this.Show_Search = new System.Windows.Forms.Button();
            this.Evaluation.SuspendLayout();
            this.panel1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Map
            // 
            this.Map.Location = new System.Drawing.Point(68, 43);
            this.Map.Name = "Map";
            this.Map.Size = new System.Drawing.Size(240, 400);
            this.Map.TabIndex = 0;
            this.Map.Paint += new System.Windows.Forms.PaintEventHandler(this.Map_Paint_1);
            // 
            // Control_Moving
            // 
            this.Control_Moving.Interval = 50;
            this.Control_Moving.Tick += new System.EventHandler(this.Control_Moving_Tick);
            // 
            // Drawing_Map
            // 
            this.Drawing_Map.Enabled = true;
            this.Drawing_Map.Interval = 50;
            this.Drawing_Map.Tick += new System.EventHandler(this.Drawing_Map_Tick);
            // 
            // label_score
            // 
            this.label_score.AutoSize = true;
            this.label_score.Font = new System.Drawing.Font("함초롬바탕 확장", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label_score.Location = new System.Drawing.Point(4, 52);
            this.label_score.Name = "label_score";
            this.label_score.Size = new System.Drawing.Size(63, 19);
            this.label_score.TabIndex = 1;
            this.label_score.Text = "SCORE";
            // 
            // label_Height
            // 
            this.label_Height.AutoSize = true;
            this.label_Height.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label_Height.Location = new System.Drawing.Point(3, 9);
            this.label_Height.Name = "label_Height";
            this.label_Height.Size = new System.Drawing.Size(67, 15);
            this.label_Height.TabIndex = 0;
            this.label_Height.Text = "현재높이";
            // 
            // Evaluation
            // 
            this.Evaluation.Controls.Add(this.label_H_Difference);
            this.Evaluation.Controls.Add(this.label_FutureHeight);
            this.Evaluation.Controls.Add(this.label_Side);
            this.Evaluation.Controls.Add(this.label_Final);
            this.Evaluation.Controls.Add(this.label_AverageHeight);
            this.Evaluation.Controls.Add(this.label_Adjacent);
            this.Evaluation.Controls.Add(this.label_Empty);
            this.Evaluation.Controls.Add(this.label_Height);
            this.Evaluation.Location = new System.Drawing.Point(369, 52);
            this.Evaluation.Name = "Evaluation";
            this.Evaluation.Size = new System.Drawing.Size(666, 254);
            this.Evaluation.TabIndex = 2;
            this.Evaluation.Paint += new System.Windows.Forms.PaintEventHandler(this.Evaluation_Paint);
            // 
            // label_H_Difference
            // 
            this.label_H_Difference.AutoSize = true;
            this.label_H_Difference.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label_H_Difference.Location = new System.Drawing.Point(4, 136);
            this.label_H_Difference.Name = "label_H_Difference";
            this.label_H_Difference.Size = new System.Drawing.Size(87, 15);
            this.label_H_Difference.TabIndex = 12;
            this.label_H_Difference.Text = "평균 높이차";
            // 
            // label_FutureHeight
            // 
            this.label_FutureHeight.AutoSize = true;
            this.label_FutureHeight.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label_FutureHeight.Location = new System.Drawing.Point(3, 96);
            this.label_FutureHeight.Name = "label_FutureHeight";
            this.label_FutureHeight.Size = new System.Drawing.Size(102, 15);
            this.label_FutureHeight.TabIndex = 11;
            this.label_FutureHeight.Text = "미래 평균높이";
            // 
            // label_Side
            // 
            this.label_Side.AutoSize = true;
            this.label_Side.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label_Side.Location = new System.Drawing.Point(3, 81);
            this.label_Side.Name = "label_Side";
            this.label_Side.Size = new System.Drawing.Size(37, 15);
            this.label_Side.TabIndex = 10;
            this.label_Side.Text = "측면";
            // 
            // label_Final
            // 
            this.label_Final.AutoSize = true;
            this.label_Final.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label_Final.Location = new System.Drawing.Point(4, 153);
            this.label_Final.Name = "label_Final";
            this.label_Final.Size = new System.Drawing.Size(87, 15);
            this.label_Final.TabIndex = 9;
            this.label_Final.Text = "최종 탐색값";
            // 
            // label_AverageHeight
            // 
            this.label_AverageHeight.AutoSize = true;
            this.label_AverageHeight.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label_AverageHeight.Location = new System.Drawing.Point(3, 39);
            this.label_AverageHeight.Name = "label_AverageHeight";
            this.label_AverageHeight.Size = new System.Drawing.Size(67, 15);
            this.label_AverageHeight.TabIndex = 4;
            this.label_AverageHeight.Text = "평균높이";
            // 
            // label_Adjacent
            // 
            this.label_Adjacent.AutoSize = true;
            this.label_Adjacent.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label_Adjacent.Location = new System.Drawing.Point(4, 184);
            this.label_Adjacent.Name = "label_Adjacent";
            this.label_Adjacent.Size = new System.Drawing.Size(72, 15);
            this.label_Adjacent.TabIndex = 3;
            this.label_Adjacent.Text = "밀착 공간";
            // 
            // label_Empty
            // 
            this.label_Empty.AutoSize = true;
            this.label_Empty.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label_Empty.Location = new System.Drawing.Point(3, 24);
            this.label_Empty.Name = "label_Empty";
            this.label_Empty.Size = new System.Drawing.Size(52, 15);
            this.label_Empty.TabIndex = 3;
            this.label_Empty.Text = "빈공간";
            // 
            // Program_Indicator
            // 
            this.Program_Indicator.Interval = 50;
            this.Program_Indicator.Tick += new System.EventHandler(this.Program_Indicator_Tick);
            // 
            // Game
            // 
            this.Game.BackColor = System.Drawing.Color.LightCyan;
            this.Game.Font = new System.Drawing.Font("문체부 돋음체", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Game.Location = new System.Drawing.Point(1041, 52);
            this.Game.Name = "Game";
            this.Game.Size = new System.Drawing.Size(133, 57);
            this.Game.TabIndex = 4;
            this.Game.Text = "시작/멈춤";
            this.Game.UseVisualStyleBackColor = false;
            this.Game.Click += new System.EventHandler(this.Game_Click);
            // 
            // label_Search
            // 
            this.label_Search.AutoSize = true;
            this.label_Search.Font = new System.Drawing.Font("문체부 돋음체", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label_Search.Location = new System.Drawing.Point(366, 34);
            this.label_Search.Name = "label_Search";
            this.label_Search.Size = new System.Drawing.Size(92, 17);
            this.label_Search.TabIndex = 6;
            this.label_Search.Text = "탐색(열, 값)";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label_Serial);
            this.panel1.Controls.Add(this.label_Weight);
            this.panel1.Controls.Add(this.label_Generation);
            this.panel1.Location = new System.Drawing.Point(369, 331);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(666, 69);
            this.panel1.TabIndex = 7;
            // 
            // label_Serial
            // 
            this.label_Serial.AutoSize = true;
            this.label_Serial.Font = new System.Drawing.Font("문체부 돋음체", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label_Serial.Location = new System.Drawing.Point(3, 29);
            this.label_Serial.Name = "label_Serial";
            this.label_Serial.Size = new System.Drawing.Size(45, 19);
            this.label_Serial.TabIndex = 15;
            this.label_Serial.Text = "번호";
            // 
            // label_Weight
            // 
            this.label_Weight.AutoSize = true;
            this.label_Weight.Font = new System.Drawing.Font("문체부 돋음체", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label_Weight.Location = new System.Drawing.Point(2, 48);
            this.label_Weight.Name = "label_Weight";
            this.label_Weight.Size = new System.Drawing.Size(63, 19);
            this.label_Weight.TabIndex = 17;
            this.label_Weight.Text = "가중치";
            // 
            // label_Generation
            // 
            this.label_Generation.AutoSize = true;
            this.label_Generation.Font = new System.Drawing.Font("문체부 돋음체", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label_Generation.Location = new System.Drawing.Point(3, 5);
            this.label_Generation.Name = "label_Generation";
            this.label_Generation.Size = new System.Drawing.Size(45, 19);
            this.label_Generation.TabIndex = 1;
            this.label_Generation.Text = "세대";
            // 
            // label_Gene
            // 
            this.label_Gene.AutoSize = true;
            this.label_Gene.Font = new System.Drawing.Font("문체부 돋음체", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label_Gene.Location = new System.Drawing.Point(365, 309);
            this.label_Gene.Name = "label_Gene";
            this.label_Gene.Size = new System.Drawing.Size(63, 19);
            this.label_Gene.TabIndex = 0;
            this.label_Gene.Text = "유전자";
            // 
            // Gene_Generator
            // 
            this.Gene_Generator.BackColor = System.Drawing.Color.LightCyan;
            this.Gene_Generator.Font = new System.Drawing.Font("문체부 돋음체", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Gene_Generator.Location = new System.Drawing.Point(1041, 303);
            this.Gene_Generator.Name = "Gene_Generator";
            this.Gene_Generator.Size = new System.Drawing.Size(133, 57);
            this.Gene_Generator.TabIndex = 8;
            this.Gene_Generator.Text = "유전자 \r\n초기생성";
            this.Gene_Generator.UseVisualStyleBackColor = false;
            this.Gene_Generator.Click += new System.EventHandler(this.Gene_Generator_Click);
            // 
            // Evaluate_Gene
            // 
            this.Evaluate_Gene.BackColor = System.Drawing.Color.LightCyan;
            this.Evaluate_Gene.Font = new System.Drawing.Font("문체부 돋음체", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Evaluate_Gene.Location = new System.Drawing.Point(1041, 366);
            this.Evaluate_Gene.Name = "Evaluate_Gene";
            this.Evaluate_Gene.Size = new System.Drawing.Size(133, 59);
            this.Evaluate_Gene.TabIndex = 13;
            this.Evaluate_Gene.Text = "유전자 \r\n평가하기";
            this.Evaluate_Gene.UseVisualStyleBackColor = false;
            this.Evaluate_Gene.Click += new System.EventHandler(this.Evaluate_Gene_Click);
            // 
            // System_Log
            // 
            this.System_Log.FormattingEnabled = true;
            this.System_Log.ItemHeight = 15;
            this.System_Log.Location = new System.Drawing.Point(369, 406);
            this.System_Log.Name = "System_Log";
            this.System_Log.Size = new System.Drawing.Size(666, 214);
            this.System_Log.TabIndex = 14;
            // 
            // Heredity
            // 
            this.Heredity.Enabled = true;
            this.Heredity.Tick += new System.EventHandler(this.Heredity_Tick);
            // 
            // Evaluate_Generation
            // 
            this.Evaluate_Generation.BackColor = System.Drawing.Color.LightCyan;
            this.Evaluate_Generation.Font = new System.Drawing.Font("문체부 돋음체", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Evaluate_Generation.Location = new System.Drawing.Point(1041, 431);
            this.Evaluate_Generation.Name = "Evaluate_Generation";
            this.Evaluate_Generation.Size = new System.Drawing.Size(133, 61);
            this.Evaluate_Generation.TabIndex = 15;
            this.Evaluate_Generation.Text = "유전자 세대\r\n전체 평가";
            this.Evaluate_Generation.UseVisualStyleBackColor = false;
            this.Evaluate_Generation.Click += new System.EventHandler(this.Evaluate_Generation_Click);
            // 
            // Learning_heredity
            // 
            this.Learning_heredity.BackColor = System.Drawing.Color.LightCyan;
            this.Learning_heredity.Font = new System.Drawing.Font("문체부 돋음체", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Learning_heredity.Location = new System.Drawing.Point(1041, 498);
            this.Learning_heredity.Name = "Learning_heredity";
            this.Learning_heredity.Size = new System.Drawing.Size(133, 40);
            this.Learning_heredity.TabIndex = 16;
            this.Learning_heredity.Text = "유전 학습";
            this.Learning_heredity.UseVisualStyleBackColor = false;
            this.Learning_heredity.Click += new System.EventHandler(this.Learning_heredity_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(27, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(0, 15);
            this.label3.TabIndex = 18;
            // 
            // Fast_Down
            // 
            this.Fast_Down.BackColor = System.Drawing.Color.LightCyan;
            this.Fast_Down.Font = new System.Drawing.Font("문체부 돋음체", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Fast_Down.Location = new System.Drawing.Point(1041, 185);
            this.Fast_Down.Name = "Fast_Down";
            this.Fast_Down.Size = new System.Drawing.Size(133, 64);
            this.Fast_Down.TabIndex = 19;
            this.Fast_Down.Text = "빠른 진행\r\n켜기/끄기\r\n";
            this.Fast_Down.UseVisualStyleBackColor = false;
            this.Fast_Down.Click += new System.EventHandler(this.Fast_Down_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.LightCyan;
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItem_set,
            this.ToolStripMenuItem_calculateModel,
            this.ToolStrip_Pattern,
            this.ToolStripMenuItem_help});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1181, 28);
            this.menuStrip1.TabIndex = 20;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // ToolStripMenuItem_set
            // 
            this.ToolStripMenuItem_set.Name = "ToolStripMenuItem_set";
            this.ToolStripMenuItem_set.Size = new System.Drawing.Size(136, 24);
            this.ToolStripMenuItem_set.Text = "러닝 유전자 설정";
            this.ToolStripMenuItem_set.Click += new System.EventHandler(this.ToolStripMenuItem_set_Click);
            // 
            // ToolStripMenuItem_calculateModel
            // 
            this.ToolStripMenuItem_calculateModel.Name = "ToolStripMenuItem_calculateModel";
            this.ToolStripMenuItem_calculateModel.Size = new System.Drawing.Size(121, 24);
            this.ToolStripMenuItem_calculateModel.Text = "계산 모델 설정";
            this.ToolStripMenuItem_calculateModel.Click += new System.EventHandler(this.ToolStripMenuItem_calculateModel_Click);
            // 
            // ToolStrip_Pattern
            // 
            this.ToolStrip_Pattern.Name = "ToolStrip_Pattern";
            this.ToolStrip_Pattern.Size = new System.Drawing.Size(121, 24);
            this.ToolStrip_Pattern.Text = "패턴 규칙 설정";
            this.ToolStrip_Pattern.Click += new System.EventHandler(this.ToolStrip_Pattern_Click);
            // 
            // ToolStripMenuItem_help
            // 
            this.ToolStripMenuItem_help.Name = "ToolStripMenuItem_help";
            this.ToolStripMenuItem_help.Size = new System.Drawing.Size(66, 24);
            this.ToolStripMenuItem_help.Text = "도움말";
            this.ToolStripMenuItem_help.Click += new System.EventHandler(this.ToolStripMenuItem_help_Click);
            // 
            // Show_Search
            // 
            this.Show_Search.BackColor = System.Drawing.Color.LightCyan;
            this.Show_Search.Font = new System.Drawing.Font("문체부 돋음체", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Show_Search.Location = new System.Drawing.Point(1041, 115);
            this.Show_Search.Name = "Show_Search";
            this.Show_Search.Size = new System.Drawing.Size(133, 64);
            this.Show_Search.TabIndex = 21;
            this.Show_Search.Text = "탐색값 \r\n보기/숨기기";
            this.Show_Search.UseVisualStyleBackColor = false;
            this.Show_Search.Click += new System.EventHandler(this.Show_Search_Click);
            // 
            // Tetris_Map
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Azure;
            this.ClientSize = new System.Drawing.Size(1181, 632);
            this.Controls.Add(this.Show_Search);
            this.Controls.Add(this.Fast_Down);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.Learning_heredity);
            this.Controls.Add(this.Evaluate_Generation);
            this.Controls.Add(this.System_Log);
            this.Controls.Add(this.label_Gene);
            this.Controls.Add(this.Evaluate_Gene);
            this.Controls.Add(this.Gene_Generator);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label_Search);
            this.Controls.Add(this.Game);
            this.Controls.Add(this.Evaluation);
            this.Controls.Add(this.label_score);
            this.Controls.Add(this.Map);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Tetris_Map";
            this.Text = "유전 알고리즘 테트리스";
            this.Load += new System.EventHandler(this.Tetris_Map_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Tetris_Map_KeyDown);
            this.Evaluation.ResumeLayout(false);
            this.Evaluation.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel Map;
        private System.Windows.Forms.Timer Control_Moving;
        private System.Windows.Forms.Timer Drawing_Map;
        private System.Windows.Forms.Label label_score;
        private System.Windows.Forms.Label label_Height;
        private System.Windows.Forms.Panel Evaluation;
        private System.Windows.Forms.Timer Program_Indicator;
        private System.Windows.Forms.Label label_Empty;
        private System.Windows.Forms.Label label_Adjacent;
        private System.Windows.Forms.Button Game;
        private System.Windows.Forms.Label label_AverageHeight;
        private System.Windows.Forms.Label label_Search;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label_Generation;
        private System.Windows.Forms.Label label_Gene;
        private System.Windows.Forms.Button Gene_Generator;
        private System.Windows.Forms.Label label_Final;
        private System.Windows.Forms.Label label_Side;
        private System.Windows.Forms.Label label_FutureHeight;
        private System.Windows.Forms.Label label_H_Difference;
        private System.Windows.Forms.Button Evaluate_Gene;
        private System.Windows.Forms.ListBox System_Log;
        private System.Windows.Forms.Label label_Serial;
        private System.Windows.Forms.Timer Heredity;
        private System.Windows.Forms.Button Evaluate_Generation;
        private System.Windows.Forms.Button Learning_heredity;
        private System.Windows.Forms.Label label_Weight;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button Fast_Down;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_set;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_help;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_calculateModel;
        private System.Windows.Forms.ToolStripMenuItem ToolStrip_Pattern;
        private System.Windows.Forms.Button Show_Search;
    }
}

