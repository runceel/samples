using System.Diagnostics;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Logging;

namespace HelloWorld
{
    class Program
    {
        static void Main(string[] args)
        {
            // 構成情報を組み立てる
            var builder = new ConfigurationSourceBuilder();
            builder.ConfigureLogging()
                .SpecialSources
                .AllEventsCategory
                    .SendTo
                    .FlatFile("FlatFileListener")
                    .FormatWith(
                        new FormatterBuilder()
                            .TextFormatterNamed("TextFormatter")
                            .UsingTemplate("{timestamp(local:yyyy/MM/dd HH:mm:ss.fff)}: {message}"))
                    .ToFile("output.txt");

            // 組み立てた構成情報からConfigurationSourceを作成
            var config = new DictionaryConfigurationSource();
            builder.UpdateConfigurationWithReplace(config);

            // 構成情報を元にEnterpriseLibraryのコンテナの初期化
            EnterpriseLibraryContainer.Current = EnterpriseLibraryContainer.CreateDefaultContainer(config);

            // EnterpriseLibraryのコンテナからLogging Application BlockのLog書き込み部品を取得
            var logger = EnterpriseLibraryContainer.Current.GetInstance<LogWriter>();
            // ログに出力する
            logger.Write("Hello world");

            // ログを表示
            Process.Start("output.txt");
        }
    }
}
