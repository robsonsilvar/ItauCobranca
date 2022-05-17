using System;
using System.Collections.Generic;
using System.Text;

namespace Itau.Notificacao.Dominio
{
    public class NotificacaoException: Exception
    {
        public NotificacaoException(string msg) : base(msg) { }
    }
}
