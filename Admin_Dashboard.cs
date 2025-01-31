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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Mini_Project
{
    public partial class Admin_Dashboard : Form
    {
        public Admin_Dashboard()
        {
            InitializeComponent();
        }

        string connectionstring = "Data Source=localhost\\sqlexpress;Initial Catalog=Product;Integrated Security=True;TrustServerCertificate=True";

        private void Admin_Dashboard_Load(object sender, EventArgs e)
        {
            panel1.Visible = true;
            panel2.Visible = false;
            panel3.Visible = false;
            panel4.Visible = false;
            panel5.Visible = false;
        }
        
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Add train
            panel2.Visible = true; 
            panel3.Visible = false;
            panel4.Visible = false;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            //Update train
            panel3.Visible = true;
            //panel2.Visible = false;
            panel4.Visible = false;
        }
        private void button3_Click(object sender, EventArgs e)
        {
            //Delete train
            panel4.Visible = true;
            //panel2.Visible = false;
            //panel3.Visible = false;
        }
        private void button4_Click(object sender, EventArgs e)
        {
            panel5.Visible = true;
            //panel4.Visible = false;
            //panel3.Visible = false;
            //panel2.Visible = false;
        }


        private void button6_Click(object sender, EventArgs e)
        {
            //Add new train
            try
            {
                SqlConnection conn = new SqlConnection(connectionstring);

                string select = "select * from TrainDetails";
                SqlDataAdapter da = new SqlDataAdapter(select, conn);
                DataSet ds = new DataSet();
                da.Fill(ds, "TrainDetails");
                DataTable dt = ds.Tables["TrainDetails"];

                //inserting into the table
                DataRow dr = dt.NewRow();
                dr["TrainNo"] = textBox1.Text;
                dr["TrainName"] = textBox2.Text;
                dt.Rows.Add(dr);

                SqlCommandBuilder cmd = new SqlCommandBuilder(da);
                da.Update(ds, "TrainDetails");
                MessageBox.Show("Train Details Successfully Added.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            //Update Record
            SqlConnection conn = new SqlConnection(connectionstring);
            try
            {
                conn.Open();

                string oldtrainno = textBox3.Text;
                string newtrainno = textBox4.Text;
                string name = textBox5.Text;

                //Use Strong Typing for Input Validation: If train numbers are numeric, validate that inputs are numbers.
                if (!int.TryParse(oldtrainno, out _) || !int.TryParse(newtrainno, out _))
                {
                    MessageBox.Show("Train numbers must be numeric.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string update = "UPDATE TrainDetails SET TrainNo = @NewNum, TrainName = @Name WHERE TrainNo = @OldNum";
                SqlCommand cmd = new SqlCommand(update, conn);
                    
                cmd.Parameters.AddWithValue("@NewNum", newtrainno);
                cmd.Parameters.AddWithValue("@Name", name);
                cmd.Parameters.AddWithValue("@OldNum", oldtrainno);

                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    MessageBox.Show("Train Details Updated Successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Train Details Not Updated. Please enter a valid old train number.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);   
            }
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();   
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(connectionstring);
            try
            {
                conn.Open();
                string oldpass = textBox7.Text;
                string newpass = textBox8.Text;
                string confirmpass = textBox9.Text;

                if (newpass != confirmpass)
                {
                    MessageBox.Show("New Password and Confirm Password do not match. Please try again.");
                    return;
                }

                string update = "UPDATE Rail_admin SET Password = @Pass WHERE Password = @OldPass";
                SqlCommand cmd = new SqlCommand(update, conn);

                cmd.Parameters.AddWithValue("@Pass", newpass);
                cmd.Parameters.AddWithValue("@OldPass", oldpass);

                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    MessageBox.Show("Password Updated Successfully.");
                }
                else
                {
                    MessageBox.Show("Old Password is Incorrect, Please try again.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }

        
    }
}
