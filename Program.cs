using CadastroDeClientesDesafioEntityFrameworkCore.Screens;


while (true)
{
    
    Console.Clear();
    Console.WriteLine("1 - Cliente");
    Console.WriteLine("2 - Endereco");
    Console.WriteLine("0 - Sair");

    Console.Write("Selecione uma opção:");
    var opcao = Console.ReadLine();
    
    switch (opcao)
    {
        case "1":
            await MenuClienteScreen.ShowAsync();
            break;
        
        case "2":
            await MenuEnderecoScreen.ShowAsync();
            break;
        
        case "0":
            return;
                
        default:
            Console.Clear();
            Console.WriteLine("Opção inválida.");
            break;
    }
}