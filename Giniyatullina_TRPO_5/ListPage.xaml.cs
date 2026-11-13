using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Text.Json;
using System.IO;
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
    /// Логика взаимодействия для ListPage.xaml
    /// </summary>
    public partial class ListPage : Page
    {
        public ObservableCollection<Order> Orders { get; set; } = new();
        public Order? SelectedOrder { get; set; }
        public ListPage()
        {
            InitializeComponent();
            DataContext = this;
            LoadFromJson();
        }

        //загрузка из JSON
        private void LoadFromJson()
        {
            string path = "orders.json";
            if (!File.Exists(path))
            {
                return;
            }

            try
            {
                string json = File.ReadAllText(path);
                var loaded = JsonSerializer.Deserialize<ObservableCollection<Order>>(json);
                if (loaded != null)
                {
                    for (int i = 0; i < loaded.Count; i++)
                    {
                        var order = loaded[i];

                        if (order.Items == null)
                        {
                            order.Items = new ObservableCollection<OrderItem>();
                        }

                        Orders.Add(order);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки JSON: {ex.Message}");
            }
        }

        //добавить
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new OrderFormPage(Orders));
        }
        
        //удалить
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (SelectedOrder == null)
            {
                MessageBox.Show("Пользователь не выбран");
                return;
            }

            var result = MessageBox.Show(
                "Вы действительно хотите удалить запись?",
                "Подтверждение",
                MessageBoxButton.YesNo);

            if (result == MessageBoxResult.Yes)
            {
                Orders.Remove(SelectedOrder);
            }
        }

        //сохранить в json
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(Orders, options);
            File.WriteAllText("orders.json", json);
            MessageBox.Show("Сохранено в orders.json");
        }

        //редактировать 2 клика
        private void ListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (SelectedOrder == null)
            {
                return;
            }
            NavigationService.Navigate(new OrderFormPage(Orders, SelectedOrder));
        }
    }
}
