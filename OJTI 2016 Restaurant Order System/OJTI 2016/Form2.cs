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
    public partial class Form2 : Form
    {
        public static class client
        {
            public static string email { get; set; }
        }
        public Form2()
        {
            InitializeComponent();
        }
        string constr = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ghiur\OneDrive\Documente\GOOD_FOOD.mdf;Integrated Security=True;Connect Timeout=30";
        private void button1_Click(object sender, EventArgs e)
        {
            
            
            if(!textBox6.Text.Contains("@") || !textBox6.Text.Contains(".com"))
            {
                MessageBox.Show("Adresa email invalida!!");
                
            }
            else if(textBox1.Text ==""|| textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" || textBox4.Text == "" || textBox5.Text == "" || textBox6.Text == "")
            {
                MessageBox.Show("Trebuie sa completezi toate casutele!!");
            }
            else if(textBox4.Text != textBox5.Text)
            {
                MessageBox.Show("Parolele introduse nu se potrivesc!!");
                
            }
            else
            {
                SqlConnection con = new SqlConnection(constr);
                con.Open();
                
                SqlCommand cmd = new SqlCommand("Insert into Clienti (parola,nume,prenume,adresa,email) values('" + textBox4.Text + "','" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + textBox6.Text + "')",con);
                //adap.InsertCommand = new SqlCommand("Insert into Clienti (parola,nume,prenume,adresa,email) values('" + textBox4.Text + "','" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + textBox6.Text + "')", con);
                cmd.ExecuteNonQuery();
                this.Hide();
                Form3 w = new Form3();
                w.Show();
                cmd.Dispose();
               
                con.Close();
                client.email = textBox6.Text;
            }

        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
         
        }
    }
}
