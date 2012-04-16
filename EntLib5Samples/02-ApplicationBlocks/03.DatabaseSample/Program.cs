namespace DatabaseSample
{
    using System;
    using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
    using Microsoft.Practices.EnterpriseLibrary.Data;
    using System.Data.Common;
    using System.Linq;
    using System.Data;
    using System.Transactions;
    using System.Data.SqlClient;
    using System.Data.SqlServerCe;
    using System.Configuration;
    using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
    using EntLib5Sample.Commons;

    class Program
    {
        static void Main(string[] args)
        {
            // 単純にSQL文を実行
            Console.WriteLine("---------------------------------------");
            {
                // Enterprise LibraryのコンテナからDatabaseクラスのインスタンスを取得
                var database = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                // SQL文を発行してデータを格納
                var people = database.ExecuteSqlStringAccessor<Person>(
                    "SELECT Id, Name, Age FROM PERSON ORDER BY ID DESC");
                // 結果を表示
                foreach (var p in people)
                {
                    Console.WriteLine("Id: {0}, Name: {1}, Age: {2}", p.Id, p.Name, p.Age);
                }
            }
            // パラメータつきのSQL文
            Console.WriteLine("---------------------------------------");
            {
                // Enterprise LibraryのコンテナからDatabaseクラスのインスタンスを取得
                var database = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                // SQL文を発行してデータを格納
                var accessor = database.CreateSqlStringAccessor<Person>(
                    // パラメータつきのSQL文
                    "SELECT Id, Name, Age FROM PERSON WHERE NAME LIKE @p1 ORDER BY ID DESC",
                    // パラメータのマッピングルール
                    SequenceParameterMapper.Default);
                // パラメータを指定して実行
                var people = accessor.Execute("%mu%");
                // 結果を表示
                foreach (var p in people)
                {
                    Console.WriteLine("Id: {0}, Name: {1}, Age: {2}", p.Id, p.Name, p.Age);
                }
            }
            // データの登録
            Console.WriteLine("---------------------------------------");
            {
                // Enterprise LibraryのコンテナからDatabaseクラスのインスタンスを取得
                var database = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                // コネクションを作成
                using (var conn = database.CreateConnection())
                {
                    conn.Open();
                    // トランザクションを開始
                    using (var tran = conn.BeginTransaction())
                    {
                        // 登録対象のデータ
                        var p = new Person { Name = "hanami", Age = 100 };
                        // コマンドをSQLから作成
                        var command = database.GetSqlStringCommand("INSERT INTO PERSON(NAME, AGE) VALUES(@p1, @p2)");
                        // パラメータを追加
                        database.AddInParameter(command, "p1", DbType.String, p.Name);
                        database.AddInParameter(command, "p2", DbType.Int32, p.Age);
                        // トランザクションを指定してコマンドを実行
                        var count = database.ExecuteNonQuery(command, tran);

                        // DB側でふられたIDを取得
                        var newId = database.ExecuteScalar(tran, CommandType.Text, "SELECT @@IDENTITY");
                        // 登録件数と、登録時にふられたIDを表示
                        Console.WriteLine("inserted: {0}, newId: {1}", count, newId);
                        // コミット
                        tran.Commit();
                    }
                    conn.Close();
                }
            }
            // Transaction
            Console.WriteLine("---------------------------------------");
            {
                // Enterprise LibraryのコンテナからDatabaseクラスのインスタンスを取得
                using (var tc = new TransactionScope())
                {
                    var database1 = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                    Console.WriteLine("最初のdatabaseオブジェクト取得 HashCode: {0}", database1.GetHashCode());
                    for (int i = 0; i < 2; i++)
                    {
                        // 登録対象のデータ
                        var p = new Person { Name = "hanami", Age = 100 };
                        // コマンドをSQLから作成
                        var command = database1.GetSqlStringCommand("INSERT INTO PERSON(NAME, AGE) VALUES(@p1, @p2)");
                        // パラメータを追加
                        database1.AddInParameter(command, "p1", DbType.String, p.Name);
                        database1.AddInParameter(command, "p2", DbType.Int32, p.Age);
                        // トランザクションを指定してコマンドを実行
                        var count = database1.ExecuteNonQuery(command);

                        // DB側でふられたIDを取得
                        var newId = database1.ExecuteScalar(CommandType.Text, "SELECT @@IDENTITY");
                        // 登録件数と、登録時にふられたIDを表示
                        Console.WriteLine("database1: inserted: {0}, newId: {1}", count, newId);
                    }
                    var database2 = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                    Console.WriteLine("2つ目のdatabaseオブジェクト取得 HashCode: {0}", database2.GetHashCode());
                    for (int i = 0; i < 2; i++)
                    {
                        // 登録対象のデータ
                        var p = new Person { Name = "sakurai", Age = 100 };
                        // コマンドをSQLから作成
                        var command = database2.GetSqlStringCommand("INSERT INTO PERSON(NAME, AGE) VALUES(@p1, @p2)");
                        // パラメータを追加
                        database2.AddInParameter(command, "p1", DbType.String, p.Name);
                        database2.AddInParameter(command, "p2", DbType.Int32, p.Age);
                        // トランザクションを指定してコマンドを実行
                        var count = database1.ExecuteNonQuery(command);

                        // DB側でふられたIDを取得
                        var newId = database2.ExecuteScalar(CommandType.Text, "SELECT @@IDENTITY");
                        // 登録件数と、登録時にふられたIDを表示
                        Console.WriteLine("database2: inserted: {0}, newId: {1}", count, newId);
                    }
                    tc.Complete();
                }
            }
            // Transaction
            // 例外が出るのでコメントアウト
            //Console.WriteLine("---------------------------------------");
            //{
            //    using (var tc = new TransactionScope())
            //    {
            //        // 単純にTransactionScope内で2つのコネクションを開いて閉じる
            //        var conn1 = new SqlCeConnection(ConfigurationManager.ConnectionStrings["SqlCe"].ConnectionString);
            //        conn1.Open();
            //        conn1.Close();
            //        var conn2 = new SqlCeConnection(ConfigurationManager.ConnectionStrings["SqlCe"].ConnectionString);
            //        conn2.Open();
            //        conn2.Close();
            //    }
            //}
            // データの登録ヘルパーメソッド
            Console.WriteLine("---------------------------------------");
            {
                using (var tc = new TransactionScope())
                {
                    // Enterprise LibraryのコンテナからDatabaseクラスのインスタンスを取得
                    var database = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                    // 登録対象のデータ
                    var p = new Person { Name = "hanami", Age = 100 };
                    // 拡張メソッドを使って登録
                    var count = database.ExecuteUpdate(
                        "INSERT INTO PERSON(NAME, AGE) VALUES(@p1, @p2)",
                        p.Name, p.Age);
                    // DB側で割り振られたIDを取得
                    var newId = (decimal)database.ExecuteScalar(
                        CommandType.Text, "SELECT @@IDENTITY");

                    // 結果を表示
                    Console.WriteLine("inserted: {0}, newId {1}", count, newId);
                    tc.Complete();
                }
            }
            Console.WriteLine("---------------------------------------");
            {
                var builder = new ConfigurationSourceBuilder();
                builder.ConfigureData()
                    // 使用する接続文字列を取得
                    .ForDatabaseNamed("SqlCe")
                    .AsDefault();
                // Enterprise LibraryのコンテナからDatabaseクラスのインスタンスを取得
                var database = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                // SQL文を発行してデータを格納
                var people = database.ExecuteSqlStringAccessor<Person>(
                    "SELECT Id, Name, Age FROM PERSON ORDER BY ID DESC");
                // 結果を表示
                foreach (var p in people)
                {
                    Console.WriteLine("Id: {0}, Name: {1}, Age: {2}", p.Id, p.Name, p.Age);
                }
            }
        }
    }

    class Person
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
    }

    /// <summary>
    /// Databaseクラスへの拡張メソッド
    /// </summary>
    static class DatabaseExtensions
    {
        // IParameterMapperに指定したパラメータマッピングルールでSQL文を実行する
        public static int ExecuteUpdate(
            this Database self,
            string sql,
            IParameterMapper mapper,
            params object[] parameters)
        {
            using (var command = self.GetSqlStringCommand(sql))
            {
                mapper.AssignParameters(command, parameters);
                return self.ExecuteNonQuery(command);
            }
        }

        // SequenceParameterMapperのパラメータマッピングルールでSQL文を実行する
        public static int ExecuteUpdate(
            this Database self,
            string sql,
            params object[] parameters)
        {
            return self.ExecuteUpdate(sql, SequenceParameterMapper.Default, parameters);
        }
    }

    /// <summary>
    /// @p1, @p2, @p3という名前の順番でパラメータをマッピングするパラメータマッパー
    /// </summary>
    class SequenceParameterMapper : IParameterMapper
    {
        /// <summary>
        /// デフォルトインスタンス
        /// </summary>
        public static readonly IParameterMapper Default = new SequenceParameterMapper();

        public void AssignParameters(DbCommand command, object[] parameterValues)
        {
            // 引数で渡された値をCommandParameterへ変換
            var parameters = parameterValues
                .Select((value, index) =>
                {
                    var p = command.CreateParameter();
                    p.ParameterName = "p" + (index + 1);
                    p.Value = value;
                    return p;
                })
                .ToArray();
            // コマンドにパラメータを追加
            command.Parameters.AddRange(parameters);
        }
    }

}
