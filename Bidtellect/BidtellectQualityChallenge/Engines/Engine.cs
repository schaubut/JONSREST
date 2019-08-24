using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;

namespace BidtellectQualityChallenge.Engines
{
    class Engine
    {


        private readonly RestClient _client = new RestClient("http://mock.bidtellectual.com");

        private string EndPoint;

        private Models.Model Model;

        public Engine(string endPoint, Models.Model model)
        {

            this.EndPoint = endPoint;

           Model = model;

        }

        public IRestResponse Request(Models.Model model)
        {
            var request = new RestRequest(EndPoint, Method.PUT, DataFormat.Json);
            request.AddJsonBody(model);

            var response = _client.Execute(request);
           
           
            return response;
        }

    }
}
