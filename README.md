
# FIAP Cloud Games API

Bem-vindo à **FIAP Cloud Games API**, uma plataforma robusta para gerenciamento de jogos e usuários, desenvolvida como parte dos estudos na FIAP. A API é construída com .NET 8 e segue princípios de Arquitetura Limpa para garantir um código desacoplado, testável e de fácil manutenção.

-----

## ❯ Índice

  - [Visão Geral]
  - [Funcionalidades]
  - [Tecnologias Utilizadas]
  - [Estrutura do Projeto]
  - [Primeiros Passos]
      - [Pré-requisitos]
      - [Configuração]
      - [Executando a Aplicação]
  - [Documentação da API (Endpoints)]
      - [Autenticação]
      - [Usuários]
      - [Jogos]
  - [Como Utilizar a API]
  - [Testes]
  - [Contato e Licença]

-----

## ❯ Visão Geral

Este projeto é uma API RESTful que permite a administração de uma plataforma de jogos. Suporta operações completas de CRUD para usuários e jogos, com um sistema de autenticação baseado em JWT e autorização por papéis (Admin e User).

## ❯ Funcionalidades

  - ✅ **Gerenciamento de Usuários**: CRUD completo, alteração de senha e atribuição de papéis.
  - ✅ **Gerenciamento de Jogos**: CRUD completo para jogos.
  - ✅ **Autenticação Segura**: Login via JWT (JSON Web Token).
  - ✅ **Autorização por Papel**: Endpoints protegidos que exigem autenticação e, em alguns casos, o nível de `Admin`.
  - ✅ **Validações de Dados**: Validações detalhadas para criação e atualização de entidades.
  - ✅ **Documentação Interativa**: Geração automática de documentação da API com Swagger e ReDoc.
  - ✅ **Tratamento de Erros**: Middleware centralizado para tratamento de exceções.
  - ✅ **Logging**: Logs estruturados com Serilog para monitoramento e depuração.

## ❯ Tecnologias Utilizadas

  - **Backend**: .NET 8, ASP.NET Core
  - **Banco de Dados**: SQL Server, Entity Framework Core 8
  - **Autenticação**: ASP.NET Core Identity, JWT Bearer
  - **Testes**: xUnit, Moq
  - **Documentação**: Swashbuckle (Swagger), ReDoc
  - **Logging**: Serilog
  - **Hashing**: BCrypt.Net

## ❯ Estrutura do Projeto

O projeto adota uma arquitetura inspirada na Clean Architecture, dividindo as responsabilidades em camadas distintas:

  - `FiapCloudGames.Core`: Contém as entidades de negócio, DTOs (Data Transfer Objects) e as interfaces dos serviços e repositórios. Não possui dependências externas.
  - `FiapCloudGames.Application`: Implementa a lógica de negócio da aplicação (os "Services"). Depende apenas do `Core`.
  - `FiapCloudGames.Infrastructure`: Contém as implementações das interfaces definidas no `Core`, como os repositórios (usando Entity Framework Core) e a configuração do banco de dados.
  - `FiapCloudGames.API`: Ponto de entrada da aplicação. Define os Endpoints, Middlewares e realiza a injeção de dependências.
  - `FiapCloudGames.Tests`: Contém os testes de unidade para as camadas de serviço.

## ❯ Primeiros Passos

Siga os passos abaixo para configurar e executar o projeto em seu ambiente de desenvolvimento.

