using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Security;
using System.Security.Cryptography;

namespace AI_Tetris
{
    public class Genetic
    {

        string learning_file = "genetic_info";
        string meta_file = "meta_info.txt";

        string formula = null;

        int C_generation; // Current generation
        int Generation;
        int TSerial;//total serial
        int temp_serial;
        int Serial_Number;
        int total_Number;
        int score;
        int tno_weight;
        static int size_of_serial = 100;
       

        double[] weight;

        Random Random_Generator;
        /*
         * Meta Format :  [Recent_Generation] [The number of Serial in Same Generation] [total number of data] [the number of weight] [Formula]
         * Learn Format : [Generation] [Serial] [Weight] [Score]
         */

        public Genetic()
        {
            FileStream MetaHandler = new FileStream(meta_file , FileMode.OpenOrCreate);


            StreamReader reader;
            StreamWriter writer;
            string ReadBuffer;
            string[] buff;
            Random_Generator = new Random();
            try
            {
                reader = new StreamReader(MetaHandler);
                ReadBuffer = reader.ReadLine();
                buff = ReadBuffer.Split(' ');
                if (ReadBuffer != null)
                {
                    C_generation = int.Parse(buff[0]);
                    Serial_Number = int.Parse(buff[1]);
                    total_Number = int.Parse(buff[2]);
                    tno_weight = int.Parse(buff[3]);
                    formula = buff[4].ToString();
                }
                //reader.Close();
            }
            catch (Exception EX)
            {
                /*MetaData가 없으면 새로 만듦. 이때의 기본 계산수식은 F*W+S*W+T*W+(f-O)*W+f*W-S*W로 함.*/
                writer = new StreamWriter(MetaHandler);
                writer.WriteLine("1 0 100 6 F*W+S*W+T*W+(f-O)*W+f*W-s*W");
                C_generation = 1;
                total_Number = 100;
                writer.Close();
                MetaHandler.Close();

                /*Try에서 실패한 읽기작업 재실행 */
                MetaHandler= new FileStream(meta_file, FileMode.Open);
                reader = new StreamReader(MetaHandler);
                ReadBuffer = reader.ReadLine();
                buff = ReadBuffer.Split(' ');
                if (ReadBuffer != null)
                {
                    C_generation = int.Parse(buff[0]);
                    Serial_Number = int.Parse(buff[1]);
                    total_Number = int.Parse(buff[2]);
                    tno_weight = int.Parse(buff[3]);
                    formula = buff[4].ToString();
                }

                Console.WriteLine(EX.Message);
            }
            reader.Close();
            weight = new double[tno_weight];
            MetaHandler.Close();
        }

        /*Interface 1*/
            public double[] get_Weight()
        {
            return weight;
        }
        
        /*Interface 2*/
        public int get_Current_Generation()
        {
            return C_generation;
        }

        /*Interface 3*/
        public int get_Current_Serial()
        {
            return Serial_Number;
        }

        /*Interface 4*/
        public void set_Score(int temp)
        {
            score = temp;
        }


        /*Interface 5*/
        public char[] get_Formula()
        {
            return formula.ToCharArray();
        }

        /*Interface 6*/
        public string get_formula()
        {
            return formula;
        }

        public bool Next()
        {
            StreamReader LearningReader;
            try
            {
                LearningReader = new StreamReader(learning_file + C_generation.ToString() + "(" + tno_weight.ToString() + ")" + ".txt");
                if (Load(LearningReader))
                    return true;
            }
            catch(Exception error)
            {
                Console.WriteLine(error.Message);
            }
            return false;
        }

