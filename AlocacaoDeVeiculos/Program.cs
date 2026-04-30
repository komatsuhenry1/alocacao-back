using AlocacaoDeVeiculos.Data;
using Microsoft.EntityFrameworkCore;
using AlocacaoDeVeiculos.Services.Cliente;
using AlocacaoDeVeiculos.Services.Veiculo;
using AlocacaoDeVeiculos.Services.Categoria;
using AlocacaoDeVeiculos.Services.Alocacao;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IClienteInterface, ClienteService>();
builder.Services.AddScoped<IVeiculoInterface, VeiculoService>();
builder.Services.AddScoped<IAlocacaoInterface, AlocacaoService>();
builder.Services.AddScoped<ICategoriaInterface, CategoriaService>();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")); // busca config dentro da app settings e caputra o valor da chave 'DefaultConnection'
}
);

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
