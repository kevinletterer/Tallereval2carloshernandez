using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApplication4.Models.Token;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication4.Models.Token.Tests
{
    [TestClass()]
    public class TokenTests
    {
        static Token Token = new Token();
        [TestMethod()]
        public void checkUserTest()
        {
            bool ok = Token.checkUser("Kalz", "Kalz123");

            Assert.AreEqual(true,ok);
        }

        [TestMethod()]
        public void getUserIdTest()
        {
            int id = Token.getUserId("Kalz", "Kalz123");

            Assert.AreEqual(9, id);
        }

        [TestMethod()]
        public void createTokenTest()
        {
            string token = Token.createToken("Kalz", "Kalz123");
            bool ok = Token.checkTokenValid(token);

            System.Console.WriteLine(token);
            Assert.AreEqual(true, ok);
        }

        [TestMethod()]
        public void checkOldTokenTest()
        {
            bool ok = Token.checkTokenValid("D");
            Assert.AreEqual(false, ok);
        }

    }
}