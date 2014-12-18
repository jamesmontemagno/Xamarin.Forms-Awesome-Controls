using Monkeys.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace CircleImage
{
  public class App : Application
  {
    public App()
    {

      var list = new ListView();
      var viewModel = new MonkeysViewModel();

      list.ItemsSource = viewModel.Monkeys;

    
      var cell = new DataTemplate(typeof(MonkeyCell));

      list.RowHeight = Device.OnPlatform(70, 70, 125);
      list.HasUnevenRows = true;
      list.ItemTemplate = cell;


      // The root page of your application
      MainPage = new ContentPage
      {
        Title = "Monkeys",
        Content = list
      };
    }

    protected override void OnStart()
    {
      // Handle when your app starts
    }

    protected override void OnSleep()
    {
      // Handle when your app sleeps
    }

    protected override void OnResume()
    {
      // Handle when your app resumes
    }
  }
}
