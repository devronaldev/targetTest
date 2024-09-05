using System.Globalization;
using System.Xml.Linq;
namespace System
{
    class Program
    {
        static void Main(string[] args){
            //EXERCÍCIO 1 - RESPOSTA: 91.
            Console.WriteLine("=====/SEGUNDO EXERCÍCIO/=====");
            Console.Write("Insira um valor e veja se ele pertence a sequência de Fibonacci: ");
            //Sem tratamento de exceções para usuário escrevendo palavras.
            int val = int.Parse(Console.ReadLine());
            FibVal(val);
            
            Console.WriteLine("=====/TERCEIRO EXERCÍCIO/=====");
            string pathxml = "C:/dev/target/obj/faturamento.xml";
            Faturamento(pathxml);

            Console.WriteLine("=====/QUARTO EXERCÍCIO/=====");
            pathxml = "C:/dev/target/obj/distribuidora.xml";
            PercentualFaturamento(pathxml);

            Console.WriteLine("=====/QUINTO EXERCÍCIO/=====");
            Console.WriteLine("Escreva um texto para ser invertido: ");
            InverterString(Console.ReadLine());
        }

        static void PercentualFaturamento(string pathxml){
            XDocument arquivo = XDocument.Load(pathxml);

            List<string> estados = new List<string>();
            List<decimal> faturamentos = new List<decimal>();

            foreach (var local in arquivo.Descendants("local"))
            {
                string estado = local.Element("estado").Value;
                decimal faturamento = decimal.Parse(local.Element("faturamento").Value, CultureInfo.InvariantCulture);
                //Console.WriteLine($"{estado}: {faturamento} TESTE");
                estados.Add(estado);
                faturamentos.Add(faturamento);
            }

            decimal faturamentoTotal = faturamentos.Sum();
            
            Console.WriteLine($"O faturamento total é: R${faturamentoTotal}");
            Console.WriteLine($"====ESTADOS====");
            for (int i = 0; i < estados.Count; i++)
            {
                decimal percentual = (faturamentos[i] / faturamentoTotal * 100);
                Console.WriteLine($"{estados[i]}: {percentual:F2}%");
            }
        }

        static void Faturamento(string pathxml){
            XDocument arquivo = XDocument.Load(pathxml);

            List<decimal> faturamentos = arquivo.Descendants("dia").Select(d => decimal.Parse(d.Element("faturamento").Value, CultureInfo.InvariantCulture)).Where(f => f > 0).ToList();
                   
            Console.WriteLine($"Menor faturamento: {faturamentos.Min()}");
            Console.WriteLine($"Maior faturamento: {faturamentos.Max()}");
            decimal mediaMensal = decimal.Round(faturamentos.Average(), 2);
            Console.WriteLine($"Média mensal: {mediaMensal}");
            Console.WriteLine($"Dias acima da média: {faturamentos.Count(f => f > mediaMensal)}") ;

        }

        static void FibVal(int val){
            int v1 = 0, v2 = 1, v3 = 0;

            while (v3 < val)
            {
                v3 = v1 + v2;
                v1 = v2;
                v2 = v3;
            }

            if (val == v3)
            {
                Console.WriteLine($"O valor {val} pertence a sequência de Fibonacci");
            } else 
            {
                Console.WriteLine($"O valor {val} não pertence a sequência de Fibonacci");
            }
        }

        static void InverterString(string texto){
            string inverted = "";
            texto = texto.ToLower();
            for (int i = texto.Length - 1; i >= 0; i--)
            {
                inverted += texto[i];
            }

            Console.WriteLine($"Texto invertido: {inverted}");
        }
    }
}