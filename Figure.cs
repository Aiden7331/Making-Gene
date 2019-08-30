using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace AI_Tetris
{
    class Figure
    {
        Graphics graphics;
        SolidBrush PEN;
        Bitmap[] bitmap=new Bitmap[7];
        //static int width=1;

        /*this Class has functon of the painting on the panel. */
        static int Interval = 20;

        int numWidth;
        int numHeight;


        public Figure(Panel map, int Width, int Height)
        {
            numWidth = Width;
            numHeight = Height;
            graphics = map.CreateGraphics();
            bitmap[0] = new Bitmap("rsc/block0.png");
            bitmap[1] = new Bitmap("rsc/block1.png");
            bitmap[2] = new Bitmap("rsc/block2.png");
            bitmap[3] = new Bitmap("rsc/block3.png");
            bitmap[4] = new Bitmap("rsc/block4.png");
            bitmap[5] = new Bitmap("rsc/block5.png");
            bitmap[6] = new Bitmap("rsc/block6.png");
            PEN = new SolidBrush(Color.White);
        }
       
        public void DrawGame(char[,] Matrix)
        {
            for (int i = 0; i < numHeight; i++)
            {
                for (int j = 0; j < numWidth; j++)
                {
                    if (Matrix[i, j] == 'C')
                    {
                        //  PEN = new Pen(Color.Brown,width);
                        // PEN = new SolidBrush(Color.Brown);
                        graphics.DrawImage(bitmap[0], j * Interval, i * Interval, 19, 19);
                    }
                    else if (Matrix[i, j] == 'R')
                    {
                        //PEN = new SolidBrush(Color.Red);
                        graphics.DrawImage(bitmap[1], j * Interval, i * Interval, 19, 19);
                    }
                    else if (Matrix[i, j] == 'A')
                    {
                        //PEN = new SolidBrush(Color.Blue);
                        graphics.DrawImage(bitmap[2], j * Interval, i * Interval, 19, 19);
                    }
                    else if (Matrix[i, j] == 'L')
                    {
                        //PEN = new SolidBrush(Color.Purple);
                        graphics.DrawImage(bitmap[3], j * Interval, i * Interval, 19, 19);
                    }
                    else if (Matrix[i, j] == 'S')
                    {
                        //PEN = new SolidBrush(Color.GreenYellow);
                        graphics.DrawImage(bitmap[4], j * Interval, i * Interval, 19, 19);
                    }
                    else if (Matrix[i, j] == 'Z')
                    {
                        //PEN = new SolidBrush(Color.DarkCyan);
                        graphics.DrawImage(bitmap[5], j * Interval, i * Interval, 19, 19);
                    }
                    else if (Matrix[i, j] == 'P')
                    {
                        //PEN = new SolidBrush(Color.Salmon);
                        graphics.DrawImage(bitmap[6], j * Interval, i * Interval, 19, 19);
                    }
                    else if (Matrix[i, j] == 'O')
                    {
                        BasicBlock(j * Interval, i * Interval, PEN);
                    }
                }
            }
        }


        public void BasicBlock(int x, int y, SolidBrush Pen)
        {
            
            graphics.FillRectangle(Pen, new Rectangle(x , y , 19,  19));
        }

        public void BasicBlock(int x, int y, int Number)
        {

            /*pen을 사용한 선 기반 구현 코드*/
         }
     }
 }
