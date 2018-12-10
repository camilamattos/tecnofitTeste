using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Tecnofit.Models;
using Tecnofit.Views;
using Xamarin.Forms;

namespace Tecnofit.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        public LoginViewModel()
        {
            LoginCommand = new Command(Login);
        }

        private async void Login(object obj)
        {
            await Task.Delay(200);

            var token = await RestService.Post(new UserModel { email = Email, password = Senha }, App.LoginRequest, string.Empty);

            if (token == null)
                return;

            try
            {
                SaveInfo("Token", token.token);
                App.Token = token.token;

                await App.Current.MainPage.Navigation.PushAsync(new ExtratoViewPage());
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            Senha = string.Empty;
            Email = string.Empty;
        }

        public ICommand LoginCommand { get; set; }

        private string _email = "xamarin@tecnofit.com.br";
        public string Email
        {
            get { return _email; }
            set { SetProperty(ref _email, value); }
        }

        private string _senha = "xamarin";
        public string Senha
        {
            get { return _senha; }
            set { SetProperty(ref _senha, value); }
        }

    }
}
