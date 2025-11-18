## JWT Auth em .NET

# Comandos Principais do dotnet user-jwts

| Comando                                 | Descrição                                                                                   |
|------------------------------------------|---------------------------------------------------------------------------------------------|
| `dotnet user-jwts create`                | Cria um novo token JWT de desenvolvimento para autenticação local.                          |
| `dotnet user-jwts list`                  | Lista todos os tokens JWT de desenvolvimento criados para o projeto atual.                  |
| `dotnet user-jwts delete <id>`           | Remove um token JWT específico pelo seu identificador.                                      |
| `dotnet user-jwts clear`                 | Remove todos os tokens JWT de desenvolvimento do projeto atual.                             |
| `dotnet user-jwts print <id>`            | Exibe o token JWT e suas informações detalhadas pelo identificador.                         |
| `dotnet user-jwts update <id>`           | Atualiza as informações de um token JWT existente, como claims ou tempo de expiração.       |
| `dotnet user-jwts`                       | Exibe a ajuda e os comandos disponíveis para gerenciamento de JWTs de desenvolvimento.      |

---

## O que é Autenticação (Auth)?

Autenticação verifica a identidade do usuário, dispositivo ou sistema antes de permitir acesso a recursos protegidos. Pode envolver senha, token, biometria ou múltiplos fatores. Em APIs, normalmente é feita via token.

- **Senha:** Algo que o usuário sabe, como uma palavra-chave ou PIN.
- **Token:** Um código gerado e enviado ao usuário, geralmente temporário, usado para validar a identidade.
- **Biometria:** Algo que o usuário é, como impressão digital, reconhecimento facial ou de voz.
- **Múltiplos fatores:** Combinação de dois ou mais métodos acima, aumentando a segurança (ex: senha + código SMS).

## O que é Autorização?

Autorização define o que um usuário autenticado pode acessar ou fazer. Pode ser baseada em roles (funções), claims (declarações) ou policies (políticas). No ASP.NET Core, é comum usar `[Authorize]` para proteger endpoints.

- **Roles (Funções):** Permissões agrupadas por função, como "Admin" ou "Usuário".
- **Claims (Declarações):** Informações específicas sobre o usuário, como departamento ou nível de acesso.
- **Policies (Políticas):** Regras customizadas que combinam roles, claims e outras condições para autorizar ações.

## O que é JWT (JSON Web Token)?

JWT é um padrão para transmitir informações seguras entre partes como um objeto JSON assinado digitalmente. É composto por header, payload (claims) e assinatura. Usado para autenticação e autorização em APIs, 
é stateless e tem tempo de expiração. Nunca armazene dados sensíveis no payload, pois ele é apenas codificado. No .NET, a ferramenta `dotnet user-jwts` facilita a criação e gerenciamento de tokens JWT para testes locais.

- **Stateless:** O servidor não armazena informações de sessão; todos os dados necessários estão no próprio token, tornando o sistema mais escalável e simples de manter