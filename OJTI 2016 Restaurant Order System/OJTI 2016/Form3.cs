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

namespace OJTI_2016
{
   
    public partial class Form3 : Form
    {
        public static class client
        {
            public static string email { get; set; }
        }
        public Form3()
        {
            InitializeComponent();
        }
        string constr = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ghiur\OneDrive\Documente\GOOD_FOOD.mdf;Integrated Security=True;Connect Timeout=30";

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(constr);
            con.Open();
            SqlCommand cmd = new SqlCommand("Select parola,email from Clienti", con);
            SqlDataReader rd;
            rd = cmd.ExecuteReader();
            while(rd.Read())
            {
                if(textBox2.Text==rd.GetValue(0).ToString()&&textBox1.Text==rd.GetValue(1).ToString())
                {
                    client.email = textBox1.Text;
                    Form4 w = new Form4();
                    this.Hide();
                    w.Show();
                }
            }
            cmd.Dispose();
            con.Close();
        }

        private void Form3_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
