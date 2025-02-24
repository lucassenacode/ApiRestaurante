namespace ApiRestaurante;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);



        builder.Services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
                options.JsonSerializerOptions.PropertyNamingPolicy = null;
            });


        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();


        builder.Services.AddSingleton<IConfiguration>(builder.Configuration);

        // Registre os repositórios
        builder.Services.AddScoped<ApiRestaurante.Repositories.Repository.IProdutoRepository, ApiRestaurante.Repositories.Repository.ProdutoRepository>();
        builder.Services.AddScoped<ApiRestaurante.Repositories.Repository.IPedidoRepository, ApiRestaurante.Repositories.Repository.PedidoRepository>();
        builder.Services.AddScoped<ApiRestaurante.Repositories.Repository.IItemPedidoRepository, ApiRestaurante.Repositories.Repository.ItemPedidoRepository>();

        // Registre os serviços
        builder.Services.AddScoped<ApiRestaurante.Services.Service.IProdutoService, ApiRestaurante.Services.Service.ProdutoService>();
        builder.Services.AddScoped<ApiRestaurante.Services.Service.IPedidoService, ApiRestaurante.Services.Service.PedidoService>();
        builder.Services.AddScoped<ApiRestaurante.Services.Service.IItemPedidoService, ApiRestaurante.Services.Service.ItemPedidoService>();

        var app = builder.Build();

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