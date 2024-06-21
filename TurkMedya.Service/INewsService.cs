using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurkMedya.Data.Detail;
using TurkMedya.Data.News;

namespace TurkMedya.Service
{
    public interface INewsService
    {
        Task<Anasayfa> GetAnasayfaAsync();
        Task<Detay> GetDetayAsync(string id);
    }

}
