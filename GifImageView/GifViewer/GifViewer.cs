using System;

using Xamarin.Forms;
using GifImageView.FormsPlugin.Abstractions;

namespace GifViewer
{
    public class App : Application
    {
        public App()
        {
            // The root page of your application
            MainPage = new ContentPage
            {
                Content = new GifImageViewControl
                {
                            VerticalOptions = LayoutOptions.FillAndExpand,
                            HorizontalOptions = LayoutOptions.FillAndExpand,
                    Source = ImageSource.FromResource("GifViewer.dogturtle.gif")
//                            Source = ImageSource.FromUri(new Uri("http://dogoverflow.com/dRX5G8qK"))
                }
            };
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}

