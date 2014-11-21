using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace CustomProgressBar
{	
	public partial class ProgressBarXAML : ContentPage
	{	
		public ProgressBarXAML ()
		{
			InitializeComponent ();
			this.BindingContext = new ProgressBarViewModel ();
		}
	}
}

