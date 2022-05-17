using Microsoft.VisualStudio.TestTools.UnitTesting;
using Itau.Notificacao.Service;
using System;
using System.Collections.Generic;
using System.Text;
using Itau.Notificacao.Dominio.Contrato.Service;
using Itau.Notificacao.Dominio.Model.Entidade;

namespace Itau.Notificacao.Service.Tests
{
    [TestClass()]
    public class NotificacaoEmailServiceTests
    {
        [TestMethod()]
        public void Deve_Enviar()
        {
            INotificacaoEmailService notificacao = new NotificacaoEmailService();
            notificacao.Enviar(new NotificacaoEntidade
            {
                Titulo = "Titulo Teste",
                Mensagem = "Mensagem teste",
                Destinatario = "robsonsilvar@gmail.com",
                Remetente = "robsonsilvar@gmail.com"
            });

        }
    }
}