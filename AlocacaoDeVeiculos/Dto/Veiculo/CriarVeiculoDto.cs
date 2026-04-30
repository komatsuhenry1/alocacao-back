namespace AlocacaoDeVeiculos.Dto.Veiculo
{
    public class CriarVeiculoDto
    {
        public string Placa { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public int Ano { get; set; }
        public string Cor { get; set; }
        public int CategoriaId { get; set; }
        public string ImagemUrl { get; set; }
    }
}
