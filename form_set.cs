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
    public partial class form_set : Form
    {
        int Generation;
        int Serial;
        int[] ResultSet;

        public form_set()
        {
            InitializeComponent();
        }

        public int[] get_ResultSet()
        {
            if (ResultSet != null)
                return ResultSet;
            else
                return null;
        }

        private void form_set_Load(object sender, EventArgs e)
        {
        }

        private void button_Submit_Click(object sender, EventArgs e)
        {
            if (textBox_generation.Text == string.Empty && textBox_Serial.Text == string.Empty)
                MessageBox.Show("값을 입력해주세요.");
            if (textBox_generation.Text == string.Empty)
            {
                MessageBox.Show("세대 값을 입력하지 않았습니다.");
                return;
            }
            if (textBox_Serial.Text == string.Empty)
            {
                MessageBox.Show("인덱스 값을 입력하지 않았습니다.");
                return;
            }
            
            ResultSet = new int[3];
            Generation =int.Parse(textBox_generation.Text);
            Serial = int.Parse(textBox_Serial.Text);
            ResultSet[0] = Generation;
            ResultSet[1] = Serial;
            ResultSet[2]=Generation* Serial;
            this.Close();

        }

        private void button_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
