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
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;

[assembly: ExportRenderer(typeof(GifImageViewControl), typeof(GifImageViewRenderer))]
namespace GifImageView.FormsPlugin.iOSUnified
{
    /// <summary>
    /// GifImageView Renderer
    /// </summary>
    public class GifImageViewRenderer : ImageRenderer
    {

#pragma warning disable CS0108 // Member hides inherited member; missing new keyword
        /// <summary>
        /// Used for registration with dependency service
        /// </summary>
        public static void Init() { }
#pragma warning restore CS0108 // Member hides inherited member; missing new keyword

        protected override void OnElementChanged(ElementChangedEventArgs<Image> e)
        {
            base.OnElementChanged(e);
        }

        protected override async void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (e.PropertyName == Image.SourceProperty.PropertyName)
            {
                NSData bytes = null;

                var s = Element.Source;
                if (s is UriImageSource)
                {
                    using (var client = new HttpClient())
                        bytes = NSData.FromArray(await client.GetByteArrayAsync(((UriImageSource)s).Uri));
                }
                else if (s is StreamImageSource)
                {
                    bytes = NSData.FromStream(await ((StreamImageSource)s).Stream(default(CancellationToken)));
                }
                else if (s is FileImageSource)
                {
                    bytes = await Task.Run(() => { return NSData.FromFile(((FileImageSource)s).File); });
                }

                if(bytes == null)
                    return;

                try
                {
                    var sourceRef = CGImageSource.FromData(bytes);
                    CreateAnimatedImageView(sourceRef, Control);
                }
                catch(Exception ex)
                {
                    Debug.WriteLine("Unable to load gif: " + ex.Message);
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
                frameImages.Add(FromObject(frameImage));

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
