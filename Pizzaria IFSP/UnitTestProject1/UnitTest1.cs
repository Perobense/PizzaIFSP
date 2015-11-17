using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Domain;
using Persistence;


namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            MPRepository rp = new MPRepository();
            var x = rp.Login("Vitor", "123");

        }
    }
}
