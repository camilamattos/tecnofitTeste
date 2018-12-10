using Tecnofit.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Tecnofit.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class NewTransactionViewPage : ContentPage
	{
		public NewTransactionViewPage ()
		{
            BindingContext = new NewTransationViewModel();
            InitializeComponent();
		}
	}
}