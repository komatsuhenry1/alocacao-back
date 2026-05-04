using AlocacaoDeVeiculos.Data;
using AlocacaoDeVeiculos.Dto.Alocacao;
using AlocacaoDeVeiculos.Dto.Veiculo;
using AlocacaoDeVeiculos.Models;
using Microsoft.EntityFrameworkCore;

namespace AlocacaoDeVeiculos.Services.Alocacao
{
    public class AlocacaoService : IAlocacaoInterface
    {
        private readonly AppDbContext _context;
        public AlocacaoService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseModel<LocacaoModel>> BuscarAlocacaoById(int idAlocacao)
        {
            ResponseModel<LocacaoModel> response = new ResponseModel<LocacaoModel>();
            try
            {
                var alocacao = await _context.Alocacao.FirstOrDefaultAsync(a => a.Id == idAlocacao);
                if (alocacao == null)
                {
                    response.Mensagem = "Locação não encontrada!";
                    response.Status = false;
                    return response;
                }
                response.Dados = alocacao;
                response.Mensagem = "Locação buscada com sucesso!";
                response.Status = true;
                return response;
            }
            catch (Exception ex)
            {
                response.Mensagem = $"Erro ao buscar locação: {ex.Message}";
                response.Status = false;
                return response;
            }
        }

        public async Task<ResponseModel<List<LocacaoModel>>> BuscarTodasAlocacoes()
        {
            ResponseModel<List<LocacaoModel>> response = new ResponseModel<List<LocacaoModel>>();
            try
            {
                var alocacoesList = await _context.Alocacao.ToListAsync();
                if (alocacoesList == null)
                {
                    response.Mensagem = "Nenhuma locação encontrada!";
                    return response;
                }

                response.Dados = alocacoesList;
                response.Mensagem = "Locações buscadas com sucesso!";
                response.Status = true;
                return response;

            }
            catch (Exception ex)
            {
                response.Mensagem = $"Erro ao buscar locações: {ex.Message}";
                response.Status = false;
                return response;
            }
        }

        public async Task<ResponseModel<LocacaoModel>> CriarAlocacao(CriarAlocacaoDto criarAlocacaoDto)
        {
            ResponseModel<LocacaoModel> response = new ResponseModel<LocacaoModel>();
            try
            {
                var cliente = await _context.Cliente.FirstOrDefaultAsync(c => c.Id == criarAlocacaoDto.ClienteId);
                if (cliente == null)
                {
                    response.Mensagem = "Não foi possivel encontrar um cliente para essa locação. Insira um cliente Id Valido!";
                    response.Status = false;
                    return response;
                }

                if (!cliente.Ativo)
                {
                    response.Mensagem = "Apenas clientes ativos podem fazer novas locações.";
                    response.Status = false;
                    return response;
                }

                var veiculo = await _context.Veiculo.FirstOrDefaultAsync(c => c.Placa == criarAlocacaoDto.CarroPlaca);
                if (!veiculo.Ativo || !veiculo.Disponivel)
                {
                    response.Mensagem = "O veículo não está disponível para locação.";
                    response.Status = false;
                    return response;
                }
                else if (veiculo == null)
                {
                    response.Mensagem = "Não foi possivel encontrar um veículo para essa locação. Insira uma placa válida.";
                    response.Status = false;
                    return response;
                }

                var carro = await _context.Veiculo.FirstOrDefaultAsync(c => c.Placa == criarAlocacaoDto.CarroPlaca);
                if (carro == null)
                {
                    response.Mensagem = "Não foi possivel encontrar um veículo para essa locação. Insira uma placa válida.";
                    response.Status = false;
                    return response;
                }

                var categoria = await _context.Categoria.FirstOrDefaultAsync(c => c.Id == carro.CategoriaId);

                if (criarAlocacaoDto.DataRetirada >= criarAlocacaoDto.DataPrevDevolucao)
                {
                    response.Mensagem = "A data de retirada não pode ser maior ou igual a data de devolução.";
                    response.Status = false;
                    return response;
                }


                //DateTime dataRetirada = DateTime.Parse(criarAlocacaoDto.DataRetirada);
                //DateTime dataPrevDevolucao = DateTime.Parse(criarAlocacaoDto.DataRetirada);
                var dataprevia = criarAlocacaoDto.DataPrevDevolucao;
                var dataretirada = criarAlocacaoDto.DataRetirada;

                TimeSpan diferenca = criarAlocacaoDto.DataPrevDevolucao - criarAlocacaoDto.DataRetirada;

                var diasDeAlocacao = diferenca.Days;
                var valorDiariaCategoria = categoria.ValorDiaria;

                var valorTotalLocacao = diasDeAlocacao * valorDiariaCategoria;

                //desativa veiculo apos lcoacao
                veiculo.Ativo = false;
                veiculo.Disponivel = false;

                var alocacao = new LocacaoModel
                {
                    ClienteId = criarAlocacaoDto.ClienteId,
                    CarroPlaca = criarAlocacaoDto.CarroPlaca,
                    DataRetirada = criarAlocacaoDto.DataRetirada,
                    DataPrevDevolucao = criarAlocacaoDto.DataPrevDevolucao,
                    ValorTotal = valorTotalLocacao,
                    Status = Models.Enums.EnumStatusLocacao.Ativo,
                    CriadoEm = DateTime.Now
                };

                _context.Alocacao.Add(alocacao);
                await _context.SaveChangesAsync();

                response.Dados = alocacao;
                response.Mensagem = $"Locação criada com sucesso! Valor total: {valorTotalLocacao:C} por {diasDeAlocacao} dias";
                response.Status = true;

                return response;
            }
            catch (Exception ex)
            {
                response.Mensagem = $"Erro ao criar locação: {ex.Message}";
                response.Status = false;
                return response;
            }
        }

