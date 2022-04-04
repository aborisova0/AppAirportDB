using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lr3
{
    public partial class Order : Form
    {
        SqlConnection sqlConnection;
        
        public Order()
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
            SqlCommand command = new SqlCommand("SELECT * FROM [_Order]", sqlConnection);
            try
            {
                reader = await command.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    listBox1.Items.Add(Convert.ToString(reader["order_number"]) + "\t" + (Convert.ToString(reader["cipher"])) + "\t" + (Convert.ToString(reader["date_of_receipt"])) + "\t" + (Convert.ToString(reader["data_of_complete"])) + "\t" + (Convert.ToString(reader["number_of_orders"])) + "\t" + (Convert.ToString(reader["id_customer"])));
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
            if (label2.Visible)
            {
                label2.Visible = false;
            }
            if (!string.IsNullOrEmpty(textBox1.Text) && !string.IsNullOrEmpty(textBox2.Text))
            {
                    SqlCommand command = new SqlCommand("INSERT INTO [_Order] (order_number, cipher, date_of_receipt, data_of_complete, number_of_orders, id_customer) VALUES(@order_number, @cipher, @date_of_receipt, @data_of_complete, @number_of_orders, @id_customer)", sqlConnection);
                    command.Parameters.AddWithValue("order_number", textBox1.Text);
                    command.Parameters.AddWithValue("cipher", textBox2.Text);
                    command.Parameters.AddWithValue("date_of_receipt", textBox7.Text);
                    command.Parameters.AddWithValue("data_of_complete", textBox8.Text);
                    command.Parameters.AddWithValue("number_of_orders", textBox9.Text);
                    command.Parameters.AddWithValue("id_customer", textBox10.Text);

                    await command.ExecuteNonQueryAsync();
                    label3.Text = "Заказ добавлен";
                    label3.Visible = true;
                    OnScreen();                              
            }
            else
            {
                label3.Visible = true;
                label3.Text = "Поля должны быть заполнены";
            }
        }
      
        private async void button2_Click(object sender, EventArgs e)
        {
            if (label7.Visible)
            {
                label7.Visible = false;
            }
            if (!string.IsNullOrEmpty(textBox3.Text) && !string.IsNullOrWhiteSpace(textBox3.Text) &&
             
                !string.IsNullOrEmpty(textBox4.Text) && !string.IsNullOrWhiteSpace(textBox4.Text) &&
                !string.IsNullOrEmpty(textBox5.Text) && !string.IsNullOrWhiteSpace(textBox5.Text))
            {
                try
                {
                    SqlCommand command = new SqlCommand("UPDATE [_Order] SET order_number=@order_number, cipher = @cipher, number_of_orders = @number_of_orders, id_customer = @id_customer", sqlConnection);
                    command.Parameters.AddWithValue("order_number", textBox3.Text);
                    command.Parameters.AddWithValue("cipher", textBox4.Text);
                    command.Parameters.AddWithValue("number_of_orders", textBox5.Text);
                    command.Parameters.AddWithValue("id_customer", textBox11.Text);

                    await command.ExecuteNonQueryAsync();

                    label7.Text = "Заказ изменен";
                    label7.Visible = true;
                    OnScreen();
                }
                catch (Exception ex)
                {
                    label7.Text = "Ошибка";
                    label7.Visible = true;
                }

            }
            else if (!string.IsNullOrEmpty(textBox3.Text))
            {
                label7.Visible = true;
                label7.Text = "Поле должно быть заполнено";
            }

            else if (!string.IsNullOrEmpty(textBox4.Text))
            {
                label7.Visible = true;
                label7.Text = "Поле  должно быть заполнено";
            }
            else if (!string.IsNullOrEmpty(textBox5.Text))
            {
                label7.Visible = true;
                label7.Text = "Поле должно быть заполнено";
            }
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            if (label9.Visible)
            {
                label9.Visible = false;
            }

            if (!string.IsNullOrEmpty(textBox6.Text))
            {
                try
                {
                    SqlCommand command = new SqlCommand("DELETE FROM _Order WHERE order_number=@order_number", sqlConnection);
                    command.Parameters.AddWithValue("order_number", textBox6.Text);

                    await command.ExecuteNonQueryAsync();

                    label9.Visible = true;
                    label9.Text = "Заказ удален";
                    OnScreen();
                }
                catch (Exception ex)
                {
                    label9.Visible = true;
                    label9.Text = "Заказа не существует";
                }
            }
           
        }
    }
}
