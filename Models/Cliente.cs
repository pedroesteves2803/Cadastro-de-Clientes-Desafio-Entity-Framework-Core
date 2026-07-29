namespace CadastroDeClientesDesafioEntityFrameworkCore.Models;

public class Cliente{
    public int Id { get; set; }

    public string Nome { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string? Telefone { get; set; }

    public DateTime DataDeCadastro { get; set; } = DateTime.Now;

    public IList<Endereco> Enderecos { get; set; } = [];
}