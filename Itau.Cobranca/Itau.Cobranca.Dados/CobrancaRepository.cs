using Dapper;
using Itau.Cobranca.Dominio;
using Itau.Cobranca.Dominio.Repository;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Itau.Cobranca.Dados
{
    public class CobrancaRepository : ICobrancaRepository
    {
        private SqlConnection _conexao;
        private const string _conectionStringName = "DefaultConnection";
        private readonly IConfiguration _configuration;
        public CobrancaRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        public async Task<IEnumerable<CobrancaEntidade>> ListarAsync()
        {
            IEnumerable<CobrancaEntidade> cobrancas = new List<CobrancaEntidade>();
            using (_conexao = new SqlConnection(_configuration.GetConnectionString(_conectionStringName)))
            {
                _conexao.Open();
                cobrancas = await _conexao.QueryAsync<CobrancaEntidade>("SP_COBRANCA_LISTAR");
            }

            return cobrancas;
        }

        public async Task<CobrancaEntidade> ObterAsync(int idCobranca)
        {
            IEnumerable<CobrancaEntidade> cobrancas = null;
            using (_conexao = new SqlConnection(_configuration.GetConnectionString(_conectionStringName)))
            {
                _conexao.Open();
                var values = new { idCobranca = idCobranca };
                cobrancas = await _conexao.QueryAsync<CobrancaEntidade>("SP_COBRANCA_OBTER @idCobranca", values);
            }
            return cobrancas.FirstOrDefault();
        }

        public async Task<int> InserirAsync(CobrancaEntidade cobranca)        
        {
            int id = 0;
            using (_conexao = new SqlConnection(_configuration.GetConnectionString(_conectionStringName)))
            {
                _conexao.Open();
                var values = new { valorCobranca = cobranca.ValorCobranca, 
                    clienteNome = cobranca.ClienteNome,
                    clienteEndereco = cobranca.ClienteEndereco,
                    clienteCelular = cobranca.ClienteCelular,
                    clienteEmail = cobranca.ClienteEmail
                };
                
                id= await _conexao.ExecuteScalarAsync<int>(
                    "SP_COBRANCA_INSERIR @valorCobranca, @clienteNome, @clienteEndereco, @clienteCelular, @clienteEmail", 
                    values);
            }

            return id;
        }

        public async Task<bool> AtualizarEnviadoAsync(int idCobranca)
        {
            using (_conexao = new SqlConnection(_configuration.GetConnectionString(_conectionStringName)))
            {
                _conexao.Open();
                var values = new
                {
                    idCobranca = idCobranca
                };

                int result = await _conexao.ExecuteScalarAsync<int>(
                    "SP_COBRANCA_ATUALIZAR_ENVIADO @idCobranca",
                    values);

                return result > 0;
            }

        }
    }
}
