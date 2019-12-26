using Bow.Enter.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Bow.Enter.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FunctionPage : ContentPage
    {
        public FunctionPage()
        {
            InitializeComponent();
            // The FuncViewModel contains the FuncCommand which is wired up in Xaml
            BindingContext = new FuncViewModel();
        }
    }
}