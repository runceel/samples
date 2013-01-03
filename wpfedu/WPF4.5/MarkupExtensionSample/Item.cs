namespace MarkupExtensionSample
{
    using System;
    using System.Windows.Markup;

    public class Item
    {
        public string Id { get; set; }
    }

    // Idを提供するマークアップ拡張
    public class IdProviderExtension : MarkupExtension
    {
        // Idのプリフィックス
        public string Prefix { get; set; }

        // 値を提供するロジックを記述する
        public override object ProvideValue(System.IServiceProvider serviceProvider)
        {
            return Prefix + Guid.NewGuid().ToString();
        }
    }
}