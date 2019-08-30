using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI_Tetris
{
    class Formator
    {
        static char[] Color_Form = new char[] { 'A', 'R', 'S', 'L', 'Z', 'P', 'C' };
        /*
         * 0. 'A'; //ㅁ 모양
         * 1. 'R'; //ㄱ의 Y축 대칭 모양
         * 2. 'S'; //ㄴ 모양
         * 3. 'L'; //ㅣ 모양
         * 4. 'Z'; // Z 모양
         * 5. 'P'; // Z의 Y축 대칭 모양
         */

        int[,] Figure_Form = new int[6, 8] { { 0, 0, 1, 0, 0, 1, 1, 1 }, { 0, 0, 0, 1, -1, 0, -2, 0 }, { 0, 0, 0, 1, 1, 0, 2, 0 }, { 0, 0, 0, 1, 0, 2, 0, 3 }, { 0, 0, -1, 0, 0, 1, 1, 1, }, { 0, 0, 1, 0, 0, 1, -1, 1 } };
        //0 is Rect, 1~5 are polygons.
        /*
        int[] polygon1_F = new int[8] { 0, 0, 0, 1, -1, 0, -2, 0 };
        int[] polygon2_F = new int[8] { 0, 0, 0, 1, 1, 0, 2, 0 };
        int[] polygon3_F = new int[8] { 0, 0, 0, 1, 0, 2, 0, 3 };
        int[] polygon4_F = new int[8] { 0, 0, -1, 0, 0, 1, 1, 1, };
        int[] polygon5_F = new int[8] { 0, 0, 1, 0, 0, 1, -1, 1 };
        */
        
        /*Interface 1*/
        public char[] get_Color_Form()
        {
            return Color_Form;
        }

        /*Interface 2*/
        public int[,] get_Figure_Form()
        {
            return Figure_Form;
        }

        public int[,] Create_Block_Form(int Order, int x, int y)
        {
            int[,] character = new int[4, 2];

            int temp;
            int counter = 0;
            
            for (int i = 1; i > -1; i--)
            {
                if (i == 1) temp = x;
                else temp = y;
                for (int j = 3; j > -1; j--)
                {
                    character[j, i] = temp + Figure_Form[Order, counter];
                    counter += 2;
                }
                counter = 1;
            }
            return (int[,])character.Clone();
        }

        public int[,] Block_Rotate(int Order, int x, int y, int Rotation)
        {
            int counter=0;
            int[,] character = new int[4, 2];

            if (Order==0 || Rotation==0)
            {
                character=Create_Block_Form(Order, x, y);
                
            }
            else if(Order!=0 && Rotation== 1)
            {
                counter = 1;
                for (int j = 3; j > -1; j--)
                {
                    character[j, 1] = x + Figure_Form[Order, counter];
                    counter += 2;
                }
                counter = 0;
                for (int j = 3; j > -1; j--)
                {
                    character[j, 0] = y - Figure_Form[Order, counter];
                    counter += 2;
                }
            }
            else if (Order != 0 && Rotation == 2)
            {
                counter = 0;
                for (int j = 3; j > -1; j--)
                {
                    character[j, 1] = x - Figure_Form[Order, counter];
                    counter += 2;
                }
                counter = 1;
                for (int j = 3; j > -1; j--)
                {
                    character[j, 0] = y - Figure_Form[Order, counter];
                    counter += 2;
                }

            }
            else if (Order != 0 && Rotation == 3)
            {
                counter = 1;
                for (int j = 3; j > -1; j--)
                {
                    character[j, 1] = x - Figure_Form[Order, counter];
                    counter += 2;
                }
                counter = 0;
                for (int j = 3; j > -1; j--)
                {
                    character[j, 0] = y + Figure_Form[Order, counter];
                    counter += 2;
                }

            }
            return (int[,])character.Clone();
        }
    }
}
