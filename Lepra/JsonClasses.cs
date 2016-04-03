using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Lepra
{
    public class Authenticate
    {
        [JsonProperty("status")]
        public string Status { get; set; }
        [JsonProperty("csrf_token")]
        public string CsrfToken { get; set; }
        [JsonProperty("is_citizen")]
        public bool IsCitizen { get; set; }
        [JsonProperty("user")]
        public User User { get; set; }
        [JsonProperty("errors")]
        public IList<Error> Errors { get; set; }
    }

    public class Error
    {
        [JsonProperty("code")]
        public string Code { get; set; }
    }


    public class Attributes
    {
        public string enabled_latvian { get; set; }
        public string description { get; set; }
        public string name { get; set; }
        public string render_type { get; set; }
        public string favourites_count { get; set; }
    }

    public class Domain
    {
        public object readable_for_user { get; set; }
        public object owner { get; set; }
        public object excluded_from_top { get; set; }
        public int id { get; set; }
        public object elections_count { get; set; }
        public bool subscribed { get; set; }
        public object title { get; set; }
        public object marked_as_adult { get; set; }
        public object included_in_top { get; set; }
        public object is_elections_enabled { get; set; }
        public string description { get; set; }
        public int readers_count { get; set; }
        public object in_top { get; set; }
        public object readable_for_everyone { get; set; }
        public object create_post_validation_error { get; set; }
        public object posts_count { get; set; }
        public string name { get; set; }
        public int created { get; set; }
        public string url { get; set; }
        public object level { get; set; }
        public object comments_count { get; set; }
        public Attributes attributes { get; set; }
    }

    public class User
    {
        public object city { get; set; }
        public object ignored { get; set; }
        public object bans { get; set; }
        public object subscribed { get; set; }
        public object rating { get; set; }
        public int deleted { get; set; }
        public string gender { get; set; }
        public object subscribers_count { get; set; }
        public object few_words { get; set; }
        public string rank { get; set; }
        public object wiki_groups { get; set; }
        public int karma { get; set; }
        public object country { get; set; }
        public object ban { get; set; }
        public string login { get; set; }
        public int active { get; set; }
        public int id { get; set; }
    }

    public class Doc
    {
        public string body { get; set; }
        public int rating { get; set; }
        public User user { get; set; }
        public Domain domain { get; set; }
        public int unread_comments_count { get; set; }
        public object vote_enabled { get; set; }
        public object interest_comment { get; set; }
        public bool in_favourites { get; set; }
        public int has_acl { get; set; }
        public object data { get; set; }
        public int golden { get; set; }
        public int id { get; set; }
        public object user_vote { get; set; }
        public bool can_ban { get; set; }
        public object tags { get; set; }
        public bool can_moderate { get; set; }
        public bool can_delete { get; set; }
        public object can_unpublish { get; set; }
        public bool can_edit { get; set; }
        public bool in_interests { get; set; }
        public int created { get; set; }
        public int vote_weight { get; set; }
        public object reply_form_available { get; set; }
        public int comments_count { get; set; }
        public Attributes attributes { get; set; }
    }

    public class Index
    {
        public string status { get; set; }
        public IList<Doc> docs { get; set; }
        public Domain domain
        {
            get;
            set;
        }
        public User user
        {
            get;
            set;
        }
        public int offset { get; set; }
        public IList<Error> errors { get; set; }
    }

}