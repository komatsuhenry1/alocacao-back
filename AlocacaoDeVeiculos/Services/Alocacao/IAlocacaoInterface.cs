using AlocacaoDeVeiculos.Dto.Alocacao;
using AlocacaoDeVeiculos.Models;

namespace AlocacaoDeVeiculos.Services.Alocacao
{
    public interface IAlocacaoInterface
    {
        Task<ResponseModel<LocacaoModel>> CriarAlocacao(CriarAlocacaoDto criarAlocacaoDto);
        Task<ResponseModel<LocacaoModel>> BuscarAlocacaoById(int idAlocacao);
        Task<ResponseModel<List<LocacaoModel>>> BuscarTodasAlocacoes();
        Task<ResponseModel<LocacaoModel>> EditarAlocacao(int idAlocacao, EditarAlocacaoDto editarAlocacaoDto);
        Task<ResponseModel<LocacaoModel>> ExcluirAlocacao(int idAlocacao);

        Task<ResponseModel<LocacaoModel>> DarBaixaLocacao(int idAlocacao);
     }
}
