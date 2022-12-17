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
        /// <summary>
        /// Контекст базы данных
        /// </summary>
        public SupplementEntities Entities { get; set; }


        /// <summary>
        /// Конструктор класса
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            ReadData();
        }


        /// <summary>
        /// Создание новой записи в таблице [Supplement].[INVOICE] по нажатию кнопки CreateRecordButton
        /// </summary>
        /// <param name="sender">Кнопка добавления записи: CreateRecordButton</param>
        /// <param name="e"></param>
        public void CreateRecordButton_Click(object sender, RoutedEventArgs e)
        {
            AddingWindow addingWindow = new AddingWindow(this);
            addingWindow.Show();
        }

        /// <summary>
        /// Чтение данных из базы данных
        /// </summary>
        public void ReadData()
        {
            Entities = new SupplementEntities();
            InvoicesDataGrid.ItemsSource = Entities.INVOICES.ToList();
            Title = $"База данных";
        }

        /// <summary>
        /// Обновление данных по нажатию кнопки
        /// </summary>
        /// <param name="sender">Кнопка обновить: UpdateRecordButton</param>
        /// <param name="e"></param>
        public void UpdateRecordButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedInvoice = (sender as Button).DataContext as INVOICES;
            EditingWindow editingWindow = new EditingWindow(selectedInvoice, this);
            editingWindow.Show();
        }

        /// <summary>
        /// Удаление данных по нажатию кнопки удалить
        /// </summary>
        /// <param name="sender">Кнопка удалить: DeleteRecordButton</param>
        /// <param name="e"> </param>
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

        private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DatePickerEnd.SelectedDate != null && DatePickerStart.SelectedDate != null)
            {
                if(DatePickerEnd.SelectedDate > DatePickerStart.SelectedDate)
                {
                    //MessageBox.Show($"END: {DatePickerEnd.SelectedDate} \n START: {DatePickerStart.SelectedDate}","ДА",
                    //    MessageBoxButton.OK,MessageBoxImage.Information);
                    var start = DatePickerStart.SelectedDate;
                    var end = DatePickerEnd.SelectedDate;
                    var filteredData = Entities.INVOICES.Where(x => x.DATE_OF_INVOICE > start && x.DATE_OF_INVOICE < end).ToList();
                    InvoicesDataGrid.ItemsSource = filteredData;
                }
                //else
                //{
                //    MessageBox.Show($"END: {DatePickerEnd.SelectedDate} \n START: {DatePickerStart.SelectedDate}", "НЕТ",
                //        MessageBoxButton.OK, MessageBoxImage.Error);
                //}
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
