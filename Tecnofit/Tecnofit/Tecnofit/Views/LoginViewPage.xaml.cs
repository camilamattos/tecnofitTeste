using Tecnofit.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Tecnofit.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoginViewPage : ContentPage
	{
		public LoginViewPage ()
		{
            BindingContext = new LoginViewModel();
			InitializeComponent ();
		}
	}
}