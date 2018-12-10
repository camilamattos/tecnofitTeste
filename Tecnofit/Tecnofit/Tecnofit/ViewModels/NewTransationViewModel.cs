using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Tecnofit.Models;
using Xamarin.Forms;

namespace Tecnofit.ViewModels
{
    public class NewTransationViewModel : ViewModelBase
    {
        private dynamic Pickers;

        public NewTransationViewModel()
        {
            GetDados();
            CriarTransacaoCommand = new Command(CriarTransacao);
        }

        private async void CriarTransacao(object obj)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(CategoriaSelected) || string.IsNullOrWhiteSpace(CentroResponsabilidadeSelected) || string.IsNullOrWhiteSpace(FormaCobrancaSelected) || string.IsNullOrWhiteSpace(ContaBancariaSelected) || string.IsNullOrWhiteSpace(DataVencimento)
                    || string.IsNullOrWhiteSpace(NomeTransacao) || string.IsNullOrWhiteSpace(SaldoFinal))
                {
                    await ShowDialog("Nenhum campo pode estar em branco.");
                    return;
                }

                var result = await RestService.Post(new TransactionModel
                {
                    id = null,
                    empresaId = 1036,
                    tipoConta = "R",
                    categoriaContaId = GetId(Pickers.parametros.categoriaConta, CategoriaSelected),
                    centroResponsabilidadeId = GetId(Pickers.parametros.centroResponsabilidade, CentroResponsabilidadeSelected),
                    formaPagamentoId = GetId(Pickers.parametros.formaCobranca, FormaCobrancaSelected),
                    contaBancoId = GetId(Pickers.parametros.contaBanco, ContaBancariaSelected),
                    formaCobrancaId = null,
                    dataVencimento = Convert.ToDateTime(DataVencimento).ToString("yyyy-MM-dd"),
                    dataPagamento = IsValorPago ? Convert.ToDateTime(DataPagamento).ToString("yyyy-MM-dd") : null,
                    historico = NomeTransacao,
                    valor = Convert.ToDecimal(SaldoFinal),
                    valorPago = IsValorPago ? Convert.ToDecimal(ValorPago) : 0,
                    valorDesconto = Convert.ToDecimal(Desconto),
                    valorMulta = Convert.ToDecimal(Multa),
                    status = 0

                }, App.RegisterRequest, App.Token);

                if ((result as string) == null)
                {
                    await ShowDialog("Cadastro de conta realizado.");
                    await App.Current.MainPage.Navigation.PopAsync();
                }
                else
                    await ShowDialog("Verifique os dados inseridos.");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private async Task GetDados()
        {
            Pickers = await RestService.Get(App.PickerRequest, App.Token);

            if ((Pickers as string) != null)
                return;

            try
            {
                FormaCobranca = GetArray(Pickers.parametros.formaCobranca);
                Categoria = GetArray(Pickers.parametros.categoriaConta);
                CentroResponsabilidade = GetArray(Pickers.parametros.centroResponsabilidade);
                ContaBancaria = GetArray(Pickers.parametros.contaBanco);
            }
            catch (Exception e)
            {
                await ShowDialog("Verifique seus dados.");
            }
        }

        #region BINDINGS
        private string _nomeTransacao;
        public string NomeTransacao
        {
            get { return _nomeTransacao; }
            set { SetProperty(ref _nomeTransacao, value); }
        }
        private string _dataVencimento;
        public string DataVencimento
        {
            get { return _dataVencimento; }
            set { SetProperty(ref _dataVencimento, value); }
        }
        private string _desconto;
        public string Desconto
        {
            get { return _desconto; }
            set { SetProperty(ref _desconto, value); }
        }
        private string _multa;
        public string Multa
        {
            get { return _multa; }
            set { SetProperty(ref _multa, value); }
        }
        private string _saldoFinal;
        public string SaldoFinal
        {
            get { return _saldoFinal; }
            set { SetProperty(ref _saldoFinal, value); }
        }
        private bool _isValorPago;
        public bool IsValorPago
        {
            get { return _isValorPago; }
            set { SetProperty(ref _isValorPago, value); }
        }

        private string _valorPago;
        public string ValorPago
        {
            get { return _valorPago; }
            set { SetProperty(ref _valorPago, value); }
        }
        private string _dataPagamento;
        public string DataPagamento
        {
            get { return _dataPagamento; }
            set { SetProperty(ref _dataPagamento, value); }
        }
        private List<string> _contaBancaria;
        public List<string> ContaBancaria
        {
            get { return _contaBancaria; }
            set { SetProperty(ref _contaBancaria, value); }
        }
        private string _contaBancariaSelected;
        public string ContaBancariaSelected
        {
            get { return _contaBancariaSelected; }
            set { SetProperty(ref _contaBancariaSelected, value); }
        }
        private List<string> _centroResponsabilidade;
        public List<string> CentroResponsabilidade
        {
            get { return _centroResponsabilidade; }
            set { SetProperty(ref _centroResponsabilidade, value); }
        }
        private string _centroResponsabilidadeSelected;
        public string CentroResponsabilidadeSelected
        {
            get { return _centroResponsabilidadeSelected; }
            set { SetProperty(ref _centroResponsabilidadeSelected, value); }
        }
        private List<string> _categoria;
        public List<string> Categoria
        {
            get { return _categoria; }
            set { SetProperty(ref _categoria, value); }
        }
        private string _categoriaSelected;
        public string CategoriaSelected
        {
            get { return _categoriaSelected; }
            set { SetProperty(ref _categoriaSelected, value); }
        }
        private List<string> _formaCobranca;
        public List<string> FormaCobranca
        {
            get { return _formaCobranca; }
            set { SetProperty(ref _formaCobranca, value); }
        }
        private string _formaCobrancaSelected;
        public string FormaCobrancaSelected
        {
            get { return _categoriaSelected; }
            set { SetProperty(ref _categoriaSelected, value); }
        }
        public ICommand CriarTransacaoCommand { get; set; }
        #endregion

        private IEnumerable<string> GetArray(JArray jArray) => jArray.ToObject<IEnumerable<PickerModel>>().Select(jo => jo.Nome).ToList();

        private int GetId(JArray jArray, string name) => Convert.ToInt32(jArray.ToObject<IEnumerable<PickerModel>>().Where(jo => jo.Nome == name).Select(jo => jo.Id).FirstOrDefault());

    }
}
