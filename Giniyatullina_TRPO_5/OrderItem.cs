using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace Giniyatullina_TRPO_5
{
    public class OrderItem : INotifyPropertyChanged
    {
        private string _name = "";
        private decimal _price;
        private int _count;

        public string Name
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged(); OnPropertyChanged("TotalPrice"); }
        }

        public decimal Price
        {
            get { return _price; }
            set { _price = value; OnPropertyChanged(); OnPropertyChanged("TotalPrice"); }
        }

        public int Count
        {
            get { return _count; }
            set { _count = value; OnPropertyChanged(); OnPropertyChanged("TotalPrice"); }
        }

        //стоимость позиции
        public decimal TotalPrice
        {
            get { return Price * Count; }
            set { }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
