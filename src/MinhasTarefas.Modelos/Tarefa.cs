namespace MinhasTarefas.Modelos;
public class Tarefa
{
    public int Id { get; set; }
    public string? Titulo { get; set; }
    public string? Descricao { get; set; }
    public Prioridade Prioridade { get; set; } // Enum de Prioridade
    public DateTime DataCriacao { get; set; }
    public DateTime? DataConclusao { get; set; }
    public Status Status { get; set; } // Enum de Status

    public int? ResponsavelId { get; set; }
    public Usuario? Responsavel { get; set; }

    public ICollection<Comentario> Comentarios { get; set; } = new List<Comentario>();
    public int? CategoriaId { get; set; }
    public Categoria? Categoria { get; set; }
    public ICollection<SubTarefa> Subtarefas { get; set; } = new List<SubTarefa>();

}
