using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для OrderItemFormPage.xaml
    /// </summary>
    public partial class OrderItemFormPage : Page
    {
        private Order _order;
        private OrderItem _item;
        private OrderItem? _originalItem;
        public OrderItemFormPage(Order order, OrderItem? item = null)
        {
            _order = order;
            _originalItem = item;
            _item = item == null ? new OrderItem() : new OrderItem
            {
                Name = item.Name,
                Price = item.Price,
                Count = item.Count
            };

            InitializeComponent();
            DataContext = _item;
        }

        //сохранить
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (_originalItem == null)
            {
                _order.Items.Add(_item);
            }
            else
            {
                _originalItem.Name = _item.Name;
                _originalItem.Price = _item.Price;
                _originalItem.Count = _item.Count;
            }
            _order.ObnovaTotal();
            NavigationService.GoBack();
        }

        //назад
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
