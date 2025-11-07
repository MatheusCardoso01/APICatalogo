# Códigos de Status HTTP em APIs ASP.NET Core

## Status Comuns

### Respostas de Sucesso

| Método      | Código | Descrição                          |
|-------------|--------|------------------------------------|
| Ok()        | 200    | Sucesso, retorna dados             |
| Created()   | 201    | Recurso criado com sucesso         |
| Accepted()  | 202    | Requisição aceita para processamento|
| NoContent() | 204    | Sucesso, sem conteúdo para retornar|

### Respostas de Erro

| Método           | Código | Descrição                        |
|------------------|--------|----------------------------------|
| BadRequest()     | 400    | Requisição inválida              |
| Unauthorized()   | 401    | Não autenticado                  |
| Forbid()         | 403    | Proibido (sem permissão)         |
| NotFound()       | 404    | Recurso não encontrado           |
| Conflict()       | 409    | Conflito (ex: registro duplicado)|
| UnprocessableEntity() | 422 | Entidade não processável        |

## Status Menos Comuns

| Método                | Código | Descrição                                 |
|-----------------------|--------|-------------------------------------------|
| MovedPermanently()    | 301    | Recurso movido permanentemente            |
| Found()               | 302    | Redirecionamento temporário               |
| SeeOther()            | 303    | Veja outro recurso                        |
| NotModified()         | 304    | Não modificado                            |
| TemporaryRedirect()   | 307    | Redirecionamento temporário               |
| PermanentRedirect()   | 308    | Redirecionamento permanente               |
| InternalServerError() | 500    | Erro interno do servidor                  |
| NotImplemented()      | 501    | Funcionalidade não implementada           |
| BadGateway()          | 502    | Gateway inválido                          |
| ServiceUnavailable()  | 503    | Serviço indisponível                      |
| GatewayTimeout()      | 504    | Tempo de resposta do gateway esgotado     |
| StatusCode(xxx)       | xxx    | Retorna qualquer código de status desejado |
