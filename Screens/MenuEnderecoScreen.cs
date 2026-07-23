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
            Console.WriteLine("0 - Voltar");
            Console.WriteLine();

            Console.Write("Escolha uma opção: ");
            var opcao = Console.ReadLine();
            
            switch (opcao)
            {
                
                case "1":
                    Console.Clear();
                    Console.WriteLine("Cadastro de cliente");
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
    
    private static void AguardarContinuacao()
    {
        Console.WriteLine();
        Console.WriteLine("Pressione qualquer tecla para continuar...");
        Console.ReadKey();
    }

}