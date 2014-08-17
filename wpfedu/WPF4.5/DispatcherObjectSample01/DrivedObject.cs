using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace DispatcherObjectSample01
{
    public class DrivedObject : DispatcherObject
    {
        public void DoSomething()
        {
            // UIスレッドからのアクセスかチェックする
            this.VerifyAccess();
            Debug.WriteLine("DoSomething");
        }
    }
}
