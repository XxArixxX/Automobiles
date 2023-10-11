using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Authorization : Form
    {
        public Authorization()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Registration registration = new Registration();

            registration.Show();

            //Application.Run(registration);
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OleDbConnection conn = new OleDbConnection(Properties.Settings.Default.ConnStr);
            conn.Open();
            OleDbDataAdapter dataAdapter = new OleDbDataAdapter($"SELECT * FROM Клиенты WHERE логин = '{textBox1.Text}' and пароль = '{textBox2.Text}'", conn);
            DataTable dt = new DataTable();
            
            if (textBox1.Text == null || textBox2.Text == null) {
                MessageBox.Show("Заполните поля!");
            }
            else
            {
                dataAdapter.Fill(dt);
                if(textBox1.Text == "admin" && textBox2.Text == "admin")
                {
                    Globals.userType = "admin";
                    conn.Close();
                    Form1 form = new Form1();

                    form.Show();
                    this.Hide();
                }
                else if (dt.Rows.Count > 0)
                {
                    Globals.userType = "user";
                    conn.Close();
                    Form1 form = new Form1();

                    form.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Неверный логин или пароль");
                }
            }

        }

        private void Authorization_Load(object sender, EventArgs e)
        {

        }
    }
}
