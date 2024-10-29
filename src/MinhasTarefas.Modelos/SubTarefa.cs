namespace MinhasTarefas.Modelos;
public class SubTarefa
{
    public int Id { get; set; }
    public string Titulo { get; set; }
    public Status Status { get; set; } // Enum de Status
    public DateTime? DataConclusao { get; set; }

    public int TarefaPrincipalId { get; set; }
    public Tarefa? TarefaPrincipal { get; set; }

}
