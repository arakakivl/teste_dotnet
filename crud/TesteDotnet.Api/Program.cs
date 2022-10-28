using Microsoft.EntityFrameworkCore;
using TesteDotnet.Application.Services;
using TesteDotnet.Application.Services.Interfaces;
using TesteDotnet.Domain.Interfaces;
using TesteDotnet.Domain.Interfaces.Repositories;
using TesteDotnet.Infrastructure.Persistence;
using TesteDotnet.Infrastructure.Persistence.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddTransient<IPessoaRepository, PessoaRepository>();
builder.Services.AddTransient<IContatoRepository, ContatoRepository>();
builder.Services.AddTransient<IPessoaService, PessoaService>();
builder.Services.AddTransient<IContatoService, ContatoService>();

builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();

builder.Services.AddDbContext<AppDbContext>(x => x.UseInMemoryDatabase("db"));

builder.Services.AddControllers(x => x.SuppressAsyncSuffixInActionNames = false);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
