namespace _02.LoggingApplicationBlockConfigVersion
{
    using System.Diagnostics;
    using System.Linq;
    using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
    using Microsoft.Practices.EnterpriseLibrary.Logging;

    class Program
    {
        static void Main(string[] args)
        {
            // 構成ファイル(app.config)から作成されたコンテナからLogWriterを取得
            var l = EnterpriseLibraryContainer.Current.GetInstance<LogWriter>();
            // Verbose～Criticalまでのログを出力
            l.Write(new LogEntry { Message = "VerboseMessage", Severity = TraceEventType.Verbose });
            l.Write(new LogEntry { Message = "InformationMessage", Severity = TraceEventType.Information });
            l.Write(new LogEntry { Message = "WarningMessage", Severity = TraceEventType.Warning });
            l.Write(new LogEntry { Message = "ErrorMessage", Severity = TraceEventType.Error });
            l.Write(new LogEntry { Message = "CriticalMessage", Severity = TraceEventType.Critical });
            // 出力したログの表示
            Process.Start("default.log");

            // ローリングさせるために大量のログを出力
            foreach (var i in Enumerable.Range(1, 100000))
            {
                l.Write("ログメッセージ", "Rolling");
                l.Write(new LogEntry { Message = "sample message", Categories = { "Rolling" } });
            }

            // イベントログの出力
            l.Write("EventLogMessage", "EventLog", 0, 0, TraceEventType.Information);
        }
    }
}
