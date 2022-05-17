using Itau.Notificacao.Dominio.Contrato.Service;
using Itau.Notificacao.Dominio.Model.Entidade;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Itau.Notificacao.Service
{
    public class NotificacaoSMSService : INotificacaoEmailService
    {
        public Task Enviar(NotificacaoEntidade notificacao)
        {
            throw new Exception("Codigo nao implementado");
        }
    }
}
