using AppDB.Core;
using System;
using System.Linq;
using System.Windows;


namespace AppDB.View
{
    /// <summary>
    /// Логика взаимодействия для AddingWindow.xaml
    /// </summary>
    public partial class AddingWindow : Window
    {
        INVOICES _invoice; // Выбранная для изменения накладная
        MainWindow _mainWindow; // Главная страница
        SupplementEntities database;

        /// <summary>
        /// Конструктор окна добавления
        /// </summary>
        /// <param name="mainWindow">Ссылка на основное окно</param>
        public AddingWindow(MainWindow mainWindow)
        {
            InitializeComponent();
            _invoice = new INVOICES();
            _mainWindow = mainWindow;
            database = _mainWindow.Entities;
            DataContext = _invoice;
        }

        // Сбор данных с полей ввода
        private void ValidateInput()
        {
            // Чтение ввода и запись в новую накладную
            _invoice.DATE_OF_INVOICE = String.IsNullOrEmpty(TextBoxDate.Text) ? DateTime.Now : DateTime.Parse(TextBoxDate.Text);
            _invoice.PRODUCT_ID = ComboBoxProduct.SelectedIndex == -1 ? null : ComboBoxProduct.SelectedIndex + 1;
            _invoice.PURVEYOR_ID = ComboBoxPurveyor.SelectedIndex == -1 ? null : ComboBoxPurveyor.SelectedIndex + 1;
            _invoice.FORWARDER_ID = ComboBoxForwarder.SelectedIndex == -1 ? null : ComboBoxForwarder.SelectedIndex + 1;
            _invoice.SUPPLY_TYPE_ID = ComboBoxSupplyType.SelectedIndex == -1 ? null : ComboBoxSupplyType.SelectedIndex + 1;
            _invoice.DELIVERY_COST = String.IsNullOrEmpty(TextBoxCost.Text) ? 0 : int.Parse(TextBoxCost.Text);
            _invoice.DELIVERY_TONNAGE = String.IsNullOrEmpty(TextBoxTonnage.Text) ? 0 : int.Parse(TextBoxTonnage.Text);
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {

            ValidateInput();
            database.INVOICES.Add(_invoice);
            database.SaveChanges();
            //Обновление таблицы на главной странице
            _mainWindow.ReadData();
            Hide();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Инициализация данных ComboBox'ов
            ComboBoxProduct.ItemsSource = database.PRODUCTS.ToList();
            ComboBoxPurveyor.ItemsSource = database.PURVEYORS.ToList();
            ComboBoxForwarder.ItemsSource = database.FORWARDERS.ToList();
            ComboBoxSupplyType.ItemsSource = database.SUPPLY_TYPES.ToList();
        }
    }
}
