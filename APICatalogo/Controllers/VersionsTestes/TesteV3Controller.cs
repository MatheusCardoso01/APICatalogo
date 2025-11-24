using Asp.Versioning;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APICatalogo.Controllers.VersionsTestes;


[ApiExplorerSettings(IgnoreApi = true)] // Ignorar esta versão da API na documentação do Swagger
[Route("api/teste")]
[ApiController]
[ApiVersion(3)]
[ApiVersion(4)]
public class TesteV3Controller : ControllerBase
{
    [HttpGet]
    [MapToApiVersion(3)]
    public string GetVersionV3()
    {
        return "V3 - GET - Api Versão 3.0";
    }

    [HttpGet]
    [MapToApiVersion(4)]
    public string GetVersionV4()
    {
        return "V4 - GET - Api Versão 4.0";
    }
}