        public bool Make_first_Learning_file()
        {
            FileStream Temp_Handler1;
            try
            {
                //if (LearningHandler..Equals(learning_file + "1" + ".txt"))
                Temp_Handler1 = new FileStream(learning_file + 1 + "(" + tno_weight.ToString() + ")" + ".txt", FileMode.Create);
                StreamWriter writer = new StreamWriter(Temp_Handler1);
                Random_Generator = new Random();

                string WriteBuffer;
                for (int j = 0; j < size_of_serial; j++)
                {
                    WriteBuffer = '1' + " ";
                    WriteBuffer += j.ToString() + " ";

                    for (int i = 0; i < tno_weight; i++)
                        WriteBuffer += (Random_Generator.Next() % 30).ToString() + " ";
                    WriteBuffer += score.ToString();
                    writer.WriteLine(WriteBuffer);
                }
                writer.Close();

                Temp_Handler1.Close();
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return true;
        }

        /* Learn Format : [Generation] [Serial] [Weight] [Score] 
        public void Save_Gene(int[] weight)
        {
            StreamWriter writer = new StreamWriter(); 
            string WriteBuffer = Generation.ToString()+" ";
            WriteBuffer += (Serial_Number + " ");
            for(int i=0; i<tno_weight; i++)
                WriteBuffer += (weight[i] + " ");
            WriteBuffer += (score + " ");
            writer.WriteLine(WriteBuffer);
            writer.Close();
        }
        */

        public bool Save_Score(int score)
        {
            FileStream LearningHandler= new FileStream(learning_file + C_generation + "(" + tno_weight.ToString() + ")" + ".txt", FileMode.OpenOrCreate);
            StreamWriter t_writer;
            StreamReader t_reader = new StreamReader(LearningHandler);
            string[] ReadBuffer = new string[100];
            int counter = 0;

            for (int i = 0; i < 100; i++)
                ReadBuffer[i] = t_reader.ReadLine();
            t_reader.Close();
            t_reader = null;
            LearningHandler.Close();

            string[] buff = ReadBuffer[counter].Split(' ');
            temp_serial = int.Parse(buff[1]);

            while (Serial_Number != temp_serial)
            {
                counter++;
                if (counter == size_of_serial)
                {
                    Console.WriteLine("범위를 초과하였습니다.");
                    return false;
                }
                if (ReadBuffer == null)
                    return false;
                buff = ReadBuffer[counter].Split(' ');
                temp_serial = int.Parse(buff[1]);
            }
            buff[tno_weight+2]= score.ToString();
            ReadBuffer[counter] = null;
            for (int i = 0; i < tno_weight+3; i++)
                ReadBuffer[counter] += (buff[i] + " ");
            
            
            t_writer = new StreamWriter(learning_file + C_generation + "(" + tno_weight.ToString() + ")" + ".txt", false);
            for (int i = 0; i < size_of_serial; i++)
                t_writer.WriteLine(ReadBuffer[i]);

            Serial_Number++;//다음 유전자 순서로 세팅.

            t_writer.Close();
            t_writer = null;
            Save_Meta();
          
            return true;
        }

        public string Detection(string Detector)
        {
            string result = null;
            foreach (char item in Detector)
            {
                if (item != 'F' && item != 'S' && item != 'T' && item != 'O' && item != 'f' && item != 'S' && item != 's' && item != 'W' && item != 'w' && item != ' ' && item != '(' && item != ')' && item != '+' && item != '-' && item != '*' && item != '/')
                    return null;
            }

            string[] buffer = Detector.Split(' ');
            foreach (string item in buffer)
                result += item;
            return result;
        }

        public bool save_formula(string target)// this function set formula in meta file and the number of weight in current program.
        {
            int counter = 0;

            /*가중치 계산*/
            foreach (char item in target)
            {
                if (item == 'w' || item == 'W')
                    counter++;
            }

            /*present program will be revised immediately */
            tno_weight = counter; //새로 설정한 가중치 개수 적용. 
            weight = new double[tno_weight];// 가중치 적용.

            StreamWriter t_writer;
            t_writer = new StreamWriter(meta_file, false);
            t_writer.WriteLine(1 + " " + 0 + " " + 100 + " " + counter + " " + target);
            t_writer.Close();
            t_writer = null;
            return true;
        }

        /*메타 데이터 저장*/
        private void Save_Meta()
        {
            StreamWriter t_writer;
            t_writer = new StreamWriter(meta_file, false);
            t_writer.WriteLine(C_generation.ToString() + " " + Serial_Number.ToString() + " " + (100 * C_generation).ToString() + " " + tno_weight + " " + formula);
            t_writer.Close();
            t_writer = null;
        }

        /* Learn Format : [Generation] [Serial] [Weight] [Score] */
        public double[] Load_Recent_Gene()
        {
            StreamReader LearningReader = new StreamReader(learning_file + C_generation.ToString() +"(" + tno_weight.ToString() + ")"+ ".txt");
            Load(LearningReader);

            return weight;
        }

        private bool Load(StreamReader reader)
        {
            string ReadBuffer = reader.ReadLine();
            if (ReadBuffer == null)
                return false;
            string[] buff = ReadBuffer.Split(' ');


            temp_serial = int.Parse(buff[1]);
            while (Serial_Number != temp_serial)
            {
                ReadBuffer = reader.ReadLine();
                if (ReadBuffer == null)
                    return false;
                buff = ReadBuffer.Split(' ');
                temp_serial = int.Parse(buff[1]);
            }
            /*Generation = int.Parse(buff[0]);
            while (C_generation != Generation)
            {
                ReadBuffer = reader.ReadLine();
                if (ReadBuffer == null)
                    return false;
                buff = ReadBuffer.Split(' ');
                Generation = int.Parse(buff[0]);
            }*/
            reader.Close();
            for (int i = 2; i < tno_weight + 2; i++)
                weight[i - 2] = double.Parse(buff[i]);
            return true;
        }
        //RSA암복호화 출처:http://redqueen-textcube.blogspot.com/2009/12/%EB%AC%B8%EC%9E%90%EC%97%B4-%EC%95%94%ED%98%B8%ED%99%94-rsa-md5-des-c.html

        //RSA 암호화
        public static string RSAEncrypt(string sValue, string sPubKey)
        {

            /*
            //공개키 생성
            sPubKey = sPubKey.Replace(' ', '+');
            byte[] keybuf = Convert.FromBase64String(sPubKey);
            sPubKey = (new UTF8Encoding()).GetString(keybuf);
            System.Security.Cryptography.RSACryptoServiceProvider oEnc = new RSACryptoServiceProvider(); //암호화


            oEnc.FromXmlString(sPubKey);

            //암호화할 문자열을 UFT8인코딩
            byte[] inbuf = (new UTF8Encoding()).GetBytes(sValue);
            //암호화
            byte[] encbuf = oEnc.Encrypt(inbuf, false);

            //암호화된 문자열 Base64인코딩
            return Convert.ToBase64String(encbuf);
            */
            return sValue;
        }

        //RSA 복호화
        public static string RSADecrypt(string sValue, string sPrvKey)
        {
            /*
            //개인키 생성
            sPrvKey = sPrvKey.Replace(' ', '+');
            byte[] inbuf = Convert.FromBase64String(sPrvKey);
            sPrvKey = (new UTF8Encoding()).GetString(inbuf);

            //RSA객체생성
            System.Security.Cryptography.RSACryptoServiceProvider oDec = new RSACryptoServiceProvider(); //복호화
            //개인키로 활성화
            oDec.FromXmlString(sPrvKey);

            //sValue문자열을 바이트배열로 변환
            byte[] srcbuf = Convert.FromBase64String(sValue);

            //바이트배열 복호화
            byte[] decbuf = oDec.Decrypt(srcbuf, false);

            //복호화 바이트배열을 문자열로 변환
            string sDec = (new UTF8Encoding()).GetString(decbuf, 0, decbuf.Length);
            return sDec;
            */
            return sValue;
        }

        public string get_goodPattern()
        {
            string patternPath = "p"+tno_weight.ToString();

            return LoadPattern(patternPath);
        }

        public string get_badPattern()
        {
            string patternPath = "bad"+tno_weight.ToString();

            return LoadPattern(patternPath);
        }
        
        public void save_goodPattern(string target)
        {
            string patternPath = "p"+tno_weight.ToString();
            SavePattern(patternPath, target);
        }

        public void save_badPattern(string target)
        {
            string patternPath = "bad" + tno_weight.ToString();
            SavePattern(patternPath, target);
        }

        private string LoadPattern(string filename)
        {
            FileStream fileHandler;
            StreamReader Reader;
            string pattern;

            filename=RSADecrypt(filename, "PATTERN");

            try//패턴파일을 못 찾을 경우의 예외처리.
            {
                fileHandler = new FileStream(filename, FileMode.Open);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.StackTrace);
                return null;
            }
            Reader = new StreamReader(fileHandler);

            pattern=Reader.ReadLine();
            fileHandler.Close();
            Reader.Close();
            return pattern;
        }