### Pré-requisitos

  - **.NET 8 SDK**: [Download .NET 8](https://dotnet.microsoft.com/download/dotnet/8.0)
  - **SQL Server**: Qualquer edição, como a Express ou Developer.
  - **IDE de sua preferência**: Visual Studio 2022, JetBrains Rider ou Visual Studio Code.

### Configuração

1.  **Clone o repositório:**

    ```bash
    git clone <URL_DO_REPOSITORIO>
    cd FiapCloudGames.API
    ```

2.  **Configure a Conexão com o Banco de Dados:**
    Abra o arquivo `FiapCloudGames.API/appsettings.Development.json`. Modifique a string de conexão `SQLServer` para apontar para sua instância local do SQL Server.

    ```json
    "ConnectionStrings": {
      "SQLServer": "Server=SEU_SERVIDOR;Database=fiap-cloud-games;User Id=SEU_USUARIO;Password=SUA_SENHA;TrustServerCertificate=True;"
    }
    ```

3.  **Aplique as Migrations do Banco de Dados:**
    O projeto utiliza Entity Framework Core Migrations para criar e atualizar o esquema do banco de dados. Abra um terminal na pasta do projeto `FiapCloudGames.API` e execute o comando:

    ```bash
    dotnet ef database update
    ```

    Pode ser possível que precise indicar os projetos de startup e o projeto que contém as migrations.
    ```bash
    dotnet ef database update --project .\FiapCloudGames.Infrastructure\FiapCloudGames.Infrastructure.csproj  --startup-project .\FiapCloudGames.API\FiapCloudGames.API.csproj
    ```

    Isso criará o banco de dados `fiap-cloud-games`, as tabelas `Users` e `Games`, e inserirá um usuário administrador padrão.

### Executando a Aplicação

1.  **Inicie a API:**
    Ainda no terminal, na pasta `FiapCloudGames.API`, execute:

    ```bash
    dotnet run
    ```

2.  **Acesse a Documentação:**
    A aplicação iniciará e abrirá automaticamente o navegador na interface do Swagger, geralmente em `https://localhost:7253/swagger`. A partir daí, você pode explorar e interagir com todos os endpoints.
    Também é possível acessar a documentação gerada usando Redoc em `https://localhost:7253/api-docs`.

## ❯ Documentação da API (Endpoints)

A API está organizada em três grupos de endpoints:

### Autenticação

  - **`POST /api/auth`**
      - **Descrição**: Autentica um usuário e retorna um token JWT.
      - **Autorização**: Nenhuma.

### Usuários

  - **`POST /api/user`**

      - **Descrição**: Cria um novo usuário (padrão `Role` = "User").
      - **Autorização**: Nenhuma.

  - **`GET /api/user`**

      - **Descrição**: Retorna uma lista de todos os usuários.
      - **Autorização**: Admin.

  - **`GET /api/user/{id}`**

      - **Descrição**: Retorna um usuário específico pelo seu ID.
      - **Autorização**: Admin.

  - **`PUT /api/user/{id}`**

      - **Descrição**: Atualiza os dados de um usuário.
      - **Autorização**: Autenticado.

  - **`POST /api/user/{id}/changePassword`**

      - **Descrição**: Altera a senha de um usuário.
      - **Autorização**: Autenticado.

  - **`POST /api/user/{id}/changeRole/{role}`**

      - **Descrição**: Altera o papel de um usuário (`Admin` ou `User`).
      - **Autorização**: Admin.

  - **`DELETE /api/user/{id}`**

      - **Descrição**: Exclui um usuário.
      - **Autorização**: Admin.

### Jogos

  - **`GET /api/game`**

      - **Descrição**: Retorna uma lista de todos os jogos.
      - **Autorização**: Autenticado.

  - **`GET /api/game/{id}`**

      - **Descrição**: Retorna um jogo específico pelo seu ID.
      - **Autorização**: Autenticado.

  - **`POST /api/game`**

      - **Descrição**: Adiciona um novo jogo ao catálogo.
      - **Autorização**: Admin.

  - **`PUT /api/game/{id}`**

      - **Descrição**: Atualiza os dados de um jogo existente.
      - **Autorização**: Admin.

  - **`DELETE /api/game/{id}`**

      - **Descrição**: Remove um jogo do catálogo.
      - **Autorização**: Admin.

## ❯ Como Utilizar a API

1.  **Usuário Admin Padrão**:
    A aplicação já vem com um usuário `Admin` pré-configurado:

      - **Email**: `admin@fiap.com.br`
      - **Senha**: `Admin@123`

2.  **Obtenha o Token**:
    Use o endpoint `POST /api/auth` com as credenciais acima (ou de um usuário que você criou) para receber um token JWT.

3.  **Autorize suas Requisições**:
    Na interface do Swagger, clique no botão `Authorize` e insira o token recebido no formato `Bearer <SEU_TOKEN>`. Para outras ferramentas (como Postman), adicione o `Header` `Authorization` com o mesmo valor em todas as requisições para endpoints protegidos.

## ❯ Testes

O projeto inclui testes de unidade para garantir a qualidade e o correto funcionamento da lógica de negócio. Para executar os testes, navegue até a raiz do repositório e utilize o comando:

```bash
dotnet test
```

## ❯ Contato e Licença

  - **Autor**: Taylor Figueira Martins
  - **Email**: [taylorfmartins@gmail.com](mailto:taylorfmartins@gmail.com)
  - **LinkedIn**: [linkedin.com/in/taylorfmartins](https://www.linkedin.com/in/taylorfmartins/)

Este projeto está sob a licença **MIT**.
