using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Logging.TraceListeners;
using Microsoft.Practices.EnterpriseLibrary.Logging;

namespace HelloWorld
{
    class MyTraceListener : CustomTraceListener
    {
        public override void Write(string message)
        {
            Console.Write(message);
        }

        public override void WriteLine(string message)
        {
            Console.WriteLine(message);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ConfigurationSourceBuilder();
            builder.ConfigureLogging()
                .WithOptions.DoNotRevertImpersonation()
                .LogToCategoryNamed("HelloWorldCategory")
                    .SendTo.Custom<MyTraceListener>("sample")
                    .FormatWith(new FormatterBuilder().TextFormatterNamed("Text Formatter").UsingTemplate(
                        "Message: {message}{newline}Category: {category}{newline}Priority: {priority}{newline}"));

            var config = new DictionaryConfigurationSource();
            builder.UpdateConfigurationWithReplace(config);

            EnterpriseLibraryContainer.Current = EnterpriseLibraryContainer.CreateDefaultContainer(config);

            var logger = EnterpriseLibraryContainer.Current.GetInstance<LogWriter>();
            logger.Write("Hello world", "HelloWorldCategory", 2, 9999);
        }
    }
}
