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
    public partial class Patient : Form
    {
        SqlConnection Con = new SqlConnection(@"Data Source=IN-WIN-AGAFOOR\SQLEXPRESS;Initial Catalog=HospitalDatabase;Integrated Security=True");
        public Patient()
        {
            InitializeComponent();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            Home h = new Home();
            h.Show();
            this.Hide();
        }
        void populate()
        {
            Con.Open();
            string query = "select * from PatientTable";
            SqlDataAdapter da = new SqlDataAdapter(query, Con);
            SqlCommandBuilder build = new SqlCommandBuilder(da);
            var ds = new DataSet();
            da.Fill(ds);
            DoctorGV.DataSource = ds.Tables[0];
            Con.Close();

        }
        void ClearData()
        {
            patId.Text = "";
            patName.Text = "";
            patAddress.Text = "";
            patPhone.Text = "";
            cmbGender.Text = "";
            cmbBlood.Text = "";
            patAge.Text = "";
            patMajor.Text = "";
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {

            if (patId.Text == "" || patName.Text == "" || patAddress.Text == "" || patPhone.Text == "" ||patAge.Text=="" || patMajor.Text=="" )
                MessageBox.Show("No Empty Field Accepted");
            else
            {
                Con.Open();
                string query = "insert into PatientTable values('" + patId.Text + "','" + patName.Text + "','" + patAddress.Text + "','" + patPhone.Text + "'," + patAge.Text + ",'" + cmbGender.SelectedItem.ToString() + "','" + cmbBlood.SelectedItem.ToString() + "','" + patMajor.Text + "')";
                SqlCommand cmd = new SqlCommand(query, Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Patient Details Successfully Added");
                Con.Close();
                populate();
                ClearData();
            }
        }

        private void Patient_Load(object sender, EventArgs e)
        {
            populate();
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            Con.Open();
            string query = "delete from PatientTable where PatientId='" + patId.Text + "'";
            SqlCommand cmd = new SqlCommand(query, Con);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Patient Details Successfully Deleted");
            Con.Close();
            populate();
            ClearData();
        }

        private void patientGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           // patId.Text = DoctorGV.SelectedRows[0].Cells[0].Value.ToString();
           // patName.Text = DoctorGV.SelectedRows[0].Cells[1].Value.ToString();
           // patAddress.Text = DoctorGV.SelectedRows[0].Cells[2].Value.ToString();
            patPhone.Text = DoctorGV.SelectedRows[0].Cells[3].Value.ToString();
            patAge.Text = DoctorGV.SelectedRows[0].Cells[4].Value.ToString();
            //cmbGender.Text= DoctorGV.SelectedRows[0].Cells[5].Value.ToString();
            //cmbBlood.Text = DoctorGV.SelectedRows[0].Cells[6].Value.ToString();
            patMajor.Text = DoctorGV.SelectedRows[0].Cells[7].Value.ToString();


        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            Con.Open();
            string query = "update PatientTable set PatientName='" + patName.Text + "',PatientAddress='" + patAddress.Text + "',PatientPhone='" + patPhone.Text + "',PatientAge=" + patAge.Text + ",PatientDisease='" + patMajor.Text + "',PatientGender='" + cmbGender.SelectedItem.ToString() + "', PatientBlood='" + cmbBlood.SelectedItem.ToString() + "'where PatientId='" + patId.Text + "'";
            SqlCommand cmd = new SqlCommand(query, Con);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Patient Details Updated Successfully");
            Con.Close();
            populate();
            ClearData();
        }

        private void DoctorGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            patId.Text = DoctorGV.SelectedRows[0].Cells[0].Value.ToString();
            patName.Text = DoctorGV.SelectedRows[0].Cells[1].Value.ToString();
            patAddress.Text = DoctorGV.SelectedRows[0].Cells[2].Value.ToString();
            patPhone.Text = DoctorGV.SelectedRows[0].Cells[3].Value.ToString();
            patAge.Text = DoctorGV.SelectedRows[0].Cells[4].Value.ToString();
            cmbGender.Text= DoctorGV.SelectedRows[0].Cells[5].Value.ToString();
            cmbBlood.Text = DoctorGV.SelectedRows[0].Cells[6].Value.ToString();
            patMajor.Text = DoctorGV.SelectedRows[0].Cells[7].Value.ToString();
        }

        private void label12_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
