using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BidtellectQualityChallenge.Engines;
using BidtellectQualityChallenge.Models;
using Newtonsoft.Json;
using NUnit.Framework;
using RestSharp;
using System.Net;

namespace BidtellectQualityChallenge
{
    [TestFixture]
    public class UserTests
    {

        //The use of the TestCaseSource Attribute allows for the data used in the tests to not be required at compile time.  

            

        const string blanks = "    ";
        const string emptyStr = "";
        const string saveEndPointConst = "/api/user/save";
        const string linkEndPointConst = "/api/user/link";

        private static IEnumerable<TestCaseData> SaveUserTestData
        {
            get
            {

                yield return new TestCaseData("lowercase", "wired", 2, new string[] { });
                yield return new TestCaseData("lowercase", "wired", 1, new string[] { });
                yield return new TestCaseData("UPPERCASE", "WIRED", 1, new string[] {});
                yield return new TestCaseData("*&^%$$##", "(*&^#@!)", 1, new string[] { });
                yield return new TestCaseData("HTTP://CNN.com", "WIRED", 1, new string[] { });
                yield return new TestCaseData("Bob", "HTTP://CNN.COM", 1, new string[] { });
                yield return new TestCaseData(@"\\jbjdsjfbdsjfbjdsbjfbsdjfbsdiiiuh76487632846832723ghvdjkjnkfnsdinfwnninfwehoiuoiuwoiuewoufweuirguyegruyweguirwggweyrgweuygrweuygruwieyg20-02-032-04-0842904209480", "WIRED", 1, new string[] { });
                yield return new TestCaseData("Bob", "98409234o3hih234i2348978979dsf87jkdnkfkdngkddssdfkjdsnfsdkfjnfksdjfksddddddddddddddddddddddddddddddnksdfn9877f98sd7f79dsf7sdfsd98f7sd9f7sd98f7d9s8f7sd998sdf", 1, new string[] { });
                yield return new TestCaseData(blanks, "Wired", 1, new string[] { "First name cannot be blank" });
                yield return new TestCaseData(emptyStr, "Wired", 1, new string[] { "First name cannot be blank" });
                yield return new TestCaseData(null, "Wired", 1, new string[] { "First name cannot be blank" });   
                yield return new TestCaseData("Bob", null, 1, new string[] { "Last name cannot be blank" });
                yield return new TestCaseData("Bob", blanks, 1, new string[] { "Last name cannot be blank" });
                yield return new TestCaseData(blanks, "Wired", 0, new string[] {"First name cannot be blank", "Invalid payment type"});
                yield return new TestCaseData( null, "Wired", 0, new string[] { "First name cannot be blank", "Invalid payment type" });   
                yield return new TestCaseData("Bob",null, 0, new string[] { "Last name cannot be blank", "Invalid payment type" });
                yield return new TestCaseData("Bob", " ", 0, new string[] { "Last name cannot be blank", "Invalid payment type" });
                yield return new TestCaseData("Bob", "Schaubhut", 0, new string[] { "Invalid payment type" });
                yield return new TestCaseData("Bob", "Wired", 3, new string[] { "Invalid payment type" });
                yield return new TestCaseData("Bob", emptyStr, 1, new string[] { "Last name cannot be blank" });
                yield return new TestCaseData("Bob", "Wired", 1, new string[] { });
                yield return new TestCaseData("Check", "Wire", 1, new string[] { });
                yield return new TestCaseData("Check", "Wire", -1, new string[] { "Invalid payment type" });
    
            }
        }

        

        [Test, TestCaseSource(nameof(SaveUserTestData))]
        public void Save_User_Endpoint_Test(string firstName,string lastName, int paymentType, string[] expectedResults)
        {
            
            IRestResponse response = null;

            Model user = new Model(firstName, lastName, paymentType);

            Users userEndPoint = new Users(saveEndPointConst, user);

            response = userEndPoint.UserRequest();

            DeserializeResult results = JsonConvert.DeserializeObject<DeserializeResult>(response.Content);

            string success =  results.Success;

            string[] Errors = results.Errors.Cast<string>().ToArray();

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

            for (int errorIndex=0;errorIndex < Errors.Length;errorIndex++)
            {

                Assert.AreEqual(expectedResults[errorIndex], Errors[errorIndex]);

            }
            
        }

        private static IEnumerable<TestCaseData> SaveUserTestData2
        {
            get
            {

                yield return new TestCaseData("0", "1000", new string[] { });
                yield return new TestCaseData("100", "1000", new string[] { });
                yield return new TestCaseData("55", "1000", new string[] { });   // Valid
                yield return new TestCaseData("101", "1000", new string[] { "Invalid UserId" });
                yield return new TestCaseData("-1", "1000", new string[] { "Invalid UserId" });
                yield return new TestCaseData("-101", "1000", new string[] { "Invalid UserId" });
                yield return new TestCaseData("0", "1000", new string[] { });   // Valid
                yield return new TestCaseData("0", "2000", new string[] { });   // Valid
                yield return new TestCaseData("0", "2001", new string[] { "Invalid CustomerId" });
                yield return new TestCaseData("0", "999", new string[] { "Invalid CustomerId" });
                yield return new TestCaseData("0", "-1", new string[] { "Invalid CustomerId" });
                yield return new TestCaseData("-1", "999", new string[] { "Invalid UserId","Invalid CustomerId" });
                yield return new TestCaseData("-1", "-999", new string[] { "Invalid UserId", "Invalid CustomerId" });
                yield return new TestCaseData("-200", "-2000", new string[] { "Invalid UserId", "Invalid CustomerId" });
                yield return new TestCaseData("327676", "32767", new string[] { "Invalid UserId", "Invalid CustomerId" });
                yield return new TestCaseData("ABCD", "ABCD", new string[] { "Invalid UserId", "Invalid CustomerId" });
                yield return new TestCaseData("x20", "x20", new string[] { "Invalid UserId", "Invalid CustomerId" });
                yield return new TestCaseData(".", ".", new string[] { "Invalid UserId", "Invalid CustomerId" });
                yield return new TestCaseData("Check", "check", new string[] { "Invalid UserId", "Invalid CustomerId" });
                yield return new TestCaseData(".001", ".001", new string[] { "Invalid UserId", "Invalid CustomerId" });
                yield return new TestCaseData("1.1", "1.1", new string[] { "Invalid UserId", "Invalid CustomerId" });
                yield return new TestCaseData("/柒", "/柒", new string[] { "Invalid UserId", "Invalid CustomerId" });
                yield return new TestCaseData("", "", new string[] { "Invalid UserId", "Invalid CustomerId" });
                yield return new TestCaseData(" ", " ", new string[] { "Invalid UserId", "Invalid CustomerId" });
                yield return new TestCaseData(null, null, new string[] { "Invalid UserId", "Invalid CustomerId" });
   
            }
        }



        [Test, TestCaseSource(nameof(SaveUserTestData2))]
        public void Link_Endpoint_Test(string  userID,string customerID, string[] expectedResults)
        {

            IRestResponse response = null;

            Model customer = new Model(userID, customerID);

            Customers customerEndPoint = new Customers(linkEndPointConst, customer);

            response = customerEndPoint.UserRequest();

            DeserializeResult results = JsonConvert.DeserializeObject<DeserializeResult>(response.Content);

            string success = results.Success;

            string[] Errors = results.Errors.Cast<string>().ToArray();

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

            for (int errorIndex = 0; errorIndex < Errors.Length; errorIndex++)
            {

                Assert.AreEqual(expectedResults[errorIndex], Errors[errorIndex]);

            }

        }

    }
}
