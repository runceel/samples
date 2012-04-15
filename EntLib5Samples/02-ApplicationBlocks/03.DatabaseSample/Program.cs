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

    class Program
    {
        static void Main(string[] args)
        {
            // 単純にSQL文を実行
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
            {
                // Enterprise LibraryのコンテナからDatabaseクラスのインスタンスを取得
                var database = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                // SQL文を発行してデータを格納
                var accessor = database.CreateSqlStringAccessor<Person>(
                    // パラメータつきのSQL文
                    "SELECT Id, Name, Age FROM PERSON WHERE NAME LIKE @p0 ORDER BY ID DESC",
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
            {
                // Enterprise LibraryのコンテナからDatabaseクラスのインスタンスを取得
                var database = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                var p = new Person { Name = "hanami", Age = 100 };
                var count = database.ExecuteUpdate(
                    "INSERT INTO PERSON(NAME, AGE) VALUES(@p0, @p1)",
                    SequenceParameterMapper.Default,
                    p.Name, p.Age);

                Console.WriteLine(count);
            }
            // Transaction
            {
                // Enterprise LibraryのコンテナからDatabaseクラスのインスタンスを取得
                using (var tc = new TransactionScope(TransactionScopeOption.Required))
                {
                    {
                        var database = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                        for (int i = 0; i < 100; i++)
                        {
                            var p = new Person { Name = "inoue", Age = 100 };
                            var count = database.ExecuteUpdate(
                                "INSERT INTO PERSON(NAME, AGE) VALUES(@p0, @p1)",
                                SequenceParameterMapper.Default,
                                p.Name, p.Age);
                        }
                    }
                    {
                        var database = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                        for (int i = 0; i < 100; i++)
                        {
                            var p = new Person { Name = "inoue", Age = 100 };
                            var count = database.ExecuteUpdate(
                                "INSERT INTO PERSON(NAME, AGE) VALUES(@p0, @p1)",
                                SequenceParameterMapper.Default,
                                p.Name, p.Age);
                        }
                    }
                    tc.Complete();
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

    static class DatabaseExtensions
    {
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
    }

    /// <summary>
    /// @p0, @p1, @p2という名前の順番でパラメータをマッピングするパラメータマッパー
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
                    p.ParameterName = "p" + index;
                    p.Value = value;
                    return p;
                })
                .ToArray();
            // コマンドにパラメータを追加
            command.Parameters.AddRange(parameters);
        }
    }

}
