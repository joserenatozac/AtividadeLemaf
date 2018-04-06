using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AtividadeLemafJoseRenato.Util;

namespace AtividadeLemafJoseRenato.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            InicializadorBanco.InicializaBanco();
            Assert.IsTrue(true);
        }
    }
}
