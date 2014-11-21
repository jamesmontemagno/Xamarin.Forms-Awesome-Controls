using System;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using Android.Support.V4.Widget;
using PullToRefreshAndroidForms;
using PullToRefreshAndroidFormsAndroid;

[assembly:ExportRendererAttribute(typeof(PullToRefreshContentView), typeof(PullToRefreshContentViewRenderer))]
namespace PullToRefreshAndroidFormsAndroid
{

	public class PullToRefreshContentViewRenderer : SwipeRefreshLayout,
	IVisualElementRenderer,
		SwipeRefreshLayout.IOnRefreshListener
	{
		public PullToRefreshContentViewRenderer ()
		{
		}

		SwipeRefreshLayout refresher;

		protected override void OnElementChanged (ElementChangedEventArgs<PullToRefreshContentView> e)
		{
			base.OnElementChanged (e);

			if (refresher != null)
				return;

			refresher = new SwipeRefreshLayout (Xamarin.Forms.Forms.Context);
			refresher.LayoutParameters = new LayoutParams (LayoutParams.MatchParent, LayoutParams.MatchParent);
			refresher.SetColorSchemeResources (Resource.Color.xam_purple);

			refresher.SetOnRefreshListener (this);
			//This gets called when we pull down to refresh to trigger command
			refresher.Refresh += (object sender2, EventArgs e2) => {
				var command = this.Element.RefreshCommand;
				if (command == null)
					return;

				command.Execute (null);
			};

			//HACK as I need to add the sub group to the content group...
			//var text = new Android.Widget.ListView (Xamarin.Forms.Forms.Context);
			//text.LayoutParameters = new LayoutParams (LayoutParams.MatchParent, LayoutParams.MatchParent);
			//text.Text = "Hello world";

			//refresher.AddView (text);

			refresher.SetBackgroundColor (Android.Graphics.Color.Red);
			SetNativeControl (refresher);
		}

		protected override void OnElementPropertyChanged (object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			base.OnElementPropertyChanged (sender, e);

			if (refresher == null || this.Element == null)
				return;

			if (e.PropertyName == PullToRefreshContentView.IsRefreshingProperty.PropertyName) {
				refresher.Refreshing = this.Element.IsRefreshing;
			}
		}

		#region IVisualElementRenderer implementation
		public event EventHandler<VisualElementChangedEventArgs> ElementChanged;
		public void SetElement (VisualElement element)
		{
			throw new NotImplementedException ();
		}
		public SizeRequest GetDesiredSize (int widthConstraint, int heightConstraint)
		{
			throw new NotImplementedException ();
		}
		public void UpdateLayout ()
		{
			throw new NotImplementedException ();
		}
		public VisualElementTracker Tracker {
			get {
				throw new NotImplementedException ();
			}
		}
		public Android.Views.ViewGroup ViewGroup {
			get {
				throw new NotImplementedException ();
			}
		}
		public VisualElement Element {
			get {
				throw new NotImplementedException ();
			}
		}
		#endregion
		#region IDisposable implementation
		public void Dispose ()
		{
			throw new NotImplementedException ();
		}
		#endregion

		public void OnRefresh ()
		{
			if (refresher == null || this.Element == null)
				return;

			this.Element.IsRefreshing = refresher.Refreshing;
		}
	}
}

