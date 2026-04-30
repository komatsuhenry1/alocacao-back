using AlocacaoDeVeiculos.Models;
using AlocacaoDeVeiculos.Services.Veiculo;
using Microsoft.AspNetCore.Mvc;
using AlocacaoDeVeiculos.Dto.Veiculo;

namespace AlocacaoDeVeiculos.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class VeiculoController : ControllerBase
    {
        private readonly IVeiculoInterface _veiculoInterface;
        public VeiculoController(IVeiculoInterface veiculoInterface)
        {
            _veiculoInterface = veiculoInterface;
        }

        [HttpGet("listar_todos")]
        public async Task<ActionResult<ResponseModel<List<VeiculoModel>>>> ListarVeiculos()
        {
            var listaVeiculos = await _veiculoInterface.BuscarTodosVeiculos();
            return Ok(listaVeiculos);
        }

        [HttpGet("buscar/{placa}")]
        public async Task<ActionResult<ResponseModel<VeiculoModel>>> BuscarVeiculo(string placa)
        {
            var veiculo = await _veiculoInterface.BuscarVeiculoPorPlaca(placa);
            return Ok(veiculo);
        }

        [HttpPost("criar")]
        public async Task<ActionResult<ResponseModel<VeiculoModel>>> CriarVeiculo([FromBody] CriarVeiculoDto criarVeiculoDto)
        {
            var resultado = await _veiculoInterface.CriarVeiculo(criarVeiculoDto);
            return Ok(resultado);
        }

        [HttpDelete("deletar/{placa}")]
        public async Task<ActionResult<ResponseModel<VeiculoModel>>> ExcluirVeiculo(string placa)
        {
            var resultado = await _veiculoInterface.ExcluirVeiculo(placa);
            return Ok(resultado);
        }

        [HttpPut("editar/{placa}")]
        public async Task<ActionResult<ResponseModel<VeiculoModel>>> EditarVeiculo(string placa, [FromBody] EditarVeiculoDto editarVeiculoDto)
        {
            var resultado = await _veiculoInterface.EditarVeiculo(placa, editarVeiculoDto);
            return Ok(resultado);
        }
    }
}
