using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BidtellectQualityChallenge.Models;
using RestSharp;

namespace BidtellectQualityChallenge.Engines
{
     class Users : Engine
    {

        private Model User;

        public Users(string endPoint, Model user) : base(endPoint, user)
        {

            this.User = user;

        }

        public IRestResponse UserRequest() 
        {

            return Request(User);

        }

    }
}