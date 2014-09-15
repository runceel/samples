using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DataBindingSample04
{
    public class Person : INotifyPropertyChanged, INotifyDataErrorInfo
    {
        // INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        private void SetProperty<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            field = value;
            var h = this.PropertyChanged;
            if (h != null) { h(this, new PropertyChangedEventArgs(propertyName)); }
        }

        // INotifyErrorsInfo
        private Dictionary<string, IEnumerable> errors = new Dictionary<string, IEnumerable>();
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;
        private void OnErrorsChanged([CallerMemberName] string propertyName = null)
        {
            var h = this.ErrorsChanged;
            if (h != null) { h(this, new DataErrorsChangedEventArgs(propertyName)); }
        }

        public IEnumerable GetErrors(string propertyName)
        {
            IEnumerable error = null;
            this.errors.TryGetValue(propertyName, out error);
            return error;
        }

        public bool HasErrors
        {
            get { return this.errors.Values.Any(e => e != null); }
        }

        private string name;

        public string Name
        {
            get { return this.name; }
            set 
            { 
                this.SetProperty(ref this.name, value); 
                if (string.IsNullOrEmpty(value))
                {
                    this.errors["Name"] = new[] {"名前を入力してください" };
                }
                else
                {
                    this.errors["Name"] = null;
                }
                this.OnErrorsChanged();
            }
        }

        private int age;

        public int Age
        {
            get { return this.age; }
            set 
            { 
                this.SetProperty(ref this.age, value); 
                if (value < 0)
                {
                    this.errors["Age"] = new[] { "年齢は0以上を入力してください" };
                }
                else
                {
                    this.errors["Age"] = null;
                }
                this.OnErrorsChanged();
            }
        }

    }
}
