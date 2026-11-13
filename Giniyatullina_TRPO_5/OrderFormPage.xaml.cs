using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Giniyatullina_TRPO_5
{
    /// <summary>
    /// Логика взаимодействия для OrderFormPage.xaml
    /// </summary>
    public partial class OrderFormPage : Page
    {
        private ObservableCollection<Order> _orderList; //лист
        private Order _order; //заказ с которым мы работаем прямо сейчас
        private Order? _originalOrder;
        public OrderItem? SelectedItem { get; set; }
        public OrderFormPage(ObservableCollection<Order> orderList, Order? order = null)
        {
            _originalOrder = order;
            _order = order == null ? new Order() : new Order
            {
                Id = order.Id,
                Client = order.Client,
                Date = order.Date,
                Status = order.Status,
                Items = order.Items
            };
            _orderList = orderList;

            InitializeComponent();
            DataContext = _order;
        }

        //удалить
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedItem == null)
            {
                MessageBox.Show("Не выбрана позиция");
                return;
            }
            _order.Items.Remove(SelectedItem);
            _order.ObnovaTotal();
        }

        //добавит
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new OrderItemFormPage(_order));
        }

        //изменить
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (SelectedItem == null)
            {
                MessageBox.Show("Не выбрана позиция");
                return;
            }
            NavigationService.Navigate(new OrderItemFormPage(_order, SelectedItem));
        }

        //сохранит
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            _order.ObnovaTotal();

            if (_originalOrder == null)
            {
                _orderList.Add(_order);
            }
            else
            {
                _originalOrder.Id = _order.Id;
                _originalOrder.Client = _order.Client;
                _originalOrder.Date = _order.Date;
                _originalOrder.Status = _order.Status;
                _originalOrder.Items = _order.Items;
                _originalOrder.ObnovaTotal();
            }
            NavigationService.GoBack();
        }

        //назад
        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
