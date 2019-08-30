using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;

namespace AI_Tetris
{
    public class CalculationNetwork
    {
        DataTable Calculate;
        char[] formula;
        double[] weight;
        
        int[] First;
        int[] Second;
        int[] Third;
        double[] Fourth;
        double[] fifth;
        double[] Sixth;

        /*
         * 가중치는 W로 표현되며, 값은 서수의 앞자리를 따서 표현됨.
         */

        public CalculationNetwork(char[] Formula, double[] Weight)
        {
            Calculate = new DataTable(); 
            formula = (char[])Formula.Clone();
            weight = Weight;
        }

        public CalculationNetwork(char[] Formula)
        {
            Calculate = new DataTable();
            formula = (char[])Formula.Clone();
            weight = null;
        }

        public void set_weight(double[] Weight)
        {
            weight = Weight;
        }
        public void set_formula(char[] Formula)
        {
            formula = (char[])Formula.Clone();
            weight = null;
        }

        public void set_formula(string Formula)
        {
            formula = (char[])Formula.ToCharArray().Clone();
            weight = null;
        }
        public double[] get_weight()
        {
            return weight;
        }

        public void Set_Value(int[] Height,int[] BlockEmpty, int[] Minimum_Under_Blocks, double[] AverageHeight,double[] heurisitcHeight,double[] Side_Block )
        {
            First= Height;
            Second=BlockEmpty;
            Third=Minimum_Under_Blocks;
            Fourth=AverageHeight;
            fifth=heurisitcHeight;
            Sixth = Side_Block;
        }

        public string prepare(int index)
        {
            string result=null;
            int W_counter = 0; //가중치(Weight) 카운터.

            foreach (char item in formula) {
                if(item=='+'|| item == '-' || item == '/' || item == '*' || item == '(' || item == ')')
                {
                    result += item.ToString();
                }
                else if(item == 'W')
                {
                    result += calculate(W_counter);//가중치 계산기.
                    W_counter++;
                }
                else{
                    result += calculate(item, index);
                }
            }

            return result;
        }

        public double execute(string target)
        {

            return double.Parse(Calculate.Compute(target, "").ToString());
        }

        private string calculate(int W_counter)
        {
            return weight[W_counter].ToString();
       }

        private string calculate(char value,int index)
        {
            if (value == 'F')
                return First[index].ToString();
            if (value == 'S')
                return Second[index].ToString();
            if (value == 'T')
                return Third[index].ToString();
            if (value == 'O')
                return Fourth[index].ToString();
            if (value == 'f')
                return fifth[index].ToString();
            if (value == 's')
                return Sixth[index].ToString();
            return null;
        }


    }
}
