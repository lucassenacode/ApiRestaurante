using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Org.BouncyCastle.Asn1.Cms;

namespace ApiRestaurante;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Configuração dos controladores e JSON
        builder.Services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
                options.JsonSerializerOptions.PropertyNamingPolicy = null;
            });

        // Configuração do CORS
        builder.Services.AddCors(options =>
        {
            options.AddDefaultPolicy(policy =>
            {
                policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
            });
        });

        // Configuração do Swagger
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "ApiRestaurante", Version = "v1" });

            // Configuração de segurança para o botão "Authorize"
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Insira o token JWT assim: Bearer {seu_token}",
                Name = "Authorization",
                Type = SecuritySchemeType.Http, // Alterado para Http
                Scheme = "Bearer" // Adicionado o scheme Bearer
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
            },
            new List<string>()
        }
            });
        });

        // Configuração do Singleton para IConfiguration
        builder.Services.AddSingleton<IConfiguration>(builder.Configuration);

        // Registro dos repositórios
        builder.Services.AddScoped<Repositories.Repository.IProdutoRepository, Repositories.Repository.ProdutoRepository>();
        builder.Services.AddScoped<Repositories.Repository.IPedidoRepository, Repositories.Repository.PedidoRepository>();
        builder.Services.AddScoped<Repositories.Repository.IItemPedidoRepository, Repositories.Repository.ItemPedidoRepository>();
        builder.Services.AddScoped<Repositories.Repository.IUsuarioRepository, Repositories.Repository.UsuarioRepository>();

        // Registro dos serviços
        builder.Services.AddScoped<Services.Service.IProdutoService, Services.Service.ProdutoService>();
        builder.Services.AddScoped<Services.Service.IPedidoService, Services.Service.PedidoService>();
        builder.Services.AddScoped<Services.Service.IItemPedidoService, Services.Service.ItemPedidoService>();
        builder.Services.AddScoped<Services.Service.IUsuarioService, Services.Service.UsuarioService>();
        builder.Services.AddScoped<Services.Service.AutorizacaoService>();

        // Configuração do JWT
        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(o =>
        {
            o.TokenValidationParameters = new TokenValidationParameters
            {
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SenhaJWT"])),
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true
                //ClokSkew = TimeSpan.ZeroVerificar
            };
        });
        builder.Services.AddAuthorization();

        // Construção da aplicação
        var app = builder.Build();

        // Configuração do ambiente de desenvolvimento
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        // Middlewares
        app.UseHttpsRedirection();
        app.UseCors();
        app.UseAuthentication();
        app.UseAuthorization();

        // Mapeamento dos controladores
        app.MapControllers();

        // Inicialização da aplicação
        app.Run();
    }
}