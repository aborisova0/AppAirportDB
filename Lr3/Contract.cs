using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lr3
{
    public partial class Contract : Form
    {
        SqlConnection sqlConnection;
        public Contract()
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
            SqlCommand command = new SqlCommand("SELECT * FROM [Contract]", sqlConnection);
            try
            {
                reader = await command.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    listBox1.Items.Add(Convert.ToString(reader["number_of_contract"]) + "\t" + (Convert.ToString(reader["passport_number"])) + "\t" + (Convert.ToString(reader["start_date"])) + "\t" + (Convert.ToString(reader["contract_term"])) + "\t" + (Convert.ToString(reader["terminate_or_not"])) + "\t" + (Convert.ToString(reader["end_of_contact"])));
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
            if (label3.Visible)
            {
                label3.Visible = false;
            }
            if (!string.IsNullOrEmpty(textBox1.Text) && !string.IsNullOrEmpty(textBox2.Text))
            {
                SqlCommand sqlCommand = new SqlCommand("INSERT INTO Contract (number_of_contract, passport_number, start_date, contract_term, terminate_or_not, end_of_contact) VALUES(@number_of_contract, @passport_number, @start_date, @contract_term, @terminate_or_not, @end_of_contact)", sqlConnection);
                sqlCommand.Parameters.AddWithValue("number_of_contract", textBox1.Text);
                sqlCommand.Parameters.AddWithValue("passport_number", textBox2.Text);
                sqlCommand.Parameters.AddWithValue("start_date", textBox3.Text);
                sqlCommand.Parameters.AddWithValue("contract_term", textBox4.Text);
                sqlCommand.Parameters.AddWithValue("terminate_or_not", textBox6.Text);
                sqlCommand.Parameters.AddWithValue("end_of_contact", textBox9.Text);

                await sqlCommand.ExecuteNonQueryAsync();
                label3.Visible = true;
                label3.Text = "Контракт добавлен";
                OnScreen();
            }
            else
            {
                label3.Visible = true;
                label3.Text = "Поля дожны быть заполнены";
            }
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            if (label10.Visible)
            {
                label10.Visible = false;
            }
            if (!string.IsNullOrEmpty(textBox5.Text))
            {
                
                SqlCommand command = new SqlCommand("DELETE FROM Contract WHERE number_of_contract=@number_of_contract", sqlConnection);
                command.Parameters.AddWithValue("number_of_contract", textBox5.Text);

                await command.ExecuteNonQueryAsync();

                label10.Text = "Запись успешно удалена";
                label10.Visible = true;
                OnScreen();
            }
            else
            {
                label10.Visible = true;
                label10.Text = "Введите номер контракта";
            }
        }

        private async void button5_Click(object sender, EventArgs e)
        {
            listBox2.Items.Clear();
            SqlDataReader reader = null;

            try
            {
                SqlCommand command = new SqlCommand("SELECT Writer.surname, Contract.terminate_or_not FROM Writer INNER JOIN Contract ON Writer.passport_number = Contract.passport_number", sqlConnection);
               // command.Parameters.AddWithValue("name", textBox7.Text);
                reader = await command.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    listBox2.Items.Add(Convert.ToString(reader["surname"]) + "\t" + (Convert.ToString(reader["terminate_or_not"])));

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

        private async void button6_Click(object sender, EventArgs e)
        {
            listBox2.Items.Clear();
            SqlDataReader reader = null;

            try
            {
                SqlCommand command = new SqlCommand("SELECT Writer.surname, Contract.terminate_or_not FROM Writer INNER JOIN Contract ON Writer.passport_number = Contract.passport_number WHERE Contract.terminate_or_not = 'да'", sqlConnection);
                // command.Parameters.AddWithValue("name", textBox7.Text);
                reader = await command.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    listBox2.Items.Add(Convert.ToString(reader["surname"]) + "\t" + (Convert.ToString(reader["terminate_or_not"])));

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

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}
