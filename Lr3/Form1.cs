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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var btn = (Button)sender;
            switch (btn.Text)
            {
                case "Книги":
                    {
                        Book book = new Book();
                        book.Show();
                       
                        break;
                    }

                case "Заказы":
                    {
                        Order order = new Order();
                        order.Show();
                       
                        break;
                    }
                case "Писатели":
                    {

                        Writer writer = new Writer();
                        writer.Show();
                       
                        break;
                    }
                case "Заказчики":
                    {
                        Customer customer = new Customer();
                        customer.Show();
                       
                        break;
                    }
                case "Каталог":
                    {
                        Catalog catalog = new Catalog();
                        catalog.Show();
                       
                        break;
                    }
                case "Контракты":
                    {
                        Contract contract = new Contract();
                        contract.Show();
                       
                        break;
                    }
            }
        }
    }
}
