using Tecnofit.Views;
using Xamarin.Forms;

namespace Tecnofit
{
	public partial class App : Application
	{
        public static string AppName = "Tecnofit";
        public static string RestUrl = "http://api.tecnofit.com.br";
        public static string RegisterRequest = "/financeiro/conta-pr/cadastro";
        public static string LoginRequest = "/auth";
        public static string ListRequest = "/financeiro/extrato/dia/1036/";
        public static string PickerRequest = "/financeiro/conta-pr/1036/R";
        public static string Token;

        public App ()
		{
			InitializeComponent();

			MainPage = new NavigationPage(new LoginViewPage());
		}

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
