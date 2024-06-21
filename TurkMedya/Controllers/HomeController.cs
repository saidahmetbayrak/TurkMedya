using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TurkMedya.Models;
using TurkMedya.Service;

namespace TurkMedya.Controllers
{
    public class HomeController : Controller
    {
        private readonly INewsService _newsService;

        // INewsService ba��ml�l���n�n constructor ile al�nmas�
        public HomeController(INewsService newsService)
        {
            _newsService = newsService;
        }

        // Anasayfa i�in Index action metodu
        public async Task<IActionResult> Index(int page = 1, string category = null, string keyword = null)
        {
            // Haberleri news service kullanarak al
            var data = await _newsService.GetAnasayfaAsync();

            // Haberlerden kategorileri ��kar ve benzersiz bir liste olu�tur
            var categories = data.Data
                .SelectMany(s => s.ItemList)
                .Select(n => n.Category.Title)
                .Distinct()
                .ToList();

            // Kategorileri, mevcut kategoriyi ve anahtar kelimeyi ViewBag'e ekle
            ViewBag.Categories = categories;
            ViewBag.CurrentCategory = category;
            ViewBag.Keyword = keyword;

            // Se�ilen kategori ve anahtar kelimeye g�re haberleri filtrele
            var filteredNews = data.Data
                .SelectMany(s => s.ItemList)
                .Where(n => string.IsNullOrEmpty(category) || n.Category.Title.Equals(category, StringComparison.OrdinalIgnoreCase))
                .Where(n => string.IsNullOrEmpty(keyword) || n.Title.Contains(keyword, StringComparison.OrdinalIgnoreCase))
                .ToList();

            // Sayfa ba��na g�sterilecek haber say�s�n� belirle ve sayfalama i�lemini ger�ekle�tir
            int pageSize = 5;
            var pagedNews = filteredNews.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            // Toplam sayfa say�s�n� hesapla ve ViewBag'e ekle
            ViewBag.TotalPages = Math.Ceiling((double)filteredNews.Count / pageSize);
            ViewBag.CurrentPage = page;

            // Filtrelenmi� ve sayfalara ayr�lm�� haberleri view'e d�nd�r
            return View(pagedNews);
        }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
