namespace CollectionXaml
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    public class Item
    {
        public Item()
        {
            this.Children = new ItemCollection();
        }

        public string Id { get; set; }
        // コレクション型のプロパティ
        public ItemCollection Children { get; set; }
    }

    public class ItemCollection : Collection<Item> { }
}