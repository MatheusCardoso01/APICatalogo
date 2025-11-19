## 📚 DOCUMENTAÇÃO COMPLETA DO PROJETO APICatalogo
---
## 📋 Índice

### 1. [Visão Geral](#visão-geral)
   - Principais Funcionalidades

### 2. [Arquitetura](#arquitetura)
   - Padrões Implementados
   - Estrutura de Camadas

### 3. [Tecnologias Utilizadas](#tecnologias-utilizadas)

### 4. [Estrutura do Projeto](#estrutura-do-projeto)

### 5. [Modelos de Dados](#modelos-de-dados)
   - 5.1. [Categoria](#1-categoria)
   - 5.2. [Produto](#2-produto)
   - 5.3. [Cliente](#3-cliente)
   - 5.4. [ApplicationUser](#4-applicationuser)
   - 5.5. [ErrorDetails](#5-errordetails)

### 6. [DTOs (Data Transfer Objects)](#dtos-data-transfer-objects)
   - 6.1. [DTOs de Autenticação (Identity)](#dtos-de-autenticação-identity)
     - LoginModel
     - RegisterModel
     - TokenModel
     - Response
   - 6.2. [DTOs de Categoria](#dtos-de-categoria)
     - CategoriaDTO
     - CategoriaDTOUpdateRequest
     - CategoriaDTOUpdateResponse
   - 6.3. [DTOs de Produto](#dtos-de-produto)
     - ProdutoDTO
     - ProdutoDTOUpdateRequest
     - ProdutoDTOUpdateResponse
   - 6.4. [DTOs de Cliente](#dtos-de-cliente)
     - ClienteDTO
     - ClienteDTOUpdateRequest
     - ClienteDTOUpdateResponse

### 7. [Repositórios](#repositórios)
   - 7.1. [Hierarquia de Repositórios](#hierarquia-de-repositórios)
   - 7.2. [IRepository<T> (Interface Genérica)](#irepository-interface-genérica)
   - 7.3. [Repository<T> (Implementação Base)](#repository-implementação-base)
   - 7.4. [IProdutoRepository](#iprodutorepository)
   - 7.5. [ICategoriaRepository](#icategoriarepository)
   - 7.6. [IClienteRepository](#iclienterepository)
   - 7.7. [IUnityOfWork (Unit of Work)](#iunityofwork-unit-of-work)

### 8. [Controllers](#controllers)
   - 8.1. [AuthController](#authcontroller)
     - 8.1.1. [Login](#1-login)
     - 8.1.2. [Register](#2-register)
     - 8.1.3. [RefreshToken](#3-refresh-token)
     - 8.1.4. [Revoke](#4-revoke)
   - 8.2. [ProdutosController](#produtoscontroller)
     - 8.2.1. [GetAllAsync](#1-getallasync)
     - 8.2.2. [GetPrimeiroAsync](#2-getprimeiroasync)
     - 8.2.3. [GetAsync](#3-getasync)
     - 8.2.4. [GetProdutosPorCategoriaEspecificaAsync](#4-getprodutosporcategoriaespecificaasync)
     - 8.2.5. [PostAsync](#5-postasync)
     - 8.2.6. [PatchAsync (Atualização Parcial)](#6-patchasync-atualização-parcial)
     - 8.2.7. [PutAsync](#7-putasync)
     - 8.2.8. [DeleteAsync](#8-deleteasync)
     - 8.2.9. [GetAsync (Paginação)](#9-getasync-paginação)
     - 8.2.10. [GetProdutosFiltroPrecoAsync](#10-getprodutosfiltroprecoasync)
   - 8.3. [CategoriasController](#categoriascontroller)
     - 8.3.1. [GetAllAsync](#1-getallasync-1)
     - 8.3.2. [GetAsync](#2-getasync-1)
     - 8.3.3. [GetCategoriasEProdutosAsync](#3-getcategoriaseprodutosasync)
     - 8.3.4. [PostAsync](#4-postasync-1)
     - 8.3.5. [Patch (Atualização Parcial)](#5-patch-atualização-parcial)
     - 8.3.6. [PutAsync](#6-putasync-1)
     - 8.3.7. [DeleteAsync](#7-deleteasync-1)
     - 8.3.8. [Get (Paginação)](#8-get-paginação)
     - 8.3.9. [GetCategoriaFilterNome](#9-getcategoriafiltronome)
     - 8.3.10. [GetSaudacaoFromServices (Exemplo)](#10-getsaudacaofromservices-exemplo)
   - 8.4. [ClientesController](#clientescontroller)

### 9. [Autenticação e Autorização](#autenticação-e-autorização)
   - 9.1. [JWT (JSON Web Token)](#jwt-json-web-token)
   - 9.2. [TokenService (ITokenService)](#tokenservice-itokenservice)
     - 9.2.1. [GenerateAccessToken](#1-generateaccesstoken)
     - 9.2.2. [GenerateRefreshToken](#2-generaterefreshtoken)
     - 9.2.3. [GetPrincipalFromExpiredToken](#3-getprincipalfromexpiredtoken)
   - 9.3. [Fluxo de Autenticação](#fluxo-de-autenticação)
   - 9.4. [Configuração no Program.cs](#configuração-no-programcs)
   - 9.5. [ASP.NET Core Identity](#aspnet-core-identity)

### 10. [Serviços](#serviços)
   - TokenService
   - MeuServico

### 11. [Filtros](#filtros)
   - 11.1. [ApiExceptionFilter](#apiexceptionfilter)
   - 11.2. [ApiLoggingFilter](#apiloggingfilter)

### 12. [Paginação](#paginação)
   - 12.1. [PagedList<T>](#pagedlist)
   - 12.2. [Parameters](#parameters)
   - 12.3. [CategoriaFiltroNome](#categoriafiltronome)
   - 12.4. [ProdutosFiltroPreco](#produtosfiltroproco)
   - 12.5. [Exemplo de Uso](#exemplo-de-uso)

### 13. [Validações](#validações)
   - 13.1. [Validações Customizadas (Data Annotations)](#validações-customizadas-data-annotations)
     - 13.1.1. [PrimeiraLetraMaiusculaAttribute](#primeiraletramai%C3%BAsculaattribute)
     - 13.1.2. [PermissoesDeSexoAttribute](#permissoesdesexoattribute)
   - 13.2. [Validação com IValidatableObject](#validação-com-ivalidatableobject)
   - 13.3. [Validações Padrão do ASP.NET Core](#validações-padrão-do-aspnet-core)

### 14. [Logging](#logging)
   - 14.1. [Sistema de Logging Customizado](#sistema-de-logging-customizado)
   - 14.2. [CustomLogger](#customlogger)
   - 14.3. [CustomLoggerProvider](#customloggerprovider)
   - 14.4. [CustomLoggerProviderConfiguration](#customloggerproviderconfiguration)
   - 14.5. [Configuração no Program.cs](#configuração-no-programcs-1)
   - 14.6. [Exemplo de Log Gerado](#exemplo-de-log-gerado)

### 15. [Middlewares](#middlewares)
   - 15.1. [ApiExceptionMiddlewareExtensions](#apiexceptionmiddlewareextensions)
   - 15.2. [Pipeline de Middlewares (Program.cs)](#pipeline-de-middlewares-programcs)

### 16. [Configurações](#configurações)
   - 16.1. [appsettings.json](#appsettingsjson)
   - 16.2. [Program.cs - Configuração Completa](#programcs---configuração-completa)
     - 16.2.1. [Controllers e JSON](#1-controllers-e-json)
     - 16.2.2. [Repositórios](#2-repositórios)
     - 16.2.3. [Serviços](#3-serviços)
     - 16.2.4. [Swagger](#4-swagger)
     - 16.2.5. [Banco de Dados](#5-banco-de-dados)
     - 16.2.6. [Identity](#6-identity)
     - 16.2.7. [Autenticação JWT](#7-autenticação-jwt)
     - 16.2.8. [AutoMapper](#8-automapper)
     - 16.2.9. [Logging Customizado](#9-logging-customizado)

### 17. [Endpoints da API](#endpoints-da-api)
   - 17.1. [Resumo de Todos os Endpoints](#resumo-de-todos-os-endpoints)
     - 17.1.1. [Autenticação (/api/auth)](#autenticação-apiauth)
     - 17.1.2. [Produtos (/api/produtos)](#produtos-apiprodutos)
     - 17.1.3. [Categorias (/api/categorias)](#categorias-apicategorias)
     - 17.1.4. [Clientes (/api/clientes)](#clientes-apiclientes)
   - 17.2. [Exemplos de Requisições](#exemplos-de-requisições)
     - 17.2.1. [Autenticação](#autenticação)
     - 17.2.2. [Produtos](#produtos)
     - 17.2.3. [Categorias](#categorias)
     - 17.2.4. [Clientes](#clientes)

---
## 🎯 Visão Geral
APICatalogo é uma API RESTful desenvolvida em .NET 8 com ASP.NET Core, que implementa um sistema de catálogo de produtos com autenticação JWT, paginação, filtros avançados e logging personalizado.
Principais Funcionalidades:
•	✅ CRUD completo de Produtos, Categorias e Clientes
•	✅ Autenticação e autorização com JWT (Access Token + Refresh Token)
•	✅ Paginação de resultados
•	✅ Filtros personalizados
•	✅ Validações customizadas
•	✅ Logging customizado
•	✅ Tratamento global de exceções
•	✅ Suporte a PATCH (atualização parcial)
•	✅ Pattern Repository e Unit of Work
•	✅ AutoMapper para mapeamento de DTOs
•	✅ ASP.NET Core Identity para gerenciamento de usuários
---
## 🏗️ Arquitetura
O projeto segue uma arquitetura em camadas com os seguintes padrões:
Padrões Implementados:
•	Repository Pattern: Abstração da camada de acesso a dados
•	Unit of Work: Gerenciamento de transações e contexto do banco
•	DTO Pattern: Separação entre modelos de domínio e transferência de dados
•	Dependency Injection: Inversão de controle nativa do ASP.NET Core
•	Middleware Pattern: Para tratamento de exceções
•	Filter Pattern: Para logging e tratamento de exceções

Estrutura de Camadas:

````````
┌─────────────────────────────────────┐
│         Controllers                 │  ← Endpoints da API
├─────────────────────────────────────┤
│         Services/Filters            │  ← Lógica de negócio e interceptação
├─────────────────────────────────────┤
│    Repositories (Unit of Work)      │  ← Acesso a dados
├─────────────────────────────────────┤
│         DbContext (EF Core)         │  ← ORM
├─────────────────────────────────────┤
│         MySQL Database              │  ← Persistência
└─────────────────────────────────────┘
````````
---
## 🛠️ Tecnologias Utilizadas
Tecnologia	Versão	Uso
.NET	8.0	Framework principal
C#	12.0	Linguagem de programação
ASP.NET Core	8.0	Framework Web API
Entity Framework Core	9.0.10	ORM para acesso ao banco
MySQL	-	Banco de dados relacional
ASP.NET Core Identity	-	Gerenciamento de usuários e roles
JWT Bearer	-	Autenticação baseada em tokens
AutoMapper	-	Mapeamento objeto-objeto
Newtonsoft.Json	-	Serialização JSON
Swashbuckle (Swagger)	-	Documentação da API
___
## 📁 Estrutura do Projeto
````````
APICatalogo/
│
├── Controllers/              # Controladores da API
│   ├── AuthController.cs
│   ├── CategoriasController.cs
│   ├── ClientesController.cs
│   └── ProdutosController.cs
│
├── Models/                   # Modelos de domínio
│   ├── ApplicationUser.cs
│   ├── Categoria.cs
│   ├── Cliente.cs
│   ├── ErrorDetails.cs
│   └── Produto.cs
│
├── DTOs/                     # Data Transfer Objects
│   ├── Identity/
│   │   ├── LoginModel.cs
│   │   ├── RegisterModel.cs
│   │   ├── TokenModel.cs
│   │   └── Response.cs
│   ├── Mappings/
│   │   ├── AutoMapperDTOMappingProfile.cs
│   │   └── CategoriaDTOMappingExtensions.cs
│   ├── CategoriaDTO.cs
│   ├── CategoriaDTOUpdateRequest.cs
│   ├── CategoriaDTOUpdateResponse.cs
│   ├── ClienteDTO.cs
│   ├── ClienteDTOUpdateRequest.cs
│   ├── ClienteDTOUpdateResponse.cs
│   ├── ProdutoDTO.cs
│   ├── ProdutoDTOUpdateRequest.cs
│   └── ProdutoDTOUpdateResponse.cs
│
├── Repositories/             # Camada de acesso a dados
│   ├── Interfaces/
│   │   ├── IRepository.cs
│   │   ├── IUnityOfWork.cs
│   │   ├── ICategoriaRepository.cs
│   │   ├── IClienteRepository.cs
│   │   └── IProdutoRepository.cs
│   ├── Repository.cs
│   ├── UnityOfWork.cs
│   ├── CategoriaRepository.cs
│   ├── ClienteRepository.cs
│   └── ProdutoRepository.cs
│
├── Services/                 # Serviços
│   ├── ITokenService.cs
│   ├── TokenService.cs
│   ├── IMeuServico.cs
│   └── MeuServico.cs
│
├── Filters/                  # Filtros de ação
│   ├── ApiExceptionFilter.cs
│   └── ApiLoggingFilter.cs
│
├── Extensions/               # Extensões
│   └── ApiExceptionMiddlewareExtensions.cs
│
├── Validations/              # Validações customizadas
│   ├── PrimeiraLetraMaiusculaAttribute.cs
│   └── PermissoesDeSexoAttribute.cs
│
├── Pagination/               # Paginação
│   ├── PagedList.cs
│   ├── Parameters.cs
│   ├── CategoriaFiltroNome.cs
│   └── ProdutosFiltroPreco.cs
│
├── Logging/                  # Sistema de logging
│   ├── CustomLogger.cs
│   ├── CustomLoggerProvider.cs
│   └── CustomLoggerProviderConfiguration.cs
│
├── Context/                  # Contexto do banco de dados
│   └── AppDbContext.cs
│
├── Migrations/               # Migrações do EF Core
│
├── Program.cs                # Configuração da aplicação
└── appsettings.json          # Configurações
````````
___

## 🗃️ Modelos de Dados
1. Categoria
Representa uma categoria de produtos.


public class Categoria
{
    public int CategoriaId { get; set; }
    public string? Nome { get; set; }            // max 80 caracteres
    public string? ImageUrl { get; set; }        // max 300 caracteres
    public ICollection<Produto>? Produtos { get; set; }
}

Relacionamentos:
•	Um para muitos com Produto
---
2. Produto
Representa um produto do catálogo.

public class Produto : IValidatableObject
{
    public int ProdutoId { get; set; }
    public string? Nome { get; set; }            // 5-80 caracteres, primeira letra maiúscula
    public string? Descricao { get; set; }       // max 300 caracteres
    public decimal Preco { get; set; }           // 1-10000
    public string? ImageUrl { get; set; }        // max 300 caracteres
    public int Estoque { get; set; }             // > 0
    public DateTime DataCadastro { get; set; }
    public int CategoriaId { get; set; }
    public Categoria? Categoria { get; set; }
}

Validações:
•	Nome: primeira letra maiúscula (custom attribute)
•	Descrição: primeira letra maiúscula (IValidatableObject)
•	Estoque: maior que 0
•	Preço: entre 1 e 10000
Relacionamentos:
•	Muitos para um com Categoria
---
3. Cliente
Representa um cliente.

public class Cliente
{
    public int ClienteId { get; set; }
    public string? Nome { get; set; }
    public int Idade { get; set; }               // 0-120
    public string? Sexo { get; set; }            // "Masculino" ou "Feminino"
}

Validações:
•	Idade: 0-120
•	Sexo: apenas "Masculino" ou "Feminino" (custom attribute)
---
4. ApplicationUser
Estende IdentityUser para incluir refresh tokens.

public class ApplicationUser : IdentityUser
{
    public string? RefreshToken { get; set; }
    public DateTime RefreshTokenExpirityTime { get; set; }
}

Propriedades herdadas de IdentityUser:
•	Id, UserName, Email, PasswordHash, SecurityStamp, etc.
---
5. ErrorDetails
Modelo para detalhes de erros.

public class ErrorDetails
{
    public int StatusCode { get; set; }
    public string? Message { get; set; }
    public string Trace { get; set; }
}

---
## 📦 DTOs (Data Transfer Objects)
DTOs de Autenticação (Identity)

# LoginModel

public class LoginModel
{
    [Required] public string? UserName { get; set; }
    [Required] public string? Password { get; set; }
}

# RegisterModel

public class RegisterModel
{
    [Required] public string? Username { get; set; }
    [Required, EmailAddress] public string? Email { get; set; }
    [Required] public string? Password { get; set; }
}

# TokenModel

public class TokenModel
{
    public string? AccessToken { get; set; }
    public string? RefreshToken { get; set; }
}

# Response

public class Response
{
    public string? Status { get; set; }
    public string? Message { get; set; }
}

---
# DTOs de Categoria
# CategoriaDTO

public class CategoriaDTO
{
    public int CategoriaId { get; set; }
    [Required, StringLength(80)] public string? Nome { get; set; }
    [Required, StringLength(300)] public string? ImageUrl { get; set; }
}

# CategoriaDTOUpdateRequest

public class CategoriaDTOUpdateRequest
{
    public string? Nome { get; set; }
    public string? ImageUrl { get; set; }
}

# CategoriaDTOUpdateResponse

public class CategoriaDTOUpdateResponse
{
    public int CategoriaId { get; set; }
    public string? Nome { get; set; }
    public string? ImageUrl { get; set; }
}

---

# DTOs de Produto
# ProdutoDTO

public class ProdutoDTO
{
    public int ProdutoId { get; set; }
    [Required, StringLength(80, MinimumLength = 5)]
    [PrimeiraLetraMaiuscula]
    public string? Nome { get; set; }
    
    [Required, StringLength(300)]
    public string? Descricao { get; set; }
    
    [Required, Range(1, 10000)]
    public decimal Preco { get; set; }
    
    [Required, StringLength(300)]
    public string? ImageUrl { get; set; }
}

# ProdutoDTOUpdateRequest

public class ProdutoDTOUpdateRequest : IValidatableObject
{
    [Range(1, 9999)] public int Estoque { get; set; }
    public DateTime DataCadastro { get; set; }
    
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (DataCadastro <= DateTime.Now.Date)
            yield return new ValidationResult("A data deve ser maior que a data atual");
    }
}

# ProdutoDTOUpdateResponse

public class ProdutoDTOUpdateResponse
{
    public int ProdutoId { get; set; }
    public string? Nome { get; set; }
    public string? Descricao { get; set; }
    public decimal Preco { get; set; }
    public string? ImageUrl { get; set; }
    public int Estoque { get; set; }
    public DateTime DataCadastro { get; set; }
    public int CategoriaId { get; set; }
}
---
# DTOs de Cliente
# ClienteDTO

public class ClienteDTO
{
    public int ClienteId { get; set; }
    [Required] public string? Nome { get; set; }
    [Required] public int Idade { get; set; }
    [Required] public string? Sexo { get; set; }
}

# ClienteDTOUpdateRequest

public class ClienteDTOUpdateRequest
{
    public string? Nome { get; set; }
    [Range(0, 120)] public int Idade { get; set; }
    [PermissoesDeSexo] public string? Sexo { get; set; }
}

# ClienteDTOUpdateResponse

public class ClienteDTOUpdateResponse
{
    public int ClienteId { get; set; }
    public string? Nome { get; set; }
    public int Idade { get; set; }
    public string? Sexo { get; set; }
}
---
# 🗄️ Repositórios
Hierarquia de Repositórios
IRepository<T>                    (interface genérica)
    ↓
Repository<T>                     (implementação base)
    ↓
├── IProdutoRepository  →  ProdutoRepository
├── ICategoriaRepository  →  CategoriaRepository
└── IClienteRepository  →  ClienteRepository
___
# IRepository<T> (Interface Genérica)
Define operações CRUD básicas para qualquer entidade.

public interface IRepository<T>
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T?> GetAsync(Expression<Func<T, bool>> predicate);
    T Create(T entity);
    T Update(T entity);
    T Delete(T entity);
}

Métodos:
•	GetAllAsync(): Retorna todas as entidades
•	GetAsync(predicate): Busca por expressão lambda
•	Create(entity): Adiciona nova entidade
•	Update(entity): Atualiza entidade existente
•	Delete(entity): Remove entidade
---
# Repository<T> (Implementação Base)
Implementa a interface genérica usando Entity Framework Core.

public class Repository<T> : IRepository<T> where T : class
{
    protected readonly AppDbContext _context;
    
    // Implementações usando _context.Set<T>()
}

Características:
•	Usa AsNoTracking() para melhor performance em leituras
•	Operações são síncronas no contexto, commit é feito pelo Unit of Work
---
# IProdutoRepository
Interface específica para produtos com métodos adicionais.

public interface IProdutoRepository : IRepository<Produto>
{
    Task<PagedList<Produto>> GetProdutosAsync(Parameters produtosParams);
    Task<Produto> GetPrimeiroAsync();
    Task<IEnumerable<Produto>> GetProdutosPorCategoriaEspecificaAsync(int id);
    Task<PagedList<Produto>> GetProdutosFiltroPrecoAsync(ProdutosFiltroPreco produtosFiltroParams);
}

Métodos Específicos:
1.	GetProdutosAsync: Paginação de produtos
2.	GetPrimeiroAsync: Retorna o primeiro produto
3.	GetProdutosPorCategoriaEspecificaAsync: Filtra por categoria
4.	GetProdutosFiltroPrecoAsync: Filtro de preço com critérios (maior/menor/igual)
---
# ICategoriaRepository
Interface específica para categorias.

public interface ICategoriaRepository
{
    Task<PagedList<Categoria>> GetCategoriasAsync(Parameters categoriasParams);
    Task<PagedList<Categoria>> GetCategoriaFiltroNomeAsync(CategoriaFiltroNome categoriaFiltroParams);
    Task<IEnumerable<Categoria>> GetCategoriasAsync();
    Task<Categoria> GetCategoriaAsync(int id);
    Task<Categoria> CreateCategoriaAsync(Categoria categoria);
    Task<Categoria> UpdateCategoriaAsync(Categoria categoria);
    Task<Categoria> DeleteCategoriaAsync(int id);
    Task<IEnumerable<Categoria>> GetCategoriasEProdutosAsync();
}

Métodos Específicos:
1.	GetCategoriasEProdutosAsync: Retorna categorias com produtos (Include)
2.	GetCategoriaFiltroNomeAsync: Filtro por nome com paginação
---
# IClienteRepository
Interface específica para clientes.

public interface IClienteRepository : IRepository<Cliente>
{
    Task<PagedList<Cliente>> GetClientesAsync(Parameters clientesParams);
}
___
# IUnityOfWork (Unit of Work)
Gerencia os repositórios e transações.

public interface IUnityOfWork
{
    IProdutoRepository ProdutoRepository { get; }
    ICategoriaRepository CategoriaRepository { get; }
    IClienteRepository ClienteRepository { get; }
    Task CommitAsync();
}

Implementação (UnityOfWork):
public class UnityOfWork : IUnityOfWork
{
    private IProdutoRepository? _produtoRepo;
    private ICategoriaRepository? _categoriaRepo;
    private IClienteRepository? _clienteRepo;
    public AppDbContext _context;
    
    // Propriedades com lazy loading
    public IProdutoRepository ProdutoRepository => 
        _produtoRepo ??= new ProdutoRepository(_context);
    
    // ... outras propriedades
    
    public async Task CommitAsync() => 
        await _context.SaveChangesAsync();
}

Características:
•	Lazy Loading: Repositórios são criados apenas quando acessados
•	Transação única: Todos os repositórios compartilham o mesmo contexto
•	CommitAsync: Salva todas as mudanças de uma vez
---
## 🎮 Controllers
# AuthController
Gerencia autenticação e autorização de usuários.
Rota Base: /api/auth

# Métodos:
# 1. Login

[HttpPost("login")]
public async Task<IActionResult> Login([FromBody] LoginModel model)

Funcionalidade:
•	Valida credenciais do usuário
•	Busca roles do usuário
•	Gera claims (nome, email, roles, Jti)
•	Cria access token JWT
•	Gera refresh token aleatório
•	Salva refresh token no usuário com data de expiração
•	Retorna: token, refreshToken, expiration

Resposta de Sucesso (200):

{
  "token": "eyJhbGc...",
  "refreshToken": "base64string...",
  "expiration": "2025-11-19T12:00:00Z"
}

Resposta de Erro (401): Unauthorized
---
# 2. Register

[HttpPost("register")]
public async Task<IActionResult> Register([FromBody] RegisterModel model)

Funcionalidade:
•	Verifica se usuário já existe
•	Cria novo ApplicationUser
•	Gera SecurityStamp único
•	Cria usuário no Identity
•	Retorna status de sucesso/erro
Resposta de Sucesso (200):

{
  "status": "Success",
  "message": "User created successfully!"
}

Resposta de Erro (400): 

{
  "status": "Error",
  "message": "User already exists!"
}
---
# 3. Refresh Token

[HttpPost("refresh-token")]
public async Task<IActionResult> RefreshToken(TokenModel tokenModel)

Funcionalidade:
•	Valida access token expirado
•	Extrai claims do token expirado
•	Valida refresh token do usuário
•	Verifica expiração do refresh token
•	Gera novos tokens
•	Atualiza refresh token do usuário
Fluxo:
1.	Recebe access token expirado + refresh token
2.	Extrai principal (claims) do token expirado
3.	Busca usuário e valida refresh token
4.	Gera novos tokens
5.	Retorna novos tokens
Resposta de Sucesso (200):

{
  "token": "newAccessToken...",
  "refreshToken": "newRefreshToken..."
}
___
# 4. Revoke

[Authorize]
[HttpPost("revoke/{username}")]
public async Task<IActionResult> Revoke(string username)

Funcionalidade:
•	Requer autenticação
•	Busca usuário por username
•	Remove refresh token (logout)
•	Retorna 204 No Content
Resposta de Sucesso (204): No Content
Resposta de Erro (404): Not Found
---
# ProdutosController
Gerencia operações CRUD de produtos.
Rota Base: /api/produtos
Dependências:
•	IUnityOfWork: Acesso aos repositórios
•	ILogger: Logging
•	IMapper: Mapeamento de DTOs
Métodos:
# 1. GetAllAsync

[HttpGet]
[Authorize]
public async Task<ActionResult<IEnumerable<ProdutoDTO>>> GetAllAsync()

Funcionalidade:
•	Requer autenticação
•	Retorna todos os produtos
•	Mapeia para ProdutoDTO
Endpoint: GET /api/produtos
Resposta (200): Lista de ProdutoDTO
---
# 2. GetPrimeiroAsync

[HttpGet("primeiro")]
[HttpGet("/primeiro")]
public async Task<ActionResult<ProdutoDTO>> GetPrimeiroAsync()

Funcionalidade:
•	Retorna o primeiro produto do catálogo
•	Suporta duas rotas: relativa e absoluta
Endpoints:
•	GET /api/produtos/primeiro
•	GET /primeiro (rota absoluta)
---
# 3. GetAsync

[HttpGet("{id:int:min(1)}", Name = "ObterProduto")]
public async Task<ActionResult<ProdutoDTO>> GetAsync(int id)

Funcionalidade:
•	Busca produto por ID
•	Restrição de rota: id deve ser int >= 1
Endpoint: GET /api/produtos/5
Resposta (404): Se não encontrado
---
# 4. GetProdutosPorCategoriaEspecificaAsync

[HttpGet("porcategoria/{id:int}")]
public async Task<ActionResult<IEnumerable<ProdutoDTO>>> GetProdutosPorCategoriaEspecificaAsync(int id)

Funcionalidade:
•	Filtra produtos por categoria
•	Retorna lista de produtos da categoria especificada
Endpoint: GET /api/produtos/porcategoria/2
---
# 5. PostAsync

[HttpPost]
public async Task<ActionResult<ProdutoDTO>> PostAsync(ProdutoDTO produtoDTO)

Funcionalidade:
•	Cria novo produto
•	Valida dados
•	Retorna CreatedAtRoute com link para o produto criado
Endpoint: POST /api/produtos
Resposta (201): Created com location header
---
# 6. PatchAsync (Atualização Parcial)

[HttpPatch("{id:int}/UpdatePartial")]
public async Task<ActionResult<ProdutoDTOUpdateResponse>> PatchAsync(
    int id, 
    JsonPatchDocument<ProdutoDTOUpdateRequest> patchProdutoDTO)

Funcionalidade:
•	Atualização parcial usando JSON Patch
•	Permite atualizar apenas campos específicos
•	Valida dados após aplicar o patch
•	Se DataCadastro não for incluído, usa data atual
Endpoint: PATCH /api/produtos/5/UpdatePartial

Exemplo de Request Body:

[
  { "op": "replace", "path": "/Estoque", "value": 100 }
]

Operações suportadas:
•	add: Adicionar valor
•	remove: Remover valor
•	replace: Substituir valor
•	copy: Copiar valor
•	move: Mover valor
•	test: Testar valor
---
# 7. PutAsync

[HttpPut("{id:int}")]
public async Task<ActionResult<ProdutoDTO>> PutAsync(int id, ProdutoDTO produtoDTO)

Funcionalidade:
•	Atualização completa do produto
•	Valida se ID do parâmetro == ID do DTO
•	Retorna produto atualizado
Endpoint: PUT /api/produtos/5
---
# 8. DeleteAsync

[HttpDelete("{id:int}")]
public async Task<ActionResult<ProdutoDTO>> DeleteAsync(int id)

Funcionalidade:
•	Deleta produto por ID
•	Retorna produto deletado
Endpoint: DELETE /api/produtos/5
---
# 9. GetAsync (Paginação)

[HttpGet("pagination")]
public async Task<ActionResult<IEnumerable<ProdutoDTO>>> GetAsync([FromQuery] Parameters produtosParams)

Funcionalidade:
•	Retorna produtos paginados
•	Parâmetros: pageNumber, pageSize
•	Adiciona metadados de paginação no header X-Pagination
Endpoint: GET /api/produtos/pagination?pageNumber=1&pageSize=10

Header de Resposta:

X-Pagination: {
  "totalCount": 100,
  "pageSize": 10,
  "currentPage": 1,
  "totalPages": 10,
  "hasNext": true,
  "hasPrevious": false
}
---
# 10. GetProdutosFiltroPrecoAsync

[HttpGet("filter/preco/pagination")]
public async Task<ActionResult<IEnumerable<ProdutoDTO>>> GetProdutosFilterPrecoAsync(
    [FromQuery] ProdutosFiltroPreco produtosFilterParams)

Funcionalidade:
•	Filtra produtos por preço com critério
•	Critérios: "maior", "menor", "igual"
•	Com paginação
Endpoint: GET /api/produtos/filter/preco/pagination?preco=50&precoCriterio=maior&pageNumber=1&pageSize=10
---
# CategoriasController
Gerencia operações CRUD de categorias.
Rota Base: /api/categorias
Dependências:
•	ICategoriaRepository: Repositório específico (não usa Unit of Work)
•	ILogger: Logging

# Métodos:
# 1. GetAllAsync

[HttpGet]
[ServiceFilter(typeof(ApiLoggingFilter))]
public async Task<ActionResult<IEnumerable<CategoriaDTO>>> GetAllAsync()

Funcionalidade:
•	Retorna todas as categorias
•	Aplica filtro de logging (ApiLoggingFilter)
•	Usa extension methods para mapeamento
Endpoint: GET /api/categorias
---
# 2. GetAsync

[HttpGet("categorias_produtos")]
public async Task<ActionResult<IEnumerable<CategoriaDTO>>> GetCategoriasEProdutosAsync()

Funcionalidade:
•	Retorna categorias com produtos relacionados
•	Usa .Include(p => p.Produtos) do EF Core
•	Limitado a 20 categorias, 100 produtos
Endpoint: GET /api/categorias/categorias_produtos
---
# 4. PostAsync

[HttpPost]
public async Task<ActionResult<CategoriaDTO>> PostAsync(CategoriaDTO categoriaDTO)

Funcionalidade:
•	Cria nova categoria
•	Usa extension methods para conversão
Endpoint: POST /api/categorias
---
# 5. Patch (Atualização Parcial)

[HttpPatch("{id:int}/UpdatePartial")]
public async Task<ActionResult<CategoriaDTOUpdateResponse>> Patch(
    int id, 
    JsonPatchDocument<CategoriaDTOUpdateRequest> patchCategoriaDTO)

Funcionalidade:
•	Atualização parcial usando JSON Patch
•	Valida ModelState após aplicar patch
Endpoint: PATCH /api/categorias/3/UpdatePartial
---
# 6. PutAsync

[HttpPut("{id:int}")]
public async Task<ActionResult<CategoriaDTO>> PutAsync(int id, CategoriaDTO categoriaDTO)

Funcionalidade:
•	Atualização completa da categoria
Endpoint: PUT /api/categorias/3
---
# 7. DeleteAsync

[HttpDelete("{id:int}")]
public async Task<ActionResult<CategoriaDTO>> DeleteAsync(int id)

Funcionalidade:
•	Deleta categoria por ID
Endpoint: DELETE /api/categorias/3
---
# 8. Get (Paginação)

[HttpGet("pagination")]
public async Task<ActionResult<IEnumerable<CategoriaDTO>>> Get([FromQuery] Parameters categoriasParams)

Funcionalidade:
•	Paginação de categorias
Endpoint: GET /api/categorias/pagination?pageNumber=1&pageSize=10
---
# 9. GetCategoriaFilterNome

[HttpGet("filter/nome/pagination")]
public async Task<ActionResult<IEnumerable<CategoriaDTO>>> GetCategoriaFilterNome(
    [FromQuery] CategoriaFiltroNome categoriaFiltroParams)

Funcionalidade:
•	Filtra categorias por nome (case-insensitive)
•	Com paginação
Endpoint: GET /api/categorias/filter/nome/pagination?nome=Eletrônicos&pageNumber=1&pageSize=10
---
# 10. GetSaudacaoFromServices (Exemplo)

[HttpGet("filter/nome/pagination")]
public async Task<ActionResult<IEnumerable<CategoriaDTO>>> GetCategoriaFilterNome(
    [FromQuery] CategoriaFiltroNome categoriaFiltroParams)

Funcionalidade:
•	Exemplo de injeção de dependência via [FromServices]
•	Demonstra uso de serviços personalizados
---
# ClientesController
Gerencia operações CRUD de clientes.
Rota Base: /api/clientes
# Dependências:
•	IUnityOfWork: Acesso aos repositórios
•	IMapper: Mapeamento de DTOs
# Métodos (similar aos outros controllers):
1.	GetAllAsync: GET /api/clientes
2.	GetAsync: GET /api/clientes/{id}
3.	PostAsync: POST /api/clientes
4.	PatchAsync: PATCH /api/clientes/{id}/UpdatePartial
5.	PutAsync: PUT /api/clientes/{id}
6.	DeleteAsync: DELETE /api/clientes/{id}
7.	GetClientesAsync (Paginação): GET /api/clientes/pagination
---
## 🔐 Autenticação e Autorização
JWT (JSON Web Token)
O projeto usa autenticação baseada em JWT com suporte a Refresh Tokens.

# Configurações (appsettings.json):

"JWT": {
  "ValidAudience": "http://localhost:5179,https://localhost:7052",
  "ValidIssuer": "http://localhost:5179,https://localhost:7052",
  "SecretKey": "Minha@Super#Secreta&Chave*Privada!2023%",
  "TokenValidityInMinutes": 1,
  "RefreshTokenValidityInDays": 7
}
---
# TokenService (ITokenService)
Serviço responsável pela geração e validação de tokens.

# Métodos:
# 1. GenerateAccessToken

public JwtSecurityToken GenerateAccessToken(IEnumerable<Claim> claims, IConfiguration _config)

Funcionalidade:
•	Gera JWT access token
•	Configura claims do usuário
•	Define expiração (TokenValidityInMinutes)
•	Assina com chave secreta (HMAC-SHA256)

Estrutura do Token:

{
  "header": {
    "alg": "HS256",
    "typ": "JWT"
  },
  "payload": {
    "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name": "usuario",
    "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress": "email@exemplo.com",
    "http://schemas.microsoft.com/ws/2008/06/identity/claims/role": "Admin",
    "jti": "guid-unico",
    "exp": 1700000000,
    "iss": "http://localhost:5179",
    "aud": "http://localhost:5179"
  },
  "signature": "..."
}

---
# 2. GenerateRefreshToken


public ClaimsPrincipal GetPrincipalFromExpiredToken(string token, IConfiguration _config)

Funcionalidade:
•	Extrai claims de um token expirado
•	Não valida expiração (ValidateLifetime = false)
•	Valida assinatura e algoritmo
•	Retorna ClaimsPrincipal com informações do usuário
Uso: Necessário para refresh token flow
---
# Fluxo de Autenticação

sequenceDiagram
    participant Cliente
    participant API
    participant TokenService
    participant Database

    Cliente->>API: POST /api/auth/login (credentials)
    API->>Database: Valida usuário e senha
    Database-->>API: Usuário válido
    API->>Database: Busca roles do usuário
    Database-->>API: Roles
    API->>TokenService: GenerateAccessToken(claims)
    TokenService-->>API: JWT Access Token
    API->>TokenService: GenerateRefreshToken()
    TokenService-->>API: Refresh Token
    API->>Database: Salva refresh token no usuário
    API-->>Cliente: Access Token + Refresh Token

    Note over Cliente: Access Token expira

    Cliente->>API: POST /api/auth/refresh-token
    API->>TokenService: GetPrincipalFromExpiredToken(oldToken)
    TokenService-->>API: ClaimsPrincipal
    API->>Database: Valida refresh token
    Database-->>API: Token válido
    API->>TokenService: GenerateAccessToken(claims)
    TokenService-->>API: Novo Access Token
    API->>TokenService: GenerateRefreshToken()
    TokenService-->>API: Novo Refresh Token
    API->>Database: Atualiza refresh token
    API-->>Cliente: Novos tokens
___
# Configuração no Program.cs

// Autenticação JWT
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ClockSkew = TimeSpan.Zero,
        ValidAudience = builder.Configuration["JWT:ValidAudience"],
        ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
        IssuerSigningKey = new SymmetricSecurityKey(
            System.Text.Encoding.UTF8.GetBytes(secretkey))
    };
});

builder.Services.AddAuthorization();

---
# ASP.NET Core Identity
Gerencia usuários, senhas, roles e claims.

Configuração:

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

Tabelas Criadas (Migrations):
•	AspNetUsers: Usuários
•	AspNetRoles: Perfis/Roles
•	AspNetUserRoles: Relacionamento usuário-role
•	AspNetUserClaims: Claims dos usuários
•	AspNetUserLogins: Logins externos
•	AspNetUserTokens: Tokens de recuperação
•	AspNetRoleClaims: Claims das roles
---
## 🛡️ Filtros
# ApiExceptionFilter
Filtro global para tratamento de exceções não tratadas.

public class ApiExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        _logger.LogError(context.Exception, "Exceção não tratada: Status Code 500");
        
        context.Result = new ObjectResult("Problema ao tratar solicitação: Status 500")
        {
            StatusCode = StatusCodes.Status500InternalServerError,
        };
    }
}

Funcionalidade:
•	Intercepta todas as exceções não tratadas
•	Loga o erro
•	Retorna resposta padronizada 500

Registro:

builder.Services.AddControllers(options =>
{
    options.Filters.Add(typeof(ApiExceptionFilter));
});

--
# ApiLoggingFilter
Filtro de ação para logging de requisições.

public class ApiLoggingFilter : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        // Antes da execução da action
        _logger.LogInformation("### Executando -> OnActionExecuted");
        _logger.LogInformation($"Hora: {DateTime.Now.ToLongTimeString()}");
        _logger.LogInformation($"ModelState: {context.ModelState.IsValid}");
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        // Depois da execução da action
        _logger.LogInformation("### Executado -> OnActionExecuted");
        _logger.LogInformation($"Hora: {DateTime.Now.ToLongTimeString()}");
        _logger.LogInformation($"Status Code: {context.HttpContext.Response.StatusCode}");
    }
}

Funcionalidade:
•	Loga antes e depois da execução de uma action
•	Registra hora, ModelState e Status Code

Uso:

// Registro global
builder.Services.AddScoped<ApiLoggingFilter>();

// Uso em controller
[ServiceFilter(typeof(ApiLoggingFilter))]
public async Task<IActionResult> GetAllAsync()

## 📄 Paginação
# PagedList<T>
Classe genérica para resultados paginados.

public class PagedList<T> : List<T> where T : class
{
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
    public int PageSize { get; set; }
    public int TotalCount { get; set; }
    
    public bool HasPrevious => CurrentPage > 1;
    public bool HasNext => CurrentPage < TotalPages;
    
    public static PagedList<T> ToPagedList(IQueryable<T> source, int pageNumber, int pageSize)
    {
        var count = source.Count();
        var items = source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
        
        return new PagedList<T>(items, count, pageNumber, pageSize);
    }
}

Propriedades:
•	CurrentPage: Página atual
•	TotalPages: Total de páginas
•	PageSize: Itens por página
•	TotalCount: Total de itens
•	HasPrevious: Tem página anterior?
•	HasNext: Tem próxima página?
---
# Parameters
Classe base para parâmetros de paginação.

public class Parameters
{
    const int maxPageSize = 1000;
    
    public int PageNumber { get; set; } = 1;
    
    private int _pageSize = 10;
    public int PageSize
    {
        get { return _pageSize; }
        set { _pageSize = (value > maxPageSize || value == 0) ? maxPageSize : value; }
    }
}

Características:
•	Página padrão: 1
•	Tamanho padrão: 10
•	Tamanho máximo: 1000
---
# CategoriaFiltroNome
Parâmetros de filtro por nome para categorias.

public class CategoriaFiltroNome : Parameters
{
    public string? Nome { get; set; }
}

Uso: GET /api/categorias/filter/nome/pagination?nome=Eletrônicos&pageNumber=1&pageSize=10
---
# ProdutosFiltroPreco
Parâmetros de filtro por preço para produtos.

public class ProdutosFiltroPreco : Parameters
{
    public decimal? Preco { get; set; }
    public string? PrecoCriterio { get; set; } // "maior", "menor" ou "igual"
}

Uso: GET /api/produtos/filter/preco/pagination?preco=50&precoCriterio=maior&pageNumber=1&pageSize=10
---
Exemplo de Uso:

// No repositório
public async Task<PagedList<Produto>> GetProdutosAsync(Parameters produtosParams)
{
    var produtosOrdenados = (await GetAllAsync()).OrderBy(p => p.ProdutoId).AsQueryable();
    var produtosPaginados = PagedList<Produto>.ToPagedList(
        produtosOrdenados, 
        produtosParams.PageNumber, 
        produtosParams.PageSize
    );
    
    return produtosPaginados;
}

// No controller
var produtos = await _uof.ProdutoRepository.GetProdutosAsync(produtosParams);

var metadata = new
{
    produtos.TotalCount,
    produtos.PageSize,
    produtos.CurrentPage,
    produtos.TotalPages,
    produtos.HasNext,
    produtos.HasPrevious
};

Response.Headers.Append("X-Pagination", JsonConvert.SerializeObject(metadata));

---
## ✅ Validações
Validações Customizadas (Data Annotations)

# PrimeiraLetraMaiusculaAttribute
Valida se a primeira letra de uma string é maiúscula.

public class PrimeiraLetraMaiusculaAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value == null || string.IsNullOrEmpty(value.ToString()))
            return ValidationResult.Success;
        
        var primeiraLetra = value.ToString()[0].ToString();
        
        if (primeiraLetra != primeiraLetra.ToUpper())
            return new ValidationResult("A primeira letra deve ser maiúscula");
        
        return ValidationResult.Success;
    }
}

Uso:

[PrimeiraLetraMaiuscula]
public string? Nome { get; set; }

---
# PermissoesDeSexoAttribute
Valida se o sexo é "Masculino" ou "Feminino" (case-insensitive).

public class PermissoesDeSexoAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value is null) return ValidationResult.Success;
        
        var sexo = value.ToString();
        
        if (sexo.Equals("masculino", StringComparison.OrdinalIgnoreCase) ||
            sexo.Equals("feminino", StringComparison.OrdinalIgnoreCase))
            return ValidationResult.Success;
        
        return new ValidationResult("O campo sexo deve ser 'Masculino' ou 'Feminino'");
    }
}

Uso:

[PermissoesDeSexo]
public string? Sexo { get; set; }

---
# Validação com IValidatableObject
Permite validações complexas que envolvem múltiplas propriedades.

Exemplo no Modelo Produto:

public class Produto : IValidatableObject
{
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        // Validação da descrição
        if (!string.IsNullOrEmpty(this.Descricao))
        {
            var primeiraLetra = this.Descricao[0].ToString();
            if (primeiraLetra != primeiraLetra.ToUpper())
            {
                yield return new ValidationResult(
                    "A primeira letra deve ser maiúscula", 
                    new[] { nameof(this.Descricao) }
                );
            }
        }
        
        // Validação do estoque
        if (this.Estoque <= 0)
        {
            yield return new ValidationResult(
                "O estoque deve ser maior que 0", 
                new[] { nameof(this.Estoque) }
            );
        }
    }
}
---

Exemplo no DTO ProdutoDTOUpdateRequest:

public class ProdutoDTOUpdateRequest : IValidatableObject
{
    [Range(1, 9999)]
    public int Estoque { get; set; }
    
    public DateTime DataCadastro { get; set; }
    
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (DataCadastro <= DateTime.Now.Date)
        {
            yield return new ValidationResult(
                "A data deve ser maior que a data atual", 
                new[] { nameof(this.DataCadastro) }
            );
        }
    }
}

---
Validações Padrão do ASP.NET Core
Atributos Comuns:
•	[Required]: Campo obrigatório
•	[StringLength(max, MinimumLength = min)]: Tamanho da string
•	[Range(min, max)]: Intervalo numérico
•	[EmailAddress]: Valida formato de email
•	[RegularExpression(pattern)]: Expressão regular
•	[Compare("property")]: Compara com outra propriedade
•	[Url]: Valida URL
•	[Phone]: Valida telefone
•	[CreditCard]: Valida cartão de crédito
---
## 📝 Logging
Sistema de Logging Customizado
O projeto implementa um sistema de logging que grava logs em arquivo.

# CustomLogger
Logger que escreve em arquivo de texto.

public class CustomLogger : ILogger
{
    readonly string loggerName;
    readonly CustomLoggerProviderConfiguration loggerConfig;
    
    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, 
        Exception? exception, Func<TState, Exception?, string> formatter)
    {
        string mensagem = $"{logLevel.ToString()}: {eventId.Id} - {formatter(state, exception)}";
        EscreverTextoNoArquivo(mensagem);
    }
    
    private void EscreverTextoNoArquivo(string mensagem)
    {
        string caminhoArquivo = @"C:\...\Logging\Logs\Executions_Log.txt";
        // Escreve no arquivo
    }
}
---
# CustomLoggerProvider
Provider que gerencia instâncias de CustomLogger.

public class CustomLoggerProvider : ILoggerProvider
{
    readonly CustomLoggerProviderConfiguration loggerConfig;
    readonly ConcurrentDictionary<string, CustomLogger> loggers = 
        new ConcurrentDictionary<string, CustomLogger>();
    
    public ILogger CreateLogger(string categoryName)
    {
        return loggers.GetOrAdd(categoryName, name => new CustomLogger(name, loggerConfig));
    }
}

Características:
•	Usa ConcurrentDictionary para thread-safety
•	Cache de loggers por categoria
---
# CustomLoggerProviderConfiguration
Configuração do logger.

public class CustomLoggerProviderConfiguration
{
    public LogLevel LogLevel { get; set; } = LogLevel.Information;
    public int EventId { get; set; } = 0;
}
---
Configuração no Program.cs:

builder.Logging.AddProvider(new CustomLoggerProvider(new CustomLoggerProviderConfiguration
{
    LogLevel = LogLevel.Information
}));

---
Exemplo de Log Gerado:

Information: 14 - Now listening on: https://localhost:7052
Information: 14 - Now listening on: http://localhost:5179
Information: 0 - Application started. Press Ctrl+C to shut down.
Information: 20101 - Executed DbCommand (41ms) [Parameters=[], ...]
Warning: 0 - You do not have a valid license key for AutoMapper...

---
## 🔧 Middlewares
# ApiExceptionMiddlewareExtensions
Extension method para configurar middleware global de exceções.

public static class ApiExceptionMiddlewareExtensions
{
    public static void ConfigureExceptionHandler(this IApplicationBuilder app)
    {
        app.UseExceptionHandler(appError =>
        {
            appError.Run(async context =>
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";
                
                var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                
                if (contextFeature != null)
                {
                    await context.Response.WriteAsync(new ErrorDetails()
                    {
                        StatusCode = context.Response.StatusCode,
                        Message = contextFeature.Error.Message,
                        Trace = contextFeature.Error.StackTrace
                    }.ToString());
                }
            });
        });
    }
}

Funcionalidade:
•	Captura exceções não tratadas
•	Retorna JSON com detalhes do erro
•	Inclui stack trace (útil em desenvolvimento)
Uso:

if (app.Environment.IsDevelopment())
{
    app.ConfigureExceptionHandler();
}

---
# Pipeline de Middlewares (Program.cs):

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.ConfigureExceptionHandler();  // Middleware customizado
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

Ordem de Execução:
1.	Exception Handler (em dev)
2.	HTTPS Redirection
3.	Authorization
4.	Controllers
---
## ⚙️ Configurações
# appsettings.json

{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=CatalogoDB;Uid=root;Pwd=root"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "JWT": {
    "ValidAudience": "http://localhost:5179,https://localhost:7052",
    "ValidIssuer": "http://localhost:5179,https://localhost:7052",
    "SecretKey": "Minha@Super#Secreta&Chave*Privada!2023%",
    "TokenValidityInMinutes": 1,
    "RefreshTokenValidityInDays": 7
  },
  "AllowedHosts": "*"
}

---
# Program.cs - Configuração Completa
# 1. Controllers e JSON

builder.Services.AddControllers(options =>
{
    options.Filters.Add(typeof(ApiExceptionFilter));  // Filtro global
})
.AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
})
.AddNewtonsoftJson();  // Suporte ao JSON Patch

---
# 2. Repositórios

builder.Services.AddScoped<ICategoriaRepository, CategoriaRepository>();
builder.Services.AddScoped<IProdutoRepository, ProdutoRepository>();
builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));  // Genérico
builder.Services.AddScoped<IUnityOfWork, UnityOfWork>();

---
# 3. Serviços

builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddTransient<IMeuServico, MeuServico>();
builder.Services.AddScoped<ApiLoggingFilter>();

---
# 4. Swagger

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "apicatalogo", Version = "v1" });
    
    // Configuração de autenticação JWT no Swagger
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Bearer JWT",
    });
    
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

---
# 5. Banco de Dados

string mySqlConnection = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(mySqlConnection, ServerVersion.AutoDetect(mySqlConnection)));

---
# 6. Identity

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

---
# 7. Autenticação JWT

var secretkey = builder.Configuration["JWT:SecretKey"] ?? 
    throw new ArgumentException("invalid secret key");

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ClockSkew = TimeSpan.Zero,
        ValidAudience = builder.Configuration["JWT:ValidAudience"],
        ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
        IssuerSigningKey = new SymmetricSecurityKey(
            System.Text.Encoding.UTF8.GetBytes(secretkey))
    };
});

---
# 8. AutoMapper

builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<AutoMapperDTOMappingProfile>();
});

---
# 9. Logging Customizado

builder.Logging.AddProvider(new CustomLoggerProvider(new CustomLoggerProviderConfiguration
{
    LogLevel = LogLevel.Information
}));

---
## 🌐 Endpoints da API
Resumo de Todos os Endpoints

# Autenticação (/api/auth)
Método	Endpoint	Descrição	Auth
POST	/login	Login do usuário	❌
POST	/register	Registro de novo usuário	❌
POST	/refresh-token	Renovar access token	❌
POST	/revoke/{username}	Revogar refresh token (logout)	✅
---
# Produtos (/api/produtos)
Método	Endpoint	Descrição	Auth
GET	/	Listar todos os produtos	✅
GET	/primeiro	Primeiro produto	❌
GET	/{id}	Buscar produto por ID	❌
GET	/porcategoria/{id}	Produtos por categoria	❌
GET	/pagination	Produtos paginados	❌
GET	/filter/preco/pagination	Filtro de preço + paginação	❌
| POST | `/` | Criar novo produto | ❌ |
| PATCH | `/{id}/UpdatePartial` | Atualização parcial | ❌ |
| PUT | `/{id}` | Atualizar produto | ❌ |
| DELETE | `/{id}` | Deletar produto | ❌ |

---

#### **Categorias** (`/api/categorias`)

| Método | Endpoint | Descrição | Auth |
|--------|----------|-----------|------|
| GET | `/` | Listar todas as categorias | ❌ |
| GET | `/{id}` | Buscar categoria por ID | ❌ |
| GET | `/categorias_produtos` | Categorias com produtos | ❌ |
| GET | `/pagination` | Categorias paginadas | ❌ |
| GET | `/filter/nome/pagination` | Filtro por nome + paginação | ❌ |
| POST | `/` | Criar nova categoria | ❌ |
| PATCH | `/{id}/UpdatePartial` | Atualização parcial | ❌ |
| PUT | `/{id}` | Atualizar categoria | ❌ |
| DELETE | `/{id}` | Deletar categoria | ❌ |

---

#### **Clientes** (`/api/clientes`)

| Método | Endpoint | Descrição | Auth |
|--------|----------|-----------|------|
| GET | `/` | Listar todos os clientes | ❌ |
| GET | `/{id}` | Buscar cliente por ID | ❌ |
| GET | `/pagination` | Clientes paginados | ❌ |
| POST | `/` | Criar novo cliente | ❌ |
| PATCH | `/{id}/UpdatePartial` | Atualização parcial | ❌ |
| PUT | `/{id}` | Atualizar cliente | ❌ |
| DELETE | `/{id}` | Deletar cliente | ❌ |

---

## 📋 Exemplos de Requisições

### **Autenticação**

#### 1. Registro de Usuário