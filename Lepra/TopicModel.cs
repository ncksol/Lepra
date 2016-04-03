using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lepra
{
    public class TopicModel
    {
        public TopicModel(Doc doc)
        {
            Body = doc.body;
            Rating = doc.rating;
            NewCommentsCount = doc.unread_comments_count;
            Author = new UserModel(doc.user);
            DateCreated = DateTimeOffset.FromUnixTimeSeconds(doc.created);
        }

        public string Body { get; set; }
        public int Rating { get; set; }
        public int NewCommentsCount { get; set; }
        public UserModel Author { get; set; }
        public DateTimeOffset DateCreated { get; set; }

        public string DateCreatedDisplayValue
        {
            get
            {
                if ((DateCreated - DateTime.Today).Days == 0)
                {
                    return string.Format("сегодня в {0}", DateCreated.ToString("HH.mm"));
                }
                else if ((DateCreated - DateTime.Today).Days == 1)
                {
                    return string.Format("вчера в {0}", DateCreated.ToString("HH.mm"));
                }
                else
                {
                    return string.Format("{0} в {1}", DateCreated.Date.ToString("dd MMMM yyyy", new CultureInfo("RU-ru")), DateCreated.ToString("HH.mm"));
                }
            }
        }

        public string Prefix
        {
            get { return string.Format("{0}{1}", Author.Gender == 0 ? "Написал" : "Написала", !string.IsNullOrEmpty(Author.Rank) ? " " + Author.Rank : ""); }
        }
    }
}
