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


namespace OJTI_2017
{
    public partial class Form2 : Form
    {
        bool init = false;
        int k = 0,j=0;
        int[] folosit = new int[50];
        string[] images=new string[50];
        public static string constr = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=F:\Projects\OJTI 2017\OJTI 2017\Turism.mdf;Integrated Security=True;Connect Timeout=30";
        public Form2()
        {
            InitializeComponent();

            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            fillpic();
        }
        void fillpic()
        {
            SqlConnection con = new SqlConnection(constr);
            con.Open();
            SqlCommand cmd;
            int i = 0;
            cmd = new SqlCommand("Select CaleFisier from Imagini", con);
            SqlDataReader dR = cmd.ExecuteReader();
            while (dR.Read())
            {
                images[i]=Form1.fbd.SelectedPath+ @"\" + dR.GetValue(0).ToString();
                
                i++;
            }
        }
        void fill()
        {
            SqlConnection con = new SqlConnection(constr);
            con.Open();
            SqlCommand cmd = new SqlCommand("Select Nume,Datastart from Planificari INNER JOIN Localitati on Planificari.IDLocalitate = Localitati.IDLocalitate where DataStart between '" + dateTimePicker1.Value.ToString() + "' and '" + dateTimePicker2.Value.ToString() + "'", con);
            SqlDataAdapter dA = new SqlDataAdapter(cmd);
            DataTable table = new DataTable();
            dA.Fill(table);
            dataGridView3.DataSource = table;
            dataGridView3.Columns[0].HeaderText = "Localitate";
        
            dataGridView3.Columns[1].HeaderText = "Data";
            dataGridView3.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView3.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }
        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            DataTable table = new DataTable();
            //table.Columns.Add("Nume", typeof(string));
           // table.Columns.Add("DataStart", typeof(DateTime));
            //table.Columns.Add("DataStop", typeof(DateTime));
           // table.Columns.Add("Frecventa", typeof(string));
           // table.Columns.Add("Ziua", typeof(int));
            SqlConnection con = new SqlConnection(constr);
            con.Open();
            SqlCommand cmd = new SqlCommand("Select Nume,DataStart,DataStop,Frecventa,Ziua from Planificari inner join Localitati on Localitati.IdLocalitate =Planificari.IdLocalitate ", con);
            cmd.ExecuteNonQuery();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(table);
            dataGridView2.DataSource = table;
            dataGridView1.DataSource = table;


        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(constr);
            con.Open();
            SqlCommand cmd = new SqlCommand("Select Nume,Frecventa,Datastart,Datastop,Ziua from Planificari INNER JOIN Localitati on Planificari.IDLocalitate = Localitati.IDLocalitate where DataStart between '" + dateTimePicker1.Value.ToString() + "' and '" + dateTimePicker2.Value.ToString() + "'", con);
            SqlDataAdapter dA = new SqlDataAdapter(cmd);

            

            dA.Fill(this.turismDataSet.Planificari);


            dataGridView2.DataSource = turismDataSet.Planificari;
            fill();
            progressBar1.Maximum = dataGridView3.RowCount;


        }

        private void bindingSource1_CurrentChanged(object sender, EventArgs e)
        {

        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(tabControl1.SelectedIndex==3)
            {
                timer1.Interval = 2000;
                timer1.Start();
                

            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (progressBar1.Value < progressBar1.Maximum)
            {
                progressBar1.Value++;
                for (j = 0; j <= dataGridView3.RowCount; j++)
                    if (folosit[k] != 1)
                    {
                        pictureBox1.Image = Image.FromFile(images[k]);
                        folosit[k] = 1;
                        k = 0;
                    }
                    else k++;
            }
            else timer1.Stop();
        }
    }
}
