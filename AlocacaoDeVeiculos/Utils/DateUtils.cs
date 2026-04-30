namespace AlocacaoDeVeiculos.Utils
{
    public class DateUtils
    {
        public static int CalcularIdade(DateTime dataNascimento)
        {
            var hoje = DateTime.Today;
            var idade = hoje.Year - dataNascimento.Year;
            if (dataNascimento.Date > hoje.AddYears(-idade)) idade--;
            return idade;
        }
        public static bool MaiorOuIgualA18(DateTime dataNascimento)
            => CalcularIdade(dataNascimento) >= 18;
    }
}