        public async Task<ResponseModel<LocacaoModel>> EditarAlocacao(int idAlocacao, EditarAlocacaoDto editarAlocacaoDto)
        {
            ResponseModel<LocacaoModel> response = new ResponseModel<LocacaoModel>();
            try
            {
                var alocacao = await _context.Alocacao.FirstOrDefaultAsync(a => a.Id == idAlocacao);

                if (alocacao == null)
                {
                    response.Mensagem = "Locação não encontrada!";
                    response.Status = false;
                    return response;
                }
                var cliente = await _context.Cliente.FirstOrDefaultAsync(c => c.Id == editarAlocacaoDto.ClienteId);
                if (cliente == null)
                {
                    response.Mensagem = "Não foi possivel encontrar um cliente para essa locação. Insira um cliente Id Valido!";
                    response.Status = false;
                    return response;
                }

                if (!cliente.Ativo)
                {
                    response.Mensagem = "Apenas clientes ativos podem fazer novas locações.";
                    response.Status = false;
                    return response;
                }

                var carro = await _context.Veiculo.FirstOrDefaultAsync(c => c.Placa == editarAlocacaoDto.CarroPlaca);
                if (carro == null)
                {
                    response.Mensagem = "Não foi possivel encontrar um veículo para essa locação. Insira uma placa válida.";
                    response.Status = false;
                    return response;
                }

                if (alocacao.CarroPlaca != editarAlocacaoDto.CarroPlaca)
                {
                    if (!carro.Ativo || !carro.Disponivel)
                    {
                        response.Mensagem = "O veículo não está disponível para locação.";
                        response.Status = false;
                        return response;
                    }

                    var oldCarro = await _context.Veiculo.FirstOrDefaultAsync(c => c.Placa == alocacao.CarroPlaca);
                    if (oldCarro != null)
                    {
                        oldCarro.Ativo = true;
                        oldCarro.Disponivel = true;
                    }

                    carro.Ativo = false;
                    carro.Disponivel = false;
                }

                var categoria = await _context.Categoria.FirstOrDefaultAsync(c => c.Id == carro.CategoriaId);

                if (editarAlocacaoDto.DataRetirada >= editarAlocacaoDto.DataPrevDevolucao)
                {
                    response.Mensagem = "A data de retirada não pode ser maior ou igual a data de devolução.";
                    response.Status = false;
                    return response;
                }

                TimeSpan diferenca = editarAlocacaoDto.DataPrevDevolucao - editarAlocacaoDto.DataRetirada;
                var diasDeAlocacao = diferenca.Days;
                var valorDiariaCategoria = categoria.ValorDiaria;
                var valorTotalLocacao = diasDeAlocacao * valorDiariaCategoria;

                alocacao.ClienteId = editarAlocacaoDto.ClienteId;
                alocacao.CarroPlaca = editarAlocacaoDto.CarroPlaca;
                alocacao.DataRetirada = editarAlocacaoDto.DataRetirada;
                alocacao.DataPrevDevolucao = editarAlocacaoDto.DataPrevDevolucao;
                alocacao.ValorTotal = valorTotalLocacao;
                alocacao.Status = editarAlocacaoDto.Status;

                _context.Update(alocacao);
                await _context.SaveChangesAsync();

                response.Dados = alocacao;
                response.Mensagem = "Locação editada com sucesso!";
                response.Status = true;

                return response;
            }
            catch (Exception ex)
            {
                response.Mensagem = $"Erro ao editar locação: {ex.Message}";
                response.Status = false;
                return response;
            }
        }

