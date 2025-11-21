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
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;
using System.Threading.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Add Controllers

builder.Services.AddControllers(options =>
{
    options.Filters.Add(typeof(ApiExceptionFilter));
})
.AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
}).AddNewtonsoftJson();

// Add CORS (Cross-Origin Resource Sharing)

builder.Services.AddCors(options =>
{
    options.AddPolicy("OrigensComAcessoPermitido", policy =>
    {
        policy.WithOrigins("https://localhost:7169") // sem a barra '/' no fim, se nao falha
            .WithMethods("GET", "POST")
            .AllowAnyHeader();
    });
});

// Add global filter de logging

builder.Services.AddScoped<ApiLoggingFilter>();

// Add Repositories personalizados

builder.Services.AddScoped<ICategoriaRepository, CategoriaRepository>();
builder.Services.AddScoped<IProdutoRepository, ProdutoRepository>();
builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IUnityOfWork, UnityOfWork>(); // Unit of Work(.NET only)
builder.Services.AddScoped<ITokenService, TokenService>();

// Add Services personalizados

builder.Services.AddTransient<IMeuServico, MeuServico>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddEndpointsApiExplorer(); // obrigatório para usar swaggergen

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "apicatalogo", Version = "v1" });

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

string mySqlConnection = builder.Configuration.GetConnectionString("DefaultConnection"); // DefaultConnection é o nome da connection string no appsettings.json

// acessar valor de 'appsettings.json': var valor1 = builder.Configuration["chave1"];

builder.Services.AddDbContext<AppDbContext>(options =>
                    options.UseMySql(mySqlConnection,
                    ServerVersion.AutoDetect(mySqlConnection)));

// Add Services Identity

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                    .AddEntityFrameworkStores<AppDbContext>()
                    .AddDefaultTokenProviders();

// Autenticação e Autorização com JWT (JSON Web Tokens)

var secretkey = builder.Configuration["JWT:SecretKey"] ?? throw new ArgumentException("invalid secret key");

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
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

builder.Logging.AddProvider(new CustomLoggerProvider(new CustomLoggerProviderConfiguration
{
    LogLevel = LogLevel.Information
}));

// Add Rate Limiting

var myOptions = new MyRateLimitOptions();

builder.Configuration.GetSection(MyRateLimitOptions.MyRateLimit).Bind(myOptions);

builder.Services.AddRateLimiter(options => // Exemplo de Rate Limiter por política
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

builder.Services.AddRateLimiter(options => // Exemplo de Rate Limiter Global
{
    options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;

    options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(httpcontext =>
        RateLimitPartition.GetFixedWindowLimiter(partitionKey: httpcontext.User.Identity?.Name ?? httpcontext.Request.Headers.Host.ToString(),
        factory: partition => new FixedWindowRateLimiterOptions
        {
            AutoReplenishment = true,
            PermitLimit = 2,
            QueueLimit = 0,
            Window = TimeSpan.FromSeconds(10)
        }));
});

// Add AutoMapper para mapeamento de DTOs

builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<AutoMapperDTOMappingProfile>();
});

// registra o perfil de mapeamento

var app = builder.Build();

// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.ConfigureExceptionHandler(); // middleware de tratamento de exceções
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseRateLimiter(); // sempre depois do routing

app.UseCors("OrigensComAcessoPermitido");

app.UseAuthorization();

// Mapeia Atributos dos Controllers Existentes

app.MapControllers();

app.Run();
