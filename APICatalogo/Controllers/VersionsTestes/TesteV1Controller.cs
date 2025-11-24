using Asp.Versioning;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APICatalogo.Controllers.VersionsTestes;

[ApiExplorerSettings(IgnoreApi = true)] // Ignorar esta versão da API na documentação do Swagger
[Route("api/v{version:apiVersion}/teste")]
[ApiController]
[ApiVersion("1.0", Deprecated = true)]
public class TesteV1Controller : ControllerBase
{
    [HttpGet]
    public string GetVersion()
    {
        return "TesteV1 - Get - Api Versão 1.0";
    }
}
