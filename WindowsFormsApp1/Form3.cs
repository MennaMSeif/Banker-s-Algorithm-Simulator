using System;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form3 : Form
    {
        public static int npr, nrc;
        public int[,] maxMatrix;
        public int[,] allocationMatrix;
        public int[] availableVector;
        public int[] total;
        public int[,] remain;
        bool [] visted = {false};
        int currpr=-1;
        public Form3()
        {
            InitializeComponent();
        }

        void check()
        {
            int i, j;
            //  bool notsafe = true;
            while (visted[currpr])
                  {
                currpr = (currpr + 1) % npr;
              }

            for ( j=0;j< nrc; j++)
                {
                    if (remain[currpr,j]> availableVector[j])
                    {
                        break;
                    }
                }
                if (j == nrc)
                {
                 //   notsafe = false;
                    visted[currpr] = true;
                   
                    label1.Text +=("p" + currpr + " has avialble resources and done\n");
                    for (j = 0; j < nrc; j++)
                    {
                        availableVector[j] += allocationMatrix[currpr, j];
                        allocationMatrix[currpr, j] = 0;
                        remain[currpr, j] = 0;
                    }
                    
                    return;
                }
            else
            {
                label1.Text += ("p" + currpr + " has no resources and wait\n");
                return;
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //check system finished or not
            bool finish = true;
            for (int t = 0; t < nrc; t++)
            {
                if (availableVector[t] < total[t])
                    finish = false;
            }
            if (finish)
            {
                label1.Text += "processes finished, safe\n";
                button1.Enabled = false;
                return;
            }
           
                currpr = (currpr + 1) % npr;
     
                check();
            update_rem();
            int i = 0;
            if (currpr == npr-1)
            {
                if (check_unsafe())
                {
                    label1.Text += "unsafe!\n";
                    button1.Enabled = false;
                    return;

                }
            }
            foreach (DataGridViewRow r in dataGridView2.Rows)
            {
                foreach (DataGridViewCell c in r.Cells)
                {
                    c.Value = availableVector[i];
                    i++;
                }
            }

        }

        

        private void button2_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

       
       
        void update_rem()
        {
            int k = 0, t = 0;
            foreach (DataGridViewRow r in dataGridView1.Rows)
            {
                t = 0;
                if (r.Index == npr)
                {
                    continue;
                }
                foreach (DataGridViewCell c in r.Cells)
                {
                    if (c.ColumnIndex == 0)
                    { continue; }
                    c.Value = remain[k, t];
                    t++;
                }
                k++;
            }
        }
        bool check_unsafe()
        {
            bool uns= true;
            for(int i = 0; i < npr; i++)
            {
                while (visted[i]&i!=npr-1)
                {
                    i++;
                }
                bool ch = true;
                for (int j = 0; j < nrc; j++)
                {
                    if (remain[i, j] > availableVector[j])
                    {
                        ch = false;
                        break;
                    }
                }
                if (ch)
                    uns = false;
            }


            return uns;
        }
        private void Form3_Load(object sender, EventArgs e)
        {
            npr = Form2.npr;
            nrc=Form2.nrc;
            maxMatrix=Form2.maxMatrix;
            allocationMatrix = Form2.allocationMatrix;
            availableVector= Form2.availableVector;
            total= Form2.total;
            remain = new int[npr, nrc];
            visted = new bool[npr];

            for (int i=0;i<npr;i++)
            {
                for(int j = 0; j < nrc; j++)
                {
                    remain[i,j] = maxMatrix[i, j] - allocationMatrix[i,j];
                }
            }

            for (int i = 0; i < nrc; i++)
            {
                dataGridView2.Columns.Add("resource" + i, " rs" + i);
            }

            dataGridView1.Columns.Add("process", "process");
            for (int i = 0; i < nrc; i++)
            {
                dataGridView1.Columns.Add("resource" + i, " rs" + i);
            }
            for (int i = 0; i < npr; i++)
            {
                dataGridView1.Rows.Add("p" + i);
            }

            update_rem();
        }
    }

}
