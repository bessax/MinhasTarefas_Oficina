using Microsoft.EntityFrameworkCore;
using MinhasTarefas.API.Converter;
using MinhasTarefas.API.DTO;
using MinhasTarefas.API.Endpoints;
using MinhasTarefas.Dados;
using MinhasTarefas.Modelos;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configuração do banco de dados
builder.Services.AddDbContext<TarefaDbContext>(options =>
{
   options.UseInMemoryDatabase("TarefasDb");
});

//Problemas de referência cíclica
builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options => options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//Método para popular o banco de dados SeedData
void SeedData(TarefaDbContext context)
{
    if (!context.Usuarios.Any() && !context.Tarefas.Any())
    {
        // Adiciona usuários
        var usuario1 = new Usuario { Nome = "Alice", Email = "alice@example.com", Senha = "senha123", DataCadastro = DateTime.Now };
        var usuario2 = new Usuario { Nome = "Bob", Email = "bob@example.com", Senha = "senha123", DataCadastro = DateTime.Now };
        var usuario3 = new Usuario { Nome = "Carol", Email = "carol@example.com", Senha = "senha123", DataCadastro = DateTime.Now };

        context.Usuarios.AddRange(usuario1, usuario2, usuario3);

        // Adiciona tarefas
        var tarefa1 = new Tarefa
        {
            Titulo = "Planejar reunião de equipe",
            Descricao = "Organizar a agenda para a reunião semanal de equipe",
            Prioridade = Prioridade.Media,
            DataCriacao = DateTime.Now,
            Status = Status.Aberta,
            Responsavel = usuario1
        };

        var tarefa2 = new Tarefa
        {
            Titulo = "Desenvolver funcionalidades do sistema",
            Descricao = "Implementar a API para o módulo de usuários",
            Prioridade = Prioridade.Alta,
            DataCriacao = DateTime.Now,
            Status = Status.EmProgresso,
            Responsavel = usuario2
        };

        var tarefa3 = new Tarefa
        {
            Titulo = "Testar aplicação",
            Descricao = "Executar testes de unidade e de integração",
            Prioridade = Prioridade.Baixa,
            DataCriacao = DateTime.Now,
            Status = Status.Aberta,
            Responsavel = usuario3
        };

    
        context.Tarefas.AddRange(tarefa1, tarefa2, tarefa3);

        // Salva mudanças no banco de dados
        context.SaveChanges();
    }
}

//Buscando do container de injeção de dependência.

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<TarefaDbContext>();
    SeedData(dbContext);
}

//Adicionando os endpoints
app.AddEndpointTarefas();
app.AddEndpointsUsuario();

app.Run();

