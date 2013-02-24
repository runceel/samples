namespace DataGridSample03
{
    // 性別を表す列挙型
    public enum Gender
    {
        None,
        Men,
        Women
    }

    // DataGridに表示するデータ
    public class Person
    {
        // 名前
        public string Name { get; set; }
        // 性別
        public Gender Gender { get; set; }
        // 認証済みユーザーかどうか
        public bool AuthMember { get; set; }
    }
}
