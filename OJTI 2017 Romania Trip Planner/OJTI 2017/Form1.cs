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
using System.Drawing.Imaging;

namespace OJTI_2017
{
    public partial class Form1 : Form
    {
        public static FolderBrowserDialog fbd = new FolderBrowserDialog();
        public static string constr = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=F:\Projects\OJTI 2017\OJTI 2017\Turism.mdf;Integrated Security=True;Connect Timeout=30";
        public Form1()
        {
            InitializeComponent();
            stergere();
            Initializare();
            
            fbd.ShowDialog();
            fillCombo();
           

        }
       
        private void button1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Add(comboBox2.GetItemText(comboBox2.SelectedItem));
        }
        void fillCombo()
        {
            SqlConnection con = new SqlConnection(constr);
            con.Open();
            SqlCommand cmd = new SqlCommand("Select Nume from Localitati", con);

            SqlDataReader dR = cmd.ExecuteReader();
                while(dR.Read())
            {
                comboBox1.Items.Add(dR.GetValue(0));

            }
            cmd.Dispose();
            con.Close();


        }
        
        private void stergere()
        {
            
            SqlConnection con = new SqlConnection(constr);
            con.Open();
            SqlCommand cmd = new SqlCommand("Delete from Imagini", con);
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            cmd = new SqlCommand("Delete from Planificari", con);
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            cmd = new SqlCommand("Delete from Localitati", con);
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            cmd = new SqlCommand("TRUNCATE TABLE Localitati", con);
            cmd.ExecuteNonQuery();

            cmd.Dispose();
            con.Close();
        }
        private static void Initializare()
        {
            SqlConnection con = new SqlConnection(constr);
            SqlCommand cmd;
            StreamReader sr = new StreamReader(Application.StartupPath + @"\..\..\planificari.txt");
            string sir;
            char[] split= {'*'};
            con.Open();
            DateTime dt1, dt2;
            while((sir=sr.ReadLine())!=null)
            {
                string[] siruri = sir.Split(split);
                cmd = new SqlCommand("insert into Localitati(nume) values(@localitate)", con);
                cmd.Parameters.AddWithValue("localitate", siruri[0].Trim());
                cmd.ExecuteNonQuery();
                cmd = new SqlCommand("select IdLocalitate from Localitati where nume=@nume", con);
                cmd.Parameters.AddWithValue("nume", siruri[0].Trim());
                int idlocalitate = Convert.ToInt32(cmd.ExecuteScalar());
                int nrzile;
                switch(siruri[1].Trim())
                {
                    case "ocazional":
                        string d1 = siruri[2], d2 = siruri[3];
                        dt1 = Convert.ToDateTime(d1.Trim(), System.Globalization.CultureInfo.GetCultureInfo("fr-FR"));
                        dt2 = Convert.ToDateTime(d2.Trim(), System.Globalization.CultureInfo.GetCultureInfo("fr-FR"));
                        int i = 4;
                        while(i<siruri.Length)
                        {
                            cmd = new SqlCommand(@"insert into imagini(IdLocalitate,CaleFisier) values(@idlocalitate,@calefisier)", con);
                            cmd.Parameters.AddWithValue("idlocalitate", idlocalitate);
                            cmd.Parameters.AddWithValue("calefisier", siruri[i].Trim());
                            cmd.ExecuteNonQuery();
                            i++;
                        }
                        cmd = new SqlCommand(@"insert into planificari(IdLocalitate,Frecventa,DataStart,DataStop) values(@idlocalitate,@frecventa,@datastart,@datastop)", con);
                        cmd.Parameters.AddWithValue("idlocalitate", idlocalitate);
                        cmd.Parameters.AddWithValue("frecventa", "ocazional");
                        cmd.Parameters.AddWithValue("datastart", dt1);
                        cmd.Parameters.AddWithValue("datastop", dt2);
                        cmd.ExecuteNonQuery();
                        break;

                    case "anual":
                        nrzile = int.Parse(siruri[2]);
                        i = 3;
                        while (i < siruri.Length)
                        {
                            cmd = new SqlCommand(@"insert into imagini(IdLocalitate,CaleFisier) values(@idlocalitate,@calefisier)", con);
                            cmd.Parameters.AddWithValue("idlocalitate", idlocalitate);
                            cmd.Parameters.AddWithValue("calefisier", siruri[i].Trim());
                            cmd.ExecuteNonQuery();
                            i++;
                        }
                        cmd = new SqlCommand(@"insert into planificari(IdLocalitate,Frecventa,Ziua) values(@idlocalitate,@frecventa,@ziua)", con);
                        cmd.Parameters.AddWithValue("idlocalitate", idlocalitate);
                        cmd.Parameters.AddWithValue("frecventa", "anual");
                        cmd.Parameters.AddWithValue("ziua", nrzile);
                       
                        cmd.ExecuteNonQuery();
                        break;


                    case "lunar":
                        nrzile = int.Parse(siruri[2]);
                        i = 3;
                        while (i < siruri.Length)
                        {
                            cmd = new SqlCommand(@"insert into imagini(IdLocalitate,CaleFisier) values(@idlocalitate,@calefisier)", con);
                            cmd.Parameters.AddWithValue("idlocalitate", idlocalitate);
                            cmd.Parameters.AddWithValue("calefisier", siruri[i].Trim());
                            cmd.ExecuteNonQuery();
                            i++;
                        }
                        cmd = new SqlCommand(@"insert into planificari(IdLocalitate,Frecventa,Ziua) values(@idlocalitate,@frecventa,@ziua)", con);
                        cmd.Parameters.AddWithValue("idlocalitate", idlocalitate);
                        cmd.Parameters.AddWithValue("frecventa", "lunar");
                        cmd.Parameters.AddWithValue("ziua", nrzile);
                        
                        cmd.ExecuteNonQuery();
                        break;

                }
                 
            }
            
           
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox2.Items.Clear();
            comboBox2.Text = "";
            SqlConnection con = new SqlConnection(constr);
            con.Open();
            SqlCommand cmd;
            
            cmd = new SqlCommand("Select IdLocalitate,CaleFisier from Imagini", con);
            SqlDataReader dR = cmd.ExecuteReader();
            while(dR.Read())
            {
                if(Convert.ToInt32(dR.GetValue(0))==comboBox1.SelectedIndex+1) comboBox2.Items.Add(dR.GetValue(1));
            }
            dR.Close();
            cmd.Dispose();
            con.Close();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //MessageBox.Show(fbd.SelectedPath + listBox1.GetItemText(listBox1.SelectedItem));
            pictureBox1.Image=Image.FromFile(fbd.SelectedPath + @"\" + listBox1.GetItemText(listBox1.SelectedItem));
            

        }

        private void button2_Click(object sender, EventArgs e)
        {
            SaveFileDialog w = new SaveFileDialog();
            if (textBox1.Text != "")
            {
                w.FileName = textBox1.Text;
                w.FileName+=".png";
                w.Filter = ".PNG|";
                if(w.ShowDialog()==DialogResult.OK)
                {
                    try
                    {
                        using (Bitmap bitmap = new Bitmap(pictureBox1.Image))
                        {
                            bitmap.Save(w.FileName,ImageFormat.Png);
                        }
                        MessageBox.Show("Picture Saved Successfully.");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("An Error has occured: \n" + ex.Message);
                    }
                }
            }
            else
                MessageBox.Show("Alege un titlu pentru Poster");
           
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            pictureBox1.Image = Image.FromFile(fbd.SelectedPath + @"\" + comboBox2.GetItemText(comboBox2.SelectedItem));
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form2 q = new Form2();
            q.Show();
           
                
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
