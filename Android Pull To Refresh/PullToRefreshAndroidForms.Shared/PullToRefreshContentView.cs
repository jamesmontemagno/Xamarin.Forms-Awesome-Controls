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

using Xamarin.Forms;

namespace PullToRefreshAndroidForms
{
	public class PullToRefreshContentView : ContentView
	{
		public static readonly BindableProperty IsRefreshingProperty = 
			BindableProperty.Create<PullToRefreshContentView,bool> (
				p => p.IsRefreshing, false);

		public bool IsRefreshing {
			get { return (bool)GetValue (IsRefreshingProperty); }
			set { SetValue (IsRefreshingProperty, value); }
		}

		public static readonly BindableProperty RefreshCommandProperty = 
			BindableProperty.Create<PullToRefreshContentView,Command> (
				p => p.RefreshCommand, null);

		public Command RefreshCommand {
			get { return (Command)GetValue (RefreshCommandProperty); }
			set { SetValue (RefreshCommandProperty, value); }
		}

		/// <param name="widthConstraint">The available width for the element to use.</param>
		/// <param name="heightConstraint">The available height for the element to use.</param>
		/// <summary>
		/// Optimization as we can get the size here of our content all in DIP
		/// </summary>
		protected override SizeRequest OnSizeRequest (double widthConstraint, double heightConstraint)
		{
			if (Content == null)
				return new SizeRequest(new Size(100, 100));

			return Content.GetSizeRequest (widthConstraint, heightConstraint);
		}
	}
}

