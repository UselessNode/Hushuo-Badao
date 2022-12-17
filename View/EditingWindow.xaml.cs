using AppDB.Core;
using AppDB.View;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Runtime.Remoting.Contexts;
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
    /// Логика взаимодействия для EditingWindow.xaml
    /// </summary>
    public partial class EditingWindow : Window
    {

        INVOICES _invoice; // Выбранная для изменения накладная
        MainWindow _mainWindow; // Главная страница

        SupplementEntities database;

        /// <summary>
        /// Конструктор окна обновления записи
        /// </summary>
        /// <param name="invoice">Ссылка типа INVOICE на редактируемую запись</param>
        /// <param name="mainWindow">Ссылка на главное окно</param>
        public EditingWindow(INVOICES invoice, MainWindow mainWindow)
        {
            InitializeComponent();
            _invoice = invoice;
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
            database.SaveChanges();
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
            // Индексы при редактировании
            ComboBoxProduct.SelectedIndex = _invoice.PRODUCT_ID is null ? -1 : (int)_invoice.PRODUCT_ID - 1;
            ComboBoxPurveyor.SelectedIndex = _invoice.PURVEYOR_ID is null? -1 : (int)_invoice.PURVEYOR_ID - 1;
            ComboBoxForwarder.SelectedIndex = _invoice.FORWARDER_ID is null ? -1 : (int)_invoice.FORWARDER_ID - 1;
            ComboBoxSupplyType.SelectedIndex = _invoice.SUPPLY_TYPE_ID is null ? -1 : (int)_invoice.SUPPLY_TYPE_ID - 1;
        }
    }
}
