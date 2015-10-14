using GifImageView.FormsPlugin.Abstractions;
using System;
using Xamarin.Forms;
using GifImageView.FormsPlugin.Android;
using Xamarin.Forms.Platform.Android;
using System.Net.Http;

[assembly: ExportRenderer(typeof(GifImageView.FormsPlugin.Abstractions.GifImageViewControl), typeof(GifImageViewRenderer))]
namespace GifImageView.FormsPlugin.Android
{
    /// <summary>
    /// GifImageView Renderer
    /// </summary>
    public class GifImageViewRenderer : ViewRenderer<Image, Felipecsl.GifImageViewLibrary.GifImageView>
    {
        /// <summary>
        /// Used for registration with dependency service
        /// </summary>
        public static void Init() { }

        Felipecsl.GifImageViewLibrary.GifImageView gif;
        protected override void OnElementChanged(ElementChangedEventArgs<Image> e)
        {
            base.OnElementChanged(e);
            if (e.OldElement != null || this.Element == null)
                return;

            gif = new Felipecsl.GifImageViewLibrary.GifImageView(Xamarin.Forms.Forms.Context);

            SetNativeControl(gif);
        }

        bool loaded = false;
        protected override async void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (e.PropertyName == "Renderer")
            {
                if (loaded)
                    return;

                var uriImageSource = Element.Source as UriImageSource;
                if (uriImageSource == null || uriImageSource.Uri == null)
                    return;

                try
                {
                    loaded = true;
                    using (var client = new HttpClient())
                    {
                        var bytes = await client.GetByteArrayAsync(uriImageSource.Uri);
                        gif.SetBytes(bytes);
                        gif.StartAnimation();
                    }
                }
                catch(Exception ex)
                {
                    Console.WriteLine("Unable to load gif: " + ex.Message);
                }

            }
        }
    }
}