using Itau.Notificacao.Dominio;
using Itau.Notificacao.Dominio.Contrato.Service;
using Itau.Notificacao.Dominio.Model.Entidade;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010.Account;


namespace Itau.Notificacao.Service
{
    public class NotificacaoSMSService : INotificacaoSMSService
    {
        public void Enviar(NotificacaoEntidade notificacao)
        {         
            string accountSid = "AC7099010415096bf267fe9fa17b148e70";
            string authToken = "3630934eae5dbf5a7f8166e9d4bc7943";
            string virtualPhone = "+19206956824";

            try
            {
                TwilioClient.Init(accountSid, authToken);
           
                MessageResource message = MessageResource.Create(
                    body: notificacao.Mensagem,
                    from: new Twilio.Types.PhoneNumber(virtualPhone),
                    to: new Twilio.Types.PhoneNumber(notificacao.Destinatario));
               
            }
            catch (Exception e)
            {
                //registra log

                throw new NotificacaoException("Não foi possível enviar SMS");
            }
            
        }
    }
}
