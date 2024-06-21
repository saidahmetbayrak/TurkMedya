namespace TurkMedya.Data.Detail
{
    public class Data
    {
        public NewsDetail NewsDetail { get; set; }
        public Ad HeaderAd { get; set; }
        public Ad FooterAd { get; set; }
        public Multimedia Multimedia { get; set; }
        public List<Item> ItemList { get; set; }
        public NewsDetail RelatedNews { get; set; }
        public Item Video { get; set; }
        public Item PhotoGallery { get; set; }
    }
}
