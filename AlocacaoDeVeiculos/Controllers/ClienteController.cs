using AlocacaoDeVeiculos.Models;
using AlocacaoDeVeiculos.Services.Cliente;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AlocacaoDeVeiculos.Dto.Cliente;

namespace AlocacaoDeVeiculos.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        public readonly IClienteInterface _clienteInterface;
        public ClienteController(IClienteInterface clienteInterface)
        {
            _clienteInterface = clienteInterface;
        }

        [HttpGet("listar_todos")]
        public async Task<ActionResult<ResponseModel<List<ClienteModel>>>> ListarClientes()
        {
            var listaClientes = await _clienteInterface.BuscarTodosClientes();
            return Ok(listaClientes);
        }

        [HttpGet("buscar/{id}")]
        public async Task<ActionResult<ResponseModel<ClienteModel>>> BuscarCliente(int id)
        {
            var cliente = await _clienteInterface.BuscarClienteById(id);
            return Ok(cliente);
        }

        [HttpPost("criar")]
        public async Task<ActionResult<ResponseModel<ClienteModel>>> CriarCliente([FromBody] CriarClienteDto criarClienteDto)
        {
            var resultado = await _clienteInterface.CriarCliente(criarClienteDto);
            return Ok(resultado);
        }

        [HttpDelete("deletar/{id}")]
        public async Task<ActionResult<ResponseModel<ClienteModel>>> ExcluirCliente(int id)
        {
            var resultado = await _clienteInterface.ExcluirCliente(id);
            return Ok(resultado);
        }

        [HttpPut("editar/{id}")]
        public async Task<ActionResult<ResponseModel<ClienteModel>>> EditarCliente(int id, [FromBody] EditarClienteDto editarClienteDto)
        {
            var resultado = await _clienteInterface.EditarCliente(id, editarClienteDto);
            return Ok(resultado);
        }
    }
}
