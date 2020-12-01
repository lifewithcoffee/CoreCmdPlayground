using Lucene.Net.Documents;
using Lucene.Net.Index;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lucene.Net.Store;
using System.IO;
using Lucene.Net.Analysis.Cn;
using static Lucene.Net.Search.SimpleFacetedSearch;
using Lucene.Net.Search;
using Lucene.Net.QueryParsers;
using CoreCmdPlayground.LuceneNet.Models;

namespace CoreCmdPlayground.LuceneNet.LuceneNet
{
    public interface ILuceneSearcher
    {
        void CreateIndex(IEnumerable<ArticleLink> articleLinks);
        List<ArticleLinkInfo> Seach(String queryString, int fetchTopResult = -1);
    }

    public class LuceneNetSearcher : ILuceneSearcher
    {
        const string id_field = "Id";
        const string title_field = "Title";
        const string scraptime_field = "ScrapTime";
        const string url_field = "Url";

        string indexDir = @"C:\LuceneNetIndex";

        public LuceneNetSearcher(string indexDir = "")
        {
            if(!string.IsNullOrWhiteSpace(indexDir))
                this.indexDir = indexDir;
        }

        public void CreateIndex(IEnumerable<ArticleLink> articleLinks)
        {
            using (IndexWriter writer = new IndexWriter(FSDirectory.Open(new DirectoryInfo(this.indexDir))
                                                       , new ChineseAnalyzer()
                                                       , false // always append index
                                                       , IndexWriter.MaxFieldLength.LIMITED
                                                       ))
            {
                try
                {
                    foreach (var articleLink in articleLinks)
                    {
                        Document doc = new Document();


                        /* Field.Index.NOT_ANALYZED : 表示索引字段的时候不使用分词器，ANALYZED则表示使用
                         * 
                         * if a field is stored (set "Field.Store.YES"), its value can be fetched later:
                         *      myrow[0]=doc.Get("id").ToString();
                         *      myrow[1]=doc.Get("title").ToString();
                         */
                        doc.Add(new Field(id_field, articleLink.Id.ToString(), Field.Store.YES, Field.Index.NOT_ANALYZED));
                        doc.Add(new Field(title_field, articleLink.Title, Field.Store.YES, Field.Index.ANALYZED));
                        doc.Add(new Field(scraptime_field, articleLink.ScrapTime.ToString(), Field.Store.YES, Field.Index.NOT_ANALYZED));
                        doc.Add(new Field(url_field, articleLink.Url, Field.Store.YES, Field.Index.NOT_ANALYZED));

                        writer.AddDocument(doc);
                    }
                    writer.Optimize();
                    writer.Commit();
                    //writer.Dispose();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
        }

        public List<ArticleLinkInfo> Seach(String queryString, int fetchTopResult = -1)
        {
            List<ArticleLinkInfo> result = new List<ArticleLinkInfo>();

            try
            {
                QueryParser queryParser = new MultiFieldQueryParser(Lucene.Net.Util.Version.LUCENE_30, new string[] { "Title", "ScrapTime" }, new ChineseAnalyzer());
                Query query = queryParser.Parse(queryString);
                BooleanQuery booleanQuery = new BooleanQuery();
                booleanQuery.Add(query, Occur.MUST);

                List<SortField> sortFieldList = new List<SortField>();
                sortFieldList.Add(new SortField("ScrapTime", SortField.DOUBLE, true));
                Sort sort = new Sort(sortFieldList.ToArray());

                using (IndexSearcher indexSearcher = new IndexSearcher(FSDirectory.Open(new DirectoryInfo(this.indexDir))))
                {
                    if (fetchTopResult < 0)
                        fetchTopResult = indexSearcher.MaxDoc;
                    TopFieldDocs docs = indexSearcher.Search(booleanQuery, null, fetchTopResult, sort);
                    //hits = docs.TotalHits;

                    foreach(var scoreDoc in docs.ScoreDocs)
                    {
                        Document doc = indexSearcher.Doc(scoreDoc.Doc);
                        Console.WriteLine("{0}|{1}|{2}",doc.Get(scraptime_field),doc.Get(id_field),doc.Get(title_field));
                        result.Add(new ArticleLinkInfo(doc.Get(id_field), doc.Get(scraptime_field), doc.Get(title_field), doc.Get(url_field)));
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return result;
        }
    }
}
