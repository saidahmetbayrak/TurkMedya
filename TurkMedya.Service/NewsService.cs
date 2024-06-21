using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurkMedya.Data.Detail;
using TurkMedya.Data.News;

namespace TurkMedya.Service
{
    public class NewsService : INewsService
    {
        private readonly HttpClient _httpClient;

        // HttpClient bağımlılığının constructor ile alınması
        public NewsService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // Anasayfa verilerini almak için asenkron metod
        public async Task<Anasayfa> GetAnasayfaAsync()
        {
            // Veriyi URL'den asenkron olarak al
            var response = await _httpClient.GetStringAsync("https://www.turkmedya.com.tr/anasayfa.json");

            // Alınan JSON verisini Anasayfa nesnesine dönüştür ve döndür
            return JsonConvert.DeserializeObject<Anasayfa>(response);
        }

        // Belirli bir haberin detaylarını almak için asenkron metod
        public async Task<Detay> GetDetayAsync(string id)
        {
            // Veriyi URL'den asenkron olarak al
            var response = await _httpClient.GetStringAsync($"https://www.turkmedya.com.tr/detay.json?itemId={id}");

            // Alınan JSON verisini Detay nesnesine dönüştür ve döndür
            return JsonConvert.DeserializeObject<Detay>(response);
        }
    }

}
