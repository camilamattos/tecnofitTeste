using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Tecnofit.ViewModels.Services;
using Xamarin.Forms;

namespace Tecnofit.ViewModels
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        protected IRestService RestService { get; private set; }

        public ViewModelBase()
        {
            RestService = DependencyService.Get<IRestService>();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs((propertyName)));
        }

        protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName]string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(storage, value))
            {
                return false;
            }
            storage = value;
            OnPropertyChanged(propertyName);

            return true;
        }
        public async Task ShowDialog(string message)
        {
            await Xamarin.Forms.Application.Current.MainPage.DisplayAlert(App.AppName, message, "Ok");
        }

        protected async void SaveInfo(string property, object file)
        {
            if (Application.Current.Properties.ContainsKey(property))
                Application.Current.Properties[property] = file;
            else
                Application.Current.Properties.Add(property, file);

            await Application.Current.SavePropertiesAsync();
        }
    }
}
