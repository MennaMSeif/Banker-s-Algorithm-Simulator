using System;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public static int npr, nrc;
        public Form1()
        {
            InitializeComponent();
        }

        public  void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
             npr = (int)numericUpDown1.Value;
      
        }

        public  void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            nrc= (int)numericUpDown2.Value;
        }

        

        private void button1_Click(object sender, EventArgs e)
        {
            bool flag = false;

            if ((int)numericUpDown1.Value == 0|| (int)numericUpDown2.Value == 0)
            {
                flag = true;
                if (flag)
                {
                    MessageBox.Show("Processes or Resources can't be equal zero");
                }
            }
            
            else
            {
                Form2 f2 = new Form2();
                f2.ShowDialog();
            }
            
        }
    }
}
