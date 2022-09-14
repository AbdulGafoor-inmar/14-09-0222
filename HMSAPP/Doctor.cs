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
    public partial class Doctor : Form
    {
        SqlConnection Con = new SqlConnection(@"Data Source=IN-WIN-AGAFOOR\SQLEXPRESS;Initial Catalog=HospitalDatabase;Integrated Security=True");
        public Doctor()
        {
            InitializeComponent();
        }
        void populate()
        {
            Con.Open();
            string query = "select * from DoctorTable";
            SqlDataAdapter da = new SqlDataAdapter(query,Con);
            SqlCommandBuilder build = new SqlCommandBuilder(da);
            var ds = new DataSet();
            da.Fill(ds);
            DoctorGV.DataSource = ds.Tables[0];
            Con.Close();
        }
        void ClearData()
        {
            DocId.Text = "";
            DocName.Text = "";
            DocExp.Text = "";
            DocPass.Text = "";
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            Home h = new Home();
            h.Show();
            this.Hide();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {

            if (DocId.Text == "" || DocName.Text == "" || DocExp.Text == "" || DocPass.Text == "")
                MessageBox.Show("No Empty Field Accepted");
            else
            {
                Con.Open();
                string query = "insert into DoctorTable values('" + DocId.Text + "','" + DocName.Text + "','" + DocExp.Text + "','" + DocPass.Text + "')";
                SqlCommand cmd = new SqlCommand(query, Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Doctor Details Successfully Added");
                Con.Close();
                populate();
                ClearData();
            }
        }

        private void Doctor_Load(object sender, EventArgs e)
        {
            populate();
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            Con.Open();
            string query = "delete from DoctorTable where DoctorId='" + DocId.Text + "'";
            SqlCommand cmd = new SqlCommand(query,Con);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Doctor Details Successfully Deleted");
            Con.Close();
            populate();
            ClearData();
        }

        private void DoctorGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DocId.Text = DoctorGV.SelectedRows[0].Cells[0].Value.ToString();
            DocName.Text = DoctorGV.SelectedRows[0].Cells[1].Value.ToString();
            DocExp.Text = DoctorGV.SelectedRows[0].Cells[2].Value.ToString();
            DocPass.Text = DoctorGV.SelectedRows[0].Cells[3].Value.ToString();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            Con.Open();
            string query = "update DoctorTable set DoctorName='" + DocName.Text + "',DoctorExp='" + DocExp.Text + "',DoctorPass='" + DocPass.Text + "' where DoctorId='"+DocId.Text +"'";
            SqlCommand cmd = new SqlCommand(query,Con);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Doctor Details Updated Successfully");
            Con.Close();
            populate();
            ClearData();
        }

        private void label12_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
