using System;

namespace Itau.Cobranca.Dominio
{
    public class CobrancaEntidade
    {
        public int Id { get; set; }
        public decimal ValorCobranca { get; set; }     
        public string ClienteNome { get; set; }
        public string ClienteEndereco { get; set; }
        public string ClienteCelular { get; set; }
        public string ClienteEmail { get; set; }
        public bool? Enviado { get; set; }

    }
}
