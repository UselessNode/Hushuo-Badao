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

        public EditingWindow(INVOICES invoice, MainWindow mainWindow)
        {
            InitializeComponent();
            _invoice = invoice;
            _mainWindow = mainWindow;
            database = _mainWindow.Entities;
            DataContext = _invoice;
        }

        // Сбор данных с полей ввода
        private int ValidateInput(Control control)
        {
            // Проверка типов TextBox и ComboBox
            if (!(control is TextBox))
                return -1;
            else if (!(control is ComboBox))
                return -1;

            // Проверка на пустые значения
            if (control is TextBox && String.IsNullOrEmpty((control as TextBox).Text) ||
               control is ComboBox && (control as ComboBox).SelectedIndex == -1)
                return 0;
            // Чтение ввода и запись в новую накладную
            _invoice.DATE_OF_INVOICE = DateTime.Parse(TextBoxDate.Text);
            _invoice.PRODUCT_ID = ComboBoxProduct.SelectedIndex + 1;
            _invoice.PURVEYOR_ID = ComboBoxPurveyor.SelectedIndex + 1;
            _invoice.FORWARDER_ID = ComboBoxForwarder.SelectedIndex + 1;
            _invoice.SUPPLY_TYPE_ID = ComboBoxSupplyType.SelectedIndex + 1;
            _invoice.DELIVERY_TONNAGE = Int16.Parse(TextBoxTonnage.Text);
            _invoice.DELIVERY_COST = Int32.Parse(TextBoxCost.Text);
            return 1;
        }


        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            foreach (Control item in EditingGrid.Children)
            {
                ValidateInput(item);
            }
            //entities.INVOICES.Add(_invoice);
            database.SaveChanges();
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
            ComboBoxProduct.SelectedIndex = (int)_invoice.PRODUCT_ID - 1;
            ComboBoxPurveyor.SelectedIndex = (int)_invoice.PURVEYOR_ID - 1;
            ComboBoxForwarder.SelectedIndex = (int)_invoice.FORWARDER_ID - 1;
            ComboBoxSupplyType.SelectedIndex = (int)_invoice.SUPPLY_TYPE_ID - 1;
        }
    }
}
