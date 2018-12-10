using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Tecnofit.ViewModels.Services;
using Tecnofit.Views.Services;
using Xamarin.Forms;
using System;

[assembly: Dependency(typeof(RestService))]
namespace Tecnofit.Views.Services
{
    class RestService : IRestService
    {
        public async Task<dynamic> Get(string restRequest, string token)
        {
            await Task.Yield();
            try
            {
                var restClient = new RestClient(App.RestUrl);
                var request = new RestRequest(restRequest, Method.GET) { RequestFormat = DataFormat.Json };
                request.AddParameter("Authorization", string.Format("Bearer " + token), ParameterType.HttpHeader);
                request.AddHeader("Content-Type", "application/json");
                var content = restClient.Execute(request);

                if (content.IsSuccessful)
                {
                    return JObject.Parse(content.Content);
                }
                return content.ErrorMessage;
            }
            catch (Exception e)
            {
                return e.Message;
            }

        }

        public async Task<dynamic> Post(dynamic item, string restRequest, string token)
        {
            await Task.Yield();

            try
            {
                var json = JsonConvert.SerializeObject(item);
                var restClient = new RestClient(App.RestUrl);
                var request = new RestRequest(restRequest, Method.POST) { RequestFormat = DataFormat.Json };

                if (!string.IsNullOrWhiteSpace(token))
                    request.AddParameter("Authorization", string.Format("Bearer " + token), ParameterType.HttpHeader);

                request.AddHeader("Content-Type", "application/json");
                request.AddParameter("application/json; charset=utf-8", json, ParameterType.RequestBody);

                var content = restClient.Execute(request);
                if (content.IsSuccessful)
                {
                    return JObject.Parse(content.Content);
                }
                return content.ErrorMessage;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}