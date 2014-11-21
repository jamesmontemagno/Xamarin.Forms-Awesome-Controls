using System;
using Xamarin.Forms;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace PullToRefresh
{
	public class App
	{
		public static Page GetMainPage ()
		{	

			var viewModel = new TestViewModel ();
		
			var refreshList = new PullToRefreshListView {
				RefreshCommand = viewModel.RefreshCommand,
				Message = "loading..."
			};

			refreshList.SetBinding<TestViewModel> (PullToRefreshListView.IsRefreshingProperty, vm => vm.IsBusy);
			refreshList.SetBinding<TestViewModel> (PullToRefreshListView.ItemsSourceProperty, vm => vm.Items);

			return new NavigationPage(new ContentPage { 
				BindingContext = viewModel,
				Content = refreshList,
				Title = "Pull To Refresh"
			});
		}


	}

	public class TestViewModel : INotifyPropertyChanged
	{
		public ObservableCollection<string> Items { get; set;}

		public TestViewModel()
		{
			Items = new ObservableCollection<string> ();
		}

		private bool isBusy;
		public bool IsBusy
		{
			get { return isBusy; }
			set 
			{
				if (isBusy == value)
					return;

				isBusy = value;
				OnPropertyChanged ("IsBusy");
			}
		}

		private Command refreshCommand;
		public Command RefreshCommand
		{
			get { return refreshCommand ?? (refreshCommand = new Command (async ()=> await ExecuteRefreshCommand())); }
		}

		private async Task ExecuteRefreshCommand()
		{
			if (IsBusy)
				return;

			IsBusy = true;
			Items.Clear ();

			await Task.Run(()=>{System.Threading.Thread.Sleep (5000);});

			for (int i = 0; i < 10; i++)
				Items.Add (DateTime.Now.AddMinutes (i).ToString ("F"));
				
			IsBusy = false;
		}

		#region INotifyPropertyChanged implementation

		public event PropertyChangedEventHandler PropertyChanged;

		#endregion

		public void OnPropertyChanged(string propertyName)
		{
			if (PropertyChanged == null)
				return;

			PropertyChanged (this, new PropertyChangedEventArgs (propertyName));
		}

	}
}