        public async Task<ResponseModel<LocacaoModel>> ExcluirAlocacao(int idAlocacao)
        {
            ResponseModel<LocacaoModel> response = new ResponseModel<LocacaoModel>();
            try
            {
                var alocacao = await _context.Alocacao.FirstOrDefaultAsync(a => a.Id == idAlocacao);
                if (alocacao == null)
                {
                    response.Mensagem = "Locação não encontrada!";
                    response.Status = false;
                    return response;
                }

                var carro = await _context.Veiculo.FirstOrDefaultAsync(c => c.Placa == alocacao.CarroPlaca);
                if (carro == null)
                {
                    response.Mensagem = "Não foi possivel encontrar um veículo para essa locação. Insira uma placa válida.";
                    response.Status = false;
                    return response;
                }

                if (!carro.Ativo || !carro.Disponivel)
                {
                    response.Mensagem = "A locação só pode ser cancelada se o carro ainda não foi retirado!";
                    response.Status = false;
                    return response;
                }

                _context.Alocacao.Remove(alocacao);
                await _context.SaveChangesAsync();
                response.Status = true;
                response.Mensagem = "Locação excluida com sucesso!";
                return response;
            }
            catch (Exception ex)
            {
                response.Mensagem = $"Erro ao excluir locação: {ex.Message}";
                response.Status = false;
                return response;
            }
        }

        public async Task<ResponseModel<LocacaoModel>> DarBaixaLocacao(int idAlocacao)
        {
            ResponseModel<LocacaoModel> response = new ResponseModel<LocacaoModel>();
            try
            {
                var alocacao = await _context.Alocacao.FirstOrDefaultAsync(a => a.Id == idAlocacao);
                if (alocacao == null)
                {
                    response.Mensagem = "Locação não encontrada!";
                    response.Status = false;
                    return response;
                }

                var carro = await _context.Veiculo.FirstOrDefaultAsync(c => c.Placa == alocacao.CarroPlaca);
                if (carro == null)
                {
                    response.Mensagem = "Não foi possivel encontrar um veículo para essa locação. Insira uma placa válida.";
                    response.Status = false;
                    return response;
                }
                var categoria = await _context.Categoria.FirstOrDefaultAsync(c => c.Id == carro.CategoriaId);
                if (categoria == null)
                {
                    response.Mensagem = "Não foi possivel encontrar uma categoria para essa locação. Insira uma placa válida.";
                    response.Status = false;
                    return response;
                }

                //Se a devolução ocorrer antes do prazo previsto, cobra-se o valor dos dias efetivamente utilizados
                if (DateTime.Now < alocacao.DataPrevDevolucao)
                {
                    var diasUtilizados = (DateTime.Now - alocacao.DataRetirada).Days;
                    var valorACobrar = diasUtilizados * (categoria.ValorDiaria);
                    alocacao.ValorTotal = valorACobrar;
                    response.Mensagem = $"Baixa na locação realizada com sucesso! Valor total cobrado por {diasUtilizados} dias foi: R${valorACobrar}";
                }

                alocacao.Status = Models.Enums.EnumStatusLocacao.Concluido;

                carro.Disponivel = true;
                carro.Ativo = true;

                _context.Update(alocacao);
                await _context.SaveChangesAsync();

                response.Dados = alocacao;
                response.Status = true;

                return response;
            }
            catch (Exception ex)
            {
                response.Mensagem = $"Erro ao dar baixa na locação: {ex.Message}";
                response.Status = false;
                return response;
            }
        }
    }
}
