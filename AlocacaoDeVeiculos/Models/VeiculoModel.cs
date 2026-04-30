using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AlocacaoDeVeiculos.Models
{
    [Table("TB_CARRO")]
    public class VeiculoModel
    {
        [Key]
        [Column("CAR_PLACA")]
        [Display(Name = "Placa")]
        public string Placa { get; set; }

        [Column("CAR_MARCA")]
        [Display(Name = "Marca")]
        [Required]
        public string Marca { get; set; }

        [Column("CAR_MODELO")]
        [Display(Name = "Modelo")]
        [Required]
        public string Modelo { get; set; }

        [Column("CAR_ANO")]
        [Display(Name = "Ano")]
        [Required]
        public int Ano { get; set; }

        [Column("CAR_COR")]
        [Display(Name = "Cor")]
        [Required]
        public string Cor { get; set; }

        [Column("CAT_ID")]
        [Display(Name = "Categoria")]
        [ForeignKey("CategoriaObj")] // mesmo nome da propriedade abaixo
        [Required]
        public int CategoriaId { get; set; }
        public virtual CategoriaModel CategoriaObj { get; set; }

        [Column("CAR_IMAGEM_URL")]
        [Display(Name = "Imagem")]
        public string ImagemUrl { get; set; }

        [Column("CAR_DISPONIVEL")]
        [Display(Name = "Disponível")]
        public bool Disponivel { get; set; }

        [Column("CAR_ATIVO")]
        [Display(Name = "Ativo")]
        public bool Ativo { get; set; }
    }
}