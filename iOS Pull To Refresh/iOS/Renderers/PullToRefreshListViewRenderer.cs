using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using PullToRefresh.iOS.Renderers;
using PullToRefresh;
using UIKit;

[assembly:ExportRendererAttribute(typeof(PullToRefreshListView), typeof(PullToRefreshListViewRenderer))]
namespace PullToRefresh.iOS.Renderers
{
	public class PullToRefreshListViewRenderer : ListViewRenderer
	{
		public PullToRefreshListViewRenderer ()
		{
		}

		private FormsUIRefreshControl refreshControl;
	

		protected override void OnElementChanged (ElementChangedEventArgs<ListView> e)
		{
			base.OnElementChanged (e);

			if (refreshControl != null)
				return;

			var pullToRefreshListView = (PullToRefreshListView)this.Element; 

			refreshControl = new FormsUIRefreshControl ();
			refreshControl.RefreshCommand = pullToRefreshListView.RefreshCommand;
			refreshControl.Message = pullToRefreshListView.Message;
			this.Control.AddSubview (refreshControl);
		}

		/// <summary>
		/// Raises the element property changed event.
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="e">E.</param>
		protected override void OnElementPropertyChanged (object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			base.OnElementPropertyChanged (sender, e);
			var pullToRefreshListView = this.Element as PullToRefreshListView;
			if(pullToRefreshListView == null)
				return;


			if (e.PropertyName == PullToRefreshListView.IsRefreshingProperty.PropertyName) {
				refreshControl.IsRefreshing = pullToRefreshListView.IsRefreshing;
			} else if (e.PropertyName == PullToRefreshListView.MessageProperty.PropertyName) {
				refreshControl.Message = pullToRefreshListView.Message;
			} else if (e.PropertyName == PullToRefreshListView.RefreshCommandProperty.PropertyName) {
				refreshControl.RefreshCommand = pullToRefreshListView.RefreshCommand;
			}
		}
	}
}

