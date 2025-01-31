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
    public partial class Reservation_Counter : Form
    {
        public Reservation_Counter()
        {
            InitializeComponent();
        }
        //Connection String
        string connectionString = "Data Source=localhost\\sqlexpress;Initial Catalog=Product;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";
        
        private void Reservation_Counter_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //train detail Clear
            comboBox1.ResetText();
            textBox1.Clear();
            textBox2.Clear();
            comboBox2.ResetText();
            comboBox3.ResetText();
            textBox3.Clear();

            //Consumer detail Clear
            comboBox4.ResetText();
            comboBox5.ResetText();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
            textBox7.Clear();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem == null)
            {
                MessageBox.Show("Please select a train number.");
                return;
            }

            //Select Data from Combobox 
            string selectquery = "SELECT TrainNo FROM TrainDetails";
            SqlConnection connection = new SqlConnection(connectionString);   
            connection.Open();
            SqlCommand command = new SqlCommand(selectquery, connection);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                comboBox1.Items.Add(reader["TrainNo"].ToString());
            }
            reader.Close();

            //Display data into textbox
            string selectedTrainNo = comboBox1.SelectedItem.ToString();
            string updatequery = "SELECT TrainName FROM TrainDetails WHERE TrainNo = @TrainNo";
            SqlCommand cmd = new SqlCommand(updatequery, connection);
            cmd.Parameters.AddWithValue("@TrainNo", selectedTrainNo);
            SqlDataReader rd = cmd.ExecuteReader();
            if (rd.Read())
            {
                textBox1.Text = rd["TrainName"].ToString();
            }
            else
            {
                textBox1.Text = "Train not found.";
            }
            connection.Close();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.SelectedItem.ToString() == "Nashik" && comboBox3.SelectedItem.ToString() == "Pune")
            {
                textBox3.Text = "200";
            }
            else if(comboBox2.SelectedItem.ToString() == "Nashik" && comboBox3.SelectedItem.ToString() == "kalyan")
            {
                textBox3.Text = "90";
            }
            else if (comboBox2.SelectedItem.ToString() == "Nashik" && comboBox3.SelectedItem.ToString() == "Dadar")
            {
                textBox3.Text = "110";
            }
            else if (comboBox2.SelectedItem.ToString() == "Nashik" && comboBox3.SelectedItem.ToString() == "Dhule")
            {
                textBox3.Text = "160";
            }
            else if (comboBox2.SelectedItem.ToString() == "Nashik" && comboBox3.SelectedItem.ToString() == "Bhusaval")
            {
                textBox3.Text = "300";
            }
            else if (comboBox2.SelectedItem.ToString() == "Nashik" && comboBox3.SelectedItem.ToString() == "Nagpur")
            {
                textBox3.Text = "380";
            }
            else if (comboBox2.SelectedItem.ToString() == "Nashik" && comboBox3.SelectedItem.ToString() == "Varanasi")
            {
                textBox3.Text = "450";
            }
            else
            {
                textBox3.Text = string.Empty;
            }
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox2_SelectedIndexChanged(sender, e); // Reuse the same logic
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //Login login = new Login();
            //login.Show();
            //this.Hide();
            this.Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string connectionstring = "Data Source=localhost\\sqlexpress;Initial Catalog=Product;Integrated Security=True;TrustServerCertificate=True";
                SqlConnection conn = new SqlConnection(connectionstring);

                if (string.IsNullOrEmpty(textBox4.Text) )
                {
                    textBox4.Text = "No Data";
                }
                if (string.IsNullOrEmpty(textBox5.Text))
                {
                    textBox5.Text = "No Data";
                }
                if (string.IsNullOrEmpty(textBox6.Text))
                {
                    textBox6.Text = "No Data";
                }
                if (string.IsNullOrEmpty(textBox7.Text))
                {
                    textBox7.Text = "No Data";
                }

                string select = "select * from Booking_table";
                SqlDataAdapter da = new SqlDataAdapter(select, conn);
                DataSet ds = new DataSet();
                da.Fill(ds, "Booking_table");
                DataTable dt = ds.Tables["Booking_table"];

                Random r = new Random();
                textBox2.Text = r.Next(0, 1000000).ToString();

                //inserting into the table
                DataRow dr = dt.NewRow();
                dr["TrainNo"] = comboBox1.SelectedItem;
                dr["TrainName"] = textBox1.Text;
                dr["TicketNo"] = textBox2.Text;
                dr["Source"] = comboBox2.SelectedItem;
                dr["Destination"] = comboBox3.SelectedItem;
                dr["Rent"] = textBox3.Text;
                dr["Total_Adult_No"] = comboBox4.SelectedItem;
                dr["Total_CHildren_No"] = comboBox5.SelectedItem;
                dr["First_Member_Name"] = textBox4.Text;
                dr["Second_Member_Name"] = textBox5.Text;
                dr["Third_Member_Name"] = textBox6.Text;
                dr["Fourth_Member_Name"] = textBox7.Text;

                dt.Rows.Add(dr);

                SqlCommandBuilder cmd = new SqlCommandBuilder(da);
                da.Update(ds, "Booking_table");
                MessageBox.Show("Booking Successfully Completed.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
    }
}
