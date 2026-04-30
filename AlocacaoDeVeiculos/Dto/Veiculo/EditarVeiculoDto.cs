namespace AlocacaoDeVeiculos.Dto.Veiculo
{
    public class EditarVeiculoDto
    {
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public int Ano { get; set; }
        public string Cor { get; set; }
        public int CategoriaId { get; set; }
        public string ImagemUrl { get; set; }
        public bool Disponivel { get; set; }
        public bool Ativo { get; set; }
    }
}
