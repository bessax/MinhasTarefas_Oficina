using Microsoft.EntityFrameworkCore;
using MinhasTarefas.Modelos;

namespace MinhasTarefas.Dados;
public class TarefaDbContext:DbContext
{
    public TarefaDbContext(DbContextOptions<TarefaDbContext> options) : base(options) { }

    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Tarefa> Tarefas { get; set; }
    public DbSet<Categoria> Categorias { get; set; }
    public DbSet<Comentario> Comentarios { get; set; }
    public DbSet<SubTarefa> SubTarefas { get; set; }

}
