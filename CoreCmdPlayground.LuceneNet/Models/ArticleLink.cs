using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreCmdPlayground.LuceneNet.Models
{
    public class ArticleLink
    {
        public int Id { get; set; }
        public DateTime? ArticleTime { get; set; }       // to be populated manually

        // populate when the link is just scraped
        public DateTime ScrapTime { get; set; }
        public string Title { get; set; }               // link text (or article title)
        public string Url { get; set; }                 // the complete link url
        public bool IsNewLink { get; set; } = true;        // in manual review queue; this field is also used in removing duplicated links

        /// <summary>
        /// For links with the same titles but different URL, only display one but hide others.
        /// The hidden ones will be used as alternative if the displayed one gets broken.
        /// </summary>
        public bool IsHidden { get; set; } = false;

        // dead link detection
        public DateTime? DetectTime { get; set; }        // the time when a dead link is detected
        public bool IsDeadLink { get; set; }
    }
}
