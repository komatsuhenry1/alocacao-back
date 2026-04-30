namespace AlocacaoDeVeiculos.Dto.Categoria
{
    public class EditarCategoriaDto
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public decimal ValorDiaria { get; set; }
        public bool Ativo { get; set; }
    }
}
