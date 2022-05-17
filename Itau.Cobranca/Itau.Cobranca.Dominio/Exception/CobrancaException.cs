using System;
using System.Collections.Generic;
using System.Text;

namespace Itau.Cobranca.Dominio
{
    public class CobrancaException : Exception
    {
        public CobrancaException(string msg) : base(msg) { }
    }
}
