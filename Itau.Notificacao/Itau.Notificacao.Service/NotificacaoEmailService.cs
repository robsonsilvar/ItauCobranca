using System;
using System.Collections.Generic;
using System.Text;
using Itau.Notificacao.Dominio.Model.Entidade;
using Itau.Notificacao.Dominio.Contrato.Service;

using System.Threading.Tasks;
using System.Net.Mail;
using System.Net;
using Itau.Notificacao.Dominio;

namespace Itau.Notificacao.Service
{
    public class NotificacaoEmailService : INotificacaoEmailService
    {
        public void Enviar(NotificacaoEntidade notificacao)
        {
            try
            {
                var fromAddress = new MailAddress("robsonsilvar@gmail.com", "Itaú Cobrança");
                var toAddress = new MailAddress(notificacao.Destinatario, notificacao.Destinatario);
                string fromPassword = "ItauTestegmail";
                string subject = notificacao.Titulo;
                string body = notificacao.Mensagem;

                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential("robsonsilvar@gmail.com", fromPassword)
                };

                var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    Body = body
                };

                smtp.Send(message);
               
            }
            catch(Exception e)
            {
                //loga erro

                throw new NotificacaoException("Não foi possível enviar e-mail");
            }

        }
    }
}
