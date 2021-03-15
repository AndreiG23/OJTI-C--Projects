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
    public partial class Form3 : Form
    {
        string constr = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ghiur\OneDrive\Documente\DBTimpSpatiu.mdf;Integrated Security=True;Connect Timeout=30";
        string[] porturi = new string[15];
        Point[] v = new Point[15];
        int i = 1;
        bool ok = false;
        public Form3()
        {
            InitializeComponent();
            
            porturi[1] = "Constanta";
            porturi[2] = "Varna";
            porturi[3] = "Burgas";
            porturi[4] = "Instambul";
            porturi[5] = "Kozlu";
            porturi[6] = "Samsun";
            porturi[7] = "Batumi";
            porturi[8] = "Sokhumi";
            porturi[9] = "Soci";
            porturi[10] = "Anapa";
            porturi[11] = "Yalta";
            porturi[12] = "Sevastopol";
            porturi[13] = "Odessa";

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (ok)
            {
                MouseEventArgs m = (MouseEventArgs)e;
                Point coord = m.Location;
                label1.Text = coord.X.ToString();
                label2.Text = coord.Y.ToString();
                v[i].X = coord.X;
                v[i].Y = coord.Y;
                i++;
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            ok = true;
        }
        void stergere()
        {
            SqlConnection con = new SqlConnection(constr);
            con.Open();
            SqlCommand cmd = new SqlCommand("Delete from Porturi", con);
            cmd.ExecuteNonQuery();
             cmd = new SqlCommand("TRUNCATE Table Porturi", con);
            cmd.ExecuteNonQuery();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            stergere();
            SqlConnection con = new SqlConnection(constr);
            con.Open();
            SqlCommand cmd;
            int k = 1;
            while(k<=i)
            {
                cmd = new SqlCommand("Insert into Porturi(Nume_Port,Pozitie_X,Pozitie_Y)values('" + porturi[k] + "','" + v[k].X + "','" + v[k].Y + "')", con);
                cmd.ExecuteNonQuery();
                k++;
            }
            if (k == i+1)
                MessageBox.Show("Coordonatele au fost salvate!");
        }
    }
}
