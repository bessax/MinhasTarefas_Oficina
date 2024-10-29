using Microsoft.EntityFrameworkCore;
using MinhasTarefas.API.Converter;
using MinhasTarefas.API.DTO;
using MinhasTarefas.Dados;
using MinhasTarefas.Modelos;

namespace MinhasTarefas.API.Endpoints;

public static class TarefaExtensions
{
    public static void AddEndpointTarefas(this WebApplication app)
    {
        //Endpoint de Tarefa
        app.MapPost("/tarefas", async (TarefaDbContext db, AdicionarTarefaDTO tarefa) =>
        {
            var tarefaModel = AdicionarTarefaDTOConverter.Converter(tarefa);
            db.Tarefas.Add(tarefaModel);
            await db.SaveChangesAsync();
            return Results.Created($"/tarefas/{tarefaModel.Id}", tarefa);
        }).WithTags("Tarefas").WithSummary("Adiciona uma tarefa.").WithOpenApi();

        app.MapGet("/tarefas/{id}", async (TarefaDbContext db, int id) =>
        {
            var tarefa = await db.Tarefas.Include(t => t.Comentarios).Include(t => t.Subtarefas).FirstOrDefaultAsync(t => t.Id == id);
            var tarefaDTO = ListaTarefaDTOConverter.Converter(tarefa!);

            return tarefaDTO != null ? Results.Ok(tarefaDTO) : Results.NotFound();
        }).WithTags("Tarefas").WithSummary("Listagem de tarefas por id").WithOpenApi();

        app.MapGet("/tarefas", async (TarefaDbContext db) => {
            var tarefas = await db.Tarefas.ToListAsync();
            var tarefasDTO = tarefas.Select(ListaTarefaDTOConverter.Converter).ToList();
            return Results.Ok(tarefasDTO);
        }).WithTags("Tarefas").WithSummary("Recupera as tarefas.").WithOpenApi();

        app.MapPut("/tarefas/{id}", async (TarefaDbContext db, int id, AtualizaTarefaDTO tarefaAtualizada) =>
        {
            var tarefa = await db.Tarefas.FindAsync(id);
            if (tarefa == null) return Results.NotFound();

            tarefa.Titulo = tarefaAtualizada.Titulo;
            tarefa.Descricao = tarefaAtualizada.Descricao;
            tarefa.Prioridade = (Prioridade)tarefaAtualizada.Prioridade;
            tarefa.Status = (Status)tarefaAtualizada.Status;
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

    }
}
