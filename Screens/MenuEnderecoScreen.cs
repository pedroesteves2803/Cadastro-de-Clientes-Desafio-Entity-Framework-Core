using CadastroDeClientesDesafioEntityFrameworkCore.Data;
using CadastroDeClientesDesafioEntityFrameworkCore.Models;
using Microsoft.EntityFrameworkCore;

namespace CadastroDeClientesDesafioEntityFrameworkCore.Screens;

public static class MenuEnderecoScreen
{
    public static async Task ShowAsync()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Cadastro de endereco");
            
            Console.WriteLine("1 - Cadastrar endereco");
            Console.WriteLine("2 - Listar enderecos");
            Console.WriteLine("3 - Listar enderecos do cliente");
            Console.WriteLine("4 - Atualiza endereco");
            Console.WriteLine("5 - Excluir endereco");
            Console.WriteLine("6 - Listar enderecos por cidade");
            Console.WriteLine("7 - Listar enderecos ordenados");
            Console.WriteLine("8 - Listar cep e rua de enderecos");
            Console.WriteLine("9 - Listar quantidade de enderecos dos clientes");
            Console.WriteLine("10 - Listar clientes com enderecos");
            Console.WriteLine("11 - Listar clientes sem  enderecos");
            Console.WriteLine("12 - Comparar include X select");
            Console.WriteLine("0 - Voltar");
            Console.WriteLine();

            Console.Write("Escolha uma opção: ");
            var opcao = Console.ReadLine();
            
            switch (opcao)
            {
                case "1":
                    await CadastrarEnderecoAsync();
                    break;
                
                case "2":
                    await ListarEnderecosAsync();
                    break;
                
                case "3":
                    await ListarEnderecosDoClienteAsync();
                    break;
                
                case "4":
                    await AtualizarEnderecoAsync();
                    break;
                
                case "5":
                    await ExcluirEnderecoAsync();
                    break;
                
                case "6":
                    await ListarEnderecoPelaCidadeAsync();
                    break;
                
                case "7":
                    await ListarEnderecosOrdenadosAsync();
                    break;
                
                case "8":   
                    await ListarResumoDosEnderecosAsync();
                    break;
                    
                case "9":
                    await ListarQuantidadeDeEnderecosPorClienteAsync();
                    break;
                
                case "10":
                    await ListarClientesComEnderecosAsync();
                    break;

                case "11":
                    await ListarClientesSemEnderecosAsync();
                    break;

                case "12":
                    await CompararIncludeESelectAsync();
                    break;

                case "0":
                return;
                
                default:
                Console.Clear();
                Console.WriteLine("Opção inválida.");
                break;
            }
            
