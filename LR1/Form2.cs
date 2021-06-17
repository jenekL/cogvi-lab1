using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LR1
{
    public partial class Form2 : Form
    {
        public int alpha;
        public int beta;
        public int heightKernel;
        public int widthKernel;
        public double sigma1;
        public double sigma2;
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(textBox1.Text) >= 0 && Convert.ToInt32(textBox2.Text) <= 255)
            {
                alpha = Convert.ToInt32(textBox1.Text);
                beta = Convert.ToInt32(textBox2.Text);
                heightKernel = Convert.ToInt32(textBox3.Text);
                widthKernel = Convert.ToInt32(textBox4.Text);

                sigma1 = Convert.ToDouble(textBox5.Text);
                sigma2 = Convert.ToDouble(textBox6.Text);
                this.Close();
            }
            else
            {
                MessageBox.Show("Введены не правильные данные! Необходимые данные в интервале от 0 до 255");
            }
            
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
