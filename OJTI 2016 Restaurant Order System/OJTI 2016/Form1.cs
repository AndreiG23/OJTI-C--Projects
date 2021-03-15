using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;

namespace OJTI_2016
{
    public partial class Form1 : Form
    {
        string constr= @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=F:\Projects\OJTI 2016\OJTI 2016\GOOD_FOOD.mdf;Integrated Security=True;Connect Timeout=30";
        public Form1()
        {
            InitializeComponent();
            stergere();
            initializare();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form2 w = new Form2();
            w.Show();
            
            
            
        }
        void stergere()
        {
            SqlConnection con = new SqlConnection(constr);
            con.Open();
            SqlCommand cmd = new SqlCommand("Delete from Meniu",con);
            cmd.ExecuteNonQuery();
            cmd = new SqlCommand("TRUNCATE TABLE Meniu", con);
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            con.Close();
        }
        void initializare()
        {
            SqlConnection con = new SqlConnection(constr);
            SqlCommand cmd;
            StreamReader sr = new StreamReader(Application.StartupPath + @"\..\..\meniu.txt");
            string sir;
            char[] split={';'};
            con.Open();
            while((sir=sr.ReadLine())!=null)
            {
                
                string[] siruri = sir.Split(split);
               
                cmd = new SqlCommand("Insert into Meniu(id_produs,denumire_produs,descriere,pret,kcal,felul) values('" + siruri[0] + "','" +siruri[1].Trim()+ "', '"+siruri[2]+"','" + Convert.ToInt32(siruri[3]) + "','"+ Convert.ToInt32(siruri[4])+"', '"+ Convert.ToInt32(siruri[5])+"')", con);
               
               cmd.ExecuteNonQuery();

            }
            
            con.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form3 w = new Form3();
            w.ShowDialog();
        }
    }
}
