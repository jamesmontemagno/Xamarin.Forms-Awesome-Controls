using CircleImage;
using CircleImage.WinPhone;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using Xamarin.Forms;
using Xamarin.Forms.Platform.WinPhone;

[assembly: ExportRenderer(typeof(ImageCircle), typeof(ImageCircleRenderer))]

namespace CircleImage.WinPhone
{
  public class ImageCircleRenderer : ImageRenderer
  {

    public ImageCircleRenderer()
    {
    }

    protected override void OnElementChanged(ElementChangedEventArgs<Image> e)
    {
      base.OnElementChanged(e);

      if (e.OldElement != null || this.Element == null)
        return;

    }




    protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
      base.OnElementPropertyChanged(sender, e);

      if (Control != null && Control.Clip == null)
      {
        var min = Math.Min(Element.Width, Element.Height) / 2.0f;

        if (min <= 0)
          return;

        Control.Clip = new EllipseGeometry
        {
          Center = new System.Windows.Point(min, min),
          RadiusX = min,
          RadiusY = min
        };
      }
    }



  }
}
