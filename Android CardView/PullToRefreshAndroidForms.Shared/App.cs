using System;
using Xamarin.Forms;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace CardViewForms
{
	public class App
	{
		public static Page GetMainPage ()
		{	




					
			var stack = new StackLayout {
				Padding = 10,
				Spacing = 10
			};

			for (int i = 0; i < 10; i++) {
				var card = new CardContentView {
					Padding = 40,
					BackgroundColor = Color.Red,
					CornderRadius = (float)i,
					Content = new Label{Text = "I am a card: " + i + "!!!",
						Font = Font.SystemFontOfSize(NamedSize.Large),
						VerticalOptions = LayoutOptions.Center}
				};

				stack.Children.Add (card);

			}

			return new NavigationPage(new ContentPage { 
				Content = new ScrollView
				{
					Content = stack
				},
				Title = "Card View Forms!"
			});
		}

	}
}

