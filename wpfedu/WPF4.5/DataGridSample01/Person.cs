using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace DataGridSample01
{
    public class Person : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private bool SetProperty<T>(ref T holder, T value, [CallerMemberName] string propertyName = "")
        {
            if (Equals(holder, value))
            {
                return false;
            }

            holder = value;
            var h = this.PropertyChanged;
            if (h != null)
            {
                h(this, new PropertyChangedEventArgs(propertyName));
            }
            return true;
        }

        private int age;

        public int Age
        {
            get { return this.age; }
            set { this.SetProperty(ref this.age, value); }
        }

        private string name;

        public string Name
        {
            get { return this.name; }
            set { this.SetProperty(ref this.name, value); }
        }

        private string kana;

        public string Kana
        {
            get { return this.kana; }
            set { this.SetProperty(ref this.kana, value); }
        }

        private string address;

        public string Address
        {
            get { return this.address; }
            set { this.SetProperty(ref this.address, value); }
        }

        private string phone;

        public string Phone
        {
            get { return this.phone; }
            set { this.SetProperty(ref this.phone, value); }
        }

    }
}
