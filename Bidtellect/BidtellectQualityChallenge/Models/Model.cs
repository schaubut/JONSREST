using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BidtellectQualityChallenge.Models
{
    class Model
    {
        private string UserID { get; set; }
        private string CustomerID { get; set; }
        private string FirstName { get; set; }
        private string LastName { get; set; }
        private int PaymentType { get; set; }

        public Model(string userID, string customerID)
        {

            this.UserID = userID;

            this.CustomerID = customerID;

        }

        public Model(string firstName, string lastName, int paymentType)
        {

            this.FirstName = firstName;

            this.LastName = lastName;

            this.PaymentType = paymentType;

        }

    }

    public enum PaymentType
    {
        Unknown = 0,
        Check = 1,
        Wire = 2
    }
}
