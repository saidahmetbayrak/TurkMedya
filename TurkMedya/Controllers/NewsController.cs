using Microsoft.AspNetCore.Mvc;
using TurkMedya.Service;

namespace TurkMedya.Controllers
{
    public class NewsController : Controller
    {
        private readonly INewsService _newsService;

        // INewsService bağımlılığının constructor ile alınması
        public NewsController(INewsService newsService)
        {
            _newsService = newsService;
        }

        // Haber detaylarını gösteren Details action metodu
        public async Task<IActionResult> Details(string id)
        {
            // Haber detaylarını news service kullanarak al
            var newsDetail = await _newsService.GetDetayAsync(id);

            // Haber detaylarını view'e döndür
            return View(newsDetail);
        }
    }

}
