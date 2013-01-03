namespace CollectionXaml
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Windows.Markup;

    // Childrenプロパティをコンテンツプロパティとして指定
    [ContentProperty("Children")]
    public class Item
    {
        public Item()
        {
            this.Children = new ItemCollection();
        }

        public string Id { get; set; }
        public ItemCollection Children { get; set; }
    }

    public class ItemCollection : Collection<Item> { }
}