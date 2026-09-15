# 🎮 ApiSistemaGeek

API REST para um sistema de e-commerce de produtos geek, desenvolvida em **C# com ASP.NET Core**.

O projeto começou como um exercício de aprendizado em C# e ASP.NET Core e evoluiu gradualmente para uma aplicação completa, com autenticação, gerenciamento de produtos, usuários, pedidos e controle de estoque.

## 🚀 Tecnologias utilizadas

* C#
* ASP.NET Core
* Entity Framework Core
* SQL Server
* JWT Authentication
* BCrypt
* Swagger / OpenAPI
* HTML
* CSS
* JavaScript

## 📌 Funcionalidades

### Usuários

* Cadastro de usuários
* Login
* Autenticação com JWT
* Controle de acesso entre usuário e administrador
* Senha armazenada de forma segura utilizando hash

### Produtos

* Cadastro de produtos
* Consulta de produtos
* Alteração de produtos
* Exclusão de produtos
* Imagem dos produtos

### Estoque

* Criação automática do estoque ao cadastrar um produto
* Entrada de estoque
* Saída de estoque
* Consulta de estoque
* Validação de quantidade disponível
* Validação de produto sem estoque

### Pedidos

* Carrinho de compras
* Criação de pedidos
* Itens do pedido
* Controle da quantidade disponível em estoque
* Validação durante o checkout

## 🏗️ Estrutura da API

O projeto está organizado em camadas principais:

```text
ApiSistemaGeek
│
├── Controllers
├── Data
├── DTOs
├── Model
├── Program.cs
└── appsettings.json
```

### Controllers

Responsáveis por receber as requisições HTTP e disponibilizar os endpoints da API.

### Model

Contém as entidades utilizadas pela aplicação e relacionadas ao banco de dados.

### DTOs

Objetos utilizados para transportar os dados necessários entre cliente e API.

### Data

Contém o `AppDbContext` e a configuração de acesso ao banco de dados utilizando Entity Framework Core.

## 🔐 Autenticação

A API utiliza **JWT (JSON Web Token)** para autenticação.

Após o login, o token é armazenado no frontend e enviado nas requisições que necessitam de autorização.

Exemplo:

```text
Authorization: Bearer {token}
```

## 🗄️ Banco de dados

O projeto utiliza **SQL Server** com **Entity Framework Core**.

Principais entidades:

* ProdutoGeek
* Usuario
* Pedido
* PedidoItem
* Estoque

## 📖 Documentação da API

A API possui documentação interativa através do **Swagger**.

Com a aplicação executando, os endpoints podem ser testados diretamente pelo Swagger.

## 🖥️ Frontend

O frontend foi desenvolvido utilizando:

* HTML
* CSS
* JavaScript

Ele realiza as requisições para a API ASP.NET Core utilizando `fetch`.

## 🎯 Objetivo do projeto

O objetivo inicial era praticar conceitos de desenvolvimento em C#.

Durante o desenvolvimento, o projeto foi evoluindo para colocar em prática conceitos como:

* Programação Orientada a Objetos
* APIs REST
* HTTP
* CRUD
* Entity Framework Core
* SQL
* Autenticação e autorização
* JWT
* DTOs
* Relacionamentos entre entidades
* Regras de negócio
* Controle de estoque
* Integração entre frontend e backend

## 📚 Aprendizados

Este projeto foi desenvolvido de forma incremental, permitindo aplicar na prática conhecimentos adquiridos durante meus estudos de **C# e .NET**.

O projeto continua em evolução e novas funcionalidades poderão ser adicionadas futuramente.
