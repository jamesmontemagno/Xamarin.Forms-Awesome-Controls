using System;
using System.ComponentModel;
using Xamarin.Forms;

namespace CustomProgressBar
{
	public class ProgressBarViewModel : INotifyPropertyChanged
	{
		public ProgressBarViewModel ()
		{
		}

		private bool isIndeterminate;

		/// <summary>
		/// Gets or sets a value indicating whether this instance is indeterminate.
		/// </summary>
		/// <value><c>true</c> if this instance is indeterminate; otherwise, <c>false</c>.</value>
		public bool IsIndeterminate
		{
			get { return isIndeterminate; }
			set { isIndeterminate = value; OnPropertyChanged ("IsIndeterminate");}
		}

		private float progress = 0.0f;


		/// <summary>
		/// Gets or sets the progress.
		/// </summary>
		/// <value>The progress.</value>
		public float Progress
		{
			get { return progress; }
			set {
				progress = value;
				OnPropertyChanged ("Progress");
			}
		}

		private double speed = 100;



		/// <summary>
		/// Gets or sets the speed f.
		/// I could use an IValueConverter here to convert from double to int if I needed to
		/// </summary>
		/// <value>The speed f.</value>
		public double SpeedF
		{
			get { return speed; }
			set {
				if (Math.Abs (speed - value) < double.Epsilon)
					return;

				speed = value;
				OnPropertyChanged ("Speed");
			}
		}
		/// <summary>
		/// Get to beind to the actual speed
		/// </summary>
		/// <value>The speed.</value>
		public int Speed
		{
			get { return (int)speed; }
		}


		private Command toggleIndeterminateCommand;
		/// <summary>
		/// When triggered InDeterminate flag will be flipped
		/// </summary>
		/// <value>The toggle indeterminate command.</value>
		public Command ToggleIndeterminateCommand
		{
			get { return toggleIndeterminateCommand ?? 
				(toggleIndeterminateCommand = new Command (ExecuteToggleIndeterminateCommand)); }
		}


		private void ExecuteToggleIndeterminateCommand()
		{
			IsIndeterminate = !IsIndeterminate;
		}

		private Command<string> addProgressCommand;
		/// <summary>
		/// Based on the "float" that is passed in via string progress will be added or subtracted
		/// </summary>
		/// <value>The add progress command.</value>
		public Command AddProgressCommand
		{
			get { return addProgressCommand ?? (addProgressCommand = new Command<string> (ExecuteAddProgressCommand)); }
		}


		private void ExecuteAddProgressCommand(string toAdd)
		{
			float addThis = 0.0F;
			if(float.TryParse(toAdd, out addThis))
				Progress += addThis;
		}

		/// <summary>
		/// Gets the default color of the progress bar
		/// </summary>
		/// <value>The color of the progress.</value>
		public Color ProgressColor 
		{
			get { return Color.FromHex ("3498DB"); }
		}

		/// <summary>
		/// Gets the default background color of the progress bar
		/// </summary>
		/// <value>The color of the progress background.</value>
		public Color ProgressBackgroundColor 
		{
			get { return Color.FromHex ("B4BCBC"); }
		}

		#region INotifyPropertyChanged implementation
		public event PropertyChangedEventHandler PropertyChanged;
		#endregion

		public void OnPropertyChanged(string propertyname)
		{
			if (PropertyChanged == null)
				return;

			PropertyChanged (this, new PropertyChangedEventArgs (propertyname));
		}
	}
}

