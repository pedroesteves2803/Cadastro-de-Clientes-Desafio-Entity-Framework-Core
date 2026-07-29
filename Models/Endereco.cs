namespace CadastroDeClientesDesafioEntityFrameworkCore.Models;

public class Endereco
{
    public int Id { get; set; }
    public string Cep { get; set; } = string.Empty;
    public string Rua { get; set; } =  string.Empty;
    public string Numero { get; set; } = string.Empty;
    public string Bairro { get; set; } = string.Empty;
    public string Cidade { get; set; } = string.Empty;
    public string Estado { get; set; } = string.Empty;

    public int ClienteId { get; set; }
    public required Cliente Cliente { get; set; }
}