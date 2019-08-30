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
    public partial class form_pattern : Form
    {
        public Genetic IO;

        public form_pattern()
        {
            InitializeComponent();
        }

        private void form_pattern_Load(object sender, EventArgs e)
        {
            textBox_good.Text = IO.get_goodPattern();
            textBox_bad.Text = IO.get_badPattern();
        }

        private void button_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button_Apply_Click(object sender, EventArgs e)
        {
            IO.save_goodPattern(textBox_good.Text);
            if(textBox_bad.Text!=null)
                IO.save_badPattern(textBox_bad.Text);

            this.Close();
        }
    }
}