            AguardarContinuacao();
        }
    }
    private static async Task CadastrarEnderecoAsync()
    {
        Console.Clear();
        Console.WriteLine("Cadastro de endereço");

        try
        {
            Console.Write("Id do cliente: ");
            if (!int.TryParse(Console.ReadLine(), out int idCliente))
            {
                Console.WriteLine("ID inválido.");
                return;
            }
            await using var context = new CadastroClienteDataContext();
            var cliente = await context.Clientes.FindAsync(idCliente);

            if (cliente == null)
            {
                Console.WriteLine("Cliente não encontrado.");
                return;
            }
            
            Console.Write("Cep: ");
            var cep = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(cep))
            {
                Console.WriteLine();
                Console.WriteLine("Cep não pode ser vazio.");
                return;
            }
            
            cep = cep.Replace("-", "").Trim();

            if (cep.Length != 8 || !cep.All(char.IsDigit))
            {
                Console.WriteLine("CEP inválido. Digite exatamente 8 números.");
                return;
            }
            
            Console.Write("Rua: ");
            var rua = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(rua))
            {
                Console.WriteLine();
                Console.WriteLine("Rua não pode ser vazio.");
                return;
            }
            
            Console.Write("Numero: ");
            var numero = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(numero))
            {
                Console.WriteLine();
                Console.WriteLine("Numero não pode ser vazio.");
                return;
            }
            
            Console.Write("Bairro: ");
            var bairro = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(bairro))
            {
                Console.WriteLine();
                Console.WriteLine("Bairro nao pode ser vazio.");
                return;
            }
            
            Console.Write("Cidade: ");
            var cidade = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(cidade))
            {
                Console.WriteLine();
                Console.WriteLine("Cidade nao pode ser vazio.");
                return;
            }
            
            Console.Write("Estado: ");
            var estado = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(estado))
            {
                Console.WriteLine();
                Console.WriteLine("Estado nao pode ser vazio.");
                return;
            }
            
            await context.Enderecos.AddAsync(new Endereco
            {
                Cep = cep,
                Bairro = bairro,
                Cidade = cidade,
                Estado = estado,
                Numero = numero,
                Rua = rua,
                Cliente = cliente
            });
            await context.SaveChangesAsync();

            Console.WriteLine();
            Console.WriteLine("Cadastro realizado com sucesso!");
        }
        catch (DbUpdateException exception)
        {
            Console.WriteLine();
            Console.WriteLine("Não foi possível cadastrar o endereco");
            Console.WriteLine(exception.InnerException?.Message);
        }
        catch (Exception exception)
        {
            Console.WriteLine();
            Console.WriteLine("Ocorreu um erro inesperado.");
            Console.WriteLine(exception.Message);
        }
        
    }

    private static async Task ListarEnderecosAsync()
    {
        Console.Clear();
        Console.WriteLine("Lista de endereços");
        
        try
        {
            await using var context = new CadastroClienteDataContext();
            
            var enderecos =  await context.Enderecos
                .AsNoTracking()
                .ToListAsync();
            
            if (!enderecos.Any())
            {
                Console.WriteLine("Nenhum endereço cadastrado.");
                return;
            }

            foreach (var endereco in enderecos)
                Console.WriteLine($"ID: {endereco.Id} - Cep: {endereco.Cep} - Rua: {endereco.Rua}");

        }
        catch (DbUpdateException exception)
        {
            Console.WriteLine();
            Console.WriteLine("Não foi possível listar os enderecos");
            Console.WriteLine(exception.InnerException?.Message);
        }
        catch (Exception exception)
        {
            Console.WriteLine();
            Console.WriteLine("Ocorreu um erro inesperado.");
            Console.WriteLine(exception.Message);
        }
    }
    
    private static async Task ListarEnderecosDoClienteAsync()
    {
        Console.Clear();
        Console.WriteLine("Lista de endereços do cliente");
        
        try
        {
            Console.Write("Id do Cliente: ");
            if (!int.TryParse(Console.ReadLine(), out int idCliente))
            {
                Console.WriteLine("Id invalido.");
                return;
            }
            
            await using var context = new CadastroClienteDataContext();
            var cliente = await context.Clientes
                .AsNoTracking()
                .Include(c => c.Enderecos)
                .FirstOrDefaultAsync(c => c.Id == idCliente);

            if (cliente is null)
            {
                Console.WriteLine("Cliente não encontrado.");
                return;
            }
            
            if (!cliente.Enderecos.Any())
            {
                Console.WriteLine("Este cliente não possui endereços.");
                return;
            }
            
            foreach (var endereco in cliente.Enderecos)
                Console.WriteLine($"ID: {endereco.Id} - Cep: {endereco.Cep} - Rua: {endereco.Rua}");
        }
        catch (DbUpdateException exception)
        {
            Console.WriteLine();
            Console.WriteLine("Não foi possível listar os enderecos do  cliente");
            Console.WriteLine(exception.InnerException?.Message);
        }
        catch (Exception exception)
        {
            Console.WriteLine();
            Console.WriteLine("Ocorreu um erro inesperado.");
            Console.WriteLine(exception.Message);
        }
    }

    private static async Task AtualizarEnderecoAsync()
    {
        Console.Clear();
        Console.WriteLine("Atualização de endereco do cliente");
        Console.WriteLine();
        
        try
        {
             Console.Write("Id do endereco: ");
            if (!int.TryParse(Console.ReadLine(), out int idEndereco))
            {
                Console.WriteLine("ID inválido.");
                return;
            }
            await using var context = new CadastroClienteDataContext();
            var endereco = await context.Enderecos
                .FindAsync(idEndereco);
            
            if (endereco is null)
            {
                Console.WriteLine("Endereco não encontrado.");
                return;
            }
            
            Console.Write($"Cep ({endereco.Cep}): ");
            var cep = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(cep))
            {
                cep = endereco.Cep;
            }
            
            cep = cep.Replace("-", "").Trim();

            if (cep.Length != 8 || !cep.All(char.IsDigit))
            {
                Console.WriteLine("CEP inválido. Digite exatamente 8 números.");
                return;
            }
            
            Console.Write($"Rua ({endereco.Rua}): ");
            var rua = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(rua))
            {
                rua = endereco.Rua;
            }
            
            Console.Write($"Numero ({endereco.Numero}): ");
            var numero = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(numero))
            {
                numero = endereco.Numero;
            }
            
            Console.Write($"Bairro ({endereco.Bairro}): ");
            var bairro = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(bairro))
            {
                bairro =  endereco.Bairro;
            }
            
            Console.Write($"Cidade ({endereco.Cidade}): ");
            var cidade = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(cidade))
            {
                cidade =  endereco.Cidade;
            }
            
            Console.Write($"Estado ({endereco.Estado}): ");
            var estado = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(estado))
            {
               estado = endereco.Estado;
            }
            
            endereco.Cep = cep;
            endereco.Rua = rua;
            endereco.Numero = numero;
            endereco.Bairro = bairro;
            endereco.Cidade = cidade;
            endereco.Estado = estado;
            
            await context.SaveChangesAsync();

            Console.WriteLine();
            Console.WriteLine("Endereco atualizado com sucesso!");
        }
        catch (DbUpdateException exception)
        {
            Console.WriteLine();
            Console.WriteLine("Não foi possível atualizar o endereco do cliente");
            Console.WriteLine(exception.InnerException?.Message);
        }
        catch (Exception exception)
        {
            Console.WriteLine();
            Console.WriteLine("Ocorreu um erro inesperado.");
            Console.WriteLine(exception.Message);
        }
    }

    private static async Task ExcluirEnderecoAsync()
    {
        Console.Clear();
        Console.WriteLine("Deleta endereco do cliente");
        Console.WriteLine();

        try
        {
            Console.Write("Id do endereco: ");
            if (!int.TryParse(Console.ReadLine(), out int idEndereco))
            {
                Console.WriteLine("Id inválido.");
                return;
            }
            
            await using var context = new CadastroClienteDataContext();
            
            var endereco =  await context.Enderecos.FindAsync(idEndereco);

            if (endereco is null)
            {
                Console.Write("Endereco não encontrado.");
                return;
            }

            Console.Write($"Deseja excluir ({endereco.Cep}) S/N?: ");

            if (Console.ReadKey().Key == ConsoleKey.S)
            {
                context.Enderecos.Remove(endereco);
                await context.SaveChangesAsync();
                Console.WriteLine();
                Console.WriteLine("Endereco removido com sucesso!");
            }            
        }
        catch (DbUpdateException exception)
        {
            Console.WriteLine();
            Console.WriteLine("Não foi possível excluir o endereço do cliente.");
            Console.WriteLine(exception.InnerException?.Message);
        }
        catch (Exception exception)
        {
            Console.WriteLine();
            Console.WriteLine("Ocorreu um erro inesperado.");
            Console.WriteLine(exception.Message);
        }
    }

    private static async Task ListarEnderecoPelaCidadeAsync()
    {
        Console.Clear();
        Console.WriteLine("Listar endereco do cliente pela cidade");
        Console.WriteLine();

        try
        {
            Console.Write("Cidade: ");
            var cidade = Console.ReadLine();
            
            if (string.IsNullOrWhiteSpace(cidade))
            {
                Console.WriteLine("Cidade não pode ser vazia.");
                return;
            }

            using var context = new CadastroClienteDataContext();
            
            cidade = cidade.Trim();

            var enderecos = await context.Enderecos
                .Where(x => x.Cidade == cidade)
                .AsNoTracking()
                .OrderBy(x => x.Rua)
                .ToListAsync();

            if (!enderecos.Any())
            {
                Console.WriteLine("Endereço não encontrado.");
                return;
            }
            
            foreach (var endereco in enderecos)
                Console.WriteLine($"ID: {endereco.Id} - Cep: {endereco.Cep} - Cidade: {endereco.Cidade} - Rua: {endereco.Rua}");
        }
        catch (DbUpdateException exception)
        {
            Console.WriteLine();
            Console.WriteLine("Não foi possível excluir o endereço do cliente.");
            Console.WriteLine(exception.InnerException?.Message);
        }
        catch (Exception exception)
        {
            Console.WriteLine();
            Console.WriteLine("Ocorreu um erro inesperado.");
            Console.WriteLine(exception.Message);
        }
    }

    private static async Task ListarEnderecosOrdenadosAsync()
    {
        Console.Clear();
        Console.WriteLine("Listar enderecos ordenados");
        Console.WriteLine();

        try
        {
            using var context = new  CadastroClienteDataContext();

            var enderecos = await context.Enderecos
                .AsNoTracking()
                .OrderBy(x => x.Estado)
                .ThenBy(x => x.Cidade)
                .ThenBy(x => x.Rua)
                .ToListAsync();

            if (!enderecos.Any())
            {
                Console.WriteLine("Nenhum endereço cadastrado.");
                return;
            }

            foreach (var endereco in enderecos)
                Console.WriteLine($"ID: {endereco.Id} - Estado: {endereco.Estado} - Cidade: {endereco.Cidade} - Rua: {endereco.Rua}");

        }
        catch (DbUpdateException exception)
        {
            Console.WriteLine();
            Console.WriteLine("Não foi possível listar os endereços ordenados.");
            Console.WriteLine(exception.InnerException?.Message);
        }
        catch (Exception exception)
        {
            Console.WriteLine();
            Console.WriteLine("Ocorreu um erro inesperado.");
            Console.WriteLine(exception.Message);
        }
    }

    private static async Task ListarResumoDosEnderecosAsync()
    {
        Console.Clear();
        Console.WriteLine("Listar cep e rua do endereco pela");
        Console.WriteLine();

        try
        {
            using var context = new CadastroClienteDataContext();

            var enderecos = await context.Enderecos
                .Select(x => new
                {
                    x.Cep,
                    x.Rua
                })
                .AsNoTracking()
                .ToListAsync();
            
            if (!enderecos.Any())
            {
                Console.WriteLine("Nenhum endereço encontrado.");
                return;
            }
            
            foreach (var endereco in enderecos)
                Console.WriteLine($"Cep: {endereco.Cep} - Rua: {endereco.Rua}");
        }
        catch (DbUpdateException exception)
        {
            Console.WriteLine();
            Console.WriteLine("Não foi possível listar o cep e a rua dos endereços.");
            Console.WriteLine(exception.InnerException?.Message);
        }
        catch (Exception exception)
        {
            Console.WriteLine();
            Console.WriteLine("Ocorreu um erro inesperado.");
            Console.WriteLine(exception.Message);
        }
    }

    private static async Task ListarQuantidadeDeEnderecosPorClienteAsync()
    {
        Console.Clear();
        Console.WriteLine("Quantidade de endereços por cliente");
        Console.WriteLine();

        try
        {
            using var context =  new CadastroClienteDataContext();

            var clientes = await context.Clientes
                .Select(x => new
                {
                    x.Nome,
                    QuantidadeDeEndereco = x.Enderecos.Count
                })
                .OrderByDescending(x => x.QuantidadeDeEndereco)
                .ThenBy(x => x.Nome)
                .ToListAsync();
            
            if (!clientes.Any())
            {
                Console.WriteLine("Nenhum cliente com endereço encontrado.");
                return;
            }

            foreach (var cliente in clientes)
                Console.WriteLine($"Nome: {cliente.Nome} - {cliente.QuantidadeDeEndereco}");
        }
        catch (DbUpdateException exception)
        {
            Console.WriteLine();
            Console.WriteLine("Não foi possível listar a quantidade de endereços por cliente.");
            Console.WriteLine(exception.InnerException?.Message);
        }
        catch (Exception exception)
        {
            Console.WriteLine();
            Console.WriteLine("Ocorreu um erro inesperado.");
            Console.WriteLine(exception.Message);
        }
        
    }

    private static async Task ListarClientesComEnderecosAsync()
    {
        Console.Clear();
        Console.WriteLine("Clientes com endereco");
        Console.WriteLine();

        try
        {
            using var context =  new CadastroClienteDataContext();

            var clientes = await context.Clientes
                .Where(x => x.Enderecos.Any())
                .Select(x => new
                {
                    x.Nome,
                    Enderecos = x.Enderecos
                        .Select(e => new
                        {
                            e.Estado,
                            e.Cidade,
                            e.Rua
                        })
                        .ToList()
                })
                .AsNoTracking()
                .OrderBy(x => x.Nome)
                .ToListAsync();

            if (!clientes.Any())
            {
                Console.WriteLine("Nenhum cliente sem endereço encontrado.");
                return;
            }

            foreach (var cliente in clientes)
            {
                Console.WriteLine($"Nome: {cliente.Nome}");

                foreach (var endereco in cliente.Enderecos)
                    Console.WriteLine($"Estado: {endereco.Estado} - Cidade: {endereco.Cidade} - Rua: {endereco.Rua}");
            }
        }
        catch (DbUpdateException exception)
        {
            Console.WriteLine();
            Console.WriteLine("Não foi possível listar a clientes com endereco.");
            Console.WriteLine(exception.InnerException?.Message);
        }
        catch (Exception exception)
        {
            Console.WriteLine();
            Console.WriteLine("Ocorreu um erro inesperado.");
            Console.WriteLine(exception.Message);
        }
    }

    private static async Task ListarClientesSemEnderecosAsync()
    {
        Console.Clear();
        Console.WriteLine("Clientes sem endereco");
        Console.WriteLine();

        try
        {
            using var context =  new CadastroClienteDataContext();

            var clientes = await context.Clientes
                .Where(x => !x.Enderecos.Any())
                .Select(x => new
                {
                    x.Nome
                })
                .AsNoTracking()
                .OrderBy(x => x.Nome)
                .ToListAsync();

            if (!clientes.Any())
            {
                Console.WriteLine("Nenhum cliente cadastrado.");
                return;
            }

            foreach (var cliente in clientes)
            {
                Console.WriteLine($"Nome: {cliente.Nome}");
            }
        }
        catch (DbUpdateException exception)
        {
            Console.WriteLine();
            Console.WriteLine("Não foi possível listar a clientes sem endereco.");
            Console.WriteLine(exception.InnerException?.Message);
        }
        catch (Exception exception)
        {
            Console.WriteLine();
            Console.WriteLine("Ocorreu um erro inesperado.");
            Console.WriteLine(exception.Message);
        }
    }

    private static async Task CompararIncludeESelectAsync()
    {
        Console.Clear();
        Console.WriteLine("Comparação entre Include e Select");
        Console.WriteLine();

        Console.Write("ID do cliente: ");
        if (!int.TryParse(Console.ReadLine(), out var clienteId))
        {
            Console.WriteLine("ID inválido.");
            return;
        }

        await using var context = new CadastroClienteDataContext();

        var consultaComInclude = context.Clientes
            .AsNoTracking()
            .Include(c => c.Enderecos)
            .Where(c => c.Id == clienteId);

        Console.WriteLine("SQL COM INCLUDE");
        Console.WriteLine(consultaComInclude.ToQueryString());
        Console.WriteLine();

        var clienteComInclude = await consultaComInclude
            .FirstOrDefaultAsync();

        var consultaComSelect = context.Clientes
            .AsNoTracking()
            .Where(c => c.Id == clienteId)
            .Select(c => new
            {
                c.Nome,
                Enderecos = c.Enderecos
                    .Select(e => new
                    {
                        e.Rua,
                        e.Numero
                    })
                    .ToList()
            });

        Console.WriteLine("SQL COM SELECT");
        Console.WriteLine(consultaComSelect.ToQueryString());
        Console.WriteLine();

        var clienteComSelect = await consultaComSelect
            .FirstOrDefaultAsync();

        Console.WriteLine("Resultado com Include:");

        if (clienteComInclude is not null)
        {
            Console.WriteLine(clienteComInclude.Nome);

            foreach (var endereco in clienteComInclude.Enderecos)
            {
                Console.WriteLine($"{endereco.Rua}, {endereco.Numero}");
            }
        }

        Console.WriteLine();

        Console.WriteLine("Resultado com Select:");

        if (clienteComSelect is not null)
        {
            Console.WriteLine(clienteComSelect.Nome);

            foreach (var endereco in clienteComSelect.Enderecos)
            {
                Console.WriteLine($"{endereco.Rua}, {endereco.Numero}");
            }
        }
    }

    
    private static void AguardarContinuacao()
    {
        Console.WriteLine();
        Console.WriteLine("Pressione qualquer tecla para continuar...");
        Console.ReadKey();
    }
}
