using AlocacaoDeVeiculos.Models.Enums;

namespace AlocacaoDeVeiculos.Dto.Alocacao
{
    public class EditarAlocacaoDto
    {
        public int ClienteId { get; set; }
        public string CarroPlaca { get; set; }
        public DateTime DataRetirada { get; set; }
        public DateTime DataPrevDevolucao { get; set; }
        // public decimal ValorTotal { get; set; }
        public EnumStatusLocacao Status { get; set; }
    }
}
