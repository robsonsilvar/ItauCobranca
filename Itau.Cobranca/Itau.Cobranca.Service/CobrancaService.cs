using Itau.Cobranca.Dados;
using Itau.Cobranca.Dominio;
using Itau.Cobranca.Dominio.Repository;
using Itau.Cobranca.Dominio.Service;
using Itau.Notificacao.Dominio;
using Itau.Notificacao.Dominio.Contrato.Service;
using Itau.Notificacao.Dominio.Model.Entidade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Itau.Cobranca.Service
{
    public class CobrancaService : ICobrancaService
    {
        private ICobrancaRepository _cobrancaRepository;
        private INotificacaoEmailService _notificadorEmail;
        private INotificacaoSMSService _notificadorSMS;
        public CobrancaService(ICobrancaRepository cobrancaDados,
            INotificacaoEmailService notificadorEmail,
            INotificacaoSMSService notificacaoSMS)
        {
            _cobrancaRepository = cobrancaDados;
            _notificadorEmail = notificadorEmail;
            _notificadorSMS = notificacaoSMS;
        }
        public async Task<IEnumerable<CobrancaEntidade>> ListarAsync(string filtro)
        {
            IEnumerable<CobrancaEntidade> cobrancas = await _cobrancaRepository.ListarAsync();
            switch (filtro)
            {               
                case "pendente":
                    cobrancas = cobrancas.Where(c => c.Enviado == null || c.Enviado == false).ToList();
                    break;
                case "enviado":
                    cobrancas = cobrancas.Where(c => c.Enviado != null  && (bool)c.Enviado).ToList(); 
                    break;
            }
            return cobrancas;
        }

        public async Task<CobrancaEntidade> ObterAsync(int idCobranca)
        {
            return await _cobrancaRepository.ObterAsync(idCobranca);
        }

        public async Task NotificarClientePorEmailAsync(int idCobranca)
        {
            Task<CobrancaEntidade> cobrancaTask = ObterAsync(idCobranca);
            CobrancaEntidade cobranca = await cobrancaTask;

            try
            {
                _notificadorEmail.Enviar(new NotificacaoEntidade
                {
                    Destinatario = cobranca.ClienteEmail,
                    Mensagem = string.Format("Isso é uma cobrança no valor de {0}", cobranca.ValorCobranca),
                    Remetente = "bancoitau@itau.com",
                    Titulo = "Cobrança"
                });

                await _cobrancaRepository.AtualizarEnviadoAsync(idCobranca);
            }
            catch (NotificacaoException e)
            {
                throw new CobrancaException(e.Message);
            }
        }

        public async Task NotificarClientePorSMSAsync(int idCobranca)
        {
            CobrancaEntidade cobranca = await ObterAsync(idCobranca);

            try
            {
                _notificadorSMS.Enviar(new NotificacaoEntidade
                {
                    Destinatario = cobranca.ClienteCelular,
                    Mensagem = string.Format("Isso é uma cobrança no valor de {0}", cobranca.ValorCobranca),
                    Remetente = "+551199999999",
                    Titulo = "Cobrança"
                });

                await _cobrancaRepository.AtualizarEnviadoAsync(idCobranca);
            }
            catch (NotificacaoException e)
            {
                throw new CobrancaException(e.Message);
            }

        }
        public async Task<int> InserirAsync(CobrancaEntidade cobranca)
        {
            Validar(cobranca);
            return await _cobrancaRepository.InserirAsync(cobranca);
        }

        private void Validar(CobrancaEntidade cobranca)
        {
            string msgErro = "";
            if (string.IsNullOrEmpty(cobranca.ClienteNome))
                msgErro = "Nome inválido";
            if (string.IsNullOrEmpty(cobranca.ClienteEndereco))
                msgErro = "Endereço inválido";
            if (string.IsNullOrEmpty(cobranca.ClienteEmail))
                msgErro = "Email inválido";
            if (string.IsNullOrEmpty(cobranca.ClienteCelular))
                msgErro = "Celular inválido";

            if (msgErro != "")
                throw new CobrancaException(msgErro);


        }
    }
}
