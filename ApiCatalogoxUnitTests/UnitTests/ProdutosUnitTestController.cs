using APICatalogo.Context;
using APICatalogo.DTOs.Mappings;
using APICatalogo.Repositories;
using APICatalogo.Repositories.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ApiCatalogoxUnitTests.UnitTests;

public class ProdutosUnitTestController
{
    public IUnityOfWork repository;
    public IMapper mapper;
    public ILogger<APICatalogo.Controllers.ProdutosController> logger;
    public static DbContextOptions<AppDbContext> dbContextOptions { get; }

    private static readonly string connectionString = "Server=localhost;Database=CatalogoDB;Uid=root;Pwd=root";

    static ProdutosUnitTestController()
    {
        dbContextOptions = new DbContextOptionsBuilder<AppDbContext>()
            .UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
            .Options;
    }

    public ProdutosUnitTestController()
    {
        // AutoMapper 15 - Criar ILoggerFactory
        var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());

        // Criar o logger a partir do loggerFactory
        logger = loggerFactory.CreateLogger<APICatalogo.Controllers.ProdutosController>();

        var configExpression = new MapperConfigurationExpression();
        configExpression.AddProfile<AutoMapperDTOMappingProfile>();

        var config = new MapperConfiguration(configExpression, loggerFactory);

        mapper = config.CreateMapper();
        var context = new AppDbContext(dbContextOptions);
        repository = new UnityOfWork(context);
    }
}
