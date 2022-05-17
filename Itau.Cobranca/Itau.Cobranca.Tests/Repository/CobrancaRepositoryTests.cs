using Microsoft.VisualStudio.TestTools.UnitTesting;
using Itau.Cobranca.Dados;
using System;
using System.Collections.Generic;
using System.Text;
using Itau.Cobranca.Dominio.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using FluentAssertions;
using System.Threading.Tasks;

namespace Itau.Cobranca.Dados.Tests
{
    [TestClass()]
    public class CobrancaRepositoryTests
    {
        private IConfiguration _configuration;

        public void Init()
        {
            _configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build(); 
        }

        [TestMethod()]
        public async Task Deve_ListarAsyncTest()
        {
            Init();
            ICobrancaRepository cobrancaRepo = new CobrancaRepository(_configuration);
            var retorno = await cobrancaRepo.ListarAsync();
            retorno.Should().NotBeNull();
        }

        [TestMethod()]
        public async Task Deve_AtaulizarEnviadoAsyncTest()
        {
            Init();
            ICobrancaRepository cobrancaRepo = new CobrancaRepository(_configuration);
            bool  retorno = await cobrancaRepo.AtualizarEnviadoAsync(17);
            retorno.Should().BeTrue();

        }
    }
}