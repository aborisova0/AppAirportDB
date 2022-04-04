using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Lr3
{
    public partial class Book : Form
    {
        SqlConnection sqlConnection;
        public Book()
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

        private async void button1_Click(object sender, EventArgs e)
        {
            if (label3.Visible)
            {
                label3.Visible = false;
            }
            if (!string.IsNullOrEmpty(textBox1.Text) && !string.IsNullOrEmpty(textBox2.Text))
            { 
                SqlCommand sqlCommand = new SqlCommand("INSERT INTO Book (cipher, circulation, data, cost_price, fee, price, name_book) VALUES(@cipher, @circulation, @data, @cost_price,  @fee, @price, @name_book)", sqlConnection);
                sqlCommand.Parameters.AddWithValue("name_book", textBox1.Text);
                sqlCommand.Parameters.AddWithValue("price", textBox2.Text);
                sqlCommand.Parameters.AddWithValue("cipher", textBox7.Text);
                sqlCommand.Parameters.AddWithValue("circulation", textBox8.Text);
                sqlCommand.Parameters.AddWithValue("data", textBox9.Text);
                sqlCommand.Parameters.AddWithValue("cost_price", textBox10.Text);
                sqlCommand.Parameters.AddWithValue("fee", textBox11.Text);

                await sqlCommand.ExecuteNonQueryAsync();
                label3.Visible = true;
                label3.Text = "Книга добавлена";
                OnScreen();
            }
            else
            {
                label3.Visible = true;
                label3.Text = "Поля дожны быть заполнены";
            }            
        }
        private async void OnScreen()
        {
            listBox1.Items.Clear();
            SqlDataReader reader = null;
            SqlCommand command = new SqlCommand("SELECT * FROM [Book]", sqlConnection);
            try
            {
                reader = await command.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    listBox1.Items.Add(Convert.ToString(reader["cipher"]) + "\t" + (Convert.ToString(reader["circulation"])) + "\t" + (Convert.ToString(reader["data"])) + "\t" + (Convert.ToString(reader["cost_price"])) + "\t" + (Convert.ToString(reader["fee"])) + "\t" + (Convert.ToString(reader["price"])) + "\t" + (Convert.ToString(reader["name_book"])));
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

        private async void button2_Click(object sender, EventArgs e)
        {            
            if (!string.IsNullOrEmpty(textBox3.Text) && !string.IsNullOrEmpty(textBox5.Text) && !string.IsNullOrEmpty(textBox12.Text) && !string.IsNullOrEmpty(textBox13.Text))
            {               
                SqlCommand command = new SqlCommand("UPDATE [Book] SET name_book=@name_book, price=@price, fee=@fee  WHERE cipher=@cipher", sqlConnection);
                command.Parameters.AddWithValue("cipher", textBox3.Text);
                command.Parameters.AddWithValue("name_book", textBox5.Text);
                command.Parameters.AddWithValue("price", textBox12.Text);
                command.Parameters.AddWithValue("fee", textBox13.Text);
                await command.ExecuteNonQueryAsync();
                label8.Text = "Информация обновлена";
                label8.Visible = true;
                OnScreen();
            }
            else if(!string.IsNullOrEmpty(textBox3.Text))
            {
                label8.Visible = true;
                label8.Text = "Введите шифр";
            }
            else
            {
                label8.Visible = true;
                label8.Text = "Все поля должны быть заполнены!";
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
                SqlCommand command = new SqlCommand("DELETE FROM Book WHERE cipher=@cipher", sqlConnection);
                command.Parameters.AddWithValue("cipher",textBox6.Text);

                await command.ExecuteNonQueryAsync();

                label10.Text = "Книга успешно удалена";
                label10.Visible = true;
                OnScreen();
            }
            else
            {
                label10.Visible = true;
                label10.Text = "Введите шифр";
            }
        }

        //public void Book_FormClosed(object sender, FormClosedEventArgs e)
        //{
        //    Form1.Show();
        //}
    }
}
