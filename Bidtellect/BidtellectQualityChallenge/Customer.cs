using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BidtellectQualityChallenge.Models;
using RestSharp;

namespace BidtellectQualityChallenge.Engines
{
    class Customers : Engine
    {

        private Model Customer;

        public Customers(string endPoint, Model customer) : base(endPoint, customer)
        {

            this.Customer = customer;

        }

        public IRestResponse UserRequest()
        {

            return Request(Customer);

        }

    }
}
