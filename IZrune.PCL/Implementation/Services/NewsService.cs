using IZrune.PCL.Abstraction.Models;
using IZrune.PCL.Abstraction.Services;
using IZrune.PCL.Implementation.Models;
using IZrune.PCL.WebUtils;
using IZrune.TransferModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IZrune.PCL.Implementation.Services
{
    class NewsService : INewsService
    {
        public async Task<string> GetMoreInfoAsync()
        {
            try
            {
                var Data = await IzruneWebClient.Instance.GetDataAsync<MoreInfoDTO>("https://izrune.ge/api.php?op=getMoreInfo&hashcode=4bb9341135d155cf4f1079d7b146895b");
              
                

                return Data.info;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<IEnumerable<INews>> GetNewsAsync()
        {
            try
            {
                var Data = await IzruneWebClient.Instance.GetDataAsync<NewsRootDTO>("https://izrune.ge/api.php?op=getNews&hashcode=fa32492c23bfeebaf25dc3a817d91bfa");
                var jsn = Data.news;

                var Result = jsn.Select(i => new News()
                {
                    Category = i?.category,
                    Content = i?.content,
                    date = DateTime.Parse(i?.date),
                    Description = i?.description,
                    ImageUrl = i?.image_url,
                    Title = i?.title


                });


                return Result;
            }
            catch(Exception ex)
            {
                return null;
            }
        }
    }
}
