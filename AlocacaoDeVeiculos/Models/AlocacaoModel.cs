using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AlocacaoDeVeiculos.Models.Enums;

namespace AlocacaoDeVeiculos.Models
{
    [Table("TB_LOCACAO")]
    public class LocacaoModel
    {
        [Key]
        [Column("LOC_ID")]
        [Display(Name = "Código")]
        public int Id { get; set; }

        [Column("CLI_ID")]
        [Display(Name = "Cliente")]
        [ForeignKey("ClienteObj")] // mesmo nome da propriedade abaixo
        [Required]
        public int ClienteId { get; set; }
        public virtual ClienteModel ClienteObj { get; set; }

        [Column("CAR_PLACA")]
        [Display(Name = "Placa")]
        [ForeignKey("VeiculoObj")] // mesmo nome da propriedade abaixo
        [Required]
        public string CarroPlaca { get; set; }
        public virtual VeiculoModel VeiculoObj { get; set; }

        [Column("LOC_DATA_RETIRADA")]
        [Display(Name = "Data de Retirada")]
        [Required]
        public DateTime DataRetirada { get; set; }

        [Column("LOC_DATA_PREV_DEVOLUCAO")]
        [Display(Name = "Data Prevista de Devolução")]
        [Required]
        public DateTime DataPrevDevolucao { get; set; }

        [Column("LOC_VALOR_TOTAL")]
        [Display(Name = "Valor Total")]
        public decimal ValorTotal { get; set; }

        [Column("LOC_STATUS")]
        [Display(Name = "Status")]
        public EnumStatusLocacao Status { get; set; } = EnumStatusLocacao.Ativo;

        [Column("LOC_CRIADO_EM")]
        [Display(Name = "Criado Em")]
        public DateTime CriadoEm { get; set; }
    }
}