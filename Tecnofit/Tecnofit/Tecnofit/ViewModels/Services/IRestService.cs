using System.Threading.Tasks;

namespace Tecnofit.ViewModels.Services
{
    public interface IRestService
    {
        Task<dynamic> Post(dynamic item, string restRequest, string token);
        Task<dynamic> Get(string restRequest, string token);
    }
}
