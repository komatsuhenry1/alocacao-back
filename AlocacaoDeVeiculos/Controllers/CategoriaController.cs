using AlocacaoDeVeiculos.Models;
using AlocacaoDeVeiculos.Services.Categoria;
using Microsoft.AspNetCore.Mvc;
using AlocacaoDeVeiculos.Dto.Categoria;

namespace AlocacaoDeVeiculos.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        public readonly ICategoriaInterface _categoriaInterface;
        public CategoriaController(ICategoriaInterface categoriaInterface)
        {
            _categoriaInterface = categoriaInterface;
        }

        [HttpGet("listar_todos")]
        public async Task<ActionResult<ResponseModel<List<CategoriaModel>>>> ListarCategorias()
        {
            var listaCategorias = await _categoriaInterface.BuscarTodasCategorias();
            return Ok(listaCategorias);
        }

        [HttpGet("buscar/{id}")]
        public async Task<ActionResult<ResponseModel<CategoriaModel>>> BuscarCategoria(int id)
        {
            var categoria = await _categoriaInterface.BuscarCategoriaById(id);
            return Ok(categoria);
        }

        [HttpPost("criar")]
        public async Task<ActionResult<ResponseModel<CategoriaModel>>> CriarCategoria([FromBody] CriarCategoriaDto criarCategoriaDto)
        {
            var resultado = await _categoriaInterface.CriarCategoria(criarCategoriaDto);
            return Ok(resultado);
        }

        [HttpDelete("deletar/{id}")]
        public async Task<ActionResult<ResponseModel<CategoriaModel>>> ExcluirCategoria(int id)
        {
            var resultado = await _categoriaInterface.ExcluirCategoria(id);
            return Ok(resultado);
        }

        [HttpPut("editar/{id}")]
        public async Task<ActionResult<ResponseModel<CategoriaModel>>> EditarCategoria(int id, [FromBody] EditarCategoriaDto editarCategoriaDto)
        {
            var resultado = await _categoriaInterface.EditarCategoria(id, editarCategoriaDto);
            return Ok(resultado);
        }
    }
}
