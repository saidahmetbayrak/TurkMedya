using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TurkMedya.Models;
using TurkMedya.Service;

namespace TurkMedya.Controllers
{
    public class HomeController : Controller
    {
        private readonly INewsService _newsService;

        // INewsService baðýmlýlýðýnýn constructor ile alýnmasý
        public HomeController(INewsService newsService)
        {
            _newsService = newsService;
        }

        // Anasayfa için Index action metodu
        public async Task<IActionResult> Index(int page = 1, string category = null, string keyword = null)
        {
            // Haberleri news service kullanarak al
            var data = await _newsService.GetAnasayfaAsync();

            // Haberlerden kategorileri çýkar ve benzersiz bir liste oluþtur
            var categories = data.Data
                .SelectMany(s => s.ItemList)
                .Select(n => n.Category.Title)
                .Distinct()
                .ToList();

            // Kategorileri, mevcut kategoriyi ve anahtar kelimeyi ViewBag'e ekle
            ViewBag.Categories = categories;
            ViewBag.CurrentCategory = category;
            ViewBag.Keyword = keyword;

            // Seçilen kategori ve anahtar kelimeye göre haberleri filtrele
            var filteredNews = data.Data
                .SelectMany(s => s.ItemList)
                .Where(n => string.IsNullOrEmpty(category) || n.Category.Title.Equals(category, StringComparison.OrdinalIgnoreCase))
                .Where(n => string.IsNullOrEmpty(keyword) || n.Title.Contains(keyword, StringComparison.OrdinalIgnoreCase))
                .ToList();

            // Sayfa baþýna gösterilecek haber sayýsýný belirle ve sayfalama iþlemini gerçekleþtir
            int pageSize = 5;
            var pagedNews = filteredNews.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            // Toplam sayfa sayýsýný hesapla ve ViewBag'e ekle
            ViewBag.TotalPages = Math.Ceiling((double)filteredNews.Count / pageSize);
            ViewBag.CurrentPage = page;

            // Filtrelenmiþ ve sayfalara ayrýlmýþ haberleri view'e döndür
            return View(pagedNews);
        }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
