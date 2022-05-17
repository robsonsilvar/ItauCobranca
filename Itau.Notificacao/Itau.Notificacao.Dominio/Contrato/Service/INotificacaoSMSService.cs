using Itau.Notificacao.Dominio.Model.Entidade;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Itau.Notificacao.Dominio.Contrato.Service
{
    public interface INotificacaoSMSService
    {
        void Enviar(NotificacaoEntidade notificacao);
    }
}
