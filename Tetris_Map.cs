using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

/* 성공회대학교 정보통신공학과 201333038 홍민석 */

namespace AI_Tetris
{
    public partial class Tetris_Map : Form
    {
        Figure figure;
        Graphics graphics;
        Mapping mapping;
        Heuristics heuristics;
        Random Generator;
        Genetic Learning;
        CalculationNetwork Calculator;

        static int width = 240;//가로 길이(PX)
        static int height = 460;//세로 길이(PX)
        static int Interval = 20;//블럭 사이즈 및 간격
        static char GameOver = 'Q'; // 게임이 종료되었을때 사용
        
        /* 타이머 조절 */
        bool FLAG_DRAW_MAP = false;
        bool BlockArrival = false;
        bool Moving = false;

        /*블록 빨리 내리기 켜기/끄기 */
        bool fastClear = false;  //false면 빨리 내림, true면 천천히 내림.
        
        /*한번 평가*/
        bool OnceEvaluation = false;

        /*유전 학습 모드*/
        bool heredityMode = false;
        bool scoreWriting = false;
        bool evaluation = false;
        bool lastEvaluation = false;
        bool clickListener = false;
        bool NextEvaluation = false;

        /*게임 On Off*/
        bool OnOff = true;

        /*UI Controller*/
        bool trial; //form load 부분에서 설정
        bool useUI; //form load 부분에서 설정


        char BlockColor;
        char[,] temporary;

        int[] Meta_info;
        int Current_Block;
        int Next_Block;

        /*유전 학습*/
        int score;

        public Tetris_Map()
        {
            InitializeComponent();
        }

        private void Tetris_Map_Load(object sender, EventArgs e)
        {
            /*맵 그리기*/
            Map.Width = width;
            Map.Height = height;
            graphics = Map.CreateGraphics();
            figure = new Figure(Map, width / Interval, height / Interval);
            Map.BackColor = Color.White;

            /* 유전 알고리즘 삽입 */
            Learning = new Genetic();

            /*AI_객체 생성*/
            try
            {
                Calculator = new CalculationNetwork(Learning.get_Formula(), Learning.Load_Recent_Gene());
            }
            catch (Exception ex)
            {
                Calculator = new CalculationNetwork(Learning.get_Formula());
                Console.WriteLine(ex.Message);
            }
            heuristics = new Heuristics(width / Interval, height / Interval, Calculator);
            Generator = new Random();

            /*테트리스 객체 생성*/
            mapping = new Mapping(heuristics, width / Interval, height / Interval);

            /*UI Initializer*/
            trial = false;
            useUI = false;
            UI_Clear();
            Genetic_UI_Drawer();
        }


        private void Map_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void Moving_down(bool Decision) // true means that time pass, false means that block completely down.
        {
            if (Moving)
                while (Moving != true) ;//busy waiting.
            else
                Moving = true;

            if (Decision)
                BlockArrival = mapping.TimePass(BlockColor);
            else
                BlockArrival = mapping.Full_Down(BlockColor);

            FLAG_DRAW_MAP = mapping.get_Draw();
            if (BlockArrival == true)
            {
                if (BlockColor == GameOver)
                {
                    score = mapping.get_Score();
                    scoreWriting = true;
                    FLAG_DRAW_MAP = true;
                    Control_Moving.Stop();
                    //while (scoreWriting != false) ;//busy waiting.
                }
                else
                {
                    Current_Block = Next_Block;
                    BlockColor = mapping.init_Character(Current_Block, 5);
                    Next_Block = Generator.Next(6);
                }
                BlockArrival = false;
            }
            Moving = false;
        }

        private void Moving_down(bool Decision, bool fast) // true means that time pass, false means that block completely down.
        {
            if (Moving)
                while (Moving == true) ;//busy waiting
            else
                Moving = true;

            if (Decision)
                BlockArrival = mapping.TimePass(BlockColor);
            else
                BlockArrival = mapping.Full_Down(BlockColor);
            if (!fast)
                FLAG_DRAW_MAP = mapping.get_Draw();
            if (BlockArrival == true)
            {
                if (BlockColor == GameOver)
                {
                    score = mapping.get_Score();
                    scoreWriting = true;
                    FLAG_DRAW_MAP = true;
                    Control_Moving.Stop();
                    //while (scoreWriting != false) ;//busy waiting.
                }
                else
                {
                    Current_Block = Next_Block;
                    BlockColor = mapping.init_Character(Current_Block, 5);
                    Next_Block = Generator.Next(6);
                }
                BlockArrival = false;
            }
            Moving = false;
        }

