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
using Android.Support.V4.Widget;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using PullToRefreshAndroidForms;
using PullToRefreshAndroidFormsAndroid;
using Android.Views;

[assembly:ExportRendererAttribute(typeof(PullToRefreshContentView), typeof(FormsSwipeRefreshLayout))]
namespace PullToRefreshAndroidFormsAndroid
{
	public class FormsSwipeRefreshLayout : SwipeRefreshLayout, 
		IVisualElementRenderer,
		SwipeRefreshLayout.IOnRefreshListener
	{
		public FormsSwipeRefreshLayout () : base (Forms.Context)
		{
		}

		public event EventHandler<VisualElementChangedEventArgs> ElementChanged;

		bool init;
		ViewGroup packed;
		/// <summary>
		/// Setup our SwipeRefreshLayout and register for property changed notifications.
		/// </summary>
		/// <param name="element">Element.</param>
		public void SetElement (VisualElement element)
		{
			var oldElement = this.Element;

			//unregister old and re-register new
			if (oldElement != null)
				oldElement.PropertyChanged -= HandlePropertyChanged;

			this.Element = element;
			if (this.Element != null) {
				UpdateContent ();
				this.Element.PropertyChanged += HandlePropertyChanged;
			}

			if (!init) {
				init = true;
				//sizes to match the forms view
				//updates properties, handles visual element properties
				Tracker = new VisualElementTracker (this);
				//add and remove children automatically
				//this is broken right now so I do it manually, but might
				//be fixed in the future.
				//packager = new VisualElementPackager (this);
				//packager.Load ();

				//TODO: this is hardcoded for now via resources, but could be bindable.
				SetColorSchemeResources (Resource.Color.xam_dark_blue,
					Resource.Color.xam_purple,
					Resource.Color.xam_gray,
					Resource.Color.xam_green);

				this.SetOnRefreshListener (this);
			}

			if(ElementChanged != null)
				ElementChanged (this, new VisualElementChangedEventArgs (oldElement, this.Element));
		}

		/// <summary>
		/// Managest adding and removing the android viewgroup to our actual swiperefreshlayout
		/// </summary>
		private void UpdateContent()
		{
			if (RefreshView.Content == null)
				return;

			if (packed != null)
				RemoveView (packed);

			var child = RendererFactory.GetRenderer (RefreshView.Content);
			packed = child.ViewGroup;
			AddView (packed);
		}

		/// <summary>
		/// Determines whether this instance can child scroll up.
		/// We do this since the actual swipe refresh can't figure it out
		/// </summary>
		/// <returns><c>true</c> if this instance can child scroll up; otherwise, <c>false</c>.</returns>
		public override bool CanChildScrollUp ()
		{
			var child = packed.GetChildAt (0) as Android.Widget.ListView;
			if (child != null) {
				if (child.FirstVisiblePosition == 0) {
					var subChild = child.GetChildAt (0);

					return subChild != null && subChild.Top != 0;
				}

				//if children are in list and we are scrolled a bit... sure you can scroll up
				return true;
			}

			//TODO: handle other View Types here such as scroll view... :(

			//no children
			return false;
		}

		/// <summary>
		/// Helpers to cast our element easily
		/// Will throw an exception if the Element is not correct
		/// </summary>
		/// <value>The refresh view.</value>
		public PullToRefreshContentView RefreshView
		{
			get { return this.Element == null ? null : (PullToRefreshContentView)Element; }
		}

		public override bool Refreshing {
			get {
				return base.Refreshing;
			}
			set {
				//this will break binding :( sad panda we need to wait for next version for this
				//right now you can't update the binding.. so it is 1 way
				//if(RefreshView != null)
				//	RefreshView.IsRefreshing = value;

				base.Refreshing = value;
			}
		}

		/// <summary>
		/// The refresh view has been refreshed
		/// </summary>
		public void OnRefresh ()
		{
			//someone pulled down to refresh or it is done
			if (RefreshView == null)
				return;
				
			var command = this.RefreshView.RefreshCommand;
			if (command == null)
				return;

			command.Execute (null);
		}

		/// <summary>
		/// Handles the property changed.
		/// Update the control and trigger refreshing
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="e">E.</param>
		void HandlePropertyChanged (object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			if (e.PropertyName == PullToRefreshContentView.IsRefreshingProperty.PropertyName) {
				this.Refreshing = this.RefreshView.IsRefreshing;
			} else if (e.PropertyName == "Content") {
				UpdateContent ();
			}
		}

		/// <summary>
		/// Gets the size of the desired.
		/// </summary>
		/// <returns>The desired size.</returns>
		/// <param name="widthConstraint">Width constraint.</param>
		/// <param name="heightConstraint">Height constraint.</param>
		public SizeRequest GetDesiredSize (int widthConstraint, int heightConstraint)
		{
			packed.Measure (widthConstraint, heightConstraint);

			//Measure child here and determine size
			return new SizeRequest (new Size (packed.MeasuredWidth, packed.MeasuredHeight));
		}

		public void UpdateLayout ()
		{
			if (Tracker == null)
				return;

			Tracker.UpdateLayout ();
		}

		public VisualElementTracker Tracker {
			get;
			private set;
		}

		public Android.Views.ViewGroup ViewGroup {
			get{ return this; }
		}

		public VisualElement Element {
			get;
			private set;
		}
	}
}

