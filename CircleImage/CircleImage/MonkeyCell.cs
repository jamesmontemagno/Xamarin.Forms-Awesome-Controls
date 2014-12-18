using Monkeys.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CircleImage
{
  public class MonkeyCell : ViewCell
  {
    public MonkeyCell()
    {
      var name = new Label
      {
        VerticalOptions = LayoutOptions.Center,
        Font = Font.SystemFontOfSize(NamedSize.Large)
      };
      name.SetBinding<Monkey>(Label.TextProperty, s => s.Name);

      var location = new Label
      {
        VerticalOptions = LayoutOptions.Center,
        Font = Font.SystemFontOfSize(NamedSize.Large),
        LineBreakMode = LineBreakMode.NoWrap
      };
      location.SetBinding<Monkey>(Label.TextProperty, s => s.Location);

      int photoSize = Device.OnPlatform(50, 50, 80);
      var photo = new ImageCircle
      {
        WidthRequest = photoSize,
        HeightRequest = photoSize,
        Aspect = Aspect.AspectFill,
        HorizontalOptions = LayoutOptions.Center
      };
      photo.SetBinding<Monkey>(Image.SourceProperty, s => s.Image);

      var stackname = new StackLayout
      {
        Orientation = StackOrientation.Vertical,
        HorizontalOptions = LayoutOptions.FillAndExpand,
        VerticalOptions = LayoutOptions.FillAndExpand,
        Spacing = 5,
        Children = { name, location }
      };

      var stack = new StackLayout
            {
              Orientation = StackOrientation.Horizontal,
              HorizontalOptions = LayoutOptions.FillAndExpand,
              VerticalOptions = LayoutOptions.FillAndExpand,
              Padding = new Thickness(Device.OnPlatform(5, 10, 10), Device.OnPlatform(10, 10, 20)),
              Spacing = 5,
              Children = { photo, stackname }
            };

      View = stack;
    }
  }
}
