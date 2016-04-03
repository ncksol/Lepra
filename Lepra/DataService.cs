using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Web.Http;
using Windows.Web.Http.Filters;
using Newtonsoft.Json;

namespace Lepra
{
    public class DataService
    {
        private readonly HttpClient _httpClient;
        private HttpBaseProtocolFilter _httpBaseProtocolFilter;
        private string _authToken;

        public DataService()
        {
            _httpBaseProtocolFilter = new HttpBaseProtocolFilter();
            _httpClient = new HttpClient(_httpBaseProtocolFilter);

            var cookiesString = ApplicationData.Current.LocalSettings.Values["AuthCookies"] as string;
            if (cookiesString != null)
            {
                var authToken = ApplicationData.Current.LocalSettings.Values["AuthToken"] as string;
                _authToken = authToken;

                var cookiesDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(cookiesString);

                foreach (var keyValuePair in cookiesDictionary)
                {
                    var cookie = new HttpCookie(keyValuePair.Key, "leprosorium.ru", "/");
                    cookie.Value = keyValuePair.Value;

                    _httpBaseProtocolFilter.CookieManager.SetCookie(cookie);
                }
            }
        }

        public bool IsAuthorised => !string.IsNullOrEmpty(_authToken);

        public async Task<Authenticate> SignIn(string username, string password)
        {
            var content = new HttpFormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("username", username),
                new KeyValuePair<string, string>("password", password),
            });

            var httpResponseMessage = await _httpClient.PostAsync(new Uri("https://leprosorium.ru/ajax/auth/login"), content);

            var authResult = await ConvertResponseToData<Authenticate>(httpResponseMessage);

            var cookies = _httpBaseProtocolFilter.CookieManager.GetCookies(new Uri("https://leprosorium.ru"));

            var cookiesDictionary = cookies.ToDictionary(cookie => cookie.Name, cookie => cookie.Value);

            ApplicationData.Current.LocalSettings.Values["AuthCookies"] = JsonConvert.SerializeObject(cookiesDictionary);
            ApplicationData.Current.LocalSettings.Values["AuthToken"] = authResult.CsrfToken;

            _authToken = authResult.CsrfToken;
            return authResult;
        }

        public async Task<List<TopicModel>> GetIndexPage(int offset = 0)
        {
            var subsContent = new HttpFormUrlEncodedContent(new[]
                    {
                        new KeyValuePair<string, string>("csrf_token", _authToken),
                        new KeyValuePair<string, string>("docs_in_json", "true"),
                        new KeyValuePair<string, string>("offset", offset.ToString()),
                        new KeyValuePair<string, string>("sorting", "last_activity"),
                        new KeyValuePair<string, string>("feed_type", "main"),
                        new KeyValuePair<string, string>("threshold", "disabled"),
                    });

            var httpResponseMessage = await _httpClient.PostAsync(new Uri("https://leprosorium.ru/ajax/index/moar"), subsContent);

            var getIndexResult = await ConvertResponseToData<Index>(httpResponseMessage);

            if(getIndexResult.status != "OK")
                throw new InvalidOperationException(getIndexResult.errors.FirstOrDefault().Code);

            return getIndexResult.docs.Select(x => new TopicModel(x)).ToList();
        }
        
        private async Task<T> ConvertResponseToData<T>(HttpResponseMessage message)
        {
            var resultString = await message.Content.ReadAsStringAsync();
            var resultObject = JsonConvert.DeserializeObject<T>(resultString);
            return resultObject;
        }
    }
}
