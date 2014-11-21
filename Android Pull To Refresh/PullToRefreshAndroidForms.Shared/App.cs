/*
 * Copyright (C) 2014 Refractored LLC: 
 * http://github.com/JamesMontemagno
 * http://twitter.com/JamesMontemagno
 * http://refractored.com
 * 
 * The MIT License (MIT) see GitHub For more information
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */
using System;
using Xamarin.Forms;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace PullToRefreshAndroidForms
{
	public static class App
	{
		public static Page GetMainPage ()
		{	

			var viewModel = new TestViewModel ();

			var listView = new ListView ();
			listView.SetBinding<TestViewModel> (ListView.ItemsSourceProperty, vm => vm.Items);

			var refreshView = new PullToRefreshContentView {
				RefreshCommand = viewModel.RefreshCommand,
				Content = listView
			};

			refreshView.SetBinding<TestViewModel> (PullToRefreshContentView.IsRefreshingProperty, vm => vm.IsBusy);



			return new NavigationPage(new ContentPage { 
				BindingContext = viewModel,
				Content = refreshView,
				Title = "Pull To Refresh"
			});
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

				for (int i = 0; i < 100; i++)
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
}

