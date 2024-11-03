using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsDemo
{
    public partial class add : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source = (localdb)\MSSQLLocalDB;Initial Catalog = UserDB;Integrated Security = True");
       
        public add()
        {
            InitializeComponent();
        }

        private void add_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form1 f = new Form1();
            f.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
                email_l.Hide();
                mobile_l.Hide();
                userid_l.Hide();
                country_l.Hide();
                state_l.Hide();
            try
            {

                int user_id;
                bool isuseridValid = int.TryParse(textBox1.Text, out user_id) && user_id > 0;

                string name = textBox2.Text;
                string email_pattern = @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$";

                string email = textBox3.Text;
                bool isEmailValid = Regex.IsMatch(textBox3.Text, email_pattern);

                string mobile_pattern = @"^\(?\d{3}\)?[-.\s]?\d{3}[-.\s]?\d{4}$";
                string mobile = textBox4.Text;
                bool isMobileValid = Regex.IsMatch(textBox4.Text, mobile_pattern);

                string country = textBox5.Text;
                string state = textBox6.Text;

                if(comboBox1.SelectedItem == null)
                {
                    throw new Exception("Gender missing");
                }
                string g = comboBox1.SelectedItem.ToString();
                con.Open();
                if (!isuseridValid)
                {
                    userid_l.Show();
                    return;
                }
                else if (string.IsNullOrEmpty(textBox2.Text))
                {
                    MessageBox.Show("Name field required.");
                }
                else if (!isEmailValid)
                {
                    email_l.Show();
                    return;
                }
                else if (!isMobileValid)
                {
                    mobile_l.Show();
                    return;
                }
                else if(comboBox1.SelectedItem == null)
                {
                    MessageBox.Show("Select gender");
                    return;
                }
                else if(string.IsNullOrEmpty(textBox5.Text))
                {
                    country_l.Show();
                    return;
                }
                else if(string.IsNullOrEmpty(textBox6.Text))
                {
                    state_l.Show();
                    return;
                }
                else
                {
                    using (SqlCommand cmd = new SqlCommand("AddUser", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@UserId", user_id);
                        cmd.Parameters.AddWithValue("@name", name);
                        cmd.Parameters.AddWithValue("@Gender", g);
                        cmd.Parameters.AddWithValue("@EmailID", email);
                        cmd.Parameters.AddWithValue("@MobileNo", mobile);
                        cmd.Parameters.AddWithValue("@Country", country);
                        cmd.Parameters.AddWithValue("@State", state);

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("User added successfully");

                        void ClearAllText(Control con)
                        {
                            foreach (Control c in con.Controls)
                            {
                                if (c is TextBox)
                                {
                                    ((TextBox)c).Clear();
                                }
                                else
                                {
                                    ClearAllText(c);
                                }
                            }
                        comboBox1.SelectedIndex = -1;
                        }
                        ClearAllText(this);

                    }
                }
                

                
            } 
            catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if(con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            update u = new update();
            u.Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            delete d = new delete();
            d.Show();
            this.Hide();
        }
    }
}
