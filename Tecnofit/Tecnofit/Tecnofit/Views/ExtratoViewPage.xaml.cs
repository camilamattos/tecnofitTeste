using Tecnofit.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Tecnofit.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ExtratoViewPage : ContentPage
	{
		public ExtratoViewPage ()
		{
            BindingContext = new ExtratoViewModel();
            InitializeComponent();
		}
	}
}