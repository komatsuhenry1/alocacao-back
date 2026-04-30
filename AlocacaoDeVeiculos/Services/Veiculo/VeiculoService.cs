using AlocacaoDeVeiculos.Data;
using AlocacaoDeVeiculos.Models;
using Microsoft.EntityFrameworkCore;
using AlocacaoDeVeiculos.Dto.Veiculo;

namespace AlocacaoDeVeiculos.Services.Veiculo
{
    public class VeiculoService : IVeiculoInterface
    {
        private readonly AppDbContext _context;
        public VeiculoService(AppDbContext context)
        {
            _context = context; 
        }

        public async Task<ResponseModel<VeiculoModel>> BuscarVeiculoPorPlaca(string placa)
        {
            ResponseModel<VeiculoModel> response = new ResponseModel<VeiculoModel>();
            try 
            { 
                var veiculo = await _context.Veiculo.FirstOrDefaultAsync(v => v.Placa == placa);
                if (veiculo == null)
                {
                    response.Mensagem = "Veículo não encontrado!";
                    return response;
                }
                response.Dados = veiculo;
                response.Mensagem = "Veículo buscado com sucesso!";
                response.Status = true;
                return response;
            }
            catch (Exception ex)
            {
                response.Mensagem = $"Erro ao buscar veículo: {ex.Message}";
                response.Status = false;
                return response;
            }
        }

        public async Task<ResponseModel<List<VeiculoModel>>> BuscarTodosVeiculos()
        {
            ResponseModel<List<VeiculoModel>> response = new ResponseModel<List<VeiculoModel>>();
            try
            {
                var veiculosList = await _context.Veiculo.ToListAsync();
                if (veiculosList == null)
                {
                    response.Mensagem = "Nenhum veículo encontrado!";
                    return response;
                }

                response.Dados = veiculosList;
                response.Mensagem = "Veículos buscados com sucesso!";
                response.Status = true;
                return response;

            }
            catch (Exception ex)
            {
                response.Mensagem = $"Erro ao buscar veículos: {ex.Message}";
                response.Status = false;
                return response;
            }
        }

        public async Task<ResponseModel<VeiculoModel>> CriarVeiculo(CriarVeiculoDto criarVeiculoDto) 
        {
            ResponseModel<VeiculoModel> response = new ResponseModel<VeiculoModel>();
            try
            {
                var categoriaId = await _context.Categoria.FirstOrDefaultAsync(c => c.Id == criarVeiculoDto.CategoriaId);
                if (categoriaId == null)
                {
                    response.Mensagem = "Não foi possivel encontrar uma categoria para esse veículo.";
                    return response;
                }

                var veiculo = new VeiculoModel
                {
                    Placa = criarVeiculoDto.Placa,
                    Marca = criarVeiculoDto.Marca,
                    Modelo = criarVeiculoDto.Modelo,
                    Ano = criarVeiculoDto.Ano,
                    Cor = criarVeiculoDto.Cor,
                    CategoriaId = criarVeiculoDto.CategoriaId,
                    ImagemUrl = criarVeiculoDto.ImagemUrl,
                    Disponivel = true,
                    Ativo = true
                };

                _context.Veiculo.Add(veiculo);
                await _context.SaveChangesAsync();

                response.Dados = veiculo;
                response.Mensagem = "Veículo criado com sucesso!";
                response.Status = true;

                return response;

            }
            catch (Exception ex)
            {
                response.Mensagem = $"Erro ao criar veículo: {ex.Message}";
                response.Status = false;
                return response;
            }
        }

        public async Task<ResponseModel<VeiculoModel>> EditarVeiculo(string placa, EditarVeiculoDto editarVeiculoDto)
        {
            ResponseModel<VeiculoModel> response = new ResponseModel<VeiculoModel>();
            try
            {
                var veiculo = await _context.Veiculo.FirstOrDefaultAsync(v => v.Placa == placa);
                if (veiculo == null)
                {
                    response.Mensagem = "Veículo não encontrado!";
                    return response;
                }
                
                veiculo.Marca = editarVeiculoDto.Marca;
                veiculo.Modelo = editarVeiculoDto.Modelo;
                veiculo.Ano = editarVeiculoDto.Ano;
                veiculo.Cor = editarVeiculoDto.Cor;
                veiculo.CategoriaId = editarVeiculoDto.CategoriaId;
                veiculo.ImagemUrl = editarVeiculoDto.ImagemUrl;
                veiculo.Disponivel = editarVeiculoDto.Disponivel;
                veiculo.Ativo = editarVeiculoDto.Ativo;

                _context.Update(veiculo);
                await _context.SaveChangesAsync();

                response.Dados = veiculo;
                response.Mensagem = "Veículo editado com sucesso!";
                response.Status = true;

                return response;
            }
            catch (Exception ex)
            {
                response.Mensagem = $"Erro ao editar veículo: {ex.Message}";
                response.Status = false;
                return response;
            }
        }

        public async Task<ResponseModel<VeiculoModel>> ExcluirVeiculo(string placa)
        {
            ResponseModel<VeiculoModel> response = new ResponseModel<VeiculoModel>();
            try
            {
                var veiculo = await _context.Veiculo.FirstOrDefaultAsync(v => v.Placa == placa);
                if (veiculo == null)
                {
                    response.Mensagem = "Veículo não encontrado!";
                    return response;
                }

                _context.Veiculo.Remove(veiculo);
                await _context.SaveChangesAsync();
                response.Status = true;
                response.Mensagem = "Veículo excluido com sucesso!";
                return response;
            }
            catch (Exception ex)
            {
                response.Mensagem = $"Erro ao excluir veículo: {ex.Message}";
                response.Status = false;
                return response;
            }
        }
    }
}
