## Fonte de Dados (Model Binding) em APIs ASP.NET Core

O Model Binding do ASP.NET Core converte automaticamente dados da requisição HTTP em parâmetros dos métodos do controller. Veja exemplos práticos e descrições claras para cada fonte de dados:

---

### 1. Query Strings

| Exemplo de URL                                      | Estrutura no Controller                                 | Descrição                                                                                   |
|-----------------------------------------------------|---------------------------------------------------------|---------------------------------------------------------------------------------------------|
| `/api/produtos?nome=refri`                          | `public IActionResult Get(string nome)`                 | O valor de `nome` na URL é passado como parâmetro para o método.                            |
| `/api/produtos?nome=refri&estoque=10`               | `public IActionResult Get(string nome, int estoque)`    | Vários parâmetros na URL são mapeados para parâmetros do método.                            |
| `/api/produtos?filtro.nome=refri&filtro.preco=5.45` | `public IActionResult Get([FromQuery] Filtro filtro)`   | Parâmetros com prefixo são mapeados para propriedades de um objeto complexo.                |

---

### 2. Route Data

| Exemplo de URL                  | Estrutura no Controller                                                                               | Descrição                                                                                   |
|---------------------------------|-------------------------------------------------------------------------------------------------------|---------------------------------------------------------------------------------------------|
| `/api/produtos/5`               | `[HttpGet("{id}")] public IActionResult Get(int id)`                                                  | O valor `5` na URL é passado como parâmetro `id` para o método.                             |
| `/api/categorias/10/produtos`   | `[HttpGet("categorias/{categoriaId}/produtos")] public IActionResult GetByCategoria(int categoriaId)` | O valor `10` na URL é passado como parâmetro `categoriaId` para o método.                   |
| `/api/produtos/5/detalhes/2025` | `[HttpGet("{id}/detalhes/{ano}")] public IActionResult GetDetalhes(int id, int ano)`                  | Vários valores na URL são mapeados para múltiplos parâmetros do método.                     |

---

### 3. Body (JSON, XML, etc.)

| Exemplo de Body (JSON)                                                                 | Estrutura no Controller                                        | Descrição                                                                                   |
|----------------------------------------------------------------------------------------|----------------------------------------------------------------|---------------------------------------------------------------------------------------------|
| `{ "nome": "Coca-Cola", "preco": 5.45 }`                                               | `public IActionResult Post([FromBody] Produto produto)`        | O JSON enviado no corpo da requisição é convertido em um objeto `Produto`.                  |
| `[ { "nome": "Coca", "preco": 5 }, { "nome": "Fanta", "preco": 6 } ]`                  | `public IActionResult Post([FromBody] List<Produto> produtos)` | Um array JSON no corpo é convertido em uma lista de objetos.                                |
| `{ "filtro": { "nome": "Refri", "preco": 5.45 } }`                                     | `public IActionResult Post([FromBody] FiltroWrapper filtro)`   | Um objeto JSON aninhado é convertido em um objeto complexo do tipo `FiltroWrapper`.         |

---

### 4. Form Data

| Exemplo de Body (Form)                        | Estrutura no Controller                                                                  | Descrição                                                                                   |
|-----------------------------------------------|------------------------------------------------------------------------------------------|---------------------------------------------------------------------------------------------|
| `nome=Coca-Cola&preco=5.45`                   | `public IActionResult Post([FromForm] Produto produto)`                                  | Dados enviados como formulário são convertidos em um objeto `Produto`.                      |
| (upload de arquivo + campos)                  | `public IActionResult Upload([FromForm] IFormFile arquivo, [FromForm] string descricao)` | Um arquivo e um campo de texto enviados juntos são recebidos como parâmetros separados.     |
| (vários arquivos)                             | `public IActionResult Upload([FromForm] List<IFormFile> arquivos)`                       | Vários arquivos enviados no formulário são recebidos como uma lista de arquivos.            |

---

### 5. Header

