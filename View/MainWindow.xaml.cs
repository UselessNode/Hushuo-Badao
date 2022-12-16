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
            Title = $"База данных";
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

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var input = (sender as TextBox).Text.ToLower();
            if (!(String.IsNullOrEmpty(input)))
            {
                int resultCount = Entities.INVOICES.Count(x => x.PRODUCTS.PRODUCT_NAME.Contains(input));
                InvoicesDataGrid.ItemsSource = Entities.INVOICES.Where(x => x.PRODUCTS.PRODUCT_NAME.Contains(input)).ToList();
                Title = $"База данных | Поиск: {input} | Результатов: {resultCount} из {Entities.INVOICES.ToList().Count()}";
            }
            else
                ReadData();
        }

        private void ResetSearchButton_Click(object sender, RoutedEventArgs e)
        {
            SearchTextBox.Text = "";
            DatePickerEnd.Text = DatePickerStart.Text = "";
            ReadData();
        }

        private void DatePickerEnd_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DatePickerEnd.SelectedDate != null && DatePickerStart.SelectedDate != null)
            {
                if(DatePickerEnd.SelectedDate > DatePickerStart.SelectedDate)
                {
                    var start = DatePickerStart.SelectedDate;
                    var end = DatePickerEnd.SelectedDate;
                    //MessageBox.Show($"END: {DatePickerEnd.SelectedDate} \n START: {DatePickerStart.SelectedDate}","ДА",
                    //    MessageBoxButton.OK,MessageBoxImage.Information);

                    var filteredData = Entities.INVOICES.Where(x => x.DATE_OF_INVOICE > start && x.DATE_OF_INVOICE < end).ToList();

                    InvoicesDataGrid.ItemsSource = filteredData;
                }
                else
                {
                    //MessageBox.Show($"END: {DatePickerEnd.SelectedDate} \n START: {DatePickerStart.SelectedDate}", "НЕТ",
                    //    MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void DatePickerStart_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
