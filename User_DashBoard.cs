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
    public partial class User_DashBoard : Form
    {
        public User_DashBoard()
        {
            InitializeComponent();
        }

        string str = "Data Source=localhost\\sqlexpress;Initial Catalog=Product;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";

        private void User_DashBoard_Load(object sender, EventArgs e)
        {
            panel1.Visible = true;
            panel2.Visible = false;
            panel3.Visible = false;
            panel4.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Update Profile
            panel2.Visible = true;
            panel3.Visible = false;
            panel4.Visible = false;
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Book Ticket
            Reservation_Counter rs = new Reservation_Counter();
            rs.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //Booking History
            panel3.Visible = true;
            panel4.Visible = false;
            panel2.Visible = false;

        }

        private void button4_Click(object sender, EventArgs e)
        {
            //Change Password
            panel4.Visible = true;
            panel2.Visible = false;
            panel3.Visible = false;

        }

        private void button5_Click(object sender, EventArgs e)
        {

            //Update Record
            
            SqlConnection conn = new SqlConnection(str);
            try
            {
                conn.Open();
                string name = textBox1.Text;
                string email = textBox2.Text;
                string uname = textBox3.Text;
                string gender = textBox4.Text;
                string olduname = textBox5.Text;

                string update = "UPDATE Rail_user SET Name = @Name, Email = @Email, Username = @Username, Gender = @Gender WHERE Username = @OldUsername";
                SqlCommand cmd = new SqlCommand(update, conn);
                //user for preventing sql injections
                cmd.Parameters.AddWithValue("@Name", name);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Username", uname);
                cmd.Parameters.AddWithValue("@Gender", gender);
                cmd.Parameters.AddWithValue("@OldUsername", olduname);
                
                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    MessageBox.Show("Record Updated Successfully.","Error",MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("No Records Updated. Please Enter Valide Old Username.","Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                        
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            
        }

        private void button6_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(str);
            try
            {
                conn.Open();
                string oldpass = textBox6.Text;
                string newpass = textBox7.Text;
                string confirmpass = textBox8.Text;

                if (newpass != confirmpass)
                {
                    MessageBox.Show("New Password and Confirm Password do not match. Please try again.");
                    return;
                }

                string update = "UPDATE Rail_user SET Password = @Pass WHERE Password = @OldPass";
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
                // Display the exception message
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button7_Click_1(object sender, EventArgs e)
        {
            //Display data in Grid View
            string query = "select * from Booking_table";
            SqlConnection conn = new SqlConnection(str);
            conn.Open();
            try
            {
                SqlCommand com = new SqlCommand(query, conn);
                SqlDataAdapter myadapt = new SqlDataAdapter();
                myadapt.SelectCommand = com;
                DataTable dtable = new DataTable();
                myadapt.Fill(dtable);
                dataGridView1.DataSource = dtable;
            }
            catch
            {
                MessageBox.Show("Error Loading data");
            }
        }
    }
}
