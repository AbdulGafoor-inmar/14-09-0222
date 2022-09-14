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
    public partial class Diagnosis : Form
    {
        
        public Diagnosis()
        {
            InitializeComponent();
        }
        public void populateCombo()
        {
            SqlConnection Con = new SqlConnection(@"Data Source=IN-WIN-AGAFOOR\SQLEXPRESS;Initial Catalog=HospitalDatabase;Integrated Security=True");
            Con.Open();
            string sql = "select * from PatientTable";
            SqlCommand cmd = new SqlCommand(sql, Con);
            SqlDataReader rdr;
            try
            {
                
                DataTable dt = new DataTable();
                dt.Columns.Add("PatientId",typeof(string));
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                diapatId.ValueMember = "PatientId";
                diapatId.DataSource = dt;
                Con.Close();
            }
            catch(Exception e)
            {

            }
        }
        string patname;
        public void fetchPatient()
        {
            SqlConnection Con = new SqlConnection(@"Data Source=IN-WIN-AGAFOOR\SQLEXPRESS;Initial Catalog=HospitalDatabase;Integrated Security=True");
            Con.Open();
            string mysql = "select * from PatientTable where PatientId=" +  diapatId.SelectedValue.ToString() + "";
            SqlCommand cmd = new SqlCommand(mysql,Con);
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            foreach(DataRow dr in dt.Rows)
            {
                patname = dr["PatientName"].ToString();
                diaPatname.Text = patname;
            }
            Con.Close();
        }
        private void Diagnosis_Load(object sender, EventArgs e)
        {
            populateCombo();
            populate();
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            if (diapatId.Text == "")
            {
                MessageBox.Show("Enter the Diagnosis Id");
            }
            else
            {
                SqlConnection Con = new SqlConnection(@"Data Source=IN-WIN-AGAFOOR\SQLEXPRESS;Initial Catalog=HospitalDatabase;Integrated Security=True");
                Con.Open();
                string query = "delete from DiagnosisTable where DiagnosisId='" + diaId.Text + "'";
                SqlCommand cmd = new SqlCommand(query, Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Diagnosis Details Successfully Deleted");
                populate();
                ClearData();
                Con.Close();
                
            }
        }
        public void populate()
        {
            SqlConnection Con = new SqlConnection(@"Data Source=IN-WIN-AGAFOOR\SQLEXPRESS;Initial Catalog=HospitalDatabase;Integrated Security=True");
            Con.Open();
            string query = "select * from DiagnosisTable";
            SqlDataAdapter da = new SqlDataAdapter(query, Con);
            SqlCommandBuilder build = new SqlCommandBuilder(da);
            var ds = new DataSet();
            da.Fill(ds);
            DiagnosisGV.DataSource = ds.Tables[0];
            Con.Close();
        }
        void ClearData()
        {
            diaSym.Text = "";
            diaPatname.Text = "";
            diapatId.Text = "";
            diaMedicines.Text = "";
            diaDia.Text = "";
            diaId.Text = "";
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (diapatId.Text == "" || diaPatname.Text == "" || diaMedicines.Text == "" || diaDia.Text == "" || diaSym.Text == "" )
                MessageBox.Show("No Empty Field Accepted");
            else
            {
                SqlConnection Con = new SqlConnection(@"Data Source=IN-WIN-AGAFOOR\SQLEXPRESS;Initial Catalog=HospitalDatabase;Integrated Security=True");
                Con.Open();
                string query = "insert into DiagnosisTable values('" + diaId.Text + "'," + diapatId.SelectedValue.ToString() + ",'" +diaPatname.Text + "','" + diaDia.Text + "','" + diaSym.Text + "','" + diaMedicines.Text + "')";
                SqlCommand cmd = new SqlCommand(query, Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Diagnosis Details Successfully Added");
                Con.Close();
                populate();
                ClearData();
            }
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            Home h = new Home();
            h.Show();
            this.Hide();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            SqlConnection Con = new SqlConnection(@"Data Source=IN-WIN-AGAFOOR\SQLEXPRESS;Initial Catalog=HospitalDatabase;Integrated Security=True");
            Con.Open();
            string query = "update DiagnosisTable set PatientId ='" + diapatId.SelectedValue.ToString() + "',PatientName='" + diaPatname.Text + "',Symptoms='" + diaSym.Text + "',Diagnosis='" + diaDia.Text + "',Medicines='" + diaMedicines.Text + "'where DiagnosisId='" + diaId.Text + "'";
            SqlCommand cmd = new SqlCommand(query, Con);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Diagnosis Details Updated Successfully");
            populate();
            ClearData();
            Con.Close();
           
        }

        private void diapatId_SelectedIndexChanged(object sender, EventArgs e)
        {
            fetchPatient();
        }

        private void diapatId_SelectionChangeCommitted(object sender, EventArgs e)
        {
            fetchPatient();
        }

        private void DiagnosisGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            diaId.Text = DiagnosisGV.SelectedRows[0].Cells[0].Value.ToString();
            diapatId.SelectedValue = DiagnosisGV.SelectedRows[0].Cells[1].Value.ToString();
            diaPatname.Text = DiagnosisGV.SelectedRows[0].Cells[2].Value.ToString();
            diaSym.Text = DiagnosisGV.SelectedRows[0].Cells[3].Value.ToString();
            diaDia.Text = DiagnosisGV.SelectedRows[0].Cells[4].Value.ToString();
            diaMedicines.Text = DiagnosisGV.SelectedRows[0].Cells[5].Value.ToString();
            lblpatientname.Text= DiagnosisGV.SelectedRows[0].Cells[2].Value.ToString();
            lbldiagnosis.Text= DiagnosisGV.SelectedRows[0].Cells[4].Value.ToString();
            lblsymptoms.Text= DiagnosisGV.SelectedRows[0].Cells[3].Value.ToString();
            lblmedicines.Text= DiagnosisGV.SelectedRows[0].Cells[5].Value.ToString();

        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            if(printPreviewDialog1.ShowDialog()==DialogResult.OK)
            {
                printDocument1.Print();
            }
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawString(label9.Text ,new Font("Century Gothic", 30, FontStyle.Bold), Brushes.Red, new Point(300));
            e.Graphics.DrawString( lblpatientname.Text+"\n" + lbldiagnosis.Text+"\n" + lblsymptoms.Text+"\n" + lblmedicines.Text , new Font("Century Gothic", 28, FontStyle.Regular), Brushes.Black, new Point(330,250));
            e.Graphics.DrawString( label10.Text +"\n"+ label11.Text, new Font("Century Gothic", 35, FontStyle.Regular), Brushes.Red, new Point(400,550));
        }

        private void label12_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
