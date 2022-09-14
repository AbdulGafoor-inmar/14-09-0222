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

namespace HMSAPP
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=IN-WIN-AGAFOOR\SQLEXPRESS;Initial Catalog=HospitalDatabase;Integrated Security=True");
        private void button1_Click(object sender, EventArgs e)
        {
            if (DocName.Text == "" || DocPass.Text == "")
                MessageBox.Show("Enter a Username and Password");
            else
            {
                SqlConnection Con = new SqlConnection(@"Data Source=IN-WIN-AGAFOOR\SQLEXPRESS;Initial Catalog=HospitalDatabase;Integrated Security=True");
                Con.Open();
                SqlDataAdapter sad = new SqlDataAdapter("select Count(*) from DoctorTable where DoctorName='" + DocName.Text + "' and DoctorPass='" + DocPass.Text+"'",Con);
                DataTable dt = new DataTable();
                sad.Fill(dt);
                if(dt.Rows[0][0].ToString()=="1")
                {
                    Home H = new Home();
                    H.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Incorrect Username or Password");
                }
                Con.Close();
            }
        }

        private void label12_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            DocName.Text = "";
            DocPass.Text = "";
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
