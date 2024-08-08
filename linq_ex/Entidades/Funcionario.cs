using System.Globalization;

namespace linq_ex.Entidades
{
    internal class Funcionario
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public double Salario { get; set; }

        public Funcionario(string nome, string email, double salario)
        {
            Nome = nome;
            this.Email = email;
            this.Salario = salario;
        }

        public override string ToString()
        {
            return $"Nome: {Nome}, Emali: {Email}, Salario: {Salario.ToString("F2", CultureInfo.InvariantCulture)}"; 
        }
    }
}
