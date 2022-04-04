using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Lr3
{
    public partial class Catalog : Form
    {
        SqlConnection sqlConnection;
        public Catalog()
        {
            InitializeComponent();
        }

        private async void _Connect(object sender, EventArgs e)
        {
            string connectionString = @"Data Source=localhost;Initial Catalog=ЭВМ ЛР-2;Integrated Security=True";
            sqlConnection = new SqlConnection(connectionString);
            await sqlConnection.OpenAsync();
            OnScreen();
        }

        private async void button1_Click(object sender, EventArgs e) //добавить запись
        {
            if (label3.Visible)
            {
                label3.Visible = false;
            }
            if (!string.IsNullOrEmpty(textBox7.Text) && !string.IsNullOrEmpty(textBox8.Text)&& !string.IsNullOrEmpty(textBox1.Text))
            {                              
                SqlCommand sqlCommand = new SqlCommand("INSERT INTO Catalog (cipher, passport_number, id) VALUES(@cipher, @passport_number, @id)", sqlConnection);
               
                sqlCommand.Parameters.AddWithValue("cipher", textBox7.Text);
                sqlCommand.Parameters.AddWithValue("passport_number", textBox8.Text);
                sqlCommand.Parameters.AddWithValue("id", textBox1.Text);
                await sqlCommand.ExecuteNonQueryAsync();
                label3.Visible = true;
                label3.Text = "Запись добавлена";
                OnScreen();
            }
            else
            {
                label3.Visible = true;
                label3.Text = "Поля дожны быть заполнены!";
            }
        }

        private async void OnScreen()
        {
            listBox1.Items.Clear();
            SqlDataReader reader = null;
            SqlCommand command = new SqlCommand("SELECT * FROM [Catalog]", sqlConnection);
            try
            {
                reader = await command.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    listBox1.Items.Add(Convert.ToString(reader["cipher"]) + "\t" + (Convert.ToString(reader["passport_number"])) + "\t" + (Convert.ToString(reader["id"])));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
            }
        }

        private async void button3_Click(object sender, EventArgs e) //удаление
        {
            if (label10.Visible)
            {
                label10.Visible = false;
            }
            if (!string.IsNullOrEmpty(textBox6.Text))
            {               
                SqlCommand command = new SqlCommand("DELETE FROM Catalog WHERE id=@id", sqlConnection);
                command.Parameters.AddWithValue("id", textBox6.Text);
                await command.ExecuteNonQueryAsync();
                label10.Text = "Запись удалена";
                label10.Visible = true;
                OnScreen();
            }
            else
            {
                label10.Visible = true;
                label10.Text = "Введите код записи";
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
