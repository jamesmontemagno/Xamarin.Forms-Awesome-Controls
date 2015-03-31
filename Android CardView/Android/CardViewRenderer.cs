using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Android.Views;
using CardViewForms;
using Android.Support.V7.Widget;
using CardViewFormsAndroid;
using Android.Widget;

[assembly:ExportRendererAttribute(typeof(CardContentView), typeof(CardViewRenderer))]
namespace CardViewFormsAndroid
{
    public class CardViewRenderer : ViewRenderer<CardContentView, CardView>
    {
        protected override void OnElementChanged(ElementChangedEventArgs<CardContentView> e)
        {
            base.OnElementChanged(e);

            if (Control == null)
            {
                var control = new CardView(Forms.Context);

                control.UseCompatPadding = true;

                SetNativeControl(control);
            }
        }

        protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (Control != null)
            {

                if (e.PropertyName == "BackgroundColor" || e.PropertyName == "Renderer")
                {
                    Control.SetCardBackgroundColor(Element.BackgroundColor.ToAndroid());                   
                }

                if (e.PropertyName == "Padding" || e.PropertyName == "Renderer")
                {
                    Control.SetPadding((int)Element.Padding.Left, (int)Element.Padding.Top, (int)Element.Padding.Right, (int)Element.Padding.Bottom);
                }

                if (e.PropertyName == "CornderRadius" || e.PropertyName == "Renderer")
                {
                    Control.Radius = Element.CornderRadius;
                }
            }
        }
    }  
}
