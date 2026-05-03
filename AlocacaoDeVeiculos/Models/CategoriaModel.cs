using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AlocacaoDeVeiculos.Models
{
    [Table("TB_CATEGORIA")]
    public class CategoriaModel
    {
        [Column("CAT_ID")]
        [Display(Name = "Código")]
        public int Id { get; set; }

        [Column("CAT_NOME")]
        [Display(Name = "Nome")]
        [MaxLength(255)]
        public string Nome { get; set; }

        [Column("CAT_DESCRICAO")]
        [Display(Name = "Descrição")]
        [MaxLength(150)]
        [Required]
        public string Descricao { get; set; }

        [Column("CAT_VALOR_DIARIA")]
        [Display(Name = "Valor Diária")]
        [Required]
        public decimal ValorDiaria { get; set; }

        [Column("CAT_ATIVO")]
        [Display(Name = "Ativo")]
        public bool Ativo { get; set; } = true;
    }
}