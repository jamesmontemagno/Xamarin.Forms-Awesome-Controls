using System;
using Xamarin.Forms;

namespace CardViewForms
{
	public class CardContentView : ContentView
	{
		public static readonly BindableProperty CornerRadiusProperty = 
			BindableProperty.Create<CardContentView,float> 
		( p => p.CornderRadius, 3.0F);   

		public float CornderRadius 
		{
			get { return (float)GetValue (CornerRadiusProperty); } 
			set { SetValue (CornerRadiusProperty, value); } 
		}

		protected override SizeRequest OnSizeRequest (double widthConstraint, double heightConstraint)
		{
			if (Content == null)
				return new SizeRequest(new Size(100, 100));

			return Content.GetSizeRequest (widthConstraint, heightConstraint);
		}
	}
}

