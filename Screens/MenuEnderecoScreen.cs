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
                    await AtualizaEnderecoAsync();
                    break;
                
                case "5":
                    await ExcluirEnderecoAsync();
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
            var cliente = await context.Clientes.FirstOrDefaultAsync(x => x.Id == idCliente);

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
            }
            
            Console.Write("Cidade: ");
            var cidade = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(cidade))
            {
                Console.WriteLine();
                Console.WriteLine("Cidade nao pode ser vazio.");
            }
            
            Console.Write("Estado: ");
            var estado = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(estado))
            {
                Console.WriteLine();
                Console.WriteLine("Estado nao pode ser vazio.");
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
            
            var enderecos =  await context.Enderecos.ToListAsync();

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
                .Include(c => c.Enderecos)
                .FirstOrDefaultAsync(c => c.Id == idCliente);

            if (cliente is null)
            {
                Console.WriteLine("Cliente não encontrado.");
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

    private static async Task AtualizaEnderecoAsync()
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
                .FirstOrDefaultAsync(x => x.Id == idEndereco);
            
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
            
            context.Enderecos.Update(endereco);
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
            
            var endereco =  await context.Enderecos.FirstOrDefaultAsync(x => x.Id == idEndereco);

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
            Console.WriteLine("Não foi possível atualizar o endereco do cliente");
            Console.WriteLine(exception.InnerException?.Message);
        }
        catch (Exception exception)
        {
            Console.WriteLine();
            Console.WriteLine("Ocorreu um erro inesperado.");
        }
    }
    
    
    private static void AguardarContinuacao()
    {
        Console.WriteLine();
        Console.WriteLine("Pressione qualquer tecla para continuar...");
        Console.ReadKey();
    }
}