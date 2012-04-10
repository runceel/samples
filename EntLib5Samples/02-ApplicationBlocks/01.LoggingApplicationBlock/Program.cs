namespace LoggingApplicationBlock
{
    using System.Diagnostics;
    using EntLib5Sample.Commons;
    using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
    using Microsoft.Practices.EnterpriseLibrary.Logging;
    using Microsoft.Practices.EnterpriseLibrary.Logging.TraceListeners;
    using System.Linq;

    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ConfigurationSourceBuilder();
            builder.ConfigureLogging()
                .WithOptions.FilterOnPriority("aaa").UpToPriority(10)
                // 名前を付けてログの定義を開始
                .LogToCategoryNamed("General")
                    // WithOptionsで追加オプション
                    // ここではGeneralをデフォルトのカテゴリとして設定
                    .WithOptions.SetAsDefaultCategory()
                    // フラットファイルに出力ファイル名はdefault.log
                    .SendTo.FlatFile("FlatFileListener").ToFile("default.log")
                    // フィルタリング（警告以上を表示する）
                    .Filter(SourceLevels.Warning)
                    // ログのフォーマットを指定
                    .FormatWith(new FormatterBuilder()
                        // フォーマッタの名前を指定
                        .TextFormatterNamed("LogFormatter")
                        // フォーマットを指定
                        .UsingTemplate("{timestamp(local:yyyy/MM/dd HH:mm:ss.fff)}:  {severity}: {message}"))
                // Rollingという名前でログの定義を開始
                .LogToCategoryNamed("Rolling")
                    // SendTo.RollingFileで一定の条件を満たしたらローリング
                    .SendTo.RollingFile("RollingFileListener")
                        // 1000KBでローリング
                        .RollAfterSize(1000)
                        // 1分間隔でローリング
                        .RollEvery(RollInterval.Minute)
                        // ローリングしたファイルにタイムスタンプをつける
                        .UseTimeStampPattern("yyyyMMddHHmmssfff")
                        // 10世代管理
                        .CleanUpArchivedFilesWhenMoreThan(10)
                        // ファイル名はrolling.log
                        .ToFile("rolling.log")
                // EventLogという名前でログの定義を開始
                .LogToCategoryNamed("EventLog")
                    // EventLogに送信するEventLogListener
                    .SendTo.EventLog("EventLogListener")
                    // ソースはApplication
                    .UsingEventLogSource("Application");
                    

            EnterpriseLibraryContainer.Current = builder.CreateContainer();

            var l = EnterpriseLibraryContainer.Current.GetInstance<LogWriter>();
            // Verbose～Criticalまでのログを出力
            l.Write(new LogEntry { Message = "VerboseMessage", Severity = TraceEventType.Verbose});
            l.Write(new LogEntry { Message = "InformationMessage", Severity = TraceEventType.Information});
            l.Write(new LogEntry { Message = "WarningMessage", Severity = TraceEventType.Warning});
            l.Write(new LogEntry { Message = "ErrorMessage", Severity = TraceEventType.Error});
            l.Write(new LogEntry { Message = "CriticalMessage", Severity = TraceEventType.Critical});

            Process.Start("default.log");

            foreach (var i in Enumerable.Range(1, 100000))
            {
                l.Write("ログメッセージ", "Rolling");
                l.Write(new LogEntry { Message = "sample message", Categories = { "Rolling" } });
            }

            l.Write("EventLogMessage", "EventLog", 0, 0, TraceEventType.Information);
        }
    }
}
