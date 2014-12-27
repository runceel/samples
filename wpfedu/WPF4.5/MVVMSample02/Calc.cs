using Microsoft.Practices.Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMSample02
{
    public class Calc : BindableBase
    {
        private AppContext appContext;

        private double lhs;

        public double Lhs
        {
            get { return this.lhs; }
            set { this.SetProperty(ref this.lhs, value); }
        }

        private double rhs;

        public double Rhs
        {
            get { return this.rhs; }
            set { this.SetProperty(ref this.rhs, value); }
        }

        private OperatorType operatorType;

        public OperatorType OperatorType
        {
            get { return this.operatorType; }
            set { this.SetProperty(ref this.operatorType, value); }
        }

        private double answer;

        public double Answer
        {
            get { return this.answer; }
            set { this.SetProperty(ref this.answer, value); }
        }

        public Calc(AppContext appContext)
        {
            this.appContext = appContext;
        }

        public void Execute()
        {
            switch (this.OperatorType)
            {
                case OperatorType.Add:
                    this.Answer = this.Lhs + this.Rhs;
                    break;
                case OperatorType.Sub:
                    this.Answer = this.Lhs - this.Rhs;
                    break;
                case OperatorType.Mul:
                    this.Answer = this.Lhs * this.Rhs;
                    break;
                case OperatorType.Div:
                    if (this.Rhs == 0) 
                    {
                        this.appContext.Message = "0除算エラー";
                        return;
                    }
                    this.Answer = this.Lhs / this.Rhs;
                    break;
                default:
                    throw new InvalidOperationException();
            }
        }
    }

    public enum OperatorType
    {
        Add,
        Sub,
        Mul,
        Div,
    }
}