| Exemplo de Header                        | Estrutura no Controller                                                        | Descrição                                                                                   |
|------------------------------------------|--------------------------------------------------------------------------------|---------------------------------------------------------------------------------------------|
| `X-Api-Key: 12345`                       | `public IActionResult Get([FromHeader] string xApiKey)`                        | O valor do header `X-Api-Key` é passado como parâmetro para o método.                       |
| `User-Agent: PostmanRuntime/7.28.4`      | `public IActionResult Get([FromHeader(Name = "User-Agent")] string userAgent)` | O valor do header padrão `User-Agent` é recebido como parâmetro.                            |
| `Authorization: Bearer <token>`          | `public IActionResult Get([FromHeader] string authorization)`                  | O valor do header `Authorization` é recebido como parâmetro.                                |

---

### 6. Services (Injeção de Dependência)

| Exemplo de Uso                             | Estrutura no Controller                                        | Descrição                                                                                   |
|--------------------------------------------|----------------------------------------------------------------|---------------------------------------------------------------------------------------------|
| Injetar serviço no construtor              | `public ProdutosController(IMeuServico servico)`               | O serviço é fornecido automaticamente pelo sistema de injeção de dependência.               |
| Injetar serviço em método                  | `public IActionResult Get([FromServices] IMeuServico servico)` | O serviço é injetado diretamente como parâmetro do método.                                  |
| Injetar contexto de dados                  | `public ProdutosController(AppDbContext context)`              | O contexto do banco de dados é injetado no construtor do controller.                        |

---

### 7. Arquivos (File Upload)

| Exemplo de Uso                            | Estrutura no Controller                                                                  | Descrição                                                                                   |
|--------------------------------------------|-----------------------------------------------------------------------------------------|---------------------------------------------------------------------------------------------|
| Upload de arquivo                         | `public IActionResult Upload([FromForm] IFormFile arquivo)`                              | Um arquivo enviado via formulário é recebido como parâmetro do tipo `IFormFile`.            |
| Upload de múltiplos arquivos              | `public IActionResult Upload([FromForm] List<IFormFile> arquivos)`                       | Vários arquivos enviados são recebidos como uma lista de arquivos.                          |
| Upload de arquivo + dados                 | `public IActionResult Upload([FromForm] IFormFile arquivo, [FromForm] string descricao)` | Um arquivo e um campo de texto enviados juntos são recebidos como parâmetros separados.     |

---

### 8. Cookie

| Exemplo de Cookie                         | Estrutura no Controller                                               | Descrição                                                                                   |
|-------------------------------------------|-----------------------------------------------------------------------|---------------------------------------------------------------------------------------------|
| `Cookie: sessionId=abc123`                | `public IActionResult Get([FromCookie] string sessionId)`             | O valor do cookie `sessionId` é recebido como parâmetro.                                    |
| `Cookie: theme=dark; sessionId=abc123`    | `public IActionResult Get([FromCookie(Name = "theme")] string theme)` | O valor do cookie `theme` é recebido como parâmetro nomeado.                                |
| Lendo cookie manualmente                  | `Request.Cookies["sessionId"]`                                        | O valor do cookie é acessado manualmente pela coleção de cookies da requisição.             |

---

## Principais Atributos de Model Binding

| Atributo         | Descrição                                                                                   |
|------------------|---------------------------------------------------------------------------------------------|
| `[FromQuery]`    | Obtém o valor do parâmetro a partir da query string da URL (ex: `?nome=valor`).             |
| `[FromRoute]`    | Obtém o valor do parâmetro a partir dos dados da rota definidos na URL.                     |
| `[FromBody]`     | Obtém e desserializa o valor do parâmetro a partir do corpo da requisição (geralmente JSON).|
| `[FromForm]`     | Obtém o valor do parâmetro a partir dos dados enviados via formulário (form-data).          |
| `[FromHeader]`   | Obtém o valor do parâmetro a partir de um header HTTP específico da requisição.             |
| `[FromServices]` | Injeta uma instância de serviço registrado no container de dependência no parâmetro.        |

---

**Resumo:**  
Cada fonte de dados permite que informações da requisição HTTP sejam automaticamente convertidas em parâmetros do método, facilitando o desenvolvimento de APIs limpas e seguras.