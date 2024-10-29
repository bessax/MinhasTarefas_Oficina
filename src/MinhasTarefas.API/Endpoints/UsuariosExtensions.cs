using Microsoft.EntityFrameworkCore;
using MinhasTarefas.API.Converter;
using MinhasTarefas.Dados;

namespace MinhasTarefas.API.Endpoints;

public static class UsuariosExtensions
{
   public static void AddEndpointsUsuario(this WebApplication app)
    {
        app.MapGet("/usuarios/{id}", async (TarefaDbContext db, int id) =>
        {
            var usuario = await db.Usuarios.Include(x => x.Tarefas).FirstOrDefaultAsync(y => y.Id == id);
            var usuarioDTO = ListaUsuarioDTOConverter.Converter(usuario!);
            return usuarioDTO != null ? Results.Ok(usuarioDTO) : Results.NotFound();
        }).WithTags("Usuários").WithSummary("Recuper usuário por Id.").WithOpenApi();

        app.MapGet("/usuarios", async (TarefaDbContext db) => {


            var listaUsuarios = await db.Usuarios.Include(x => x.Tarefas).ToListAsync();
            var listaUsuariosDTO = listaUsuarios.Select(ListaUsuarioDTOConverter.Converter).ToList();
            return Results.Ok(listaUsuariosDTO);

        }).WithTags("Usuários").WithSummary("Recupera usuários.").WithOpenApi();
    }
}
