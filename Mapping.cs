using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI_Tetris
{
    class Mapping
    {
        Heuristics heuristics;
        Formator form;

        char[,] Matrix;
        int[,] character = new int[4, 2];
        int[] user = new int[2];//0 is x, 1 is y.
        /*Figure Formater
        for(int j=3; j>-1; j--)
            for(int i=1; i>-1; i--)
            */
        int[,] Figure_Form;
        //0 is Rect, 1~5 are polygons.
        /*
        int[] polygon1_F = new int[8] { 0, 0, -1, 0, -2, 0, 0, 1 };
        int[] polygon2_F = new int[8] { 0, 0, 0, 1, 1, 1, 2, 1 };
        int[] polygon3_F = new int[8] { 0, 0, 0, 1, 0, 2, 0, 3 };
        int[] polygon4_F = new int[8] { 0, 0, -1, 0, 0, 1, 1, 1, };
        int[] polygon5_F = new int[8] { 0, 0, 1, 0, 0, 1, -1, 1 };
        */
        int Score;
        int Tower_Height;
        int Rotation;//회전 회수. Block_Rotation()에서 사용되고 Init_Character에서 초기화.
        int Figure;//현재 블록 모형. Move()에서 사용.

        int numWidth; //열
        int numHeight; //행


        static char GameOver = 'Q'; // 블럭 높이가 다 차 올랐을때 사용.
        static char empty = 'O';//빈공간
        static char[] Color_Form;
        /*
         * 0. 'A'; //ㅁ 모양
         * 1. 'R'; //ㄱ의 Y축 대칭 모양
         * 2. 'S'; //ㄴ 모양
         * 3. 'L'; //ㅣ 모양
         * 4. 'Z'; // Z 모양
         * 5. 'P'; // Z의 Y축 대칭 모양
         */
        static char CharacterColor = 'C'; //주인공일때 색.
        static int UnitOfScore = 10; //한 줄 맞출때마다 얻는 점수.


        bool flow = false;
        bool Drawed = false;
        bool Full_In_Line = false;
        bool Critical_Section = false;
        bool Line_swap = false;//use in busy waiting 
        /*
         * 캐릭터 좌표는 반드시 0,0이 가장 아래쪽에 오도록 구현할것.
         * 
         */


        public Mapping(Heuristics H_temp, int Width, int Height)
        {
            heuristics = H_temp;
            numWidth = Width;
            numHeight = Height;
            Tower_Height = numHeight;
            Score = 0;
            form = new Formator();
            Color_Form = form.get_Color_Form();
            Figure_Form = form.get_Figure_Form();
            Matrix = new char[numHeight, numWidth];
            for (int i = 0; i < numHeight; i++)
                for (int j = 0; j < numWidth; j++)
                    Matrix[i, j] = empty;
            Rotation = 0;//회전 값 초기화.
        }

        /* Interface1 */
        public bool get_Draw()
        {
            return Drawed;
        }

        /* Interface2 */
        public void set_Draw(bool arg)
        {
            Drawed = arg;
        }

        /* Interface3 */
        public char[,] get_Matrix()
        {
            return Matrix;
        }

        /* Interface4 */
        public int get_Score()
        {
            return Score;
        }
        
        /* Interface5 */
        public void set_User(int x)
        {
            user[0] = x;
        }

        /* Interface6 */
        public int get_User()
        {
            return user[0];
        }

        public void Reset_Rotation()
        {
            Rotation = 0;
        }


        public bool TimePass(char Color)
        {
            bool isPass = false;
            if (flow != true)
            {
                for (int i = 0; i < 4; i++)
                {
                    if (character[i, 0] < numHeight - 1 && Matrix[character[i, 0] + 1, character[i, 1]] != CharacterColor && Matrix[character[i, 0] + 1, character[i, 1]] == empty)
                    {
                        isPass = true;
                    }
                    else if (character[i, 0] < numHeight - 1 && Matrix[character[i, 0] + 1, character[i, 1]] == CharacterColor)
                    {
                        isPass = true;
                    }
                    else
                    {
                        isPass = false;
                        break;
                    }
                    
                }
                if (isPass == true)
                {
                    if (!Repaint(empty))
                        return false;//Map이 변경되지 않음.
                    user[1]++;
                    character = form.Block_Rotate(Figure, user[0], user[1], Rotation);
                    Repaint(CharacterColor);
                    Drawed = true;//Map 이 변경되었음.
                }
                else
                {
                    heuristics.set_Matrix(Matrix);
                    Create(Color);
                    Height_Detector();
                    Block_Remover();
                    flow = true;
                }
            }
            return flow;
        }

        public bool Full_Down(char Color)
        {
            bool isPass = false;
            while (flow != true)
            {
                for (int i = 0; i < 4; i++)
                {
                    if (character[i, 0] < numHeight - 1 && Matrix[character[i, 0] + 1, character[i, 1]] != CharacterColor && Matrix[character[i, 0] + 1, character[i, 1]] == empty)
                    {
                        isPass = true;
                    }
                    else if (character[i, 0] < numHeight - 1 && Matrix[character[i, 0] + 1, character[i, 1]] == CharacterColor)
                    {
                        isPass = true;
                    }
                    else
                    {
                        isPass = false;
                        break;
                    }

                }
                if (isPass == true)
                {
                    if (!Repaint(empty))
                        return false;//Map이 변경되지 않음.
                    user[1]++;
                    character = form.Block_Rotate(Figure, user[0], user[1], Rotation);
                    Repaint(CharacterColor);
                    Drawed = true;//Map 이 변경되었음.
                }
                else
                {
                    heuristics.set_Matrix(Matrix);
                    //heuristics.set_TimerController(true);
                    Create(Color);
                    Height_Detector();
                    Block_Remover();
                    flow = true;
                }
            }
            return flow;
        }

        /*블럭 높이 계산 함수*/
        private void Height_Detector()
        {
            int[,] Detector = new int[4, 2];
            int unused = -1;

            for (int i = 0; i < 4; i++)
            {
                Detector[i, 1] = -1;
            }

            for (int i=0; i<4; i++)
            {
                for (int j = 0; j <4; j++)
                {
                    if (Detector[j, 1] == character[i, 1] && Detector[j, 0] > character[i, 0])
                    {
                        Detector[j, 0] = character[i, 0];
                        break;
                    }
                    else if (Detector[j, 1] == character[i, 1])
                        break;
                    else if (Detector[j, 1] == unused)
                    {
                        Detector[j, 1] = character[i, 1];
                        Detector[j, 0] = character[i, 0];
                        break;
                    }
                }
            }
            for (int i = 0; i < 4; i++)
            {
                if (Detector[i, 1] == unused)
                    break;
                heuristics.set_Height(Detector[i, 1], numHeight - Detector[i, 0]);

                if (character[i, 0] < Tower_Height)
                    Tower_Height = character[i, 0];
            }


        }

        public bool Right_Move()
        {
            bool isPass = false;
            Drawed = false;

            for (int i = 0; i < 4; i++)
            {
                if (character[i, 1] < numWidth - 1 && Matrix[character[i, 0], character[i, 1] + 1] != CharacterColor && Matrix[character[i, 0], character[i, 1] + 1] == empty)
                {
                    isPass = true;
                }
                else if (character[i, 1] <numWidth - 1 && Matrix[character[i, 0], character[i, 1] + 1] == CharacterColor)
                {
                    isPass = true;
                }
                else
                {
                    isPass = false;
                    break;
                }

            }
            if (isPass == true)
            {
                if (!Repaint(empty))
                    return false;//Map이 변경되지 않음.
                user[0]++;
                character = form.Block_Rotate(Figure, user[0], user[1], Rotation);
                Repaint(CharacterColor);
                Drawed = true;//Map이 변경되었음.
            }
            return Drawed;
        }

        public bool Left_Move()
        {
            bool isPass = false;
            Drawed = false;
            for (int i = 0; i < 4; i++)
            {
                if (character[i, 1] > 0 && Matrix[character[i, 0], character[i, 1] - 1] != CharacterColor && Matrix[character[i, 0], character[i, 1] - 1] == empty)
                {
                    isPass = true;
                }
                else if (character[i, 1] > 0 && Matrix[character[i, 0], character[i, 1] - 1] == CharacterColor)
                {
                    isPass = true;
                }
                else
                {
                    isPass = false;
                    break;
                }

            }
            if (isPass == true)
            {
                if (!Repaint(empty))
                    return false; //Map이 변경되지 않음.
                user[0]--;
                character = form.Block_Rotate(Figure, user[0], user[1],Rotation);
                Repaint(CharacterColor);
                Drawed = true;//Map 이 변경되었음.
            }
            return Drawed;
        }

        public void ClearMap()
        {
            for (int i = 0; i < numWidth; i++)
                for (int j = 0; j < numHeight; j++)
                    Matrix[j, i] = empty;
            
        }

        public char init_Character(int Order, int x)
        {
            int y = 0;
            user[0] = x;
            user[1] = y;
            Figure = Order;
            flow = false;
            Rotation = 0;

            if (Tower_Height <= 0)
                return GameOver;

            character=form.Create_Block_Form(Order, x, 0);
            Create(CharacterColor);
            heuristics.set_TimerController(true);
            return Color_Form[Order];

        }

        private bool Repaint(char Color)
        {
            if (Color != 'O')
                Critical_Section = false;
            while (Critical_Section != false) ;//busy waiting
            if(Color=='O')
                Critical_Section = true;//임계영역 진입
            for (int i = 0; i < 4; i++)
            {
                if (character[i, 1] >= numWidth || character[i, 1] < 0 || character[i, 0] >= numHeight || character[i, 0] < 0)
                    return false;
            }

            for (int i = 0; i < 4; i++)
            {
                Matrix[character[i, 0], character[i, 1]] = Color;
            }
            Drawed = true;
            return true;
        }

        private void Create(char Color)
        {
            for (int i = 0; i < 4; i++)
            {
                if (character[i, 0] > numHeight-1)
                    character[i, 0]--;
                Matrix[character[i, 0], character[i, 1]] = Color;

            }
            Drawed = true;
        }

        public bool Block_Rotation(int Order)
        {
            int[,] detector = new int[4, 2];
            int temp_Rotation=Rotation;

            temp_Rotation++;
            if (Order == 3 || Order == 4 || Order == 5)
                if (temp_Rotation > 1)
                    temp_Rotation = 0;
            if (temp_Rotation > 3)
                temp_Rotation = 0;

            detector = form.Block_Rotate(Order, user[0], user[1], temp_Rotation);

            if (Order != 0)
            {
                for (int j = 3; j > -1; j--)
                {
                    if (detector[j, 1] < 0 || detector[j, 1] >= numWidth || detector[j, 0] >= numHeight || detector[j, 0] < 0)
                    {
                        return false;
                    }
                    if (Matrix[detector[j, 0], detector[j, 1]] != empty && Matrix[detector[j, 0], detector[j, 1]] != CharacterColor)
                    {
                        return false;
                    }
                }
                if (!Repaint(empty))
                {
                    return false;
                }
                character = detector;
                Repaint(CharacterColor);
                Rotation = temp_Rotation;
            }
            return true;
        }


        private void Block_Remover()
        {
            int[] target_line = new int[4];

            for (int i = 0; i < 4; i++)
                target_line[i] = character[i, 0];

            Array.Sort(target_line);

            for (int i = 1; i < 4; i++)
                if (target_line[i - 1] == target_line[i])
                    target_line[i] = -1; // -1 is a trash value.

            for (int j = 0; j < 4; j++)
            {
                Full_In_Line = true;
                if (target_line[j] == -1)
                    continue;
                for (int i = 0; i < numWidth; i++)
                    if (Matrix[target_line[j], i] == empty)
                    {
                        Full_In_Line = false;
                        break;
                    }
                if (Full_In_Line == true)
                {
                    Score += UnitOfScore;
                    Tower_Height++;
                    Full_In_Line = true;
                    Line_Swap(target_line[j]);
                }
                //counter++;
            }
        }

        private void Line_Swap(int Level)
        {
            while (Line_swap) ; //busy waiting.
            /*임계영역 진입*/
            Line_swap = true;
            
            heuristics.set_Height(0, -1);//블록 제거 연산을 높이 정보에 반영.

            for (int j = Level; j > 0; j--)
                for (int i = 0; i < numWidth; i++)
                    Matrix[j, i] = Matrix[j - 1, i];

            Line_swap = false;//임계영역 탈출
        }
        
    }
}
