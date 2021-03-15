using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace OJTI_2015
{
    public partial class Form1 : Form
    {
        public static string pass;
        string constr = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ghiur\OneDrive\Documente\DBTimpSpatiu.mdf;Integrated Security=True;Connect Timeout=30";
        public Form1()
        {
            InitializeComponent();
            comboBox1.Items.Add("Administrator");
            comboBox1.Items.Add("Turist");
            comboBox1.SelectedIndex = 0;
        }
       
    
        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0 && textBox1.Text == "agentie2015")
            {
                pass = "agentie2015";
                Form2 w = new Form2();
                
                w.Show();
            }
            else if (comboBox1.SelectedIndex ==1 && textBox1.Text == "oti2015")
            {
                pass = "oti2015";
                Form2 w = new Form2();
               
                w.Show();
            }
            else
            {
                MessageBox.Show("Parola Gresita!!");
                textBox1.Text = "";
            }

        }
    }
}
