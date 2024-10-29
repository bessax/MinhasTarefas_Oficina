using Microsoft.EntityFrameworkCore;
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

//Endpoint de Usuário
app.MapGet("/usuarios/{id}", async (TarefaDbContext db, int id) =>
{
    var usuario = await db.Usuarios.Include(x=>x.Tarefas).FirstOrDefaultAsync(y=>y.Id==id);
    return usuario != null ? Results.Ok(usuario) : Results.NotFound();
}).WithTags("Usuários").WithSummary("Listagem de usuários").WithOpenApi(); 

app.MapGet("/usuarios", async (TarefaDbContext db) => await db.Usuarios.Include(x=>x.Tarefas).ToListAsync()).WithTags("Usuários").WithSummary("Recupera o usuário por Id.").WithOpenApi();

//Endpoint de Tarefa
app.MapPost("/tarefas", async (TarefaDbContext db, Tarefa tarefa) =>
{
    db.Tarefas.Add(tarefa);
    await db.SaveChangesAsync();
    return Results.Created($"/tarefas/{tarefa.Id}", tarefa);
}).WithTags("Tarefas").WithSummary("Adiciona uma tarefa.").WithOpenApi();

app.MapGet("/tarefas/{id}", async (TarefaDbContext db, int id) =>
{
    var tarefa = await db.Tarefas.Include(t => t.Comentarios).Include(t => t.Subtarefas).FirstOrDefaultAsync(t => t.Id == id);
    return tarefa != null ? Results.Ok(tarefa) : Results.NotFound();
}).WithTags("Tarefas").WithSummary("Listagem de tarefas por id").WithOpenApi();

app.MapGet("/tarefas", async (TarefaDbContext db) => await db.Tarefas.ToListAsync()).WithTags("Tarefas").WithSummary("Recupera as tarefas.").WithOpenApi();

app.MapPut("/tarefas/{id}", async (TarefaDbContext db, int id, Tarefa tarefaAtualizada) =>
{
    var tarefa = await db.Tarefas.FindAsync(id);
    if (tarefa == null) return Results.NotFound();

    tarefa.Titulo = tarefaAtualizada.Titulo;
    tarefa.Descricao = tarefaAtualizada.Descricao;
    tarefa.Prioridade = tarefaAtualizada.Prioridade;
    tarefa.Status = tarefaAtualizada.Status;
    await db.SaveChangesAsync();

    return Results.NoContent();
}).WithTags("Tarefas").WithSummary("Atualiza uma tarefa por Id.").WithOpenApi();

app.MapDelete("/tarefas/{id}", async (TarefaDbContext db, int id) =>
{
    var tarefa = await db.Tarefas.FindAsync(id);
    if (tarefa == null) return Results.NotFound();

    db.Tarefas.Remove(tarefa);
    await db.SaveChangesAsync();

    return Results.NoContent();
}).WithTags("Tarefas").WithSummary("Remove uma tarefa por Id.").WithOpenApi();



app.Run();

