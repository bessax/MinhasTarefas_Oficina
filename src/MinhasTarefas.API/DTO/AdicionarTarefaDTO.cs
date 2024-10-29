namespace MinhasTarefas.API.DTO;

public record AdicionarTarefaDTO
{
    public string? Titulo { get; init; }
    public string? Descricao { get; init; }
    public int Status { get; init; }
    public int Prioridade { get; init; }
    public DateTime DataCriacao { get; init; }
    public DateTime DataConclusao { get; init; }
}
