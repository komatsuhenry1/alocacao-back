using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AlocacaoDeVeiculos.Models
{
    [Table("TB_CLIENTE")]
    public class ClienteModel
    {

        [Column("CLI_ID")]
        [Display(Name = "Código")]
        public int Id { get; set; }

        [Column("CLI_NOME")]
        [Display(Name = "Nome")]
        [MaxLength(150)]
        [Required]
        public string Nome { get; set; }

        [Column("CLI_CPF")]
        [Display(Name = "CPF")] // colocar validacao de cpf
        [Required]
        public string Cpf { get; set; }

        [Column("CLI_EMAIL")]
        [Display(Name = "Email")] // colocar validacao de email
        [Required]
        public string Email { get; set; }

        [Column("CLI_SENHA")]
        [Display(Name = "Senha")]
        [MaxLength(255)]
        [Required]
        public string Senha { get; set; }

        [Column("CLI_DATA_NASCIMENTO")]
        [Display(Name = "Data de Nascimento")]
        [Required]
        public DateTime DataDeNascimento { get; set; }

        [Column("CLI_TELEFONE")]
        [Display(Name = "Telefone")]
        public string Telefone { get; set; }

        [Column("CLI_ENDERECO")]
        [Display(Name = "Endereço")]
        public string Endereco { get; set; }

        public bool Ativo { get; set; } = true;

        public DateTime CriadoEm { get; set; }
    }
}