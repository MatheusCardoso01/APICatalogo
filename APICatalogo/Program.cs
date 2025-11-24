using APICatalogo.Context;
using APICatalogo.DTOs.Mappings;
using APICatalogo.Extensions;
using APICatalogo.Filters;
using APICatalogo.Logging;
using APICatalogo.Models;
using APICatalogo.RateLimit;
using APICatalogo.Repositories;
using APICatalogo.Repositories.Interfaces;
using APICatalogo.Services;
using Asp.Versioning;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text.Json.Serialization;
using System.Threading.RateLimiting;


// 0. Configuração do Builder
var builder = WebApplication.CreateBuilder(args);

// 1. Controllers e Filtros Globais
builder.Services.AddControllers(options =>
{
    options.Filters.Add(typeof(ApiExceptionFilter)); // Filtro global de exceções
})          
.AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
})
.AddNewtonsoftJson();

// 2. Banco de Dados
string mySqlConnection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(mySqlConnection, ServerVersion.AutoDetect(mySqlConnection)));

// 3. Repositórios e Unit of Work
builder.Services.AddScoped<ICategoriaRepository, CategoriaRepository>();
builder.Services.AddScoped<IProdutoRepository, ProdutoRepository>();
builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IUnityOfWork, UnityOfWork>();
builder.Services.AddScoped<ITokenService, TokenService>();

// 4. Serviços Personalizados
builder.Services.AddTransient<IMeuServico, MeuServico>();

// 5. AutoMapper
builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<AutoMapperDTOMappingProfile>();
});

// 6. Identity
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

// 7. JWT (Autenticação e Autorização via JWT)
var secretkey = builder.Configuration["JWT:SecretKey"] ?? throw new ArgumentException("Chave secreta inválida");

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
        IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(secretkey))
    };
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
    options.AddPolicy("SuperAdminOnly", policy => policy.RequireRole("Admin").RequireClaim("id", "matheus"));
    options.AddPolicy("UserOnly", policy => policy.RequireRole("User"));
    options.AddPolicy("ExclusiveOnly", policy => policy.RequireAssertion(context =>
        context.User.HasClaim(claim => claim.Type == "id" && claim.Value == "matheus") || context.User.IsInRole("SuperAdmin")));
});

// 8. CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("OrigensComAcessoPermitido", policy =>
    {
        policy.WithOrigins("https://localhost:7169")
              .WithMethods("GET", "POST")
              .AllowAnyHeader();
    });
});

// 9. Rate Limiting
var myOptions = new MyRateLimitOptions();
builder.Configuration.GetSection(MyRateLimitOptions.MyRateLimit).Bind(myOptions);

// Limitação por política
builder.Services.AddRateLimiter(options =>
{
    options.AddFixedWindowLimiter(policyName: "fixedwindow", fixedWindowOptions =>
    {
        fixedWindowOptions.PermitLimit = myOptions.PermitLimit;
        fixedWindowOptions.Window = TimeSpan.FromSeconds(myOptions.Window);
        fixedWindowOptions.QueueLimit = myOptions.QueueLimit;
        fixedWindowOptions.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
    });
    options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
});

// Limitação global
builder.Services.AddRateLimiter(options =>
{
    options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
    options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(httpcontext =>
        RateLimitPartition.GetFixedWindowLimiter(
            partitionKey: httpcontext.User.Identity?.Name ?? httpcontext.Request.Headers.Host.ToString(),
            factory: partition => new FixedWindowRateLimiterOptions
            {
                AutoReplenishment = true,
                PermitLimit = 2,
                QueueLimit = 0,
                Window = TimeSpan.FromSeconds(10)
            }));
});

// 10. Versionamento de API
builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ReportApiVersions = true;
    options.ApiVersionReader = ApiVersionReader.Combine(new QueryStringApiVersionReader(), new UrlSegmentApiVersionReader());
})
.AddApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV";
    options.SubstituteApiVersionInUrl = true;
});

// 11. Logging (Filtro de Logging customizado)
builder.Services.AddScoped<ApiLoggingFilter>();

// 12. Logging Customizado (Provedor de Logging customizado)
builder.Logging.AddProvider(new CustomLoggerProvider(new CustomLoggerProviderConfiguration
{
    LogLevel = LogLevel.Information
}));

// 13. Swagger/OpenAPI (Documentação e Segurança JWT)
builder.Services.AddEndpointsApiExplorer(); // Necessário para Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "APICatalogo",
        Description = "Catálogo de Produtos e Categorias",
        TermsOfService = new Uri("https://algumenderecoapi.com/terms"),
        Contact = new OpenApiContact
        {
            Name = "Matheus Cardoso",
            Email = "matheusfake@email.com",
            Url = new Uri("https://matheuscardoso.com.br"),
        },
        License = new OpenApiLicense
        {
            Name = "Usar sobre LICX exemplo",
            Url = new Uri("https://algumenderecoapi.com/license"),
        }
    });

    // Necessário para acessar endpoints com autenticação JWT via Swagger
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

// 14. Construção do App
var app = builder.Build();

// Middleware para ambiente de desenvolvimento
if (app.Environment.IsDevelopment())
{
    app.ConfigureExceptionHandler(); // Middleware de tratamento de exceções
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "APICatalogo v1");
    });
}

// 15. Middlewares comuns

app.UseHttpsRedirection();
app.UseRouting();

app.UseRateLimiter(); // Sempre após o routing

app.UseCors("OrigensComAcessoPermitido");

app.UseAuthorization();

app.MapControllers();

app.Run();