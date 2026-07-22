using CadastroDeClientesDesafioEntityFrameworkCore.Data;
using CadastroDeClientesDesafioEntityFrameworkCore.Models;
using Microsoft.EntityFrameworkCore;

namespace CadastroDeClientesDesafioEntityFrameworkCore.Screens;

public static class MenuScreen
{
    public static async Task ShowAsync()    
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Cadastro de clientes");
            
            Console.WriteLine("1 - Cadastrar cliente");
            Console.WriteLine("2 - Listar clientes");
            Console.WriteLine("3 - Buscar cliente pelo ID");
            Console.WriteLine("4 - Atualizar cliente");
            Console.WriteLine("5 - Excluir cliente");
            Console.WriteLine("0 - Sair");
            Console.WriteLine();

            Console.Write("Escolha uma opção: ");
            var opcao = Console.ReadLine();

            switch (opcao)
            {
                case "1":
                    await CadastrarClienteAsync();
                    break;
                
                case "2":
                    await ListarClientesAsync();
                    break;
                
                case "3":
                    await BuscarClientePeloIdAsync();
                    break;
                
                case "4":
                    await AtualizarClienteAsync();
                    break;
                
                case "5":
                    await ExcluirClienteAsync();
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

    private static async Task CadastrarClienteAsync()
    {
        Console.Clear();
        Console.WriteLine("Cadastro de cliente");
        Console.WriteLine();

        try
        {
            Console.Write("Nome: ");
            var nome = Console.ReadLine()?.Trim();
            
            if (string.IsNullOrWhiteSpace(nome))
            {
                Console.WriteLine();
                Console.WriteLine("Nome não pode ser vazio.");
                return;
            }
            
            Console.Write("Email: ");
            var email = Console.ReadLine()?.Trim().ToLowerInvariant();
            
            if (string.IsNullOrWhiteSpace(email))
            {
                Console.WriteLine();
                Console.WriteLine("Email não pode ser vazio.");
                return;
            }
            
            Console.Write("Telefone: ");
            var telefone = Console.ReadLine()?.Trim();

            await using var context = new CadastroClienteDataContext();
        
            var emailJaExiste = await context.Clientes
                .AnyAsync(x => x.Email == email);

            if (emailJaExiste)
            {
                Console.WriteLine();
                Console.WriteLine("Já existe um cliente com esse e-mail.");
                return;
            }

            var cliente = new Cliente
            {
                Nome = nome,
                Email = email,
                Telefone = string.IsNullOrWhiteSpace(telefone)
                    ? null
                    : telefone
            };
        
            await context.Clientes.AddAsync(cliente);
            await context.SaveChangesAsync();

            Console.WriteLine();
            Console.WriteLine("Cadastro realizado com sucesso!");
        }
        catch (DbUpdateException exception)
        {
            Console.WriteLine();
            Console.WriteLine("Não foi possível cadastrar o cliente");
            Console.WriteLine(exception.InnerException?.Message);
        }
        catch (Exception exception)
        {
            Console.WriteLine();
            Console.WriteLine("Ocorreu um erro inesperado.");
            Console.WriteLine(exception.Message);
        }
    }

    private static async Task ListarClientesAsync()
    {
        Console.Clear();
        Console.WriteLine("Listar clientes");
        Console.WriteLine();
        
        try
        {
            await using var context = new CadastroClienteDataContext();
            
            var clientes = await context.Clientes
                .AsNoTracking()
                .OrderBy(x => x.Nome)
                .ToListAsync();
            
            if (clientes.Count == 0)
            {
                Console.WriteLine("Nenhum cliente cadastrado.");
                return;
            }
            
            foreach (var cliente in clientes)
                Console.WriteLine($"ID: {cliente.Id} - Nome: {cliente.Nome} -  Email: {cliente.Email} - Telefone: {cliente.Telefone}");
        }
        catch (DbUpdateException exception)
        {
            Console.WriteLine();
            Console.WriteLine("Não foi possível listar os clientes.");
            Console.WriteLine(exception.InnerException?.Message);
        }
        catch (Exception exception)
        {
            Console.WriteLine();
            Console.WriteLine("Ocorreu um erro inesperado.");
            Console.WriteLine(exception.Message);
        }
    }

    private static async Task BuscarClientePeloIdAsync()
    {
        Console.Clear();
        Console.WriteLine("Buscar cliente pelo ID");
        Console.WriteLine();
        
        Console.Write("ID: ");

        if (!int.TryParse(Console.ReadLine(), out var id))
        {
            Console.WriteLine("ID inválido.");
            return;
        }

        try
        {
            await using var context = new CadastroClienteDataContext();

            var cliente = await context.Clientes.FirstOrDefaultAsync(x => x.Id == id);

            if (cliente == null)
            {
                Console.WriteLine("Cliente não encontrado.");
                return;
            }

            Console.Clear();
            Console.WriteLine(
                $"ID: {cliente.Id} - Nome: {cliente.Nome} -  Email: {cliente.Email} - Telefone: {cliente.Telefone}");
        }
        catch (DbUpdateException exception)
        {
            Console.WriteLine();
            Console.WriteLine("Não foi possível buscar o cliente.");
            Console.WriteLine(exception.InnerException?.Message);
        }
        catch (Exception exception)
        {
            Console.WriteLine();
            Console.WriteLine("Ocorreu um erro inesperado.");
            Console.WriteLine(exception.Message);
        }
    }

    private static async Task AtualizarClienteAsync()
    {
        Console.Clear();
        Console.WriteLine("Atualização de cliente");
        Console.WriteLine();

        Console.Write("ID: ");

        if (!int.TryParse(Console.ReadLine(), out var id))
        {
            Console.WriteLine("ID inválido.");
            return;
        }

        try
        {
            await using var context = new CadastroClienteDataContext();

            var cliente = await context.Clientes
                .FirstOrDefaultAsync(x => x.Id == id);

            if (cliente is null)
            {
                Console.WriteLine("Cliente não encontrado.");
                return;
            }

            Console.Write($"Nome ({cliente.Nome}): ");
            var nome = Console.ReadLine()?.Trim();

            if (string.IsNullOrWhiteSpace(nome))
            {
                nome = cliente.Nome;
            }

            Console.Write($"E-mail ({cliente.Email}): ");
            var email = Console.ReadLine()?.Trim().ToLowerInvariant();
            
            if (string.IsNullOrWhiteSpace(email))
            {
                email = cliente.Email;
            }

            Console.Write($"Telefone ({cliente.Telefone}): ");
            var telefone = Console.ReadLine()?.Trim();
            
            if (string.IsNullOrWhiteSpace(telefone))
            {
                telefone = cliente.Telefone;
            }

            var emailJaExiste = await context.Clientes.AnyAsync(x =>
                x.Email == email && x.Id != id
            );

            if (emailJaExiste)
            {
                Console.WriteLine("Já existe outro cliente com esse e-mail.");
                return;
            }

            cliente.Nome = nome;
            cliente.Email = email;
            cliente.Telefone = telefone;

            await context.SaveChangesAsync();

            Console.WriteLine();
            Console.WriteLine("Cliente atualizado com sucesso!");
        }
        catch (DbUpdateException exception)
        {
            Console.WriteLine("Não foi possível atualizar o cliente.");
            Console.WriteLine(exception.InnerException?.Message);
        }
        catch (Exception exception)
        {
            Console.WriteLine();
            Console.WriteLine("Ocorreu um erro inesperado.");
            Console.WriteLine(exception.Message);
        }
    }
    private static async Task ExcluirClienteAsync()
    {
        Console.Clear();
        Console.WriteLine("Excluir cliente");
        Console.WriteLine();

        try
        {
            Console.Write("ID: ");
            var id = Console.ReadLine();

            if (!int.TryParse(id, out var idCliente))
            {
                Console.WriteLine("Digite um ID válido.");
                return;
            }
            
            await using var context = new CadastroClienteDataContext();
            
            var cliente = await context.Clientes.FirstOrDefaultAsync(x => x.Id == idCliente);

            if (cliente == null)
            {
                Console.WriteLine("Cliente não encontrado.");
                return;
            }
            
            Console.Write($"Deseja excluir ({cliente.Nome}) S/N?");

            if (Console.ReadKey().Key == ConsoleKey.S)
            {
                context.Clientes.Remove(cliente);
                await context.SaveChangesAsync();
                Console.WriteLine();
                Console.WriteLine("Cliente removido com sucesso!");
            }
        }
        catch (DbUpdateException exception)
        {
            Console.WriteLine();
            Console.WriteLine("Não foi possível excluir o cliente.");
            Console.WriteLine(exception.InnerException?.Message);
        }
        catch (Exception exception)
        {
            Console.WriteLine();
            Console.WriteLine("Ocorreu um erro inesperado.");
            Console.WriteLine(exception.Message);
        }
        
    }

    private static void AguardarContinuacao()
    {
        Console.WriteLine();
        Console.WriteLine("Pressione qualquer tecla para continuar...");
        Console.ReadKey();
    }
    
}