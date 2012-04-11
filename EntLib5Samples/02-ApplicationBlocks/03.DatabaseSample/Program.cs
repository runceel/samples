namespace DatabaseSample
{
    using System;
    using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
    using Microsoft.Practices.EnterpriseLibrary.Data;

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
                    "SELECT Id, Name, Age FROM PERSON ORDER BY Age DESC");
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
                    "SELECT Id, Name, Age FROM PERSON WHERE NAME LIKE @p1 ORDER BY Age DESC");
                var people = accessor.Execute("%mu%");
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
}
