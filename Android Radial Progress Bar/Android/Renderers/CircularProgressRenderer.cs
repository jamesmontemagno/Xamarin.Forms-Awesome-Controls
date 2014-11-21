using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using com.refractored.monodroidtoolkit;
using CustomProgressBar.Droid.Renderers;
using CustomProgressBar.CustomControls;

[assembly:ExportRenderer(typeof(CircularProgress), typeof(CircularProgressRenderer))]
namespace CustomProgressBar.Droid.Renderers
{
	public class CircularProgressRenderer : ViewRenderer<CircularProgress, HoloCircularProgressBar>
	{

		public CircularProgressRenderer ()
		{
		}

		protected override void OnElementChanged (ElementChangedEventArgs<CircularProgress> e)
		{
			base.OnElementChanged (e);

			if (e.OldElement != null || this.Element == null)
				return;
				

			var progress = new HoloCircularProgressBar (Forms.Context) {
				Max = Element.Max,
				Progress = Element.Progress,
				Indeterminate = Element.Indeterminate,
				ProgressColor = Element.ProgressColor.ToAndroid (),
				ProgressBackgroundColor = Element.ProgressBackgroundColor.ToAndroid (),
				IndeterminateInterval = Element.IndeterminateSpeed
			};


			SetNativeControl (progress);
		}


		protected override void OnElementPropertyChanged (object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			base.OnElementPropertyChanged (sender, e);

			if (Control == null || Element == null)
				return;
				
			if (e.PropertyName == CircularProgress.MaxProperty.PropertyName) {
				Control.Max = Element.Max;
			} else if (e.PropertyName == CircularProgress.ProgressProperty.PropertyName) {
				Control.Progress = Element.Progress;
			} else if (e.PropertyName == CircularProgress.IndeterminateProperty.PropertyName) {
				Control.Indeterminate = Element.Indeterminate;
			}else if (e.PropertyName == CircularProgress.ProgressBackgroundColorProperty.PropertyName) {
				Control.ProgressBackgroundColor = Element.ProgressBackgroundColor.ToAndroid ();
			}else if (e.PropertyName == CircularProgress.ProgressColorProperty.PropertyName) {
				Control.ProgressColor = Element.ProgressColor.ToAndroid ();
			}else if (e.PropertyName == CircularProgress.IndeterminateSpeedProperty.PropertyName) {
				Control.IndeterminateInterval = Element.IndeterminateSpeed;
			}

		}
	}
}

