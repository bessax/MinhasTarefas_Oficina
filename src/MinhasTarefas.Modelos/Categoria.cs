namespace MinhasTarefas.Modelos;
public class Categoria
{
    public int Id { get; set; }
    public string? Nome { get; set; }
    public string? Descricao { get; set; }

    public ICollection<Tarefa> Tarefas { get; set; } = new List<Tarefa>();

}
