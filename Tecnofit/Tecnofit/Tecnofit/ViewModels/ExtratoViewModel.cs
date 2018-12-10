using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Tecnofit.Models;
using Tecnofit.Views;
using Xamarin.Forms;

namespace Tecnofit.ViewModels
{
    public class ExtratoViewModel : ViewModelBase
    {
        public ExtratoViewModel()
        {
            AddTransacaoCommand = new Command(AddTransacao);
        }

        private async Task GetDados()
        {
            try
            {
                var accountStatement = (await RestService.Get($"{App.ListRequest}{DataSelecionada:yyyy-MM-dd}", App.Token)).accountStatement;

                if ((accountStatement as string) != null)
                    return;

                TotalSaidas = $"R$ {accountStatement.accountsReceivable}";
                TotalEntradas = $"R$ {accountStatement.accountsPayable}";
                var aaa = GetArray(accountStatement.accounts);
                ListaContas = GetArray(accountStatement.accounts);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private async void AddTransacao(object obj)
        {
            await App.Current.MainPage.Navigation.PushAsync(new NewTransactionViewPage());
        }

        #region BINDINGS
        public ICommand AddTransacaoCommand { get; set; }

        private DateTime _dataSelecionada = DateTime.Now;
        public DateTime DataSelecionada
        {
            get { return _dataSelecionada; }
            set
            {
                SetProperty(ref _dataSelecionada, value);
                GetDados();
            }
        }

        private string _totalEntradas;
        public string TotalEntradas
        {
            get { return _totalEntradas; }
            set { SetProperty(ref _totalEntradas, value); }
        }
        private string _totalSaidas;
        public string TotalSaidas
        {
            get { return _totalSaidas; }
            set { SetProperty(ref _totalSaidas, value); }
        }
        IEnumerable<ContasModel> _listaContas;
        public IEnumerable<ContasModel> ListaContas
        {
            get { return _listaContas; }
            set { SetProperty(ref _listaContas, value); }
        }
        private string _accountCategory;
        public string accountCategory
        {
            get { return _accountCategory; }
            set
            {
                if(value.Contains("ENTRADA"))
                {
                    SetProperty(ref _accountCategory, "ENTRADA");
                    ImageList = "rightArrow.png";
                }
                else
                {
                    SetProperty(ref _accountCategory, "SAIDA");
                    ImageList = "leftArrow.png";
                }
            }
        }
        private string _imageList;
        public string ImageList
        {
            get { return _imageList; }
            set { SetProperty(ref _accountCategory, value); }
        }
        #endregion

        private IEnumerable<ContasModel> GetArray(JArray jArray) => jArray.ToObject<IEnumerable<ContasModel>>();
    }
}
