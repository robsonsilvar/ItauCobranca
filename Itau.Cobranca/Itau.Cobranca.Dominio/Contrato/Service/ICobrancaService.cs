using Itau.Cobranca.Dominio;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Itau.Cobranca.Dominio.Service
{
    public interface ICobrancaService
    {
        Task<IEnumerable<CobrancaEntidade>> ListarAsync(string filtro);
        Task NotificarClientePorEmailAsync(int idCobranca);
        Task<CobrancaEntidade> ObterAsync(int idCobranca);
        Task NotificarClientePorSMSAsync(int idCobranca);
        Task<int> InserirAsync(CobrancaEntidade cobranca);
    }
}
