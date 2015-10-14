using GifImageView.FormsPlugin.Abstractions;
using System;
using Xamarin.Forms;
using GifImageView.FormsPlugin.iOSUnified;
using Xamarin.Forms.Platform.iOS;
using UIKit;
using ImageIO;
using System.Collections.Generic;
using CoreGraphics;
using Foundation;
using CoreAnimation;
using System.Net.Http;

[assembly: ExportRenderer(typeof(GifImageView.FormsPlugin.Abstractions.GifImageViewControl), typeof(GifImageViewRenderer))]
namespace GifImageView.FormsPlugin.iOSUnified
{
    /// <summary>
    /// GifImageView Renderer
    /// </summary>
    public class GifImageViewRenderer : ImageRenderer
    {
        
        /// <summary>
        /// Used for registration with dependency service
        /// </summary>
        public static void Init() { }
        bool loaded = false;

        protected override void OnElementChanged(ElementChangedEventArgs<Image> e)
        {
            base.OnElementChanged(e);
        }
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
                        var sourceRef = CGImageSource.FromData(NSData.FromArray(bytes));
                        CreateAnimatedImageView (sourceRef, this.Control);
                    }
                }
                catch(Exception ex)
                {
                    Console.WriteLine("Unable to load gif: " + ex.Message);
                }

            }
        }

        private static UIImageView CreateAnimatedImageView(CGImageSource imageSource, UIImageView imageView = null)
        {
            var frameCount = imageSource.ImageCount;

            var frameImages = new List<NSObject>((int)frameCount);
            var frameCGImages = new List<CGImage>((int)frameCount);
            var frameDurations = new List<double>((int)frameCount);

            var totalFrameDuration = 0.0;

            for (int i = 0; i < frameCount; i++)
            {
                var frameImage = imageSource.CreateImage(i, null);

                frameCGImages.Add(frameImage);
                frameImages.Add(NSObject.FromObject(frameImage));

                var properties = imageSource.GetProperties(i, null);
                var duration = properties.Dictionary["{GIF}"];
                var delayTime = duration.ValueForKey(new NSString("DelayTime"));
                duration.Dispose ();
                var realDuration = double.Parse(delayTime.ToString());
                frameDurations.Add(realDuration);
                totalFrameDuration += realDuration;
                frameImage.Dispose ();
            }

            var framePercentageDurations = new List<NSNumber>((int)frameCount);
            var framePercentageDurationsDouble = new List<double>((int)frameCount);
            NSNumber currentDurationPercentage = 0.0f;
            double currentDurationDouble = 0.0f;
            for (int i = 0; i < frameCount; i++)
            {
                if (i != 0)
                {
                    var previousDuration = frameDurations[i - 1];
                    var previousDurationPercentage = framePercentageDurationsDouble[i - 1];

                    var number = previousDurationPercentage + (previousDuration/totalFrameDuration);
                    currentDurationDouble = number;
                    currentDurationPercentage = new NSNumber(number);
                }
                framePercentageDurationsDouble.Add(currentDurationDouble);
                framePercentageDurations.Add(currentDurationPercentage);
            }

            var imageSourceProperties = imageSource.GetProperties(null);
            var imageSourceGIFProperties = imageSourceProperties.Dictionary["{GIF}"];
            var loopCount = imageSourceGIFProperties.ValueForKey(new NSString("LoopCount"));
            var imageSourceLoopCount = float.Parse(loopCount.ToString());
            var frameAnimation = new CAKeyFrameAnimation();
            frameAnimation.KeyPath = "contents";
            if (imageSourceLoopCount <= 0.0f)
            {
                frameAnimation.RepeatCount = float.MaxValue;
            }
            else
            {
                frameAnimation.RepeatCount = imageSourceLoopCount;
            }

            imageSourceGIFProperties.Dispose ();


            frameAnimation.CalculationMode = CAAnimation.AnimationDescrete;
            frameAnimation.Values = frameImages.ToArray();
            frameAnimation.Duration = totalFrameDuration;
            frameAnimation.KeyTimes = framePercentageDurations.ToArray();
            frameAnimation.RemovedOnCompletion = false;
            var firstFrame = frameCGImages[0];
            if(imageView == null)
                imageView = new UIImageView(new CGRect(0.0f, 0.0f, firstFrame.Width, firstFrame.Height));
            else
                imageView.Layer.RemoveAllAnimations();

            imageView.Layer.AddAnimation(frameAnimation, "contents");

            frameAnimation.Dispose ();
            return imageView;
        }
    }
}
