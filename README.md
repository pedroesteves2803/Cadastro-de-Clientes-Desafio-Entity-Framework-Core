# Cadastro de Clientes - Desafio Entity Framework Core

## Objetivo

Este projeto tem como objetivo praticar os fundamentos do **Entity Framework Core** utilizando **C#**, desenvolvendo uma aplicação Console simples para gerenciamento de clientes.

O foco não é criar um sistema completo, mas entender todo o fluxo de comunicação entre a aplicação e o banco de dados.

Ao final deste desafio você deverá compreender:

* O que é uma Entidade (Entity)
* O que é um `DbContext`
* O que é um `DbSet`
* Como funciona o Fluent API (Mapping)
* Como criar e executar Migrations
* Como salvar dados no banco
* Como consultar dados utilizando LINQ
* Como realizar um CRUD completo utilizando Entity Framework Core

---

# Tecnologias

* .NET
* C#
* Entity Framework Core
* SQL Server
* Fluent API
* LINQ
* Console Application

---

# Objetivos de Aprendizagem

Ao concluir este projeto você deverá ser capaz de responder:

* Como o Entity Framework conversa com o banco?
* Qual a função do DbContext?
* Para que serve um DbSet?
* O que é uma Migration?
* Como o EF sabe qual tabela utilizar?
* Como inserir, atualizar, consultar e remover dados?

---

# Estrutura Esperada

```text
CadastroDeClientes
│
├── Data
│   ├── CadastroDataContext.cs
│   └── Mappings
│       └── ClienteMap.cs
│
├── Models
│   └── Cliente.cs
│
├── Screens
│   └── MenuScreen.cs
│
└── Program.cs
```

---

# Funcionalidades

O sistema deverá permitir:

* Cadastrar cliente
* Listar clientes
* Buscar cliente pelo ID
* Atualizar cliente
* Excluir cliente

---

# Entidade Cliente

A entidade deverá possuir os seguintes campos:

| Campo        | Tipo     | Regras                                    |
| ------------ | -------- | ----------------------------------------- |
| Id           | int      | Chave primária, auto incremento           |
| Nome         | string   | Obrigatório, máximo 100 caracteres        |
| Email        | string   | Obrigatório, máximo 150 caracteres, único |
| Telefone     | string   | Opcional, máximo 20 caracteres            |
| DataCadastro | DateTime | Obrigatório                               |

---

# Regras de Banco

Configure utilizando Fluent API:

* Tabela chamada **Cliente**
* Chave primária
* Identity para Id
* Nome obrigatório
* Email obrigatório
* Índice único para Email
* Tamanho máximo dos campos
* DataCadastro obrigatória

Não utilize Data Annotations.

---

# Banco de Dados

Utilize SQL Server.

Crie uma Migration chamada:

```
InitialCreate
```

Depois execute a atualização do banco.

Ao finalizar deverão existir pelo menos as tabelas:

* Cliente
* __EFMigrationsHistory

---

# Menu

O sistema deverá apresentar:

```
================================
CADASTRO DE CLIENTES
================================
1 - Cadastrar cliente
2 - Listar clientes
3 - Buscar cliente pelo ID
4 - Atualizar cliente
5 - Excluir cliente
0 - Sair
================================
Escolha uma opção:
```

---

# Regras do CRUD

## Cadastro

Solicitar:

* Nome
* Email
* Telefone

Validações:

* Nome obrigatório
* Email obrigatório
* Email não pode existir

Ao cadastrar corretamente:

```
Cliente cadastrado com sucesso.
```

---

## Listagem

Listar todos os clientes ordenados pelo nome.

Caso não existam registros:

```
Nenhum cliente cadastrado.
```

---

## Busca

Buscar pelo ID.

Caso não exista:

```
Cliente não encontrado.
```

---

## Atualização

Permitir alterar:

* Nome
* Email
* Telefone

Validar novamente:

* Nome obrigatório
* Email obrigatório
* Email único

---

## Exclusão

Antes de excluir solicitar confirmação.

```
Deseja realmente excluir este cliente? (S/N)
```

---

# Regras Técnicas

Durante o desenvolvimento utilize:

* async/await
* SaveChangesAsync()
* ToListAsync()
* AnyAsync()
* FindAsync() ou FirstOrDefaultAsync()
* await using para o DbContext
* int.TryParse para leitura do ID

Evite utilizar:

* Repository Pattern
* Service Layer
* Clean Architecture
* AutoMapper

O objetivo deste desafio é aprender Entity Framework Core.

---

# Ordem de Desenvolvimento

## Etapa 1

* Criar projeto
* Instalar pacotes
* Criar entidade
* Criar Mapping
* Criar DbContext
* Criar Migration
* Criar Banco

---

## Etapa 2

* Criar Menu
* Implementar Cadastro
* Implementar Listagem
* Implementar Busca

---

## Etapa 3

* Implementar Atualização
* Implementar Exclusão
* Validar regras

---

# Testes Obrigatórios

Realizar os seguintes testes:

* Cadastrar cliente válido
* Cadastrar nome vazio
* Cadastrar email vazio
* Cadastrar email duplicado
* Buscar cliente inexistente
* Atualizar cliente
* Atualizar utilizando email já existente
* Excluir cliente
* Cancelar exclusão
* Digitar letras onde é esperado um número

---

# Critério de Conclusão

O desafio será considerado concluído quando for possível:

* Criar o banco utilizando Migration
* Inserir clientes
* Consultar clientes
* Atualizar clientes
* Excluir clientes
* Explicar o papel de:

  * DbContext
  * DbSet
  * Mapping
  * Migration
  * SaveChangesAsync

Sem consultar o código ou o curso.

---

# Desafio Extra

Após concluir o projeto:

1. Adicione a entidade **Endereço**.
2. Crie um relacionamento **Cliente 1:N Endereços**.
3. Atualize o banco utilizando uma nova Migration.
4. Implemente um CRUD de endereços.

> **Importante:** só faça o desafio extra depois de concluir todo o CRUD de Clientes e entender completamente o fluxo do Entity Framework Core.
