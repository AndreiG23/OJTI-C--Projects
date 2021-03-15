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

namespace OJTI_2018
{
    public partial class Form2 : Form
    {
        string constr = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ghiur\OneDrive\Documente\eLearning1918.mdf;Integrated Security=True;Connect Timeout=30";
        string raspuns;
        int tip,punctaj=1,nr=1;
        public Form2()
        {
            InitializeComponent();
            panel1.Hide();
            panel2.Hide();
            panel3.Hide();
            panel4.Hide();

            button2.Enabled = false;
            fill();
            graph();
            
        }
        void fill()
        {
            
            SqlConnection con = new SqlConnection(constr);
            con.Open();
            SqlCommand cmd = new SqlCommand("Select NotaEvaluare,DataEvaluare from Evaluari where IdElev='" + Form1.ID + "'", con);
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
           
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;




        }
        void random()
        {
            

            SqlConnection con = new SqlConnection(constr);
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT TOP 1 TipItem,EnuntItem,Raspuns1Item,Raspuns2Item,Raspuns3Item,Raspuns4Item,RaspunsCorectItem FROM Itemi ORDER BY NEWID()", con);
            SqlDataReader dr = cmd.ExecuteReader();
            
            while (dr.Read())
            {
                
                tip=Convert.ToInt32( dr.GetValue(0));
                richTextBox1.Text = "" + dr.GetValue(1);
                radioButton1.Text = "" + dr.GetValue(2);
                radioButton2.Text = "" + dr.GetValue(3);
                radioButton3.Text = "" + dr.GetValue(4);
                radioButton4.Text = "" + dr.GetValue(5);
                checkBox1.Text = "" + dr.GetValue(2);
                checkBox2.Text = "" + dr.GetValue(3);
                checkBox3.Text = "" + dr.GetValue(4);
                checkBox4.Text = "" + dr.GetValue(5);
                raspuns = dr.GetValue(6).ToString();


            }
            

            if (tip == 1)
            {
               
                panel1.Show();
            }
            if (tip == 2)
            {
                
                panel2.Show();

            }
            if (tip == 3)
            {
               
                panel3.Show();

            }
            if (tip == 4)
            {
                
                panel4.Show();
                
            }
            con.Close();

        }
        private void iesireToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
        void graph()
        {
            SqlConnection con = new SqlConnection(constr);
            con.Open();
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("select NotaEvaluare,DataEvaluare,IdElev,IdEvaluare from Evaluari where IdElev='" + Form1.ID + "'", con);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                this.chart1.Series[0].Points.AddXY(dr.GetInt32(3), dr.GetInt32(0));
            }
            con.Close();
            cmd.Dispose();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            panel1.Hide();
            panel2.Hide();
            panel3.Hide();
            panel4.Hide();
            nr++;
            if (nr < 10)
            {
                label2.Text = "Item nr." + nr.ToString();
                if (tip == 1)
                {
                    if (textBox1.Text.ToLower() == raspuns.ToLower())
                    {
                        punctaj++;
                        label1.Text = "Punctaj:" + punctaj.ToString();
                    }
                }
                else if (tip == 2)
                {
                    if (radioButton1.Checked == true && raspuns == "1")
                    {
                        punctaj++;
                        label1.Text = "Punctaj:" + punctaj.ToString();
                    }
                    if (radioButton2.Checked == true && raspuns == "2")
                    {
                        punctaj++;
                        label1.Text = "Punctaj:" + punctaj.ToString();
                    }
                    if (radioButton3.Checked == true && raspuns == "3")
                    {
                        punctaj++;
                        label1.Text = "Punctaj:" + punctaj.ToString();
                    }
                    if (radioButton4.Checked == true && raspuns == "4")
                    {
                        punctaj++;
                        label1.Text = "Punctaj:" + punctaj.ToString();
                    }
                }
                else if (tip == 3)
                {
                    int r = 0;
                    if (checkBox1.Checked == true)
                        r = r * 10 + 1;
                    if (checkBox2.Checked == true)
                        r = r * 10 + 2;
                    if (checkBox3.Checked == true)
                        r = r * 10 + 3;
                    if (checkBox4.Checked == true)
                        r = r * 10 + 4;
                    if (r.ToString() == raspuns)
                    {
                        punctaj++;
                        label1.Text = "Punctaj:" + punctaj.ToString();
                    }
                }
                else if (tip == 4)
                {
                    if (radioButton5.Checked && raspuns == "1")
                    {
                        punctaj++;
                        label1.Text = "Punctaj:" + punctaj.ToString();
                    }
                    if (radioButton6.Checked && raspuns == "0")
                    {
                        punctaj++;
                        label1.Text = "Punctaj:" + punctaj.ToString();
                    }

                }
                textBox1.Text = "";
                radioButton1.Checked = false;
                radioButton2.Checked = false;
                radioButton3.Checked = false;
                radioButton4.Checked = false;
                radioButton5.Checked = false;
                radioButton6.Checked = false;
                checkBox1.Checked = false;
                checkBox2.Checked = false;
                checkBox3.Checked = false;
                checkBox4.Checked = false;

                random();
            }
            else
            {
                MessageBox.Show("Ai luat nota " + punctaj.ToString());
                button2.Enabled = false;
                button1.Enabled = true;
                panel1.Hide();
                panel2.Hide();
                panel3.Hide();
                panel4.Hide();
                richTextBox1.Text = "";
                
                

                SqlConnection con = new SqlConnection(constr);
                con.Open();
                
                SqlCommand cmd = new SqlCommand("Insert into Evaluari (IdElev,DataEvaluare,NotaEvaluare) values('" + Form1.ID + "','" + DateTime.Now + "','" + punctaj + "')", con);
                cmd.ExecuteNonQuery();

                punctaj = 1;
                nr = 1;
                label2.Text = "Punctaj:1";
                fill();
                graph();
            }
        }

        private void carnetDeNoteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(1);

        }

        private void graficNoteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(2);
        }

        private void testeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(0);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            random();
            button1.Enabled = false;
            button2.Enabled = true;
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
