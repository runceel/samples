using Microsoft.Practices.Prism;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMSample02
{
    public class MainWindowViewModel : BindableBase, IDisposable
    {
        private string lhs;

        public string Lhs
        {
            get { return this.lhs; }
            set 
            { 
                this.SetProperty(ref this.lhs, value);
                this.ExecuteCommand.RaiseCanExecuteChanged();
            }
        }

        private string rhs;

        public string Rhs
        {
            get { return this.rhs; }
            set 
            { 
                this.SetProperty(ref this.rhs, value);
                this.ExecuteCommand.RaiseCanExecuteChanged();
            }
        }

        private double answer;

        public double Answer
        {
            get { return this.answer; }
            set { this.SetProperty(ref this.answer, value); }
        }

        private string message;

        public string Message
        {
            get { return this.message; }
            set { this.SetProperty(ref this.message, value); }
        }

        public OperatorTypeViewModel[] OperatorTypes { get; private set; }

        private OperatorTypeViewModel selectedOperatorType;

        public OperatorTypeViewModel SelectedOperatorType
        {
            get { return this.selectedOperatorType; }
            set 
            { 
                this.SetProperty(ref this.selectedOperatorType, value);
                this.ExecuteCommand.RaiseCanExecuteChanged();
            }
        }

        public DelegateCommand ExecuteCommand { get; private set; }

        private AppContext appContext = new AppContext();

        public MainWindowViewModel()
        {
            this.OperatorTypes = OperatorTypeViewModel.OperatorTypes;

            this.ExecuteCommand = new DelegateCommand(this.Execute, this.CanExecute);

            // Modelの監視
            this.appContext.PropertyChanged += this.AppContextPropertyChanged;
            this.appContext.Calc.PropertyChanged += this.CalcPropertyChanged;
        }

        private void CalcPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Answer")
            {
                this.Answer = this.appContext.Calc.Answer;
            }
        }

        private void AppContextPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Message")
            {
                this.Message = this.appContext.Message;
            }
        }

        private void Execute()
        {
            this.appContext.Calc.Lhs = double.Parse(this.Lhs);
            this.appContext.Calc.Rhs = double.Parse(this.Rhs);
            this.appContext.Calc.OperatorType = this.SelectedOperatorType.OperatorType;
            this.appContext.Calc.Execute();
        }

        private bool CanExecute()
        {
            double dummy;
            if (!double.TryParse(this.Lhs, out dummy))
            {
                return false;
            }

            if (!double.TryParse(this.Rhs, out dummy))
            {
                return false;
            }

            if (this.SelectedOperatorType == null)
            {
                return false;
            }

            return true;
        }

        public void Dispose()
        {
            // Modelの監視解除
            this.appContext.PropertyChanged -= this.AppContextPropertyChanged;
            this.appContext.Calc.PropertyChanged -= this.CalcPropertyChanged;
        }
    }
}
