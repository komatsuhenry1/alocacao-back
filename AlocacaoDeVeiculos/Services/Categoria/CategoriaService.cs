using AlocacaoDeVeiculos.Data;
using AlocacaoDeVeiculos.Dto.Categoria;
using AlocacaoDeVeiculos.Models;
using AlocacaoDeVeiculos.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace AlocacaoDeVeiculos.Services.Categoria
{
    public class CategoriaService : ICategoriaInterface
    {
        private readonly AppDbContext _context;
        public CategoriaService(AppDbContext context)
        {
            _context = context; 
        }

        public async Task<ResponseModel<CategoriaModel>> BuscarCategoriaById(int idCategoria)
        {
            ResponseModel<CategoriaModel> response = new ResponseModel<CategoriaModel>();
            try 
            { 
                var categoria = await _context.Categoria.FirstOrDefaultAsync(c => c.Id == idCategoria);
                if (categoria == null)
                {
                    response.Mensagem = "Categoria não encontrada!";
                }
                response.Dados = categoria;
                response.Mensagem = "Categoria buscada com sucesso!";
                response.Status = true;
                return response;
            }
            catch (Exception ex)
            {
                response.Mensagem = $"Erro ao buscar categoria: {ex.Message}";
                response.Status = false;
                return response;
            }
        }

        public async Task<ResponseModel<List<CategoriaModel>>> BuscarTodasCategorias()
        {
            ResponseModel<List<CategoriaModel>> response = new ResponseModel<List<CategoriaModel>>();
            try
            {
                var categoriasList = await _context.Categoria.ToListAsync();
                if (categoriasList == null)
                {
                    response.Mensagem = "Categoria não encontrada!";
                    return response;
                }

                response.Dados = categoriasList;
                response.Mensagem = "Categorias buscadas com sucesso!";
                response.Status = true;
                return response;

            }
            catch (Exception ex)
            {
                response.Mensagem = $"Erro ao buscar categorias: {ex.Message}";
                response.Status = false;
                return response;
            }
        }

        public async Task<ResponseModel<CategoriaModel>> CriarCategoria(CriarCategoriaDto criarCategoriaDto) 
        {
            ResponseModel<CategoriaModel> response = new ResponseModel<CategoriaModel>();
            try
            {
                
                if (criarCategoriaDto.ValorDiaria <= 0)
                {
                    response.Mensagem = "O valor da diaria deve ser mais que 0.";
                    return response;
                }
                var categoria = new CategoriaModel
                {
                    Nome = criarCategoriaDto.Nome,
                    Descricao = criarCategoriaDto.Descricao,
                    ValorDiaria = criarCategoriaDto.ValorDiaria,
                    Ativo = true
                };

                _context.Categoria.Add(categoria);
                await _context.SaveChangesAsync();

                response.Dados = categoria;
                response.Mensagem = "Categoria criada com sucesso!";
                response.Status = true;

                return response;

            }
            catch (Exception ex)
            {
                response.Mensagem = $"Erro ao criar categoria: {ex.Message}";
                response.Status = false;
                return response;
            }
            
        }

        public async Task<ResponseModel<CategoriaModel>> EditarCategoria(int idCategoria, EditarCategoriaDto editarCategoriaDto)
        {
            ResponseModel<CategoriaModel> response = new ResponseModel<CategoriaModel>();
            try
            {
                var categoria = await _context.Categoria.FirstOrDefaultAsync(c => c.Id == idCategoria);
                if (categoria == null)
                {
                    response.Mensagem = "Categoria não encontrada!";
                    return response;
                }

                var veiculo = await _context.Veiculo.FirstOrDefaultAsync(v => v.CategoriaId == idCategoria);
                if (veiculo.Ativo)
                {
                    response.Mensagem = "Não é possível editar a categoria de um veículo que está ativo!";
                    return response;
                }
                var locacao = await _context.Alocacao.FirstOrDefaultAsync(l => l.CarroPlaca == veiculo.Placa);
                if (locacao.Status == EnumStatusLocacao.Ativo)
                {
                    response.Mensagem = "Não é possível editar a categoria de um veículo que está alugado!";
                    return response;
                }

                categoria.Nome = editarCategoriaDto.Nome;
                categoria.Descricao = editarCategoriaDto.Descricao;
                categoria.ValorDiaria = editarCategoriaDto.ValorDiaria;
                categoria.Ativo = editarCategoriaDto.Ativo;

                _context.Update(categoria);
                await _context.SaveChangesAsync();

                response.Dados = categoria;
                response.Mensagem = "Categoria editada com sucesso!";
                response.Status = true;

                return response;


            }
            catch (Exception ex)
            {
                response.Mensagem = $"Erro ao editar categoria: {ex.Message}";
                response.Status = false;
                return response;
            }

        }

        public async Task<ResponseModel<CategoriaModel>> ExcluirCategoria(int idCategoria)
        {
            ResponseModel<CategoriaModel> response = new ResponseModel<CategoriaModel>();
            try
            {
                var categoria = await _context.Categoria.FirstOrDefaultAsync(c => c.Id == idCategoria);
                if (categoria == null)
                {
                    response.Mensagem = "Categoria não encontrada!";
                    return response;
                }

                _context.Categoria.Remove(categoria);
                await _context.SaveChangesAsync();
                response.Status = true;
                response.Mensagem = "Categoria excluida com sucesso!";
                return response;
            }
            catch (Exception ex)
            {
                response.Mensagem = $"Erro ao excluir categoria {ex.Message}";
                response.Status = false;
                return response;
            }
        }
    }
}
