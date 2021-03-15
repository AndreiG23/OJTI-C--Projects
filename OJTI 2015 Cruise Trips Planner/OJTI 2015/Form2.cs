using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OJTI_2015
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            if(Form1.pass=="oti2015")
            {
                button1.Text = "Turisti";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(Form1.pass == "oti2015")
            {
                Form4 w = new Form4();
                this.Close();
                w.Show();
            }
            else
            {
                Form3 w = new Form3();
                this.Close();
                w.Show();
            }
        }
    }
}
