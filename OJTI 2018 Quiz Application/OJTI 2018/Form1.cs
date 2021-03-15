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
using System.IO;

namespace OJTI_2018
{
    public partial class Form1 : Form
    {
        bool auto = true;
        string constr = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ghiur\OneDrive\Documente\eLearning1918.mdf;Integrated Security=True;Connect Timeout=30";
        public static string ID;
        string[] images=new string[5];
        public Form1()
        {
            InitializeComponent();
            stergere();
            initializare();
            timer1.Enabled = true;
            timer1.Start();
            timer1.Interval=2000;
            progressBar1.Maximum = 4;
            button2.Enabled = false;
            button3.Enabled = false;
             images[0] = (Application.StartupPath + @"\..\..\1.jpg");
             images[1] = (Application.StartupPath + @"\..\..\2.jpg");
              images[2] = (Application.StartupPath + @"\..\..\3.jpg");
              images[3] = (Application.StartupPath + @"\..\..\4.jpg");
            images[4] = (Application.StartupPath + @"\..\..\5.jpg");

            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.Image = Image.FromFile(images[0]);

        }
        void initializare()
        {
            SqlConnection con = new SqlConnection(constr);
            SqlCommand cmd;
            StreamReader sr = new StreamReader(Application.StartupPath + @"\..\..\date.txt");
            string sir;
            char[] split = { ';' };
            con.Open();
            int d = 0;
                while ((sir = sr.ReadLine()) != null)
            {
                if (sir.Contains("Utilizatori:") )
                {
                    d = 1;
                    sir = sr.ReadLine();
                }
                if (sir.Contains("Itemi:"))
                {
                    d = 2;
                    sir = sr.ReadLine();
                }
                if (sir.Contains("Evaluari:"))
                {
                    d = 3;
                    sir = sr.ReadLine();
                }
               if(d==1)
                {
                    string[] siruri = sir.Split(split);
                    
                    cmd = new SqlCommand("Insert into Utilizatori (NumePrenumeUtilizator,ParolaUtilizator,EmailUtilizator,ClasaUtilizator) values('" + siruri[0] + "','" + siruri[1] + "','" + siruri[2] + "','" + siruri[3] + "')", con);

                    cmd.ExecuteNonQuery();
                }
               else if(d==2)
                {
                    string[] siruri = sir.Split(split);
                    
                    cmd = new SqlCommand("Insert into Itemi (TipItem,EnuntItem,Raspuns1Item,Raspuns2Item,Raspuns3Item,Raspuns4Item,RaspunsCorectItem) values('" +Convert.ToInt32(siruri[0]) + "','" + siruri[1] + "','" + siruri[2] + "','" + siruri[3] + "','"+siruri[4]+"','"+siruri[5]+ "','" + siruri[6] + "')", con);

                    cmd.ExecuteNonQuery();
                }
               else if(d==3)
                {
                    string[] siruri = sir.Split(split);
                    
                    cmd = new SqlCommand("Insert into Evaluari (IdElev,DataEvaluare,NotaEvaluare) values('" +Convert.ToInt32(siruri[0]) + "','" +Convert.ToDateTime(siruri[1]) + "','" + Convert.ToInt32(siruri[2]) + "')", con);

                    cmd.ExecuteNonQuery();
                }

            }
            
            con.Close();
        }
        void stergere()
        {
            SqlConnection con = new SqlConnection(constr);
            con.Open();
            SqlCommand cmd = new SqlCommand("Delete from Utilizatori", con);
            cmd.ExecuteNonQuery();
            cmd = new SqlCommand("TRUNCATE TABLE Utilizatori", con);
            cmd.ExecuteNonQuery();
            cmd = new SqlCommand("Delete from Itemi", con);
            cmd.ExecuteNonQuery();
            cmd = new SqlCommand("TRUNCATE TABLE Itemi", con);
            cmd.ExecuteNonQuery();
            cmd = new SqlCommand("Delete from Evaluari", con);
            cmd.ExecuteNonQuery();
            cmd = new SqlCommand("TRUNCATE TABLE Evaluari", con);
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            con.Close();
        }
        

        private void button4_Click(object sender, EventArgs e)
        {
            bool ok = false;
            SqlConnection con = new SqlConnection(constr);
            con.Open();
            SqlCommand cmd = new SqlCommand("Select EmailUtilizator,ParolaUtilizator,IdUtilizator from Utilizatori",con);
            SqlDataReader rd;
            rd = cmd.ExecuteReader();
            while(rd.Read())
            {
                if (rd.GetValue(0).ToString() == textBox1.Text && rd.GetValue(1).ToString() == textBox2.Text)
                {
                    ID = rd.GetValue(2).ToString();
                    ok = true;
                    Form2 w = new Form2();
                    this.Hide();
                    w.Show();
                }
                
            }
            if (!ok)
            {
                MessageBox.Show("Eroare de Autentificare!!");
                textBox1.Text = "";
                textBox2.Text = "";

            }
            con.Close();
            cmd.Dispose();
           
        }
        bool ok = false;
        private void timer1_Tick(object sender, EventArgs e)
        {if (auto)
            {
                if (ok == true)
                {
                    progressBar1.Value = 0;
                    pictureBox1.Image = Image.FromFile(images[progressBar1.Value]);
                    ok = false;
                }
                if (progressBar1.Value != 4)
                {
                    pictureBox1.Image = Image.FromFile(images[progressBar1.Value]);

                    progressBar1.Value++;
                    

                }
                if (progressBar1.Value == 4)
                {


                    ok = true;


                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
           if(auto)
            {
                progressBar1.Value = 0;
                auto = false;
                button1.Text = "Auto";
                button2.Enabled = true;
                button3.Enabled = true;
                pictureBox1.Image = Image.FromFile(images[0]);
            }
           else
            {
                progressBar1.Value = 0;
                auto = true;
                button1.Text = "Manual";
                button2.Enabled = false;
                button3.Enabled = false;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (progressBar1.Value !=4)
            {
                button2.Enabled = true;progressBar1.Value++;
                pictureBox1.Image = Image.FromFile(images[progressBar1.Value]);
                
                
            }
            else
            {
                button3.Enabled = false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (progressBar1.Value >0)
            {
                button3.Enabled = true;progressBar1.Value--;
                pictureBox1.Image = Image.FromFile(images[progressBar1.Value]);
                
                
            }
            else
            {
                button2.Enabled = false;
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {

        }
    }
}
