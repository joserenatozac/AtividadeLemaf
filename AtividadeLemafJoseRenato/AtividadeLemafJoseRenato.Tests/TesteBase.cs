using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AtividadeLemafJoseRenato.Util;

namespace AtividadeLemafJoseRenato.Tests
{
    [TestClass]
    public class TesteBase
    {
        public TesteBase()
        {
            InicializadorBanco.InicializaBanco();
        }
    }
}
