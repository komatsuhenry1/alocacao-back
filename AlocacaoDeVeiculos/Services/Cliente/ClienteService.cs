using AlocacaoDeVeiculos.Data;
using AlocacaoDeVeiculos.Models;
using AlocacaoDeVeiculos.Utils;
using Azure;
using Microsoft.EntityFrameworkCore;
using AlocacaoDeVeiculos.Dto.Cliente;


namespace AlocacaoDeVeiculos.Services.Cliente
{
    public class ClienteService : IClienteInterface
    {

        private readonly AppDbContext _context;
        public ClienteService(AppDbContext context)
        {
            _context = context; 
        }

        public async Task<ResponseModel<ClienteModel>> BuscarClienteById(int idCliente)
        {
            ResponseModel<ClienteModel> response = new ResponseModel<ClienteModel>();
            try 
            { 
                //var cliente = await _context.Cliente.FindAsync(idCliente); // busca baseado na PK
                var cliente = await _context.Cliente.FirstOrDefaultAsync(c => c.Id == idCliente);
                if (cliente == null)
                {
                    response.Mensagem = "Cliente não encontrado!";
                }
                response.Dados = cliente;
                response.Mensagem = "Cliente buscado com sucesso!";
                response.Status = true;
                return response;
            }
            catch (Exception ex)
            {
                response.Mensagem = $"Erro ao buscar cliente: {ex.Message}";
                response.Status = false;
                return response;
            }
        }

        public async Task<ResponseModel<List<ClienteModel>>> BuscarTodosClientes()
        {
            ResponseModel<List<ClienteModel>> response = new ResponseModel<List<ClienteModel>>();
            try
            {
                var clientesList = await _context.Cliente.ToListAsync();
                if (clientesList == null)
                {
                    response.Mensagem = "Cliente não encontrado!";
                    return response;
                }

                response.Dados = clientesList;
                response.Mensagem = "Clientes buscados com sucesso!";
                response.Status = true;
                return response;

            }
            catch (Exception ex)
            {
                response.Mensagem = $"Erro ao buscar clientes: {ex.Message}";
                response.Status = false;
                return response;
            }
        }

        public async Task<ResponseModel<ClienteModel>> CriarCliente(CriarClienteDto criarClienteDto) 
        {
            ResponseModel<ClienteModel> response = new ResponseModel<ClienteModel>();
            try
            {
                if (!DateUtils.MaiorOuIgualA18(criarClienteDto.DataDeNascimento))
                {
                    response.Mensagem = "Cliente precisa ter 18 anos ou mais.";
                    response.Status = false;
                    return response;
                }

                var cpfClienteExistente = await _context.Cliente.FirstOrDefaultAsync(c => c.Cpf == criarClienteDto.Cpf);
                if (cpfClienteExistente != null)
                {
                    response.Mensagem = "CPF já cadastrado!";
                    response.Status = false;
                    return response;
                }
                var emailClienteExistente = await _context.Cliente.FirstOrDefaultAsync(c => c.Email == criarClienteDto.Email);
                if (emailClienteExistente != null)
                {
                    response.Mensagem = "Email já cadastrado!";
                    response.Status = false;
                    return response;
                }

                var cliente = new ClienteModel
                {
                    Nome = criarClienteDto.Nome,
                    Cpf = criarClienteDto.Cpf,
                    Email = criarClienteDto.Email,
                    Senha = criarClienteDto.Senha,
                    DataDeNascimento = criarClienteDto.DataDeNascimento,
                    Telefone = criarClienteDto.Telefone,
                    Endereco = criarClienteDto.Endereco,
                    Ativo = true
                };



                //seta datetime
                cliente.CriadoEm = DateTime.Now;

                //criptogrfar senha
                var senhaCliente = cliente.Senha;
                cliente.Senha = Crypto.GerarHash(senhaCliente);

                _context.Cliente.Add(cliente);
                await _context.SaveChangesAsync(); //await pq chama o banco aqui


                response.Dados = cliente;
                response.Mensagem = "Cliente criado com sucesso!";
                response.Status = true;

                return response;

            }
            catch (Exception ex)
            {
                response.Mensagem = $"Erro ao buscar clientes: {ex.Message}";
                response.Status = false;
                return response;
            }
            
        }

        public async Task<ResponseModel<ClienteModel>> EditarCliente(int idCliente, EditarClienteDto editarClienteDto)
        {
            ResponseModel<ClienteModel> response = new ResponseModel<ClienteModel>();
            try
            {
                var cliente = await _context.Cliente.FirstOrDefaultAsync(cliente => cliente.Id == idCliente);

                if (cliente == null)
                {
                    response.Mensagem = "Cliente não encontrado!";
                    return response;
                }
                // adiciona as alterações
                cliente.Id = idCliente;
                cliente.Nome = editarClienteDto.Nome;
                cliente.Cpf = editarClienteDto.Cpf;
                cliente.Email = editarClienteDto.Email;
                cliente.Senha = Crypto.GerarHash(editarClienteDto.Senha);
                cliente.DataDeNascimento = editarClienteDto.DataDeNascimento;
                cliente.Telefone = editarClienteDto.Telefone;
                cliente.Endereco = editarClienteDto.Endereco;
                cliente.Ativo = editarClienteDto.Ativo;

                _context.Update(cliente);
                await _context.SaveChangesAsync();

                response.Dados = cliente;
                response.Mensagem = "Cliente editado com sucesso!";
                response.Status = true;

                return response;


            }
            catch (Exception ex)
            {
                response.Mensagem = $"Erro ao editar cliente: {ex.Message}";
                response.Status = false;
                return response;
            }

        }

        public async Task<ResponseModel<ClienteModel>> ExcluirCliente(int idCliente)
        {
            ResponseModel<ClienteModel> response = new ResponseModel<ClienteModel>();
            try
            {
                var cliente = await _context.Cliente.FirstOrDefaultAsync(c => c.Id == idCliente);
                if (cliente == null)
                {
                    response.Mensagem = "Cliente não encontrado!";
                    return response;
                } 
                
                var alocacoesAtivas = await _context.Alocacao.FirstOrDefaultAsync(a => a.ClienteId == idCliente);
                if (alocacoesAtivas != null)
                {
                    response.Mensagem = "Não é possível excluir o cliente, pois existem alocações ativas associadas a ele.";
                    return response;
                }

                _context.Cliente.Remove(cliente);
                await _context.SaveChangesAsync();
                response.Status = true;
                response.Mensagem = "Cliente excluido com sucesso!";
                return response;
            }
            catch (Exception ex)
            {
                response.Mensagem = $"Erro ao excluir cliente {ex.Message}";
                response.Status = false;
                return response;
            }
        }
    }
}
