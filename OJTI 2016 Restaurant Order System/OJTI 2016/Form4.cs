using System;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;
using System.Data;


namespace OJTI_2016
{
    public partial class Form4 : Form
    {
        string constr= @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=F:\Projects\OJTI 2016\OJTI 2016\GOOD_FOOD.mdf;Integrated Security=True;Connect Timeout=30";
        int[] cant = new int[100];
        int k = 0;
        public Form4()
        {
            InitializeComponent();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox1.Text==""|| textBox2.Text == ""|| textBox3.Text == "")
            {
                MessageBox.Show("Introduceti datele in casutele alaturate!!");
            }
            else
            {
                SqlConnection con = new SqlConnection(constr);
                SqlDataAdapter adapter = new SqlDataAdapter();
                con.Open();
                int S = 0;
                S += Convert.ToInt32(textBox1.Text);
                S += Convert.ToInt32(textBox2.Text);
                S += Convert.ToInt32(textBox3.Text);
                if (S < 250) textBox4.Text = "1800";
                if (S >= 250 && S <= 275) textBox4.Text = "2200";
                if (S > 275) textBox4.Text = "2500";

                textBox5.Text = textBox4.Text;
                textBox10.Text = textBox4.Text;
                
                adapter.UpdateCommand= new SqlCommand("Update Clienti set kcal_zilnice='" + textBox4.Text + "' where email='" + Form3.client.email.ToString() + "'",con);
                adapter.UpdateCommand.ExecuteNonQuery();
                adapter.UpdateCommand.Dispose();
                con.Close();
            }
        }
        void graph()
        {
            
            int i = 0;
            while (i < dataGridView2.RowCount)
            {
                this.chart1.Series[0].Points.AddXY(dataGridView2[0,i].Value, dataGridView2[1,i].Value.ToString());
                i++;
            }
           
        }


        private void Form4_Load(object sender, EventArgs e)
        {

            // TODO: This line of code loads data into the 'gOOD_FOODDataSet.Meniu' table. You can move, or remove it, as needed.

            DataTable table = new DataTable();
            table.Columns.Add("id_produs", typeof(int));
            table.Columns.Add("denumire_produs", typeof(string));
            table.Columns.Add("descriere", typeof(string));
            table.Columns.Add("pret", typeof(int));
            table.Columns.Add("kcal", typeof(int));
            table.Columns.Add("felul", typeof(int));
            table.Columns.Add("Cantitate", typeof(int));
            DataGridViewButtonColumn btn1 = new DataGridViewButtonColumn();
            dataGridView2.Columns.Add(btn1);
            btn1.HeaderText = "Elimina";
            btn1.Text = "Elimina";
            btn1.UseColumnTextForButtonValue = true;
            dataGridView2.AutoResizeColumns();

            SqlConnection con = new SqlConnection(constr);
            con.Open();
            SqlDataReader dr;
            SqlCommand cmd= new SqlCommand("Select id_produs,denumire_produs,descriere,pret,kcal,felul from Meniu",con);
            
            dr = cmd.ExecuteReader();
            while(dr.Read())
            {
                table.Rows.Add(dr.GetValue(0), dr.GetValue(1), dr.GetValue(2), dr.GetValue(3), dr.GetValue(4), dr.GetValue(5), 1);
            }
            dataGridView1.DataSource = table;
            DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
            
            dataGridView1.Columns.Add(btn);
            btn.HeaderText = "Adauga";
            btn.Text = "Adauga";
            btn.UseColumnTextForButtonValue = true;
            con.Close();
            cmd.Dispose();
            

        }
            
        private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.ColumnIndex == 0)
            {
                if (cant[e.RowIndex] == 0)
                {
                    this.dataGridView2.Rows.Add(dataGridView1["denumire_produs", e.RowIndex].Value.ToString(), dataGridView1["kcal", e.RowIndex].Value, dataGridView1["pret", e.RowIndex].Value, dataGridView1["Cantitate", e.RowIndex].Value);
                    k++;
                    cant[e.RowIndex] =k;
                    

                }
                else
                {
                    this.dataGridView2[3, cant[e.RowIndex]-1].Value = (int)dataGridView2[3, cant[e.RowIndex] - 1].Value +(int)dataGridView1[3, cant[e.RowIndex] - 1].Value;
                    this.dataGridView2[1, cant[e.RowIndex] - 1].Value= Convert.ToInt32(dataGridView2[1, cant[e.RowIndex] - 1].Value) + Convert.ToInt32(dataGridView2[1, cant[e.RowIndex] - 1].Value);
                    this.dataGridView2[2, cant[e.RowIndex] - 1].Value = Convert.ToInt32(dataGridView2[2, cant[e.RowIndex] - 1].Value) + Convert.ToInt32(dataGridView2[2, cant[e.RowIndex] - 1].Value);
                }
                int sumKcal;
                    int sK;
                if (textBox6.Text != "")
                    sK = Convert.ToInt32(textBox6.Text);
                else sK = 0;
                int sK1 = Convert.ToInt32(dataGridView1["kcal", e.RowIndex].Value);
                int sK2 = Convert.ToInt32(dataGridView1["Cantitate",e.RowIndex].Value);
                sumKcal = sK + sK1 * sK2;
                int sumPret;
                    int sP;
                if (textBox7.Text != "")
                    sP = Convert.ToInt32(textBox7.Text);
                else sP = 0;
                int sP1 = Convert.ToInt32(dataGridView1["pret", e.RowIndex].Value);
                int sP2 =Convert.ToInt32(dataGridView1["Cantitate", e.RowIndex].Value);
                sumPret = sP + sP1 * sP2;
                textBox6.Text = sumKcal.ToString();
                textBox9.Text = sumKcal.ToString();
                textBox7.Text = sumPret.ToString();
                textBox8.Text = sumPret.ToString();
            }
            
        }

        private void Form4_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.ColumnIndex==4)
            {
                
                
                int sumKcal;
                int sK;
                if (textBox6.Text != "")
                    sK = Convert.ToInt32(textBox6.Text);
                else sK = 0;
                int sK1 = Convert.ToInt32(dataGridView2[1, e.RowIndex].Value);
                int sK2 = Convert.ToInt32(dataGridView2[3, e.RowIndex].Value);
                sumKcal = sK - sK1 * sK2;
                int sumPret;
                int sP;
                if (textBox7.Text != "")
                    sP = Convert.ToInt32(textBox7.Text);
                else sP = 0;
                int sP1 = Convert.ToInt32(dataGridView2[2, e.RowIndex].Value);
                int sP2 = Convert.ToInt32(dataGridView2[3, e.RowIndex].Value);
                sumPret = sP - sP1 * sP2;
                textBox6.Text = sumKcal.ToString();
                textBox9.Text = sumKcal.ToString();
                textBox7.Text = sumPret.ToString();
                textBox8.Text = sumPret.ToString();
                dataGridView2.Rows.RemoveAt(e.RowIndex);

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Comanda a fost trimisa!!");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Comanda a fost trimisa!!");
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(tabControl1.SelectedIndex==3)
            {
                graph();
            }
        }
    }
}
