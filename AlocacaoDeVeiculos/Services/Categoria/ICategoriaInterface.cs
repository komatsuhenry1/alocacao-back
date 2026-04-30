using AlocacaoDeVeiculos.Dto.Categoria;
using AlocacaoDeVeiculos.Models;

namespace AlocacaoDeVeiculos.Services.Categoria
{
    public interface ICategoriaInterface
    {
        Task<ResponseModel<CategoriaModel>> CriarCategoria(CriarCategoriaDto criarCategoriaDto);
        Task<ResponseModel<CategoriaModel>> BuscarCategoriaById(int idCategoria);
        Task<ResponseModel<List<CategoriaModel>>> BuscarTodasCategorias();
        Task<ResponseModel<CategoriaModel>> EditarCategoria(int idCategoria, EditarCategoriaDto editarCategoriaDto);
        Task<ResponseModel<CategoriaModel>> ExcluirCategoria(int idCategoria);
    }
}
