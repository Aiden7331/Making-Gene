using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace AI_Tetris
{
    class Analyzer
    {
        int tno_weight;
        int Generation;
        
        double[,] Weight;
        string[] Sequence;
        string PatternSet=null;
        int[] NOPattern;// repeative number of pattern.

        bool compileStatistics = false;

        public Analyzer()
        {
        }

        public Analyzer(string metafile, string genefile)
        {
            FileStream fileHandler = new FileStream(metafile, FileMode.Open);
            StreamReader Reader = new StreamReader(fileHandler);
            string[] buffer = Reader.ReadLine().Split(' ');
            Generation = int.Parse(buffer[0]);
            tno_weight = int.Parse(buffer[3]);
            buffer = null;
            Reader.Close();
            fileHandler.Close();
            Prepare(genefile);
        }

        public Analyzer(string genefile)
        {
            if (!genefile.Contains(".txt"))
                genefile += ".txt";
            try
            {
                FileStream fileHandler = new FileStream(genefile, FileMode.Open);
                StreamReader Reader = new StreamReader(fileHandler);
                string[] buffer = Reader.ReadLine().Split(' ');

                Generation = int.Parse(buffer[0]);
                tno_weight = buffer.Length - 4;
                buffer = null;
                Reader.Close();
                fileHandler.Close();
            }catch(Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }

        public double[,] Prepare(string filename)
        {
            Weight = new double[100, 8];
            double [,] Suitability = new double[100, 2]; // 0번에 index, 1번에 데이터 저장. 
            if (!filename.Contains(".txt"))
                filename += ".txt";
            FileStream fileHandler = new FileStream(filename, FileMode.Open);
            StreamReader Reader = new StreamReader(fileHandler);
            string[] buffer;

            /* Mapping data and save program's variables*/
            for (int i = 0; i < 100; i++)
            {
                buffer = Reader.ReadLine().Split(' ');
                for (int j = 0; j < tno_weight; j++)
                    Weight[i, j] = double.Parse(buffer[2 + j]);
                Suitability[i, 0] = i;
                Suitability[i, 1] = int.Parse(buffer[2 + tno_weight]);
            }

            Reader.Close();
            fileHandler.Close();
            return Suitability;
        }

        private string SerializeWeight(int TNO_WEIGHT, double[] WEIGHT)
        {
            string DataSet=null;
            double[,] Raw = new double[TNO_WEIGHT, 2];

            for (int i = 0; i < TNO_WEIGHT; i++) {
                Raw[i, 0] = i+1;
                Raw[i, 1] = WEIGHT[i];
            }
            
            Raw = BubbleSort(Raw, TNO_WEIGHT);
            for (int j = 0; j < TNO_WEIGHT - 1; j++)
            {
                DataSet += ("Weight " + Raw[j, 0].ToString() + "> ");
            }
            DataSet += ("Weight " + Raw[TNO_WEIGHT - 1, 0].ToString());
            
            return DataSet;
        }

        public string[] get_Sequence(double[,] Prepared)
        {
            string[] DataSet = new string[100];
            double[,] Data = BubbleSort(Prepared, 100);
            double[,] Raw = new double[tno_weight, 2];

            for (int i = 0; i < 100; i++)
            {
                for (int j = 0; j < tno_weight; j++)
                {
                    Raw[j, 0] = j + 1;
                    Raw[j, 1] = Weight[i, j];
                }
                Raw = BubbleSort(Raw, tno_weight);
                for (int j = 0; j < tno_weight - 1; j++)
                {
                    DataSet[i] += ("Weight " + Raw[j, 0].ToString() + "> ");
                }
                DataSet[i] += ("Weight " + Raw[tno_weight - 1, 0].ToString());

            }
            
            return DataSet;
        }

        public string[] PatternReverse(string target) //패턴을 문자열 배열로 만들어서 반환
        {
            string[] result = target.Split(',');

            return result;
        }

        public string[] compile_statistics(string[] Sequence, int index, int size) // index is started from 0.
        {
            string[] Pattern = new string[size];
            string[] result;
            int[] PatternRepeat = new int[size];
            int counter = 0;
            int repeat_counter = index;

            bool breakDownFlow = false;

            if (Sequence == null)
                return null;

            for (int i = 0; i < size; i++)
            {
                while (Pattern[i] == null)
                {
                    if (repeat_counter >= index + size)
                    {
                        breakDownFlow = true;
                        break;
                    }
                    if (Sequence[repeat_counter] != null)
                    {
                        Pattern[i] = Sequence[repeat_counter];
                        repeat_counter++;
                    }
                    else
                        repeat_counter++;
                }

                if (breakDownFlow)
                    break;

                for (int j = repeat_counter; j < index + size; j++)
                {
                    if (Sequence[j] != null && Pattern[i].Equals(Sequence[j]))
                    {
                        PatternRepeat[i]++;
                        Sequence[j] = null;
                    }
                }
            }

            for (int i = 0; i < size; i++)
            {
                if (Pattern[i] != null)
                {
                    ++PatternRepeat[counter++];
                }
            }



            /*Pattern number withdrawl*/
            NOPattern = PatternRepeat;
            compileStatistics = true;//이 함수를 쓸때마다 활성화.

            result = new string[counter];
            for (int i = 0; i < counter; i++)
                result[i] = Pattern[i];

            return result;
        }

        public string Patternize(string[] target, int start, int end)
        {
            string[] PatternString;
            string Pattern = null;
            if (target == null)
                return null;
            PatternString = compile_statistics(target, start, end - start + 1);

            for (int i = 0; i < PatternString.Length - 1; i++)
                Pattern += (PatternString[i] + ",");
            Pattern += PatternString[PatternString.Length - 1];
            return Pattern;
        }

        public string AddPattern(string patternString, string pattern)
        {
            string[] target;
            target = patternString.Split(',');
            for (int i = 0; i < target.Length; i++)
            {
                if (! pattern.Contains(target[i]))
                {
                    pattern += ("," + target[i]);
                }
            }
            return pattern;
        }
        public void set_PatternSet(string target)
        {
            PatternSet = target;
        }
        public bool CompareToPattern(double[] weight,int size,int generation, string badRegulation)
        {
            string pattern=SerializeWeight(size, weight);
            /*3세대 이하는 패턴 미적용.*/
            if (generation <= 3)
                return true;
            if (PatternSet == null)
                return true;

            /*사용자 지정 패턴 거르기.*/
            if (badRegulation.Contains(pattern))
                return false;
            if(PatternSet.Contains(pattern))
                return true;

            return false;
        }
        /*
        public string[] compile_statistics(string[] DataSet, int index, int size) // it will be started from the data that user designated
        {
            string[] Pattern = new string[size];
            string[] result;
            int[] PatternRepeat = new int[size];
            int counter = 0;
            int repeat_counter = index;

            bool breakDownFlow = false;

            if (DataSet == null)
                return null;

            for (int i = 0; i < size; i++)
            {
                while (Pattern[i] == null)
                {
                    if (repeat_counter >= index + size)
                    {
                        breakDownFlow = true;
                        break;
                    }
                    if (DataSet[repeat_counter] != null)
                    {
                        Pattern[i] = DataSet[repeat_counter];
                        repeat_counter++;
                    }
                    else
                        repeat_counter++;
                }

                if (breakDownFlow)
                    break;

                for (int j = repeat_counter; j < index + size; j++)
                {
                    if (DataSet[j] != null && Pattern[i].Equals(DataSet[j]))
                    {
                        PatternRepeat[i]++;
                        DataSet[j] = null;
                    }
                }
            }

            for (int i = 0; i < size; i++)
            {
                if (Pattern[i] != null)
                {
                    ++PatternRepeat[counter++];
                }
            }

            Pattern number withdrawl
            NOPattern = PatternRepeat;
            compileStatistics = true;//이 함수를 쓸때마다 활성화.

            result = new string[counter];
            for (int i = 0; i < counter; i++)
                result[i] = Pattern[i];

            return result;
        }
        */

        public int[] get_PatternInfo() // this function will provide values only one time.
        {
            if (compileStatistics == false)
                return null;
            compileStatistics = false;
            return NOPattern;
        }

        public int[,] BestAnalyzer(int[,] Suitability)
        {
            int[,] result;

            result = BubbleSort(Suitability, 100);

            return (int[,])result.Clone();
        }

        private double[,] BubbleSort(double[,] Data, int Size)
        {
            int interval = 1;
            double[] temp = new double[2];
            for (; interval < Size; interval++)
            {
                for (int i = 0; i < Size - interval; i++)
                {
                    /*Data의 앞 요소와 뒤 요소를 비교 후 조건 충족시 스왑*/
                    if (Data[i, 1] < Data[i + 1, 1])
                    {
                        temp[0] = Data[i, 0];
                        temp[1] = Data[i, 1];
                        Data[i, 0] = Data[i + 1, 0];
                        Data[i, 1] = Data[i + 1, 1];
                        Data[i + 1, 0] = temp[0];
                        Data[i + 1, 1] = temp[1];
                    }
                }
            }

            return Data;
        }
        private int[,] BubbleSort(int[,] Data, int Size)
        {
            int interval = 1;
            int[] temp = new int[2];
            for (; interval < Size; interval++)
            {
                for (int i = 0; i < Size - interval; i++)
                {
                    /*Data의 앞 요소와 뒤 요소를 비교 후 조건 충족시 스왑*/
                    if (Data[i, 1] < Data[i + 1, 1])
                    {
                        temp[0] = Data[i, 0];
                        temp[1] = Data[i, 1];
                        Data[i, 0] = Data[i + 1, 0];
                        Data[i, 1] = Data[i + 1, 1];
                        Data[i + 1, 0] = temp[0];
                        Data[i + 1, 1] = temp[1];
                    }
                }
            }

            return Data;
        }

    }
}
