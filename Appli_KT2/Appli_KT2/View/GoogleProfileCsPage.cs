using Appli_KT2.Model;
using Appli_KT2.Utils;
using Appli_KT2.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace Appli_KT2.View
{
    public class GoogleProfileCsPage : ContentPage
    {
        private readonly LoginViewModel _googleViewModel = new LoginViewModel();

        public GoogleProfileCsPage()
        {

            BindingContext = _googleViewModel;

            Title = "Google Profile";
            BackgroundColor = Color.White;

            var authRequest =
                  "http://accounts.google.com/o/oauth2/v2/auth"
                  + "?response_type=code"
                  + "&scope=openid"
                  + "&redirect_uri=" + GoogleService.RedirectUri
                  + "&client_id=" + GoogleService.ClientId;

            var webView = new WebView
            {
                Source = authRequest,
                HeightRequest = 1
            };

            //webView.Navigating += (s, e) =>
            //{
            //    if (e.Url.StartsWith("http"))
            //    {
            //        try
            //        {
            //            var uri = new Uri(e.Url);
            //            webView.Source = uri;
            //        }
            //        catch (Exception)
            //        {
            //        }

            //        e.Cancel = true;
            //    }
            //};
            webView.Navigated += WebViewOnNavigated;

            Content = webView;
        }

        private async void WebViewOnNavigated(object sender, WebNavigatedEventArgs e)
        {

            var code = ExtractCodeFromUrl(e.Url);

            if (code != "")
            {

                var accessToken = await _googleViewModel.GetAccessTokenAsync(code);

                await _googleViewModel.SetGoogleUserProfileAsync(accessToken);

                SetPageContent(_googleViewModel.GoogleProfile);
            }
        }

        private void SetPageContent(GoogleProfile googleProfile)
        {
            Content = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                Padding = new Thickness(8, 30),
                Children =
                {
                    new Label
                    {
                        Text = googleProfile.DisplayName,
                        TextColor = Color.Black,
                        FontSize = 22,
                    },
                    new Label
                    {
                        Text = googleProfile.Id,
                        TextColor = Color.Black,
                        FontSize = 22,
                    },
                    new Label
                    {
                        Text = googleProfile.Verified.ToString(),
                        TextColor = Color.Black,
                        FontSize = 22,
                    },
                    new Label
                    {
                        Text = googleProfile.Gender,
                        TextColor = Color.Black,
                        FontSize = 22,
                    },
                    new Label
                    {
                        Text = googleProfile.Tagline,
                        TextColor = Color.Black,
                        FontSize = 22,
                    },
                    new Label
                    {
                        Text = googleProfile.CircledByCount.ToString(),
                        TextColor = Color.Black,
                        FontSize = 22,
                    },
                    new Label
                    {
                        Text = googleProfile.Occupation,
                        TextColor = Color.Black,
                        FontSize = 22,
                    },
                    new Xamarin.Forms.Image
                    {
                         Source = googleProfile.Image.Url,
                         HeightRequest = 100
                    },
                     new Xamarin.Forms.Image
                    {
                         Source = googleProfile.Cover.CoverPhoto.Url,
                         HeightRequest = 100
                    },
                }
            };
        }

        private string ExtractCodeFromUrl(string url)
        {
            if (url.Contains("code="))
            {
                var attributes = url.Split('&');

                var code = attributes.FirstOrDefault(s => s.Contains("code=")).Split('=')[1];

                return code;
            }

            return string.Empty;
        }
    }
}