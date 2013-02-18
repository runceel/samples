namespace DataGridSample02
{
    // 性別
    public enum Gender
    {
        None,
        Men,
        Women
    }

    // DataGridに表示するデータ
    public class Person
    {
        public string Name { get; set; }
        public Gender Gender { get; set; }
        public int Age { get; set; }
        public bool AuthMember { get; set; }
    }
}
