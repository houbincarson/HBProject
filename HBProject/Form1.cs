using HBProject.BLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HBProject.Model;

namespace HBProject
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        CreateDbBusiness business = new CreateDbBusiness();
        private void Form1_Load(object sender, EventArgs e)
        {
            business.CreateInitTable();
            DataTable dt = business.QueryUserInfo().Tables[0];
            this.dataGridView1.DataSource = dt.DefaultView;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            UserInfo user = new UserInfo
            {
                UserName = textBox1.Text.Trim(),
                Password = textBox2.Text.Trim(),
            };
            business.InsertUserInfo(user);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var davUser = dataGridView1.CurrentRow;
            if (davUser == null) return;
            textBox1.Text = davUser.Cells["name"].Value.ToString();
            textBox2.Text = davUser.Cells["password"].Value.ToString();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var davUser = dataGridView1.CurrentRow;
            if (davUser == null) return;
            textBox1.Text = davUser.Cells["name"].Value.ToString();
            textBox2.Text = davUser.Cells["password"].Value.ToString();
        }
    }
}
