/*
 * Copyright (C) 2015 Refractored LLC & James Montemagno: 
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
using Xamarin.Forms.Platform.iOS;
using Xamarin.Forms;
using RefreshSample;
using RefreshSample.iOS;
using UIKit;
using System.Windows.Input;
using System.ComponentModel;
using System.Diagnostics;

[assembly:ExportRenderer(typeof(PullToRefreshLayout), typeof(PullToRefreshLayoutRenderer))]
namespace RefreshSample.iOS
{

    public class PullToRefreshLayoutRenderer : ViewRenderer<PullToRefreshLayout, UIView>
    {
       

        UIRefreshControl refreshControl;


        protected override void OnElementChanged (ElementChangedEventArgs<PullToRefreshLayout> e)
        {
            base.OnElementChanged (e);

            if (e.OldElement != null || this.Element == null)
                return;

  

            refreshControl = new UIRefreshControl ();

            refreshControl.ValueChanged += OnRefresh;

            var packed = RendererFactory.GetRenderer(Element.Content);

            Element.Content.SetValue(RendererProperty, packed);

            SetNativeControl(packed.NativeView);
            try
            {
                TryInsertRefresh(Control);
            }
            catch(Exception ex)
            {
                Debug.WriteLine("View is not supported in PullToRefreshLayout: " + ex);
            }
           

            UpdateColors();
            UpdateIsRefreshing();
            UpdateIsSwipeToRefreshEnabled();
        }

       

        bool TryInsertRefresh(UIView view, int index = 0)
        {
            
            if (view is UITableView)
            {
                view.InsertSubview(refreshControl, index);
                return true;
            }
            else if (view is UIScrollView)
            {
                view.InsertSubview(refreshControl, index);
                return true;
            }

            if (view.Subviews == null)
                return false;

            for (int i = 0; i < view.Subviews.Length; i++)
            {
                var control = view.Subviews[i];
                if (TryInsertRefresh(control, i))
                    return true;
            }

            return false;
        }



        BindableProperty rendererProperty = null;

        /// <summary>
        /// Gets the bindable property.
        /// </summary>
        /// <returns>The bindable property.</returns>
        BindableProperty RendererProperty
        {
            get
            {
                if (rendererProperty != null)
                    return rendererProperty;

                var type = Type.GetType("Xamarin.Forms.Platform.iOS.Platform, Xamarin.Forms.Platform.iOS");
                var prop = type.GetField("RendererProperty");
                var val = prop.GetValue(null);
                rendererProperty = val as BindableProperty;

                return rendererProperty;
            }
        }

        void UpdateColors()
        {
            if (RefreshView == null)
                return;
            if (RefreshView.RefreshColor.HasValue)
                refreshControl.TintColor = RefreshView.RefreshColor.Value.ToUIColor();
            if (RefreshView.RefreshBackgroundColor.HasValue)
                refreshControl.BackgroundColor = RefreshView.RefreshBackgroundColor.Value.ToUIColor();
        }


        void UpdateIsRefreshing()
        {
            IsRefreshing = RefreshView.IsRefreshing;
        }

        void UpdateIsSwipeToRefreshEnabled()
        {
            refreshControl.Enabled = RefreshView.IsPullToRefreshEnabled;
        }


        /// <summary>
        /// Helpers to cast our element easily
        /// Will throw an exception if the Element is not correct
        /// </summary>
        /// <value>The refresh view.</value>
        public PullToRefreshLayout RefreshView
        {
            get { return this.Element == null ? null : (PullToRefreshLayout)Element; }
        }



        bool isRefreshing;
        /// <summary>
        /// Gets or sets a value indicating whether this instance is refreshing.
        /// </summary>
        /// <value><c>true</c> if this instance is refreshing; otherwise, <c>false</c>.</value>
        public bool IsRefreshing
        {
            get { return isRefreshing;}
            set
            { 
                isRefreshing = value; 
                if (isRefreshing)
                    refreshControl.BeginRefreshing();
                else
                    refreshControl.EndRefreshing();
            }
        }

        /// <summary>
        /// The refresh view has been refreshed
        /// </summary>
        void OnRefresh(object sender, EventArgs e)
        {
            //someone pulled down to refresh or it is done
            if (RefreshView == null)
                return;

            var command = RefreshView.RefreshCommand;
            if (command == null)
                return;

            command.Execute(null);
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (e.PropertyName == PullToRefreshLayout.IsPullToRefreshEnabledProperty.PropertyName)
                UpdateIsSwipeToRefreshEnabled();
            else if (e.PropertyName == PullToRefreshLayout.IsRefreshingProperty.PropertyName)
                UpdateIsRefreshing();
            else if (e.PropertyName == PullToRefreshLayout.RefreshColorProperty.PropertyName)
                UpdateColors();
            else if (e.PropertyName == PullToRefreshLayout.RefreshBackgroundColorProperty.PropertyName)
                UpdateColors();
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (refreshControl != null)
            {
                refreshControl.ValueChanged -= OnRefresh;
            }
        }
            
    }

  
}

