namespace CoreCmdPlayground.LuceneNet.Models
{
    public class ArticleLinkInfo
    {
        public string Id { get; set; }
        public string ScrapTime { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }

        public ArticleLinkInfo(string id, string scraptime, string title, string url)
        {
            this.Id = id;
            this.ScrapTime = scraptime;
            this.Title = title;
            this.Url = url;
        }
    }
}
