using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Itau.Cobranca.Dominio;
using Itau.Cobranca.Dominio.Service;
using Itau.Notificacao.Dominio;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Itau.Cobranca.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CobrancaController : ControllerBase
    {
        private readonly ICobrancaService _cobranca;

        public CobrancaController(ICobrancaService cobranca)
        {
            _cobranca = cobranca;
        }

        [HttpGet("cobrancas/{filtro}")]
        public async Task<IActionResult> Listar([FromRoute] string filtro)
        {
            try
            {
                return StatusCode(200, await _cobranca.ListarAsync(filtro));
            }
            catch (Exception e)
            {
                return StatusCode(500, "Erro. tente novamente mais tarde.");
            }
        }

        [HttpGet("notificar-cliente-por-email/id/{idCobranca}")]
        public async Task<IActionResult> NotificarClientePorEmail([FromRoute] int idCobranca)
        {
            try
            {
                await _cobranca.NotificarClientePorEmailAsync(idCobranca);
                return StatusCode(200);
            }           
            catch (CobrancaException e)
            {
                return StatusCode(400, e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Erro. tente novamente mais tarde.");
            }

        }

        [HttpGet("notificar-cliente-por-sms/id/{idCobranca}")]
        public async Task<IActionResult> NotificarClientePorSMS([FromRoute] int idCobranca)
        {
            try
            {
                await _cobranca.NotificarClientePorSMSAsync(idCobranca);
                return StatusCode(200);
            }
            catch (CobrancaException e)
            {
                return StatusCode(400, e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Erro. tente novamente mais tarde.");
            }
        }

        [HttpGet("{idCobranca}")]
        public async Task<IActionResult> Obter([FromRoute] int idCobranca)
        {
            try
            {
                var cobranca = await _cobranca.ObterAsync(idCobranca);
                return StatusCode(200, cobranca);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Erro. tente novamente mais tarde.");
            }
        }


        [HttpPost]
        public async Task<IActionResult> Inserir([FromBody] CobrancaEntidade cobranca)
        {
            try
            {
                int id = await _cobranca.InserirAsync(cobranca);
                return StatusCode(200, id);
            }
            catch (CobrancaException ex)
            {
                return StatusCode(400, ex.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Erro. tente novamente mais tarde.");
            }

        }
    }
}
