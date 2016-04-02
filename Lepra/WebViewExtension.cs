using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Lepra
{
    public class WebViewExtension
    {
        private static string _htmlFragment =
                "<html><head><script type='text/javascript'>" +
                "function getDocHeight(){ " +
                "  var D = document;" +
                "  var doubled = Math.max(" +
                "    Math.max(D.body.scrollHeight, D.documentElement.scrollHeight)," +
                "    Math.max(D.body.offsetHeight, D.documentElement.offsetHeight)," +
                "    Math.max(D.body.clientHeight, D.documentElement.clientHeight));" +
                "  window.external.notify(doubled.toString());" +
                "  return doubled.toString();" +
                "};" +
                "</script></head><body>{0}</body></html>";

        public static string GetHTML(DependencyObject obj)
        {
            return (string)obj.GetValue(HTMLProperty);
        }

        public static void SetHTML(DependencyObject obj, string value)
        {
            obj.SetValue(HTMLProperty, value);
        }
        
        public static readonly DependencyProperty HTMLProperty =
          DependencyProperty.RegisterAttached("HTML", typeof(string), typeof(WebViewExtension), new PropertyMetadata("", new PropertyChangedCallback(OnHTMLChanged)));

        private static async void WvOnLoadCompleted(object sender, NavigationEventArgs navigationEventArgs)
        {
            var webView = ((WebView)sender);

            var aa = await webView.InvokeScriptAsync("getDocHeight", null);
            ((WebView)sender).Height = int.Parse(aa);
        }

        private static void OnHTMLChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var wv = d as WebView;

            wv.LoadCompleted += WvOnLoadCompleted;

            var page = _htmlFragment + e.NewValue + "</body></html>";

            wv?.NavigateToString(page);
        }
    }
}
