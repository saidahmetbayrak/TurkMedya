namespace TurkMedya.Data.News
{
    public class Anasayfa
    {
        public int ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public List<Section> Data { get; set; }
    }
}
