using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsDemo
{
    public partial class Form1 : Form
    {
        private add a;
        private update u;
        private view V;
        private delete d;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            a = new add();
            a.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            V = new view();
            V.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            u = new update();
            u.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {            
            d = new delete();
            d.Show();
            this.Hide();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
