using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AI_Tetris
{
    public partial class form_calcM : Form
    {
        public string formula;
        public Genetic Learning;
        public CalculationNetwork Calc;

        public form_calcM()
        {
            InitializeComponent();
        }

        private void form_calculateModel_Load(object sender, EventArgs e)
        {
            Current_formula.Text = "설정된 계산식 \"" + formula + "\"";
        }

        
        private void button2_Click(object sender, EventArgs e)//계산식 초기화 이벤트
        {
            string init_formula = "F*W+S*W+T*W+(f-O)*W+f*W-s*W";

            /*계산식 설정 이벤트 작성*/
            Learning.save_formula(init_formula);
            Calc.set_formula(init_formula);

            Current_formula.Text = "설정된 계산식 \"" + init_formula + "\"";
            Notice_Board.Text = "계산식 설정이 완료되었습니다.";
        }

        private void Setting_Click(object sender, EventArgs e)
        {
            string Detector = textBox1.Text;
            if ((Detector = Learning.Detection(Detector)) == null)
            {
                Notice_Board.Text = "계산식이 약속에 맞지 않습니다. 상단의 메뉴얼을 참조해주세요.";
                return;
            }
            /*계산식 설정 이벤트 작성*/
            Learning.save_formula(Detector);
            Calc.set_formula(Detector);

            Current_formula.Text = "설정된 계산식 \"" + Detector +"\"";
            Notice_Board.Text = "계산식 설정이 완료되었습니다.";
        }

        

    }
}
