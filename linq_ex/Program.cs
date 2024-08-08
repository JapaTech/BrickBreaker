using linq_ex.Entidades;
using System.Globalization;

namespace linq_ex
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Insira o caminho do arquivo: ");
            string path = Console.ReadLine();
            List<Funcionario> funcionarios = new List<Funcionario>();

            try
            {
                using (StreamReader sr = File.OpenText(path))
                {
                    while (!sr.EndOfStream)
                    {
                        string[] dados = sr.ReadLine().Split(',');
                        funcionarios.Add(new Funcionario(dados[0], dados[1], double.Parse(dados[2], CultureInfo.InvariantCulture)));
                    }
                }
            }
            catch (IOException e)
            {

                Console.WriteLine(e.Message);
            }

            Console.Write("\nA partir de qual valor filtrar os funcionários: ");
            double valor = double.Parse(Console.ReadLine());
            var r1 = funcionarios.Where(f => f.Salario > valor).OrderBy(f => f.Email).Select(f => f.Email);
            
            foreach (string emails in r1)
            {
                Console.WriteLine(emails);
            }

            var r2 = funcionarios.Where(f => f.Nome[0] == 'M').Select(f => f.Salario).
                DefaultIfEmpty(0.0).
                Sum();
            Console.WriteLine("\nSoma dos salarios de todos que começam com M: " + r2.ToString("F2", CultureInfo.InvariantCulture));
        }
    }
}
