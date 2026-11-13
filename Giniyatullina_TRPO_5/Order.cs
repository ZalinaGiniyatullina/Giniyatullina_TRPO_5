using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace Giniyatullina_TRPO_5
{
    public class Order : INotifyPropertyChanged
    {
        private int _id;
        private string _client = "";
        private DateTime _date = DateTime.Now;
        private string _status = "";
        private ObservableCollection<OrderItem> _items = new();

        public int Id
        {
            get { return _id; }
            set { _id = value; OnPropertyChanged(); }
        }

        public string Client
        {
            get { return _client; }
            set { _client = value; OnPropertyChanged(); }
        }

        public DateTime Date
        {
            get { return _date; }
            set { _date = value; OnPropertyChanged(); }
        }

        public string Status
        {
            get { return _status; }
            set { _status = value; OnPropertyChanged(); }
        }

        public ObservableCollection<OrderItem> Items
        {
            get { return _items; }
            set { _items = value; OnPropertyChanged(); }
        }

        //общая стоимость заказа
        public decimal TotalAmount
        {
            get
            {
                decimal sum = 0;
                foreach (var item in Items)
                {
                    sum += item.TotalPrice;
                }
                return sum;
            }
            set { }
        }

        //чтобы TotalAmount обновлялся при изменении позиций
        public void ObnovaTotal()
        {
            OnPropertyChanged("TotalAmount");
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
