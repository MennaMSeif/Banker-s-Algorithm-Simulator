using System;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form2 : Form
    {
        public static int npr, nrc;
        public static  int[,] maxMatrix;
        public static int[,] allocationMatrix;
        public static int[] availableVector;
        public static  int[] total;

        public Form2()
        {
            InitializeComponent();
        }

        public bool compare()
        {
            for(int i = 0; i <npr; i++)
                
            {
                for (int j = 0 ; j <nrc; j++)
                {
                    if (allocationMatrix[i, j] > maxMatrix[i, j])
                    {

                        MessageBox.Show("Current alllocation can't be greater than the Maximum need");
                        return false;
                    }
                }

            }
            return true;
        }

        //calculate
        private void button1_Click(object sender, EventArgs e)
        {

            bool flag = false;
            

            foreach (DataGridViewRow rw in this.dataGridView1.Rows)
            {
                for (int i = 1; i < rw.Cells.Count; i++)
                {
                    if (rw.Cells[i].Value == null || rw.Cells[i].Value == DBNull.Value || String.IsNullOrWhiteSpace(rw.Cells[i].Value.ToString()))
                    {
                        flag = true;
                        
                    }
                
                }
            }
            if (flag)
            {
                MessageBox.Show("Please fill all the cells in Allocation table");
            }
     


            foreach (DataGridViewRow rw in this.dataGridView2.Rows)
            {
                for (int i = 0; i < rw.Cells.Count; i++)
                {
                    if (rw.Cells[i].Value == null || rw.Cells[i].Value == DBNull.Value || String.IsNullOrWhiteSpace(rw.Cells[i].Value.ToString()))
                    {
                        flag = true;
                    }
                }
            }
            if (flag)
            {
                MessageBox.Show("Please fill all the cells in Maximum Need table");
            }



            foreach (DataGridViewRow rw in this.dataGridView4.Rows)
            {
                for (int i = 0; i < rw.Cells.Count; i++)
                {
                    if (rw.Cells[i].Value == null || rw.Cells[i].Value == DBNull.Value || String.IsNullOrWhiteSpace(rw.Cells[i].Value.ToString()))
                    {
                        flag = true;
                    }
                }
            }
            if (flag)
            {
                MessageBox.Show("Please fill all the cells in Available table");
            }

            if (comboBox1.SelectedIndex == -1 || comboBox2.SelectedIndex == -1 || numericUpDown1.Text == "" ) 
            
            {
                 flag= true;
            }
          
            if (flag)
            {
                MessageBox.Show("Please fill all the request and its resource");
            }

            else if (flag == false && compare() == true )
            {
                if (availableVector[comboBox2.SelectedIndex]< numericUpDown1.Value)
                {
                    MessageBox.Show("Request is more than available resources");
                }
                else if(numericUpDown1.Value>maxMatrix[comboBox1.SelectedIndex, comboBox2.SelectedIndex]- allocationMatrix[comboBox1.SelectedIndex, comboBox2.SelectedIndex])
                {
                    MessageBox.Show("Request is more than the maximum resource ");
                }
                else
                {
                    MessageBox.Show("Request is approved ");
                    allocationMatrix[comboBox1.SelectedIndex, comboBox2.SelectedIndex] +=(int) numericUpDown1.Value;
                    availableVector[comboBox2.SelectedIndex] -= (int)numericUpDown1.Value;
                }
                Form3 f3 = new Form3();
                f3.ShowDialog();
            }
          
        }

        //total
        private void button2_Click(object sender, EventArgs e)
        {
            bool flag = false;

            foreach (DataGridViewRow rw in this.dataGridView1.Rows)
            {
                for (int k = 0; k < rw.Cells.Count; k++)
                {
                    if (rw.Cells[k].Value == null || rw.Cells[k].Value == DBNull.Value || String.IsNullOrWhiteSpace(rw.Cells[k].Value.ToString()))
                    {
                        flag = true;
                    }
                }
            }

            foreach (DataGridViewRow rw in this.dataGridView4.Rows)
            {
                for (int k = 0; k < rw.Cells.Count; k++)
                {
                    if (rw.Cells[k].Value == null || rw.Cells[k].Value == DBNull.Value || String.IsNullOrWhiteSpace(rw.Cells[k].Value.ToString()))
                    {
                        flag = true;
                    }
                }
            }
            foreach (DataGridViewRow rw in this.dataGridView2.Rows)
            {
                for (int i = 0; i < rw.Cells.Count; i++)
                {
                    if (rw.Cells[i].Value == null || rw.Cells[i].Value == DBNull.Value || String.IsNullOrWhiteSpace(rw.Cells[i].Value.ToString()))
                    {
                        flag = true;
                    }
                }
            }

            if (flag)
            {
                MessageBox.Show("Can't view total table, Please check that all cells contain data");
            }

            else
            {

                int i = 0, j = 0;
                foreach (DataGridViewRow r in dataGridView1.Rows)
                {
                    if (r.Index == npr)
                    { continue; }
                    foreach (DataGridViewCell c in r.Cells)
                    {
                        if (c.ColumnIndex == 0)
                        { continue; }
                        allocationMatrix[i, j] = int.Parse((string)c.Value);
                        j++;
                    }
                    i++;
                    j = 0;
                }

                i = 0; j = 0;
                foreach (DataGridViewRow r in dataGridView2.Rows)
                {
                    if (r.Index == npr)
                    { continue; }
                    foreach (DataGridViewCell c in r.Cells)
                    {
                        if (c.ColumnIndex == 0)
                        { continue; }
                        maxMatrix[i, j] = int.Parse((string)c.Value);
                        j++;
                    }
                    i++;
                    j = 0;
                }

                //calculate the total table
                foreach (DataGridViewRow r in dataGridView4.Rows)
                {
                    if (r.Index == 1)
                    { continue; }
                    foreach (DataGridViewCell c in r.Cells)
                    {
                        availableVector[j] = int.Parse((string)c.Value);
                        j++;
                    }

                }

                for (i = 0; i < nrc; i++)
                {
                    total[i] = availableVector[i];
                    for (j = 0; j < npr; j++)
                    {
                        total[i] += allocationMatrix[j, i];
                    }
                }


                i = 0;
                foreach (DataGridViewRow r in dataGridView3.Rows)
                {
                    foreach (DataGridViewCell c in r.Cells)
                    {
                        c.Value = total[i];
                        i++;
                    }
                }
                button1.Enabled = true;

            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            npr = Form1.npr;
            nrc = Form1.nrc;
            button1.Enabled = false;
            
            //int x = 'A';

            dataGridView1.Columns.Add("Process", "");
            for (int i = 0; i < nrc; i++)
            {
                dataGridView1.Columns.Add("resource" + i, " R" + i);
            }
            for (int i = 0; i < npr; i++)
            {
                dataGridView1.Rows.Add("P" + i);
            }

            dataGridView2.Columns.Add("Process", "");
            for (int i = 0; i < nrc; i++)
            {
                dataGridView2.Columns.Add("resource" + i, "R" + i);
            }
            for (int i = 0; i < npr; i++)
            {
                dataGridView2.Rows.Add("P" + i);
            }
            

            for (int i = 0; i < nrc; i++)
            {
                dataGridView3.Columns.Add("resource" + i, "R" + i);
            }
            //dataGridView3.Rows.Add();
          
            for (int i = 0; i < nrc; i++)
            {
                dataGridView4.Columns.Add("resource" + i, " R" + i);
            }
          
                dataGridView4.Rows.Add();
            

            for (int i = 0; i < npr; i++)
            {
                comboBox1.Items.Add("P"+i);
            }

            for (int i = 0; i < nrc; i++)
            {
                comboBox2.Items.Add("R" + i);
            }
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
            maxMatrix = new int[npr, nrc];
            allocationMatrix = new int[npr, nrc];
            availableVector = new int[ nrc];
            total = new int[nrc];

        }
    }
}
