namespace MinhasTarefas.Modelos;
public class Comentario
{
    public int Id { get; set; }
    public string? Texto { get; set; }
    public DateTime DataCriacao { get; set; }

    public int AutorId { get; set; }
    public Usuario? Autor { get; set; }
    public int TarefaRelacionadaId { get; set; }
    public Tarefa? TarefaRelacionada { get; set; }

}
