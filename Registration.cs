using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Mini_Project
{
    public partial class Registration : Form
    {
        public Registration()
        {
            InitializeComponent();
        }


        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string connectionstring = "Data Source=localhost\\sqlexpress;Initial Catalog=Product;Integrated Security=True;TrustServerCertificate=True";
                SqlConnection conn = new SqlConnection(connectionstring);

                if (string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(textBox2.Text) || string.IsNullOrEmpty(textBox3.Text) || string.IsNullOrEmpty(textBox4.Text) || string.IsNullOrEmpty(textBox5.Text))
                {
                    MessageBox.Show("Please Fill all Details.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string select = "select * from Rail_user";
                SqlDataAdapter da = new SqlDataAdapter(select, conn);
                DataSet ds = new DataSet();
                da.Fill(ds, "Rail_user");
                DataTable dt = ds.Tables["Rail_user"];

                //inserting into the table
                DataRow dr = dt.NewRow();
                dr["Name"] = textBox1.Text;
                dr["Email"] = textBox2.Text;
                dr["Username"] = textBox3.Text;
                dr["Password"] = textBox4.Text;
                dr["Gender"] = textBox5.Text;
                dt.Rows.Add(dr);

                SqlCommandBuilder cmd = new SqlCommandBuilder(da);
                da.Update(ds, "Rail_user");
                MessageBox.Show("Registration Successfully Completed.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Registration_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Login ll = new Login();
            ll.Show();
        }
    }
}
