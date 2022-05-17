using System;
using System.Collections.Generic;
using System.Text;
using Itau.Notificacao.Dominio.Model.Entidade;
using Itau.Notificacao.Dominio.Contrato.Service;

using System.Threading.Tasks;

namespace Itau.Notificacao.Service
{
    public class NotificacaoEmailService: INotificacaoEmailService
    {
        public Task Enviar(NotificacaoEntidade notificacao) {              
            throw new Exception("Codigo nao implementado");
        }
    }
}
