using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CoreCmdPlayground.LuceneNet.LuceneNet
{
    public interface IArticleLinkIndexer
    {
        void CreateIndex();
        void Search(string queryString);
    }

    public class ArticleLinkIndexer : IArticleLinkIndexer
    {
        public void CreateIndex()
        {
            //using (var uow = new UnitOfWork(DatabaseConfig.WorkingConnectionStringName))
            //{
            //    var articleLinkService = new ArticleLinkService(uow);
            //    var luceneSearcher = new LuceneNetSearcher();

            //    var latest3000ChineseLinks = articleLinkService.GetMany(l => l.Language.Id == 1).OrderByDescending(l => l.ScrapTime).Skip(0).Take(18000).ToList();
            //    luceneSearcher.CreateIndex(latest3000ChineseLinks);
            //}
        }

        public void Search(string queryString)
        {
            var luceneSearcher = new LuceneNetSearcher();
            luceneSearcher.Seach(queryString);
        }
    }
}
