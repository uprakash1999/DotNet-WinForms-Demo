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
    public partial class view : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source = (localdb)\MSSQLLocalDB;Initial Catalog = UserDB;Integrated Security = True");
        public view()
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

        private void button5_Click(object sender, EventArgs e)
        {
            delete d = new delete();
            d.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int userid;
            if(!int.TryParse(textBox1.Text, out userid))
            {
                MessageBox.Show("Please enter a valid ID.");
                return;
            }
            try
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("GetUserById", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("UserId", userid);

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
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
            }catch(Exception ex)
            {
                MessageBox.Show("An error occured: " + ex.Message);
            }
            finally
            {
                if(con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }

        }
    }
}
