using AlocacaoDeVeiculos.Models;
using AlocacaoDeVeiculos.Services.Alocacao;
using Microsoft.AspNetCore.Mvc;
using AlocacaoDeVeiculos.Dto.Alocacao;

namespace AlocacaoDeVeiculos.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AlocacaoController : ControllerBase
    {
        private readonly IAlocacaoInterface _alocacaoInterface;
        public AlocacaoController(IAlocacaoInterface alocacaoInterface)
        {
            _alocacaoInterface = alocacaoInterface;
        }

        [HttpGet("listar_todos")]
        public async Task<ActionResult<ResponseModel<List<LocacaoModel>>>> ListarAlocacoes()
        {
            var listaAlocacoes = await _alocacaoInterface.BuscarTodasAlocacoes();
            return Ok(listaAlocacoes);
        }

        [HttpGet("buscar/{id}")]
        public async Task<ActionResult<ResponseModel<LocacaoModel>>> BuscarAlocacao(int id)
        {
            var alocacao = await _alocacaoInterface.BuscarAlocacaoById(id);
            return Ok(alocacao);
        }

        [HttpPost("criar")]
        public async Task<ActionResult<ResponseModel<LocacaoModel>>> CriarAlocacao([FromBody] CriarAlocacaoDto criarAlocacaoDto)
        {
            var resultado = await _alocacaoInterface.CriarAlocacao(criarAlocacaoDto);
            return Ok(resultado);
        }

        [HttpDelete("deletar/{id}")]
        public async Task<ActionResult<ResponseModel<LocacaoModel>>> ExcluirAlocacao(int id)
        {
            var resultado = await _alocacaoInterface.ExcluirAlocacao(id);
            return Ok(resultado);
        }

        [HttpPut("editar/{id}")]
        public async Task<ActionResult<ResponseModel<LocacaoModel>>> EditarAlocacao(int id, [FromBody] EditarAlocacaoDto editarAlocacaoDto)
        {
            var resultado = await _alocacaoInterface.EditarAlocacao(id, editarAlocacaoDto);
            return Ok(resultado);
        }

        [HttpPut("baixa/{id}")]
        public async Task<ActionResult<ResponseModel<LocacaoModel>>> DarBaixaLocacao(int id)
        {
            var resultado = await _alocacaoInterface.DarBaixaLocacao(id);
            return Ok(resultado);
        }
    }
}
