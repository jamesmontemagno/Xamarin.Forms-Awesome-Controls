Xamarin.Forms Android Pull To Refresh/SwipeRefreshLayout
===================================

See the new official way here: http://motzcod.es/post/113280718807/official-pull-to-refresh-in-xamarinforms-140

Implementation of pull to refresh for Xamarin.Forms Android. Currently supports ListViews. Read about the implementation on the blog:

http://motzcod.es/post/103224921992/pull-swipe-to-refresh-for-xamarin-forms-android-apps


![](http://media.tumblr.com/bb7899a2585a769a17edaf472663cd7f/tumblr_inline_nfeoptHlDJ1qzumo9.gif)


## How to use

Create a new PullToRefreshContentView and bind the RefreshCommand to your ViewModel’s ICommand that will be triggered when the control is pulled down to refresh:

```
var listView = new ListView ();
listView.SetBinding<TestViewModel> (ListView.ItemsSourceProperty, vm => vm.Items);

  var refreshView = new PullToRefreshContentView {
    RefreshCommand = viewModel.RefreshCommand,
    Content = listView
			};

  refreshView.SetBinding<TestViewModel> (PullToRefreshContentView.IsRefreshingProperty, vm => vm.IsBusy);
```

Add the refreshView as the Content of your page, and you are done!

## Customize
Currently the colors are hard coded and used from a colors.xml resource file. You can update these in the Renderer’s SetElement method:

```
SetColorSchemeResources (Resource.Color.xam_dark_blue,
					Resource.Color.xam_purple,
					Resource.Color.xam_gray,
					Resource.Color.xam_green);
```

## Limitations

Mostly, this is really proof of concept and I need to do a bit more work to really make it bullet proof, but here are the current limitations.

* Only works with ListView set as Content of ContentView
* IsRefreshing Binding only goes 1 way from ViewModel down to Control (will be fixed in 1.3.X of Xamarin.Forms)
* You must manually SetColorSchemeResources in code behind (no bindings here)
* No support for other normal bindable properties of a ContentView such as BackgroundColor
