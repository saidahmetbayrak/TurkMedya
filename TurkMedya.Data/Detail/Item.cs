namespace TurkMedya.Data.Detail
{
    public class Item
    {
        public List<Item> ItemList { get; set; }
        public string ItemId { get; set; }
        public string Title { get; set; }
        public string ImageUrl { get; set; }
        public string ItemType { get; set; }
        public bool TitleVisible { get; set; }
        public string ShortText { get; set; }
        public string BodyText { get; set; }
        public string VideoUrl { get; set; }
    }
}
