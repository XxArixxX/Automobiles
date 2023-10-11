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
    public partial class Automobiles : Form
    {
        public Automobiles()
        {
            InitializeComponent();
            FillClient();
        }

        private void FillClient()
        {
            string SQL = "Select * FROM Автомобили";

            OleDbConnection conn = new OleDbConnection(Properties.Settings.Default.ConnStr);
            conn.Open();

            OleDbDataAdapter dataAdapter = new OleDbDataAdapter(SQL, conn);
            DataSet ds = new DataSet();
            dataAdapter.Fill(ds);

            dataGridView1.DataSource = ds.Tables[0];
            conn.Close();
        }
        private void Automobiles_Load(object sender, EventArgs e)
        {

        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count != 1)
            {
                MessageBox.Show("Выберите одну строку!", "Внимание!");
                return;
            }

            int index = dataGridView1.SelectedRows[0].Index;

            if (dataGridView1.Rows[index].Cells[0].Value == null ||
                dataGridView1.Rows[index].Cells[1].Value == null ||
                dataGridView1.Rows[index].Cells[2].Value == null ||
                dataGridView1.Rows[index].Cells[3].Value == null ||
                dataGridView1.Rows[index].Cells[4].Value == null)
            {
                MessageBox.Show("Не все данные введены!", "Внимание!");
                return;
            }

            string id = dataGridView1.Rows[index].Cells[0].Value.ToString();
            string model = dataGridView1.Rows[index].Cells[1].Value.ToString();
            string color = dataGridView1.Rows[index].Cells[2].Value.ToString();
            string yearMade = dataGridView1.Rows[index].Cells[3].Value.ToString();
            string govNumber = dataGridView1.Rows[index].Cells[4].Value.ToString(); 

            OleDbConnection conn = new OleDbConnection(Properties.Settings.Default.ConnStr);
            conn.Open();

            string Query = $"INSERT INTO Автомобили VALUES ( {id}, '{model}', '{color}', '{yearMade}', '{govNumber}')";
            OleDbCommand dbCommand = new OleDbCommand(Query, conn);

            if (dbCommand.ExecuteNonQuery() != 1)
                MessageBox.Show("Ошибка выполнения запроса!", "Ошибка!");
            else
                MessageBox.Show("Данные добавлены!", "Внимание!");
            conn.Close();
            FillClient();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count != 1)
            {
                MessageBox.Show("Выберите одну строку!", "Внимание!");
                return;
            }

            int index = dataGridView1.SelectedRows[0].Index;

            if (dataGridView1.Rows[index].Cells[0].Value == null)
            {
                MessageBox.Show("Не все данные введены!", "Внимание!");
                return;
            }

            string id = dataGridView1.Rows[index].Cells[0].Value.ToString();

            OleDbConnection conn = new OleDbConnection(Properties.Settings.Default.ConnStr);
            conn.Open();

            string Query = "DELETE FROM Автомобили WHERE id = " + id;
            OleDbCommand dbCommand = new OleDbCommand(Query, conn);

            if (dbCommand.ExecuteNonQuery() != 1)
                MessageBox.Show("Ошибка выполнения запроса!", "Ошибка!");
            else
                MessageBox.Show("Данные удалены!", "Внимание!");
            conn.Close();
            FillClient();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count != 1)
            {
                MessageBox.Show("Выберите одну строку!", "Внимание!");
                return;
            }

            int index = dataGridView1.SelectedRows[0].Index;

            if (dataGridView1.Rows[index].Cells[0].Value == null ||
                dataGridView1.Rows[index].Cells[1].Value == null ||
                dataGridView1.Rows[index].Cells[2].Value == null ||
                dataGridView1.Rows[index].Cells[3].Value == null ||
                dataGridView1.Rows[index].Cells[4].Value == null)
            {
                MessageBox.Show("Не все данные введены!", "Внимание!");
                return;
            }

            string id = dataGridView1.Rows[index].Cells[0].Value.ToString();
            string model = dataGridView1.Rows[index].Cells[1].Value.ToString();
            string color = dataGridView1.Rows[index].Cells[2].Value.ToString();
            string yearMade = dataGridView1.Rows[index].Cells[3].Value.ToString();
            string govNumber = dataGridView1.Rows[index].Cells[4].Value.ToString();

            OleDbConnection conn = new OleDbConnection(Properties.Settings.Default.ConnStr);
            conn.Open();

            string Query = $"UPDATE Автомобили SET [Модель автомобиля] = '{model}' , [Цвет автомобиля] =  '{color}', [Год выпуска автомобиля] = '{yearMade}', [Госномер автомобиля] = '{govNumber}' WHERE ID = {id}";
            OleDbCommand dbCommand = new OleDbCommand(Query, conn);

            if (dbCommand.ExecuteNonQuery() != 1)
                MessageBox.Show("Ошибка выполнения запроса!", "Ошибка!");
            else
                MessageBox.Show("Данные изменены!", "Внимание!");
            conn.Close();
            FillClient();
        }

        private void прокатToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form1 form = new Form1();

            form.Show();
            this.Close();
        }

        private void автомобилиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Automobiles form = new Automobiles();

            form.Show();
            this.Close();
        }

        private void клиентыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Globals.userType == "admin")
            {
                Clients form = new Clients();

                form.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Только администратор может просматривать таблицу клиентов");
            }
        }
    }
}
