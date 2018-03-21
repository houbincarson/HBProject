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
using System.Reflection;

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
            DataTable id = (DataTable) business.InsertUserInfo(user); 
            DataTable dt = business.QueryUserInfo().Tables[0];
            this.dataGridView1.DataSource = dt.DefaultView;
            int row = int.Parse(id.Rows[0][0].ToString());
            DataRow dr = dt.Rows.Find(row);
            this.dataGridView1.CurrentCell = dr[0] as DataGridViewCell;
            this.dataGridView1.Rows[row - 1].Selected = true;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            var davUser = dataGridView1.CurrentRow.DataBoundItem as DataRowView;
            UserInfo user = new UserInfo();
            user = Common.DataRowToT<UserInfo>(davUser.Row);
            DataTable id = (DataTable)business.DeleteUserInfo(user);
            DataTable dt = business.QueryUserInfo().Tables[0];
            this.dataGridView1.DataSource = dt.DefaultView;
            int row = int.Parse(id.Rows[0][0].ToString());
            this.dataGridView1.CurrentCell = this.dataGridView1.Rows[row - 1].Cells[0];
            this.dataGridView1.Rows[row - 1].Selected = true;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var davUser = dataGridView1.CurrentRow;
            if (davUser == null) return;
            textBox1.Text = davUser.Cells["name"].Value.ToString();
            textBox2.Text = davUser.Cells["password"].Value.ToString();
        }


        public void update(string xxx)
        {
            textBox2.Text = xxx;
        }

        private void button3_Click(object sender, EventArgs e)
        {
           Form2 frm = new Form2();
           frm.Dis += update;
           frm.Show();
        }
    }
}
