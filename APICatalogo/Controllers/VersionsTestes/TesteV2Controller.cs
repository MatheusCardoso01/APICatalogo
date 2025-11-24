using Asp.Versioning;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APICatalogo.Controllers.VersionsTestes;

[ApiExplorerSettings(IgnoreApi = true)] // Ignorar esta versão da API na documentação do Swagger
[Route("api/v{version:apiVersion}/teste")]
[ApiController]
[ApiVersion("2.0")]
public class TesteV2Controller : ControllerBase
{
    [HttpGet]
    public string GetVersion()
    {
        return "TesteV2 - GET - Api Versão 2.0";
    }
}
