using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.AxHost;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using System.Xml.Linq;

namespace WinFormsDemo
{
    public partial class delete : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source = (localdb)\MSSQLLocalDB;Initial Catalog = UserDB;Integrated Security = True");
        public delete()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form1 f = new Form1();
            f.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            add a = new add();
            a.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            update u = new update();
            u.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int userid;
            if(!int.TryParse(textBox1.Text, out userid))
            {
                MessageBox.Show("Please enter valid User ID.");
                return;
            }
            try
            {
                con.Open();
                string userDetails = GetUserDetails(userid);

                if (userDetails == null)
                {
                    MessageBox.Show("No user found with the specified ID.");
                    return;
                }

                var confirmResult = MessageBox.Show("Are you sure you want to delete the following user?\n\n" + userDetails,
                                             "Confirm Delete",
                                             MessageBoxButtons.YesNo);

                if (confirmResult == DialogResult.Yes) {
                    using (SqlCommand cmd = new SqlCommand("DELETEUSER", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@UserId", userid);

                        int rowAffected = cmd.ExecuteNonQuery();
                        if (rowAffected > 0)
                        {
                            MessageBox.Show("User Deleted Successfully.");
                            richTextBox1.AppendText("Deleted User Details:\n" + userDetails + "\n\n");
                        }
                        else
                        {
                            MessageBox.Show("No user found with the specified ID.");
                        }
                    }
                }
            }catch(Exception ex)
            {
                MessageBox.Show("An error occured: "+ex.Message);
            }
            finally
            {
                if(con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }
        private string GetUserDetails(int userId)
        {
            using (SqlCommand cmd = new SqlCommand("GetUserById", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserId", userId);

                using (SqlDataReader dr = cmd.ExecuteReader()) {
                    if (dr.Read()) { 
                        string name = dr["Name"].ToString();
                        string email = dr["EmailId"].ToString();
                        string mobile = dr["MobileNo"].ToString();
                        string country = dr["Country"].ToString();
                        string state = dr["State"].ToString();
                        string Gender = dr["gender"].ToString();

                        return $"User ID: {userId}\nName: {name}\nEmail: {email}\nMobile: {mobile}\nCountry: {country}\nState: {state}\nGender: {Gender}";
                    }
                }
                }
            return null;
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
                        

        }
    }
}
