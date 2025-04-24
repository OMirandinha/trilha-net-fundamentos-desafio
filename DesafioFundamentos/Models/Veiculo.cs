namespace DesafioFundamentos.Models
{
    //classe referente a o objeto de veículo no programa principal
    public class Veiculo
    {
        public string Placa { get; set; }
        public DateTime HoraEntrada { get; set; }

        public Veiculo(string placa)
        {
            Placa = placa;
            HoraEntrada = DateTime.Now;//Armazena o horário de entrada do veículo
        }
    }
}