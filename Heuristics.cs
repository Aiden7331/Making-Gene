using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI_Tetris
{
    class Heuristics
    {
        Formator form;
        CalculationNetwork Calculator;

        static int LINE_SWAP = -1;
        static int Disenable = 1000;
        static char Empty = 'O';
        char[] Color_Form;
        int[,] Figure_Form;


        char[,] Matrix = null;
        int[] Height_info;// 현재 높이
        int[] BlockEmpty;// 빈 공간
        int[,] Under_Blocks;//밀착공간
        int[] Minimum_Under_Blocks;//최소 밀착공간
        int[] Minimum_Rotation; //최소 회전 수

        double[] Weight;//가중치
        double[] Average_Height;
        double[] Heuristic_Height;
        double[] Heuristic_Value;
        double[] Side_Block;

        int locate_counter = 0;

        int Width;
        int Height;

        bool TimerController = false;

        public Heuristics(int numWidth, int numHeight,CalculationNetwork calculator)
        {
            form = new Formator();
            Color_Form = form.get_Color_Form();
            Figure_Form = form.get_Figure_Form();
            Height_info = new int[numWidth];
            BlockEmpty = new int[numWidth];
            Under_Blocks = new int[4, numWidth];
            Heuristic_Value = new double[numWidth];
            Side_Block = new double[numWidth];

            Weight = calculator.get_weight();
            Calculator = calculator;
            Width = numWidth;
            Height = numHeight;
        }

       

        /*Interface 1*/
        public void set_Height(int theNumWidth, int Height_Value)//if Height_Value가 -1이면 Line Swap()으로 인지.
        {
            /*Line Swap시 일어나는 위치 정보 계산*/
            if (Height_Value == LINE_SWAP)
            {
                for (int i = 0; i < Width; i++)
                    Height_info[i]--;
                return;
            }
            //if(Height_info[theNumWidth]!=Height_Value+1)
                Height_info[theNumWidth] = Height_Value;
            Discovery_Empty(theNumWidth);
        }

        /*Interface 2*/
        public int[] get_Height()
        {
            return Height_info;
        }

        /*Interface 3*/
        public int[,] get_Under_Blocks()
        {
            return Under_Blocks;
        }

        /*Interface 4*/
        public void set_Matrix(char[,] T_Matrix)
        {
            Matrix = (char[,])T_Matrix.Clone();
        }

        /*Interface 5*/
        public bool get_TimerController()
        {
            return TimerController;
        }

        /*Interface 6*/
        public void set_TimerController(bool TEMP)
        {
            TimerController = TEMP;
        }


        /*Interface 7*/
        public int[] get_Empty_Space()
        {
            return BlockEmpty;
        }


        /*Interface 8*/
        public double[] get_Heuristics_Height()
        {
            return Heuristic_Height;
        }

        /*Interface 9*/
        public double[] get_Average_Height()
        {
            return Average_Height;
        }

        /*Interface 10*/
        public double[] get_Heuristic_Value()
        {
            return Heuristic_Value;
        }

        /*Interface 11*/
        public double[] get_Side_Block()
        {
            return Side_Block;
        }

        /*Interface 12*/
        public int get_Locate_Counter()
        {
            return locate_counter;
        }

        /*Interface 13*/
        public void Reset_Locate_Counter()
        {
            locate_counter = 0;
        }

        /*Interface 14*/
        public void set_Weight(double[] temp)
        {
            Weight = (double[])temp.Clone();
        }

        /*Interface 15*/
        public double[] get_Weight()
        {
            return Weight;
        }

        /* 배치했을 경우의 평균높이를 계산. */
        public void Evaluate_Height(int Order)
        {
            Heuristic_Height = new double[Width];
            Average_Height = new double[Width];
            int[,] Detector;
            char[,] temp;
            int x = 0, y;
            double height_Average1 = 0;
            double height_Average2 = 0;
            //bool check_detection = false;

            temp = (char[,])Matrix.Clone();// 깊은 복사 수행
            

            while (x < Width)
            {
                y = Height - Height_info[x] - 1;//실제 높이.

                while (!Check_Height(x, y, Order, temp, Minimum_Rotation[x]) && y > 0)
                    y--;
                if (y <= 0)
                {
                        Heuristic_Height[x] = Disenable;
                        x++;
                        continue;
                }
                Detector = form.Block_Rotate(Order, x, y, Minimum_Rotation[x]);
                
                for (int i = 0; i < 4; i++)
                {
                    height_Average1 += Detector[i, 0];
                    height_Average2 += Height_info[Detector[i, 1]];
                }

                height_Average1 /= 4;
                height_Average2 /= 4;
                for (int j = 0; j < 4; j++)
                    temp[Detector[j, 0], Detector[j, 1]] = Empty;

                Heuristic_Height[x] = Height - height_Average1;
                Average_Height[x] = height_Average2;
                height_Average1 = 0;
                height_Average2 = 0;
                x++;
            }


        }


        private void Minimum_Under()
        {
            int lowest, numOfRot;
            Minimum_Under_Blocks = new int[Width];
            Minimum_Rotation = new int[Width];
            int x = 0;

            while (x < Width)
            {
                lowest = Under_Blocks[0, x];
                numOfRot = 0;
                for (int i = 1; i < Under_Blocks.Length/Width; i++)
                    if (lowest > Under_Blocks[i, x])
                    {
                        lowest = Under_Blocks[i, x];
                        numOfRot = i;
                    }
                Minimum_Under_Blocks[x] = lowest;
                Minimum_Rotation[x] = numOfRot;
                x++;
            }
        }

        public int[] Heuristic_Function(int Order)
        {
            double temp=0;
            //double weight1 = 8, weight2 = 5, weight3 = 20, weight4 = 8, weight5 = 5, weight6 = 3;
            
            string formula=null;

            int[] location = new int[2]; // 1 is location. 2 is rotation.

            Minimum_Under();
            Evaluate_Height(Order);
            Evaluate_Side_Block_Relevance(Order);
            //   Height_info + BlockEmpty + Minimum_Under_Blocks + Heuristic_Height - Side_Block;

            Calculator.Set_Value(Height_info, BlockEmpty, Minimum_Under_Blocks, Average_Height, Heuristic_Height, Side_Block);
            for (int i = 0; i < Width; i++) {
                formula=Calculator.prepare(i);
                Heuristic_Value[i]=Calculator.execute(formula);
            }


            temp = Heuristic_Value[0];
            location[0] = 0;
            location[1] = Minimum_Rotation[0];
            
            for (int i = 1; i < Width; i++)
            {
                if (temp > Heuristic_Value[i])
                {
                    temp = Heuristic_Value[i];
                    location[0] = i;
                    location[1] = Minimum_Rotation[i];
                }
            }
            
            return location;
        }

        public int[] Relocate(int[] target)
        {
            double temp;
            int[] result=new int[2];
            Heuristic_Value[target[0]] = Disenable;
            temp = Heuristic_Value[0];
            result[0] = 0;
            result[1] = Minimum_Rotation[0];
            for(int i=1; i<Width; i++) {
                if (temp > Heuristic_Value[i])
                {
                    temp = Heuristic_Value[i];
                    result[0] = i;
                    result[1] = Minimum_Rotation[i];
                }
            }
            locate_counter++;
            return result;
        }

        private void Discovery_Empty(int index)
        {
            int counter = 0;

            for (int i = 0; i < Height_info[index]; i++)
                if (Matrix[Height - i - 1, index] == Empty)
                    counter++;

            BlockEmpty[index] = counter;
        }


        /* 반드시 Evaluate_Block_Relevance()함수 이후에 쓸것 */
        private void Evaluate_Side_Block_Relevance(int Order)
        {
            int[,] Detector;
            char[,] temp;
            int x = 0, y=0, S_counter;

            while (x < Width)
            {
                S_counter = 0;
                y = Height - Height_info[x] - 1;//실제 높이.

                temp = (char[,])Matrix.Clone();// 깊은 복사 수행

                while (!Check_Height(x, y, Order, temp, Minimum_Rotation[x]) && y > 0)
                    y--;
                if (y <= 0)
                {
                    Side_Block[x] = -Disenable;
                    x++;
                    continue;
                }
                Detector = form.Block_Rotate(Order, x, y, Minimum_Rotation[x]);

                /*양 옆 체크*/
                for (int i = 0; i < 4; i++)
                {
                    if (Detector[i, 1] + 1 != Width)
                    {
                        if (temp[Detector[i, 0], Detector[i, 1] + 1] != 'C' && temp[Detector[i, 0], Detector[i, 1] + 1] != Empty)
                            S_counter++;
                    }
                    else
                    {
                        S_counter++;
                    }

                    if(Detector[i, 1] - 1 != -1)
                    {
                        if (temp[Detector[i, 0], Detector[i, 1] - 1] != 'C' && temp[Detector[i, 0], Detector[i, 1] - 1] != Empty)
                            S_counter++;
                    }
                    else
                    {
                        S_counter++;
                    }
                }

                for (int j = 0; j < 4; j++)
                    temp[Detector[j, 0], Detector[j, 1]] = Empty;

                Side_Block[x] = S_counter;
                x++;
            }

        }

        /*빈 공간을 체크. 빈 공간이 많을 수록 값이 높아짐 */
        public void Evaluate_Block_Relevance(int Order, char[,]Map_info)
        {
            int[,] Detector;
            char[,] temp;
            int x = 0, y, Rotate = 0, Repeater;
            //bool check_detection = false;

            temp = (char[,])Map_info.Clone();// 깊은 복사 수행


            if (Order == 3 || Order == 4 || Order == 5)
                Repeater = 2;
            else if (Order == 0)
                Repeater = 1;
            else
                Repeater = 4;
            Under_Blocks = null;
            Under_Blocks = new int[Repeater, Width];

            while (Rotate != Repeater)
            {
                while (x < Width)
                {
                    int counter = 0;
                    y = Height - Height_info[x];//실제 높이.

                    while (!Check_Height(x, y, Order, temp, Rotate) && y > 0)
                        y--;
                    if (y <= 0)
                    {
                        Under_Blocks[Rotate, x] = Disenable;
                        x++;
                        continue;
                    }
                    Detector = form.Block_Rotate(Order, x, y, Rotate);

                    

                        /*밑 바닥 체크*/
                    for (int i = 0; i < 4; i++)
                    {
                        if (Detector[i, 0] + 1 != Height)
                        {
                            if (temp[Detector[i, 0] + 1, Detector[i, 1]] != 'C' && temp[Detector[i, 0] + 1, Detector[i, 1]] == Empty)
                            {
                                counter++;
                                if (Detector[i, 1] < 0 || Detector[i, 1] >= Width || Detector[i, 0] + 2 < 0 || Detector[i, 0] + 2 >= Height)
                                    ;
                                else if (temp[Detector[i, 0] + 2, Detector[i, 1]] != 'C' && temp[Detector[i, 0] + 2, Detector[i, 1]] == Empty)
                                    counter++;
                            }
                        }
                    }

                    for (int j = 0; j < 4; j++)
                        temp[Detector[j, 0], Detector[j, 1]] = Empty;
                    
                    Under_Blocks[Rotate, x] = counter;
                    x++;
                }
                Rotate++;
                x = 0;
            }
        }

        private bool Check_Height(int x, int y, int Order, char[,] Map_info, int Rotate)
        {
            int[,] Detector = form.Block_Rotate(Order, x, y,Rotate);

            for (int j = 0; j < 4; j++)
                if (Detector[j, 1]<0 || Detector[j, 1]>=Width || Detector[j, 0] < 0 || Detector[j, 0] >= Height )
                    return false;

            for (int j = 0; j < 4; j++)
                if (Map_info[Detector[j, 0], Detector[j, 1]] != Empty)
                    return false;


            for (int j = 0; j < 4; j++)
                Map_info[Detector[j, 0], Detector[j, 1]] = 'C';

            return true;
        }

        private bool Detection_Field(int[,] target,int Array_Index, int W_range, int H_range)
        {
            if (target[Array_Index, 1] + W_range < 0 || target[Array_Index, 1] + W_range >= Width || target[Array_Index, 0] >= Height + H_range || target[Array_Index, 0] < 0)
                return false;

            return true;
        }
    }
}
