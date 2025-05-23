using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace ApiRestaurante.Domain.Models.Exceptions
{
    public class ValidacaoException : Exception
    {
        public ValidacaoException() { }
        public ValidacaoException(string message)
            : base(message) { }

        public ValidacaoException(string message, Exception inner)
            : base(message, inner) { }
    }
}