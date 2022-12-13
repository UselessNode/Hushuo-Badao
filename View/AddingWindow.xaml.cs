using AppDB.Core;
using AppDB.View;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;


namespace AppDB.View
{
    /// <summary>
    /// Логика взаимодействия для AddingWindow.xaml
    /// </summary>
    public partial class AddingWindow : Window
    {
        private INVOICES invoice;
        public AddingWindow()
        {
            InitializeComponent();
        }

        void CreateNewRecord()
        {
            using (DatabaseManager manager = new DatabaseManager())
            {
                if(invoice != null)
                    manager.Invoices.Add(invoice);
                manager.SaveChanges();
            }
        }
    }
}
