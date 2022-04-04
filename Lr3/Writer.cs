using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Lr3
{
    public partial class Writer : Form
    {
        SqlConnection sqlConnection;
        public Writer()
        {
            InitializeComponent();
        }
        private async void _Connect(object sender, EventArgs e)
        {
            string connectionString = @"Data Source=localhost;Initial Catalog=ЭВМ ЛР-2;Integrated Security=True";
            sqlConnection = new SqlConnection(connectionString);
            await sqlConnection.OpenAsync();
            _Table();           
        }
        private async void button1_Click(object sender, EventArgs e)
        {
            if (label2.Visible)
            {
                label2.Visible = false;
            }
            if (!string.IsNullOrEmpty(textBox1.Text) && !string.IsNullOrEmpty(textBox2.Text) && !string.IsNullOrEmpty(textBox8.Text) &&
                !string.IsNullOrEmpty(textBox3.Text)  && !string.IsNullOrEmpty(textBox4.Text) && !string.IsNullOrEmpty(textBox11.Text))
            {
                SqlCommand command = new SqlCommand("INSERT INTO [Writer] (passport_number, name, surname, second_name, address, phone) VALUES(@passport_number, @name, @surname, @second_name, @address, @phone)", sqlConnection);
                command.Parameters.AddWithValue("name", textBox4.Text);
                command.Parameters.AddWithValue("second_name", textBox10.Text);
                command.Parameters.AddWithValue("surname", textBox11.Text);
                command.Parameters.AddWithValue("address", textBox3.Text);
                command.Parameters.AddWithValue("passport_number", textBox2.Text);
                command.Parameters.AddWithValue("phone", textBox1.Text);

                await command.ExecuteNonQueryAsync();
                label2.Text = "Запись добавлена";
                label2.Visible = true;
                _Table();
            }
            else
            {
                label2.Visible = true;
                label2.Text = "Поля должны быть заполнены";
            }
        }
        private async void _Table() //вывод на экран
        {
            listBox2.Items.Clear();
            SqlDataReader reader = null;
            SqlCommand command = new SqlCommand("SELECT * FROM [Writer]", sqlConnection);
            try
            {
                reader = await command.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    listBox2.Items.Add(Convert.ToString(reader["passport_number"]) + "\t" + (Convert.ToString(reader["name"])) + "\t" + (Convert.ToString(reader["surname"])) + "\t" + (Convert.ToString(reader["second_name"])) + "\t" + (Convert.ToString(reader["address"])) + "\t" + (Convert.ToString(reader["phone"])));
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

        private async void button2_Click(object sender, EventArgs e) //изменить данные
        {
            if (label10.Visible)
            {
                label10.Visible = false;
            }
            if (!string.IsNullOrEmpty(textBox6.Text) && !string.IsNullOrEmpty(textBox7.Text) &&
                !string.IsNullOrEmpty(textBox5.Text) && !string.IsNullOrEmpty(textBox8.Text))
            {                
                SqlCommand command = new SqlCommand("UPDATE [Writer] SET phone=@phone, address=@address, surname=@surname WHERE passport_number=@passport_number", sqlConnection);
                command.Parameters.AddWithValue("phone", textBox8.Text);
                command.Parameters.AddWithValue("address", textBox7.Text);
                command.Parameters.AddWithValue("surname", textBox6.Text);
                command.Parameters.AddWithValue("passport_number", textBox5.Text);
                await command.ExecuteNonQueryAsync();
                label10.Text = "Информация обновлена";
                label10.Visible = true;
                _Table();
            }
            else if (!string.IsNullOrEmpty(textBox5.Text) && !string.IsNullOrWhiteSpace(textBox5.Text))
            {
                label10.Visible = true;
                label10.Text = "Введите номер паспорта";
            }
        }

        private async void button3_Click(object sender, EventArgs e) //удалить данные
        {
            if (label12.Visible)
            {
                label12.Visible = false;
            }
            if (!string.IsNullOrEmpty(textBox9.Text) && !string.IsNullOrWhiteSpace(textBox9.Text))
            {
                SqlCommand command = new SqlCommand("DELETE FROM Writer WHERE passport_number=@passport_number", sqlConnection);
                command.Parameters.AddWithValue("passport_number", textBox9.Text);
                await command.ExecuteNonQueryAsync();
                label12.Text = "Писатель удален";
                label12.Visible = true;
                _Table();
            }
            else
            {
                label12.Visible = true;
                label12.Text = "Введите номер паспорта";
            }
        }
    }
}
