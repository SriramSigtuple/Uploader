using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Cloud_Models;
namespace Tests
{
   public class MD5HelperTests
    {
        [SetUp]
        public void Setup()
        {

        }
        [TearDown]
        public void TearDown()
        {
            
        }
        [Test]
        public void GetMd5HashForString()
        {
            #region Arrange 
            var inputValue = "sriram";
            var expectedValue = "BF0760F552EEAF6064CF7E7E33E25201";
            var actualValue = string.Empty;
            #endregion

            #region Act calling the method to be unit tested

            actualValue = MD5Helper.GetMd5Hash(inputValue);
            #endregion

            #region Assert 
            Assert.AreEqual(expectedValue, actualValue);
            #endregion


        }
    }
}