        private bool SavePattern(string filename, string pattern)
        {
            FileStream fileHandler;
            StreamWriter writer;

            filename = RSAEncrypt(filename, "PATTERN");

            try//패턴파일을 못 찾을 경우의 예외처리.
            {
                fileHandler = new FileStream(filename, FileMode.Create);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                return false;
            }
            writer = new StreamWriter(fileHandler);

            writer.WriteLine(pattern);
            writer.Close();
            fileHandler.Close();
            return true;
        }

        public string Learn(int combinationValue)//argument will be used as combination argument.
        {
            Analyzer analyzer = new Analyzer(learning_file + C_generation.ToString() + "(" + tno_weight.ToString() + ")" + ".txt");
            StreamWriter t_writer;
            string[] ReaderBuffer = new string[size_of_serial];
            string[] buff = new string[combinationValue];
            string[] patternlist;
            double[,] sample = new double[combinationValue,tno_weight];
            double[,] new_Weight = new double[size_of_serial, tno_weight];
            double[,] temp;
            double[] t_w1 = new double[combinationValue];
            double[] t_w2 = new double[combinationValue];
            double[] new_t = new double[combinationValue];
            int W_counter = 0 ;
            int term = 40; // 패턴 매칭 반복주기.
            int M_Counter=0;
            string patternPath = tno_weight.ToString();
            string pattern;
            string newPattern;
            string bad_pattern = get_badPattern();
            
            sample = get_Sample(combinationValue);

            /*Load Pattern data and edit*/
            temp = analyzer.Prepare(learning_file + C_generation.ToString() + "(" + tno_weight.ToString() + ")" + ".txt");
            newPattern = analyzer.Patternize(analyzer.get_Sequence(temp),0,10);
            if (C_generation != 1)
            {
                if ((pattern = LoadPattern(patternPath)) == null) // 패턴불러오기 실패한 경우.
                    return null;
                newPattern = analyzer.AddPattern(newPattern, pattern);
            }
            SavePattern(patternPath, newPattern);

            analyzer.set_PatternSet(newPattern);
            
            /*10 Combination 2 = 45*/
            for (int j = 0; j < combinationValue; j++)
                for (int i = j + 1; i < combinationValue; i++)
                {
                    for (int n = 0; n < tno_weight; n++) {
                        t_w1[n] = sample[i, n];
                        t_w2[n] = sample[j, n];
                    }
                    new_t = Reproduction(t_w1, t_w2);

                    //ConditionalFlag = analyzer.CompareToPattern(new_t, tno_weight,C_generation);
                    /*if (ConditionalFlag == false)
                    {
                        재생산시 패턴에 안맞는 값이 나오면 값에 부합하게 가중치값 랜덤 재수정.
                        for(int m=0; m<tno_weight; m++)
                        {
                            int luck = (Random_Generator.Next() % 2)+1;
                            double adjustment = 1;
                            for (int repeater = 0; repeater < luck; repeater++)
                            {
                                adjustment *= (-1);
                            }
                            t_w1[m] += adjustment;
                            t_w2[tno_weight - (m + 1)] += adjustment;
                        }
                    }*/

                    
                    for (int m = 0; m < tno_weight; m++)
                    {
                        new_Weight[W_counter, m] = new_t[m];
                    }
                    W_counter++;

                }

            /*10 Combination 2 = 45. 합 90개*/
            for (int j = 0; j < combinationValue; j++)
                for (int i = j + 1; i < combinationValue; i++)
                {
                    for (int n = 0; n < tno_weight; n++)
                    {
                        t_w1[n] = sample[i, n];
                        t_w2[n] = sample[j, n];
                    }
                    while (M_Counter < term)
                    {
                        new_t = Cross(t_w1, t_w2);
                        if (analyzer.CompareToPattern(new_t, tno_weight, C_generation, bad_pattern))
                            break;
                        M_Counter++;

                    }
                    M_Counter = 0;

                    
                    for (int m = 0; m < tno_weight; m++)
                    {
                        new_Weight[W_counter, m] = new_t[m];
                    }
                    W_counter++;

                }
            /*랜덤 가중치 연산. 돌연변이 10개 합 100개*/
            for (int n = 0; n < tno_weight; n++)
                t_w1[n] = sample[0, n];

            for (int i = 0; i < 6; i++)
            {
                double[] temporary=new double[tno_weight];
                while (M_Counter < term)
                {
                    temporary = Mutation(t_w1, 1, i);
                    if (analyzer.CompareToPattern(temporary, tno_weight, C_generation, bad_pattern))
                        break;
                    M_Counter++;

                }
                M_Counter = 0;

                for (int n = 0; n < tno_weight; n++)
                    new_Weight[W_counter, n] = temporary[n];
                W_counter++;
            }

            for (int i = 0; i < 4; i++)
            {
                double[] temporary=new double[tno_weight];
                while (M_Counter < term)
                {
                    temporary = Mutation(t_w1, 1, i);
                    if (analyzer.CompareToPattern(temporary, tno_weight, C_generation, bad_pattern))
                        break;
                    M_Counter++;

                }
                M_Counter = 0;
                for (int n = 0; n < tno_weight; n++)
                    new_Weight[W_counter, n] = temporary[n];
                W_counter++;
            }

            /*
            for (int i = 0; i < 6; i++)
            {
                for (int n = 0; n < tno_weight; n++)
                {
                    if(i==n)
                        new_Weight[W_counter, n] = Random_Generator.Next()%40;
                    else
                        new_Weight[W_counter, n] = t_w1[n];
                }
                W_counter++;
            }

            for (int i = 0; i < 4; i++)
            {
                for (int n = 0; n < tno_weight; n++)
                {
                    if (i == n || (i+2) == n)
                        new_Weight[W_counter, n] = Random_Generator.Next() % 40;
                    else
                        new_Weight[W_counter, n] = t_w1[n];
                }
                W_counter++;
            }*/
            /*
            for (int n = 0; n < tno_weight; n++)
            {
                new_Weight[W_counter, n] = Random_Generator.Next() % 40;
            }
            W_counter++;*/
            /*새로운 유전자 정보 생성*/
            C_generation++;
            Serial_Number = 0;
            t_writer = new StreamWriter(learning_file + C_generation.ToString()+"(" + tno_weight.ToString() + ")" + ".txt", false);
            string WriteBuffer;
            for (int j = 0; j < size_of_serial; j++)
            {
                WriteBuffer = C_generation + " ";
                WriteBuffer += j.ToString() + " ";

                for (int i = 0; i < tno_weight; i++)
                    WriteBuffer += new_Weight[j,i].ToString() + " ";
                WriteBuffer += '0';
                t_writer.WriteLine(WriteBuffer);
            }
            t_writer.Close();
            Save_Meta();
            string Message = C_generation + "세대 학습완료.";
            return Message;
        }

