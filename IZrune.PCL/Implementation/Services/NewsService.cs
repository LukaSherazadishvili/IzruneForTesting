using IZrune.PCL.Abstraction.Models;
using IZrune.PCL.Abstraction.Services;
using IZrune.PCL.WebUtils;
using IZrune.TransferModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IZrune.PCL.Implementation.Services
{
    class NewsService : INewsService
    {
        public async Task<IEnumerable<INews>> GetNewsAsync()
        {
            var Data = await IzruneWebClient.Instance.GetDataAsync<NewsRootDTO>("http://izrune.ge/api.php?op=getTest&hashcode=26e0c75cd4f8b1242b620a46aa701431");
            var jsn = Data.news;
            return null;
        }
    }
}
