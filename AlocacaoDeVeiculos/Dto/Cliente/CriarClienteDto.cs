namespace AlocacaoDeVeiculos.Dto.Cliente
{
    public class CriarClienteDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }

        public string Cpf { get; set; }

        public string Email { get; set; }

        public string Senha { get; set; }
        public DateTime DataDeNascimento { get; set; }
        public string Telefone { get; set; }
        public string Endereco { get; set; }

    }
}
