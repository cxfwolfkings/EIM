using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Bow.Enter.Views;
using Xamarin.Forms;

namespace Bow.Enter.ViewModels
{
    /// <summary>
	/// ViewModel to demonstrate binding to a Command from a GestureRecognizer
	/// </summary>
	/// <remarks>
	/// View models can be used regardless of whether the UI is build in code or with Xaml.
	/// In this example the view model is referenced by a Xaml page, but the same bindings
	/// can be done in C#.
	/// </remarks>
	public class FuncViewModel : INotifyPropertyChanged
    {
        public FuncViewModel()
        {
            // configure the FuncCommand with a method
            FuncCommand = new Command(OnTapped);
        }

        /// <summary>
        /// Expose the FuncCommand via a property so that Xaml can bind to it
        /// </summary>
        public ICommand FuncCommand { get; }

        /// <summary>
        /// Called whenever TapCommand is executed (because it was wired up in the constructor)
        /// </summary>
        void OnTapped(object s)
        {
            // Debug.WriteLine("parameter: " + s);
            switch (s.ToString())
            {
                case "0.0":
                    Application.Current.MainPage.Navigation.PushAsync(new NavigationPage(new MainPage()));
                    break;
                case "0.1":
                    break;
                case "0.2":
                    break;
                case "1.0":
                    break;
                case "1.1":
                    break;
                case "1.2":
                    break;
                case "2.0":
                    break;
                case "2.1":
                    break;
                case "2.2":
                    break;
            }
        }

        #region INotifyPropertyChanged 
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