        private double[,] get_Sample(int combinationValue)
        {
            StreamReader t_reader = new StreamReader(learning_file + C_generation + "(" + tno_weight.ToString() + ")" + ".txt");
            
            string[] ReaderBuffer = new string[size_of_serial];
            string[] buff;

            double[,] sample = new double[size_of_serial, tno_weight];
            double[,] result = new double[size_of_serial, tno_weight];

            int[,] score = new int[size_of_serial, 2];

            

            /*유전자 별 적합도(점수) 및 가중치 정보를 각각 score와 sample에 저장합니다.*/
            for (int counter=0; counter<size_of_serial; counter++)
            {
                ReaderBuffer[counter] = t_reader.ReadLine();
                buff = ReaderBuffer[counter].Split(' ');
                score[counter, 0] = counter;
                score[counter, 1] = int.Parse(buff[tno_weight + 2]);
                for(int i=0; i<tno_weight; i++)
                {
                    sample[counter,i]=double.Parse(buff[2 + i]);
                }
            }

            score=BubbleSort(score, size_of_serial);

            for(int counter=0; counter<combinationValue; counter++)
            {
                for (int j = 0; j < tno_weight; j++)
                    result[counter, j] = sample[score[counter, 0], j];
            }
            ReaderBuffer = null;
            t_reader.Close();
            return result;

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

        private double[] Reproduction(double[] W1, double[] W2)
        {
            double[] result = new double[tno_weight];

            for (int i = 0; i < tno_weight; i++)
                result[i] = (W1[i] + W2[i])/2;
            return result;
        }

        private double[] Cross(double[] W1, double[] W2)
        {
            double[] result = new double[tno_weight];
            int Luck = Random_Generator.Next() % 100;

            for (int i = 0; i < tno_weight; i++)
            {
                if (Luck % 2 == 1)
                    result[i] = W1[i];
                else
                    result[i] = W2[i];
                Luck = Random_Generator.Next() % 100;
            }
            return result;
        }

        private double[] Mutation(double[] W, int division, int interval)
        {
            double[] result=new double[tno_weight];

            for(int i=0; i<tno_weight; i++)
            {
                if (division == 1)
                {
                    if (i == interval)
                        result[i] = (double)Random_Generator.Next() % 40;
                    else
                        result[i] = W[i];
                }
                else
                {
                    if (i == interval || i == interval + division)
                        result[i] = (double)Random_Generator.Next() % 40;
                    else
                        result[i] = W[i];
                }
            }

            return result;
        }
        
        private double[] ProductionByPattern(string target)//패턴 중 하나를 뽑아서 순서에 맞게 랜덤하게 생성.
        {
            double[] new_t=new double[tno_weight];
            string[] buf;
            int[] index;
            int dif = 30 % tno_weight;

            buf = target.Split('>');
            index = new int[buf.Length];
            for (int i = 0; i < buf.Length; i++) {
                index[i] = int.Parse(buf[i].Replace("Weight ", ""));
                index[i]--;
            }
            for (int i = 0; i < index.Length; i++)
                new_t[index[i]] = Random_Generator.Next() % dif + (i * dif);

            return new_t;
        }

        public void set_Meta(int[] temp)
        {
            C_generation = temp[0];
            Serial_Number = temp[1];
            total_Number = temp[2];
            Save_Meta();
        }

    }
}
