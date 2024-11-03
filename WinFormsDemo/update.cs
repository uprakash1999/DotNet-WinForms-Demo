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

namespace WinFormsDemo
{
    public partial class update : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source = (localdb)\MSSQLLocalDB;Initial Catalog = UserDB;Integrated Security = True");
        public update()
        {
            InitializeComponent();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            delete d = new delete();
            d.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            add a = new add();
            a.Show();
            this.Hide();
        }

        //Updat Button
        private void button2_Click(object sender, EventArgs e)
        {
            int userid;
            if (!int.TryParse(textBox1.Text, out userid))
            {
                MessageBox.Show("Please enter valid id.");
                return;
            }
            string name = textBox2.Text;
            string email = textBox3.Text;
            string mobile = textBox4.Text;
            string country = textBox5.Text;
            string state = textBox6.Text;
            string gender = comboBox1.SelectedItem?.ToString();

            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(mobile) ||
               string.IsNullOrEmpty(country) || string.IsNullOrEmpty(state) || string.IsNullOrEmpty(gender))
            {
                MessageBox.Show("Please fill in all fields.");
                return;
            }
            try
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("UpdateUser", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserId", userid);
                    cmd.Parameters.AddWithValue("@Name", name);
                    cmd.Parameters.AddWithValue("@Gender", gender);
                    cmd.Parameters.AddWithValue("@EmailID", email);
                    cmd.Parameters.AddWithValue("@MobileNo", mobile);
                    cmd.Parameters.AddWithValue("@Country", country);
                    cmd.Parameters.AddWithValue("@State", state);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("User Updated Successfully.");
                        ClearFields();
                    }
                    else
                    {
                        MessageBox.Show("No user found with the specific id.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }
        //Retrieve Data
        private void button4_Click(object sender, EventArgs e)
        {
            int userid;
            if(!int.TryParse(textBox1.Text, out userid))
            {
                MessageBox.Show("Please enter valid id.");
                return;
            }
            try
            {
                con.Open();
                using(SqlCommand cmd = new SqlCommand("GetuserById", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("UserId", userid);
                    using(SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        if(dt.Rows.Count > 0)
                        {
                            DataRow dr = dt.Rows[0];

                            textBox1.Text = dr["UserId"].ToString();
                            textBox2.Text = dr["Name"].ToString();
                            textBox3.Text = dr["EmailId"].ToString();
                            textBox4.Text = dr["MobileNo"].ToString();
                            textBox5.Text = dr["Country"].ToString();
                            textBox6.Text = dr["State"].ToString();

                            comboBox1.SelectedItem = dr["Gender"].ToString();
                        }
                        else
                        {
                            MessageBox.Show("No user found with the specified Id.");
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("An Error occured: " + ex.Message);
            }
            finally
            {
                if(con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }
        private void ClearFields()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
            comboBox1.SelectedIndex = -1;
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            Form1 f = new Form1();
            f.Show();
            this.Close();
        }
    }
}
