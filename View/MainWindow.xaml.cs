using AppDB.View;
using AppDB.Core;
using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.Entity;

namespace AppDB
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public SupplementEntities Entities { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            ReadData();
        }

        public void CreateRecordButton_Click(object sender, RoutedEventArgs e)
        {
            AddingWindow addingWindow = new AddingWindow(this);
            addingWindow.Show();
        }

        public void ReadData()
        {
            Entities = new SupplementEntities();
            InvoicesDataGrid.ItemsSource = Entities.INVOICES.ToList();
        }

        public void UpdateRecordButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedInvoice = (sender as Button).DataContext as INVOICES;
            EditingWindow editingWindow = new EditingWindow(selectedInvoice, this);
            editingWindow.Show();
        }

        public void DeleteRecordButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedInvoice = ((sender as Button).DataContext as INVOICES);
            if (MessageBox.Show($"Вы уверены, что хотите удалить запись под номером {selectedInvoice.INVOICE_ID}?",
                "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                Entities.INVOICES.Remove(selectedInvoice);
                Entities.SaveChanges();
                ReadData();
            }
        }
    }
}