        private void Control_Moving_Tick(object sender, EventArgs e)
        {
            Moving_down(true);
        }


        private void Drawing_Map_Tick(object sender, EventArgs e)
        {
            if (FLAG_DRAW_MAP == true)
            {
                temporary = mapping.get_Matrix();
                figure.DrawGame(temporary);
                FLAG_DRAW_MAP = false;
                mapping.set_Draw(false);
            }

            label_score.Text = "SCORE \n" + mapping.get_Score().ToString();

        }

        private void Tetris_Map_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Left)
            {
                //Control_Moving.Stop();
                mapping.Left_Move();
                //Control_Moving.Start();
            }
            if (e.KeyCode == Keys.Right)
            {
                //Control_Moving.Stop();
                mapping.Right_Move();
                //Control_Moving.Start();
            }
            if (e.KeyCode == Keys.Down)
            {
                mapping.Full_Down(BlockColor);
            }
            if (e.KeyCode == Keys.Up)
            {
                mapping.Block_Rotation(Current_Block);
            }
        }

        private void Program_Indicator_Tick(object sender, EventArgs e)
        {
            if (heuristics.get_TimerController() == true)
            {

                if (useUI)
                {
                    UI_Drawer_Int("현재높이", heuristics.get_Height(), label_Height);

                    UI_Drawer_Int("빈공간", heuristics.get_Empty_Space(), label_Empty);
                }


                /*블록 평가 */
                heuristics.set_Matrix(mapping.get_Matrix());
                heuristics.Evaluate_Block_Relevance(Current_Block, mapping.get_Matrix());

                label_Adjacent.Text = null;
                /*UI 부분 업로드*/
                if (useUI)
                {
                    for (int j = 0; j < heuristics.get_Under_Blocks().Length / 12; j++)
                    {
                        label_Adjacent.Text = label_Adjacent.Text + "밀착 공간(아래, 회전수" + j + ")";
                        for (int i = 0; i < width / Interval; i++)
                            label_Adjacent.Text = label_Adjacent.Text + " " + heuristics.get_Under_Blocks()[j, i];
                        label_Adjacent.Text = label_Adjacent.Text + "\n";
                    }
                    try
                    {
                        UI_Drawer("평균높이", heuristics.get_Average_Height(), label_AverageHeight, true);
                        UI_Drawer("미래 평균높이", heuristics.get_Heuristics_Height(), label_FutureHeight, true);


                        /*UI 부분 업로드*/
                        label_H_Difference.Text = "평균 높이차";
                        for (int i = 0; i < width / Interval; i++)
                            label_H_Difference.Text = label_H_Difference.Text + " " + (heuristics.get_Heuristics_Height()[i] - heuristics.get_Average_Height()[i]);

                    }
                    catch (NullReferenceException E)
                    {
                        Console.WriteLine(E.Message);
                    }
                }


                trial = true;
                heuristics.set_TimerController(false);
            }
            if (trial == false)
                return;
            AI_Manuplate();
        }

        private void Game_Click(object sender, EventArgs e)
        {
            if (OnOff == true)
            {
                Control_Moving.Stop();
                Drawing_Map.Stop();
                Program_Indicator.Stop();
                OnOff = false;
            }
            else
            {
                Control_Moving.Start();
                Drawing_Map.Start();
                Program_Indicator.Start();
                OnOff = true;
            }
        }

        private void Evaluation_Paint(object sender, PaintEventArgs e)
        {

        }

        private void AI_Manuplate()
        {
            int[] location;
            bool once = true;

            if (trial)
                trial = false;

            location = heuristics.Heuristic_Function(Current_Block);


            if (useUI)
            {
                UI_Drawer("측면공간", heuristics.get_Side_Block(), label_Side, false);
                UI_Drawer("최종 탐색값", heuristics.get_Heuristic_Value(), label_Final, true);
            }
            if (location[1] > 0)
            {
                Moving_down(true, true);
                Moving_down(true, true);
                once = false;
            }
            location = Location_Modifier(location);

            while (location[0] != mapping.get_User())
            {
                if (heuristics.get_Locate_Counter() > width / Interval)
                {
                    break;
                }
                if (location[0] < mapping.get_User())
                {
                    if (!mapping.Left_Move())
                    {
                        Console.WriteLine("FAIL LEFT MOVE!");
                        mapping.Reset_Rotation();
                        location = (int[])heuristics.Relocate(location).Clone();
                        if (once && location[1] > 0)
                        {
                            Moving_down(true, true);
                            Moving_down(true, true);
                            once = false;
                        }
                        location = Location_Modifier(location);
                        continue;
                    }
                }
                else if (location[0] > mapping.get_User())
                {
                    if (!mapping.Right_Move())
                    {
                        Console.WriteLine("FAIL RIGHT MOVE!");
                        mapping.Reset_Rotation();
                        location = (int[])heuristics.Relocate(location).Clone();
                        if (once && location[1] > 0)
                        {
                            Moving_down(true, true);
                            Moving_down(true, true);
                            once = false;
                        }
                        location = Location_Modifier(location);
                        continue;
                    }
                }
            }
            Moving_down(fastClear, true);
            heuristics.Reset_Locate_Counter();

        }

        private int[] Location_Modifier(int[] location)
        {

            for (int i = 0; i < location[1]; i++)
            {
                if (!mapping.Block_Rotation(Current_Block))
                {
                    Console.WriteLine("FAIL ROTATION!");
                    mapping.Reset_Rotation();
                    location = (int[])heuristics.Relocate(location).Clone();
                    if (heuristics.get_Locate_Counter() > width / Interval)
                    {
                        break;
                    }
                    i = 0;
                    continue;
                }
            }
            return location;
        }
        /*
        private void Initializer_Click(object sender, EventArgs e)
        {
            // 초기화가 안되어 있으면 실행
            
            Control_Moving.Stop();
            Program_Indicator.Stop();

            game_Start();
            Control_Moving.Start();
            Program_Indicator.Start();
        }
        */
        private void Gene_Generator_Click(object sender, EventArgs e)
        {
            if (Learning.get_Current_Generation() != 1)
            {
                if (MessageBox.Show("이미 만들어진 1세대 유전자 정보는 삭제됩니다. \n계속하시겠습니까?", "알림", MessageBoxButtons.YesNo) == DialogResult.No)
                    return;
            }

            if (Learning.Make_first_Learning_file())
            {
                heuristics.set_Weight(Learning.Load_Recent_Gene());
                Calculator.set_weight(Learning.Load_Recent_Gene());
                System_Log.Items.Add("1세대 유전자 100개 생성");
            }
        }

        public void game_Start()
        {
            try
            {
                Calculator = new CalculationNetwork(Learning.get_Formula(), Learning.Load_Recent_Gene());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            heuristics = new Heuristics(width / Interval, height / Interval, Calculator);
            mapping = new Mapping(heuristics, width / Interval, height / Interval);

            heuristics.set_Weight(Learning.get_Weight());
            Next_Block = Generator.Next(6);
            Current_Block = Generator.Next(6);
            BlockColor = mapping.init_Character(Current_Block, 5);
        }

        private void Evaluate_Gene_Click(object sender, EventArgs e)
        {
            if (!OnceEvaluation)
            {
                if (Learning.Next())
                {
                    System_Log.Items.Add("유전자 불러오기 성공");
                    Gene_Generator.Enabled = false;
                    Evaluate_Generation.Enabled = false;
                    Learning_heredity.Enabled = false;
                }
                else
                {
                    System_Log.Items.Add("유전자 불러오기 실패");
                    return;
                }

                Control_Moving.Stop();
                Program_Indicator.Stop();

                label_Generation.Text = Learning.get_Current_Generation() + " 세대";
                label_Serial.Text = Learning.get_Current_Serial() + "번 유전자";

                game_Start();

                UI_Drawer("가중치", heuristics.get_Weight(), label_Weight, false);


                Control_Moving.Start();
                Program_Indicator.Start();
                string message = "가중치 ";
                foreach (int n in Learning.get_Weight())
                    message = message + n + " ";
                message += "에 대한 평가 시작";
                System_Log.Items.Add(message);
                OnceEvaluation = true;
            }
            else
            {
                Control_Moving.Stop();
                Program_Indicator.Stop();
                game_Start();
                System_Log.Items.Add("평가를 취소했습니다. 이번 평가는 저장되지 않습니다.");
                Control_Moving.Stop();
                Program_Indicator.Stop();


                Evaluate_Generation.Enabled = true;
                Learning_heredity.Enabled = true;
                Gene_Generator.Enabled = true;

                OnceEvaluation = false;
            }
        }

        private void Heredity_Tick(object sender, EventArgs e)
        {
            /* 한 평가가 종료되고 그 평가의 점수를 기록 */
            if (scoreWriting == true)
            {
                if (!OnceEvaluation && !evaluation && !lastEvaluation)
                {
                    scoreWriting = false;
                    return;
                }
                if (Learning.Save_Score(score))
                {
                    try
                    {
                        System_Log.Items.Add(score.ToString() + "점 기록 완료");
                    }
                    catch (Exception error)
                    {
                        Console.WriteLine(error.Message);
                    }
                }
                OnceEvaluation = false;
                //전체 유전자 평가 종료시
                if (lastEvaluation)
                {
                    Evaluate_Gene.Enabled = true;
                    Learning_heredity.Enabled = true;
                    Gene_Generator.Enabled = true;
                    System_Log.Items.Add("한 세대 평가를 완료하였습니다.");
                    lastEvaluation = false;
                    clickListener = false;
                }
                //단일 유전자 평가 종료시
                if (!evaluation)
                {
                    Evaluate_Generation.Enabled = true;
                    Learning_heredity.Enabled = true;
                    Gene_Generator.Enabled = true;
                }
                if(heredityMode)
                    NextEvaluation = true;
                //heredityMode = true;
                scoreWriting = false;
            }


            if (evaluation && heredityMode && NextEvaluation)
            {
                NextEvaluation = false;
                if (Learning.Next())
                {
                    System_Log.Items.Add("유전자 불러오기 성공");
                    Control_Moving.Stop();
                    Program_Indicator.Stop();

                    label_Generation.Text = Learning.get_Current_Generation() + " 세대";
                    label_Serial.Text = Learning.get_Current_Serial() + "번 유전자";

                    /*타이머 종료 조건*/
                    if (Learning.get_Current_Serial() == 99)
                    {
                        evaluation = false;
                        lastEvaluation = true;
                    }
                    game_Start();

                    UI_Drawer("가중치", heuristics.get_Weight(), label_Weight, false);

                    Control_Moving.Start();
                    Program_Indicator.Start();
                    string message = "가중치 ";
                    foreach (int n in Learning.get_Weight())
                        message = message + n + " ";
                    message += "에 대한 평가 시작";
                    System_Log.Items.Add(message);
                }
                else // 이 부분은 전체 세대 평가 기능 작동시에만 구동됩니다.
                {
                    System_Log.Items.Add("유전자 불러오기 실패");
                    evaluation = false;
                    Evaluate_Gene.Enabled = true;
                    Learning_heredity.Enabled = true;
                    Gene_Generator.Enabled = true;
                    clickListener = false;
                }
            }
        }

        /*UI 부분 업로드*/
        private void UI_Drawer(string name, double[] value, Label target, bool cut)
        {
            int counter = 1;
            target.Text = name;
            foreach (double n in value)
            {
                target.Text += (" (" + counter + "c, " + n + ")");
                if (cut && counter == 7)
                    target.Text += "\n";
                counter++;
            }
        }

        /*UI 부분 업로드*/
        private void UI_Drawer_Int(string name, int[] value, Label target)
        {
            int counter = 1;
            target.Text = name;
            foreach (int n in value)
            {
                target.Text += (" (" + counter + ", " + n + ")");
                counter++;
            }
        }

        private void Evaluate_Generation_Click(object sender, EventArgs e)
        {
            if (clickListener == false)
            {
                clickListener = true;
                System_Log.Items.Add("세대 전체 평가 시작..");
                Gene_Generator.Enabled = false;
                Evaluate_Gene.Enabled = false;
                Learning_heredity.Enabled = false;
                evaluation = true;
                heredityMode = true;
                NextEvaluation = true;
            }
            else
            {
                clickListener = false;
                System_Log.Items.Add("평가 중단.. 이번 세대까지 평가합니다.");
                Gene_Generator.Enabled = true;
                Evaluate_Gene.Enabled = true;
                Learning_heredity.Enabled = true;
                evaluation = false;
            }

        }

        private void Learning_heredity_Click(object sender, EventArgs e)
        {
            string temp;

            if (Learning.get_Current_Serial() != 99)
                if (MessageBox.Show("이번 세대를 유전학습 하시겠습니까?", "알림", MessageBoxButtons.YesNo) == DialogResult.No)
                    return;

            temp = Learning.Learn(10);
            if (temp!=null)
                System_Log.Items.Add(temp);
            else
                System_Log.Items.Add("유전학습에 오류가 있습니다.");
            Genetic_UI_Drawer();
        }

        private void UI_Clear()
        {
            label_Adjacent.Text = null;
            label_AverageHeight.Text = null;
            label_Empty.Text = null;
            label_Final.Text = null;
            label_FutureHeight.Text = null;
            label_Height.Text = "빠른 진행 모드, 탐색값 숨기기 모드일 때는 표시되지 않습니다.";
            label_H_Difference.Text = null;
            label_Side.Text = null;
        }

        private void Fast_Down_Click(object sender, EventArgs e)
        {
            if (fastClear)
            {
                useUI = false;
                fastClear = false;
                UI_Clear();
                Fast_Down.Text = "빠른진행\n끄기";
            }
            else
            {
                useUI = true;
                fastClear = true;
                Fast_Down.Text = "빠른진행\n켜기";
            }
        }

        private void ToolStripMenuItem_set_Click(object sender, EventArgs e)
        {
            form_set W_Set = new form_set();
            W_Set.ShowDialog();

            Meta_info = W_Set.get_ResultSet();
            if (Meta_info == null)
            {
                MessageBox.Show("입력받은 정보가 없습니다.");
                return;
            }
            else
            {
                Learning.set_Meta(Meta_info);
                System_Log.Items.Add(Meta_info[0] + "세대 " + Meta_info[1] + "번 유전자를 설정했습니다.");
                Genetic_UI_Drawer();
            }
        }

        private void Genetic_UI_Drawer()
        {
            /*UI UP_LOAD*/
            label_Generation.Text = Learning.get_Current_Generation() + " 세대";
            label_Serial.Text = Learning.get_Current_Serial() + "번 유전자";
        }


        private void ToolStripMenuItem_help_Click(object sender, EventArgs e)
        {
            form_help W_Help = new form_help();
            W_Help.ShowDialog();

        }

        private void ToolStripMenuItem_calculateModel_Click(object sender, EventArgs e)
        {
            form_calcM C_Model = new form_calcM();
            C_Model.Owner = this;
            C_Model.formula = Learning.get_formula();
            C_Model.Learning = Learning;
            C_Model.Calc = Calculator;
            C_Model.ShowDialog();
        }

        private void ToolStrip_Pattern_Click(object sender, EventArgs e)
        {
            form_pattern P_Mode = new form_pattern();
            P_Mode.Owner = this;
            P_Mode.IO = Learning;
            P_Mode.ShowDialog();
        }

        private void Show_Search_Click(object sender, EventArgs e)
        {
            if (useUI&&fastClear)
            {
                useUI = false;
                UI_Clear();
                System_Log.Items.Add("탐색값이 표시되지 않습니다.");
                Show_Search.Text = "탐색값 보이기";
            }
            else if(!useUI&&fastClear)
            {
                useUI = true;
                System_Log.Items.Add("탐색값이 표시됩니다.");
                Show_Search.Text = "탐색값 숨기기";
            }
            else if (!fastClear)
            {
                System_Log.Items.Add("먼저 빠른 진행 모드를 종료해 주세요.");
            }
        }
    }
}
