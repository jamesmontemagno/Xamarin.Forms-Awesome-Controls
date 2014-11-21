using System;
using Xamarin.Forms;

namespace PullToRefresh
{
	public class PullToRefreshListView : ListView
	{
		public PullToRefreshListView ()
		{
		}
			

		public static readonly BindableProperty IsRefreshingProperty = 
			BindableProperty.Create<PullToRefreshListView,bool> (
				p => p.IsRefreshing, false);

		public bool IsRefreshing {
			get { return (bool)GetValue (IsRefreshingProperty); }
			set { SetValue (IsRefreshingProperty, value); }
		}

		public static readonly BindableProperty RefreshCommandProperty = 
			BindableProperty.Create<PullToRefreshListView,Command> (
				p => p.RefreshCommand, null);

		public Command RefreshCommand {
			get { return (Command)GetValue (RefreshCommandProperty); }
			set { SetValue (RefreshCommandProperty, value); }
		}


		public static readonly BindableProperty MessageProperty = 
			BindableProperty.Create<PullToRefreshListView,string> (
				p => p.Message, string.Empty);

		public string Message {
			get { return (string)GetValue (MessageProperty); }
			set { SetValue (MessageProperty, value); }
		}

	}
}

