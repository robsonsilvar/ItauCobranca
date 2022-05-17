using Itau.Cobranca.Dominio;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Itau.Cobranca.Dominio.Repository
{
    public interface ICobrancaRepository
    {
        Task<IEnumerable<CobrancaEntidade>> ListarAsync();
        Task<CobrancaEntidade> ObterAsync(int idCobranca);
        Task<int> InserirAsync(CobrancaEntidade cobranca);
        Task<bool> AtualizarEnviadoAsync(int idCobranca);
    }
}
