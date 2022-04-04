using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Lr3
{
    public partial class Customer : Form
    {       
        SqlConnection sqlConnection;
        public Customer()
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

        private async void OnScreen()
        {
            listBox1.Items.Clear();
            SqlDataReader reader = null;
            SqlCommand command = new SqlCommand("SELECT * FROM [Customer]", sqlConnection);

            try
            {
                reader = await command.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    listBox1.Items.Add(Convert.ToString(reader["id_customer"]) + "\t" + (Convert.ToString(reader["name_surname"])) + "\t" + (Convert.ToString(reader["customer_name"])) + "\t" + (Convert.ToString(reader["address"])) + "\t" + (Convert.ToString(reader["phone"])));

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

        private async void button1_Click(object sender, EventArgs e)
        {

            if (label5.Visible)
            {
                label5.Visible = false;
            }
            if (!string.IsNullOrEmpty(textBox1.Text) && !string.IsNullOrEmpty(textBox2.Text) &&
                !string.IsNullOrWhiteSpace(textBox1.Text) && !string.IsNullOrWhiteSpace(textBox2.Text) &&
                !string.IsNullOrEmpty(textBox3.Text) && !string.IsNullOrWhiteSpace(textBox3.Text) &&
                !string.IsNullOrEmpty(textBox6.Text) && !string.IsNullOrWhiteSpace(textBox6.Text))
            {
                SqlCommand command = new SqlCommand("INSERT INTO [Customer] (id_customer, name_surname, customer_name, address, phone) VALUES(@id_customer, @name_surname, @customer_name, @address, @phone)", sqlConnection);
                command.Parameters.AddWithValue("name_surname", textBox1.Text);
                command.Parameters.AddWithValue("customer_name", textBox2.Text);
                command.Parameters.AddWithValue("address", textBox9.Text);
                command.Parameters.AddWithValue("phone", textBox6.Text);
                command.Parameters.AddWithValue("id_customer", textBox3.Text);
                await command.ExecuteNonQueryAsync();
                label5.Text = "Заказчик добавлен";
                label5.Visible = true;
                OnScreen();
            }
            else
            {
                label5.Visible = true;
                label5.Text = "Поля должны быть заполнены!";
            }
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            if (label8.Visible)
            {
                label8.Visible = false;
            }
            if (!string.IsNullOrEmpty(textBox5.Text) && !string.IsNullOrWhiteSpace(textBox5.Text))
            {
                SqlCommand command = new SqlCommand("DELETE FROM Customer WHERE id_customer=@id_customer", sqlConnection);
                command.Parameters.AddWithValue("id_customer", textBox5.Text);
                await command.ExecuteNonQueryAsync();
                label8.Text = "Информация удалена";
                label8.Visible = true;
                OnScreen();
            }
            else
            {
                label8.Visible = true;
                label8.Text = "Введите код заказчика";
            }
        }

        private async void button3_Click(object sender, EventArgs e)
        {          
            listBox2.Items.Clear();
            SqlDataReader reader = null;
            try
            {              
                SqlCommand command = new SqlCommand("SELECT * FROM Customer WHERE Customer.address LIKE 'г.Самара%'", sqlConnection);
                //command.Parameters.AddWithValue("address", textBox7.Text);
                reader = await command.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    listBox2.Items.Add(Convert.ToString(reader["id_customer"]) + "\t" + (Convert.ToString(reader["name_surname"])) + "\t" + (Convert.ToString(reader["customer_name"])) + "\t" + (Convert.ToString(reader["address"])) + "\t" + (Convert.ToString(reader["phone"])));

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
     
        private async void button5_Click(object sender, EventArgs e)
        {
            listBox2.Items.Clear();
            SqlDataReader reader = null;

            try
            {               
                SqlCommand command = new SqlCommand("SELECT * FROM Customer WHERE Customer.address NOT LIKE 'г.Самара%'", sqlConnection);

                //command.Parameters.AddWithValue("address", textBox7.Text);
                reader = await command.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    listBox2.Items.Add(Convert.ToString(reader["id_customer"]) + "\t" + (Convert.ToString(reader["name_surname"])) + "\t" + (Convert.ToString(reader["customer_name"])) + "\t" + (Convert.ToString(reader["address"])) + "\t" + (Convert.ToString(reader["phone"])));
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
    }
}
