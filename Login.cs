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

namespace Mini_Project
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        string connectionString = "Data Source=localhost\\sqlexpress;Initial Catalog=Product;Integrated Security=True;TrustServerCertificate=True";

        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string username = textBox1.Text.Trim();
            string password = textBox2.Text.Trim();

            // Check if fields are empty
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Please enter both username and password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string userQuery = "SELECT COUNT(*) FROM Rail_user WHERE Username = @Username AND Password = @Password";
            string adminQuery = "SELECT COUNT(*) FROM Rail_admin WHERE Username = @Username AND Password = @Password";


            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            SqlCommand userCommand = new SqlCommand(userQuery, connection);
            userCommand.Parameters.AddWithValue("@Username", username);
            userCommand.Parameters.AddWithValue("@Password", password);

            int userCount = Convert.ToInt32(userCommand.ExecuteScalar());
            if (userCount > 0)
            {
                User_DashBoard ud = new User_DashBoard();
                ud.Show();
            }

            SqlCommand adminCommand = new SqlCommand(adminQuery, connection);
            adminCommand.Parameters.AddWithValue("@Username", username);
            adminCommand.Parameters.AddWithValue("@Password", password);

            int adminCount = Convert.ToInt32(adminCommand.ExecuteScalar());
            if (adminCount > 0)
            {
                Admin_Dashboard ad = new Admin_Dashboard();
                ad.Show();
            }
            //else
            //{
            //    MessageBox.Show("Invalid username or password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Registration rs = new Registration();
            rs.Show();
        }
    }
}
