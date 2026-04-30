using AlocacaoDeVeiculos.Dto.Cliente;
using AlocacaoDeVeiculos.Models;

namespace AlocacaoDeVeiculos.Services.Cliente
{
    public interface IClienteInterface
    {
        Task<ResponseModel<ClienteModel>> CriarCliente(CriarClienteDto criarClienteDto);

        //buscar por id
        Task<ResponseModel<ClienteModel>> BuscarClienteById(int idCliente);

        //buscar todos 
        Task<ResponseModel<List<ClienteModel>>> BuscarTodosClientes();

        //editar 
        Task<ResponseModel<ClienteModel>> EditarCliente(int idCliente, EditarClienteDto editarClienteDto);

        //excluir
        Task<ResponseModel<ClienteModel>> ExcluirCliente(int idCliente);
    }
}
