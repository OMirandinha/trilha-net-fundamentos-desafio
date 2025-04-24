using System.Text.RegularExpressions;

namespace DesafioFundamentos.Models
{
    public class Estacionamento
    {
        private decimal precoInicial = 0;
        private decimal precoPorHora = 0;
        private List<Veiculo> veiculos = new List<Veiculo>();

        public Estacionamento(decimal precoInicial, decimal precoPorHora)
        {
            this.precoInicial = precoInicial;
            this.precoPorHora = precoPorHora;
        }

        public void AdicionarVeiculo()
        {
            // Puxa o input do usuário
            Console.WriteLine("Digite a placa do veículo para estacionar:");
            string placa = Console.ReadLine()?.Replace(" ", "").ToUpper();

            // Variáveis para definir os padrões de placa usados no Brasil
            Regex padraoAntigo = new Regex(@"^[A-Z]{3}[0-9]{4}$");
            Regex padraoMercosul = new Regex(@"^[A-Z]{3}[0-9][A-Z][0-9]{2}$");

            // Se a string "placa" estiver em branco retorna um erro
            if (string.IsNullOrWhiteSpace(placa))
            {
                Console.WriteLine("Placa inválida. Não pode estar em branco.");
            }
            // Verifica se o padrão está de acordo
            else if (!padraoAntigo.IsMatch(placa) && !padraoMercosul.IsMatch(placa))
            {
                Console.WriteLine("Placa inválida. Informe uma placa no formato válido (ex: ABC1234 ou ABC1D23).");
            }
            // Se estiver tudo certo, adiciona o veículo à lista
            else
            {
                veiculos.Add(new Veiculo(placa));
                Console.WriteLine($"Veículo com placa {placa} adicionado com sucesso.");
            }
        }

        public void RemoverVeiculo()
        {
            Console.WriteLine("Digite a placa do veículo para remover:");

            // Pede para o usuário digitar a placa e armazenar na variável placa
            string placa = Console.ReadLine()?.Replace(" ", "").ToUpper();

            // Procura o veículo na lista usando a placa
            var veiculo = veiculos.FirstOrDefault(x => x.Placa.ToUpper() == placa);

            // Verifica se o veículo existe
            if (veiculo != null)
            {
                Console.WriteLine("Digite a quantidade de horas que o veículo permaneceu estacionado:");
                int horas;
                bool horasValidas = int.TryParse(Console.ReadLine(), out horas) && horas >= 0;

                Console.WriteLine("Digite a quantidade de minutos adicionais (0 a 59):");
                int minutos;
                bool minutosValidos = int.TryParse(Console.ReadLine(), out minutos) && minutos >= 0 && minutos < 60;

                if (!horasValidas || !minutosValidos)
                {
                    Console.WriteLine("Horas ou minutos inválidos. Certifique-se de que os valores não são negativos e minutos estão entre 0 e 59.");
                    return;
                }

                if (horas == 0 && minutos == 0)
                {
                    Console.WriteLine("O tempo total não pode ser zero. Insira um tempo válido.");
                    return;
                }

                // Calcula o valor total
                decimal tarifaPorMinuto = precoPorHora / 60;
                decimal valorTotal = precoInicial + (precoPorHora * horas) + (tarifaPorMinuto * minutos);

                // Remove o veículo da lista
                veiculos.Remove(veiculo);

                Console.WriteLine($"O veículo {placa} foi removido e o preço total foi de: R$ {valorTotal:F2}");
            }
            else
            {
                Console.WriteLine("Desculpe, esse veículo não está estacionado aqui. Confira se digitou a placa corretamente.");
            }
        }

        public void ListarVeiculos()
        {
            // Verifica se há veículos no estacionamento
            if (veiculos.Any())
            {
                Console.WriteLine("Veículos estacionados (ordem de chegada):");

                // Realiza um laço de repetição, exibindo os veículos estacionados
                foreach (var veiculo in veiculos)
                {
                    // Mostra a placa e a hora de entrada, mantendo a ordem de cadastro
                    Console.WriteLine($"- {veiculo.Placa} (entrou às {veiculo.HoraEntrada:HH:mm})");
                }
            }
            else
            {
                Console.WriteLine("Não há veículos estacionados.");
            }
        }
    }
}
