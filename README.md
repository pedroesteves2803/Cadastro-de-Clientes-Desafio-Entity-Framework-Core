# Cadastro de Clientes com Entity Framework Core

Projeto de estudo desenvolvido em C# para praticar o uso do Entity Framework
Core com SQL Server. A aplicação roda no console e permite gerenciar clientes
e seus endereços.

O objetivo deste repositório é registrar minha evolução com acesso a dados:
comecei com um CRUD simples de clientes e depois adicionei um relacionamento
1:N, novas migrations e consultas LINQ um pouco mais elaboradas.

## Funcionalidades

### Clientes

- Cadastro com validação de nome, email e telefone.
- Listagem ordenada por nome.
- Busca por ID.
- Atualização dos dados.
- Exclusão com confirmação.
- Validação de email único.

### Endereços

- Cadastro de endereço para um cliente existente.
- Listagem geral e por cliente.
- Atualização e exclusão.
- Busca por cidade.
- Ordenação por estado, cidade e rua.
- Projeção de campos específicos.
- Contagem de endereços por cliente.
- Listagem de clientes com e sem endereço.
- Comparação do SQL gerado por `Include()` e `Select()`.

## Modelo de dados

O projeto possui um relacionamento de um para muitos:

```text
Cliente (1) ────────── (N) Endereco
                         ClienteId
```

`Endereco.ClienteId` é a chave estrangeira que referencia `Cliente.Id`. O
relacionamento foi configurado com Fluent API por meio de
`HasOne()`, `WithMany()` e `HasForeignKey()`.

As entidades possuem propriedades de navegação nos dois sentidos:

- `Cliente.Enderecos` representa a coleção de endereços do cliente.
- `Endereco.Cliente` representa o cliente ao qual o endereço pertence.

A migration do relacionamento utiliza exclusão em cascata. Portanto, ao
excluir um cliente, seus endereços também são removidos pelo banco.

## Conceitos praticados

- `DbContext` e `DbSet`.
- Fluent API e classes de mapping.
- Migrations.
- Chave primária, chave estrangeira e índice único.
- Relacionamento 1:N.
- Propriedades de navegação.
- Change Tracking e `AsNoTracking()`.
- Consultas LINQ traduzidas para SQL.
- Execução assíncrona com `async`/`await`.
- `FindAsync()`, `FirstOrDefaultAsync()`, `AnyAsync()` e `ToListAsync()`.
- `Where()`, `OrderBy()`, `ThenBy()`, `Select()` e `Count`.
- Carregamento relacionado com `Include()`.
- Projeções para retornar apenas os campos necessários.
- Inspeção do SQL com `ToQueryString()`.

## Include e Select

Uma das consultas do projeto recupera o mesmo cliente e seus endereços de
duas formas para permitir a comparação do SQL:

- `Include()` retorna as entidades completas de cliente e endereço.
- `Select()` cria uma projeção somente com nome, rua e número.

Quando a aplicação precisa trabalhar com as entidades completas, `Include()`
é uma opção direta. Quando precisa apenas de alguns dados para leitura, a
projeção evita que colunas desnecessárias sejam retornadas.

## Estrutura

```text
.
├── Data
│   ├── CadastroClienteDataContext.cs
│   └── Mappings
│       ├── ClienteMap.cs
│       └── EnderecoMap.cs
├── Migrations
├── Models
│   ├── Cliente.cs
│   └── Endereco.cs
├── Screens
│   ├── MenuClienteScreen.cs
│   └── MenuEnderecoScreen.cs
├── Program.cs
├── appsettings.example.json
└── CadastroDeClientesDesafioEntityFrameworkCore.csproj
```

O acesso ao `DbContext` permanece direto nas telas de console. Isso foi
intencional: nesta etapa, o foco era compreender o comportamento do EF Core
antes de introduzir Repository Pattern, Service Layer ou outras abstrações.

## Tecnologias

- .NET 10
- C#
- Entity Framework Core 10
- SQL Server
- Microsoft.EntityFrameworkCore.SqlServer
- Microsoft.EntityFrameworkCore.Design

## Como executar

### Pré-requisitos

- .NET 10 SDK.
- SQL Server disponível localmente ou em container.
- Ferramenta `dotnet-ef`.

### 1. Clone o repositório

```bash
git clone https://github.com/pedroesteves2803/cadastro-clientes-ef-core.git
cd cadastro-clientes-ef-core
```

### 2. Configure a conexão

Copie `appsettings.example.json` para `appsettings.json` e informe os dados da
sua instância do SQL Server:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost,1433;Database=CadastroClientes;User ID=sa;Password=SUA_SENHA;TrustServerCertificate=True"
  }
}
```

Não envie credenciais reais para o repositório.

### 3. Restaure os pacotes e crie o banco

```bash
dotnet restore
dotnet ef database update
```

### 4. Execute

```bash
dotnet run
```

## Migrations

O histórico do projeto mostra a evolução do modelo:

1. Criação da tabela de clientes.
2. Ajuste do tipo de `DataCadastro`.
3. Alteração de telefone para campo opcional.
4. Criação da tabela de endereços e da chave estrangeira.

Para conferir se o modelo possui alterações ainda não representadas por uma
migration:

```bash
dotnet ef migrations has-pending-model-changes
```

## Validação

Os fluxos foram exercitados manualmente pelo menu da aplicação, incluindo:

- Campos obrigatórios vazios.
- Email duplicado.
- IDs inválidos e registros inexistentes.
- Cliente com vários endereços.
- Cliente sem endereço.
- Atualização e exclusão.
- Integridade da chave estrangeira.
- Exclusão em cascata.

O projeto pode ser validado com:

```bash
dotnet build
```

## Status

Projeto concluído como exercício de fundamentos do Entity Framework Core e
LINQ.
