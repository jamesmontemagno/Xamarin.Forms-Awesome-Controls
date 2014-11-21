using Xamarin.Forms;

namespace CustomProgressBar
{
	public class App
	{
		public static Page GetMainPage ()
		{	

			var contentPage = new ContentPage {
				Title = "Progress Bar"
			};
		
			var codeBehind = new Button {
				Text = "Code Behind Example"
			};


			var xaml = new Button {
				Text = "XAML Example"
			};

			codeBehind.Clicked += (sender, args) => {
				contentPage.Navigation.PushAsync(new ProgressBarCodeBehind());
			};

			xaml.Clicked += (sender, args) => {
				contentPage.Navigation.PushAsync(new ProgressBarXAML());
			};

			contentPage.Content = new StackLayout {
				Spacing = 10,
				Padding = 10,
				Children = { codeBehind, xaml}
			};

			return new NavigationPage (contentPage);
		}
	}
}

