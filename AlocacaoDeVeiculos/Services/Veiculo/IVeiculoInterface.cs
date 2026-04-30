using AlocacaoDeVeiculos.Dto.Veiculo;
using AlocacaoDeVeiculos.Models;

namespace AlocacaoDeVeiculos.Services.Veiculo
{
    public interface IVeiculoInterface
    {
        Task<ResponseModel<VeiculoModel>> CriarVeiculo(CriarVeiculoDto criarVeiculoDto);
        Task<ResponseModel<VeiculoModel>> BuscarVeiculoPorPlaca(string placa);
        Task<ResponseModel<List<VeiculoModel>>> BuscarTodosVeiculos();
        Task<ResponseModel<VeiculoModel>> EditarVeiculo(string placa, EditarVeiculoDto editarVeiculoDto);
        Task<ResponseModel<VeiculoModel>> ExcluirVeiculo(string placa);
    }
}
