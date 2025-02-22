
namespace ApiRestaurante;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddScoped<ApiRestaurante.Services.Service.IProdutoService, ApiRestaurante.Services.Service.ProdutoService>();
        builder.Services.AddScoped<ApiRestaurante.Repositories.Repository.IProdutoRepository, ApiRestaurante.Repositories.Repository.ProdutoRepository>();

        builder.Services.AddScoped<ApiRestaurante.Services.Service.IPedidoService, ApiRestaurante.Services.Service.PedidoService>();
        builder.Services.AddScoped<ApiRestaurante.Repositories.Repository.IPedidoRepository, ApiRestaurante.Repositories.Repository.PedidoRepository>();

        builder.Services.AddScoped<ApiRestaurante.Services.Service.IItemPedidoService, ApiRestaurante.Services.Service.ItemPedidoService>();
        builder.Services.AddScoped<ApiRestaurante.Repositories.Repository.IItemPedidoRepository, ApiRestaurante.Repositories.Repository.ItemPedidoRepository>();
        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}
