using System;

namespace Itau.Notificacao.Dominio.Model.Entidade
{
    public class NotificacaoEntidade
    {
        public string Titulo { get; set; }       
        public string Mensagem { get; set; }
        public string Destinatario { get; set; }
        public string Remetente { get; set; }
    }
}
